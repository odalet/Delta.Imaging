using System;
using System.Windows.Forms;
using System.ComponentModel;

using Delta.CertXplorer.UI.Theming;
using Delta.CertXplorer.DocumentModel;

namespace Delta.CertXplorer.Asn1Decoder
{
    internal partial class Asn1DocumentControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Asn1DocumentControl"/> class.
        /// </summary>
        public Asn1DocumentControl()
        {
            InitializeComponent();

            ThemesManager.RegisterThemeAwareControl(this, (renderer) =>
            {
                    if (renderer is ToolStripProfessionalRenderer)
                        ((ToolStripProfessionalRenderer)renderer).RoundedEdges = false;
                    tstrip.Renderer = renderer;
            });

            tstrip.SetRoundedEdges(false);
            parseOctetStringsToolStripButton.Checked = viewer.ParseOctetStrings;
            showInvalidTaggedObjectsToolStripButton.Checked = viewer.ShowInvalidTaggedObjects;
        }

        public void SetData(byte[] bytes)
        {
            var data = bytes;

            // ASN.1 viewer doesn't support (yet) null data!
            if (data == null) data = new byte[0];
            viewer.Initialize(data);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.UserControl.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            parseOctetStringsToolStripButton.Checked = false;
            refreshToolStripButton.Click += (s, _) => viewer.ParseData();
            parseOctetStringsToolStripButton.CheckedChanged += (s, _) =>
            {
                viewer.ParseOctetStrings = parseOctetStringsToolStripButton.Checked;
                viewer.ParseData();
            };

            showInvalidTaggedObjectsToolStripButton.CheckedChanged += (s, _) =>
            {
                viewer.ShowInvalidTaggedObjects = showInvalidTaggedObjectsToolStripButton.Checked;
                viewer.ParseData();
            };
        }
    }
}
