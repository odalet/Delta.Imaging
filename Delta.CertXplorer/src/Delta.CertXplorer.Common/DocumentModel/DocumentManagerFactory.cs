
namespace Delta.CertXplorer.DocumentModel
{
    /// <summary>
    /// Allows for creation of a document manager service.
    /// </summary>
    public static class DocumentManagerFactory
    {
        /// <summary>
        /// Creates a document manager service.
        /// </summary>
        /// <param name="chrome">The chrome that contains the views.</param>
        /// <returns>
        /// An instance of an object implementing <see cref="IDocumentManagerService"/>.
        /// </returns>
        public static IDocumentManagerService CreateManager(IDocumentBasedUI chrome)
        {
            return new DocumentManagerService(chrome);
        }
    }
}
