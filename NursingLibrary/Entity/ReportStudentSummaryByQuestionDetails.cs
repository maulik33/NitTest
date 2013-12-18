using System.Collections.Generic;

namespace NursingLibrary.Entity
{
    public class ReportStudentSummaryByQuestionDetails
    {
        public int StudentId { get; set; }

        public int Score { get; set; }
        
        public IDictionary<string, string> QuestionScrore { get; set; }
    }
}
