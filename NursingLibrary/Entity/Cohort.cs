using System;

namespace NursingLibrary.Entity
{
    [Serializable]
    public class Cohort
    {
        public Institution Institution { get; set; }        

        public string CohortDescription { get; set; }
        
        public string CohortName { get; set; }
        
        public int CohortId { get; set; }
        
        public int CohortCreateUser { get; set; }
        
        public int CohortStatus { get; set; }
        
        public int InstitutionId { get; set; }
        
        public int StudentCount { get; set; }
        
        public DateTime? CohortStartDate { get; set; }
        
        public DateTime? CohortEndDate { get; set; }

        public State State { get; set; }

        public TimeZones TimeZones { get; set; }

        public int ProgramofStudyId { get; set; }

        public int RepeatingStudentCount { get; set; }
    }
}
