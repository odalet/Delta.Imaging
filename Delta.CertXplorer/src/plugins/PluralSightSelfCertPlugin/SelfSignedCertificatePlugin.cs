using System;
using System.Drawing;
using System.Windows.Forms;
using Delta.CertXplorer.Extensibility;
using Delta.CertXplorer.Extensibility.UI;
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

                // Get the user config directory.
                string userConfigDir = null;
                try
                {
                    var host = base.Services.GetService<IHostService>(true);
                    userConfigDir = host.UserConfigDirectory;
                }
                catch (Exception ex)
                {
                    base.Log.Error(string.Format(
                        "Could not retrieve the User Configuration Directory: {0}", ex.Message), ex);
                }

                using (var form = new GenerateSelfSignedCertForm())
                {
                    if (userConfigDir != null)
                        form.GetUserConfigDirectory = () => userConfigDir;
                            
                    form.ShowDialog(owner);
                }

                return true;
            }
            catch (Exception ex)
            {
                base.Log.Error(ex);
                var message = string.Format(
                    "There was an error while executing plugin {0}:\r\n\r\n{1}", PluginName, ex.Message);
                ErrorBox.Show(owner, message);

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
