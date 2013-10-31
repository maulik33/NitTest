using System.Collections.Generic;
using NursingLibrary.Common;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IUserListView
    {
        int ProgramOfStudyId { get; set; }

        int GroupId { get; set; }

        int CohortId { get; set; }

        int StudentId { get; set; }

        string InstitutionId { get; set; }

        string SearchString { get; set; }

        string StudentStartDate { get; set; }

        string StudentEndDate { get; set; }

        bool IsUnAssigned { get; set; }

        bool IfUserExists { get; set; }

        void ShowErrorMessage(string message);

        void SaveUser(int newGroupId);

        void DeleteUser(int groupId);

        void PopulateStudentTest(StudentTestDates testDate);

        void PopulateStudentTests(IEnumerable<StudentTestDates> testDates);

        void PopulateGroup(IEnumerable<Group> groups);

        void PopulateInstitution(IEnumerable<Institution> institutes);

        void PopulateCohort(IEnumerable<Cohort> cohorts);

        void PopulateStudent(IEnumerable<Student> students, SortInfo sortMetaData);

        void PopulateStudentForTest(IEnumerable<Student> students);

        void RefreshPage(Student student, UserAction action, string title, string subTitle, bool hasDeletePermission, bool hasAddPermission, bool hasChangePermission);

        void GetStudentDetails(Student student);

        void GetDatesByCohortId(StudentEntity student);

        void PopulateProgramForTest(Program program);

        void PopulateGroupForTest(Group group);

        void PopulateCountry(IEnumerable<Country> country);

        void PopulateState(IEnumerable<State> state);

        void PopulateAddress(Address address);

        void PopulateAdhocGroupForTest(IEnumerable<AdhocGroupTestDetails> adhocGrouptestdetails);

        void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudy);
    }
}
