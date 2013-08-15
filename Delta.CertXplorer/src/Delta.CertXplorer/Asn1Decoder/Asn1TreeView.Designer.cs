namespace Delta.CertXplorer.Asn1Decoder
{
    partial class Asn1TreeView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Asn1TreeView));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Empty.png");
            this.imageList.Images.SetKeyName(1, "Document.png");
            this.imageList.Images.SetKeyName(2, "Asn1Sequence.png");
            this.imageList.Images.SetKeyName(3, "Asn1Set.png");
            this.imageList.Images.SetKeyName(4, "Asn1Oid.png");
            this.imageList.Images.SetKeyName(5, "Numbers.png");
            this.imageList.Images.SetKeyName(6, "Asn1String.png");
            this.imageList.Images.SetKeyName(7, "Calendar.png");
            this.imageList.Images.SetKeyName(8, "QuestionMark.png");
            this.imageList.Images.SetKeyName(9, "OpenedBox.png");
            this.imageList.Images.SetKeyName(10, "CheckedUnchecked.png");
            this.imageList.Images.SetKeyName(11, "Exclamation.png");
            // 
            // Asn1TreeView
            // 
            this.ImageIndex = 0;
            this.ImageList = this.imageList;
            this.LineColor = System.Drawing.Color.Black;
            this.SelectedImageIndex = 0;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList;
    }
}
