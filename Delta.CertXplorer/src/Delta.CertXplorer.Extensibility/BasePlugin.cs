using System;
using System.Drawing;
using Delta.CertXplorer.Extensibility.Logging;

namespace Delta.CertXplorer.Extensibility
{
    public abstract class BasePlugin : IPlugin
    {
        protected class BasicPluginInfo : IPluginInfo
        {
            #region IPluginInfo Members

            /// <summary>
            /// Gets the unique id of this plugin.
            /// </summary>
            /// <value>The unique id.</value>
            public Guid Id { get; set; }

            /// <summary>
            /// Gets the name of this plugin.
            /// </summary>
            /// <value>The name.</value>
            public string Name { get; set; }

            /// <summary>
            /// Gets the description of this plugin.
            /// </summary>
            /// <value>The description.</value>
            public string Description
            {
                get { return string.Empty; }
            }

            /// <summary>
            /// Gets the author of this plugin.
            /// </summary>
            /// <value>The author.</value>
            public string Author
            {
                get { return string.Empty; }
            }

            /// <summary>
            /// Gets the company of this plugin.
            /// </summary>
            /// <value>The company.</value>
            public string Company
            {
                get { return string.Empty; }
            }

            /// <summary>
            /// Gets the version of this plugin.
            /// </summary>
            /// <value>The version.</value>
            public string Version
            {
                get { return string.Empty; }
            }

            #endregion
        }

        private IServiceProvider services = null;

        #region IPlugin Members

        /// <summary>
        /// Initializes this plugin with the specified service provider.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <remarks>
        /// This method is called once at the beginning of the life of the plugin.
        /// </remarks>
        public void Initialize(IServiceProvider serviceProvider)
        {
            services = serviceProvider;
            OnInitialized();
        }

        /// <summary>
        /// Gets the plugin info.
        /// </summary>
        /// <value>The plugin info.</value>
        public virtual IPluginInfo PluginInfo
        {
            get
            {
                return new BasicPluginInfo()
                {
                    Id = PluginId,
                    Name = PluginName
                };
            }
        }

        /// <summary>
        /// Gets the icon representing this plugin.
        /// </summary>
        /// <value>The icon.</value>
        public virtual Image Icon
        {
            get { return null; }
        }

        #endregion

        /// <summary>
        /// Gets the services provider for this plugin.
        /// </summary>
        /// <value>This plugin's services.</value>
        public IServiceProvider Services { get { return services; } }

        /// <summary>
        /// Gets the log service for this plugin.
        /// </summary>
        /// <value>The log.</value>
        public ILogService Log
        {
            get { return services.GetService<ILogService>(true); }
        }

        /// <summary>
        /// Gets the plugin id.
        /// </summary>
        /// <value>The plugin id.</value>
        protected abstract Guid PluginId { get; }

        /// <summary>
        /// Gets the name of the plugin.
        /// </summary>
        /// <value>The name of the plugin.</value>
        protected abstract string PluginName { get; }

        /// <summary>
        /// Called when the plugin is initialized.
        /// </summary>
        protected virtual void OnInitialized()
        {
        }
    }
}
