using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using Delta.CertXplorer.UI;
using Delta.CertXplorer.Utility;

namespace Delta.CertXplorer.Diagnostics
{
    /// <summary>
    /// Displays a dialog box giving details about an exception.
    /// </summary>
    public partial class ExceptionBox : BaseAcceptForm
    {             
        private object exceptionObject = null;
        private readonly Version osVersion = Environment.OSVersion.Version;
        private string userProvidedText = string.Empty;
        internal ExceptionBox(object exObject, string text, bool canExitApplication)
        {
            try
            {
                InitializeComponent();

                base.ShowIcon = true;
                base.Icon = SystemIcons.Error;

                CanExitApplication = canExitApplication;

                if (!IsXpOrGreater)
                {
                    tbError.ScrollBars = ScrollBars.Vertical;
                    tbError.Dock = DockStyle.Fill;
                }

                exceptionObject = exObject;
                userProvidedText = text;

                Initialize();
            }
            catch (Exception ex)
            {
                // eat the exception
                var debugEx = ex;
            }
        }

        #region Public static interface

        /// <summary>
        /// Shows an Exception dialog box.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>Either <see cref="ExceptionBoxResult.Close"/> or <see cref="ExceptionBoxResult.Exit"/>.</returns>
        public static ExceptionBoxResult Show(object exception)
        {
            return Show(exception, string.Empty);
        }

        /// <summary>
        /// Shows an Exception dialog box.
        /// </summary>
        /// <param name="owner">Dialog box top-level window and owner.</param>
        /// <param name="exception">The exception.</param>
        /// <returns>Either <see cref="ExceptionBoxResult.Close"/> or <see cref="ExceptionBoxResult.Exit"/>.</returns>
        public static ExceptionBoxResult Show(IWin32Window owner, object exception)
        {
            return Show(owner, exception, string.Empty);
        }

        /// <summary>
        /// Shows an Exception dialog box.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <returns>Either <see cref="ExceptionBoxResult.Close"/> or <see cref="ExceptionBoxResult.Exit"/>.</returns>
        public static ExceptionBoxResult Show(object exception, string text)
        {
            return Show(null, exception, text, false);
        }

        /// <summary>
        /// Shows an Exception dialog box.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <returns>Either <see cref="ExceptionBoxResult.Close"/> or <see cref="ExceptionBoxResult.Exit"/>.</returns>
        public static ExceptionBoxResult Show(IWin32Window owner, object exception, string text)
        {
            return Show(owner, exception, text, false);
        }

        ///// <summary>
        ///// Shows an Exception box.
        ///// </summary>
        ///// <param name="owner">The owner.</param>
        ///// <param name="ex">The exception.</param>
        ///// <returns>Always <see cref="ExceptionBoxResult.Close"/>.</returns>
        //public static ExceptionBoxResult Show(IWin32Window owner, Exception exception)
        //{
        //    return Show(owner, exception, false);
        //}

        /// <summary>
        /// Shows the specified owner.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="text">The text to display in the message box.</param>
        /// <param name="canExitApplication">if set to <c>true</c> then the button allowing to exit the application is visible.</param>
        /// <returns>Either <see cref="ExceptionBoxResult.Close"/> or <see cref="ExceptionBoxResult.Exit"/>.</returns>
        public static ExceptionBoxResult Show(
            IWin32Window owner, object exception, string text, bool canExitApplication)
        {
            try
            {
                var box = new ExceptionBox(exception, text, canExitApplication);
                if (owner == null) box.StartPosition = FormStartPosition.CenterScreen;
                box.ShowDialog(owner);
                return box.Result;
            }
            catch (Exception ex)
            {
                // eat the exception
                var debugEx = ex;
            }

            return ExceptionBoxResult.Close;
        }

        #endregion

        private ExceptionBoxResult Result { get; set; }

        private bool CanExitApplication { get; set; }

        /// <summary>
        /// Occurs when the control resizes itself.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            try
            {
                base.OnResize(e);            
                if (IsXpOrGreater && (tbError != null)) // Because of Windows 2000
                    tbError.ScrollBars = IsFullTextVisible ? ScrollBars.None : ScrollBars.Vertical;
            }
            catch (Exception ex)
            {
                // eat the exception
                var debugEx = ex;
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Closed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            if ((DialogResult == DialogResult.OK) && CanExitApplication)
                Result = ExceptionBoxResult.Exit;
            else Result = ExceptionBoxResult.Close;           
        }

