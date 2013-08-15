using System;

namespace Delta.CertXplorer.UI.ToolboxImplementation
{
    internal delegate void ToolboxButtonEventHandler(object sender, ToolboxButtonEventArgs e);

    internal class ToolboxButtonEventArgs : EventArgs
    {
        private ToolboxButton button = null;

        public ToolboxButtonEventArgs(ToolboxButton tbb) : base() { button = tbb; }

        public ToolboxButton Button { get { return button; } }
    }
}
