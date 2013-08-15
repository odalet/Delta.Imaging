using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace Delta.CertXplorer.UI
{
    partial class TreeViewEx
    {
        protected class Sorter : System.Collections.IComparer, IComparer<TreeNode>
        {
            public Sorter() { }

            #region IComparer Members

            int System.Collections.IComparer.Compare(object x, object y)
            {
                if ((x is TreeNode) && (y is TreeNode))
                    return DoCompare((TreeNode)x, (TreeNode)y);
                else return 0;
            }

            #endregion

            #region IComparer<SolutionTreeNode> Members

            int IComparer<TreeNode>.Compare(TreeNode x, TreeNode y) { return DoCompare(x, y); }

            #endregion

            protected virtual int DoCompare(TreeNode x, TreeNode y)
            {
                Debug.Assert(x != null);
                Debug.Assert(y != null);

                TreeNodeEx node1 = x as TreeNodeEx;
                TreeNodeEx node2 = y as TreeNodeEx;

                if (node1 == null || node2 == null) return x.Text.CompareTo(y.Text);

                return node1.CompareTo(node2);
            }
        }
    }
}
