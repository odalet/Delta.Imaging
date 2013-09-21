using System.ComponentModel;
using System.Security.Cryptography;

namespace Delta.CertXplorer.CertManager.Wrappers
{
    [TypeConverter(typeof(CustomExpandableObjectConverter))]
    internal class OidWrapper : BaseWrapper, IDisplayTypeWrapper
    {
        private Oid oid = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="OidWrapper"/> class.
        /// </summary>
        /// <param name="o">The oid.</param>
        public OidWrapper(Oid o) { oid = o; }

        public string FriendlyName { get { return TryGet(() => oid.FriendlyName); } }

        public string Value { get { return TryGet(() => oid.Value); } }

        #region IDisplayTypeWrapper Members

        public string DisplayType
        {
            get 
            {
                if (oid == null) return "NULL";
                if (string.IsNullOrEmpty(oid.FriendlyName))
                    return string.IsNullOrEmpty(oid.Value) ? string.Empty : oid.Value;
                else return string.IsNullOrEmpty(oid.Value) ? oid.FriendlyName : 
                    string.Format("{0} ({1})", oid.FriendlyName, oid.Value);
            }
        }

        #endregion
    }
}
