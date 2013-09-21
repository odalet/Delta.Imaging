using System;
using Delta.CapiNet;

namespace Delta.CertXplorer.CertManager.Wrappers
{
    internal class CapiCrlWrapper : BaseWrapper
    {
        private readonly CertificateRevocationList crl;

        public CapiCrlWrapper(CertificateRevocationList certificateRevocationList)
        {
            crl = certificateRevocationList;
        }

        public string FriendlyName { get { return TryGet(() => crl.FriendlyName); } }
        
        public string IssuerName { get { return TryGet(() => crl.IssuerName.Name); } }

        /// <summary>
        /// Gets this CRL's publication date.
        /// </summary>
        public DateTime PublicationDate { get { return TryGet(() => crl.PublicationDate); } }

        /// <summary>
        /// Gets this CRL's next scheduled update.
        /// </summary>
        public DateTime NextUpdate { get { return TryGet(() => crl.NextUpdate); } }

        public bool IsValid { get { return TryGet(() => crl.IsValid); } }
    }
}
