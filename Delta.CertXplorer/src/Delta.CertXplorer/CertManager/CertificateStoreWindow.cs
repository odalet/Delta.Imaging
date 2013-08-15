using System;

using Delta.CertXplorer.UI.ToolWindows;

using WeifenLuo.WinFormsUI.Docking;

namespace Delta.CertXplorer.CertManager
{
    public partial class CertificateStoreWindow : ToolWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CertificateStoreToolWindow"/> class.
        /// </summary>
        public CertificateStoreWindow()
        {
            InitializeComponent();

            base.Icon = Properties.Resources.CertificateStore;
            base.TabText = base.Text = SR.CertificateStore;
            base.ToolTipText = SR.CertificateStoreWindow;
        }

        /// <summary>
        /// Gets this tool window globally unique id.
        /// </summary>
        /// <value>The tool window GUID.</value>
        /// <remarks>
        /// Each tool window used by the system must have a distinct guid.
        /// </remarks>
        public override Guid Guid
        {
            get { return new Guid("{5D07D1F4-652A-4b0b-BB1D-7A269AC7134D}"); }
        }

        protected override DockState DefaultDockState
        {
            get { return DockState.DockBottom; }
        }
    }
}
