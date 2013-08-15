using System;
using System.Diagnostics;

using Delta.CertXplorer.Diagnostics;

namespace Delta.CertXplorer.ApplicationModel.Services
{
    /// <summary>
    /// Handles unhandled exceptions.
    /// </summary>
    public class ExceptionHandlerService : BaseExceptionHandlerService
    {
        private const int maxApplicationExitRetryCount = 5;
        private int applicationExitRetryCount = 0;   

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandlerService"/> class.
        /// </summary>
        public ExceptionHandlerService() : base(This.Services) { }
        
        /// <summary>
        /// Processes the exception handling.
        /// </summary>
        /// <param name="exceptionObject">An <see cref="T:System.Object"/> instance containing an exception.</param>
        /// <param name="isTerminating">if set to <c>true</c> then the application should close itself.</param>
        protected override void ProcessExceptionHandling(object exceptionObject, bool isTerminating)
        {
            base.ProcessExceptionHandling(exceptionObject, isTerminating);
            bool exitApplication = false;
            try
            {
                var result = ExceptionBox.Show(null, exceptionObject, string.Empty, true);
                exitApplication = (result == ExceptionBoxResult.Exit);
            }
            catch (Exception ex)
            {
                // Eat the exception!
                var debugEx = ex;
            }

            if (exitApplication) ExitApplication();
        }

        /// <summary>
        /// Recursively tries to close the application.
        /// </summary>
        /// <remarks>
        /// We first try to be nice and close the application using 
        /// <see cref="System.Windows.Forms.Application.Exit()"/>. We try to exit 
        /// this way up to 5 times. If it doesn't work, then we rely on a more
        /// agressive way: <see cref="System.Diagnostics.Process.Kill"/>.
        /// </remarks>
        protected virtual void ExitApplication()
        {
            try
            {
                applicationExitRetryCount++;
                SafeLogInfo(string.Format(
                    "This is the #{0} try at exiting the application.", applicationExitRetryCount));                
                System.Windows.Forms.Application.Exit();
            }
            catch (Exception ex)
            {
                // Eat the exception
                var debugEx = ex;

                SafeLogInfo(string.Format("Application exit try #{0} failed: {1}.",
                    applicationExitRetryCount, ex.Message), ex);               
                
                if (applicationExitRetryCount < maxApplicationExitRetryCount)
                    ExitApplication();
                else Process.GetCurrentProcess().Kill();
            }
        }

        private void SafeLogInfo(string text) { SafeLogInfo(text, null); }

        private void SafeLogInfo(string text, Exception exception)
        {
            try { This.Logger.Info(text, exception); }
            catch (Exception ex)
            {
                // eat the exception
                var debugEx = ex;
            }
        }
    }
}
