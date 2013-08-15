using System;
using System.Drawing;
using System.Windows.Forms;

namespace Delta.CertXplorer.UI
{
    /// <summary>
    /// This is a basic UI service allowing to display message boxes.
    /// </summary>
    /// <remarks>
    /// The message boxes are bound to the correct parent form.
    /// </remarks>
    public interface ISimpleUIService
    {
        /// <summary>
        /// Shows an error box.
        /// </summary>
        /// <param name="message">The error message.</param>
        void ShowErrorBox(string message);

        /// <summary>
        /// Shows an error box.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void ShowErrorBox(Exception exception);

        /// <summary>
        /// Shows an error box.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The error message.</param>
        void ShowErrorBox(Exception exception, string message);

        /// <summary>
        /// Shows a warning box.
        /// </summary>
        /// <param name="message">The warning message.</param>
        void ShowWarningBox(string message);

        /// <summary>
        /// Shows an information box.
        /// </summary>
        /// <param name="message">The information message.</param>
        void ShowInformationBox(string message);

        /// <summary>
        /// Shows a question box.
        /// </summary>
        /// <param name="message">The question message.</param>
        /// <returns>The message box result.</returns>
        DialogResult ShowQuestionBox(string message);

        /// <summary>
        /// Shows a message box.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The message box result.</returns>
        DialogResult ShowMessageBox(string message);

        /// <summary>
        /// Shows a message box.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The message box caption.</param>
        /// <returns>The message box result.</returns>
        DialogResult ShowMessageBox(string message, string caption);

        /// <summary>
        /// Shows a message box.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="buttons">The buttons to display on the message box.</param>
        /// <returns>The message box result.</returns>
        DialogResult ShowMessageBox(string message, string caption, MessageBoxButtons buttons);

        /// <summary>
        /// Shows a message box.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="buttons">The buttons to display on the message box.</param>
        /// <param name="icon">The icon to display on the message box.</param>
        /// <returns>The message box result.</returns>
        DialogResult ShowMessageBox(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon);

        /// <summary>
        /// Shows a message box.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="buttons">The buttons to display on the message box.</param>
        /// <param name="icon">The icon to display on the message box.</param>
        /// <param name="defaultButton">The default button that will be clicked if Enter is pressed.</param>
        /// <returns>The message box result.</returns>
        DialogResult ShowMessageBox(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton);

        /// <summary>
        /// Shows a message box.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="buttons">The buttons to display on the message box.</param>
        /// <param name="icon">The icon to display on the message box.</param>
        /// <param name="defaultButton">The default button that will be clicked if Enter is pressed.</param>
        /// <param name="options">This message box options.</param>
        /// <returns>The message box result.</returns>
        DialogResult ShowMessageBox(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options);

        /// <summary>
        /// Shows an input box.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The string that was typed by the user.</returns>
        string ShowInputBox(string message);

        /// <summary>
        /// Shows the input box.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The input box caption.</param>
        /// <returns>The string that was typed by the user.</returns>
        string ShowInputBox(string message, string caption);

        /// <summary>
        /// Shows the input box.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The input box caption.</param>
        /// <param name="defaultValue">The default value if the user types nothing.</param>
        /// <returns>The string that was typed by the user.</returns>
        string ShowInputBox(string message, string caption, string defaultValue);

        /// <summary>
        /// Shows a common dialog.
        /// </summary>
        /// <param name="commonDialog">The common dialog to show.</param>
        /// <returns>The common dialog result.</returns>
        DialogResult ShowDialog(CommonDialog commonDialog);

        /// <summary>
        /// Gets the current parent window.
        /// </summary>
        IWin32Window Owner { get; }        
    }
}
