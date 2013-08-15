using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.CertXplorer.UI.ToolboxImplementation
{
    internal class ToolboxButton : DraggableToolStripButton
    {
        public ToolboxButton() : base() { }

        public ToolboxTool Tool { get { return base.Tag as ToolboxTool; } set { base.Tag = value; } }

        protected override object GetDataObject()
        {
            return Tool;
        }
    }
}
