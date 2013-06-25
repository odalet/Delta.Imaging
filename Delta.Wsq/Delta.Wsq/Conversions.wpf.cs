using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Delta.Wsq
{
    partial class Conversions
    {
        //public static unsafe RawImageData WpfImageToImageInfo(BitmapImage image)
        //{
        //    if (image == null) throw new ArgumentException("image");

        //    // 16bpp grayscale is not really supported by the underlying codec.
        //    if (/* image.Format != PixelFormats.Gray16 && */ image.Format != PixelFormats.Gray8)
        //        throw new ArgumentException("Unsupported image format.");

        //    return null;
        //    //if (image.PixelFormat != PixelFormat.Format16bppGrayScale && image.PixelFormat != PixelFormat.Format8bppIndexed)
        //    //    throw new ArgumentException("Unsupported image format.");

        //    //var info = new WsqImageInfo()
        //    //{
        //    //    Resolution = (int)image.HorizontalResolution,
        //    //    Width = image.Width,
        //    //    Height = image.Height,
        //    //    Lossy = true,
        //    //    PixelDepth = image.PixelFormat == PixelFormat.Format8bppIndexed ? 8 : 16
        //    //};

        //    //var mul = info.PixelDepth == 8 ? 1 : 2;
        //    //var w = info.Width;
        //    //var h = info.Height;

        //    //info.Data = new byte[w * h * mul];
        //    //var bmpData = image.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, image.PixelFormat);
        //    //try
        //    //{
        //    //    fixed (byte* infoData = info.Data)
        //    //    {
        //    //        var dest = infoData;
        //    //        var source = (byte*)bmpData.Scan0;
        //    //        for (int i = 0; i < h; i++)
        //    //        {
        //    //            NativeMethods.CopyMemory(dest, source, w * mul);
        //    //            dest += w * mul;
        //    //            source += bmpData.Stride;
        //    //        }
        //    //    }
        //    //}
        //    //finally
        //    //{
        //    //    image.UnlockBits(bmpData);
        //    //}

        //    //return info;
        //}

        public static BitmapSource ImageInfoToWpfImage(RawImageData info)
        {
            const int defaultDpi = 96; // Use this one if resolution not set in the wsq file.

            if (info == null || info.IsEmpty) throw new ArgumentException("info");

            // 16bpp grayscale is not really supported by the underlying codec.
            if (info.PixelDepth != 8) throw new NotSupportedException(
                "Source image must have a pixel depth of exactly 8 bpp.");

            var pformat = PixelFormats.Gray8; // The output pixel format
            var bytesPerPixel = pformat.BitsPerPixel / 8;
            var data = info.Data;
            var w = info.Width;
            var h = info.Height;
            var dpi = info.Resolution > 0 ? info.Resolution : defaultDpi;
            // Compute the stride
            var stride = w * bytesPerPixel;
            var modulo = stride % 4;
            if (modulo != 0) stride += 4 - modulo;

            var pixels = new byte[stride * h * bytesPerPixel]; // The output image data
            // Fill it
            var garbage = stride - w * bytesPerPixel;
            for (var i = 0; i < w * h; i++)
            {
                var y = stride > 0 ? i / w : h - i / w;
                var j = i * bytesPerPixel + y * garbage;
                pixels[j] = data[i];
            }

            return BitmapSource.Create(w, h, (double)dpi, (double)dpi, pformat, null, pixels, stride);
        }

        public static RawImageData WpfImageToImageInfo(BitmapSource image)
        {
            if (image == null) return RawImageData.Empty;
            if (image.Format != PixelFormats.Bgr24 &&
                image.Format != PixelFormats.Bgr32 &&
                image.Format != PixelFormats.Bgra32 &&
                image.Format != PixelFormats.Gray8 &&
                image.Format != PixelFormats.Indexed8)  // We loose the palette here!
                throw new NotSupportedException(string.Format(
                    "Pixel Format {0} is not supported.", image.Format));

            var w = image.PixelWidth;
            var h = image.PixelHeight;
            // Compute the stride
            var bytesPerPixel = image.Format.BitsPerPixel / 8;
            var stride = w * bytesPerPixel;
            var modulo = stride % 4;
            if (modulo != 0) stride += 4 - modulo;

            var imageData = new byte[stride * h];
            image.CopyPixels(imageData, stride, 0);

            // Now build the raw data output buffer
            var rawData = new byte[w * h];
            var garbage = stride - w * bytesPerPixel;

            for (var i = 0; i < w * h; i++)
            {
                var y = stride > 0 ? i / w : h - i / w;
                var j = i * bytesPerPixel + y * garbage;
                if (bytesPerPixel >= 3)
                    rawData[i] = (byte)((imageData[j] + imageData[j + 1] + imageData[j + 2]) / 3);
                else // 1 byte = 1 pixel
                    rawData[i] = imageData[j];
            }

            return new RawImageData()
            {
                Data = rawData,
                Width = w,
                Height = h,
                Resolution = (int)image.DpiX,
                PixelDepth = 8
            };
        }

        public static BitmapSource ToGray8BitmapSource(BitmapSource input)
        {
            if (input == null) throw new ArgumentNullException("input");
            if (input.Format == PixelFormats.Gray8)
                return input; // We are already good!

            if (input.Format != PixelFormats.Bgr24 &&
                input.Format != PixelFormats.Bgr32 &&
                input.Format != PixelFormats.Bgra32 &&
                input.Format != PixelFormats.Indexed8) // We loose the palette here!
                throw new NotSupportedException(string.Format(
                    "Pixel Format {0} is not supported.", input.Format));

            var w = input.PixelWidth;
            var h = input.PixelHeight;

            var inputStride = GetStride(w, input.Format);
            var inputData = new byte[inputStride * h];
            var inputBytesPerPixel = input.Format.BitsPerPixel / 8;
            var igarbage = inputStride - w * inputBytesPerPixel;
            input.CopyPixels(inputData, inputStride, 0);

            var outputStride = GetStride(w, PixelFormats.Gray8);
            var outputData = new byte[outputStride * h];
            var outputBytesPerPixel = 1;
            var ogarbage = outputStride - w * PixelFormats.Gray8.BitsPerPixel / 8;

            for (var i = 0; i < w * h; i++)
            {
                var iy = inputStride > 0 ? i / w : h - i / w;
                var oy = outputStride > 0 ? i / w : h - i / w;
                var ij = i * inputBytesPerPixel + iy * igarbage;
                var oj = i * outputBytesPerPixel + oy * ogarbage;
                if (inputBytesPerPixel >= 3)
                    outputData[oj] = (byte)((inputData[ij] + inputData[ij + 1] + inputData[ij + 2]) / 3);
                else // 1 byte = 1 pixel
                    outputData[oj] = inputData[ij];
            }

            return BitmapSource.Create(w, h, input.DpiX, input.DpiY, PixelFormats.Gray8, null, outputData, outputStride);
        }

        private static int GetStride(int width, PixelFormat pformat)
        {
            return GetStride(width, pformat.BitsPerPixel);
        }

        private static int GetStride(int width, int bpp)
        {
            var bytesPerPixel = bpp / 8;
            var stride = width * bytesPerPixel;
            var modulo = stride % 4;
            if (modulo != 0) stride += 4 - modulo;

            return stride;
        }
    }
}
