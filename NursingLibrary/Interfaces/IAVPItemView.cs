using System.Collections.Generic;
using NursingLibrary.Common;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
   public interface IAVPItemView
    {
       string TestName { get; set; }

       string URL { get; set; }

       string PopWidth { get; set; }
       
       string PopHeight { get; set; }
       
       string HeaderLabelText { get; set; }

       int TestId { get; set; }

       string Sort { get; set; }

       void RefreshPage(IEnumerable<Test> tests, SortInfo sortMetaData);

       void RenderProgramOfStudyUI(IEnumerable<ProgramofStudy> programofStudies, int selectedProgramOfStudyId, bool programOfStudiesDropDownEnabled);
       
       bool Confirm();
    }
}
