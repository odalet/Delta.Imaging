using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Delta.Wsq
{
    public static partial class WsqCodec
    {   
        /// <summary>
        /// Encodes the input image (raw data) to WSQ.
        /// </summary>
        /// <param name="image">The input image.</param>
        /// <param name="bitrate">The bit rate (in bits per pixels).</param>
        /// <param name="comment">An optional comment that is added to the WSQ image.</param>
        /// <returns>WSQ Encoded image.</returns>
        public static byte[] Encode(RawImageData image, float bitrate, string comment)
        {
            if (image == null || image.IsEmpty)
                return new byte[0];

            var inputData = image.Data;
            var inputDataPtr = Marshal.AllocHGlobal(inputData.Length);
            var outputDataPtr = IntPtr.Zero;
            try
            {
                Marshal.Copy(inputData, 0, inputDataPtr, inputData.Length);

                var outputLength = 0;
                var result = NativeMethods.wsq_encode(
                    out outputDataPtr, out outputLength, bitrate, inputDataPtr, image.Width, image.Height,
                    image.PixelDepth, image.Resolution, comment ?? string.Empty);

                if (result == 0) // ok
                {
                    var outputData = new byte[outputLength];
                    Marshal.Copy(outputDataPtr, outputData, 0, outputLength);
                    return outputData;
                }
            }
            finally
            {
                if (inputDataPtr != IntPtr.Zero)
                    Marshal.FreeHGlobal(inputDataPtr);
                if (outputDataPtr != IntPtr.Zero)
                    NativeMethods.free_mem(outputDataPtr);
            }

            return null;
        }

        public static unsafe string[] GetComments(byte[] data)
        {
            if (data == null || data.Length == 0) 
                return new string[0];

            // Allocates native memory for the input data
            var inputLength = data.Length;
            var inputDataPtr = Marshal.AllocHGlobal(inputLength);
            var outputCommentPtr = IntPtr.Zero;
            var comments = new List<string>();
            try
            {
                // copy the input data to native memory
                Marshal.Copy(data, 0, inputDataPtr, inputLength);

                // get comments
                int ccount = 0;
                var result = NativeMethods.wsq_get_comments(
                    out outputCommentPtr, out ccount, inputDataPtr, inputLength);
                
                if (result == 0) // ok
                {
                    // retrieve image comment.
                    if (outputCommentPtr != IntPtr.Zero)
                    {
                        if (ccount > 0)
                        {
                            var commentPtr = (IntPtr*)outputCommentPtr;
                            for (int i = 0; i < ccount; i++)
                            {
                                var ptr = *commentPtr;
                                comments.Add(Marshal.PtrToStringAnsi(ptr));
                                NativeMethods.free_mem(ptr);
                                commentPtr++;
                            }
                        }

                        NativeMethods.free_mem(outputCommentPtr);
                    }
                }
            }
            finally
            {
                // Free the memory we allocated for the input data.
                if (inputDataPtr != IntPtr.Zero)
                    Marshal.FreeHGlobal(inputDataPtr);
            }

            return comments.ToArray();
        }

        /// <summary>
        /// Decodes the input WSQ data into raw image data.
        /// </summary>
        /// <param name="data">The WSQ Image data.</param>
        /// <returns>Decoded data.</returns>
        public static unsafe RawImageData Decode(byte[] data)
        {
            if (data == null || data.Length == 0)
                return RawImageData.Empty;

            // Allocates native memory for the input data
            var inputLength = data.Length;
            var inputDataPtr = Marshal.AllocHGlobal(inputLength);
            var outputDataPtr = IntPtr.Zero;
            try
            {
                // copy the input data to native memory
                Marshal.Copy(data, 0, inputDataPtr, inputLength);

                // Decode
                int width, height, depth, dpi, lossy;
                var result = NativeMethods.wsq_decode(
                    out outputDataPtr, out width, out height, out depth, out dpi, out lossy, inputDataPtr, inputLength);
                
                if (result == 0) // ok
                {                    
                    int outputLength = width * height;
                    var outputData = new byte[outputLength];

                    // copy decoded data back from native memory
                    Marshal.Copy(outputDataPtr, outputData, 0, outputLength);
                    return new RawImageData()
                    {
                        Data = outputData,
                        Width = width,
                        Height = height,
                        PixelDepth = depth,
                        Resolution = dpi
                    };
                }
            }
            finally
            {
                // Free the memory we allocated for the input data.
                if (inputDataPtr != IntPtr.Zero)
                    Marshal.FreeHGlobal(inputDataPtr);

                // Free the memory (allocated by the wsq library).
                if (outputDataPtr != IntPtr.Zero)
                    NativeMethods.free_mem(outputDataPtr);
            }

            return RawImageData.Empty;
        }
    }
}
