using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

using Delta.CertXplorer.UI;
using Delta.CertXplorer.UI.ToolWindows;
using Delta.CertXplorer.Commanding;
using Delta.CertXplorer.Asn1Decoder;
using Delta.CertXplorer.CertManager;
using Delta.CertXplorer.DocumentModel;
using Delta.CertXplorer.ApplicationModel;

namespace Delta.CertXplorer
{
    public partial class Chrome : BaseChrome 
    {
        private List<string> filesToOpenAtStartup = new List<string>();
        /// <summary>
        /// Initializes a new instance of the <see cref="Chrome"/> class.
        /// </summary>
        public Chrome()
        {
            InitializeComponent();

            base.Text = Program.ApplicationName;
            base.MenuStrip.MdiWindowListItem = windowMenuItem;
            base.ViewMenuItem = viewMenuItem;

            CreatePluginsMenu();
        }

        internal List<string> FilesToOpenAtStartup
        {
            get { return filesToOpenAtStartup; }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitializeActions();

            // debug
            This.Logger.Verbose(string.Format(
                "Application root directory is: {0}", This.Application.RootDirectory));

            var layoutService = This.GetService<ILayoutService>();
            if (layoutService != null)
            {
                This.Logger.Verbose("Layout service was found.");
                var layoutConfigFile = Globals.LayoutSettingsFileName;
                This.Logger.Verbose(string.Format(
                    "Layout service configuration file is: {0}", layoutConfigFile));
                if (!System.IO.File.Exists(layoutConfigFile)) This.Logger.Warning(
                     "Layout service configuration does not exist.");
            }
            
            // Now open files that were passed on the command lin
            if (FilesToOpenAtStartup.Count == 0) return;

            foreach (var filename in FilesToOpenAtStartup)
                OpenFile(filename);
        }

        /// <summary>
        /// Gets this form id.
        /// </summary>
        /// <value>This form id.</value>
        /// <remarks>
        /// The id is used to store this form's bounds and state in the layout settings file.
        /// </remarks>
        protected override string FormId
        {
            get { return "Delta.CertXplorer.Chrome"; }
        }

        /// <summary>
        /// Creates the tool windows.
        /// </summary>
        protected override void CreateToolWindows()
        {
            var logWindowInfo = new ToolWindowInfo<LogWindow>();
            logWindowInfo.ToolWindow = new LogWindow(false);
            base.RegisterToolWindow(logWindowInfo);

            var certificateStoreWindowInfo = new ToolWindowInfo<CertificateStoreWindow>();
            certificateStoreWindowInfo.ToolWindow = new CertificateStoreWindow();
            base.RegisterToolWindow(certificateStoreWindowInfo);

            var documentManagerWindowInfo = new ToolWindowInfo<DocumentManagerWindow>();
            documentManagerWindowInfo.ToolWindow = new DocumentManagerWindow();
            base.RegisterToolWindow(documentManagerWindowInfo);

            var propertiesWindowInfo = new ToolWindowInfo<PropertiesWindow>();
            propertiesWindowInfo.ToolWindow = new PropertiesWindow();
            base.RegisterToolWindow(propertiesWindowInfo);

            var cerfificateListWindowInfo = new ToolWindowInfo<CertificateListWindow>();
            cerfificateListWindowInfo.ToolWindow = new CertificateListWindow();
            base.RegisterToolWindow(cerfificateListWindowInfo);
        }

        private void OpenFile(string filename)
        {
            var fname = Path.IsPathRooted(filename) ? filename : Path.Combine(Environment.CurrentDirectory, filename);
            Commands.RunVerb(Verbs.OpenFile, fname);
        }

        private void InitializeActions()
        {
            exitAction.Run += (s, e) => Close();
            aboutAction.Run += (s, e) =>
            {
                var service = This.GetService<IAboutService>();
                if (service == null) InformationBox.Show(this, string.Format(
                    "About Delta.CertXplorer Version {0}", ThisAssembly.Version));
                else service.ShowAboutDialog(this);
            };

            openFileDocumentAction.Run += (s, e) => 
                Commands.RunVerb(Verbs.OpenFile);
            openCertificateDocumentAction.Run += (s, e) => 
                Commands.RunVerb(Verbs.OpenCertificate);
        }

        private void CreatePluginsMenu()
        {
            if (!Globals.PluginsManager.GlobalPlugins.Any()) return;

            // Create the menu
            var pluginsMenuItem = new ToolStripMenuItem("&Plugins");
            base.MenuStrip.Items.Insert(3, pluginsMenuItem);

            foreach (var plugin in Globals.PluginsManager.GlobalPlugins)
            {
                var menuItem = new ToolStripMenuItem(plugin.PluginInfo.Name);
                var icon = plugin.GetIcon();
                if (icon != null) menuItem.Image = icon;
                var pluginToRun = plugin;
                menuItem.Click += (s, e) =>
                {
                    bool shouldDisablePlugin = false;
                    Globals.PluginsManager.Run(pluginToRun, this, out shouldDisablePlugin);
                    if (shouldDisablePlugin)
                    {
                        This.Logger.Error("This plugin is failing. It has been deactivated.");
                        menuItem.Enabled = false;
                    }
                };

                pluginsMenuItem.DropDownItems.Add(menuItem);
            }
        }
    }
}

