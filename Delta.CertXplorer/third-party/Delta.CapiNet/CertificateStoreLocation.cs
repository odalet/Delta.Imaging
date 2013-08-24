using System;
using System.Security.Cryptography.X509Certificates;

namespace Delta.CapiNet
{
    /// <summary>
    /// Represents Locations of Certificate Stores.
    /// </summary>
    public class CertificateStoreLocation
    {
        private SystemStoreLocationInfo locationInfo = new SystemStoreLocationInfo();

        /// <summary>
        /// Prevents a default instance of the <see cref="CertificateStoreLocation"/> class from being created.
        /// </summary>
        private CertificateStoreLocation() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CertificateStoreLocation"/> class.
        /// </summary>
        /// <param name="systemStoreLocationInfo">The system store location info.</param>
        internal CertificateStoreLocation(SystemStoreLocationInfo systemStoreLocationInfo) 
        {
            locationInfo = systemStoreLocationInfo;
        }

        internal static CertificateStoreLocation FromId(uint id)
        {
            return new CertificateStoreLocation(
                new SystemStoreLocationInfo()
                {
                    Flags = (uint)id << 16,
                    Name = id.ToString()
                });
        }

        public static CertificateStoreLocation FromStoreLocation(StoreLocation location)
        {
            return new CertificateStoreLocation(
                new SystemStoreLocationInfo()
                {
                    Flags = (uint)location << 16,
                    Name = location.ToString()
                });
        }

        public StoreLocation ToStoreLocation()
        {
            int id = (int)locationInfo.GetStoreLocationId();
            if (Enum.IsDefined(typeof(StoreLocation), id))
                return (StoreLocation)Enum.ToObject(typeof(StoreLocation), id);
            else throw new InvalidOperationException(string.Format(
                "This instance's current value can't be represented as a {0}.",
                typeof(StoreLocation)));
        }

        public string Name
        {
            get { return locationInfo.Name; }
        }

        public uint Id { get { return locationInfo.GetStoreLocationId(); } }

        internal uint Flags { get { return locationInfo.Flags; } }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(Name)) return "?";
            return Name;
        }
    }
}
