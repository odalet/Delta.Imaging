using System;
using System.Collections.Generic;

namespace TestExif.Drawing.Imaging
{
    internal static class Exif
    {
        private static Dictionary<ExifTagId, string> labels = new Dictionary<ExifTagId, string>();

        static Exif()
        {
            labels.Add(ExifTagId.ExifIfd, ExifSR.ExifIfd);
            labels.Add(ExifTagId.GpsIfd, ExifSR.GpsIfd);
        }

        public static IDictionary<ExifTagId, string> Labels
        {
            get { return labels; }
        }
    }
}
