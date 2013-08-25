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
        private Dictionary<string, IDocumentView> views = new Dictionary<string, IDocumentView>();

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
        public void SelectDocument(IDocument document)
        {
            if (document == null) throw new ArgumentNullException("document");

            ownerUI.ShowView(views[document.Key]);
            OnDocumentSelected(document);
        }

        /// <summary>
        /// Opens the specified document in a new view and sets it at the active document.
        /// </summary>
        /// <param name="document">The document.</param>
        public void OpenDocument(IDocument document)
        {
            if (document == null) throw new ArgumentNullException("document");
            if (views.ContainsKey(document.Key))
                SelectDocument(document);
            else
            {
                var view = document.CreateView();
                view.ViewClosed += (s, e) => CloseDocument(document, false);

                views.Add(document.Key, view);
                ownerUI.ShowView(views[document.Key]);
                OnDocumentAdded(document);
            }
        }

        /// <summary>
        /// Closes the specified document (and the associated view).
        /// </summary>
        /// <param name="document">The document.</param>
        public void CloseDocument(IDocument document)
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
        private void CloseDocument(IDocument document, bool shouldCloseView)
        {
            if (document == null) throw new ArgumentNullException("document");
            if (!views.ContainsKey(document.Key)) return;

            if (shouldCloseView)
            {
                var view = views[document.Key];
                CloseView(view);
            }

            views.Remove(document.Key);
            OnDocumentRemoved(document);
        }

        private void CloseView(IDocumentView view)
        {
            if (view == null) throw new ArgumentNullException("view");
            This.Logger.Verbose("Closing View.");
            view.Close();
            if (view is IDisposable)
            {
                var document = view.Document;
                var viewInfo = document == null ? view.GetType() : document.GetType();
                This.Logger.Verbose(string.Format("Disposing view {0}", viewInfo)); 
                ((IDisposable)view).Dispose();
            }
        }

        /// <summary>
        /// Called when a document is added.
        /// </summary>
        /// <param name="document">The document.</param>
        private void OnDocumentAdded(IDocument document)
        {
            if (DocumentAdded != null)
                DocumentAdded(this, new DocumentEventArgs(document));
        }

        /// <summary>
        /// Called when a document is selected.
        /// </summary>
        /// <param name="document">The document.</param>
        private void OnDocumentSelected(IDocument document)
        {
            if (DocumentSelected != null)
                DocumentSelected(this, new DocumentEventArgs(document));
        }

        /// <summary>
        /// Called when a document is removed.
        /// </summary>
        /// <param name="document">The document.</param>
        private void OnDocumentRemoved(IDocument document)
        {
            if (DocumentRemoved != null)
                DocumentRemoved(this, new DocumentEventArgs(document));
        }
    }
}
