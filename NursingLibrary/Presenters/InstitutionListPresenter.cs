using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Presenters
{
    public class InstitutionListPresenter : AuthenticatedPresenterBase<IInstitutionListView>
    {
        private readonly IAdminService _adminService;

        public InstitutionListPresenter(IAdminService service)
            : base(Module.Institution)
        {
            _adminService = service;
        }

        public void SearchInstitutions(string comaSepInstitutionIds, string searchText, string orderByProperty, string orderDirection)
        {
            View.ShowInstitutionResults(_adminService.SearchInstitutions(comaSepInstitutionIds, searchText), orderByProperty, orderDirection);
        }


        public override void RegisterAuthorizationRules()
        {

        }

        public override void RegisterQueryParameters()
        {

        }
    }
}
