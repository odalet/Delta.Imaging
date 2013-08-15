using System;
using log4net.Core;

namespace Delta.CertXplorer.Logging.log4net
{
    internal static class Helper
    {
        /// <summary>
        /// Converts a Delta.CertXplorer flavor trace level (<see cref="Delta.CertXplorer.Logging.LogLevel"/>)
        /// into a log4net like level (<see cref="E:log4net.Core.Level"/>).
        /// </summary>
        /// <param name="level">The trace level to convert.</param>
        /// <returns>log4net converted trace level.</returns>
        public static Level LogLevelToLog4NetLevel(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.All: return Level.All;
                case LogLevel.Verbose: return Level.Verbose;
                case LogLevel.Debug: return Level.Debug;
                case LogLevel.Info: return Level.Info;
                case LogLevel.Warning: return Level.Warn;
                case LogLevel.Error: return Level.Error;
                case LogLevel.Fatal: return Level.Fatal;
                case LogLevel.Off: return Level.Off;
            }

            return Level.All;
        }

        /// <summary>
        /// Converts a log4net flavor trace level (<see cref="E:log4net.Core.Level"/>)
        /// into a Delta.CertXplorer like level (<see cref="Delta.CertXplorer.Logging.LogLevel"/>).
        /// </summary>
        /// <param name="level">The log4net level to convert.</param>
        /// <returns>Delta.CertXplorer converted trace level.</returns>
        public static LogLevel Log4NetLevelToLogLevel(Level level)
        {
            if (level == Level.All) return LogLevel.All;
            if (level == Level.Verbose) return LogLevel.Verbose;
            if (level == Level.Debug) return LogLevel.Debug;
            if (level == Level.Info) return LogLevel.Info;
            if (level == Level.Warn) return LogLevel.Warning;
            if (level == Level.Error) return LogLevel.Error;
            if (level == Level.Fatal) return LogLevel.Fatal;
            if (level == Level.Off) return LogLevel.Off;
            return LogLevel.All;
        } 
    }
}
