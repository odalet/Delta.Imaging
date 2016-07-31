///
///			http://www.goheer.com
///			visist goheer.com for latest version of this control
///
///

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Text;

namespace Goheer.Exif
{
    public class ExifExtractor : IEnumerable<KeyValuePair<string, string>>
    {
        /// <summary>
        /// Summary description for translation.
        /// </summary>
        private sealed class Translation : Dictionary<int, string>
        {
            /// <summary>
            /// 
            /// </summary>
            public Translation()
            {
                Add(0x8769, "Exif IFD");
                Add(0x8825, "Gps IFD");
                Add(0xFE, "New Subfile Type");
                Add(0xFF, "Subfile Type");
                Add(0x100, "Image Width");
                Add(0x101, "Image Height");
                Add(0x102, "Bits Per Sample");
                Add(0x103, "Compression");
                Add(0x106, "Photometric Interp");
                Add(0x107, "Thresh Holding");
                Add(0x108, "Cell Width");
                Add(0x109, "Cell Height");
                Add(0x10A, "Fill Order");
                Add(0x10D, "Document Name");
                Add(0x10E, "Image Description");
                Add(0x10F, "Equip Make");
                Add(0x110, "Equip Model");
                Add(0x111, "Strip Offsets");
                Add(0x112, "Orientation");
                Add(0x115, "Samples PerPixel");
                Add(0x116, "Rows Per Strip");
                Add(0x117, "Strip Bytes Count");
                Add(0x118, "Min Sample Value");
                Add(0x119, "Max Sample Value");
                Add(0x11A, "X Resolution");
                Add(0x11B, "Y Resolution");
                Add(0x11C, "Planar Config");
                Add(0x11D, "Page Name");
                Add(0x11E, "X Position");
                Add(0x11F, "Y Position");
                Add(0x120, "Free Offset");
                Add(0x121, "Free Byte Counts");
                Add(0x122, "Gray Response Unit");
                Add(0x123, "Gray Response Curve");
                Add(0x124, "T4 Option");
                Add(0x125, "T6 Option");
                Add(0x128, "Resolution Unit");
                Add(0x129, "Page Number");
                Add(0x12D, "Transfer Funcition");
                Add(0x131, "Software Used");
                Add(0x132, "Date Time");
                Add(0x13B, "Artist");
                Add(0x13C, "Host Computer");
                Add(0x13D, "Predictor");
                Add(0x13E, "White Point");
                Add(0x13F, "Primary Chromaticities");
                Add(0x140, "ColorMap");
                Add(0x141, "Halftone Hints");
                Add(0x142, "Tile Width");
                Add(0x143, "Tile Length");
                Add(0x144, "Tile Offset");
                Add(0x145, "Tile ByteCounts");
                Add(0x14C, "InkSet");
                Add(0x14D, "Ink Names");
                Add(0x14E, "Number Of Inks");
                Add(0x150, "Dot Range");
                Add(0x151, "Target Printer");
                Add(0x152, "Extra Samples");
                Add(0x153, "Sample Format");
                Add(0x154, "S Min Sample Value");
                Add(0x155, "S Max Sample Value");
                Add(0x156, "Transfer Range");
                Add(0x200, "JPEG Proc");
                Add(0x201, "JPEG InterFormat");
                Add(0x202, "JPEG InterLength");
                Add(0x203, "JPEG RestartInterval");
                Add(0x205, "JPEG LosslessPredictors");
                Add(0x206, "JPEG PointTransforms");
                Add(0x207, "JPEG QTables");
                Add(0x208, "JPEG DCTables");
                Add(0x209, "JPEG ACTables");
                Add(0x211, "YCbCr Coefficients");
                Add(0x212, "YCbCr Subsampling");
                Add(0x213, "YCbCr Positioning");
                Add(0x214, "REF Black White");
                Add(0x8773, "ICC Profile");
                Add(0x301, "Gamma");
                Add(0x302, "ICC Profile Descriptor");
                Add(0x303, "SRGB RenderingIntent");
                Add(0x320, "Image Title");
                Add(0x8298, "Copyright");
                Add(0x5001, "Resolution X Unit");
                Add(0x5002, "Resolution Y Unit");
                Add(0x5003, "Resolution X LengthUnit");
                Add(0x5004, "Resolution Y LengthUnit");
                Add(0x5005, "Print Flags");
                Add(0x5006, "Print Flags Version");
                Add(0x5007, "Print Flags Crop");
                Add(0x5008, "Print Flags Bleed Width");
                Add(0x5009, "Print Flags Bleed Width Scale");
                Add(0x500A, "Halftone LPI");
                Add(0x500B, "Halftone LPIUnit");
                Add(0x500C, "Halftone Degree");
                Add(0x500D, "Halftone Shape");
                Add(0x500E, "Halftone Misc");
                Add(0x500F, "Halftone Screen");
                Add(0x5010, "JPEG Quality");
                Add(0x5011, "Grid Size");
                Add(0x5012, "Thumbnail Format");
                Add(0x5013, "Thumbnail Width");
                Add(0x5014, "Thumbnail Height");
                Add(0x5015, "Thumbnail ColorDepth");
                Add(0x5016, "Thumbnail Planes");
                Add(0x5017, "Thumbnail RawBytes");
                Add(0x5018, "Thumbnail Size");
                Add(0x5019, "Thumbnail CompressedSize");
                Add(0x501A, "Color Transfer Function");
                Add(0x501B, "Thumbnail Data");
                Add(0x5020, "Thumbnail ImageWidth");
                Add(0x502, "Thumbnail ImageHeight");
                Add(0x5022, "Thumbnail BitsPerSample");
                Add(0x5023, "Thumbnail Compression");
                Add(0x5024, "Thumbnail PhotometricInterp");
                Add(0x5025, "Thumbnail ImageDescription");
                Add(0x5026, "Thumbnail EquipMake");
                Add(0x5027, "Thumbnail EquipModel");
                Add(0x5028, "Thumbnail StripOffsets");
                Add(0x5029, "Thumbnail Orientation");
                Add(0x502A, "Thumbnail SamplesPerPixel");
                Add(0x502B, "Thumbnail RowsPerStrip");
                Add(0x502C, "Thumbnail StripBytesCount");
                Add(0x502D, "Thumbnail ResolutionX");
                Add(0x502E, "Thumbnail ResolutionY");
                Add(0x502F, "Thumbnail PlanarConfig");
                Add(0x5030, "Thumbnail ResolutionUnit");
                Add(0x5031, "Thumbnail TransferFunction");
                Add(0x5032, "Thumbnail SoftwareUsed");
                Add(0x5033, "Thumbnail DateTime");
                Add(0x5034, "Thumbnail Artist");
                Add(0x5035, "Thumbnail WhitePoint");
                Add(0x5036, "Thumbnail PrimaryChromaticities");
                Add(0x5037, "Thumbnail YCbCrCoefficients");
                Add(0x5038, "Thumbnail YCbCrSubsampling");
                Add(0x5039, "Thumbnail YCbCrPositioning");
                Add(0x503A, "Thumbnail RefBlackWhite");
                Add(0x503B, "Thumbnail CopyRight");
                Add(0x5090, "Luminance Table");
                Add(0x5091, "Chrominance Table");
                Add(0x5100, "Frame Delay");
                Add(0x5101, "Loop Count");
                Add(0x5110, "Pixel Unit");
                Add(0x5111, "Pixel PerUnit X");
                Add(0x5112, "Pixel PerUnit Y");
                Add(0x5113, "Palette Histogram");
                Add(0x829A, "Exposure Time");
                Add(0x829D, "F-Number");
                Add(0x8822, "Exposure Prog");
                Add(0x8824, "Spectral Sense");
                Add(0x8827, "ISO Speed");
                Add(0x8828, "OECF");
                Add(0x9000, "Ver");
                Add(0x9003, "DTOrig");
                Add(0x9004, "DTDigitized");
                Add(0x9101, "CompConfig");
                Add(0x9102, "CompBPP");
                Add(0x9201, "Shutter Speed");
                Add(0x9202, "Aperture");
                Add(0x9203, "Brightness");
                Add(0x9204, "Exposure Bias");
                Add(0x9205, "MaxAperture");
                Add(0x9206, "SubjectDist");
                Add(0x9207, "Metering Mode");
                Add(0x9208, "LightSource");
                Add(0x9209, "Flash");
                Add(0x920A, "FocalLength");
                Add(0x927C, "Maker Note");
                Add(0x9286, "User Comment");
                Add(0x9290, "DTSubsec");
                Add(0x9291, "DTOrigSS");
                Add(0x9292, "DTDigSS");
                Add(0xA000, "FPXVer");
                Add(0xA001, "ColorSpace");
                Add(0xA002, "PixXDim");
                Add(0xA003, "PixYDim");
                Add(0xA004, "RelatedWav");
                Add(0xA005, "Interop");
                Add(0xA20B, "FlashEnergy");
                Add(0xA20C, "SpatialFR");
                Add(0xA20E, "FocalXRes");
                Add(0xA20F, "FocalYRes");
                Add(0xA210, "FocalResUnit");
                Add(0xA214, "Subject Loc");
                Add(0xA215, "Exposure Index");
                Add(0xA217, "Sensing Method");
                Add(0xA300, "FileSource");
                Add(0xA301, "SceneType");
                Add(0xA302, "CfaPattern");
                Add(0x0, "Gps Ver");
                Add(0x1, "Gps LatitudeRef");
                Add(0x2, "Gps Latitude");
                Add(0x3, "Gps LongitudeRef");
                Add(0x4, "Gps Longitude");
                Add(0x5, "Gps AltitudeRef");
                Add(0x6, "Gps Altitude");
                Add(0x7, "Gps GpsTime");
                Add(0x8, "Gps GpsSatellites");
                Add(0x9, "Gps GpsStatus");
                Add(0xA, "Gps GpsMeasureMode");
                Add(0xB, "Gps GpsDop");
                Add(0xC, "Gps SpeedRef");
                Add(0xD, "Gps Speed");
                Add(0xE, "Gps TrackRef");
                Add(0xF, "Gps Track");
                Add(0x10, "Gps ImgDirRef");
                Add(0x11, "Gps ImgDir");
                Add(0x12, "Gps MapDatum");
                Add(0x13, "Gps DestLatRef");
                Add(0x14, "Gps DestLat");
                Add(0x15, "Gps DestLongRef");
                Add(0x16, "Gps DestLong");
                Add(0x17, "Gps DestBearRef");
                Add(0x18, "Gps DestBear");
                Add(0x19, "Gps DestDistRef");
                Add(0x1A, "Gps DestDist");
            }
        }

