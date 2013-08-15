using System;

namespace TestExif.Drawing.Imaging
{
    public enum ExifPropertyType : short
    {
        Unknown = 0x00,
        Byte = 0x01,        // translate to byte
        Ascii = 0x02,       // ascii 7-bit string
        Short = 0x03,       // translate to ushort
        Long = 0x04,        // translate to uint
        Rational = 0x05,    // translate to uint/uint
        Undefined = 0x07,
        SLong = 0x09,       // translate to int
        SRational = 0x0A    // translate to int/int
    }
}
