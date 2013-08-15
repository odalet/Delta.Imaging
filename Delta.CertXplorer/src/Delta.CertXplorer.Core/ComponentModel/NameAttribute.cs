using System;

namespace Delta.CertXplorer.ComponentModel
{
    /// <summary>
    /// Allows modifying a property name when it is displayed in a <see cref="Delta.CertXplorer.UI.PropertyGridEx"/> control.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NameAttribute : Attribute
    {
        /// <summary>Property name</summary>
        private string name = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="NameAttribute"/> class.
        /// </summary>
        /// <param name="nameValue">The name value.</param>
        public NameAttribute(string nameValue) { name = nameValue; }

        /// <summary>
        /// Gets the name of the property decorated by this instance.
        /// </summary>
        public string Name { get { return name; } }
    }
}
