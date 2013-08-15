namespace Delta.CertXplorer.ApplicationModel.UI
{
    partial class GeneralOptionsPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneralOptionsPanel));
            this.lcAppearance = new Delta.CertXplorer.UI.LineControl();
            this.label1 = new System.Windows.Forms.Label();
            this.cboTheme = new System.Windows.Forms.ComboBox();
            this.lcLogging = new Delta.CertXplorer.UI.LineControl();
            this.cbEnableLogWindow = new System.Windows.Forms.CheckBox();
            this.pnlTheming = new System.Windows.Forms.Panel();
            this.pnlLogging = new System.Windows.Forms.Panel();
            this.pnlTheming.SuspendLayout();
            this.pnlLogging.SuspendLayout();
            this.SuspendLayout();
            // 
            // lcAppearance
            // 
            this.lcAppearance.AccessibleDescription = null;
            this.lcAppearance.AccessibleName = null;
            resources.ApplyResources(this.lcAppearance, "lcAppearance");
            this.lcAppearance.BackgroundImage = null;
            this.lcAppearance.Font = null;
            this.lcAppearance.Name = "lcAppearance";
            this.lcAppearance.TabStop = false;
            this.lcAppearance.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(128)))), ((int)(((byte)(186)))));
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Font = null;
            this.label1.Name = "label1";
            // 
            // cboTheme
            // 
            this.cboTheme.AccessibleDescription = null;
            this.cboTheme.AccessibleName = null;
            resources.ApplyResources(this.cboTheme, "cboTheme");
            this.cboTheme.BackgroundImage = null;
            this.cboTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTheme.Font = null;
            this.cboTheme.FormattingEnabled = true;
            this.cboTheme.Name = "cboTheme";
            // 
            // lcLogging
            // 
            this.lcLogging.AccessibleDescription = null;
            this.lcLogging.AccessibleName = null;
            resources.ApplyResources(this.lcLogging, "lcLogging");
            this.lcLogging.BackgroundImage = null;
            this.lcLogging.Font = null;
            this.lcLogging.Name = "lcLogging";
            this.lcLogging.TabStop = false;
            this.lcLogging.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(128)))), ((int)(((byte)(186)))));
            // 
            // cbEnableLogWindow
            // 
            this.cbEnableLogWindow.AccessibleDescription = null;
            this.cbEnableLogWindow.AccessibleName = null;
            resources.ApplyResources(this.cbEnableLogWindow, "cbEnableLogWindow");
            this.cbEnableLogWindow.BackgroundImage = null;
            this.cbEnableLogWindow.Font = null;
            this.cbEnableLogWindow.Name = "cbEnableLogWindow";
            this.cbEnableLogWindow.UseVisualStyleBackColor = true;
            // 
            // pnlTheming
            // 
            this.pnlTheming.AccessibleDescription = null;
            this.pnlTheming.AccessibleName = null;
            resources.ApplyResources(this.pnlTheming, "pnlTheming");
            this.pnlTheming.BackgroundImage = null;
            this.pnlTheming.Controls.Add(this.lcAppearance);
            this.pnlTheming.Controls.Add(this.label1);
            this.pnlTheming.Controls.Add(this.cboTheme);
            this.pnlTheming.Font = null;
            this.pnlTheming.Name = "pnlTheming";
            // 
            // pnlLogging
            // 
            this.pnlLogging.AccessibleDescription = null;
            this.pnlLogging.AccessibleName = null;
            resources.ApplyResources(this.pnlLogging, "pnlLogging");
            this.pnlLogging.BackgroundImage = null;
            this.pnlLogging.Controls.Add(this.lcLogging);
            this.pnlLogging.Controls.Add(this.cbEnableLogWindow);
            this.pnlLogging.Font = null;
            this.pnlLogging.Name = "pnlLogging";
            // 
            // GeneralOptionsPanel
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.pnlLogging);
            this.Controls.Add(this.pnlTheming);
            this.Font = null;
            this.Name = "GeneralOptionsPanel";
            this.pnlTheming.ResumeLayout(false);
            this.pnlTheming.PerformLayout();
            this.pnlLogging.ResumeLayout(false);
            this.pnlLogging.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Delta.CertXplorer.UI.LineControl lcAppearance;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboTheme;
        private Delta.CertXplorer.UI.LineControl lcLogging;
        private System.Windows.Forms.CheckBox cbEnableLogWindow;
        private System.Windows.Forms.Panel pnlTheming;
        private System.Windows.Forms.Panel pnlLogging;
    }
}
