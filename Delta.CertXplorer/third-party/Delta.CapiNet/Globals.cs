using System;

namespace Delta.CapiNet
{
    public static class Globals
    {
        private static Func<Exception, bool> staticExceptionLogger = null;

        internal static void LogException(Exception ex)
        {
            if (!ExceptionLogger(ex)) throw ex;
        }

        /// <summary>
        /// Gets or sets the exception logger for this library.
        /// </summary>
        /// <value>The exception logger.</value>
        /// <remarks>
        /// If the exception logger returns <c>false</c>, then calling code should fail.
        /// </remarks>
        public static Func<Exception, bool> ExceptionLogger
        {
            get
            {
                if (staticExceptionLogger == null)
                    staticExceptionLogger = DefaultExceptionLogger;
                return WrapLogger(staticExceptionLogger);
            }
            set
            {
                if (value == null)
                    staticExceptionLogger = DefaultExceptionLogger;
                else staticExceptionLogger = value;
            }
        }


        /// <summary>
        /// Default exception logger.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        private static bool DefaultExceptionLogger(Exception exception)
        {
            if (exception == null)
                System.Diagnostics.Debug.WriteLine("An unknown error occurred.");
            else System.Diagnostics.Debug.WriteLine(string.Format("An error occurred: {0}\r\n{1}",
                exception.Message, exception.ToString()));
            return true;
        }

        /// <summary>
        /// Wraps the specified logger in a safe way.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <returns>The wrapped logger.</returns>
        private static Func<Exception, bool> WrapLogger(Func<Exception, bool> logger)
        {
            return ex =>
            {
                try { return logger(ex); }
                catch (Exception loggerException)
                {
                    var debugException = loggerException;
                    // intentional: we eat logger failures.
                    return true;
                }
            };
        }
    }
}
