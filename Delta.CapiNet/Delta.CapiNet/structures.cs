using Delta.CapiNet.Internals;

namespace Delta.CapiNet
{
    internal struct SystemStoreLocationInfo
    {
        public string Name;
        public uint Flags;

        public uint GetStoreLocationId() { return FlagsToId(Flags); }

        public void SetFlags(uint storeLocationId) { Flags = IdToFlags(storeLocationId); }

        internal static uint FlagsToId(uint flags)
        {
            return (uint)((int)flags >> (int)CapiConstants.CERT_SYSTEM_STORE_LOCATION_SHIFT);
        }

        internal static uint IdToFlags(uint id)
        {
            return (uint)((int)id << (int)CapiConstants.CERT_SYSTEM_STORE_LOCATION_SHIFT);
        }
    }
}
