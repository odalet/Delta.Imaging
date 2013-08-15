//using System;
//using System.Linq;
//using System.Collections.Generic;
//using System.ComponentModel.Design;

//using Delta.CertXplorer.Extensions;

//namespace Delta.CertXplorer.ComponentModel
//{
//    /// <summary>
//    /// Implémentation standard de <see cref="IServiceSelector&lt;T&gt;"/>
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    public class ServiceSelector<T> : IServiceSelector<T> where T :class
//    {
//        /// <summary><see cref="ServiceContainer"/> containing the current instance.</summary>
//        private IServiceContainer parentContainer = null;

//        /// <summary>Backer for <see cref="DefaultServiceKey"/>.</summary>
//        private string defaultServiceKey = string.Empty;
        
//        /// <summary>Stores the services (instances or callbacks).</summary>
//        private Dictionary<string, object> services = new Dictionary<string, object>();

//        #region IServiceSelector<T> Members

//        /// <summary>
//        /// Gets a value indicating whether this instance is empty.
//        /// </summary>
//        /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
//        public bool IsEmpty { get { return services.Count == 0; } }

//        /// <summary>
//        /// Gets the default service key.
//        /// </summary>
//        /// <value>The default service key.</value>
//        public string DefaultServiceKey { get { return defaultServiceKey; } }

//        /// <summary>
//        /// Ajoute le service spécifié au sélecteur de services.
//        /// </summary>
//        /// <param name="key">La clé identifiant le service à ajouter.</param>
//        /// <param name="serviceInstance">Instance du type de service à ajouter.
//        /// Cet objet doit implémenter le type indiqué par <typeparamref name="T"/> ou en hériter.</param>
//        public void AddService(string key, T serviceInstance) { AddService(key, serviceInstance, false); }

//        /// <summary>
//        /// Ajoute le service spécifié au sélecteur de services.
//        /// </summary>
//        /// <param name="key">La clé identifiant le service à ajouter.</param>
//        /// <param name="serviceInstance">Instance du type de service à ajouter.
//        /// Cet objet doit implémenter le type indiqué par <typeparamref name="T"/> ou en hériter.</param>
//        /// <param name="isDefault">if set to <c>true</c> the added service becomes the default service.</param>
//        public void AddService(string key, T serviceInstance, bool isDefault)
//        {
//            if (serviceInstance == null) throw new ArgumentNullException("serviceInstance");
//            AddServiceObject(key, serviceInstance, isDefault);
//        }

//        /// <summary>
//        /// Ajoute le service spécifié au sélecteur de services.
//        /// </summary>
//        /// <param name="key">La clé identifiant le service à ajouter.</param>
//        /// <param name="callback">Objet de rappel utilisé pour créer le service. Cela permet à
//        /// un service d'être déclaré comme disponible, mais retarde la création de l'objet
//        /// jusqu'à ce que le service soit demandé.</param>
//        public void AddService(string key, ServiceCreatorCallback<T> callback)
//        {
//            AddService(key, callback, false);
//        }

//        /// <summary>
//        /// Ajoute le service spécifié au sélecteur de services.
//        /// </summary>
//        /// <param name="key">La clé identifiant le service à ajouter.</param>
//        /// <param name="callback">Objet de rappel utilisé pour créer le service. Cela permet à
//        /// un service d'être déclaré comme disponible, mais retarde la création de l'objet
//        /// jusqu'à ce que le service soit demandé.</param>
//        /// <param name="isDefault">if set to <c>true</c> the added service becomes the default service.</param>
//        public void AddService(string key, ServiceCreatorCallback<T> callback, bool isDefault)
//        {
//            if (callback == null) throw new ArgumentNullException("callback");
//            AddServiceObject(key, callback, isDefault);
//        }

//        /// <summary>
//        /// Removes a service.
//        /// </summary>
//        /// <param name="key">The key identifying the service to remove.</param>
//        public void RemoveService(string key)
//        {
//            if (services.ContainsKey(key))
//            {
//                object service = GetAndRemoveService(key);
//                if ((defaultServiceKey == key) && (services.Count > 0))
//                    SetDefaultServiceKey(services.Keys.First());
//                else SetDefaultServiceKey(string.Empty);
//            }
//            else throw new ArgumentOutOfRangeException("key");
//        }

//        /// <summary>
//        /// Sets the default service.
//        /// </summary>
//        /// <param name="key">The key identifying the service which will become the default service.</param>
//        public void SetDefaultService(string key)
//        {
//            if (string.IsNullOrEmpty(key) || !services.ContainsKey(key))
//                throw new ArgumentOutOfRangeException("key");
//            SetDefaultServiceKey(key);
//        }

