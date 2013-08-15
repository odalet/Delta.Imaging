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
        private const string defaultDatabaseSettingsFileName = "auth.xml";

        private const string defaultApplicationSettingsDefaultContent =
            @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<settings></settings>";

        private string applicationSettingsFileName = defaultApplicationSettingsFileName;
        private string databaseSettingsFileName = defaultDatabaseSettingsFileName;

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
        protected string ApplicationCulture { get; set; }

        ///// <summary>
        ///// Gets or sets a value indicating whether database access is enabled.
        ///// </summary>
        ///// <value><c>true</c> if database access is enabled; otherwise, <c>false</c>.</value>
        //protected bool EnableDatabase { get; set; }

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
        /// Gets or sets the name of the database settings file (default value is 'auth.xml').
        /// </summary>
        /// <value>The name of the database settings file.</value>
        protected string DatabaseSettingsFileName
        {
            get
            {
                return BuildPathRootedFileName(
                    databaseSettingsFileName, defaultDatabaseSettingsFileName);
            }
            set { databaseSettingsFileName = value; }
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

        //#region Database

        ///// <summary>Creates the database provider service.</summary>
        ///// <remarks>
        ///// In this implementation, we also add the 4 standard database providers (if found) which
        ///// are: 
        ///// <list type="bullet">
        ///// <item><b>Oracle</b>: based on <c>System.Data.OracleClient</c> (the Microsoft ADO.NET provider for Oracle databases).</item>
        ///// <item><b>ODP</b>: based on <c>Oracle.DataAccess</c> (the Oracle ADO.NET provider for Oracle databases).</item>
        ///// <item><b>SqlServer</b>: based on <c>System.Data.SqlClient</c> (the Microsoft ADO.NET provider for SQL Server databases).</item>
        ///// <item><b>SqlServerCE</b>: based on <c>System.Data.SqlServerCe</c> (the Microsoft ADO.NET provider for SQL Server CE databases).</item>
        ///// </list>
        ///// Oracle, ODP, Sql Server and Sql Server CE.
        ///// </remarks>
        ///// <returns>An instance of <see cref="IDatabaseProviderService"/>.</returns>
        //protected virtual IDatabaseProviderService CreateDatabaseProviderService()
        //{
        //    var service = new DatabaseProviderService();
        //    var result = service.AddStandardProviders();

        //    // We check we have at least one provider
        //    if (result.Values.All(v => !v)) throw new ApplicationException(SR.NoDatabaseProvider);

        //    return service;
        //}

        ///// <summary>
        ///// Creates the database service.
        ///// </summary>
        ///// <returns>An instance of <see cref="IDatabaseService"/>.</returns>
        //protected virtual IDatabaseService CreateDatabaseService() { return new DatabaseService(); }

        ///// <summary>
        ///// Creates the connection string provider.
        ///// </summary>
        ///// <remarks>
        ///// In this implementation, we create a <see cref="XmlConnectionStringProvider"/>
        ///// and the connection string file is defined according to the property <see cref="DatabaseSettingsFileName"/>.
        ///// </remarks>
        ///// <returns>An instance of <see cref="IConnectionStringProvider"/>.</returns>
        //protected virtual IConnectionStringProvider CreateConnectionStringProvider()
        //{
        //    return new XmlConnectionStringProvider(DatabaseSettingsFileName);
        //}

        ///// <summary>
        ///// Tests the connection to the default database.
        ///// </summary>
        ///// <remarks>
        ///// If the connection test fails, an exception is thrown.
        ///// </remarks>
        //protected virtual void TestDatabaseConnection()
        //{
        //    TestDatabaseConnection(string.Empty);
        //}

        ///// <summary>
        ///// Tests the connection to the database identified by <paramref name="databaseId"/>.
        ///// </summary>
        ///// <param name="databaseId">The database id.</param>
        ///// <remarks>
        ///// If the connection test fails, an exception is thrown.
        ///// </remarks>
        //protected virtual void TestDatabaseConnection(string databaseId)
        //{
        //    var dbService = This.GetService<IDatabaseService>(true);
        //    try
        //    {
        //        // TODO: provide a TestDatabaseConnection method in the Database object.
        //        var db = dbService.GetDatabase(databaseId);
        //        using (var connection = db.CreateConnection())
        //        {
        //            connection.Open();
        //            connection.Close();
        //        }

        //        This.Logger.Verbose("Connection test was successful");
        //    }
        //    catch (Exception ex)
        //    {
        //        This.Logger.Fatal(ex);
        //        throw new ApplicationException(string.Format(
        //            SR.DatabaseConnectionFailedWithReason, ex.Message), ex);
        //    }
        //}

        //#endregion

        //#region Authentication

        /////// <summary>
        /////// Gets the lambda that checks if the current user is allowed to a role.
        /////// </summary>
        /////// <remarks>
        /////// In this implementation, the lambda always return <c>true</c>.
        /////// This means the user is authorized to anything.
        /////// </remarks>
        /////// <value>The 'is in role' lambda.</value>
        ////protected Func<string, bool> IsInRoleFunc
        ////{
        ////    get { return role => true; }
        ////}

        ///// <summary>
        ///// Creates the authentication service.
        ///// </summary>
        ///// <returns>/// <returns>An instance of an object implementing <see cref="Delta.CertXplorer.Security.IAuthenticationService"/>.</returns></returns>
        //protected virtual IAuthenticationService CreateAuthenticationService()
        //{
        //    return new WindowsAuthenticationService();
        //}

        ///// <summary>
        ///// Authenticates the current user against the current <see cref="Delta.CertXplorer.Security.IAuthenticationService"/>.
        ///// </summary>
        ///// <remarks>
        ///// If the authentication fails, an exception is thrown.
        ///// </remarks>
        //protected virtual AuthenticationResult Authenticate()
        //{
        //    var service = This.GetService<IAuthenticationService>(true);
        //    var returnedPrincipal = service.Authenticate(GetCredentials());

        //    if (returnedPrincipal != null) Thread.CurrentPrincipal = CreatePrincipal(returnedPrincipal);

        //    return service.Result;
        //}

        ///// <summary>
        ///// Gets the credentials.
        ///// </summary>
        //protected virtual ICredentials GetCredentials()
        //{
        //    return new WindowsCredentials(false);
        //}

        ///// <summary>
        ///// Creates a <see cref="IPrincipal"/> object specific to this application.
        ///// </summary>
        ///// <param name="originalPrincipal">The original principal returned by the authentication process.</param>
        ///// <returns>An instance of <see cref="IPrincipal"/>.</returns>
        //protected virtual IPrincipal CreatePrincipal(IPrincipal originalPrincipal)
        //{
        //    //return new BaseSidesPrincipal(originalPrincipal, IsInRoleFunc);
        //    return originalPrincipal;
        //}

        ///// <summary>
        ///// Decides wether to continue loading the application based on the authentication process result.
        ///// </summary>
        ///// <param name="result">The authentication process result.</param>
        ///// <returns><c>true</c> if the application should load; otherwise, <c>false</c>.</returns>
        //protected virtual bool ProcessAuthenticationResult(AuthenticationResult result)
        //{
        //    switch (result)
        //    {
        //        case AuthenticationResult.Success: return true;
        //        case AuthenticationResult.Canceled: return false;
        //        case AuthenticationResult.Failed:
        //            DisplayError(SR.AuthenticationFailedApplicationClosing);
        //            return false;
        //    }

        //    return false;
        //}

        //#endregion

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
