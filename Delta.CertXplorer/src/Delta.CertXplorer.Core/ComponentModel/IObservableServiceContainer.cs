using System;
using System.ComponentModel.Design;

namespace Delta.CertXplorer.ComponentModel
{
    /// <summary>
    /// Defines a specific <see cref="System.ComponentModel.Design.IServiceContainer"/> that
    /// sends an event each time a service is requested, added or removed.
    /// </summary>
    public interface IObservableServiceContainer : IObservableServiceProvider, IServiceContainer
    {
        /// <summary>
        /// Occurs when a service is added.
        /// </summary>
        event ServiceNotificationEventHandler ServiceAdded;

        /// <summary>
        /// Occurs when a service is removed.
        /// </summary>
        event ServiceNotificationEventHandler ServiceRemoved;
    }
}
