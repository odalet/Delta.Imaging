using System;

namespace Delta.CapiNet.Asn1
{
    internal sealed class InvalidTaggedObject : TaggedObject
    {
        public InvalidTaggedObject(byte[] data, int offset, int length) :
            base(data, offset, length) { }
    }

    public sealed class Asn1InvalidObject : Asn1Object
    {
        internal Asn1InvalidObject(
            Asn1Document document, InvalidTaggedObject content, Asn1Object parentObject) :
            base(document, content, parentObject)
        {
            InvalidTaggedObject = content;
        }

        internal InvalidTaggedObject InvalidTaggedObject
        {
            get;
            private set;
        }
    }
}
