using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Collections.Generic;

using Delta.CertXplorer.Collections;

namespace Delta.CertXplorer.Configuration
{
    /// <summary>
    /// This service manages multiple settings dictionaries.
    /// </summary>
    public class SettingsService : ISettingsService
    {
        protected class SettingsStore
        {
            public string FileName { get; set; }

            public XmlDocument Document { get; set; }

            /// <summary>Gets or sets the settings dictionary.</summary>
            /// <value>The settings.</value>
            public IDictionary<string, string> Settings { get; set; }
        }

        protected Dictionary<string, SettingsStore> Stores = new Dictionary<string, SettingsStore>();

        private DictionarySerializer serializer = new DictionarySerializer();

        private const string defaultStoreKey = "APP_SETTINGS_STORE";

        #region ISimpleSettingsService Members

        #region Base methods (all stores)

        /// <summary>
        /// Gets the list of all the keys identifying a settings store.
        /// </summary>
        /// <value>The settings store keys.</value>
        public string[] SettingsStoreKeys
        {
            get { return Stores.Keys.ToArray(); }
        }

        /// <summary>
        /// Determines whether this service contains a settings store identified by the specified key.
        /// </summary>
        /// <param name="storeKey">The store key.</param>
        /// <returns>
        /// 	<c>true</c> if this service contains a settings store identified by the specified key; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsSettingsStore(string storeKey)
        {
            return Stores.ContainsKey(storeKey);
        }

        /// <summary>
        /// Associate the settings read from file <paramref name="fileName"/>
        /// with the key <paramref name="key"/>.
        /// </summary>
        /// <param name="storeKey">The key that will allow further access to newly created settings store.</param>
        /// <param name="settingsFileName">Settings file to read.</param>
        public void AddSettingsStore(string storeKey, string settingsFileName)
        {
            if (string.IsNullOrEmpty(settingsFileName)) throw new ArgumentNullException("settingsFileName");
            if (Stores.ContainsKey(storeKey))
                throw new ArgumentException(string.Format("Settings store {0} already exists.", storeKey), "storeKey");

            var dictionary = new Dictionary<string, string>();

            var doc = new XmlDocument();
            if (!File.Exists(settingsFileName))
                This.Logger.Info(string.Format(SR.SettingsFileNotFound, settingsFileName, storeKey));
            else
                doc.Load(settingsFileName);
            serializer.Deserialize(dictionary, doc);

            Stores.Add(storeKey, new SettingsStore()
            {
                Settings = dictionary,
                FileName = settingsFileName,
                Document = doc
            });
        }

        /// <summary>
        /// Associate the settings read from the dictionary <paramref name="settings"/>
        /// with the key <paramref name="key"/>.
        /// </summary>
        /// <param name="storeKey">he key that will allow further access to newly created settings store.</param>
        /// <param name="settings">The settings to load.</param>
        public void AddSettingsStore(string storeKey, IDictionary<string, string> settings)
        {
            if (settings == null) throw new ArgumentNullException("settings");
            if (Stores.ContainsKey(storeKey)) throw new ArgumentException(string.Format(
                "Settings store {0} already exists.", storeKey), "storeKey");

            Stores.Add(storeKey, new SettingsStore() { Settings = settings });
        }

        /// <summary>
        /// Refreshes a settings store with data on disk.
        /// </summary>
        /// <param name="storeKey">The store key.</param>
        public void RefreshSettingsStore(string storeKey)
        {
            if (Stores.ContainsKey(storeKey))
            {
                string fileName = Stores[storeKey].FileName;
                Stores.Remove(storeKey);
                AddSettingsStore(storeKey, fileName);
            }
            else throw new ArgumentException(string.Format(
                "Settings store {0} doesn't exist.", storeKey), "storeKey");
        }

        /// <summary>
        /// Removes the settings store identified by <paramref name="key"/>
        /// from the service.
        /// </summary>
        /// <param name="storeKey">The key identifying the settings store to remove.</param>
        public void RemoveSettingsStore(string storeKey)
        {
            if (Stores.ContainsKey(storeKey)) Stores.Remove(storeKey);
        }

