using System.Windows.Forms;

using Delta.CertXplorer.UI;

namespace Delta.CertXplorer.CertManager
{
    internal partial class CertificateStoreTreeView : TreeViewEx
    {
        private class MenuProvider : ITreeViewExContextMenuStripProvider
        {
            private ContextMenuStrip menuStrip = null;

            public MenuProvider(ContextMenuStrip cm) { menuStrip = cm; }

            #region ITreeViewExContextMenuStripProvider Members

            public ContextMenuStrip GetContextMenuStrip(TreeNodeEx node)
            {
                return menuStrip;
            }

            #endregion
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="CertificateStoreTreeView"/> class.
        /// </summary>
        public CertificateStoreTreeView()
        {
            InitializeComponent();

            //base.AllowDrag = false;
            base.AllowDrop = false;
            
            //base.ContextMenuStrip = mstrip;
            //base.ContextMenuStripProvider = CreateContextMenuStripProvider();
        }

        private ITreeViewExContextMenuStripProvider CreateContextMenuStripProvider()
        {
            return new MenuProvider(mstrip);
        }        
    }
}
