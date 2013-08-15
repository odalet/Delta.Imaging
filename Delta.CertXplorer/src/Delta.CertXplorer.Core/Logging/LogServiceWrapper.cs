using System;

namespace Delta.CertXplorer.Logging
{
    /// <summary>
    /// Wraps and reexposes an inner <see cref="ILogService"/> instance.
    /// </summary>
    /// <remarks>
    /// Because, the interface implementation metjods are marked as virtual, an inheriting class
    /// can tweak the way log messages are processed, and still base on an existing Log service.
    /// </remarks>
    public class LogServiceWrapper : ILogService
    {
        private ILogService log = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogServiceWrapper"/> class.
        /// </summary>
        /// <param name="logService">The wrapped log service.</param>
        public LogServiceWrapper(ILogService logService)
        {
            if (logService == null) throw new ArgumentNullException("logService");
            log = logService;
        }

        /// <summary>
        /// Gets the wrapped logger.
        /// </summary>
        /// <value>The wrapped logger.</value>
        protected virtual ILogService WrappedLogger
        {
            get { return log; }
        }

        #region ILogService Members

        /// <summary>
        /// Logs the specified message with the specified trace level.
        /// </summary>
        /// <param name="level">The trace level.</param>
        /// <param name="message">The message to trace.</param>
        public virtual void Log(LogLevel level, string message)
        {
            WrappedLogger.Log(level, message);
        }

        /// <summary>
        /// Logs the specified message and exception with the specified trace level.
        /// </summary>
        /// <param name="level">The trace level.</param>
        /// <param name="message">The message to trace.</param>
        /// <param name="exception">The exception to trace.</param>
        public virtual void Log(LogLevel level, string message, Exception exception)
        {
            WrappedLogger.Log(level, message, exception);
        }

        /// <summary>
        /// Logs the specified exception with the specified trace level.
        /// </summary>
        /// <param name="level">The trace level.</param>
        /// <param name="exception">The exception to trace.</param>
        public virtual void Log(LogLevel level, Exception exception)
        {
            WrappedLogger.Log(level, exception);
        }

        /// <summary>
        /// Logs the specified log entry.
        /// </summary>
        /// <param name="entry">The entry to log.</param>
        public virtual void Log(LogEntry entry)
        {
            WrappedLogger.Log(entry);
        }

        #endregion
    }
}
