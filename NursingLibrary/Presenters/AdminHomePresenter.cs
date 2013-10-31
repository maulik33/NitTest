using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class AdminHomePresenter : AuthenticatedPresenterBase<IAdminHomeView>
    {
        private readonly IAdminService _adminService;

        private ViewMode _mode;

        public AdminHomePresenter(IAdminService service)
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
        }

        public void PreInitialize(ViewMode mode)
        {
            _mode = mode;
        }

        public List<Institution> GetAssignedInstitutions()
        {
          return _adminService.GetInstitutions(CurrentContext.UserId, string.Empty);            
        }
    }
}
