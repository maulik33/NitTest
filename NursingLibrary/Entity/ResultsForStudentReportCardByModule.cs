using System;

namespace NursingLibrary.Entity
{
    [Serializable]
    public class ResultsForStudentReportCardByModule
    {
        public StudentEntity Student { get; set; }

        public Modules Module { get; set; }
        
        public CaseStudy CaseStudy { get; set; }
        
        public decimal Correct { get; set; }
    }
}
