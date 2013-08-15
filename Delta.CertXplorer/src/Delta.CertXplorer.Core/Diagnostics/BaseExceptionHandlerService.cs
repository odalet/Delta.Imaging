using System;

using Delta.CertXplorer.Logging;

namespace Delta.CertXplorer.Diagnostics
{
    /// <summary>
    /// Implements a basic version of <see cref="IExceptionHandlerService"/>.
    /// </summary>
    public class BaseExceptionHandlerService : IExceptionHandlerService
    {
        private static bool isAlreadyHandlingAnException = false;
        
        /// <summary>Services provider.</summary>
        private IServiceProvider services = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseExceptionHandlerService"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public BaseExceptionHandlerService(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) throw new ArgumentNullException("serviceProvider");
            services = serviceProvider;
        }        

        #region IExceptionHandlerService Members

        /// <summary>
        /// Handles an exception.
        /// </summary>
        /// <param name="exceptionObject">An <see cref="System.Object"/> instance containing an exception.</param>
        /// <param name="isTerminating">if set to <c>true</c> then the application should close itself.</param>
        public void HandleException(object exceptionObject, bool isTerminating)
        {
            if (isAlreadyHandlingAnException) return;
            else
            {
                isAlreadyHandlingAnException = true;
                try { ProcessExceptionHandling(exceptionObject, isTerminating); }
                catch (Exception ex)
                {
                    // Eat the exception
                    var debugEx = ex;
                }
                finally { isAlreadyHandlingAnException = false; }
            }
        }

        #endregion

        /// <summary>
        /// Processes the exception handling.
        /// </summary>
        /// <param name="exceptionObject">An <see cref="T:System.Object"/> instance containing an exception.</param>
        /// <param name="isTerminating">if set to <c>true</c> then the application should close itself.</param>
        protected virtual void ProcessExceptionHandling(object exceptionObject, bool isTerminating)
        {
            Log(exceptionObject, isTerminating);
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="fatalError">if set to <c>true</c> then identifies a fatal error.</param>
        private void Log(object message, bool fatalError)
        {
            bool logged = false;

            ILogService log = services.GetService<ILogService>();
            if (log != null)
            {
                try 
                { 
					if (message is Exception)
                    {
                        if (fatalError) log.Fatal((Exception)message);
                        else log.Error((Exception)message);
                    }
                    else 
                    {
                        if (message == null) message = "?";
                        if (fatalError) log.Fatal(message.ToString());
                        else log.Error(message.ToString());
                    }

                    logged = true;
                }
                catch (Exception ex)
                {
                    // We are inside an "unhandled exceptions" handler, so, we can eat the exception!
                    System.Diagnostics.Debug.WriteLine(string.Format("EXCEPTION: {0}", ex));
                }
            }

            // Logging failed: either there was no logging service available or an exception was thrown :-(
            // Then, we rely back on the Visual Studio output window...
            if (!logged) 
            {
                try
                {
                    if (fatalError)
                        System.Diagnostics.Debug.WriteLine("FATAL: " + message);
                    else System.Diagnostics.Debug.WriteLine("ERROR: " + message);
                }
                catch(Exception ex)
                {
                    // We are inside an "unhandled exceptions" handler, so, we can eat the exception!
                    System.Diagnostics.Debug.WriteLine(string.Format("EXCEPTION: {0}", ex));
                }
            }
        }
    }
}
