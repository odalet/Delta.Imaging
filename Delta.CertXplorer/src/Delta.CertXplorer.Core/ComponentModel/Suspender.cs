using System;

namespace Delta.CertXplorer.ComponentModel
{
    /// <summary>
    /// Permet d'utiliser un objet implémentant <see cref="ISuspendable"/> dans un bloc <b>using</b>
    /// </summary>
    public class Suspender : IDisposable
    {
        private ISuspendable suspendable = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Suspender"/> class.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public Suspender(ISuspendable instance)
        {
            suspendable = instance;
            if (suspendable != null) suspendable.Suspend();
        }

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (suspendable != null) suspendable.Resume();
        }

        #endregion
    }
}
