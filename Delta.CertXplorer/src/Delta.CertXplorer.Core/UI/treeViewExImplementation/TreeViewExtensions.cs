using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Delta.CertXplorer.UI
{
    public static class TreeViewExtensions
    {
        /// <summary>
        /// Finds recursively all the child nodes of the specified tree view
        /// matching the specified predicate.
        /// </summary>
        /// <param name="treeView">The tree view.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Tree nodes matching the predicate.</returns>
        public static IEnumerable<TreeNode> Find(this TreeView treeView, Func<TreeNode, bool> predicate)
        {
            return Find(treeView.Nodes, predicate);
        }

        /// <summary>
        /// Finds recursively all the child nodes of the specified tree node collection
        /// matching the specified predicate.
        /// </summary>
        /// <param name="treeNodes">The tree node collection.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Tree nodes matching the predicate.</returns>
        public static IEnumerable<TreeNode> Find(this TreeNodeCollection treeNodes, Func<TreeNode, bool> predicate)
        {
            return Find(treeNodes.Cast<TreeNode>(), predicate);
        }

        /// <summary>
        /// Finds recursively all the child nodes of the specified tree node 
        /// (including itself) matching the specified predicate.
        /// </summary>
        /// <param name="treeNode">The tree node.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Tree nodes matching the predicate.</returns>
        public static IEnumerable<TreeNode> Find(this TreeNode treeNode, Func<TreeNode, bool> predicate)
        {
            return Find(new TreeNode[] { treeNode }, predicate);
        }

        /// <summary>
        /// Finds recursively all the child nodes of the specified nodes collection 
        /// (including themselves) matching the specified predicate.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Tree nodes matching the predicate.</returns>
        private static IEnumerable<TreeNode> Find(IEnumerable<TreeNode> nodes, Func<TreeNode, bool> predicate)
        {
            if (nodes == null) return new TreeNode[0];

            var result = new List<TreeNode>();
            if (predicate == null) result.AddRange(nodes);
            else result.AddRange(nodes.Where(predicate));
            
            // children
            foreach (var node in nodes)
                result.AddRange(Find(node.Nodes, predicate));

            return result;
        }
    }
}
