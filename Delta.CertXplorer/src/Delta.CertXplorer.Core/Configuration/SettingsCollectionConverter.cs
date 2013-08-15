using System;
using System.Linq;
using System.Xml.Linq;
using System.Reflection;
using System.Collections;
using System.Globalization;
using System.ComponentModel;

namespace Delta.CertXplorer.Configuration
{
    public class SettingsCollectionConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string)) return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string)) return true;
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (!(value is string))
                return base.ConvertFrom(context, culture, value);

            XDocument xdoc = XDocument.Parse("<settings>" + (string)value + "</settings>");
            Type targetType = (xdoc.Root.Element("itemType") != null) ?
                Type.GetType(xdoc.Root.Element("itemType").Value) :
                typeof(string);
            var items = xdoc.Root.Elements("item")
                .Select(v => v.Value.ConvertToType(targetType));
            Type collectionType = typeof(SettingsCollection<>)
                .MakeGenericType(new Type[] { targetType });
            return Activator.CreateInstance(collectionType,
                BindingFlags.CreateInstance | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null, new object[] { items }, null);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (!(destinationType == typeof(string)))
                return base.ConvertTo(context, culture, value, destinationType);

            var daValue = ((IEnumerable)value).Cast<object>();
            return "<itemType>" + daValue.First().GetType().ToString() + "</itemType>" +
                string.Join("", daValue.Select(v => "<item>" + v.ToString() + "</item>").ToArray());
        }
    }
}