        /// <summary>
        /// Obtains the settings identified by the key <paramref name="key"/>.
        /// </summary>
        /// <typeparam name="T">Represents the type of the target object.</typeparam>
        /// <param name="storeKey">The settings store key.</param>
        /// <returns>A filled instance of the target object.</returns>
        /// <remarks>
        /// 	<para>
        /// If the requested type is a <see cref="IDictionary{T, T}"/> and <c>T</c> is <see cref="System.String"/>,
        /// we just return a copy of our internal dictionary.
        /// </para>
        /// 	<para>
        /// We try to map the keys found in the source dictionary with properties of the target
        /// object.
        /// </para>
        /// 	<para>
        /// 		<b>Important: the target object type must provide a parameterless constructor.</b>
        /// 	</para>
        /// </remarks>
        public virtual T GetSettingsStore<T>(string storeKey) where T : class, new()
        {
            if (!Stores.ContainsKey(storeKey)) throw new ArgumentException(string.Format(
                "Settings store {0} doesn't exist", storeKey), "storeKey");

            T result = null;

            try
            {
                result = serializer.CreateObject<T>(Stores[storeKey].Settings);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format(
                    "Unable to cast settings from settings store {0} to type {1}:\r\n{2}",
                    storeKey, typeof(T), ex.ToFormattedString()));
            }

            return result;
        }

        /// <summary>
        /// Fills the specified object (<paramref name="target)"/>with the settings obtained
        /// from the store identified by the key <paramref name="key"/>.
        /// </summary>
        /// <typeparam name="T">Represents the type of the target object.</typeparam>
        /// <param name="storeKey">The settings store key.</param>
        /// <param name="target">The object to fill.</param>
        /// <returns>We return the passed in target.</returns>
        /// <remarks>
        /// 	<para>
        /// If the requested type is a <see cref="IDictionary{T, T}"/> and <c>T</c> is <see cref="System.String"/>, 
        /// we just return a copy of our internal dictionary.
        /// </para>
        /// 	<para>
        /// We try to map the keys found in the source dictionary with properties of the target
        /// object.
        /// </para>
        /// </remarks>
        public T FillObjectFromSettingsStore<T>(string storeKey, T target) where T : class
        {
            if (target == null) throw new ArgumentNullException("target");
            if (!Stores.ContainsKey(storeKey)) throw new ArgumentException(string.Format(
                "Settings store {0} doesn't exist", storeKey), "storeKey");

            try
            {
                serializer.FillObject(Stores[storeKey].Settings, target);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format(
                    "Unable to cast settings from settings store {0} to type {1}:\r\n{2}",
                    storeKey, typeof(T), ex.ToFormattedString()));
            }

