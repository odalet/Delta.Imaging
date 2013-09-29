using Delta.CertXplorer.Extensibility;

namespace Delta.CertXplorer.PluginsManagement
{
    internal class HostService : IHostService
    {

        #region IHostService Members

        /// <summary>
        /// Gets the host application name.
        /// </summary>
        public string Name
        {
            get { return ThisAssembly.Name; }
        }

        /// <summary>
        /// Gets the host application version.
        /// </summary>
        public string Version
        {
            get { return ThisAssembly.Version; }
        }

        /// <summary>
        /// Gets the user configuration directory.
        /// </summary>
        public string UserConfigDirectory
        {
            get { return PathHelper.UserConfigDirectory; }
        }

        /// <summary>
        /// Gets the user data directory.
        /// </summary>
        public string UserDataDirectory
        {
            get { return PathHelper.UserDataDirectory; }
        }

        #endregion
    }
}
