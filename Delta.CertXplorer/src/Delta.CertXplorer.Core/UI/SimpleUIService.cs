using System;
using System.Security;
using System.Reflection;
using System.Windows.Forms;

using Delta.CertXplorer.Internals;

namespace Delta.CertXplorer.UI
{
    /// <summary>
    /// Default implementation of <see cref="ISimpleUIService"/>.
    /// </summary>
    internal class SimpleUIService : ISimpleUIService
    {
        #region ISimpleUIService Members

        /// <summary>
        /// Shows an error box.
        /// </summary>
        /// <param name="message">The error message.</param>
        public void ShowErrorBox(string message)
        {
            ShowErrorBox(null, message);
        }

        /// <summary>
        /// Shows an error box.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void ShowErrorBox(Exception exception)
        {
            ShowErrorBox(exception, string.Empty);
        }

        /// <summary>
        /// Shows an error box.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The error message.</param>
        public void ShowErrorBox(Exception exception, string message)
        {
            var text = string.Empty;
            if (exception == null)
            {
                if (string.IsNullOrEmpty(message)) text = SR.Error;
                else text = message;
            }
            else
            {
                if (string.IsNullOrEmpty(message)) text = exception.ToFormattedString();
                else text = string.Format("{0}\r\n\r\n{1}", message, exception.ToFormattedString());
            }

            ErrorBox.Show(Owner, text);
        }

        /// <summary>
        /// Shows a warning box.
        /// </summary>
        /// <param name="message">The warning message.</param>
        public void ShowWarningBox(string message)
        {
            WarningBox.Show(Owner, message);
        }

        /// <summary>
        /// Shows an information box.
        /// </summary>
        /// <param name="message">The information message.</param>
        public void ShowInformationBox(string message)
        {
            InformationBox.Show(Owner, message);
        }

        /// <summary>
        /// Shows a question box.
        /// </summary>
        /// <param name="message">The question message.</param>
        /// <returns>The message box result.</returns>
        public DialogResult ShowQuestionBox(string message)
        {
            return QuestionBox.Show(Owner, message);
        }

        /// <summary>
        /// Shows a message box.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The message box result.</returns>
        public DialogResult ShowMessageBox(string message)
        {
            return ShowMessageBox(message, SR.Message, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0);
        }

        /// <summary>
        /// Shows a message box.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The message box caption.</param>
        /// <returns>The message box result.</returns>
        public DialogResult ShowMessageBox(string message, string caption)
        {
            return ShowMessageBox(message, caption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0);
        }

        /// <summary>
        /// Shows a message box.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="buttons">The buttons to display on the message box.</param>
        /// <returns>The message box result.</returns>
        public DialogResult ShowMessageBox(string message, string caption, MessageBoxButtons buttons)
        {
            return ShowMessageBox(message, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0);
        }

        /// <summary>
        /// Shows a message box.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="buttons">The buttons to display on the message box.</param>
        /// <param name="icon">The icon to display on the message box.</param>
        /// <returns>The message box result.</returns>
        public DialogResult ShowMessageBox(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return ShowMessageBox(message, caption, buttons, icon, MessageBoxDefaultButton.Button1, 0);
        }

        /// <summary>
        /// Shows a message box.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The message box caption.</param>
        /// <param name="buttons">The buttons to display on the message box.</param>
        /// <param name="icon">The icon to display on the message box.</param>
        /// <param name="defaultButton">The default button that will be clicked if Enter is pressed.</param>
        /// <returns>The message box result.</returns>
        public DialogResult ShowMessageBox(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            return MessageBox.Show(Owner, message, caption, buttons, icon, defaultButton, 0);
        }

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
        public DialogResult ShowMessageBox(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
        {
            return MessageBox.Show(Owner, message, caption, buttons, icon, defaultButton, options);
        }
        
        /// <summary>
        /// Shows an input box.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The string that was typed by the user.</returns>
        public string ShowInputBox(string message)
        {
            string title = GetTitleFromAssembly(Assembly.GetCallingAssembly());
            return ShowInputBox(message, title, string.Empty);
        }

        /// <summary>
        /// Shows the input box.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The input box caption.</param>
        /// <returns>The string that was typed by the user.</returns>
        public string ShowInputBox(string message, string caption)
        {
            return ShowInputBox(message, caption, string.Empty);
        }

        /// <summary>
        /// Shows the input box.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="caption">The input box caption.</param>
        /// <param name="defaultValue">The default value if the user types nothing.</param>
        /// <returns>The string that was typed by the user.</returns>
        public string ShowInputBox(string message, string caption, string defaultValue)
        {
            if (string.IsNullOrEmpty(message)) message = SR.NoMessage;
            return InputBox.Show(Owner, message, caption, defaultValue);
        }

        /// <summary>
        /// Shows a common dialog.
        /// </summary>
        /// <param name="commonDialog">The common dialog to show.</param>
        /// <returns>The common dialog result.</returns>
        public DialogResult ShowDialog(CommonDialog commonDialog)
        {
            if (commonDialog == null) throw new ArgumentNullException("commonDialog");
            return commonDialog.ShowDialog(Owner);
        }

        /// <summary>
        /// Gets the current parent window.
        /// </summary>
        /// <value></value>
        public IWin32Window Owner
        {
            get { return NativeMethods.ActiveWindow; }
        }

        #endregion

        private static string GetTitleFromAssembly(Assembly assembly)
        {
            try { return assembly.GetName().Name; }
            catch (SecurityException)
            {
                string fullName = assembly.FullName;
                int index = fullName.IndexOf(',');
                if (index >= 0) return fullName.Substring(0, index);
                return string.Empty;
            }
        }
    }
}
