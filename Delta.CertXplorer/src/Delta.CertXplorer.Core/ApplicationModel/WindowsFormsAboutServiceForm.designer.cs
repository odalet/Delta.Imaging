namespace Delta.CertXplorer.ApplicationModel
{
    partial class WindowsFormsAboutServiceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowsFormsAboutServiceForm));
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.okButton = new System.Windows.Forms.Button();
            this.pcsLinks = new Delta.CertXplorer.UI.PrintCopySaveLinks();
            this.Tabs = new Delta.CertXplorer.UI.CommunityTabControl();
            this.tpAbout = new System.Windows.Forms.TabPage();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelApplication = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelProductName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelCompanyName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.tpDetails = new System.Windows.Forms.TabPage();
            this.lineControl1 = new Delta.CertXplorer.UI.LineControl();
            this.lvModules = new Delta.CertXplorer.UI.ListViewEx(this.components);
            this.chName = new System.Windows.Forms.ColumnHeader();
            this.chVersion = new System.Windows.Forms.ColumnHeader();
            this.chPath = new System.Windows.Forms.ColumnHeader();
            this.tpCredits = new System.Windows.Forms.TabPage();
            this.tbCredits = new System.Windows.Forms.TextBox();
            this.tpHistory = new System.Windows.Forms.TabPage();
            this.tbHistory = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.Tabs.SuspendLayout();
            this.tpAbout.SuspendLayout();
            this.tpDetails.SuspendLayout();
            this.tpCredits.SuspendLayout();
            this.tpHistory.SuspendLayout();
            this.SuspendLayout();
            // 
            // logoPictureBox
            // 
            resources.ApplyResources(this.logoPictureBox, "logoPictureBox");
            this.logoPictureBox.Image = global::Delta.CertXplorer.Properties.Resources.SplashScreen;
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.TabStop = false;
            // 
            // okButton
            // 
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Name = "okButton";
            // 
            // pcsLinks
            // 
            resources.ApplyResources(this.pcsLinks, "pcsLinks");
            this.pcsLinks.BackColor = System.Drawing.Color.Transparent;
            this.pcsLinks.Name = "pcsLinks";
            this.pcsLinks.ShowCopyLink = true;
            this.pcsLinks.ShowPrintLink = false;
            this.pcsLinks.ShowSaveLink = true;
            this.pcsLinks.CopyClick += new System.EventHandler(this.pcsLinks_CopyClick);
            this.pcsLinks.SaveClick += new System.EventHandler(this.pcsLinks_SaveClick);
            // 
            // Tabs
            // 
            resources.ApplyResources(this.Tabs, "Tabs");
            this.Tabs.Controls.Add(this.tpAbout);
            this.Tabs.Controls.Add(this.tpDetails);
            this.Tabs.Controls.Add(this.tpCredits);
            this.Tabs.Controls.Add(this.tpHistory);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.ShowTabSeparator = true;
            // 
            // tpAbout
            // 
            this.tpAbout.BackColor = System.Drawing.Color.White;
            this.tpAbout.Controls.Add(this.textBoxDescription);
            this.tpAbout.Controls.Add(this.label5);
            this.tpAbout.Controls.Add(this.label7);
            this.tpAbout.Controls.Add(this.label4);
            this.tpAbout.Controls.Add(this.labelApplication);
            this.tpAbout.Controls.Add(this.label3);
            this.tpAbout.Controls.Add(this.labelProductName);
            this.tpAbout.Controls.Add(this.label2);
            this.tpAbout.Controls.Add(this.labelCompanyName);
            this.tpAbout.Controls.Add(this.label1);
            this.tpAbout.Controls.Add(this.labelVersion);
            this.tpAbout.Controls.Add(this.labelCopyright);
            resources.ApplyResources(this.tpAbout, "tpAbout");
            this.tpAbout.Name = "tpAbout";
            // 
            // textBoxDescription
            // 
            resources.ApplyResources(this.textBoxDescription, "textBoxDescription");
            this.textBoxDescription.BackColor = System.Drawing.Color.White;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ReadOnly = true;
            this.textBoxDescription.TabStop = false;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // labelApplication
            // 
            resources.ApplyResources(this.labelApplication, "labelApplication");
            this.labelApplication.Name = "labelApplication";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // labelProductName
            // 
            resources.ApplyResources(this.labelProductName, "labelProductName");
            this.labelProductName.Name = "labelProductName";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // labelCompanyName
            // 
            resources.ApplyResources(this.labelCompanyName, "labelCompanyName");
            this.labelCompanyName.Name = "labelCompanyName";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // labelVersion
            // 
            resources.ApplyResources(this.labelVersion, "labelVersion");
            this.labelVersion.Name = "labelVersion";
            // 
            // labelCopyright
            // 
            resources.ApplyResources(this.labelCopyright, "labelCopyright");
            this.labelCopyright.Name = "labelCopyright";
            // 
            // tpDetails
            // 
            this.tpDetails.BackColor = System.Drawing.Color.White;
            this.tpDetails.Controls.Add(this.lineControl1);
            this.tpDetails.Controls.Add(this.lvModules);
            resources.ApplyResources(this.tpDetails, "tpDetails");
            this.tpDetails.Name = "tpDetails";
            // 
            // lineControl1
            // 
            resources.ApplyResources(this.lineControl1, "lineControl1");
            this.lineControl1.Name = "lineControl1";
            this.lineControl1.TabStop = false;
            this.lineControl1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(128)))), ((int)(((byte)(186)))));
            // 
            // lvModules
            // 
            resources.ApplyResources(this.lvModules, "lvModules");
            this.lvModules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chVersion,
            this.chPath});
            this.lvModules.ControlPadding = new System.Windows.Forms.Padding(4);
            this.lvModules.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvModules.HideSelection = false;
            this.lvModules.Name = "lvModules";
            this.lvModules.OwnerDraw = true;
            this.lvModules.RowHeight = 16;
            this.lvModules.SortColumnOnClick = false;
            this.lvModules.UseCompatibleStateImageBehavior = false;
            this.lvModules.View = System.Windows.Forms.View.Details;
            // 
            // chName
            // 
            resources.ApplyResources(this.chName, "chName");
            // 
            // chVersion
            // 
            resources.ApplyResources(this.chVersion, "chVersion");
            // 
            // chPath
            // 
            resources.ApplyResources(this.chPath, "chPath");
            // 
            // tpCredits
            // 
            this.tpCredits.Controls.Add(this.tbCredits);
            resources.ApplyResources(this.tpCredits, "tpCredits");
            this.tpCredits.Name = "tpCredits";
            this.tpCredits.UseVisualStyleBackColor = true;
            // 
            // tbCredits
            // 
            this.tbCredits.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.tbCredits, "tbCredits");
            this.tbCredits.Name = "tbCredits";
            this.tbCredits.ReadOnly = true;
            this.tbCredits.TabStop = false;
            // 
            // tpHistory
            // 
            this.tpHistory.Controls.Add(this.tbHistory);
            resources.ApplyResources(this.tpHistory, "tpHistory");
            this.tpHistory.Name = "tpHistory";
            this.tpHistory.UseVisualStyleBackColor = true;
            // 
            // tbHistory
            // 
            this.tbHistory.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.tbHistory, "tbHistory");
            this.tbHistory.Name = "tbHistory";
            this.tbHistory.ReadOnly = true;
            this.tbHistory.TabStop = false;
            // 
            // WindowsFormsAboutServiceForm
            // 
            this.AcceptButton = this.okButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.okButton;
            this.Controls.Add(this.pcsLinks);
            this.Controls.Add(this.Tabs);
            this.Controls.Add(this.logoPictureBox);
            this.Controls.Add(this.okButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WindowsFormsAboutServiceForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.Tabs.ResumeLayout(false);
            this.tpAbout.ResumeLayout(false);
            this.tpAbout.PerformLayout();
            this.tpDetails.ResumeLayout(false);
            this.tpCredits.ResumeLayout(false);
            this.tpCredits.PerformLayout();
            this.tpHistory.ResumeLayout(false);
            this.tpHistory.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Label labelCompanyName;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.TabPage tpAbout;
        private System.Windows.Forms.TabPage tpDetails;
        private Delta.CertXplorer.UI.PrintCopySaveLinks pcsLinks;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelApplication;
        private Delta.CertXplorer.UI.LineControl lineControl1;
        private Delta.CertXplorer.UI.ListViewEx lvModules;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chVersion;
        private System.Windows.Forms.ColumnHeader chPath;
        private System.Windows.Forms.TabPage tpCredits;
        private System.Windows.Forms.TextBox tbCredits;
        protected Delta.CertXplorer.UI.CommunityTabControl Tabs;
        private System.Windows.Forms.TabPage tpHistory;
        private System.Windows.Forms.TextBox tbHistory;
    }
}
