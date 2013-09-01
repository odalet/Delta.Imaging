using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

using Delta.CertXplorer.Extensibility;

namespace Delta.CertXplorer.PluginsManagement
{
    public class PluginsManager
    {
        // This dictionary stores the plugins unique ids and their short name.
        private Dictionary<Guid, string> pluginNames = new Dictionary<Guid, string>();
        private List<IPlugin> initializedPlugins = new List<IPlugin>();
        private ServiceContainer globalServices = new ServiceContainer();
        private string[] pluginsDirectories = new string[] { "." };

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginsManager"/> class.
        /// </summary>
        public PluginsManager(IEnumerable<string> pluginsDirectoriesArray)
        {
            ParsePluginDirectories(pluginsDirectoriesArray);
            AddGlobalServices();
        }

        /// <summary>
        /// Gets or sets the global plugins.
        /// </summary>
        [ImportMany]
        public IEnumerable<IGlobalPlugin> GlobalPlugins { get; set; }

        /// <summary>
        /// Gets or sets the data handler plugins.
        /// </summary>
        [ImportMany]
        public IEnumerable<IDataHandlerPlugin> DataHandlerPlugins { get; set; }

        /// <summary>
        /// Gets the list of all plugins regardless of their type.
        /// </summary>
        public IEnumerable<IPlugin> Plugins
        {
            get
            {
                return GlobalPlugins.Cast<IPlugin>().Union(DataHandlerPlugins.Cast<IPlugin>());
            }
        }

        /// <summary>
        /// Composes: load the available plugins.
        /// </summary>
        public void Compose()
        {
            GlobalPlugins = new List<IGlobalPlugin>();
            DataHandlerPlugins = new List<IDataHandlerPlugin>();

            try
            {
                var directoryCatalogs = pluginsDirectories.Select(d =>
                    new DirectoryCatalog(d));

                var catalog = new AggregateCatalog(directoryCatalogs.ToArray());
                var container = new CompositionContainer(catalog);

                var batch = new CompositionBatch();
                batch.AddPart(this);

                container.Compose(batch);
            }
            catch (Exception ex)
            {
                var debugException = ex;
                This.Logger.Error("Could not load plugins.", ex);
            }
        }

        /// <summary>Runs the specified plugin.</summary>
        /// <param name="plugin">The plugin to run.</param>
        /// <param name="parent">The parent window.</param>
        /// <param name="shouldDisable">if set to <c>true</c> the plugin should be disabled (because it failed).</param>
        public void Run(IGlobalPlugin plugin, IWin32Window parent, out bool shouldDisable)
        {
            if (plugin == null)
            {
                This.Logger.Error("Null plugins are not allowed.");
                shouldDisable = true;
                return;
            }

            shouldDisable = !Initialize(plugin);

            if (!plugin.Run(parent))
                This.Logger.Warning("This plugin notified that it ended in an error state.");
            shouldDisable = false;
        }

        public bool Initialize(IPlugin plugin)
        {
            if (initializedPlugins.Contains(plugin))
                return true;

            try 
            {
                InitializePlugin(plugin);
                return true;
            }
            catch (Exception ex)
            {
                This.Logger.Error("Plugin initialization failed", ex);
                return false;
            }
            finally
            {
                initializedPlugins.Add(plugin);
            }            
        }

        /// <summary>
        /// Parses the plugin directories from the application configuration file.
        /// </summary>
        /// <param name="directories">The plugins directories list.</param>
        private void ParsePluginDirectories(IEnumerable<string> directories)
        {
            List<string> list = directories == null ?
                new List<string>(new[] { "." }) : directories.ToList();
            try
            {
                pluginsDirectories = list.Select(d =>
                {
                    return Path.IsPathRooted(d) ? d : Path.Combine(
                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), d);
                })
                .Where(d =>
                {
                    bool exists = Directory.Exists(d);
                    if (!exists)
                        This.Logger.Warning(string.Format("Plugins directory {0} doesn't exist.", d));
                    return exists;
                })
                .ToArray();
            }
            catch (Exception ex)
            {
                This.Logger.Error("Error while parsing the plugins directories.", ex);
                pluginsDirectories = new string[] { "." };
            }
        }

        /// <summary>
        /// Initializes the specified plugin.
        /// </summary>
        /// <param name="plugin">The plugin.</param>
        private void InitializePlugin(IPlugin plugin)
        {
            CheckPlugin(plugin);

            var services = new ServiceContainer(globalServices);
            // add local services
            services.AddService<Delta.CertXplorer.Extensibility.Logging.ILogService>(
                new PluginsLogService(pluginNames[plugin.PluginInfo.Id]));

            plugin.Initialize(services);
        }

        /// <summary>
        /// Checks the correctness of the specified plugin.
        /// </summary>
        /// <param name="plugin">The plugin.</param>
        private void CheckPlugin(IPlugin plugin)
        {
            if (plugin == null) Throw(new ArgumentNullException("plugin"));
            if (plugin.PluginInfo == null) Throw(new NullReferenceException("The PlugInfo member can't be null."));
            if (plugin.PluginInfo.Id == Guid.Empty) Throw(new NullReferenceException("The PlugInfo.Id member must be set."));

            var id = plugin.PluginInfo.Id;
            if (pluginNames.ContainsKey(id)) Throw(new ApplicationException(string.Format(
                "A plugin with id {0} was already loaded.", id)));

            // Now build the plugin's short name (it must be unique too).
            var name = plugin.PluginInfo.Name;
            if (string.IsNullOrEmpty(name)) name = id.ToString();
            if (pluginNames.Values.Contains(name))
            {
                var version = plugin.PluginInfo.Version;
                if (!string.IsNullOrEmpty(version))
                    name = string.Format("{0} {1}", name, version);
            }

            // this is very unlikely...
            if (pluginNames.Values.Contains(name))
                name = string.Format("{0} [{1}]", name, id);

            // most improbable...
            if (pluginNames.Values.Contains(name))
            {
                int index = 1;
                var baseName = name;
                while (pluginNames.Values.Contains(name))
                {
                    name = string.Format("{0} ({1})", baseName, index);
                    index++;
                }
            }

            pluginNames.Add(id, name);
        }

        /// <summary>
        /// Throws the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        private void Throw(Exception exception)
        {
            This.Logger.Error("Plugin Validation Error.", exception);
            throw exception;
        }

        /// <summary>
        /// Adds the global services.
        /// </summary>
        private void AddGlobalServices()
        {
            globalServices.AddService<Delta.CertXplorer.Extensibility.IHostService>(new HostService());
        }
    }
}
