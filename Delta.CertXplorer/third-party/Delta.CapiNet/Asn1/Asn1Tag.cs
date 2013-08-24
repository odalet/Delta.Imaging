using System;

namespace Delta.CapiNet.Asn1
{
    internal enum Asn1Tag : byte
    {
        // Type
        Boolean = 0x01,
        Integer = 0x02,
        BitString = 0x03,
        OctetString = 0x04,
        Null = 0x05,
        ObjectIdentifier = 0x06,
        ObjectDescriptor = 0x07,
        External = 0x08,
        Real = 0x09,
        Enumerated = 0x0a,
        Utf8String = 0x0c,
        Sequence = 0x10,
        Set = 0x11,
        NumericString = 0x12,
        PrintableString = 0x13,
        T61String = 0x14,
        VideoTextString = 0x15,
        Ia5String = 0x16,
        UtcTime = 0x17,
        GeneralizedTime = 0x18,
        GraphicString = 0x19,
        VisibleString = 0x1a,
        GeneralString = 0x1b,
        UniversalString = 0x1c,
        BmpString = 0x1E,

        // Class
        Universal = 0x00,
        Constructed = 0x20,
        Application = 0x40,
        ContextSpecific = 0x80,
        Private = 0xC0,

        TagMask = 0x1F,
        ClassMask = 0xC0
    }
}
