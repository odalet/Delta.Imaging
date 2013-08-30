using System;

using Delta.CertXplorer.Extensibility;

namespace PemPlugin
{
    internal class PemHandlerPluginInfo : IPluginInfo
    {
        private static readonly Guid guid = new Guid("{962F5C9E-E00C-467A-899A-E61BE4093258}");

        #region IPluginInfo Members

        public Guid Id { get { return guid; } }

        public string Name
        {
            get { return "PEM Plugin"; }
        }

        public string Description
        {
            get { return ThisAssembly.Description; }
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
