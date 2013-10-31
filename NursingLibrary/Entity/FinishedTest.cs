using System;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Entity
{
    public class FinishedTest
    {
        public string PercentCorrect { get; set; }

        public int QuestionCount { get; set; }
        
        public int UserTestId { get; set; }
        
        public string TestName { get; set; }
        
        public DateTime TestStarted { get; set; }
        
        public int TestId { get; set; }
        
        public int TestStatus { get; set; }
        
        public string ProductName { get; set; }
        
        public TestType QuizOrQBank { get; set; }
        
        public int TestSubGroup { get; set; }
    }
}
