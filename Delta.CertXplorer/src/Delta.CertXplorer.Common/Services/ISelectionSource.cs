using System;

namespace Delta.CertXplorer.Services
{
    /// <summary>
    /// A selection source notifies of selection events.
    /// </summary>
    public interface ISelectionSource
    {
        /// <summary>
        /// Occurs when the currently selected object has changed.
        /// </summary>
        event EventHandler SelectionChanged;

        /// <summary>
        /// Gets the currently selected object.
        /// </summary>
        /// <value>The selected object.</value>
        object SelectedObject { get; }
    }
}
