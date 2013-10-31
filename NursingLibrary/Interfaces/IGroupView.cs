using System.Collections.Generic;
using NursingLibrary.Common;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IGroupView
    {
        string Name { get; set; }
        
        int GroupId { get; set; }
        
        int CohortId { get; set; }
        
        string InstitutionId { get; set; }

        string CohortIds { get; }

        int ProgramofStudyId { get; set; }

        void ShowGroupResults(IEnumerable<Group> groups, SortInfo sortMetaData);
        
        void SaveGroup(int newGroupId);
        
        void DeleteGroup(int groupId);
        
        void PopulateInstitution(IEnumerable<Institution> institutions);        
        
        void PopulateCohort(IEnumerable<Cohort> cohort);
        
        void RefreshPage(Group group, UserAction action, string title, string subTitle, bool hasDeletePermission, bool hasAddPermission);
        
        void ShowGroup(Group group);
        
        void PopulateGroupTest(GroupTestDates testDetail);
        
        void PopulateGroupTests(IEnumerable<GroupTestDates> testDetails);
        
        void PopulateProgramForTest(Program program);

        void ExportGroups(IEnumerable<Group> groups, ReportAction printActions);

        void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudies);
    }
}
