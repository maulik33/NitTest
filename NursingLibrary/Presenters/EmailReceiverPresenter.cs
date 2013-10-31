using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class EmailReceiverPresenter : ReportPresenterBase<IEmailReceiverView>
    {
        #region Fields
        public const string QUERY_PARAM_EMAIL_ID = "EmailId";
        private readonly IAdminService _adminService;
        private readonly IReportDataService _reportDataService;
        private StringBuilder _message = new StringBuilder();
        #endregion

        #region Constructor
        public EmailReceiverPresenter(IAdminService adminService, IReportDataService reportDataService)
            : base(Module.Reports)
        {
            _adminService = adminService;
            _reportDataService = reportDataService;
        }
        #endregion

        public enum EmailToAdminOrStudent
        {
            Admin,
            Student
        }

        public override void RegisterAuthorizationRules()
        {
        }

        public override void RegisterQueryParameters()
        {
        }

        public override void InitParamValues()
        {
            if (View.IsEmailIdExistInQueryString && View.IsEmailEdit == false)
            {
                Parameters[ReportParamConstants.PARAM_CUSTOM_EMAILS].SelectedValues = GetParameterValue(QUERY_PARAM_EMAIL_ID);
            }
        }

        public void PreInitialize()
        {
            if (View.IsEmailEdit == false)
            {
                ReportParameter customEmailsParam = new ReportParameter(ReportParamConstants.PARAM_CUSTOM_EMAILS, PopulateCustomEmails);
                ReportParameter programofStudy = new ReportParameter(ReportParamConstants.PARAM_PROGRAM_OF_Study, PopulateProgramofStudy);
                ReportParameter institutionParam = new ReportParameter(ReportParamConstants.PARAM_INSTITUTION, PopulateInstitutions);
                ReportParameter cohortParam = new ReportParameter(ReportParamConstants.PARAM_COHORT, PopulateCohorts);
                ReportParameter groupParam = new ReportParameter(ReportParamConstants.PARAM_GROUP, PopulateGroup);

                AddParameter(customEmailsParam);
                AddParameter(programofStudy);
                AddParameter(institutionParam, programofStudy);
                AddParameter(cohortParam, institutionParam);
                AddParameter(groupParam, cohortParam, institutionParam);
            }
        }

        public void PopulateCustomEmails()
        {
            IEnumerable<Email> customEmailList = _adminService.GetEmail();
            View.PopulateCustomEmails(customEmailList);
        }

        public void PopulateInstitutions()
        {
            int programofStudyId = (Parameters[ReportParamConstants.PARAM_PROGRAM_OF_Study].SelectedValues.ToInt() == 0) ? 1 : Parameters[ReportParamConstants.PARAM_PROGRAM_OF_Study].SelectedValues.ToInt();
            IEnumerable<Institution> institutions = _adminService.GetInstitutions(CurrentContext.UserId, programofStudyId, string.Empty);
            View.PopulateInstitutions(institutions);
        }

        public void PopulateInstitutions(int programofStudyId)
        {
            IEnumerable<Institution> institutions = _adminService.GetInstitutions(CurrentContext.UserId, programofStudyId, string.Empty);
            View.PopulateInstitutions(institutions);
        }

        public void PopulateCohorts()
        {
            IEnumerable<Cohort> cohorts = _reportDataService.GetCohorts(
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues);
            View.PopulateCohorts(cohorts);
        }

        public void PopulateGroup()
        {
            IEnumerable<Group> groups = new List<Group>();
            if (!string.IsNullOrEmpty(Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues))
            {
                groups = _adminService.GetGroups(0, Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues);
            }

            View.PopulateGroup(groups);
        }

        public void PopulateProgramofStudy()
        {
            var programOfStudies = _adminService.GetProgramofStudies();
            View.PopulateProgramofStudy(programOfStudies);
        }

        public void NavigateToEmailEdit(int emailId)
        {
            Navigator.NavigateTo(AdminPageDirectory.EmailEdit, string.Empty, string.Format("{0}={1}",
                    QUERY_PARAM_EMAIL_ID, emailId));
        }

        public void PoupulateStudentAdmin()
        {
            PoupulateStudent(string.Empty);

            PopulateAdmin(string.Empty);
        }

        public void SearchStudent(string searchCriteria)
        {
            PoupulateStudent(searchCriteria);
        }

        public void SearchAdmin(string searchCriteria)
        {
            PopulateAdmin(searchCriteria);
        }

        public void GenerateEmailMission(DateTime T, int customEmails, string selectedAdminIds, string selectedStudentIds)
        {
            int missionId;
            if (IsValidateEmailMission(selectedAdminIds, selectedStudentIds))
            {
                //// Custom Email Mission
                if (customEmails != -1)
                {
                    missionId = CreatCustomEmailMission(T, customEmails, selectedAdminIds, selectedStudentIds);
                }

                if (View.UserEmailToStudents)
                {
                    missionId = CreateUserInfotoStudents(T, selectedStudentIds);
                }

                if (View.UserEmailLocalAdmins)
                {
                    missionId = CreateUserInfotoLocalAdmin(T, selectedAdminIds);
                }

                if (View.UserEmailTechAdmins)
                {
                    missionId = CreateUserInfotoTechAdmin(T, selectedAdminIds);
                }

                View.ShowSendEmailResult(_message.ToString());
            }
        }

        public IEnumerable<Email> SearchCustomEmails(string keyword)
        {
            IEnumerable<Email> searchEmail = from email in _adminService.GetEmail()
                                             where email.Title.ToLower().Contains(keyword.ToLower()) || email.Body.ToLower().Contains(keyword.ToLower())
                                             select email;
            return searchEmail;
        }

        #region Email Edit Methods
        public void GetEmailDetails()
        {
            Email emails = new Email();
            if (View.EmailId.ToInt() > 0)
            {
                emails = _adminService.GetEmail(View.EmailId.ToInt()).FirstOrDefault();
            }

            View.ShowEmailDetails(emails);
        }

        public void SaveEmail(int emailId, string title, string body)
        {
            _adminService.SaveEmail(emailId, title, body);
            NavigateToEmailReceiver();
        }

        public void NavigateToEmailReceiver()
        {
            Navigator.NavigateTo(AdminPageDirectory.EmailReceiver, string.Empty, string.Format("{0}={1}",
                    QUERY_PARAM_EMAIL_ID, View.EmailId));
        }
        #endregion

        public void NavigateToLogin()
        {
            Navigator.NavigateTo(AdminPageDirectory.AdminLogin, null, null);
        }

        private bool IsValidateEmailMission(string selectedAdminIds, string selectedStudentIds)
        {
            bool isvalidMission = true;
            StringBuilder errorMessage = new StringBuilder();
            var studentEmailMission = new List<EmailMission>();
            var adminEmailMission = new List<EmailMission>();
            List<string> InvalidEmailIds = new List<string>();
            List<string> duplicateUserNames = new List<string>();
            if (View.UserEmailToStudents || View.CustomEmailToStudents)
            {
                studentEmailMission = _adminService.GetStudentEmailMission(selectedStudentIds, Parameters[ReportParamConstants.PARAM_GROUP].SelectedValues, Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues, Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues);
            }

            if (View.UserEmailLocalAdmins || View.UserEmailTechAdmins || View.CustomEmailToAdmins)
            {
                adminEmailMission = _adminService.GetAdminEmailMission(selectedAdminIds, Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues);
            }

            foreach (EmailMission studentmail in studentEmailMission)
            {
                if (!Utilities.Utilities.IsValidEmailAddress(studentmail.EmailId))
                {
                    InvalidEmailIds.Add(studentmail.EmailId);
                }
            }

            foreach (EmailMission adminmail in adminEmailMission)
            {
                if (!Utilities.Utilities.IsValidEmailAddress(adminmail.EmailId))
                {
                    InvalidEmailIds.Add(adminmail.EmailId);
                }
            }

            var duplicateStudents = studentEmailMission.GroupBy(r => r.UserName).Where(gr => gr.Count() > 1);
            var duplicateAdmins = adminEmailMission.GroupBy(r => r.UserName).Where(gr => gr.Count() > 1);
            ////Add duplicate admin User names
            AddDuplicateUserNames(duplicateAdmins, duplicateUserNames);
            ////Add Duplicate Student user names  || 
            AddDuplicateUserNames(duplicateStudents, duplicateUserNames);
            errorMessage.Append("Email mission you are trying to create contains following errors. Please correct the errors and retry to create email mission.<br/><br/>");
            if (InvalidEmailIds.Count > 0)
            {
                isvalidMission = false;
                List<string> disctinctMailIds = new List<string>();
                disctinctMailIds.AddRange(InvalidEmailIds.Distinct());
                errorMessage.Append("Bad email addresses : <br/><ul>");
                foreach (string mailId in disctinctMailIds)
                {
                    errorMessage.Append("<li>" + mailId + "</li>");
                }

                errorMessage.Append("</ul><br/>");
            }

            if (duplicateUserNames.Count > 0)
            {
                isvalidMission = false;
                errorMessage.Append("Duplicate user names : <br/><ul>");
                foreach (string username in duplicateUserNames)
                {
                    errorMessage.Append("<li>" + username + "</li>");
                }

                errorMessage.Append("</ul><br/>");
            }

            if (!isvalidMission)
            {
                View.ShowSendEmailResult(errorMessage.ToString());
            }

            return isvalidMission;
        }

        private void AddDuplicateUserNames(IEnumerable<IGrouping<string, EmailMission>> duplicateAdmins, List<string> duplicateUserNames)
        {
            foreach (var kv in duplicateAdmins)
            {
                foreach (var kv1 in kv)
                {
                    duplicateUserNames.Add(kv1.UserName);
                    break;
                }
            }
        }

        private void PopulateAdmin(string searchCriteria)
        {
            IEnumerable<Admin> admins = _adminService.GetAdmin(Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues, searchCriteria);
            View.PopulateAdmin(admins);
        }

        private void PoupulateStudent(string searchCriteria)
        {
            IEnumerable<StudentEntity> students = _reportDataService.GetStudents(
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues,
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues,
                Parameters[ReportParamConstants.PARAM_GROUP].SelectedValues,
                searchCriteria);
            View.PopulateStudent(students);
        }

        private int CreatCustomEmailMission(DateTime T, int customEmails, string selectedAdminIds, string selectedStudentIds)
        {
            var _missionId = 0;
            ////Email to admin
            if (View.CustomEmailToAdmins)
            {
                if (!string.IsNullOrEmpty(selectedAdminIds))
                {
                    _missionId = _adminService.CreateCustomEmailToPerson(CurrentContext.UserId, T, customEmails, false, selectedAdminIds);
                    _message.Append("<li>Email mission been created. Mission ID: " + _missionId + "; Time To Send: " + T + "; Receivers: " + selectedAdminIds.Split('|').Length + " Admins; Type: Custom.</li>");
                }
                else
                {
                    if (!string.IsNullOrEmpty(Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues))
                    {
                        _missionId = _adminService.CreateCustomEmailToInstitution(CurrentContext.UserId, T, customEmails, false, Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues);
                        _message.Append("<li>Email mission been created. Mission ID: " + _missionId + "; Time To Send: " + T + "; Receivers: All Admins of " + Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues.Split('|').Length + " Institutions; Type: Custom.</li>");
                    }
                }
            }

            if (View.CustomEmailToStudents)
            {
                if (!string.IsNullOrEmpty(selectedStudentIds))
                {
                    _missionId = _adminService.CreateCustomEmailToPerson(CurrentContext.UserId, T, customEmails, true,
                                                                         selectedStudentIds);
                    _message.Append("<li>Email mission been created. Mission ID: " + _missionId + "; Time To Send: " + T +
                                    "; Receivers: " + selectedStudentIds.Split('|').Length +
                                    " Students; Type: Custom.</li>");
                }
                else if (!string.IsNullOrEmpty(Parameters[ReportParamConstants.PARAM_GROUP].SelectedValues))
                {
                    // Email to Group
                    _missionId = _adminService.CreateCustomEmailToGroup(CurrentContext.UserId, T, customEmails, true,
                                                                        Parameters[ReportParamConstants.PARAM_GROUP].SelectedValues);
                    _message.Append("<li>Email mission been created. Mission ID: " + _missionId + "; Time To Send: " +
                                    T + "; Receivers: All Students of " + Parameters[ReportParamConstants.PARAM_GROUP].SelectedValues.Split('|').Length +
                                    " Group; Type: Custom.</li>");
                }
                else if (!string.IsNullOrEmpty(Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues))
                {
                    // Email to Cohort
                    _missionId = _adminService.CreateCustomEmailToCohort(CurrentContext.UserId, T, customEmails,
                                                                         true,
                                                                         Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues);
                    _message.Append("<li>Email mission been created. Mission ID: " + _missionId +
                                    "; Time To Send: " + T + "; Receivers: All students of " +
                                    Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues.Split('|').Length + " Cohort; Type: Custom.</li>");
                }
                else if (!string.IsNullOrEmpty(Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues))
                {
                    // Email to Institution
                    _missionId = _adminService.CreateCustomEmailToInstitution(CurrentContext.UserId, T,
                                                                              customEmails, true,
                                                                              Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues);
                    _message.Append("<li>Email mission been created. Mission ID: " + _missionId +
                                    "; Time To Send: " + T + "; Receivers: All students of " +
                                    Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues.Split('|').Length + " Institution; Type: Custom.</li>");
                }
            }

            return -1;
        }

        private int CreateUserInfotoStudents(DateTime T, string selectedStudentIds)
        {
            var missionId = 0;
            if (!string.IsNullOrEmpty(selectedStudentIds))
            {
                missionId = _adminService.CreateCustomEmailToPerson(CurrentContext.UserId, T, -100, true, selectedStudentIds);
                _message.Append("<li>Email mission been created. Mission ID: " + missionId + "; Time To Send: " + T + "; Receivers: " + selectedStudentIds.Split('|').Length + " Students; Type: User Info to Student.</li>");
                return missionId;
            }
            ////Email to Group
            if (!string.IsNullOrEmpty(Parameters[ReportParamConstants.PARAM_GROUP].SelectedValues))
            {
                missionId = _adminService.CreateCustomEmailToGroup(CurrentContext.UserId, T, -100, true, Parameters[ReportParamConstants.PARAM_GROUP].SelectedValues);
                _message.Append("<li>Email mission been created. Mission ID: " + missionId + "; Time To Send: " + T + "; Receivers: All Students of " + Parameters[ReportParamConstants.PARAM_GROUP].SelectedValues.Split('|').Length + " Group; Type: User Info to Student.</li>");
                return missionId;
            }
            ////Email to Cohort
            if (!string.IsNullOrEmpty(Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues))
            {
                missionId = _adminService.CreateCustomEmailToCohort(CurrentContext.UserId, T, -100, true, Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues);
                _message.Append("<li>Email mission been created. Mission ID: " + missionId + "; Time To Send: " + T + "; Receivers: All Students of " + Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues.Split('|').Length + " Cohort; Type: User Info to Student.</li>");
                return missionId;
            }
            ////Email to Institution
            if (!string.IsNullOrEmpty(Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues))
            {
                missionId = _adminService.CreateCustomEmailToInstitution(CurrentContext.UserId, T, -100, true, Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues);
                _message.Append("<li>Email mission been created. Mission ID: " + missionId + "; Time To Send: " + T + "; Receivers: All Students of " + Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues.Split('|').Length + " Institution; Type: User Info to Student.</li>");
                return missionId;
            }

            return -1;
        }

        private int CreateUserInfotoLocalAdmin(DateTime T, string selectedAdminIds)
        {
            int missionId;
            if (!string.IsNullOrEmpty(selectedAdminIds))
            {
                if (!string.IsNullOrEmpty(Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues))
                {
                    missionId = _adminService.CreateCustomEmailToPerson(CurrentContext.UserId, T, -101, false, selectedAdminIds);
                    _message.Append("<li>Email mission been created. Mission ID: " + missionId + "; Time To Send: " + T + "; Receivers: " + selectedAdminIds.Split('|').Length + " Admins; Type: User Info to Local Admin.</li>");
                    return missionId;
                }
            }

            ////Email to Institution
            if (!string.IsNullOrEmpty(Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues))
            {
                missionId = _adminService.CreateCustomEmailToInstitution(CurrentContext.UserId, T, -101, false, Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues);
                _message.Append("<li>Email mission been created. Mission ID: " + missionId + "; Time To Send: " + T + "; Receivers: All Local Admins of " + Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues.Split('|').Length + " Institution; Type: User Info to Local Admin.</li>");
                return missionId;
            }

            return -1;
        }

        private int CreateUserInfotoTechAdmin(DateTime T, string selectedAdminIds)
        {
            int missionId;
            if (!string.IsNullOrEmpty(selectedAdminIds))
            {
                if (!string.IsNullOrEmpty(Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues))
                {
                    missionId = _adminService.CreateCustomEmailToPerson(CurrentContext.UserId, T, -102, false, selectedAdminIds);
                    _message.Append("<li>Email mission been created. Mission ID: " + missionId + "; Time To Send: " + T + "; Receivers: " + selectedAdminIds.Split('|').Length + " Admins; Type: User Info to Tech Admin.</li>");
                    return missionId;
                }
            }

            ////Email to Institution
            if (!string.IsNullOrEmpty(Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues))
            {
                missionId = _adminService.CreateCustomEmailToInstitution(CurrentContext.UserId, T, -102, false, Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues);
                _message.Append("<li>Email mission been created. Mission ID: " + missionId + "; Time To Send: " + T + "; Receivers: All Tech Admins of " + Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues.Split('|').Length + " Institution; Type: User Info to Tech Admin.</li>");
                return missionId;
            }

            return -1;
        }


    }
}