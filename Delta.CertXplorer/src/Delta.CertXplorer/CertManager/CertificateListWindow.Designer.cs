namespace Delta.CertXplorer.CertManager
{
    partial class CertificateListWindow
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
            this.certificateListControl = new Delta.CertXplorer.CertManager.CertificateListControl();
            this.SuspendLayout();
            // 
            // certificateListControl
            // 
            this.certificateListControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.certificateListControl.Location = new System.Drawing.Point(0, 0);
            this.certificateListControl.Name = "certificateListControl";
            this.certificateListControl.Size = new System.Drawing.Size(803, 371);
            this.certificateListControl.TabIndex = 0;
            // 
            // CertificateListWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 371);
            this.Controls.Add(this.certificateListControl);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "CertificateListWindow";
            this.Text = "CertificateListWindow";
            this.ResumeLayout(false);

        }

        #endregion

        private CertificateListControl certificateListControl;
    }
}