namespace Delta.CertXplorer.DocumentModel
{
    /// <summary>
    /// Represents a document.
    /// </summary>
    public interface IDocument
    {
        /// <summary>
        /// Sets the document source.
        /// </summary>
        /// <param name="source">The source.</param>
        void SetSource(IDocumentSource source);

        /// <summary>
        /// Gets or sets the document handler associated with this document.
        /// </summary>
        IDocumentHandler Handler { get; set; }

        /// <summary>
        /// Gets the document source.
        /// </summary>
        IDocumentSource Source { get; }
        
        /// <summary>
        /// Gets the document caption.
        /// </summary>
        string Caption { get; }

        /// <summary>
        /// Gets this document key (used to store document instances in dictionaries).
        /// </summary>
        string Key { get; }
    }
}