        /// <summary>
        /// private class
        /// </summary>
        private sealed class Rational
        {
            private readonly int numerator;
            private readonly int denominator;

            public Rational(int n, int d)
            {
                numerator = n;
                denominator = d;
                Simplify(ref numerator, ref denominator);
            }

            public Rational(uint n, uint d)
            {
                numerator = Convert.ToInt32(n);
                denominator = Convert.ToInt32(d);

                Simplify(ref numerator, ref denominator);
            }

            public override string ToString() => $"{numerator}/{denominator}";

            public double ToDouble()
            {
                if (denominator == 0)
                    return 0.0;

                return Math.Round(Convert.ToDouble(numerator) / Convert.ToDouble(denominator), 2);
            }

            private void Simplify(ref int a, ref int b)
            {
                if (a == 0 || b == 0) return;

                int gcd = Euclid(a, b);
                a /= gcd;
                b /= gcd;
            }

            private int Euclid(int a, int b) => b == 0 ? a : Euclid(b, a % b);
        }
        
        private readonly Translation myHash;
        private readonly Dictionary<string, string> properties;
        private readonly string sp;
        private string data;
        
        public ExifExtractor(string file, string separator) : this(separator)
        {
            BuildDB(GetExifProperties(file));
        }

        private ExifExtractor(string separator)
        {
            properties = new Dictionary<string, string>();
            myHash = new Translation();
            sp = separator;            
        }

