//using System;
//using System.Collections.Generic;

//using Delta.CertXplorer.Configuration;

//namespace Delta.CertXplorer.ApplicationModel.Services
//{
//    /// <summary>
//    /// A specialized version of <see cref="Delta.CertXplorer.Configuration.ISimpleSettingsService"/>.
//    /// </summary>
//    public interface ISettingsService : ISimpleSettingsService
//    {
//        //#region Application Settings Store

//        ///// <summary>
//        ///// Gets the settings store key for the application's settings.
//        ///// </summary>
//        ///// <remarks>
//        ///// If no application settings store was defined, this throws an exception.
//        ///// </remarks>
//        ///// <value>The application settings store key.</value>
//        //string ApplicationSettingsStoreKey { get; }

//        ///// <summary>
//        ///// Gets a value indicating whether this service contains an application settings store.
//        ///// </summary>
//        ///// <value>
//        ///// 	<c>true</c> if this service contains an application settings store; otherwise, <c>false</c>.
//        ///// </value>
//        //bool ContainsApplicationSettingsStore { get; }

//        ///// <summary>
//        ///// Gets the application settings store.
//        ///// </summary>
//        ///// <typeparam name="T">Represents the type of the target object.</typeparam>
//        ///// <returns>A filled instance of the target object.</returns>
//        ///// <remarks>
//        ///// 	<para>
//        ///// If the requested type is a <see cref="IDictionary&lt;string, string&gt;"/>, we just return a copy of our
//        ///// internal dictionary.
//        ///// </para>
//        ///// 	<para>
//        ///// We try to map the keys found in the source dictionary with properties of the target
//        ///// object.
//        ///// </para>
//        ///// 	<para>
//        ///// 		<b>Important: the target object type must provide a parameterless constructor.</b>
//        ///// 	</para>
//        ///// </remarks>
//        //T GetApplicationSettingsStore<T>() where T : class, new();

//        ///// <summary>
//        ///// Gets the application settings store as a dictionary of strings.
//        ///// </summary>
//        ///// <returns>A dictionary of strings.</returns>
//        //IDictionary<string, string> GetApplicationSettingsStore();

//        ///// <summary>
//        ///// Fills the specified object (<paramref name="target)"/>with the settings obtained
//        ///// from the application settings store.
//        ///// </summary>
//        ///// <typeparam name="T">Represents the type of the target object.</typeparam>
//        ///// <param name="target">The object to fill.</param>
//        ///// <returns>We return the passed in target.</returns>
//        ///// <remarks>
//        ///// 	<para>
//        ///// If the requested type is a <see cref="IDictionary&lt;string, string&gt;"/>, we just return a copy of our
//        ///// internal dictionary.
//        ///// </para>
//        ///// 	<para>
//        ///// We try to map the keys found in the source dictionary with properties of the target
//        ///// object.
//        ///// </para>
//        ///// </remarks>
//        //T FillObjectFromApplicationSettingsStore<T>(T target) where T : class;

//        ///// <summary>
//        ///// Sets the application settings store from the specified file.
//        ///// </summary>
//        ///// <remarks>
//        ///// If this method is called multiple times, the previous application settings store is lost.
//        ///// </remarks>
//        ///// <param name="applicationSettingsFileName">Name of the application settings file.</param>
//        //void SetApplicationSettingsStore(string applicationSettingsFileName);

//        ///// <summary>
//        ///// Sets the application settings store from the specified dictionary.
//        ///// </summary>
//        ///// <param name="settings">The settings dictionary.</param>
//        ///// <remarks>
//        ///// If this method is called multiple times, the previous application settings store is lost.
//        ///// </remarks>
//        //void SetApplicationSettingsStore(IDictionary<string, string> settings);

//        ///// <summary>
//        ///// Refreshes the application settings store with data on disk.
//        ///// </summary>
//        //void RefreshApplicationSettingsStore();  

//        ///// <summary>
//        ///// Updates the Application Settings store identified by the key <paramref name="key"/> using
//        ///// data from the specified parameter.
//        ///// </summary>
//        ///// <remarks>
//        ///// The store's underlying xml file is saved when calling this method. This means
//        ///// the method will fail, if the store is not associated with a file.
//        ///// </remarks>
//        ///// <typeparam name="T">Type of the <paramref name="data"/> parameter.</typeparam>
//        ///// <param name="data">The data.</param>
//        //void UpdateApplicationSettingsStore<T>(T data) where T : class;

//        ///// <summary>
//        ///// Updates the settings store identified by the key <paramref name="key"/> using
//        ///// data from the specified parameter.
//        ///// </summary>
//        ///// <remarks>
//        ///// The store's underlying xml file is saved when calling this method. This means
//        ///// the method will fail, if the store is not associated with a file.
//        ///// </remarks>
//        ///// <param name="data">The data.</param>
//        //void UpdateApplicationSettingsStore(IDictionary<string, string> data);

//        ///// <summary>
//        ///// Saves the Application settings store identified by the key <paramref name="key"/> using
//        ///// data from the specified parameter.
//        ///// </summary>
//        ///// <typeparam name="T">Type of the <paramref name="data"/> parameter.</typeparam>
//        ///// <param name="data">The data.</param>
//        ///// <param name="outputFileName">Name of the output xml file.</param>
//        //void SaveApplicationSettingsStore<T>(T data, string outputFileName) where T : class;

//        ///// <summary>
//        ///// Saves the settings store identified by the key <paramref name="key"/> using
//        ///// data from the specified parameter.
//        ///// </summary>
//        ///// <param name="data">The data.</param>
//        ///// <param name="outputFileName">Name of the output xml file.</param>
//        //void SaveApplicationSettingsStore(IDictionary<string, string> data, string outputFileName);

//        //#endregion

