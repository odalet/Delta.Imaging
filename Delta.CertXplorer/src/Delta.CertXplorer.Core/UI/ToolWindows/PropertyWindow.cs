using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;

namespace Delta.CertXplorer.UI.ToolWindows
{
    public partial class PropertyWindow : ToolWindow
    {
        public PropertyWindow()
        {
            InitializeComponent();

            base.Icon = Properties.Resources.PropertiesIcon;
            base.TabText = base.Text = SR.Properties;
            base.ToolTipText = SR.PropertiesWindow;
        }

        public override Guid Guid
        {
            get { return new Guid("{1797B309-958E-4fcd-875F-6FD251FDD811}"); }
        }

        public object SelectedObject     
        {
            get { return propertyControl.SelectedObject; }
            set { propertyControl.SelectedObject = value; }
        }

        protected override DockState DefaultDockState
        {
            get { return DockState.DockRight; }
        }
    }
}
