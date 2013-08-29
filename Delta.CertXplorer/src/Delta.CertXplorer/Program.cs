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
using Delta.CertXplorer.Pem;

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

            var instance = new Program()
            {
                // EnableDatabase = false,
                IsSingleInstance = false,
                ApplicationCulture = "en-US",
                LayoutSettingsFileName = Properties.Settings.Default.LayoutSettingsFileName,
                LoggingSettingsFileName = Properties.Settings.Default.LoggingSettingsFileName
            };

            Globals.LayoutSettingsFileName = instance.LayoutSettingsFileName;
            Globals.LoggingSettingsFileName = instance.LoggingSettingsFileName;

            instance.Run(arguments);
        }

        // for debug purpose
        public string GetLayoutSettingsFileName()
        {
            return base.LayoutSettingsFileName;
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
            registry.Register(() => new PemDocumentHandler(), 1000);
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
    }
}
