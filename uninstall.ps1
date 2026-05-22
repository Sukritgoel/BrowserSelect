# BrowserSelect uninstaller. Reverses everything install.ps1 created.
# HKCU-only, no admin required.

$ErrorActionPreference = "Continue"

$InstallDir   = Join-Path $env:LOCALAPPDATA "BrowserSelect"
$ShortcutPath = Join-Path $env:APPDATA      "Microsoft\Windows\Start Menu\Programs\BrowserSelect.lnk"

Write-Host "Uninstalling BrowserSelect..."

# Registry cleanup
$keysToDelete = @(
    "HKCU:\Software\BrowserSelect",
    "HKCU:\Software\Microsoft\Windows\CurrentVersion\Uninstall\BrowserSelect",
    "HKCU:\Software\Clients\StartMenuInternet\BROWSERSELECT.EXE",
    "HKCU:\Software\Classes\bselectURL"
)
foreach ($k in $keysToDelete) {
    if (Test-Path $k) {
        Remove-Item -Path $k -Recurse -Force
        Write-Host "  removed $k"
    }
}

# Remove from RegisteredApplications
$regApps = "HKCU:\Software\RegisteredApplications"
if (Test-Path $regApps) {
    Remove-ItemProperty -Path $regApps -Name "BrowserSelect" -ErrorAction SilentlyContinue
    Write-Host "  removed RegisteredApplications\BrowserSelect value"
}

# Shortcut
if (Test-Path $ShortcutPath) {
    Remove-Item -Force $ShortcutPath
    Write-Host "  removed Start Menu shortcut"
}

# Settings file (the official NSIS uninstaller leaves these behind; we clean them up)
Get-ChildItem $env:LOCALAPPDATA -Filter "BrowserSelect*" -Directory -ErrorAction SilentlyContinue |
    Where-Object { $_.Name -match "^BrowserSelect" } |
    ForEach-Object {
        Remove-Item -Recurse -Force $_.FullName -ErrorAction SilentlyContinue
        Write-Host "  removed $($_.FullName)"
    }

Write-Host ""
Write-Host "Uninstall complete."
Write-Host "If BrowserSelect was set as your default browser, Windows may have already"
Write-Host "reverted to its previous default; check Settings -> Apps -> Default apps."
