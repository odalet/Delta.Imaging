using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Delta.CertXplorer.Logging
{
    partial class CertXplorerLogService : IMultiSourceLogService
    {
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
                return new CertXplorerLogService() { currentSourceName = sourceName };
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
