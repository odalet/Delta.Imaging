using System;
using System.Globalization;
using System.Text;

namespace Delta.CapiNet.Asn1
{
    public class Asn1UtcTime : Asn1Object
    {
        private DateTime dateTime = DateTime.MinValue;

        private static readonly string[] UtcFormats = new string[]
        {
            "yyMMddHHmmssZ",
            "yyMMddHHmmZ",
            "yyMMddHHmmz",
            "yyMMddHHmmssz"
        };

        internal Asn1UtcTime(Asn1Document document, TaggedObject content, Asn1Object parentObject)
            : base(document, content, parentObject)
        {
            var ascii = Encoding.ASCII.GetString(content.Workload);
            dateTime = DateTime.ParseExact(ascii,
                UtcFormats, CultureInfo.InvariantCulture, DateTimeStyles.None);
        }

        /// <summary>
        /// Gets this instance's value.
        /// </summary>
        /// <value>The value.</value>
        public DateTime Value { get { return dateTime; } }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        /// <remarks>
        /// This forces inheritors to explicitly implement ToString.
        /// </remarks>
        public override string ToString()
        {
            if (dateTime == DateTime.MinValue) return string.Empty;
            else return dateTime.ToString("u");
        }
    }
}
