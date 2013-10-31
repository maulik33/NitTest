namespace NursingLibrary.Entity
{
    public class ProgramResults
    {
        public int Correct { get; set; }

        public int Incorrect { get; set; }
        
        public int UnAnswered { get; set; }
        
        public int CorrectToIncorrect { get; set; }
        
        public int IncorrectToCorrect { get; set; }
        
        public int IncorrectToIncorrect { get; set; }
        
        public int Norm { get; set; }
        
        public string ItemText { get; set; }
        
        public int Total { get; set; }
        
        public string ChartType { get; set; }
        
        public int UserTestID { get; set; }

        public decimal DisplayTotal { get; set; }

        public decimal DisplayNorm { get; set; }
    }
}
