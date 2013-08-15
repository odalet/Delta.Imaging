using System;

namespace Delta.CertXplorer.ComponentModel
{
    /// <summary>
    /// Defines a specific <see cref="IServiceProvider"/> that
    /// sends an event each time a service is requested.
    /// </summary>
    public interface IObservableServiceProvider : IServiceProvider
    {
        /// <summary>
        /// Occurs when a service is requested.
        /// </summary>
        event ServiceNotificationEventHandler ServiceRequested;
    }
}
