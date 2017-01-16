using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTestRecording
{
    public interface IFileServiceProvider
    {
        string GetDocumentFolderPath();

        string GetFileFullPath(string filename);

        void DeleteFile(string fileFullPathName);

        void DeleteFolder(string folderFullPathName);

        bool FileExists(string filename);

        void CopyFile(string sourceFileFullPath, string destinationFileFullPath);

        /// <summary>
        /// Return the extension of the file, including the .
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        string GetExtension(string filename);

        long GetFileSize(string fileName);
    }
}
