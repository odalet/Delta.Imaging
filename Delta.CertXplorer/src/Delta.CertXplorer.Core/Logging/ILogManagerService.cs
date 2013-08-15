using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Delta.CertXplorer.Logging
{
    /// <summary>
    /// Defines the methods that make a Log manager.
    /// </summary>
    /// <remarks>
    /// A Log manager service hosts several log services indexed by a name.
    /// </remarks>
    public interface ILogManagerService
    {
        /// <summary>
        /// Gets the default log service.
        /// </summary>
        ILogService DefaultLogger { get; }

        /// <summary>
        /// Gets the <see cref="Delta.CertXplorer.Logging.ILogService"/> with the specified type.
        /// </summary>
        ILogService this[Type type] { get; }

        /// <summary>
        /// Gets the <see cref="Delta.CertXplorer.Logging.ILogService"/> with the specified logger name.
        /// </summary>
        ILogService this[string loggerName] { get; }

        /// <summary>
        /// Gets the default log service.
        /// </summary>
        ILogService GetDefaultLogService();

        /// <summary>
        /// Gets a log service by its type.
        /// </summary>
        /// <param name="type">The type of the log service to get back.</param>
        /// <returns>A <see cref="Delta.CertXplorer.Logging.ILogService"/> if found; otherwise <c>null</c>.</returns>
        ILogService GetLogService(Type type);

        /// <summary>
        /// Gets the log service by its type.
        /// </summary>
        /// <param name="name">The name of the log service to get back.</param>
        /// <returns>A <see cref="Delta.CertXplorer.Logging.ILogService"/> if found; otherwise <c>null</c>.</returns>
        ILogService GetLogService(string name);

        /// <summary>
        /// Adds a log service to the inner dictionary of log services.
        /// </summary>
        /// <param name="service">The log service to add.</param>
        void AddLogService(ILogService service);

        /// <summary>
        /// Adds a log service to the inner dictionary of log services.
        /// </summary>
        /// <param name="name">The name of the newly added log service.</param>
        /// <param name="service">The log service to add.</param>
        void AddLogService(string name, ILogService service);

        /// <summary>
        /// Removes an existing log service from the inner dictionary by its type.
        /// </summary>
        /// <param name="type">The type of the log service to remove.</param>
        /// <returns>The removed log service.</returns>
        ILogService RemoveLogService(Type type);

        /// <summary>
        /// Removes an existing log service from the inner dictionary by its name.
        /// </summary>
        /// <param name="name">The name of the log service to remove.</param>
        /// <returns>The removed log service.</returns>
        ILogService RemoveLogService(string name);

        /// <summary>
        /// Sets the service of type <paramref name="type"/> to be the default log service.
        /// </summary>
        /// <param name="type">The type of the future default service.</param>
        /// <returns>The old default log service.</returns>
        ILogService SetDefaultLogService(Type type);

        /// <summary>
        /// Sets the service of type <paramref name="name"/> to be the default log service.
        /// </summary>
        /// <param name="name">The name of the future default service.</param>
        /// <returns>The old default log service.</returns>
        ILogService SetDefaultLogService(string name);
    }
}
