
namespace Delta.CertXplorer.PluginsManagement
{
    internal class HostService : Delta.CertXplorer.Extensibility.IHostService
    {
        #region IHostService Members

        /// <summary>
        /// Gets the host application name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return ThisAssembly.Name; }
        }

        /// <summary>
        /// Gets the host application version.
        /// </summary>
        /// <value>The version.</value>
        public string Version
        {
            get { return ThisAssembly.Version; }
        }

        #endregion
    }
}
