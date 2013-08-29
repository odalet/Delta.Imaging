
namespace Delta.CertXplorer.DocumentModel
{
    public interface IDocumentManagerService
    {
        /// <summary>
        /// Occurs when a new document is created.
        /// </summary>
        event DocumentEventHandler DocumentCreated;

        /// <summary>
        /// Occurs when a new document is added.
        /// </summary>
        event DocumentEventHandler DocumentAdded;

        /// <summary>
        /// Occurs when a document is selected.
        /// </summary>
        event DocumentEventHandler DocumentSelected;

        /// <summary>
        /// Occurs when a document is removed.
        /// </summary>
        event DocumentEventHandler DocumentRemoved;

        /// <summary>
        /// Creates a document of type <typeparamref name="T"/> from the specified source.
        /// </summary>
        /// <typeparam name="T">Type of the document.</typeparam>
        /// <param name="source">The document source.</param>
        /// <returns>Initialized document.</returns>
        IDocument CreateDocument(IDocumentSource source);

        /// <summary>
        /// Selects the specified document as the currently active document.
        /// </summary>
        /// <param name="document">The document.</param>
        void SelectDocument(IDocument document);

        /// <summary>
        /// Opens the specified document in a new view and sets it at the active document.
        /// </summary>
        /// <param name="document">The document.</param>
        void OpenDocument(IDocument document);

        /// <summary>
        /// Closes the specified document (and the associated view).
        /// </summary>
        /// <param name="document">The document.</param>
        void CloseDocument(IDocument document);
    }
}
