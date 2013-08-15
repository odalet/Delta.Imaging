using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace Delta.CertXplorer.ComponentModel
{
    internal class SpecialFolderEnumConverter : AlphaSortedEnumConverter
    {
        public SpecialFolderEnumConverter(Type type) : base(type) { }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            TypeConverter.StandardValuesCollection standardValues = 
                base.GetStandardValues(context);
            var values = new List<object>();
            int count = standardValues.Count;
            bool flag = false;
            for (int i = 0; i < count; i++)
            {
                if ((standardValues[i] is Environment.SpecialFolder) && standardValues[i].Equals(Environment.SpecialFolder.Personal))
                {
                    if (!flag)
                    {
                        flag = true;
                        values.Add(standardValues[i]);                        
                    }
                }
                else values.Add(standardValues[i]);
            }
            return new TypeConverter.StandardValuesCollection(values);
        }
    }
}
