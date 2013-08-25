using System;

namespace Delta.CertXplorer.DocumentModel
{
    /// <summary>
    /// Handles document-related events.
    /// </summary>
    public delegate void DocumentEventHandler(object sender, DocumentEventArgs e);

    /// <summary>
    /// Contains data for the <see cref="DocumentEventHandler"/> event.
    /// </summary>
    public class DocumentEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentEventArgs"/> class.
        /// </summary>
        /// <param name="document">The document.</param>
        public DocumentEventArgs(IDocument document)
        {
            Document = document;
        }

        /// <summary>
        /// Gets the document.
        /// </summary>
        /// <value>The document.</value>
        public IDocument Document
        {
            get;
            private set;
        }
    }
}
