using System;
using SD = System.Drawing;
using SDI = System.Drawing.Imaging;
using SWMI = System.Windows.Media.Imaging;

namespace Delta.Wsq
{
    public class WsqEncoder
    {
        public string Comment { get; set; }

        public byte[] EncodeQuality(RawImageData image, int quality)
        {
            return EncodeCompressionRatio(image, WsqCodec.QualityToCompressionRatio(quality));
        }

        public byte[] EncodeCompressionRatio(RawImageData image, float compressionRatio)
        {
            return Encode(image, WsqCodec.CompressionRatioToBitrate(compressionRatio));
        }

        public byte[] Encode(RawImageData image, float bitrate = WsqCodec.Constants.DefaultBitrate)
        {
            return WsqCodec.Encode(image, bitrate, Comment);
        }

        #region GDI+

        public byte[] EncodeQualityGdi(SD.Bitmap image, int quality, bool autoConvertToGrayscale = true)
        {
            return EncodeCompressionRatioGdi(image, WsqCodec.QualityToCompressionRatio(quality), autoConvertToGrayscale);
        }

        public byte[] EncodeCompressionRatioGdi(SD.Bitmap image, float compressionRatio, bool autoConvertToGrayscale = true)
        {
            return EncodeGdi(image, WsqCodec.CompressionRatioToBitrate(compressionRatio), autoConvertToGrayscale);
        }

        public byte[] EncodeGdi(SD.Bitmap image, float bitrate = WsqCodec.Constants.DefaultBitrate, bool autoConvertToGrayscale = true)
        {
            if (image == null) throw new ArgumentNullException("image");

            RawImageData data = null;
            if (autoConvertToGrayscale)
            {
                using (var source = Conversions.To8bppBitmap(image))
                    data = Conversions.GdiImageToImageInfo(source);
            }
            else data = Conversions.GdiImageToImageInfo(image);

            return WsqCodec.Encode(data, bitrate, Comment);
        }

        #endregion

        #region WPF

        public byte[] EncodeQualityWpf(SWMI.BitmapSource image, int quality, bool autoConvertToGrayscale = true)
        {
            return EncodeCompressionRatioWpf(image, WsqCodec.QualityToCompressionRatio(quality), autoConvertToGrayscale);
        }

        public byte[] EncodeCompressionRatioWpf(SWMI.BitmapSource image, float compressionRatio, bool autoConvertToGrayscale = true)
        {
            return EncodeWpf(image, WsqCodec.CompressionRatioToBitrate(compressionRatio), autoConvertToGrayscale);
        }

        public byte[] EncodeWpf(SWMI.BitmapSource image, float bitrate = WsqCodec.Constants.DefaultBitrate, bool autoConvertToGrayscale = true)
        {
            if (image == null) throw new ArgumentNullException("image");

            SWMI.BitmapSource source = null;
            if (autoConvertToGrayscale)
                source = Conversions.ToGray8BitmapSource(image);
            else source = image;
            
            var data = Conversions.WpfImageToImageInfo(source);
            return WsqCodec.Encode(data, bitrate, Comment);
        }

        #endregion
    }
}
