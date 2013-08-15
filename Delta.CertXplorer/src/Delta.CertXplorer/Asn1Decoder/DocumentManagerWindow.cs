using System;

using Delta.CertXplorer.UI;

using WeifenLuo.WinFormsUI.Docking;

namespace Delta.CertXplorer.Asn1Decoder
{
    public partial class DocumentManagerWindow : ServicedToolWindow
    {
        public DocumentManagerWindow()
        {
            InitializeComponent();

            base.Icon = Properties.Resources.DocumentManager;
            base.TabText = base.Text = SR.DocumentManager;
            base.ToolTipText = SR.DocumentManagerWindow;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            documentManager.Initialize(base.Services);
        }

        public override Guid Guid
        {
            get { return new Guid("{34E7F36C-0078-41fc-A9CD-46ADEE8A2FA8}"); }
        }

        protected override DockState DefaultDockState
        {
            get { return DockState.DockLeft; }
        }
    }
}
