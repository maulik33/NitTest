using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Interfaces;
using NursingLibrary.Entity;

namespace NursingLibrary.Presenters
{
   [Presenter]
   public class AuditTrailPresenter : AuthenticatedPresenterBase<IAuditTrailView>
   {
       private readonly IAdminService _adminService;
       private ViewMode _mode;

       public AuditTrailPresenter(IAdminService service) : base(Module.Student)
       {
           _adminService = service;
       }

       public void PreInitialize(ViewMode mode)
       {
           _mode = mode;
       }

       public void GetStudentAuditTrail(int studentId)
       {
           IEnumerable<AuditTrail> auditTrailData = _adminService.GetAuditTrailData(studentId);

           View.GetStudentAuditTrail(auditTrailData);
       }
   }
}
