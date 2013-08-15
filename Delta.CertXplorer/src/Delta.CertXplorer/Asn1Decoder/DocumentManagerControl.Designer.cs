namespace Delta.CertXplorer.Asn1Decoder
{
    partial class DocumentManagerControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentManagerControl));
            this.tvExplorer = new Delta.CertXplorer.UI.TreeViewEx();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.panel = new System.Windows.Forms.Panel();
            this.mstrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actions = new Delta.CertXplorer.UI.Actions.UIActionsManager();
            this.openAction = new Delta.CertXplorer.UI.Actions.UIAction();
            this.closeAction = new Delta.CertXplorer.UI.Actions.UIAction();
            this.panel.SuspendLayout();
            this.mstrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.actions)).BeginInit();
            this.SuspendLayout();
            // 
            // tvExplorer
            // 
            this.tvExplorer.AllowDrop = true;
            this.tvExplorer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvExplorer.CanClearSelection = false;
            this.tvExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvExplorer.FullRowSelect = true;
            this.tvExplorer.HideSelection = false;
            this.tvExplorer.ImageIndex = 0;
            this.tvExplorer.ImageList = this.imageList;
            this.tvExplorer.Location = new System.Drawing.Point(2, 2);
            this.tvExplorer.Name = "tvExplorer";
            this.tvExplorer.SelectedImageIndex = 0;
            this.tvExplorer.ShowRootLines = false;
            this.tvExplorer.Size = new System.Drawing.Size(397, 397);
            this.tvExplorer.TabIndex = 0;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList.Images.SetKeyName(0, "Documents.png");
            this.imageList.Images.SetKeyName(1, "ClosedFolder.png");
            this.imageList.Images.SetKeyName(2, "OpenedFolder.png");
            this.imageList.Images.SetKeyName(3, "Document.png");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(2, 399);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Drop Files from Windows Explorer";
            // 
            // panel
            // 
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel.Controls.Add(this.tvExplorer);
            this.panel.Controls.Add(this.label1);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Padding = new System.Windows.Forms.Padding(2);
            this.panel.Size = new System.Drawing.Size(403, 416);
            this.panel.TabIndex = 2;
            // 
            // mstrip
            // 
            this.mstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.mstrip.Name = "mstrip";
            this.mstrip.Size = new System.Drawing.Size(113, 48);
            // 
            // openToolStripMenuItem
            // 
            this.actions.SetAction(this.openToolStripMenuItem, this.openAction);
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.openToolStripMenuItem.Text = "&Open...";
            // 
            // closeToolStripMenuItem
            // 
            this.actions.SetAction(this.closeToolStripMenuItem, this.closeAction);
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.closeToolStripMenuItem.Text = "&Close";
            // 
            // actions
            // 
            this.actions.Actions.Add(this.openAction);
            this.actions.Actions.Add(this.closeAction);
            this.actions.ContainerControl = this;
            // 
            // DocumentManagerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.panel);
            this.Name = "DocumentManagerControl";
            this.Size = new System.Drawing.Size(403, 416);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.mstrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.actions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Delta.CertXplorer.UI.TreeViewEx tvExplorer;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.ContextMenuStrip mstrip;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private Delta.CertXplorer.UI.Actions.UIActionsManager actions;
        private Delta.CertXplorer.UI.Actions.UIAction openAction;
        private Delta.CertXplorer.UI.Actions.UIAction closeAction;
    }
}
