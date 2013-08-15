using System;
using System.Drawing;

using Delta.CertXplorer.UI.ToolboxModel;

namespace Delta.CertXplorer.UI.ToolboxImplementation
{
    internal class ToolboxTool : BaseToolboxTool
    {
        public ToolboxTool() : base() { }
        public ToolboxTool(Type toolType) : base(toolType) { }
    }

    internal class ToolboxPointer : ToolboxTool
    {
        public ToolboxPointer(Type toolType) : base(toolType) { }
        public ToolboxPointer() : base() { }

        public override bool IsPointer { get { return true; } }

        protected override Bitmap GetBitmap()
        {
            Bitmap bmp = Delta.CertXplorer.Properties.Resources.pointer;
            bmp.MakeTransparent(Color.Magenta);
            return bmp;
        }

        protected override string GetDisplayName() { return "Pointer"; }
    }
}
