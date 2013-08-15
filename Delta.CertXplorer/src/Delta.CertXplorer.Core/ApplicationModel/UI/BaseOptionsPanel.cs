using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Delta.CertXplorer.ApplicationModel.UI
{
    public partial class BaseOptionsPanel : UserControl, IOptionsPanel
    {
        public BaseOptionsPanel()
        {
            InitializeComponent();
        }

        #region IOptionsPanel Members

        /// <summary>
        /// Called when the <see cref="OptionsDialog"/> raises the <see cref="E:Closing"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        /// <param name="dialogResult">The parent <see cref="OptionsDialog"/> dialog result.</param>
        public void OnClosing(CancelEventArgs e, DialogResult dialogResult)
        {
            OnPanelClosing(e, dialogResult);
        }

        /// <summary>
        /// Called when the <see cref="OptionsDialog"/> raises the <see cref="E:Closed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <param name="dialogResult">The parent <see cref="OptionsDialog"/> dialog result.</param>
        public void OnClosed(EventArgs e, DialogResult dialogResult)
        {
            OnPanelClosed(e, dialogResult);
        }

        #endregion

        /// <summary>
        /// Called when the <see cref="OptionsDialog"/> raises the <see cref="E:Closing"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        /// <param name="dialogResult">The parent <see cref="OptionsDialog"/> dialog result.</param>
        protected virtual void OnPanelClosing(CancelEventArgs e, DialogResult dialogResult)
        {
        }

        /// <summary>
        /// Called when the <see cref="OptionsDialog"/> raises the <see cref="E:Closed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <param name="dialogResult">The parent <see cref="OptionsDialog"/> dialog result.</param>
        protected virtual void OnPanelClosed(EventArgs e, DialogResult dialogResult)
        {
        }
    }
}
