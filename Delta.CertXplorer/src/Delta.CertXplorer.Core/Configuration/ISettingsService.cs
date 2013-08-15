using System;
using System.Collections.Generic;

namespace Delta.CertXplorer.Configuration
{
    /// <summary>
    /// This service manages multiple settings dictionaries.
    /// </summary>    
    public interface ISettingsService
    {
        #region Base methods (all stores)

        /// <summary>
        /// Gets the list of all the keys identifying a settings store.
        /// </summary>
        /// <value>The settings store keys.</value>
        string[] SettingsStoreKeys { get; }

        /// <summary>
        /// Determines whether this service contains a settings store identified by the specified key.
        /// </summary>
        /// <param name="storeKey">The store key.</param>
        /// <returns>
        /// 	<c>true</c> if this service contains a settings store identified by the specified key; otherwise, <c>false</c>.
        /// </returns>
        bool ContainsSettingsStore(string storeKey);

        /// <summary>
        /// Associate the settings read from file <paramref name="fileName"/> 
        /// with the key <paramref name="key"/>.
        /// </summary>
        /// <param name="storeKey">The key that will allow further access to newly created settings store.</param>
        /// <param name="settingsFileName">Settings file to read.</param>
        void AddSettingsStore(string storeKey, string settingsFileName);

        /// <summary>
        /// Associate the settings read from the dictionary <paramref name="settings"/> 
        /// with the key <paramref name="key"/>.
        /// </summary>
        /// <param name="storeKey">The key that will allow further access to newly created settings store.</param>
        /// <param name="settings">The settings to load.</param>
        void AddSettingsStore(string storeKey, IDictionary<string, string> settings);

        /// <summary>
        /// Refreshes a settings store with data on disk.
        /// </summary>
        /// <param name="storeKey">The store key.</param>
        void RefreshSettingsStore(string storeKey);        
        
        /// <summary>
        /// Removes the settings store identified by <paramref name="key"/>
        /// from the service.
        /// </summary>
        /// <param name="storeKey">The key identifying the settings store to remove.</param>
        void RemoveSettingsStore(string storeKey);

        /// <summary>
        /// Obtains the settings identified by the key <paramref name="key"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the requested type is a <see cref="IDictionary{T, T}"/> and <c>T</c> is <see cref="System.String"/>, 
        /// we just return a copy of our internal dictionary.
        /// </para>
        /// <para>
        /// We try to map the keys found in the source dictionary with properties of the target
        /// object.
        /// </para>
        /// <para>
        /// <b>Important: the target object type must provide a parameterless constructor.</b>
        /// </para>
        /// </remarks>
        /// <typeparam name="T">
        /// Represents the type of the target object.
        /// </typeparam>
        /// <param name="storeKey">The settings store key.</param>
        /// <returns>A filled instance of the target object.</returns>
        T GetSettingsStore<T>(string storeKey) where T : class, new();

        /// <summary>
        /// Fills the specified object (<paramref name="target)"/>with the settings obtained
        /// from the store identified by the key <paramref name="key"/>.
        /// </summary>
        /// <remarks>
        /// 	<para>
        /// If the requested type is a <see cref="IDictionary{T, T}"/> and <c>T</c> is <see cref="System.String"/>, 
        /// we just return a copy of our internal dictionary.
        /// </para>
        /// <para>
        /// We try to map the keys found in the source dictionary with properties of the target
        /// object.
        /// </para>
        /// </remarks>
        /// <typeparam name="T">
        /// Represents the type of the target object.
        /// </typeparam>
        /// <param name="storeKey">The settings store key.</param>
        /// <param name="target">The object to fill.</param>
        /// <returns>We return the passed in target.</returns>
        T FillObjectFromSettingsStore<T>(string storeKey, T target) where T : class;

        /// <summary>
        /// Obtains the settings identified by the key <paramref name="key"/> as a dictionary of strings.
        /// </summary>
        /// <param name="storeKey">The settings store key.</param>
        /// <returns>A dictionary of strings.</returns>
        IDictionary<string, string> GetSettingsStore(string storeKey);

        /// <summary>
        /// Updates the settings store identified by the key <paramref name="key"/> using
        /// data from the specified parameter.
        /// </summary>
        /// <remarks>
        /// The store's underlying xml file is saved when calling this method. This means
        /// the method will fail, if the store is not associated with a file.
        /// </remarks>
        /// <typeparam name="T">Type of the <paramref name="data"/> parameter.</typeparam>
        /// <param name="storeKey">The store key.</param>
        /// <param name="data">The data.</param>
        void UpdateSettingsStore<T>(string storeKey, T data) where T : class;

        /// <summary>
        /// Updates the settings store identified by the key <paramref name="key"/> using
        /// data from the specified parameter.
        /// </summary>
        /// <remarks>
        /// The store's underlying xml file is saved when calling this method. This means
        /// the method will fail, if the store is not associated with a file.
        /// </remarks>
        /// <param name="storeKey">The store key.</param>
        /// <param name="data">The data.</param>
        void UpdateSettingsStore(string storeKey, IDictionary<string, string> data);

        /// <summary>
        /// Saves the settings store identified by the key <paramref name="key"/> using
        /// data from the specified parameter.
        /// </summary>
        /// <typeparam name="T">Type of the <paramref name="data"/> parameter.</typeparam>
        /// <param name="storeKey">The store key.</param>
        /// <param name="data">The data.</param>
        /// <param name="outputFileName">Name of the output xml file.</param>
        void SaveSettingsStore<T>(string storeKey, T data, string outputFileName) where T : class;