//        #region Module Settings stores

//        /// <summary>
//        /// Gets the module settings store key associated with the specified module descriptor.
//        /// </summary>
//        /// <remarks>
//        /// If no settings store was defined for the requested module, this throws an exception.
//        /// </remarks>
//        /// <param name="descriptor">The module descriptor.</param>
//        /// <returns>A <see cref="System.String"/> key if found ; otherwise, <c>null</c>.</returns>
//        string GetModuleSettingsStoreKey(IModuleDescriptor descriptor);

//        /// <summary>
//        /// Gets a module settings store for the specified module descriptor.
//        /// </summary>
//        /// <typeparam name="T">Represents the type of the target object.</typeparam>
//        /// <param name="descriptor">The descriptor.</param>
//        /// <returns>A filled instance of the target object.</returns>
//        /// <remarks>
//        /// 	<para>
//        /// If the requested type is a <see cref="IDictionary&lt;string, string&gt;"/>, we just return a copy of our
//        /// internal dictionary.
//        /// </para>
//        /// 	<para>
//        /// We try to map the keys found in the source dictionary with properties of the target
//        /// object.
//        /// </para>
//        /// 	<para>
//        /// 		<b>Important: the target object type must provide a parameterless constructor.</b>
//        /// 	</para>
//        /// </remarks>
//        T GetModuleSettingsStore<T>(IModuleDescriptor descriptor) where T : class, new();

//        /// <summary>
//        /// Gets a module settings store for the specified module descriptor as a dictionary of strings.
//        /// </summary>
//        /// <param name="descriptor">The descriptor.</param>
//        /// <returns>A dictionary of strings.</returns>
//        IDictionary<string, string> GetModuleSettingsStore(IModuleDescriptor descriptor);

//        /// <summary>
//        /// Fills the specified object (<paramref name="target)"/>with the settings obtained
//        /// from the store identified by the specified module descriptor.
//        /// </summary>
//        /// <typeparam name="T">Represents the type of the target object.</typeparam>
//        /// <param name="descriptor">The settings store key.</param>
//        /// <param name="target">The object to fill.</param>
//        /// <returns>We return the passed in target.</returns>
//        /// <remarks>
//        /// 	<para>
//        /// If the requested type is a <see cref="IDictionary&lt;string, string&gt;"/>, we just return a copy of our
//        /// internal dictionary.
//        /// </para>
//        /// 	<para>
//        /// We try to map the keys found in the source dictionary with properties of the target
//        /// object.
//        /// </para>
//        /// </remarks>
//        T FillObjectFromModuleSettingsStore<T>(IModuleDescriptor descriptor, T target) where T : class;

//        /// <summary>
//        /// Determines whether this service contains a module settings store for the specified module descriptor.
//        /// </summary>
//        /// <param name="descriptor">The module descriptor.</param>
//        /// <returns>
//        /// 	<c>true</c> if this service contains a module settings store for the specified module; otherwise, <c>false</c>.
//        /// </returns>
//        bool ContainsModuleSettingsStore(IModuleDescriptor descriptor);
        
//        /// <summary>
//        /// Adds a module settings store.
//        /// </summary>
//        /// <param name="descriptor">The module descriptor.</param>
//        void AddModuleSettingsStore(IModuleDescriptor descriptor);

//        /// <summary>
//        /// Refreshes a module settings store with data on disk.
//        /// </summary>
//        /// <param name="descriptor">The module descriptor.</param>
//        void RefreshModuleSettingsStore(IModuleDescriptor descriptor);  

//        /// <summary>
//        /// Updates a Module Settings store identified by the descriptor <paramref name="descriptor"/> using
//        /// data from the specified parameter.
//        /// </summary>
//        /// <typeparam name="T">Type of the <paramref name="data"/> parameter.</typeparam>
//        /// <param name="descriptor">The descriptor.</param>
//        /// <param name="data">The data.</param>
//        /// <remarks>
//        /// The store's underlying xml file is saved when calling this method. This means
//        /// the method will fail, if the store is not associated with a file.
//        /// </remarks>
//        void UpdateModuleSettingsStore<T>(IModuleDescriptor descriptor, T data) where T : class;

//        /// <summary>
//        /// Updates a Module Settings store identified by the descriptor <paramref name="descriptor"/> using
//        /// data from the specified parameter.
//        /// </summary>
//        /// <remarks>
//        /// The store's underlying xml file is saved when calling this method. This means
//        /// the method will fail, if the store is not associated with a file.
//        /// </remarks>
//        /// <param name="data">The data.</param>
//        /// <param name="descriptor">The descriptor.</param>
//        void UpdateModuleSettingsStore(IModuleDescriptor descriptor, IDictionary<string, string> data);

//        /// <summary>
//        /// Saves a Module Settings store identified by the descriptor <paramref name="descriptor"/> using
//        /// data from the specified parameter.
//        /// </summary>
//        /// <typeparam name="T">Type of the <paramref name="data"/> parameter.</typeparam>
//        /// <param name="descriptor">The descriptor.</param>
//        /// <param name="data">The data.</param>
//        /// <param name="outputFileName">Name of the output xml file.</param>
//        void SaveModuleSettingsStore<T>(IModuleDescriptor descriptor, T data, string outputFileName) where T : class;

//        /// <summary>
//        /// Saves a Module Settings store identified by the descriptor <paramref name="descriptor"/> using
//        /// data from the specified parameter.
//        /// </summary>
//        /// <param name="data">The data.</param>
//        /// <param name="outputFileName">Name of the output xml file.</param>
//        /// <param name="descriptor">The descriptor.</param>
//        void SaveModuleSettingsStore(IModuleDescriptor descriptor, IDictionary<string, string> data, string outputFileName);

//        #endregion
//    }
//}
