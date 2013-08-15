using System;
using System.ComponentModel;

namespace Delta.CertXplorer.ComponentModel
{
    /// <summary>
    /// Implementors support the 'dirty' notion. This means, they can notify their inner 
    /// state has changed and should be saved.
    /// </summary>
    public interface IDirtyNotifier
    {
        /// <summary>
        /// Gets the current 'dirty' state.
        /// </summary>
        /// <value><c>true</c> if dirty (non consistent with the saved state); otherwise, <c>false</c>.</value>
        bool Dirty { get; }

        /// <summary>
        /// Occurs when the dirty state has changed.
        /// </summary>
        event DirtyChangedEventHandler DirtyChanged;
    }
}
