using System;
using System.Runtime.InteropServices;

namespace Delta.CertXplorer.Internals
{
    partial class NativeMethods
    {
        #region GDI Structures

        /// <summary>http://support.microsoft.com/default.aspx?scid=kb;en-us;812425</summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            /// <summary>The left coordinate.</summary>
            public int Left;

            /// <summary>The top coordinate.</summary>
            public int Top;

            /// <summary>The right coordinate.</summary>
            public int Right;

            /// <summary>The bottom coordinate.</summary>
            public int Bottom;
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

        #region Window Structure

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

        #endregion

        #region Structures needed by RichTextBox

        /// <summary>http://support.microsoft.com/default.aspx?scid=kb;en-us;812425</summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct CHARRANGE
        {
            public int cpMin;         //First character of range (0 for start of doc)
            public int cpMax;           //Last character of range (-1 for end of doc)
        }

        /// <summary>http://support.microsoft.com/default.aspx?scid=kb;en-us;812425</summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct FORMATRANGE
        {
            public IntPtr hdc;             //Actual DC to draw on
            public IntPtr hdcTarget;       //Target DC for determining text formatting
            public RECT rc;                //Region of the DC to draw to (in twips)
            public RECT rcPage;            //Region of the whole DC (page size) (in twips)
            public CHARRANGE chrg;         //Range of text to draw (see earlier declaration)
        }

        #endregion

        #region shell32.dll Structures

        [StructLayout(LayoutKind.Sequential)]
        internal struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;

            [MarshalAs(UnmanagedType.LPStr, SizeConst = 260)]
            public string szDisplayName;

            [MarshalAs(UnmanagedType.LPStr, SizeConst = 80)]
            public string szTypeName;
        }

        internal enum SHGFI : uint
        {
            SHGFI_LARGEICON = 0x0,              // get large icon
            SHGFI_SMALLICON = 0x1,              // get small icon
            SHGFI_OPENICON = 0x2,               // get open icon
            SHGFI_SHELLICONSIZE = 0x4,          // get shell size icon
            SHGFI_PIDL = 0x8,                   // pszPath is a pidl
            SHGFI_USEFILEATTRIBUTES = 0x10,     // use passed dwFileAttribute
            SHGFI_ICON = 0x100,                 // get icon
            SHGFI_DISPLAYNAME = 0x200,          // get display name
            SHGFI_TYPENAME = 0x400,             // get type name
            SHGFI_ATTRIBUTES = 0x800,           // get attributes
            SHGFI_ICONLOCATION = 0x1000,        // get icon location
            SHGFI_EXETYPE = 0x2000,             // return exe type
            SHGFI_SYSICONINDEX = 0x4000,        // get system icon index
            SHGFI_LINKOVERLAY = 0x8000,         // put a link overlay on icon
            SHGFI_SELECTED = 0x10000,           // show icon in selected state
            SHGFI_ATTR_SPECIFIED = 0x20000      // get only specified attributes        
        }

        #endregion
    }
}
