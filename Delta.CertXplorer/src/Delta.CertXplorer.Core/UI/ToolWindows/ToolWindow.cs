using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Delta.CertXplorer.ApplicationModel;

using WeifenLuo.WinFormsUI.Docking;

namespace Delta.CertXplorer.UI.ToolWindows
{
    /// <summary>
    /// Base class for dockable tool windows.
    /// </summary>
    public partial class ToolWindow : DockContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToolWindow"/> class.
        /// </summary>
        public ToolWindow()
        {
            InitializeComponent();

            base.DockAreas = DockAreas.Float |
                DockAreas.DockBottom | DockAreas.DockTop |
                DockAreas.DockLeft | DockAreas.DockRight;

            var service = This.GetService<ILayoutService>();
            if (service != null) service.RegisterForm(Guid.ToString("D"), this);

            base.DockHandler.GetPersistStringCallback = () => Guid.ToString();
        }

        /// <summary>
        /// Gets this tool window globally unique id.
        /// </summary>
        /// <remarks>
        /// Each tool window used by the system must have a distinct guid.
        /// </remarks>
        /// <value>The tool window GUID.</value>
        public virtual Guid Guid
        {
            get { return Guid.Empty; }
        }

        /// <summary>
        /// Gets the default docking state.
        /// </summary>
        /// <value>The default docking state.</value>
        protected virtual DockState DefaultDockState
        {
            get { return DockState.DockLeft; }
        }

        /// <summary>
        /// Docks this window to its default docking state.
        /// </summary>
        /// <remarks>
        /// See <see cref="DefaultDockState"/>.
        /// </remarks>
        public void DockDefault()
        {
            base.DockState = DefaultDockState;
        }

        /// <summary>
        /// Converts this tool window to an object of type <see cref="WeifenLuo.WinFormsUI.Docking.IDockContent"/>.
        /// </summary>
        /// <returns>An object of type <see cref="WeifenLuo.WinFormsUI.Docking.IDockContent"/>.</returns>
        public IDockContent ToDockableWindow() { return (IDockContent)this; } 
    }
}
