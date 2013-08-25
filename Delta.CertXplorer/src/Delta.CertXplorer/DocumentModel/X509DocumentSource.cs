using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Delta.CertXplorer.DocumentModel
{
    internal class X509DocumentSource : IDocumentSource<byte[]>
    {
        private X509Object x509data = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="X509DocumentSource"/> class.
        /// </summary>
        /// <param name="x509">The X509.</param>
        public X509DocumentSource(X509Object x509) : base()
        {
            if (x509 == null) 
                throw new ArgumentNullException("x509");

            x509data = x509;
        }

        #region IDocumentSource<byte[]> Members

        /// <summary>
        /// Creates the data.
        /// </summary>
        /// <returns></returns>
        public byte[] CreateData()
        {
            return x509data.Data;
        }

        /// <summary>
        /// Gets a value indicating whether this source is read only.
        /// </summary>
        /// <value>
        public bool IsReadOnly
        {
            get { return true; }
        }

        /// <summary>
        /// Gets the caption.
        /// </summary>
        public string Caption
        {
            get { return x509data.DisplayName; }
        }

        public string DataKey
        {
            get 
            {
                if (x509data == null) return string.Empty;
                return string.Format("x509:{0}", Caption);
            }
        }

        #endregion
    }
}
