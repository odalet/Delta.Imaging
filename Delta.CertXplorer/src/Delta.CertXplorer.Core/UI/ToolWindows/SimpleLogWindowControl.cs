using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Delta.CertXplorer.Logging;

namespace Delta.CertXplorer.UI.ToolWindows
{
    /// <summary>
    /// Displays logging data in a very crude way (no coloring, for instance).
    /// In order to provide a more user friendly experience, 
    /// use the <see cref="Delta.CertXplorer.UI.ToolWindows.LogWindowControl"/>.
    /// </summary>
    public partial class SimpleLogWindowControl : UserControl
    {
        private LogLevel selectedLevel = LogLevel.All;
        private ITextBoxAppender appender = null;

        public SimpleLogWindowControl()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Initialize();
        }

        public TextBoxBase GetTextBox() { return tb; }

        public bool WordWrap { get { return tb.WordWrap; } }

        public int TextLength { get { return tb.TextLength; } }

        public LogLevel SelectedLevel { get { return selectedLevel; } }

        public event EventHandler WordWrapChanged;

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

        private void Initialize()
        {
            ThreadSafeTextBoxWrapper wrapper = new ThreadSafeTextBoxWrapper(tb);

            var logService = This.Logger;
            if ((logService != null) && (logService is ITextBoxAppendable))
            {
                var log = (ITextBoxAppendable)logService;
                appender = log.AddLogBox(wrapper);
                if (appender != null) appender.LogThreshold = SelectedLevel;
            }
        }

        private void DisposeAppender()
        {
            if (appender != null)
            {
                appender.Dispose();
                appender = null;
            }
        }
    }
}
