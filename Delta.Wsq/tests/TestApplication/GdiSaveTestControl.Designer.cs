namespace TestApplication
{
    partial class GdiSaveTestControl
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
            this.table = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.outbox = new System.Windows.Forms.PictureBox();
            this.outlogbox = new System.Windows.Forms.TextBox();
            this.split = new System.Windows.Forms.SplitContainer();
            this.inbox = new System.Windows.Forms.PictureBox();
            this.inlogbox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.rbQuality = new System.Windows.Forms.RadioButton();
            this.nudQuality = new System.Windows.Forms.NumericUpDown();
            this.rbCompressionRatio = new System.Windows.Forms.RadioButton();
            this.nudCompressionRatio = new System.Windows.Forms.NumericUpDown();
            this.rbBitrate = new System.Windows.Forms.RadioButton();
            this.nudBitrate = new System.Windows.Forms.NumericUpDown();
            this.table.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outbox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.split)).BeginInit();
            this.split.Panel1.SuspendLayout();
            this.split.Panel2.SuspendLayout();
            this.split.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inbox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCompressionRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBitrate)).BeginInit();
            this.SuspendLayout();
            // 
            // table
            // 
            this.table.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.table.ColumnCount = 2;
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.table.Controls.Add(this.splitContainer1, 0, 0);
            this.table.Controls.Add(this.split, 0, 0);
            this.table.Location = new System.Drawing.Point(3, 54);
            this.table.Name = "table";
            this.table.RowCount = 1;
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.table.Size = new System.Drawing.Size(367, 285);
            this.table.TabIndex = 6;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(186, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.outbox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.outlogbox);
            this.splitContainer1.Size = new System.Drawing.Size(178, 279);
            this.splitContainer1.SplitterDistance = 138;
            this.splitContainer1.TabIndex = 7;
            // 
            // outbox
            // 
            this.outbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outbox.Location = new System.Drawing.Point(0, 0);
            this.outbox.Name = "outbox";
            this.outbox.Size = new System.Drawing.Size(178, 138);
            this.outbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.outbox.TabIndex = 3;
            this.outbox.TabStop = false;
            // 
            // outlogbox
            // 
            this.outlogbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outlogbox.Font = new System.Drawing.Font("Consolas", 10F);
            this.outlogbox.Location = new System.Drawing.Point(0, 0);
            this.outlogbox.Multiline = true;
            this.outlogbox.Name = "outlogbox";
            this.outlogbox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.outlogbox.Size = new System.Drawing.Size(178, 137);
            this.outlogbox.TabIndex = 4;
            this.outlogbox.WordWrap = false;
            // 
            // split
            // 
            this.split.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.split.Location = new System.Drawing.Point(3, 3);
            this.split.Name = "split";
            this.split.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // split.Panel1
            // 
            this.split.Panel1.Controls.Add(this.inbox);
            // 
            // split.Panel2
            // 
            this.split.Panel2.Controls.Add(this.inlogbox);
            this.split.Size = new System.Drawing.Size(177, 279);
            this.split.SplitterDistance = 138;
            this.split.TabIndex = 6;
            // 
            // inbox
            // 
            this.inbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inbox.Location = new System.Drawing.Point(0, 0);
            this.inbox.Name = "inbox";
            this.inbox.Size = new System.Drawing.Size(177, 138);
            this.inbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.inbox.TabIndex = 3;
            this.inbox.TabStop = false;
            // 
            // inlogbox
            // 
            this.inlogbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inlogbox.Font = new System.Drawing.Font("Consolas", 10F);
            this.inlogbox.Location = new System.Drawing.Point(0, 0);
            this.inlogbox.Multiline = true;
            this.inlogbox.Name = "inlogbox";
            this.inlogbox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.inlogbox.Size = new System.Drawing.Size(177, 137);
            this.inlogbox.TabIndex = 4;
            this.inlogbox.WordWrap = false;
            // 
            // saveButton
            // 
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.Location = new System.Drawing.Point(6, 3);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "Save WSQ";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // rbQuality
            // 
            this.rbQuality.AutoSize = true;
            this.rbQuality.Checked = true;
            this.rbQuality.Location = new System.Drawing.Point(87, 6);
            this.rbQuality.Name = "rbQuality";
            this.rbQuality.Size = new System.Drawing.Size(36, 17);
            this.rbQuality.TabIndex = 7;
            this.rbQuality.TabStop = true;
            this.rbQuality.Text = "&Q:";
            this.rbQuality.UseVisualStyleBackColor = true;
            // 
            // nudQuality
            // 
            this.nudQuality.Location = new System.Drawing.Point(131, 6);
            this.nudQuality.Name = "nudQuality";
            this.nudQuality.Size = new System.Drawing.Size(59, 20);
            this.nudQuality.TabIndex = 8;
            this.nudQuality.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.nudQuality.ValueChanged += new System.EventHandler(this.OnNudValueChanged);
            // 
            // rbCompressionRatio
            // 
            this.rbCompressionRatio.AutoSize = true;
            this.rbCompressionRatio.Location = new System.Drawing.Point(87, 29);
            this.rbCompressionRatio.Name = "rbCompressionRatio";
            this.rbCompressionRatio.Size = new System.Drawing.Size(38, 17);
            this.rbCompressionRatio.TabIndex = 7;
            this.rbCompressionRatio.TabStop = true;
            this.rbCompressionRatio.Text = "&Cr:";
            this.rbCompressionRatio.UseVisualStyleBackColor = true;
            // 
            // nudCompressionRatio
            // 
            this.nudCompressionRatio.DecimalPlaces = 1;
            this.nudCompressionRatio.Location = new System.Drawing.Point(131, 29);
            this.nudCompressionRatio.Name = "nudCompressionRatio";
            this.nudCompressionRatio.Size = new System.Drawing.Size(59, 20);
            this.nudCompressionRatio.TabIndex = 8;
            this.nudCompressionRatio.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.nudCompressionRatio.ValueChanged += new System.EventHandler(this.OnNudValueChanged);
            // 
            // rbBitrate
            // 
            this.rbBitrate.AutoSize = true;
            this.rbBitrate.Location = new System.Drawing.Point(196, 6);
            this.rbBitrate.Name = "rbBitrate";
            this.rbBitrate.Size = new System.Drawing.Size(38, 17);
            this.rbBitrate.TabIndex = 7;
            this.rbBitrate.TabStop = true;
            this.rbBitrate.Text = "&Br:";
            this.rbBitrate.UseVisualStyleBackColor = true;
            // 
            // nudBitrate
            // 
            this.nudBitrate.DecimalPlaces = 3;
            this.nudBitrate.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudBitrate.Location = new System.Drawing.Point(240, 6);
            this.nudBitrate.Name = "nudBitrate";
            this.nudBitrate.Size = new System.Drawing.Size(130, 20);
            this.nudBitrate.TabIndex = 8;
            this.nudBitrate.Value = new decimal(new int[] {
            75,
            0,
            0,
            131072});
            this.nudBitrate.ValueChanged += new System.EventHandler(this.OnNudValueChanged);
            // 
            // GdiSaveTestControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nudBitrate);
            this.Controls.Add(this.nudCompressionRatio);
            this.Controls.Add(this.nudQuality);
            this.Controls.Add(this.rbCompressionRatio);
            this.Controls.Add(this.rbBitrate);
            this.Controls.Add(this.rbQuality);
            this.Controls.Add(this.table);
            this.Controls.Add(this.saveButton);
            this.Name = "GdiSaveTestControl";
            this.Size = new System.Drawing.Size(450, 410);
            this.table.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.outbox)).EndInit();
            this.split.Panel1.ResumeLayout(false);
            this.split.Panel2.ResumeLayout(false);
            this.split.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.split)).EndInit();
            this.split.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.inbox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCompressionRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBitrate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel table;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.SplitContainer split;
        private System.Windows.Forms.PictureBox inbox;
        private System.Windows.Forms.TextBox inlogbox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox outbox;
        private System.Windows.Forms.TextBox outlogbox;
        private System.Windows.Forms.RadioButton rbQuality;
        private System.Windows.Forms.NumericUpDown nudQuality;
        private System.Windows.Forms.RadioButton rbCompressionRatio;
        private System.Windows.Forms.NumericUpDown nudCompressionRatio;
        private System.Windows.Forms.RadioButton rbBitrate;
        private System.Windows.Forms.NumericUpDown nudBitrate;
    }
}
