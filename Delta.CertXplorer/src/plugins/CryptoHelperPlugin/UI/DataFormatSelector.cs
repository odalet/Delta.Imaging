using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace CryptoHelperPlugin.UI
{
    internal partial class DataFormatSelector : UserControl
    {
        private DataFormat dataFormat = DataFormat.Base64;
        private Dictionary<DataFormat, RadioButton> controlMapping = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataFormatSelector"/> class.
        /// </summary>
        public DataFormatSelector()
        {
            InitializeComponent();

            controlMapping = new Dictionary<DataFormat, RadioButton>();
            controlMapping.Add(DataFormat.Text, textRadioButton);
            controlMapping.Add(DataFormat.Base64, base64RadioButton);
            controlMapping.Add( DataFormat.UrlEncoded, urlEncodedRadioButton);
            controlMapping.Add(DataFormat.UrlEncodedBase64, urlEncodedBase64RadioButton);
        }

        /// <summary>
        /// Gets or sets the data format.
        /// </summary>
        /// <value>The data format.</value>
        public DataFormat DataFormat 
        {
            get { return dataFormat; }
            set
            {
                if (dataFormat == value) return;
                dataFormat = value;
                OnDataFormatChanged();
            }
        }

        public event EventHandler DataFormatChanged;

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.UserControl.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            foreach (var key in controlMapping.Keys)
            {
                var format = key;
                var rb = controlMapping[format];
                rb.Checked = format == DataFormat;
                rb.CheckedChanged += (s, _) => DataFormat = format;
            }
        }

        /// <summary>
        /// Called when [data format changed].
        /// </summary>
        protected virtual void OnDataFormatChanged()
        {
            // Ensure the correct radio button is checked
            var rb = controlMapping[dataFormat];
            if (!rb.Checked) rb.Checked = true;

            if (DataFormatChanged != null) DataFormatChanged(this, EventArgs.Empty);
        }
    }
}
