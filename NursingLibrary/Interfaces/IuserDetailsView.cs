using System.Collections.Generic;
using NursingLibrary.Common;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
   public  interface IuserDetailsView
    {
       string SearchText { get; set; }

       int ProgramofStudyId { get; set; }

       void ShowUserResults(IEnumerable<UserDetails> userDetails, SortInfo sortMetaData);

       void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudies);
    }
}