        /// <summary>
        /// Get the individual property value by supplying property name
        /// These are the valid property names :
        /// 
        /// "Exif IFD"
        /// "Gps IFD"
        /// "New Subfile Type"
        /// "Subfile Type"
        /// "Image Width"
        /// "Image Height"
        /// "Bits Per Sample"
        /// "Compression"
        /// "Photometric Interp"
        /// "Thresh Holding"
        /// "Cell Width"
        /// "Cell Height"
        /// "Fill Order"
        /// "Document Name"
        /// "Image Description"
        /// "Equip Make"
        /// "Equip Model"
        /// "Strip Offsets"
        /// "Orientation"
        /// "Samples PerPixel"
        /// "Rows Per Strip"
        /// "Strip Bytes Count"
        /// "Min Sample Value"
        /// "Max Sample Value"
        /// "X Resolution"
        /// "Y Resolution"
        /// "Planar Config"
        /// "Page Name"
        /// "X Position"
        /// "Y Position"
        /// "Free Offset"
        /// "Free Byte Counts"
        /// "Gray Response Unit"
        /// "Gray Response Curve"
        /// "T4 Option"
        /// "T6 Option"
        /// "Resolution Unit"
        /// "Page Number"
        /// "Transfer Funcition"
        /// "Software Used"
        /// "Date Time"
        /// "Artist"
        /// "Host Computer"
        /// "Predictor"
        /// "White Point"
        /// "Primary Chromaticities"
        /// "ColorMap"
        /// "Halftone Hints"
        /// "Tile Width"
        /// "Tile Length"
        /// "Tile Offset"
        /// "Tile ByteCounts"
        /// "InkSet"
        /// "Ink Names"
        /// "Number Of Inks"
        /// "Dot Range"
        /// "Target Printer"
        /// "Extra Samples"
        /// "Sample Format"
        /// "S Min Sample Value"
        /// "S Max Sample Value"
        /// "Transfer Range"
        /// "JPEG Proc"
        /// "JPEG InterFormat"
        /// "JPEG InterLength"
        /// "JPEG RestartInterval"
        /// "JPEG LosslessPredictors"
        /// "JPEG PointTransforms"
        /// "JPEG QTables"
        /// "JPEG DCTables"
        /// "JPEG ACTables"
        /// "YCbCr Coefficients"
        /// "YCbCr Subsampling"
        /// "YCbCr Positioning"
        /// "REF Black White"
        /// "ICC Profile"
        /// "Gamma"
        /// "ICC Profile Descriptor"
        /// "SRGB RenderingIntent"
        /// "Image Title"
        /// "Copyright"
        /// "Resolution X Unit"
        /// "Resolution Y Unit"
        /// "Resolution X LengthUnit"
        /// "Resolution Y LengthUnit"
        /// "Print Flags"
        /// "Print Flags Version"
        /// "Print Flags Crop"
        /// "Print Flags Bleed Width"
        /// "Print Flags Bleed Width Scale"
        /// "Halftone LPI"
        /// "Halftone LPIUnit"
        /// "Halftone Degree"
        /// "Halftone Shape"
        /// "Halftone Misc"
        /// "Halftone Screen"
        /// "JPEG Quality"
        /// "Grid Size"
        /// "Thumbnail Format"
        /// "Thumbnail Width"
        /// "Thumbnail Height"
        /// "Thumbnail ColorDepth"
        /// "Thumbnail Planes"
        /// "Thumbnail RawBytes"
        /// "Thumbnail Size"
        /// "Thumbnail CompressedSize"
        /// "Color Transfer Function"
        /// "Thumbnail Data"
        /// "Thumbnail ImageWidth"
        /// "Thumbnail ImageHeight"
        /// "Thumbnail BitsPerSample"
        /// "Thumbnail Compression"
        /// "Thumbnail PhotometricInterp"
        /// "Thumbnail ImageDescription"
        /// "Thumbnail EquipMake"
        /// "Thumbnail EquipModel"
        /// "Thumbnail StripOffsets"
        /// "Thumbnail Orientation"
        /// "Thumbnail SamplesPerPixel"
        /// "Thumbnail RowsPerStrip"
        /// "Thumbnail StripBytesCount"
        /// "Thumbnail ResolutionX"
        /// "Thumbnail ResolutionY"
        /// "Thumbnail PlanarConfig"
        /// "Thumbnail ResolutionUnit"
        /// "Thumbnail TransferFunction"
        /// "Thumbnail SoftwareUsed"
        /// "Thumbnail DateTime"
        /// "Thumbnail Artist"
        /// "Thumbnail WhitePoint"
        /// "Thumbnail PrimaryChromaticities"
        /// "Thumbnail YCbCrCoefficients"
        /// "Thumbnail YCbCrSubsampling"
        /// "Thumbnail YCbCrPositioning"
        /// "Thumbnail RefBlackWhite"
        /// "Thumbnail CopyRight"
        /// "Luminance Table"
        /// "Chrominance Table"
        /// "Frame Delay"
        /// "Loop Count"
        /// "Pixel Unit"
        /// "Pixel PerUnit X"
        /// "Pixel PerUnit Y"
        /// "Palette Histogram"
        /// "Exposure Time"
        /// "F-Number"
        /// "Exposure Prog"
        /// "Spectral Sense"
        /// "ISO Speed"
        /// "OECF"
        /// "Ver"
        /// "DTOrig"
        /// "DTDigitized"
        /// "CompConfig"
        /// "CompBPP"
        /// "Shutter Speed"
        /// "Aperture"
        /// "Brightness"
        /// "Exposure Bias"
        /// "MaxAperture"
        /// "SubjectDist"
        /// "Metering Mode"
        /// "LightSource"
        /// "Flash"
        /// "FocalLength"
        /// "Maker Note"
        /// "User Comment"
        /// "DTSubsec"
        /// "DTOrigSS"
        /// "DTDigSS"
        /// "FPXVer"
        /// "ColorSpace"
        /// "PixXDim"
        /// "PixYDim"
        /// "RelatedWav"
        /// "Interop"
        /// "FlashEnergy"
        /// "SpatialFR"
        /// "FocalXRes"
        /// "FocalYRes"
        /// "FocalResUnit"
        /// "Subject Loc"
        /// "Exposure Index"
        /// "Sensing Method"
        /// "FileSource"
        /// "SceneType"
        /// "CfaPattern"
        /// "Gps Ver"
        /// "Gps LatitudeRef"
        /// "Gps Latitude"
        /// "Gps LongitudeRef"
        /// "Gps Longitude"
        /// "Gps AltitudeRef"
        /// "Gps Altitude"
        /// "Gps GpsTime"
        /// "Gps GpsSatellites"
        /// "Gps GpsStatus"
        /// "Gps GpsMeasureMode"
        /// "Gps GpsDop"
        /// "Gps SpeedRef"
        /// "Gps Speed"
        /// "Gps TrackRef"
        /// "Gps Track"
        /// "Gps ImgDirRef"
        /// "Gps ImgDir"
        /// "Gps MapDatum"
        /// "Gps DestLatRef"
        /// "Gps DestLat"
        /// "Gps DestLongRef"
        /// "Gps DestLong"
        /// "Gps DestBearRef"
        /// "Gps DestBear"
        /// "Gps DestDistRef"
        /// "Gps DestDist"
        /// </summary>
        public string this[string index] => properties[index];

