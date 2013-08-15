/* 
 * Grabbed from Marco De Sanctis' Actions
 * see http://blogs.ugidotnet.org/crad/articles/38329.aspx
 * Original namespace: Crad.Windows.Forms.Actions
 * License: Common Public License Version 1.0
 * 
 */ 

using System;
using System.Reflection;
using System.Collections.Generic;

namespace Delta.CertXplorer.UI.Actions
{
    public class UIActionTargetDescriptor
    {
        private Dictionary<string, PropertyInfo> properties = null;
        private Type targetType = null;

        public UIActionTargetDescriptor(Type type)
        {
            properties = new Dictionary<string,PropertyInfo>();
            targetType = type;

            foreach (PropertyInfo property in targetType.GetProperties())
                properties.Add(property.Name, property);
        }        

        public Type TargetType { get { return targetType; } }

        internal void SetValue(string propertyName, object target, object value)
        {
            if (properties.ContainsKey(propertyName))
                properties[propertyName].SetValue(target, value, null);
        }

        internal object GetValue(string propertyName, object source)
        {
            if (properties.ContainsKey(propertyName))
                return properties[propertyName].GetValue(source, null);
                
            return null;
        }
    }
}
