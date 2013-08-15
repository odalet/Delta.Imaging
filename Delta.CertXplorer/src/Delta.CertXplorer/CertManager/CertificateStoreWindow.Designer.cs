namespace Delta.CertXplorer.CertManager
{
    partial class CertificateStoreWindow
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
            this.certificateStoreListControl1 = new Delta.CertXplorer.CertManager.CertificateStoreListControl();
            this.SuspendLayout();
            // 
            // certificateStoreListControl1
            // 
            this.certificateStoreListControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.certificateStoreListControl1.Location = new System.Drawing.Point(0, 0);
            this.certificateStoreListControl1.Name = "certificateStoreListControl1";
            this.certificateStoreListControl1.Size = new System.Drawing.Size(284, 262);
            this.certificateStoreListControl1.TabIndex = 0;
            // 
            // CertificateStoreToolWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.certificateStoreListControl1);
            this.Name = "CertificateStoreToolWindow";
            this.Text = "CertificateStoreToolWindow";
            this.ResumeLayout(false);

        }

        #endregion

        private Delta.CertXplorer.CertManager.CertificateStoreListControl certificateStoreListControl1;
    }
}