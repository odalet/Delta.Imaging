using System;

namespace Delta.CertXplorer.ComponentModel
{
    /// <summary>
    /// Defines the contract of objects that can be suspended then resumed (sort of pause system).
    /// </summary>
    public interface ISuspendable
    {
        /// <summary>Temporarily stops the current action.</summary>
        void Suspend();

        /// <summary>Resumes a previously suspended action.</summary>
        void Resume();
    }
}
