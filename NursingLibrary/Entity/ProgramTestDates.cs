using System;

namespace NursingLibrary.Entity
{
    [Serializable]
    public class ProgramTestDates
    {
        public Product Product { get; set; }

        public Program Program { get; set; }
        
        public Test Test { get; set; }
        
        public string TestName { get; set; }
        
        public int TestId { get; set; }
        
        public int Type { get; set; }

        public int AssetGroupId { get; set; }
    }
}
