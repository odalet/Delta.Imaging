using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

using Goheer.EXIF;

using Delta.ImageRenameTool.UI;

namespace Delta.ImageRenameTool
{
    public partial class MainForm : Form
    {
        ////private BackgroundWorker currentWorker = null;
        private Thread currentPreviewThread = null;
        private bool loaded = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            nudFirstIndex.Maximum = 0M;
            nudFirstIndex.Maximum = decimal.MaxValue;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Text = string.Format("Rename Tool {0}",
                Assembly.GetExecutingAssembly().GetName().Version);

            var dir = Properties.Settings.Default.SourceDirectory;
            if (string.IsNullOrEmpty(dir)) dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            nudFirstIndex.Value = Properties.Settings.Default.FirstIndex;
            tbDirectory.Text = dir;
            tbFilter.Text = Properties.Settings.Default.Filter;
            EnableImagePreview = Properties.Settings.Default.EnableImagePreview;
            EnableAutoRotatePreview = Properties.Settings.Default.EnableAutoRotatePreview;
            SplitHorizontal = Properties.Settings.Default.SplitHorizontal;
            if (EnableImagePreview)
                split.SplitterDistance = GetDefaultSplitterDistance();
            else split.SplitterDistance = split.Width;
            btnLoad.Select();

            loaded = true;
        }

        protected override void OnClosed(EventArgs e)
        {
            if (EnableImagePreview) Properties.Settings.Default.SplitterRatio =
                split.Orientation == Orientation.Horizontal ?
                (double)split.Panel1.Height / (double)split.Height :
                (double)split.Panel1.Width / (double)split.Width;

            Properties.Settings.Default.SplitHorizontal = SplitHorizontal;
            Properties.Settings.Default.EnableImagePreview = EnableImagePreview;
            Properties.Settings.Default.EnableAutoRotatePreview = EnableAutoRotatePreview;
            Properties.Settings.Default.SourceDirectory = tbDirectory.Text;
            Properties.Settings.Default.Filter = tbFilter.Text;
            Properties.Settings.Default.FirstIndex = GetFirstIndex();
            Properties.Settings.Default.Save();

            base.OnClosed(e);
        }

        private IEnumerable<FileRenameInfo> Infos
        {
            get { return binder.Cast<FileRenameInfo>(); }
        }

        private IEnumerable<FileRenameInfo> SelectedInfos
        {
            get { return Infos.Where(info => info.Selected); }
        }

        private bool EnableImagePreview
        {
            get { return enableImagePreviewToolStripMenuItem.Checked; }
            set { enableImagePreviewToolStripMenuItem.Checked = value; }
        }

        private bool EnableAutoRotatePreview
        {
            get { return cbAutoRotate.Checked; }
            set { cbAutoRotate.Checked = value; }
        }

        private bool SplitHorizontal
        {
            get { return splitHorizontalToolStripMenuItem.Checked; }
            set { splitHorizontalToolStripMenuItem.Checked = value; }
        }

        private int GetFirstIndex()
        {
            int index = 1;
            try { index = Convert.ToInt32(nudFirstIndex.Value); }
            catch (Exception ex)
            {
                ErrorBox.Show(this, string.Format("Could not convert {0} into an integer. The value 1 will be used:\r\n\r\n{1}",
                    nudFirstIndex.Value, ex));
                nudFirstIndex.Value = 1M;
            }

            return index;
        }

        private int GetDefaultSplitterDistance()
        {
            var distance = 0;
            var ratio = Properties.Settings.Default.SplitterRatio;

            if (split.Orientation == Orientation.Horizontal)
                distance = (int)((double)split.Height * ratio);
            else distance = (int)((double)split.Width * ratio);

            if (distance != 0) return distance;
            return split.Orientation == Orientation.Horizontal ?
                split.Height / 2 : split.Width / 2;
        }

        private void UpdateImagePreview()
        {
            var data = (FileRenameInfo)binder.Current;
            if (data == null) return;
            var enableExifRotation = EnableAutoRotatePreview;

            var thread = new Thread((state) =>
            {
                wpfViewer.LoadImageAsync(data, enableExifRotation);

                var bitmap = ReadBitmap(data.GetFileName());
                pg.Invoke(new Action(() => pg.SelectedObject = bitmap));
                rtb.Invoke(new Action(() =>
                {
                    rtb.Clear();
                    if (data.Exif == null) return;
                    foreach (Pair pair in data.Exif)
                        rtb.AppendText(pair.First + ": " + pair.Second + "\r\n");
                }));
            });

            //thread.SetApartmentState(ApartmentState.STA);

            if (currentPreviewThread != null)
                currentPreviewThread.Abort();
            currentPreviewThread = thread;
            currentPreviewThread.Start();            
        }

        private Bitmap ReadBitmap(string filename)
        {
            Bitmap bitmap = null;
            using (var stream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                bitmap = new Bitmap(stream);
                stream.Flush();
                stream.Close();
            }

            return bitmap;
        }

        private void RefreshGrid() { binder.ResetBindings(false); }

