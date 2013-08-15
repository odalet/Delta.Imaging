namespace CryptoHelperPlugin.UI
{
    partial class OperationSelector
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sha1RadioButton = new System.Windows.Forms.RadioButton();
            this.convertRadioButton = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sha1RadioButton
            // 
            this.sha1RadioButton.AutoSize = true;
            this.sha1RadioButton.Location = new System.Drawing.Point(3, 26);
            this.sha1RadioButton.Name = "sha1RadioButton";
            this.sha1RadioButton.Size = new System.Drawing.Size(132, 17);
            this.sha1RadioButton.TabIndex = 0;
            this.sha1RadioButton.TabStop = true;
            this.sha1RadioButton.Text = "Compute Hash (SHA1)";
            this.sha1RadioButton.UseVisualStyleBackColor = true;
            // 
            // convertRadioButton
            // 
            this.convertRadioButton.AutoSize = true;
            this.convertRadioButton.Location = new System.Drawing.Point(3, 3);
            this.convertRadioButton.Name = "convertRadioButton";
            this.convertRadioButton.Size = new System.Drawing.Size(62, 17);
            this.convertRadioButton.TabIndex = 0;
            this.convertRadioButton.TabStop = true;
            this.convertRadioButton.Text = "Convert";
            this.convertRadioButton.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.convertRadioButton);
            this.flowLayoutPanel1.Controls.Add(this.sha1RadioButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(195, 54);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // OperationSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "OperationSelector";
            this.Size = new System.Drawing.Size(195, 54);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton sha1RadioButton;
        private System.Windows.Forms.RadioButton convertRadioButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}
