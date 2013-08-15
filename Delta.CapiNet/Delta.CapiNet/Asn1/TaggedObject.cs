using System;
using System.Linq;
using System.Collections.Generic;

namespace Delta.CapiNet.Asn1
{
    /// <summary>
    /// A tagged object is encoded as a tag, a length and a byte array containing the workload data.
    /// </summary>
    public class TaggedObject
    {
        public struct DataTag
        {
            public ushort Value { get; set; }
            public int Length { get; set; }
        }

        public struct DataLength
        {
            public int Value { get; set; }
            public int Length { get; set; }
        }

        private byte[] allData = null;
        private int rawDataOffset = 0;
        private int rawDataLength = 0;
        private int workloadShift = 0;
        private int workloadLength = 0;

        protected TaggedObject(byte[] data, int offset, int length)
        {
            EnsureArguments(data, offset, length);
            allData = data;
            rawDataOffset = offset;
            rawDataLength = length;
        }

        public DataTag Tag
        {
            get; 
            private set;
        }

        public DataLength Length
        {
            get; 
            private set;
        }

        /// <summary>
        /// Gets a copy of the raw data.
        /// </summary>
        /// <value>The raw data.</value>
        public byte[] RawData
        {
            get { return allData.SubArray(rawDataOffset, rawDataLength); }
        }

        internal byte[] AllData
        {
            get { return allData; }
        }

        public int RawDataOffset { get { return rawDataOffset; } }

        public int RawDataLength { get { return RawDataLength; } }

        /// <summary>
        /// Gets a copy of the workload data.
        /// </summary>
        /// <value>The workload.</value>
        public byte[] Workload
        {
            get { return allData.CheckedSubArray(WorkloadOffset, WorkloadLength); } 
        }

        public int WorkloadOffset { get { return rawDataOffset + workloadShift; } }

        public int WorkloadLength { get { return workloadLength; } }

        internal static TaggedObject CreateObject(byte[] data, int offset, int length)        
        {
            int count;
            return CreateObject(data, offset, length, out count);
        }

        private static TaggedObject CreateObject(byte[] data, int offset, int length, out int count)
        {
            try
            {
                EnsureArguments(data, offset, length);
                var taggedObject = new TaggedObject(data, offset, length);
                count = taggedObject.Parse();
                return taggedObject;
            }
            catch (Exception ex)
            {
                Globals.LogException(ex);
            }

            count = length;
            return new InvalidTaggedObject(data, offset, length);
        }

        internal static TaggedObject[] CreateObjects(byte[] data, int offset, int length)
        {
            try
            {
                EnsureArguments(data, offset, length);

                var taggedObjects = new List<TaggedObject>();
                var buffer = data.SubArray(offset, length);
                var currentOffset = offset;
                var currentLength = length;
                
                while (buffer.Length > 0)
                {
                    int count = 0;
                    var taggedObject = CreateObject(data, currentOffset, currentLength, out count);
                    if (taggedObject is InvalidTaggedObject)
                    {
#pragma warning disable 219
                        // for debugging purpose: place a breakpoint here
                        var foo = 42;
#pragma warning restore 219
                    }

                    if (taggedObject != null)
                    {
                        taggedObjects.Add(taggedObject);
                        currentOffset += count;
                        currentLength -= count;

                        if (count >= buffer.Length) buffer = new byte[0];
                        else buffer = Skip(buffer, count);
                    }
                    else buffer = new byte[0];
                }

                return taggedObjects.ToArray();
            }
            catch (Exception ex)
            {
                Globals.LogException(ex);
            }

            return new TaggedObject[0];
        }

        /// <summary>
        /// Parses this instance.
        /// </summary>
        /// <returns>bytes count of the data used to build the tagged object.</returns>
        private int Parse()
        {
            var data = RawData;

            // Determine tag value and length
            ushort tagValue = 0;
            int tagLength = 0;
            if (data[0] == 0x5F || data[0] == 0x7F || data[0] == 0x9F)
            {
                tagValue = (ushort)((data[0] << 8) + data[1]);
                tagLength = 2;
            }
            else
            {
                tagValue = (ushort)data[0];
                tagLength = 1;
            }

            workloadShift = tagLength;
            Tag = new DataTag()
            {
                Value = tagValue,
                Length = tagLength
            };

            // Skip the tag
            data = data.Skip(Tag.Length).ToArray();

            // Determine data length            
            int lengthLength = 0;
            int length = data.LengthDecode(out lengthLength);

            workloadShift += lengthLength;
            workloadLength = length;
            Length = new DataLength()
            {
                Value = length,
                Length = lengthLength
            };

            return tagLength + lengthLength + length;
        }

        private static byte[] Skip(byte[] buffer, int count)
        {
            var length = buffer.Length - count;
            var result = new byte[length];
            Array.Copy(buffer, result, length);
            return result;
        }

        private static void EnsureArguments(byte[] data, int offset, int length)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (data.Length == 0) throw new ArgumentException("Empty data", "data");
            if (offset + length > data.Length) throw new ArgumentOutOfRangeException("data", 
                "input array is not long enough. Check offset and length parameters validity.");
        }
    }
}