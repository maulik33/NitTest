using System;
using System.IO;
using NursingLibrary.Common;

namespace NursingLibrary.Utilities
{
    public class FileHelper
    {
        public static string GetFileExtension(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            return fileInfo.Extension;
        }

        public static string GetFileExtension(string fileName, AvailUserImpersonation userImpersonation)
        {
            using (ImpersonateUserBase user = userImpersonation())
            {
                FileInfo fileInfo = new FileInfo(fileName);
                return fileInfo.Extension;
            }
        }

        public static string GetFileName(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            return fileInfo.Name;
        }

        public static string GetFileName(string fileName, AvailUserImpersonation userImpersonation)
        {
            using (ImpersonateUserBase user = userImpersonation())
            {
                FileInfo fileInfo = new FileInfo(fileName);
                return fileInfo.Name;
            }
        }

        public static string GetFileSizeInMB(long sizeInBytes)
        {
            return string.Format("{0} MB", Math.Round((double)sizeInBytes / 1024 / 1024, 2));
        }

        public static double GetFileSize(int sizeInBytes)
        {
            return Math.Round((double)sizeInBytes / 1024 / 1024, 5);
        }
    }
}