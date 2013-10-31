using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Common;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;

namespace NursingLibrary.Services
{
    public class AdminService : IAdminService
    {
        #region Fields

        private readonly IAdminRepository _adminRepository;
        private readonly IUnitOfWork _unitOfWork;
        private const int _userId = 1;

        #endregion Fields

        #region Constructors

        public AdminService(IUnitOfWork unitOfWork, IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
            _unitOfWork = unitOfWork;
        }

        #endregion Constructors

        #region Properties

        public ICacheManagement CacheManager { get; set; }

        #endregion Properties

        #region IAdminService Members

        public IDictionary<CategoryName, Category> GetCategories()
        {
            return CacheManager.GetNotRemovableItem(Constants.CACHE_KEY_CATEGORY, () => GetAllCategories());
        }

        public IDictionary<CategoryName, Category> GetCategories(int programofstudyId)
        {
            IDictionary<CategoryName, Category> categories = GetCategories();
            IDictionary<CategoryName, Category> filteredCategory = new Dictionary<CategoryName, Category>();
            foreach (KeyValuePair<CategoryName, Category> c in categories)
            {
                if (c.Value.ProgramofStudyId == programofstudyId)
                {
                    Category nc = new Category();
                    nc.CategoryID = c.Value.CategoryID;
                    nc.OrderNumber = c.Value.OrderNumber;
                    nc.TableName = c.Value.TableName;
                    IDictionary<int, CategoryDetail> categoryDetails = c.Value.Details.Where(r => r.Value.ProgramofStudy == programofstudyId).ToDictionary(r => r.Key, r => r.Value);
                    nc.Details = categoryDetails;
                    filteredCategory.Add(c.Key, nc);
                }
            }

            return filteredCategory;
        }

        public IEnumerable<Product> GetProducts()
        {
            return CacheManager.Get(Constants.CACHE_KEY_PRODUCTS, () => GetAllProducts());
        }

        public IDictionary<AppSettings, string> GetAppSettings()
        {
            return _adminRepository.GetAppSettings();
        }

        public void SaveAppSetting(AppSettings setting, string value)
        {
            _adminRepository.SaveAppSetting(setting, value);
        }

        public IDictionary<UserType, IDictionary<Module, IList<UserAction>>> GetAuthorizationRules()
        {
            return GetAllAuthorizationRules();
        }

        public List<Institution> GetInstitutions(int userId, string institutionIds)
        {
            return _adminRepository.GetInstitutions(userId, institutionIds)
            .Where(s => s.Status.Equals("1") && s.InstitutionName.Trim().Length > 0)
            .ToList().OrderBy(i => i.InstitutionName).ToList();
        }

        public List<Institution> GetInstitutions(int userId, int programofStudyId, string institutionIds)
        {
            return _adminRepository.GetInstitutions(userId, programofStudyId, institutionIds)
            .Where(s => s.Status.Equals("1") && s.InstitutionName.Trim().Length > 0)
            .ToList().OrderBy(i => i.InstitutionName).ToList();
        }
        public List<Institution> GetAllInstitutions(int userId, string institutionIds)
        {
            return _adminRepository.GetInstitutions(userId, institutionIds)
            .Where(s => s.InstitutionName.Trim().Length > 0)
            .ToList().OrderBy(i => i.InstitutionName).ToList();
        }

        public IEnumerable<Program> SearchPrograms(int programOfStudyId, string searchText)
        {
            return _adminRepository.SearchPrograms(programOfStudyId, searchText);
        }

        public void SaveProgram(Program program)
        {
            ProgramService programService = new ProgramService();
            programService.Validate(program);

            _adminRepository.SaveProgram(program, _userId);
        }

        public void DeleteProgram(int programId)
        {
            _adminRepository.DeleteProgram(programId, _userId);
        }

        public Program GetProgram(int programId)
        {
            return _adminRepository.GetProgram(programId);
        }

        public IEnumerable<TimeZones> GetTimeZones()
        {
            return _adminRepository.GetTimeZones();
        }

        public IEnumerable<Program> GetPrograms()
        {
            return _adminRepository.GetPrograms();
        }

