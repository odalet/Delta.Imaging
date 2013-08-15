using System;

using Delta.CertXplorer.Logging;

namespace Delta.CertXplorer.PluginsManagement
{
    internal class PluginsLogService : Delta.CertXplorer.Extensibility.Logging.ILogService
    {
        private ILogService log = null;
        private string source = string.Empty;

        public PluginsLogService(string pluginName)
        {
            // Determine the plugin name & build a source
            source = string.Format("$.{0}", pluginName);

            // Get the global log service
            log = This.Logger;
        }

        #region ILogService Members

        public void Log(Delta.CertXplorer.Extensibility.Logging.LogLevel level, string message)
        {
            log.Log(GetLevel(level), message, source);
        }

        public void Log(Delta.CertXplorer.Extensibility.Logging.LogLevel level, string message, Exception exception)
        {
            log.Log(GetLevel(level), message, exception, source);
        }

        public void Log(Delta.CertXplorer.Extensibility.Logging.LogLevel level, Exception exception)
        {
            log.Log(GetLevel(level), exception, source);
        }

        public void Log(Delta.CertXplorer.Extensibility.Logging.LogEntry entry)
        {
            log.Log(GetEntry(entry, source));
        }

        #endregion

        private LogLevel GetLevel(Delta.CertXplorer.Extensibility.Logging.LogLevel level)
        {
            switch (level)
            {
                case Delta.CertXplorer.Extensibility.Logging.LogLevel.All:
                    return LogLevel.All;
                case Delta.CertXplorer.Extensibility.Logging.LogLevel.Debug:
                    return LogLevel.Debug;
                case Delta.CertXplorer.Extensibility.Logging.LogLevel.Error:
                    return LogLevel.Error;
                case Delta.CertXplorer.Extensibility.Logging.LogLevel.Fatal:
                    return LogLevel.Fatal;
                case Delta.CertXplorer.Extensibility.Logging.LogLevel.Info:
                    return LogLevel.Info;
                case Delta.CertXplorer.Extensibility.Logging.LogLevel.Off:
                    return LogLevel.Off;
                case Delta.CertXplorer.Extensibility.Logging.LogLevel.Verbose:
                    return LogLevel.Verbose;
                case Delta.CertXplorer.Extensibility.Logging.LogLevel.Warning:
                    return LogLevel.Warning;
            }

            return LogLevel.Info; // default
        }

        private LogEntry GetEntry(Delta.CertXplorer.Extensibility.Logging.LogEntry entry, string source)
        {
            return new LogEntry()
            {
                Exception = entry.Exception,
                Level = GetLevel(entry.Level),
                Message = entry.Message,
                SourceName = source,
                Tag = entry.Tag,
                TimeStamp = entry.TimeStamp
            };
        }
    }
}
