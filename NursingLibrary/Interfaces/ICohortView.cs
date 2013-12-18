using System.Collections.Generic;
using NursingLibrary.Common;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface ICohortView
    {
        string ErrorMessage { get; set; }

        string Name { get; set; }

        string StartDate { get; set; }

        string EndDate { get; set; }

        string Description { get; set; }

        string SearchText { get; set; }

        string InstitutionId { get; set; }

        int CohortStatus { get; set; }

        int GroupId { get; set; }

        int CohortId { get; set; }

        int ProductId { get; set; }

        int TestId { get; set; }

        int ProgramId { get; set; }

        int ProgramofStudyId { get; set; }

        bool IsValidDate { get; set; }

        bool IsInValidCohort { get; set; }

        bool HasAddPermission { get; set; }

        void SaveCohort(int newCohortId);

        void DeleteCohort();

        void PopulateInstitutions(IEnumerable<Institution> institutions);

        void PopulateCohorts(IEnumerable<Cohort> cohort, SortInfo sortMetaData);

        void PopulateProducts(IEnumerable<Product> products);

        void PopulateTests(IEnumerable<Test> tests);

        void PopulateGroups(IEnumerable<Group> groups);

        void ShowCohortResults(IEnumerable<Cohort> cohort);

        void PopulateStudents(IEnumerable<Student> students, SortInfo sortMetaData);

        void PopulatePrograms(IEnumerable<Program> programs);

        void RefreshPage(Cohort cohort, UserAction action, string title, string subTitle, bool hasDeletePermission, bool hasAddPermission, bool hasAccessDatesEdit, bool hasEditPremission);

        void ShowCohort(Cohort group);

        void PopulateCohortTest(CohortTestDates testDetail);

        void PopulateCohortTests(IEnumerable<CohortTestDates> testDetails);

        void PopulateProgramForTest(Program program);

        void ShowProgramResults(IEnumerable<Program> programs, SortInfo sortMetaData);

        void ExportCohortList(IEnumerable<Cohort> reportData, ReportAction printActions);

        void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudy);

        void ExportStudents(IEnumerable<Student> reportData, ReportAction printActions);
    }
}
