using System;
using System.Drawing;
using System.Drawing.Design;

namespace Delta.CertXplorer.UI.ToolboxModel
{
    /// <summary>
    /// Represents a tool that can be added to a toolbox.
    /// </summary>
    internal class BaseToolboxTool : ToolboxItem
    {
        public BaseToolboxTool() : base() { InitializeTool(); }
        public BaseToolboxTool(Type toolType) : base(toolType) { InitializeTool(); }
        
        public virtual bool IsPointer { get { return false; } }
        
        protected virtual Bitmap GetBitmap()
        {
            Bitmap bmp = Delta.CertXplorer.Properties.Resources.Tool;
            bmp.MakeTransparent(Color.Magenta);
            return bmp;
        }

        protected virtual string GetDisplayName() { return "Tool"; }

        private void InitializeTool()
        {
            if (base.Bitmap == null) base.Bitmap = GetBitmap();
            if (string.IsNullOrEmpty(base.DisplayName)) base.DisplayName = GetDisplayName();
        }
    }
}
