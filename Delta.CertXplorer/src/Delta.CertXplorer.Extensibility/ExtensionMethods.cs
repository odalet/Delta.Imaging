using System;
using System.Text;
using System.Globalization;
using Delta.CertXplorer.Extensibility.Logging;

namespace Delta.CertXplorer.Extensibility
{
    public static class ExtensionMethods
    {
        #region Exception extensions

        /// <summary>
        /// Returns Exception information into a formatted string.
        /// </summary>
        /// <param name="exception">The exception to describe.</param>
        /// <returns>Formated (and indented) string giving information about <paramref name="exception"/></returns>
        public static string ToFormattedString(this Exception exception)
        {
            if (exception == null) return string.Empty;

            const string tab = "   ";
            const string leafEx = " + ";
            const string leafTr = " | ";
            string indent = string.Empty;

            var sb = new StringBuilder();
            for (Exception currentException = exception; currentException != null; currentException = currentException.InnerException)
            {
                sb.Append(indent);
                sb.Append(leafEx);
                sb.Append("[");
                sb.Append(currentException.GetType().ToString());
                sb.Append("] ");
                sb.Append(currentException.Message);
                sb.Append(Environment.NewLine);

                indent += tab;

                if (currentException.StackTrace != null)
                {
                    string[] stackTrace = currentException.StackTrace
                        .Replace(Environment.NewLine, "\n")
                        .Split('\n');

                    for (int i = 0; i < stackTrace.Length; i++)
                    {
                        sb.Append(indent);
                        sb.Append(leafTr);
                        sb.Append(stackTrace[i].Trim());
                        sb.Append(Environment.NewLine);
                    }
                }
            }

            return sb.ToString();
        }

        #endregion

        #region DateTime extensions

        private static DateTime[] dayCache = null;

        /// <summary>
        /// Converts the date to a short invariant string representation (format = &quot;yyyy/MM/dd&quot;).
        /// </summary>
        /// <param name="dateTime">The date time to convert.</param>
        /// <returns>The converted string representation.</returns>
        public static string ToShortInvariantString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy/MM/dd");
        }

