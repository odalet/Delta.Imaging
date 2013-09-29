using System;
using System.Linq;
using System.Windows.Forms;

using Delta.CertXplorer;
using Delta.CertXplorer.Logging;
using Delta.CertXplorer.UI.Theming;
using Delta.CertXplorer.ApplicationModel;

using Delta.CertXplorer.About;
using Delta.CertXplorer.DocumentModel;
using Delta.CertXplorer.PluginsManagement;
using System.IO;
using System.Reflection;
using Delta.CertXplorer.Config;
using Delta.CertXplorer.Configuration;

namespace Delta.CertXplorer
{
    /// <summary>
    /// The Application entry point.
    /// </summary>
    internal sealed class Program : BaseWindowsFormsApplication
    {
        public const string ApplicationName = "CertXplorer";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] arguments)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Resolve Configuration files
            Globals.ApplicationSettingsFileName = ResolveConfigFile(Properties.Settings.Default.AppSettingsFileName);
            Globals.LayoutSettingsFileName = ResolveConfigFile(Properties.Settings.Default.LayoutSettingsFileName);
            Globals.LoggingSettingsFileName = ResolveConfigFile(Properties.Settings.Default.LoggingSettingsFileName);
            
            var instance = new Program()
            {
                IsSingleInstance = false,
                ApplicationSettingsFileName = Globals.ApplicationSettingsFileName,
                LayoutSettingsFileName = Globals.LayoutSettingsFileName,
                LoggingSettingsFileName = Globals.LoggingSettingsFileName
            };

            instance.Run(arguments);
        }

        protected override void AddOtherServices()
        {
            var foo = base.LayoutSettingsFileName;
            base.AddOtherServices();
            This.AddService<IAboutService>(new AboutCertXplorerService());
            
            This.AddService<IDocumentHandlerRegistryService>(
                DocumentFactory.CreateDocumentHandlerRegistryService());
        }

        protected override ILogService CreateLogService()
        {
            var logService = base.CreateLogService();

            // Let's bind this to CapiNet
            var defaultLogger = CapiNet.Globals.ExceptionLogger;
            CapiNet.Globals.ExceptionLogger = ex =>
            {
                bool ok = true;
                if (defaultLogger != null)
                    ok = defaultLogger(ex);
                logService.Log(LogLevel.Error, ex);
                ok &= true;
                return ok;
            };

            return logService;
        }

        /// <summary>
        /// Creates the main form.
        /// </summary>
        /// <returns></returns>
        protected override Form CreateMainForm()
        {
            var chrome = new Chrome();
            Globals.MainForm = chrome;

            // Set the theme
            var themingService = This.GetService<IThemingService>();
            if (themingService != null)
            {
                const string themeId = "Delta.CertXplorer";
                if (themingService.ContainsTheme(themeId))
                    themingService.ApplyTheme(themeId);
            }

            This.AddService<IDocumentManagerService>(
                DocumentFactory.CreateDocumentManagerService(chrome));

            // Load document builders & view builders
            LoadBuilders();

            // Now tell the form what files it should open when launched.            
            if (base.CommandLineArguments != null && base.CommandLineArguments.Length > 0)
                chrome.FilesToOpenAtStartup.AddRange(base.CommandLineArguments); 

            return chrome;
        }

        private void LoadBuilders()
        {
            var registry = This.GetService<IDocumentHandlerRegistryService>(true);
            registry.Register(() => new Asn1DocumentHandler());

            foreach (var p in Globals.PluginsManager.DataHandlerPlugins)
            {
                var plugin = p;
                if (!Globals.PluginsManager.Initialize(plugin))
                {
                    This.Logger.Error("Plugin initialization failed. Disabling it.");
                    continue;
                }

                var documentHandler = new PluginBasedDocumentHandler(plugin);
                registry.RegisterHandlerPlugin(documentHandler); 
            }
        }

        /// <summary>
        /// Called before the main form is created.
        /// </summary>
        /// <returns><c>true</c> if the application should continue loading; otherwise, <c>false</c>.</returns>
        protected override bool OnBeforeCreateMainForm()
        {
            // We plug the mef composition here.
            var pluginsManager = new PluginsManager(
                Properties.Settings.Default.PluginsDirectories.Cast<string>());
            Globals.PluginsManager = pluginsManager;
            pluginsManager.Compose();

            return base.OnBeforeCreateMainForm();
        }

        protected override void LoadApplicationSettings(ISettingsService settingsService)
        {
            base.LoadApplicationSettings(settingsService);

            // The app config file may define the application culture
            var store = settingsService.GetApplicationSettingsStore();
            if (store == null || !store.ContainsKey("culture")) return;
            var cultureName = store["culture"];
            if (!string.IsNullOrEmpty(cultureName))
                base.ApplicationCulture = cultureName;
        }

        private static string ResolveConfigFile(string file, bool forceFileInitialization = false)
        {
            if (string.IsNullOrEmpty(file)) return string.Empty;

            var filename = Path.GetFileName(file);
            var userRoot = PathHelper.UserConfigDirectory;
            var userFile = Path.Combine(userRoot, filename);

            if (!File.Exists(userFile) || forceFileInitialization)
            {
                // Let's see if we don't have a template file in our resources
                var bytes = ConfigResources.GetResource(filename);
                if (bytes != null && bytes.Length > 0) 
                    File.WriteAllBytes(userFile, bytes);
            }

            return userFile;
        }
    }
}
