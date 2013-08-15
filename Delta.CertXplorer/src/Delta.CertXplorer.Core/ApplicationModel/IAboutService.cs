using System;
using System.Drawing;
using System.Windows.Forms;

namespace Delta.CertXplorer.ApplicationModel
{
    /// <summary>
    /// Defines the basic services that should be implemented in order to provide 
    /// 'About application' information.
    /// </summary>
    public interface IAboutService
    {
        /// <summary>Shows the 'About' dialog box.</summary>
        /// <param name="owner">The dialog box owner.</param>
        /// <returns>The dialog result.</returns>
        DialogResult ShowAboutDialog(IWin32Window owner);

        /// <summary>
        /// Gets the 'About' text.
        /// </summary>
        /// <returns>A string containing information about the application.</returns>
        string GetAboutText();
    }
}
