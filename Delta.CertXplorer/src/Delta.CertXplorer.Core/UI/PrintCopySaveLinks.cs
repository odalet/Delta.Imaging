using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Delta.CertXplorer.UI
{
    /// <summary>
    /// Displays a series of three links allowing to copy, print and save some data.
    /// </summary>
    [Designer(typeof(Delta.CertXplorer.UI.Design.PrintCopySaveLinksDesigner))]
    public partial class PrintCopySaveLinks : UserControl
    {
        private const int HEIGHT = 16;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrintCopySaveLinks"/> class.
        /// </summary>
        public PrintCopySaveLinks()
        {
            InitializeComponent();

            base.BackColor = Color.Transparent;

            llCopy.Click += delegate { OnCopyClick(); };
            llPrint.Click += delegate { OnPrintClick(); };
            llSave.Click += delegate { OnSaveClick(); };
        }

        /// <summary>
        /// Occurs when the copy link is clicked.
        /// </summary>
        public event EventHandler CopyClick;
        
        /// <summary>
        /// Occurs when the print link is clicked.
        /// </summary>
        public event EventHandler PrintClick;

        /// <summary>
        /// Occurs when the save link is clicked.
        /// </summary>
        public event EventHandler SaveClick;

        /// <summary>
        /// Gets or sets a value indicating whether [show save link].
        /// </summary>
        /// <value><c>true</c> if [show save link]; otherwise, <c>false</c>.</value>
        public bool ShowSaveLink
        {
            get { return llSave.Visible; }
            set { llSave.Visible = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show print link].
        /// </summary>
        /// <value><c>true</c> if [show print link]; otherwise, <c>false</c>.</value>
        public bool ShowPrintLink
        {
            get { return llPrint.Visible; }
            set { llPrint.Visible = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show copy link].
        /// </summary>
        /// <value><c>true</c> if [show copy link]; otherwise, <c>false</c>.</value>
        public bool ShowCopyLink
        {
            get { return llCopy.Visible; }
            set { llCopy.Visible = value; }
        }

        /// <summary>
        /// Saves the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public bool SaveText(string text) { return SaveText(text, "document.txt"); }

        /// <summary>
        /// Saves the specified text to the specified file.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        public bool SaveText(string text, string filename) 
        { 
            return SaveText(text, filename, SR.TextFilesFilter); 
        }

        /// <summary>
        /// Saves the specified text to the specified file but displays a 
        /// Save File dialog so that the user can override the output file name.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public bool SaveText(string text, string filename, string filter)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = filename;
            sfd.Filter = filter;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Cursor cursor = Cursor;
                Cursor = Cursors.WaitCursor;
                try
                {
                    using (StreamWriter sw = File.CreateText(sfd.FileName))
                    {
                        sw.Write(UpdateText(text));
                        sw.Flush();
                        sw.Close();
                    }
                }
                catch (Exception ex)
                {
                    ErrorBox.Show(base.ParentForm, string.Format(
                        SR.ErrorCantSaveFile, filename, ex.Message));
                    return false;
                }
                finally { Cursor = cursor; }

                return true;
            }
            return false;
        }

        /// <summary>
        /// Called when [copy click].
        /// </summary>
        protected virtual void OnCopyClick()
        {
            if (CopyClick != null) CopyClick(this, EventArgs.Empty);
        }

        /// <summary>
        /// Called when [print click].
        /// </summary>
        protected virtual void OnPrintClick()
        {
            if (PrintClick != null) PrintClick(this, EventArgs.Empty);
        }

        /// <summary>
        /// Called when [save click].
        /// </summary>
        protected virtual void OnSaveClick()
        {
            if (SaveClick != null) SaveClick(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            base.Height = HEIGHT;
        }

        /// <summary>
        /// Updates the text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        private string UpdateText(string text)
        {
            text = text.Replace("\r\n", "\r");
            text = text.Replace("\n", "\r");
            text = text.Replace("\r", "\r\n");
            return text;
        }
    }
}
