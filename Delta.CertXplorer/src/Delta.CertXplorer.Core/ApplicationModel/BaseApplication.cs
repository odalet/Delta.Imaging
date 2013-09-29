using System;
using System.IO;

using Delta.CertXplorer.Logging;
using Delta.CertXplorer.Diagnostics;
using Delta.CertXplorer.Configuration;

namespace Delta.CertXplorer.ApplicationModel
{
    public abstract class BaseApplication
    {
        private const string defaultApplicationSettingsFileName = "app.settings.xml";
        private const string defaultApplicationSettingsDefaultContent =
            @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<settings></settings>";

        private string applicationSettingsFileName = defaultApplicationSettingsFileName;
        private string applicationCulture = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApplication"/> class.
        /// </summary>
        protected BaseApplication()
        {
            // Initializes the properties default values.
            //EnableDatabase = false;
            ApplicationCulture = This.Application.Culture.ToString();
            ApplicationSettingsDefaultContent = defaultApplicationSettingsDefaultContent;
        }

        #region Standard properties available for all application types

        /// <summary>
        /// Gets or sets this application's culture.
        /// </summary>
        /// <value>The application culture.</value>
        protected string ApplicationCulture 
        {
            get { return applicationCulture; }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                if (applicationCulture == value) return;
                applicationCulture = value;
                This.SetApplicationCulture(applicationCulture);
            }
        }

        /// <summary>
        /// Gets or sets the name of the application settings file.
        /// </summary>
        /// <value>The name of the application settings file.</value>
        protected string ApplicationSettingsFileName
        {
            get
            {
                return BuildPathRootedFileName(
                    applicationSettingsFileName, defaultApplicationSettingsFileName);
            }
            set { applicationSettingsFileName = value; }
        }

        /// <summary>
        /// Gets or sets the default content of the application settings file.
        /// </summary>
        /// <value>The default content of the application settings file.</value>
        protected string ApplicationSettingsDefaultContent { get; set; }

        #endregion

        /// <summary>
        /// When implemented, initializes the application: should at least set 
        /// <c>ThisApplicationType</c> using a <c>This.InitializeXxxApplication</c> method.
        /// </summary>
        protected abstract void InitializeThisApplication();

        #region Unhandled exceptions

        /// <summary>
        /// Creates the exception handler service; in this default implementation, returns <c>null</c>.
        /// </summary>
        /// <returns>An instance of an object implementing <see cref="Delta.CertXplorer.Diagnostics.IExceptionHandlerService"/> or <c>null</c>.</returns>
        protected virtual IExceptionHandlerService CreateExceptionHandlerService()
        {
            return null;
        }

        #endregion

        #region Logging

        /// <summary>
        /// Creates the logging service; in this default implementation, returns <c>null</c>.
        /// </summary>
        /// <returns>An instance of an object implementing <see cref="Delta.CertXplorer.Logging.ILogService"/> or <c>null</c>.</returns>
        protected virtual ILogService CreateLogService()
        {
            return null;
        }

        #endregion

        #region Settings

        /// <summary>
        /// Creates the settings management service: the returned service can't be null.
        /// </summary>
        /// <returns>An instance of an object implementing <see cref="Delta.CertXplorer.Configuration.ISimpleSettingsService"/>.</returns>
        protected virtual ISettingsService CreateSettingsService()
        {
            return new SettingsService();
        }

        /// <summary>
        /// Loads the application settings, if no application settings file exists, one is created.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        protected virtual void LoadApplicationSettings(ISettingsService settingsService)
        {
            if (string.IsNullOrEmpty(ApplicationSettingsFileName))
                ApplicationSettingsFileName = defaultApplicationSettingsFileName;

            if (!File.Exists(ApplicationSettingsFileName))
            {
                using (var writer = File.CreateText(ApplicationSettingsFileName))
                {
                    writer.Write(ApplicationSettingsDefaultContent);
                    writer.Close();
                }
            }

            settingsService.SetApplicationSettingsStore(ApplicationSettingsFileName);
        }

        #endregion

        #region Other services

        /// <summary>
        /// Adds other services after all the standard service have been added.
        /// </summary>
        protected virtual void AddOtherServices() { }

        #endregion

        #region Helpers

        /// <summary>
        /// Adds a service to the global service container.
        /// </summary>
        /// <typeparam name="T">Service's type</typeparam>
        /// <param name="service">The service instance.</param>
        /// <returns>The service instance.</returns>
        protected virtual T AddService<T>(T service) where T : class
        {
            This.Services.AddService<T>(service);
            This.Logger.Verbose(string.Format("Service {0} was successfully created.", typeof(T)));
            return service;
        }

        /// <summary>
        /// Displays an error message. In this implementation, it is a simple call
        /// to <see cref="M:Console.Error.WriteLine"/>.
        /// </summary>
        /// <param name="error">The error message.</param>
        protected virtual void DisplayError(string error)
        {
            Console.Error.WriteLine(string.Format("Error: {0}", error));
            This.Logger.Error(error);
        }

        /// <summary>
        /// Builds a path rooted file name from the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="defaultFileName">The default file name.</param>
        /// <returns>Path rooted file name.</returns>
        protected virtual string BuildPathRootedFileName(string fileName, string defaultFileName)
        {
            if (string.IsNullOrEmpty(fileName))
                fileName = Path.Combine(This.Application.RootDirectory, defaultFileName);

            if (!Path.IsPathRooted(fileName))
                fileName = Path.Combine(This.Application.RootDirectory, fileName);

            return fileName;
        }

        #endregion
    }
}