        public void SaveInstitution(Institution institution)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                var _addressId = _adminRepository.SaveAddress(institution.InstitutionAddress);
                institution.AddressId = _addressId;
                _adminRepository.SaveInstitution(institution);
                transaction.Commit();
            }
        }

        public IEnumerable<Group> SearchGroups(string searchText, string institutionIds, string cohortIds)
        {
            return _adminRepository.SearchGroups(searchText, institutionIds, cohortIds).OrderBy(g => g.GroupName);
        }

        public int SaveGroup(Group group)
        {
            var response = _adminRepository.SaveGroup(group);
            return response;
        }

        public int DeleteGroup(int groupId)
        {
            return _adminRepository.DeleteGroup(groupId, _userId);
        }

        public IEnumerable<Institution> SearchInstitutes(string institutionId)
        {
            return _adminRepository.SearchInstitutes(institutionId);
        }

        public IEnumerable<Institution> SearchInstitution(string searchText, int userId)
        {
            return _adminRepository.SearchInstitutes(searchText, userId).ToList().Where(s => s.Status == "1").OrderBy(i => i.InstitutionName);
        }

        public IEnumerable<Institution> SearchInstitution(string searchText, int userId, string status, string ProgramofStudyName)
        {
            return _adminRepository.SearchInstitutes(searchText, userId).ToList().Where(s => s.Status == status && s.ProgramofStudyName == ProgramofStudyName).OrderBy(i => i.InstitutionName);
        }

        public IEnumerable<UserDetails> SearchUserDetails(string searchText, string status, int ProgramOfStudy)
        {
            if (ProgramOfStudy == (int)ProgramofStudyType.None)
            {
                return _adminRepository.SearchUnassignedUserDetails(searchText, status, ProgramOfStudy).ToList().OrderBy(i => i.FirstName);
            }
            else
            {
                return _adminRepository.SearchUserDetails(searchText, status, ProgramOfStudy).ToList().OrderBy(i => i.FirstName);
            }
        }

        public IEnumerable<Institution> GetInstitutions(int instituteId, string instituteIds, int userId)
        {
            return _adminRepository.GetInstitutes(instituteId, instituteIds, userId)
            .Where(s => s.Status.Equals("1") && s.InstitutionName.Trim().Length > 0)
            .ToList().OrderBy(i => i.InstitutionName);
        }

        public Institution GetInstitution(int instituteId, int userId)
        {
            var _instituteId = instituteId == 0 ? string.Empty : instituteId.ToString();
            return _adminRepository.GetInstitutions(userId, (int)ProgramofStudyType.Both, _instituteId).FirstOrDefault();
        }
        public List<Institution> GetAllInstitutions()
        {
            return _adminRepository.GetAllInstitution();
        }

        public IEnumerable<Cohort> SearchCohorts(string institutionIds, string searchString, int TestId, string dateFrom, string dateTo, int CohortStatus, int programofStudyId)
        {
            return _adminRepository.SearchCohorts(institutionIds, searchString, TestId, dateFrom, dateTo, CohortStatus, programofStudyId).ToList().OrderBy(i => i.CohortName);
        }

        public IEnumerable<Cohort> GetCohorts(int cohortId, string institutionId)
        {
            institutionId = string.IsNullOrEmpty(institutionId) ? "-2" : institutionId;
            return _adminRepository.GetCohorts(cohortId, institutionId).ToList().OrderBy(i => i.CohortName);
        }

        public IEnumerable<Cohort> GetCohortsByInstitutionIds(string institutionId)
        {
            institutionId = string.IsNullOrEmpty(institutionId) ? "-1" : institutionId;
            return _adminRepository.GetCohorts(0, institutionId).ToList().Where(c => c.CohortStatus == 1 && c.CohortName != string.Empty).OrderBy(i => i.CohortName);
        }

        public Cohort GetCohort(int cohortId)
        {
            return _adminRepository.GetCohorts(cohortId, string.Empty).FirstOrDefault();
        }

        public int SaveCohort(Cohort cohort)
        {
            var response = _adminRepository.SaveCohort(cohort);
            return response;
        }

        public void DeleteCohort(int cohortId, int cohortStatus, int cohortDeleteUser)
        {
            _adminRepository.DeleteCohort(cohortId, cohortStatus, cohortDeleteUser);
        }

        public Group GetGroup(int groupId)
        {
            return _adminRepository.GetGroups(groupId, string.Empty).FirstOrDefault();
        }

        public IEnumerable<Group> GetGroups(int groupId, string cohortId)
        {
            cohortId = cohortId == null ? "-1" : cohortId;
            return _adminRepository.GetGroups(groupId, cohortId).ToList().OrderBy(i => i.GroupName);
        }

        public int GetInstitutionIdForCohort(int cohortId)
        {
            return _adminRepository.GetInstitutionIdForCohort(cohortId);
        }

        public IEnumerable<Student> SearchStudents(int programOfStudyId, int institutionId, int cohortId, int groupId, string searchString)
        {
            return _adminRepository.SearchStudent(programOfStudyId, institutionId, cohortId, groupId, searchString, false);
        }

        public IEnumerable<Student> SearchStudents(int programOfStudyId, int institutionId, int cohortId, int groupId, string searchString, bool assignStudent)
        {
            return _adminRepository.SearchStudent(programOfStudyId, institutionId, cohortId, groupId, searchString, assignStudent);
        }

        public IEnumerable<Student> GetStudents(int studentId, int institutionId, int cohortId, int groupId)
        {
            return _adminRepository.GetStudentsInCohortAndGroups(institutionId, cohortId, groupId);
        }

        public IEnumerable<Group> GetGroupsList(int institutionId, int cohortId)
        {
            return _adminRepository.GetGroupsList(institutionId, cohortId).ToList().OrderBy(i => i.GroupName);
        }

        public Student GetStudent(int studentId)
        {
            return _adminRepository.GetStudents(studentId, string.Empty).FirstOrDefault();
        }

        public IEnumerable<Student> GetStudentsForGroups(int cohortId, int groupId)
        {
            return _adminRepository.GetStudentsInCohortAndGroups(-1, cohortId, groupId);
        }

        public IEnumerable<Student> GetStudentsForCohorts(int institutionId, int cohortId, int groupId)
        {
            return _adminRepository.GetStudentsInCohortAndGroups(institutionId, cohortId, groupId);
        }

        public StudentEntity GetDatesForCohortId(int cohortId)
        {
            return _adminRepository.GetDatesByCohortId(cohortId);
        }

        public int SaveUser(Student student, int AdminUserId, string AdminUserName)
        {
            int studentId = 0;
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                var addressId = 0;
                if (student.StudentAddress != null)
                {
                    addressId = _adminRepository.SaveAddress(student.StudentAddress);
                }

                student.AddressId = addressId;
                studentId = _adminRepository.SaveUser(student, AdminUserId, AdminUserName);
                transaction.Commit();
            }

            return studentId;
        }

        public void DeleteUser(int userId)
        {
            _adminRepository.DeleteUser(userId);
        }

        public Product GetProduct(int productId)
        {
            return _adminRepository.GetProducts(productId).FirstOrDefault();
        }

        public IEnumerable<Test> GetTests(int productId, string institutionIds)
        {
            return _adminRepository.GetTests(productId, 0, institutionIds, false).OrderBy(t => t.TestName);
        }

        public IEnumerable<Test> GetTests(int productId, int programofStudyId)
        {
            return _adminRepository.GetTests(productId, 0, string.Empty, false, programofStudyId).OrderBy(t => t.TestName);
        }

        public IEnumerable<Test> GetTests(int productId, int questionId, string institutionIds, bool forCMS, int programofStudy)
        {
            return _adminRepository.GetTests(productId, questionId, institutionIds, forCMS, programofStudy).OrderBy(t => t.TestName);
        }

        public Program GetCohortProgram(int iohortProgramId, int programId, int cohortId)
        {
            return _adminRepository.GetCohortProgram(iohortProgramId, programId, cohortId).FirstOrDefault();
        }

        public IEnumerable<Program> SearchCohortPrograms(int cohortId, string searchText)
        {
            return _adminRepository.SearchCohortPrograms(cohortId, searchText);
        }

        public void SaveCohortProgram(int cohortId, int programId, int active)
        {
            _adminRepository.SaveCohortProgram(cohortId, programId, active);
        }

        public void DeleteProductFromProgram(int programId, int productId, int type, int assetGroupId)
        {
            _adminRepository.DeleteProductFromProgram(programId, productId, type, assetGroupId);
        }

        public string AssignTestDateToCohort(List<CohortTestDates> testDate, UserType utype)
        {
            var studentMsg = new StringBuilder();
            foreach (var item in testDate)
            {
                if (utype.Equals(UserType.SuperAdmin))
                {
                    if (!string.IsNullOrEmpty(item.TestStartDate) && !string.IsNullOrEmpty(item.TestEndDate) && item.Type == 0)
                    {
                        var students = _adminRepository.GetAssignedStudentforCohort(item);
                        if (students.Count() > 0)
                        {
                            studentMsg.Append("Test Name : <b>" + students.FirstOrDefault().Test.TestName + "</b><br/>");
                            var groupStudents = students.Where(t => t.FirstName.Equals("GROUPNAME - ")).ToList();
                            if (groupStudents.Count() > 0)
                            {
                                groupStudents.ForEach(w => studentMsg.Append("<p><li>" + w.FirstName + ' ' + w.LastName + "</li>"));
                            }

                            var otherStudents = students.Where(t => !t.FirstName.Equals("GROUPNAME - ")).ToList();
                            if (otherStudents.Count() > 0)
                            {
                                otherStudents.ForEach(
                                    w => studentMsg.Append("<p><li>" + w.FirstName + ' ' + w.LastName + "</li>"));
                            }

                            studentMsg.Append("</p>");
                        }
                    }
                }

                _adminRepository.AssignTestDateToCohort(item);
            }

            return studentMsg.ToString();
        }

        public void AssignTestToProgram(List<ProgramTestDates> lstTestDates)
        {
            foreach (var item in lstTestDates)
            {
                _adminRepository.AssignTestToProgram(item.Program.ProgramId, item.Test.TestId, item.Product.Bundle, item.AssetGroupId);
            }
        }

        public string AssignTestToGroup(List<GroupTestDates> lstTestDates, UserType utype)
        {
            var studentMsg = new StringBuilder();
            foreach (var item in lstTestDates)
            {
                if (utype.Equals(UserType.SuperAdmin))
                {
                    var students = new List<Student>();
                    if (!string.IsNullOrEmpty(item.TestStartDate) && !string.IsNullOrEmpty(item.TestEndDate) &&
                        item.Type == 0)
                    {
                        students = _adminRepository.GetAssignedStudentforGroup(item);
                        if (students.Count() > 0)
                        {
                            studentMsg.Append("Test Name : <b>" + students.FirstOrDefault().Test.TestName + "</b><br/>");
                            students.ForEach(
                                w => studentMsg.Append("<p><li>" + w.FirstName + ' ' + w.LastName + "</li>"));
                            studentMsg.Append("</p>");
                        }
                    }
                }

                _adminRepository.AssignTestDateToGroup(item);
            }

            return studentMsg.ToString();
        }

        public void AssignTestToStudent(List<StudentTestDates> lstTestDates)
        {
            foreach (var item in lstTestDates)
            {
                _adminRepository.AssignTestDateToStudent(item);
            }
        }

        public IEnumerable<ProgramTestDates> GetTestsForProgram(int programId, string searchText)
        {
            return _adminRepository.GetTestsForProgram(programId, searchText).ToList().OrderBy(t => t.Product.ProductName);
        }

        public IEnumerable<GroupTestDates> GetTestsForGroup(int programId, int cohortId, int groupId, string searchText)
        {
            IEnumerable<GroupTestDates> groupTests = _adminRepository.GetTestsForGroup(programId, cohortId, groupId, searchText);
            var psAppendetests = groupTests.Select(x =>
            {
                x.TestName = (x.Type == 1) ? (ProgramofStudyType.RN + " " + x.TestName) : x.TestName;
                x.TestName = (x.Type == 2) ? (ProgramofStudyType.PN + " " + x.TestName) : x.TestName;
                return x;
            });

            return psAppendetests;
        }

        public IEnumerable<CohortTestDates> GetTestsForCohort(int programId, int cohortId, int TestId, string searchText)
        {
            IEnumerable<CohortTestDates> cohorttests = _adminRepository.GetTestsForCohort(programId, cohortId, TestId, searchText);
            var psAppendetests = cohorttests.Select(x =>
            {
                x.TestName = (x.Type == 1) ? (ProgramofStudyType.RN + " " + x.TestName) : x.TestName;
                x.TestName = (x.Type == 2) ? (ProgramofStudyType.PN + " " + x.TestName) : x.TestName;
                return x;
            });

            return psAppendetests;
        }

        public IEnumerable<StudentTestDates> GetTestsForStudent(int programId, int studentId, int cohortId, int groupId, int TestId, string searchText)
        {
            IEnumerable<StudentTestDates> studenttests = _adminRepository.GetTestsForStudent(programId, studentId, cohortId, groupId, TestId, searchText);
            var psAppendetests = studenttests.Select(x =>
            {
                x.TestName = (x.Type == 1) ? (ProgramofStudyType.RN + " " + x.TestName) : x.TestName;
                x.TestName = (x.Type == 2) ? (ProgramofStudyType.PN + " " + x.TestName) : x.TestName;
                return x;
            });

            return psAppendetests;
        }

        public int SaveAdmin(Admin admin)
        {
            return _adminRepository.SaveAdmin(admin);
        }

        public void DeleteAdmin(int userId)
        {
            _adminRepository.DeleteAdmin(userId);
        }

        public Admin GetAdmin(int userId)
        {
            return _adminRepository.GetAdmins(userId, string.Empty).FirstOrDefault();
        }

        public IEnumerable<Admin> GetAdmins(string adminName)
        {
            return _adminRepository.GetAdmins(0, adminName);
        }

        public bool IsDuplicateUserName(string userName, int userId, bool isAdmin)
        {
            bool IsDuplicateUserName = false;
            IEnumerable<Admin> admins = _adminRepository.GetAdmins(0, userName);
            if (isAdmin)
            {
                IsDuplicateUserName = admins.Where(a => a.UserName.ToLower() == userName.ToLower() && userId != a.UserId).Count() > 0;
            }
            else
            {
                IEnumerable<Student> students = _adminRepository.GetStudentsByName(userName);
                IsDuplicateUserName =
                    students.Where(s => s.UserName.ToLower() == userName.ToLower() && s.UserId != userId).Count() > 0;
            }

            return IsDuplicateUserName;
        }

        public IEnumerable<Admin> SearchAdmins(string institutionIds, string securityLevel, string searchString, int programofStudyId)
        {
            return _adminRepository.SearchAdmins(institutionIds, securityLevel, searchString, programofStudyId).ToList().OrderBy(u => u.UserName);
        }

        public void AssignInstitutionsToAdmin(List<Admin> admins, string institutionIds, int programofStudyId)
        {
            foreach (var admin in admins)
            {
                _adminRepository.AssignInstitutionsToAdmin(admin, institutionIds, programofStudyId);
            }
        }

        public void AssignStudentsToGroup(int groupId, string assignStudentList, string unassignedStudentList)
        {
            _adminRepository.AssignStudentsToGroup(groupId, assignStudentList, unassignedStudentList);
        }

        public void AssignStudents(string userId, int cohortId, int groupId, int institutionId)
        {
            _adminRepository.AssignStudents(userId, cohortId, groupId, institutionId);
        }

        public IEnumerable<Email> GetEmail()
        {
            return _adminRepository.GetEmail(0)
                .OrderBy(r => r.Title);
        }

        public IEnumerable<Email> GetEmail(int emailId)
        {
            return _adminRepository.GetEmail(emailId);
        }

        public IEnumerable<Institution> GetLocalInstitution(int userId)
        {
            return _adminRepository.GetLocalInstitution(userId);
        }

        public IEnumerable<Admin> GetAdmin(string institutionIds)
        {
            return _adminRepository.GetAdmin(institutionIds, string.Empty);
        }

        public IEnumerable<Admin> GetAdmin(string institutionIds, string searchCriteria)
        {
            return _adminRepository.GetAdmin(institutionIds, searchCriteria);
        }

        public IEnumerable<StudentEntity> SearchStudent(string criteria)
        {
            return _adminRepository.SearchStudent(criteria);
        }

        public IEnumerable<Admin> SearchAdmin(string criteria)
        {
            return _adminRepository.SearchAdmin(criteria);
        }

        public int CreateCustomEmailToPerson(int adminId, DateTime sendTime, int emailId, bool toAdminOrStudent, string personIds)
        {
            return _adminRepository.CreateCustomEmailToPerson(adminId, sendTime, emailId, toAdminOrStudent, personIds);
        }

        public int CreateCustomEmailToCohort(int adminId, DateTime sendTime, int emailId, bool toAdminOrStudent, string cohortIds)
        {
            return _adminRepository.CreateCustomEmailToCohort(adminId, sendTime, emailId, toAdminOrStudent, cohortIds);
        }

        public int CreateCustomEmailToInstitution(int adminId, DateTime sendTime, int emailId, bool toAdminOrStudent, string institutionIds)
        {
            return _adminRepository.CreateCustomEmailToInstitution(adminId, sendTime, emailId, toAdminOrStudent, institutionIds);
        }

        public int CreateCustomEmailToGroup(int adminId, DateTime sendTime, int emailId, bool toAdminOrStudent, string groupIds)
        {
            return _adminRepository.CreateCustomEmailToGroup(adminId, sendTime, emailId, toAdminOrStudent, groupIds);
        }

        public UserAuthentication AuthenticateUser(string userName, string userPassword)
        {
            UserAuthentication _result = new UserAuthentication()
            {
                Status = AuthenticationRequest.InValidUser,
                IsAdminLogin = true
            };
            var _user = _adminRepository.AuthenticateUser(userName, userPassword).FirstOrDefault();
            if (_user != null)
            {
                _result.User = _user;
                var _userType = EnumHelper.GetUserType(_user.SecurityLevel);

                if (_userType == UserType.InstitutionalAdmin
                    || _userType == UserType.TechAdmin
                    || _userType == UserType.LocalAdmin)
                {
                    var _institutionCount = _adminRepository.GetInstitutions(_user.UserId, string.Empty).Count();
                    if (_institutionCount == 0)
                    {
                        _result.Status = AuthenticationRequest.NoInstitution;
                    }
                    else
                    {
                        _result.Status = AuthenticationRequest.Success;
                    }
                }
                else
                {
                    _result.Status = AuthenticationRequest.Success;
                }
            }

            return _result;
        }

        public void DeleteTest(int testId, string userName)
        {
            _adminRepository.DeleteTest(testId, userName);
        }

        public void ResumeTest(string userTestId, string userName)
        {
            _adminRepository.ResumeTest(userTestId, userName);
        }

        public IEnumerable<UserTest> GetStudentsForOverRide(int institutionId, string firstName, string lastName, string userName,
            string testName, bool showIncompleteOnly, string cohortIds)
        {
            return _adminRepository.GetStudentsForOverRide(institutionId, firstName, lastName, userName, testName, showIncompleteOnly, cohortIds);
        }

        public IEnumerable<UserTest> GetDeletedTestListForStudents(int institutionId, string firstName, string lastName, string userName,
            string testName, bool showIncompleteOnly, string cohortIds)
        {
            return _adminRepository.GetDeletedTestListForStudents(institutionId, firstName, lastName, userName, testName, showIncompleteOnly, cohortIds);
        }

        public void SaveEmail(int emailId, string title, string body)
        {
            _adminRepository.SaveEmail(emailId, title, body);
        }

        public int UserLogIn(string username, string password)
        {
            return _adminRepository.UserLogIn(username, password);
        }

        public IEnumerable<Student> GetUserInfo(int UserID)
        {
            return _adminRepository.GetUserInfo(UserID);
        }

        public int GetUserID()
        {
            return _adminRepository.GetUserID();
        }

        public int GetInstitutionIDByFacilityID(int FacilityID)
        {
            return _adminRepository.GetInstitutionIDByFacilityID(FacilityID);
        }

        public object GetUser(string UserID, int institutionId)
        {
            return _adminRepository.GetUser(UserID, institutionId);
        }

        public string GetPassword()
        {
            return _adminRepository.GetPassword();
        }

        public bool GetUpdatedIntegratedUser(int UserID, string ClassCode)
        {
            return _adminRepository.GetUpdatedIntegratedUser(UserID, ClassCode);
        }

        public void DeleteInstitution(int institutionId, int userId)
        {
            _adminRepository.DeleteInstitution(institutionId, userId);
        }

        public void SaveHelpfulDocuments(HelpfulDocument helpfulDocument)
        {
            _adminRepository.SaveHelpfulDocuments(helpfulDocument);
        }

        public HelpfulDocument GetHelpfulDocument(int documentId)
        {
            return _adminRepository.GetHelpfulDocuments(documentId, string.Empty).FirstOrDefault();
        }

        public IEnumerable<HelpfulDocument> SearchHelpfulDocs(string searchKeyword, bool IsLink)
        {
            return _adminRepository.SearchHelpfulDocs(searchKeyword, IsLink);
        }

        public void DeleteHelpfulDoc(int userId, int docId)
        {
            _adminRepository.DeleteHelpfulDoc(userId, docId);
        }

        public IEnumerable<Country> GetCountries(int countryId)
        {
            return _adminRepository.GetCountries(countryId);
        }

        public IEnumerable<State> GetStates(int countryId, int stateId)
        {
            return _adminRepository.GetStates(countryId, stateId);
        }

        public int SaveAddress(Address address)
        {
            return _adminRepository.SaveAddress(address);
        }

        public Address GetAddress(int addressId)
        {
            return _adminRepository.GetAddress(addressId);
        }

        public void SaveInstitutionContact(InstitutionContact institutionConatct)
        {
            _adminRepository.SaveInstitutionContact(institutionConatct);
        }

        public IEnumerable<InstitutionContact> GetInstitutionContacts(int institutionId)
        {
            IEnumerable<InstitutionContact> institutionContacts = _adminRepository.GetInstitutionContacts(institutionId, 0);
            return institutionContacts.Where(a => a.Status == 1);
        }

        public InstitutionContact GetInstitutionContactsByContactId(int contactId)
        {
            IEnumerable<InstitutionContact> institutionContacts = _adminRepository.GetInstitutionContacts(0, contactId);
            return institutionContacts.Where(a => a.Status == 1).SingleOrDefault();
        }

        public void SaveAdhocGroup(List<int> studentIds, AdhocGroup adhocGroup)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                SaveAdhocStudentGroup(studentIds, adhocGroup);

                transaction.Commit();
            }
        }

        public void SaveAdaAdhocGroup(List<int> studentIds, AdhocGroup adaAdhocGroup)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                SaveAdhocStudentGroup(studentIds, adaAdhocGroup);
                _adminRepository.UpdateStudentsADA(string.Join("|", studentIds.Select(n => n.ToString()).ToArray()), Convert.ToBoolean(adaAdhocGroup.ADA));
                transaction.Commit();
            }
        }

        public IEnumerable<Student> SearchStudentsForTest(int institutionId, int cohortId, int groupId, string searchString)
        {
            return _adminRepository.SearchStudentForTest(institutionId, cohortId, groupId, searchString);
        }

        public IEnumerable<AdhocGroupTestDetails> GetAdhocGroupTestDetail(int adhocGroupId)
        {
            return _adminRepository.GetAdhocGroupTests(adhocGroupId);
        }

        public void SaveAdhocGroupTest(AdhocGroupTestDetails adhocGroupTestDetail)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _adminRepository.SaveAdhocGroupTest(adhocGroupTestDetail);
                IEnumerable<Student> students = _adminRepository.GetAdhocGroupStudentDetail(adhocGroupTestDetail.AdhocGroupId);
                foreach (Student s in students)
                {
                    StudentTestDates studentTestDates = new StudentTestDates();
                    studentTestDates.Cohort = new Cohort() { CohortId = s.CohortId };
                    studentTestDates.Group = new Group() { GroupId = s.GroupId };
                    studentTestDates.Student = new Student() { UserId = s.UserId };
                    studentTestDates.Product = new Product() { ProductId = adhocGroupTestDetail.TestId };
                    studentTestDates.TestStartDate = adhocGroupTestDetail.StartDate;
                    studentTestDates.TestEndDate = adhocGroupTestDetail.EndDate;
                    ////studentTestDates.AdhocGroupId = adhocGroupTestDetail.AdhocGroupId;
                    studentTestDates.Type = adhocGroupTestDetail.Type;
                    _adminRepository.AssignAdhocTests(studentTestDates);
                }

                transaction.Commit();
            }
        }

        public IEnumerable<Test> GetTestsByCohort(int programId, int cohortId, int TestId, string searchText)
        {
            return _adminRepository.GetTestsByCohort(programId, cohortId, TestId, searchText);
        }

        public IDictionary<int, string> CheckSystem(bool isProductionApp)
        {
            return _adminRepository.CheckSystem(isProductionApp);
        }

        public List<EmailMission> GetStudentEmailMission(string userIds, string groupIds, string cohortIds, string institutionIds)
        {
            return _adminRepository.GetStudentEmailMission(userIds, groupIds, cohortIds, institutionIds);
        }

        public List<EmailMission> GetAdminEmailMission(string userIds, string institutionIds)
        {
            return _adminRepository.GetAdminEmailMission(userIds, institutionIds);
        }

        public LoginContent GetLoginContent(int contentId)
        {
            return _adminRepository.GetLoginContent(contentId);
        }

        public List<Program> GetBulkProgramDetails(int testId, string type, int programOfStudyId)
        {
            return _adminRepository.GetBulkProgramDetails(testId, type, programOfStudyId);
        }

        public void SaveBulkModifiedPrograms(int testId, int type, string programIds)
        {
            _adminRepository.SaveBulkModifiedPrograms(testId, type, programIds);
        }

        public IEnumerable<ProgramofStudy> GetProgramofStudies()
        {
            return CacheManager.Get(Constants.CACHE_KEY_PROGRAM_OF_STUDIES, () => _adminRepository.GetProgramofStudies());
        }

        public IEnumerable<AuditTrail> GetAuditTrailData(int studentId)
        {
            return _adminRepository.GetAuditTrail(studentId);
        }

        public IEnumerable<AssetGroup> GetAssetGroups(int programofStudyId)
        {
            return _adminRepository.GetAssetGroups(programofStudyId);
        }

        public IEnumerable<Asset> GetAssets(int assetGroupId)
        {
            return _adminRepository.GetAssets(assetGroupId);
        }

        public IEnumerable<CaseStudy> GetCaseAssets()
        {
            return _adminRepository.GetCaseAssets();
        }

        public void AssignAssetsToProgram(List<ProgramTestDates> assetList)
        {
            foreach (var item in assetList)
            {
                _adminRepository.AssignAssetsToProgram(item.Program.ProgramId, item.Test.TestId, item.Product.Bundle, item.AssetGroupId);
            }
        }

        public IEnumerable<Program> GetProgramsByProgramofStudyId(int programofStudyId)
        {
            return _adminRepository.GetProgramsByProgramofStudyId(programofStudyId);
        }

        public int GetInstitutionIdByFacilityIdOrClassCode(int facilityId, string classCode)
        {
            return _adminRepository.GetInstitutionIdByFacilityIdOrClassCode(facilityId, classCode);
        }

        public string GetUniqueUsername(string firstName, string lastName)
        {
            return _adminRepository.GetUniqueUsername(firstName, lastName);
        }

        private IEnumerable<Product> GetAllProducts()
        {
            return _adminRepository.GetProducts(0)
                .OrderBy(p => p.ProductId);
        }

        private IDictionary<UserType, IDictionary<Module, IList<UserAction>>> GetAllAuthorizationRules()
        {
            return _adminRepository.GetAuthorizationRules();
        }

        private IDictionary<CategoryName, Category> GetAllCategories()
        {
            var allCategories = new Dictionary<CategoryName, Category>();
            IEnumerable<Category> categories = _adminRepository.GetCategories();
            foreach (var category in categories)
            {
                category.Details = _adminRepository.GetCategoryDetails(category.CategoryID, category.ProgramofStudyId).ToDictionary(k => k.Id);
                allCategories.Add(
                    (CategoryName)Enum.Parse(typeof(CategoryName), category.CategoryID.ToString()), category);
            }

            return allCategories;
        }

        private void SaveAdhocStudentGroup(List<int> studentIds, AdhocGroup adhocGroup)
        {
            _adminRepository.SaveAdhocGroup(adhocGroup);
            foreach (var studentId in studentIds)
            {
                _adminRepository.SaveAdhocGroupStudent(studentId, adhocGroup.AdhocGroupId);
            }
        }

        public List<LtiProvider> GetLtiProviders(int ltiProviderId = 0)
        {
            return _adminRepository.GetLtiProviders(ltiProviderId);
        }

        public int SaveLtiProvider(LtiProvider ltiProvider)
        {
            var response = _adminRepository.SaveLtiProvider(ltiProvider);
            return response;
        }

        public void ChangeLtiProviderStatus(int ltiProviderId)
        {
            _adminRepository.ChangeLtiProviderStatus(ltiProviderId);
        }

        public int SaveUser(Student student)
        {
            return SaveUser(student, 1, "Admin");
        }

        public void CopyProgram(Program program)
        {
            var programService = new ProgramService();
            programService.Validate(program);
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _adminRepository.SaveProgram(program, _userId);
                _adminRepository.CopyProgramTests(program.ReferenceProgramId, program.ProgramId);
                transaction.Commit();
            }
        }

        public LtiProvider GetLtiTestSecurityProviderByName(string ltiProviderName)
        {
            return _adminRepository.GetLtiTestSecurityProviderByName(ltiProviderName);
        }
        #endregion
    }
}
