using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;

namespace Delta.CertXplorer.ComponentModel
{
    /// <summary>
    /// Defines a specific <see cref="System.ComponentModel.Design.ServiceContainer"/> that
    /// sends an event each time a service is requested, added or removed. This class also
    /// provides the list of currently loaded service types by implementing 
    /// <see cref="IListableServiceProvider"/>.
    /// </summary>
    public class ObservableServiceContainer : IObservableServiceContainer, IListableServiceProvider, IDisposable
    {
        /// <summary>The inner service container.</summary>
        private ServiceContainer container = null;

        /// <summary>The parent service provider (if it exists).</summary>
        private IServiceProvider parentProvider = null;

        /// <summary>This stores references to the currently loaded servie types.</summary>
        private List<Type> serviceTypes = new List<Type>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableServiceContainer"/> class.
        /// </summary>
        public ObservableServiceContainer()
        {
            container = new ServiceContainer();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableServiceContainer"/> class.
        /// </summary>
        /// <param name="serviceProvider">The parent service provider.</param>
        public ObservableServiceContainer(IServiceProvider serviceProvider)
        {
            container = new ServiceContainer(serviceProvider);
            parentProvider = serviceProvider;
        }

        #region IObservableServiceContainer Members

        /// <summary>
        /// Occurs when a service is added.
        /// </summary>
        public event ServiceNotificationEventHandler ServiceAdded;

        /// <summary>
        /// Occurs when a service is removed.
        /// </summary>
        public event ServiceNotificationEventHandler ServiceRemoved;

        #endregion

        #region IObservableServiceProvider Members

        /// <summary>
        /// Occurs when a service is requested.
        /// </summary>
        public event ServiceNotificationEventHandler ServiceRequested;

        #endregion

        #region IServiceProvider Members

        public object GetService(Type serviceType)
        {
            if (serviceType == null) throw new ArgumentNullException("serviceType");
            OnServiceRequested(new ServiceNotificationEventArgs(serviceType));
            return container.GetService(serviceType);
        }

        #endregion

        #region IServiceContainer Members

        /// <summary>
        /// Adds the specified service to the service container.
        /// </summary>
        /// <param name="serviceType">The type of service to add.</param>
        /// <param name="callback">A callback object that is used to create the service. This allows a service to be declared as available, but delays the creation of the object until the service is requested.</param>
        public void AddService(Type serviceType, ServiceCreatorCallback callback)
        {
            AddService(serviceType, callback, false);            
        }

        /// <summary>
        /// Adds the specified service to the service container, and optionally promotes the service to parent service containers.
        /// </summary>
        /// <param name="serviceType">The type of service to add.</param>
        /// <param name="callback">A callback object that is used to create the service. This allows a service to be declared as available, but delays the creation of the object until the service is requested.</param>
        /// <param name="promote">true to promote this request to any parent service containers; otherwise, false.</param>
        public void AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
        {
            if (serviceType == null) throw new ArgumentNullException("serviceType");
            OnServiceAdded(new ServiceNotificationEventArgs(serviceType));
            container.AddService(serviceType, callback, promote);
            serviceTypes.Add(serviceType); // we store a reference to the service type so that we can list them later.
        }

        /// <summary>
        /// Adds the specified service to the service container.
        /// </summary>
        /// <param name="serviceType">The type of service to add.</param>
        /// <param name="serviceInstance">An instance of the service type to add. This object must implement or inherit from the type indicated by the <paramref name="serviceType"/> parameter.</param>
        public void AddService(Type serviceType, object serviceInstance)
        {
            AddService(serviceType, serviceInstance, false);
        }

        /// <summary>
        /// Adds the specified service to the service container, and optionally promotes the service to any parent service containers.
        /// </summary>
        /// <param name="serviceType">The type of service to add.</param>
        /// <param name="serviceInstance">An instance of the service type to add. This object must implement or inherit from the type indicated by the <paramref name="serviceType"/> parameter.</param>
        /// <param name="promote">true to promote this request to any parent service containers; otherwise, false.</param>
        public void AddService(Type serviceType, object serviceInstance, bool promote)
        {
            if (serviceType == null) throw new ArgumentNullException("serviceType");
            OnServiceAdded(new ServiceNotificationEventArgs(serviceType));
            container.AddService(serviceType, serviceInstance, promote);
            serviceTypes.Add(serviceType); // we store a reference to the service type so that we can list them later.
        }

        /// <summary>
        /// Removes the specified service type from the service container.
        /// </summary>
        /// <param name="serviceType">The type of service to remove.</param>
        public void RemoveService(Type serviceType)
        {
            RemoveService(serviceType, false);
        }

        /// <summary>
        /// Removes the specified service type from the service container, and optionally promotes the service to parent service containers.
        /// </summary>
        /// <param name="serviceType">The type of service to remove.</param>
        /// <param name="promote">true to promote this request to any parent service containers; otherwise, false.</param>
        public void RemoveService(Type serviceType, bool promote)
        {
            if (serviceType == null) throw new ArgumentNullException("serviceType");
            OnServiceRemoved(new ServiceNotificationEventArgs(serviceType));
            container.RemoveService(serviceType, promote);
            serviceTypes.Remove(serviceType); // we remove the stored reference to the service type so that we can list them later.
        }

        #endregion

        #region IListableServiceContainer Members

        /// <summary>
        /// Gets the list of the service types contained in this service provider.
        /// </summary>
        /// <returns>A list of service types.</returns>
        public IList<Type> GetServicesList() { return GetServicesList(false); }

        /// <summary>
        /// Gets the list of the service types contained in this service provider.
        /// </summary>
        /// <remarks>
        /// Because a service type can appear both in a service container and in its parent service provider,
        /// the returned list doesn't contain unique types.
        /// </remarks>
        /// <param name="promote">If set to <c>true</c>, then recurse the hierarchy of parent service providers
        /// and tries to get their service types list.</param>
        /// <returns>A list of service types.</returns>
        public IList<Type> GetServicesList(bool promote)
        {
            List<Type> list = null;

            if (promote && (parentProvider != null) && (parentProvider is IListableServiceProvider))
            {
                IList<Type> parentList = ((IListableServiceProvider)parentProvider).GetServicesList(true);
                list = new List<Type>(serviceTypes);
                list.AddRange(parentList);
            }
            else list = serviceTypes;

            return new ReadOnlyCollection<Type>(list);
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            serviceTypes.Clear();

            if (container != null)
            {
                container.Dispose();
                container = null;
            }
        }

        #endregion

        /// <summary>
        /// Raises the <see cref="E:ServiceRequested"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Delta.CertXplorer.ComponentModel.ServiceNotificationEventArgs"/> instance containing the event data.</param>
        protected virtual void OnServiceRequested(ServiceNotificationEventArgs e)
        {
            if (ServiceRequested != null) ServiceRequested(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:ServiceAdded"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Delta.CertXplorer.ComponentModel.ServiceNotificationEventArgs"/> instance containing the event data.</param>
        protected virtual void OnServiceAdded(ServiceNotificationEventArgs e)
        {
            if (ServiceAdded != null) ServiceAdded(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:ServiceRemoved"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Delta.CertXplorer.ComponentModel.ServiceNotificationEventArgs"/> instance containing the event data.</param>
        protected virtual void OnServiceRemoved(ServiceNotificationEventArgs e)
        {
            if (ServiceRemoved != null) ServiceRemoved(this, e);
        }
    }
}
