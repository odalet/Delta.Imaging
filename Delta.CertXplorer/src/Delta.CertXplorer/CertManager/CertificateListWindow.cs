using System;

using WeifenLuo.WinFormsUI.Docking;

using Delta.CertXplorer;
using Delta.CertXplorer.UI.ToolWindows;

namespace Delta.CertXplorer.CertManager
{
    public partial class CertificateListWindow : ToolWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CertificateListWindow"/> class.
        /// </summary>
        public CertificateListWindow()
        {
            InitializeComponent();

            certificateListControl.Services = This.Services;

            base.DockAreas |= DockAreas.Document;

            base.TabText = "Certificates";
            base.ToolTipText = "Certificates List";
            base.Icon = Properties.Resources.CertificatesIcon;
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
            get { return new Guid("{9452B3A0-7EFA-4638-B3E4-57B9479CBDFB}"); }
        }

        /// <summary>
        /// Gets the default docking state.
        /// </summary>
        /// <value>The default docking state.</value>
        protected override DockState DefaultDockState
        {
            get { return DockState.Document; }
        }
    }
}
