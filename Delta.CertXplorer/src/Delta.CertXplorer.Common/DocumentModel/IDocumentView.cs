using System;

namespace Delta.CertXplorer.DocumentModel
{
    /// <summary>
    /// Represents the UI displaying a document.
    /// </summary>
    public interface IDocumentView
    {
        /// <summary>
        /// Occurs when this view is closed.
        /// </summary>
        event EventHandler ViewClosed;

        /// <summary>
        /// Gets the document diplayed by this view.
        /// </summary>
        /// <value>The document.</value>
        IDocument Document { get; }

        /// <summary>
        /// Closes this view.
        /// </summary>
        void Close();
    }
}
