using System;
using System.Text;

namespace Delta.CapiNet.Asn1
{
    public class Asn1Utf8String : Asn1Object
    {
        private string utf8 = string.Empty;

        internal Asn1Utf8String(Asn1Document document, TaggedObject content, Asn1Object parentObject)
            : base(document, content, parentObject)
        {
            utf8 = Encoding.UTF8.GetString(content.Workload); 
        }

        /// <summary>
        /// Gets this instance's value.
        /// </summary>
        /// <value>The value.</value>
        public string Value { get { return utf8; } }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Utf8String: {0}", utf8);
        }
    }
}
