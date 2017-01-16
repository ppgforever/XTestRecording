using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using XTestRecording;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(XTestRecording.Droid.FileServiceProvider))]

namespace XTestRecording.Droid
{
    public class FileServiceProvider : IFileServiceProvider
    {
        public void CopyFile(string sourceFileFullPath, string destinationFileFullPath)
        {
            File.Copy(sourceFileFullPath, destinationFileFullPath, true);
        }

        public void DeleteFile(string fileFullPathName)
        {
            if (string.IsNullOrEmpty(fileFullPathName)) return;

            File.Delete(fileFullPathName);
        }

        public void DeleteFolder(string folderFullPathName)
        {
            if (string.IsNullOrEmpty(folderFullPathName)) return;

            Directory.Delete(folderFullPathName, true);
        }

        public string GetFileFullPath(string filename)
        {
            return Path.Combine(GetDocumentFolderPath(), filename);
        }

        public string GetDocumentFolderPath()
        {
            return System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        }

        public bool FileExists(string filename)
        {
            return File.Exists(filename);
        }

        public string GetExtension(string filename)
        {
            return Path.GetExtension(filename);
        }

        public long GetFileSize(string fileName)
        {
            var fileInfo = new FileInfo(fileName);
            if (fileInfo == null)
                throw new FileNotFoundException();

            return fileInfo.Length;
        }
    }
}