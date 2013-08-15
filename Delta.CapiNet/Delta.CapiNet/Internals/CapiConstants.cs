using System;

namespace Delta.CapiNet.Internals
{
    internal static class CapiConstants
    {
        #region cert store location

        public const uint CERT_SYSTEM_STORE_UNPROTECTED_FLAG = 0x40000000;
        public const uint CERT_SYSTEM_STORE_LOCATION_MASK = 0x00FF0000;
        public const uint CERT_SYSTEM_STORE_LOCATION_SHIFT = 16;

        public const uint CERT_SYSTEM_STORE_CURRENT_USER_ID = 1;
        public const uint CERT_SYSTEM_STORE_LOCAL_MACHINE_ID = 2;
        public const uint CERT_SYSTEM_STORE_CURRENT_SERVICE_ID = 4;
        public const uint CERT_SYSTEM_STORE_SERVICES_ID = 5;
        public const uint CERT_SYSTEM_STORE_USERS_ID = 6;
        public const uint CERT_SYSTEM_STORE_CURRENT_USER_GROUP_POLICY_ID = 7;
        public const uint CERT_SYSTEM_STORE_LOCAL_MACHINE_GROUP_POLICY_ID = 8;
        public const uint CERT_SYSTEM_STORE_LOCAL_MACHINE_ENTERPRISE_ID = 9;

        public const uint CERT_SYSTEM_STORE_CURRENT_USER = ((int)CERT_SYSTEM_STORE_CURRENT_USER_ID << (int)CERT_SYSTEM_STORE_LOCATION_SHIFT);
        public const uint CERT_SYSTEM_STORE_LOCAL_MACHINE = ((int)CERT_SYSTEM_STORE_LOCAL_MACHINE_ID << (int)CERT_SYSTEM_STORE_LOCATION_SHIFT);
        public const uint CERT_SYSTEM_STORE_CURRENT_SERVICE = ((int)CERT_SYSTEM_STORE_CURRENT_SERVICE_ID << (int)CERT_SYSTEM_STORE_LOCATION_SHIFT);
        public const uint CERT_SYSTEM_STORE_SERVICES = ((int)CERT_SYSTEM_STORE_SERVICES_ID << (int)CERT_SYSTEM_STORE_LOCATION_SHIFT);
        public const uint CERT_SYSTEM_STORE_USERS = ((int)CERT_SYSTEM_STORE_USERS_ID << (int)CERT_SYSTEM_STORE_LOCATION_SHIFT);
        public const uint CERT_SYSTEM_STORE_CURRENT_USER_GROUP_POLICY = ((int)CERT_SYSTEM_STORE_CURRENT_USER_GROUP_POLICY_ID << (int)CERT_SYSTEM_STORE_LOCATION_SHIFT);
        public const uint CERT_SYSTEM_STORE_LOCAL_MACHINE_GROUP_POLICY = ((int)CERT_SYSTEM_STORE_LOCAL_MACHINE_GROUP_POLICY_ID << (int)CERT_SYSTEM_STORE_LOCATION_SHIFT);
        public const uint CERT_SYSTEM_STORE_LOCAL_MACHINE_ENTERPRISE = ((int)CERT_SYSTEM_STORE_LOCAL_MACHINE_ENTERPRISE_ID << (int)CERT_SYSTEM_STORE_LOCATION_SHIFT);

        #endregion

        #region OpenPersonalTrustDBDialogEx

        public const uint WT_TRUSTDBDIALOG_ONLY_PUB_TAB_FLAG = 0x2;

        #endregion

        #region Store properties

        /// <summary>The localized name of the store.</summary>
        public const uint CERT_STORE_LOCALIZED_NAME_PROP_ID = 0x1000;

        #endregion

        #region Certificate & CRL context properties

        internal const uint CERT_KEY_PROV_HANDLE_PROP_ID = 1;
        internal const uint CERT_KEY_PROV_INFO_PROP_ID = 2;
        internal const uint CERT_SHA1_HASH_PROP_ID = 3;
        internal const uint CERT_MD5_HASH_PROP_ID = 4;
        internal const uint CERT_HASH_PROP_ID = CERT_SHA1_HASH_PROP_ID;
        internal const uint CERT_KEY_CONTEXT_PROP_ID = 5;
        internal const uint CERT_KEY_SPEC_PROP_ID = 6;
        internal const uint CERT_IE30_RESERVED_PROP_ID = 7;
        internal const uint CERT_PUBKEY_HASH_RESERVED_PROP_ID = 8;
        internal const uint CERT_ENHKEY_USAGE_PROP_ID = 9;
        internal const uint CERT_CTL_USAGE_PROP_ID = CERT_ENHKEY_USAGE_PROP_ID;
        internal const uint CERT_NEXT_UPDATE_LOCATION_PROP_ID = 10;
        internal const uint CERT_FRIENDLY_NAME_PROP_ID = 11;
        internal const uint CERT_PVK_FILE_PROP_ID = 12;
        internal const uint CERT_DESCRIPTION_PROP_ID = 13;
        internal const uint CERT_ACCESS_STATE_PROP_ID = 14;
        internal const uint CERT_SIGNATURE_HASH_PROP_ID = 15;
        internal const uint CERT_SMART_CARD_DATA_PROP_ID = 16;
        internal const uint CERT_EFS_PROP_ID = 17;
        internal const uint CERT_FORTEZZA_DATA_PROP_ID = 18;
        internal const uint CERT_ARCHIVED_PROP_ID = 19;
        internal const uint CERT_KEY_IDENTIFIER_PROP_ID = 20;
        internal const uint CERT_AUTO_ENROLL_PROP_ID = 21;
        internal const uint CERT_PUBKEY_ALG_PARA_PROP_ID = 22;
        internal const uint CERT_CROSS_CERT_DIST_POINTS_PROP_ID = 23;
        internal const uint CERT_ISSUER_PUBLIC_KEY_MD5_HASH_PROP_ID = 24;
        internal const uint CERT_SUBJECT_PUBLIC_KEY_MD5_HASH_PROP_ID = 25;
        internal const uint CERT_ENROLLMENT_PROP_ID = 26;
        internal const uint CERT_DATE_STAMP_PROP_ID = 27;
        internal const uint CERT_ISSUER_SERIAL_NUMBER_MD5_HASH_PROP_ID = 28;
        internal const uint CERT_SUBJECT_NAME_MD5_HASH_PROP_ID = 29;
        internal const uint CERT_EXTENDED_ERROR_INFO_PROP_ID = 30;
        internal const uint CERT_RENEWAL_PROP_ID = 64;
        internal const uint CERT_ARCHIVED_KEY_HASH_PROP_ID = 65;
        internal const uint CERT_FIRST_RESERVED_PROP_ID = 66;

