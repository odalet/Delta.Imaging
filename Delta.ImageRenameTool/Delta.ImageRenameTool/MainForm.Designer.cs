namespace Delta.ImageRenameTool
{
    partial class MainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tbDirectory = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbFilter = new System.Windows.Forms.TextBox();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnRename = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.sstrip = new System.Windows.Forms.StatusStrip();
            this.split = new System.Windows.Forms.SplitContainer();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.Selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.originalFileNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpdateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.newFileNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resultDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cm = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unselectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unselectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.clearGeneratedNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearDescriptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.binder = new System.Windows.Forms.BindingSource(this.components);
            this.tc = new System.Windows.Forms.TabControl();
            this.previewPage = new System.Windows.Forms.TabPage();
            this.wpfHost = new System.Windows.Forms.Integration.ElementHost();
            this.wpfViewer = new Delta.ImageRenameTool.UI.WpfViewer();
            this.cbAutoRotate = new System.Windows.Forms.CheckBox();
            this.exifPage = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.pg = new System.Windows.Forms.PropertyGrid();
            this.rtb = new System.Windows.Forms.RichTextBox();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnUnselectAll = new System.Windows.Forms.Button();
            this.enableImagePreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitHorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mstrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.nudFirstIndex = new System.Windows.Forms.NumericUpDown();
            this.cbSkipUnselected = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.split.Panel1.SuspendLayout();
            this.split.Panel2.SuspendLayout();
            this.split.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.cm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.binder)).BeginInit();
            this.tc.SuspendLayout();
            this.previewPage.SuspendLayout();
            this.exifPage.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.mstrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFirstIndex)).BeginInit();
            this.SuspendLayout();
            // 
            // tbDirectory
            // 
            this.tbDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDirectory.Location = new System.Drawing.Point(70, 29);
            this.tbDirectory.Name = "tbDirectory";
            this.tbDirectory.Size = new System.Drawing.Size(911, 20);
            this.tbDirectory.TabIndex = 1;
            this.tbDirectory.Text = "c:\\";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Directory:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(987, 27);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "&Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Filter:";
            // 
            // tbFilter
            // 
            this.tbFilter.AcceptsReturn = true;
            this.tbFilter.Location = new System.Drawing.Point(70, 58);
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new System.Drawing.Size(218, 20);
            this.tbFilter.TabIndex = 4;
            this.tbFilter.Text = "*.jpeg;*.jpg";
            this.tbFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbFilter_KeyPress);
            // 
            // btnPreview
            // 
            this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPreview.Location = new System.Drawing.Point(174, 452);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(75, 23);
            this.btnPreview.TabIndex = 7;
            this.btnPreview.Text = "&Generate";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnRename
            // 
            this.btnRename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRename.Location = new System.Drawing.Point(255, 452);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(75, 23);
            this.btnRename.TabIndex = 8;
            this.btnRename.Text = "&Rename";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Location = new System.Drawing.Point(987, 56);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 5;
            this.btnLoad.Text = "&Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // sstrip
            // 
            this.sstrip.Location = new System.Drawing.Point(0, 478);
            this.sstrip.Name = "sstrip";
            this.sstrip.Size = new System.Drawing.Size(1074, 22);
            this.sstrip.TabIndex = 10;
            this.sstrip.Text = "statusStrip1";
            // 
            // split
            // 
            this.split.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.split.Location = new System.Drawing.Point(12, 85);
            this.split.Name = "split";
            // 
            // split.Panel1
            // 
            this.split.Panel1.Controls.Add(this.dgv);
            // 
            // split.Panel2
            // 
            this.split.Panel2.Controls.Add(this.tc);
            this.split.Panel2MinSize = 0;
            this.split.Size = new System.Drawing.Size(1050, 361);
            this.split.SplitterDistance = 688;
            this.split.TabIndex = 6;
            // 
            // dgv
            // 
            this.dgv.AutoGenerateColumns = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selected,
            this.originalFileNameDataGridViewTextBoxColumn,
            this.UpdateTime,
            this.Description,
            this.newFileNameDataGridViewTextBoxColumn,
            this.resultDataGridViewTextBoxColumn});
            this.dgv.ContextMenuStrip = this.cm;
            this.dgv.DataSource = this.binder;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Name = "dgv";
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(688, 361);
            this.dgv.TabIndex = 0;
            // 
            // Selected
            // 
            this.Selected.DataPropertyName = "Selected";
            this.Selected.HeaderText = "";
            this.Selected.Name = "Selected";
            this.Selected.Width = 20;
            // 
            // originalFileNameDataGridViewTextBoxColumn
            // 
            this.originalFileNameDataGridViewTextBoxColumn.DataPropertyName = "OriginalFileName";
            this.originalFileNameDataGridViewTextBoxColumn.HeaderText = "Original Name";
            this.originalFileNameDataGridViewTextBoxColumn.Name = "originalFileNameDataGridViewTextBoxColumn";
            this.originalFileNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.originalFileNameDataGridViewTextBoxColumn.Width = 200;
            // 
            // UpdateTime
            // 
            this.UpdateTime.DataPropertyName = "PhotoTime";
            dataGridViewCellStyle1.Format = "d";
            dataGridViewCellStyle1.NullValue = null;
            this.UpdateTime.DefaultCellStyle = dataGridViewCellStyle1;
            this.UpdateTime.HeaderText = "Date";
            this.UpdateTime.Name = "UpdateTime";
            this.UpdateTime.ReadOnly = true;
            this.UpdateTime.ToolTipText = "File Creation time";
            this.UpdateTime.Width = 70;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            // 
            // newFileNameDataGridViewTextBoxColumn
            // 
            this.newFileNameDataGridViewTextBoxColumn.DataPropertyName = "NewFileName";
            this.newFileNameDataGridViewTextBoxColumn.HeaderText = "New Name";
            this.newFileNameDataGridViewTextBoxColumn.Name = "newFileNameDataGridViewTextBoxColumn";
            this.newFileNameDataGridViewTextBoxColumn.Width = 200;
            // 
            // resultDataGridViewTextBoxColumn
            // 
            this.resultDataGridViewTextBoxColumn.DataPropertyName = "Result";
            this.resultDataGridViewTextBoxColumn.HeaderText = "Result";
            this.resultDataGridViewTextBoxColumn.Name = "resultDataGridViewTextBoxColumn";
            this.resultDataGridViewTextBoxColumn.Width = 150;
            // 
            // cm
            // 
            this.cm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.unselectAllToolStripMenuItem,
            this.selectToolStripMenuItem,
            this.unselectToolStripMenuItem,
            this.toolStripMenuItem2,
            this.clearGeneratedNameToolStripMenuItem,
            this.clearDescriptionToolStripMenuItem,
            this.clearAllToolStripMenuItem});
            this.cm.Name = "cm";
            this.cm.Size = new System.Drawing.Size(252, 164);
            this.cm.Opening += new System.ComponentModel.CancelEventHandler(this.cm_Opening);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(251, 22);
            this.selectAllToolStripMenuItem.Text = "Select &All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // unselectAllToolStripMenuItem
            // 
            this.unselectAllToolStripMenuItem.Name = "unselectAllToolStripMenuItem";
            this.unselectAllToolStripMenuItem.Size = new System.Drawing.Size(251, 22);
            this.unselectAllToolStripMenuItem.Text = "U&nselect All";
            this.unselectAllToolStripMenuItem.Click += new System.EventHandler(this.unselectAllToolStripMenuItem_Click);
            // 
            // selectToolStripMenuItem
            // 
            this.selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            this.selectToolStripMenuItem.Size = new System.Drawing.Size(251, 22);
            this.selectToolStripMenuItem.Text = "&Select";
            this.selectToolStripMenuItem.Click += new System.EventHandler(this.selectToolStripMenuItem_Click);
            // 
            // unselectToolStripMenuItem
            // 
            this.unselectToolStripMenuItem.Name = "unselectToolStripMenuItem";
            this.unselectToolStripMenuItem.Size = new System.Drawing.Size(251, 22);
            this.unselectToolStripMenuItem.Text = "&Unselect";
            this.unselectToolStripMenuItem.Click += new System.EventHandler(this.unselectToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(248, 6);
            // 
            // clearGeneratedNameToolStripMenuItem
            // 
            this.clearGeneratedNameToolStripMenuItem.Name = "clearGeneratedNameToolStripMenuItem";
            this.clearGeneratedNameToolStripMenuItem.Size = new System.Drawing.Size(251, 22);
            this.clearGeneratedNameToolStripMenuItem.Text = "&Clear Generated Name and Result";
            this.clearGeneratedNameToolStripMenuItem.Click += new System.EventHandler(this.clearGeneratedNameToolStripMenuItem_Click);
            // 
            // clearDescriptionToolStripMenuItem
            // 
            this.clearDescriptionToolStripMenuItem.Name = "clearDescriptionToolStripMenuItem";
            this.clearDescriptionToolStripMenuItem.Size = new System.Drawing.Size(251, 22);
            this.clearDescriptionToolStripMenuItem.Text = "Clear &Description";
            this.clearDescriptionToolStripMenuItem.Click += new System.EventHandler(this.clearDescriptionToolStripMenuItem_Click);
            // 
            // clearAllToolStripMenuItem
            // 
            this.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
            this.clearAllToolStripMenuItem.Size = new System.Drawing.Size(251, 22);
            this.clearAllToolStripMenuItem.Text = "Clear A&ll";
            this.clearAllToolStripMenuItem.Click += new System.EventHandler(this.clearAllToolStripMenuItem_Click);
            // 
            // binder
            // 
            this.binder.DataSource = typeof(Delta.ImageRenameTool.FileRenameInfo);
            this.binder.CurrentItemChanged += new System.EventHandler(this.binder_CurrentItemChanged);
            // 
            // tc
            // 
            this.tc.Controls.Add(this.previewPage);
            this.tc.Controls.Add(this.exifPage);
            this.tc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc.Location = new System.Drawing.Point(0, 0);
            this.tc.Name = "tc";
            this.tc.SelectedIndex = 0;
            this.tc.Size = new System.Drawing.Size(358, 361);
            this.tc.TabIndex = 1;
            // 
            // previewPage
            // 
            this.previewPage.Controls.Add(this.wpfHost);
            this.previewPage.Controls.Add(this.cbAutoRotate);
            this.previewPage.Location = new System.Drawing.Point(4, 22);
            this.previewPage.Name = "previewPage";
            this.previewPage.Padding = new System.Windows.Forms.Padding(3);
            this.previewPage.Size = new System.Drawing.Size(350, 335);
            this.previewPage.TabIndex = 0;
            this.previewPage.Text = "Preview";
            this.previewPage.UseVisualStyleBackColor = true;
            // 
            // wpfHost
            // 
            this.wpfHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wpfHost.Location = new System.Drawing.Point(3, 3);
            this.wpfHost.Name = "wpfHost";
            this.wpfHost.Size = new System.Drawing.Size(344, 312);
            this.wpfHost.TabIndex = 0;
            this.wpfHost.Text = "elementHost1";
            this.wpfHost.Child = this.wpfViewer;
            // 
            // cbAutoRotate
            // 
            this.cbAutoRotate.AutoSize = true;
            this.cbAutoRotate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cbAutoRotate.Location = new System.Drawing.Point(3, 315);
            this.cbAutoRotate.Name = "cbAutoRotate";
            this.cbAutoRotate.Size = new System.Drawing.Size(344, 17);
            this.cbAutoRotate.TabIndex = 1;
            this.cbAutoRotate.Text = "Enable Automatic Rotation (EXIF)";
            this.cbAutoRotate.UseVisualStyleBackColor = true;
            this.cbAutoRotate.CheckedChanged += new System.EventHandler(this.cbAutoRotate_CheckedChanged);
            // 
            // exifPage
            // 
            this.exifPage.Controls.Add(this.splitContainer2);
            this.exifPage.Location = new System.Drawing.Point(4, 22);
            this.exifPage.Name = "exifPage";
            this.exifPage.Padding = new System.Windows.Forms.Padding(3);
            this.exifPage.Size = new System.Drawing.Size(350, 335);
            this.exifPage.TabIndex = 1;
            this.exifPage.Text = "Properties";
            this.exifPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.pg);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.rtb);
            this.splitContainer2.Panel2.Controls.Add(this.label4);
            this.splitContainer2.Size = new System.Drawing.Size(344, 329);
            this.splitContainer2.SplitterDistance = 99;
            this.splitContainer2.TabIndex = 2;
            // 
            // pg
            // 
            this.pg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pg.Location = new System.Drawing.Point(0, 0);
            this.pg.Name = "pg";
            this.pg.Size = new System.Drawing.Size(344, 99);
            this.pg.TabIndex = 0;
            // 
            // rtb
            // 
            this.rtb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb.Location = new System.Drawing.Point(0, 13);
            this.rtb.Name = "rtb";
            this.rtb.Size = new System.Drawing.Size(344, 213);
            this.rtb.TabIndex = 0;
            this.rtb.Text = "";
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectAll.Location = new System.Drawing.Point(12, 452);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 11;
            this.btnSelectAll.Text = "Select &All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnUnselectAll
            // 
            this.btnUnselectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUnselectAll.Location = new System.Drawing.Point(93, 452);
            this.btnUnselectAll.Name = "btnUnselectAll";
            this.btnUnselectAll.Size = new System.Drawing.Size(75, 23);
            this.btnUnselectAll.TabIndex = 11;
            this.btnUnselectAll.Text = "&Unselect All";
            this.btnUnselectAll.UseVisualStyleBackColor = true;
            this.btnUnselectAll.Click += new System.EventHandler(this.btnUnselectAll_Click);
            // 
            // enableImagePreviewToolStripMenuItem
            // 
            this.enableImagePreviewToolStripMenuItem.CheckOnClick = true;
            this.enableImagePreviewToolStripMenuItem.Name = "enableImagePreviewToolStripMenuItem";
            this.enableImagePreviewToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.enableImagePreviewToolStripMenuItem.Text = "Enable Image Preview";
            this.enableImagePreviewToolStripMenuItem.CheckedChanged += new System.EventHandler(this.enableImagePreviewToolStripMenuItem_CheckedChanged);
            // 
            // splitHorizontalToolStripMenuItem
            // 
            this.splitHorizontalToolStripMenuItem.CheckOnClick = true;
            this.splitHorizontalToolStripMenuItem.Name = "splitHorizontalToolStripMenuItem";
            this.splitHorizontalToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.splitHorizontalToolStripMenuItem.Text = "Horizontal Split ";
            this.splitHorizontalToolStripMenuItem.CheckedChanged += new System.EventHandler(this.splitHorizontalToolStripMenuItem_CheckedChanged);
            // 
            // mstrip
            // 
            this.mstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mstrip.Location = new System.Drawing.Point(0, 0);
            this.mstrip.Name = "mstrip";
            this.mstrip.Size = new System.Drawing.Size(1074, 24);
            this.mstrip.TabIndex = 12;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableImagePreviewToolStripMenuItem,
            this.splitHorizontalToolStripMenuItem,
            this.toolStripMenuItem1,
            this.aboutToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(186, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1)));
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(186, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(294, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "First Index:";
            // 
            // nudFirstIndex
            // 
            this.nudFirstIndex.Location = new System.Drawing.Point(358, 59);
            this.nudFirstIndex.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudFirstIndex.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFirstIndex.Name = "nudFirstIndex";
            this.nudFirstIndex.Size = new System.Drawing.Size(120, 20);
            this.nudFirstIndex.TabIndex = 13;
            this.nudFirstIndex.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbSkipUnselected
            // 
            this.cbSkipUnselected.AutoSize = true;
            this.cbSkipUnselected.Location = new System.Drawing.Point(484, 60);
            this.cbSkipUnselected.Name = "cbSkipUnselected";
            this.cbSkipUnselected.Size = new System.Drawing.Size(222, 17);
            this.cbSkipUnselected.TabIndex = 15;
            this.cbSkipUnselected.Text = "Skip unselected rows when incrementing.";
            this.cbSkipUnselected.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "EXIF Properties:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1074, 500);
            this.Controls.Add(this.nudFirstIndex);
            this.Controls.Add(this.cbSkipUnselected);
            this.Controls.Add(this.mstrip);
            this.Controls.Add(this.btnUnselectAll);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.split);
            this.Controls.Add(this.sstrip);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbFilter);
            this.Controls.Add(this.tbDirectory);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rename Tool";
            this.split.Panel1.ResumeLayout(false);
            this.split.Panel2.ResumeLayout(false);
            this.split.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.cm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.binder)).EndInit();
            this.tc.ResumeLayout(false);
            this.previewPage.ResumeLayout(false);
            this.previewPage.PerformLayout();
            this.exifPage.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.mstrip.ResumeLayout(false);
            this.mstrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFirstIndex)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbDirectory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbFilter;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.StatusStrip sstrip;
        private System.Windows.Forms.SplitContainer split;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.BindingSource binder;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnUnselectAll;
        private System.Windows.Forms.ToolStripMenuItem enableImagePreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem splitHorizontalToolStripMenuItem;
        private System.Windows.Forms.MenuStrip mstrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudFirstIndex;
        private System.Windows.Forms.ContextMenuStrip cm;
        private System.Windows.Forms.ToolStripMenuItem selectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unselectToolStripMenuItem;
        private System.Windows.Forms.CheckBox cbSkipUnselected;
        private System.Windows.Forms.ToolStripMenuItem clearGeneratedNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unselectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem clearDescriptionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAllToolStripMenuItem;
        private System.Windows.Forms.TabControl tc;
        private System.Windows.Forms.TabPage previewPage;
        private System.Windows.Forms.TabPage exifPage;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PropertyGrid pg;
        private System.Windows.Forms.RichTextBox rtb;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selected;
        private System.Windows.Forms.DataGridViewTextBoxColumn originalFileNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn UpdateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn newFileNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn resultDataGridViewTextBoxColumn;
        private System.Windows.Forms.CheckBox cbAutoRotate;
        private System.Windows.Forms.Integration.ElementHost wpfHost;
        private UI.WpfViewer wpfViewer;
        private System.Windows.Forms.Label label4;
    }
}