        private void FillGrid()
        {
            var result = new List<FileRenameInfo>();
            try
            {
                var filters = tbFilter.Text.Split(';');

                foreach (var filter in filters)
                {
                    string[] files = Directory.GetFiles(tbDirectory.Text, filter, SearchOption.TopDirectoryOnly);
                    foreach (var file in files)
                        result.Add(new FileRenameInfo(file));
                }
            }
            catch (Exception ex)
            {
                ErrorBox.Show(this, string.Format("Could not retrieve files list:\r\n\r\n{0}", ex.ToString()));
            }

            result = result.OrderBy(fri => fri.PhotoTime).ToList();
            binder.DataSource = result;
        }

        private void SetSelectionForAllRows(bool selected)
        {
            foreach (var info in Infos) info.Selected = selected;
            RefreshGrid();
        }

        private void Browse()
        {
            using (var browser = new FolderBrowserDialogEx())
            {
                browser.SelectedPath = tbDirectory.Text;
                browser.ShowNewFolderButton = true;
                browser.Description = "Select the source directoy";

                if (browser.ShowDialog(this) == DialogResult.OK)
                    tbDirectory.Text = browser.SelectedPath;
            }
        }

        private void Load()
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                FillGrid();
            }
            finally { Cursor = Cursors.Default; }
        }

        private void RunPreview()
        {
            int index = GetFirstIndex();
            foreach (var info in Infos)
            {
                if (info.Selected)
                {
                    info.CreateNewName(index);
                    if (cbSkipUnselected.Checked) index++;
                }

                if (!cbSkipUnselected.Checked) index++;
            }

            RefreshGrid();
        }

        private void Rename()
        {
            foreach (var info in SelectedInfos) 
                info.Rename();
            RefreshGrid();
        }

        private void btnBrowse_Click(object sender, EventArgs e) { Browse(); }

        private void btnLoad_Click(object sender, EventArgs e) { Load(); }
        
        private void binder_CurrentItemChanged(object sender, EventArgs e)
        {
            if (!EnableImagePreview) return;
            UpdateImagePreview();
        }

        private void btnPreview_Click(object sender, EventArgs e) { RunPreview(); }

        private void btnRename_Click(object sender, EventArgs e) { Rename(); }

        private void btnSelectAll_Click(object sender, EventArgs e) { SetSelectionForAllRows(true); }

        private void btnUnselectAll_Click(object sender, EventArgs e) { SetSelectionForAllRows(false); }

        private void tbFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                FillGrid();
                e.Handled = true;
            }
        }

        private void tbDirectory_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Load();
                e.Handled = true;
            }
        }

        private void enableImagePreviewToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (!enableImagePreviewToolStripMenuItem.Checked)
            {
                split.SplitterDistance = split.Orientation == Orientation.Horizontal ?
                    split.Height : split.Width;
                wpfViewer.ResetImage();
            }
            else
            {
                split.SplitterDistance = GetDefaultSplitterDistance();
                UpdateImagePreview();
            }
        }

        private void splitHorizontalToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            var newOrientation = splitHorizontalToolStripMenuItem.Checked ?
                Orientation.Horizontal : Orientation.Vertical;

            if (newOrientation != split.Orientation)
            {
                if (split.Orientation == Orientation.Vertical)
                {
                    var ratio = (double)split.Panel1.Width / (double)split.Width;
                    split.Orientation = Orientation.Horizontal;
                    split.SplitterDistance = (int)((double)split.Height * ratio);
                }
                else
                {
                    var ratio = (double)split.Panel1.Height / (double)split.Height;
                    split.Orientation = Orientation.Vertical;
                    split.SplitterDistance = (int)((double)split.Width * ratio);
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog(this);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) { Close(); }

        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgv.SelectedRows)
                ((FileRenameInfo)row.DataBoundItem).Selected = true;

            RefreshGrid();
        }

        private void unselectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgv.SelectedRows)
                ((FileRenameInfo)row.DataBoundItem).Selected = false;

            RefreshGrid();
        }

        private void clearGeneratedNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgv.SelectedRows)
            {
                ((FileRenameInfo)row.DataBoundItem).NewFileName = string.Empty;
                ((FileRenameInfo)row.DataBoundItem).Result = string.Empty;
            }

            RefreshGrid();
        }

        private void cm_Opening(object sender, CancelEventArgs e)
        {
            var count = dgv.SelectedRows.Count;
            foreach (ToolStripItem item in cm.Items)
            {
                if (item == selectAllToolStripMenuItem) continue;
                if (item == unselectAllToolStripMenuItem) continue;
                item.Enabled = count > 0;
            }

            selectAllToolStripMenuItem.Enabled = unselectAllToolStripMenuItem.Enabled =
                dgv.RowCount > 0;
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetSelectionForAllRows(true);
        }

        private void unselectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetSelectionForAllRows(false);
        }

        private void clearDescriptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgv.SelectedRows)
                ((FileRenameInfo)row.DataBoundItem).Description = string.Empty;

            RefreshGrid();
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgv.SelectedRows)
            {
                var data = (FileRenameInfo)row.DataBoundItem;

                data.NewFileName = string.Empty;
                data.Result = string.Empty;
                data.Description = string.Empty;
            }

            RefreshGrid();
        }

        private void cbAutoRotate_CheckedChanged(object sender, EventArgs e)
        {
            if (!loaded) return;
            UpdateImagePreview();
        }

        private void btnClearDescriptions_Click(object sender, EventArgs e)
        {
            foreach (var info in SelectedInfos)
                info.Description = string.Empty;
            RefreshGrid();
        }
    }
}