        #endregion

        #region CryptUIDlgViewContext related constants

        public const uint CERT_STORE_CERTIFICATE_CONTEXT = 1;
        public const uint CERT_STORE_CRL_CONTEXT = 2;
        public const uint CERT_STORE_CTL_CONTEXT = 3;

        #endregion

        #region UNDOCUMENTED

        // The following definitions were extracted from an old (and not maintained any more) cryptui.h file.

        #region CryptUIWiz... related constants

        /// <summary>
        /// This constant is valid when used with <c>CryptUIWizImport</c> or <c>CryptUIWizExport</c>.
        /// <para>
        /// In the import case, the <c>CryptUIWizImport</c> function will perform the import based on the information in the <c>CRYPTUI_WIZ_EXPORT_INFO</c> 
        /// structure pointed to by <c>pImportSrc</c> into the store specified by <c>hDestCertStore</c> without displaying any user interface. 
        /// If this flag is not specified, the function will display a wizard to guide the user through the import process.
        /// </para>
        /// <para>
        /// In the export case, the <c>CryptUIWizExport</c> function will perform the export based on the information in the <c>CRYPTUI_WIZ_EXPORT_INFO</c> 
        /// structure pointed to by <c>pExportInfo</c> without displaying any user interface. If the flag is not specified, this function will display a 
        /// wizard to guide the user through the export process.
        /// </para>
        /// </summary>        
        /// <remarks>
        /// See http://msdn.microsoft.com/en-us/library/windows/desktop/aa380598.aspx for the <c>CryptUIWizImport</c> function or
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/aa380395.aspx for the <c>CryptUIWizExport</c> function.
        /// </remarks>
        /// <seealso cref="NativeMethods.CryptUIWizBuildCTL"/>
        public const uint CRYPTUI_WIZ_NO_UI = 0x0001;

        /// <summary>
        /// CRYPTUI_WIZ_NO_INSTALL_ROOT is only valid for CryptUIWizCertRequest API
        /// the wizard will not install the issued certificate chain into the root store,
        //  instead, it will put the certificate chain into the CA store.
        /// </summary>
        /// <remarks>
        /// The following definition is undocumented: it was extracted from an old (and not maintained any more) <c>cryptui.h</c> file.
        /// </remarks>
        /// <seealso cref="NativeMethods.CryptUIWizBuildCTL"/>
        public const uint CRYPTUI_WIZ_NO_INSTALL_ROOT = 0x0010;

        /// <summary>
        /// <c>CRYPTUI_WIZ_BUILDCTL_SKIP_DESTINATION is only valid for use with the <c>CryptUIWizBuildCTL</c> API function.
        /// The wizard will skip the page which asks the user to enter the destination where the CTL will be stored.
        /// </summary>
        /// <remarks>
        /// This definition is undocumented: it was extracted from an old (and not maintained any more) <c>cryptui.h</c> file.
        /// </remarks>
        /// <seealso cref="NativeMethods.CryptUIWizBuildCTL"/>
        public const uint CRYPTUI_WIZ_BUILDCTL_SKIP_DESTINATION = 0x0004;

        /// <summary>
        /// <c>CRYPTUI_WIZ_BUILDCTL_SKIP_SIGNING</c> is only valid for use with the <c>CryptUIWizBuildCTL</c> API function.
        /// The wizard will skip the page which asks user to sign the CTL. The CTLContext returned by CryptUIWizBuildCTL 
        /// will not be signed. The caller can then use <c>CryptUIWizDigitalSign</c> to sign the CTL.
        /// </summary>
        /// <remarks>
        /// This definition is undocumented: it was extracted from an old (and not maintained any more) <c>cryptui.h</c> file.
        /// </remarks>
        /// <seealso cref="NativeMethods.CryptUIWizBuildCTL"/>
        public const uint CRYPTUI_WIZ_BUILDCTL_SKIP_SIGNING = 0x0008;

        /// <summary>
        /// <c>CRYPTUI_WIZ_BUILDCTL_SKIP_PURPOSE</c> is only valid for use with the <c>CryptUIWizBuildCTL</c> API function.
        /// The wizard will skip the page which asks the user for the purpose, validity, and list ID of the CTL.
        /// </summary>
        /// <remarks>
        /// This definition is undocumented: it was extracted from an old (and not maintained any more) <c>cryptui.h</c> file.
        /// </remarks>
        /// <seealso cref="NativeMethods.CryptUIWizBuildCTL"/>
        public const uint CRYPTUI_WIZ_BUILDCTL_SKIP_PURPOSE = 0x0010;

        #endregion

        #endregion
    }
}
