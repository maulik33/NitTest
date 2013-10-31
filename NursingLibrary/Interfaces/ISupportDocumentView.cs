using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Entity;
using NursingLibrary.Common;

namespace NursingLibrary.Interfaces
{
    public interface ISupportDocumentView
    {
        void DisplayDocumentData(SupportDocument supportDocument);
        void DisplaySearchResult(IEnumerable<SupportDocument> docs, SortInfo sortInfo, bool canUploadFiles);
        void SaveDocument(string folderFullPath);
        void DisplayErrorMessage(string fileName);
    }
}
