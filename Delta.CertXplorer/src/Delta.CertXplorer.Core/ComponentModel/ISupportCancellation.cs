using System;

namespace Delta.CertXplorer.ComponentModel
{
    /// <summary>
    /// A class that implements this interface can send notifications about changes in its 'dirty' state.
    /// These changes can be either cancelled or accepted.
    /// </summary>
    public interface ISupportCancellation : IDirtyNotifier
    {
        /// <summary>Cancels the latest modifications</summary>
        void Cancel();

        /// <summary>Accepts the latest modifications</summary>
        void Accept();
    }
}
