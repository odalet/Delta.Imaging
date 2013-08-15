using System;

using Delta.CertXplorer.Asn1Decoder;
using Delta.CertXplorer.DocumentModel;

namespace Delta.CertXplorer
{
    partial class Chrome : IDocumentBasedUI
    {
        #region IDocumentBasedUI Members

        /// <summary>
        /// Occurs when the currently active document has changed.
        /// </summary>
        public event EventHandler ActiveDocumentChanged;

        /// <summary>
        /// Gets the active document view.
        /// </summary>
        /// <value>The active document view.</value>
        public IDocumentView ActiveDocumentView
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Shows the specified view as the current window.
        /// </summary>
        /// <param name="view">The view.</param>
        public void ShowView(IDocumentView view)
        {
            if (view is DocumentWindow)
                ((DocumentWindow)view).Show(base.Workspace);
            else throw new InvalidCastException();
        }

        #endregion
    }
}
