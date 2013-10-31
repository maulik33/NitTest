using System.Collections.Generic;
using System.Text;
using NursingLibrary.Common;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IOverrideView
    {
        string CohortIds { get; }

        string AssignIds { get; }

        void PopulateInstitution(IEnumerable<Institution> institutions);

        void ShowStudentTests(IList<UserTest> studentTests, SortInfo sortMetaData);
        
        void PopulateCohort(IEnumerable<Cohort> cohorts);

        void PopulateProgramOfStudy(IEnumerable<ProgramofStudy> programofStudies);
    }
}
