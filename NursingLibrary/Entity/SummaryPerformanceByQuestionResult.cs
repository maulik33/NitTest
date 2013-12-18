namespace NursingLibrary.Entity
{
    public class SummaryPerformanceByQuestionResult
    {
        public string QuestionId { get; set; }

        public string Answer { get; set; }
        
        public int Total1 { get; set; }
        
        public int Total2 { get; set; }
        
        public int Total3 { get; set; }
        
        public int Total4 { get; set; }
        
        public int TotalN { get; set; }
        
        public int TotalNumberCorrect { get; set; }
        
        public int TotalNumberWrong { get; set; }
        
        public decimal CorrectPercent { get; set; }
        
        public int StudentNumber { get; set; }
    }
}
