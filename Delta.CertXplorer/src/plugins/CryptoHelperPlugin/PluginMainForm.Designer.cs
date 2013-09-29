namespace CryptoHelperPlugin
{
    partial class PluginMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginMainForm));
            this.lineControl1 = new Delta.CertXplorer.UI.LineControl();
            this.closeButton = new System.Windows.Forms.Button();
            this.lineControl2 = new Delta.CertXplorer.UI.LineControl();
            this.lineControl3 = new Delta.CertXplorer.UI.LineControl();
            this.lineControl4 = new Delta.CertXplorer.UI.LineControl();
            this.runButton = new System.Windows.Forms.Button();
            this.split = new System.Windows.Forms.SplitContainer();
            this.inbox = new CryptoHelperPlugin.UI.DataBox();
            this.inputFormatSelector = new CryptoHelperPlugin.UI.DataFormatSelector();
            this.operationSelector = new CryptoHelperPlugin.UI.OperationSelector();
            this.outputFormatSelector = new CryptoHelperPlugin.UI.DataFormatSelector();
            this.outbox = new CryptoHelperPlugin.UI.DataBox();
            ((System.ComponentModel.ISupportInitialize)(this.split)).BeginInit();
            this.split.Panel1.SuspendLayout();
            this.split.Panel2.SuspendLayout();
            this.split.SuspendLayout();
            this.SuspendLayout();
            // 
            // lineControl1
            // 
            this.lineControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lineControl1.Location = new System.Drawing.Point(12, 388);
            this.lineControl1.Name = "lineControl1";
            this.lineControl1.Size = new System.Drawing.Size(600, 12);
            this.lineControl1.TabIndex = 9;
            this.lineControl1.TabStop = false;
            this.lineControl1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(128)))), ((int)(((byte)(186)))));
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(537, 406);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 10;
            this.closeButton.Text = "&Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // lineControl2
            // 
            this.lineControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lineControl2.Location = new System.Drawing.Point(12, 12);
            this.lineControl2.Name = "lineControl2";
            this.lineControl2.Size = new System.Drawing.Size(600, 12);
            this.lineControl2.TabIndex = 0;
            this.lineControl2.TabStop = false;
            this.lineControl2.Text = "Input";
            this.lineControl2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(128)))), ((int)(((byte)(186)))));
            // 
            // lineControl3
            // 
            this.lineControl3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lineControl3.Location = new System.Drawing.Point(12, 3);
            this.lineControl3.Name = "lineControl3";
            this.lineControl3.Size = new System.Drawing.Size(600, 12);
            this.lineControl3.TabIndex = 3;
            this.lineControl3.TabStop = false;
            this.lineControl3.Text = "Output";
            this.lineControl3.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(128)))), ((int)(((byte)(186)))));
            // 
            // lineControl4
            // 
            this.lineControl4.Location = new System.Drawing.Point(12, 126);
            this.lineControl4.Name = "lineControl4";
            this.lineControl4.Size = new System.Drawing.Size(170, 12);
            this.lineControl4.TabIndex = 6;
            this.lineControl4.TabStop = false;
            this.lineControl4.Text = "Operation";
            this.lineControl4.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(128)))), ((int)(((byte)(186)))));
            // 
            // runButton
            // 
            this.runButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.runButton.Location = new System.Drawing.Point(12, 215);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(75, 23);
            this.runButton.TabIndex = 8;
            this.runButton.Text = "&Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // split
            // 
            this.split.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.split.Location = new System.Drawing.Point(0, 0);
            this.split.Margin = new System.Windows.Forms.Padding(0);
            this.split.Name = "split";
            this.split.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // split.Panel1
            // 
            this.split.Panel1.Controls.Add(this.inbox);
            this.split.Panel1.Controls.Add(this.inputFormatSelector);
            this.split.Panel1.Controls.Add(this.lineControl2);
            // 
            // split.Panel2
            // 
            this.split.Panel2.Controls.Add(this.operationSelector);
            this.split.Panel2.Controls.Add(this.runButton);
            this.split.Panel2.Controls.Add(this.outputFormatSelector);
            this.split.Panel2.Controls.Add(this.lineControl3);
            this.split.Panel2.Controls.Add(this.outbox);
            this.split.Panel2.Controls.Add(this.lineControl4);
            this.split.Size = new System.Drawing.Size(625, 385);
            this.split.SplitterDistance = 140;
            this.split.TabIndex = 11;
            // 
            // inbox
            // 
            this.inbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inbox.BackColor = System.Drawing.Color.White;
            this.inbox.Location = new System.Drawing.Point(188, 30);
            this.inbox.Name = "inbox";
            this.inbox.Size = new System.Drawing.Size(424, 107);
            this.inbox.TabIndex = 2;
            // 
            // inputFormatSelector
            // 
            this.inputFormatSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.inputFormatSelector.DataFormat = CryptoHelperPlugin.DataFormat.Text;
            this.inputFormatSelector.Location = new System.Drawing.Point(12, 30);
            this.inputFormatSelector.Name = "inputFormatSelector";
            this.inputFormatSelector.Size = new System.Drawing.Size(170, 107);
            this.inputFormatSelector.TabIndex = 1;
            // 
            // operationSelector
            // 
            this.operationSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.operationSelector.Location = new System.Drawing.Point(12, 144);
            this.operationSelector.Name = "operationSelector";
            this.operationSelector.Operation = CryptoHelperPlugin.Operation.Convert;
            this.operationSelector.Size = new System.Drawing.Size(170, 65);
            this.operationSelector.TabIndex = 7;
            // 
            // outputFormatSelector
            // 
            this.outputFormatSelector.DataFormat = CryptoHelperPlugin.DataFormat.Text;
            this.outputFormatSelector.Location = new System.Drawing.Point(12, 21);
            this.outputFormatSelector.Name = "outputFormatSelector";
            this.outputFormatSelector.Size = new System.Drawing.Size(170, 99);
            this.outputFormatSelector.TabIndex = 4;
            // 
            // outbox
            // 
            this.outbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outbox.BackColor = System.Drawing.Color.White;
            this.outbox.Location = new System.Drawing.Point(187, 21);
            this.outbox.Name = "outbox";
            this.outbox.Size = new System.Drawing.Size(425, 217);
            this.outbox.TabIndex = 5;
            // 
            // PluginMainForm
            // 
            this.AcceptButton = this.runButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.split);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.lineControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "PluginMainForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Crypto Helper";
            this.split.Panel1.ResumeLayout(false);
            this.split.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.split)).EndInit();
            this.split.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Delta.CertXplorer.UI.LineControl lineControl1;
        private System.Windows.Forms.Button closeButton;
        private CryptoHelperPlugin.UI.DataFormatSelector inputFormatSelector;
        private CryptoHelperPlugin.UI.DataFormatSelector outputFormatSelector;
        private Delta.CertXplorer.UI.LineControl lineControl2;
        private Delta.CertXplorer.UI.LineControl lineControl3;
        private Delta.CertXplorer.UI.LineControl lineControl4;
        private CryptoHelperPlugin.UI.DataBox outbox;
        private CryptoHelperPlugin.UI.DataBox inbox;
        private CryptoHelperPlugin.UI.OperationSelector operationSelector;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.SplitContainer split;
    }
}