using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface ICMSRepository
    {
        IEnumerable<QuestionResult> SearchQuestions(QuestionCriteria searchCriteria);
        
        IEnumerable<Remediation> SearchRemediation(QuestionCriteria searchCriteria);
        
        List<Test> GetAVPItems(int testId, int productId, int testSubGroup);
        
        IEnumerable<Test> SearchAVPItems(string testName, int programOfStudyId);
        
        void SaveAVPItems(Test test);
        
        void DeleteTestById(int id);
        
        void UpdateTestsReleaseStatusById(int testId, string releaseStatus);
        
        IEnumerable<Norming> GetNormings(int testId, string testIds);
        
        void DeleteNormingById(int id);
        
        void SaveNorming(Norming norming);
        
        IEnumerable<ClientNeedsCategory> GetClientNeedCategory(int clientNeedId);
        
        IEnumerable<Norm> GetNorms(int testId, string chartType, string testIds);
        
        void SaveNorm(Norm norm);
        
        Lippincott GetLippincottRemediationByID(int lippinCottId);
        
        IEnumerable<Lippincott> SearchLippincotts(string lippinCottTitle);
        
        void SaveLippinCott(Lippincott lippinCott);
        
        IEnumerable<QuestionLippincott> GetQuestionLippincottByIds(int QID, int lippinCottId, string lippincottIds);
        
        IEnumerable<Lippincott> GetLippincottById(int lippinCottId);
        
        void InsertQuestionLippinCott(int QId, int lippinCottId);
        
        IEnumerable<Lippincott> GetQuestionLippincotts(int lippinCottId);
        
        void DeleteLippinCott(int lippinCottId);
        
        void DeleteLippinCottQuestion(int lippinCottId, int questionId);
        
        IEnumerable<Question> GetQuestions(string questionId, int qId, string remediationId, bool forEdit, string releaseStatus);
        
        Question InsertUpdateQuestion(Question question);
        
        Lippincott GetLippincottByRemediationID(int remediationId);
        
        IEnumerable<Remediation> GetRemediations(int remediationId, string releaseStatus);
        
        void SaveRemediation(Remediation remediation);
        
        void DeleteRemediation(int remediationId);
        
        IEnumerable<Lippincott> GetLippincotts(int lippinCottId, string releaseStatus);
        
        void SaveCustomTest(Test test);
        
        IEnumerable<Test> SearchCustomTests(int programOfStudyId, string testName);
        
        IEnumerable<Test> GetCustomTests(int testId, int productId, string testName);
        
        void CopyCustomTest(int originalTestId, int newTestId);
        
        void DeleteCustomTest(int testId);
        
        IEnumerable<CategoryDetail> GetTestcategoriesForTestQuestions(int testId, int testType);
        
        void SaveTestCategory(int testId, int categoryId, int student, int admin);
        
        IEnumerable<TestCategory> GetTestCategories(int testId, string testIds);
        
        IEnumerable<AnswerChoice> GetAnswers(int questionId, int actionType, string Qids);

        int SaveQuestion(Question question, int adminId);
        
        void SaveAnswer(AnswerChoice answer);
        
        void DeleteQuestion(int questionId);
        
        void AssignQuestion(Test test);
        
        IEnumerable<Test> GetTestsForQuestion(int questionId);
        
        IEnumerable<Question> GetNextQuestion(int userTestId, int questionNumber, string typeOfFileId);
        
        IEnumerable<Question> GetPreviousQuestion(int userTestId, int questionNumber, string typeOfFileId);
        
        IEnumerable<Topic> GetTitles();
        
        IEnumerable<TestQuestion> GetTestQuestions(int testId, string testIds);

        IEnumerable<TestQuestion> GetProdTestQuestions(int testId, string testIds);
        
        void ReleaseRemediation(Remediation remediation);
        
        void ReleaseLippincott(Lippincott lippincot);
        
        void ReleaseQuestionLippincott(QuestionLippincott questionLippincot);
        
        void ReleaseNorm(Norm norm);
        
        void ReleaseNorming(Norming norming);
        
        void ReleaseTestCategory(TestCategory testCategory);
        
        void ReleaseTestQuestion(TestQuestion testQuestion);
        
        void ReleaseTests(Test test);

        void ReleaseQuestions(Question questions, int userId);
        
        void ReleaseAnswerChoice(AnswerChoice answerChoice);
        
        void DeleteTestDependentRows(string testIds);
        
        void DeleteReleaseQuestionLippinCott(string lippincottIds);
        
        IEnumerable<Question> GetReleaseQuestions();
        
        IEnumerable<Lippincott> GetReleaseLippinCots();
        
        IEnumerable<Test> GetReleaseTests(string status);
        
        void UpdateReleaseStatus(string ids, string releaseStatus, string releaseChoice);
        
        List<Lippincott> GetLippincotts(int qId);
        
        void SaveTestQuestion(TestQuestion testQuestion);
        
        void DeleteTestQuestions(int testId);
        
        IEnumerable<QuestionResult> GetQuestionListInTest(int testId);

        void SyncQuestionLookup(Question question);

        CustomFRLookupData GetCustomFRLookupMappings(int questionId,int programofStudyId);

        IList<LookupMapping> GetLookupMappings(string ids, LookupType type, bool IsReverseMapping);

        Lookup GetLookup(string matchText, LookupType type);

        Lookup GetLookup(int id);
        
        Lookup GetLookup(int originalId, LookupType type);

        int InsertLookup(int originalId, LookupType type, string displayText, int sortOrder);

        int InsertLookupMapping(LookupType type, int lookupId, int mappedTo);

        void DeleteLookup(int id);

        void DeleteLookupMapping(int mappingId);

        bool IsQuestionIdExist(string questionId);

        void SaveUploadedQuestionDetails(string GUID, string fileName, int userId);

        void SaveUploadedRemediation(Remediation remediation);

        void ReleaseLoginContent(LoginContent loginContent);

        void SaveLoginContents(LoginContent loginContent);

        void RevertLoginContent(LoginContent loginContent);

        void UpdateQuestionLog(Question question);

        void DeleteAnswerChoices(string AnswerIds);

        void DeleteQuestionMappingByQId(int QId);
    }
}
