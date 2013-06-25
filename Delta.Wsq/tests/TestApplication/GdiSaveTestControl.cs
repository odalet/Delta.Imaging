using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using Delta.Wsq;

namespace TestApplication
{
    public partial class GdiSaveTestControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GdiSaveTestControl"/> class.
        /// </summary>
        public GdiSaveTestControl()
        {
            InitializeComponent();
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

        private void LogInfo(RawImageData info, string[] comments, TextBox logbox)
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

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.UserControl.Load" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;
            
            var path = Path.Combine(Helper.GetImagesPath(), "fp.png");
            var bitmap = new Bitmap(path);
            inbox.Image = bitmap;

            nudCompressionRatio.Minimum = (decimal)WsqCodec.Constants.MinCompressionRatio;
            nudCompressionRatio.Maximum = (decimal)WsqCodec.Constants.MaxCompressionRatio;

            nudQuality.Minimum = (decimal)WsqCodec.Constants.MinQuality;
            nudQuality.Maximum = (decimal)WsqCodec.Constants.MaxQuality;

            rbBitrate.Checked = true;
            nudBitrate.Value =(decimal) WsqCodec.Constants.DefaultBitrate;
            UpdateNudValues();
            rbQuality.Checked = true;
        }

        private byte[] Encode(WsqEncoder enc, Bitmap image)
        {
            if (rbQuality.Checked)
                return enc.EncodeQualityGdi(image, (int)nudQuality.Value);
            else if (rbCompressionRatio.Checked)
                return enc.EncodeCompressionRatioGdi(image, (int)nudCompressionRatio.Value);
            else if (rbBitrate.Checked)
                return enc.EncodeGdi(image, (float)nudBitrate.Value);
            else throw new Exception("???");
        }

        private void UpdateNudValues()
        {
            if (rbQuality.Checked)
            {
                nudCompressionRatio.Value = (decimal)WsqCodec.QualityToCompressionRatio((int)nudQuality.Value);
                nudBitrate.Value = (decimal)WsqCodec.CompressionRatioToBitrate((float)nudCompressionRatio.Value);
            }
            else if (rbCompressionRatio.Checked)
            {
                nudQuality.Value = (decimal)WsqCodec.CompressionRatioToQuality((float)nudCompressionRatio.Value);
                nudBitrate.Value = (decimal)WsqCodec.CompressionRatioToBitrate((float)nudCompressionRatio.Value);
            }
            else if (rbBitrate.Checked)
            {
                nudCompressionRatio.Value = (decimal)WsqCodec.BitrateToCompressionRatio((float)nudBitrate.Value);
                nudQuality.Value = (decimal)WsqCodec.CompressionRatioToQuality((float)nudCompressionRatio.Value);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var path = Path.Combine(Helper.GetImagesPath(), "saved_fp.wsq");
            var enc = new WsqEncoder();
            var dec = new WsqDecoder();
            try
            {
                //var info = WsqCodec.GdiImageToImageInfo((Bitmap)inbox.Image);
                //var bytes = WsqCodec.Encode(info, 100, "This is David!");

                enc.Comment = "This is David!";
                var bytes = Encode(enc, (Bitmap)inbox.Image);

                File.WriteAllBytes(path, bytes);

                // Try to display the wsq image
                //var outinfo = WsqCodec.Decode(bytes);
                //if (outinfo != null && !outinfo.IsEmpty)
                //{
                //    var wsqbmp = WsqCodec.ImageInfoToGdiImage(outinfo);
                //    outbox.Image = wsqbmp;
                //}

                var outinfo = dec.Decode(bytes);
                var wsqbmp = dec.DecodeGdi(bytes);
                outbox.Image = wsqbmp;

                var comments = WsqCodec.GetComments(bytes);

                outlogbox.AppendText(string.Format("Q            = {0}\r\n", nudQuality.Value));
                outlogbox.AppendText(string.Format("Cr           = {0}\r\n", nudCompressionRatio.Value));
                outlogbox.AppendText(string.Format("Br           = {0}\r\n", nudBitrate.Value));
                outlogbox.AppendText(string.Format("Size (bytes) = {0}\r\n", bytes.Length));
                LogInfo(outinfo, comments, outlogbox);
                outlogbox.AppendText("--------------------------------------------------------\r\n");
                outlogbox.Select(outlogbox.Text.Length, 0);
                outlogbox.ScrollToCaret();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, string.Format("Error: {0}", ex), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool suspendValueChanged = false;

        private void OnNudValueChanged(object sender, EventArgs e)
        {
            if (suspendValueChanged) return;

            suspendValueChanged = true;
            try
            {
                UpdateNudValues();
            }
            finally { suspendValueChanged = false; }
        }
    }
}
