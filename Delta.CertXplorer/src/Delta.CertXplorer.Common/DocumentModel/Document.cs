namespace Delta.CertXplorer.DocumentModel
{
    public abstract class Document
    {
        /// <summary>
        /// Creates the view that will contain this document.
        /// </summary>
        /// <returns>An instance of a view.</returns>
        public abstract IDocumentView CreateView();
    }
}
