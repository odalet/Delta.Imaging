using System.Windows.Forms;

using Delta.CertXplorer.UI;
using Delta.CertXplorer.UI.Theming;
using Delta.CertXplorer.Logging;
using Delta.CertXplorer.Logging.log4net;
using Delta.CertXplorer.Diagnostics;
using Delta.CertXplorer.Configuration;
using Delta.CertXplorer.ApplicationModel.Services;

namespace Delta.CertXplorer.ApplicationModel
{

    /// <summary>
    /// Base implementation of a Sides application.
    /// </summary>
    public abstract class BaseWindowsFormsApplication : BaseApplication
    {
        private const string defaultLoggingSettingsFileName = "log4net.config";
        private const string defaultLayoutSettingsFileName = "app.layout.xml";

        private string loggingSettingsFileName = defaultLoggingSettingsFileName;
        private string layoutSettingsFileName = defaultLayoutSettingsFileName;

        /// <summary>
        /// Used to retrieve the default theme from the application configuration file.
        /// </summary>
        private class ThemingSettings
        {
            public string Theme { get; set; }
        }

        //private BaseModulesManager modulesManager = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSidesApplication"/> class.
        /// </summary>
        protected BaseWindowsFormsApplication()
        {
            // Properties default values
            //EnableDatabase = true;
            IsSingleInstance = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this application 
        /// is a single-instance application.
        /// </summary>
        /// <value>
        /// <c>true</c> if this application is whether this application is a 
        /// single-instance application; otherwise, <c>false</c>.
        /// </value>
        protected bool IsSingleInstance { get; set; }

        /// <summary>
        /// Gets or sets the name of the layout settings file.
        /// </summary>
        /// <value>The name of the layout settings file.</value>
        protected string LayoutSettingsFileName
        {
            get
            {
                return base.BuildPathRootedFileName(
                    layoutSettingsFileName, defaultLayoutSettingsFileName);
            }
            set { layoutSettingsFileName = value; }
        }

        /// <summary>
        /// Gets or sets the name of the logging settings file.
        /// </summary>
        /// <value>The name of the logging settings file.</value>
        protected string LoggingSettingsFileName
        {
            get
            {
                return base.BuildPathRootedFileName(
                    loggingSettingsFileName, defaultLoggingSettingsFileName);
            }
            set { loggingSettingsFileName = value; }
        }

        /// <summary>
        /// Gets the command line arguments.
        /// </summary>
        protected string[] CommandLineArguments { get; private set; }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        protected virtual void Run(string[] arguments)
        {
            CommandLineArguments = arguments;
            InitializeThisApplication();

            AddService<ILogService>(CreateLogService());

            // Unhandled exceptions
            AddService<IExceptionHandlerService>(CreateExceptionHandlerService());

            // Settings
            var settingsService = CreateSettingsService();
            AddService<ISettingsService>(settingsService);
            LoadApplicationSettings(settingsService);

            // Layout
            AddService<ILayoutService>(CreateLayoutService());
            
            AddThemingService();
            AddOtherServices();
            if (OnBeforeCreateMainForm()) ShowMainForm();
        }

        #region Initialization

        /// <summary>Initializes the application.</summary>
        protected override void InitializeThisApplication()
        {
            InitializeThisApplication(ApplicationCulture, IsSingleInstance);
        }

        /// <summary>Initializes the application.</summary>
        /// <param name="culture">The application's culture.</param>
        /// <param name="singleInstance">
        /// if set to <c>true</c> then the application will be a single-instance application.
        /// </param>
        /// <remarks>
        /// The culture passed to this method is affected to both the current culture and the current UI culture.
        /// </remarks>
        protected virtual void InitializeThisApplication(string culture, bool singleInstance)
        {
            This.InitializeWindowsFormsApplication(culture, singleInstance);
            ToolStripManager.Renderer = new BaseToolStripRenderer();
        }

        #endregion

        #region Unhandled exceptions

        /// <summary>
        /// Creates the exception handler service.
        /// </summary>
        /// <returns></returns>
        protected override IExceptionHandlerService CreateExceptionHandlerService()
        {
            return new ExceptionHandlerService();
        }

        #endregion

        #region Layout

        /// <summary>
        /// Creates the layout service.
        /// </summary>
        /// <returns>An instance of an object implementing <see cref="Delta.CertXplorer.ApplicationModel.ILayoutService"/>.</returns>
        protected virtual ILayoutService CreateLayoutService()
        {
            return new LayoutService(LayoutSettingsFileName);
        }

        #endregion

        #region Log

        /// <summary>
        /// Creates the logging service.
        /// </summary>
        /// <returns>An instance of an object implementing <see cref="Delta.CertXplorer.Logging.ILogService"/>.</returns>
        protected override ILogService CreateLogService()
        {
            return Log4NetServiceFactory.CreateService(LoggingSettingsFileName);
        }

        #endregion

        #region Settings

        /// <summary>
        /// Creates the settings management service: the returned service can't be null.
        /// </summary>
        /// <returns>An instance of an object implementing <see cref="Delta.CertXplorer.Configuration.ISimpleSettingsService"/>.</returns>
        protected override ISettingsService CreateSettingsService()
        {
            return new SettingsService();
        }

        #endregion

        #region Theming

        /// <summary>
        /// Adds the theming service (if one is provided through <see cref="CreateThemingService"/>).
        /// </summary>
        protected virtual void AddThemingService()
        {
            var service = CreateThemingService();
            if (service != null)
            {
                AddService<IThemingService>(service);

                // Try to read the default theme from the application settings file.
                var settingsService = This.GetService<ISettingsService>(true);
                var theme = settingsService.GetApplicationSettingsStore<ThemingSettings>().Theme;
                if (!string.IsNullOrEmpty(theme) && service.ContainsTheme(theme))
                    service.ApplyTheme(theme);
            }
        }

        /// <summary>
        /// Creates the theming service (The theming service can be <c>null</c>).
        /// </summary>
        /// <returns></returns>
        protected virtual IThemingService CreateThemingService()
        {
            return new ThemingService();
        }

        #endregion

        #region Main Form

        /// <summary>
        /// Gets a value indicating whether the authorized modules should be automatically
        /// created and displayed on application startup.
        /// </summary>
        /// <value><c>true</c> if we should automatically open the modules; otherwise, <c>false</c>.</value>
        protected virtual bool AutoOpenModules
        {
            get { return true; }
        }

        /// <summary>
        /// Creates the main form.
        /// </summary>
        /// <returns>A <see cref="System.Windows.Forms.Form"/> instance.</returns>
        protected abstract Form CreateMainForm();

        /// <summary>
        /// Shows the main form.
        /// </summary>
        protected virtual void ShowMainForm()
        {
            var form = CreateMainForm();
            OnBeforeShowMainForm(form);
            This.Application.Run(form);
        }

        #endregion

        /// <summary>
        /// Called before the main form is created.
        /// </summary>
        /// <returns><c>true</c> if the application should continue loading; otherwise, <c>false</c>.</returns>
        protected virtual bool OnBeforeCreateMainForm()
        {
            return true;
        }

        /// <summary>
        /// Called before the main form is shown.
        /// </summary>
        protected virtual void OnBeforeShowMainForm(Form form)
        {
        }

        protected override void DisplayError(string error)
        {
            ErrorBox.Show(error);
            This.Logger.Error(error);
        }
    }
}
