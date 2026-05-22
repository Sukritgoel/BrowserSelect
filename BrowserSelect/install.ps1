# BrowserSelect local installer (PowerShell port of installer.nsi).
# Mirrors the NSIS script exactly: HKCU-only, no admin required.
# Creates 2 files + 1 shortcut + ~10 registry keys, all reversible via uninstall.ps1.

$ErrorActionPreference = "Stop"

$InstallDir   = Join-Path $env:LOCALAPPDATA "BrowserSelect"
$SourceDir    = Join-Path $PSScriptRoot     "bin\Release"
$ShortcutPath = Join-Path $env:APPDATA      "Microsoft\Windows\Start Menu\Programs\BrowserSelect.lnk"

if (-not (Test-Path (Join-Path $SourceDir "BrowserSelect.exe"))) {
    throw "Build output not found at $SourceDir. Run 'msbuild BrowserSelect.csproj -p:Configuration=Release' first."
}

Write-Host "Installing BrowserSelect to $InstallDir"

# 1. Files
New-Item -ItemType Directory -Force -Path $InstallDir | Out-Null
Copy-Item -Force (Join-Path $SourceDir "BrowserSelect.exe")    $InstallDir
Copy-Item -Force (Join-Path $SourceDir "Newtonsoft.Json.dll")  $InstallDir
Copy-Item -Force (Join-Path $PSScriptRoot "..\uninstall.ps1")  $InstallDir -ErrorAction SilentlyContinue

# 2. Start Menu shortcut
$WshShell = New-Object -ComObject WScript.Shell
$Shortcut = $WshShell.CreateShortcut($ShortcutPath)
$Shortcut.TargetPath       = Join-Path $InstallDir "BrowserSelect.exe"
$Shortcut.WorkingDirectory = $InstallDir
$Shortcut.IconLocation     = (Join-Path $InstallDir "BrowserSelect.exe") + ",0"
$Shortcut.Save()

# 3. Registry (HKCU only)
function Set-Default([string]$Path, [string]$Value) {
    if (-not (Test-Path $Path)) { New-Item -Path $Path -Force | Out-Null }
    Set-ItemProperty -Path $Path -Name "(default)" -Value $Value
}
function Set-Named([string]$Path, [string]$Name, [string]$Value) {
    if (-not (Test-Path $Path)) { New-Item -Path $Path -Force | Out-Null }
    New-ItemProperty -Path $Path -Name $Name -Value $Value -PropertyType String -Force | Out-Null
}

$ExePath        = Join-Path $InstallDir "BrowserSelect.exe"
$ExeWithIcon    = "$ExePath,0"
$UninstallCmd   = "powershell.exe -NoProfile -ExecutionPolicy Bypass -File `"$InstallDir\uninstall.ps1`""

# Install dir + ARP entry
Set-Default "HKCU:\Software\BrowserSelect" $InstallDir
Set-Named   "HKCU:\Software\Microsoft\Windows\CurrentVersion\Uninstall\BrowserSelect" "DisplayName"     "BrowserSelect -- select browser dynamically"
Set-Named   "HKCU:\Software\Microsoft\Windows\CurrentVersion\Uninstall\BrowserSelect" "UninstallString" $UninstallCmd

# Register as a browser (StartMenuInternet)
Set-Default "HKCU:\Software\Clients\StartMenuInternet\BROWSERSELECT.EXE"                                "Browser Select"
Set-Named   "HKCU:\Software\Clients\StartMenuInternet\BROWSERSELECT.EXE\Capabilities" "ApplicationName"        "BrowserSelect"
Set-Named   "HKCU:\Software\Clients\StartMenuInternet\BROWSERSELECT.EXE\Capabilities" "ApplicationDescription" "Choose a Browser dynamically."
Set-Named   "HKCU:\Software\Clients\StartMenuInternet\BROWSERSELECT.EXE\Capabilities" "ApplicationIcon"        $ExeWithIcon
Set-Named   "HKCU:\Software\Clients\StartMenuInternet\BROWSERSELECT.EXE\Capabilities\StartMenu"       "StartMenuInternet" "BROWSERSELECT.EXE"
Set-Named   "HKCU:\Software\Clients\StartMenuInternet\BROWSERSELECT.EXE\Capabilities\URLAssociations" "http"  "bselectURL"
Set-Named   "HKCU:\Software\Clients\StartMenuInternet\BROWSERSELECT.EXE\Capabilities\URLAssociations" "https" "bselectURL"
Set-Default "HKCU:\Software\Clients\StartMenuInternet\BROWSERSELECT.EXE\DefaultIcon"          $ExeWithIcon
Set-Default "HKCU:\Software\Clients\StartMenuInternet\BROWSERSELECT.EXE\shell\open\command"   "`"$ExePath`""

# RegisteredApplications pointer (so it appears in Windows Settings > Default Apps)
Set-Named "HKCU:\Software\RegisteredApplications" "BrowserSelect" "Software\Clients\StartMenuInternet\BROWSERSELECT.EXE\Capabilities"

# URL handler ProgId
Set-Default "HKCU:\Software\Classes\bselectURL"                      "BrowserSelect Url"
Set-Default "HKCU:\Software\Classes\bselectURL\shell\open\command"   "`"$ExePath`" `"%1`""

Write-Host ""
Write-Host "Install complete."
Write-Host ""
Write-Host "Next step:"
Write-Host "  Open Windows Settings -> Apps -> Default apps, search 'BrowserSelect',"
Write-Host "  and set it as the default for HTTP, HTTPS, .HTM, and .HTML."
Write-Host ""
Write-Host "To uninstall later: run '$InstallDir\uninstall.ps1' or use Add/Remove Programs."
