using System;

namespace NursingLibrary.Entity
{
    [Serializable]
    public class AuditTrail
    {
        public int AuditTrailId { get; set; }
        public int StudentId { get; set; }
        public string StudentUserName { get; set; }
        public string FromInstitution { get; set; }
        public string FromCohort { get; set; }
        public string FromGroup { get; set; }
        public string ToInstitution { get; set; }
        public string ToCohort { get; set; }
        public string ToGroup { get; set; }
        public DateTime? DateMoved { get; set; }
        public int AdminUserId { get; set; }
        public string AdminUserName { get; set; }
    }
}
