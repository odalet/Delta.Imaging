using System;
using System.Windows.Forms;
using System.Collections.Generic;

using Delta.CertXplorer.IO;
using Delta.CertXplorer.Logging;
using Delta.CertXplorer.UI.Theming;

namespace Delta.CertXplorer.UI.ToolWindows
{
    public partial class LogWindowControl : UserControl
    {
        private List<ToolStripItem> items = new List<ToolStripItem>();
        private LogLevel selectedLevel = LogLevel.All;
        private ITextBoxAppender appender = null;
        private IFileService fileService = null;

        public LogWindowControl()
        {
            InitializeComponent();
            CanOverrideRenderer = true;

            ThemesManager.RegisterThemeAwareControl(this, (renderer) =>
            {
                if (CanOverrideRenderer)
                {
                    if (renderer is ToolStripProfessionalRenderer)
                        ((ToolStripProfessionalRenderer)renderer).RoundedEdges = false;

                    ts.Renderer = renderer;
                }
            });
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.UserControl.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Initialize();
        }

        /// <summary>
        /// Gets or sets the render mode used by this control's toolstrip and menu.
        /// </summary>
        /// <value>The render mode.</value>
        internal ToolStripRenderMode RenderMode
        {
            get { return ts.RenderMode; }
            set
            {
                ts.RenderMode = value;
                cm.RenderMode = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the global toolstrip renderer 
        /// can be overriden in this instance.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can override renderer; otherwise, <c>false</c>.
        /// </value>
        public bool CanOverrideRenderer { get; set; }

        public TextBoxBase GetTextBox() { return tb; }

        public bool WordWrap { get { return tb.WordWrap; } }

        public int TextLength { get { return tb.TextLength; } }

        public LogLevel SelectedLevel
        {
            get { return selectedLevel; }
            set
            {
                if (value != selectedLevel)
                {
                    selectedLevel = value;
                    logLevelList.SelectedItem = selectedLevel;
                }
            }
        }

        public event EventHandler WordWrapChanged;

        public event LinkClickedEventHandler LinkClicked;

        public void CopyLog()
        {
            if (tb.SelectionLength == 0)
            {
                tb.SelectAll();
                tb.Copy();
                tb.Select(tb.Text.Length - 1, 0);
                tb.ScrollToCaret();
            }
            else tb.Copy();
        }

        public void ClearLog() { tb.Clear(); }

        public void SelectLog() { tb.SelectAll(); }

        public void ToggleWordWrapLog()
        {
            tb.WordWrap = !tb.WordWrap;
            if (WordWrapChanged != null) WordWrapChanged(this, null);
        }

        public void SaveLog()
        {
            var fs = GetFileService();
            if (fs == null)
            {
                This.Logger.Error("Can't get an IFileService instance");
                return;
            }

            bool foo = fs.DirectoryExists("c:\\");
            var result = fs.SafeSave((filename, type) =>
            {
                if (string.IsNullOrEmpty(filename)) return;

                var isRtf = (type == FileType.RTF || FileType.RTF.Matches(filename));
                var streamType = (isRtf ?
                    RichTextBoxStreamType.RichText :
                    RichTextBoxStreamType.UnicodePlainText);
                tb.SaveFile(filename, streamType);
            },
            "log.rtf",
            new FileType[] { FileType.LOG, FileType.RTF, FileType.ALL }, 1, "Save Log As");

            if (result == OperationResult.Failed) This.Logger.Error("Unable to save log to a file");
        }

        public void Print() { tb.Print(); }

        public void PrintWithUI() { tb.Print(true); }

        public void PrintPreview() { tb.PrintPreview(); }

        public void PageSetup() { tb.PageSetup(); }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.EnabledChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            if (appender != null)
                appender.LogThreshold = Enabled ? selectedLevel : LogLevel.Off;
        }

        /// <summary>
        /// Obtains a service implementing <see cref="IFileService"/>.
        /// </summary>
        /// <returns>An object implementing <see cref="IFileService"/>.</returns>
        protected virtual IFileService GetFileService()
        {
            fileService = This.GetService<IFileService>();
            if (fileService == null) // We create our own service privately ...
            {
                try
                {
                    fileService = new FileService(This.Services);
                }
                catch (Exception ex)
                {
                    This.Logger.Error(string.Format(
                        "Unable to create a file service: {0}", ex.Message), ex);
                }
            }

            return fileService;
        }

        private void Initialize()
        {
            ThreadSafeTextBoxWrapper wrapper = null;
            if (tb is RichTextBox) wrapper = new ThreadSafeRichTextBoxWrapper((RichTextBox)tb);
            else wrapper = new ThreadSafeTextBoxWrapper(tb);

            var logService = This.Logger;
            if ((logService != null) && (logService is ITextBoxAppendable))
            {
                var log = (ITextBoxAppendable)logService;
                appender = log.AddLogBox(wrapper, "%date [%thread] (%logger) %-8level- %message%newline");
                if (appender != null) appender.LogThreshold = SelectedLevel;
            }

            var levels = new List<LogLevel>();
            levels.AddRange(Enums<LogLevel>.Values);
            levels.Sort((l1, l2) => (int)l1 - (int)l2);
            levels.ForEach(level => logLevelList.Items.Add(level));

            //TODO: select the log level depending on a settings file.
            logLevelList.SelectedItem = selectedLevel;

            foreach (ToolStripItem item in cm.Items) items.Add(item);
            foreach (ToolStripItem item in ts.Items)
            {
                if ((item != logLeveltoolStripLabel) && (item != logLevelList) && (item != toggleWordWrapToolStripButton))
                    items.Add(item);
            }

            UpdateToolStripItems();

            tb.TextChanged += (s, e) => UpdateToolStripItems();
            tb.LinkClicked += (s, e) =>
            {
                // We send the event to our parent.
                if (LinkClicked != null) LinkClicked(this, e);
            };
        }

        private void DisposeAppender()
        {
            if (appender != null)
            {
                appender.Dispose();
                appender = null;
            }
        }

        private void UpdateToolStripItems()
        {
            foreach (ToolStripItem item in items) item.Enabled = (tb.TextLength > 0);
        }

        #region Event Handlers

        private void saveToolStripButton_Click(object sender, EventArgs e) { SaveLog(); }
        private void copyToolStripButton_Click(object sender, EventArgs e) { CopyLog(); }
        private void clearAllToolStripButton_Click(object sender, EventArgs e) { ClearLog(); }
        private void toggleWordWrapToolStripButton_Click(object sender, EventArgs e) { ToggleWordWrapLog(); }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) { SaveLog(); }
        private void printToolStripMenuItem_Click(object sender, EventArgs e) { Print(); }
        private void copyToolStripMenuItem_Click(object sender, EventArgs e) { CopyLog(); }
        private void deleteAllToolStripMenuItem_Click(object sender, EventArgs e) { ClearLog(); }
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e) { SelectLog(); }

        private void logLevelList_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedLevel = (LogLevel)logLevelList.SelectedItem;
            if (appender != null) appender.LogThreshold = selectedLevel;
        }

        private void printToolStripSplitButton_ButtonClick(object sender, EventArgs e) { Print(); }
        private void quickPrintToolStripMenuItem_Click(object sender, EventArgs e) { Print(); }
        private void printToolStripMenuItem1_Click(object sender, EventArgs e) { PrintWithUI(); }
        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e) { PrintPreview(); }
        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e) { PageSetup(); }

        #endregion
    }
}
