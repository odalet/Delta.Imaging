using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Delta.CertXplorer.Internals
{
    /// <summary>
    /// Classes d'utilitaires permettant la manipulation de fenêtres Windows natives.
    /// </summary>
    internal static partial class NativeMethods
    {
        #region Windows Messages Constants

        public const int WM_WINDOWPOSCHANGING = 0x0046;

        public const int WM_NCACTIVATE = 0x0086;

        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_RBUTTONDOWN = 0x0204;
        public const int WM_PARENTNOTIFY = 0x0210;

        public const uint WM_USER = 0x400;
        public const uint EM_FORMATRANGE = WM_USER + 57;

        #endregion
        
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

        #region Other constants

        public const uint SRCCOPY = 0x00CC0020;

        /// <summary>Obtient les styles d'une fenêtre.</summary>
        public const int GWL_STYLE = -16;

        /// <summary>Obtient les styles étendus d'une fenêtre.</summary>
        public const int GWL_EXSTYLE = -16;

        /// <summary>Style vertical d'une <b>ProgressBar</b>.</summary>
        public const int PBS_VERTICAL = 4;

        /// <summary>Message de positionnement de la valeur d'une <b>ProgressBar</b>.</summary>
        public const int PBM_SETPOS = 0x0402;

        /// <summary>
        /// This style can be added or removed to a window style (or extended style) 
        /// without modifying the resulting style.
        /// </summary>
        public const int NULL_STYLE = 0;

        /// <summary>Voir http://msdn2.microsoft.com/en-us/library/ms633545.aspx.</summary>
        public static readonly IntPtr HWND_TOP = new IntPtr(0);

        /// <summary>Voir http://msdn2.microsoft.com/en-us/library/ms633545.aspx.</summary>
        public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        /// <summary>Voir http://msdn2.microsoft.com/en-us/library/ms633545.aspx.</summary>
        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

        /// <summary>Voir http://msdn2.microsoft.com/en-us/library/ms633545.aspx.</summary>
        public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);

        /// <summary>Voir http://msdn2.microsoft.com/en-us/library/ms632599(VS.85).aspx#message_only. </summary>
        public static readonly IntPtr HWND_MESSAGE = new IntPtr(-3);

        #endregion

        #region Interop

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern IntPtr DeleteObject(IntPtr hObject);
        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hdcDest, Int32 nXDest, Int32 nYDest, Int32 nWidth, Int32 nHeight, IntPtr hdcSrc, Int32 nXSrc, Int32 nYSrc, UInt32 dwRop);

        /// <summary>Voir http://msdn2.microsoft.com/en-us/library/ms633591.aspx.</summary>
        /// <param name="hwnd" />
        /// <param name="nIndex" />
        /// <param name="dwNewLong" />
        /// <returns />
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hwnd, int nIndex, int dwNewLong);

        /// <summary>Voir http://msdn2.microsoft.com/en-us/library/ms633584(VS.85).aspx.</summary>
        /// <param name="hwnd" />
        /// <param name="nIndex" />
        /// <returns />
        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hwnd, int nIndex);

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

        /// <summary>
        /// Voir http://msdn2.microsoft.com/en-us/library/ms633545.aspx.
        /// </summary>
        /// <param name="hwnd" />
        /// <param name="hwndInsertAfter" />
        /// <param name="x" />
        /// <param name="y" />
        /// <param name="width" />
        /// <param name="height" />
        /// <param name="flags" />
        /// <returns></returns>
        [DllImport("User32", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetWindowPos(
            IntPtr hwnd, IntPtr hwndInsertAfter,
            int x, int y, int width, int height, uint flags);

        /// <summary>
        /// Gets the currently active window.
        /// </summary>
        /// <returns>A HWND representing the active window.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern short VkKeyScan(char key);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool RedrawWindow(IntPtr hwnd, COMRECT rcUpdate, IntPtr hrgnUpdate, int flags);

        [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory",
             SetLastError = true, CharSet = CharSet.Auto,
             ExactSpelling = true,
             CallingConvention = CallingConvention.StdCall)]
        public static extern void RtlMoveMemory(
            [In, MarshalAs(UnmanagedType.I4)]int hpvDest,
            [In, Out]byte[] hpvSource,
            int cbCopy);

        #endregion

        #region Helper methods and properties

        /// <summary>
        /// Gets the currently active window.
        /// </summary>
        /// <value>The active window.</value>
        public static IWin32Window ActiveWindow
        {
            get { return new Win32Window(GetActiveWindow()); }
        }

        /// <summary>Gets the style of the window identified by <paramref name="hwnd"/>.</summary>
        /// <param name="hwnd">A Window handle.</param>
        /// <returns>The window style.</returns>
        public static int GetWindowStyle(IntPtr hwnd) { return GetWindowLong(hwnd, GWL_STYLE); }

        /// <summary>Gets the extended style of the window identified by <paramref name="hwnd"/>.</summary>
        /// <param name="hwnd">A Window handle.</param>
        /// <returns>The window extended style.</returns>
        public static int GetWindowStyleEx(IntPtr hwnd) { return GetWindowLong(hwnd, GWL_EXSTYLE); }

        /// <summary>Sets the style of the window identified by <paramref name="hwnd"/>.</summary>
        /// <param name="hwnd">A Window handle.</param>
        /// <param name="styles">The styles to apply to the window.</param>
        public static void SetWindowStyle(IntPtr hwnd, int styles) { SetWindowLong(hwnd, GWL_STYLE, styles); }

        /// <summary>Gets the extended style of the window identified by <paramref name="hwnd"/>.</summary>
        /// <param name="hwnd">A Window handle.</param>
        /// <param name="styles">The extended styles to apply to the window.</param>
        public static void SetWindowStyleEx(IntPtr hwnd, int styles) { SetWindowLong(hwnd, GWL_EXSTYLE, styles); }

        /// <summary>
        /// Modifies the style of the window identified by <paramref name="hwnd"/> by
        /// adding <paramref name="stylesToAdd"/> and removing <paramref name="stylesToRemove"/>.
        /// </summary>
        /// <param name="hwnd">A Window handle.</param>
        /// <param name="stylesToAdd">The styles to add.</param>
        /// <param name="stylesToRemove">The styles to remove.</param>
        public static void ModifyWindowStyle(IntPtr hwnd, int stylesToAdd, int stylesToRemove) 
        {
            int newStyle = GetWindowStyle(hwnd);

            // Si un style apparaît dans les deux paramètres, alors il sera 
            // ajouté (c'est pour cea que l'on commence par retirer les styles).
            newStyle &= ~stylesToRemove;
            newStyle |= stylesToAdd;            
            
            SetWindowStyle(hwnd, newStyle); 
        }

        /// <summary>
        /// Modifies the extended style of the window identified by <paramref name="hwnd"/> by
        /// adding <paramref name="stylesToAdd"/> and removing <paramref name="stylesToRemove"/>.
        /// </summary>
        /// <param name="hwnd">A Window handle.</param>
        /// <param name="stylesToAdd">The extended styles to add.</param>
        /// <param name="stylesToRemove">The extended styles to remove.</param>
        public static void ModifyWindowStyleEx(IntPtr hwnd, int stylesToAdd, int stylesToRemove)
        {
            int newStyle = GetWindowStyleEx(hwnd);

            // Si un style apparaît dans les deux paramètres, alors il sera 
            // ajouté (c'est pour cea que l'on commence par retirer les styles).
            newStyle &= ~stylesToRemove;
            newStyle |= stylesToAdd;

            SetWindowStyleEx(hwnd, newStyle);
        }

        #endregion
    }
}
