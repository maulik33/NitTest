using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using NursingLibrary.Common;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;

namespace NursingLibrary.Services
{
    public class ReportDataService : IReportDataService
    {
        #region Fields

        private readonly IReportRepository _reportRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAdminService _adminService;

        #endregion Fields

        #region Constructors

        public ReportDataService(IReportRepository reportRepository, IUnitOfWork unitOfWork, IAdminService adminService)
        {
            _reportRepository = reportRepository;
            _unitOfWork = unitOfWork;
            _adminService = adminService;
        }

        #endregion Constructors

        #region Properties

        public ICacheManagement CacheManager { get; set; }

        #endregion Properties

        #region IReportDataService Methods

        public IEnumerable<Institution> GetInstitutions(int userId)
        {
            return _reportRepository.GetInstitutions(userId, string.Empty)
            .Where(s => s.InstitutionName.Trim().Length > 0)
            .ToList().OrderBy(i => i.InstitutionName).ToList();
        }

        public IEnumerable<Cohort> GetCohorts(int institutionId)
        {
            string institution = string.IsNullOrEmpty(institutionId.ToString()) ? "-2" : institutionId.ToString();
            return _reportRepository.GetCohorts(0, institution).ToList().OrderBy(i => i.CohortName);
        }

        public IEnumerable<Cohort> GetCohorts(string institutionIds)
        {
            institutionIds = string.IsNullOrEmpty(institutionIds) ? "-2" : institutionIds;
            return _reportRepository.GetCohorts(0, institutionIds).ToList().OrderBy(i => i.CohortName);
        }

        public IEnumerable<Cohort> GetCohorts(int cohortId, string institutionIds)
        {
            institutionIds = string.IsNullOrEmpty(institutionIds) ? "-2" : institutionIds;
            return _reportRepository.GetCohorts(cohortId, institutionIds).ToList().OrderBy(i => i.CohortName);
        }

        public IEnumerable<Product> GetProducts()
        {
            return CacheManager.Get(Constants.CACHE_KEY_PRODUCTS, () => GetAllProducts()).OrderBy(p => p.ProductId);
        }

        public Product GetProduct(int productId)
        {
            return _reportRepository.GetProducts(productId).FirstOrDefault();
        }

        public IEnumerable<UserTest> GetTests(int productId, string cohortIds)
        {
            return GetTests(productId.ToString(), cohortIds, "0");
        }

        public IEnumerable<UserTest> GetTests(string productIds, string cohortIds)
        {
            return _reportRepository.GetTests(productIds, cohortIds, "0", 0)
                .OrderBy(p => p.TestName);
        }

        public IEnumerable<UserTest> GetTests(int userId, int productId)
        {
            return _reportRepository.GetTests(userId, productId)
                .OrderBy(p => p.TestName);
        }

        public IEnumerable<UserTest> GetTestsByInstitute(string institutionIds, string productIds)
        {
            return _reportRepository.GetTests(institutionIds, productIds)
            .OrderBy(p => p.TestName);
        }

        /// <summary>
        /// Gets Institution Details
        /// </summary>
        /// <param name="institutionId">If 0 returns all institutions. If multiple institutuions pass as comma seperated</param>
        /// <returns></returns>
        public IEnumerable<Institution> GetInstitutionDetails(string institutionId)
        {
            return _reportRepository.GetInstitutionDetails(institutionId);
        }

        /// <summary>
        /// Get list of products
        /// </summary>
        /// <returns>IEnumerable<Product></returns>
        public IEnumerable<Product> GetListOfAllProducts()
        {
            return CacheManager.Get(Constants.CACHE_KEY_PRODUCTS, () => GetAllProducts());
        }

        /// <summary>
        /// Returns list of tests for given product and cohort ids.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="cohortIds"></param>
        /// <returns></returns>
        public IEnumerable<UserTest> GetTests(string productIds, string cohortIds, string studentIds)
        {
            return _reportRepository.GetTests(productIds, cohortIds, studentIds, 0)
                .OrderBy(p => p.TestName);
        }

        public DataTable GetStudentSummaryByQuestionDetails(int instituteId, int productId, string cohortIds, int testId)
        {
            return _reportRepository.GetStudentSummaryByQuestionDetails(instituteId, productId, cohortIds, testId);
        }

