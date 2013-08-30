using System;
using System.Windows.Forms;
using Delta.CertXplorer.DocumentModel;

namespace Delta.CertXplorer
{
    public static class VariousExtensions
    {
        public static IntPtr GetHandleOrZero(this IWin32Window window)
        {
            if (window == null) return IntPtr.Zero;
            else return window.Handle;
        }
    }
}
