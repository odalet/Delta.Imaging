using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace CryptoHelperPlugin.UI
{
    internal partial class OperationSelector : UserControl
    {
        private Operation operation = Operation.Convert;
        private Dictionary<Operation, RadioButton> controlMapping = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataFormatSelector"/> class.
        /// </summary>
        public OperationSelector()
        {
            InitializeComponent();

            controlMapping = new Dictionary<Operation, RadioButton>();
            controlMapping.Add(Operation.Convert, convertRadioButton);
            controlMapping.Add(Operation.Sha1, sha1RadioButton);
        }

        /// <summary>
        /// Gets or sets the data format.
        /// </summary>
        /// <value>The data format.</value>
        public Operation Operation 
        {
            get { return operation; }
            set
            {
                if (operation == value) return;
                operation = value;
                OnOperationChanged();
            }
        }

        public event EventHandler OperationChanged;

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
                rb.Checked = format == Operation;
                rb.CheckedChanged += (s, _) => Operation = format;
            }
        }

        /// <summary>
        /// Called when the selected operation has changed.
        /// </summary>
        protected virtual void OnOperationChanged()
        {
            // Ensure the correct radio button is checked
            var rb = controlMapping[operation];
            if (!rb.Checked) rb.Checked = true;

            if (OperationChanged != null) OperationChanged(this, EventArgs.Empty);
        }
    }
}
