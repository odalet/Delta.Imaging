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

        public IntPtr Handle { get { return TryGet(() => x509.Handle); } }
        
        public string Issuer { get { return TryGet(() => x509.Issuer); } }
        
        public string Subject { get { return TryGet(() => x509.Subject); } }

        protected static T TryGet<T>(Func<T> function)
        {
            try
            {
                return function();
            }
            catch (Exception ex)
            {
                This.Logger.Error(string.Format("Could not execute function: {0}", ex.Message), ex);
            }

            return default(T);
        }

    }
}
