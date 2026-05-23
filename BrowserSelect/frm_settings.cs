using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BrowserSelect.Properties;
using Microsoft.Win32;

namespace BrowserSelect
{
    public partial class frm_settings : Form
    {
        public Form1 mainForm;

        public frm_settings(Form mainForm)
        {
            this.mainForm = (Form1)mainForm;
            InitializeComponent();
        }

        private void frm_settings_Load(object sender, EventArgs e)
        {
            // disable the "Set as default" button if BrowserSelect is already default for http
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice"))
            {
                var default_browser = key?.GetValue("ProgId");
                if (default_browser != null && (string)default_browser == "bselectURL")
                    btn_setdefault.Enabled = false;
            }

            // populate browser hide/show checklist
            var browsers = BrowserFinder.find();
            foreach (Browser b in browsers)
            {
                browser_filter.Items.Add(b, !Settings.Default.HideBrowsers.Contains(b.Identifier));
            }

            chk_check_update.Checked = Settings.Default.check_update != "nope";
        }

        private void btn_setdefault_Click(object sender, EventArgs e)
        {
            // Note: on Windows 10/11 the UserChoice key is hash-protected; this write
            // typically gets reverted. Use Windows Settings -> Default apps instead.
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(
                    @"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice"))
            {
                key.SetValue("ProgId", "bselectURL");
            }
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(
                    @"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\https\UserChoice"))
            {
                key.SetValue("ProgId", "bselectURL");
            }
            btn_setdefault.Enabled = false;
        }

        private void browser_filter_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
                Settings.Default.HideBrowsers.Remove(((Browser)browser_filter.Items[e.Index]).Identifier);
            else
                Settings.Default.HideBrowsers.Add(((Browser)browser_filter.Items[e.Index]).Identifier);
            Settings.Default.Save();
        }

        private void btn_check_update_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Update checks have been removed from this build. No network requests will be made.");
        }

        private void chk_check_update_CheckedChanged(object sender, EventArgs e)
        {
            // Update checks are removed from this build; the setting is preserved
            // for backward compatibility but has no effect.
            Settings.Default.check_update = "nope";
            Settings.Default.Save();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            List<Browser> browsers = BrowserFinder.find(true);
            browser_filter.Items.Clear();
            foreach (Browser b in browsers)
            {
                browser_filter.Items.Add(b, !Settings.Default.HideBrowsers.Contains(b.Identifier));
            }
            this.mainForm.updateBrowsers();
        }
    }
}
