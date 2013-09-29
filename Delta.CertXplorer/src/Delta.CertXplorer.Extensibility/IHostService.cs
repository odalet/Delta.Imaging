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
        string Name { get; }

        /// <summary>
        /// Gets the host application version.
        /// </summary>
        string Version { get; }

        /// <summary>
        /// Gets the user configuration directory.
        /// </summary>
        string UserConfigDirectory { get; }

        /// <summary>
        /// Gets the user data directory.
        /// </summary>
        string UserDataDirectory { get; }
    }
}
