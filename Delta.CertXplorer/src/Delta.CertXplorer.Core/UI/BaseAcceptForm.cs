using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Delta.CertXplorer.UI
{
    /// <summary>
    /// Base class for forms displaying a <b>OK</b> and a <b>Cancel</b> button
    /// </summary>
    public partial class BaseAcceptForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAcceptForm"/> class.
        /// </summary>
        public BaseAcceptForm() { InitializeComponent(); }

        /// <summary>
        /// Gets or sets the accept button text.
        /// </summary>
        /// <value>The accept button text.</value>
        public string AcceptButtonText { get { return btnAccept.Text; } set { btnAccept.Text = value; } }

        /// <summary>
        /// Gets or sets the cancel button text.
        /// </summary>
        /// <value>The cancel button text.</value>
        public string CancelButtonText { get { return btnCancel.Text; } set { btnCancel.Text = value; } }

        /// <summary>
        /// Gets or sets a value indicating whether [accept button visible].
        /// </summary>
        /// <value><c>true</c> if [accept button visible]; otherwise, <c>false</c>.</value>
        public bool AcceptButtonVisible { get { return btnAccept.Visible; } set { btnAccept.Visible = value; } }

        /// <summary>
        /// Gets or sets a value indicating whether [cancel button visible].
        /// </summary>
        /// <value><c>true</c> if [cancel button visible]; otherwise, <c>false</c>.</value>
        public bool CancelButtonVisible { get { return btnCancel.Visible; } set { btnCancel.Visible = value; } }

        /// <summary>
        /// Gets or sets a value indicating whether [accept button enabled].
        /// </summary>
        /// <value><c>true</c> if [accept button enabled]; otherwise, <c>false</c>.</value>
        public bool AcceptButtonEnabled { get { return btnAccept.Enabled; } set { btnAccept.Enabled = value; } }

        /// <summary>
        /// Gets or sets a value indicating whether [cancel button enabled].
        /// </summary>
        /// <value><c>true</c> if [cancel button enabled]; otherwise, <c>false</c>.</value>
        public bool CancelButtonEnabled { get { return btnCancel.Enabled; } set { btnCancel.Enabled = value; } }

        /// <summary>
        /// Handles the Click event of the btnAccept control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnAccept_Click(object sender, EventArgs e)
        {
            CancelEventArgs ce = new CancelEventArgs(false);
            OnAccepting(ce);
            if (ce.Cancel) DialogResult = DialogResult.None;
            else
            {
                OnAccepted(e);
                DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelEventArgs ce = new CancelEventArgs(false);
            OnCanceling(ce);
            if (ce.Cancel) DialogResult = DialogResult.None;
            else
            {
                OnCanceled(e);
                DialogResult = DialogResult.Cancel;
            }
        }

        /// <summary>
        /// Raises the <see cref="E:Accepting"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        protected virtual void OnAccepting(CancelEventArgs e) { }

        /// <summary>
        /// Raises the <see cref="E:Canceling"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        protected virtual void OnCanceling(CancelEventArgs e) { }

        /// <summary>
        /// Raises the <see cref="E:Accepted"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnAccepted(EventArgs e) { }

        /// <summary>
        /// Raises the <see cref="E:Canceled"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnCanceled(EventArgs e) { }
    }
}
