using System;
using Delta.CertXplorer.Extensibility;

namespace PluralSightSelfCertPlugin
{
    internal class SelfSignedCertificatePluginInfo : IPluginInfo
    {
        private static readonly Guid guid = new Guid("{1ABE44BB-39B2-4e99-937F-12C346B1B7C6}");

        #region IPluginInfo Members

        public Guid Id { get { return guid; } }

        public string Name
        {
            get { return "Pluralsight's SelfCert"; }
        }

        public string Description
        {
            get { return ThisAssembly.Description; }
        }

        public string Author
        {
            get { return "O. DALET based on Pluralsight's work"; }
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
