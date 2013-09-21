using System.Security.Cryptography.X509Certificates;

using Delta.CapiNet.Internals;

namespace Delta.CapiNet
{
    public class CertificateStore
    {
        private string name = string.Empty;
        private CertificateStoreLocation location = null;

        /// <summary>
        /// Prevents a default instance of the <see cref="CertificateStore"/> class from being created.
        /// </summary>
        private CertificateStore() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CertificateStore"/> class.
        /// </summary>
        /// <param name="storeName">Name of the store.</param>
        /// <param name="storeLocation">The store location.</param>
        internal CertificateStore(string storeName, CertificateStoreLocation storeLocation)
        {
            name = storeName;
            location = storeLocation;
        }

        /// <summary>
        /// Gets the name of the store.
        /// </summary>
        /// <value>The name of the store.</value>
        public string Name { get { return name; } }

        /// <summary>
        /// Gets the location of the store.
        /// </summary>
        /// <value>The location of the store.</value>
        public CertificateStoreLocation Location
        {
            get { return location; }
        }

        /// <summary>
        /// Gets the localized name of the store.
        /// </summary>
        /// <value>The localized name of the store.</value>
        public string LocalizedName 
        { 
            get { return NativeMethods.CryptFindLocalizedName(name); } 
        }

        /// <summary>
        /// Creates a new <see cref="System.Security.Cryptography.X509Certificates.X509Store"/>
        /// object based on this store.
        /// </summary>
        /// <returns>An instance of <see cref="System.Security.Cryptography.X509Certificates.X509Store"/>.</returns>
        public X509Store GetX509Store()
        {
            return new X509Store(name, location.ToStoreLocation());
        }

        /// <summary>
        /// Returns a long <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A long <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public string ToLongString()
        {
            string localizedName = LocalizedName;
            if (string.IsNullOrEmpty(localizedName)) return ToString();
            else return string.Format("{0} [{1}]", ToString(), localizedName);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(name)) return "?";
            return name;
        }
    }
}
