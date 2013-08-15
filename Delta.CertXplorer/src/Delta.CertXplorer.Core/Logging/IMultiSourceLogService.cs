using System;

namespace Delta.CertXplorer.Logging
{
    /// <summary>
    /// Specialized version of <see cref="ILogService"/> allowing to choose a log source.
    /// </summary>
    public interface IMultiSourceLogService : ILogService
    {
        /// <summary>
        /// Gets the <see cref="Delta.CertXplorer.Logging.ILogService"/> with the specified source name.
        /// </summary>
        /// <remarks>
        /// If the specified source does not exist, it is created.
        /// </remarks>
        /// <value>An instance of <see cref="ILogService"/>. Never <c>null</c>.</value>
        ILogService this[string sourceName] { get; }

        /// <summary>
        /// Gets the <see cref="Delta.CertXplorer.Logging.ILogService"/> with the specified source.
        /// </summary>
        /// <remarks>
        /// If the specified source does not exist, it is created.
        /// </remarks>
        /// <value>An instance of <see cref="ILogService"/>. Never <c>null</c>.</value>
        ILogService this[LogSource source] { get; }

        /// <summary>
        /// Gets the current source associated with this logger.
        /// </summary>
        /// <value>The current source (never <c>null</c>).</value>
        LogSource CurrentSource { get; }
    }
}
