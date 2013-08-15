using System;

namespace Delta.CertXplorer.Logging
{
    // Is this really thread-safe?
    public class ThreadSafeLogServiceWrapper : LogServiceWrapper
    {
        private ILogService logger = null;

        public ThreadSafeLogServiceWrapper(ILogService logService)
            : base(logService)
        {
            logger = logService;
        }

        public override void Log(LogEntry entry)
        {
            lock(logger) logger.Log(entry);
        }

        public override void Log(LogLevel level, Exception exception)
        {
            lock (logger) logger.Log(level, exception);
        }

        public override void Log(LogLevel level, string message)
        {
            lock (logger) logger.Log(level, message);
        }

        public override void Log(LogLevel level, string message, Exception exception)
        {
            lock (logger) logger.Log(level, message, exception);
        }
    }
}
