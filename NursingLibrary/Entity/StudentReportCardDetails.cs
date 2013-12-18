using System;

namespace NursingLibrary.Entity
{
    public class StudentReportCardDetails
    {
        public StudentEntity Student { get; set; }

        public Cohort Cohort { get; set; }
        
        public Product Product { get; set; }
        
        public Group Group { get; set; }
        
        public int TestId { get; set; }
        
        public string TestName { get; set; }
        
        public DateTime? TestTaken { get; set; }
        
        public string RemediationTime { get; set; }
        
        public decimal Correct { get; set; }
        
        public string Rank { get; set; }
        
        public int UserTestId { get; set; }

        public int QuestionCount { get; set; }

        public int Ranking { get; set; }

        public string TimeUsed { get; set; }

        public string TestStyle { get; set; }
    }
}
