//using System;
//using System.IO;
//using System.Collections.Generic;

//using Delta.CertXplorer;
//using Delta.CertXplorer.Extensions;
//using Delta.CertXplorer.Configuration;

//using Delta.CertXplorer.ApplicationModel.Extensions;

//namespace Delta.CertXplorer.ApplicationModel.Services
//{
//    /// <summary>
//    /// A complete Settings service holding settings for the application, for modules, or custom stores.
//    /// </summary>
//    internal class SettingsService : SimpleSettingsService, ISettingsService
//    {
//        #region Module Settings Stores

//        /// <summary>
//        /// Gets the module settings store key associated with the specified module descriptor.
//        /// </summary>
//        /// <param name="descriptor">The module descriptor.</param>
//        /// <returns>
//        /// A <see cref="System.String"/> key if found ; otherwise, <c>null</c>.
//        /// </returns>
//        /// <remarks>
//        /// If no settings store was defined for the requested module, this throws an exception.
//        /// </remarks>
//        public string GetModuleSettingsStoreKey(IModuleDescriptor descriptor)
//        {
//            if (descriptor == null) throw new ArgumentNullException("descriptor");
//            return descriptor.Id;
//        }

//        /// <summary>
//        /// Determines whether this service contains a module settings store for the specified module descriptor.
//        /// </summary>
//        /// <param name="descriptor">The module descriptor.</param>
//        /// <returns>
//        /// 	<c>true</c> if this service contains a module settings store for the specified module; otherwise, <c>false</c>.
//        /// </returns>
//        public bool ContainsModuleSettingsStore(IModuleDescriptor descriptor)
//        {
//            return base.Stores.ContainsKey(GetModuleSettingsStoreKey(descriptor));
//        }

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
//        public T GetModuleSettingsStore<T>(IModuleDescriptor descriptor) where T : class, new()
//        {
//            if (descriptor == null) throw new ArgumentNullException("descriptor");
//            return base.GetSettingsStore<T>(descriptor.Id);
//        }

//        /// <summary>
//        /// Gets a module settings store for the specified module descriptor as a dictionary of strings.
//        /// </summary>
//        /// <param name="descriptor">The descriptor.</param>
//        /// <returns>A dictionary of strings.</returns>
//        public IDictionary<string, string> GetModuleSettingsStore(IModuleDescriptor descriptor)
//        {
//            return GetModuleSettingsStore<Dictionary<string, string>>(descriptor);
//        }

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
//        public T FillObjectFromModuleSettingsStore<T>(IModuleDescriptor descriptor, T target) where T : class
//        {
//            return base.FillObjectFromSettingsStore(GetModuleSettingsStoreKey(descriptor), target);
//        }

//        /// <summary>
//        /// Adds a module settings store.
//        /// </summary>
//        /// <param name="descriptor">The module descriptor.</param>
//        public void AddModuleSettingsStore(IModuleDescriptor descriptor)
//        {
//            if (descriptor == null) throw new ArgumentNullException("descriptor");

//            if (base.Stores.ContainsKey(descriptor.Id)) throw new ArgumentException(string.Format(
//                "Settings store for module {0} already exists.", descriptor.Id), "descriptor");

//            Type settingsType = descriptor.ModuleSettingsType;
//            if (settingsType == null) return;

//            // Try to load the module settings file.
//            var fileName = ResolveSettingsFileName(descriptor);
//            if (!File.Exists(fileName))
//            {
//                This.Logger.Warning(string.Format(
//                   "Module settings file (file://{0}) for module {1} was not found.", fileName, descriptor.Id));
//                return;
//            }

//            base.AddSettingsStore(descriptor.Id, fileName);
//        }

//        /// <summary>
//        /// Refreshes a module settings store with data on disk.
//        /// </summary>
//        /// <param name="descriptor">The module descriptor.</param>
//        public void RefreshModuleSettingsStore(IModuleDescriptor descriptor)
//        {
//            base.RefreshSettingsStore(GetModuleSettingsStoreKey(descriptor));
//        }

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
//        public void UpdateModuleSettingsStore<T>(IModuleDescriptor descriptor, T data) where T : class
//        {
//            base.UpdateSettingsStore(GetModuleSettingsStoreKey(descriptor), data);
//        }

//        /// <summary>
//        /// Updates a Module Settings store identified by the descriptor <paramref name="descriptor"/> using
//        /// data from the specified parameter.
//        /// </summary>
//        /// <param name="descriptor">The descriptor.</param>
//        /// <param name="data">The data.</param>
//        /// <remarks>
//        /// The store's underlying xml file is saved when calling this method. This means
//        /// the method will fail, if the store is not associated with a file.
//        /// </remarks>
//        public void UpdateModuleSettingsStore(IModuleDescriptor descriptor, IDictionary<string, string> data)
//        {
//            base.UpdateSettingsStore(GetModuleSettingsStoreKey(descriptor), data);
//        }

//        /// <summary>
//        /// Saves a Module Settings store identified by the descriptor <paramref name="descriptor"/> using
//        /// data from the specified parameter.
//        /// </summary>
//        /// <typeparam name="T">Type of the <paramref name="data"/> parameter.</typeparam>
//        /// <param name="descriptor">The descriptor.</param>
//        /// <param name="data">The data.</param>
//        /// <param name="outputFileName">Name of the output xml file.</param>
//        public void SaveModuleSettingsStore<T>(IModuleDescriptor descriptor, T data, string outputFileName) where T : class
//        {
//            base.SaveSettingsStore(GetModuleSettingsStoreKey(descriptor), data, outputFileName);
//        }

//        /// <summary>
//        /// Saves a Module Settings store identified by the descriptor <paramref name="descriptor"/> using
//        /// data from the specified parameter.
//        /// </summary>
//        /// <param name="descriptor">The descriptor.</param>
//        /// <param name="data">The data.</param>
//        /// <param name="outputFileName">Name of the output xml file.</param>
//        public void SaveModuleSettingsStore(IModuleDescriptor descriptor, IDictionary<string, string> data, string outputFileName)
//        {
//            base.SaveSettingsStore(GetModuleSettingsStoreKey(descriptor), data, outputFileName);
//        }        

//        #endregion

//        /// <summary>
//        /// Resolves the name of the settings file associated with the module identified 
//        /// by the specified descriptor.
//        /// </summary>
//        /// <param name="descriptor">A module descriptor.</param>
//        /// <returns>The settings file associated with the module identified 
//        /// by the specified descriptor.</returns>
//        private string ResolveSettingsFileName(IModuleDescriptor descriptor)
//        {
//            return descriptor.GetSettingsFileName();
//        }
//    }
//}
