namespace Delta.CertXplorer.CertManager
{
    partial class PropertiesWindow
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
            this.propertiesControl = new Delta.CertXplorer.CertManager.PropertiesControl();
            this.SuspendLayout();
            // 
            // propertiesControl
            // 
            this.propertiesControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertiesControl.Location = new System.Drawing.Point(0, 0);
            this.propertiesControl.Name = "propertiesControl";
            this.propertiesControl.Size = new System.Drawing.Size(292, 266);
            this.propertiesControl.TabIndex = 0;
            // 
            // PropertiesWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionText = "PropertiesWindow";
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.propertiesControl);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "PropertiesWindow";
            this.TabText = "PropertiesWindow";
            this.Text = "PropertiesWindow";
            this.ResumeLayout(false);

        }

        #endregion

        private Delta.CertXplorer.CertManager.PropertiesControl propertiesControl;
    }
}