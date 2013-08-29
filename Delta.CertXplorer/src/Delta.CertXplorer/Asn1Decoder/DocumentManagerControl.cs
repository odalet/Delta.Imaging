using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

using Delta.CertXplorer.UI;
using Delta.CertXplorer.Services;
using Delta.CertXplorer.Commanding;
using Delta.CertXplorer.CertManager;
using Delta.CertXplorer.DocumentModel;

namespace Delta.CertXplorer.Asn1Decoder
{
    public partial class DocumentManagerControl : ServicedUserControl, ISelectionSource
    {
        private class MenuProvider : ITreeViewExContextMenuStripProvider
        {
            private ContextMenuStrip menuStrip = null;

            /// <summary>
            /// Initializes a new instance of the <see cref="MenuProvider"/> class.
            /// </summary>
            /// <param name="cm">The context menu.</param>
            public MenuProvider(ContextMenuStrip cm) { menuStrip = cm; }

            #region ITreeViewExContextMenuStripProvider Members

            public ContextMenuStrip GetContextMenuStrip(TreeNodeEx node)
            {
                if (node.Tag != null && node.Tag is IDocument)
                    return menuStrip;
                else return null;
            }

            #endregion
        }

        private const int documentsIndex = 0;
        private const int closedFolderIndex = 1;
        private const int openedFolderIndex = 2;
        private const int documentIndex = 3;

        private IServiceProvider services = null;
        private TreeNode filesRoot = null;
        private TreeNode certificatesRoot = null;

        private Dictionary<IDocument, TreeNode> documents =
            new Dictionary<IDocument, TreeNode>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentManagerControl"/> class.
        /// </summary>
        public DocumentManagerControl() 
        {
            InitializeComponent();
            //tvExplorer.AllowDrag = false;

            tvExplorer.ContextMenuStripProvider = new MenuProvider(mstrip);
        }

        #region ISelectionSource Members

        /// <summary>
        /// Occurs when the currently selected object has changed.
        /// </summary>
        public event EventHandler SelectionChanged;

        /// <summary>
        /// Gets the currently selected object.
        /// </summary>
        /// <value>The selected object.</value>
        public object SelectedObject
        {
            get { return SelectedDocument; }
        }

        #endregion

        /// <summary>
        /// Initializes this instance with the specified service provider.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public void Initialize(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) throw new ArgumentNullException("serviceProvider");
            services = serviceProvider;

            var service = services.GetService<IDocumentManagerService>(true);
            service.DocumentAdded += (s, e) => OnDocumentAdded(s, e);
            service.DocumentRemoved += (s, e) => OnDocumentRemoved(s, e); 
            service.DocumentSelected += (s, e) => OnDocumentSelected(s, e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.UserControl.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;

            var root = new TreeNode("Documents");
            root.ImageIndex = documentsIndex;

            filesRoot = CreateFolderTreeNode(SR.Files);
            certificatesRoot = CreateFolderTreeNode(SR.Certificates);

            root.Nodes.Add(filesRoot);
            root.Nodes.Add(certificatesRoot);
            tvExplorer.Nodes.Add(root);
            root.Expand();

            GlobalSelectionService
                .GetOrCreateSelectionService(Services)
                .AddSource(this);

            InitializeActions();

            tvExplorer.SelectedNodeChanged += (s, ev) => NotifySelectionChanged();

            tvExplorer.NodeMouseDoubleClick += (s, ev) =>
            {
                var doc = ev.Node.Tag as IDocument;
                if (doc != null) Commands.RunVerb(Verbs.OpenExisting, doc);
                    //services.GetService<IDocumentManagerService>(true).OpenDocument(doc);
            };

            tvExplorer.DragDrop += (s, ev) =>
            {
                var docList = (string[])ev.Data.GetData(DataFormats.FileDrop);
                if (docList.Length != 0)
                {
                    foreach (string doc in docList)
                    {
                        if (File.Exists(doc))
                            Commands.RunVerb(Verbs.OpenFile, doc);
                    }
                }
            };

            tvExplorer.DragOver += (s, ev) => ev.Effect = DragDropEffects.Copy;
        }

        private IDocument SelectedDocument
        {
            get
            {
                var node = tvExplorer.SelectedNode;
                return node.Tag as IDocument;
            }
        }

        private void InitializeActions()
        {
            openAction.Run += (s, ev) =>
            {
                var doc = SelectedDocument;
                if (doc != null) Commands.RunVerb(Verbs.OpenExisting, doc);
            };

            closeAction.Run += (s, ev) =>
            {
                var doc = SelectedDocument;
                if (doc != null) Commands.RunVerb(Verbs.CloseDocument, doc);
            };
        }

        private FolderTreeNode CreateFolderTreeNode(string text)
        {
            var node = new FolderTreeNode(text);
            node.CollapsedImageIndex = closedFolderIndex;
            node.SelectedCollapsedImageIndex = closedFolderIndex;
            node.ExpandedImageIndex = openedFolderIndex;
            node.SelectedExpandedImageIndex = openedFolderIndex;

            return node;
        }

        /// <summary>
        /// Called when [document selected].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Delta.CertXplorer.Common.DocumentModel.DocumentEventArgs"/> instance containing the event data.</param>
        private void OnDocumentSelected(object sender, DocumentEventArgs e)
        {
            if (documents.ContainsKey(e.Document))
                tvExplorer.SelectedNode = documents[e.Document];
        }

        /// <summary>
        /// Called when [document removed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Delta.CertXplorer.Common.DocumentModel.DocumentEventArgs"/> instance containing the event data.</param>
        private void OnDocumentRemoved(object sender, DocumentEventArgs e)
        {
            if (documents.ContainsKey(e.Document))
            {
                TreeNode tn = documents[e.Document];
                documents.Remove(e.Document);
                filesRoot.Nodes.Remove(tn);
            }
        }

        /// <summary>
        /// Called when [document added].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Delta.CertXplorer.Common.DocumentModel.DocumentEventArgs"/> instance containing the event data.</param>
        private void OnDocumentAdded(object sender, DocumentEventArgs e)
        {
            if (!documents.ContainsKey(e.Document))
            {
                var tnRoot = filesRoot;
                
                var caption = "Document";
                if (e.Document is IDocument)
                    caption = ((IDocument)e.Document).Caption;

                var tn = new TreeNodeEx(caption);

                if (e.Document.Source is FileDocumentSource)
                    tnRoot = filesRoot;
                else if (e.Document.Source is X509DocumentSource)
                    tnRoot = certificatesRoot;

                tn.Tag = e.Document;
                tn.ImageIndex = documentIndex;
                tn.SelectedImageIndex = documentIndex;
                documents.Add(e.Document, tn);
                tnRoot.Nodes.Add(tn);

                tnRoot.Expand();
            }
        }

        /// <summary>
        /// Notifies that the current selection has changed.
        /// </summary>
        private void NotifySelectionChanged()
        {
            if (SelectedDocument != null)
                SelectionChanged(this, EventArgs.Empty);
        }
    }
}
