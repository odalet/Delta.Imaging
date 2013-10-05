using System;
using System.Security;
using System.Runtime.InteropServices;

namespace Delta.CapiNet.Internals
{
    [SuppressUnmanagedCodeSecurity]
    internal static class NativeMethods
    {
        #region General

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LocalFree(IntPtr handle);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern LocalAllocHandle LocalAlloc([In]uint uFlags, [In]IntPtr uBytes);

        #endregion

        #region Localization

        /// <summary>
        /// See http://msdn.microsoft.com/en-us/library/windows/desktop/aa379937.aspx:
        /// </summary>
        /// <remarks>
        /// Original definition:
        /// <code>
        /// LPCWSTR WINAPI CryptFindLocalizedName(
        ///   __in  LPCWSTR pwszCryptName
        /// );
        /// </code>
        /// </remarks>
        [DllImport("crypt32.dll", EntryPoint = "CryptFindLocalizedName", CharSet = CharSet.Unicode)]
        private static extern IntPtr _CryptFindLocalizedName([In, MarshalAs(UnmanagedType.LPWStr)]string pwszCryptName);

        /// <summary>
        /// Finds the localized name for the specified name, such as the localized name of the "Root" system store. 
        /// This function can be used before displaying any UI that included a name that might have a localized form.
        /// </summary>
        /// <param name="pwszCryptName">
        /// The specified name. An internal table is searched to compare a predefined localized name to the specified name. 
        /// The search matches the localized name by using a case insensitive string comparison.
        /// </param>
        /// <returns>
        /// If the specified name is found, the localized name is returned otherwise, an empty string. 
        /// </returns>
        /// <remarks>
        /// Because the native function's documentation clearly specifies that the returned string should not be freed, this method
        /// is a thin wrapper around an interop definition. By using a simple <c>MarshalAs(UnmanagedType.LPWStr)</c>, the return 
        /// string would have got freed.
        /// </remarks>
        public static string CryptFindLocalizedName(string pwszCryptName)
        {
            // We need this wrapper, because, we must not free the returned pointer!
            // And by using a simple MarshalAs(UnmanagedType.LPWStr), the return string gets freed.
            var result = _CryptFindLocalizedName(pwszCryptName);
            if (result == IntPtr.Zero) return string.Empty;
            return Marshal.PtrToStringUni(result);
        }

        #endregion

        #region Store Location

        #region CertEnumSystemStoreLocation

        /// <summary>
        /// See http://msdn.microsoft.com/en-us/library/aa376060.aspx:
        /// <remarks>
        /// Original definition:
        /// <code>
        /// BOOL WINAPI CertEnumSystemStoreLocation(
        ///  __in  DWORD dwFlags,
        ///  __in  void *pvArg,
        ///  __in  PFN_CERT_ENUM_SYSTEM_STORE_LOCATION pfnEnum
        /// );
        /// </code></remarks>
        /// </summary>
        [DllImport("crypt32.dll", CharSet = CharSet.Unicode)]
        public static extern bool CertEnumSystemStoreLocation(
            [In]uint dwFlags,
            [In]IntPtr pvArg,
            [In]CertEnumSystemStoreLocationCallback pfnEnum);

        /// <summary>
        /// See http://msdn.microsoft.com/en-us/library/aa376059.aspx:
        /// <remarks>
        /// Original definition:
        /// <code>
        /// BOOL WINAPI CertEnumSystemStoreLocationCallback(
        ///   __in  LPCWSTR pvszStoreLocations,
        ///   __in  DWORD dwFlags,
        ///   __in  void *pvReserved,
        ///   __in  void *pvArg
        /// );
        /// </code></remarks>
        /// </summary>
        public delegate bool CertEnumSystemStoreLocationCallback(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pvszStoreLocations,
            uint dwFlags,
            uint pvReserved,
            IntPtr pvArg);

        #endregion

        #endregion

        #region Store

