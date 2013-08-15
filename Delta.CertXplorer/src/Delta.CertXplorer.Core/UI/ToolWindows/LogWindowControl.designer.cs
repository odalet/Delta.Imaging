namespace Delta.CertXplorer.UI.ToolWindows
{
    partial class LogWindowControl
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
                DisposeAppender();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogWindowControl));
            this.cm = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ts = new System.Windows.Forms.ToolStrip();
            this.logLeveltoolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.logLevelList = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.printToolStripSplitButton = new System.Windows.Forms.ToolStripSplitButton();
            this.quickPrintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pageSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.clearAllToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toggleWordWrapToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.tb = new Delta.CertXplorer.UI.LogTextBox();
            this.cm.SuspendLayout();
            this.ts.SuspendLayout();
            this.SuspendLayout();
            // 
            // cm
            // 
            this.cm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.printToolStripMenuItem,
            this.toolStripMenuItem1,
            this.copyToolStripMenuItem,
            this.deleteAllToolStripMenuItem,
            this.selectAllToolStripMenuItem});
            this.cm.Name = "cm";
            resources.ApplyResources(this.cm, "cm");
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::Delta.CertXplorer.Properties.Resources.SaveFile;
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Image = global::Delta.CertXplorer.Properties.Resources.Print;
            resources.ApplyResources(this.printToolStripMenuItem, "printToolStripMenuItem");
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = global::Delta.CertXplorer.Properties.Resources.Copy;
            resources.ApplyResources(this.copyToolStripMenuItem, "copyToolStripMenuItem");
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // deleteAllToolStripMenuItem
            // 
            this.deleteAllToolStripMenuItem.Image = global::Delta.CertXplorer.Properties.Resources.ClearLog;
            resources.ApplyResources(this.deleteAllToolStripMenuItem, "deleteAllToolStripMenuItem");
            this.deleteAllToolStripMenuItem.Name = "deleteAllToolStripMenuItem";
            this.deleteAllToolStripMenuItem.Click += new System.EventHandler(this.deleteAllToolStripMenuItem_Click);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            resources.ApplyResources(this.selectAllToolStripMenuItem, "selectAllToolStripMenuItem");
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // ts
            // 
            this.ts.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logLeveltoolStripLabel,
            this.logLevelList,
            this.toolStripSeparator1,
            this.saveToolStripButton,
            this.printToolStripSplitButton,
            this.copyToolStripButton,
            this.clearAllToolStripButton,
            this.toggleWordWrapToolStripButton});
            resources.ApplyResources(this.ts, "ts");
            this.ts.Name = "ts";
            // 
            // logLeveltoolStripLabel
            // 
            this.logLeveltoolStripLabel.Name = "logLeveltoolStripLabel";
            this.logLeveltoolStripLabel.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            resources.ApplyResources(this.logLeveltoolStripLabel, "logLeveltoolStripLabel");
            // 
            // logLevelList
            // 
            this.logLevelList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.logLevelList.Name = "logLevelList";
            resources.ApplyResources(this.logLevelList, "logLevelList");
            this.logLevelList.SelectedIndexChanged += new System.EventHandler(this.logLevelList_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.saveToolStripButton, "saveToolStripButton");
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // printToolStripSplitButton
            // 
            this.printToolStripSplitButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printToolStripSplitButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quickPrintToolStripMenuItem,
            this.printToolStripMenuItem1,
            this.printPreviewToolStripMenuItem,
            this.pageSetupToolStripMenuItem});
            this.printToolStripSplitButton.Image = global::Delta.CertXplorer.Properties.Resources.Print;
            resources.ApplyResources(this.printToolStripSplitButton, "printToolStripSplitButton");
            this.printToolStripSplitButton.Name = "printToolStripSplitButton";
            this.printToolStripSplitButton.ButtonClick += new System.EventHandler(this.printToolStripSplitButton_ButtonClick);
            // 
            // quickPrintToolStripMenuItem
            // 
            this.quickPrintToolStripMenuItem.Name = "quickPrintToolStripMenuItem";
            resources.ApplyResources(this.quickPrintToolStripMenuItem, "quickPrintToolStripMenuItem");
            this.quickPrintToolStripMenuItem.Click += new System.EventHandler(this.quickPrintToolStripMenuItem_Click);
            // 
            // printToolStripMenuItem1
            // 
            this.printToolStripMenuItem1.Image = global::Delta.CertXplorer.Properties.Resources.Print;
            resources.ApplyResources(this.printToolStripMenuItem1, "printToolStripMenuItem1");
            this.printToolStripMenuItem1.Name = "printToolStripMenuItem1";
            this.printToolStripMenuItem1.Click += new System.EventHandler(this.printToolStripMenuItem1_Click);
            // 
            // printPreviewToolStripMenuItem
            // 
            this.printPreviewToolStripMenuItem.Image = global::Delta.CertXplorer.Properties.Resources.PrintPreview;
            resources.ApplyResources(this.printPreviewToolStripMenuItem, "printPreviewToolStripMenuItem");
            this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            this.printPreviewToolStripMenuItem.Click += new System.EventHandler(this.printPreviewToolStripMenuItem_Click);
            // 
            // pageSetupToolStripMenuItem
            // 
            this.pageSetupToolStripMenuItem.Image = global::Delta.CertXplorer.Properties.Resources.PageSetup;
            resources.ApplyResources(this.pageSetupToolStripMenuItem, "pageSetupToolStripMenuItem");
            this.pageSetupToolStripMenuItem.Name = "pageSetupToolStripMenuItem";
            this.pageSetupToolStripMenuItem.Click += new System.EventHandler(this.pageSetupToolStripMenuItem_Click);
            // 
            // copyToolStripButton
            // 
            this.copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.copyToolStripButton, "copyToolStripButton");
            this.copyToolStripButton.Name = "copyToolStripButton";
            this.copyToolStripButton.Click += new System.EventHandler(this.copyToolStripButton_Click);
            // 
            // clearAllToolStripButton
            // 
            this.clearAllToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.clearAllToolStripButton, "clearAllToolStripButton");
            this.clearAllToolStripButton.Name = "clearAllToolStripButton";
            this.clearAllToolStripButton.Click += new System.EventHandler(this.clearAllToolStripButton_Click);
            // 
            // toggleWordWrapToolStripButton
            // 
            this.toggleWordWrapToolStripButton.CheckOnClick = true;
            this.toggleWordWrapToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toggleWordWrapToolStripButton, "toggleWordWrapToolStripButton");
            this.toggleWordWrapToolStripButton.Name = "toggleWordWrapToolStripButton";
            this.toggleWordWrapToolStripButton.Click += new System.EventHandler(this.toggleWordWrapToolStripButton_Click);
            // 
            // tb
            // 
            this.tb.BackColor = System.Drawing.SystemColors.Window;
            this.tb.ContextMenuStrip = this.cm;
            resources.ApplyResources(this.tb, "tb");
            this.tb.HideSelection = false;
            this.tb.Name = "tb";
            this.tb.ReadOnly = true;
            this.tb.ShowSelectionMargin = true;
            // 
            // LogWindowControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tb);
            this.Controls.Add(this.ts);
            this.Name = "LogWindowControl";
            this.cm.ResumeLayout(false);
            this.ts.ResumeLayout(false);
            this.ts.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Delta.CertXplorer.UI.LogTextBox tb;
        private System.Windows.Forms.ToolStrip ts;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripButton copyToolStripButton;
        private System.Windows.Forms.ToolStripButton clearAllToolStripButton;
        private System.Windows.Forms.ToolStripButton toggleWordWrapToolStripButton;
        private System.Windows.Forms.ContextMenuStrip cm;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox logLevelList;
        private System.Windows.Forms.ToolStripLabel logLeveltoolStripLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSplitButton printToolStripSplitButton;
        private System.Windows.Forms.ToolStripMenuItem quickPrintToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pageSetupToolStripMenuItem;
    }
}
