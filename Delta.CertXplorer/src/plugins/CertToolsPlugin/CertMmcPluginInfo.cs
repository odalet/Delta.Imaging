using System;
using Delta.CertXplorer.Extensibility;

namespace CertToolsPlugin
{
    internal class CertMmcPluginInfo : IPluginInfo
    {
        private static readonly Guid guid = new Guid("{6588DED6-558C-45A3-AF2D-7FDBB612641E}");

        #region IPluginInfo Members

        public Guid Id { get { return guid; } }

        public string Name
        {
            get { return "Certificates MMC Plugin"; }
        }

        public string Description
        {
            get { return "This plugin launches a MMC console preconfigured with user and local machines certificates snap-ins."; }
        }

        public string Author
        {
            get { return "O. DALET"; }
        }

        public string Company
        {
            get { return ThisAssembly.Company; }
        }

        public string Version
        {
            get { return ThisAssembly.PluginVersion; }
        }

        #endregion
    }
}
