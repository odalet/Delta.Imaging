using System;
using System.Windows.Forms;

namespace Delta.CertXplorer.UI
{
    public interface ITreeViewExContextMenuStripProvider
    {
        ContextMenuStrip GetContextMenuStrip(TreeNodeEx node);
    }
}
