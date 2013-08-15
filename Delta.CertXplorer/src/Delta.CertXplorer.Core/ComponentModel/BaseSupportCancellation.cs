using System;

namespace Delta.CertXplorer.ComponentModel
{
    /// <summary>
    /// Base implementation for the <see cref="ISupportCancellation"/> interface.
    /// </summary>
    public abstract class BaseSupportCancellation : BaseDirtyNotifier, ISupportCancellation
    {
        #region ISupportCancellation Members

        /// <summary>Cancels the latest modifications</summary>
        public void Cancel()
        {
            CancelChanges();
            SetDirty(false);
        }

        /// <summary>Accepts the latest modifications</summary>
        public void Accept()
        {
            if (AcceptChanges()) SetDirty(false);
        }

        #endregion

        /// <summary>Cancels the current changes.</summary>
        protected abstract void CancelChanges();
        
        /// <summary>Accepts the current changes.</summary>
        /// <remarks>
        /// AcceptChanges usually invokes a 'Save' method. If the save process could not complete, 
        /// this method should return <c>false</c>. This way, the Dirty state won't be set to <c>false</c>
        /// by <see cref="Accept"/>.
        /// </remarks>
        /// <returns><c>true</c> if the changes could be accepted; otherwise <c>false</c>.</returns>
        protected abstract bool AcceptChanges();
    }
}
