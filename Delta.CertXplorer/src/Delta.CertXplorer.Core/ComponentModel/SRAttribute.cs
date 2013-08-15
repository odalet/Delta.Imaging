using System;

namespace Delta.CertXplorer.ComponentModel
{
    /// <summary>
    /// Base class for attributes mapping a string resource 
    /// (Used by <see cref="Delta.CertXplorer.UI.PropertyGridEx"/>).
    /// </summary>
    public abstract class SRAttribute : Attribute
    {
        /// <summary>Resource name.</summary>
        string resourceName = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="SRAttribute"/> class.
        /// </summary>
        /// <param name="resName">Name of the resource.</param>
        public SRAttribute(string resName) { resourceName = resName; }

        /// <summary>
        /// Gets the name of the resource.
        /// </summary>
        /// <value>The name of the resource.</value>
        public string ResourceName { get { return resourceName; } }
    }

    /// <summary>
    /// Use this attribute to map a property's name to a string resource
    /// (Used by <see cref="Delta.CertXplorer.UI.PropertyGridEx"/>).
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public class SRNameAttribute : SRAttribute
    {
        public SRNameAttribute(string resName) : base(resName) { }
    }

    /// <summary>
    /// Use this attribute to map a property's category to a string resource
    /// (Used by <see cref="Delta.CertXplorer.UI.PropertyGridEx"/>).
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public class SRCategoryAttribute : SRAttribute
    {
        public SRCategoryAttribute(string resName) : base(resName) { }
    }

    /// <summary>
    /// Use this attribute to map a property's description to a string resource
    /// (Used by <see cref="Delta.CertXplorer.UI.PropertyGridEx"/>).
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public class SRDescriptionAttribute : SRAttribute
    {
        public SRDescriptionAttribute(string resName) : base(resName) { }
    }
}
