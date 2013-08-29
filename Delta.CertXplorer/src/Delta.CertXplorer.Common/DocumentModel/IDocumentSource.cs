namespace Delta.CertXplorer.DocumentModel
{
    public interface IDocumentSource
    {
        string Uri { get; }

        /// <summary>
        /// Gets a value indicating whether this source is read only.
        /// </summary>
        bool IsReadOnly { get; }
    }
}
