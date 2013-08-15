using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;

namespace Delta.CertXplorer.UI.ToolWindows
{
    /// <summary>
    /// Displays a <see cref="LogWindowControl"/> or a <see cref="SimpleLogWindowControl"/> inside a tool window.
    /// </summary>
    public partial class LogWindow : ToolWindow
    {
        private bool useSimpleControl = false;
        private LogWindowControl logControl = null;
        private SimpleLogWindowControl simpleLogControl = null;

        public LogWindow() : this(false) { }

        public LogWindow(bool simpleControl)
        {
            InitializeComponent();

            useSimpleControl = simpleControl;

            if (simpleControl)
            {
                simpleLogControl = new SimpleLogWindowControl();
                simpleLogControl.Dock = DockStyle.Fill;
                simpleLogControl.Name = "simpleLogControl";
                simpleLogControl.TabIndex = 0;
                Controls.Add(simpleLogControl);
            }
            else
            {
                logControl = new LogWindowControl();
                logControl.Dock = DockStyle.Fill;
                logControl.Name = "logControl";
                logControl.TabIndex = 0;
                Controls.Add(logControl);
            }

            base.Icon = Properties.Resources.LogIcon;
            base.TabText = base.Text = SR.Log;
            base.ToolTipText = SR.LogWindow;

            if (!simpleControl) logControl.LinkClicked += (s, e) => HandleUrl(e.LinkText);
        }

        public bool UseSimpleControl
        {
            get { return useSimpleControl; }
        }

        public override Guid Guid
        {
            get { return new Guid("{2A823517-2135-4417-9B82-3A43EDBE4532}"); }
        }

        protected override DockState DefaultDockState
        {
            get { return DockState.DockBottom; }
        }

        /// <summary>
        /// Gets or sets the render mode used by this tool window's toolstrip and menu.
        /// </summary>
        /// <value>The render mode.</value>
        protected ToolStripRenderMode RenderMode
        {
            get 
            {
                if (useSimpleControl) return ToolStripRenderMode.ManagerRenderMode;
                else return logControl.RenderMode; 
            }
            set 
            {
                if (!useSimpleControl) logControl.RenderMode = value; 
            }
        }

        private void HandleUrl(string url)
        {
            if (useSimpleControl) return;

            if (string.IsNullOrEmpty(url)) return;

            if (url.StartsWith("file://")) // Check existence
            {
                string path = url.Substring(7);
                if (string.IsNullOrEmpty(path)) return;
                if (!File.Exists(path) && !Directory.Exists(path))
                {
                    ErrorBox.Show(this, string.Format(SR.PathNotFound, path));
                    return;
                }
            }

            try { Process.Start(url); }
            catch (Exception ex)
            {
                ErrorBox.Show(this, string.Format(SR.CantHandleUrl, url, ex.ToFormattedString()));
            }
        }
    }
}
