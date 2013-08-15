using System;

namespace Delta.CertXplorer.Extensibility
{
    /// <summary>
    /// The Host service represents the hosting application.
    /// </summary>
    public interface IHostService
    {
        /// <summary>
        /// Gets the host application name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Gets the host application version.
        /// </summary>
        /// <value>The version.</value>
        string Version { get; }
    }
}
