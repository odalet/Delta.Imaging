using System;
using System.Collections.Generic;

namespace Delta.CapiNet.Asn1
{
    public abstract class Asn1StructuredObject : Asn1Object
    {
        internal Asn1StructuredObject(Asn1Document document, TaggedObject content, Asn1Object parentObject)
            : base(document, content, parentObject) 
        {
            ParseContent();
        }

        public Asn1Object[] Nodes
        {
            get;
            protected set;
        }

        protected virtual void ParseContent()
        {
            var taggedObjects = TaggedObject.CreateObjects(
                base.TaggedObject.AllData, 
                base.TaggedObject.WorkloadOffset, 
                base.TaggedObject.WorkloadLength);
            if (taggedObjects != null && taggedObjects.Length > 0)
            {
                var objects = new List<Asn1Object>();
                foreach (var taggedObject in taggedObjects)
                {
                    var asn1 = Asn1Document.CreateAsn1Object(base.Document, taggedObject, this);
                    if (asn1 != null) objects.Add(asn1);
                }

                Nodes = objects.ToArray();
            }
            else Nodes = new Asn1Object[0];
        }
    }
}
