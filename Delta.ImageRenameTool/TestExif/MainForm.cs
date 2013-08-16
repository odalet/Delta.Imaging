using System;
using System.Drawing;
using System.Windows.Forms;
using Goheer.EXIF;

namespace TestExif
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private Bitmap image = null;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            image = new Bitmap("IMGP0157.JPG");

            pb.Image = image;
            pg.SelectedObject = image;

            Goheer.EXIF.EXIFextractor er =
             new Goheer.EXIF.EXIFextractor(ref image, "\r\n");

            rtb.Text = string.Empty;
            foreach (Pair pair in er)
                rtb.AppendText(pair.First + ": " + pair.Second + "\r\n");
        }
    }
}
