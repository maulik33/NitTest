using System;

namespace NursingLibrary.Entity
{
    [Serializable]
    public class Group
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; }
        
        public int GroupUserId { get; set; }
        
        public Program Program { get; set; }
        
        public Institution Institution { get; set; }
        
        public Cohort Cohort { get; set; }
    }
}
