using System;
using Delta.CapiNet;

namespace Delta.CertXplorer.CertManager.Wrappers
{
    internal class CapiCtlWrapper : BaseWrapper
    {
        private readonly CertificateTrustList ctl;

        public CapiCtlWrapper(CertificateTrustList certificateTrustList)
        {
            ctl = certificateTrustList;
        }

        public string FriendlyName { get { return TryGet(() => ctl.FriendlyName); } }

        /// <summary>
        /// Gets this CTL's publication date.
        /// </summary>
        public DateTime PublicationDate { get { return TryGet(() => ctl.PublicationDate); } }

        /// <summary>
        /// Gets this CTL's next scheduled update.
        /// </summary>
        public DateTime NextUpdate { get { return TryGet(() => ctl.NextUpdate); } }

        public bool IsValid { get { return TryGet(() => ctl.IsValid); } }
    }
}
