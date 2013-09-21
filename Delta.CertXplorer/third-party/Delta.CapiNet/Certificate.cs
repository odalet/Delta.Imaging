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
        private X509Certificate2 x509 = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Certificate"/> class.
        /// </summary>
        /// <param name="certificate">The certificate.</param>
        public Certificate(X509Certificate2 certificate)
        {
            x509 = certificate;
        }

        #region Properties

        [Browsable(false)]
        public X509Certificate2 X509
        {
            get { return x509; }
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
                return now >= x509.NotBefore && now <= x509.NotAfter;
            }
        }

        #region X509Certificate2 forwarding properties

        /// <summary>
        /// Gets the subject distinguished name of this certificate.
        /// </summary>
        public X500DistinguishedName SubjectName
        {
            get { return x509.SubjectName; }
        }

        /// <summary>
        /// Gets a value that indicates whether this certificate contains a private key.
        /// </summary>
        public bool HasPrivateKey
        {
            get { return x509.HasPrivateKey; }
        }

        public X500DistinguishedName IssuerName 
        {
            get { return x509.IssuerName; }
        }

        /// <summary>
        /// Gets or sets the associated alias for a certificate.
        /// </summary>
        public string FriendlyName
        {
            get { return x509.FriendlyName; }
            set { x509.FriendlyName = value; }
        }

        #endregion

        #endregion
    }
}
