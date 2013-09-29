using System;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

using Delta.CertXplorer.Extensibility;
using Delta.CertXplorer.Extensibility.UI;
using System.ComponentModel;

namespace CertToolsPlugin
{
    internal class CertMmcPlugin : BaseGlobalPlugin
    {
        private static readonly CertMmcPluginInfo pluginInfo = new CertMmcPluginInfo();

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

                // Get the user data directory.
                string userDataDir = null;
                try
                {
                    var host = base.Services.GetService<IHostService>(true);
                    userDataDir = host.UserDataDirectory;
                }
                catch (Exception ex)
                {
                    base.Log.Warning(string.Format(
                        "Could not retrieve the User Data Directory; falling back to the System temporary data directory: {0}", ex.Message), ex);
                }

                try
                {
                    if (userDataDir == null)
                        userDataDir = Path.GetTempPath();
                    if (File.Exists(userDataDir)) File.Delete(userDataDir);
                    if (!Directory.Exists(userDataDir)) Directory.CreateDirectory(userDataDir);
                }
                catch (Exception ex)
                {
                    var message = string.Format("Could not retrieve the System temporary data directory: {0}", ex.Message);
                    base.Log.Error(message, ex);
                    ErrorBox.Show(owner, message);

                    return false;
                }

                try
                {
                    var targetFile = Path.Combine(userDataDir, "certs.msc");
                    if (!File.Exists(targetFile))
                    {
                        var msc = CertificateMmcTemplates.MscTemplate;
                        File.WriteAllText(targetFile, msc);
                    }

                    Process.Start(targetFile);
                }
                catch (Win32Exception wex)
                {
                    if ((uint)wex.ErrorCode == (uint)0x80004005) // operation was canceled by the user.
                    {
                        base.Log.Info(wex.Message);
                        return true;
                    }
                    else throw;
                }
                catch (Exception ex)
                {
                    var message = string.Format("Could not run the certificates console: {0}", ex.Message);
                    base.Log.Error(message, ex);
                    ErrorBox.Show(owner, message);

                    return false;
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
            get { return Properties.Resources.mmc16; }
        }
    }
}
