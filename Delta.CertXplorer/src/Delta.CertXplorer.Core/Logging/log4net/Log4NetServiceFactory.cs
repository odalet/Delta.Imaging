using System;

using Delta.CertXplorer.Logging;
using System.IO;

namespace Delta.CertXplorer.Logging.log4net
{
    /// <summary>
    /// This class allows for the creation of a trace service instance based
    /// on the log4net framework.
    /// </summary>
    public static class Log4NetServiceFactory
    {
        /// <summary>
        /// Creates the logging service and return the newly created instance.
        /// </summary>
        /// <remarks>
        /// The configuration file used by log4net will be the application's configuration
        /// file (<b>web.config</b> ou <b>app.exe.config</b>) which will have to contain a
        /// <b>&lt;log4net&gt;</b> section.
        /// </remarks>
        /// <returns>An instance of <see cref="Delta.CertXplorer.Logging.ILogService"/>.</returns>
        public static ILogService CreateService() { return new Log4NetService(); }

        /// <summary>
        /// Creates the logging service and return the newly created instance.
        /// </summary>
        /// <remarks>
        /// If the configuration file can't be found (for instance, filename without a full path),
        /// it is searched in the folder that contains the application's configuration file.
        /// </remarks>
        /// <param name="configurationFile">Log4net specific configuration file.</param>
        /// <returns>An instance of <see cref="Delta.CertXplorer.Logging.ILogService"/>.</returns>
        public static ILogService CreateService(string configurationFile)
        {
            FileInfo fileInfo = null;
            if (File.Exists(configurationFile)) fileInfo = new FileInfo(configurationFile);
            else
            {
                // If we couldn't find the file, we try to search for it in the application's configuration
                // file directory.
                string appConfig = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
                string path = Path.GetDirectoryName(appConfig);
                string configFile = Path.Combine(path, configurationFile);
                if (File.Exists(configFile)) fileInfo = new FileInfo(configFile);
                else throw new FileNotFoundException("Log4Net configuration file not found: " + configurationFile, configurationFile);
            }

            return CreateService(fileInfo);
        }

        /// <summary>
        /// Creates the logging service and return the newly created instance.
        /// </summary>
        /// <param name="configurationFile">Log4net specific configuration file.</param>
        /// <returns>An instance of <see cref="Delta.CertXplorer.Logging.ILogService"/>.</returns>
        public static ILogService CreateService(FileInfo configurationFile)
        {
            return new Log4NetService(configurationFile);
        }
    }
}
