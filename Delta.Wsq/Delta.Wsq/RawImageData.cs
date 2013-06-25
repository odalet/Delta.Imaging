
namespace Delta.Wsq
{
    public class RawImageData
    {
        public static RawImageData Empty = new RawImageData()
        {
            Data = new byte[0],
            Width = 0,
            Height = 0,
            PixelDepth = 0,
            Resolution = 0
        };

        public byte[] Data { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int PixelDepth { get; set; } // Always 8bpp
        public int Resolution { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </value>
        public bool IsEmpty
        {
            get { return Data == null || Data.Length == 0; }
        }
    }
}
