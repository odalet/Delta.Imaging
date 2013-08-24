using System;
using Microsoft.Win32.SafeHandles;

namespace Delta.CapiNet.Internals
{
    /// <summary>
    /// <b>Safely</b> holds a local memory allocation handle.
    /// </summary>
    internal sealed class LocalAllocHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalAllocHandle"/> class.
        /// </summary>
        private LocalAllocHandle() : base(true) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalAllocHandle"/> class.
        /// </summary>
        /// <param name="handle">The handle.</param>
        public LocalAllocHandle(IntPtr handle) : base(true)
        {
            base.SetHandle(handle);
        }

        /// <summary>
        /// Allocates memory using the Win32 <c>LocalAlloc</c> API function and safely stores the returned handle.
        /// </summary>
        /// <param name="uFlags">The allocation flags.</param>
        /// <param name="sizetdwBytes">The size to allocate in bytes.</param>
        /// <returns>An instance of <see cref="LocalAllocHandle"/> or an <see cref="OutOfMemoryException"/> 
        /// if allocation failed.</returns>
        /// <remarks>
        /// See <c>LocalAlloc</c> documentation here for more information: 
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/aa366723.aspx
        /// </remarks>
        public static LocalAllocHandle Allocate(uint uFlags, IntPtr size)
        {
            var handle = NativeMethods.LocalAlloc(uFlags, size);
            if ((handle == null) || handle.IsInvalid)
                throw new OutOfMemoryException("Could not allocate memory using LocalAlloc native call.");
            return handle;
        }

        /// <summary>
        /// Gets an invalid handle.
        /// </summary>
        /// <value>An invalid handle.</value>
        public static LocalAllocHandle InvalidHandle
        {
            get { return new LocalAllocHandle(IntPtr.Zero); }
        }

        /// <summary>
        /// When overridden in a derived class, executes the code required to free the handle.
        /// </summary>
        /// <c>True</c> if the handle is released successfully; otherwise, in the event of a 
        /// catastrophic failure, <c>false</c>. In this case, it generates a <c>releaseHandleFailed</c>
        /// MDA (Managed Debugging Assistant).
        /// </returns>
        protected override bool ReleaseHandle()
        {
            return (NativeMethods.LocalFree(base.handle) == IntPtr.Zero);
        }
    }
}
