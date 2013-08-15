using System;
using System.Linq;
using System.ComponentModel;

using Delta.CapiNet.Asn1;

using Delta.CertXplorer.UI;
using Delta.CertXplorer.Services;
using Delta.CertXplorer.CertManager;
using Delta.CertXplorer.Diagnostics;

namespace Delta.CertXplorer.Asn1Decoder
{
    /// <summary>
    /// Main control for the ASN1 Documents
    /// </summary>
    internal partial class Asn1Viewer : ServicedUserControl, ISelectionSource
    {
        private byte[] content = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Asn1Viewer"/> class.
        /// </summary>
        public Asn1Viewer() 
        {
            InitializeComponent();

            ParseOctetStrings = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether octet strings should be parsed.
        /// </summary>
        /// <value><c>true</c> if we should parse octet strings; otherwise, <c>false</c>.</value>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ParseOctetStrings { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether invalid tagged objects should be shown.
        /// </summary>
        /// <value><c>true</c> if we should show invalid tagged objects; otherwise, <c>false</c>.</value>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ShowInvalidTaggedObjects { get; set; }

        #region ISelectionSource Members

        /// <summary>
        /// Occurs when the currently selected object has changed.
        /// </summary>
        public event EventHandler SelectionChanged;

        /// <summary>
        /// Gets the currently selected object.
        /// </summary>
        /// <value>The selected object.</value>
        public object SelectedObject
        {
            get 
            {
                var node = asnTreeView.SelectedNode;
                if (node != null) return node.Tag;

                return null;
            }
        }

        #endregion

        /// <summary>
        /// Initializes the control with the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        public void Initialize(byte[] data)
        {
            try
            {
                content = data;
                hexViewer.Data = content;
            }
            catch (Exception ex)
            {
                HandleException("Unable to retrieve content: {0}.", ex);
                return;
            }

            ParseData();            
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.UserControl.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GlobalSelectionService
                .GetOrCreateSelectionService(Services)
                .AddSource(this);

            // wire events
            asnTreeView.SelectedNodeChanged += (s, ev) =>
            {
                if (SelectedObject != null && SelectionChanged != null)
                    SelectionChanged(this, EventArgs.Empty);

                try 
                {
                    ShowSelection(); 
                }
                catch (Exception ex)
                {
                    HandleException("Unable to show node: {0}.", ex);
                }
            };
        }

        public void ParseData()
        {
            try
            {
                asnTreeView.Nodes.Clear();
                hexViewer.Select(0, 0);
                asciiTextBox.Clear();
                bytesTextBox.Clear();

                var doc = new Asn1Document(content, ParseOctetStrings, ShowInvalidTaggedObjects);
                asnTreeView.CreateDocumentNodes(doc, "Document");
                asnTreeView.ExpandAll();
            }
            catch (Exception ex)
            {
                HandleException("Unable to decode content: {0}.", ex);
            }
        }

        #region Implementation

        private void ShowData(byte[] bytes)
        {
            bytesTextBox.Text = string.Join(" ", bytes.Select(b => b.ToString("X2")).ToArray());
            asciiTextBox.Text = System.Text.Encoding.ASCII.GetString(bytes);
        }

        private void ShowSelection()
        {
            if (asnTreeView.SelectedNode == null || asnTreeView.SelectedNode.Tag == null)
                return;

            var tag = asnTreeView.SelectedNode.Tag;

            byte[] data = null;
            int index = 0;

            if (tag is Asn1Document)
            {
                var asn1Document = (Asn1Document)tag;
                data = asn1Document.Data;
            }
            else if (tag is Asn1Object)
            {
                var asn1Object = (Asn1Object)tag;
                data = asn1Object.Workload;
                index = asn1Object.WorkloadOffset;
                This.Logger.Debug(string.Format("Node {0}: index={1}, length={2}, data={3}", asn1Object, index, data.Length, data.ToDebugString(5)));
            }            

            ShowData(data);
            if (data.Length > 0) hexViewer.Select(index, data.Length);
            else This.Logger.Verbose("Empty node.");

            if (!hexViewer.Focused) hexViewer.Focus();
            hexViewer.Refresh();
        }

        #endregion

        private void HandleException(string format, Exception ex)
        {
            var message = string.Format(format, ex.Message);
            This.Logger.Error(message);
            ExceptionBox.Show(this, ex, message);
        }
    }
}
