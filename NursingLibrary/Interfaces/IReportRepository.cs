using System;
using System.Collections.Generic;
using System.Data;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IReportRepository
    {
        /// <summary>
        /// Gets Institution Details
        /// </summary>
        /// <param name="institutionId">If 0 returns all institutions. If multiple institutuions pass as comma seperated</param>
        /// <returns></returns>
        IEnumerable<Institution> GetInstitutionDetails(string institutionId);

        /// <summary>
        /// Returns list of tests for given products, cohort ids and program of study
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="cohortIds"></param>
        /// <returns></returns>
        IEnumerable<UserTest> GetTests(string productIds, string cohortIds, string studentIds, int programOfStudyId);

        /// <summary>
        /// Returns Student Summary By Question Details
        /// </summary>
        /// <param name="instituteId"></param>
        /// <param name="productId"></param>
        /// <param name="cohortIds"></param>
        /// <param name="testId"></param>
        /// <returns></returns>
        DataTable GetStudentSummaryByQuestionDetails(int instituteId, int productId, string cohortIds, int testId);

        DataTable GetStudentSummaryByQuestionHeader(int productId, string cohortIds, int testId);

        /// <summary>
        /// Get list of group details for Institution Id And Cohort Id
        /// </summary>
        /// <param name="institutionId"></param>
        /// <param name="cohortIds"></param>
        /// <returns></returns>
        IEnumerable<Group> GetGroups(int institutionId, string cohortIds);

        IEnumerable<Group> GetGroups(string institutionIds, string cohortIds);

        /// <summary>
        /// Get list of students in institute for given cohort and group ids
        /// </summary>
        /// <param name="institutionIds"></param>
        /// <param name="cohortIds"></param>
        /// <param name="groupIds"></param>
        /// <returns>IEnumerable<StudentEntity></returns>
        IEnumerable<StudentEntity> GetStudents(string institutionIds, string cohortIds, string groupIds, string searchCriteria);

        /// <summary>
        /// Get list of students in institute for given cohort and group ids
        /// </summary>
        /// <param name="institutionId"></param>
        /// <param name="cohortIds"></param>
        /// <param name="groupIds"></param>
        /// <returns>IEnumerable<StudentEntity></returns>
        IEnumerable<Test> GetTests(string cohortIds, string studentIds, string productIds, string groupIds, int institutionId);

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
        IEnumerable<StudentReportCardDetails> GetStudentReportCardDetails(string studentIds, string testIds, int institutionId, string testTypeId);

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
        IEnumerable<EnglishForNursingTracking> GetEnglishNursingTrackingDetails(string institutionId, string cohortIds, string studentIds, string testIds, string qIds);

        /// <summary>
        /// Gets the tests by cohort ID and product I ds.
        /// </summary>
        /// <param name="productIds">The product ids.</param>
        /// <param name="cohortIds">The cohort ids.</param>
        /// <returns>Collection of Test Entity</returns>
        IEnumerable<UserTest> GetTestsByCohortIDAndProductIDs(string productIds, string cohortIds);

        /// <summary>
        /// Gets the cohort by test details.
        /// </summary>
        /// <param name="institutionId">The institution id.</param>
        /// <param name="testsIds">The tests ids.</param>
        /// <param name="cohortIds">The cohort ids.</param>
        /// <param name="groupIds">The group ids.</param>
        /// <param name="productIds">The product ids.</param>
        /// <returns>Collection of CohortByTest Entity</returns>
        IEnumerable<CohortByTest> GetCohortByTestDetails(int institutionId, string testsIds, string cohortIds, string groupIds, string productIds);

        /// <summary>
        /// Get Results From Program
        /// </summary>
        /// <param name="userTestID"></param>
        /// <param name="charttype"></param>
        /// <returns></returns>
        ResultsFromTheProgram GetResultsFromTheProgram(int userTestId, int chartType);

        /// <summary>
        /// Get Test Characteristics
        /// </summary>
        /// <param name="testID"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        IEnumerable<string> GetTestCharacteristics(int userTestId, string userType);

        IEnumerable<string> GetTestCharacteristicsByTestId(int testID, string userType);

        /// <summary>
        /// Get Probability for test
        /// </summary>
        /// <param name="userTestId"></param>
        /// <param name="Correct"></param>
        /// <returns></returns>
        int GetProbability(int userTestId, int correct);

        /// <summary>
        /// Get Percentile Rank for test
        /// </summary>
        /// <param name="userTestId"></param>
        /// <param name="correct"></param>
        /// <returns></returns>
        int GetPercentileRank(int userTestId, int correct);

        /// <summary>
        /// Checks Probability Exist for test
        /// </summary>
        /// <param name="userTestId"></param>
        /// <returns></returns>
        int CheckProbabilityExist(int userTestId);

        /// <summary>
        /// Checks Percentile Rank Exist for test
        /// </summary>
        /// <param name="userTestId"></param>
        /// <returns></returns>
        int CheckPercentileRankExist(int userTestId);

        /// <summary>
        /// Get Results From Program for chart
        /// </summary>
        /// <param name="userTestID"></param>
        /// <param name="charttype"></param>
        /// <returns></returns>
        IEnumerable<ResultsFromTheProgramForChart> GetResultsFromTheProgramForChart(int userTestId, string chartType);

        /// <summary>
        /// Get Test Assignment
        /// </summary>
        /// <param name="testID"></param>
        /// <returns></returns>
        IEnumerable<TestCategory> GetTestAssignment(int userTestId);

        /// <summary>
        /// Get Remediation Time Details For Test
        /// </summary>
        /// <param name="userTestId"></param>
        /// <param name="typeOfFileId"></param>
        /// <returns></returns>
        IEnumerable<TestRemediationTimeDetails> GetRemediationTimeForTest(int userTestId, string typeOfFileId);

        IEnumerable<TestRemediationTimeDetails> GetRemediationTimeForNCLXTest(int userTestId, string typeOfFileId);

        IEnumerable<UserTest> GetTests(int userId, int productId);

        IEnumerable<UserTest> GetTests(string institutionIds, string productIds);

        IEnumerable<Cohort> GetCohortsForStudent(int studentId);

        IEnumerable<UserTest> GetUserTestByID(int userTestId);

        IEnumerable<TestRemediationExplainationDetails> GetTestRemediation(int userId, int productId, int institutionId, string cohortIds);

        IEnumerable<TestByInstitutionResults> GetTestByInstitutionResults(string institutionIds, string cohortIds, int productId, int testId);

        IEnumerable<SummaryPerformanceByQuestionResult> GetSummaryPerformanceByQuestionReportResult(String cohortIds, int productId, int testId);

        IEnumerable<CaseStudy> GetCaseStudies();

        IEnumerable<StudentEntity> GetListOfStudents(int cohortId, int institutionId, int caseId, string searchText);

        IEnumerable<StudentEntity> GetListOfStudents(int cohortId);

        IEnumerable<Modules> GetModule();

        IEnumerable<CaseByCohortResult> GetCaseByCohort(String institutionIds, int caseId, int moduleId);

        IEnumerable<ResultsFromTheCohortForChart> GetResultsFromTheCohotForChart(int institutionId, int subCategoryId, int chartType, string productIds, string tests, string cohortIds);

        IEnumerable<ResultsForStudentReportCardByModule> GetResultsForStudentReportCardByModule(string institutionIds, int caseId, string caseName, string moduleIds,
           int cohortId, string studentIds);

        CohortResultsByModule GetCohortResultsbyModule(int caseId, string moduleIds, string cohortIds);

        IEnumerable<CohortResultsByModule> GetCaseSubCategoryResultbyCohortModule(int caseId, string moduleIds, string cohortIds, string categoryName);

        IEnumerable<CategoryDetail> GetCaseSubCategories();

        IEnumerable<ResultsFromTheProgramForChart> GetResultsFromCohortForChart(string cohortIds, string testTypeIds, string testIds, string chartType, string fromDate, string toDate);

        IEnumerable<ResultsFromTheProgramForChart> GetResultsFromInstitutionForChart(string institutionIds, string testTypeIds, string testIds, string chartType, string fromDate, string toDate);

        ResultsFromTheProgram GetQuestionResultsForCohort(string cohortIds, string testTypes, string testIds, int chartType, string groupId);

        ResultsFromTheProgram GetQuestionResultsForInstitution(int institutionId, string testTypes, string testIds, int chartType);

        decimal GetNormForTest(int testId);

        decimal GetRankForTest(int testId);

        IEnumerable<ResultsFromTheProgram> GetResultsFromInstitutions(string institutionIds, int chartType, string testTypeIds, string testIds, string fromDate, string toDate);

        IEnumerable<ResultsFromTheCohortForChart> GetResultsForCohortsBySubCategoryChart(string cohorts, int categoryId, int subCategoryId, string cases, string modules, int institutionId);

        DataTable GetResultsForStudentSummaryByAnswerChoice(string cohortIds, int productId, int testId);

        DataTable GetResultsByCohortQuestions(string cohortIds, int productId, int testId);

        DataTable GetResultsByInstitutionQuestions(string cohortIds, int productId, int testId);

        int GetStudentNumberByCohortTest(int cohortId, int productId, int testId);

        IEnumerable<Institution> GetInstitutionByStudentID(int UserID);

        IEnumerable<ReportTestsScheduledbyDate> GetTestsScheduledByDate(string programOfStudyName,string institutionIds, string cohortIds, string groupIds, string productIds, DateTime? startDate, DateTime? endDate);

        IEnumerable<State> GetStates(int countryId, int stateId);


        /// <summary>
        /// Gets the Student Report Card test list for given product ids, cohort ids and student ids
        /// </summary>
        /// <param name="productIds"></param>
        /// <param name="cohortIds"></param>
        /// <param name="studentIds"></param>
        /// <returns></returns>
        IEnumerable<UserTest> GetTestsForStudentReportCard(string productIds, string studentIds);

        IEnumerable<UserTest> GetTestsForEnglishNursingTracking(string cohortIds, string studentIds);

        IEnumerable<Question> GetQIDForEnglishNursingTracking(string cohortIds, string studentIds, string testIds);

        IEnumerable<StudentEntity> GetStudents(string institutionIds, string cohortIds);

        List<Institution> GetInstitutions(int userId, string institutionIds);

        IEnumerable<Cohort> GetCohorts(int cohortId, string institutionIds);

        IEnumerable<Product> GetProducts(int productId);

        IEnumerable<Student> GetStudents(int studentId, string searchText);

        IEnumerable<Test> GetTests(int productId, int questionId, string institutionIds, bool forCMS, int programofStudy);

        IEnumerable<Category> GetCategories();

        /// <summary>
        /// Gets the category details for a given main category. Filters the list with main category's Program Of Study.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="programOfStudyIdForCategory"></param>
        /// <returns></returns>
        IEnumerable<CategoryDetail> GetCategoryDetails(int categoryId, int programOfStudyIdForCategory);

        IEnumerable<MultiCampusReportDetails> GetMultiCastReportCardDetails(string studentIds, string testIds, string institutionIds, string testTypeIds);

        IEnumerable<UserTest> GetTestByProdCohortId(string productIds, string cohortIds);

        IEnumerable<Test> GetTestsByProgramOfStudy(int productId, int programOfStudy);

        bool IsMultipleProgramofStudyAssignedToAdmin(int userId);

        List<Institution> GetInstitutions(int userId, int programofStudyId, string institutionIds);
    }
}