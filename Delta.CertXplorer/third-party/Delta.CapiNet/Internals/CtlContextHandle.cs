using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

using Microsoft.Win32.SafeHandles;

namespace Delta.CapiNet.Internals
{
    /// <summary>
    /// <b>Safely</b> holds a CTL (Certificate Trust List) context handle.
    /// </summary>
    internal sealed class CtlContextHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CtlContextHandle"/> class.
        /// </summary>
        private CtlContextHandle() : base(true) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CtlContextHandle"/> class.
        /// </summary>
        /// <param name="handle">The handle.</param>
        public CtlContextHandle(IntPtr handle) : base(true) { base.SetHandle(handle); }

        /// <summary>
        /// Gets an invalid CRL context handle.
        /// </summary>
        /// <value>An invalid CRL context handle.</value>
        public static CtlContextHandle InvalidHandle
        {
            get { return new CtlContextHandle(IntPtr.Zero); }
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
            return NativeMethods.CertFreeCTLContext(base.handle);
        }
    }
}