//        /// <summary>
//        /// Determines whether the current instance contains the service identified by <paramref name="key"/>.
//        /// </summary>
//        /// <param name="key">The key.</param>
//        /// <returns>
//        /// 	<c>true</c> if the current instance contains the service identified by <paramref name="key"/>; otherwise, <c>false</c>.
//        /// </returns>
//        public bool ContainsService(string key) { return services.ContainsKey(key); }

//        /// <summary>
//        /// Gets the default service object.
//        /// </summary>
//        /// <returns>
//        /// A service object of type <typeparamref name="T"/> -or- null if the current instance is empty.
//        /// </returns>
//        public T GetService() { return GetService(defaultServiceKey); }

//        /// <summary>
//        /// Gets the service object for the specified key.
//        /// </summary>
//        /// <param name="key">The key identifying the service to retrieve.</param>
//        /// <returns>
//        /// A service object of type <typeparamref name="T"/> -or- null if the current instance is empty.
//        /// </returns>
//        public T GetService(string key)
//        {
//            if (services.ContainsKey(key))
//            {
//                object service = services[key];
//                if (service is ServiceCreatorCallback<T>)
//                {
//                    T instance = ((ServiceCreatorCallback<T>)service)();
//                    services[key] = instance;
//                    return instance;
//                }
//                return (T)service;
//            }
//            else throw new ArgumentOutOfRangeException("key");
//        }
        
//        #endregion

//        #region IDisposable Members

//        /// <summary>
//        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
//        /// </summary>
//        public void Dispose()
//        {
//            defaultServiceKey = string.Empty;

//            List<string> keys = new List<string>(services.Keys);
//            foreach (string key in keys)
//            {
//                object service = GetAndRemoveService(key);
//                if (service is IDisposable)
//                    ((IDisposable)service).Dispose();
//            }
//        }

//        #endregion

//        /// <summary>
//        /// Adds the current instance to a service container.
//        /// </summary>
//        /// <param name="serviceContainer">The service container.</param>
//        /// <param name="promote"><b>true</b> pour promouvoir cette demande vers les 
//        /// conteneurs de services parents éventuels ; sinon, <b>false</b>.</param>
//        internal void AddSelectorToContainer(IServiceContainer serviceContainer, bool promote)
//        {
//            if (parentContainer != null) throw new ApplicationException(SR.InstanceAlreadyAddedToServiceContainer);
//            if (serviceContainer == null) throw new ArgumentNullException("serviceContainer");

//            parentContainer = serviceContainer;

//            parentContainer.AddService<IServiceSelector<T>>(this, promote);
//            parentContainer.AddService<T>((container, serviceType) => services[defaultServiceKey], promote);
//        }

//        /// <summary>
//        /// Removes the selector from container.
//        /// </summary>
//        /// <param name="promote"><b>true</b> pour promouvoir cette demande vers les 
//        /// conteneurs de services parents éventuels ; sinon, <b>false</b>.</param>
//        internal void RemoveSelectorFromContainer(bool promote)
//        {
//            if (parentContainer == null) throw new ApplicationException(SR.InstanceNeverAddedToServiceContainer);
//            parentContainer.RemoveService<T>(promote);
//            parentContainer.RemoveService <IServiceSelector<T>>(promote);
//        }

//        /// <summary>
//        /// Gets a service by its key, then removes it from the inner dictionnary.
//        /// </summary>
//        /// <param name="key">The key identifying the service to retrieve.</param>
//        /// <returns></returns>
//        private object GetAndRemoveService(string key)
//        {
//            object service = services[key];
//            services.Remove(key);
//            return service;
//        }

//        /// <summary>
//        /// Adds the service object.
//        /// </summary>
//        /// <param name="key">The key.</param>
//        /// <param name="service">The service.</param>
//        /// <param name="isDefault">if set to <c>true</c> [is default].</param>
//        private void AddServiceObject(string key, object service, bool isDefault)
//        {
//            if (services.ContainsKey(key)) throw new ArgumentOutOfRangeException("key");
//            if (string.IsNullOrEmpty(defaultServiceKey)) isDefault = true;
//            if (isDefault) SetDefaultServiceKey(key);

//            services.Add(key, service);
//        }

//        /// <summary>
//        /// Sets the default service key.
//        /// </summary>
//        /// <param name="key">The key.</param>
//        private void SetDefaultServiceKey(string key)
//        {
//            defaultServiceKey = key;

//            if (parentContainer != null)
//            {
//                parentContainer.RemoveService<T>();
//                parentContainer.AddService<T>((container, serviceType) => services[defaultServiceKey]);
//            }
//        }
//    }
//}
