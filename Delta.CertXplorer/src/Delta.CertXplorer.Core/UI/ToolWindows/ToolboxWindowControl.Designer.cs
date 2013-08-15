namespace Delta.CertXplorer.UI.ToolWindows
{
    partial class ToolboxWindowControl
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
            this.tbox = new Delta.CertXplorer.UI.ToolboxImplementation.Toolbox();
            this.SuspendLayout();
            // 
            // tbox
            // 
            this.tbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbox.Location = new System.Drawing.Point(0, 0);
            this.tbox.Name = "tbox";
            this.tbox.Size = new System.Drawing.Size(150, 383);
            this.tbox.TabIndex = 0;
            // 
            // ToolboxWindowControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbox);
            this.Name = "ToolboxWindowControl";
            this.Size = new System.Drawing.Size(150, 383);
            this.ResumeLayout(false);

        }

        #endregion

        private Delta.CertXplorer.UI.ToolboxImplementation.Toolbox tbox;

    }
}
