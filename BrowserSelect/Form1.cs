using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BrowserSelect.Properties;
using SHDocVw;

namespace BrowserSelect
{
    public partial class Form1 : Form
    {
        List<Browser> browsers;

        private ButtonsUC buc;

        public Form1()
        {
            InitializeComponent();
        }

        public void updateBrowsers()
        {
            SuspendLayout();
            browsers = BrowserFinder.find().Where(b => !Settings.Default.HideBrowsers.Contains(b.Identifier)).ToList();

            // Remove any previously added BrowserUC rows from the scroll panel.
            for (int k = listPanel.Controls.Count - 1; k >= 0; k--)
            {
                if (listPanel.Controls[k] is BrowserUC)
                    listPanel.Controls.RemoveAt(k);
            }

            int i = 0;
            int rowWidth = 360;
            int rowHeight = 28;
            foreach (var browser in browsers)
            {
                var row = new BrowserUC(browser);
                rowWidth = row.Width;
                rowHeight = row.Height;
                row.Left = 0;
                row.Top = rowHeight * i++;
                row.Click += browser_click;
                listPanel.Controls.Add(row);
            }

            // Cap panel height at workArea - 100 so the scrollbar appears for long lists.
            var workArea = Screen.FromPoint(Cursor.Position).WorkingArea;
            int contentHeight = i * rowHeight;
            int maxHeight = Math.Max(rowHeight, workArea.Height - 100);
            listPanel.Size = new System.Drawing.Size(rowWidth, Math.Min(contentHeight, maxHeight));

            // Sidebar sits to the right of the scroll panel.
            buc.Left = listPanel.Width;
            buc.Top = 0;

            ResumeLayout();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.KeyPreview = true;
            this.Text = Program.url;
            this.Icon = IconExtractor.fromFile(Application.ExecutablePath);

            // Sidebar (Settings / About buttons).
            buc = new ButtonsUC(this);
            this.Controls.Add(buc);

            this.updateBrowsers();
            center_me();
        }

        private void browser_click(object sender, EventArgs e)
        {
            BrowserUC uc;
            if (sender is BrowserUC)
                uc = (BrowserUC)sender;
            else if (((Control)sender).Parent is BrowserUC)
                uc = (BrowserUC)((Control)sender).Parent;
            else
                throw new Exception("this should not happen");

            bool incognito = (ModifierKeys & Keys.Shift) != 0 || (ModifierKeys & Keys.Alt) != 0;
            open_url(uc.browser, incognito);
        }

        public static void open_url(Browser b, bool incognito = false)
        {
            var args = new List<string>();
            if (!string.IsNullOrEmpty(b.additionalArgs))
                args.Add(b.additionalArgs);
            if (incognito)
                args.Add(b.private_arg);
            if (b.exec.ToLower().EndsWith("brave.exe"))
                args.Add("--");
            args.Add(Program.url.Replace("\"", "%22"));

            if (b.exec.EndsWith("iexplore.exe") && !incognito)
            {
                // IE: navigate an existing window if one is open.
                bool found = false;
                ShellWindows iExplorerInstances = new ShellWindows();
                foreach (InternetExplorer iExplorer in iExplorerInstances)
                {
                    if (iExplorer.Name.EndsWith("Internet Explorer"))
                    {
                        iExplorer.Navigate(Program.url, 0x800);
                        ForegroundAgent.RestoreWindow(iExplorer.HWND);
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Process.Start(b.exec, Program.Args2Str(args));
                }
            }
            else
            {
                Process.Start(b.exec, Program.Args2Str(args));
            }
            Application.Exit();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int i = 1;
            foreach (var browser in browsers)
            {
                if (browser.shortcuts.Contains(e.KeyChar) || e.KeyChar == (Convert.ToString(i++))[0])
                {
                    open_url(browser);
                    return;
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
        }

        private void center_me()
        {
            var wa = Screen.FromPoint(new Point(Cursor.Position.X, Cursor.Position.Y)).WorkingArea;
            var left = wa.Width / 2 + wa.Left - Width / 2;
            var top = wa.Height / 2 + wa.Top - Height / 2;

            this.Location = new Point(left, top);
            this.Activate();
        }
    }
}
