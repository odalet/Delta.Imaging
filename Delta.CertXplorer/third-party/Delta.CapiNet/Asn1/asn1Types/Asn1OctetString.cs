using System;

namespace Delta.CapiNet.Asn1
{
    // According to PKCS, octet strings may be structures...
    // This is why we try to find children in it.
    public class Asn1OctetString : Asn1StructuredObject
    {
        internal Asn1OctetString(Asn1Document document, TaggedObject content, Asn1Object parentObject)
            : base(document, content, parentObject) { } 

        protected override void ParseContent()
        {
            if (base.Document != null && base.Document.ParseOctetStrings)
                base.ParseContent();
            else base.Nodes = new Asn1Object[0];
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("OctetString: {0}", base.Workload.ToFormattedString());
        }
    }
}
