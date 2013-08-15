using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Delta.CertXplorer.UI.ToolWindows
{
    public partial class PropertyWindowControl : UserControl
    {
        public PropertyWindowControl()
        {
            InitializeComponent();
        }

        public object SelectedObject
        {
            get { return pg.SelectedObject; }
            set { pg.SelectedObject = value; }
        }
    }
}
