namespace Delta.CertXplorer.DocumentModel
{
    public interface IDocumentSource
    {
        /// <summary>
        /// Gets a value indicating whether this source is read only.
        /// </summary>
        bool IsReadOnly { get; }

        /// <summary>
        /// Gets the document caption as it should appear on the view's title.
        /// </summary>
        string Caption { get; }

        /// <summary>
        /// Gets the data key used in dictionaries.
        /// </summary>
        string DataKey { get; }
    }

    public interface IDocumentSource<T> : IDocumentSource
    {
        /// <summary>
        /// Creates the data that will fill the document.
        /// </summary>
        /// <returns>Data read from the source.</returns>
        T CreateData();
    }
}
