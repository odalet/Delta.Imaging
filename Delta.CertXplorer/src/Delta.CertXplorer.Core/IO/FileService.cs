using System;
using System.IO;
using System.Windows.Forms;

using Delta.CertXplorer.Logging;

namespace Delta.CertXplorer.IO
{
    public class FileService : IFileService
    {
        private IServiceProvider services = null;
        private ILogService logService = null;

        public FileService(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) throw new ArgumentNullException("serviceProvider");
            services = serviceProvider;
        }

        #region IFileService Members

        public bool FileExists(string path) { return File.Exists(path); }
        public bool DirectoryExists(string path) { return Directory.Exists(path); }

        public OperationResult SafeOpen(FileOperationDelegate operation) { return SafeOpen(operation, new FileType[] { FileType.ALL }); }
        public OperationResult SafeOpen(FileOperationDelegate operation, FileType[] types) { return SafeOpen(operation, types, 0); }
        public OperationResult SafeOpen(FileOperationDelegate operation, FileType[] types, int defaultTypeIndex) { return SafeOpen(operation, types, defaultTypeIndex, "Open..."); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="types">The types.</param>
        /// <param name="defaultTypeIndex">zero-based filter index</param>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        public OperationResult SafeOpen(FileOperationDelegate operation, FileType[] types, int defaultTypeIndex, string title)
        {
            FileType[] fileTypes = null;
            string filter = FileType.CombineFilters(out fileTypes, types);
            if ((fileTypes == null) || (fileTypes.Length == 0))
            {
                fileTypes = new FileType[] { FileType.ALL };
                filter = FileType.ALL.Filter;
            }

            if (defaultTypeIndex >= fileTypes.Length) defaultTypeIndex = 0;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = filter;
            ofd.FilterIndex = defaultTypeIndex + 1; // FilterIndex est basé sur 1
            ofd.Title = title;
            ofd.SupportMultiDottedExtensions = true;
            ofd.RestoreDirectory = true;
            ofd.Multiselect = false;

            DialogResult result = ShowDialog(ofd);

            if (result == DialogResult.OK)
            {
                int typeIndex = ofd.FilterIndex - 1; // FilterIndex est basé sur 1
                FileType selectedFileType = FileType.UNKNOWN;
                if (typeIndex < fileTypes.Length) selectedFileType = types[typeIndex];

                try
                {
                    operation(ofd.FileName, selectedFileType);

                    return OperationResult.OK;
                }
                catch (Exception ex)
                {
                    LogError(ex);
                    return OperationResult.Failed;
                }
            }
            else return OperationResult.Cancelled;
        }

        public OperationResult SafeSave(FileOperationDelegate operation, string filename) { return SafeSave(operation, filename, new FileType[] { FileType.ALL }); }
        public OperationResult SafeSave(FileOperationDelegate operation, string filename, FileType[] types) { return SafeSave(operation, filename, types, 0); }
        public OperationResult SafeSave(FileOperationDelegate operation, string filename, FileType[] types, int defaultTypeIndex) { return SafeSave(operation, filename, types, defaultTypeIndex, "Save File As"); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="filename"></param>
        /// <param name="types"></param>
        /// <param name="defaultTypeIndex">zero-based filter index</param>
        /// <param name="title"></param>
        /// <returns></returns>
        public OperationResult SafeSave(FileOperationDelegate operation, string filename, FileType[] types, int defaultTypeIndex, string title)
        {
            FileType[] fileTypes = null;
            string filter = FileType.CombineFilters(out fileTypes, types);
            if ((fileTypes == null) || (fileTypes.Length == 0))
            {
                fileTypes = new FileType[] { FileType.ALL };
                filter = FileType.ALL.Filter;
            }

            if (defaultTypeIndex >= fileTypes.Length) defaultTypeIndex = 0;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = filter;
            sfd.FilterIndex = defaultTypeIndex + 1; // FilterIndex est basé sur 1
            sfd.Title = title;
            sfd.SupportMultiDottedExtensions = true;
            sfd.RestoreDirectory = true;
            sfd.OverwritePrompt = true;
                        
            DialogResult result = ShowDialog(sfd);

            if (result == DialogResult.OK)
            {
                int typeIndex = sfd.FilterIndex - 1; // FilterIndex est basé sur 1
                FileType selectedFileType = FileType.UNKNOWN;
                if (typeIndex < fileTypes.Length) selectedFileType = types[typeIndex];

                try
                {
                    operation(sfd.FileName, selectedFileType);

                    return OperationResult.OK;
                }
                catch (Exception ex)
                {
                    LogError(ex);
                    return OperationResult.Failed;
                }
            }
            else return OperationResult.Cancelled;
        }

        #endregion
        
        private ILogService GetLogService()
        {
            if (logService == null) logService = services.GetService<ILogService>();
            return logService;
        }

        private DialogResult ShowDialog(Form form)
        {
            return form.ShowDialog();
        }

        private DialogResult ShowDialog(CommonDialog commonDialog)
        {
            return commonDialog.ShowDialog();
        }

        #region LogError

        private void LogError(string message) { LogError(message, null); }
        private void LogError(Exception ex) { LogError(string.Empty, ex); }
        private void LogError(string message, Exception ex)
        {            
            string text = string.Empty;

            ILogService log = GetLogService();
            if (log == null)
            {
                if (ex == null)
                {
                    if (string.IsNullOrEmpty(message)) Console.Error.WriteLine("ERROR\r\n");
                    else Console.Error.WriteLine("ERROR - " + message + "\r\n");
                }
                else
                {
                    if (string.IsNullOrEmpty(message)) Console.Error.WriteLine("ERROR - Exception: " + ex.ToString() + "\r\n");
                    else Console.Error.WriteLine("ERROR - " + message + "\r\nERROR - Exception: " + ex.ToString() + "\r\n");
                }
            }
            else
            {
                if (ex == null)
                {
                    if (string.IsNullOrEmpty(message)) log.Error(string.Empty);
                    else log.Error(message);
                }
                else
                {
                    if (string.IsNullOrEmpty(message)) log.Error("Exception: ", ex);
                    else log.Error(message, ex); 
                }
            }
        }

        #endregion
    }
}
