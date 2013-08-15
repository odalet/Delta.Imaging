using System;

namespace Delta.CertXplorer.ComponentModel
{
    /// <summary>
    /// Base implementation for the <see cref="ISuspendable"/> interface.
    /// </summary>
    public abstract class BaseSuspendable : ISuspendable
    {
        /// <summary>
        /// value indicating whether this instance is suspended.
        /// </summary>
        private bool isSuspended = false;

        /// <summary>
        /// Gets a value indicating whether this instance is suspended.
        /// </summary>
        public bool IsSuspended { get { return isSuspended; } }

        #region ISuspendable Members

        /// <summary>Suspends this instance.</summary>
        public void Suspend() 
        { 
            isSuspended = true;
            OnSuspended();
        }

        /// <summary>Resumes this instance.</summary>
        public void Resume() 
        { 
            isSuspended = false;
            OnResumed();
        }

        #endregion

        /// <summary>Called when this instance is suspended.</summary>
        protected abstract void OnSuspended();

        /// <summary>Called when this instance is resumed.</summary>
        protected abstract void OnResumed();
    }
}
