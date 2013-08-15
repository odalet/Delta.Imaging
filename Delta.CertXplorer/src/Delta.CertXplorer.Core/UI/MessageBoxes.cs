using System;
using System.Windows.Forms;

namespace Delta.CertXplorer.UI
{
    /// <summary>Shows an Error dialog box.</summary>
    public static class ErrorBox
    {
        /// <summary>Shows an Error dialog box.</summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <returns>Always returns <see cref="System.Windows.Forms.DialogResult.OK"/>.</returns>
        public static DialogResult Show(string text)
        {
            return Show(null, text);
        }

        /// <summary>Shows an Error dialog box.</summary>
        /// <param name="owner">Dialog box top-level window and owner.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <returns>Always returns <see cref="System.Windows.Forms.DialogResult.OK"/>.</returns>
        public static DialogResult Show(IWin32Window owner, string text)
        {
            return MsgBox.Show(owner, text, SR.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>Shows a Warning dialog box.</summary>
    public static class WarningBox
    {
        /// <summary>Shows a Warning dialog box.</summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <returns>Always returns <see cref="System.Windows.Forms.DialogResult.OK"/>.</returns>
        public static DialogResult Show(string text)
        {
            return Show(null, text);
        }

        /// <summary>Shows a Warning dialog box.</summary>
        /// <param name="owner">Dialog box top-level window and owner.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <returns>Always returns <see cref="System.Windows.Forms.DialogResult.OK"/>.</returns>
        public static DialogResult Show(IWin32Window owner, string text)
        {
            return Show(owner, text, MessageBoxButtons.OK);
        }

        /// <summary> Shows a Warning dialog box. </summary>
        /// <param name="owner">Dialog box top-level window and owner.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="buttons">The buttons to display</param>
        /// <returns> A <see cref="System.Windows.Forms.DialogResult"/> depending on the button which 
        /// was clicked by the user. </returns>
        public static DialogResult Show(IWin32Window owner, string text, MessageBoxButtons buttons)
        {
            return MsgBox.Show(owner, text, SR.Warning, buttons, MessageBoxIcon.Warning);
        }
    }

    /// <summary>Shows an Information dialog box.</summary>
    public static class InformationBox
    {
        /// <summary>Shows an Information dialog box.</summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <returns>Always returns <see cref="System.Windows.Forms.DialogResult.OK"/>.</returns>
        public static DialogResult Show(string text)
        {
            return Show(null, text);
        }

        /// <summary>Shows an Information dialog box.</summary>
        /// <param name="owner">Dialog box top-level window and owner.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <returns>Always returns <see cref="System.Windows.Forms.DialogResult.OK"/>.</returns>
        public static DialogResult Show(IWin32Window owner, string text)
        {
            return MsgBox.Show(owner, text, SR.Information, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    /// <summary>Shows a Question dialog box.</summary>
    public static class QuestionBox
    {
        /// <summary>Shows a Question dialog box.</summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <returns>Returns either <see cref="System.Windows.Forms.DialogResult.Yes"/> or 
        /// <see cref="System.Windows.Forms.DialogResult.No"/> depending on the button which 
        /// was clicked by the user.</returns>
        public static DialogResult Show(string text)
        {
            return Show(null, text);
        }

        /// <summary>Shows a Question dialog box.</summary>
        /// <param name="owner">Dialog box top-level window and owner.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <returns>Returns either <see cref="System.Windows.Forms.DialogResult.Yes"/> or 
        /// <see cref="System.Windows.Forms.DialogResult.No"/> depending on the button which 
        /// was clicked by the user.</returns>
        public static DialogResult Show(IWin32Window owner, string text)
        {
            return MsgBox.Show(owner, text, SR.Question, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }

    /// <summary>
    /// Shows a Confirmation dialog box
    /// </summary>
    public static class ConfirmationBox
    {
        /// <summary>
        /// Shows a Confirmation dialog box.
        /// </summary>
        /// <param name="text">The text to display in the message box.</param>
        /// <returns>Returns either <see cref="System.Windows.Forms.DialogResult.OK"/> or
        /// <see cref="System.Windows.Forms.DialogResult.Cancel"/> depending on the button which
        /// was clicked by the user.</returns>
        public static DialogResult Show(string text)
        {
            return Show(null, text);
        }

        /// <summary>
        /// Shows a Confirmation dialog box.
        /// </summary>
        /// <param name="owner">Dialog box top-level window and owner.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <returns>
        /// Returns either <see cref="System.Windows.Forms.DialogResult.OK"/> or
        /// <see cref="System.Windows.Forms.DialogResult.Cancel"/> depending on the button which
        /// was clicked by the user.
        /// </returns>
        public static DialogResult Show(IWin32Window owner, string text)
        {
            return MsgBox.Show(owner, text, SR.Confirmation, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }
    }

    /// <summary>
    /// Wraps a call to either <see cref="System.Windows.Forms.MessageBox"/>
    /// or <see cref="ISimpleUIService.ShowMessageBox(string)"/>.
    /// </summary>
    internal static class MsgBox
    {
        /// <summary>
        /// Shows the specified owner.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        /// <returns></returns>
        public static DialogResult Show(
            IWin32Window owner, 
            string text, 
            string caption, 
            MessageBoxButtons buttons,
            MessageBoxIcon icon)
        {
            bool useSimpleUIService = false;
            ISimpleUIService ui = null;

            if (owner == null)
            {
                ui = This.GetService<ISimpleUIService>();
                useSimpleUIService = (ui != null);
            }

            // Determine the correct options depending on the RTL property
            // of the current culture.
            MessageBoxOptions options = 0;
            if (System.Globalization.CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft)
                options = MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign;

            if (useSimpleUIService) return ui.ShowMessageBox(
                text, caption, buttons, icon, 
                MessageBoxDefaultButton.Button1, options);
            else return MessageBox.Show(
                owner, text, caption, buttons, icon, 
                MessageBoxDefaultButton.Button1, options);
        }
    }
}
