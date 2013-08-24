using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices;

using Delta.CapiNet.Internals;

namespace Delta.CapiNet
{
    /// <summary>
    /// Represents A Certificate Revocation List.
    /// </summary>
    public class CertificateRevocationList
    {
        private byte[] cachedData = null;
        private DateTime thisUpdate = DateTime.MinValue;
        private DateTime nextUpdate = DateTime.MinValue;
        private X500DistinguishedName issuerName = null; // cached Issuer Name
        private CrlContextHandle safeHandle = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="CertificateRevocationList"/> class.
        /// </summary>
        public CertificateRevocationList() 
        {
            safeHandle = CrlContextHandle.InvalidHandle;
        }

        public CertificateRevocationList(IntPtr handle) : this()
        {
            if (handle == IntPtr.Zero) throw new ArgumentException("Invalid Handle");
            safeHandle = NativeMethods.CertDuplicateCRLContext(handle);
        }

        #region Properties

        public string FriendlyName
        {
            get
            {
                if (safeHandle.IsInvalid) throw new CryptographicException("Invalid Handle");

                var allocHandle = LocalAllocHandle.InvalidHandle;
                uint pcbData = 0;

                // 1st call gives the memory amount to allocate
                if (!NativeMethods.CertGetCRLContextProperty(
                    safeHandle, CapiConstants.CERT_FRIENDLY_NAME_PROP_ID, allocHandle, ref pcbData)) 
                    return string.Empty;

                // 2nd call fills the memory
                allocHandle = LocalAllocHandle.Allocate(0, new IntPtr((long)pcbData));
                try
                {
                    if (!NativeMethods.CertGetCRLContextProperty(
                        safeHandle, CapiConstants.CERT_FRIENDLY_NAME_PROP_ID, allocHandle, ref pcbData))
                        return string.Empty;

                    // Get the data back to a managed string and release the native memory.
                    var result = Marshal.PtrToStringUni(allocHandle.DangerousGetHandle());
                    return result;
                }
                finally { allocHandle.Dispose(); }
            }
        }

        internal CrlContextHandle SafeHandle
        {
            get { return safeHandle; }
        }

        public X500DistinguishedName IssuerName
        {
            get
            {
                if (safeHandle.IsInvalid) throw new CryptographicException("Invalid Handle");
                if (issuerName == null)
                {
                    var crlInfo = GetCrlInfo();
                    if (crlInfo.HasValue)
                        issuerName = new X500DistinguishedName(crlInfo.Value.Issuer.ToByteArray());
                }
                return issuerName;
            }
        }

        /// <summary>
        /// Gets this CRL's publication date.
        /// </summary>
        public DateTime PublicationDate
        {
            get
            {
                if (safeHandle.IsInvalid) throw new CryptographicException("Invalid Handle");
                if (thisUpdate == DateTime.MinValue)
                {
                    var crlInfo = GetCrlInfo();
                    if (crlInfo.HasValue)
                        thisUpdate = crlInfo.Value.ThisUpdate.ToDateTime();
                }

                return thisUpdate;
            }
        }

        /// <summary>
        /// Gets this CRL's next scheduled update.
        /// </summary>
        public DateTime NextUpdate
        {
            get
            {
                if (safeHandle.IsInvalid) throw new CryptographicException("Invalid Handle");
                if (nextUpdate == DateTime.MinValue)
                {
                    var crlInfo = GetCrlInfo();
                    if (crlInfo.HasValue)
                        nextUpdate = crlInfo.Value.NextUpdate.ToDateTime();
                }

                return nextUpdate;
            }
        }

        public bool IsValid
        {
            get
            {
                var now = DateTime.Now;
                return now >= PublicationDate && now <= NextUpdate;
            }
        }

        public byte[] RawData
        {
            get
            {
                if (safeHandle.IsInvalid) throw new CryptographicException("Invalid Handle");  
              
                if (cachedData == null)
                    cachedData = GetRawData() ?? new byte[0];
                return cachedData;
            }
        }

        #endregion

        private unsafe CRL_INFO? GetCrlInfo()
        {
            try
            {
                var crlContext = *((CRL_CONTEXT*)safeHandle.DangerousGetHandle());
                return (CRL_INFO)Marshal.PtrToStructure(crlContext.pCrlInfo, typeof(CRL_INFO));
            }
            catch (Exception ex)
            {
                var debugEx = ex; // for debugging purpose
            }

            return null;
        }

        private unsafe byte[] GetRawData()
        {
            try
            {
                var crlContext = *((CRL_CONTEXT*)safeHandle.DangerousGetHandle());
                int size = (int)crlContext.cbCrlEncoded;
                var data = new byte[size];
                Marshal.Copy(crlContext.pbCrlEncoded, data, 0, size);
                return data;
            }
            catch (Exception ex)
            {
                var debugEx = ex; // for debugging purpose
            }

            return null;
        }
    }
}
