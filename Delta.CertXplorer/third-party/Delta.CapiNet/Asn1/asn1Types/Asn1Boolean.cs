using System;

namespace Delta.CapiNet.Asn1
{
    public class Asn1Boolean : Asn1Object
    {
        private bool boolean;

        internal Asn1Boolean(Asn1Document document, TaggedObject content, Asn1Object parentObject)
            : base(document, content, parentObject)
        {
            boolean = content.Workload[0] == 0xFF;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is true.
        /// </summary>
        /// <value><c>true</c> if this instance is true; otherwise, <c>false</c>.</value>
        public bool Value { get { return boolean; } }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Boolean: {0}", boolean);
        }
    }
}
