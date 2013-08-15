using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace TestExif.Drawing.Imaging
{
    public class ExifInfo
    {
        private Bitmap image = null;
        private List<ExifProperty> properties = new List<ExifProperty>();

        public ExifInfo(Bitmap sourceImage)
        {
            if (image == null) throw new ArgumentNullException("sourceImage");
            image = sourceImage;

            ExtractMetada(image.PropertyItems);
        }

        public IList<ExifProperty> Properties
        {
            get { return properties; }
        }

        private void ExtractMetada(PropertyItem[] items)
        {
            properties.Clear();
            foreach (var item in items)
                properties.Add(CreateProperty(item));
        }

        private ExifProperty CreateProperty(PropertyItem item)
        {
            return new ExifProperty(item);
        }
    }
}
