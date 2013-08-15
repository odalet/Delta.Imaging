using System;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace Delta.CapiNet
{
    /// <summary>
    /// This class wraps a <see cref="System.Security.Cryptography.X509Certificates.X509Certificate2"/> object.
    /// </summary>
    public class Certificate
    {
        private X509Certificate2 innerObject = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Certificate"/> class.
        /// </summary>
        /// <param name="certificate">The certificate.</param>
        public Certificate(X509Certificate2 certificate)
        {
            innerObject = certificate;
        }

        [Browsable(false)]
        public X509Certificate2 X509Certificate2
        {
            get { return innerObject; }
        }

        /// <summary>
        /// Gets a value indicating whether this certificate is valid.
        /// </summary>
        /// <remarks>
        /// The validity of the certificate is only verified by examining its 
        /// validity period. No certification chain verification is done.
        /// </remarks>
        /// <value><c>true</c> if this certificate is valid; otherwise, <c>false</c>.</value>
        public bool IsValid
        {
            get
            {
                var now = DateTime.Now;
                return now >= innerObject.NotBefore && now <= innerObject.NotAfter;
            }
        }
    }
}
