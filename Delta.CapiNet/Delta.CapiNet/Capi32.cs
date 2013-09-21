using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

using Delta.CapiNet.Internals;

namespace Delta.CapiNet
{
    public static class Capi32
    {
        private class EnumSystemStore
        {
            private uint flags = (uint)0;
            private List<string> tempSystemStoreList = null;

            public EnumSystemStore(uint locationFlags)
            {
                flags = locationFlags;
            }

            /// <summary>
            /// Gets the system stores.
            /// </summary>
            /// <returns>An array of strings (never null).</returns>
            public string[] GetSystemStores()
            {
                tempSystemStoreList = new List<string>();
                bool ok = NativeMethods.CertEnumSystemStore(flags, 0, IntPtr.Zero,
                    new NativeMethods.CertEnumSystemStoreCallback(CertEnumSystemStoreCallback));

                string[] array = null;
                if (ok) array = tempSystemStoreList.ToArray();
                else array = new string[] { };

                tempSystemStoreList = null;
                return array;
            }

            private bool CertEnumSystemStoreCallback(
                        string pvSystemStore,
                        uint dwFlags,
                        ref CERT_SYSTEM_STORE_INFO pStoreInfo,
                        uint pvReserved,
                        IntPtr pvArg)
            {
                tempSystemStoreList.Add(pvSystemStore);
                return true;
            }
        }

        private class EnumPhysicalStore
        {
            private string systemStoreName = string.Empty;
            private List<string> tempPhysicalStoreList = null;

            public EnumPhysicalStore(string parent)
            {
                systemStoreName = parent;
            }

            /// <summary>
            /// Gets the system stores.
            /// </summary>
            /// <param name="flags">The Flags representing a System Stores Location.</param>
            /// <returns>An array of strings (never null).</returns>
            public string[] GetPhysicalStores()
            {
                tempPhysicalStoreList = new List<string>();
                var cb = new NativeMethods.CertEnumPhysicalStoreCallback(CertEnumPhysicalStoreCallback);
                bool ok = NativeMethods.CertEnumPhysicalStore(
                    systemStoreName,
                    CapiConstants.CERT_SYSTEM_STORE_CURRENT_USER,
                    IntPtr.Zero,
                    cb);

                string[] array = null;

                if (ok) array = tempPhysicalStoreList.ToArray();
                else array = new string[] { };

                tempPhysicalStoreList = null;
                return array;
            }

            private bool CertEnumPhysicalStoreCallback(
                string pvSystemStore,
                uint dwFlags,
                string pwszStoreName,
                ref CERT_PHYSICAL_STORE_INFO pStoreInfo,
                uint pvReserved,
                IntPtr pvArg)
            {
                tempPhysicalStoreList.Add(pwszStoreName);
                return true;
            }
        }
        
        #region GetPhysicalStores

        /// <summary>
        /// Gets the 'My' physical stores.
        /// </summary>
        /// <returns>An array of strings (never null).</returns>
        public static IEnumerable<string> GetPhysicalStores()
        {
            return GetPhysicalStores("My");
        }

        /// <summary>
        /// Gets the physical stores for the specified system store.
        /// </summary>
        /// <param name="systemStoreName">Name of the system store.</param>
        /// <returns>An array of strings (never null).</returns>
        public static IEnumerable<string> GetPhysicalStores(string systemStoreName)
        {
            return new EnumPhysicalStore(systemStoreName).GetPhysicalStores();
        }      

        #endregion

        #region GetSystemStores

        /// <summary>
        /// Gets the system stores for the current user location.
        /// </summary>
        /// <returns>An array of strings (never null).</returns>
        public static IEnumerable<CertificateStore> GetSystemStores()        
        {
            var names = new EnumSystemStore(CapiConstants.CERT_SYSTEM_STORE_CURRENT_USER).GetSystemStores();
            return names.Select(name => new CertificateStore(name, CertificateStoreLocation.FromId(CapiConstants.CERT_SYSTEM_STORE_CURRENT_USER_ID)));
        }

        /// <summary>
        /// Gets the system stores present in the specified Location.
        /// </summary>
        /// <param name="storeLocation">The System Stores Location.</param>
        /// <returns>An array of strings (never null).</returns>
        public static IEnumerable<CertificateStore> GetSystemStores(StoreLocation storeLocation)
        {
            return GetSystemStores(CertificateStoreLocation.FromStoreLocation(storeLocation));
        }

        /// <summary>
        /// Gets the system stores present in the specified Location.
        /// </summary>
        /// <param name="systemStoreLocation">The System Stores Location.</param>
        /// <returns>An array of strings (never null).</returns>
        public static IEnumerable<CertificateStore> GetSystemStores(CertificateStoreLocation systemStoreLocation)
        {
            var names = new EnumSystemStore(systemStoreLocation.Flags).GetSystemStores();
            return names.Select(name => new CertificateStore(name, systemStoreLocation));
        }

        public static CertificateStore GetCertificateStore(string storeName, StoreLocation storeLocation)
        {
            return GetCertificateStore(storeName, CertificateStoreLocation.FromStoreLocation(storeLocation));
        }

        public static CertificateStore GetCertificateStore(string storeName, CertificateStoreLocation systemStoreLocation)
        {
            return new CertificateStore(storeName, systemStoreLocation);
        }

        #endregion

        #region GetSystemStoreLocations
        
        /// <summary>
        /// Gets the system stores locations.
        /// </summary>
        /// <returns>An enumeration of <see cref="SystemStoreLocationInfo"/> objects (never null).</returns>
        public static IEnumerable<CertificateStoreLocation> GetSystemStoreLocations()
        {
            var locations = new List<SystemStoreLocationInfo>();
            var ok = NativeMethods.CertEnumSystemStoreLocation((uint)0, IntPtr.Zero,
                (pvszStoreLocations, dwFlags, pvReserved, pvArg) =>
                {
                    locations.Add(new SystemStoreLocationInfo()
                    {
                        Name = pvszStoreLocations,
                        Flags = dwFlags
                    });
                    return true;
                });

            if (ok) return locations.Select(info => new CertificateStoreLocation(info));
            else return new CertificateStoreLocation[] { };
        }

        #endregion
                
        public static string LocalizeName(string name)
        {
            return NativeMethods.CryptFindLocalizedName(name);
        }
    }
}
