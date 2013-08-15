namespace Delta.CertXplorer.Asn1Decoder
{
    partial class DocumentManagerWindow
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
            this.documentManager = new Delta.CertXplorer.Asn1Decoder.DocumentManagerControl();
            this.SuspendLayout();
            // 
            // fileManager
            // 
            this.documentManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentManager.Location = new System.Drawing.Point(0, 0);
            this.documentManager.Name = "documentManager";
            this.documentManager.Size = new System.Drawing.Size(292, 266);
            this.documentManager.TabIndex = 0;
            // 
            // DocumentManagerToolForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionText = "DocumentManagerWindow";
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.documentManager);
            this.Name = "DocumentManagerWindow";
            this.TabText = "FileManagerToolForm";
            this.Text = "FileManagerToolForm";
            this.ResumeLayout(false);
        }

        #endregion

        private DocumentManagerControl documentManager;
    }
}