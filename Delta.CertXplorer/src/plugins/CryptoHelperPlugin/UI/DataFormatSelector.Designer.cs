namespace CryptoHelperPlugin.UI
{
    partial class DataFormatSelector
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
            this.base64RadioButton = new System.Windows.Forms.RadioButton();
            this.textRadioButton = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.urlEncodedRadioButton = new System.Windows.Forms.RadioButton();
            this.urlEncodedBase64RadioButton = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // base64RadioButton
            // 
            this.base64RadioButton.AutoSize = true;
            this.base64RadioButton.Location = new System.Drawing.Point(3, 26);
            this.base64RadioButton.Name = "base64RadioButton";
            this.base64RadioButton.Size = new System.Drawing.Size(61, 17);
            this.base64RadioButton.TabIndex = 0;
            this.base64RadioButton.TabStop = true;
            this.base64RadioButton.Text = "Base64";
            this.base64RadioButton.UseVisualStyleBackColor = true;
            // 
            // textRadioButton
            // 
            this.textRadioButton.AutoSize = true;
            this.textRadioButton.Location = new System.Drawing.Point(3, 3);
            this.textRadioButton.Name = "textRadioButton";
            this.textRadioButton.Size = new System.Drawing.Size(46, 17);
            this.textRadioButton.TabIndex = 0;
            this.textRadioButton.TabStop = true;
            this.textRadioButton.Text = "Text";
            this.textRadioButton.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.textRadioButton);
            this.flowLayoutPanel1.Controls.Add(this.base64RadioButton);
            this.flowLayoutPanel1.Controls.Add(this.urlEncodedRadioButton);
            this.flowLayoutPanel1.Controls.Add(this.urlEncodedBase64RadioButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(195, 122);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // urlEncodedRadioButton
            // 
            this.urlEncodedRadioButton.AutoSize = true;
            this.urlEncodedRadioButton.Location = new System.Drawing.Point(3, 49);
            this.urlEncodedRadioButton.Name = "urlEncodedRadioButton";
            this.urlEncodedRadioButton.Size = new System.Drawing.Size(93, 17);
            this.urlEncodedRadioButton.TabIndex = 1;
            this.urlEncodedRadioButton.TabStop = true;
            this.urlEncodedRadioButton.Text = "URL Encoded";
            this.urlEncodedRadioButton.UseVisualStyleBackColor = true;
            // 
            // urlEncodedBase64RadioButton
            // 
            this.urlEncodedBase64RadioButton.AutoSize = true;
            this.urlEncodedBase64RadioButton.Location = new System.Drawing.Point(3, 72);
            this.urlEncodedBase64RadioButton.Name = "urlEncodedBase64RadioButton";
            this.urlEncodedBase64RadioButton.Size = new System.Drawing.Size(153, 17);
            this.urlEncodedBase64RadioButton.TabIndex = 2;
            this.urlEncodedBase64RadioButton.TabStop = true;
            this.urlEncodedBase64RadioButton.Text = "URL Encoded and Base64";
            this.urlEncodedBase64RadioButton.UseVisualStyleBackColor = true;
            // 
            // DataFormatSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "DataFormatSelector";
            this.Size = new System.Drawing.Size(195, 122);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton base64RadioButton;
        private System.Windows.Forms.RadioButton textRadioButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton urlEncodedRadioButton;
        private System.Windows.Forms.RadioButton urlEncodedBase64RadioButton;
    }
}
