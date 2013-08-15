using System;
using WeifenLuo.WinFormsUI.Docking;
using Delta.CertXplorer.DocumentModel;

namespace Delta.CertXplorer.Asn1Decoder
{
    public partial class DocumentWindow : DockContent, IDocumentView
    {
        private BaseDocument document = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentWindow"/> class.
        /// </summary>
        public DocumentWindow()
        {
            InitializeComponent();
        }

        #region IDocumentView Members

        /// <summary>
        /// Occurs when this view is closed.
        /// </summary>
        public event EventHandler ViewClosed;

        /// <summary>
        /// Gets the document diplayed by this view.
        /// </summary>
        /// <value>The document.</value>
        public Document Document
        {
            get { return document; }
            internal set
            {
                if (value == null)  throw new ArgumentNullException("value");
                if (!(value is BaseDocument)) throw new NotSupportedException(string.Format(
                    "Documents of type {0} are not supported in this view.", document.GetType()));

                document = (BaseDocument)value;
                asn1Viewer.Document = document;
                base.Text = document.DocumentCaption;
            }
        }

        #endregion

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Closed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (ViewClosed != null) ViewClosed(this, EventArgs.Empty);
        }
    }
}
