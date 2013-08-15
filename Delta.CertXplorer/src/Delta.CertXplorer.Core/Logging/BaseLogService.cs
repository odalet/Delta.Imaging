using System;
using System.Text;
using System.Globalization;
using System.Diagnostics;

namespace Delta.CertXplorer.Logging
{
    /// <summary>
    /// Abstract base class allowing for a quick implementatio of a trace service.
    /// </summary>
    /// <seealso cref="Delta.CertXplorer.Logging.ILogService"/>
    public abstract class BaseLogService : ILogService
    {
        #region ILogService Members

        /// <summary>
        /// Logs the specified message with the specified trace level.
        /// </summary>
        /// <param name="level">The trace level.</param>
        /// <param name="message">The message to trace.</param>
        public void Log(LogLevel level, string message)
        {
            Log(level, message, null);
        }

        /// <summary>
        /// Logs the specified exception with the specified trace level.
        /// </summary>
        /// <param name="level">The trace level.</param>
        /// <param name="exception">The exception to trace.</param>
        public void Log(LogLevel level, Exception exception)
        {
            Log(level, string.Empty, exception);
        }

        /// <summary>
        /// Logs the specified message and exception with the specified trace level.
        /// </summary>
        /// <param name="level">The trace level.</param>
        /// <param name="message">The message to trace.</param>
        /// <param name="exception">The exception to trace.</param>
        public void Log(LogLevel level, string message, Exception exception)
        {
            Log(new LogEntry() { Level = level, Message = message, Exception = exception });
        }
        
        /// <summary>
        /// Logs the specified log entry.
        /// </summary>
        /// <param name="entry">The entry to log.</param>
        public abstract void Log(LogEntry entry);

        #endregion  
    }
}
