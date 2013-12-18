using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IStudentRepository
    {
        Student GetStudent(string userName, string password);

        Student GetStudent(int userId);

        void SaveUserSession(int userId, string sessionId);

        IEnumerable<CaseStudy> GetCaseStudies();

        IEnumerable<AvpContent> GetAvpContent(int userId, int productId, int testSubGroup);

        bool ChangePassword(int userId, string newPassword);

        ProgramResults GetProgramResults(int userTestId, int chartType);

        IEnumerable<ProgramResults> GetProgramResultsByNorm(int userTestId, int testId);

        IEnumerable<ClientNeedsCategory> LoadClientNeedsCategoryInfo(int userId,int programofStudyId);

        IEnumerable<ClientNeeds> LoadClientNeeds(int programofStudyId);

        int CreateSkillsModulesDetails(int TestId, int UserId);

        int GetNumberOfCategory();

        CaseModuleScore LoadCaseModuleScore();

        CaseSubCategory LoadCaseSubCategory();

        bool InsertSubCategory(CaseSubCategory subCategory);

        bool InsertModuleScore(CaseModuleScore moduleScore);

        bool CheckExistCaseModuleStudent(int CID, int MID, string SID);

        string GetRemediationExplainationByID(int remediationId);

        string GetSession(int userId);

        IEnumerable<Systems> GetCategories(string typeIds);

        IEnumerable<Topic> GetTopics(string categoryIds, bool isTest,int programofstudyId);

        void CreateFRQBankRemediation(ReviewRemediation ReviewRem, string systems, string topics, int programofstudyId);

        IEnumerable<ReviewRemediation> GetRemediationsForTheUser(int studentId);

        int GetAvailableRemediations(string systems, string topics,int programofstudyId);

        IEnumerable<ReviewRemediation> GetRemediationExplainationByReviewID(int revRemID);

        IEnumerable<Lippincott> GetLippincottForReviewRemediation(int revRemQID);

        void UpdateReviewRemediation(int reviewId, int time);

        void DeleteRemediations(int remediationReviewId);

        IEnumerable<ReviewRemediation> GetNextPrevRemediation(int Id, int RemNumber, string action);

        void UpdateTotalRemediatedTime(int remediationReviewId);

        UserTest CreateFRQBankRepeatTest(int userTestId);

        void UpdateAltTabClick(int userTestId, int QId, bool IsAltTabClicked);

        IEnumerable<SMUserVideoTransaction> GetSkillsModuleVideos(int skillsModuleUserId);

        void UpdateSkillModuleStatus(int SMUserVideoId);

        IEnumerable<SMTest> GetSkillsModuleTests(int SMUserId, int TestId);

        void ResetSkillModuleStatus(int SMUserId);

        void UpdateSkillModuleVideoStatus(int SMUserVideoId);

        Student GetStudentInfoByUserNameEmail(string userName, string email);

        int GetSkillModuleUserId(int testId, int userId);

        int GetOriginalSMTestId(int newtestId, int userId);

        IEnumerable<SMTest> GetSkillsModuleTestsByUserId(int SMUserId);

        void UpdateScrambledAnswerChoice(string scrambledanswerchoice, int usertestId, int questionId);

        string GetScrambledAnswerChoice(int usertestId, int questionId);

        IEnumerable<AssetDetail> GetDashBoardLinks(int programId);

        IEnumerable<AssetGroup> GetAssetGroupByProgramId(int programId);

        IEnumerable<Test> GetQBankTest(int userId, int productId, int testSubGroupId, int timeOffSet);

        string GetUserDetailsById(int userId);
    }
}
