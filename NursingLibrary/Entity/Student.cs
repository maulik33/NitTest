using System;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Entity
{
    [Serializable]
    public class Student
    {
        #region Properties
        public Program Program { get; set; }

        public Test Test { get; set; }
        
        public Institution Institution { get; set; }
        
        public Cohort Cohort { get; set; }
        
        public Group Group { get; set; }
        
        public int UserId { get; set; }
        
        public string UserName { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public int CohortId { get; set; }
        
        public int GroupId { get; set; }
        
        public int InstitutionId { get; set; }

        public string RepeatExpiryDate { get; set; }

        public int ProgramId { get; set; }
        
        public int TimeOffset { get; set; }
        
        public bool Ada { get; set; }
        
        public string KaplanUserId { get; set; }
        
        public string EnrollmentId { get; set; }
        
        public string InstitutionIpLock { get; set; }
        
        public bool IsIntegratedTest { get; set; }
        
        public bool IsFocusedReviewTest { get; set; }
        
        public bool IsNclexTest { get; set; }
        
        public bool IsQbankTest { get; set; }
        
        public bool IsQbankSampleTest { get; set; }
        
        public bool IsTimedQbankTest { get; set; }
        
        public bool IsDignosticTest { get; set; }
        
        public bool IsReadinessTest { get; set; }
        
        public bool IsDignosticResultTest { get; set; }
        
        public bool IsReadinessResultTest { get; set; }
        
        public bool IsDiagnosticAndReadinessTest { get; set; }
        
        public int ProductId { get; set; }
        
        public TestType TestType { get; set; }
        
        public TestType QuizOrQBank { get; set; }
        
        public int TestSubGroup { get; set; }
        
        public int UserTestId { get; set; }
        
        public int TestId { get; set; }
        
        public Presenters.Action Action { get; set; }
        
        public string SuspendType { get; set; }
        
        public int QuestionId { get; set; }
        
        public QuestionType QuestionType { get; set; }
        
        public int NumberOfQuestions { get; set; }
        
        public string UserPass { get; set; }
        
        public string Email { get; set; }
        
        public string Telephone { get; set; }
        
        public string UserType { get; set; }
        
        public string CountryCode { get; set; }
        
        public DateTime UserCreateDate { get; set; }
        
        public DateTime? UserExpireDate { get; set; }
        
        public DateTime? UserStartDate { get; set; }
        
        public DateTime? UserUpdateDate { get; set; }
        
        public DateTime? UserDeleteData { get; set; }
        
        public int Integreted { get; set; }
        
        public string ExpireDate { get; set; }
        
        public string StartDate { get; set; }
        
        public string Password { get; set; }
        
        public int CreateUser { get; set; }
        
        public int Hour { get; set; }
        
        public int UpdateUser { get; set; }

        public int AddressId { get; set; }

        public Address StudentAddress { get; set; }

        public string ContactPerson { get; set; }

        public string EmergencyPhone { get; set; }

        public ReviewRemediation ReviewRemediation { get; set; }

        public bool IsCustomizedFRRemediation { get; set; }

        public int SMUserId { get; set; }

        public bool IsSkillsModuleTest { get; set; }

        public bool ManageAccount { get; set; }

        public int ProgramofStudyId { get; set; }

        public bool IsCaseStudyEnabled { get; set; }

        public int  QBankProgramofStudyId { get; set; }

        public int IsProctorTrackEnabled { get; set; }

        #endregion
    }
}