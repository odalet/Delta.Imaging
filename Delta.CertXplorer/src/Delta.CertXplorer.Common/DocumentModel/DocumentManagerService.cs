using System;
using System.Collections.Generic;

namespace Delta.CertXplorer.DocumentModel
{
    /// <summary>
    /// Implementation of a simple Document manager service.
    /// </summary>
    internal class DocumentManagerService : IDocumentManagerService
    {
        private IDocumentBasedUI ownerUI = null;
        private Dictionary<Document, IDocumentView> views = new Dictionary<Document, IDocumentView>();
                
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentManagerService"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        public DocumentManagerService(IDocumentBasedUI owner)
        {
            if (owner == null) throw new ArgumentNullException("owner");
            ownerUI = owner;

            //TODO: is this useful? Or correctly placed?
            ownerUI.ActiveDocumentChanged += (s, e) =>
            {
                var view = ownerUI.ActiveDocumentView;
                if (view != null && view.Document != null)
                    SelectDocument(view.Document);
            };
        }

        #region IDocumentManagerService Members

        /// <summary>
        /// Occurs when a new document is added.
        /// </summary>
        public event DocumentEventHandler DocumentAdded;

        /// <summary>
        /// Occurs when a document is selected.
        /// </summary>
        public event DocumentEventHandler DocumentSelected;

        /// <summary>
        /// Occurs when a document is removed.
        /// </summary>
        public event DocumentEventHandler DocumentRemoved;

        /// <summary>
        /// Selects the specified document as the currently active document.
        /// </summary>
        /// <param name="document">The document.</param>
        public void SelectDocument(Document document)
        {
            ownerUI.ShowView(views[document]);
            OnDocumentSelected(document);
        }

        /// <summary>
        /// Opens the specified document in a new view and sets it at the active document.
        /// </summary>
        /// <param name="document">The document.</param>
        public void OpenDocument(Document document)
        {
            if (document == null) throw new ArgumentNullException("document");
            if (views.ContainsKey(document))
                SelectDocument(document);
            else
            {
                var view = document.CreateView();
                view.ViewClosed += (s, e) => CloseDocument(document, false);

                views.Add(document, view);
                ownerUI.ShowView(views[document]);
                OnDocumentAdded(document);
            }
        }

        /// <summary>
        /// Closes the specified document (and the associated view).
        /// </summary>
        /// <param name="document">The document.</param>
        public void CloseDocument(Document document)
        {
            CloseDocument(document, true);
        }        

        #endregion
        
        /// <summary>
        /// Closes the specified document (and the associated view 
        /// only if <paramref name="shouldCloseView"/> is <c>true</c>).
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="shouldCloseView">if set to <c>true</c> closes the view associated with the document.</param>
        private void CloseDocument(Document document, bool shouldCloseView)
        {
            if (document == null) throw new ArgumentNullException("document");
            if (views.ContainsKey(document))
            {
                if (shouldCloseView)
                {
                    var view = views[document];
                    view.Close();
                }

                views.Remove(document);
                OnDocumentRemoved(document);
            }
        }

        /// <summary>
        /// Called when a document is added.
        /// </summary>
        /// <param name="document">The document.</param>
        private void OnDocumentAdded(Document document)
        {
            if (DocumentAdded != null)
                DocumentAdded(this, new DocumentEventArgs(document));
        }

        /// <summary>
        /// Called when a document is selected.
        /// </summary>
        /// <param name="document">The document.</param>
        private void OnDocumentSelected(Document document)
        {
            if (DocumentSelected != null)
                DocumentSelected(this, new DocumentEventArgs(document));
        }

        /// <summary>
        /// Called when a document is removed.
        /// </summary>
        /// <param name="document">The document.</param>
        private void OnDocumentRemoved(Document document)
        {
            if (DocumentRemoved != null)
                DocumentRemoved(this, new DocumentEventArgs(document));
        }
    }
}
