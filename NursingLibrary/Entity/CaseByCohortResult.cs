namespace NursingLibrary.Entity
{
    public class CaseByCohortResult
    {
        public Cohort Cohort { get; set; }

        public int InstitutionId { get; set; }
        
        public decimal Percentage { get; set; }
        
        public int NumberOfStudents { get; set; }
    }
}