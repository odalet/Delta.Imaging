using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Delta.CertXplorer.Diagnostics
{
    /// <summary>
    /// Defines a service that can handle unhandled exceptions.
    /// </summary>
    /// <remarks>
    /// These services are usually used inside methods handling the <see cref="AppDomain.UnhandledException"/> event.
    /// </remarks>
    public interface IExceptionHandlerService
    {
        /// <summary>
        /// Handles an exception.
        /// </summary>
        /// <param name="exceptionObject">An <see cref="System.Object"/> instance containing an exception.</param>
        /// <param name="isTerminating">if set to <c>true</c> then the application should close itself.</param>
        void HandleException(object exceptionObject, bool isTerminating);
    }
}
