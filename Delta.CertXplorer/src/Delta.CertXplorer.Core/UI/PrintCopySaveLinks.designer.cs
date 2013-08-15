namespace Delta.CertXplorer.UI
{
    partial class PrintCopySaveLinks
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintCopySaveLinks));
            this.il = new System.Windows.Forms.ImageList(this.components);
            this.llCopy = new System.Windows.Forms.LinkLabel();
            this.llSave = new System.Windows.Forms.LinkLabel();
            this.llPrint = new System.Windows.Forms.LinkLabel();
            this.flowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.flowPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // il
            // 
            this.il.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il.ImageStream")));
            this.il.TransparentColor = System.Drawing.Color.Fuchsia;
            this.il.Images.SetKeyName(0, "Print.bmp");
            this.il.Images.SetKeyName(1, "SaveFile.bmp");
            this.il.Images.SetKeyName(2, "Copy.bmp");
            // 
            // llCopy
            // 
            this.llCopy.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(162)))), ((int)(((byte)(77)))), ((int)(((byte)(103)))));
            resources.ApplyResources(this.llCopy, "llCopy");
            this.llCopy.BackColor = System.Drawing.Color.Transparent;
            this.llCopy.ImageList = this.il;
            this.llCopy.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(103)))), ((int)(((byte)(162)))));
            this.llCopy.Name = "llCopy";
            this.llCopy.TabStop = true;
            // 
            // llSave
            // 
            this.llSave.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(162)))), ((int)(((byte)(77)))), ((int)(((byte)(103)))));
            resources.ApplyResources(this.llSave, "llSave");
            this.llSave.BackColor = System.Drawing.Color.Transparent;
            this.llSave.ImageList = this.il;
            this.llSave.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(103)))), ((int)(((byte)(162)))));
            this.llSave.Name = "llSave";
            this.llSave.TabStop = true;
            // 
            // llPrint
            // 
            this.llPrint.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(162)))), ((int)(((byte)(77)))), ((int)(((byte)(103)))));
            resources.ApplyResources(this.llPrint, "llPrint");
            this.llPrint.BackColor = System.Drawing.Color.Transparent;
            this.llPrint.ImageList = this.il;
            this.llPrint.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(103)))), ((int)(((byte)(162)))));
            this.llPrint.Name = "llPrint";
            this.llPrint.TabStop = true;
            // 
            // flowPanel
            // 
            this.flowPanel.BackColor = System.Drawing.Color.Transparent;
            this.flowPanel.Controls.Add(this.llSave);
            this.flowPanel.Controls.Add(this.llPrint);
            this.flowPanel.Controls.Add(this.llCopy);
            resources.ApplyResources(this.flowPanel, "flowPanel");
            this.flowPanel.Name = "flowPanel";
            // 
            // PrintCopySaveLinks
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.flowPanel);
            this.Name = "PrintCopySaveLinks";
            this.flowPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.LinkLabel llCopy;
        private System.Windows.Forms.LinkLabel llPrint;
        private System.Windows.Forms.LinkLabel llSave;
        private System.Windows.Forms.ImageList il;
        private System.Windows.Forms.FlowLayoutPanel flowPanel;
    }
}
