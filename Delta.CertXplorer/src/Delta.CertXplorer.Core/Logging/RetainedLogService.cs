using System;
using System.Collections.Generic;

namespace Delta.CertXplorer.Logging
{
    /// <summary>
    /// This log service wraps an existing Log service and keeps track of the 
    /// log entries until its <see cref="RetainedLogService.Flush"/> method is called.
    /// </summary>
    public class RetainedLogService : ILogService, IDisposable
    {
        public const int DefaultMaxEntriesToKeep = 1000;

        private List<LogEntry> logEntries = new List<LogEntry>();
        private int maxEntriesToKeep = DefaultMaxEntriesToKeep;
        private ILogService realLogger = null;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="C:RetainedLogger"/> class.
        /// </summary>
        public RetainedLogService() : this(null, 0) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="C:RetainedLogger"/> class.
        /// </summary>
        /// <param name="maxEntries">The maximum number of entries to keep.</param>
        public RetainedLogService(int maxEntries) : this(null, maxEntries) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="C:RetainedLogger"/> class.
        /// </summary>
        /// <param name="logService">The log service.</param>
        public RetainedLogService(ILogService logService) : this(logService, 0) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="C:RetainedLogger"/> class.
        /// </summary>
        /// <param name="logService">The log service.</param>
        /// <param name="maxEntries">The maximum number of entries to keep.</param>
        public RetainedLogService(ILogService logService, int maxEntries)
        {
            if (logService == null) realLogger = This.Logger;
            else realLogger = logService;

            if (maxEntries > 0) maxEntriesToKeep = maxEntries;
        }

        /// <summary>
        /// Flushes the saved log entries to the real logger.
        /// </summary>
        public void Flush()
        {
            try
            {
                // Now flush the saved log entries to the real logger
                foreach (var entry in logEntries) realLogger.Log(entry);
                logEntries.Clear();
            }
            catch (Exception ex)
            {
                try
                {
                    This.Logger.Error(string.Format("There was an error while flushing the retained log messages: {0}",
                        ex.Message), ex);
                }
                catch (Exception ex2)
                {
                    var debugException = ex2;
                }
            }
        }

        #region ILogService Members

        /// <summary>
        /// Logs the specified entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        public void Log(LogEntry entry)
        {
            if (entry != null)
            {
                while (logEntries.Count >= maxEntriesToKeep)
                    logEntries.RemoveAt(0);

                logEntries.Add(entry);
            }
        }

        /// <summary>
        /// Logs the specified exception with the specified trace level.
        /// </summary>
        /// <param name="level">The trace level.</param>
        /// <param name="exception">The exception to trace.</param>
        public void Log(LogLevel level, Exception exception)
        {
            Log(new LogEntry()
            {
                Level = level,
                Message = null,
                Exception = exception,
                Tag = null,
                TimeStamp = DateTime.Now
            });
        }

        /// <summary>
        /// Logs the specified message and exception with the specified trace level.
        /// </summary>
        /// <param name="level">The trace level.</param>
        /// <param name="message">The message to trace.</param>
        /// <param name="exception">The exception to trace.</param>
        public void Log(LogLevel level, string message, Exception exception)
        {
            Log(new LogEntry()
            {
                Level = level,
                Message = message,
                Exception = exception,
                Tag = null,
                TimeStamp = DateTime.Now
            });
        }

        /// <summary>
        /// Logs the specified message with the specified trace level.
        /// </summary>
        /// <param name="level">The trace level.</param>
        /// <param name="message">The message to trace.</param>
        public void Log(LogLevel level, string message)
        {
            Log(new LogEntry()
            {
                Level = level,
                Message = message,
                Exception = null,
                Tag = null,
                TimeStamp = DateTime.Now
            });
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <remarks>
        /// We flush the retained log entries.
        /// </remarks>
        public void Dispose()
        {
            Flush();
        }

        #endregion
    }
}
