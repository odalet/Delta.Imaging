using System;

namespace TestExif.Drawing.Imaging
{
    public enum ExifTagId : int
    {
        Unknown = 0,
        ExifIfd = 0x8769,
        GpsIfd = 0x8825,

        NewSubfileType = 0xFE,
        SubfileType = 0xFF,
        ImageWidth = 0x100,
        ImageHeight = 0x101,
        BitsPerSample = 0x102
    }
}
