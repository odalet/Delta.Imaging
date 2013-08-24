using System;
using System.Security.Cryptography.X509Certificates;

using Microsoft.Win32.SafeHandles;

namespace Delta.CapiNet.Internals
{
    /// <summary>
    /// <b>Safely</b> holds a certificates store handle.
    /// </summary>
    internal sealed class CertStoreHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoreHandle"/> class.
        /// </summary>
        private CertStoreHandle() : base(true) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreHandle"/> class.
        /// </summary>
        /// <param name="handle">The handle.</param>
        public CertStoreHandle(IntPtr handle) : base(true) { base.SetHandle(handle); }

        public static CertStoreHandle FromX509Store(X509Store store)
        {
            if (store == null) return InvalidHandle;
            return new CertStoreHandle(store.StoreHandle);
        }

        /// <summary>
        /// Gets an invalid store handle.
        /// </summary>
        /// <value>An invalid store handle.</value>
        public static CertStoreHandle InvalidHandle
        {
            get { return new CertStoreHandle(IntPtr.Zero); }
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
