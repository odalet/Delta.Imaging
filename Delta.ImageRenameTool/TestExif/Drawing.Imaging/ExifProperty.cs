using System;
using System.Drawing.Imaging;

namespace TestExif.Drawing.Imaging
{
    public class ExifProperty
    {
        private PropertyItem item = null;
        private ExifTag tag = null;
        private string stringValue = string.Empty;
        private object value = null;
        private Type valueType = null;

        public ExifProperty(PropertyItem propertyItem)
        {
            if (propertyItem == null) throw new ArgumentNullException("propertyItem");
            item = propertyItem;

            CreateTag();
            CreateValue();
        }

        public PropertyItem Item { get { return item; } }

        public ExifTag Tag { get { return tag; } }
        
        public ExifPropertyType ExifType
        {
            get
            {
                return Enum.IsDefined(typeof(ExifPropertyType), item.Type) ?
                    (ExifPropertyType)item.Type : ExifPropertyType.Unknown;
            }
        }

        public string StringValue { get { return stringValue; } }

        public object Value { get { return value; } }
        public Type ValueType { get { return valueType; } }

        private void CreateTag()
        {
            tag = ExifTag.FromId(item.Id);
        }

        private void CreateValue()
        {
            if (tag.IsUnknown) return;

            switch (ExifType)
            {
                case ExifPropertyType.Unknown:
                    break;
            }
        }
    }
}
