using System;

namespace Delta.CapiNet.Asn1
{
    public class Asn1Null : Asn1Object
    {
        private bool warning = false;

        internal Asn1Null(Asn1Document document, TaggedObject content, Asn1Object parentObject)
            : base(document, content, parentObject)
        {
            warning = content.WorkloadLength != 0;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (warning) return ("Null: WARNING - Should be empty.");
            else return "Null";
        }
    }
}
