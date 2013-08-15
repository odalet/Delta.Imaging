using System;

namespace Delta.CertXplorer.Extensibility.Logging
{
    /// <summary>
    /// Represents a logging service dedicated to plugins.
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// Logs the specified message with the specified trace level.
        /// </summary>
        /// <param name="level">The trace level.</param>
        /// <param name="message">The message to trace.</param>
        void Log(LogLevel level, string message);

        /// <summary>
        /// Logs the specified message and exception with the specified trace level.
        /// </summary>
        /// <param name="level">The trace level.</param>
        /// <param name="message">The message to trace.</param>
        /// <param name="exception">The exception to trace.</param>
        void Log(LogLevel level, string message, Exception exception);

        /// <summary>
        /// Logs the specified exception with the specified trace level.
        /// </summary>
        /// <param name="level">The trace level.</param>
        /// <param name="exception">The exception to trace.</param>
        void Log(LogLevel level, Exception exception);

        /// <summary>
        /// Logs the specified log entry.
        /// </summary>
        /// <param name="entry">The entry to log.</param>
        void Log(LogEntry entry);
    }
}
