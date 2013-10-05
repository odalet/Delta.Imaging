namespace Delta.CertXplorer.CertManager
{
    partial class CertificateListControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CertificateListControl));
            this.listView = new System.Windows.Forms.ListView();
            this.chSubject = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chIssuer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCreationDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chExpirationDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFriendlyName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openCertificateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.largeImages = new System.Windows.Forms.ImageList(this.components);
            this.smallImages = new System.Windows.Forms.ImageList(this.components);
            this.tstrip = new System.Windows.Forms.ToolStrip();
            this.viewDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.tileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.largeIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smallIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actions = new Delta.CertXplorer.UI.Actions.UIActionsManager();
            this.openCertificateAction = new Delta.CertXplorer.UI.Actions.UIAction();
            this.viewInformationAction = new Delta.CertXplorer.UI.Actions.UIAction();
            this.listContextMenu.SuspendLayout();
            this.tstrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.actions)).BeginInit();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chSubject,
            this.chIssuer,
            this.chCreationDate,
            this.chExpirationDate,
            this.chFriendlyName});
            this.listView.ContextMenuStrip = this.listContextMenu;
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.FullRowSelect = true;
            this.listView.HideSelection = false;
            this.listView.LargeImageList = this.largeImages;
            this.listView.Location = new System.Drawing.Point(0, 25);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(859, 343);
            this.listView.SmallImageList = this.smallImages;
            this.listView.TabIndex = 1;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // chSubject
            // 
            this.chSubject.Text = "Issued to";
            this.chSubject.Width = 200;
            // 
            // chIssuer
            // 
            this.chIssuer.Text = "Issued by";
            this.chIssuer.Width = 200;
            // 
            // chCreationDate
            // 
            this.chCreationDate.Text = "Valid from";
            this.chCreationDate.Width = 75;
            // 
            // chExpirationDate
            // 
            this.chExpirationDate.Text = "Valid to";
            this.chExpirationDate.Width = 75;
            // 
            // chFriendlyName
            // 
            this.chFriendlyName.Text = "Friendly name";
            this.chFriendlyName.Width = 150;
            // 
            // listContextMenu
            // 
            this.listContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openCertificateToolStripMenuItem,
            this.viewInformationToolStripMenuItem});
            this.listContextMenu.Name = "listContextMenu";
            this.listContextMenu.Size = new System.Drawing.Size(175, 48);
            // 
            // openCertificateToolStripMenuItem
            // 
            this.actions.SetAction(this.openCertificateToolStripMenuItem, this.openCertificateAction);
            this.openCertificateToolStripMenuItem.Name = "openCertificateToolStripMenuItem";
            this.openCertificateToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.openCertificateToolStripMenuItem.Text = "&Decode...";
            // 
            // viewInformationToolStripMenuItem
            // 
            this.actions.SetAction(this.viewInformationToolStripMenuItem, this.viewInformationAction);
            this.viewInformationToolStripMenuItem.Name = "viewInformationToolStripMenuItem";
            this.viewInformationToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.viewInformationToolStripMenuItem.Text = "&View Information...";
            // 
            // largeImages
            // 
            this.largeImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("largeImages.ImageStream")));
            this.largeImages.TransparentColor = System.Drawing.Color.Transparent;
            this.largeImages.Images.SetKeyName(0, "Certificate32.png");
            this.largeImages.Images.SetKeyName(1, "CertificateWithPrivatKey32.png");
            this.largeImages.Images.SetKeyName(2, "Crl32.png");
            this.largeImages.Images.SetKeyName(3, "Ctl32.png");
            // 
            // smallImages
            // 
            this.smallImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("smallImages.ImageStream")));
            this.smallImages.TransparentColor = System.Drawing.Color.Transparent;
            this.smallImages.Images.SetKeyName(0, "Certificate.png");
            this.smallImages.Images.SetKeyName(1, "CertificateWithPrivatKey.png");
            this.smallImages.Images.SetKeyName(2, "Crl.png");
            this.smallImages.Images.SetKeyName(3, "Ctl.png");
            // 
            // tstrip
            // 
            this.tstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewDropDownButton});
            this.tstrip.Location = new System.Drawing.Point(0, 0);
            this.tstrip.Name = "tstrip";
            this.tstrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.tstrip.Size = new System.Drawing.Size(859, 25);
            this.tstrip.TabIndex = 2;
            this.tstrip.Text = "toolStrip1";
            // 
            // viewDropDownButton
            // 
            this.viewDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.viewDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tileToolStripMenuItem,
            this.largeIconsToolStripMenuItem,
            this.smallIconsToolStripMenuItem,
            this.listToolStripMenuItem,
            this.detailsToolStripMenuItem});
            this.viewDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("viewDropDownButton.Image")));
            this.viewDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.viewDropDownButton.Name = "viewDropDownButton";
            this.viewDropDownButton.Size = new System.Drawing.Size(45, 22);
            this.viewDropDownButton.Text = "View";
            // 
            // tileToolStripMenuItem
            // 
            this.tileToolStripMenuItem.Name = "tileToolStripMenuItem";
            this.tileToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.tileToolStripMenuItem.Text = "Tile";
            // 
            // largeIconsToolStripMenuItem
            // 
            this.largeIconsToolStripMenuItem.Name = "largeIconsToolStripMenuItem";
            this.largeIconsToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.largeIconsToolStripMenuItem.Text = "Large Icons";
            // 
            // smallIconsToolStripMenuItem
            // 
            this.smallIconsToolStripMenuItem.Name = "smallIconsToolStripMenuItem";
            this.smallIconsToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.smallIconsToolStripMenuItem.Text = "Small Icons";
            // 
            // listToolStripMenuItem
            // 
            this.listToolStripMenuItem.Name = "listToolStripMenuItem";
            this.listToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.listToolStripMenuItem.Text = "List";
            // 
            // detailsToolStripMenuItem
            // 
            this.detailsToolStripMenuItem.Name = "detailsToolStripMenuItem";
            this.detailsToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.detailsToolStripMenuItem.Text = "Details";
            // 
            // actions
            // 
            this.actions.Actions.Add(this.openCertificateAction);
            this.actions.Actions.Add(this.viewInformationAction);
            this.actions.ContainerControl = this;
            // 
            // CertificateListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView);
            this.Controls.Add(this.tstrip);
            this.Name = "CertificateListControl";
            this.Size = new System.Drawing.Size(859, 368);
            this.listContextMenu.ResumeLayout(false);
            this.tstrip.ResumeLayout(false);
            this.tstrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.actions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader chSubject;
        private System.Windows.Forms.ColumnHeader chIssuer;
        private System.Windows.Forms.ImageList smallImages;
        private System.Windows.Forms.ImageList largeImages;
        private System.Windows.Forms.ToolStrip tstrip;
        private System.Windows.Forms.ToolStripDropDownButton viewDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem largeIconsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smallIconsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader chFriendlyName;
        private System.Windows.Forms.ColumnHeader chExpirationDate;
        private System.Windows.Forms.ColumnHeader chCreationDate;
        private System.Windows.Forms.ContextMenuStrip listContextMenu;
        private System.Windows.Forms.ToolStripMenuItem openCertificateToolStripMenuItem;
        private Delta.CertXplorer.UI.Actions.UIActionsManager actions;
        private Delta.CertXplorer.UI.Actions.UIAction openCertificateAction;
        private Delta.CertXplorer.UI.Actions.UIAction viewInformationAction;
        private System.Windows.Forms.ToolStripMenuItem viewInformationToolStripMenuItem;

    }
}
