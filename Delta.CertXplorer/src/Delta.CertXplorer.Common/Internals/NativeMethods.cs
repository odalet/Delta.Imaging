using System;
using System.Runtime.InteropServices;

namespace Delta.CertXplorer.Internals
{
    /// <summary>
    /// Contains Native Windows API interop code.
    /// </summary>
    internal static class NativeMethods
    {
        #region Constants

        public const int WM_WINDOWPOSCHANGING = 0x0046;

        public const int WM_NCACTIVATE = 0x0086;

        /// <summary>
        /// WM_KEYDOWN windows message.
        /// </summary>
        public const int WM_KEYDOWN = 0x100;

        /// <summary>
        /// WM_KEYUP windows message.
        /// </summary>
        public const int WM_KEYUP = 0x101;

        /// <summary>
        /// WM_CHAR windows message.
        /// </summary>
        public const int WM_CHAR = 0x102;

        #region RedrawWindow flags

        public const int RDW_INVALIDATE = 0x0001;
        public const int RDW_INTERNALPAINT = 0x0002;
        public const int RDW_ERASE = 0x0004;

        public const int RDW_VALIDATE = 0x0008;
        public const int RDW_NOINTERNALPAINT = 0x0010;
        public const int RDW_NOERASE = 0x0020;

        public const int RDW_NOCHILDREN = 0x0040;
        public const int RDW_ALLCHILDREN = 0x0080;

        public const int RDW_UPDATENOW = 0x0100;
        public const int RDW_ERASENOW = 0x0200;

        public const int RDW_FRAME = 0x0400;
        public const int RDW_NOFRAME = 0x0800;

        #endregion

        #endregion

        #region Structures

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPOS
        {
            public IntPtr hwnd;
            public IntPtr hwndInsertAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public int flags;
        }

        // Similar to RECT, but because this is a class, we can pass null to a method using it.
        [StructLayout(LayoutKind.Sequential)]
        public class COMRECT
        {
            /// <summary>The left coordinate.</summary>
            public int Left;

            /// <summary>The top coordinate.</summary>
            public int Top;

            /// <summary>The right coordinate.</summary>
            public int Right;

            /// <summary>The bottom coordinate.</summary>
            public int Bottom;

            /// <summary>
            /// Initializes a new instance of the <see cref="COMRECT"/> class.
            /// </summary>
            public COMRECT() { }

            /// <summary>
            /// Initializes a new instance of the <see cref="COMRECT"/> class.
            /// </summary>
            /// <param name="left">The left coordinate.</param>
            /// <param name="top">The top coordinate.</param>
            /// <param name="right">The right coordinate.</param>
            /// <param name="bottom">The bottom coordinate.</param>
            public COMRECT(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }
        }

        #endregion

        #region Methods

        // Caret definitions
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool CreateCaret(IntPtr hWnd, IntPtr hBitmap, int nWidth, int nHeight);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool ShowCaret(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool DestroyCaret();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetCaretPos(int x, int y);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool RedrawWindow(IntPtr hwnd, COMRECT rcUpdate, IntPtr hrgnUpdate, int flags);

        /// <summary>
        /// The SendMessage function sends the specified message to a window 
        /// or windows.<br />
        /// It calls the window procedure for the specified window and does not 
        /// return until the window procedure has processed the message.</summary>
        /// <remarks>Voir http://msdn2.microsoft.com/en-us/library/ms644950.aspx.</remarks> 
        /// <param name="hwnd">A window handle.</param>
        /// <param name="msg">Specifies the message to be sent.</param>
        /// <param name="wParam">Specifies additional message-specific information.</param>
        /// <param name="lParam">Specifies additional message-specific information.</param>
        /// <returns>
        /// The return value specifies the result of the message processing; 
        /// it depends on the message sent.
        /// </returns>
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hwnd, int msg, int wParam, int lParam);

        #endregion
    }
}
