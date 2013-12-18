using System.Collections.Generic;
using NursingLibrary.Common;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface ILippincottView
    {
        string AddMessage { set; }

        string SerachTextBox { set; }

        string SearchCondition { get; set; }

        string Sort { get; set; }

        void OnViewInitialized();
        
        void SearchLippincott(IEnumerable<Lippincott> lippinCotts, SortInfo sortMetaData);
        
        void PopulateQuestionList(IEnumerable<Lippincott> lippinCotts);
        
        void PopulateControls(IEnumerable<Remediation> remediations, Lippincott lippincott);
        
        void ShowMessage();
    }
}
