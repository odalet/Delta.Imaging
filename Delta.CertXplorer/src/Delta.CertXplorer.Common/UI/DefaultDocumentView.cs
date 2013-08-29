using System;
using WeifenLuo.WinFormsUI.Docking;
using Delta.CertXplorer.DocumentModel;

namespace Delta.CertXplorer.UI
{
    public partial class DefaultDocumentView : DockContent, IDocumentView
    {
        private IDocument document = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultDocumentView"/> class.
        /// </summary>
        public DefaultDocumentView()
        {
            InitializeComponent();
        }

        #region IDocumentView Members

        public void SetDocument(IDocument doc)
        {
            if (doc == null) throw new ArgumentNullException("doc");
            if (document != null) throw new InvalidOperationException("This view's document can only be set once.");

            document = doc;

            UpdateInfo();
        }

        public IDocument Document
        {
            get { return Document; }
        }

        #endregion
        
        #region IView Members

        public event EventHandler ViewClosed;

        #endregion

        protected virtual void UpdateInfo()
        {
            base.Text = document.Caption;

            documentCaptionBox.Text = document.Caption;
            documentKeyBox.Text = document.Key;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Closed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (ViewClosed != null) ViewClosed(this, EventArgs.Empty);
        }
    }
}
