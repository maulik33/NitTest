using System;

namespace NursingLibrary.Entity
{
   [Serializable] 
   public class ResultsFromTheProgramForChart
    {
       public string InstitutionName { get; set; }

       public int Total { get; set; }
       
       public int NCorrect { get; set; }
       
       public string LevelOfDifficulty { get; set; }
       
       public decimal Norm { get; set; }
       
       public decimal Percentage { get; set; }
       
       public string ItemText { get; set; }
    }
}
