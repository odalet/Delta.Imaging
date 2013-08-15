//using System;
//using System.Collections.Generic;
//using System.ComponentModel.Design;

//namespace Delta.CertXplorer.ComponentModel
//{
//    /// <summary>
//    /// This callback allows for delayed cration of a service.
//    /// </summary>
//    public delegate T ServiceCreatorCallback<T>() where T : class;

//    /// <summary>
//    /// When implemented in classes, provides a means for storing multiple services, but exposed by the same interface.
//    /// </summary>
//    /// <remarks>
//    /// A key is added to the common Add/Remove/GetService semantic to allow to distinguish between implementations
//    /// of the same interface
//    /// </remarks>
//    /// <typeparam name="T">Type of the services to store.</typeparam>
//    public interface IServiceSelector<T> : IDisposable where T : class
//    {
//        /// <summary>Ajoute le service spécifié au sélecteur de services.</summary>
//        /// <param name="key">La clé identifiant le service à ajouter.</param>
//        /// <param name="serviceInstance">Instance du type de service à ajouter. 
//        /// Cet objet doit implémenter le type indiqué par <typeparamref name="T"/> ou en hériter.</param>
//        void AddService(string key, T serviceInstance);

//        /// <summary>Ajoute le service spécifié au sélecteur de services.</summary>
//        /// <param name="key">La clé identifiant le service à ajouter.</param>
//        /// <param name="serviceInstance">Instance du type de service à ajouter. 
//        /// Cet objet doit implémenter le type indiqué par <typeparamref name="T"/> ou en hériter.</param>
//        /// <param name="isDefault">if set to <c>true</c> the added service becomes the default service.</param>
//        void AddService(string key, T serviceInstance, bool isDefault);

//        /// <summary>Ajoute le service spécifié au sélecteur de services.</summary>
//        /// <param name="key">La clé identifiant le service à ajouter.</param>
//        /// <param name="callback">Objet de rappel utilisé pour créer le service. Cela permet à 
//        /// un service d'être déclaré comme disponible, mais retarde la création de l'objet 
//        /// jusqu'à ce que le service soit demandé.</param>
//        void AddService(string key, ServiceCreatorCallback<T> callback);

//        /// <summary>Ajoute le service spécifié au sélecteur de services.</summary>
//        /// <param name="key">La clé identifiant le service à ajouter.</param>
//        /// <param name="callback">Objet de rappel utilisé pour créer le service. Cela permet à 
//        /// un service d'être déclaré comme disponible, mais retarde la création de l'objet 
//        /// jusqu'à ce que le service soit demandé.</param>
//        /// <param name="isDefault">if set to <c>true</c> the added service becomes the default service.</param>
//        void AddService(string key, ServiceCreatorCallback<T> callback, bool isDefault);

//        /// <summary>
//        /// Removes a service.
//        /// </summary>
//        /// <param name="key">The key identifying the service to remove.</param>
//        void RemoveService(string key);

//        /// <summary>
//        /// Sets the default service.
//        /// </summary>
//        /// <param name="key">The key identifying the service which will become the default service.</param>
//        void SetDefaultService(string key);

//        /// <summary>
//        /// Determines whether the current instance contains the service identified by <paramref name="key"/>.
//        /// </summary>
//        /// <param name="key">The key.</param>
//        /// <returns>
//        /// 	<c>true</c> if the current instance contains the service identified by <paramref name="key"/>; otherwise, <c>false</c>.
//        /// </returns>
//        bool ContainsService(string key);

//        /// <summary>
//        /// Gets a value indicating whether this instance is empty.
//        /// </summary>
//        /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
//        bool IsEmpty { get; }

//        /// <summary>
//        /// Gets the default service key.
//        /// </summary>
//        /// <value>The default service key.</value>
//        string DefaultServiceKey { get; }

//        /// <summary>
//        /// Gets the service object for the specified key.
//        /// </summary>
//        /// <param name="key">The key identifying the service to retrieve.</param>
//        /// <returns>
//        /// A service object of type <typeparamref name="T"/> -or- null if the current instance is empty.
//        /// </returns>
//        T GetService(string key);

//        /// <summary>
//        /// Gets the default service object.
//        /// </summary>
//        /// <returns>
//        /// A service object of type <typeparamref name="T"/> -or- null if the current instance is empty.
//        /// </returns>
//        T GetService();
//    }
//}
