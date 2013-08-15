using System;

namespace Delta.CertXplorer.ComponentModel
{
    /// <summary>
    /// Default implementation for the <see cref="ISuspendable"/> interface.
    /// </summary>
    public class Suspendable : BaseSuspendable
    {
        /// <summary>
        /// Called when this instance is suspended. In this implementation, does nothing.
        /// </summary>
        protected override void OnSuspended() { }

        /// <summary>
        /// Called when this instance is resumed. In this implementation, does nothing.
        /// </summary>
        protected override void OnResumed() { }
    }
}
