using System;
using System.Linq;

namespace Delta.CapiNet.Asn1
{
    public class Asn1BitString : Asn1Object
    {
        private byte unusedBytes = 0;
        private byte[] bitString = null;

        internal Asn1BitString(Asn1Document document, TaggedObject content, Asn1Object parentObject)
            : base(document, content, parentObject)
        {
            unusedBytes = content.Workload[0];
            bitString = content.Workload.Skip(1).ToArray();
        }

        public byte UnusedBytes { get { return unusedBytes; } }

        public byte[] BitString { get { return bitString; } }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format(
                "BitString ({0} padding bytes):\r\n{1}",
                unusedBytes,
                bitString.ToFormattedString());
        }
    }
}
