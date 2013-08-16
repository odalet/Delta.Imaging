using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace Delta.ImageRenameTool.ComponentModel
{
    internal class SpecialFolderEnumConverter : AlphaSortedEnumConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpecialFolderEnumConverter"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public SpecialFolderEnumConverter(Type type) : base(type) { }

        /// <summary>
        /// Gets a collection of standard values for the data type this validator is designed for.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
        /// <returns>
        /// A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that holds a standard set of valid values, or null if the data type does not support a standard set of values.
        /// </returns>
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            var standardValues = base.GetStandardValues(context);
            var values = new List<object>();
            var count = standardValues.Count;
            var flag = false;

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
