using System;
using WeifenLuo.WinFormsUI.Docking;
using Delta.CertXplorer.DocumentModel;

namespace Delta.CertXplorer.Asn1Decoder
{
    public partial class Asn1DocumentView : DockContent, IDocumentView
    {
        private IDocument document = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Asn1DocumentView"/> class.
        /// </summary>
        public Asn1DocumentView()
        {
            InitializeComponent();
        }

        #region IDocumentView Members

        /// <summary>
        /// Occurs when this view is closed.
        /// </summary>
        public event EventHandler ViewClosed;

        public void SetDocument(IDocument doc)
        {
            if (doc == null) throw new ArgumentNullException("doc");
            if (!(doc is IDocumentData<byte[]>)) throw new NotSupportedException(string.Format(
                "Documents of type {0} are not supported in this view.", document.GetType()));

            document = doc;
            asn1Viewer.SetData(((IDocumentData<byte[]>)document).Data);
            base.Text = document.Caption;
        }

        /// <summary>
        /// Gets the document diplayed by this view.
        /// </summary>
        /// <value>The document.</value>
        public IDocument Document
        {
            get { return document; }
        }

        #endregion

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
