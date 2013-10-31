using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface ITestRepository
    {
        bool TestExists(int userId, int productId, int testSubGroup, int type, int timeOffset);

        IEnumerable<FinishedTest> GetFinishedTests(int userId, int productId, int timeOffset);
        
        IEnumerable<UserTest> GetSuspendedTests(int userId, int productId, int testSubGroup, int timeOffset);
        
        IEnumerable<Test> GetUntakenTests(int userId, int productId, int testSubGroup, int timeOffset);
        
        IEnumerable<Test> GetAllTests(int userId, int productId, int bundleId, int testSubGroup, int timeOffset);
        
        IEnumerable<UserTest> GetTakenTests(int userId, int productId, int testSubGroup, int timeOffset);
        
        IEnumerable<UserTest> GetUserTests(int userId, int productId, int testSubGroup, int timeOffset, int testStatus);
                
        bool ContinueTest(int userId, int testId);
        
        TestType GetTestType(int testId);
        
        void UpdateTestStatus(int userTestId, int testStatus);

        void UpdateTestStatusOnExpiry(int userTestId, int testStatus, int timeRemaining, int questionId);

        string GetTestName(int testId);
        
        IEnumerable<ProductTest> LoadProductTest();
        
        void UpdateEndTest(UserTest userTest);
        
        IEnumerable<UserTest> GetUserTestByID(int userTestId);
        
        IEnumerable<UserTest> GetTestByProductUser(int userId, int productId, int timeOffset);

        IEnumerable<Category> GetStudentTestCharacteristics(int testId);
        
        double GetProbability(int testId, int correctAnswers);
        
        bool CheckProbabilityExists(int testId);
        
        double GetPercentileRank(int testId, int correctAnswers);
        
        bool CheckPercentileRankExists(int testId);
        
        IEnumerable<FinishedTest> GetTestsNCLEXInfoForTheUser(int UserID, int ProductID, int offset);
        
        IEnumerable<UserQuestion> GetTestQuestionsForUserId(int userTestId, string typeOfFileId);
        
        IEnumerable<ProgramResults> GetQBankTestPerformancyByChartType(int UserID, int ProductID, int charttype, int overViewOrDetails); // added by sudhin for QBank_P
        
        IEnumerable<UserTest> GetTestsByUserProductSubGroup(int userId, int productId, int testSubGroup, int timeOffset);
        
        int GetTestQuestionsCount(int testId, string fileType);
        
        IList<UserTest> GetUserTests(int userId, int testId);

        IEnumerable<UserTest> GetCustomizedFRTests(int userId, int timeOffSet);

        IEnumerable<UserTest> GetSkillsModulesAvailableQuizzes(int userId, int timeOffSet, int productId);

        int GetCustomFRTestQuestionCount(int userTestId);

        int GetQuestionCount(int userTestId);
    }
}
