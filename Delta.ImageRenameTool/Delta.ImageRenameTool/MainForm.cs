using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Delta.ImageRenameTool.UI;
using Goheer.Exif;

namespace Delta.ImageRenameTool
{
    public partial class MainForm : Form
    {
        private static readonly string okStatus = "OK";
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

            Text = $"Rename Tool {Assembly.GetExecutingAssembly().GetName().Version}";

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
                (double)split.Panel1.Height / split.Height :
                (double)split.Panel1.Width / split.Width;

            Properties.Settings.Default.SplitHorizontal = SplitHorizontal;
            Properties.Settings.Default.EnableImagePreview = EnableImagePreview;
            Properties.Settings.Default.EnableAutoRotatePreview = EnableAutoRotatePreview;
            Properties.Settings.Default.SourceDirectory = tbDirectory.Text;
            Properties.Settings.Default.Filter = tbFilter.Text;
            Properties.Settings.Default.FirstIndex = GetFirstIndex();
            Properties.Settings.Default.Save();

            base.OnClosed(e);
        }

        private IEnumerable<FileRenameInfo> Infos => binder.Cast<FileRenameInfo>();

        private IEnumerable<FileRenameInfo> SelectedInfos => Infos.Where(info => info.Selected);

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
                ErrorBox.Show(this, $"Could not convert {nudFirstIndex.Value} into an integer. The value 1 will be used:\r\n\r\n{ex}");
                nudFirstIndex.Value = 1M;
            }

            return index;
        }

        private int GetDefaultSplitterDistance()
        {
            var ratio = Properties.Settings.Default.SplitterRatio;

            var distance = split.Orientation == Orientation.Horizontal ?
                (int)(split.Height * ratio) : (int)(split.Width * ratio);

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
                    foreach (var pair in data.Exif)
                        rtb.AppendText($"{pair.Key}: {pair.Value}\r\n");
                }));
            });

            if (currentPreviewThread != null)
                currentPreviewThread.Abort();
            currentPreviewThread = thread;
            currentPreviewThread.Start();
        }

        private Bitmap ReadBitmap(string filename)
        {
            Bitmap bitmap = null;
            try
            {
                using (var stream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    bitmap = new Bitmap(stream);
                    stream.Flush();
                    stream.Close();
                }

                statusLabel.Text = okStatus;
            }
            catch (Exception ex)
            {
                // An error occurred while loading the image file (maybe it does not exist any more...)
                // Let's make an image indicating the problem.
                statusLabel.Text = $"Error: {ex.Message}";
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
                    var files = Directory.GetFiles(tbDirectory.Text, filter, SearchOption.TopDirectoryOnly);
                    foreach (var file in files)
                        result.Add(new FileRenameInfo(file));
                }
            }
            catch (Exception ex)
            {
                ErrorBox.Show(this, $"Could not retrieve files list:\r\n\r\n{ex}");
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

        private void LoadImages()
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

        private void btnBrowse_Click(object sender, EventArgs e) => Browse();

        private void btnLoad_Click(object sender, EventArgs e) => LoadImages();

        private void binder_CurrentItemChanged(object sender, EventArgs e)
        {
            if (!EnableImagePreview) return;
            UpdateImagePreview();
        }

        private void btnPreview_Click(object sender, EventArgs e) => RunPreview();

        private void btnRename_Click(object sender, EventArgs e) => Rename();

        private void btnSelectAll_Click(object sender, EventArgs e) => SetSelectionForAllRows(true);

        private void btnUnselectAll_Click(object sender, EventArgs e) => SetSelectionForAllRows(false);

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
                LoadImages();
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

            if (newOrientation == split.Orientation) return;

            if (split.Orientation == Orientation.Vertical)
            {
                var ratio = (double)split.Panel1.Width / split.Width;
                split.Orientation = Orientation.Horizontal;
                split.SplitterDistance = (int)(split.Height * ratio);
            }
            else
            {
                var ratio = (double)split.Panel1.Height / split.Height;
                split.Orientation = Orientation.Vertical;
                split.SplitterDistance = (int)(split.Width * ratio);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) => new AboutBox().ShowDialog(this);

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) => Close();

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

            var enabled = dgv.RowCount > 0;
            selectAllToolStripMenuItem.Enabled = enabled;
            unselectAllToolStripMenuItem.Enabled = enabled;
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e) => SetSelectionForAllRows(true);

        private void unselectAllToolStripMenuItem_Click(object sender, EventArgs e) => SetSelectionForAllRows(false);

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
