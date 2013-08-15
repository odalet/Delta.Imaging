using System;

namespace Delta.CertXplorer.Logging
{
    /// <summary>
    /// <see cref="LogSource"/> base implementation.
    /// </summary>
    public class LogSource : IEquatable<LogSource>
    {
        private static readonly Func<LogSource, LogEntry, LogEntry> defaultProcessLogEntryFunction =
            (source, entry) =>
            {
                entry.Source = source;

                // Modify message to display the source if not the root source
                string formattedSourceName = FormatSourceName(source);
                if (!string.IsNullOrEmpty(formattedSourceName))
                {
                    if (string.IsNullOrEmpty(entry.Message)) entry.Message =
                        formattedSourceName;
                    else entry.Message = string.Format("{0} {1}",
                        formattedSourceName, entry.Message);
                }

                return entry;
            };

        private static Func<LogSource, LogEntry, LogEntry> processLogEntryFunction = null;

        /// <summary>
        /// The name of the root source.
        /// </summary>
        public const string RootSourceName = "$";

        /// <summary>The root source.</summary>
        public static readonly LogSource Root = new LogSource(RootSourceName);

        /// <summary>
        /// Gets or sets the function that processes log entries before they are written.
        /// </summary>
        /// <value>The process log entry function.</value>
        public static Func<LogSource, LogEntry, LogEntry> ProcessLogEntryFunction
        {
            get { return processLogEntryFunction ?? defaultProcessLogEntryFunction; }
            set { processLogEntryFunction = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogSource"/> class.
        /// </summary>
        /// <param name="name">The source name.</param>
        public LogSource(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets the name of this Log source.
        /// </summary>
        /// <value>The Log source name.</value>
        public string Name { get; protected set; }

        /// <summary>
        /// Processes the specified log entry before it is written to its log destination.
        /// </summary>
        /// <param name="entry">The log entry.</param>
        /// <returns>The specified log entry, or <c>null</c> if the entry should not be logged.</returns>
        public LogEntry ProcessLogEntry(LogEntry entry)
        {
            return ProcessLogEntryFunction(this, entry);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }
        
        #region IEquatable<LogSource> Members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        public bool Equals(LogSource other)
        {
            if (other == null) return false;
            return other.Name == Name;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">
        /// The <paramref name="obj"/> parameter is null.
        /// </exception>
        public override bool Equals(object obj)
        {
            if (obj is LogSource) return Equals((LogSource)obj);
            return false;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        #endregion
        
        /// <summary>
        /// Formats the name of the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Formatted source name.</returns>
        private static string FormatSourceName(LogSource source)
        {
            if (source == null) return string.Empty;
            if (source.Name == RootSourceName) return string.Empty;

            var name = source.Name.StartsWith(RootSourceName + ".") ?
                source.Name.Substring(RootSourceName.Length + 1) : source.Name;
            return string.Format("[{0}]", name);
        }
    }
}
