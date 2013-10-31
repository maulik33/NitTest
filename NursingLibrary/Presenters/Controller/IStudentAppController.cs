using System;
using System.Collections.Generic;
using NursingLibrary.Common;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Presenters.Controller
{
    public interface IStudentAppController : ITestAppController, IQuestionAppController
    {
        Student Student { get; }

        ReviewRemediation ReviewRemediation { get; }

        IEnumerable<AvpContent> GetAvpContent(int productid, int testSubgroup);

        void Intialize();

        void SaveSession();

        void StartTrace(int userId, string userName, string environment);

        TraceToken GetTraceToken();

        void SaveSessionInfo(ExecutionContext context);

        void ShowPage(PageDirectory page, string fragment, string query);

        void LogIn(string userName, string password, string sessionId);

        IEnumerable<CaseStudy> GetCaseStudies();

        IEnumerable<Product> GetAllProducts();

        bool ChangePassword(string newPassword);

        ProgramResults GetProgramResults(int chartType);

        IEnumerable<ClientNeeds> GetListOfAllClientNeeds(int programofStudyId);

        IEnumerable<ClientNeedsCategory> GetListOfAllClientNeedsCategoryInfo(int UserID);

        int GetNumberOfCategory();

        Dictionary<CategoryName, Category> GetAllCategories();

        string GetCategoryDescription(CategoryName category, int categoryDetailId);

        CaseModuleScore GetCaseModuleScore();

        CaseSubCategory GetCaseSubCategory();

        bool InsertModuleScore(CaseModuleScore moduleScore);

        bool InsertSubCategory(CaseSubCategory subCategory);

        bool CheckExistCaseModuleStudent(int CID, int MID, string SID);

        string GetRemediationExplainationByID(int remediationId);

        IEnumerable<Systems> GetCategories(string typeIds);

        IEnumerable<Topic> GetTopics(string categoryIds, bool isTest);

        void CreateFRQBankRemediation(ReviewRemediation ReviewRem, string systems, string topics);

        int CreateSkillsModulesDetails(int TestId, int UserId);

        IEnumerable<ReviewRemediation> GetRemediationsForTheUser(int studentId);

        int GetAvailableRemediations(string SystemID, string TopicID);

        ReviewRemediation GetRemediationExplainationByReviewID(int revRemID);

        IEnumerable<Lippincott> GetLippincottForReviewRemediation(int revRemQID);

        void UpdateReviewRemediation(int reviewId, int time);

        void DeleteRemediations(int remediationReviewId);

        IEnumerable<ReviewRemediation> GetNextPrevRemediation(int Id, int RemNumber, string action);

        void UpdateTotalRemediatedTime(int remediationReviewId);

        int GetCFRAvailableQuestions(int userId, string categoryIds, string topicIds, bool isTest, int reUseMode);

        UserTest CreateFRQBankRepeatTest(int userTestId);

        void UpdateSkillModuleStatus(int SMUserVideoId);

        IEnumerable<SMTest> GetSkillsModuleTests(int SMUserId, int TestId);

        void ResetSkillModuleStatus(int SMUserId);

        void UpdateSkillModuleVideoStatus(int SMUserVideoId);

        Student GetStudentInfoByUserNameEmail(string userName, string email);

        int GetSkillModuleUserId(int testId, int userId);

        int GetOriginalSMTestId(int newtestId, int userId);

        IEnumerable<AssetDetail> GetDashBoardLinks(int programId);

        IEnumerable<AssetGroup> GetAssetGroupByProgramId(int programId);

        IEnumerable<Test> GetQBankTest(int userId, int productId, int testSubGroupId, int timeOffSet);
    }

    public interface ITestAppController
    {
        IEnumerable<FinishedTest> GetFinishedTests();

        bool DoesTestExists(int productId, int testSubgroup, int type);

        IEnumerable<Test> GetUnTakenTests();

        IEnumerable<Test> GetUnTakenTestsforSkillsModules();

        IEnumerable<Test> GetAllTests(int bundleId);

        IEnumerable<UserTest> GetSuspendedTests();

        IEnumerable<UserTest> GetTakenTests();

        bool ContinueTest();

        TestType GetTestType();

        void UpdateTestStatus();

        void UpdateTestStatusOnExpiry();

        string GetTestName();

        IEnumerable<ProductTest> GetAllProductTests();

        bool UpdateEndTest(UserTest test);

        IEnumerable<UserTest> GetTestByProductUser();

        IEnumerable<Category> GetStudentTestCharacteristics();

        int GetProbability(int correctAnswers);

        bool CheckProbabilityExists();

        int GetPercentileRank(int correctAnswers);

        bool CheckPercentileRankExists();

        IEnumerable<ProgramResults> GetProgramResultsByNorm();

        IEnumerable<FinishedTest> GetTestsNCLEXInfoForTheUser();

        IEnumerable<ProgramResults> GetQBankTestPerformanceByProductIDChartType(int chartType, int overViewOrDetails);

        IEnumerable<UserQuestion> GetTestQuestionsForUser();

        string GetQBankGraphData(int aType);

        IEnumerable<UserTest> GetUserTestByID();

        int GetTestQuestionsCount();

        IEnumerable<UserTest> GetUserTests(int userId, int testId);

        IEnumerable<UserTest> GetCustomizedFRTests(int userId, int timeOffSet);

        IEnumerable<UserTest> GetSkillsModulesAvailableQuizzes(int userId, int timeOffSet, int productId);

        int GetCustomFRTestQuestionCount(int userTestId);

        int GetQuestionCount(int userTestId);

        LtiProvider GetLtiTestSecurityProviderByName(string ltiProviderName);
    }

    public interface IQuestionAppController
    {
        UserTest CreateTest();

        UserTest CreateQBankTest(int tutorMode, int reUserMode, int correct, string options);

        IEnumerable<Question> GetQuestionByTest();

        IEnumerable<Question> GetQuestionByUserTest();

        IEnumerable<Lippincott> GetLippincottAssignedInQuestion();

        IEnumerable<UserAnswer> GetAnswersForQuestion();

        QuestionExhibit GetQuestionExhibitByID();

        IEnumerable<UserAnswer> GetUserAnswerByID();

        IEnumerable<UserAnswer> GetHotSpotAnswerByID();

        IEnumerable<Question> GetPrevNextQuestions(int questionNumber, QuestionFileType ftype, bool inCorrectOnly);

        IEnumerable<Question> GetPrevNextQuestionInTest(int questionNumber, QuestionFileType ftype);

        bool IsTest74Question();

        bool SaveQuestionInTheUserTest(Question question, IList<UserAnswer> userAnswers, UserTest userTest);

        void UpdateQuestionExplanation(int timerValue);

        void UpdateQuestionRemediation(int timerValue);

        UserTest CreateFRQBankTest(int tutorMode, int reUserMode, int correct, string systemIds, string topics, string name);

        void UpdateAltTabClick(int userTestId, int QId, bool IsAltTabClicked);

        IEnumerable<SMUserVideoTransaction> GetSkillsModuleVideos(int skillsModuleUserId);

        UserTest CreateSkillsModuleTest(int testId);

        IEnumerable<SMTest> GetSkillsModuleTestsByUserId(int SMUserId);

        LoginContent GetLoginContent(int contentId);

        void UpdateScrambledAnswerChoice(string scrambledanswerchoice, int usertestId, int questionId);

        string GetScrambledAnswerChoice(int usertestId, int questionId);

        IEnumerable<AnswerChoice> GetAnswers(int questionId, int actionType);

        string GetUserDetailsById(int userId);
    }
}
