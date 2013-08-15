using System;

using Delta.CertXplorer.UI;

using WeifenLuo.WinFormsUI.Docking;

namespace Delta.CertXplorer.CertManager
{
    public partial class PropertiesWindow : ServicedToolWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertiesWindow"/> class.
        /// </summary>
        public PropertiesWindow()
        {
            InitializeComponent();

            base.Icon = Properties.Resources.Properties;
            base.TabText = base.Text = SR.Properties;
            base.ToolTipText = SR.PropertiesWindow;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        public override Guid Guid
        {
            get { return new Guid("{70B5E014-8DAC-4cfb-9A5A-5D5E93BA6DFE}"); }
        }

        protected override DockState DefaultDockState
        {
            get { return DockState.DockRight; }
        }
    }
}
