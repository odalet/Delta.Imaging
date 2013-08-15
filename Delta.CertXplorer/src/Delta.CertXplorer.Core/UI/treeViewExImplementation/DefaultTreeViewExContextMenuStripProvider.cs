using System;
using System.Windows.Forms;

namespace Delta.CertXplorer.UI
{
    public class DefaultTreeViewExContextMenuStripProvider : ITreeViewExContextMenuStripProvider
    {
        protected TreeViewEx treeView = null;

        public DefaultTreeViewExContextMenuStripProvider(TreeViewEx treeView) { this.treeView = treeView; }

        #region ITreeViewExContextMenuStripProvider Members

        public virtual ContextMenuStrip GetContextMenuStrip(TreeNodeEx node)
        {
            // Example
            //ContextMenuStrip strip = new ContextMenuStrip();
            //strip.Items.Add("Menu 1");
            //strip.Items.Add("Menu 2");
            //return strip;

            return null; 
        }

        #endregion
    }
}
