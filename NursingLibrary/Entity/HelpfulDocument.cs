using System;
using System.IO;
using NursingLibrary.Common;
using NursingLibrary.Utilities;

namespace NursingLibrary.Entity
{
    public class HelpfulDocument
    {
        public int Id { get; set; }

        public string FileName { get; set; }
        
        public string Title { get; set; }
        
        public string Type { get; set; }
        
        public double Size { get; set; }
        
        public DateTime? CreatedDateTime { get; set; }
        
        public string Description { get; set; }
        
        public int Status { get; set; }
        
        public string GUID { get; set; }
        
        public int CreatedBy { get; set; }

        public bool IsLink { get; set; }
        
        public Admin Admin { get; set; }

        public string FullFileName
        {
            get
            {
                return GetFullFileName(GUID);
            }
        }

        public string GetFullFileName(string guid)
        {
            return Path.Combine(KTPApp.HelpfulDocumentFolderPath, string.Concat(guid, FileHelper.GetFileExtension(FileName)));
        }
    }
}
