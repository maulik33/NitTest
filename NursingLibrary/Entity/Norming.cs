namespace NursingLibrary.Entity
{
    public class Norming
    {
        public int Id { get; set; }

        public double NumberCorrect { get; set; }
        
        public double Correct { get; set; }
        
        public double PercentileRank { get; set; }
        
        public double Probability { get; set; }
        
        public int TestId { get; set; }
    }
}
