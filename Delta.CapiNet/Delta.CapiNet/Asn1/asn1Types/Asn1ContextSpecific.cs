using System;

namespace Delta.CapiNet.Asn1
{
    // TODO: rename this node; it can contain context specific tags but in fact any tag that is not a universal tag
    // ie, application, constructed or private tags (see Asn1Tag & Asn1Document.IsUniversalTag)
    public class Asn1ContextSpecific : Asn1StructuredObject
    {
        private Asn1Tag asn1Class = Asn1Tag.ContextSpecific;

        internal Asn1ContextSpecific(Asn1Document document, TaggedObject content, Asn1Object parentObject)
            : base(document, content, parentObject)
        {
            asn1Class = (Asn1Tag)Asn1Document.GetAsn1ClassValue(content.Tag.Value);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("ContextSpecific: {0}", asn1Class);
        }
    }
}
