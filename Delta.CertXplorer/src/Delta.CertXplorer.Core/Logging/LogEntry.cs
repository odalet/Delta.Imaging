using System;
using System.Text;

namespace Delta.CertXplorer.Logging
{
    /// <summary>
    /// This class stores the properties, a logging message is made of.
    /// </summary>
    public class LogEntry
    {
        private LogSource source = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="LogEntry"/> class.
        /// </summary>
        public LogEntry() { TimeStamp = DateTime.Now; }

        /// <summary>
        /// Gets or sets the trace level for this entry.
        /// </summary>
        public LogLevel Level { get; set; }

        /// <summary>
        /// Gets or sets the log source for this entry.
        /// </summary>
        public LogSource Source { get; set; }

        /// <summary>
        /// Gets or sets the name of the log source for this entry.
        /// </summary>
        public string SourceName
        {
            get
            {
                if (source == null) return string.Empty;
                return source.Name;
            }
            set { source = new LogSource(value); }
        }

        /// <summary>
        /// Gets or sets the message object for this entry.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the exception for this entry.
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Gets or sets the creation time of this entry.
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets additional data related to this log entry.
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the default string representation of this entry.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (Source != null)
            {
                var source = Source.ToString();
                if (!string.IsNullOrEmpty(source))
                    sb.AppendFormat("[{0}] ", source);
            }

            sb.Append(Level.ToString().ToUpperInvariant());

            if ((TimeStamp != DateTime.MinValue) && (TimeStamp != DateTime.MaxValue))
                sb.AppendFormat(" [{0}]", TimeStamp.ToLongInvariantString());

            sb.Append(": ");

            StringBuilder sbText = new StringBuilder();

            if (!string.IsNullOrEmpty(Message))
            {
                sbText.Append(Message);
                if (Exception != null) sbText.Append(" - ");
            }

            if (Exception != null)
                sbText.Append(Exception.ToFormattedString());

            string text = sbText.ToString();
            if (string.IsNullOrEmpty(text)) sb.Append("No message");
            else sb.Append(text);

            return sb.ToString();
        }
    }
}
