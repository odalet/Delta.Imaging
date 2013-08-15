using System;

namespace Delta.CertXplorer.ComponentModel
{
    /// <summary>
    /// Represents the method that will handle service provider or service container events.
    /// </summary>
    /// <param name="sender">Event source.</param>
    /// <param name="e">Event data.</param>
    public delegate void ServiceNotificationEventHandler(object sender, ServiceNotificationEventArgs e);

    /// <summary>
    /// Used when notifying a change in a service provider or a service container.
    /// </summary>
    public class ServiceNotificationEventArgs : EventArgs
    {
        /// <summary>The service type.</summary>
        private Type serviceType = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceNotificationEventArgs"/> class.
        /// </summary>
        /// <param name="type">The service type.</param>
        public ServiceNotificationEventArgs(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            serviceType = type;
        }

        /// <summary>
        /// Gets the type of the service.
        /// </summary>
        /// <value>The type of the service.</value>
        public Type ServiceType { get { return serviceType; } }
    }
}
