using System;

namespace Delta.CapiNet.Asn1
{
    public class Asn1NumericString : Asn1Object
    {
        internal Asn1NumericString(Asn1Document document, TaggedObject content, Asn1Object parentObject)
            : base(document, content, parentObject) { }

        /// <summary>
        /// Gets this instance's value.
        /// </summary>
        /// <value>The value.</value>
        public byte[] Value { get { return base.Workload; } }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Asn1NumericString: {0}", Value.ToFormattedString());
        } 
    }
}
