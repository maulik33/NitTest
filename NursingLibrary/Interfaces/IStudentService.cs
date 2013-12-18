using System;
using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IStudentService
    {
        Response<Student> AuthenticateStudent(AuthenticateRequest request);

        Response<String> GetSessionId(Request request);

        CollectionResponse<AvpContent> GetAvpContents(Request request);

        CollectionResponse<CaseStudy> GetCaseStudies();

        CollectionResponse<Product> GetAllProducts();

        CollectionResponse<FinishedTest> GetFinishedTests(Request request);

        BoolResponse DoesTestExists(Request request);

        CollectionResponse<Test> GetUntakenTests(Request request);

        CollectionResponse<Test> GetAllTests(Request request);

        CollectionResponse<UserTest> GetSuspendedTests(Request request);

        CollectionResponse<UserTest> GetTakenTests(Request request);

        BoolResponse ContinueTest(Request request);

        BoolResponse ChangePassword(AuthenticateRequest request);

        Response<TestType> GetTestType(Request request);

        void UpdateTestStatus(Request request);

        void UpdateTestStatusOnExpiry(Request request);

        Response<string> GetTestName(Request request);

        CollectionResponse<ProductTest> GetAllProductTests();

        Response<UserTest> CreateTest(Request request);

        Response<UserTest> CreateQBankTest(Request request);

        CollectionResponse<Question> GetQuestionByTest(Request request);

        CollectionResponse<Question> GetQuestionsByUserTest(Request request);

        CollectionResponse<Lippincott> GetLippincottAssignedInQuestion(Request request);

        CollectionResponse<UserAnswer> GetAnswersForQuestion(Request request);

        Response<QuestionExhibit> GetQuestionExhibitByID(Request request);

        CollectionResponse<UserAnswer> GetUserAnswerByID(Request request);

        CollectionResponse<UserAnswer> GetHotSpotAnswerByID(Request request);

        CollectionResponse<Question> GetPrevNextQuestions(Request request);

        CollectionResponse<Question> GetPrevNextQuestionInTest(Request request);

        void UpdateEndTest(UserTest request);

        BoolResponse IsTest74Question(Request request);

        BoolResponse SaveQuestionInTheUserTest(SaveQuestionRequest request);

        CollectionResponse<UserTest> GetUserTestByID(Request request);

        void UpdateQuestionRemediation(Request request);

        void UpdateQuestionExplanation(Request request);

        CollectionResponse<UserTest> GetTestByProductUser(Request request);

        Response<ProgramResults> GetProgramResults(Request request);

        CollectionResponse<Category> GetStudentTestCharacteristics(Request request);

        Response<int> GetProbability(Request request);

        BoolResponse CheckProbabilityExists(Request request);

        Response<int> GetPercentileRank(Request request);

        BoolResponse CheckPercentileRankExists(Request request);

        CollectionResponse<ProgramResults> GetProgramResultsByNorm(Request request);

        CollectionResponse<FinishedTest> GetTestsNCLEXInfoForTheUser(Request request);

        CollectionResponse<ProgramResults> GetQBankTestPerformanceByProductIDChartType(Request request);

        CollectionResponse<ClientNeeds> GetListOfAllClientNeeds(int programofStudyId);

        CollectionResponse<ClientNeedsCategory> GetListOfAllClientNeedsCategoryInfo(Request request);

        Response<int> GetNumberOfCategory();

        CollectionResponse<UserQuestion> GetTestQuestionsForUserId(Request request);

        Response<string> GetQBankGraphXML(Request request);

        Response<Dictionary<CategoryName, Category>> GetCategories();

        Response<CaseModuleScore> GetCaseModuleScore();

        Response<CaseSubCategory> GetCaseSubCategory();

        BoolResponse InsertModuleScore(CaseModuleScore moduleScore);

        BoolResponse InsertSubCategory(CaseSubCategory subCategory);

        BoolResponse CheckExistCaseModuleStudent(int CID, int MID, string SID);

        Response<int> GetTestQuestionsCount(Request request);

        Response<string> GetRemediationExplainationByID(int remediationId);

        CollectionResponse<UserTest> GetUserTests(Request request);

        IEnumerable<Systems> GetCategories(string typeIds);

        IEnumerable<Topic> GetTopics(string categoryIds, bool isTest,int programofstudyId);

        void CreateFRQBankRemediation(ReviewRemediation ReviewRem, string systems, string topics, int programofstudyId);

        int CreateSkillsModulesDetails(int TestId, int UserId);

        IEnumerable<ReviewRemediation> GetRemediationsForTheUser(int studentId);

        int GetAvailableRemediations(string systems, string topics, int programofstudyId);

        ReviewRemediation GetRemediationExplainationByReviewID(int revRemID);

        IEnumerable<Lippincott> GetLippincottForReviewRemediation(int revRemQID);

        void UpdateReviewRemediation(int reviewId, int time);

        void DeleteRemediations(int remediationsReviewId);

        IEnumerable<ReviewRemediation> GetNextPrevRemediation(int Id, int RemNumber, string action);

        void UpdateTotalRemediatedTime(int remediationReviewId);

        Response<UserTest> CreateFRQBankTest(Request request);

        int GetCFRAvailableQuestions(int userId, string categoryIds, string topicIds, bool isTest, int reUseMode, int programofstudyId);

        UserTest CreateFRQBankRepeatTest(int userTestId);

        IEnumerable<UserTest> GetCustomizedFRTests(int userId, int timeOffSet);

        IEnumerable<UserTest> GetSkillsModulesAvailableQuizzes(int userId, int timeOffSet, int productId);

        int GetCustomFRTestQuestionCount(int userTestId);

        int GetQuestionCount(int userTestId);

        void UpdateAltTabClick(int userTestId, int QId, bool IsAltTabClicked);

        IEnumerable<SMUserVideoTransaction> GetSkillsModuleVideos(int skillsModuleUserId);

        Response<UserTest> CreateSkillsModuleTest(Request request);

        void UpdateSkillModuleStatus(int SMUserVideoId);

        IEnumerable<SMTest> GetSkillsModuleTests(int SMUserId, int TestId);

        void ResetSkillModuleStatus(int SMUserId);

        void UpdateSkillModuleVideoStatus(int SMUserVideoId);

        Student GetStudentInfoByUserNameEmail(string userName, string email);

        int GetSkillModuleUserId(int testId, int userId);

        int GetOriginalSMTestId(int newtestId, int userId);

        IEnumerable<SMTest> GetSkillsModuleTestsByUserId(int SMUserId);

        LoginContent GetLoginContent(int contentId);

        void UpdateScrambledAnswerChoice(string scrambledanswerchoice, int usertestId, int questionId);

        string GetScrambledAnswerChoice(int usertestId, int questionId);

        IEnumerable<AnswerChoice> GetAnswers(int questionId, int actionType);

        IEnumerable<AssetDetail> GetDashBoardLinks(int programId);

        IEnumerable<AssetGroup> GetAssetGroupByProgramId(int programId);

        IEnumerable<Test> GetQBankTest(int userId, int productId, int testSubGroupId, int timeOffSet);

        string GetUserDetailsById(int userId);

        LtiProvider GetLtiTestSecurityProviderByName(string ltiProviderName);
    }
}