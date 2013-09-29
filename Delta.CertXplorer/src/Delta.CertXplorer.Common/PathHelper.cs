using System;
using System.IO;

namespace Delta.CertXplorer
{
    public static class PathHelper
    {
        private static string userConfigDirectory = null;
        private static string userDataDirectory = null;
        
        /// <summary>
        /// Gets the user configuration directory.
        /// </summary>
        public static string UserConfigDirectory
        {
            get
            {
                if (userConfigDirectory == null)
                {
                    var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    userConfigDirectory = Path.Combine(localAppData, ThisAssembly.Product);
                    if (File.Exists(userConfigDirectory)) File.Delete(userConfigDirectory);
                    if (!Directory.Exists(userConfigDirectory)) Directory.CreateDirectory(userConfigDirectory);
                }

                return userConfigDirectory;
            }
        }

        /// <summary>
        /// Gets the user data directory.
        /// </summary>
        public static string UserDataDirectory
        {
            get
            {
                if (userDataDirectory == null)
                {
                    var configDirectory = UserConfigDirectory;
                    userDataDirectory = Path.Combine(configDirectory, "data");
                    if (File.Exists(userDataDirectory)) File.Delete(userDataDirectory);
                    if (!Directory.Exists(userDataDirectory)) Directory.CreateDirectory(userDataDirectory);
                }

                return userDataDirectory;
            }
        }
    }
}
