namespace Delta.CertXplorer.About
{
    partial class AboutPluginsControl
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
            this.pluginIcons = new System.Windows.Forms.ImageList(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pluginsListView = new Delta.CertXplorer.UI.ListViewEx(this.components);
            this.SuspendLayout();
            // 
            // pluginIcons
            // 
            this.pluginIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.pluginIcons.ImageSize = new System.Drawing.Size(16, 16);
            this.pluginIcons.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // toolTip
            // 
            this.toolTip.IsBalloon = true;
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip.ToolTipTitle = "Plugin Info:";
            // 
            // pluginsListView
            // 
            this.pluginsListView.ControlPadding = new System.Windows.Forms.Padding(4);
            this.pluginsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pluginsListView.Location = new System.Drawing.Point(0, 0);
            this.pluginsListView.Name = "pluginsListView";
            this.pluginsListView.OwnerDraw = true;
            this.pluginsListView.RowHeight = 16;
            this.pluginsListView.Size = new System.Drawing.Size(286, 96);
            this.pluginsListView.SmallImageList = this.pluginIcons;
            this.pluginsListView.TabIndex = 0;
            this.pluginsListView.UseCompatibleStateImageBehavior = false;
            this.pluginsListView.View = System.Windows.Forms.View.Details;
            this.pluginsListView.SelectedIndexChanged += new System.EventHandler(this.pluginsListView_SelectedIndexChanged);
            // 
            // AboutPluginsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pluginsListView);
            this.Name = "AboutPluginsControl";
            this.Size = new System.Drawing.Size(286, 96);
            this.ResumeLayout(false);

        }

        #endregion

        private Delta.CertXplorer.UI.ListViewEx pluginsListView;
        private System.Windows.Forms.ImageList pluginIcons;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
