using System;
using System.Collections.Generic;

namespace NursingLibrary.Entity
{
    public class UserTest
    {
        public int UserTestId { get; set; }

        public DateTime TestStarted { get; set; }
        
        public Student Student { get; set; }
        
        public Test Test { get; set; }
        
        public int TestId { get; set; }
        
        public string SuspendType { get; set; }
        
        public string TestName { get; set; }
        
        public int TestNumber { get; set; }
        
        public bool Override { get; set; }
        
        public int SuspendQID { get; set; }
        
        public string TimeRemaining { get; set; }
        
        public int SuspendQuestionNumber { get; set; }
        
        public int TimedTest { get; set; }
        
        public int TutorMode { get; set; }
        
        public int NumberOfQuestions { get; set; }
        
        public int TestStatus { get; set; }
        
        public string UserName { get; set; }
        
        public DateTime TestDeletedDate { get; set; }
        
        public Dictionary<int, Question> Questions { get; set; }

        public bool IsCustomizedFRTest { get; set; }

        public DateTime? TestCompleted { get; set; }

        public string TimeUsed { get; set; }

        public int AnsweredCount { get; set; }

        public int LastQuestionAnswer { get; set; }

        public bool Active { get; set; }

        public string InstitutionNameWithProgOfStudy { get; set; }

        public string CohortName { get; set; }
    }
}