        /// <summary>
        /// Saves the settings store identified by the key <paramref name="key"/> using
        /// data from the specified parameter.
        /// </summary>
        /// <param name="storeKey">The store key.</param>
        /// <param name="data">The data.</param>
        /// <param name="outputFileName">Name of the output xml file.</param>
        void SaveSettingsStore(string storeKey, IDictionary<string, string> data, string outputFileName);

        #endregion
        
        #region Application Settings Store (default store)

        /// <summary>
        /// Gets the settings store key for the application's settings.
        /// </summary>
        /// <remarks>
        /// If no application settings store was defined, this throws an exception.
        /// </remarks>
        /// <value>The application settings store key.</value>
        string ApplicationSettingsStoreKey { get; }

        /// <summary>
        /// Gets a value indicating whether this service contains an application settings store.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this service contains an application settings store; otherwise, <c>false</c>.
        /// </value>
        bool ContainsApplicationSettingsStore { get; }

        /// <summary>
        /// Gets the application settings store.
        /// </summary>
        /// <typeparam name="T">Represents the type of the target object.</typeparam>
        /// <returns>A filled instance of the target object.</returns>
        /// <remarks>
        /// 	<para>
        /// If the requested type is a <see cref="IDictionary{TKey, TValue}"/>, of <see cref="string"/>
        /// and <see cref="string"/>, we just return a copy of our internal dictionary.
        /// </para>
        /// <para>
        /// We try to map the keys found in the source dictionary with properties of the target
        /// object.
        /// </para>
        /// <para>
        /// <b>Important: the target object type must provide a parameterless constructor.</b>
        /// </para>
        /// </remarks>
        T GetApplicationSettingsStore<T>() where T : class, new();

        /// <summary>
        /// Gets the application settings store as a dictionary of strings.
        /// </summary>
        /// <returns>A dictionary of strings.</returns>
        IDictionary<string, string> GetApplicationSettingsStore();

        /// <summary>
        /// Fills the specified object (<paramref name="target)"/>with the settings obtained
        /// from the application settings store.
        /// </summary>
        /// <typeparam name="T">Represents the type of the target object.</typeparam>
        /// <param name="target">The object to fill.</param>
        /// <returns>We return the passed in target.</returns>
        /// <remarks>
        /// <para>
        /// If the requested type is a <see cref="IDictionary{TKey, TValue}"/> of <see cref="string"/>
        /// and <see cref="string"/>, we just return a copy of our internal dictionary.
        /// </para>
        /// <para>
        /// We try to map the keys found in the source dictionary with properties of the target
        /// object.
        /// </para>
        /// </remarks>
        T FillObjectFromApplicationSettingsStore<T>(T target) where T : class;

        /// <summary>
        /// Sets the application settings store from the specified file.
        /// </summary>
        /// <remarks>
        /// If this method is called multiple times, the previous application settings store is lost.
        /// </remarks>
        /// <param name="applicationSettingsFileName">Name of the application settings file.</param>
        void SetApplicationSettingsStore(string applicationSettingsFileName);

        /// <summary>
        /// Sets the application settings store from the specified dictionary.
        /// </summary>
        /// <param name="settings">The settings dictionary.</param>
        /// <remarks>
        /// If this method is called multiple times, the previous application settings store is lost.
        /// </remarks>
        void SetApplicationSettingsStore(IDictionary<string, string> settings);

        /// <summary>
        /// Refreshes the application settings store with data on disk.
        /// </summary>
        void RefreshApplicationSettingsStore();

        /// <summary>
        /// Updates the Application Settings store identified by the key <paramref name="key"/> using
        /// data from the specified parameter.
        /// </summary>
        /// <remarks>
        /// The store's underlying xml file is saved when calling this method. This means
        /// the method will fail, if the store is not associated with a file.
        /// </remarks>
        /// <typeparam name="T">Type of the <paramref name="data"/> parameter.</typeparam>
        /// <param name="data">The data.</param>
        void UpdateApplicationSettingsStore<T>(T data) where T : class;

        /// <summary>
        /// Updates the settings store identified by the key <paramref name="key"/> using
        /// data from the specified parameter.
        /// </summary>
        /// <remarks>
        /// The store's underlying xml file is saved when calling this method. This means
        /// the method will fail, if the store is not associated with a file.
        /// </remarks>
        /// <param name="data">The data.</param>
        void UpdateApplicationSettingsStore(IDictionary<string, string> data);

        /// <summary>
        /// Saves the Application settings store identified by the key <paramref name="key"/> using
        /// data from the specified parameter.
        /// </summary>
        /// <typeparam name="T">Type of the <paramref name="data"/> parameter.</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="outputFileName">Name of the output xml file.</param>
        void SaveApplicationSettingsStore<T>(T data, string outputFileName) where T : class;

        /// <summary>
        /// Saves the settings store identified by the key <paramref name="key"/> using
        /// data from the specified parameter.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="outputFileName">Name of the output xml file.</param>
        void SaveApplicationSettingsStore(IDictionary<string, string> data, string outputFileName);

        #endregion

        #region Other methods (store-independant)

        /// <summary>
        /// Fills the specified object (<paramref name="target)"/> with the settings obtained
        /// by reading this object's default values (specified with 
        /// <see cref="System.ComponentModel.DefaultValueAttribute"/>).
        /// </summary>
        /// <typeparam name="T">
        /// Represents the type of the target object.
        /// </typeparam>
        /// <param name="target">The object to fill.</param>
        /// <returns>We return the passed in target.</returns>
        T FillObjectWithDefaultValues<T>(T target) where T : class;

        #endregion
    }
}
