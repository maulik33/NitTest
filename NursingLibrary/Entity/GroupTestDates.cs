using System;

namespace NursingLibrary.Entity
{
    [Serializable]
    public class GroupTestDates
    {
        public Product Product { get; set; }

        public Test Test { get; set; }
        
        public Group Group { get; set; }
        
        public Program Program { get; set; }
        
        public Institution Institution { get; set; }
        
        public Cohort Cohort { get; set; }
        
        public string TestName { get; set; }
        
        public int TestType { get; set; }
        
        public string TestStartDate { get; set; }
        
        public string TestEndDate { get; set; }
        
        public string TestStartTime { get; set; }
        
        public string TestEndTime { get; set; }
        
        public int TestStartHour { get; set; }
        
        public int TestEndHour { get; set; }

        public int TestStartMin { get; set; }

        public int TestEndMin { get; set; }
        
        public int Type { get; set; }
        
        public int TestId { get; set; }
    }
}
