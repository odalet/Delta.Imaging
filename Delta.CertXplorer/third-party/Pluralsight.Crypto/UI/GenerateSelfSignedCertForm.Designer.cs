//
// This code was written by Keith Brown, and may be freely used.
// Want to learn more about .NET? Visit pluralsight.com today!
//
namespace Pluralsight.Crypto.UI
{
    partial class GenerateSelfSignedCertForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenerateSelfSignedCertForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lnkKeithRecommends = new System.Windows.Forms.LinkLabel();
            this.dtpValidTo = new System.Windows.Forms.DateTimePicker();
            this.dtpValidFrom = new System.Windows.Forms.DateTimePicker();
            this.cboKeySize = new System.Windows.Forms.ComboBox();
            this.txtDN = new System.Windows.Forms.TextBox();
            this.chkExportablePrivateKey = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnSaveAsPFX = new System.Windows.Forms.Button();
            this.btnSaveToCertStore = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cboStoreName = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboStoreLocation = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lnkTitle = new System.Windows.Forms.LinkLabel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.lnkAbout = new System.Windows.Forms.LinkLabel();
            this.lnkVersion = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lnkKeithRecommends);
            this.groupBox1.Controls.Add(this.dtpValidTo);
            this.groupBox1.Controls.Add(this.dtpValidFrom);
            this.groupBox1.Controls.Add(this.cboKeySize);
            this.groupBox1.Controls.Add(this.txtDN);
            this.groupBox1.Controls.Add(this.chkExportablePrivateKey);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(533, 165);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "certificate info";
            // 
            // lnkKeithRecommends
            // 
            this.lnkKeithRecommends.AutoSize = true;
            this.lnkKeithRecommends.LinkArea = new System.Windows.Forms.LinkArea(0, 5);
            this.lnkKeithRecommends.Location = new System.Drawing.Point(286, 57);
            this.lnkKeithRecommends.Name = "lnkKeithRecommends";
            this.lnkKeithRecommends.Size = new System.Drawing.Size(182, 17);
            this.lnkKeithRecommends.TabIndex = 5;
            this.lnkKeithRecommends.TabStop = true;
            this.lnkKeithRecommends.Text = "Keith recommends 2048 or greater!";
            this.lnkKeithRecommends.UseCompatibleTextRendering = true;
            this.lnkKeithRecommends.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkKeithRecommends_LinkClicked);
            // 
            // dtpValidTo
            // 
            this.dtpValidTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpValidTo.Location = new System.Drawing.Point(165, 110);
            this.dtpValidTo.Name = "dtpValidTo";
            this.dtpValidTo.Size = new System.Drawing.Size(94, 20);
            this.dtpValidTo.TabIndex = 4;
            // 
            // dtpValidFrom
            // 
            this.dtpValidFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpValidFrom.Location = new System.Drawing.Point(165, 81);
            this.dtpValidFrom.Name = "dtpValidFrom";
            this.dtpValidFrom.Size = new System.Drawing.Size(94, 20);
            this.dtpValidFrom.TabIndex = 4;
            // 
            // cboKeySize
            // 
            this.cboKeySize.FormattingEnabled = true;
            this.cboKeySize.Items.AddRange(new object[] {
            "384",
            "512",
            "1024",
            "2048",
            "4096",
            "8192",
            "16384"});
            this.cboKeySize.Location = new System.Drawing.Point(165, 54);
            this.cboKeySize.Name = "cboKeySize";
            this.cboKeySize.Size = new System.Drawing.Size(94, 21);
            this.cboKeySize.TabIndex = 3;
            this.toolTip.SetToolTip(this.cboKeySize, "This should be a valid RSA key size.");
            this.cboKeySize.Validating += new System.ComponentModel.CancelEventHandler(this.cboKeySize_Validating);
            // 
            // txtDN
            // 
            this.txtDN.Location = new System.Drawing.Point(165, 27);
            this.txtDN.Name = "txtDN";
            this.txtDN.Size = new System.Drawing.Size(335, 20);
            this.txtDN.TabIndex = 2;
            this.toolTip.SetToolTip(this.txtDN, "This must be a distinguished name (DN) such as \"cn=test\" or, \"cn=foo,o=pluralsigh" +
        "t\", etc.");
            this.txtDN.Validating += new System.ComponentModel.CancelEventHandler(this.txtDN_Validating);
            // 
            // chkExportablePrivateKey
            // 
            this.chkExportablePrivateKey.AutoSize = true;
            this.chkExportablePrivateKey.Location = new System.Drawing.Point(165, 137);
            this.chkExportablePrivateKey.Name = "chkExportablePrivateKey";
            this.chkExportablePrivateKey.Size = new System.Drawing.Size(309, 17);
            this.chkExportablePrivateKey.TabIndex = 1;
            this.chkExportablePrivateKey.Text = "Exportable private key (currently broken - always exportable)";
            this.toolTip.SetToolTip(this.chkExportablePrivateKey, "Check this box if you want to move the certificate once you install it (this is i" +
        "gnored if you export a PFX file).");
            this.chkExportablePrivateKey.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(114, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Valid to:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(105, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Vald from:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(85, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Key size (bits):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "X.500 distinguished name:";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // btnSaveAsPFX
            // 
            this.btnSaveAsPFX.Location = new System.Drawing.Point(41, 43);
            this.btnSaveAsPFX.Name = "btnSaveAsPFX";
            this.btnSaveAsPFX.Size = new System.Drawing.Size(147, 23);
            this.btnSaveAsPFX.TabIndex = 2;
            this.btnSaveAsPFX.Text = "Save to PFX file";
            this.btnSaveAsPFX.UseVisualStyleBackColor = true;
            this.btnSaveAsPFX.Click += new System.EventHandler(this.btnSaveAsPFX_Click);
            // 
            // btnSaveToCertStore
            // 
            this.btnSaveToCertStore.Location = new System.Drawing.Point(198, 23);
            this.btnSaveToCertStore.Name = "btnSaveToCertStore";
            this.btnSaveToCertStore.Size = new System.Drawing.Size(53, 37);
            this.btnSaveToCertStore.TabIndex = 2;
            this.btnSaveToCertStore.Text = "Save";
            this.btnSaveToCertStore.UseVisualStyleBackColor = true;
            this.btnSaveToCertStore.Click += new System.EventHandler(this.btnSaveToCertStore_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtPassword);
            this.groupBox2.Controls.Add(this.btnSaveAsPFX);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(13, 265);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(228, 76);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "save as PFX";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(68, 17);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(131, 20);
            this.txtPassword.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Password:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cboStoreName);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.cboStoreLocation);
            this.groupBox3.Controls.Add(this.btnSaveToCertStore);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(247, 265);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(298, 76);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "save to cert store";
            // 
            // cboStoreName
            // 
            this.cboStoreName.FormattingEnabled = true;
            this.cboStoreName.Items.AddRange(new object[] {
            "LocalMachine",
            "CurrentUser"});
            this.cboStoreName.Location = new System.Drawing.Point(74, 45);
            this.cboStoreName.Name = "cboStoreName";
            this.cboStoreName.Size = new System.Drawing.Size(105, 21);
            this.cboStoreName.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(33, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Store:";
            // 
            // cboStoreLocation
            // 
            this.cboStoreLocation.FormattingEnabled = true;
            this.cboStoreLocation.Location = new System.Drawing.Point(74, 17);
            this.cboStoreLocation.Name = "cboStoreLocation";
            this.cboStoreLocation.Size = new System.Drawing.Size(105, 21);
            this.cboStoreLocation.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Location:";
            // 
            // lnkTitle
            // 
            this.lnkTitle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnkTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lnkTitle.Image = ((System.Drawing.Image)(resources.GetObject("lnkTitle.Image")));
            this.lnkTitle.Location = new System.Drawing.Point(0, 0);
            this.lnkTitle.Name = "lnkTitle";
            this.lnkTitle.Size = new System.Drawing.Size(557, 90);
            this.lnkTitle.TabIndex = 3;
            this.lnkTitle.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lnkTitle_LinkClicked);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(470, 452);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(141, 361);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(216, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "It is offered free and without warranty. Enjoy!";
            // 
            // lnkAbout
            // 
            this.lnkAbout.AutoSize = true;
            this.lnkAbout.LinkArea = new System.Windows.Forms.LinkArea(0, 9);
            this.lnkAbout.Location = new System.Drawing.Point(20, 344);
            this.lnkAbout.Name = "lnkAbout";
            this.lnkAbout.Size = new System.Drawing.Size(517, 17);
            this.lnkAbout.TabIndex = 8;
            this.lnkAbout.TabStop = true;
            this.lnkAbout.Text = "This tool is designed to help you create self-signed certificates for use with SS" +
    "L and other applications.";
            this.lnkAbout.UseCompatibleTextRendering = true;
            this.lnkAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkAbout_LinkClicked);
            // 
            // lnkVersion
            // 
            this.lnkVersion.AutoSize = true;
            this.lnkVersion.Location = new System.Drawing.Point(482, 364);
            this.lnkVersion.Name = "lnkVersion";
            this.lnkVersion.Size = new System.Drawing.Size(47, 13);
            this.lnkVersion.TabIndex = 9;
            this.lnkVersion.TabStop = true;
            this.lnkVersion.Text = "(version)";
            this.lnkVersion.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkVersion_LinkClicked);
            // 
            // GenerateSelfSignedCertForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(557, 386);
            this.Controls.Add(this.lnkVersion);
            this.Controls.Add(this.lnkAbout);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lnkTitle);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GenerateSelfSignedCertForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pluralsight\'s Self-Cert";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkExportablePrivateKey;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboKeySize;
        private System.Windows.Forms.TextBox txtDN;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Button btnSaveAsPFX;
        private System.Windows.Forms.LinkLabel lnkTitle;
        private System.Windows.Forms.Button btnSaveToCertStore;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpValidTo;
        private System.Windows.Forms.DateTimePicker dtpValidFrom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel lnkKeithRecommends;
        private System.Windows.Forms.ComboBox cboStoreName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboStoreLocation;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.LinkLabel lnkAbout;
        private System.Windows.Forms.LinkLabel lnkVersion;
    }
}

