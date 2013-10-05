using System.Security.Cryptography.X509Certificates;
using Delta.CapiNet;

namespace Delta.CertXplorer.CertManager.Wrappers
{
    internal static class ObjectWrapper
    {
        /// <summary>
        /// Wraps the specified item so that it is displayed friendly in a property grid.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Wrapped item.</returns>
        public static object Wrap(object item)
        {
            if (item == null) return null;
            if (item is Certificate) return new CapiCertificateWrapper((Certificate)item);
            if (item is CertificateRevocationList) return new CapiCrlWrapper((CertificateRevocationList)item);
            if (item is CertificateTrustList) return new CapiCtlWrapper((CertificateTrustList)item);
            if (item is X509Certificate2) return new X509CertificateWrapper2((X509Certificate2)item);
            if (item is X509Certificate) return new X509CertificateWrapper((X509Certificate)item);
            return item;
        }
    }
}
