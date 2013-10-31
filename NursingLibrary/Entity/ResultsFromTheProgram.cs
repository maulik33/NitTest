using System;

namespace NursingLibrary.Entity
{
    [Serializable]
    public class ResultsFromTheProgram
    {
        public decimal Total { get; set; }

        public int NCorrect { get; set; }
        
        public int NInCorrect { get; set; }
        
        public int NAnswered { get; set; }
        
        public int CI { get; set; }
        
        public int II { get; set; }
        
        public int IC { get; set; }
        
        public int InstitutionId { get; set; }
    }
}
