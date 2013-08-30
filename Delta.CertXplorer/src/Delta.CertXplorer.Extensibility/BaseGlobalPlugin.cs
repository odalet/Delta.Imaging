using System.Drawing;
using System.Windows.Forms;

namespace Delta.CertXplorer.Extensibility
{
    public abstract class BaseGlobalPlugin : BasePlugin, IGlobalPlugin
    {
        #region IGlobalPlugin Members

        /// <summary>
        /// Runs this plugin passing it the specified Windows forms parent object.
        /// </summary>
        /// <param name="owner">The Windows forms parent object.</param>
        /// <returns>
        /// 	<c>true</c> if the execution was successful; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool Run(IWin32Window owner);
        
        #endregion
    }
}
