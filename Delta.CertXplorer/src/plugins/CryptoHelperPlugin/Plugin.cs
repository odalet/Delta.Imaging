using System;
using System.Drawing;
using System.Windows.Forms;

using Delta.CertXplorer.Extensibility;

namespace CryptoHelperPlugin
{
    internal class Plugin : BaseGlobalPlugin
    {
        private static readonly PluginInfo pluginInfo = new PluginInfo();

        /// <summary>
        /// Runs this plugin passing it the specified Windows forms parent object.
        /// </summary>
        /// <param name="owner">The Windows forms parent object.</param>
        /// <returns>
        /// 	<c>true</c> if the execution was successful; otherwise, <c>false</c>.
        /// </returns>
        public override bool Run(IWin32Window owner)
        {
            try
            {
                base.Log.Verbose(string.Format("Running {0} plugin.", PluginName));
                using (var form = new PluginMainForm())
                {
                    form.Plugin = this;
                    form.ShowDialog(owner);
                }

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

        /// <summary>
        /// Gets the plugin id.
        /// </summary>
        /// <value>The plugin id.</value>
        protected override Guid PluginId
        {
            get { return pluginInfo.Id; }
        }

        /// <summary>
        /// Gets the name of the plugin.
        /// </summary>
        /// <value>The name of the plugin.</value>
        protected override string PluginName
        {
            get { return pluginInfo.Name; }
        }

        /// <summary>
        /// Gets the plugin info.
        /// </summary>
        /// <value>The plugin info.</value>
        public override IPluginInfo PluginInfo
        {
            get { return pluginInfo; }
        }

        /// <summary>
        /// Gets the icon representing this plugin.
        /// </summary>
        /// <value>The icon.</value>
        public override Image Icon
        {
            get { return Properties.Resources.Key16; }
        }
    }
}
