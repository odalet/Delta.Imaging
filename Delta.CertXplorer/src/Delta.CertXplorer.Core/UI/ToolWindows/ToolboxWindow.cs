using System;

using WeifenLuo.WinFormsUI.Docking;

namespace Delta.CertXplorer.UI.ToolWindows
{
    public partial class ToolboxWindow : ToolWindow
    {
        public ToolboxWindow()
        {
            InitializeComponent();

            base.Icon = Properties.Resources.ToolboxIcon;
            base.TabText = base.Text = SR.Toolbox;
            base.ToolTipText = SR.ToolboxWindow;
        }

        public override Guid Guid
        {
            get { return new Guid("{0559FE22-EAC1-4590-8C20-D69F0326BD85}"); }
        }

        protected override DockState DefaultDockState
        {
            get { return DockState.DockLeft; }
        }
    }
}
