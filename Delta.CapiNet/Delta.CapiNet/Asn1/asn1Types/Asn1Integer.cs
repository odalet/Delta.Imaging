using System;

namespace Delta.CapiNet.Asn1
{
    public class Asn1Integer : Asn1Object
    {
        internal Asn1Integer(Asn1Document document, TaggedObject content, Asn1Object parentObject)
            : base(document, content, parentObject)
        {
            long value = 0L;
            foreach (byte element in content.Workload)
            {
                value <<= 8;
                value += element;
            }

            Value = value;
        }

        /// <summary>
        /// Gets this instance's value.
        /// </summary>
        /// <value>The value.</value>
        public long Value
        {
            get; 
            private set;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Integer: {0} (0x{0:X})", Value);
        }
    }
}
