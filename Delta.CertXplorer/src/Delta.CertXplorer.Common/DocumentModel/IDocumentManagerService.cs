
namespace Delta.CertXplorer.DocumentModel
{
    public interface IDocumentManagerService
    {
        /// <summary>
        /// Selects the specified document as the currently active document.
        /// </summary>
        /// <param name="document">The document.</param>
        void SelectDocument(Document document);

        /// <summary>
        /// Opens the specified document in a new view and sets it at the active document.
        /// </summary>
        /// <param name="document">The document.</param>
        void OpenDocument(Document document);

        /// <summary>
        /// Closes the specified document (and the associated view).
        /// </summary>
        /// <param name="document">The document.</param>
        void CloseDocument(Document document);

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
    }
}
