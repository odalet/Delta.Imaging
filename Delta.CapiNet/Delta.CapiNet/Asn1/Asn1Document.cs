using System;
using System.Collections.Generic;

namespace Delta.CapiNet.Asn1
{
    public class Asn1Document
    {
        private const byte tagMask = 0x1F;
        private const byte classMask = 0xC0;

        public Asn1Document(byte[] data, bool parseOctetStrings, bool showInvalidTaggedObjects)
        {
            Data = data;
            ParseOctetStrings = parseOctetStrings;
            ShowInvalidTaggedObjects = showInvalidTaggedObjects;

            var taggedObjects = CreateTaggedObjects(data);
            if (taggedObjects != null && taggedObjects.Length > 0)
            {
                var objects = new List<Asn1Object>();
                foreach (var taggedObject in taggedObjects)
                {
                    var asn1 = CreateAsn1Object(this, taggedObject, null);
                    if (asn1 != null) objects.Add(asn1);
                }

                Nodes = objects.ToArray();
            }
            else Nodes = new Asn1Object[0];
        }

        public byte[] Data
        {
            get;
            private set;
        }

        public bool ParseOctetStrings
        {
            get;
            private set;
        }

        public bool ShowInvalidTaggedObjects
        {
            get;
            private set;
        }

        public Asn1Object[] Nodes
        {
            get;
            protected set;
        }

        #region Helpers

        private static TaggedObject[] CreateTaggedObjects(byte[] data)
        {
            return TaggedObject.CreateObjects(data, 0, data.Length);
        }

        private static bool IsUniversalTag(ushort tag)
        {
            return (tag & (ushort)Asn1Tag.ClassMask) == (ushort)Asn1Tag.Universal && tag < (ushort)0x100;
        }

        internal static byte GetAsn1ClassValue(ushort tag)
        {
            return (byte)(tag & (ushort)Asn1Tag.ClassMask);
        }

        private static byte GetAsn1TagValue(ushort tag)
        {
            return (byte)(tag & (ushort)Asn1Tag.TagMask);
        }

        private static bool IsInvalidTaggedObject(TaggedObject content)
        {
            return content == null || content is InvalidTaggedObject;
        }

        #endregion

        internal static Asn1Object CreateAsn1Object(
            Asn1Document document, TaggedObject content, Asn1Object parent)
        {
            Asn1Object result = null;
            try
            {
                if (document.ShowInvalidTaggedObjects && IsInvalidTaggedObject(content))
                    return new Asn1InvalidObject(document, (InvalidTaggedObject)content, parent);

                var tagValue = content.Tag.Value;                
                if (!IsUniversalTag(tagValue)) return new Asn1ContextSpecific(document, content, parent);

                var asn1TagValue = GetAsn1TagValue(tagValue);
                switch (asn1TagValue)
                {
                    case (int)Asn1Tag.Boolean:
                        return new Asn1Boolean(document, content, parent);
                    case (int)Asn1Tag.Integer:
                        return new Asn1Integer(document, content, parent);
                    case (int)Asn1Tag.BitString:
                        return new Asn1BitString(document, content, parent);
                    case (int)Asn1Tag.OctetString:
                        return new Asn1OctetString(document, content, parent);
                    case (int)Asn1Tag.Null:
                        return new Asn1Null(document, content, parent);
                    case (int)Asn1Tag.ObjectIdentifier:
                        return new Asn1Oid(document, content, parent);
                    case (int)Asn1Tag.Utf8String:
                        return new Asn1Utf8String(document, content, parent);
                    case (int)Asn1Tag.Sequence:
                        return new Asn1Sequence(document, content, parent);
                    case (int)Asn1Tag.Set:
                        return new Asn1Set(document, content, parent);
                    case (int)Asn1Tag.NumericString:
                        return new Asn1NumericString(document, content, parent);
                    case (int)Asn1Tag.PrintableString:
                        return new Asn1PrintableString(document, content, parent);
                    case (int)Asn1Tag.UtcTime:
                        return new Asn1UtcTime(document, content, parent);
                }
            }
            catch (Exception ex)
            {
                // Could not create a "real" ASN1 object --> return the special "Unsupported" object.
                Globals.LogException(ex);
                result = new Asn1Unsupported(document, content, parent, ex);
            }

            return result;
        }
    }
}
