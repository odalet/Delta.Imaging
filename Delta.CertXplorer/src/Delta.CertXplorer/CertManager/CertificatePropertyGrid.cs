using System.Windows.Forms;
using System.ComponentModel;

using Delta.CertXplorer.UI;

namespace Delta.CertXplorer.CertManager
{
    internal class CertificatePropertyGrid : PropertyGridEx
    {
        // TODO: remove design-time access to this property in PropertyGridEx
        // Hide this property, so that it is not set by the designer...
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private new ToolStripRenderer Renderer
        {
            get { return base.Renderer; }
            set { base.Renderer = value; }
        }
    }
}
