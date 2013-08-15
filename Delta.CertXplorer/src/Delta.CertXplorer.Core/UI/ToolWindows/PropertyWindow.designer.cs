namespace Delta.CertXplorer.UI.ToolWindows
{
    partial class PropertyWindow
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
            this.propertyControl = new Delta.CertXplorer.UI.ToolWindows.PropertyWindowControl();
            this.SuspendLayout();
            // 
            // propertyControl
            // 
            this.propertyControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyControl.Location = new System.Drawing.Point(0, 0);
            this.propertyControl.Name = "propertyControl";
            this.propertyControl.Size = new System.Drawing.Size(292, 266);
            this.propertyControl.TabIndex = 0;
            // 
            // PropertyWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.propertyControl);
            this.Name = "PropertyWindow";
            this.TabText = "PropertyWindow";
            this.Text = "PropertyWindow";
            this.ResumeLayout(false);

        }

        #endregion

        private PropertyWindowControl propertyControl;
    }
}