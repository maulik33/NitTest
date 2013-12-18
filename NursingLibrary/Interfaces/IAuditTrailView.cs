using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
   public interface IAuditTrailView
   {

       void GetStudentAuditTrail(IEnumerable<AuditTrail> auditTrailData);
   }
}
