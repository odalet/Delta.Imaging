using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Delta.Wsq
{
    public static partial class Conversions
    {
        public static unsafe Bitmap ImageInfoToGdiImage(RawImageData info)
        {
            if (info == null || info.IsEmpty) throw new ArgumentException("info");

            // 16bpp grayscale is not really supported by the underlying codec.
            if (info.PixelDepth != 8) throw new NotSupportedException(
                "Source image must have a pixel depth of exactly 8 bpp.");

            var pformat = info.PixelDepth == 8 ?
                PixelFormat.Format8bppIndexed : PixelFormat.Format16bppGrayScale;

            var mul = info.PixelDepth == 8 ? 1 : 2;
            var w = info.Width;
            var h = info.Height;

            var image = new Bitmap(w, h, pformat);

            var bmpData = image.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, pformat);
            try
            {
                fixed (byte* infoData = info.Data)
                {
                    var source = infoData;
                    var dest = (byte*)bmpData.Scan0;
                    for (int i = 0; i < h; i++)
                    {
                        NativeMethods.CopyMemory(dest, source, w * mul);
                        dest += bmpData.Stride;
                        source += w * mul;
                    }
                }

            }
            finally
            {
                image.UnlockBits(bmpData);
            }

            if (info.PixelDepth == 8)
            {
                // For some crazy reason, we have to write the palette in this roundabout fashion.
                var palette = image.Palette;
                for (int i = 0; i < 256; i++)
                    palette.Entries[i] = Color.FromArgb(i, i, i);
                image.Palette = palette;
            }

            SafeSetResolution(image, info.Resolution);
            return image;
        }

        public static unsafe RawImageData GdiImageToImageInfo(Bitmap image)
        {
            if (image == null) throw new ArgumentException("image");

            // 16bpp grayscale is not really supported by the underlying codec.
            if (/* image.PixelFormat != PixelFormat.Format16bppGrayScale && */ image.PixelFormat != PixelFormat.Format8bppIndexed)
                throw new ArgumentException("Unsupported image format.");

            var info = new RawImageData()
            {
                Resolution = (int)image.HorizontalResolution,
                Width = image.Width,
                Height = image.Height,
                PixelDepth = image.PixelFormat == PixelFormat.Format8bppIndexed ? 8 : 16
            };

            var mul = info.PixelDepth == 8 ? 1 : 2;
            var w = info.Width;
            var h = info.Height;

            info.Data = new byte[w * h * mul];
            var bmpData = image.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, image.PixelFormat);
            try
            {
                fixed (byte* infoData = info.Data)
                {
                    var dest = infoData;
                    var source = (byte*)bmpData.Scan0;
                    for (int i = 0; i < h; i++)
                    {
                        NativeMethods.CopyMemory(dest, source, w * mul);
                        dest += w * mul;
                        source += bmpData.Stride;
                    }
                }
            }
            finally
            {
                image.UnlockBits(bmpData);
            }

            return info;
        }

        public unsafe static Bitmap To8bppBitmap(Bitmap input)
        {
            if (input == null) throw new ArgumentNullException("input");
            if (input.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                var clone = (Bitmap)input.Clone();
                clone.SetResolution(input.HorizontalResolution, input.VerticalResolution);
                return clone;
            }

            var result = new Bitmap(input.Width, input.Height, PixelFormat.Format8bppIndexed);

            // Temporary image is 24bpp
            using (var bitmap = new Bitmap(input.Width, input.Height, PixelFormat.Format24bppRgb))
            {
                using (var g = Graphics.FromImage(bitmap))
                    g.DrawImage(input, 0, 0, input.Width, input.Height);

                var w = bitmap.Width;
                var h = bitmap.Height;
                var bitmapData = bitmap.LockBits(new Rectangle(0, 0, w, h),
                    ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                var resultData = result.LockBits(new Rectangle(0, 0, w, h),
                    ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

                try
                {
                    var inputStride = bitmapData.Stride;
                    var inputScan0 = (int)bitmapData.Scan0;
                    var inputGarbage = inputStride - w * 3;

                    var outputStride = resultData.Stride;
                    var outputScan0 = (int)resultData.Scan0;
                    var outputGarbage = outputStride - w;

                    for (var i = 0; i < w * h; i++)
                    {
                        var iy = inputStride > 0 ? i / w : h - i / w;
                        var oy = outputStride > 0 ? i / w : h - i / w;
                        var pixptr = (byte*)(inputScan0 + i * 3 + iy * inputGarbage);
                        var resptr = (byte*)(outputScan0 + i + oy * outputGarbage);
                        *resptr = (byte)((pixptr[0] + pixptr[1] + pixptr[2]) / 3);
                    }
                }
                finally
                {
                    result.UnlockBits(resultData);
                    bitmap.UnlockBits(bitmapData);
                }
            }

            result.SetResolution(input.HorizontalResolution, input.VerticalResolution);

            return result;
        }

        private static void SafeSetResolution(Bitmap image, int resolution)
        {
            const int defaultResolution = 96;
            if (resolution <= 0) // Invalid (or not set) 
                image.SetResolution((float)defaultResolution, (float)defaultResolution);
            else
            {
                try
                {
                    image.SetResolution((float)resolution, (float)resolution);
                }
                catch (Exception ex)
                {
                    var debugException = ex;
                    image.SetResolution((float)defaultResolution, (float)defaultResolution);
                }
            }
        }
    }
}
