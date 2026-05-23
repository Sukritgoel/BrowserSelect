namespace BrowserSelect {
    partial class frm_settings {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btn_setdefault = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.browser_filter = new System.Windows.Forms.CheckedListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chk_check_update = new System.Windows.Forms.CheckBox();
            this.btn_check_update = new System.Windows.Forms.Button();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            //
            // btn_setdefault
            //
            this.btn_setdefault.Location = new System.Drawing.Point(6, 87);
            this.btn_setdefault.Name = "btn_setdefault";
            this.btn_setdefault.Size = new System.Drawing.Size(135, 31);
            this.btn_setdefault.TabIndex = 0;
            this.btn_setdefault.Text = "Set as Default Browser";
            this.btn_setdefault.UseVisualStyleBackColor = true;
            this.btn_setdefault.Click += new System.EventHandler(this.btn_setdefault_Click);
            //
            // label1
            //
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 75);
            this.label1.TabIndex = 1;
            this.label1.Text = "BrowserSelect must be set as default browser for it to function correctly. On Wi" +
    "ndows 10/11 use Settings -> Default apps.";
            //
            // label2
            //
            this.label2.Location = new System.Drawing.Point(12, 316);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 56);
            this.label2.TabIndex = 2;
            this.label2.Text = "If you have feature requests, bug reports or suggestions please submit an issue o" +
    "n the project\'s Github.";
            //
            // groupBox1
            //
            this.groupBox1.Controls.Add(this.browser_filter);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(150, 125);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Browsers";
            //
            // browser_filter
            //
            this.browser_filter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browser_filter.FormattingEnabled = true;
            this.browser_filter.Location = new System.Drawing.Point(3, 16);
            this.browser_filter.Name = "browser_filter";
            this.browser_filter.Size = new System.Drawing.Size(144, 106);
            this.browser_filter.TabIndex = 0;
            this.browser_filter.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.browser_filter_ItemCheck);
            //
            // groupBox2
            //
            this.groupBox2.Controls.Add(this.btn_setdefault);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 143);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(150, 123);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Default Browser";
            //
            // groupBox4
            //
            this.groupBox4.Controls.Add(this.chk_check_update);
            this.groupBox4.Controls.Add(this.btn_check_update);
            this.groupBox4.Location = new System.Drawing.Point(12, 272);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(150, 41);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Update checker";
            //
            // chk_check_update
            //
            this.chk_check_update.AutoSize = true;
            this.chk_check_update.Location = new System.Drawing.Point(12, 16);
            this.chk_check_update.Name = "chk_check_update";
            this.chk_check_update.Size = new System.Drawing.Size(58, 17);
            this.chk_check_update.TabIndex = 1;
            this.chk_check_update.Text = "enable";
            this.chk_check_update.UseVisualStyleBackColor = true;
            this.chk_check_update.CheckedChanged += new System.EventHandler(this.chk_check_update_CheckedChanged);
            //
            // btn_check_update
            //
            this.btn_check_update.Location = new System.Drawing.Point(69, 12);
            this.btn_check_update.Name = "btn_check_update";
            this.btn_check_update.Size = new System.Drawing.Size(75, 23);
            this.btn_check_update.TabIndex = 0;
            this.btn_check_update.Text = "check now";
            this.btn_check_update.UseVisualStyleBackColor = true;
            this.btn_check_update.Click += new System.EventHandler(this.btn_check_update_Click);
            //
            // btn_refresh
            //
            this.btn_refresh.Location = new System.Drawing.Point(104, 7);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(59, 19);
            this.btn_refresh.TabIndex = 2;
            this.btn_refresh.Text = "Refresh";
            this.btn_refresh.UseVisualStyleBackColor = true;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            //
            // frm_settings
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(180, 385);
            this.Controls.Add(this.btn_refresh);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MinimumSize = new System.Drawing.Size(190, 420);
            this.Name = "frm_settings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.frm_settings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_setdefault;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox browser_filter;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chk_check_update;
        private System.Windows.Forms.Button btn_check_update;
        private System.Windows.Forms.Button btn_refresh;
    }
}
