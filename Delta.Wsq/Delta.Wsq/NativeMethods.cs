using System;
using System.Runtime.InteropServices;

namespace Delta.Wsq
{
    internal static class NativeMethods
    {
        //private const string prefix = "nistbio";
        private const string dllPrefix = "nbis";
#if DEBUG
        private const string dllSuffix = "d";
#else
        private const string dllSuffix = "";
#endif

        private const string x86Dll = dllPrefix + "32" + dllSuffix + ".dll";
        private const string x64Dll = dllPrefix + "64" + dllSuffix + ".dll";

        #region x86

        [DllImport(x86Dll, EntryPoint = "wsq_get_comments")]
        private static extern int wsq_get_comments32(
            out IntPtr commentData,
            out int commentsCount,
            IntPtr inputData,
            int inputLength);

        [DllImport(x86Dll, EntryPoint = "wsq_decode")]
        private static extern int wsq_decode32(
            out IntPtr outputData,
            out int width,
            out int height,
            out int depth,
            out int dpi,
            out int lossyflag,
            IntPtr inputData,
            int inputLength);

        [DllImport(x86Dll, EntryPoint = "wsq_encode")]
        private static extern int wsq_encode32(
            out IntPtr outputData,
            out int outputLength,
            float bitrate,
            IntPtr inputData,
            int width,
            int height,
            int depth,
            int dpi,
            string comment_text);

        [DllImport(x86Dll, EntryPoint = "free_mem")]
        private static extern void free_mem32(IntPtr data);

        #endregion

        #region x64

        [DllImport(x64Dll, EntryPoint = "wsq_get_comments")]
        private static extern int wsq_get_comments64(
            out IntPtr commentData,
            out int commentsCount,
            IntPtr inputData,
            int inputLength);

        [DllImport(x64Dll, EntryPoint = "wsq_decode")]
        private static extern int wsq_decode64(
            out IntPtr outputData,
            out int width,
            out int height,
            out int depth,
            out int dpi,
            out int lossyflag,
            IntPtr inputData,
            int inputLength);

        [DllImport(x64Dll, EntryPoint = "wsq_encode")]
        private static extern int wsq_encode64(
            out IntPtr outputData,
            out int outputLength,
            float bitrate,
            IntPtr inputData,
            int width,
            int height,
            int depth,
            int dpi,
            string comment_text);

        [DllImport(x64Dll, EntryPoint = "free_mem")]
        private static extern void free_mem64(IntPtr data);

        #endregion

        #region Windows API

        [DllImport("kernel32.dll")]
        public static extern unsafe void CopyMemory(void* destination, void* source, int length);

        #endregion

        public static int wsq_decode(
            out IntPtr outputData,
            out int width,
            out int height,
            out int depth,
            out int dpi,
            out int lossyflag,
            IntPtr inputData,
            int inputLength)
        {
            return IntPtr.Size == 8 ?
                wsq_decode64(out outputData, out width, out height, out depth, out dpi, out lossyflag, inputData, inputLength) :
                wsq_decode32(out outputData, out width, out height, out depth, out dpi, out lossyflag, inputData, inputLength);
        }

        public static int wsq_get_comments(
            out IntPtr commentData,
            out int commentsCount,
            IntPtr inputData,
            int inputLength)
        {
            return IntPtr.Size == 8 ?
                wsq_get_comments64(out commentData, out commentsCount, inputData, inputLength) :
                wsq_get_comments32(out commentData, out commentsCount, inputData, inputLength);
        }

        public static int wsq_encode(
            out IntPtr outputData,
            out int outputLength,
            float bitrate,
            IntPtr inputData,
            int width,
            int height,
            int depth,
            int dpi,
            string comment_text)
        {
            return IntPtr.Size == 8 ?
                wsq_encode64(out outputData, out outputLength, bitrate, inputData, width, height, depth, dpi, comment_text) :
                wsq_encode32(out outputData, out outputLength, bitrate, inputData, width, height, depth, dpi, comment_text);
        }

        public static void free_mem(IntPtr data)
        {
            if (IntPtr.Size == 8)
                free_mem64(data);
            else free_mem32(data);
        }
    }
}