        /// <summary>
        /// Get list of group details for Institution Id And Cohort Id
        /// </summary>
        /// <param name="institutionId"></param>
        /// <param name="cohortIds"></param>
        /// <returns></returns>
        public IEnumerable<Group> GetGroups(int institutionId, string cohortIds)
        {
            return _reportRepository.GetGroups(institutionId, cohortIds);
        }

        public IEnumerable<Group> GetGroups(string institutionIds, string cohortIds)
        {
            return _reportRepository.GetGroups(institutionIds, cohortIds);
        }

        /// <summary>
        /// Get list of students in institute for given cohort and group ids
        /// </summary>
        /// <param name="institutionId"></param>
        /// <param name="cohortIds"></param>
        /// <param name="groupIds"></param>
        /// <returns>IEnumerable<StudentEntity></returns>
        public IEnumerable<StudentEntity> GetStudents(string institutionIds, string cohortIds, string groupIds, UserType userType)
        {
            IEnumerable<StudentEntity> students = _reportRepository.GetStudents(institutionIds, cohortIds, groupIds, string.Empty);
            if (userType != UserType.InstitutionalAdmin && userType != UserType.LocalAdmin && userType != UserType.TechAdmin)
            {
                if (students != null)
                {
                    students = students.Where(s => s.InstitutionStatus == true);
                }
            }

            return students;
        }

        public IEnumerable<StudentEntity> GetStudents(string institutionIds, string cohortIds)
        {
            return _reportRepository.GetStudents(institutionIds, cohortIds);
        }

        public IEnumerable<StudentEntity> GetStudents(string institutionIds, string cohortIds, string groupIds, string searchCriteria)
        {
            IEnumerable<StudentEntity> students = _reportRepository.GetStudents(institutionIds, cohortIds, groupIds, searchCriteria);
            if (students != null)
            {
                students = students.Where(s => s.InstitutionStatus == true);
            }

            return students;
        }

        public Student GetStudentDetails(int studentId)
        {
            return _reportRepository.GetStudents(studentId, string.Empty).FirstOrDefault();
        }

        /// <summary>
        /// Get list of students in institute for given cohort and group ids
        /// </summary>
        /// <param name="institutionId"></param>
        /// <param name="cohortIds"></param>
        /// <param name="groupIds"></param>
        /// <returns>IEnumerable<StudentEntity></returns>
        public IEnumerable<Test> GetTests(string cohortIds, string studentIds, string productIds, string groupIds, int institutionId)
        {
            return _reportRepository.GetTests(cohortIds, studentIds, productIds, groupIds, institutionId);
        }

        /// <summary>
        /// Gets Student Report card details
        /// </summary>
        /// <param name="studentIds"></param>
        /// <param name="testIds"></param>
        /// <param name="institutionId"></param>
        /// <param name="testTypeId"></param>
        /// <param name="cohortIds"></param>
        /// <param name="groupIds"></param>
        /// <returns></returns>
        public IEnumerable<StudentReportCardDetails> GetStudentReportCardDetails(string studentIds, string testIds, int institutionId, string testTypeId)
        {
            var _testTypeId = testTypeId.Replace(",", "|");
            IEnumerable<StudentReportCardDetails> reportCardDetails = _reportRepository.GetStudentReportCardDetails(studentIds, testIds, institutionId, testTypeId);
            foreach (StudentReportCardDetails srcd in reportCardDetails)
            {
                if (srcd.Product != null && srcd.Product.ProductId == (int)ProductType.SkillsModules)
                {
                    if (string.IsNullOrEmpty(srcd.RemediationTime))
                    {
                        srcd.RemediationTime = "N/A";
                    }

                    if (string.IsNullOrEmpty(srcd.Rank) || srcd.Rank == "0")
                    {
                        srcd.Rank = "N/A";
                        srcd.Ranking = 101;
                    }
                }
            }

            return reportCardDetails;
        }

        public IEnumerable<CohortByTest> GetCohortByTestDetails(int institutionId, string testsIds, string cohortIds, string groupIds, string productIds)
        {
            return _reportRepository.GetCohortByTestDetails(institutionId, testsIds, cohortIds, groupIds, productIds)
                .OrderBy(r => r.TestName);
        }

