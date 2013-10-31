using System;

namespace NursingLibrary.Entity
{
    [Serializable]
    public class CohortResultsByModule
    {
        public int SubcategoryID { get; set; }

        public int Correct { get; set; }
        
        public int Total { get; set; }
    }
}
