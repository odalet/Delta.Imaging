using System;
using System.Windows.Forms;

namespace Delta.CertXplorer.UI
{
    public delegate void TreeNodeExEventHandler(object sender, TreeNodeExEventArgs e);

    public class TreeNodeExEventArgs : EventArgs
    {
        private TreeNodeEx node = null;
        public TreeNodeExEventArgs(TreeNodeEx bstn) : base() { node = bstn; }
        public TreeNodeEx Node { get { return node; } }
    }

    public delegate void TreeNodeCollectionEventHandler(object sender, TreeNodeCollectionEventArgs e);

    public class TreeNodeCollectionEventArgs : EventArgs
    {
        private TreeNodeCollection nodes = null;
        public TreeNodeCollectionEventArgs(TreeNodeCollection coll) : base() { nodes = coll; }
        public TreeNodeCollection Nodes { get { return nodes; } }
    }
}
