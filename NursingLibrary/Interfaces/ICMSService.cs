using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface ICMSService
    {
        ICacheManagement CacheManager { get; set; }

        IEnumerable<QuestionResult> SearchQuestions(QuestionCriteria searchCriteria);

        IEnumerable<Remediation> SearchRemediaton(QuestionCriteria searchCriteria);

        Test GetAVPItemByTestId(int testId);

        IEnumerable<Test> SearchAVPItems(string testName, int programOfStudyId);

        void DeleteTestById(int id);

        void SaveAVPItems(Test test);

        void UpdateTestsReleaseStatusById(int testId, string releaseStatus);

        IEnumerable<Norming> GetNormings(int testId);

        void DeleteNormingById(int id);

        void SaveNorming(Norming norming);

        IEnumerable<Product> GetListOfAllProducts();

        IEnumerable<Test> GetTests(int productId);

        IEnumerable<ClientNeedsCategory> GetClientNeedCategory(int clientNeedId);

        IEnumerable<ClientNeedsCategory> GetClientNeedCategory();

        IEnumerable<Norm> GetNorms(int testId, string chartType);

        void SaveNorm(Norm norm);

        Lippincott GetLippincottRemediationByID(int lippinCottId);

        IEnumerable<Lippincott> SearchLippincotts(string lippinCottTitle);

        void SaveLippinCott(Lippincott lippinCott);

        IEnumerable<QuestionLippincott> GetQuestionLippincottByIds(int QID, int lippinCottId);

        IEnumerable<Lippincott> GetLippincottById(int lippinCottId);

        void InsertQuestionLippinCott(int QId, int lippinCottId);

        IEnumerable<Lippincott> GetQuestionLippincotts(int lippinCottId);

        void DeleteLippinCott(int lippinCottId);

        IEnumerable<Remediation> GetRemediations();

        Question GetQuestion(int questionId, bool forEdit);

        void DeleteLippinCottQuestion(int lippinCottId, int questionId);

        Question GetQuestionByQuestionId(string questionId);

        Question GetQuestionByQId(int qId);

        Question InsertUpdateQuestion(Question question);

        Lippincott GetLippincottByRemediationID(int remediationId);

        Remediation GetRemediationById(int remediationId);

        IEnumerable<Question> GetQuestionByRemediationId(string remediationId);

        void SaveRemediation(Remediation remediation);

        void DeleteRemediation(int remediationId);

        List<Lippincott> GetLippincotts(int qId);

        Test GetTestById(int testId);

        void SaveCustomTest(Test test);

        IEnumerable<Test> SearchCustomTests(int programOfStudyId, string testName);

        IEnumerable<Test> GetCustomTests(int productId, string testName);

        void CopyCustomTest(int originalTestId, int newTestId);

        void DeleteCustomTest(int testId);

        IEnumerable<Test> GetTests(int productId, int questionId, bool forCMS, int programofStudy);

        IEnumerable<CategoryDetail> GetTestcategoriesForTestQuestions(int testId, int testType);

        void SaveTestCategory(int testId, int categoryId, int student, int admin);

        IEnumerable<TestCategory> GetTestCategories(int testId);

        IEnumerable<AnswerChoice> GetAnswers(int questionId, int actionType);

        int SaveQuestion(Question question, List<AnswerChoice> lstAnswers, int adminId, List<AnswerChoice> dbAnswerChoice);

        void DeleteQuestion(int questionId);

        void AssignQuestion(List<Test> lstTest);

        IEnumerable<Test> GetTestsForQuestion(int questionId);

        IEnumerable<Question> GetNextQuestion(int userTestId, int questionNumber, string typeOfFileId);

        IEnumerable<Question> GetPreviousQuestion(int userTestId, int questionNumber, string typeOfFileId);

        IEnumerable<Topic> GetTitles();

        IEnumerable<Lippincott> GetLippincotts(string releaseStatus);

        void ReleaseToProduction(string showQuestion, string showLippinCott, string showTest, int userId);

        void UpdateReleaseStatus(string ids, string releaseStatus, string releaseChoice);

        IEnumerable<Test> GetReleaseTests(string status);

        IEnumerable<Remediation> GetRemediationByStatus(string status);

        IEnumerable<Lippincott> GetReleaseLippinCots();

        IEnumerable<Question> GetReleaseQuestions();

        void SaveTestQuestion(TestQuestion testQuestion);

        void DeleteTestQuestions(int testId);

        IEnumerable<QuestionResult> GetQuestionListInTest(int testId);

        IList<LookupMapping> GetLookupMappings(int id, LookupType type, bool IsReverseMapping);

        Lookup GetLookup(string matchText, LookupType type);

        bool IsQuestionIdExist(string questionId);

        void SaveUploadedQuestionDetails(string GUID, string fileName, int userId);

        void SaveUploadedQuestions(List<UploadQuestionDetails> uploadQuestions, int adminId);

        void SaveUploadedRemediations(List<Remediation> remediations);

        void ReleaseLoginContent(LoginContent loginContent);

        void SaveLoginContents(LoginContent loginContent);

        LoginContent GetLoginContent(int contentId);

        void RevertLoginContent(LoginContent loginContent);

        IEnumerable<ProgramofStudy> GetProgramofStudies();

        IEnumerable<Test> GetTests(int productId, int programofStudyId);

        IDictionary<CategoryName, Category> GetCategories(int programofstudyId);
    }
}
