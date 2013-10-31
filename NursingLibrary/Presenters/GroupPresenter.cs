using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class GroupPresenter : AuthenticatedPresenterBase<IGroupView>
    {
        private const string END_DATE_ERROR_MESSAGE = " Cannot set Test dates beyond Cohort End Date";
        private const string START_DATE_ERROR_MESSAGE = " Start date should be before the finish date";
        private const string DATE_SAVE_MESSAGE = "Test dates have been  saved";
        private const string WRONG_DATE_FORMAT_MESSAGE = " Wrong Date Format";
        private readonly IAdminService _adminService;
        private ViewMode _mode;
        private DateTime? startDate = null;
        private DateTime? endDate = null;
        private bool _isValidDate = false;
        private int _programId;

        public GroupPresenter(IAdminService service)
            : base(Module.Group)
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
            if (_mode == ViewMode.View)
            {
                RegisterQueryParameter(QUERY_PARAM_ID);
            }
            else if (_mode == ViewMode.Edit)
            {
                RegisterQueryParameter(QUERY_PARAM_ACTION_TYPE);
                RegisterQueryParameter(QUERY_PARAM_ID, QUERY_PARAM_ACTION_TYPE, "2");
            }
        }

        public void PreInitialize(ViewMode mode)
        {
            _mode = mode;
        }

        public void GetInstitutionByProgramofStudy(int programofStudyId)
        {
            View.PopulateInstitution(_adminService.GetInstitutions(CurrentContext.UserId, programofStudyId, string.Empty));
        }

        public void GetInstitutionList(string sortMetaData,string searchText)
        {
            IOrderedEnumerable<Institution> _institutionIds;
            StringBuilder _assignedInstitutionList;
            GetAssignedInstitutions(out _institutionIds, out _assignedInstitutionList);
            View.PopulateInstitution(_institutionIds);
            View.PopulateCohort(_adminService.GetCohortsByInstitutionIds(_assignedInstitutionList.ToString()));
            View.ShowGroupResults(_adminService.SearchGroups(searchText, _assignedInstitutionList.ToString(), string.Empty),
                   SortHelper.Parse(sortMetaData));
        }

        public void ShowProgramofStudyDetails()
        {
            var programOfStudies = _adminService.GetProgramofStudies();
            View.PopulateProgramofStudy(programOfStudies);
        }

        public void SearchGroups(string searchText, string sortMetaData, bool cohortSelectAllFlag)
        {
            var _institutions = string.Empty;
            _institutions = GetInstitutionIds(_institutions);

            var _cohorts = string.Empty;

            if (View.CohortIds != null)
            {
                _cohorts = View.CohortIds.Replace(",", "|");
            }

            View.ShowGroupResults(_adminService.SearchGroups(searchText, _institutions.ToString(), cohortSelectAllFlag ? string.Empty : _cohorts.ToString()),
            SortHelper.Parse(sortMetaData));
        }

        public void SaveGroup()
        {
            Group group = new Group()
            {
                GroupId = Id,
                GroupName = View.Name,
                Cohort = new Cohort() { CohortId = View.CohortId },
                GroupUserId = 1
            };
            Id = _adminService.SaveGroup(group);
            Navigator.NavigateTo(AdminPageDirectory.GroupView, string.Empty, string.Format("{0}={1}", QUERY_PARAM_ID, Id));
        }

        public void ShowGroupDetails()
        {
            var group = GetGroupDetails();
            string title = string.Empty;
            string subTitle = string.Empty;
            bool hasDeletePermission = CurrentContext.UserType != UserType.TechAdmin ? true : false;
            bool hasAddPermission = CurrentContext.UserType != UserType.TechAdmin ? true : false;
            if (ActionType == UserAction.Edit)
            {
                title = "Edit > Group ";
                subTitle = "Use this page to edit a Group. ";
                View.PopulateInstitution(_adminService.GetInstitutions(CurrentContext.UserId, group.Institution.ProgramOfStudyId, string.Empty));
                View.PopulateCohort(_adminService.GetCohorts(0, group.Institution.InstitutionId.ToString()));
            }
            else
            {
                group.GroupName = string.Empty;
                title = "Add > Group ";
                subTitle = "Use this page to add a new Group. ";
                if (CurrentContext.UserType == UserType.SuperAdmin)
                {
                    var programOfStudies = _adminService.GetProgramofStudies();
                    View.PopulateProgramofStudy(programOfStudies);
                }

                View.PopulateInstitution(_adminService.GetInstitutions(CurrentContext.UserId, View.ProgramofStudyId, string.Empty));
                View.PopulateCohort(View.InstitutionId.ToInt() != -1
                                        ? _adminService.GetCohorts(0, View.InstitutionId)
                                        : _adminService.GetCohorts(0, String.Empty));

                hasDeletePermission = false;
            }

            View.RefreshPage(group, ActionType, title, subTitle, hasDeletePermission, hasAddPermission);
        }

        public void DeleteGroup()
        {
            View.DeleteGroup(_adminService.DeleteGroup(Id));
            Navigator.NavigateTo(AdminPageDirectory.GroupView);
        }

        public void GetCohortList(string institutuinIds)
        {
            View.PopulateCohort(_adminService.GetCohorts(0, institutuinIds));
        }

        public void NavigateToEdit(string groupId, UserAction actionType)
        {
            if (actionType == UserAction.Add)
            {
                Navigator.NavigateTo(AdminPageDirectory.GroupEdit, string.Empty, string.Format("{0}={1}",
                    QUERY_PARAM_ACTION_TYPE, ((int)actionType).ToString()));
            }
            else
            {
                Navigator.NavigateTo(AdminPageDirectory.GroupEdit, string.Empty, string.Format("{0}={1}&{2}={3}",
                    QUERY_PARAM_ACTION_TYPE, ((int)actionType).ToString(), QUERY_PARAM_ID, groupId));
            }
        }

        public void NavigateToEdit(UserAction actionType)
        {
            NavigateToEdit(GetParameterValue(QUERY_PARAM_ID), actionType);
        }

        public void NavigateFromGroupList(AdminPageDirectory navigateTo, int groupId)
        {
            if (navigateTo == AdminPageDirectory.StudentListForGroup)
            {
                Navigator.NavigateTo(AdminPageDirectory.StudentListForGroup, string.Empty, string.Format("{0}={1}",
                    QUERY_PARAM_ID, groupId));
            }
            else if (navigateTo == AdminPageDirectory.GroupTestDates)
            {
                Navigator.NavigateTo(AdminPageDirectory.GroupTestDates, string.Empty, string.Format("{0}={1}",
                    QUERY_PARAM_ID, groupId));
            }
            else if (navigateTo == AdminPageDirectory.GroupList)
            {
                Navigator.NavigateTo(AdminPageDirectory.GroupList, string.Empty, string.Format("{0}={1}",
                    QUERY_PARAM_ID, groupId));
            }
            else if (navigateTo == AdminPageDirectory.GroupView)
            {
                Navigator.NavigateTo(AdminPageDirectory.GroupView, string.Empty, string.Format("{0}={1}",
                    QUERY_PARAM_ID, groupId));
            }
        }

        public void ShowGroup()
        {
            var group = GetGroupDetails();
            View.ShowGroup(group);
            bool hasAddPermission = CurrentContext.UserType != UserType.LocalAdmin && CurrentContext.UserType != UserType.TechAdmin;
            View.RefreshPage(group, ActionType, string.Empty, string.Empty, true, hasAddPermission);
        }

        public void CanEditTestDates()
        {
            bool hasAccessDatesEdit = false;
            hasAccessDatesEdit = HasPermission(UserAction.EditTestDates);
            View.RefreshPage(null, ActionType, string.Empty, string.Empty, true, hasAccessDatesEdit);
        }

        public void ShowTestDatesForGroups()
        {
            bool canAssignTestDates = HasPermission(UserAction.EditTestDates);
            View.RefreshPage(null, ActionType, string.Empty, string.Empty, false, canAssignTestDates);
            var group = _adminService.GetGroup(Id);
            var cohort = _adminService.GetCohort(group.Cohort.CohortId);
            View.CohortId = cohort.CohortId;
            View.GroupId = group.GroupId;
            var institution = _adminService.GetInstitutions(CurrentContext.UserId, 0, cohort.InstitutionId.ToString()).FirstOrDefault();
            cohort.Institution = new Institution() { InstitutionName = institution.InstitutionName, InstitutionId = institution.InstitutionId };
            var cohortProgram = _adminService.GetCohortProgram(0, 0, cohort.CohortId);
            if (cohortProgram != null)
            {
                _programId = cohortProgram.ProgramId.ToInt();
                Program program = _adminService.GetProgram(_programId);
                View.PopulateProgramForTest(program);
            }

            GroupTestDates testDates = new GroupTestDates()
            {
                Group = new Group() { GroupId = group.GroupId, GroupName = group.GroupName },
                Cohort = new Cohort() { CohortId = cohort.CohortId, CohortName = cohort.CohortName, CohortStartDate = cohort.CohortStartDate, CohortEndDate = cohort.CohortEndDate },
                Institution = new Institution() { InstitutionId = institution.InstitutionId, InstitutionName = institution.InstitutionName, InstitutionNameWithProgOfStudy = institution.InstitutionNameWithProgOfStudy},
            };
            View.PopulateGroupTest(testDates);
            ShowTests(_programId, string.Empty);
        }

        public void ShowTests(int programId, string searchText)
        {
            bool hasChangePermission = false;
            hasChangePermission = HasPermission(UserAction.EditTestDates);
            View.RefreshPage(null, ActionType, string.Empty, string.Empty, false, hasChangePermission);
            searchText = String.IsNullOrEmpty(searchText) ? string.Empty : searchText;
            View.PopulateGroupTests(_adminService.GetTestsForGroup(programId, View.CohortId, View.GroupId, searchText));
        }

        public void ValidateDate()
        {
            string _startDate = string.Empty;
            string _endDate = string.Empty;
            bool success = true;
            DateTime _tempStartDate;
            DateTime _tempEndDate;
            if (_startDate.Trim().Length > 0)
            {
                success = DateTime.TryParse(_startDate.Trim(), out _tempStartDate);
                {
                    if (success)
                    {
                        startDate = _tempStartDate;
                    }
                }
            }
            else if (_startDate.Trim().Length == 0)
            {
                success = true;
            }

            if (success)
            {
                if (_endDate.Trim().Length > 0)
                {
                    success = DateTime.TryParse(_endDate.Trim(), out _tempEndDate);
                    if (success)
                    {
                        endDate = _tempEndDate;
                    }
                }
                else if (_endDate.Trim().Length == 0)
                {
                    success = true;
                }
            }

            _isValidDate = success;
        }

        public string AssignTestDateToGroup(List<GroupTestDates> testDates, ref string studentsMsg)
        {
            bool _validCohortSDate = false;
            string _message = string.Empty;
            List<GroupTestDates> validtestDates = new List<GroupTestDates>();
            foreach (var testDate in testDates)
            {
                testDate.TestStartDate = testDate.TestStartDate.Trim();
                testDate.TestEndDate = testDate.TestEndDate.Trim();
                var cohort = _adminService.GetCohort(testDate.Cohort.CohortId);
                testDate.Cohort.CohortEndDate = cohort.CohortEndDate;
                testDate.Cohort.CohortStartDate = cohort.CohortStartDate;
                ValidateDate();
                if (_isValidDate)
                {
                    if (testDate.TestStartDate.Length != 0)
                    {
                        testDate.TestStartDate = testDate.TestStartDate.ToDateTime().ToShortDateString() + " " + testDate.TestStartHour + ":" + testDate.TestStartMin + ":00" + testDate.TestStartTime;
                    }

                    if (testDate.TestEndDate.Length != 0)
                    {
                        testDate.TestEndDate = testDate.TestEndDate.ToDateTime().ToShortDateString() + " " + testDate.TestEndHour + ":" + testDate.TestEndMin + ":00" + testDate.TestEndTime;
                    }

                    if (testDate.TestStartDate.Length != 0 && testDate.TestEndDate.Length != 0)
                    {
                        string _cohortStartDate = cohort.CohortStartDate.Value.ToString();
                        string _cohortEndDate = cohort.CohortEndDate.Value.ToString();
                        
                        if (cohort.CohortEndDate != null)
                        {
                            _cohortEndDate = cohort.CohortEndDate.ToDateTime().AddDays(1).ToString();
                        }

                        if (CompareDates(_cohortStartDate, testDate.TestStartDate))
                        {
                            if (CompareDates(_cohortStartDate, testDate.TestEndDate))
                            {
                                if (CompareDates(testDate.TestStartDate, _cohortEndDate))
                                {
                                    if (CompareDates(testDate.TestEndDate, _cohortEndDate))
                                    {
                                        _validCohortSDate = true;
                                    }
                                    else
                                    {
                                        _validCohortSDate = false;
                                        _message = END_DATE_ERROR_MESSAGE;
                                    }
                                }
                                else
                                {
                                    _validCohortSDate = false;
                                    _message = END_DATE_ERROR_MESSAGE;
                                }
                            }
                            else
                            {
                                _validCohortSDate = false;
                                _message = START_DATE_ERROR_MESSAGE;
                            }
                        }
                        else
                        {
                            _validCohortSDate = false;
                            _message = END_DATE_ERROR_MESSAGE;
                        }

                        if (_validCohortSDate)
                        {
                            bool validTestTime = CompareDates(testDate.TestStartDate, testDate.TestEndDate);
                            if (validTestTime)
                            {
                                    validtestDates.Add(testDate);
                            }
                            else
                            {
                                _message = START_DATE_ERROR_MESSAGE;
                            }
                        }
                    }
                    else
                    {
                        validtestDates.Add(testDate);
                    }
                }
                else
                {
                    _message = WRONG_DATE_FORMAT_MESSAGE;
                }
            }

            if (string.IsNullOrEmpty(_message) && validtestDates.Count > 0)
            {
                if (IsStartEndDateDifferenceValid( validtestDates, ref _message))
                {
                    if (CurrentContext.UserType.Equals(UserType.SuperAdmin))
                    {
                        var studentMessage = _adminService.AssignTestToGroup(validtestDates, CurrentContext.UserType);
                        studentsMsg = studentMessage;
                    }
                    else
                    {
                        _adminService.AssignTestToGroup(validtestDates, CurrentContext.UserType);
                    }

                    _message = DATE_SAVE_MESSAGE;
                }
            }

            return _message;
        }

        private bool IsStartEndDateDifferenceValid(List<GroupTestDates> validtestDates, ref string message)
        {
            StringBuilder testNames = new StringBuilder();
            foreach (GroupTestDates ctd in validtestDates)
            {
                if (ctd.TestStartDate.Length != 0 && ctd.TestEndDate.Length != 0)
                {
                    var timeSpanHours =
                        Convert.ToDateTime(ctd.TestEndDate).Subtract(Convert.ToDateTime(ctd.TestStartDate)).TotalHours;

                    if ((timeSpanHours > 8) && (ctd.Product.ProductType == "1") &&
                        (ctd.Cohort.CohortEndDate >= DateTime.Today))
                    {
                        testNames.Append(ctd.TestName + ", ");
                    }
                }
            }

            message = "Please schedule following Integrated tests: " + testNames.ToString().Trim().TrimEnd(',') + " to remain open for 8 hours or less.";
            return testNames.Length == 0;
        }

        public void ExportGroups(string searchText, ReportAction printActions)
        {
            ////StringBuilder _assignedInstitutionList = new StringBuilder();
            ////_assignedInstitutionList = GetAssignedInstitutions(_assignedInstitutionList);
            var _institutions = string.Empty;
            _institutions = GetInstitutionIds(_institutions);
            var _cohorts = string.Empty;
            if (View.CohortIds != null)
            {
                _cohorts = View.CohortIds.Replace(",", "|");
            }

            View.ExportGroups(_adminService.SearchGroups(searchText, _institutions.ToString(), _cohorts.ToString()), printActions);
        }

        public void GetActiveCohortList(string institutuinIds)
        {
            View.PopulateCohort(_adminService.GetCohortsByInstitutionIds(institutuinIds));
        }

        public IEnumerable<Student> GetStudentsForGroups(int cohortId, int groupId)
        {
            return _adminService.GetStudentsForGroups(cohortId, groupId);
        }

        private string GetInstitutionIds(string _institutions)
        {
            if (View.InstitutionId == string.Empty)
            {
                IOrderedEnumerable<Institution> _institutionIds;
                StringBuilder _assignedinstitutionList;
                GetAssignedInstitutions(out _institutionIds, out _assignedinstitutionList);
                _institutions = _assignedinstitutionList.ToString();
            }
            else
            {
                _institutions = View.InstitutionId.Replace(",", "|");
            }

            return _institutions;
        }

        private bool CompareDates(string firstDate, string secondDate)
        {
            DateTime _datestart;
            DateTime _dateend;
            bool isValidEndDate = false;
            bool isValidStart = false;
            bool results = false;

            isValidStart = DateTime.TryParse(firstDate, out _datestart);
            if (isValidStart)
            {
                isValidEndDate = DateTime.TryParse(secondDate, out _dateend);
                if (isValidEndDate)
                {
                    int first = _dateend.CompareTo(_datestart);

                    if (first >= 0)
                    {
                        results = true;
                    }
                }
            }

            return results;
        }

        private Group GetGroupDetails()
        {
            var cohortName = string.Empty;
            var cohortId = 0;
            var institutionName = string.Empty;
            var institutionId = 0;
            var programofStudyName = string.Empty;
            var programofStudyId = 0;
            var institutionNameWithProgOfStudy = string.Empty;
            var group = new Group();
            if (ActionType != UserAction.Add)
            {
                group = _adminService.GetGroup(Id);
                if (group.Cohort.CohortId > 0)
                {
                    var cohort = _adminService.GetCohort(group.Cohort.CohortId);
                    cohortName = cohort.CohortName.ToString();
                    cohortId = cohort.CohortId.ToInt();
                    var institution = _adminService.GetInstitutions(CurrentContext.UserId, 0, cohort.InstitutionId.ToString()).FirstOrDefault();
                    institutionId = institution.InstitutionId;
                    institutionName = institution.InstitutionName;
                    programofStudyName = institution.ProgramofStudyName;
                    institutionNameWithProgOfStudy = institution.InstitutionNameWithProgOfStudy;
                    programofStudyId = programofStudyName == ProgramofStudyType.RN.ToString()
                                           ? (int)ProgramofStudyType.RN
                                           : (int)ProgramofStudyType.PN;
                }

                group.Cohort = new Cohort() { CohortName = cohortName, CohortId = cohortId };
                group.Institution = new Institution() { InstitutionName = institutionName, InstitutionId = institutionId, ProgramOfStudyId = programofStudyId, InstitutionNameWithProgOfStudy = institutionNameWithProgOfStudy };
                View.Name = group.GroupName;
                View.CohortId = group.Cohort.CohortId;
                View.GroupId = group.GroupId;
                View.InstitutionId = group.Institution.InstitutionId.ToString();
            }
            else
            {
                group.GroupName = string.Empty;
                View.CohortId = 0;
                View.GroupId = 0;
                View.InstitutionId = string.Empty;
            }

            return group;
        }

        private void GetAssignedInstitutions(out IOrderedEnumerable<Institution> _institutionIds, out StringBuilder _assignedinstitutionList)
        {
            _institutionIds = _adminService.GetInstitutions(CurrentContext.UserId, View.ProgramofStudyId, string.Empty).OrderBy(i => i.InstitutionName);
            _assignedinstitutionList = new StringBuilder();
            _assignedinstitutionList.Append(string.Empty);

            foreach (var institution in _institutionIds)
            {
                _assignedinstitutionList = _assignedinstitutionList.Append(institution.InstitutionId + "|");
            }
        }
    }
}
