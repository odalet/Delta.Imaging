using System;
using System.Security.Cryptography.X509Certificates;

using Delta.CapiNet;

namespace Delta.CertXplorer.Asn1Decoder
{
    /// <summary>
    /// Wraps either a X509 certificate, a CRL or a CTL (certificate trust list).
    /// </summary>
    internal class X509Object
    {
        private X509Object(string storeName, StoreLocation storeLocation)
        {
            StoreName = storeName;
            StoreLocation = storeLocation;
        }

        public string StoreName { get; private set; }

        public StoreLocation StoreLocation { get; private set; }

        public object Value { get; private set; }

        public byte[] Data { get; private set; }

        public string DisplayName
        {
            get { return FormatDisplayName(StoreName, StoreLocation); }
        }

        public static X509Object Create(X509Certificate2 certificate, string storeName, StoreLocation storeLocation)
        {
            if (certificate == null) throw new ArgumentNullException("certificate");

            return new X509Object(storeName, storeLocation) 
            {
                Value = certificate,
                Data = certificate.RawData
            };
        }

        public static X509Object Create(CertificateRevocationList crl, string storeName, StoreLocation storeLocation)
        {
            if (crl == null) throw new ArgumentNullException("crl");
            return new X509Object(storeName, storeLocation) 
            {
                Value = crl,
                Data = crl.RawData
            };
        }

        /// <summary>
        /// Formats the display name.
        /// </summary>
        /// <param name="storeName">Name of the certififcates store.</param>
        /// <param name="storeLocation">The store location.</param>
        private string FormatDisplayName(string storeName, StoreLocation storeLocation)
        {
            var fullStoreName = string.Format("{0}/{1}", storeLocation, storeName);
            var certName = "?";
            if (Value == null) certName = "Null";
            if (Value is X509Certificate2) certName = GetX509CertificateDisplayName((X509Certificate2)Value);
            if (Value is CertificateRevocationList) certName = GetCrlDisplayName((CertificateRevocationList)Value);
            return string.Format("{0}/{1}", fullStoreName, certName);
        }

        private string GetCrlDisplayName(CertificateRevocationList crl)
        {
            if (!string.IsNullOrEmpty(crl.FriendlyName))
                return crl.FriendlyName;
            return FormatDistinguishedName(crl.IssuerName);
        }

        private string GetX509CertificateDisplayName(X509Certificate2 x509)
        {
            if (!string.IsNullOrEmpty(x509.FriendlyName))
                return x509.FriendlyName;
            return FormatDistinguishedName(x509.SubjectName);            
        }

        private string FormatDistinguishedName(X500DistinguishedName dn)
        {
            var subjectName = dn.Name;
            if (subjectName.Contains("\""))
            {
                bool insideQuotes = false;
                string subjectName2 = string.Empty;
                for (int i = 0; i < subjectName.Length; i++)
                {
                    if (subjectName[i] == '"') insideQuotes = !insideQuotes;
                    if ((subjectName[i] == ',') && insideQuotes)
                        subjectName2 += '#';
                    else subjectName2 += subjectName[i];
                }

                subjectName = subjectName2;
            }

            var parts = subjectName.Split(',');
            var part = string.Empty;

            if (parts.Length == 0) part = dn.Name;
            else part = parts[0];

            part = part.Replace('#', ',');
            part = part.Replace("\"", string.Empty);

            int index = part.IndexOf('=');
            if (index != -1) part = part.Substring(index + 1);

            return part;
        }
    }
}
