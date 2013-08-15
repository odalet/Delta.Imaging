using System;
using System.IO;
using System.Windows.Forms;

namespace Delta.CertXplorer.ThisImplementation
{
    /// <summary>
    /// Contains the list of the local file system special directories.
    /// </summary>
    public class SpecialDirectoriesProxy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpecialDirectoriesProxy"/> class.
        /// </summary>
        internal SpecialDirectoriesProxy() { }

        /// <summary>
        /// Gets the 'all users application data' directory.
        /// </summary>
        public string AllUsersApplicationData
        {
            get
            {
                return GetDirectoryPath(Application.CommonAppDataPath,
                    "AllUsersApplicationData");
            }
        }

        /// <summary>
        /// Gets the 'current user application data' directory.
        /// </summary>
        public string CurrentUserApplicationData
        {
            get
            {
                return GetDirectoryPath(Application.UserAppDataPath,
                    "CurrentUserApplicationData");
            }
        }

        /// <summary>Gets the 'desktop' directory.</summary>
        public string Desktop
        {
            get
            {
                return GetDirectoryPath(Environment.GetFolderPath(
                    Environment.SpecialFolder.Desktop), "Desktop");
            }
        }

        /// <summary>Gets 'My documents' directory.</summary>
        public string MyDocuments
        {
            get
            {
                return GetDirectoryPath(Environment.GetFolderPath(
                    Environment.SpecialFolder.Personal), "MyDocuments");
            }
        }

        /// <summary>Gets 'My music' directory.</summary>
        public string MyMusic
        {
            get
            {
                return GetDirectoryPath(Environment.GetFolderPath(
                    Environment.SpecialFolder.MyMusic), "MyMusic");
            }
        }

        /// <summary>Gets 'My picture' directory.</summary>
        public string MyPictures
        {
            get
            {
                return GetDirectoryPath(Environment.GetFolderPath(
                    Environment.SpecialFolder.MyPictures), "MyPictures");
            }
        }

        /// <summary>Gets the 'Program Files' directory.</summary>
        public string ProgramFiles
        {
            get
            {
                return GetDirectoryPath(Environment.GetFolderPath(
                    Environment.SpecialFolder.ProgramFiles), "ProgramFiles");
            }
        }

        /// <summary>Gets the 'Programs' directory.</summary>
        public string Programs
        {
            get
            {
                return GetDirectoryPath(Environment.GetFolderPath(
                    Environment.SpecialFolder.Programs), "Programs");
            }
        }

        /// <summary>Gets the 'Temp' directory.</summary>
        public string Temp
        {
            get
            {
                return GetDirectoryPath(Path.GetTempPath(), "Temp");
            }
        }

        #region Private implementation

        private static string GetDirectoryPath(string directory, string directoryName)
        {

            if (string.IsNullOrEmpty(directory)) throw new DirectoryNotFoundException(string.Format(
                "Special Directory {0} ({1}) not found.", directory, directoryName));

            return FileSystemProxy.NormalizePath(directory);
        }

        #endregion
    }
}
