using System;
using System.Windows.Forms;

namespace Delta.CertXplorer.Internals
{
    /// <summary>
    /// Wraps a <see cref="System.Windows.Forms.IWin32Window"/> object.
    /// </summary>
    internal class Win32Window : IWin32Window
    {
        private IntPtr handle = IntPtr.Zero;

        public Win32Window(IntPtr hwnd) { handle = hwnd; }

        #region IWin32Window Members

        public IntPtr Handle { get { return handle; } }

        #endregion
    }
}