        /// <summary>
        /// Gets English Nursing Tracking details
        /// </summary>
        /// <param name="studentIds"></param>
        /// <param name="testIds"></param>
        /// <param name="institutionId"></param>
        /// <param name="testTypeId"></param>
        /// <param name="cohortIds"></param>
        /// <param name="groupIds"></param>
        /// <returns></returns>
        public IEnumerable<EnglishForNursingTracking> GetEnglishNursingTrackingDetails(string institutionId, string cohortIds, string studentIds, string testIds, string qIds)
        {
            ////var _testTypeId = testTypeId.Replace(",", "|");
            return _reportRepository.GetEnglishNursingTrackingDetails(institutionId, cohortIds, studentIds, testIds, qIds);
        }

        /// <summary>
        /// Get Results From Program
        /// </summary>
        /// <param name="userTestID"></param>
        /// <param name="charttype"></param>
        /// <returns></returns>
        public ResultsFromTheProgram GetResultsFromTheProgram(int userTestID, int charttype)
        {
            return _reportRepository.GetResultsFromTheProgram(userTestID, charttype);
        }

        public IEnumerable<string> GetTestCharacteristics(int userTestID, string userType)
        {
            return _reportRepository.GetTestCharacteristics(userTestID, userType);
        }

        public IEnumerable<string> GetTestCharacteristicsByTestId(int testID, string userType)
        {
            return _reportRepository.GetTestCharacteristicsByTestId(testID, userType);
        }

        /// <summary>
        /// Get Probability for test
        /// </summary>
        /// <param name="userTestId"></param>
        /// <param name="Correct"></param>
        /// <returns></returns>
        public int GetProbability(int userTestId, int correct)
        {
            return _reportRepository.GetProbability(userTestId, correct);
        }

        /// <summary>
        /// Get Percentile Rank for test
        /// </summary>
        /// <param name="userTestId"></param>
        /// <param name="correct"></param>
        /// <returns></returns>
        public int GetPercentileRank(int userTestId, int correct)
        {
            return _reportRepository.GetPercentileRank(userTestId, correct);
        }

        /// <summary>
        /// Checks Probability Exist for test
        /// </summary>
        /// <param name="userTestId"></param>
        /// <returns></returns>
        public int CheckProbabilityExist(int userTestId)
        {
            return _reportRepository.CheckProbabilityExist(userTestId);
        }

        /// <summary>
        /// Checks Percentile Rank Exist for test
        /// </summary>
        /// <param name="userTestId"></param>
        /// <returns></returns>
        public int CheckPercentileRankExist(int userTestId)
        {
            return _reportRepository.CheckPercentileRankExist(userTestId);
        }

        /// <summary>
        /// Get Results From Program for chart
        /// </summary>
        /// <param name="userTestID"></param>
        /// <param name="charttype"></param>
        /// <returns></returns>
        public IEnumerable<ResultsFromTheProgramForChart> GetResultsFromTheProgramForChart(int userTestId, string chartType)
        {
            return _reportRepository.GetResultsFromTheProgramForChart(userTestId, chartType);
        }

        /// <summary>
        /// Get Test Assignment
        /// </summary>
        /// <param name="userTestId"></param>
        /// <returns></returns>
        public IEnumerable<TestCategory> GetTestAssignment(int userTestId)
        {
            return _reportRepository.GetTestAssignment(userTestId)
                .OrderBy(p => p.OrderNumber);
        }

        /// <summary>
        /// Get Remediation Time Details For Test
        /// </summary>
        /// <param name="userTestId"></param>
        /// <param name="typeOfFileId"></param>
        /// <returns></returns>
        public IEnumerable<TestRemediationTimeDetails> GetRemediationTimeForTest(int userTestId, string typeOfFileId)
        {
            return _reportRepository.GetRemediationTimeForTest(userTestId, typeOfFileId)
                .OrderBy(p => p.QuestionNumber);
        }

        public IEnumerable<TestRemediationTimeDetails> GetRemediationTimeForNCLXTest(int userTestId, string typeOfFileId)
        {
            return _reportRepository.GetRemediationTimeForNCLXTest(userTestId, typeOfFileId)
                .OrderBy(p => p.QuestionNumber);
        }

        public IEnumerable<Cohort> GetCohortsForStudent(int studentId)
        {
            return _reportRepository.GetCohortsForStudent(studentId);
        }