        public bool ContainsKey(string key) => properties.ContainsKey(key);

        public int Count => properties.Count;
        
        public static PropertyItem[] GetExifProperties(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var image = Image.FromStream(stream, useEmbeddedColorManagement: true, validateImageData: false))
                return image.PropertyItems;
        }

        /// <summary>
        /// 
        /// </summary>
        private void BuildDB(PropertyItem[] propertyItems)
        {
            properties.Clear();
            data = string.Empty;

            foreach (var p in propertyItems)
            {
                var v = string.Empty;

                var name = myHash.ContainsKey(p.Id) ? myHash[p.Id] : "0x" + p.Id.ToString("X");
                data += $"{name}: ";

                //1 = BYTE An 8-bit unsigned integer.,
                if (p.Type == 0x1) v = p.Value[0].ToString();
                else if (p.Type == 0x2) //2 = ASCII An 8-bit byte containing one 7-bit ASCII code. The final byte is terminated with NULL.,
                {
                    // ODT: remove \0
                    v = Encoding.ASCII.GetString(p.Value);
                    if (string.IsNullOrEmpty(v)) v = string.Empty;
                    else if (v[v.Length - 1] == '\0')
                        v = v.Substring(0, v.Length - 1);
                }
                else if (p.Type == 0x3) //3 = SHORT A 16-bit (2 -byte) unsigned integer,
                {
                    // orientation // lookup table					
                    switch (p.Id)
                    {
                        case 0x8827: // ISO
                            v = "ISO-" + ConvertToUInt16(p.Value);
                            break;
                        case 0xA217: // sensing method
                            {
                                switch (ConvertToUInt16(p.Value))
                                {
                                    case 1: v = "Not defined"; break;
                                    case 2: v = "One-chip color area sensor"; break;
                                    case 3: v = "Two-chip color area sensor"; break;
                                    case 4: v = "Three-chip color area sensor"; break;
                                    case 5: v = "Color sequential area sensor"; break;
                                    case 7: v = "Trilinear sensor"; break;
                                    case 8: v = "Color sequential linear sensor"; break;
                                    default: v = " reserved"; break;
                                }
                            }
                            break;
                        case 0x8822: // aperture 
                            switch (ConvertToUInt16(p.Value))
                            {
                                case 0: v = "Not defined"; break;
                                case 1: v = "Manual"; break;
                                case 2: v = "Normal program"; break;
                                case 3: v = "Aperture priority"; break;
                                case 4: v = "Shutter priority"; break;
                                case 5: v = "Creative program (biased toward depth of field)"; break;
                                case 6: v = "Action program (biased toward fast shutter speed)"; break;
                                case 7: v = "Portrait mode (for closeup photos with the background out of focus)"; break;
                                case 8: v = "Landscape mode (for landscape photos with the background in focus)"; break;
                                default: v = "reserved"; break;
                            }
                            break;
                        case 0x9207: // metering mode
                            switch (ConvertToUInt16(p.Value))
                            {
                                case 0: v = "unknown"; break;
                                case 1: v = "Average"; break;
                                case 2: v = "CenterWeightedAverage"; break;
                                case 3: v = "Spot"; break;
                                case 4: v = "MultiSpot"; break;
                                case 5: v = "Pattern"; break;
                                case 6: v = "Partial"; break;
                                case 255: v = "Other"; break;
                                default: v = "reserved"; break;
                            }
                            break;
                        case 0x9208: // light source
                            {
                                switch (ConvertToUInt16(p.Value))
                                {
                                    case 0: v = "unknown"; break;
                                    case 1: v = "Daylight"; break;
                                    case 2: v = "Fluorescent"; break;
                                    case 3: v = "Tungsten"; break;
                                    case 17: v = "Standard light A"; break;
                                    case 18: v = "Standard light B"; break;
                                    case 19: v = "Standard light C"; break;
                                    case 20: v = "D55"; break;
                                    case 21: v = "D65"; break;
                                    case 22: v = "D75"; break;
                                    case 255: v = "other"; break;
                                    default: v = "reserved"; break;
                                }
                            }
                            break;
                        case 0x9209:
                            {
                                switch (ConvertToUInt16(p.Value))
                                {
                                    case 0: v = "Flash did not fire"; break;
                                    case 1: v = "Flash fired"; break;
                                    case 5: v = "Strobe return light not detected"; break;
                                    case 7: v = "Strobe return light detected"; break;
                                    default: v = "reserved"; break;
                                }
                            }
                            break;
                        default:
                            v = ConvertToUInt16(p.Value).ToString();
                            break;
                    }
                }
                else if (p.Type == 0x4) //4 = LONG A 32-bit (4 -byte) unsigned integer,
                {
                    // orientation // lookup table					
                    v = ConvertToUInt32(p.Value).ToString();
                }
                else if (p.Type == 0x5) //5 = RATIONAL Two LONGs. The first LONG is the numerator and the second LONG expresses the//denominator.,
                {
                    var n = new byte[p.Len / 2];
                    var d = new byte[p.Len / 2];
                    Array.Copy(p.Value, 0, n, 0, p.Len / 2);
                    Array.Copy(p.Value, p.Len / 2, d, 0, p.Len / 2);
                    var a = ConvertToUInt32(n);
                    var b = ConvertToUInt32(d);
                    var r = new Rational(a, b);
                    //
                    //convert here
                    //
                    switch (p.Id)
                    {
                        case 0x9202: // aperture
                            v = "F/" + Math.Round(Math.Pow(Math.Sqrt(2), r.ToDouble()), 2).ToString(CultureInfo.InvariantCulture);
                            break;
                        case 0x920A:
                        case 0x829A:
                            v = r.ToDouble().ToString(CultureInfo.InvariantCulture);
                            break;
                        case 0x829D: // F-number
                            v = "F/" + r.ToDouble().ToString(CultureInfo.InvariantCulture);
                            break;
                        default:
                            v = r.ToString();
                            break;
                    }

                }
                //7 = UNDEFINED An 8-bit byte that can take any value depending on the field definition,
                else if (p.Type == 0x7)
                {
                    switch (p.Id)
                    {
                        case 0xA300:
                            {
                                if (p.Value[0] == 3)
                                    v = "DSC";
                                else
                                    v = "reserved";
                                break;
                            }
                        case 0xA301:
                            if (p.Value[0] == 1)
                                v = "A directly photographed image";
                            else
                                v = "Not a directly photographed image";
                            break;
                        default:
                            v = "-";
                            break;
                    }
                }
                else if (p.Type == 0x9) //9 = SLONG A 32-bit (4 -byte) signed integer (2's complement notation),
                    v = ConvertToInt32(p.Value).ToString();
                else if (p.Type == 0xA) //10 = SRATIONAL Two SLONGs. The first SLONG is the numerator and the second SLONG is the denominator.
                {
                    // rational
                    var n = new byte[p.Len / 2];
                    var d = new byte[p.Len / 2];
                    Array.Copy(p.Value, 0, n, 0, p.Len / 2);
                    Array.Copy(p.Value, p.Len / 2, d, 0, p.Len / 2);
                    var a = ConvertToInt32(n);
                    var b = ConvertToInt32(d);
                    var r = new Rational(a, b);

                    switch (p.Id)
                    {
                        case 0x9201: // shutter speed
                            v = "1/" + Math.Round(Math.Pow(2, r.ToDouble()), 2).ToString(CultureInfo.InvariantCulture);
                            break;
                        case 0x9203:
                            v = Math.Round(r.ToDouble(), 4).ToString(CultureInfo.InvariantCulture);
                            break;
                        default:
                            v = r.ToString();
                            break;
                    }
                }

                v = v.Trim();

                // add it to the list
                if (!properties.ContainsKey(name)) properties.Add(name, v);

                // cat it too
                data += v;
                data += sp;
            }

        }

        public object GetPropertyValue(string name) => properties?[name];

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => data;

        private int ConvertToInt32(byte[] arr) => arr.Length != 4 ? 0 : arr[3] << 24 | arr[2] << 16 | arr[1] << 8 | arr[0];
        
        private uint ConvertToUInt32(byte[] arr) => arr.Length != 4 ? (uint)0 : Convert.ToUInt32(arr[3] << 24 | arr[2] << 16 | arr[1] << 8 | arr[0]);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        private uint ConvertToUInt16(byte[] arr) => arr.Length != 2 ? (ushort)0 : Convert.ToUInt16(arr[1] << 8 | arr[0]);

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator() => properties.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal sealed class Pair
    {
        // Fields
        public object First;
        public object Second;

        public Pair(object x, object y)
        {
            First = x;
            Second = y;
        }
    }
}
