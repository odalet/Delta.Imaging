using System;
using System.Drawing;
using System.Windows.Forms;

using Delta.CertXplorer.Extensibility;

using Pluralsight.Crypto.UI;

namespace PluralSightSelfCertPlugin
{
    internal class SelfSignedCertificatePlugin : BaseGlobalPlugin
    {
        private static readonly SelfSignedCertificatePluginInfo pluginInfo = new SelfSignedCertificatePluginInfo();

        public override bool Run(IWin32Window owner)
        {
            try
            {
                base.Log.Verbose(string.Format("Running {0} plugin.", PluginName));
                new GenerateSelfSignedCertForm().ShowDialog(owner);
                return true;
            }
            catch (Exception ex)
            {
                base.Log.Error(ex);
                var message = string.Format(
                    "There was an error while executing plugin {0}:\r\n\r\n{1}", PluginName, ex.Message);
                MessageBox.Show(owner, message, "Error", MessageBoxButtons.OK);

                return false;
            }
        }

        protected override Guid PluginId
        {
            get { return pluginInfo.Id; }
        }

        protected override string PluginName
        {
            get { return pluginInfo.Name; }
        }

        public override IPluginInfo PluginInfo
        {
            get { return pluginInfo; }
        }

        public override Image Icon
        {
            get { return Properties.Resources.pluralsight; }
        }
    }
}
