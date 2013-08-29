using System;
using Delta.CertXplorer.UI;

namespace Delta.CertXplorer.DocumentModel
{
    /// <summary>
    /// Represents the UI displaying a document.
    /// </summary>
    public interface IDocumentView : IView
    {
        /// <summary>
        /// Sets the document this view displays.
        /// </summary>
        /// <param name="doc">The document.</param>
        void SetDocument(IDocument doc);

        /// <summary>
        /// Gets the document displayed by this view.
        /// </summary>
        /// <value>The document.</value>
        IDocument Document { get; }
    }
}