        [DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool CertCloseStore(IntPtr hCertStore, uint dwFlags);

        /// <summary>
        /// See http://msdn.microsoft.com/en-us/library/aa376089.aspx:
        /// </summary>
        /// <remarks>
        /// Original definition:
        /// <code>
        /// BOOL CertGetStoreProperty(
        ///   __in     HCERTSTORE hCertStore,
        ///   __in     DWORD dwPropId,
        ///   __out    void *pvData,
        ///   __inout  DWORD *pcbData
        /// );
        /// </code>
        /// </remarks>
        [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool CertGetStoreProperty(
            [In]CertStoreHandle hCertStore,
            [In] uint dwPropId,
            [In, Out]LocalAllocHandle pvData,
            [In, Out]ref uint pcbData);

        #region CertEnumSystemStore

        /// <summary>
        /// See http://msdn.microsoft.com/en-us/library/aa376058.aspx:
        /// <remarks>
        /// Original definition:
        /// <code>
        /// BOOL WINAPI CertEnumSystemStore(
        ///  __in      DWORD dwFlags,
        ///  __in_opt  void *pvSystemStoreLocationPara,
        ///  __in      void *pvArg,
        ///  __in      PFN_CERT_ENUM_SYSTEM_STORE pfnEnum
        /// );
        /// </code></remarks>
        /// </summary>
        [DllImport("crypt32.dll", CharSet = CharSet.Unicode)]
        public static extern bool CertEnumSystemStore(
            [In]uint dwFlags,
            [In]uint pvSystemStoreLocationPara,
            [In]IntPtr pvArg,
            [In]CertEnumSystemStoreCallback pfnEnum);

        /// <summary>
        /// See http://msdn.microsoft.com/en-us/library/aa376059.aspx:
        /// <remarks>
        /// Original definition:
        /// <code>
        /// BOOL WINAPI CertEnumSystemStoreCallback(
        ///  __in  const void *pvSystemStore,
        ///  __in  DWORD dwFlags,
        ///  __in  PCERT_SYSTEM_STORE_INFO pStoreInfo,
        ///  __in  void *pvReserved,
        ///  __in  void *pvArg
        /// );
        /// 
        /// typedef void (WINAPI *PFN_CERT_ENUM_SYSTEM_STORE)( );
        /// </code></remarks>
        /// </summary>
        public delegate bool CertEnumSystemStoreCallback(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pvSystemStore,
            uint dwFlags,
            ref CERT_SYSTEM_STORE_INFO pStoreInfo,
            uint pvReserved,
            IntPtr pvArg);


        #endregion

        #region CertEnumPhysicalStore

        /// <summary>
        /// See http://msdn.microsoft.com/en-us/library/aa376055.aspx:
        /// <remarks>
        /// Original definition:
        /// <code>
        /// BOOL WINAPI CertEnumPhysicalStore(
        ///   __in  const void *pvSystemStore,
        ///   __in  DWORD dwFlags,
        ///   __in  void *pvArg,
        ///   __in  PFN_CERT_ENUM_PHYSICAL_STORE pfnEnum
        /// );
        /// </code></remarks>
        /// </summary>
        [DllImport("crypt32.dll", CharSet = CharSet.Unicode)]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern bool CertEnumPhysicalStore(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pvSystemStore,
            [In]uint dwFlags,
            [In]IntPtr pvArg,
            [In]CertEnumPhysicalStoreCallback pfnEnum);

        /// <summary>
        /// See http://msdn.microsoft.com/en-us/library/aa376056.aspx:
        /// <remarks>
        /// Original definition:
        /// <code>
        /// BOOL WINAPI CertEnumPhysicalStoreCallback(
        ///   __in  const void *pvSystemStore,
        ///   __in  DWORD dwFlags,
        ///   __in  LPCWSTR pwszStoreName,
        ///   __in  PCERT_PHYSICAL_STORE_INFO pStoreInfo,
        ///   __in  void *pvReserved,
        ///   __in  void *pvArg
        /// );
        /// 
        /// typedef void (WINAPI *PFN_CERT_ENUM_PHYSICAL_STORE)( );
        /// </code></remarks>
        /// </summary>
        public delegate bool CertEnumPhysicalStoreCallback(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pvSystemStore,
            uint dwFlags,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pwszStoreName,
            ref CERT_PHYSICAL_STORE_INFO pStoreInfo,
            uint pvReserved,
            IntPtr pvArg);

        #endregion

        #endregion

        #region CRL

        /// <summary>
        /// See http://msdn.microsoft.com/en-us/library/aa376052.aspx:
        /// <remarks>
        /// Original definition:
        /// <code>
        /// PCCRL_CONTEXT WINAPI CertEnumCRLsInStore(
        ///   _In_  HCERTSTORE hCertStore,
        ///   _In_  PCCRL_CONTEXT pPrevCrlContext
        /// );
        /// </code></remarks>
        /// </summary>
        [DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr CertEnumCRLsInStore([In]CertStoreHandle hCertStore, [In]IntPtr pPrevCrlContext);

        /// <summary>
        /// See http://msdn.microsoft.com/en-us/library/aa376076.aspx:
        /// <remarks>
        /// Original definition:
        /// <code>
        /// BOOL WINAPI CertFreeCRLContext(
        ///   __in  PCCRL_CONTEXT pCrlContext
        /// );
        /// </code></remarks>
        /// </summary>
        [DllImport("crypt32.dll", SetLastError = true)]
        public static extern bool CertFreeCRLContext([In]IntPtr pCrlContext);

        /// <summary>
        /// See http://msdn.microsoft.com/en-us/library/aa376036.aspx:
        /// </summary>
        /// <remarks>
        /// Original definition:
        /// <code>
        /// PCCRL_CONTEXT WINAPI CertDuplicateCRLContext(
        ///   __in  PCCRL_CONTEXT pCrlContext
        /// );
        /// </code></remarks>
        [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern CrlContextHandle CertDuplicateCRLContext([In]IntPtr pCrlContext);

        /// <summary>
        /// See http://msdn.microsoft.com/en-us/library/aa376080.aspx:
        /// </summary>
        /// <remarks>
        /// Original definition:
        /// <code>
        /// BOOL WINAPI CertGetCRLContextProperty(
        ///   __in     PCCRL_CONTEXT pCrlContext,
        ///   __in     DWORD dwPropId,
        ///   __out    void *pvData,
        ///   __inout  DWORD *pcbData
        /// );
        /// </code></remarks>
        [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool CertGetCRLContextProperty(
            [In]CrlContextHandle pCrlContext,
            [In] uint dwPropId,
            [In, Out]LocalAllocHandle pvData,
            [In, Out]ref uint pcbData);

        #endregion

        #region CTL

        /// <summary>
        /// See http://msdn.microsoft.com/en-us/library/aa376054.aspx:
        /// <remarks>
        /// Original definition:
        /// <code>
        /// PCCTL_CONTEXT WINAPI CertEnumCTLsInStore(
        ///   _In_  HCERTSTORE hCertStore,
        ///   _In_  PCCTL_CONTEXT pPrevCtlContext
        /// );
        /// </code></remarks>
        /// </summary>
        [DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr CertEnumCTLsInStore([In]CertStoreHandle hCertStore, [In]IntPtr pPrevCtlContext);

        /// <summary>
        /// See http://msdn.microsoft.com/en-us/library/aa376077.aspx:
        /// <remarks>
        /// Original definition:
        /// <code>
        /// BOOL WINAPI CertFreeCTLContext(
        ///   __in  PCCTL_CONTEXT pCtlContext
        /// );
        /// </code></remarks>
        /// </summary>
        [DllImport("crypt32.dll", SetLastError = true)]
        public static extern bool CertFreeCTLContext([In]IntPtr pCtlContext);

        /// <summary>
        /// See http://msdn.microsoft.com/en-us/library/aa376047.aspx:
        /// </summary>
        /// <remarks>
        /// Original definition:
        /// <code>
        /// PCCTL_CONTEXT WINAPI CertDuplicateCTLContext(
        ///   __in  PCCTL_CONTEXT pCtlContext
        /// );
        /// </code></remarks>
        [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern CtlContextHandle CertDuplicateCTLContext([In]IntPtr pCtlContext);

        /// <summary>
        /// See http://msdn.microsoft.com/en-us/library/aa376082.aspx:
        /// </summary>
        /// <remarks>
        /// Original definition:
        /// <code>
        /// BOOL WINAPI CertGetCTLContextProperty(
        ///   __in     PCCTL_CONTEXT pCtlContext,
        ///   __in     DWORD dwPropId,
        ///   __out    void *pvData,
        ///   __inout  DWORD *pcbData
        /// );
        /// </code></remarks>
        [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool CertGetCTLContextProperty(
            [In]CtlContextHandle pCtlContext,
            [In] uint dwPropId,
            [In, Out]LocalAllocHandle pvData,
            [In, Out]ref uint pcbData);

        #endregion

        #region Certificate

        [DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr CertEnumCertificatesInStore([In]CertStoreHandle hCertStore, [In]IntPtr pPrevCertContext);

        /// <summary>
        /// See http://msdn.microsoft.com/en-us/library/windows/desktop/aa376075.aspx
        /// </summary>
        /// <remarks>
        /// Original definition:
        /// <code>
        /// BOOL WINAPI CertFreeCertificateContext(
        ///   _In_  PCCERT_CONTEXT pCertContext
        /// );
        /// </code></remarks>
        [DllImport("crypt32.dll", SetLastError = true)]
        public static extern bool CertFreeCertificateContext([In]IntPtr pCertContext);

        [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern CertContextHandle CertDuplicateCertificateContext([In] IntPtr pCertContext);

        [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool CertGetCertificateContextProperty(
            [In]CertContextHandle pCertContext,
            [In] uint dwPropId,
            [In, Out]LocalAllocHandle pvData,
            [In, Out]ref uint pcbData);

        #endregion

        #region UI

        /// <summary>
        /// See http://msdn.microsoft.com/en-us/library/aa387036.aspx:
        /// <remarks>
        /// Original definition:
        /// <code>
        /// BOOL WINAPI OpenPersonalTrustDBDialog(
        ///  __in_opt  HWND hwndParent
        /// );
        /// </code></remarks>
        /// </summary>
        [DllImport("wintrust.dll", CharSet = CharSet.Unicode)]
        public static extern bool OpenPersonalTrustDBDialog([In]IntPtr hwndParent);

        /// <summary>
        /// See http://msdn.microsoft.com/en-us/library/aa387037.aspx:
        /// <remarks>
        /// Original definition:
        /// <code>
        /// BOOL WINAPI OpenPersonalTrustDBDialog(
        ///  __in_opt  HWND hwndParent
        ///  __in         DWORD dwFlags,
        ///  __inout_opt  PVOID *pvReserved
        /// );
        /// </code>
        /// </remarks>
        /// </summary>
        [DllImport("wintrust.dll", CharSet = CharSet.Unicode)]
        public static extern bool OpenPersonalTrustDBDialogEx([In]IntPtr hwndParent, [In]uint dwFlags, [In, Out]IntPtr pvReserved);

        /// <summary>
        /// See http://msdn.microsoft.com/en-us/library/aa380290.aspx:
        /// </summary>
        /// <remarks>
        /// Original definition:
        /// <code>
        /// BOOL WINAPI CryptUIDlgViewContext(
        ///  __in  DWORD dwContextType,
        ///  __in  const void *pvContext,
        ///  __in  HWND hwnd,
        ///  __in  LPCWSTR pwszTitle,
        ///  __in  DWORD dwFlags,
        ///  __in  void *pvReserved
        /// );
        /// </code>
        /// </remarks>
        [DllImport("cryptui.dll", CharSet = CharSet.Unicode)]
        public static extern bool CryptUIDlgViewContext(
            [In]uint dwContextType,
            [In]CrlContextHandle pvContext,
            [In]IntPtr hwnd,
            [In, MarshalAs(UnmanagedType.LPWStr)]string pwszTitle,
            [In]uint dwFlags,
            [In]IntPtr pvReserved);

        // Same as above but for a Certificate Trust List
        [DllImport("cryptui.dll", CharSet = CharSet.Unicode)]
        public static extern bool CryptUIDlgViewContext(
            [In]uint dwContextType,
            [In]CtlContextHandle pvContext,
            [In]IntPtr hwnd,
            [In, MarshalAs(UnmanagedType.LPWStr)]string pwszTitle,
            [In]uint dwFlags,
            [In]IntPtr pvReserved);

        // Same as above but without the typed IntPtr. 
        [DllImport("cryptui.dll", CharSet = CharSet.Unicode)]
        public static extern bool CryptUIDlgViewContext(
            [In]uint dwContextType,
            [In]IntPtr pvContext,
            [In]IntPtr hwnd,
            [In, MarshalAs(UnmanagedType.LPWStr)]string pwszTitle,
            [In]uint dwFlags,
            [In]IntPtr pvReserved);

        #region UNDOCUMENTED

        /// <summary>Build a new CTL or modify an existing CTL.</summary>
        /// <param name="dwFlags">
        /// IN  Optional:   Can be set to the any combination of the following:
        ///                 CRYPTUI_WIZ_BUILDCTL_SKIP_DESTINATION
        ///                 CRYPTUI_WIZ_BUILDCTL_SKIP_SIGNING
        ///                 CRYPTUI_WIZ_BUILDCTL_SKIP_PURPOSE.
        /// </param>
        /// <param name="hwndParent">IN  Optional:   The parent window handle.</param>
        /// <param name="pwszWizardTitle">
        /// IN  Optional:   The title of the wizard
        ///                 If NULL, the default will be IDS_BUILDCTL_WIZARD_TITLE.
        /// </param>
        /// <param name="pBuildCTLSrc">IN  Optional:   The source from which the CTL will be built.</param>
        /// <param name="pBuildCTLDest">IN  Optional:   The desination where the newly built CTL will be stored.</param>
        /// <param name="ppCTLContext">OUT Optional:   The newly build CTL.</param>
        /// <returns>This function returns TRUE on success and FALSE on failure.</returns>
        /// <remarks>
        /// Build a new CTL or modify an existing CTL. The UI for wizard will always show in this case.
        /// <code>
        /// BOOL WINAPI CryptUIWizBuildCTL(
        /// IN              DWORD                                   dwFlags,
        /// IN  OPTIONAL    HWND                                    hwndParent,
        /// IN  OPTIONAL    LPCWSTR                                 pwszWizardTitle,
        /// IN  OPTIONAL    PCCRYPTUI_WIZ_BUILDCTL_SRC_INFO         pBuildCTLSrc,
        /// IN  OPTIONAL    PCCRYPTUI_WIZ_BUILDCTL_DEST_INFO        pBuildCTLDest,
        /// OUT OPTIONAL    PCCTL_CONTEXT                           *ppCTLContext
        /// );
        /// </code>
        /// </remarks>
        [DllImport("cryptui.dll", CharSet = CharSet.Unicode)]
        public static extern bool CryptUIWizBuildCTL(
            [In]uint dwFlags,
            [In]IntPtr hwndParent,
            [In, MarshalAs(UnmanagedType.LPWStr)]string pwszWizardTitle,
            [In]IntPtr pBuildCTLSrc,
            [In]IntPtr pBuildCTLDest,
            [Out]out IntPtr ppCTLContext);

        #endregion

        #endregion
    }
}
