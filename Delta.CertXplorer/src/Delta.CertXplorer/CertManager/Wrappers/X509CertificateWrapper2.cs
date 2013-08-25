using System;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Delta.CertXplorer.CertManager.Wrappers
{
    internal class X509CertificateWrapper2 : X509CertificateWrapper
    {
        private X509Certificate2 x509 = null;
        private X509ExtensionWrapper[] extensions = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="X509CertificateWrapper2"/> class.
        /// </summary>
        /// <param name="certificate">The certificate.</param>
        public X509CertificateWrapper2(X509Certificate2 certificate)
            : base(certificate)
        {
            x509 = certificate;
            FillExtensions();
        }

        public bool Archived { get { return x509.Archived; } }
        
        public X509ExtensionWrapper[] Extensions { get { return TryGet(() => extensions); } }
        
        public string FriendlyName { get { return TryGet(() => x509.FriendlyName); } }
        
        public bool HasPrivateKey { get { return TryGet(() => x509.HasPrivateKey); } }
        
        public string IssuerName { get { return TryGet(() => x509.IssuerName.Name); } }
        
        public DateTime NotAfter { get { return TryGet(() => x509.NotAfter); } }
        
        public DateTime NotBefore { get { return TryGet(() => x509.NotBefore); } }
        
        public AsymmetricAlgorithm PrivateKey { get { return TryGet(() => x509.PrivateKey); } }
        
        public PublicKey PublicKey { get { return TryGet(() => x509.PublicKey); } }
        
        public byte[] RawData { get { return TryGet(() => x509.RawData); } }
        
        public string SerialNumber { get { return TryGet(() => x509.SerialNumber); } }
        
        public Oid SignatureAlgorithm { get { return TryGet(() => x509.SignatureAlgorithm); } }
        
        public string SubjectName { get { return TryGet(() => x509.SubjectName.Name); } }

        public string Thumbprint { get { return TryGet(() => x509.Thumbprint); } }
        
        public int Version { get { return TryGet(() => x509.Version); } }

        private void FillExtensions()
        {
            if (x509.Extensions != null && x509.Extensions.Count > 0)
                extensions = x509.Extensions
                    .Cast<X509Extension>()
                    .Select(x => X509ExtensionWrapper.Create(x))
                    .ToArray();
            else extensions = new X509ExtensionWrapper[0];
        }
    }
}
