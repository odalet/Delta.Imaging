namespace Delta.CertXplorer.Asn1Decoder
{
    partial class Asn1Viewer
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
            this.mainSplit = new System.Windows.Forms.SplitContainer();
            this.asnTreeView = new Delta.CertXplorer.Asn1Decoder.Asn1TreeView();
            this.rightSplit = new System.Windows.Forms.SplitContainer();
            this.hexViewer = new Delta.CertXplorer.UI.HexViewer();
            this.bytesTextBox = new System.Windows.Forms.TextBox();
            this.asciiTextBox = new System.Windows.Forms.TextBox();
            this.mainSplit.Panel1.SuspendLayout();
            this.mainSplit.Panel2.SuspendLayout();
            this.mainSplit.SuspendLayout();
            this.rightSplit.Panel1.SuspendLayout();
            this.rightSplit.Panel2.SuspendLayout();
            this.rightSplit.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainSplit
            // 
            this.mainSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.mainSplit.Location = new System.Drawing.Point(0, 0);
            this.mainSplit.Name = "mainSplit";
            // 
            // mainSplit.Panel1
            // 
            this.mainSplit.Panel1.Controls.Add(this.asnTreeView);
            // 
            // mainSplit.Panel2
            // 
            this.mainSplit.Panel2.Controls.Add(this.rightSplit);
            this.mainSplit.Size = new System.Drawing.Size(769, 522);
            this.mainSplit.SplitterDistance = 276;
            this.mainSplit.TabIndex = 0;
            // 
            // asnTreeView
            // 
            this.asnTreeView.CanClearSelection = false;
            this.asnTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.asnTreeView.FullRowSelect = true;
            this.asnTreeView.HideSelection = false;
            this.asnTreeView.ImageIndex = 0;
            this.asnTreeView.Location = new System.Drawing.Point(0, 0);
            this.asnTreeView.Name = "asnTreeView";
            this.asnTreeView.SelectedImageIndex = 0;
            this.asnTreeView.Size = new System.Drawing.Size(276, 522);
            this.asnTreeView.TabIndex = 0;
            // 
            // rightSplit
            // 
            this.rightSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightSplit.Location = new System.Drawing.Point(0, 0);
            this.rightSplit.Name = "rightSplit";
            this.rightSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // rightSplit.Panel1
            // 
            this.rightSplit.Panel1.Controls.Add(this.hexViewer);
            // 
            // rightSplit.Panel2
            // 
            this.rightSplit.Panel2.Controls.Add(this.bytesTextBox);
            this.rightSplit.Panel2.Controls.Add(this.asciiTextBox);
            this.rightSplit.Size = new System.Drawing.Size(489, 522);
            this.rightSplit.SplitterDistance = 350;
            this.rightSplit.TabIndex = 0;
            // 
            // hexViewer
            // 
            this.hexViewer.AllowDrop = true;
            this.hexViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hexViewer.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hexViewer.LineInfoVisible = true;
            this.hexViewer.Location = new System.Drawing.Point(0, 0);
            this.hexViewer.Name = "hexViewer";
            this.hexViewer.SelectionLength = ((long)(0));
            this.hexViewer.SelectionStart = ((long)(-1));
            this.hexViewer.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexViewer.Size = new System.Drawing.Size(489, 350);
            this.hexViewer.StringViewVisible = true;
            this.hexViewer.TabIndex = 0;
            this.hexViewer.UseFixedBytesPerLine = true;
            this.hexViewer.VScrollBarVisible = true;
            // 
            // bytesTextBox
            // 
            this.bytesTextBox.AcceptsReturn = true;
            this.bytesTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bytesTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bytesTextBox.Location = new System.Drawing.Point(0, 0);
            this.bytesTextBox.Multiline = true;
            this.bytesTextBox.Name = "bytesTextBox";
            this.bytesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.bytesTextBox.Size = new System.Drawing.Size(334, 168);
            this.bytesTextBox.TabIndex = 0;
            // 
            // asciiTextBox
            // 
            this.asciiTextBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.asciiTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.asciiTextBox.Location = new System.Drawing.Point(334, 0);
            this.asciiTextBox.Multiline = true;
            this.asciiTextBox.Name = "asciiTextBox";
            this.asciiTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.asciiTextBox.Size = new System.Drawing.Size(155, 168);
            this.asciiTextBox.TabIndex = 1;
            // 
            // Asn1Viewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainSplit);
            this.Name = "Asn1Viewer";
            this.Size = new System.Drawing.Size(769, 522);
            this.mainSplit.Panel1.ResumeLayout(false);
            this.mainSplit.Panel2.ResumeLayout(false);
            this.mainSplit.ResumeLayout(false);
            this.rightSplit.Panel1.ResumeLayout(false);
            this.rightSplit.Panel2.ResumeLayout(false);
            this.rightSplit.Panel2.PerformLayout();
            this.rightSplit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer mainSplit;
        private Asn1TreeView asnTreeView;
        private System.Windows.Forms.SplitContainer rightSplit;
        private Delta.CertXplorer.UI.HexViewer hexViewer;
        private System.Windows.Forms.TextBox bytesTextBox;
        private System.Windows.Forms.TextBox asciiTextBox;
    }
}
