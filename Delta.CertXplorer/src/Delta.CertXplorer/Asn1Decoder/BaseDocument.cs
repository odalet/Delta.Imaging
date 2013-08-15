
using Delta.CertXplorer.DocumentModel;

namespace Delta.CertXplorer.Asn1Decoder
{
    /// <summary>
    /// Base document for ASN1 viewer
    /// </summary>
    internal class BaseDocument : Document
    {
        private byte[] data = null;

        /// <summary>
        /// Creates the view that will contain this document.
        /// </summary>
        /// <returns>An instance of a view.</returns>
        public override IDocumentView CreateView()
        {
            var form = new DocumentWindow();
            form.Document = this;
            form.Text = DocumentCaption;
            return form;
        }

        /// <summary>
        /// Gets the document caption as it should appear on the view's title.
        /// </summary>
        /// <value>The document caption.</value>
        public virtual string DocumentCaption
        {
            get { return "doc"; }
        }

        /// <summary>
        /// Gets this document's data.
        /// </summary>
        /// <value>The data.</value>
        public byte[] Data 
        {
            get { return data; } 
        }

        /// <summary>
        /// Opens the document.
        /// </summary>
        protected void OpenDocument()
        {
            data = CreateData();
        }

        /// <summary>
        /// Creates the data this document holds.
        /// </summary>
        /// <returns>An array of bytes.</returns>
        protected virtual byte[] CreateData() { return null; }
    }
}
