using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Interfaces;
using NursingLibrary.Entity;
using NursingLibrary.Utilities;

namespace NursingLibrary.Presenters
{
     [Presenter]
   public class UserDetailsPresenter:AuthenticatedPresenterBase<IuserDetailsView>
    {
        private readonly IAdminService _adminService;
        private ViewMode _mode;

        public UserDetailsPresenter(IAdminService service): base(Module.Institution)
        {
            _adminService = service;
        }

        public override void RegisterAuthorizationRules()
        {
        }

        public override void RegisterQueryParameters()
        {
        }

        public void PreInitialize(ViewMode mode)
        {
            _mode = mode;
        }

        public void SearchUserDetails(string sortMetaData, string status)
        {
            var _userType = CurrentContext.UserType;
            View.SearchText = String.IsNullOrEmpty(View.SearchText) ? string.Empty : View.SearchText;

            if (_userType == UserType.SuperAdmin)
            {
                View.ShowUserResults(_adminService.SearchUserDetails(View.SearchText, status, View.ProgramofStudyId), SortHelper.Parse(sortMetaData));
            }
        }
 
        public void ShowProgramofStudyDetails()
        {
            if (CurrentContext.UserType == UserType.SuperAdmin)
            {
                var programOfStudies = _adminService.GetProgramofStudies();
                View.PopulateProgramofStudy(programOfStudies);
            }
          
        }

    }
}
