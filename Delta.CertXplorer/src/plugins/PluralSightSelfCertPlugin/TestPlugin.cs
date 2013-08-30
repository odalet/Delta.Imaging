using System;
using System.Windows.Forms;

using Delta.CertXplorer.Extensibility;

namespace PluralSightSelfCertPlugin
{
    internal class TestPlugin : BaseGlobalPlugin
    {
        private static readonly Guid guid = new Guid("{74BAA351-6DC2-4677-B01F-4B6D62428808}");

        /// <summary>
        /// Called when the plugin is initialized.
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (MessageBox.Show("Throw ?", "?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                throw new ApplicationException("Test Exception");
            else base.Log.Info("Did not throw!");
        }

        /// <summary>
        /// Runs this plugin passing it the specified Windows forms parent object.
        /// </summary>
        /// <param name="owner">The Windows forms parent object.</param>
        /// <returns>
        /// 	<c>true</c> if the execution was successful; otherwise, <c>false</c>.
        /// </returns>
        public override bool Run(System.Windows.Forms.IWin32Window owner)
        {
            base.Log.Debug("Test plugin!");
            return true;
        }

        /// <summary>
        /// Gets the plugin id.
        /// </summary>
        /// <value>The plugin id.</value>
        protected override Guid PluginId
        {
            get { return guid; }
        }

        /// <summary>
        /// Gets the name of the plugin.
        /// </summary>
        /// <value>The name of the plugin.</value>
        protected override string PluginName
        {
            get { return "Test plugin"; }
        }
    }
}
