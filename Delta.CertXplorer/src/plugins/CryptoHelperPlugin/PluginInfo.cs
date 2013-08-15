using System;

using Delta.CertXplorer.Extensibility;

namespace CryptoHelperPlugin
{
    internal class PluginInfo : IPluginInfo
    {
        private static readonly Guid guid = new Guid("{19A43451-B110-4f9a-A103-63A2B569CA0C}");

        #region IPluginInfo Members

        public Guid Id { get { return guid; } }

        public string Name
        {
            get { return "Crypto Helper"; }
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
