using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Delta.CapiNet.Internals;

namespace Delta.CapiNet
{
    /// <summary>
    /// Represents A Certificate Trust List.
    /// </summary>
    public class CertificateTrustList
    {
        private byte[] cachedData = null;
        private DateTime thisUpdate = DateTime.MinValue;
        private DateTime nextUpdate = DateTime.MinValue;
        private X500DistinguishedName issuerName = null; // cached Issuer Name
        private CtlContextHandle safeHandle = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="CertificateTrustList"/> class.
        /// </summary>
        public CertificateTrustList() 
        {
            safeHandle = CtlContextHandle.InvalidHandle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CertificateTrustList"/> class.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <exception cref="System.ArgumentException">Invalid Handle</exception>
        public CertificateTrustList(IntPtr handle) : this()
        {
            if (handle == IntPtr.Zero) throw new ArgumentException("Invalid Handle");
            safeHandle = NativeMethods.CertDuplicateCTLContext(handle);
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
                if (!NativeMethods.CertGetCTLContextProperty(
                    safeHandle, CapiConstants.CERT_FRIENDLY_NAME_PROP_ID, allocHandle, ref pcbData))
                    return string.Empty;

                // 2nd call fills the memory
                allocHandle = LocalAllocHandle.Allocate(0, new IntPtr((long)pcbData));
                try
                {
                    if (!NativeMethods.CertGetCTLContextProperty(
                        safeHandle, CapiConstants.CERT_FRIENDLY_NAME_PROP_ID, allocHandle, ref pcbData))
                        return string.Empty;

                    // Get the data back to a managed string and release the native memory.
                    var result = Marshal.PtrToStringUni(allocHandle.DangerousGetHandle());
                    return result;
                }
                finally { allocHandle.Dispose(); }
            }
        }

        internal CtlContextHandle SafeHandle
        {
            get { return safeHandle; }
        }

        /// <summary>
        /// Gets this CTL's publication date.
        /// </summary>
        public DateTime PublicationDate
        {
            get
            {
                if (safeHandle.IsInvalid) throw new CryptographicException("Invalid Handle");
                if (thisUpdate == DateTime.MinValue)
                {
                    var ctlInfo = GetCtlInfo();
                    if (ctlInfo.HasValue)
                        thisUpdate = ctlInfo.Value.ThisUpdate.ToDateTime();
                }

                return thisUpdate;
            }
        }

        /// <summary>
        /// Gets this CTL's next scheduled update.
        /// </summary>
        public DateTime NextUpdate
        {
            get
            {
                if (safeHandle.IsInvalid) throw new CryptographicException("Invalid Handle");
                if (nextUpdate == DateTime.MinValue)
                {
                    var ctlInfo = GetCtlInfo();
                    if (ctlInfo.HasValue)
                        nextUpdate = ctlInfo.Value.NextUpdate.ToDateTime();
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

        private unsafe CTL_INFO? GetCtlInfo()
        {
            try
            {
                var ctlContext = *((CTL_CONTEXT*)safeHandle.DangerousGetHandle());
                return (CTL_INFO)Marshal.PtrToStructure(ctlContext.pCtlInfo, typeof(CTL_INFO));
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
                var ctlContext = *((CTL_CONTEXT*)safeHandle.DangerousGetHandle());
                int size = (int)ctlContext.cbCtlEncoded;
                var data = new byte[size];
                Marshal.Copy(ctlContext.pbCtlEncoded, data, 0, size);
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
