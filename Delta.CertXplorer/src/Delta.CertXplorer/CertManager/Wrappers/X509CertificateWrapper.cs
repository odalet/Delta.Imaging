using System;
using System.Security.Cryptography.X509Certificates;

namespace Delta.CertXplorer.CertManager.Wrappers
{
    internal class X509CertificateWrapper
    {
        private X509Certificate x509 = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="X509CertificateWrapper"/> class.
        /// </summary>
        /// <param name="certificate">The certificate.</param>
        public X509CertificateWrapper(X509Certificate certificate)
        {
            x509 = certificate;
        }

        public IntPtr Handle { get { return x509.Handle; } }
        public string Issuer { get { return x509.Issuer; } }
        public string Subject { get { return x509.Subject; } }

    }
}
