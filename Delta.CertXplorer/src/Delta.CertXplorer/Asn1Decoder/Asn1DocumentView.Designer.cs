namespace Delta.CertXplorer.Asn1Decoder
{
    partial class Asn1DocumentView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Asn1DocumentView));
            this.asn1Viewer = new Delta.CertXplorer.Asn1Decoder.Asn1DocumentControl();
            this.SuspendLayout();
            // 
            // asn1Viewer
            // 
            this.asn1Viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.asn1Viewer.Location = new System.Drawing.Point(0, 0);
            this.asn1Viewer.Name = "asn1Viewer";
            this.asn1Viewer.Size = new System.Drawing.Size(601, 370);
            this.asn1Viewer.TabIndex = 0;
            // 
            // DocumentWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 370);
            this.Controls.Add(this.asn1Viewer);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DocumentWindow";
            this.Text = "DocumentWindow";
            this.ResumeLayout(false);

        }

        #endregion

        private Asn1DocumentControl asn1Viewer;

    }
}