        public IEnumerable<UserTest> GetUserTestByID(int userTestId)
        {
            return _reportRepository.GetUserTestByID(userTestId);
        }

        public IEnumerable<TestRemediationExplainationDetails> GetTestRemediation(int userId, int productId, int institutionId, string cohortIds)
        {
            IEnumerable<TestRemediationExplainationDetails> testRemediations = _reportRepository.GetTestRemediation(userId, productId, institutionId, cohortIds);
            foreach (TestRemediationExplainationDetails t in testRemediations)
            {
                if (t.ProductId == (int)ProductType.SkillsModules)
                {
                    t.RemediationOrExplaination = "N/A";
                }
            }

            return testRemediations;
        }

        public IEnumerable<TestByInstitutionResults> GetTestByInstitutionResults(string institutionIds, string cohortIds, int productId, int testId)
        {
            IEnumerable<TestByInstitutionResults> testByInstitutionResults = _reportRepository.GetTestByInstitutionResults(institutionIds, cohortIds, productId, testId);
            foreach (TestByInstitutionResults result in testByInstitutionResults)
            {
                if (string.IsNullOrEmpty(result.Normed))
                {
                    result.Normed = "N/A";
                }
            }
            return testByInstitutionResults;
        }

        public IEnumerable<SummaryPerformanceByQuestionResult> GetSummaryPerformanceByQuestionReportResult(String cohortIds, int productId, int testId)
        {
            return _reportRepository.GetSummaryPerformanceByQuestionReportResult(cohortIds, productId, testId)
                .OrderBy(p => p.QuestionId);
        }

        public IEnumerable<CaseStudy> GetCaseStudies()
        {
            return _reportRepository.GetCaseStudies()
                .OrderBy(p => p.CaseOrder);
        }

        public IEnumerable<StudentEntity> GetListOfStudents(int cohortId, int institutionId, int caseId, string searchText)
        {
            return _reportRepository.GetListOfStudents(cohortId, institutionId, caseId, searchText);
        }

        public IEnumerable<StudentEntity> GetListOfStudents(int cohortId)
        {
            return _reportRepository.GetListOfStudents(cohortId);
        }

        public IEnumerable<Modules> GetModule()
        {
            return _reportRepository.GetModule();
        }

        public IEnumerable<CaseByCohortResult> GetCaseByCohort(String institutionIds, int caseId, int moduleId)
        {
            return _reportRepository.GetCaseByCohort(institutionIds, caseId, moduleId);
        }

        public IEnumerable<ResultsFromTheCohortForChart> GetResultsFromTheCohotForChart(int institutionId, int subCategoryId, int chartType, string productIds, string tests, string cohortIds)
        {
            return _reportRepository.GetResultsFromTheCohotForChart(institutionId, subCategoryId, chartType, productIds, tests, cohortIds);
        }

        public IEnumerable<ResultsForStudentReportCardByModule> GetResultsForStudentReportCardByModule(string institutionIds, int caseId, string caseName,
           string moduleIds, int cohortId, string studentIds)
        {
            return _reportRepository.GetResultsForStudentReportCardByModule(institutionIds, caseId, caseName, moduleIds, cohortId, studentIds);
        }

        public CohortResultsByModule GetCohortResultsbyModule(int caseId, string moduleIds, string cohortIds)
        {
            return _reportRepository.GetCohortResultsbyModule(caseId, moduleIds, cohortIds);
        }

        public IEnumerable<CohortResultsByModule> GetCaseSubCategoryResultbyCohortModule(int caseId, string moduleIds, string cohortIds, string categoryName)
        {
            return _reportRepository.GetCaseSubCategoryResultbyCohortModule(caseId, moduleIds, cohortIds, categoryName);
        }

        public IEnumerable<CategoryDetail> GetCaseSubCategories()
        {
            return _reportRepository.GetCaseSubCategories();
        }

        public IEnumerable<ResultsFromTheProgramForChart> GetResultsFromCohortForChart(string cohortIds, string testTypeIds, string testIds, string chartType, string fromDate, string toDate)
        {
            return _reportRepository.GetResultsFromCohortForChart(cohortIds, testTypeIds, testIds, chartType, fromDate, toDate);
        }

