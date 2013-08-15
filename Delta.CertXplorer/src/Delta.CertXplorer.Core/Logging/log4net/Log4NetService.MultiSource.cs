using System;
using System.Collections.Generic;

using log4net;

namespace Delta.CertXplorer.Logging.log4net
{
    partial class Log4NetService : IMultiSourceLogService
    {
        // In order for other sources to onherit this logger properties, they should begin with "$."
        public const string DefaultSourceName = LogSource.RootSourceName;

        private string currentSourceName = DefaultSourceName;

        #region IMultiSourceLogService Members

        /// <summary>
        /// Gets the <see cref="Delta.CertXplorer.Logging.ILogService"/> with the specified source name.
        /// </summary>
        /// <value></value>
        public ILogService this[string sourceName]
        {
            get 
            {
                if (string.IsNullOrEmpty(sourceName)) sourceName = currentSourceName;
                if (sourceName == currentSourceName) return this;
                return new Log4NetService(configurationFileInfo, false) { currentSourceName = sourceName };
            }
        }

        /// <summary>
        /// Gets the <see cref="Delta.CertXplorer.Logging.ILogService"/> with the specified source.
        /// </summary>
        /// <value></value>
        public ILogService this[LogSource source]
        {
            get 
            {
                if ((source != null) && (source.Name == currentSourceName)) 
                    return this;
                return this[source.Name];
            }
        }

        /// <summary>
        /// Gets the current source associated with this logger.
        /// </summary>
        /// <value>The current source (never <c>null</c>).</value>
        public LogSource CurrentSource
        {
            get { return new LogSource(currentSourceName); }
        }

        #endregion
    }
}
