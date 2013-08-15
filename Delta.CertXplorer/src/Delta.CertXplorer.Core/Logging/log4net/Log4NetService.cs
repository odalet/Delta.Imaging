using System;
using System.IO;
using System.Globalization;
using System.Diagnostics;

using log4net;
using log4net.Core;
using log4net.Util;
using log4net.Config;

using Delta.CertXplorer.Logging;
using Delta.CertXplorer.UI;

namespace Delta.CertXplorer.Logging.log4net
{
    /// <summary>
    /// Wraps the logging Log4Net logging framework into an <see cref="Delta.CertXplorer.Logging.ILogService"/>
    /// so that it can be used as any logging service of the Delta.CertXplorer framework.
    /// </summary>
    public partial class Log4NetService : BaseLogService, ITextBoxAppendable
    {
        private static Type thisServiceType = typeof(Log4NetService);
        private FileInfo configurationFileInfo = null;

        /// <summary>
        /// Gets the current log object.
        /// </summary>
        /// <value>The current log object.</value>
        private ILog CurrentLog
        {
            get
            {
                //var loggers = LogManager.GetCurrentLoggers();
                string name = currentSourceName;
                if (string.IsNullOrEmpty(name)) name = DefaultSourceName;
                return LogManager.GetLogger(name);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Log4NetService"/> class.
        /// </summary>
        /// <remarks>
        /// As we don't state anything, log4net will look for its configuration inside
        /// the application's configuration file (<b>web.config</b> or <b>app.exe.config</b>)
        /// which will have to contain a <b>&lt;log4net&gt;</b> section.
        /// </remarks>
        public Log4NetService() : this(null, true) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Log4NetService"/> class.
        /// </summary>
        /// <param name="configurationFile">Configuration file specific to log4net.</param>
        public Log4NetService(FileInfo configurationFile) : this(configurationFile, true) { }

        private Log4NetService(FileInfo configurationFile, bool configure)
        {
            if (configurationFile == null) configurationFile = new FileInfo(
                AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            configurationFileInfo = configurationFile;

            if (configure) XmlConfigurator.ConfigureAndWatch(configurationFileInfo);
        }

        /// <summary>
        /// Gets the underlying log4net logger.
        /// </summary>
        /// <returns>An <see cref="I:log4net.ILog"/> instance.</returns>
        public ILog GetLogger() { return CurrentLog; }

        /// <summary>
        /// Logs the specified log entry.
        /// </summary>
        /// <param name="entry">The entry to log.</param>
        public override void Log(LogEntry entry)
        {
            if (entry == null) throw new ArgumentNullException("entry");

            // Do we have a source? If yes ask-it to pre process the entry.
            if (CurrentSource != null) entry = CurrentSource.ProcessLogEntry(entry);
            if (entry == null) return; // The current source decided this entry should not be logged.

            CurrentLog.Logger.Log(
                thisServiceType,
                Helper.LogLevelToLog4NetLevel(entry.Level),
                entry.Message,
                entry.Exception);
        }

        #region ITextBoxAppendable Members

        /// <summary>
        /// Adds the log box.
        /// </summary>
        /// <param name="textboxWrapper">The textbox wrapper.</param>
        /// <returns></returns>
        public ITextBoxAppender AddLogBox(ThreadSafeTextBoxWrapper textboxWrapper)
        {
            return AddLogBox(textboxWrapper, string.Empty);
        }

        public ITextBoxAppender AddLogBox(ThreadSafeTextBoxWrapper textboxWrapper, string patternLayout)
        {
            if (textboxWrapper == null) throw new ArgumentNullException("textboxWrapper");
            
            if (CurrentLog != null)
            {
                var appenderAttachable = CurrentLog.Logger as IAppenderAttachable;
                if (appenderAttachable != null)
                {
                    TextBoxBaseAppender appender = null;
                    if (string.IsNullOrEmpty(patternLayout))
                        appender = new TextBoxBaseAppender(textboxWrapper);
                    else appender = new TextBoxBaseAppender(textboxWrapper, 
                        new global::log4net.Layout.PatternLayout(patternLayout));

                    appender.LogThreshold = LogLevel.All;
                    appenderAttachable.AddAppender(appender);
                    return appender;
                }
            }

            return null;
        }

        #endregion
    }
}
