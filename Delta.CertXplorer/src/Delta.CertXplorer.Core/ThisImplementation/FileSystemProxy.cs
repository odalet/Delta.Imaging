using System;
using System.IO;
using System.Security;

namespace Delta.CertXplorer.ThisImplementation
{
    /// <summary>
    /// Provides properties and methods for accessing the local file system.
    /// </summary>
    /// <remarks>
    /// This is a basically a C# translation of <c>Microsoft.VisualBasic.FileIO.FileSystem</c>.
    /// </remarks>
    public class FileSystemProxy
    {
        private SpecialDirectoriesProxy specialDirectoriesProxy = null;

        /// <summary>Gets the name of a new temporary file.</summary>
        /// <returns>The name of a new temporary file.</returns>
        public string GetTempFileName() { return Path.GetTempFileName(); }

        /// <summary>
        /// Gets or sets the current directory.
        /// </summary>
        /// <value>The current directory.</value>
        public string CurrentDirectory
        {
            get { return NormalizePath(Directory.GetCurrentDirectory()); }
            set { Directory.SetCurrentDirectory(value); }
        }

        /// <summary>
        /// Gets the special directories list.
        /// </summary>
        /// <value>The special directories.</value>
        public SpecialDirectoriesProxy SpecialDirectories
        {
            get
            {
                if (specialDirectoriesProxy == null)
                    specialDirectoriesProxy = new SpecialDirectoriesProxy();
                return specialDirectoriesProxy;
            }
        }

        #region Private implementation

        /// <summary>
        /// Normalizes the path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>normalized path.</returns>
        internal static string NormalizePath(string path)
        {
            return GetLongPath(RemoveEndingSeparator(Path.GetFullPath(path)));
        }

        /// <summary>Removes the ending separator of a path.</summary>
        /// <param name="path">The path.</param>
        /// <returns>normalized path.</returns>
        private static string RemoveEndingSeparator(string path)
        {
            if (Path.IsPathRooted(path) &&
                path.Equals(Path.GetPathRoot(path), StringComparison.OrdinalIgnoreCase))
                return path;

            return path.TrimEnd(new char[] 
            { 
                Path.DirectorySeparatorChar, 
                Path.AltDirectorySeparatorChar 
            });
        }

        /// <summary>
        /// Determines whether the specified path is a root path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>
        /// 	<c>true</c> if the specified path is a root path; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsRoot(string path)
        {
            if (!Path.IsPathRooted(path)) return false;

            path = path.TrimEnd(new char[] 
            { 
                Path.DirectorySeparatorChar, 
                Path.AltDirectorySeparatorChar 
            });

            return (string.Compare(
                path,
                Path.GetPathRoot(path),
                StringComparison.OrdinalIgnoreCase) == 0);
        }

        /// <summary>
        /// Gets the long version of a path.
        /// </summary>
        /// <param name="fullPath">The full path.</param>
        /// <returns>normalized path.</returns>
        private static string GetLongPath(string fullPath)
        {
            try
            {
                if (!IsRoot(fullPath))
                {
                    DirectoryInfo info = new DirectoryInfo(GetParentPath(fullPath));
                    if (File.Exists(fullPath))
                        return info.GetFiles(Path.GetFileName(fullPath))[0].FullName;

                    if (Directory.Exists(fullPath))
                        return info.GetDirectories(Path.GetFileName(fullPath))[0].FullName;
                }
                return fullPath;
            }
            catch (Exception exception)
            {
                if (!(exception is ArgumentException) &&
                    !(exception is ArgumentNullException) &&
                    !(exception is PathTooLongException) &&
                    !(exception is NotSupportedException) &&
                    !(exception is DirectoryNotFoundException) &&
                    !(exception is SecurityException) &&
                    !(exception is UnauthorizedAccessException)) throw;

                return fullPath;
            }
        }

        /// <summary>
        /// Gets the parent path of <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The path's parent.</returns>
        private static string GetParentPath(string path)
        {
            if (IsRoot(path)) throw new ArgumentException(string.Format(
                "Can't get parent path for{0}: it is a root path.", path), "path");

            return Path.GetDirectoryName(
                path.TrimEnd(new char[] 
                { 
                    Path.DirectorySeparatorChar, 
                    Path.AltDirectorySeparatorChar 
                }));
        }

        #endregion
    }
}
