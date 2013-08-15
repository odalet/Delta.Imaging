using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.CertXplorer.IO
{
    public interface IFileService
    {
        bool FileExists(string path);
        bool DirectoryExists(string path);

        OperationResult SafeOpen(FileOperationDelegate operation);
        OperationResult SafeOpen(FileOperationDelegate operation, FileType[] types);
        OperationResult SafeOpen(FileOperationDelegate operation, FileType[] types, int defaultTypeIndex);
        OperationResult SafeOpen(FileOperationDelegate operation, FileType[] types, int defaultTypeIndex, string title);

        OperationResult SafeSave(FileOperationDelegate operation, string filename);
        OperationResult SafeSave(FileOperationDelegate operation, string filename, FileType[] types);
        OperationResult SafeSave(FileOperationDelegate operation, string filename, FileType[] types, int defaultTypeIndex); 
        OperationResult SafeSave(FileOperationDelegate operation, string filename, FileType[] types, int defaultTypeIndex, string title);
    }
}
