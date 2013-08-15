using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Delta.CertXplorer.Logging
{
    /// <summary>
    /// This logging service reimplements the functionality that was previously available
    /// in the <see cref="Delta.CertXplorer.Diagnostics.TraceHandler"/> class. It works the same way 
    /// as the old trace class, but it is now exposed as a <see cref="Delta.CertXplorer.Logging.ILogService"/>.
    /// </summary>
    public partial class CertXplorerLogService : BaseLogService
    {
        /// <summary>
        /// This class represents the configuration (to add to <c>app.config</c>)
        /// used to configure the <see cref="Delta.CertXplorerLogService"/>.
        /// </summary>
        private class CertXplorerLogConfig
        {
            /// <summary>
            /// Stores all the constants used to access the configuration file.
            /// </summary>
            private static class ConfigConstants
            {
                public const string EVENTLOG_ENABLED = "eventlog.enabled";
                public const string EVENTLOG_NAME = "eventlog.name";
                public const string EVENTLOG_SOURCENAME = "eventlog.sourcename";
                public const string EVENTLOG_MACHINE = "eventlog.machine";
                public const string EVENTLOG_LOGLEVEL = "eventlog.loglevel";

                public const string TRACING_ENABLED = "tracing.enabled";
                public const string TRACING_FILE_PATH = "tracing.filepath";
                public const string TRACING_FILE_NAME = "tracing.filename";
                public const string TRACING_FILE_EXT = "tracing.fileextension";
                public const string TRACING_TRACELEVEL = "tracing.tracelevel";
            }

            private string configSection = string.Empty;

            private LogLevel eventLogLevel = LogLevel.Off;
            private string eventLogName = "Application";
            private string eventLogMachine = ".";
            private string eventLogSourceName = string.Empty;

            private LogLevel tracingLogLevel = LogLevel.Off;
            private string tracingFilePath = string.Empty;
            private string tracingFileName = string.Empty;
            private string tracingFileExt = ".txt";

            /// <summary>
            /// This list stores warnings and errors that may arise while
            /// reading the configuration. They will be sent to the logger
            /// once the initialization is complete.
            /// </summary>
            private List<LogEntry> storedLogEntries = new List<LogEntry>();

            /// <summary>
            /// Initializes a new instance of the <see cref="Delta.CertXplorerLogConfig"/> class.
            /// </summary>
            /// <param name="section">The name of the configuration section that should be read.</param>
            public CertXplorerLogConfig(string section)
            {
                configSection = section;

                InitializeDefaultConfiguration();
            }

            /// <summary>Gets the event log level.</summary>
            public LogLevel EventLogLevel { get { return eventLogLevel; } }

            /// <summary>Gets the name of the event log.</summary>
            public string EventLogName { get { return eventLogName; } }

            /// <summary>Gets the event log machine.</summary>
            public string EventLogMachine { get { return eventLogMachine; } }

            /// <summary>Gets the name of the event source.</summary>
            public string EventSourceName { get { return eventLogSourceName; } }

            /// <summary>Gets the tracing trace level.</summary>
            public LogLevel TracingLogLevel { get { return tracingLogLevel; } }

            /// <summary>Gets the tracing file path.</summary>
            public string TracingFilePath { get { return tracingFilePath; } }

            /// <summary>Gets the name of the tracing file.</summary>
            public string TracingFileName { get { return tracingFileName; } }

            /// <summary>Gets the tracing file extension.</summary>
            public string TracingFileExt { get { return tracingFileExt; } }

            /// <summary>
            /// Gets the default path to use to store the trace file if <see cref="TracingFilePath"/> is invalid.
            /// </summary>
            public string DefaultPath
            {
                get { return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData); }
            }

            public IList<LogEntry> StoredLogEntries { get { return storedLogEntries; } }

            /// <summary>
            /// Reads the configuration.
            /// </summary>
            public void ReadConfiguration()
            {
                // Reads the configuration file.
                NameValueCollection nvc = null;
                try
                {
                    nvc = (NameValueCollection)ConfigurationManager.GetSection(configSection);
                }
                catch (Exception ex)
                {
                    // We couldn't find configuration, rely on the default config, but trace the error.
                    StoreLogEntry(
                        LogLevel.Error,
                        string.Format(SR.CantReadConfigurationFileSection, configSection),
                        ex);
                }

                if ((nvc == null) || (nvc.Keys.Count == 0)) return; // No configuration? rely opn default configuration

                // Reads configuration keys one by one
                foreach (string k in nvc.Keys)
                {
                    try
                    {
                        switch (k)
                        {
                            case ConfigConstants.EVENTLOG_NAME:
                                eventLogName = nvc[k];
                                break;
                            case ConfigConstants.EVENTLOG_MACHINE:
                                eventLogMachine = nvc[k];
                                break;
                            case ConfigConstants.EVENTLOG_SOURCENAME:
                                eventLogSourceName = nvc[k];
                                break;
                            case ConfigConstants.TRACING_FILE_PATH:
                                tracingFilePath = nvc[k];
                                break;
                            case ConfigConstants.TRACING_FILE_NAME:
                                tracingFileName = nvc[k];
                                break;
                            case ConfigConstants.TRACING_FILE_EXT:
                                tracingFileExt = nvc[k];
                                break;
                            case ConfigConstants.TRACING_TRACELEVEL:
                                tracingLogLevel = ConvertToLogLevel(nvc[k]);
                                break;
                            case ConfigConstants.EVENTLOG_LOGLEVEL:
                                eventLogLevel = ConvertToLogLevel(nvc[k]);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        // We couldn't read the configuration, rely on a muix between the default config and
                        // already read settings, but trace the error.
                        StoreLogEntry(
                            LogLevel.Error,
                            string.Format(SR.CantReadConfigurationWithTypeAndKey, GetType().FullName, k),
                            ex);
                    }
                }
            }

            /// <summary>
            /// Initializes the default configuration.
            /// </summary>
            private void InitializeDefaultConfiguration()
            {
                eventLogLevel = LogLevel.Off;
                eventLogName = "Application";
                eventLogMachine = ".";
                eventLogSourceName = string.Empty;

                tracingLogLevel = LogLevel.All;
                tracingFilePath = DefaultPath;
                try
                {
                    tracingFileName = This.Application.EntryAssembly.FullName.Split(',')[0];
                }
                catch (Exception ex)
                {
                    // we couldn't determine the file name from the entry assembly, so use "defaultLog", and eat the exception!
                    System.Diagnostics.Debug.WriteLine(string.Format("ERROR: {0}", ex));                    
                    tracingFileName = "defaultLog";
                }

                tracingFileExt = ".txt";
            }

            // The level configured in the config file is a value that must first be 
            // converted to TraceLevel, then to LogLevel
            private LogLevel ConvertToLogLevel(string configuredLevel)
            {
                // The log level we'll return if wa can't parse the configuration.
                var defaultLogLevel = LogLevel.Error;

                // Read the configured level as an int.
                int intLevel = 0;

                // If we don't even have an int, return a default log level
                if (int.TryParse(configuredLevel, out intLevel))
                {
                    // Now we have an int. Let's convert it first to a TraceLevel, not a LogLevel.
                    var minTraceLevel = Enum.GetValues(typeof(TraceLevel)).Cast<int>().Min();
                    var maxTraceLevel = Enum.GetValues(typeof(TraceLevel)).Cast<int>().Max();

                    if (intLevel >= minTraceLevel && intLevel <= maxTraceLevel)
                    {
                        var traceLevel = (TraceLevel)intLevel;

                        // Now we have a correct TraceLevel, build a LogLevel
                        switch (traceLevel)
                        {
                            case TraceLevel.Off: return LogLevel.Off;
                            case TraceLevel.Error: return LogLevel.Error;
                            case TraceLevel.Warning: return LogLevel.Warning;
                            case TraceLevel.Info: return LogLevel.Info;
                            case TraceLevel.Verbose: return LogLevel.Verbose;
                        }
                    }
                    else if (intLevel < minTraceLevel) return LogLevel.Off; // < min means nothing for trace levels
                    else if (intLevel > maxTraceLevel) return LogLevel.All; // > max means everything for trace levels
                }

                return defaultLogLevel;
            }

            private void StoreLogEntry(LogLevel level, string message, Exception exception)
            {
                storedLogEntries.Add(new LogEntry()
                {
                    Level = level,
                    Message = message,
                    Exception = exception
                });
            }
        }

        private const string CONFIG_SECTION = "Delta.CertXplorer/log";
        private static CertXplorerLogConfig configuration = null;
        private static bool classIsInitialized = false;
        private static EventLog eventLog = null;
        private static string traceFilename = string.Empty;

        /// <summary>
        /// This list stores warnings and errors that may arise while
        /// initializing the logger.
        /// </summary>
        private List<LogEntry> storedLogEntries = new List<LogEntry>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Delta.CertXplorerLogService"/> class.
        /// </summary>
        public CertXplorerLogService() 
        {
            if (!classIsInitialized) InitializeLogger();
        }

        public override void Log(LogEntry entry)
        {
            LogEntry(entry);
        }

        /// <summary>
        /// Initializes the <see cref="Delta.CertXplorerLogService"/> class.
        /// </summary>
        private void InitializeLogger()
        {
            configuration = new CertXplorerLogConfig(CONFIG_SECTION);
            configuration.ReadConfiguration();

            if (configuration.EventLogLevel < LogLevel.Off) // We must initialize the event log
            {
                try
                {
                    eventLog = new EventLog(
                        configuration.EventLogName,
                        configuration.EventLogMachine,
                        configuration.EventSourceName);
                    Debug.Listeners.Add(new EventLogTraceListener(eventLog));
                }
                catch (Exception ex)
                {
                    string error = string.Format(SR.InitializationError, ex.Message);
                    storedLogEntries.Add(new LogEntry 
                    { 
                        Level = LogLevel.Error, 
                        Message = error, 
                        Exception = ex 
                    });
                }
            }

            if (configuration.TracingLogLevel < LogLevel.Off) // We must initialize the file log
            {
                try
                {
                    string extension = configuration.TracingFileExt;
                    if (!extension.StartsWith(".")) extension = "." + extension;

                    traceFilename = string.Format("{0}_{1}{2}",
                        configuration.TracingFileName,
                        DateTime.Now.ToString("yyyyMMdd"),
                        extension);

                    string path = configuration.TracingFilePath;
                    if (!Directory.Exists(configuration.TracingFilePath))
                    {
                        path = configuration.DefaultPath;
                        string error = string.Format(SR.InvalidTracePath, configuration.TracingFilePath, path);
                        storedLogEntries.Add(new LogEntry { Level = LogLevel.Error, Message = error });
                    }

                    traceFilename = Path.Combine(path, traceFilename);
                }
                catch (Exception ex)
                {
                    string error = string.Format(SR.InitializationError, ex.Message);
                    storedLogEntries.Add(new LogEntry 
                    { 
                        Level = LogLevel.Error, 
                        Message = error, 
                        Exception = ex 
                    });
                }
            }

            // We need to log eventual errors and warnings that could have been raised by the configuration.
            foreach (var entry in configuration.StoredLogEntries) LogEntry(entry);
            // We also need to log eventual errors and warnings that could have been raised in this initialization method.
            storedLogEntries.ForEach(entry => LogEntry(entry));

            classIsInitialized = true;
        }

        private void LogEntry(LogEntry entry)
        {
            // Here, we throw an exception because it is an applicative error,
            // not a logging failure.
            if (entry == null) throw new ArgumentNullException("entry");

            // Do we have a source? If yes ask-it to pre process the entry.
            if (CurrentSource != null) entry = CurrentSource.ProcessLogEntry(entry);
            if (entry == null) return; // The current source decided this entry should not be logged.

            // if trace level is Off, just log to the Visual Studio output window.
            if (entry.Level <= LogLevel.Off) Debug.WriteLine(string.Format("DEBUG: {0}", entry));

            // Eventlog logging
            try
            {
                if (entry.Level <= configuration.EventLogLevel) LogToEventLog(entry);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("ERROR: Logging error.\r\nLogged entry:\r\n{0}\r\nException:{1}", entry, ex));
                //throw ex; // Remark: logging shouldn't trhow exceptions if it can't log...
            }

            // File logging
            try
            {
                if (entry.Level <= configuration.TracingLogLevel) LogToTraceFile(entry);                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("ERROR: Logging error.\r\nLogged entry:\r\n{0}\r\nException:{1}", entry, ex));
                //throw ex; // Remark: logging shouldn't trhow exceptions if it can't log...
            }

            // Console logging: whatever log level was set, we also log to Console.Out.
            try { LogToWriter(Console.Out, entry); }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("ERROR: Logging error.\r\nLogged entry:\r\n{0}\r\nException:{1}", entry, ex));
                //throw ex; // Remark: logging shouldn't trhow exceptions if it can't log...
            }
        }

        private void LogToEventLog(LogEntry entry)
        {
            EventLogEntryType entryType = EventLogEntryType.Error;

            // We convert from LogLevel to EventLogEntryType
            switch (entry.Level)
            {
                case LogLevel.Fatal:
                case LogLevel.Error:
                    entryType = EventLogEntryType.Error;
                    break;
                case LogLevel.Warning:
                    entryType = EventLogEntryType.Warning;
                    break;
                case LogLevel.Info:
                case LogLevel.Debug:
                case LogLevel.Verbose:
                default:
                    entryType = EventLogEntryType.Information;
                    break;
            }

            if (eventLog != null) eventLog.WriteEntry(entry.ToString(), entryType);
            else Debug.WriteLine(string.Format("{0}: {1}", entryType, entry));
        }

        private void LogToTraceFile(LogEntry entry)
        {

            using (FileStream stream = new FileStream(traceFilename, 
                FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            {
                using (TextWriter writer = new StreamWriter(stream))
                {
                    LogToWriter(writer, entry);
                    writer.Flush();
                }
                
                stream.Close();
            }
        }

        private void LogToWriter(TextWriter writer, LogEntry entry)
        {
            string typeName = GetCallingMethod();

            string sourceName = string.Empty;
            if (currentSourceName != DefaultSourceName) sourceName = currentSourceName;

            // Traces the message
            if (string.IsNullOrEmpty(sourceName)) writer.WriteLine(string.Format("{0} - {1} - {2}",
                entry.Level.ToString(),
                typeName,
                entry.TimeStamp));
            else writer.WriteLine(string.Format("[{0}] {1} - {2} - {3}",
                sourceName,
                entry.Level.ToString(),
                typeName,
                entry.TimeStamp));

            if (!string.IsNullOrEmpty(entry.Message)) writer.WriteLine(entry.Message);

            // Traces the exception
            if (entry.Exception != null)
            {
                writer.WriteLine();
                writer.WriteLine(entry.Exception.ToFormattedString());
            }

            writer.WriteLine(new string('-', 60));
            writer.Flush();
        }

        /// <summary>
        /// Gets the calling method.
        /// </summary>
        /// <remarks>
        /// We are searching for the calling method: the first method which is not belonging to Delta.CertXplorerLogService or TraceHandler.
        /// </remarks>
        /// <returns>A string containing the calling method's name.</returns>
        private string GetCallingMethod()
        {
            // We are searching for the calling method: 
            // This is the first method that belongs neither to this class, 
            // nor to any class of the namespaces Delta.CertXplorer.Logging or Delta.CertXplorer.Diagnostics,
            // nor to the Delta.CertXplorer.Extensions.LoggingExtensions class.

            StackTrace stackTrace = new StackTrace();
            MethodBase method = null;
            Type declaringType = MethodBase.GetCurrentMethod().DeclaringType;
            // TODO: remove from original code
//#pragma warning disable 618
//            Type traceHandlerType = typeof(Delta.CertXplorer.Diagnostics.TraceHandler);
//#pragma warning restore
            for (int i = 0; i < stackTrace.FrameCount; i++)
            {
                MethodBase current = stackTrace.GetFrame(i).GetMethod();
                if ((current.DeclaringType != declaringType) && 
                    (!current.DeclaringType.FullName.Contains("Delta.CertXplorer.Logging")) &&
                    (!current.DeclaringType.FullName.Contains("Delta.CertXplorer.Diagnostics")) &&
                    (!current.DeclaringType.FullName.Contains("Delta.CertXplorer.Extensions.LoggingExtensions")))
                {
                    method = current;
                    break;
                }
            }

            return (method != null ?
                string.Format("{0}.{1}", method.DeclaringType.FullName, method.Name) : 
                "?");
        }
    }
}
