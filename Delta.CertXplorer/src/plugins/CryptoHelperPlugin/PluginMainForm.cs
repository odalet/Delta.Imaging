using System;
using System.Windows.Forms;

namespace CryptoHelperPlugin
{
    internal partial class PluginMainForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginMainForm"/> class.
        /// </summary>
        public PluginMainForm()
        {
            InitializeComponent();

#if DEBUG
            inbox.Text = "YVtfH1hNUlogYmlkb24gcG91ciB0ZXN0ZXIgbCc/Y3JpdHVyZSBkdSBERzEuICBMYSBNUlogZXN0IHZyYWltZW50IHJlbXBsaWUgZGUgdmFsZXVycyBiaWRvbnMu";
            inputFormatSelector.DataFormat = DataFormat.Base64;
#endif
        }

        public Plugin Plugin { get; set; }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            outbox.Text = ConversionEngine.Run(
                inbox.Text,
                inputFormatSelector.DataFormat,
                outputFormatSelector.DataFormat,
                operationSelector.Operation);
        }
    }
}
