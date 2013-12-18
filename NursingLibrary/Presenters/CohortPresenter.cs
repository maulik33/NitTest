using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class CohortPresenter : AuthenticatedPresenterBase<ICohortView>
    {
        private const string QUERY_PARAM_TEXTBOX = "textbox";
        private const string END_DATE_ERROR_MESSAGE = " Cannot set Test dates beyond Cohort End Date";
        private const string START_DATE_ERROR_MESSAGE = " Start date should be before the finish date";
        private const string DATE_SAVE_MESSAGE = "Test dates have been saved";
        private const string WRONG_DATE_FORMAT_MESSAGE = " Wrong Date Format";
        private const string ACTIVE_COHORT_EXISTS = "Active cohort with the same name/code exists for the current institution";
        private const string INACTIVE_COHORT_EXISTS = "Inactive cohort with the same name/code exists for the current institution";
        private readonly IAdminService _adminService;
        private ViewMode _mode;
        private DateTime? startDate = null;
        private DateTime? endDate = null;

        public CohortPresenter(IAdminService service)
            : base(Module.Cohort)
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

        public void SaveCohort()
        {
            int institution = 0;

            if (View.InstitutionId.ToInt() == -1)
            {
                institution = 0;
            }
            else
            {
                institution = View.InstitutionId.ToInt();
            }

            var cohort = new Cohort()
            {
                CohortId = Id,
                CohortDescription = View.Description,
                CohortName = View.Name,
                CohortEndDate = endDate,
                CohortStartDate = startDate,
                InstitutionId = institution,
                CohortStatus = View.CohortStatus,
                CohortCreateUser = CurrentContext.UserId,
            };
            Id = _adminService.SaveCohort(cohort);
            Navigator.NavigateTo(AdminPageDirectory.CohortView, string.Empty, string.Format("{0}={1}", QUERY_PARAM_ID, Id));
        }

        public void ShowCohortDetails()
        {
            View.PopulateProgramofStudy(_adminService.GetProgramofStudies());
            if (CurrentContext.UserType != UserType.SuperAdmin)
            {
                View.PopulateInstitutions(_adminService.GetInstitutions(CurrentContext.UserId, 0, string.Empty).OrderBy(i => i.InstitutionName));
            }
            else
            {
                View.PopulateInstitutions(_adminService.GetInstitutions(CurrentContext.UserId, View.ProgramofStudyId, string.Empty).OrderBy(i => i.InstitutionName));
            }

            string title = string.Empty;
            string subTitle = string.Empty;
            bool hasDeletePermission = true;
            bool hasAddPermission = true;
            bool hasAccessDatesEdit = true;
            bool hasEditPermission = true;
            if (ActionType == UserAction.Edit)
            {
                title = "Edit > Cohort ";
                subTitle = "Use this page to edit a Cohort. ";
                hasDeletePermission = HasPermission(UserAction.Delete);
                Cohort cohort = _adminService.GetCohort(Id);
                if (CurrentContext.UserType == UserType.SuperAdmin)
                {
                    View.PopulateInstitutions(_adminService.GetInstitutions(CurrentContext.UserId, cohort.ProgramofStudyId, string.Empty).OrderBy(i => i.InstitutionName));
                }
                View.ShowCohort(cohort);
            }
            else
            {
                title = "Add > Cohort ";
                subTitle = "Use this page to add a new Cohort. <br/> Cohort Start and End Dates are global and control login access for all students assigned to the Cohort.";
                hasDeletePermission = false;
            }

            hasEditPermission = HasPermission(UserAction.Edit);
            hasAddPermission = HasPermission(UserAction.Add);
            hasAccessDatesEdit = HasPermission(UserAction.AccessDatesEdit);
            View.RefreshPage(null, ActionType, title, subTitle, hasDeletePermission, hasAddPermission, hasAccessDatesEdit, hasEditPermission);
        }

        public void PopulateInstitution(int programofStudyId)
        {
            View.PopulateInstitutions(_adminService.GetInstitutions(CurrentContext.UserId, programofStudyId, string.Empty).OrderBy(i => i.InstitutionName));
        }

        public void DeleteCohort()
        {
            _adminService.DeleteCohort(Id, 0, CurrentContext.UserId);
            Navigator.NavigateTo(AdminPageDirectory.CohortList);
        }

        public void GetInstitutionList(string sortMetaData)
        {
            bool hasAddPermission = false;
            hasAddPermission = CurrentContext.UserType.Equals(UserType.SuperAdmin) ? true : false;
            View.RefreshPage(null, ActionType, string.Empty, string.Empty, false, hasAddPermission, false, true);

            IOrderedEnumerable<Institution> _institutionIds;
            StringBuilder _assignedinstitutionList;
            if (CurrentContext.UserType == UserType.SuperAdmin)
            {
                View.PopulateProgramofStudy(_adminService.GetProgramofStudies());
            }
            GetAssignedInstitutions(out _institutionIds, out _assignedinstitutionList);
            View.PopulateInstitutions(_institutionIds);
            View.PopulateProducts(_adminService.GetProducts());
            View.PopulateTests(_adminService.GetTests(View.ProductId, View.InstitutionId));
            View.PopulateCohorts(_adminService.SearchCohorts(_assignedinstitutionList.ToString(), View.SearchText, View.TestId, View.StartDate, View.EndDate, View.CohortStatus, View.ProgramofStudyId),
                SortHelper.Parse(sortMetaData));
        }

        public void GetCohortList(string institutuinIds)
        {
            View.PopulateCohorts(_adminService.GetCohorts(0, institutuinIds),
                SortHelper.Parse("CohortName|ASC"));
        }

        public void NavigateToEdit(string cohortId, UserAction actionType)
        {
            if (actionType == UserAction.Add)
            {
                Navigator.NavigateTo(AdminPageDirectory.CohortEdit, string.Empty, string.Format("{0}={1}",
                    QUERY_PARAM_ACTION_TYPE, ((int)actionType).ToString()));
            }
            else
            {
                Navigator.NavigateTo(AdminPageDirectory.CohortEdit, string.Empty, string.Format("{0}={1}&{2}={3}",
                    QUERY_PARAM_ACTION_TYPE, ((int)actionType).ToString(), QUERY_PARAM_ID, cohortId));
            }
        }

        public void NavigateToEdit(UserAction actionType)
        {
            NavigateToEdit(GetParameterValue(QUERY_PARAM_ID), actionType);
        }

        public void NavigateFromCohortList(AdminPageDirectory navigateTo, int cohortId)
        {
            if (navigateTo == AdminPageDirectory.CohortProgramAssign)
            {
                Navigator.NavigateTo(AdminPageDirectory.CohortProgramAssign, string.Empty, string.Format("{0}={1}",
                    QUERY_PARAM_ID, cohortId));
            }
            else if (navigateTo == AdminPageDirectory.StudentsInCohort)
            {
                Navigator.NavigateTo(AdminPageDirectory.StudentsInCohort, string.Empty, string.Format("{0}={1}",
                    QUERY_PARAM_ID, cohortId));
            }
            else if (navigateTo == AdminPageDirectory.CohortTestDates)
            {
                Navigator.NavigateTo(AdminPageDirectory.CohortTestDates, string.Empty, string.Format("{0}={1}",
                    QUERY_PARAM_ID, cohortId));
            }
            else if (navigateTo == AdminPageDirectory.CohortList)
            {
                Navigator.NavigateTo(AdminPageDirectory.CohortList, string.Empty, string.Format("{0}={1}",
                    QUERY_PARAM_ID, cohortId));
            }
            else if (navigateTo == AdminPageDirectory.CohortView)
            {
                Navigator.NavigateTo(AdminPageDirectory.CohortView, string.Empty, string.Format("{0}={1}",
                    QUERY_PARAM_ID, cohortId));
            }
            else if (navigateTo == AdminPageDirectory.CohortEdit)
            {
                Navigator.NavigateTo(AdminPageDirectory.CohortEdit, string.Empty, string.Format("{0}={1}",
                    QUERY_PARAM_ID, cohortId));
            }
            else if (navigateTo == AdminPageDirectory.ProgramAddAssign)
            {
                Navigator.NavigateTo(AdminPageDirectory.ProgramAddAssign, string.Empty, string.Format("{0}={1}",
                    QUERY_PARAM_ID, cohortId));
            }
        }

        public void NavigateFromStudentList(AdminPageDirectory navigateTo, int userId)
        {
            if (navigateTo == AdminPageDirectory.UserEdit)
            {
                Navigator.NavigateTo(AdminPageDirectory.UserEdit, string.Empty, string.Format("{0}={1}&{2}={3}",
                    QUERY_PARAM_ACTION_TYPE, "2", QUERY_PARAM_ID, userId));
            }
            else if (navigateTo == AdminPageDirectory.UserTestDates)
            {
                Navigator.NavigateTo(AdminPageDirectory.UserTestDates, string.Empty, string.Format("{0}={1}",
                    QUERY_PARAM_ID, userId));
            }
        }

        public void ShowCohort()
        {
            var cohort = _adminService.GetCohort(Id);
            if (cohort.InstitutionId == -1 || cohort.InstitutionId == 0)
            {
                cohort.InstitutionId = 0;
            }
            else
            {
                var institution = _adminService.GetInstitutions(CurrentContext.UserId, cohort.ProgramofStudyId, cohort.InstitutionId.ToString()).SingleOrDefault();
                cohort.Institution = new Institution()
                {
                    InstitutionName = institution.InstitutionName,
                    InstitutionId = institution.InstitutionId,
                    ProgramofStudyName = institution.ProgramofStudyName,
                    ProgramOfStudyId = institution.ProgramOfStudyId,
                    InstitutionNameWithProgOfStudy = institution.InstitutionNameWithProgOfStudy
                };
            }
            View.ProgramofStudyId = cohort.ProgramofStudyId;
            View.ShowCohort(cohort);
            bool hasAddPermission = CurrentContext.UserType == UserType.LocalAdmin || CurrentContext.UserType == UserType.TechAdmin ? false : true;
            View.RefreshPage(null, ActionType, string.Empty, string.Empty, true, hasAddPermission, true, false);
        }

        public void ValidateDate()
        {
            bool success = true;
            DateTime _tempStartDate;
            DateTime _tempEndDate;
            if (View.StartDate.Trim().Length > 0)
            {
                success = DateTime.TryParse(View.StartDate.Trim(), out _tempStartDate);
                if (success)
                {
                    startDate = _tempStartDate;
                }
            }
            else if (View.StartDate.Trim().Length == 0)
            {
                success = true;
            }

            if (success)
            {
                if (View.EndDate.Trim().Length > 0)
                {
                    success = DateTime.TryParse(View.EndDate.Trim(), out _tempEndDate);
                    if (success)
                    {
                        endDate = _tempEndDate;
                    }
                }
                else if (View.EndDate.Trim().Length == 0)
                {
                    success = true;
                }
            }

            View.IsValidDate = success;
        }

        public void ValidateCohort()
        {
            ValidateDate();
            if (View.IsValidDate)
            {
                List<Cohort> cohorts = _adminService.GetCohorts(0, View.InstitutionId).ToList();
                Cohort cohort = cohorts.Where(c => (c.CohortName.ToLower() == View.Name.ToLower() || (!string.IsNullOrEmpty(View.Description) && c.CohortDescription.ToLower() == View.Description.ToLower())) && c.CohortId != Id).FirstOrDefault();
                if (cohort != null)
                {
                    if (cohort.CohortStatus == (int)Status.Active)
                    {
                        View.ErrorMessage = ACTIVE_COHORT_EXISTS;
                    }
                    else
                    {
                        View.ErrorMessage = INACTIVE_COHORT_EXISTS;
                    }

                    View.IsInValidCohort = true;
                }
            }
        }

        public string AssignTestDateToCohort(List<CohortTestDates> testDates, ref string studentsMsg)
        {
            bool _validCohortSDate = false;
            string _message = string.Empty;
            List<CohortTestDates> validtestDates = new List<CohortTestDates>();
            var cohort = _adminService.GetCohort(Id);
            foreach (var testDate in testDates)
            {
                View.StartDate = testDate.TestStartDate;
                View.EndDate = testDate.TestEndDate;
                testDate.TestStartDate = testDate.TestStartDate.Trim();
                testDate.TestEndDate = testDate.TestEndDate.Trim();
                ValidateDate();
                if (View.IsValidDate)
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


                        if (!string.IsNullOrEmpty(_cohortEndDate))
                        {
                            _cohortEndDate = _cohortEndDate.ToDateTime().AddDays(1).ToString();
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

                if (IsStartEndDateDifferenceValid(cohort, validtestDates, ref _message))
                {
                    if (CurrentContext.UserType.Equals(UserType.SuperAdmin))
                    {
                        var studentMessage = _adminService.AssignTestDateToCohort(validtestDates, CurrentContext.UserType);
                        studentsMsg = studentMessage;
                    }
                    else
                    {
                        _adminService.AssignTestDateToCohort(validtestDates, CurrentContext.UserType);
                    }

                    _message = DATE_SAVE_MESSAGE;
                }
            }

            return _message;
        }

        private bool IsStartEndDateDifferenceValid(Cohort cohort, List<CohortTestDates> validtestDates, ref string message)
        {
            StringBuilder testNames = new StringBuilder();
            foreach (CohortTestDates ctd in validtestDates)
            {
                if (ctd.TestStartDate.Length != 0 && ctd.TestEndDate.Length != 0)
                {
                    var timeSpanHours =
                        Convert.ToDateTime(ctd.TestEndDate).Subtract(Convert.ToDateTime(ctd.TestStartDate)).TotalHours;

                    if ((timeSpanHours > 8) && (ctd.Product.ProductType == "1") &&
                        (cohort.CohortEndDate >= DateTime.Today))
                    {
                        testNames.Append(ctd.TestName + ", ");
                    }
                }
            }

            message = "Please schedule following Integrated tests:" + testNames.ToString().Trim().TrimEnd(',') + " to remain open for 8 hours or less.";
            return testNames.Length == 0;
        }

        public void ShowProgramList(string sortMetaData)
        {
            var program = new Program();
            var cohortProgramId = 0;
            bool hasAddPermission = HasPermission(UserAction.AssignProgram);
            View.RefreshPage(null, ActionType, string.Empty, string.Empty, true, hasAddPermission, true, false);
            ShowCohort();
            View.CohortId = Id;
            var cohortProgram = _adminService.GetCohortProgram(0, 0, Id);
            if (cohortProgram != null)
            {
                cohortProgramId = cohortProgram.ProgramId.ToInt();
                program = _adminService.GetProgram(cohortProgramId);
            }

            View.Name = program.ProgramName;
            View.Description = program.Description;
            View.ProgramId = program.ProgramId.ToInt();
            if (hasAddPermission)
            {
                SearchPrograms(string.Empty, sortMetaData);
            }
            else
            {
                SearchProgramForCohorts(string.Empty, sortMetaData);
            }
        }

        public void ShowTestForCohort()
        {
            bool hasAccessDatesEdit = false;
            hasAccessDatesEdit = HasPermission(UserAction.EditTestDates);
            View.RefreshPage(null, ActionType, string.Empty, string.Empty, false, false, hasAccessDatesEdit, false);
            View.CohortId = Id;
            var cohort = _adminService.GetCohort(Id);
            var institution = _adminService.GetInstitutions(CurrentContext.UserId, cohort.ProgramofStudyId, cohort.InstitutionId.ToString()).SingleOrDefault();
            cohort.Institution = new Institution() { InstitutionName = institution.InstitutionName, InstitutionId = institution.InstitutionId, InstitutionNameWithProgOfStudy = institution.InstitutionNameWithProgOfStudy };
            var cohortProgram = _adminService.GetCohortProgram(0, 0, cohort.CohortId);
            if (cohortProgram != null)
            {
                var cohortProgramId = cohortProgram.ProgramId.ToInt();
                Program program = _adminService.GetProgram(cohortProgramId);
                View.ProgramId = program.ProgramId;
                View.PopulateProgramForTest(program);
            }

            CohortTestDates testDates = new CohortTestDates()
            {
                Cohort = new Cohort() { CohortId = cohort.CohortId, CohortName = cohort.CohortName, CohortStartDate = cohort.CohortStartDate, CohortEndDate = cohort.CohortEndDate },
                Institution = new Institution() { InstitutionId = institution.InstitutionId, InstitutionName = institution.InstitutionName, InstitutionNameWithProgOfStudy = institution.InstitutionNameWithProgOfStudy },
            };
            View.InstitutionId = institution.InstitutionId.ToString();
            View.PopulateCohortTest(testDates);
            ShowTests();
        }

        public void ShowTests()
        {
            #region Trace Information
            TraceHelper.Create(CurrentContext.TraceToken, "Create Test Clicked")
                .Add("Program Id", View.ProgramId.ToString())
                .Add("Cohort Id", View.CohortId.ToString())
                .Add("SearchText", View.SearchText)
                .Write();
            #endregion
            bool hasAccessDatesEdit = false;
            hasAccessDatesEdit = HasPermission(UserAction.EditTestDates);
            View.RefreshPage(null, ActionType, string.Empty, string.Empty, false, false, hasAccessDatesEdit, false);
            View.SearchText = String.IsNullOrEmpty(View.SearchText) ? string.Empty : View.SearchText;
            View.PopulateCohortTests(_adminService.GetTestsForCohort(View.ProgramId, View.CohortId, 0, View.SearchText));
        }

        public void CanEditTestDates()
        {
            bool hasAccessDatesEdit = false;
            hasAccessDatesEdit = HasPermission(UserAction.EditTestDates);
            View.RefreshPage(null, ActionType, string.Empty, string.Empty, false, false, hasAccessDatesEdit, false);
        }

        public void ShowStudentsforCohort()
        {
            var cohort = _adminService.GetCohort(Id);
            if (CurrentContext.UserType == UserType.SuperAdmin)
            {
                View.PopulateProgramofStudy(_adminService.GetProgramofStudies());
                View.ProgramofStudyId = cohort.ProgramofStudyId;
                View.PopulateInstitutions(_adminService.GetInstitutions(CurrentContext.UserId, cohort.ProgramofStudyId, string.Empty).OrderBy(i => i.InstitutionName));
            }
            else
            {
                View.PopulateInstitutions(_adminService.GetInstitutions(CurrentContext.UserId, string.Empty).OrderBy(i => i.InstitutionName));
            }

            var institution = _adminService.GetInstitutions(CurrentContext.UserId, cohort.ProgramofStudyId, cohort.InstitutionId.ToString()).SingleOrDefault();
            if (institution != null)
            {
                cohort.Institution = new Institution() { InstitutionName = institution.InstitutionName, InstitutionId = institution.InstitutionId };
            }
            else
            {
                cohort.Institution = new Institution() { InstitutionName = string.Empty, InstitutionId = 0 };
            }

            View.InstitutionId = cohort.Institution.InstitutionId.ToString();
            View.CohortId = cohort.CohortId;
            View.InstitutionId = String.IsNullOrEmpty(View.InstitutionId) ? string.Empty : View.InstitutionId;
            View.PopulateCohorts(_adminService.GetCohorts(Id, cohort.Institution.InstitutionId.ToString()),
                SortHelper.Parse("CohortName|ASC"));
            var group = _adminService.GetGroups(0, View.CohortId.ToString());
            if (group.Count() > 0)
            {
                View.GroupId = group.FirstOrDefault().GroupId;
            }
            else
            {
                View.GroupId = 0;
            }

            View.PopulateGroups(group);
            View.RefreshPage(null, ActionType, cohort.Institution.InstitutionId.ToString(), string.Empty, true, false, false, false);
        }

        public void GetStudentsForCohort(string sortMetaData)
        {
            var cohortId = View.CohortId == -1 ? 0 : View.CohortId;
            var groupId = View.GroupId == -1 ? 0 : View.GroupId;
            View.PopulateStudents(_adminService.GetStudentsForCohorts(View.InstitutionId.ToInt(), cohortId, groupId),
                SortHelper.Parse(sortMetaData));
        }

        public void HasAssignPermission()
        {
            View.HasAddPermission = HasPermission(UserAction.AssignProgram);
        }

        public void AssignProgram()
        {
            _adminService.SaveCohortProgram(Id, View.ProgramId, 1);
        }

        public void GetTests()
        {
            View.PopulateTests(_adminService.GetTests(View.ProductId, View.InstitutionId));
        }

        public void showCohorts(string sortMetaData, bool useSearchString)
        {
            View.TestId = View.TestId == -1 ? 0 : View.TestId;
            var _institutions = string.Empty;
            _institutions = GetInstitutionIds(_institutions);

            if (useSearchString)
            {
                View.PopulateCohorts(_adminService.SearchCohorts(_institutions.ToString(), View.SearchText, View.TestId, View.StartDate, View.EndDate, View.CohortStatus, View.ProgramofStudyId),
                  SortHelper.Parse(sortMetaData));
            }
            else
            {
                View.PopulateCohorts(_adminService.SearchCohorts(_institutions.ToString(), string.Empty, View.TestId, View.StartDate, View.EndDate, View.CohortStatus, View.ProgramofStudyId),
                    SortHelper.Parse(sortMetaData));
            }
        }

        public void SearchProgram()
        {
            View.PopulatePrograms(_adminService.SearchPrograms(View.ProgramofStudyId, View.SearchText));
        }

        public void SearchPrograms(string searchText, string sortMetaData)
        {
            View.ShowProgramResults(_adminService.SearchPrograms(View.ProgramofStudyId, searchText),
                SortHelper.Parse(sortMetaData));
        }

        public void SearchProgramForCohorts()
        {
            View.PopulatePrograms(_adminService.SearchCohortPrograms(Id, View.SearchText));
        }

        public void SearchProgramForCohorts(string searchText, string sortMetaData)
        {
            View.ShowProgramResults(_adminService.SearchCohortPrograms(Id, searchText),
                SortHelper.Parse(sortMetaData));
        }

        public void ShowGroups()
        {
            View.CohortId = View.CohortId;
            View.PopulateGroups(_adminService.GetGroups(0, View.CohortId.ToString()));
        }

        public string GetControlValue()
        {
            string _result = String.Empty;
            if (IsQueryParameterExist(QUERY_PARAM_TEXTBOX))
            {
                _result = GetParameterValue(QUERY_PARAM_TEXTBOX);
            }

            return _result;
        }

        public void ExportCohorts(ReportAction printActions, bool useSearchString)
        {
            View.TestId = View.TestId == -1 ? 0 : View.TestId;
            var _institutions = string.Empty;
            _institutions = GetInstitutionIds(_institutions);

            if (useSearchString)
            {
                View.ExportCohortList(_adminService.SearchCohorts(_institutions.ToString(), View.SearchText, View.TestId, View.StartDate, View.EndDate, View.CohortStatus, View.ProgramofStudyId), printActions);
            }
            else
            {
                View.ExportCohortList(_adminService.SearchCohorts(_institutions.ToString(), string.Empty, View.TestId, View.StartDate, View.EndDate, View.CohortStatus, View.ProgramofStudyId), printActions);
            }
        }

        public string GetAnnotation(int Id)
        {
            IEnumerable<Institution> institutions = _adminService.GetInstitutions(CurrentContext.UserId.ToInt(), View.ProgramofStudyId, Id.ToString());
            return institutions.SingleOrDefault().Annotation;
        }

        public void ExportStudentsinCohorts(ReportAction printActions, bool useSearchString)
        {
            var cohortId = View.CohortId == -1 ? 0 : View.CohortId;
            var groupId = View.GroupId == -1 ? 0 : View.GroupId;
            View.ExportStudents(_adminService.GetStudentsForCohorts(View.InstitutionId.ToInt(), cohortId, groupId), printActions);
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

        private bool IsQueryParameterExist(string key)
        {
            bool keyExist = false;
            string[] keys = HttpContext.Current.Request.QueryString.AllKeys;
            if (keys != null && keys.Count() > 0)
            {
                List<string> keylist = keys.ToList<string>();
                keyExist = keylist.Contains(key);
            }

            return keyExist;
        }

        private void GetAssignedStates(out IOrderedEnumerable<State> _stateIds, out StringBuilder _assignedstateList)
        {
            _stateIds = _adminService.GetStates(0, 0).OrderBy(i => i.StateId);
            _assignedstateList = new StringBuilder();
            _assignedstateList.Append(string.Empty);

            foreach (var state in _stateIds)
            {
                _assignedstateList = _assignedstateList.Append(state.StateId + "|");
            }
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

        private bool CompareDates(string FirstDate, string SecondDate)
        {
            DateTime _datestart;
            DateTime _dateend;
            bool isValidEndDate = false;
            bool isValidStart = false;
            bool results = false;

            isValidStart = DateTime.TryParse(FirstDate, out _datestart);
            if (isValidStart)
            {
                isValidEndDate = DateTime.TryParse(SecondDate, out _dateend);
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
    }
}
