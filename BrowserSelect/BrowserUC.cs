using System;
using System.Windows.Forms;

namespace BrowserSelect {
    public partial class BrowserUC : UserControl {
        public Browser browser;
        public BrowserUC(Browser b) {
            InitializeComponent();
            this.browser = b;
            name.Text = b.name;
            icon.Image = b.string2Icon();
            icon.SizeMode = PictureBoxSizeMode.Zoom;
        }
        public new event EventHandler Click {
            add {
                base.Click += value;
                foreach (Control control in Controls) {
                    control.Click += value;
                }
            }
            remove {
                base.Click -= value;
                foreach (Control control in Controls) {
                    control.Click -= value;
                }
            }
        }
    }
}
