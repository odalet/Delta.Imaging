namespace Delta.CertXplorer.CertManager
{
    partial class CertificateStoreTreeView
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
            this.mstrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuSetAsDefault = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTestConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDeleteConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRenameConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSaveConnections = new System.Windows.Forms.ToolStripMenuItem();
            this.mstrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mstrip
            // 
            this.mstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSetAsDefault,
            this.mnuAddConnection,
            this.mnuTestConnection,
            this.mnuDeleteConnection,
            this.mnuRenameConnection,
            this.toolStripMenuItem1,
            this.mnuSaveConnections});
            this.mstrip.Name = "mstrip";
            this.mstrip.Size = new System.Drawing.Size(236, 142);
            // 
            // mnuSetAsDefault
            // 
            this.mnuSetAsDefault.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.mnuSetAsDefault.Name = "mnuSetAsDefault";
            this.mnuSetAsDefault.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.mnuSetAsDefault.Size = new System.Drawing.Size(235, 22);
            this.mnuSetAsDefault.Text = "Set as default &connection";
            // 
            // mnuAddConnection
            // 
            this.mnuAddConnection.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.mnuAddConnection.Name = "mnuAddConnection";
            this.mnuAddConnection.Size = new System.Drawing.Size(235, 22);
            this.mnuAddConnection.Text = "&Add new connection...";
            // 
            // mnuTestConnection
            // 
            this.mnuTestConnection.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.mnuTestConnection.Name = "mnuTestConnection";
            this.mnuTestConnection.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.mnuTestConnection.Size = new System.Drawing.Size(235, 22);
            this.mnuTestConnection.Text = "&Test connection";
            // 
            // mnuDeleteConnection
            // 
            this.mnuDeleteConnection.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.mnuDeleteConnection.Name = "mnuDeleteConnection";
            this.mnuDeleteConnection.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.mnuDeleteConnection.Size = new System.Drawing.Size(235, 22);
            this.mnuDeleteConnection.Text = "&Delete connection";
            // 
            // mnuRenameConnection
            // 
            this.mnuRenameConnection.Name = "mnuRenameConnection";
            this.mnuRenameConnection.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.mnuRenameConnection.Size = new System.Drawing.Size(235, 22);
            this.mnuRenameConnection.Text = "&Rename connection";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(232, 6);
            // 
            // mnuSaveConnections
            // 
            this.mnuSaveConnections.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.mnuSaveConnections.Name = "mnuSaveConnections";
            this.mnuSaveConnections.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuSaveConnections.Size = new System.Drawing.Size(235, 22);
            this.mnuSaveConnections.Text = "&Save";
            // 
            // CertificateStoreTreeView
            // 
            this.AllowDrop = false;
            this.LineColor = System.Drawing.Color.Black;
            this.mstrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip mstrip;
        private System.Windows.Forms.ToolStripMenuItem mnuSetAsDefault;
        private System.Windows.Forms.ToolStripMenuItem mnuAddConnection;
        private System.Windows.Forms.ToolStripMenuItem mnuTestConnection;
        private System.Windows.Forms.ToolStripMenuItem mnuDeleteConnection;
        private System.Windows.Forms.ToolStripMenuItem mnuRenameConnection;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuSaveConnections;
    }
}
