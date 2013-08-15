using System;
using System.Globalization;
using System.ComponentModel;

namespace Delta.CertXplorer.CertManager.Wrappers
{
    internal interface IDisplayTypeWrapper
    {
        string DisplayType { get; }
    }

    internal class CustomExpandableObjectConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(
            ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value is IDisplayTypeWrapper)
                    return ((IDisplayTypeWrapper)value).DisplayType;
            }
                
return base.ConvertTo(context, culture, value, destinationType);            
        }
    }
}
