using System;
using System.Linq;

namespace Delta.CapiNet.Asn1
{
    internal static class Asn1Helper
    {
        public static string ToFormattedString(this byte[] array)
        {
            return string.Join(" ", array.Select(b => b.ToString("X2")).ToArray());
        }

        public static byte[] SubArray(this byte[] data, int offset, int length)
        {
            byte[] b = new byte[length];
            for (int i = 0; i < length; i++) b[i] = data[offset + i];
            return b;
        }

        public static byte[] CheckedSubArray(this byte[] data, int offset, int length)
        {
            Exception fooException;
            var result = CheckedSubArray(data, offset, length, out fooException);
            if (fooException != null)
                Globals.LogException(fooException);
            return result;
        }

        public static byte[] CheckedSubArray(
            this byte[] data, int offset, int length, out Exception argumentsException)
        {
            argumentsException = null;
            if (data == null)
            {
                argumentsException = new ArgumentNullException("data");
                return new byte[0];
            }

            var dataLength = data.Length;
            if (offset < 0)
            {
                argumentsException = new ArgumentOutOfRangeException(
                    "offset", "Offset must be strictly greater than 0.");
                return new byte[0];
            }

            if (offset >= dataLength)
            {
                argumentsException = new ArgumentOutOfRangeException(
                    "offset", "Offset must be strictly lower than data length.");
                return new byte[0];
            }

            if (length <= 0)
            {
                argumentsException = new ArgumentOutOfRangeException(
                    "length", "Length must be greater than 0.");
                return new byte[0];
            }

            if (offset + length > dataLength)
            {
                argumentsException = new ArgumentOutOfRangeException(
                    "offset + length", "Offset + Length must be lower than data length.");
                length = dataLength - offset; // let's retrieve what we can.
            }

            byte[] b = new byte[length];
            for (int i = 0; i < length; i++) b[i] = data[offset + i];
                        
            return b;
        }

        public static ushort LengthDecode(this byte[] data, out int lengthLength)
        {
            if (data[0] < 128)
            {
                lengthLength = 1;
                return data[0];
            }
            else if (data[0] == 0x81)
            {
                lengthLength = 2;
                return data[1];
            }
            else if (data[0] == 0x82)
            {
                lengthLength = 3;
                return (ushort)((data[1] << 8) + data[2]);
            }
            else throw new InvalidOperationException(
                "The encoded length must be less than 128 or start with 0x81 or 0x82.");
        }
    }
}
