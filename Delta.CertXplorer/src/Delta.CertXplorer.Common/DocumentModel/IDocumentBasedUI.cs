using System;

namespace Delta.CertXplorer.DocumentModel
{
    /// <summary>
    /// Implemented by forms that can handle multiple document views.
    /// </summary>
    public interface IDocumentBasedUI
    {
        /// <summary>
        /// Occurs when the currently active document has changed.
        /// </summary>
        event EventHandler ActiveDocumentChanged;

        /// <summary>
        /// Gets the active document view.
        /// </summary>
        /// <value>The active document view.</value>
        IDocumentView ActiveDocumentView { get; }

        /// <summary>
        /// Shows the specified view as the current window.
        /// </summary>
        /// <param name="view">The view.</param>
        void ShowView(IDocumentView view);
    }
}
