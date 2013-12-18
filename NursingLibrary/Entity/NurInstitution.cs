using System;

namespace NursingLibrary.Entity
{
    public class NurInstitution
    {
        public int InstitutionId { get; set; }

        public string InstitutionName { get; set; }
        
        public string Description { get; set; }
        
        public string Status { get; set; }
        
        public string ContactName { get; set; }
        
        public string ContactPhone { get; set; }
        
        public string CenterId { get; set; }
        
        public int TimeZone { get; set; }
        
        public string IP { get; set; }
        
        public DateTime UpdateDate { get; set; }
        
        public int UpdateUser { get; set; }
        
        public int CreateUser { get; set; }
        
        public DateTime CreateDate { get; set; }
        
        public int FacilityID { get; set; }
        
        public int ProgramID { get; set; }
        
        public TimeZones TimeZones { get; set; }
        
        public int DeleteUser { get; set; }
    }
}
