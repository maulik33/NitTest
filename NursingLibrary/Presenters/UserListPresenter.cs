using System;
using System.Collections.Generic;
using System.Linq;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;
using System.Text;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class UserListPresenter : AuthenticatedPresenterBase<IUserListView>
    {
        private const string END_DATE_ERROR_MESSAGE = " Cannot set Test dates beyond Cohort End Date";
        private const string START_DATE_ERROR_MESSAGE = " Start date should be before the finish date";
        private const string DATE_SAVE_MESSAGE = "Test dates have been  saved";
        private const string WRONG_DATE_FORMAT_MESSAGE = " Wrong Date Format";
        private const string QUERY_PARAM_COHORTID = "CohortId";
        private readonly IAdminService _adminService;
        private ViewMode _mode;
        private DateTime? startDate = null;
        private DateTime? endDate = null;
        private bool _isValidDate = false;

        public UserListPresenter(IAdminService service)
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

            RegisterAuthorizationRule(UserAction.Add);
            RegisterAuthorizationRule(UserAction.Edit);
            RegisterAuthorizationRule(UserAction.Delete);
        }

        public override void RegisterQueryParameters()
        {
            if (_mode == ViewMode.Edit)
            {
                RegisterQueryParameter(QUERY_PARAM_ACTION_TYPE);
                RegisterQueryParameter(QUERY_PARAM_ID, QUERY_PARAM_ACTION_TYPE, "2");
            }
            else if (_mode == ViewMode.View)
            {
                RegisterQueryParameter(QUERY_PARAM_ID);
            }
        }

        public void PreInitialize(ViewMode mode)
        {
            _mode = mode;
        }

        public void ShowStudentList()
        {
            bool _showNotSelected = CurrentContext.UserType == UserType.SuperAdmin || CurrentContext.UserType == UserType.AcademicAdmin ? true : false;
            View.RefreshPage(null, ActionType, string.Empty, string.Empty, _showNotSelected, false, false);
            if (CurrentContext.UserType == UserType.SuperAdmin)
            {
                View.PopulateProgramofStudy(_adminService.GetProgramofStudies());
                GetInstitutionByProgramofStudy(1); //Default Program Of Study is RN
            }
            else
                View.PopulateInstitution(_adminService.GetInstitutions(CurrentContext.UserId, string.Empty));
            View.PopulateCohort(_adminService.GetCohorts(0, View.InstitutionId));
            View.PopulateGroup(_adminService.GetGroups(-1, View.CohortId.ToString()));
        }

        public void ShowProgramDetails()
        {
            bool _showNotSelected = CurrentContext.UserType == UserType.SuperAdmin || CurrentContext.UserType == UserType.AcademicAdmin ? true : false;
            View.RefreshPage(null, ActionType, string.Empty, string.Empty, _showNotSelected, false, false);
            View.PopulateProgramofStudy(_adminService.GetProgramofStudies());
            View.PopulateCohort(_adminService.GetCohorts(0, View.InstitutionId));
            View.PopulateGroup(_adminService.GetGroups(-1, View.CohortId.ToString()));
        }

        public void GetCohortListForInstitute()
        {
            if (View.InstitutionId.ToInt() <= 0)
            {
                View.CohortId = 0;
            }

            View.PopulateCohort(_adminService.GetCohorts(View.CohortId, View.InstitutionId));
        }

        public void GetGroupListForInstitute()
        {
            View.PopulateGroup(_adminService.GetGroups(0, View.CohortId.ToString()));
        }

        public void GetStudentList(string sortMetaData)
        {
            View.InstitutionId = (View.InstitutionId.ToInt() == -1) ? "0" : View.InstitutionId.ToInt().ToString();
            View.CohortId = (View.CohortId == -1) ? 0 : View.CohortId.ToInt();
            View.GroupId = (View.GroupId == -1) ? 0 : View.GroupId.ToInt();
            View.PopulateStudent(_adminService.SearchStudents(View.ProgramOfStudyId, View.InstitutionId.ToInt(), View.CohortId, View.GroupId, View.SearchString),
                SortHelper.Parse(sortMetaData));
        }

        public void GetStudentList()
        {
            View.InstitutionId = (View.InstitutionId.ToInt() == -1) ? "0" : View.InstitutionId.ToInt().ToString();
            View.CohortId = (View.CohortId == -1) ? 0 : View.CohortId.ToInt();
            View.GroupId = (View.GroupId == -1) ? 0 : View.GroupId.ToInt();
            View.PopulateStudentForTest(_adminService.SearchStudentsForTest(View.InstitutionId.ToInt(), View.CohortId, View.GroupId, View.SearchString));
        }

        public void PopulateStates(int countryId, int stateId)
        {
            View.PopulateState(_adminService.GetStates(countryId, stateId));
        }

        public int GetDefaultCountryForCountryList()
        {
            var val = _adminService.GetAppSettings();
            return 0;
        }

        public void PopulateCountryList()
        {
            var countryList = _adminService.GetCountries(0);
            View.PopulateCountry(countryList);
        }

        public void GetStudentDetail()
        {
            Student student = _adminService.GetStudent(Id);
            string title = string.Empty;
            string subTitle = string.Empty;
            bool hasEditPermission = false;
            bool hasDeletePermission = false;
            bool hasAddPermission = false;
            PopulateCountryList();

            if (ActionType == UserAction.Edit)
            {
                title = "View/Edit >> Student";
                subTitle = "Use this page to edit a Student";
                hasDeletePermission = HasPermission(UserAction.Delete);
                var _populateState = true;
                var _countryId = 0;
                var _defaultAddressCountry = KTPApp.DefaultAddressCountry.ToInt();
                var _countriesWithState = KTPApp.CountriesWithStates;
                var _addressId = student.StudentAddress.AddressId;
                var _address = _adminService.GetAddress(_addressId);

                if (_address.AddressId == 0 || _address.AddressCountry == null || _address.AddressCountry.CountryId == _defaultAddressCountry)
                {
                    _countryId = _defaultAddressCountry;
                }
                else if (_countriesWithState.Contains(_address.AddressCountry.CountryId.ToString()))
                {
                    _countryId = _address.AddressCountry.CountryId;
                }
                else
                {
                    _populateState = false;
                }

                if (true == _populateState)
                {
                    PopulateStates(_countryId, 0);
                }

                if (_addressId == 0)
                {
                    _address.AddressCountry = new Country { CountryId = _defaultAddressCountry };
                }

                View.PopulateAddress(_address);
                PopulateStates(KTPApp.DefaultAddressCountry.ToInt(), 0);
                View.InstitutionId = student.Institution.InstitutionId.ToInt().ToString();
                View.CohortId = student.Cohort.CohortId.ToInt();
                View.PopulateCohort(_adminService.GetCohorts(0, student.Institution.InstitutionId.ToInt().ToString()));
                GetInstitutionByProgramofStudy(student.Institution.ProgramOfStudyId);
                View.PopulateGroup(_adminService.GetGroups(0, View.CohortId.ToString()));
                View.GetStudentDetails(student);
            }
            else
            {
                title = "Add >  New Student ";
                subTitle = "Use this page to add a new Student. This is a temp page for QA and will not be needed once integration is done.";
                hasDeletePermission = false;
                View.InstitutionId = string.Empty;
                View.CohortId = 0;
                if (CurrentContext.UserType == UserType.SuperAdmin)
                    View.PopulateProgramofStudy(_adminService.GetProgramofStudies());
                else
                    View.PopulateInstitution(_adminService.GetInstitutions(CurrentContext.UserId, string.Empty));
                View.PopulateCohort(_adminService.GetCohorts(0, View.InstitutionId));
                View.PopulateGroup(_adminService.GetGroups(-1, View.CohortId.ToString()));
                PopulateStates(KTPApp.DefaultAddressCountry.ToInt(), 0);
            }

            hasAddPermission = HasPermission(UserAction.Add);
            hasEditPermission = HasPermission(UserAction.Edit);
            View.RefreshPage(student, ActionType, title, subTitle, hasDeletePermission, hasAddPermission, hasEditPermission);
        }

        public void GetInstitutionByProgramofStudy(int programOfStudyId)
        {
            View.PopulateInstitution(_adminService.GetInstitutions(CurrentContext.UserId, programOfStudyId, string.Empty));
        }

        public void ShowStduentsForGroup(string sortMetaData)
        {
            bool hasAccessToAssign = false;
            var cohortProgramId = 0;
            var _programName = string.Empty;
            hasAccessToAssign = HasPermission(UserAction.AssignToGroup);
            View.RefreshPage(null, ActionType, string.Empty, string.Empty, false, false, hasAccessToAssign);
            var group = _adminService.GetGroup(Id);
            var cohort = _adminService.GetCohort(group.Cohort.CohortId);
            var institution = _adminService.GetInstitutions(CurrentContext.UserId, 0, cohort.InstitutionId.ToString()).FirstOrDefault();
            cohort.Institution = new Institution() { InstitutionName = institution.InstitutionName, InstitutionId = institution.InstitutionId, InstitutionNameWithProgOfStudy = institution.InstitutionNameWithProgOfStudy };
            var cohortProgram = _adminService.GetCohortProgram(0, 0, group.Cohort.CohortId);
            if (cohortProgram != null && cohortProgram.ProgramId.ToInt() > 0)
            {
                cohortProgramId = cohortProgram.ProgramId.ToInt();
                _programName = _adminService.GetProgram(cohortProgramId).ProgramName.ToString();
            }

            var student = new Student();
            student.Cohort = new Cohort() { CohortName = cohort.CohortName };
            student.Institution = new Institution() { InstitutionName = institution.InstitutionName, InstitutionNameWithProgOfStudy = institution.InstitutionNameWithProgOfStudy };
            student.Group = new Group() { GroupName = group.GroupName };
            student.Program = new Program() { ProgramName = _programName };
            View.GetStudentDetails(student);
            var students = _adminService.GetStudentsForGroups(cohort.CohortId, group.GroupId);
            View.PopulateStudent(students, SortHelper.Parse(sortMetaData));
        }

        public void ShowTestDatesForStudents()
        {
            Student student = _adminService.GetStudent(Id);
            var cohortProgramId = 0;
            if (student.Group.GroupId > 0)
            {
                var group = _adminService.GetGroup(student.Group.GroupId);
                View.GroupId = group.GroupId;
                View.PopulateGroupForTest(group);
            }

            var cohort = _adminService.GetCohort(student.Cohort.CohortId);
            View.CohortId = cohort.CohortId;
            var institution = _adminService.GetInstitution(cohort.InstitutionId, CurrentContext.UserId);
            cohort.Institution = new Institution() { InstitutionName = institution.InstitutionName, InstitutionId = institution.InstitutionId, InstitutionNameWithProgOfStudy = institution.InstitutionNameWithProgOfStudy };
            var cohortProgram = _adminService.GetCohortProgram(0, 0, cohort.CohortId);

            if (cohortProgram != null)
            {
                cohortProgramId = cohortProgram.ProgramId.ToInt();
                View.PopulateProgramForTest(_adminService.GetProgram(cohortProgramId));
            }

            StudentTestDates testDates = new StudentTestDates()
            {
                Cohort = new Cohort() { CohortId = cohort.CohortId, CohortName = cohort.CohortName, CohortStartDate = cohort.CohortStartDate, CohortEndDate = cohort.CohortEndDate },
                Institution = new Institution() { InstitutionId = institution.InstitutionId, InstitutionName = institution.InstitutionName, InstitutionNameWithProgOfStudy = institution.InstitutionNameWithProgOfStudy },
                Student = new Student() { FirstName = student.FirstName, LastName = student.LastName },
            };
            View.PopulateStudentTest(testDates);
            ShowTests(cohortProgramId);
        }

        public void ShowTests(int programId)
        {
            bool hasChangePermission = false;
            hasChangePermission = HasPermission(UserAction.EditTestDates);
            View.RefreshPage(null, ActionType, string.Empty, string.Empty, false, false, hasChangePermission);
            View.SearchString = String.IsNullOrEmpty(View.SearchString) ? string.Empty : View.SearchString;
            View.PopulateStudentTests(_adminService.GetTestsForStudent(programId, Id, View.CohortId, View.GroupId, 0, View.SearchString));
        }

        public void GetDatesByCohortId()
        {
            View.GetDatesByCohortId(_adminService.GetDatesForCohortId(View.CohortId));
        }

        public int SaveUser(Student student)
        {
            int studentId = 0;
            if (IsValidStudent(student))
            {
                studentId = _adminService.SaveUser(student, CurrentContext.User.UserId, CurrentContext.User.UserName);
                if (studentId != -1)
                {
                    Navigator.NavigateTo(AdminPageDirectory.UserView, string.Empty, string.Format("{0}={1}", QUERY_PARAM_ID, studentId));
                }
            }

            return studentId;
        }

        public void DeleteUser()
        {
            _adminService.DeleteUser(GetParameterValue(QUERY_PARAM_ID).ToInt());
            Navigator.NavigateTo(AdminPageDirectory.UserList, string.Empty, string.Format("{0}={1}", QUERY_PARAM_ID, 0)); // passing 0 as in old code no querystring passed.
        }

        public void NavigateToEdit(string userId, UserAction actionType)
        {
            if (actionType == UserAction.Add)
            {
                Navigator.NavigateTo(AdminPageDirectory.UserAdd, string.Empty, string.Format("{0}={1}",
                    QUERY_PARAM_ACTION_TYPE, ((int)actionType).ToString()));
            }
            else
            {
                var nav = new Navigation.PageNavigator();
                nav.NavigateTo(AdminPageDirectory.UserEdit, string.Empty, string.Format("{0}={1}&{2}={3}",
                    QUERY_PARAM_ACTION_TYPE, ((int)actionType).ToString(), QUERY_PARAM_ID, userId));
            }
        }

        public void NavigateToEdit(UserAction actionType)
        {
            NavigateToEdit(GetParameterValue(QUERY_PARAM_ID), actionType);
        }

        public void NavigateToAssignTests(string userId)
        {
            Navigator.NavigateTo(AdminPageDirectory.UserTestDates, string.Empty, string.Format("{0}={1}",
                 QUERY_PARAM_ID, userId));
        }

        public void NavigateStudentToAssignTests(int adhocGroupId, int cohortId)
        {
            Navigator.NavigateTo(AdminPageDirectory.AssignStudentTest, string.Empty, string.Format("{0}={1}&{2}={3}",
                 QUERY_PARAM_ID, adhocGroupId, QUERY_PARAM_COHORTID, cohortId));
        }

        public void NavigateStudentToAdhocGroup()
        {
            Navigator.NavigateTo(AdminPageDirectory.AdhocGroup, string.Empty, string.Empty);
        }

        public void ShowUser()
        {
            Student student = _adminService.GetStudent(Id);
            var _defaultAddressCountry = KTPApp.DefaultAddressCountry.ToInt();
            var _countriesWithState = KTPApp.CountriesWithStates;
            var groupName = "Not Assigned";
            var cohortName = "Not Assigned";
            var institutionName = "Not Assigned";
            var programOfStudyName = "Not Assigned";
            Institution institution = new Institution();
            if (student.Group.GroupId > 0)
            {
                groupName = _adminService.GetGroup(student.Group.GroupId).GroupName;
            }

            if (student.Cohort.CohortId > 0)
            {
                cohortName = _adminService.GetCohort(student.Cohort.CohortId).CohortName;
            }

            if (student.Institution.InstitutionId > 0)
            {
                institution = _adminService.GetInstitution(student.Institution.InstitutionId, CurrentContext.UserId);

                institutionName = institution.InstitutionNameWithProgOfStudy;
                programOfStudyName = institution.ProgramofStudyName;
            }

            var _addressId = student.StudentAddress.AddressId;
            View.PopulateCountry(_adminService.GetCountries(0));
            var _address = _adminService.GetAddress(_addressId);
            View.PopulateAddress(_address);
            student.Cohort = new Cohort() { CohortName = cohortName };
            student.Institution = new Institution() { InstitutionName = institutionName, ProgramofStudyName = programOfStudyName };
            student.Group = new Group() { GroupName = groupName };
            View.GetStudentDetails(student);
        }

        public void GetUnAssignedStudentList(string sortMetaData)
        {
            View.InstitutionId = "0";
            View.CohortId = 0;
            View.GroupId = 0;
            View.SearchString = (View.SearchString == null) ? string.Empty : View.SearchString;
            View.PopulateStudent(_adminService.SearchStudents(View.ProgramOfStudyId, View.InstitutionId.ToInt(), View.CohortId, View.GroupId, View.SearchString, true),
                               SortHelper.Parse(sortMetaData));
        }

        public void AssignStudents(string userId, int institutionId, int cohortId, int groupId)
        {
            _adminService.AssignStudents(userId, cohortId, groupId, institutionId);
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
                if (success)
                {
                    startDate = _tempStartDate;
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

        public string AssignTestDateToStudent(List<StudentTestDates> testDates)
        {
            bool _validCohortSDate = false;
            string _message = string.Empty;
            List<StudentTestDates> validtestDates = new List<StudentTestDates>();
            foreach (var testDate in testDates)
            {
                testDate.Student = new Student() { UserId = Id };
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
                if (IsStartEndDateDifferenceValid(validtestDates, ref _message))
                {
                    _adminService.AssignTestToStudent(validtestDates);
                    _message = DATE_SAVE_MESSAGE;
                }
            }

            return _message;
        }

        private bool IsStartEndDateDifferenceValid(List<StudentTestDates> validtestDates, ref string message)
        {
            StringBuilder testNames = new StringBuilder();
            foreach (StudentTestDates std in validtestDates)
            {
                if (std.TestStartDate.Length != 0 && std.TestEndDate.Length != 0)
                {
                    var timeSpanHours =
                        Convert.ToDateTime(std.TestEndDate).Subtract(Convert.ToDateTime(std.TestStartDate)).TotalHours;

                    if ((timeSpanHours > 8) && (std.Product.ProductType == "1") &&
                        (std.Cohort.CohortEndDate >= DateTime.Today))
                    {
                        testNames.Append(std.TestName + ", ");
                    }
                }
            }

            message = "Please schedule following Integrated tests: " + testNames.ToString().Trim().TrimEnd(',') + " to remain open for 8 hours or less.";
            return testNames.Length == 0;
        }

        public void AssignStudentsToGroup(string assignStudentList, string unassignedStudentList)
        {
            _adminService.AssignStudentsToGroup(Id, assignStudentList, unassignedStudentList);
        }

        public void AssignStudentsToAdhocGroup(List<int> studentIds, AdhocGroup adhocGroup, int cohortId)
        {
            adhocGroup.CreatedBy = CurrentContext.UserId;
            _adminService.SaveAdhocGroup(studentIds, adhocGroup);
            NavigateStudentToAssignTests(adhocGroup.AdhocGroupId, cohortId);
        }

        public void SaveAdaAdhocGroup(List<int> StudentIds, AdhocGroup adaAdhocGroup)
        {
            adaAdhocGroup.CreatedBy = CurrentContext.UserId;
            _adminService.SaveAdaAdhocGroup(StudentIds, adaAdhocGroup);
        }

        public void PopulateAdhocAssignTest()
        {
            View.PopulateAdhocGroupForTest(_adminService.GetAdhocGroupTestDetail(Id));
        }

        public string SaveAdhocGroupTest(AdhocGroupTestDetails adhocGroupTestDetails, int cohortId)
        {
            bool _validDate = false;
            string _message = string.Empty;
            ValidateDate();
            adhocGroupTestDetails.AdhocGroupId = Id;
            adhocGroupTestDetails.CreatedBy = CurrentContext.UserId;
            var cohort = _adminService.GetCohort(cohortId);
            if (adhocGroupTestDetails.EndDate != null && adhocGroupTestDetails.StartDate != null)
            {
                if (adhocGroupTestDetails.StartDate.Length != 0 && adhocGroupTestDetails.EndDate.Length != 0)
                {
                    string _cohortStartDate = cohort.CohortStartDate.Value.ToString();
                    string _cohortEndDate = cohort.CohortEndDate.Value.ToString();
                    if (CompareDates(_cohortStartDate, adhocGroupTestDetails.StartDate))
                    {
                        if (CompareDates(_cohortStartDate, adhocGroupTestDetails.EndDate))
                        {
                            if (CompareDates(adhocGroupTestDetails.StartDate, _cohortEndDate))
                            {
                                if (CompareDates(adhocGroupTestDetails.EndDate, _cohortEndDate))
                                {
                                    _validDate = true;
                                }
                                else
                                {
                                    _validDate = false;
                                    _message = END_DATE_ERROR_MESSAGE;
                                }
                            }
                            else
                            {
                                _validDate = false;
                                _message = END_DATE_ERROR_MESSAGE;
                            }
                        }
                        else
                        {
                            _validDate = false;
                            _message = START_DATE_ERROR_MESSAGE;
                        }
                    }
                    else
                    {
                        _validDate = false;
                        _message = END_DATE_ERROR_MESSAGE;
                    }

                    if (_validDate)
                    {
                        bool validTestTime = CompareDates(adhocGroupTestDetails.StartDate, adhocGroupTestDetails.EndDate);
                        if (validTestTime)
                        {
                            _adminService.SaveAdhocGroupTest(adhocGroupTestDetails);
                            _message = DATE_SAVE_MESSAGE;
                        }
                        else
                        {
                            _message = START_DATE_ERROR_MESSAGE;
                        }
                    }
                }
            }

            return _message;
        }

        public IEnumerable<Test> GetTests(int cohortId)
        {
            var cohortProgram = _adminService.GetCohortProgram(0, 0, cohortId);
            return _adminService.GetTestsByCohort(cohortProgram.ProgramId.ToInt(), cohortId, 0, string.Empty);
        }

        public bool IsDuplicateUsername(string studentName, int studentId)
        {
           return _adminService.IsDuplicateUserName(studentName, studentId, false);
        }

        public bool CompareDates(string firstDate, string secondDate)
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

        public string GetLtiRequestFormForTestSecurityProvider(string studentId,string institutionId)
        {
            LtiProvider ltiProvider = GetLtiTestSecurityProvider();
            LtiResourceInfo ltiResourceInfo = GetLtiSecuredTestResourceInfo(studentId,institutionId);

            var ltiRequst = LtiUtility.CreateLtiRequest(ltiProvider, ltiResourceInfo);

            string ltiRequestForm = LtiRequestForm.BuildPostRequestForm(ltiRequst);

            return ltiRequestForm;
        }

        public LtiProvider GetLtiTestSecurityProvider()
        {
            return _adminService.GetLtiTestSecurityProviderByName(Constants.LTI_VERIFICIENT_ADMINREPORT_SECURITY_PROVIDER);
        }

        public LtiResourceInfo GetLtiSecuredTestResourceInfo(string studentId,string institutionId)
        {
            return new LtiResourceInfo
                {
                    UserId = CurrentContext.UserId.ToString(),
                    UserType = CurrentContext.UserType.ToString(),
                    StudentId = studentId,
                    ProductId = 1,
                    TestId = 0,
                    TestName = "",
                    InstitutionId = institutionId
                };
        }

        private bool IsValidStudent(Student student)
        {
            string errorMessage = string.Empty;
            bool isValid = true;
            if (!Utilities.Utilities.IsValidEmailAddress(student.Email))
            {
                isValid = false;
                errorMessage = "Please enter a valid email.";
            }

            if (!isValid)
            {
                View.ShowErrorMessage(errorMessage);
            }

            return isValid;
        }
    }
}
