namespace Delta.CertXplorer.Diagnostics
{
    partial class ExceptionBox
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExceptionBox));
            this.rtb = new Delta.CertXplorer.UI.PrintableRichTextBox();
            this.cm = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.line = new Delta.CertXplorer.UI.LineControl();
            this.tc = new Delta.CertXplorer.UI.CommunityTabControl();
            this.tpError = new System.Windows.Forms.TabPage();
            this.tbError = new System.Windows.Forms.TextBox();
            this.tpDetails = new System.Windows.Forms.TabPage();
            this.pcsLinks = new Delta.CertXplorer.UI.PrintCopySaveLinks();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.cm.SuspendLayout();
            this.tc.SuspendLayout();
            this.tpError.SuspendLayout();
            this.tpDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAccept
            // 
            resources.ApplyResources(this.btnAccept, "btnAccept");
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            // 
            // rtb
            // 
            resources.ApplyResources(this.rtb, "rtb");
            this.rtb.ContextMenuStrip = this.cm;
            this.rtb.Name = "rtb";
            this.rtb.ReadOnly = true;
            this.rtb.ShowSelectionMargin = true;
            // 
            // cm
            // 
            this.cm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.printToolStripMenuItem,
            this.toolStripMenuItem1,
            this.copyToolStripMenuItem,
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
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            resources.ApplyResources(this.selectAllToolStripMenuItem, "selectAllToolStripMenuItem");
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // line
            // 
            resources.ApplyResources(this.line, "line");
            this.line.BackColor = System.Drawing.Color.Transparent;
            this.line.Name = "line";
            this.line.TabStop = false;
            this.line.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(128)))), ((int)(((byte)(186)))));
            // 
            // tc
            // 
            resources.ApplyResources(this.tc, "tc");
            this.tc.Controls.Add(this.tpError);
            this.tc.Controls.Add(this.tpDetails);
            this.tc.Name = "tc";
            this.tc.SelectedIndex = 0;
            this.tc.ShowTabSeparator = true;
            // 
            // tpError
            // 
            this.tpError.Controls.Add(this.tbError);
            resources.ApplyResources(this.tpError, "tpError");
            this.tpError.Name = "tpError";
            this.tpError.UseVisualStyleBackColor = true;
            // 
            // tbError
            // 
            resources.ApplyResources(this.tbError, "tbError");
            this.tbError.BackColor = System.Drawing.SystemColors.Window;
            this.tbError.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbError.Name = "tbError";
            this.tbError.ReadOnly = true;
            // 
            // tpDetails
            // 
            this.tpDetails.Controls.Add(this.pcsLinks);
            this.tpDetails.Controls.Add(this.rtb);
            resources.ApplyResources(this.tpDetails, "tpDetails");
            this.tpDetails.Name = "tpDetails";
            this.tpDetails.UseVisualStyleBackColor = true;
            // 
            // pcsLinks
            // 
            resources.ApplyResources(this.pcsLinks, "pcsLinks");
            this.pcsLinks.BackColor = System.Drawing.Color.Transparent;
            this.pcsLinks.Name = "pcsLinks";
            this.pcsLinks.ShowCopyLink = false;
            this.pcsLinks.ShowPrintLink = false;
            this.pcsLinks.ShowSaveLink = false;
            this.pcsLinks.CopyClick += new System.EventHandler(this.pcsLinks_CopyClick);
            this.pcsLinks.PrintClick += new System.EventHandler(this.pcsLinks_PrintClick);
            this.pcsLinks.SaveClick += new System.EventHandler(this.pcsLinks_SaveClick);
            // 
            // ExceptionBox
            // 
            this.AcceptButtonVisible = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButtonText = "&Annuler";
            this.CancelButtonVisible = true;
            this.Controls.Add(this.tc);
            this.Controls.Add(this.line);
            this.Name = "ExceptionBox";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Controls.SetChildIndex(this.btnAccept, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.line, 0);
            this.Controls.SetChildIndex(this.tc, 0);
            this.cm.ResumeLayout(false);
            this.tc.ResumeLayout(false);
            this.tpError.ResumeLayout(false);
            this.tpError.PerformLayout();
            this.tpDetails.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Delta.CertXplorer.UI.PrintableRichTextBox rtb;
        private Delta.CertXplorer.UI.LineControl line;
        private Delta.CertXplorer.UI.CommunityTabControl tc;
        private System.Windows.Forms.TabPage tpError;
        private System.Windows.Forms.TabPage tpDetails;
        private System.Windows.Forms.TextBox tbError;
        private Delta.CertXplorer.UI.PrintCopySaveLinks pcsLinks;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;		
        private System.Windows.Forms.ContextMenuStrip cm;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
    }
}