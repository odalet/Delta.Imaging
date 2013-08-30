using System;
using System.Drawing;

namespace Delta.CertXplorer.Extensibility
{
    /// <summary>
    /// This interface isn't intended to be implemented by plugins.
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Initializes this plugin with the specified service provider.
        /// </summary>
        /// <remarks>
        /// This method is called once at the beginning of the life of the plugin.
        /// </remarks>
        /// <param name="serviceProvider">The service provider.</param>
        void Initialize(IServiceProvider serviceProvider);

        /// <summary>
        /// Gets the plugin info.
        /// </summary>
        /// <value>The plugin info.</value>
        IPluginInfo PluginInfo { get; }

        /// <summary>
        /// Gets the icon representing this plugin.
        /// </summary>
        /// <value>The icon.</value>
        Image Icon { get; }
    }
}
