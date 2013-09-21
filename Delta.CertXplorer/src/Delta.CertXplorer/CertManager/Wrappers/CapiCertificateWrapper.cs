using Delta.CapiNet;

namespace Delta.CertXplorer.CertManager.Wrappers
{
    internal class CapiCertificateWrapper : X509CertificateWrapper2
    {
        private readonly Certificate cert;

        /// <summary>
        /// Initializes a new instance of the <see cref="CapiCertificateWrapper"/> class.
        /// </summary>
        /// <param name="certificate">The certificate.</param>
        public CapiCertificateWrapper(Certificate certificate) : base(certificate.X509)
        {
            cert = certificate;
        }

        public bool IsValid { get { return TryGet(() => cert.IsValid); } }
    }
}
