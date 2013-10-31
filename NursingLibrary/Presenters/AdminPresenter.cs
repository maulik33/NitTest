using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class AdminPresenter : AuthenticatedPresenterBase<IAdminView>
    {
        private const string SECURITY_ACADEMIC_INSTITUTIONAL_CONDITION = "2|3|4|5";
        private const string SECURITY_LOCALADMIN_CONDITION = "3|4";
        private readonly IAdminService _adminService;
        private ViewMode _mode;

        public AdminPresenter(IAdminService service)
            : base(Module.UserManagement)
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

        public void ShowProgramofStudyDetails()
        {
            if (CurrentContext.UserType == UserType.SuperAdmin)
            {
                var programOfStudies = _adminService.GetProgramofStudies();
                View.PopulateProgramofStudy(programOfStudies);
            }
            else
            {
                ShowInstitutions();
            }
        }

        public void ShowAdminUserList(string sortMetaData)
        {
            StringBuilder _sbInstituionIds = new StringBuilder();
            _sbInstituionIds = GetInstitutionIds(_sbInstituionIds);
            var _userType = CurrentContext.UserType;
            View.SearchText = String.IsNullOrEmpty(View.SearchText) ? string.Empty : View.SearchText;

            if (_userType == UserType.SuperAdmin)
            {
                View.ShowAdminList(_adminService.SearchAdmins(string.Empty, string.Empty, View.SearchText, View.ProgramofStudyId),
                   SortHelper.Parse(sortMetaData));
            }
            else if (_userType == UserType.AcademicAdmin || _userType == UserType.InstitutionalAdmin)
            {
                View.ShowAdminList(_adminService.SearchAdmins(_sbInstituionIds.ToString(), SECURITY_ACADEMIC_INSTITUTIONAL_CONDITION, View.SearchText, View.ProgramofStudyId),
                    SortHelper.Parse(sortMetaData));
            }
            else if (_userType == UserType.LocalAdmin)
            {
                View.ShowAdminList(_adminService.SearchAdmins(_sbInstituionIds.ToString(), SECURITY_LOCALADMIN_CONDITION, View.SearchText, View.ProgramofStudyId),
                    SortHelper.Parse(sortMetaData));
            }
        }

        public void SaveAdmin(Admin admin)
        {
            if (IsValidAdmin(admin))
            {
                var _instituionId = 0;
                if (CurrentContext.UserType == UserType.LocalAdmin)
                {
                    _instituionId = _adminService.GetInstitution(0, CurrentContext.UserId).InstitutionId;
                }

                admin.Institution = new Institution() { InstitutionId = _instituionId };
                View.AdminId = _adminService.SaveAdmin(admin);
                if (View.AdminId != -1)
                {
                    Navigator.NavigateTo(AdminPageDirectory.AdminView, string.Empty, string.Format("{0}={1}", QUERY_PARAM_ID, View.AdminId));
                }
            }
        }

        public void DeleteAdmin()
        {
            _adminService.DeleteAdmin(Id);
            Navigator.NavigateTo(AdminPageDirectory.AdminList);
        }

        public void ShowAdminDetails()
        {
            bool hasAssignPermission = true;
            var admin = _adminService.GetAdmin(Id);
            View.PopulateAdmin(admin);
            Dictionary<int, string> ddList = new Dictionary<int, string>();
            if (ActionType == UserAction.Add)
            {
                ddList = GetUserTypes(admin.SecurityLevel);
            }
            else
            {
                ddList = GetUserTypes((int)UserType.SuperAdmin); // Passing as super-admin to get all types of users. Also in web form Read only field so can't change user type
            }

            if ((CurrentContext.UserType == UserType.LocalAdmin) || (admin.SecurityLevel == 0))
            {
                hasAssignPermission = false;
            }

            View.RefreshPage(null, ActionType, ddList, String.Empty, String.Empty, hasAssignPermission, false);
        }

        public void ShowAdminInstitutions()
        {
            View.PopulateInstitutions(_adminService.GetInstitutions(Id, 0, string.Empty).Where(i => i.Active == 1));
        }

        public void ShowInstitutions()
        {
            var _assignedInstitutions = _adminService.GetInstitutions(Id, 0, string.Empty).Where(i => i.Active == 1);
            var _institutions = _adminService.GetInstitutions(CurrentContext.UserId, CurrentContext.UserType == UserType.SuperAdmin ? View.ProgramofStudyId : 0, string.Empty);
            IEnumerable<Institution> onlyActiveInstitutions = _assignedInstitutions.Except(_institutions);

            foreach (var item in onlyActiveInstitutions)
            {
                var _activeInstitution = from ai in _institutions
                                         where (ai.InstitutionId == item.InstitutionId)
                                         select ai;
                if (_activeInstitution.Count() > 0)
                {
                    _activeInstitution.FirstOrDefault().Active = 1;
                }
            }

            View.PopulateInstitutions(_institutions);
        }
        //Fix for selected AssignInstituion going wrong for all admins except superadmin 
        public void ShowAssignInstitutions()
        {
            var _assignedInstitutions = _adminService.GetInstitutions(Id, 0, string.Empty).Where(i => i.Active == 1);
            var _institutions = _adminService.GetInstitutions(CurrentContext.UserId, CurrentContext.UserType == UserType.SuperAdmin ? View.ProgramofStudyId : 0, string.Empty);
            _institutions.ForEach(x => x.Active = 0);
            IEnumerable<Institution> onlyActiveInstitutions = _assignedInstitutions.Except(_institutions);

            foreach (var item in onlyActiveInstitutions)
            {
                var _activeInstitution = from ai in _institutions
                                         where (ai.InstitutionId == item.InstitutionId)
                                         select ai;
                if (_activeInstitution.Count() > 0)
                {
                    _activeInstitution.FirstOrDefault().Active = 1;
                }
            }

            View.PopulateInstitutions(_institutions);
        }

        public void AssignInstitutions(List<Admin> admins, string institutionIds)
        {
            _adminService.AssignInstitutionsToAdmin(admins, institutionIds, View.ProgramofStudyId);
            Navigator.NavigateTo(AdminPageDirectory.AdminView, string.Empty, string.Format("{0}={1}", QUERY_PARAM_ID, Id));
        }

        public void GetAdminDetails()
        {
            string title = string.Empty;
            string subTitle = string.Empty;
            bool hasDeletePermission = false;
            bool hasAddPermission = false;

            if (ActionType == UserAction.Edit)
            {
                title = "Edit > Administrator ";
                subTitle = "Use this page to edit an Administrator ";
                ShowAdminDetails();
            }
            else
            {
                title = "Add >  Administrator ";
                subTitle = "Use this page to add a new Administrator ";
                hasDeletePermission = false;
                Dictionary<int, string> ddList = GetUserTypes((int)CurrentContext.UserType);
                View.RefreshPage(null, ActionType, ddList, title, subTitle, hasDeletePermission, hasAddPermission);
            }
        }

        public void NavigateToEdit(string adminId, UserAction actionType)
        {
            if (actionType == UserAction.Add)
            {
                Navigator.NavigateTo(AdminPageDirectory.AdminEdit, string.Empty, string.Format("{0}={1}",
                    QUERY_PARAM_ACTION_TYPE, ((int)actionType).ToString()));
            }
            else if (actionType == UserAction.View)
            {
                Navigator.NavigateTo(AdminPageDirectory.AdminView, string.Empty, string.Format("{0}={1}",
                    QUERY_PARAM_ACTION_TYPE, ((int)actionType).ToString()));
            }
            else
            {
                Navigator.NavigateTo(AdminPageDirectory.AdminEdit, string.Empty, string.Format("{0}={1}&{2}={3}",
                    QUERY_PARAM_ACTION_TYPE, ((int)actionType).ToString(), QUERY_PARAM_ID, adminId));
            }
        }

        public void NavigateToEdit(UserAction actionType)
        {
            NavigateToEdit(GetParameterValue(QUERY_PARAM_ID), actionType);
        }

        public void NavigateToAssignInstitutions(string adminId)
        {
            Navigator.NavigateTo(AdminPageDirectory.AssignInstitute, string.Empty, string.Format("{0}={1}", QUERY_PARAM_ID, Id));
        }

        public void ExportAdminUsers(ReportAction printActions)
        {
            StringBuilder _sbInstituionIds = new StringBuilder();
            _sbInstituionIds = GetInstitutionIds(_sbInstituionIds);
            var _userType = CurrentContext.UserType;
            View.SearchText = String.IsNullOrEmpty(View.SearchText) ? string.Empty : View.SearchText;

            if (_userType == UserType.SuperAdmin)
            {
                View.ExportAdminUserList(_adminService.SearchAdmins(string.Empty, string.Empty, View.SearchText, View.ProgramofStudyId), printActions);
            }
            else if (_userType == UserType.AcademicAdmin || _userType == UserType.InstitutionalAdmin)
            {
                View.ExportAdminUserList(_adminService.SearchAdmins(_sbInstituionIds.ToString(), SECURITY_ACADEMIC_INSTITUTIONAL_CONDITION, View.SearchText, View.ProgramofStudyId),
                    printActions);
            }
            else if (_userType == UserType.LocalAdmin)
            {
                View.ExportAdminUserList(_adminService.SearchAdmins(_sbInstituionIds.ToString(), SECURITY_LOCALADMIN_CONDITION, View.SearchText, View.ProgramofStudyId),
                    printActions);
            }
        }

        private bool IsValidAdmin(Admin admin)
        {
            string errorMessage = string.Empty;
            bool isValid = true;
            if (!Utilities.Utilities.IsValidEmailAddress(admin.Email))
            {
                isValid = false;
                errorMessage = "Please enter a valid email.";
            }
            else if (_adminService.IsDuplicateUserName(admin.UserName, admin.UserId, true))
                {
                    isValid = false;
                    errorMessage = "User name already exists.";
                }

            if (!isValid)
            {
                View.ShowErrorMessage(errorMessage);
            }

            return isValid;
        }

        private Dictionary<int, string> GetUserTypes(int adminSecurityLevel)
        {
            UserTypeHelper objUserType = new UserTypeHelper();
            Dictionary<int, string> ddList = new Dictionary<int, string>();

            var _securityOptions = objUserType.GetUserTypes((UserType)adminSecurityLevel);
            var _adminTypeList = from s in _securityOptions
                                 select new KeyValuePair<int, UserType>((int)(UserType)s == 4 ? 0 : (int)(UserType)s, s);

            ddList = _adminTypeList.ToDictionary(k => k.Key, v => objUserType.GetUserForDisplay(v.Value));
            return ddList;
        }

        private StringBuilder GetInstitutionIds(StringBuilder _sbInstituionIds)
        {
            var _instituionIdsCollection = _adminService.GetInstitutions(CurrentContext.UserId, string.Empty).ToArray();
            if (_instituionIdsCollection.Count() > 0)
            {
                foreach (var item in _instituionIdsCollection)
                {
                    _sbInstituionIds.Append(item.InstitutionId + "|");
                }

                _sbInstituionIds = _sbInstituionIds.Remove(_sbInstituionIds.Length - 1, 1);
            }
            else
            {
                _sbInstituionIds.Append(string.Empty);
            }

            return _sbInstituionIds;
        }
    }
}