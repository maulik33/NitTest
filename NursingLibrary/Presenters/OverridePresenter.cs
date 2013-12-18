using System.Linq;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class OverridePresenter : AuthenticatedPresenterBase<IOverrideView>
    {
        private readonly IAdminService _adminService;
        private ViewMode _mode;

        public OverridePresenter(IAdminService service)
            : base(Module.Student)
        {
            _adminService = service;
        }

        public override void RegisterAuthorizationRules()
        {
            if (_mode != ViewMode.Edit)
            {
                return;
            }
        }

        public override void RegisterQueryParameters()
        {
        }

        public void PreInitialize(ViewMode mode)
        {
            _mode = mode;
        }

        public void ShowList(int institutionId, string firstName, string lastName, string userName, string testName,
                bool showIncompleteOnly, int activeTab, string sortMetaData, string CohortIds)
        {
            if (activeTab == 0)
            {
                View.ShowStudentTests(_adminService.GetStudentsForOverRide(institutionId, firstName, lastName, userName, testName, showIncompleteOnly, CohortIds).ToList(),
                    SortHelper.Parse(sortMetaData));
            }
            else
            {
                View.ShowStudentTests(_adminService.GetDeletedTestListForStudents(institutionId, firstName, lastName, userName, testName, showIncompleteOnly, CohortIds).ToList(),
                            SortHelper.Parse(sortMetaData));
            }
        }

        public void PopulateProgramOfStudies()
        {
            if (CurrentContext.UserType == UserType.SuperAdmin)
                View.PopulateProgramOfStudy(_adminService.GetProgramofStudies());
        }

        public void GetInstitutionByProgramofStudy(int programOfStudyId)
        {
            View.PopulateInstitution(_adminService.GetInstitutions(CurrentContext.UserId, programOfStudyId, string.Empty));
        }

        public void GetActiveCohortList(string institutuinIds)
        {
            View.PopulateCohort(_adminService.GetCohortsByInstitutionIds(institutuinIds));
        }

        public void DeleteTest(int testId)
        {
            _adminService.DeleteTest(testId, CurrentContext.User.UserName);
        }

        public void ResumeTest(string userTestId)
        {
            _adminService.ResumeTest(userTestId, CurrentContext.User.UserName);
        }
    }
}
