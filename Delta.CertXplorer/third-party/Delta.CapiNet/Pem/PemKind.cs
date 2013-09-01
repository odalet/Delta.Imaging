using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace Delta.CapiNet.Pem
{
    /// <summary>
    /// Represents various well-known PEM-encapsulated kinds of data.
    /// </summary>
    /// <remarks>
    /// The definitions here come from openssl source code. Precisely, the following snippet 
    /// extracted from <b>openssl/crypto/pem/pem.h</b>
    /// <code>
    /// #define PEM_STRING_X509_OLD	"X509 CERTIFICATE"
    /// #define PEM_STRING_X509		"CERTIFICATE"
    /// #define PEM_STRING_X509_PAIR	"CERTIFICATE PAIR"
    /// #define PEM_STRING_X509_TRUSTED	"TRUSTED CERTIFICATE"
    /// #define PEM_STRING_X509_REQ_OLD	"NEW CERTIFICATE REQUEST"
    /// #define PEM_STRING_X509_REQ	"CERTIFICATE REQUEST"
    /// #define PEM_STRING_X509_CRL	"X509 CRL"
    /// #define PEM_STRING_EVP_PKEY	"ANY PRIVATE KEY"
    /// #define PEM_STRING_PUBLIC	"PUBLIC KEY"
    /// #define PEM_STRING_RSA		"RSA PRIVATE KEY"
    /// #define PEM_STRING_RSA_PUBLIC	"RSA PUBLIC KEY"
    /// #define PEM_STRING_DSA		"DSA PRIVATE KEY"
    /// #define PEM_STRING_DSA_PUBLIC	"DSA PUBLIC KEY"
    /// #define PEM_STRING_PKCS7	"PKCS7"
    /// #define PEM_STRING_PKCS7_SIGNED	"PKCS #7 SIGNED DATA"
    /// #define PEM_STRING_PKCS8	"ENCRYPTED PRIVATE KEY"
    /// #define PEM_STRING_PKCS8INF	"PRIVATE KEY"
    /// #define PEM_STRING_DHPARAMS	"DH PARAMETERS"
    /// #define PEM_STRING_DHXPARAMS	"X9.42 DH PARAMETERS"
    /// #define PEM_STRING_SSL_SESSION	"SSL SESSION PARAMETERS"
    /// #define PEM_STRING_DSAPARAMS	"DSA PARAMETERS"
    /// #define PEM_STRING_ECDSA_PUBLIC "ECDSA PUBLIC KEY"
    /// #define PEM_STRING_ECPARAMETERS "EC PARAMETERS"
    /// #define PEM_STRING_ECPRIVATEKEY	"EC PRIVATE KEY"
    /// #define PEM_STRING_PARAMETERS	"PARAMETERS"
    /// #define PEM_STRING_CMS		"CMS"
    /// </code>
    /// </remarks>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public sealed class PemKind
    {
        private static readonly Dictionary<string, PemKind> registry;

        public static readonly PemKind X509Certificate;
        public static readonly PemKind X509CertificatePair;
        public static readonly PemKind X509TrustedCertificate;
        public static readonly PemKind X509CertificateRequest;
        public static readonly PemKind X509CertificateRevocationList;

        public static readonly PemKind AnyPrivateKey;
        public static readonly PemKind PublicKey;
        public static readonly PemKind RsaPrivateKey;
        public static readonly PemKind RsaPublicKey;
        public static readonly PemKind DsaPrivateKey;
        public static readonly PemKind DsaPublicKey;
        public static readonly PemKind DsaParameters;
        public static readonly PemKind EcdsaPrivateKey;
        public static readonly PemKind EcdsaPublicKey;
        public static readonly PemKind EcdsaParameters;
        public static readonly PemKind Parameters;
        
        public static readonly PemKind Pkcs7;
        public static readonly PemKind Pkcs7Signed;

        public static readonly PemKind Pkcs8Encrypted;
        public static readonly PemKind Pkcs8;

        public static readonly PemKind DiffieHelmmanPkcs3;
        public static readonly PemKind DiffieHelmmanX942;

        public static readonly PemKind SslSession;

        public static readonly PemKind Cms;

        public static readonly PemKind X509CertificateOld;
        public static readonly PemKind X509CertificateRequestOld;

        /// <summary>
        /// Initializes the <see cref="PemKind"/> class.
        /// </summary>
        static PemKind()
        {
            registry = new Dictionary<string, PemKind>();

            X509Certificate = new PemKind("CERTIFICATE", "Stores a X509 Certificate");
            X509CertificatePair = new PemKind("CERTIFICATE PAIR", "Stores a pair of X509 Certificates");
            X509TrustedCertificate = new PemKind("TRUSTED CERTIFICATE", "Stores a trusted X509 Certificate");
            X509CertificateRequest = new PemKind("CERTIFICATE REQUEST", "Stores a X509 Certificate Signature Request"); // PKCS #10
            X509CertificateRevocationList = new PemKind("X509 CRL", "Stores a X509 Certificate Revocation List");

            AnyPrivateKey = new PemKind("ANY PRIVATE KEY", "Stores a private key");
            PublicKey = new PemKind("PUBLIC KEY", "Stores a public key");
            RsaPrivateKey = new PemKind("RSA PRIVATE KEY", "Stores a RSA private key");
            RsaPublicKey = new PemKind("RSA PUBLIC KEY", "Stores a RSA public key");
            DsaPrivateKey = new PemKind("DSA PRIVATE KEY", "Stores a DSA private key");
            DsaPublicKey = new PemKind("DSA PUBLIC KEY", "Stores a DSA public key");
            DsaParameters = new PemKind("DSA PARAMETERS", "Stores DSA parameters");
            EcdsaPrivateKey = new PemKind("EC PRIVATE KEY", "Stores a ECDSA private key");
            EcdsaPublicKey = new PemKind("ECDSA PUBLIC KEY", "Stores a ECDSA public key");
            EcdsaParameters = new PemKind("EC PARAMETERS", "Stores ECDSA parameters");
            Parameters = new PemKind("PARAMETERS", "Stores a cryptographic algorithm parameters");

            Pkcs7 = new PemKind("PKCS7", "Stores a PKCS #7 message");
            Pkcs7Signed = new PemKind("PKCS #7 SIGNED DATA", "Stores a signed PKCS #7 message");

            Pkcs8Encrypted = new PemKind("ENCRYPTED PRIVATE KEY", "Stores an encrypted PKCS #8 key pair");
            Pkcs8 = new PemKind("PRIVATE KEY", "Stores a PKCS #8 key pair");

            DiffieHelmmanPkcs3 = new PemKind("DH PARAMETERS", "Stores Diffie–Hellman Key Agreement parameters according to the scheme specified by PKCS #3");
            DiffieHelmmanX942 = new PemKind("X9.42 DH PARAMETERS", "Stores Diffie–Hellman Key Agreement parameters according to the scheme specified in ANSI X9.42");

            SslSession = new PemKind("SSL SESSION PARAMETERS", "Stores the parameters of a SSL session");

            Cms = new PemKind("CMS", "Stores Cryptographic Message Syntax data");

            X509CertificateOld = new PemKind("X509 CERTIFICATE", "Stores a X509 Certificate; obsolete, prefer 'CERTIFICATE'", true);
            X509CertificateRequestOld = new PemKind("NEW CERTIFICATE REQUEST", "Stores a X509 Certificate Request; obsolete, prefer 'CERTIFICATE REQUEST'", true);
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="PemKind"/> class from being created.
        /// </summary>
        /// <param name="header">The PEM header.</param>
        /// <param name="description">The description.</param>
        /// <param name="obsolete">if set to <c>true</c> this header is obsolete.</param>
        /// <param name="custom">if set to <c>true</c> this header is custom.</param>
        private PemKind(string header, string description, bool obsolete = false, bool custom = false)
        {
            Header = header;
            Description = description;
            Obsolete = obsolete;
            Custom = custom;

            registry.Add(header, this);
        }

        /// <summary>
        /// Gets or creates a custom PEM kind.
        /// </summary>
        /// <param name="header">The custom PEM header.</param>
        /// <param name="description">An optional description.</param>
        /// <returns>A <see cref="PemKind"/> instance.</returns>
        public static PemKind GetCustom(string header, string description = "")
        {
            if (string.IsNullOrEmpty(header)) throw new ArgumentNullException("header");
            header = header.ToUpperInvariant();
            if (registry.ContainsKey(header)) return registry[header];

            return new PemKind(header, description, false, true);
        }

        public static PemKind Find(string header)
        {
            header = header.ToUpperInvariant();
            if (registry.ContainsKey(header)) return registry[header];
            return null;
        }

        /// <summary>
        /// Gets the PEM header associated with this kind.
        /// </summary>
        public string Header { get; private set; }

        /// <summary>
        /// Gets the description of the content of this kind of PEM data.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="PemKind"/> is obsolete.
        /// </summary>
        public bool Obsolete { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="PemKind"/> was custom built.
        /// </summary>
        public bool Custom { get; private set; }
    }
}
