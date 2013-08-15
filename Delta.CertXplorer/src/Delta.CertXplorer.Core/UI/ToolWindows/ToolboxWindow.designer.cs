namespace Delta.CertXplorer.UI.ToolWindows
{
    partial class ToolboxWindow
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
            this.toolbox = new Delta.CertXplorer.UI.ToolWindows.ToolboxWindowControl();
            this.SuspendLayout();
            // 
            // toolbox
            // 
            this.toolbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolbox.Location = new System.Drawing.Point(0, 0);
            this.toolbox.Name = "toolbox";
            this.toolbox.Size = new System.Drawing.Size(179, 417);
            this.toolbox.TabIndex = 0;
            // 
            // ToolboxWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(179, 417);
            this.Controls.Add(this.toolbox);
            this.Name = "ToolboxWindow";
            this.Text = "ToolboxWindow";
            this.ResumeLayout(false);

        }

        #endregion

        private ToolboxWindowControl toolbox;
    }
}