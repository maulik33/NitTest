using System;
using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IAdminRepository
    {
        IEnumerable<Product> GetProducts(int productId);

        IEnumerable<Category> GetCategories();

        ////IEnumerable<Category> GetCategories(int programofstudyId);

        IEnumerable<CategoryDetail> GetCategoryDetails(int categoryId, int programOfStudyIdForCategory);

        IDictionary<AppSettings, string> GetAppSettings();

        void SaveAppSetting(AppSettings settings, string value);

        IDictionary<UserType, IDictionary<Module, IList<UserAction>>> GetAuthorizationRules();

        IEnumerable<Program> SearchPrograms(int programOfStudyId, string searchText);

        void SaveProgram(Program program, int userId);

        void DeleteProgram(int programId, int userId);

        Program GetProgram(int programId);

        IEnumerable<Program> GetPrograms();

        List<Institution> GetInstitutions(int userId, string institutionIds);

        List<Institution> GetInstitutions(int userId, int programofStudyId, string institutionIds);

        void DeleteInstitution(int institutionId, int userId);

        IEnumerable<TimeZones> GetTimeZones();

        void SaveInstitution(Institution institution);

        IEnumerable<Institution> SearchInstitutes(string institutionIds);

        IEnumerable<Institution> SearchInstitutes(string SearchText, int userId);

        IEnumerable<UserDetails> SearchUserDetails(string SearchText, string status,int ProgramOfStudy);

        IEnumerable<UserDetails> SearchUnassignedUserDetails(string searchText, string status, int programofStudyId);
 
        IEnumerable<Institution> GetInstitutes(int instituteId, string instituteIds, int userId);

        IEnumerable<Cohort> SearchCohorts(string institutionIds, string searchString, int TestId, string dateFrom, string dateTo, int cohortStatus, int programofStudyId);

        IEnumerable<Cohort> GetCohorts(int CohortId, string institutionId);

        IEnumerable<Test> GetTests(int productId, int questionId, string institutionIds, bool forCMS, int programofStudy);

        IEnumerable<Test> GetTests(int productId, int questionId, string institutionIds, bool forCMS);

        int SaveCohort(Cohort cohort);

        void DeleteCohort(int CohortId, int CohortStatus, int CohortDeleteUser);

        IEnumerable<Group> GetGroupsList(int institutionIds, int cohortId);

        int SaveGroup(Group group);

        IEnumerable<Group> SearchGroups(string searchText, string institutionIds, string cohortIds);

        IEnumerable<Group> GetGroups(int groupId, string cohortIds);

        IEnumerable<Student> SearchStudent(int programOfStudyId, int institutionId, int CohortId, int GroupId, string SearchString, bool assignStudent);

        int GetInstitutionIdForCohort(int cohortId);

        int DeleteGroup(int groupId, int groupDeleteUser);

        IEnumerable<Student> GetStudents(int studentId, string searchText);

        StudentEntity GetDatesByCohortId(int CohortId);

        int SaveUser(Student student, int AdminUserId, string AdminUserName);

        void DeleteUser(int userId);

        IEnumerable<Program> GetCohortProgram(int CohortProgramId, int ProgramId, int CohortId);

        IEnumerable<Program> SearchCohortPrograms(int CohortId, string SearchText);

        void SaveCohortProgram(int CohortId, int ProgramId, int Active);

        void DeleteProductFromProgram(int ProgramId, int ProductId, int Type, int AssetGroupId);

        void AssignTestDateToCohort(CohortTestDates testDate);

        void AssignTestDateToGroup(GroupTestDates testDate);

        void AssignTestDateToStudent(StudentTestDates testDate);

        IEnumerable<Student> GetStudentsInCohortAndGroups(int institutionId, int cohortId, int groupId);

        void AssignTestToProgram(int programId, int testId, int type, int assetGroupId);

        IEnumerable<ProgramTestDates> GetTestsForProgram(int programId, string searchText);

        IEnumerable<GroupTestDates> GetTestsForGroup(int programId, int cohortId, int groupId, string searchText);

        IEnumerable<CohortTestDates> GetTestsForCohort(int programId, int cohortId, int TestId, string searchText);

        IEnumerable<StudentTestDates> GetTestsForStudent(int programId, int studentId, int cohortId, int groupId, int TestId, string searchText);

        int SaveAdmin(Admin admin);

        void DeleteAdmin(int userId);

        IEnumerable<Admin> GetAdmins(int userId, string searchString);

        void AssignInstitutionsToAdmin(Admin admin, string institutionId, int programofStudyId);

        IEnumerable<Admin> SearchAdmins(string institutionIds, string securityLevel, string searchString, int programofStudyId);

        void AssignStudentsToGroup(int groupId, string assignStudentList, string unassignedStudentList);

        void AssignStudents(string userId, int cohortId, int groupId, int institutionId);

        IEnumerable<Admin> AuthenticateUser(string userName, string userPassword);

        IEnumerable<Email> GetEmail(int emailId);

        IEnumerable<Institution> GetLocalInstitution(int userId);

        IEnumerable<Admin> GetAdmin(string institutionIds, string searchCriteria);

        IEnumerable<StudentEntity> SearchStudent(string criteria);

        IEnumerable<Admin> SearchAdmin(string criteria);

        int CreateCustomEmailToGroup(int adminId, DateTime sendTime, int emailId, bool toAdminOrStudent, string groupIds);

        int CreateCustomEmailToCohort(int adminId, DateTime sendTime, int emailId, bool toAdminOrStudent, string cohortIds);

        int CreateCustomEmailToInstitution(int adminId, DateTime sendTime, int emailId, bool toAdminOrStudent, string institutionIds);

        int CreateCustomEmailToPerson(int adminId, DateTime sendTime, int emailId, bool toAdminOrStudent, string personIds);

        IEnumerable<UserTest> GetStudentsForOverRide(int institutionId, string firstName, string lastName, string userName, string testName, bool showIncompleteOnly, string cohortIds);

        IEnumerable<UserTest> GetDeletedTestListForStudents(int institutionId, string firstName, string lastName, string userName, string testName, bool showIncompleteOnly, string cohortIds);

        void DeleteTest(int testId, string userName);

        void SaveEmail(int emailId, string title, string body);

        int UserLogIn(string username, string password);

        IEnumerable<Student> GetUserInfo(int userId);

        int GetUserID();

        int GetInstitutionIDByFacilityID(int facilityId);

        string GetPassword();

        bool GetUpdatedIntegratedUser(int userId, string classCode);

        object GetUser(string userID, int institutionId);

        void SaveHelpfulDocuments(HelpfulDocument helpfulDocument);

        IEnumerable<HelpfulDocument> GetHelpfulDocuments(int documentId, string GUID);

        IEnumerable<HelpfulDocument> SearchHelpfulDocs(string searchKeyword, bool IsLink);

        void DeleteHelpfulDoc(int userId, int docId);

        IEnumerable<Country> GetCountries(int countryId);

        IEnumerable<State> GetStates(int countryId, int stateId);

        int SaveAddress(Address address);

        Address GetAddress(int addressId);

        void SaveInstitutionContact(InstitutionContact institutionConatct);

        IEnumerable<InstitutionContact> GetInstitutionContacts(int institutionId, int contactId);

        IEnumerable<Student> SearchStudentForTest(int institutionId, int CohortId, int GroupId, string SearchString);

        void SaveAdhocGroup(AdhocGroup adhocGroup);

        void SaveAdhocGroupStudent(int studentId, int adhocGroupId);

        void UpdateStudentsADA(string students, bool Ada);

        IEnumerable<AdhocGroupTestDetails> GetAdhocGroupTests(int adhocGroupId);

        void SaveAdhocGroupTest(AdhocGroupTestDetails adhocGroupTestDetails);

        IEnumerable<Student> GetAdhocGroupStudentDetail(int adhocGroupId);

        void AssignAdhocTests(StudentTestDates studentTestDates);

        IEnumerable<Test> GetTestsByCohort(int programId, int cohortId, int TestId, string searchText);

        IDictionary<int, string> CheckSystem(bool isProductionApp);

        List<Student> GetAssignedStudentforGroup(GroupTestDates testDates);

        List<Student> GetAssignedStudentforCohort(CohortTestDates testDates);

        void ResumeTest(string userTestId, string userName);

        List<EmailMission> GetAdminEmailMission(string userIds, string institutionIds);

        List<EmailMission> GetStudentEmailMission(string userIds, string groupIds, string cohortIds, string institutionIds);

        LoginContent GetLoginContent(int contentId);

        List<Program> GetBulkProgramDetails(int testId, string type, int programOfStudyId);

        void SaveBulkModifiedPrograms(int testId, int type, string programIds);

        IEnumerable<ProgramofStudy> GetProgramofStudies();

        IEnumerable<AuditTrail> GetAuditTrail(int studentId);

        IEnumerable<AssetGroup> GetAssetGroups(int programofStudyId);

        IEnumerable<Asset> GetAssets(int assetGroupId);

        IEnumerable<CaseStudy> GetCaseAssets();

        void AssignAssetsToProgram(int programId, int testId, int bundle, int assetGroupId);

        IEnumerable<Program> GetProgramsByProgramofStudyId(int programofStudyId);
        List<Institution> GetAllInstitution();

        int GetInstitutionIdByFacilityIdOrClassCode(int facilityId, string classCode);

        IEnumerable<Student> GetStudentsByName(string userName);

        string GetUniqueUsername(string firstName, string lastName);
        
        List<LtiProvider> GetLtiProviders(int ltiProviderId);

        LtiProvider GetLtiTestSecurityProviderByName(string ltiProviderName);

        int SaveLtiProvider(LtiProvider ltiProvider);

        void ChangeLtiProviderStatus(int ltiProviderId);

        void CopyProgramTests(int originalProgramIdId, int newProgramId);

    }
}
