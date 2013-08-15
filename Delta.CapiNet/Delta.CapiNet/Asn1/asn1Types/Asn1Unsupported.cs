using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Delta.CapiNet.Asn1
{
    public class Asn1Unsupported : Asn1Object
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Asn1Unsupported"/> class.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="content">The content.</param>
        /// <param name="parentObject">The parent object.</param>
        internal Asn1Unsupported(Asn1Document document, TaggedObject content, Asn1Object parentObject)
            : this(document, content, parentObject, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Asn1Unsupported"/> class.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="content">The content.</param>
        /// <param name="parentObject">The parent object.</param>
        /// <param name="exception">The exception.</param>
        internal Asn1Unsupported(Asn1Document document, TaggedObject content, Asn1Object parentObject, Exception exception)
            : base(document, content, parentObject) 
        {
            Exception = exception;
        }

        /// <summary>
        /// Gets or sets the exception that was the cause of the unsupported ASN1 tag creation.
        /// </summary>
        /// <value>The exception.</value>
        private Exception Exception { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var baseMessage = string.Format("Unsupported tag: 0x{0:X2}", base.TaggedObject.Tag.Value);
            if (Exception == null)
                return baseMessage;
            else return string.Format("{0}\r\n\r\nException:\r\n{1}", baseMessage, Exception);
        }
    }
}