        /// <summary>
        /// Gets a value indicating whether the OS is xp or greater.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if the OS is xp or greater; otherwise, <c>false</c>.
        /// </value>
        private bool IsXpOrGreater
        {
            get { return (osVersion.Major >= 5) && (osVersion.Minor >= 1); }
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            base.AcceptButtonVisible = CanExitApplication;
            if (CanExitApplication)
            {
                base.AcceptButtonText = SR.ExitButton;
                base.AcceptButton.DialogResult = DialogResult.OK;
                toolTip.SetToolTip((Control)base.AcceptButton, SR.ExitApplication);
            }

            base.CancelButtonText = SR.CloseButton;
            base.CancelButton.DialogResult = DialogResult.Cancel;
            toolTip.SetToolTip((Control)base.CancelButton, SR.CloseBoxAndReturnToApplication);

            if (exceptionObject is Exception)
            {
                var exception = (Exception)exceptionObject;
                tbError.Text = BuildText(exception.Message, userProvidedText);
            }
            else
            {
                string exceptionMessage = string.Empty;
                if (exceptionObject == null) exceptionMessage = "?";
                else exceptionMessage = exceptionObject.ToString();
                tbError.Text = BuildText(exceptionMessage, userProvidedText);
            }

            rtb.Text = exceptionObject.GetFullDiagnosticsInformation();
        }

        private static string BuildText(string message, string text)
        {
            if (string.IsNullOrEmpty(message))
                return string.IsNullOrEmpty(text) ? string.Empty : text;
            if (string.IsNullOrEmpty(text)) return message;
            return string.Format("{0}\r\n\r\n{1}", text, message);
        }

        private bool IsFullTextVisible
        {
            get
            {
                Size size = Size.Empty;
                int lcount = 0;
                string stringToMeasure = string.Empty;

                if ((tbError == null) || (tbError.Lines == null)) return false; // Windows 2000 !

                foreach (string line in tbError.Lines)
                {
                    if (line.Length > 0)
                    {
                        int index = tbError.GetFirstCharIndexFromLine(lcount) + line.Length - 1;
                        Point pt = tbError.GetPositionFromCharIndex(index);
                        if (pt.X > size.Width) size.Width = pt.X;
                        if (pt.Y > size.Height) size.Height = pt.Y;

                        if (!string.IsNullOrEmpty(line)) stringToMeasure = line;
                    }
                    lcount++;
                }

                // We had the height of one text line
                if (!string.IsNullOrEmpty(stringToMeasure))
                {
                    using (Graphics g = tbError.CreateGraphics())
                    {
                        SizeF sf = g.MeasureString(stringToMeasure, tbError.Font);
                        size.Height += (int)Math.Ceiling((double)sf.Height);
                    }
                }

                return (tbError.Width >= size.Width) && (tbError.Height >= size.Height);
            }
        }

        private void Copy()
        {
            try { SmartClipboard.SetText(rtb.Text); }
            catch (Exception ex)
            {
                // eat the exception
                var debugEx = ex;
            }
        }

        private void Print()
        {
            try { rtb.Print(true); }
            catch (Exception ex)
            {
                // eat the exception
                var debugEx = ex;
            }
        }

        private void Save()
        {
            try { pcsLinks.SaveText(rtb.Text); }
            catch (Exception ex)
            {
                // eat the exception
                var debugEx = ex;
            }
        }

        private void SelectAll()
        {
            try { rtb.SelectAll(); }
            catch (Exception ex)
            {
                // eat the exception
                var debugEx = ex;
            }
        }

        private void pcsLinks_CopyClick(object sender, EventArgs e) { Copy(); }
        private void pcsLinks_PrintClick(object sender, EventArgs e) { Print(); }
        private void pcsLinks_SaveClick(object sender, EventArgs e) { Save(); }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) { Save(); }
        private void printToolStripMenuItem_Click(object sender, EventArgs e) { Print(); }
        private void copyToolStripMenuItem_Click(object sender, EventArgs e) { Copy(); }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e) { SelectAll();  }
    }
}
