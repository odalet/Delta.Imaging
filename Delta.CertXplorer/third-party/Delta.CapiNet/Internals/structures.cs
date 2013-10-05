using System;
using System.Runtime.InteropServices;

using Microsoft.Win32.SafeHandles;

namespace Delta.CapiNet.Internals
{    
    /// <doc>
    /// See http://msdn.microsoft.com/en-us/library/aa377568.aspx:
    /// <code>
    /// typedef struct _CERT_SYSTEM_STORE_INFO {
    ///   DWORD cbSize;
    /// } CERT_SYSTEM_STORE_INFO, *PCERT_SYSTEM_STORE_INFO;
    /// </code>
    /// </doc>
    [StructLayout(LayoutKind.Sequential)]
    internal struct CERT_SYSTEM_STORE_INFO
    {
        /// <summary>Size of this stucture in bytes.</summary>
        uint cbSize;
    }

    /// <doc>
    /// See http://msdn.microsoft.com/en-us/library/aa377221.aspx:
    /// <code>
    /// typedef struct _CERT_PHYSICAL_STORE_INFO {
    ///   DWORD           cbSize;
    ///   LPSTR           pszOpenStoreProvider;
    ///   DWORD           dwOpenEncodingType;
    ///   DWORD           dwOpenFlags;
    ///   CRYPT_DATA_BLOB OpenParameters;
    ///   DWORD           dwFlags;
    ///   DWORD           dwPriority;
    /// }CERT_PHYSICAL_STORE_INFO, *PCERT_PHYSICAL_STORE_INFO;
    /// </code>
    /// </doc>
    [StructLayout(LayoutKind.Sequential)]
    internal struct CERT_PHYSICAL_STORE_INFO
    {
        /// <summary>The size, in bytes, of this structure.</summary>
        uint cbSize;

        /// <summary>A pointer to a string that names a certificate store provider type.</summary>
        [MarshalAs(UnmanagedType.LPStr)]
        string pszOpenStoreProvider;

        uint dwOpenEncodingType;

        uint dwOpenFlags;

        CRYPT_DATA_BLOB OpenParameters;

        /// <summary></summary>
        uint dwFlags;
        
        /// <summary></summary>
        uint dwPriority;
    }

    /// <doc>
    /// See http://msdn.microsoft.com/en-us/library/aa381414.aspx:
    /// <code>
    /// typedef struct _CRYPTOAPI_BLOB {
    ///  DWORD cbData;
    ///  BYTE *pbData;
    /// } CRYPT_DATA_BLOB ...
    /// </code>
    /// </doc>
    [StructLayout(LayoutKind.Sequential)]
    internal struct CRYPT_DATA_BLOB
    {
        /// <summary>A DWORD variable that contains the count, in bytes, of data.</summary>
        uint cbData;

        /// <summary>A pointer to the data buffer.</summary>
        IntPtr pbData;
    }

    #region Certificate & CRL context and info structures

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct CERT_CONTEXT
    {
        internal uint dwCertEncodingType;
        internal IntPtr pbCertEncoded;
        internal uint cbCertEncoded;
        internal IntPtr pCertInfo;
        internal IntPtr hCertStore;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct CERT_INFO
    {
        internal uint dwVersion;
        internal CRYPTOAPI_BLOB SerialNumber;
        internal CRYPT_ALGORITHM_IDENTIFIER SignatureAlgorithm;
        internal CRYPTOAPI_BLOB Issuer;
        internal System.Runtime.InteropServices.ComTypes.FILETIME NotBefore;
        internal System.Runtime.InteropServices.ComTypes.FILETIME NotAfter;
        internal CRYPTOAPI_BLOB Subject;
        internal CERT_PUBLIC_KEY_INFO SubjectPublicKeyInfo;
        internal CRYPT_BIT_BLOB IssuerUniqueId;
        internal CRYPT_BIT_BLOB SubjectUniqueId;
        internal uint cExtension;
        internal IntPtr rgExtension;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct CRL_CONTEXT
    {
        internal uint dwCertEncodingType;
        internal IntPtr pbCrlEncoded;
        internal uint cbCrlEncoded;
        internal IntPtr pCrlInfo; // --> points to a CRL_INFO structure
        internal IntPtr hCertStore;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct CRL_INFO
    {
        internal uint dwVersion;
        internal CRYPT_ALGORITHM_IDENTIFIER SignatureAlgorithm;
        internal CRYPTOAPI_BLOB Issuer;
        internal System.Runtime.InteropServices.ComTypes.FILETIME ThisUpdate;
        internal System.Runtime.InteropServices.ComTypes.FILETIME NextUpdate;
        internal uint cCRLEntry;
        internal IntPtr rgCRLEntry; // --> Array of CRL_ENTRY structures.
        internal uint cExtension;
        internal IntPtr rgExtension;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct CTL_CONTEXT
    {
        internal uint dwMsgAndCertEncodingType;
        internal IntPtr pbCtlEncoded;
        internal uint cbCtlEncoded;
        internal IntPtr pCtlInfo; // --> points to a CTL_INFO structure
        internal IntPtr hCertStore;
        internal IntPtr hCryptMsg; // --> ???????????
        internal IntPtr pbCtlContent;
        internal uint cbCtlContent;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct CTL_INFO
    {
        internal uint dwVersion;
        internal CTL_USAGE SubjectUsage;
        internal CRYPT_DATA_BLOB ListIdentifier;
        internal CRYPT_INTEGER_BLOB SequenceNumber;
        internal System.Runtime.InteropServices.ComTypes.FILETIME ThisUpdate;
        internal System.Runtime.InteropServices.ComTypes.FILETIME NextUpdate;
        internal CRYPT_ALGORITHM_IDENTIFIER SubjectAlgorithm;
        internal uint cCTLEntry;
        internal IntPtr rgCTLEntry; // --> Array of CTL_ENTRY structures.
        internal uint cExtension;
        internal IntPtr rgExtension;
    }

    // TODO: CRL_ENTRY, CTL_ENTRY

    #endregion

    #region misc data structures

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct CRYPTOAPI_BLOB
    {
        internal uint cbData;
        internal IntPtr pbData;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct CRYPT_BIT_BLOB
    {
        internal uint cbData;
        internal IntPtr pbData;
        internal uint cUnusedBits;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct CRYPT_INTEGER_BLOB
    {
        internal uint cbData;
        internal IntPtr pbData;
    }
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct CRYPT_ALGORITHM_IDENTIFIER
    {
        [MarshalAs(UnmanagedType.LPStr)]
        internal string pszObjId;
        internal CRYPTOAPI_BLOB Parameters;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct CERT_PUBLIC_KEY_INFO
    {
        internal CRYPT_ALGORITHM_IDENTIFIER Algorithm;
        internal CRYPT_BIT_BLOB PublicKey;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct CTL_USAGE
    {
        internal uint cUsageIdentifier;
        internal IntPtr rgpszUsageIdentifier;
    }
    
    #endregion
}
