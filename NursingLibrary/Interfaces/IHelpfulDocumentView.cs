using System.Collections.Generic;
using NursingLibrary.Common;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IHelpfulDocumentView
    {
        bool IsLink { get; set; }

        bool CanUploadFiles { get; set; }

        void DisplayDocumentData(HelpfulDocument helpfulDocument);

        void DisplaySearchResult(IEnumerable<HelpfulDocument> docs, SortInfo sortInfo);
        
        void SaveDocument(string folderFullPath);
        
        void DisplayErrorMessage(string fileName);
    }
}
