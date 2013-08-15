using System;
using System.Collections.Generic;

namespace Delta.CertXplorer.Logging
{
    /// <summary>
    /// This is the default Log Manager service implementation.
    /// </summary>
    /// <remarks>
    /// This class hosts Log services indexed by a name. It also acts
    /// as a <see cref="ILogService"/>: all the log messages sent to it
    /// are redirected to its <see cref="LogManagerService.DefaultLogger"/> property.
    /// </remarks>
    public class LogManagerService : ILogManagerService, ILogService
    {
        /// <summary>Dictionary of log services indexed by a string.</summary>
        private Dictionary<string, ILogService> logServices = new Dictionary<string, ILogService>();

        /// <summary>The default log service.</summary>
        private ILogService defaultLogService = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogManagerService"/> class.
        /// </summary>
        /// <param name="defaultService">The default log service.</param>
        public LogManagerService(ILogService defaultService)
        {
            if (defaultService == null) defaultLogService = new SimpleLogService();
            else defaultLogService = defaultService;
        }
        
        #region ILogManagerService Members

        /// <summary>
        /// Gets the default log service.
        /// </summary>
        public ILogService DefaultLogger { get { return defaultLogService; } }

        /// <summary>
        /// Gets the <see cref="Delta.CertXplorer.Logging.ILogService"/> with the specified type.
        /// </summary>
        public ILogService this[Type type] { get { return GetLogService(type); } }

        /// <summary>
        /// Gets the <see cref="Delta.CertXplorer.Logging.ILogService"/> with the specified logger name.
        /// </summary>
        /// <value></value>
        public ILogService this[string loggerName] { get { return GetLogService(loggerName); } }

        /// <summary>
        /// Gets the default log service.
        /// </summary>
        /// <returns></returns>
        public ILogService GetDefaultLogService() { return GetLogService(string.Empty); }

        /// <summary>
        /// Gets a log service by its type.
        /// </summary>
        /// <remarks>
        /// If <paramref name="type"/> is null, this method returns the <b>default</b> log service.
        /// </remarks>
        /// <param name="type">The type of the log service to get back.</param>
        /// <returns>
        /// A <see cref="Delta.CertXplorer.Logging.ILogService"/> if found; otherwise <c>null</c>.
        /// </returns>
        public ILogService GetLogService(Type type)
        {
            if (type == null) return GetLogService(string.Empty);
            return GetLogService(type.FullName);
        }

        /// <summary>
        /// Gets a log service by its name.
        /// </summary>
        /// <remarks>
        /// If <paramref name="name"/> is null or empty, this method returns the <b>default</b> log service.
        /// </remarks>
        /// <param name="name">The name of the log service to retrieve.</param>
        /// <returns>An instance of a class implementing <see cref="ILogService"/> if found; otherwise, <c>null</c>.</returns>
        public ILogService GetLogService(string name)
        {
            if (string.IsNullOrEmpty(name)) return defaultLogService;
            if (logServices.ContainsKey(name)) return logServices[name];
            return null;
        }

        /// <summary>
        /// Adds a log service to the inner dictionary of log services.
        /// </summary>
        /// <param name="service">The log service to add.</param>
        public void AddLogService(ILogService service)
        {
            if (service == null) throw new ArgumentException("service");
            AddLogService(service.GetType().FullName, service);
        }

        /// <summary>
        /// Adds a log service to the inner dictionary of log services.
        /// </summary>
        /// <param name="name">The name of the newly added log service.</param>
        /// <param name="service">The log service to add.</param>
        public void AddLogService(string name, ILogService service)
        {
            if (service == null) throw new ArgumentException("service");
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            if (logServices.ContainsKey(name)) throw new ArgumentException(string.Format(
                "Logger {0} is already present.", name), "name");

            logServices.Add(name, service);
        }

        /// <summary>
        /// Removes an existing log service from the inner dictionary by its type.
        /// </summary>
        /// <param name="type">The type of the log service to remove.</param>
        /// <returns>The removed log service.</returns>
        public ILogService RemoveLogService(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            return RemoveLogService(type.FullName);
        }

        /// <summary>
        /// Removes an existing log service from the inner dictionary by its name.
        /// </summary>
        /// <param name="name">The name of the log service to remove.</param>
        /// <returns>The removed log service.</returns>
        public ILogService RemoveLogService(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            if (logServices.ContainsKey(name))
            {
                ILogService logger = logServices[name];
                logServices.Remove(name);
                return logger;
            }
            else return null;
        }

        /// <summary>
        /// Sets the service of type <paramref name="type"/> to be the default log service.
        /// </summary>
        /// <param name="type">The type of the future default service.</param>
        /// <returns>The old default log service.</returns>
        public ILogService SetDefaultLogService(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            return SetDefaultLogService(type.FullName);
        }

        /// <summary>
        /// Sets the default log service.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The old <b>default</b> log service.</returns>
        public ILogService SetDefaultLogService(string name)
        {
            if (!logServices.ContainsKey(name)) throw new ArgumentException(string.Format(
                "Logger {0} could not be found.", name), "name");

            ILogService oldDefaultLogService = defaultLogService;
            defaultLogService = logServices[name];
            return oldDefaultLogService;
        }

        #endregion

        #region ILogService Members

        public void Log(LogLevel level, string message)
        {
            defaultLogService.Log(level, message);
        }

        public void Log(LogLevel level, string message, Exception exception)
        {
            defaultLogService.Log(level, message, exception);
        }

        public void Log(LogLevel level, Exception exception)
        {
            defaultLogService.Log(level, exception);
        }

        public void Log(LogEntry entry)
        {
            defaultLogService.Log(entry);
        }

        #endregion
    }
}
