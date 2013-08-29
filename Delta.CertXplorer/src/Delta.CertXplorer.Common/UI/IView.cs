using System;

namespace Delta.CertXplorer.UI
{
    public interface IView
    {
        /// <summary>
        /// Occurs when this view is closed.
        /// </summary>
        event EventHandler ViewClosed;

        /// <summary>
        /// Closes this view.
        /// </summary>
        void Close();
    }
}
