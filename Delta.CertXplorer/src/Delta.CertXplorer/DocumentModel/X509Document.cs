using System;

namespace Delta.CertXplorer.DocumentModel
{
    internal class X509Document : BaseDocument
    {
        private string displayName = string.Empty;
        private X509Object x509data = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="X509Document"/> class.
        /// </summary>
        /// <param name="storeName">Name of the store.</param>
        /// <param name="storeLocation">The store location.</param>
        /// <param name="x509">The X509.</param>
        public X509Document(X509Object x509) : base()
        {
            if (x509 == null) 
                throw new ArgumentNullException("x509");

            x509data = x509;
            base.OpenDocument();
        }

        /// <summary>
        /// Gets the document caption as it should appear on the view's title.
        /// </summary>
        /// <value>The document caption.</value>
        public override string DocumentCaption
        {
            get { return x509data.DisplayName; }
        }

        /// <summary>
        /// Creates the data this document holds.
        /// </summary>
        /// <returns>An array of bytes.</returns>
        protected override byte[] CreateData()
        {
            return x509data.Data;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">
        /// The <paramref name="obj"/> parameter is null.
        /// </exception>
        public override bool Equals(object obj)
        {
            if (obj is X509Document)
            {
                return ((X509Document)obj).x509data.Equals(x509data) &&
                    ((X509Document)obj).displayName.Equals(displayName);
            }
            return false;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            if (x509data == null) return 0;
            else return x509data.GetHashCode() ^ displayName.GetHashCode();
        }

        
    }
}