            return target;
        }

        /// <summary>
        /// Obtains the settings identified by the key <paramref name="key"/> as a dictionary of strings.
        /// </summary>
        /// <param name="storeKey">The settings store key.</param>
        /// <returns>A dictionary of strings.</returns>
        public IDictionary<string, string> GetSettingsStore(string storeKey)
        {
            return GetSettingsStore<Dictionary<string, string>>(storeKey);
        }

        /// <summary>
        /// Updates the settings store identified by the key <paramref name="key"/> using
        /// data from the specified parameter.
        /// </summary>
        /// <typeparam name="T">Type of the <paramref name="data"/> parameter.</typeparam>
        /// <param name="storeKey">The store key.</param>
        /// <param name="data">The data.</param>
        /// <remarks>
        /// The store's underlying xml file is saved when calling this method. This means
        /// the method will fail if the store is not associated with a file.
        /// </remarks>
        public void UpdateSettingsStore<T>(string storeKey, T data) where T : class
        {
            if (!Stores.ContainsKey(storeKey)) throw new ArgumentException(string.Format(
                "Settings store {0} doesn't exist", storeKey), "storeKey");
            if (string.IsNullOrEmpty(Stores[storeKey].FileName)) throw new ApplicationException(string.Format(
                "Can't update settings store {0}: it is not associated with a file. You should rather use SaveSettingsStore.", storeKey));

            if (data == null) return;

            // First update our internal dictionary
            serializer.UpdateDictionary(Stores[storeKey].Settings, data);

            // Then save back the dictionary to the xml document and file.
            var store = Stores[storeKey];
            if (store.Document == null) store.Document = new XmlDocument();
            serializer.Serialize(store.Settings, store.Document);
            store.Document.Save(store.FileName);
        }

        /// <summary>
        /// Updates the settings store identified by the key <paramref name="key"/> using
        /// data from the specified parameter.
        /// </summary>
        /// <param name="storeKey">The store key.</param>
        /// <param name="data">The data.</param>
        /// <remarks>
        /// The store's underlying xml file is saved when calling this method. This means
        /// the method will fail, if the store is not associated with a file.
        /// </remarks>
        public void UpdateSettingsStore(string storeKey, IDictionary<string, string> data)
        {
            UpdateSettingsStore<IDictionary<string, string>>(storeKey, data);
        }

        /// <summary>
        /// Saves the settings store identified by the key <paramref name="key"/> using
        /// data from the specified parameter.
        /// </summary>
        /// <typeparam name="T">Type of the <paramref name="data"/> parameter.</typeparam>
        /// <param name="storeKey">The store key.</param>
        /// <param name="data">The data.</param>
        /// <param name="outputFileName">Name of the output xml file.</param>
        public void SaveSettingsStore<T>(string storeKey, T data, string outputFileName) where T : class
        {
            if (!Stores.ContainsKey(storeKey)) throw new ArgumentException(string.Format(
                "Settings store {0} doesn't exist", storeKey), "storeKey");

            Stores[storeKey].FileName = outputFileName;
            UpdateSettingsStore(storeKey, data);
        }

        /// <summary>
        /// Saves the settings store identified by the key <paramref name="key"/> using
        /// data from the specified parameter.
        /// </summary>
        /// <param name="storeKey">The store key.</param>
        /// <param name="data">The data.</param>
        /// <param name="outputFileName">Name of the output xml file.</param>
        public void SaveSettingsStore(string storeKey, IDictionary<string, string> data, string outputFileName)
        {
            SaveSettingsStore<IDictionary<string, string>>(storeKey, data, outputFileName);
        }

        #endregion

        #region Application Settings Store

        /// <summary>
        /// Gets the settings store key for the application's settings.
        /// </summary>
        /// <value>The application settings store key.</value>
        /// <remarks>
        /// If no application settings store was defined, this throws an exception.
        /// </remarks>
        public string ApplicationSettingsStoreKey { get { return defaultStoreKey; } }

        /// <summary>
        /// Gets a value indicating whether this service contains an application settings store.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this service contains an application settings store; otherwise, <c>false</c>.
        /// </value>
        public bool ContainsApplicationSettingsStore
        {
            get { return Stores.ContainsKey(ApplicationSettingsStoreKey); }
        }

        /// <summary>
        /// Gets the application settings store.
        /// </summary>
        /// <typeparam name="T">Represents the type of the target object.</typeparam>
        /// <returns>A filled instance of the target object.</returns>
        /// <remarks>
        /// <para>
        /// If the requested type is a <see cref="IDictionary{TKey, TValue}"/> of <see cref="string"/>
        /// and <see cref="string"/>, we just return a copy of our internal dictionary.
        /// </para>
        /// <para>
        /// We try to map the keys found in the source dictionary with properties of the target
        /// object.
        /// </para>
        /// 	<para>
        /// 		<b>Important: the target object type must provide a parameterless constructor.</b>
        /// 	</para>
        /// </remarks>
        public T GetApplicationSettingsStore<T>() where T : class, new()
        {
            return GetSettingsStore<T>(ApplicationSettingsStoreKey);
        }

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
        public T FillObjectFromApplicationSettingsStore<T>(T target) where T : class
        {
            return FillObjectFromSettingsStore(ApplicationSettingsStoreKey, target);
        }

        /// <summary>
        /// Gets the application settings store as a dictionary of strings.
        /// </summary>
        /// <returns>A dictionary of strings.</returns>
        public IDictionary<string, string> GetApplicationSettingsStore()
        {
            return GetApplicationSettingsStore<Dictionary<string, string>>();
        }

        /// <summary>
        /// Sets the application settings store from the specified file.
        /// </summary>
        /// <param name="applicationSettingsFileName">Name of the application settings file.</param>
        /// <remarks>
        /// If this method is called multiple times, the previous application settings store is lost.
        /// </remarks>
        public void SetApplicationSettingsStore(string applicationSettingsFileName)
        {
            SetApplicationSettingsStore(() =>
                AddSettingsStore(ApplicationSettingsStoreKey, applicationSettingsFileName));
        }

        /// <summary>
        /// Sets the application settings store from the specified dictionary.
        /// </summary>
        /// <param name="settings">The settings dictionary.</param>
        /// <remarks>
        /// If this method is called multiple times, the previous application settings store is lost.
        /// </remarks>
        public void SetApplicationSettingsStore(IDictionary<string, string> settings)
        {
            SetApplicationSettingsStore(() =>
                AddSettingsStore(ApplicationSettingsStoreKey, settings));
        }

        /// <summary>
        /// Refreshes the application settings store with data on disk.
        /// </summary>
        public void RefreshApplicationSettingsStore()
        {
            RefreshSettingsStore(ApplicationSettingsStoreKey);
        }

        /// <summary>
        /// Updates the Application Settings store identified by the key <paramref name="key"/> using
        /// data from the specified parameter.
        /// </summary>
        /// <typeparam name="T">Type of the <paramref name="data"/> parameter.</typeparam>
        /// <param name="data">The data.</param>
        /// <remarks>
        /// The store's underlying xml file is saved when calling this method. This means
        /// the method will fail, if the store is not associated with a file.
        /// </remarks>
        public void UpdateApplicationSettingsStore<T>(T data) where T : class
        {
            UpdateSettingsStore(ApplicationSettingsStoreKey, data);
        }

        /// <summary>
        /// Updates the settings store identified by the key <paramref name="key"/> using
        /// data from the specified parameter.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <remarks>
        /// The store's underlying xml file is saved when calling this method. This means
        /// the method will fail, if the store is not associated with a file.
        /// </remarks>
        public void UpdateApplicationSettingsStore(IDictionary<string, string> data)
        {
            UpdateSettingsStore(ApplicationSettingsStoreKey, data);
        }

        /// <summary>
        /// Saves the Application settings store identified by the key <paramref name="key"/> using
        /// data from the specified parameter.
        /// </summary>
        /// <typeparam name="T">Type of the <paramref name="data"/> parameter.</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="outputFileName">Name of the output xml file.</param>
        public void SaveApplicationSettingsStore<T>(T data, string outputFileName) where T : class
        {
            SaveSettingsStore(ApplicationSettingsStoreKey, data, outputFileName);
        }

        /// <summary>
        /// Saves the settings store identified by the key <paramref name="key"/> using
        /// data from the specified parameter.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="outputFileName">Name of the output xml file.</param>
        public void SaveApplicationSettingsStore(IDictionary<string, string> data, string outputFileName)
        {
            SaveSettingsStore(ApplicationSettingsStoreKey, data, outputFileName);
        }

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
        public T FillObjectWithDefaultValues<T>(T target) where T : class
        {
            if (target == null) throw new ArgumentNullException("target");
            try { target.FillWithDefaultValues(); }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format(
                    "Unable to fill object {0} of type {1} with default values:\r\n{2}",
                    target, typeof(T), ex.ToFormattedString()));
            }

            return target;
        }

        #endregion

        #endregion

        /// <summary>
        /// Sets the application settings store.
        /// </summary>
        /// <param name="addSettingsStore">The add settings store.</param>
        private void SetApplicationSettingsStore(Action addSettingsStore)
        {
            // We save the old settings before overriding them in case there is a failure.
            SettingsStore savedStore = null;
            if (Stores.ContainsKey(ApplicationSettingsStoreKey))
            {
                savedStore = Stores[ApplicationSettingsStoreKey];
                Stores.Remove(ApplicationSettingsStoreKey);
            }

            try { addSettingsStore(); }
            catch
            {
                if (savedStore != null)
                    Stores.Add(ApplicationSettingsStoreKey, savedStore);
                throw;
            }
        }
    }
}