        public IEnumerable<ResultsFromTheProgramForChart> GetResultsFromInstitutionForChart(string institutionIds, string testTypeIds, string testIds, string chartType, string fromDate, string toDate)
        {
            return _reportRepository.GetResultsFromInstitutionForChart(institutionIds, testTypeIds, testIds, chartType, fromDate, toDate);
        }

        public ResultsFromTheProgram GetQuestionResultsForCohort(string cohortIds, string testTypes, string testIds, int chartType)
        {
            return _reportRepository.GetQuestionResultsForCohort(cohortIds, testTypes, testIds, chartType, string.Empty);
        }

        public ResultsFromTheProgram GetQuestionResultsForCohort(string cohortIds, string testTypes, string testIds, int chartType, string groupIds)
        {
            return _reportRepository.GetQuestionResultsForCohort(cohortIds, testTypes, testIds, chartType, groupIds);
        }

        public ResultsFromTheProgram GetQuestionResultsForInstitution(int institutionId, string testTypes, string testIds, int chartType)
        {
            return _reportRepository.GetQuestionResultsForInstitution(institutionId, testTypes, testIds, chartType);
        }

        public decimal GetNormForTest(int testId)
        {
            return _reportRepository.GetNormForTest(testId);
        }

        public decimal GetRankForTest(int testId)
        {
            return _reportRepository.GetRankForTest(testId);
        }

        public IEnumerable<ResultsFromTheProgram> GetResultsFromInstitutions(string institutionIds, int chartType, string testTypeIds, string testIds, string fromDate, string toDate)
        {
            return _reportRepository.GetResultsFromInstitutions(institutionIds, chartType, testTypeIds, testIds, fromDate, toDate);
        }

        public IEnumerable<ResultsFromTheCohortForChart> GetResultsForCohortsBySubCategoryChart(string cohorts, int categoryId, int subCategoryId, string cases, string modules, int institutionId)
        {
            return _reportRepository.GetResultsForCohortsBySubCategoryChart(cohorts, categoryId, subCategoryId, cases, modules, institutionId);
        }

        public DataTable GetResultsForStudentSummaryByAnswerChoice(string cohortIds, int productId, int testId)
        {
            return _reportRepository.GetResultsForStudentSummaryByAnswerChoice(cohortIds, productId, testId);
        }

        public DataTable GetStudentSummaryByQuestionHeader(int productId, string cohortIds, int testId)
        {
            return _reportRepository.GetStudentSummaryByQuestionHeader(productId, cohortIds, testId);
        }

        public DataTable GetResultsByCohortQuestions(string cohortIds, int productId, int testId)
        {
            DataTable dt = _reportRepository.GetResultsByCohortQuestions(cohortIds, productId, testId);
            dt.DefaultView.Sort = "TopicTitle";
            return dt;
        }

        public DataTable GetResultsByInstitutionQuestions(string cohortIds, int productId, int testId)
        {
            return _reportRepository.GetResultsByInstitutionQuestions(cohortIds, productId, testId);
        }

        public int GetStudentNumberByCohortTest(int cohortId, int productId, int testId)
        {
            return _reportRepository.GetStudentNumberByCohortTest(cohortId, productId, testId);
        }

        public IEnumerable<Test> GetTestsByProgramofStudyType(int productId, int programofStudy)
        {
            return _reportRepository.GetTests(productId, 0, string.Empty, false, programofStudy).OrderBy(t => t.TestName);
        }

        public IEnumerable<Test> GetTestsForProgramOfStudy(int productId, int programOfStudy)
        {
            return _reportRepository.GetTestsByProgramOfStudy(productId, programOfStudy).OrderBy(t => t.TestName);
        }

        public IEnumerable<Institution> GetInstitutionByStudentID(int UserID)
        {
            return _reportRepository.GetInstitutionByStudentID(UserID);
        }

        public IEnumerable<ReportTestsScheduledbyDate> GetTestsScheduledByDate(string programOfStudyName, string institutionIds, string cohortIds, string groupIds, string productIds, DateTime? startDate, DateTime? endDate)
        {
            return _reportRepository.GetTestsScheduledByDate(programOfStudyName,institutionIds, cohortIds, groupIds, productIds, startDate, endDate).Where(r => r.CohortName != string.Empty);
        }

