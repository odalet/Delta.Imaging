using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace Delta.CertXplorer.UI
{
    [DesignTimeVisible(false)]
    public interface ITreeNodeResolver
    {
        TreeNode FindNode(TreeNodeCollection nodes, string key);
        string GetNodeKey(TreeNode node);
    }
}
