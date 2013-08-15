using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

using Microsoft.Win32.SafeHandles;

namespace Delta.CapiNet.Internals
{
    /// <summary>
    /// <b>Safely</b> holds a CRL (Certificate Revocation List) context handle.
    /// </summary>
    internal sealed class CrlContextHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrlContextHandle"/> class.
        /// </summary>
        private CrlContextHandle() : base(true) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CrlContextHandle"/> class.
        /// </summary>
        /// <param name="handle">The handle.</param>
        public CrlContextHandle(IntPtr handle) : base(true) { base.SetHandle(handle); }

        /// <summary>
        /// Gets an invalid CRL context handle.
        /// </summary>
        /// <value>An invalid CRL context handle.</value>
        public static CrlContextHandle InvalidHandle
        {
            get { return new CrlContextHandle(IntPtr.Zero); }
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
            return NativeMethods.CertCloseStore(base.handle, 0);
        }
    }
}
