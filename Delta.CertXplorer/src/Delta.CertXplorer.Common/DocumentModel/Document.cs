namespace Delta.CertXplorer.DocumentModel
{
    public interface IDocument
    {
        /// <summary>
        /// Creates the view that will contain this document.
        /// </summary>
        /// <returns>An instance of a view.</returns>
        IDocumentView CreateView();

        IDocumentSource Source { get; }

        string Key { get; }
    }
}
