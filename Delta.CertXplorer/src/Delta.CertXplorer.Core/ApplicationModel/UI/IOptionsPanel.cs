using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Delta.CertXplorer.ApplicationModel.UI
{
    /// <summary>
    /// Defines an option panel (that can be displayed by the <see cref="OptionsDialog"/> form.
    /// </summary>
    public interface IOptionsPanel
    {
        /// <summary>
        /// Called when the <see cref="OptionsDialog"/> raises the <see cref="E:Closing"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        /// <param name="dialogResult">The parent <see cref="OptionsDialog"/> dialog result.</param>
        void OnClosing(CancelEventArgs e, DialogResult dialogResult);

        /// <summary>
        /// Called when the <see cref="OptionsDialog"/> raises the <see cref="E:Closed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <param name="dialogResult">The parent <see cref="OptionsDialog"/> dialog result.</param>
        void OnClosed(EventArgs e, DialogResult dialogResult);
    }
}
