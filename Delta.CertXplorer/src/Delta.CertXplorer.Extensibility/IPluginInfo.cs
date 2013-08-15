using System;

namespace Delta.CertXplorer.Extensibility
{
    /// <summary>
    /// Represents information about a given plugin.
    /// </summary>
    public interface IPluginInfo
    {
        /// <summary>
        /// Gets the unique id of this plugin.
        /// </summary>
        /// <value>The unique id.</value>
        Guid Id { get; }

        /// <summary>
        /// Gets the name of this plugin.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Gets the description of this plugin.
        /// </summary>
        /// <value>The description.</value>
        string Description { get; }

        /// <summary>
        /// Gets the author of this plugin.
        /// </summary>
        /// <value>The author.</value>
        string Author { get; }

        /// <summary>
        /// Gets the company of this plugin.
        /// </summary>
        /// <value>The company.</value>
        string Company { get; }

        /// <summary>
        /// Gets the version of this plugin.
        /// </summary>
        /// <value>The version.</value>
        string Version { get; }

    }
}
