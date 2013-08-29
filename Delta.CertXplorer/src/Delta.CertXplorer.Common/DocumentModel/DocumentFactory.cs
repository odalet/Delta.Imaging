namespace Delta.CertXplorer.DocumentModel
{
    /// <summary>
    /// Allows for creation of a document manager service.
    /// </summary>
    public static class DocumentFactory
    {
        /// <summary>
        /// Creates a document manager service.
        /// </summary>
        /// <param name="chrome">The chrome that contains the views.</param>
        /// <returns>
        /// An instance of an object implementing <see cref="IDocumentManagerService"/>.
        /// </returns>
        public static IDocumentManagerService CreateDocumentManagerService(IDocumentBasedUI chrome)
        {
            return new DocumentManagerService(chrome);
        }

        /// <summary>
        /// Creates the document handler registry service.
        /// </summary>
        /// <returns>
        /// An instance of an object implementing <see cref="IDocumentHandlerRegistryService"/>.
        /// </returns>
        public static IDocumentHandlerRegistryService CreateDocumentHandlerRegistryService()
        {
            return new DocumentHandlerRegistryService();
        }
    }
}
