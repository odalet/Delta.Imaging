using System;

namespace Delta.CertXplorer.DocumentModel
{
    internal class X509DocumentSource : IDocumentSource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="X509DocumentSource"/> class.
        /// </summary>
        /// <param name="x509">The X509.</param>
        public X509DocumentSource(X509Object x509)
        {
            if (x509 == null)
                throw new ArgumentNullException("x509");

            X509Data = x509;
        }

        #region IDocumentSource Members

        public string Uri
        {
            get { return X509Data.DisplayName; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        #endregion

        public X509Object X509Data
        {
            get;
            private set;
        }
    }
}
