using System;
using System.IO;
using System.Windows.Forms;
using Delta.Wsq;

namespace TestApplication
{
    public partial class GdiLoadTestControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GdiLoadTestControl"/> class.
        /// </summary>
        public GdiLoadTestControl()
        {
            InitializeComponent();
            fileBox.Text = @"C:\Temp\ldsdump\DG3_0_0_f0.wsq";
        }

        private static string NormalizeComment(string comment, int tabc)
        {
            if (!comment.Contains("\n") && !comment.Contains("\r"))
                return comment;

            var tab = new string(' ', tabc);
            var c = comment
                .Replace('\n', '\r')
                .Replace("\r\r", "\r\n")
                .Replace("\r", "\r\n")
                .Replace("\r\n", "\r\n" + tab);
            return c;
        }

        private void LogInfo(RawImageData info, string[] comments = null)
        {
            if (info == null)
                logbox.AppendText("NULL\r\n");
            else if (info.IsEmpty)
                logbox.AppendText("Empty\r\n");
            else
            {
                logbox.AppendText(string.Format("Width  = {0}\r\n", info.Width));
                logbox.AppendText(string.Format("Height = {0}\r\n", info.Height));
                logbox.AppendText(string.Format("Depth  = {0}\r\n", info.PixelDepth));
                logbox.AppendText(string.Format("Dpi    = {0}\r\n", info.Resolution));
                if (comments != null)
                {
                    logbox.AppendText("Comments:\r\n");
                    var index = 0;
                    foreach (var c in comments)
                    {
                        logbox.AppendText(string.Format("[{0}]    = {1}\r\n", index, NormalizeComment(c, 9)));
                        index++;
                    }
                }
            }
        }

        private void LoadWsq(string path)
        {
            var dec = new WsqDecoder();
            try
            {
                var bytes = File.ReadAllBytes(path);
                var info = dec.Decode(bytes);
                var bitmap = dec.DecodeGdi(bytes);
                pbox.Image = bitmap;

                var comments = WsqCodec.GetComments(bytes);

                LogInfo(info, comments);
                logbox.AppendText("--------------------------------------------------------\r\n");
                logbox.Select(logbox.Text.Length, 0);
                logbox.ScrollToCaret();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, string.Format("Error: {0}", ex), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(fileBox.Text))
                LoadWsq(fileBox.Text);
            else
            {
                var path = Path.Combine(Helper.GetImagesPath(), "fp.wsq");
                LoadWsq(path);
            }
        }
    }
}
