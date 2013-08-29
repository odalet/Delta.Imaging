using System;
using System.Linq;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Delta.CertXplorer.CertManager.Wrappers
{
    [TypeConverter(typeof(CustomExpandableObjectConverter))]
    internal abstract class X509ExtensionWrapper : IDisplayTypeWrapper
    {
        private class X509SimpleExtensionWrapper : X509ExtensionWrapper
        {
            private X509Extension x509 = null;

            /// <summary>
            /// Initializes a new instance of the <see cref="X509ExtensionWrapper"/> class.
            /// </summary>
            /// <param name="extension">The extension.</param>
            public X509SimpleExtensionWrapper(X509Extension extension)
                : base(extension)
            {
                x509 = extension;
            }

            public Type Type { get { return x509.GetType(); } }
        }

        private class X509BasicConstraintsExtensionWrapper : X509ExtensionWrapper
        {
            private X509BasicConstraintsExtension x509 = null;

            /// <summary>
            /// Initializes a new instance of the <see cref="X509ExtensionWrapper"/> class.
            /// </summary>
            /// <param name="extension">The extension.</param>
            public X509BasicConstraintsExtensionWrapper(X509BasicConstraintsExtension extension)
                : base(extension)
            {
                x509 = extension;
            }

            public bool CertificateAuthority { get { return x509.CertificateAuthority; } }
            public bool HasPathLengthConstraint { get { return x509.HasPathLengthConstraint; } }
            public int PathLengthConstraint { get { return x509.PathLengthConstraint; } }
        }

        private class X509KeyUsageExtensionWrapper : X509ExtensionWrapper
        {
            private X509KeyUsageExtension x509 = null;

            public X509KeyUsageExtensionWrapper(X509KeyUsageExtension extension)
                : base(extension)
            {
                x509 = extension;
            }

            public X509KeyUsageFlags KeyUsages { get { return x509.KeyUsages; } }
        }

        private class X509EnhancedKeyUsageExtensionWrapper : X509ExtensionWrapper
        {
            private OidWrapper[] oids = null;
            private X509EnhancedKeyUsageExtension x509 = null;

            public X509EnhancedKeyUsageExtensionWrapper(X509EnhancedKeyUsageExtension extension)
                : base(extension)
            {
                x509 = extension;
                FillOids();
            }

            public OidWrapper[] EnhancedKeyUsages { get { return oids; } }

            private void FillOids()
            {
                if (x509.EnhancedKeyUsages != null && x509.EnhancedKeyUsages.Count > 0)
                    oids = x509.EnhancedKeyUsages.Cast<Oid>().Select(o => new OidWrapper(o)).ToArray();
                else oids = new OidWrapper[0];
            }
        }

        private class X509SubjectKeyIdentifierExtensionWrapper : X509ExtensionWrapper
        {
            private X509SubjectKeyIdentifierExtension x509 = null;

            public X509SubjectKeyIdentifierExtensionWrapper(X509SubjectKeyIdentifierExtension extension)
                : base(extension)
            {
                x509 = extension;
            }

            public string SubjectKeyIdentifier { get { return x509.SubjectKeyIdentifier; } }
        }

        private X509Extension x509ext = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="X509ExtensionWrapper"/> class.
        /// </summary>
        /// <param name="extension">The extension.</param>
        protected X509ExtensionWrapper(X509Extension extension)
        {
            x509ext = extension;
        }

        public bool Critical { get { return x509ext.Critical; } }
        public OidWrapper Oid { get { return new OidWrapper(x509ext.Oid); } }
        public byte[] RawData { get { return x509ext.RawData; } }

        public static X509ExtensionWrapper Create(X509Extension extension)
        {
            if (extension == null) return null;
            else if (extension is X509BasicConstraintsExtension)
                return new X509BasicConstraintsExtensionWrapper((X509BasicConstraintsExtension)extension);
            else if (extension is X509EnhancedKeyUsageExtension)
                return new X509EnhancedKeyUsageExtensionWrapper((X509EnhancedKeyUsageExtension)extension);
            else if (extension is X509KeyUsageExtension)
                return new X509KeyUsageExtensionWrapper((X509KeyUsageExtension)extension);
            else if (extension is X509SubjectKeyIdentifierExtension)
                return new X509SubjectKeyIdentifierExtensionWrapper((X509SubjectKeyIdentifierExtension)extension);
            else return new X509SimpleExtensionWrapper(extension);
        }

        #region IDisplayTypeWrapper Members

        public virtual string DisplayType
        {
            get { return "X509 Extension: " + Oid.DisplayType; }
        }

        #endregion
    }
}
