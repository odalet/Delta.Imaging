using System;

namespace Delta.CapiNet.Asn1
{
    public abstract class Asn1Object
    {        
        internal Asn1Object(Asn1Document document, TaggedObject content, Asn1Object parentObject)
        {
            if (content == null) throw new ArgumentNullException("content");
            TaggedObject = content;

            Document = document;
            Parent = parentObject;
        }

        public Asn1Document Document
        {
            get;
            private set;
        }

        public Asn1Object Parent
        {
            get;
            private set;
        }

        protected internal TaggedObject TaggedObject { get; private set; }

        public byte[] RawData
        {
            get { return TaggedObject.RawData; }
        }

        public byte[] Workload
        {
            get { return TaggedObject.Workload; }
        }

        public int WorkloadOffset
        {
            get { return TaggedObject.WorkloadOffset; }
        }
    }
}