        public IEnumerable<State> GetStates(int countryId, int stateId)
        {
            return _reportRepository.GetStates(countryId, stateId);
        }


        public IEnumerable<UserTest> GetTestsForStudentReportCard(string productIds, string studentIds)
        {
            return _reportRepository.GetTestsForStudentReportCard(productIds, studentIds)
                .OrderBy(p => p.TestName);
        }

        public IEnumerable<UserTest> GetTestsForEnglishNursingTracking(string cohortIds, string studentIds)
        {
            return _reportRepository.GetTestsForEnglishNursingTracking(cohortIds, studentIds)
                .OrderBy(p => p.TestName);
        }

        public IEnumerable<Question> GetQIDForEnglishNursingTracking(string cohortIds, string studentIds, string testIds)
        {
            return _reportRepository.GetQIDForEnglishNursingTracking(cohortIds, studentIds, testIds)
                .OrderBy(p => p.Id);
        }

        public IDictionary<CategoryName, Category> GetCategories()
        {
            return CacheManager.GetNotRemovableItem(Constants.CACHE_KEY_CATEGORY, () => GetAllCategories());
        }

        public IEnumerable<MultiCampusReportDetails> GetMultiCastReportCardDetails(string studentIds, string testIds, string institutionIds, string testTypeIds)
        {
            IEnumerable<MultiCampusReportDetails> multiCastReportDetails = _reportRepository.GetMultiCastReportCardDetails(studentIds, testIds, institutionIds, testTypeIds);
            foreach (MultiCampusReportDetails srcd in multiCastReportDetails)
            {
                if (srcd.Product != null && srcd.Product.ProductId == (int)ProductType.SkillsModules)
                {
                    if (string.IsNullOrEmpty(srcd.RemediationTime))
                    {
                        srcd.RemediationTime = "N/A";
                    }

                    if (string.IsNullOrEmpty(srcd.Rank) || srcd.Rank == "0")
                    {
                        srcd.Rank = "N/A";
                        srcd.Ranking = 101;
                    }
                }
            }

            return multiCastReportDetails;
        }

        public IEnumerable<UserTest> GetTestByProdCohortId(string productIds, string cohortIds)
        {
            return _reportRepository.GetTestByProdCohortId(productIds, cohortIds).OrderBy(p => p.TestName);
        }

        private IDictionary<CategoryName, Category> GetAllCategories()
        {
            var allCategories = new Dictionary<CategoryName, Category>();
            IEnumerable<Category> categories = _reportRepository.GetCategories();
            foreach (var category in categories)
            {
                category.Details = _reportRepository.GetCategoryDetails(category.CategoryID, category.ProgramofStudyId).ToDictionary(k => k.Id);
                allCategories.Add(
                    (CategoryName)Enum.Parse(typeof(CategoryName), category.CategoryID.ToString()), category);
            }

            return allCategories;
        }

        private IEnumerable<Product> GetAllProducts()
        {
            return _reportRepository.GetProducts(0)
                .OrderBy(p => p.ProductId);
        }

        public IEnumerable<ProgramofStudy> GetProgramOfStudies()
        {
            return _adminService.GetProgramofStudies();
        }

        public List<Institution> GetInstitutions(int userId, int programofStudyId, string institutionIds)
        {
            return _reportRepository.GetInstitutions(userId, programofStudyId, institutionIds)
            .Where(s => s.Status.Equals("1") && s.InstitutionName.Trim().Length > 0)
            .ToList().OrderBy(i => i.InstitutionName).ToList();
        }

        public bool IsMultipleProgramofStudyAssignedToAdmin(int userId)
        {
            return _reportRepository.IsMultipleProgramofStudyAssignedToAdmin(userId);
        }

        public IDictionary<CategoryName, Category> GetCategories(int programOfStudyId)
        {
            return GetCategories().Where(cat => cat.Value.ProgramofStudyId == programOfStudyId).ToDictionary(cat => cat.Key, cat => cat.Value);
        }

        public IEnumerable<UserTest> GetTests(string productIds, string cohortIds, int programOfStudyId)
        {
            return _reportRepository.GetTests(productIds, cohortIds, "0", programOfStudyId)
                .OrderBy(p => p.TestName);
        }

        #endregion
    }
}
