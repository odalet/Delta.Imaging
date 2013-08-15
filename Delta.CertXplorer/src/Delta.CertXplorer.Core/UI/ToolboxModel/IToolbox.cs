using System;
using System.Drawing.Design;

namespace Delta.CertXplorer.UI.ToolboxModel
{
    public interface IToolbox
    {
        IToolboxService ToolboxService { get; }
        IToolboxTab CreateToolboxTab(string id, string text);
        void RefreshLayout();
    }
}
