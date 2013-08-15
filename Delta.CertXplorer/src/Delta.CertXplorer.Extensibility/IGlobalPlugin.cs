using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel.Composition;

namespace Delta.CertXplorer.Extensibility
{
    /// <summary>
    /// Basic interface for plugin that are global to the application.
    /// </summary>
    [InheritedExport]
    public interface IGlobalPlugin : IPlugin
    {
        /// <summary>
        /// Runs this plugin passing it the specified Windows forms parent object.
        /// </summary>
        /// <param name="owner">The Windows forms parent object.</param>
        /// <returns><c>true</c> if the execution was successful; otherwise, <c>false</c>.</returns>
        bool Run(IWin32Window owner);

        /// <summary>
        /// Gets the icon representing this plugin.
        /// </summary>
        /// <value>The icon.</value>
        Image Icon { get; }
    }
}
