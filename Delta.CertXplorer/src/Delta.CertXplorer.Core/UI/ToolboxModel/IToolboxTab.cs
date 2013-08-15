using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Delta.CertXplorer.UI.ToolboxModel
{
    public interface IToolboxTab
    {
        void AddPointer();
        void AddTool(Type type);
    }
}
