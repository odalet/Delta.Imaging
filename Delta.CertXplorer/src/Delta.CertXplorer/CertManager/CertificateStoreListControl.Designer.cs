namespace Delta.CertXplorer.CertManager
{
    partial class CertificateStoreListControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CertificateStoreListControl));
            this.tstrip = new System.Windows.Forms.ToolStrip();
            this.refreshToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.tvStores = new Delta.CertXplorer.CertManager.CertificateStoreTreeView();
            this.images = new System.Windows.Forms.ImageList(this.components);
            this.tstrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tstrip
            // 
            this.tstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripButton});
            this.tstrip.Location = new System.Drawing.Point(0, 0);
            this.tstrip.Name = "tstrip";
            this.tstrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.tstrip.Size = new System.Drawing.Size(281, 25);
            this.tstrip.TabIndex = 0;
            this.tstrip.Text = "toolStrip1";
            // 
            // refreshToolStripButton
            // 
            this.refreshToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshToolStripButton.Image = global::Delta.CertXplorer.Properties.Resources.Refresh;
            this.refreshToolStripButton.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.refreshToolStripButton.Name = "refreshToolStripButton";
            this.refreshToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.refreshToolStripButton.Text = "TEST";
            this.refreshToolStripButton.ToolTipText = "TEST";
            this.refreshToolStripButton.Visible = false;
            this.refreshToolStripButton.Click += new System.EventHandler(this.refreshToolStripButton_Click);
            // 
            // tvStores
            // 
            this.tvStores.AllowDrop = true;
            this.tvStores.CanClearSelection = false;
            this.tvStores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvStores.FullRowSelect = true;
            this.tvStores.HideSelection = false;
            this.tvStores.ImageIndex = 0;
            this.tvStores.ImageList = this.images;
            this.tvStores.LabelEdit = true;
            this.tvStores.Location = new System.Drawing.Point(0, 25);
            this.tvStores.Name = "tvStores";
            this.tvStores.SelectedImageIndex = 0;
            this.tvStores.ShowRootLines = false;
            this.tvStores.Size = new System.Drawing.Size(281, 459);
            this.tvStores.Sorted = false;
            this.tvStores.TabIndex = 1;
            // 
            // images
            // 
            this.images.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("images.ImageStream")));
            this.images.TransparentColor = System.Drawing.Color.Transparent;
            this.images.Images.SetKeyName(0, "Certificates.png");
            this.images.Images.SetKeyName(1, "ClosedFolder.png");
            this.images.Images.SetKeyName(2, "OpenedFolder.png");
            // 
            // CertificateStoreListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvStores);
            this.Controls.Add(this.tstrip);
            this.Name = "CertificateStoreListControl";
            this.Size = new System.Drawing.Size(281, 484);
            this.tstrip.ResumeLayout(false);
            this.tstrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tstrip;
        private System.Windows.Forms.ToolStripButton refreshToolStripButton;
        private Delta.CertXplorer.CertManager.CertificateStoreTreeView tvStores;
        private System.Windows.Forms.ImageList images;
    }
}
