using System;
using System.Security.Cryptography.X509Certificates;

using Microsoft.Win32.SafeHandles;

namespace Delta.CapiNet.Internals
{
    /// <summary>
    /// <b>Safely</b> holds a certificate context handle.
    /// </summary>
    internal sealed class CertContextHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CertContextHandle"/> class.
        /// </summary>
        private CertContextHandle() : base(true) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CertContextHandle"/> class.
        /// </summary>
        /// <param name="handle">The handle.</param>
        public CertContextHandle(IntPtr handle) : base(true) { base.SetHandle(handle); }

        /// <summary>
        /// Gets an invalid certificate context handle.
        /// </summary>
        /// <value>An invalid store handle.</value>
        public static CertContextHandle InvalidHandle
        {
            get { return new CertContextHandle(IntPtr.Zero); }
        }
        
        /// <summary>
        /// When overridden in a derived class, executes the code required to free the handle.
        /// </summary>
        /// <returns>
        /// <c>True</c> if the handle is released successfully; otherwise, in the event of a 
        /// catastrophic failure, <c>false</c>. In this case, it generates a <c>releaseHandleFailed</c>
        /// MDA (Managed Debugging Assistant).
        /// </returns>
        protected override bool ReleaseHandle()
        {
            return NativeMethods.CertFreeCertificateContext(base.handle);
        }
    }
}
