using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Interfaces;
using NursingLibrary.Entity;

namespace NursingLibrary.Presenters
{
    public class InstitutionViewPresenter : AuthenticatedPresenterBase<IInstitutionView>
    {

         private readonly IAdminService _adminService;

         public InstitutionViewPresenter(IAdminService service) : base(Module.Institution)
        {
            _adminService = service;
        }

         public void OnViewInitialize(int institutionId)
         {
            NurInstitution institution = _adminService.GetNurInstitutionById(institutionId);
            View.Institution = institution;
            View.TimeZones = _adminService.GetTimeZones();
            if (institution != null)
            {
                View.Program = _adminService.GetNurProgramById(institution.ProgramID);
            }
            View.BindData();
         }


         public override void RegisterAuthorizationRules()
         {
             
         }

         public override void RegisterQueryParameters()
         {
             
         }
    }
}
