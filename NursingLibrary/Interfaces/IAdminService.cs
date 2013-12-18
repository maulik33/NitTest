using System;
using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IAdminService
    {
        ICacheManagement CacheManager { get; set; }

        IDictionary<CategoryName, Category> GetCategories();

        IDictionary<CategoryName, Category> GetCategories(int programofstudyId);

        IDictionary<AppSettings, string> GetAppSettings();

        void SaveAppSetting(AppSettings id, string value);

        IDictionary<UserType, IDictionary<Module, IList<UserAction>>> GetAuthorizationRules();

        IEnumerable<Program> SearchPrograms(int programOfStudyId, string searchText);

        void SaveProgram(Program program);

        void DeleteProgram(int programId);

        void DeleteProductFromProgram(int ProgramId, int ProductId, int Type, int AssetGroupId);

        Program GetProgram(int programId);

        IEnumerable<TimeZones> GetTimeZones();

        IEnumerable<Program> GetPrograms();

        void SaveInstitution(Institution institution);

        void DeleteInstitution(int institutionId, int userId);

        List<Institution> GetInstitutions(int userId, string institutionIds);

        List<Institution> GetInstitutions(int userId, int programofStudyId, string institutionIds);

        int SaveGroup(Group group);

        int DeleteGroup(int groupId);

        Group GetGroup(int groupId);

        IEnumerable<Group> GetGroups(int groupId, string cohortId);

        IEnumerable<Group> SearchGroups(string searchText, string institutionIds, string cohortIds);

        IEnumerable<Institution> SearchInstitutes(string institutionIds);

        IEnumerable<Institution> SearchInstitution(string searchText, int userId);

        IEnumerable<Institution> SearchInstitution(string searchText, int userId, string status, string ProgramofStudy);

        IEnumerable<UserDetails> SearchUserDetails(string searchText, string status, int programofStudyId);

     

        IEnumerable<Institution> GetInstitutions(int instituteId, string instituteIds, int userId);

        Institution GetInstitution(int instituteId, int userId);

        List<Institution> GetAllInstitutions();

        IEnumerable<Cohort> SearchCohorts(string institutionIds, string searchString, int TestId, string dateFrom, string dateTo, int cohortStatus, int programofStudyId);

        IEnumerable<Cohort> GetCohorts(int cohortId, string institutionId);

        Cohort GetCohort(int cohortId);

        void DeleteCohort(int CohortId, int CohortStatus, int CohortDeleteUser);

        int SaveCohort(Cohort cohort);

        IEnumerable<Product> GetProducts();

        Product GetProduct(int productId);

        IEnumerable<Test> GetTests(int productId, string institutionIds);

        IEnumerable<Test> GetTests(int productId, int questionId, string institutionIds, bool forCMS, int programofStudy);

        int GetInstitutionIdForCohort(int cohortId);

        IEnumerable<Student> SearchStudents(int programOfStudyId, int institutionId, int cohortId, int groupId, string searchString);

        IEnumerable<Student> SearchStudents(int programOfStudyId, int institutionId, int cohortId, int groupId, string searchString, bool assignStudent);

        IEnumerable<Student> GetStudents(int studentId, int institutionId, int cohortId, int groupId);

        IEnumerable<Student> GetStudentsForGroups(int cohortId, int groupId);

        IEnumerable<Student> GetStudentsForCohorts(int institutionId, int cohortId, int groupId);

        Student GetStudent(int studentId);

        StudentEntity GetDatesForCohortId(int cohortId);

        int SaveUser(Student student, int AdminUserId, string AdminUserName);

        void DeleteUser(int userId);

        Program GetCohortProgram(int cohortProgramId, int programId, int cohortId);

        IEnumerable<Program> SearchCohortPrograms(int cohortId, string searchText);

        void SaveCohortProgram(int cohortId, int programId, int active);

        string AssignTestDateToCohort(List<CohortTestDates> testDate, UserType utype);

        void AssignTestToProgram(List<ProgramTestDates> lstTestDates);

        string AssignTestToGroup(List<GroupTestDates> lstTestDates, UserType utype);

        void AssignTestToStudent(List<StudentTestDates> lstTestDates);

        IEnumerable<ProgramTestDates> GetTestsForProgram(int programId, string searchText);

        IEnumerable<GroupTestDates> GetTestsForGroup(int programId, int cohortId, int groupId, string searchText);

        IEnumerable<CohortTestDates> GetTestsForCohort(int programId, int cohortId, int TestId, string searchText);

        IEnumerable<StudentTestDates> GetTestsForStudent(int programId, int studentId, int cohortId, int groupId, int TestId, string searchText);

        int SaveAdmin(Admin admin);

        void DeleteAdmin(int userId);

        Admin GetAdmin(int userId);

        IEnumerable<Admin> SearchAdmins(string institutionIds, string securityLevel, string searchString, int programofStudyId);

        void AssignInstitutionsToAdmin(List<Admin> admin, string institutionId, int programofStudyId);

        void AssignStudentsToGroup(int groupId, string assignStudentList, string unassignedStudentList);

        void AssignStudents(string userId, int cohortId, int groupId, int institutionId);

        UserAuthentication AuthenticateUser(string userName, string userPassword);

        IEnumerable<Email> GetEmail();

        IEnumerable<Email> GetEmail(int emailId);

        IEnumerable<Institution> GetLocalInstitution(int userId);

        IEnumerable<Admin> GetAdmin(string institutionIds);

        IEnumerable<Admin> GetAdmin(string institutionIds, string searchCriteria);

        IEnumerable<StudentEntity> SearchStudent(string criteria);

        IEnumerable<Admin> SearchAdmin(string criteria);

        int CreateCustomEmailToPerson(int adminId, DateTime sendTime, int emailId, bool toAdminOrStudent, string personIds);

        int CreateCustomEmailToCohort(int adminId, DateTime sendTime, int emailId, bool toAdminOrStudent, string cohortIds);

        int CreateCustomEmailToInstitution(int adminId, DateTime sendTime, int emailId, bool toAdminOrStudent, string institutionIds);

        int CreateCustomEmailToGroup(int adminId, DateTime sendTime, int emailId, bool toAdminOrStudent, string groupIds);

        IEnumerable<UserTest> GetStudentsForOverRide(int institutionId, string firstName, string lastName, string userName, string testName, bool showIncompleteOnly, string cohortIds);

        IEnumerable<UserTest> GetDeletedTestListForStudents(int institutionId, string firstName, string lastName, string userName, string testName, bool showIncompleteOnly, string cohortIds);

        void DeleteTest(int testId, string userName);

        void SaveEmail(int emailId, string title, string body);

        int UserLogIn(string username, string password);

        IEnumerable<Student> GetUserInfo(int UserID);

        int GetUserID();

        int GetInstitutionIDByFacilityID(int FacilityID);

        object GetUser(string UserID, int institutionId = 0);

        string GetPassword();

        bool GetUpdatedIntegratedUser(int UserID, string ClassCode);

        void SaveHelpfulDocuments(HelpfulDocument helpfulDocument);

        HelpfulDocument GetHelpfulDocument(int documentId);

        IEnumerable<HelpfulDocument> SearchHelpfulDocs(string searchKeyword, bool IsLink);

        void DeleteHelpfulDoc(int userId, int docId);

        IEnumerable<Country> GetCountries(int countryId);

        IEnumerable<State> GetStates(int countryId, int stateId);

        int SaveAddress(Address address);

        Address GetAddress(int addressId);

        List<Institution> GetAllInstitutions(int userId, string institutionIds);

        void SaveInstitutionContact(InstitutionContact institutionConatct);

        IEnumerable<InstitutionContact> GetInstitutionContacts(int institutionId);

        InstitutionContact GetInstitutionContactsByContactId(int contactId);

        IEnumerable<Student> SearchStudentsForTest(int institutionId, int cohortId, int groupId, string searchString);

        void SaveAdhocGroup(List<int> studentIds, AdhocGroup adhocGroup);

        void SaveAdaAdhocGroup(List<int> studentIds, AdhocGroup adaAdhocGroup);

        void SaveAdhocGroupTest(AdhocGroupTestDetails adhocGroupTestDetail);

        IEnumerable<AdhocGroupTestDetails> GetAdhocGroupTestDetail(int adhocGroupId);

        IEnumerable<Test> GetTestsByCohort(int programId, int cohortId, int TestId, string searchText);

        IDictionary<int, string> CheckSystem(bool isProductionApp);

        void ResumeTest(string userTestId, string userName);

        IEnumerable<Cohort> GetCohortsByInstitutionIds(string institutionId);

        List<EmailMission> GetAdminEmailMission(string userIds, string institutionIds);

        List<EmailMission> GetStudentEmailMission(string userIds, string groupIds, string cohortIds, string institutionIds);

        LoginContent GetLoginContent(int contentId);

        List<Program> GetBulkProgramDetails(int testId, string type, int programOfStudyId);

        void SaveBulkModifiedPrograms(int testId, int type, string programIds);

        IEnumerable<ProgramofStudy> GetProgramofStudies();

        IEnumerable<Test> GetTests(int productId, int programofStudyId);

        IEnumerable<AuditTrail> GetAuditTrailData(int studentId);

        IEnumerable<AssetGroup> GetAssetGroups(int programofStudyId);

        IEnumerable<Asset> GetAssets(int assetGroupId);

        IEnumerable<CaseStudy> GetCaseAssets();

        void AssignAssetsToProgram(List<ProgramTestDates> assetList);

        IEnumerable<Program> GetProgramsByProgramofStudyId(int programofStudyId);

        int GetInstitutionIdByFacilityIdOrClassCode(int facilityId, string classCode);

        bool IsDuplicateUserName(string userName, int userId, bool IsAdmin);

        string GetUniqueUsername(string firstName, string lastName);

        List<LtiProvider> GetLtiProviders(int ltiProviderId = 0);

        int SaveLtiProvider(LtiProvider ltiProvider);

        void ChangeLtiProviderStatus(int ltiProviderId);

        int SaveUser(Student student);

        void CopyProgram(Program program);

        LtiProvider GetLtiTestSecurityProviderByName(string ltiProviderName);
    }
}