        /// <summary>
        /// Converts the date to a long invariant string representation (format = &quot;yyyy/MM/dd hh:mm:ss&quot;).
        /// </summary>
        /// <param name="dateTime">The date time to convert.</param>
        /// <returns>The converted string representation.</returns>
        public static string ToLongInvariantString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy/MM/dd HH:mm:ss");
        }

        /// <summary>
        /// Converts the date to a very long invariant string representation (format = &quot;yyyy/MM/dd hh:mm:ss.FFFFFFF&quot;).
        /// </summary>
        /// <param name="dateTime">The date time to convert.</param>
        /// <returns>The converted string representation.</returns>
        public static string ToVeryLongInvariantString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy/MM/dd HH:mm:ss.FFFFFFF");
        }

        public static string ToShortString(this DayOfWeek day)
        {
            return ToShortString(day, CultureInfo.CurrentUICulture);
        }

        public static string ToShortString(this DayOfWeek day, CultureInfo culture)
        {
            return ToFormattedString(day, "ddd", culture);
        }

        public static string ToLongString(this DayOfWeek day)
        {
            return ToLongString(day, CultureInfo.CurrentUICulture);
        }

        public static string ToLongString(this DayOfWeek day, CultureInfo culture)
        {
            return ToFormattedString(day, "dddd", culture);
        }

        private static string ToFormattedString(this DayOfWeek day, string format, CultureInfo culture)
        {
            if (dayCache == null)
            {
                dayCache = new DateTime[7];
                int sundayIndex = 2; // January the 2nd 2000 was a Sunday.                
                for (int i = 0; i < 7; i++)
                {
                    dayCache[i] = new DateTime(2000, 1, sundayIndex);
                    sundayIndex++;
                }
            }

            var dt = dayCache[(int)day];
            return dt.ToString(format, culture);
        } 

        #endregion

        #region ILogService extensions

        #region Verbose

        /// <summary>
        /// Logs the specified message with the <see cref="LogLevel.Verbose"/> trace level.
        /// </summary>
        /// <param name="log">The logging service used to output the trace.</param>
        /// <param name="message">The message to trace.</param>
        public static void Verbose(this ILogService log, string message) { log.Log(LogLevel.Verbose, message); }

        /// <summary>
        /// Logs the specified exception with the <see cref="LogLevel.Verbose"/> trace level.
        /// </summary>
        /// <param name="log">The logging service used to output the trace.</param>
        /// <param name="exception">The exception to trace.</param>
        public static void Verbose(this ILogService log, Exception exception) { log.Log(LogLevel.Verbose, exception); }

        /// <summary>
        /// Logs the specified message and exception with the <see cref="LogLevel.Verbose"/> trace level.
        /// </summary>
        /// <param name="log">The logging service used to output the trace.</param>
        /// <param name="message">The message to trace.</param>
        /// <param name="exception">The exception to trace.</param>
        public static void Verbose(this ILogService log, string message, Exception exception) { log.Log(LogLevel.Verbose, message, exception); }
        
        #endregion

        #region Debug

        /// <summary>
        /// Logs the specified message with the <see cref="LogLevel.Debug"/> trace level.
        /// </summary>
        /// <param name="log">The logging service used to output the trace.</param>
        /// <param name="message">The message to trace.</param>
        public static void Debug(this ILogService log, string message) { log.Log(LogLevel.Debug, message); }

        /// <summary>
        /// Logs the specified exception with the <see cref="LogLevel.Debug"/> trace level.
        /// </summary>
        /// <param name="log">The logging service used to output the trace.</param>
        /// <param name="exception">The exception to trace.</param>
        public static void Debug(this ILogService log, Exception exception) { log.Log(LogLevel.Debug, exception); }

        /// <summary>
        /// Logs the specified message and exception with the <see cref="LogLevel.Debug"/> trace level.
        /// </summary>
        /// <param name="log">The logging service used to output the trace.</param>
        /// <param name="message">The message to trace.</param>
        /// <param name="exception">The exception to trace.</param>
        public static void Debug(this ILogService log, string message, Exception exception) { log.Log(LogLevel.Debug, message, exception); }
        
        #endregion

        #region Info

        /// <summary>
        /// Logs the specified message with the <see cref="LogLevel.Info"/> trace level.
        /// </summary>
        /// <param name="log">The logging service used to output the trace.</param>
        /// <param name="message">The message to trace.</param>
        public static void Info(this ILogService log, string message) { log.Log(LogLevel.Info, message); }

        /// <summary>
        /// Logs the specified exception with the <see cref="LogLevel.Info"/> trace level.
        /// </summary>
        /// <param name="log">The logging service used to output the trace.</param>
        /// <param name="exception">The exception to trace.</param>
        public static void Info(this ILogService log, Exception exception) { log.Log(LogLevel.Info, exception); }

        /// <summary>
        /// Logs the specified message and exception with the <see cref="LogLevel.Info"/> trace level.
        /// </summary>
        /// <param name="log">The logging service used to output the trace.</param>
        /// <param name="message">The message to trace.</param>
        /// <param name="exception">The exception to trace.</param>
        public static void Info(this ILogService log, string message, Exception exception) { log.Log(LogLevel.Info, message, exception); }
        
        #endregion

        #region Warning

        /// <summary>
        /// Logs the specified message with the <see cref="LogLevel.Warning"/> trace level.
        /// </summary>
        /// <param name="log">The logging service used to output the trace.</param>
        /// <param name="message">The message to trace.</param>
        public static void Warning(this ILogService log, string message) { log.Log(LogLevel.Warning, message); }

        /// <summary>
        /// Logs the specified exception with the <see cref="LogLevel.Warning"/> trace level.
        /// </summary>
        /// <param name="log">The logging service used to output the trace.</param>
        /// <param name="exception">The exception to trace.</param>
        public static void Warning(this ILogService log, Exception exception) { log.Log(LogLevel.Warning, exception); }

        /// <summary>
        /// Logs the specified message and exception with the <see cref="LogLevel.Warning"/> trace level.
        /// </summary>
        /// <param name="log">The logging service used to output the trace.</param>
        /// <param name="message">The message to trace.</param>
        /// <param name="exception">The exception to trace.</param>
        public static void Warning(this ILogService log, string message, Exception exception) { log.Log(LogLevel.Warning, message, exception); }
        
        #endregion

        #region Error
        
        /// <summary>
        /// Logs the specified message with the <see cref="LogLevel.Error"/> trace level.
        /// </summary>
        /// <param name="log">The logging service used to output the trace.</param>
        /// <param name="message">The message to trace.</param>
        public static void Error(this ILogService log, string message) { log.Log(LogLevel.Error, message); }

        /// <summary>
        /// Logs the specified exception with the <see cref="LogLevel.Error"/> trace level.
        /// </summary>
        /// <param name="log">The logging service used to output the trace.</param>
        /// <param name="exception">The exception to trace.</param>
        public static void Error(this ILogService log, Exception exception) { log.Log(LogLevel.Error, exception); }

        /// <summary>
        /// Logs the specified message and exception with the <see cref="LogLevel.Error"/> trace level.
        /// </summary>
        /// <param name="log">The logging service used to output the trace.</param>
        /// <param name="message">The message to trace.</param>
        /// <param name="exception">The exception to trace.</param>
        public static void Error(this ILogService log, string message, Exception exception) { log.Log(LogLevel.Error, message, exception); }

        #endregion

        #region Fatal

        /// <summary>
        /// Logs the specified message with the <see cref="LogLevel.Fatal"/> trace level.
        /// </summary>
        /// <param name="log">The logging service used to output the trace.</param>
        /// <param name="message">The message to trace.</param>
        public static void Fatal(this ILogService log, string message) { log.Log(LogLevel.Fatal, message); }

        /// <summary>
        /// Logs the specified exception with the <see cref="LogLevel.Fatal"/> trace level.
        /// </summary>
        /// <param name="log">The logging service used to output the trace.</param>
        /// <param name="exception">The exception to trace.</param>
        public static void Fatal(this ILogService log, Exception exception) { log.Log(LogLevel.Fatal, exception); }

        /// <summary>
        /// Logs the specified message and exception with the <see cref="LogLevel.Fatal"/> trace level.
        /// </summary>
        /// <param name="log">The logging service used to output the trace.</param>
        /// <param name="message">The message to trace.</param>
        /// <param name="exception">The exception to trace.</param>
        public static void Fatal(this ILogService log, string message, Exception exception) { log.Log(LogLevel.Fatal, message, exception); }

        #endregion

        #endregion

        #region IServiceProvider extensions

        /// <summary>
        /// Obtient une instance de service.
        /// </summary>
        /// <typeparam name="T">Type de service à récupérer.</typeparam>
        /// <param name="serviceProvider">The service provider.</param>
        /// <returns>L'instance de service ou <b>null</b></returns>
        public static T GetService<T>(this IServiceProvider serviceProvider) where T : class
        {
            return serviceProvider.GetService(typeof(T)) as T;
        }

        /// <summary>
        /// Obtient une instance de service.
        /// </summary>
        /// <typeparam name="T">Type de service à récupérer.</typeparam>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="mandatory">Si à <c>true</c>, lève une
        /// exception <see cref="Delta.CertXplorer.ComponentModel.ServiceNotFoundException&lt;T&gt;"/>
        /// dans le cas où le service demandé n'a pas pu être trouvé.</param>
        /// <returns>L'instance de service ou <b>null</b></returns>
        public static T GetService<T>(this IServiceProvider serviceProvider, bool mandatory) where T : class
        {
            T t = serviceProvider.GetService<T>();
            if (t == null)
            {
                if (mandatory) throw new ServiceNotFoundException<T>();
                else return null;
            }

            return t;
        }

        #endregion
    }
}
