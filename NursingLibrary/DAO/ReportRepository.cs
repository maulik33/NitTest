using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;

namespace NursingLibrary.DAO
{
    public class ReportRepository : IReportRepository
    {
        #region Fields

        private readonly IDataContext _dataContext;
        private readonly IDataContext _cmsDataContext;

        #endregion Fields

        #region Constructors

        public ReportRepository(IDataContext cmsDataContext, IDataContext reportContext)
        {
            _cmsDataContext = cmsDataContext;
            _dataContext = reportContext;
        }

        #endregion Constructors

        #region IReportRepository Methods
        /// <summary>
        /// Gets Institution Details
        /// </summary>
        /// <param name="institutionId">If 0 returns all institutions. If multiple institutuions pass as comma seperated</param>
        /// <returns>IEnumerable<Institution></returns>
        public IEnumerable<Institution> GetInstitutionDetails(string institutionId)
        {
            var institutions = new List<Institution>();
            #region Sql Parameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@InstitutionID", SqlDbType.VarChar) { Value = institutionId };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetInstitutionDetailsById", sqlParameters))
            {
                while (reader.Read())
                {
                    var content = new Institution
                    {
                        InstitutionName = Convert.ToString(reader["InstitutionName"]),
                        InstitutionId = (reader["InstitutionID"] as int?).GetValueOrDefault(0),
                        ProgramOfStudyId = (reader["ProgramOfStudyId"] as int?).GetValueOrDefault(0)
                    };

                    institutions.Add(content);
                }
            }

            return institutions;
        }

        /// <summary>
        /// Returns list of tests for given product and cohort ids.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="cohortIds"></param>
        /// <returns></returns>
        public IEnumerable<UserTest> GetTests(string productIds, string cohortIds, string studentIds, int programOfStudyId)
        {
            var tests = new List<UserTest>();
            #region Sql Parameters
            var sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@ProductIDs", SqlDbType.VarChar) { Value = productIds };
            sqlParameters[1] = new SqlParameter("@CohortIDs", SqlDbType.VarChar) { Value = cohortIds };
            sqlParameters[2] = new SqlParameter("@StudentIds", SqlDbType.VarChar) { Value = studentIds };
            sqlParameters[3] = new SqlParameter("@InstitutionIds", SqlDbType.VarChar) { Value = string.Empty };
            sqlParameters[4] = new SqlParameter("@ProgramOfStudyId", SqlDbType.Int) { Value = programOfStudyId };

            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetTestsForCohortAndProduct", sqlParameters))
            {
                while (reader.Read())
                {
                    var test = new UserTest
                    {
                        TestName = Convert.ToString(reader["TestName"]),
                        TestId = (reader["TestId"] as int?).GetValueOrDefault(0),
                    };

                    tests.Add(test);
                }
            }

            return tests;
        }

        public IEnumerable<UserTest> GetTests(string institutionIds, string productIds)
        {
            var tests = new List<UserTest>();
            #region Sql Parameters
            var sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@ProductIDs", SqlDbType.VarChar) { Value = productIds };
            sqlParameters[1] = new SqlParameter("@CohortIDs", SqlDbType.VarChar) { Value = "0" };
            sqlParameters[2] = new SqlParameter("@StudentIds", SqlDbType.VarChar) { Value = "0" };
            sqlParameters[3] = new SqlParameter("@InstitutionIds", SqlDbType.VarChar) { Value = institutionIds };
            sqlParameters[4] = new SqlParameter("@ProgramOfStudyId", SqlDbType.Int) { Value = 0 };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetTestsForCohortAndProduct", sqlParameters))
            {
                while (reader.Read())
                {
                    var test = new UserTest
                    {
                        TestName = Convert.ToString(reader["TestName"]),
                        TestId = (reader["TestId"] as int?).GetValueOrDefault(0),
                    };

                    tests.Add(test);
                }
            }

            return tests;
        }

        public DataTable GetStudentSummaryByQuestionHeader(int productId, string cohortIds, int testId)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter { ParameterName = "@ProductId", SqlDbType = SqlDbType.Int, Value = productId };
            sqlParameters[1] = new SqlParameter { ParameterName = "@CohortIds", SqlDbType = SqlDbType.VarChar, Value = cohortIds };
            sqlParameters[2] = new SqlParameter { ParameterName = "@TestId", SqlDbType = SqlDbType.Int, Value = testId };

            #endregion

            return _dataContext.GetDataTable("USPReturnStudentSummaryByQuestionHeader", sqlParameters);
        }

        /// <summary>
        /// Returns Student Summary By Question Details
        /// </summary>
        /// <param name="instituteId"></param>
        /// <param name="productId"></param>
        /// <param name="cohortIds"></param>
        /// <param name="testId"></param>
        /// <returns></returns>
        public DataTable GetStudentSummaryByQuestionDetails(int instituteId, int productId, string cohortIds, int testId)
        {
            var studentSummaryByQuestionDetails = new List<ReportStudentSummaryByQuestionDetails>();
            #region Sql Parameters
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@InstitutionId", SqlDbType.Int, 4) { Value = instituteId };
            sqlParameters[1] = new SqlParameter("@ProductID", SqlDbType.Int, 4) { Value = productId };
            sqlParameters[2] = new SqlParameter("@CohortIDs", SqlDbType.VarChar, 4000) { Value = cohortIds };
            sqlParameters[3] = new SqlParameter("@TestID", SqlDbType.Int, 4) { Value = testId };
            #endregion

            // return _dataContext.GetDataTable("USPGetDataForStudentSummaryByQuestionReport", sqlParameters);
            return _dataContext.GetDataTable("uspReturnStudentSummaryByQuestion", sqlParameters);
        }

        /// <summary>
        /// Get list of group details for Institution Id And Cohort Id
        /// </summary>
        /// <param name="institutionId"></param>
        /// <param name="cohortIds"></param>
        /// <returns></returns>
        public IEnumerable<Group> GetGroups(int institutionId, string cohortIds)
        {
            var groups = new List<Group>();
            #region Sql Parameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@InstitutionID", SqlDbType.Int, 4) { Value = institutionId };
            sqlParameters[1] = new SqlParameter("@CohortIds", SqlDbType.VarChar) { Value = cohortIds };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetGroupByInstitutionIdAndCohortId", sqlParameters))
            {
                while (reader.Read())
                {
                    var group = new Group
                    {
                        GroupName = Convert.ToString(reader["GroupName"]),
                        GroupId = (reader["GroupId"] as int?).GetValueOrDefault(0),
                    };

                    groups.Add(group);
                }
            }

            return groups;
        }

        public IEnumerable<Group> GetGroups(string institutionIds, string cohortIds)
        {
            var groups = new List<Group>();
            #region Sql Parameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@InstitutionIds", SqlDbType.VarChar) { Value = institutionIds };
            sqlParameters[1] = new SqlParameter("@CohortIds", SqlDbType.VarChar) { Value = cohortIds };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetGroupsByInstitutionIdsAndCohortIds", sqlParameters))
            {
                while (reader.Read())
                {
                    var group = new Group
                    {
                        GroupName = Convert.ToString(reader["GroupName"]),
                        GroupId = (reader["GroupId"] as int?).GetValueOrDefault(0),
                    };

                    groups.Add(group);
                }
            }

            return groups;
        }

        /// <summary>
        /// Get list of students in institute for given cohort and group ids
        /// </summary>
        /// <param name="institutionId"></param>
        /// <param name="cohortIds"></param>
        /// <param name="groupIds"></param>
        /// <returns>IEnumerable<StudentEntity></returns>
        public IEnumerable<StudentEntity> GetStudents(string institutionIds, string cohortIds, string groupIds, string searchCriteria)
        {
            var students = new List<StudentEntity>();
            string value = string.Empty;
            #region Sql Parameters
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@InstitutionIds", SqlDbType.VarChar) { Value = institutionIds };
            sqlParameters[1] = new SqlParameter("@CohortIds", SqlDbType.VarChar) { Value = cohortIds };
            sqlParameters[2] = new SqlParameter("@GroupIds", SqlDbType.VarChar) { Value = groupIds };
            sqlParameters[3] = new SqlParameter("@SearchText", SqlDbType.VarChar) { Value = searchCriteria };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetStudentsInInstitutionByCohortAndGroups", sqlParameters))
            {
                while (reader.Read())
                {
                    var student = new StudentEntity
                    {
                        StudentName = Convert.ToString(reader["Name"]),
                        StudentId = (reader["UserId"] as int?).GetValueOrDefault(0),
                        FirstName = Convert.ToString(reader["FirstName"]),
                        LastName = Convert.ToString(reader["LastName"]),
                        UserName = Convert.ToString(reader["UserName"]),
                        Email = Convert.ToString(reader["Email"]),
                        InstitutionStatus = Convert.ToBoolean(Convert.ToInt32(reader["Status"]))
                    };

                    students.Add(student);
                }
            }

            return students;
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
            var tests = new List<Test>();
            #region Sql Parameters
            var sqlParameters = new SqlParameter[5];

            sqlParameters[0] = new SqlParameter("@tProductIds", SqlDbType.VarChar, 4000) { Value = productIds };
            sqlParameters[1] = new SqlParameter("@tCohortIds", SqlDbType.VarChar, 4000) { Value = cohortIds };
            sqlParameters[2] = new SqlParameter("@tStudentIds", SqlDbType.VarChar, 4000) { Value = studentIds };
            sqlParameters[3] = new SqlParameter("@GroupIds", SqlDbType.VarChar, 4000) { Value = groupIds };
            sqlParameters[4] = new SqlParameter("@InstitutionID", SqlDbType.Int, 4) { Value = institutionId };

            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetTestsByProdCohortUserIds", sqlParameters))
            {
                while (reader.Read())
                {
                    var test = new Test
                    {
                        TestName = Convert.ToString(reader["Name"]),
                        TestId = (reader["UserId"] as int?).GetValueOrDefault(0),
                    };

                    tests.Add(test);
                }
            }

            return tests;
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
            var studentReportCardDetails = new List<StudentReportCardDetails>();
            #region Sql Parameters
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@StudentIds", SqlDbType.VarChar) { Value = studentIds };
            sqlParameters[1] = new SqlParameter("@testIds", SqlDbType.VarChar) { Value = testIds };
            sqlParameters[2] = new SqlParameter("@InstitutionID", SqlDbType.Int, 4) { Value = institutionId };
            sqlParameters[3] = new SqlParameter("@testTypeId", SqlDbType.VarChar) { Value = testTypeId };

            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetStudentReportCardDetails", sqlParameters))
            {
                while (reader.Read())
                {
                    var studentReportCardDetail = new StudentReportCardDetails
                    {
                        Student = new StudentEntity()
                        {
                            FirstName = Convert.ToString(reader["FirstName"]),
                            LastName = Convert.ToString(reader["LastName"]),
                            StudentId = (reader["UserId"] as int?).GetValueOrDefault(0),
                        },
                        Cohort = new Cohort
                        {
                            CohortId = (reader["CohortId"] as int?).GetValueOrDefault(0),
                            CohortName = Convert.ToString(reader["CohortName"]),
                        },
                        Product = new Product
                        {
                            ProductId = (reader["TestTypeId"] as int?).GetValueOrDefault(0),
                            ProductName = Convert.ToString(reader["TestType"]),
                        },
                        Group = new Group
                        {
                            GroupName = Convert.ToString(reader["GroupName"]),
                        },
                        TestId = (reader["TestId"] as int?).GetValueOrDefault(0),
                        TestName = Convert.ToString(reader["TestName"]),
                        TestTaken = Convert.ToDateTime(reader["TestTaken"]),
                        RemediationTime = Convert.ToString(reader["RemediationTime"]),
                        Correct = Convert.ToDecimal(reader["Correct"]),
                        UserTestId = (reader["UserTestId"] as int?).GetValueOrDefault(0),
                        Rank = Convert.ToString(reader["Rank"]),
                        TimeUsed = reader["TimeUsed"] as string ?? string.Empty,
                        QuestionCount = (reader["QuestionCount"] as int?) ?? 0,
                        Ranking = (Convert.ToString(reader["Rank"]) == "n/a") ? 0 : Convert.ToInt32(reader["Rank"].ToString() == string.Empty ? "0" : reader["Rank"].ToString()),
                        TestStyle = reader["TestStyle"] as string ?? string.Empty,
                    };

                    studentReportCardDetails.Add(studentReportCardDetail);
                }
            }

            return studentReportCardDetails;
        }

        /// <summary>
        /// Gets the tests by cohort ID and product I ds.
        /// </summary>
        /// <param name="productIds">The product ids.</param>
        /// <param name="cohortIds">The cohort ids.</param>
        /// <returns>Collection of Test Entity</returns>
        public IEnumerable<UserTest> GetTestsByCohortIDAndProductIDs(string productIds, string cohortIds)
        {
            var tests = new List<UserTest>();
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter { ParameterName = "@ProductIds", SqlDbType = SqlDbType.NVarChar, Value = productIds };
            sqlParameters[1] = new SqlParameter { ParameterName = "@CohortIds", SqlDbType = SqlDbType.NVarChar, Value = cohortIds };

            using (IDataReader reader = _dataContext.GetDataReader("USPGetTestsByCohortIDAndProductIDs", sqlParameters))
            {
                while (reader.Read())
                {
                    tests.Add(new UserTest { TestId = (reader["TestId"] as int?).GetValueOrDefault(0), TestName = reader["TestName"].ToString() });
                }
            }

            return tests;
        }

        /// <summary>
        /// Gets the cohort by test details.
        /// </summary>
        /// <param name="institutionId">The institution id.</param>
        /// <param name="testsIds">The tests ids.</param>
        /// <param name="cohortIds">The cohort ids.</param>
        /// <param name="groupIds">The group ids.</param>
        /// <param name="productIds">The product ids.</param>
        /// <returns>Collection of CohortByTest Entity</returns>
        public IEnumerable<CohortByTest> GetCohortByTestDetails(int institutionId, string testsIds, string cohortIds, string groupIds, string productIds)
        {
            var cohortByTestList = new List<CohortByTest>();
            var sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter { ParameterName = "@TestIds", SqlDbType = SqlDbType.VarChar, Value = testsIds };
            sqlParameters[1] = new SqlParameter { ParameterName = "@CohortIds", SqlDbType = SqlDbType.VarChar, Value = cohortIds };
            sqlParameters[2] = new SqlParameter { ParameterName = "@GroupIds", SqlDbType = SqlDbType.VarChar, Value = groupIds };
            sqlParameters[3] = new SqlParameter { ParameterName = "@InstitutionId", SqlDbType = SqlDbType.VarChar, Value = institutionId };
            sqlParameters[4] = new SqlParameter { ParameterName = "@ProductIds", SqlDbType = SqlDbType.VarChar, Value = productIds };

            using (IDataReader reader = _dataContext.GetDataReader("USPGetDetailsForCohortByTest", sqlParameters))
            {
                while (reader.Read())
                {
                    cohortByTestList.Add(
                        new CohortByTest
                        {
                            CohortId = (reader["CohortId"] as int?).GetValueOrDefault(0),
                            CohortName = Convert.ToString(reader["CohortName"]),
                            InstitutionID = (reader["InstitutionID"] as int?).GetValueOrDefault(0),
                            NormedPercCorrect = (reader["NormedPercCorrect"] as float?).GetValueOrDefault(0),
                            NStudents = (reader["NStudents"] as int?).GetValueOrDefault(0),
                            Percentage = (reader["Percantage"] as decimal?).GetValueOrDefault(0),
                            ProductID = (reader["ProductID"] as int?).GetValueOrDefault(0),
                            TestId = (reader["TestId"] as int?).GetValueOrDefault(0),
                            TestName = reader["TestName"].ToString()
                        });
                }
            }

            return cohortByTestList;
        }

        /// <summary>
        /// Get Results From Program
        /// </summary>
        /// <param name="userTestID"></param>
        /// <param name="charttype"></param>
        /// <returns></returns>
        public ResultsFromTheProgram GetResultsFromTheProgram(int userTestId, int chartType)
        {
            ResultsFromTheProgram resultsFromTheProgram = new ResultsFromTheProgram();
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter { ParameterName = "@userTestID", SqlDbType = SqlDbType.Int, Value = userTestId };
            sqlParameters[1] = new SqlParameter { ParameterName = "@charttype", SqlDbType = SqlDbType.Int, Value = chartType };

            using (IDataReader reader = _dataContext.GetDataReader("USPGetResultsFromTheProgram", sqlParameters))
            {
                if (reader.Read())
                {
                    if (chartType == 1)
                    {
                        resultsFromTheProgram = new ResultsFromTheProgram
                        {
                            Total = Convert.ToDecimal(reader["Total"]),
                        };
                    }
                    else if (chartType == 2)
                    {
                        resultsFromTheProgram = new ResultsFromTheProgram
                        {
                            NCorrect = (reader["N_Correct"] as int?).GetValueOrDefault(0),
                            NInCorrect = (reader["N_InCorrect"] as int?).GetValueOrDefault(0),
                            NAnswered = (reader["N_NAnswered"] as int?).GetValueOrDefault(0),
                            CI = (reader["N_CI"] as int?).GetValueOrDefault(0),
                            II = (reader["N_II"] as int?).GetValueOrDefault(0),
                            IC = (reader["N_IC"] as int?).GetValueOrDefault(0)
                        };
                    }
                }
            }

            return resultsFromTheProgram;
        }

        /// <summary>
        /// Get Test Characteristics
        /// </summary>
        /// <param name="TestID"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public IEnumerable<string> GetTestCharacteristics(int userTestID, string userType)
        {
            var result = new List<string>();
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter { ParameterName = "@UserTestID", SqlDbType = SqlDbType.Int, Value = userTestID };
            sqlParameters[1] = new SqlParameter { ParameterName = "@UserType", SqlDbType = SqlDbType.VarChar, Value = userType };

            using (IDataReader reader = _dataContext.GetDataReader("USPReturnTestCharacteristicsForUserAdminByUserTestID", sqlParameters))
            {
                while (reader.Read())
                {
                    switch (reader["TableName"].ToString().Trim())
                    {
                        case "LevelOfDifficulty":
                        case "NursingProcess":
                        case "ClinicalConcept":
                        case "Demographic":
                        case "ClientNeeds":
                        case "ClientNeedCategory":
                        case "CognitiveLevel":
                        case "SpecialtyArea":
                        case "Systems":
                        case "CriticalThinking":
                        case "AccreditationCategories":
                        case "QSENKSACompetencies":
                        case "Concepts":
                        case "Reading":
                        case "Math":
                        case "Writing":
                            result.Add(reader["TableName"].ToString().Trim());
                            break;
                    }
                }
            }

            return result;
        }

        public IEnumerable<string> GetTestCharacteristicsByTestId(int testID, string userType)
        {
            var result = new List<string>();
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter { ParameterName = "@TestID", SqlDbType = SqlDbType.Int, Value = testID };
            sqlParameters[1] = new SqlParameter { ParameterName = "@UserType", SqlDbType = SqlDbType.VarChar, Value = userType };

            using (IDataReader reader = _dataContext.GetDataReader("USPReturnTestCharacteristicsForUserAdminByTestID", sqlParameters))
            {
                while (reader.Read())
                {
                    switch (reader["TableName"].ToString().Trim())
                    {
                        case "LevelOfDifficulty":
                        case "NursingProcess":
                        case "ClinicalConcept":
                        case "Demographic":
                        case "ClientNeeds":
                        case "ClientNeedCategory":
                        case "CognitiveLevel":
                        case "SpecialtyArea":
                        case "Systems":
                        case "CriticalThinking":
                        case "AccreditationCategories":
                        case "QSENKSACompetencies":
                        case "Concepts":
                        case "Reading":
                        case "Math":
                        case "Writing":
                            result.Add(reader["TableName"].ToString().Trim());
                            break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Get Probability for test
        /// </summary>
        /// <param name="userTestId"></param>
        /// <param name="Correct"></param>
        /// <returns></returns>
        public int GetProbability(int userTestId, int correct)
        {
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter { ParameterName = "@UserTestId", SqlDbType = SqlDbType.Int, Value = userTestId };
            sqlParameters[1] = new SqlParameter { ParameterName = "@NumberCorrect", SqlDbType = SqlDbType.Float, Value = correct };

            return (_dataContext.ExecuteScalar("USPReturnProbability", sqlParameters) as int?).GetValueOrDefault(0);
        }

        /// <summary>
        /// Get Percentile Rank for test
        /// </summary>
        /// <param name="testId"></param>
        /// <param name="correct"></param>
        /// <returns></returns>
        public int GetPercentileRank(int userTestId, int correct)
        {
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter { ParameterName = "@UserTestId", SqlDbType = SqlDbType.Int, Value = userTestId };
            sqlParameters[1] = new SqlParameter { ParameterName = "@NumberCorrect", SqlDbType = SqlDbType.Float, Value = correct };

            return (int)(_dataContext.ExecuteScalar("USPReturnPercentileRank", sqlParameters) as double?).GetValueOrDefault(0);
        }

        /// <summary>
        /// Checks Probability Exist for test
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        public int CheckProbabilityExist(int userTestId)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter { ParameterName = "@UserTestId", SqlDbType = SqlDbType.Int, Value = userTestId };

            return (_dataContext.ExecuteScalar("USPCheckProbabilityExist", sqlParameters) as int?) ?? 0;
        }

        /// <summary>
        /// Checks Percentile Rank Exist for test
        /// </summary>
        /// <param name="userTestId"></param>
        /// <returns></returns>
        public int CheckPercentileRankExist(int userTestId)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter { ParameterName = "@UserTestID", SqlDbType = SqlDbType.Int, Value = userTestId };

            return (_dataContext.ExecuteScalar("USPCheckPercentileRankExist", sqlParameters) as int?) ?? 0;
        }

        /// <summary>
        /// Get Results From Program for chart
        /// </summary>
        /// <param name="userTestID"></param>
        /// <param name="charttype"></param>
        /// <returns></returns>
        public IEnumerable<ResultsFromTheProgramForChart> GetResultsFromTheProgramForChart(int userTestId, string chartType)
        {
            var result = new List<ResultsFromTheProgramForChart>();
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter { ParameterName = "@userTestID", SqlDbType = SqlDbType.Int, Value = userTestId };
            sqlParameters[1] = new SqlParameter { ParameterName = "@charttype", SqlDbType = SqlDbType.VarChar, Value = chartType };

            using (IDataReader reader = _dataContext.GetDataReader("USPGetResultsFromTheProgramForChart", sqlParameters))
            {
                while (reader.Read())
                {
                    result.Add(new ResultsFromTheProgramForChart
                    {
                        Total = (reader["Total"] as int?).GetValueOrDefault(0),
                        NCorrect = (reader["Correct_N"] as int?).GetValueOrDefault(0),

                        // LevelOfDifficulty = Convert.ToString(reader["LevelOfDifficulty"]),
                        ItemText = Convert.ToString(reader["ItemText"]),
                        Norm = (decimal)(reader["Norm"] as Single?).GetValueOrDefault()
                    });
                }
            }

            return result;
        }

        /// <summary>
        /// Get Test Assignment
        /// </summary>
        /// <param name="testID"></param>
        /// <returns></returns>
        public IEnumerable<TestCategory> GetTestAssignment(int userTestId)
        {
            var result = new List<TestCategory>();
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter { ParameterName = "@UserTestID", SqlDbType = SqlDbType.Int, Value = userTestId };

            using (IDataReader reader = _dataContext.GetDataReader("USPGetTestAssignment", sqlParameters))
            {
                while (reader.Read())
                {
                    result.Add(
                        new TestCategory
                        {
                            OrderNumber = (reader["OrderNumber"] as int?).GetValueOrDefault(0),
                            TableName = Convert.ToString(reader["TableName"]),
                            IsAdmin = Convert.ToBoolean(reader["Admin"]),
                        });
                }
            }

            return result;
        }

        /// <summary>
        /// Get Remediation Time Details For Test
        /// </summary>
        /// <param name="userTestId"></param>
        /// <param name="typeOfFileId"></param>
        /// <returns></returns>
        public IEnumerable<TestRemediationTimeDetails> GetRemediationTimeForTest(int userTestId, string typeOfFileId)
        {
            var testRemediationTimeDetails = new List<TestRemediationTimeDetails>();
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter { ParameterName = "@userTestID", SqlDbType = SqlDbType.Int, Value = userTestId };
            sqlParameters[1] = new SqlParameter { ParameterName = "@TypeOfFileID", SqlDbType = SqlDbType.VarChar, Value = typeOfFileId };

            using (IDataReader reader = _dataContext.GetDataReader("USPGetRemediationTimeByTestId", sqlParameters))
            {
                while (reader.Read())
                {
                    testRemediationTimeDetails.Add(new TestRemediationTimeDetails
                    {
                        ClientNeedsCategoryId = (reader["ClientNeedsCategoryID"] as int?).GetValueOrDefault(0),
                        QId = (reader["QID"] as int?).GetValueOrDefault(0),
                        QuestionId = Convert.ToString(reader["QuestionID"]),
                        QuestionType = Convert.ToString(reader["QuestionType"]),
                        RemediationId = Convert.ToString(reader["RemediationID"]),
                        TopicTitle = Convert.ToString(reader["TopicTitle"]),
                        QuestionNumber = (reader["QuestionNumber"] as int?).GetValueOrDefault(0),
                        TypeOfFileId = Convert.ToString(reader["TypeOfFileID"]),
                        LevelOfDifficulty = Convert.ToString(reader["LevelOfDifficulty"]),
                        NursingProcess = Convert.ToString(reader["NursingProcess"]),
                        ClinicalConcept = Convert.ToString(reader["ClinicalConcept"]),
                        SpecialtyAreaId = Convert.ToString(reader["SpecialtyAreaID"]),
                        Demographic = Convert.ToString(reader["Demographic"]),
                        NursingProcessId = (reader["NursingProcessID"] as int?).GetValueOrDefault(0),
                        TimeSpendForQuestion = (reader["TimeSpendForQuestion"] as int?).GetValueOrDefault(0),
                        TimeSpendForExplanation = (reader["TimeSpendForExplanation"] as int?).GetValueOrDefault(0),
                        Correct = (reader["Correct"] as int?).GetValueOrDefault(0),
                        TestName = Convert.ToString(reader["TestName"]),
                        TimeSpendForRemedation = (reader["TimeSpendForRemedation"] as int?).GetValueOrDefault(0),
                        ClientNeeds = Convert.ToString(reader["ClientNeeds"]),
                        ClientNeedsId = (reader["ClientNeedsID"] as int?).GetValueOrDefault(0),
                        ClinicalConceptsId = Convert.ToString(reader["ClinicalConceptsID"]),
                        LevelOfDifficultyId = Convert.ToString(reader["LevelOfDifficultyID"]),
                        CriticalThinkingId = Convert.ToString(reader["CriticalThinkingID"]),
                        CriticalThinking = Convert.ToString(reader["CriticalThinking"]),
                        CognitiveLevelId = Convert.ToString(reader["CognitiveLevelID"]),
                        CognitiveLevel = Convert.ToString(reader["CognitiveLevel"]),
                        SpecialtyArea = Convert.ToString(reader["SpecialtyArea"]),
                        SystemId = Convert.ToString(reader["SystemID"]),
                        System = Convert.ToString(reader["System"]),
                        AccreditationCategoriesId = Convert.ToString(reader["AccreditationCategoriesID"]),
                        AccreditationCategories = Convert.ToString(reader["AccreditationCategories"]),
                        QSENKSACompetenciesId = Convert.ToString(reader["QSENKSACompetenciesID"]),
                        QSENKSACompetencies = Convert.ToString(reader["QSENKSACompetencies"]),
                        ClientNeedCategory = Convert.ToString(reader["ClientNeedCategory"]),
                        Concepts = Convert.ToString(reader["Concepts"]),
                    });
                }
            }

            return testRemediationTimeDetails;
        }

        /// <summary>
        /// Get Remediation Time Details For NCLX Test
        /// </summary>
        /// <param name="userTestId"></param>
        /// <param name="typeOfFileId"></param>
        /// <returns></returns>
        public IEnumerable<TestRemediationTimeDetails> GetRemediationTimeForNCLXTest(int userTestId, string typeOfFileId)
        {
            var testRemediationTimeDetails = new List<TestRemediationTimeDetails>();
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter { ParameterName = "@userTestID", SqlDbType = SqlDbType.Int, Value = userTestId };
            sqlParameters[1] = new SqlParameter { ParameterName = "@TypeOfFileID", SqlDbType = SqlDbType.VarChar, Value = typeOfFileId };

            using (IDataReader reader = _dataContext.GetDataReader("USPGetRemediationTimeForNCLEX", sqlParameters))
            {
                while (reader.Read())
                {
                    testRemediationTimeDetails.Add(new TestRemediationTimeDetails
                    {
                        ClientNeedsCategoryId = (reader["ClientNeedsCategoryID"] as int?).GetValueOrDefault(0),
                        QId = (reader["QID"] as int?).GetValueOrDefault(0),
                        QuestionId = Convert.ToString(reader["QuestionID"]),
                        QuestionType = Convert.ToString(reader["QuestionType"]),
                        RemediationId = Convert.ToString(reader["RemediationID"]),
                        TopicTitle = Convert.ToString(reader["TopicTitle"]),
                        QuestionNumber = (reader["QuestionNumber"] as int?).GetValueOrDefault(0),
                        TypeOfFileId = Convert.ToString(reader["TypeOfFileID"]),
                        NursingProcess = Convert.ToString(reader["NursingProcess"]),
                        Demographic = Convert.ToString(reader["Demographic"]),
                        TimeSpendForQuestion = (reader["TimeSpendForQuestion"] as int?).GetValueOrDefault(0),
                        TimeSpendForExplanation = (reader["TimeSpendForExplanation"] as int?).GetValueOrDefault(0),
                        Correct = (reader["Correct"] as int?).GetValueOrDefault(0),
                        TestName = Convert.ToString(reader["TestName"]),
                        TimeSpendForRemedation = (reader["TimeSpendForRemedation"] as int?).GetValueOrDefault(0),
                        ClientNeeds = Convert.ToString(reader["ClientNeeds"]),
                        ClientNeedCategory = Convert.ToString(reader["ClientNeedCategory"]),
                    });
                }
            }

            return testRemediationTimeDetails;
        }

        public IEnumerable<UserTest> GetTests(int userId, int productId)
        {
            var result = new List<UserTest>();
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter { ParameterName = "@UserID", SqlDbType = SqlDbType.Int, Value = userId };
            sqlParameters[1] = new SqlParameter { ParameterName = "@ProductID", SqlDbType = SqlDbType.Int, Value = productId };

            using (IDataReader reader = _dataContext.GetDataReader("USPReturnTestsByProductAndUser", sqlParameters))
            {
                while (reader.Read())
                {
                    result.Add(new UserTest
                    {
                        UserTestId = (reader["UserTestID"] as int?).GetValueOrDefault(0),
                        TestName = Convert.ToString(reader["TestName"]),
                    });
                }
            }

            return result;
        }

        public IEnumerable<Cohort> GetCohortsForStudent(int studentId)
        {
            var cohorts = new List<Cohort>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@StudentId", SqlDbType.Int, 4) { Value = studentId };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetCohortsByStudentID", sqlParameters))
            {
                while (reader.Read())
                {
                    cohorts.Add(new Cohort
                    {
                        CohortName = (reader["CohortName"] as string) ?? string.Empty,
                        CohortId = (reader["CohortID"] as int?) ?? 0,
                    });
                }
            }

            return cohorts;
        }

        public IEnumerable<UserTest> GetUserTestByID(int userTestId)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@UserTestID", SqlDbType.Int, 4) { Value = userTestId };

            var userTests = new List<UserTest>();
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("uspGetUserTestByID", sqlParameters))
            {
                while (reader.Read())
                {
                    userTests.Add(new UserTest
                    {
                        TestStarted = (reader["TestStarted"] as DateTime?) ?? DateTime.MinValue,
                        TestId = (reader["TestID"] as int?) ?? 0,
                        SuspendType = (reader["SuspendType"] as string) ?? string.Empty,
                        UserTestId = (reader["UserTestID"] as int?) ?? 0,
                        TestName = (reader["TestName"] as string) ?? string.Empty,
                        TestNumber = (reader["TestNumber"] as int?) ?? 0,
                        SuspendQID = (reader["SuspendQID"] as int?) ?? 0,
                        TimedTest = (reader["TimedTest"] as int?) ?? 0,
                        TimeRemaining = (reader["TimeRemaining"] as string) ?? string.Empty,
                        TutorMode = (reader["TutorMode"] as int?) ?? 0
                    });
                }
            }

            return userTests;
        }

        public IEnumerable<TestRemediationExplainationDetails> GetTestRemediation(int userId, int productId, int institutionId, string cohortIds)
        {
            var result = new List<TestRemediationExplainationDetails>();
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@userID", SqlDbType.Int, 4) { Value = userId };
            sqlParameters[1] = new SqlParameter("@ProductID", SqlDbType.Int, 4) { Value = productId };
            sqlParameters[2] = new SqlParameter("@institutionID", SqlDbType.Int, 4) { Value = institutionId };
            sqlParameters[3] = new SqlParameter("@cohortIDs", SqlDbType.VarChar) { Value = cohortIds };

            using (IDataReader reader = _dataContext.GetDataReader("uspGetRemediationByTest", sqlParameters))
            {
                while (reader.Read())
                {
                    result.Add(new TestRemediationExplainationDetails
                    {
                        CohortName = Convert.ToString(reader["CohortName"]),
                        TestName = Convert.ToString(reader["TestName"]),
                        RemediationOrExplaination = Convert.ToString(reader["Remedation"]),
                        ProductId = (reader["ProductId"] as int?) ?? 0
                    });
                }
            }

            return result;
        }

        public IEnumerable<TestByInstitutionResults> GetTestByInstitutionResults(string institutionIds, string cohortIds, int productId, int testId)
        {
            var result = new List<TestByInstitutionResults>();
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@InstitutionIds", SqlDbType.VarChar) { Value = institutionIds };
            sqlParameters[1] = new SqlParameter("@ProductID", SqlDbType.Int, 4) { Value = productId };
            sqlParameters[2] = new SqlParameter("@cohortIds", SqlDbType.VarChar) { Value = cohortIds };
            sqlParameters[3] = new SqlParameter("@TestId", SqlDbType.Int, 4) { Value = testId };

            using (IDataReader reader = _dataContext.GetDataReader("USPGetTestByInstitutionResults", sqlParameters))
            {
                while (reader.Read())
                {
                    result.Add(new TestByInstitutionResults
                    {
                        Cohort = new Cohort
                        {
                            CohortId = (reader["CohortID"] as int?) ?? 0,
                            CohortName = Convert.ToString(reader["CohortName"]),
                        },
                        Institution = new Institution
                        {
                            InstitutionId = (reader["InstitutionID"] as int?) ?? 0,
                            InstitutionName = string.Format("{0} - {1}", (reader["InstitutionName"] as string) ?? string.Empty, (reader["ProgramofStudyName"] as string) ?? string.Empty),
                        },
                        NStudents = (reader["NStudents"] as int?) ?? 0,
                        Percantage = (reader["Percantage"] as decimal?) ?? 0,
                        Normed = Convert.ToString(reader["Normed"])
                    });
                }
            }

            return result;
        }

        public IEnumerable<SummaryPerformanceByQuestionResult> GetSummaryPerformanceByQuestionReportResult(String cohortIds, int productId, int testId)
        {
            var result = new List<SummaryPerformanceByQuestionResult>();
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@ProductID", SqlDbType.Int, 4) { Value = productId };
            sqlParameters[1] = new SqlParameter("@cohortId", SqlDbType.VarChar) { Value = cohortIds };
            sqlParameters[2] = new SqlParameter("@TestId", SqlDbType.Int, 4) { Value = testId };

            using (IDataReader reader = _dataContext.GetDataReader("USPReturnSummaryPerformanceByQuestionReport", sqlParameters))
            {
                while (reader.Read())
                {
                    result.Add(new SummaryPerformanceByQuestionResult
                    {
                        Answer = Convert.ToString(reader["Answer"]),
                        QuestionId = Convert.ToString(reader["QuestionId"]),
                        Total1 = (reader["Total1"] as int?) ?? 0,
                        Total2 = (reader["Total2"] as int?) ?? 0,
                        Total3 = (reader["Total3"] as int?) ?? 0,
                        Total4 = (reader["Total4"] as int?) ?? 0,
                        TotalN = (reader["TotalN"] as int?) ?? 0,
                        TotalNumberCorrect = (reader["Total#Correct"] as int?) ?? 0,
                        TotalNumberWrong = (reader["Total#Wrong"] as int?) ?? 0,
                        StudentNumber = (reader["StudentNumber"] as int?) ?? 0,
                        CorrectPercent = (reader["CorrectPercent"] as decimal?) ?? 0,
                    });
                }
            }

            return result;
        }

        public IEnumerable<CaseStudy> GetCaseStudies()
        {
            var caseStudies = new List<CaseStudy>();
            using (IDataReader reader = _dataContext.GetDataReader("uspGetCaseStudies"))
            {
                while (reader.Read())
                {
                    caseStudies.Add(new CaseStudy { CaseId = (reader["CaseID"] as int?) ?? 0, CaseName = (reader["CaseName"] as string) ?? string.Empty, CaseOrder = (reader["CaseOrder"] as int?) ?? 0 });
                }
            }

            return caseStudies;
        }

        public IEnumerable<StudentEntity> GetListOfStudents(int cohortId, int institutionId, int caseId, string searchText)
        {
            var result = new List<StudentEntity>();
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@CohortId", SqlDbType.Int, 4) { Value = cohortId };
            sqlParameters[1] = new SqlParameter("@InstitutionId", SqlDbType.Int, 4) { Value = institutionId };
            sqlParameters[2] = new SqlParameter("@CaseId", SqlDbType.Int, 4) { Value = caseId };
            sqlParameters[3] = new SqlParameter("@sText", SqlDbType.VarChar) { Value = searchText };

            using (IDataReader reader = _dataContext.GetDataReader("USPGetListOfStudents", sqlParameters))
            {
                while (reader.Read())
                {
                    result.Add(new StudentEntity
                    {
                        LastName = Convert.ToString(reader["LastName"]),
                        FirstName = Convert.ToString(reader["FirstName"]),
                        UserName = Convert.ToString(reader["UserName"]),
                        StudentId = (reader["UserID"] as int?) ?? 0,
                        EnrollmentId = Convert.ToString(reader["EnrollmentID"]),
                    });
                }
            }

            return result;
        }

        public IEnumerable<StudentEntity> GetListOfStudents(int cohortId)
        {
            var result = new List<StudentEntity>();
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@CohortId", SqlDbType.Int, 4) { Value = cohortId };

            using (IDataReader reader = _dataContext.GetDataReader("USPGetListOfStudentsByCohortID", sqlParameters))
            {
                while (reader.Read())
                {
                    result.Add(new StudentEntity
                    {
                        LastName = Convert.ToString(reader["LastName"]),
                        FirstName = Convert.ToString(reader["FirstName"]),
                        StudentName = Convert.ToString(reader["Name"]),
                        UserName = Convert.ToString(reader["UserName"]),
                        StudentId = (reader["UserID"] as int?) ?? 0,
                    });
                }
            }

            return result;
        }

        public IEnumerable<Modules> GetModule()
        {
            var result = new List<Modules>();
            using (IDataReader reader = _dataContext.GetDataReader("USPGetModule"))
            {
                while (reader.Read())
                {
                    result.Add(new Modules
                    {
                        ModuleName = Convert.ToString(reader["ModuleName"]),
                        ModuleId = (reader["ModuleId"] as int?) ?? 0,
                    });
                }
            }

            return result;
        }

        public IEnumerable<CaseByCohortResult> GetCaseByCohort(String institutionIds, int caseId, int moduleId)
        {
            var result = new List<CaseByCohortResult>();
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@InstitutionIds", SqlDbType.VarChar) { Value = institutionIds };
            sqlParameters[1] = new SqlParameter("@CaseId", SqlDbType.Int, 4) { Value = caseId };
            sqlParameters[2] = new SqlParameter("@ModuleId", SqlDbType.Int, 4) { Value = moduleId };

            using (IDataReader reader = _dataContext.GetDataReader("USPGetCaseByCohortResult", sqlParameters))
            {
                while (reader.Read())
                {
                    result.Add(new CaseByCohortResult
                    {
                        Cohort = new Cohort { CohortId = (reader["CohortID"] as int?) ?? 0, CohortName = Convert.ToString(reader["CohortName"]) },
                        InstitutionId = (reader["InstitutionId"] as int?) ?? 0,
                        NumberOfStudents = (reader["NStudents"] as int?) ?? 0,
                        Percentage = (reader["Percantage"] as decimal?) ?? 0,
                    });
                }
            }

            return result;
        }

        public IEnumerable<ResultsFromTheCohortForChart> GetResultsFromTheCohotForChart(int institutionId, int subCategoryId, int chartType, string productIds, string tests, string cohortIds)
        {
            var result = new List<ResultsFromTheCohortForChart>();
            var sqlParameters = new SqlParameter[6];
            sqlParameters[0] = new SqlParameter { ParameterName = "@InstitutionId", SqlDbType = SqlDbType.Int, Value = institutionId };
            sqlParameters[1] = new SqlParameter { ParameterName = "@SubCategoryId", SqlDbType = SqlDbType.Int, Value = subCategoryId };
            sqlParameters[2] = new SqlParameter { ParameterName = "@ChartType", SqlDbType = SqlDbType.Int, Value = chartType };
            sqlParameters[3] = new SqlParameter { ParameterName = "@ProductIds", SqlDbType = SqlDbType.NVarChar, Value = productIds };
            sqlParameters[4] = new SqlParameter { ParameterName = "@Tests", SqlDbType = SqlDbType.NVarChar, Value = tests };
            sqlParameters[5] = new SqlParameter { ParameterName = "@CohortIds", SqlDbType = SqlDbType.NVarChar, Value = cohortIds };

            using (IDataReader reader = _dataContext.GetDataReader("USPGetResultsFromTheCohortsForChart", sqlParameters))
            {
                while (reader.Read())
                {
                    result.Add(new ResultsFromTheCohortForChart
                    {
                        CohortName = (reader["CohortName"] as string) ?? string.Empty,
                        Correct = (reader["N_Correct"] as decimal?) ?? 0,
                    });
                }
            }

            return result;
        }

        public IEnumerable<ResultsForStudentReportCardByModule> GetResultsForStudentReportCardByModule(string institutionIds, int caseId, string caseName,
           string moduleIds, int cohortId, string studentIds)
        {
            var result = new List<ResultsForStudentReportCardByModule>();
            var sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@InstitutionIds", SqlDbType.VarChar) { Value = institutionIds };
            sqlParameters[1] = new SqlParameter("@CaseId", SqlDbType.Int, 4) { Value = caseId };
            sqlParameters[2] = new SqlParameter("@ModuleIds", SqlDbType.VarChar) { Value = moduleIds };
            sqlParameters[3] = new SqlParameter("@cohortId", SqlDbType.Int, 4) { Value = cohortId };
            sqlParameters[4] = new SqlParameter("@studentIds", SqlDbType.VarChar) { Value = studentIds };

            using (IDataReader reader = _dataContext.GetDataReader("USPGetResultsForStudentReportCardByModule", sqlParameters))
            {
                while (reader.Read())
                {
                    result.Add(new ResultsForStudentReportCardByModule
                    {
                        Student = new StudentEntity
                        {
                            FirstName = Convert.ToString(reader["FirstName"]),
                            LastName = Convert.ToString(reader["LastName"])
                        },
                        Module = new Modules
                        {
                            ModuleId = (reader["ModuleId"] as int?) ?? 0,
                            ModuleName = Convert.ToString(reader["ModuleName"])
                        },
                        CaseStudy = new CaseStudy { CaseName = caseName },
                        Correct = Convert.ToDecimal(reader["Correct"]),
                    });
                }
            }

            return result;
        }

        public CohortResultsByModule GetCohortResultsbyModule(int caseId, string moduleIds, string cohortIds)
        {
            var result = new CohortResultsByModule();
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@CaseId", SqlDbType.Int, 4) { Value = caseId };
            sqlParameters[1] = new SqlParameter("@MID", SqlDbType.VarChar) { Value = moduleIds };
            sqlParameters[2] = new SqlParameter("@cohortIds", SqlDbType.VarChar) { Value = cohortIds };

            using (IDataReader reader = _dataContext.GetDataReader("USPGetCohortResultbyModule", sqlParameters))
            {
                if (reader.Read())
                {
                    result = new CohortResultsByModule
                    {
                        Correct = Convert.ToInt32(reader["Correct"]),
                        Total = Convert.ToInt32(reader["Total"])
                    };
                }
            }

            return result;
        }

        public IEnumerable<CohortResultsByModule> GetCaseSubCategoryResultbyCohortModule(int caseId, string moduleIds, string cohortIds, string categoryName)
        {
            var result = new List<CohortResultsByModule>();
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@CaseId", SqlDbType.Int, 4) { Value = caseId };
            sqlParameters[1] = new SqlParameter("@ModuleIds", SqlDbType.VarChar) { Value = moduleIds };
            sqlParameters[2] = new SqlParameter("@cohortIds", SqlDbType.VarChar) { Value = cohortIds };
            sqlParameters[3] = new SqlParameter("@CategoryName", SqlDbType.VarChar) { Value = categoryName };

            using (IDataReader reader = _dataContext.GetDataReader("USPGetCaseSubCategoryResultbyCohortModule", sqlParameters))
            {
                while (reader.Read())
                {
                    result.Add(new CohortResultsByModule
                    {
                        SubcategoryID = Convert.ToInt32(reader["SubcategoryID"]),
                        Correct = Convert.ToInt32(reader["Correct"]),
                        Total = Convert.ToInt32(reader["Total"])
                    });
                }
            }

            return result;
        }

        public IEnumerable<CategoryDetail> GetCaseSubCategories()
        {
            var result = new List<CategoryDetail>();
            using (IDataReader reader = _dataContext.GetDataReader("USPGetCaseSubCategories"))
            {
                while (reader.Read())
                {
                    result.Add(new CategoryDetail
                    {
                        Description = Convert.ToString(reader["CategoryName"])
                    });
                }
            }

            return result;
        }

        public IEnumerable<ResultsFromTheProgramForChart> GetResultsFromCohortForChart(string cohortIds, string testTypeIds, string testIds, string chartType, string fromDate, string toDate)
        {
            var result = new List<ResultsFromTheProgramForChart>();
            var sqlParameters = new SqlParameter[6];
            sqlParameters[0] = new SqlParameter { ParameterName = "@cohortIds", SqlDbType = SqlDbType.VarChar, Value = cohortIds };
            sqlParameters[1] = new SqlParameter { ParameterName = "@testTypeIds", SqlDbType = SqlDbType.VarChar, Value = testTypeIds };
            sqlParameters[2] = new SqlParameter { ParameterName = "@testIds", SqlDbType = SqlDbType.VarChar, Value = testIds };
            sqlParameters[3] = new SqlParameter { ParameterName = "@charttype", SqlDbType = SqlDbType.VarChar, Value = chartType };
            sqlParameters[4] = new SqlParameter { ParameterName = "@fromDate", SqlDbType = SqlDbType.VarChar, Value = fromDate };
            sqlParameters[5] = new SqlParameter { ParameterName = "@toDate", SqlDbType = SqlDbType.VarChar, Value = toDate };

            using (IDataReader reader = _dataContext.GetDataReader("USPGetResultsFromTheCohortForChart", sqlParameters))
            {
                while (reader.Read())
                {
                    result.Add(new ResultsFromTheProgramForChart
                    {
                        Total = (reader["Total"] as int?).GetValueOrDefault(0),
                        NCorrect = (reader["Correct_N"] as int?).GetValueOrDefault(0),
                        ItemText = Convert.ToString(reader["ItemText"]),
                        Norm = (decimal)(reader["Norm"] as Single?).GetValueOrDefault(),
                        Percentage = (decimal)(reader["Percentage"] as double?).GetValueOrDefault()
                    });
                }
            }

            return result;
        }

        public IEnumerable<ResultsFromTheProgramForChart> GetResultsFromInstitutionForChart(string institutionIds, string testTypeIds, string testIds, string chartType,
            string fromDate, string toDate)
        {
            var result = new List<ResultsFromTheProgramForChart>();
            var sqlParameters = new SqlParameter[6];
            sqlParameters[0] = new SqlParameter { ParameterName = "@InstitutionIds", SqlDbType = SqlDbType.VarChar, Value = institutionIds };
            sqlParameters[1] = new SqlParameter { ParameterName = "@testTypeIds", SqlDbType = SqlDbType.VarChar, Value = testTypeIds };
            sqlParameters[2] = new SqlParameter { ParameterName = "@testIds", SqlDbType = SqlDbType.VarChar, Value = testIds };
            sqlParameters[3] = new SqlParameter { ParameterName = "@charttype", SqlDbType = SqlDbType.VarChar, Value = chartType };
            sqlParameters[4] = new SqlParameter { ParameterName = "@fromDate", SqlDbType = SqlDbType.VarChar, Value = fromDate };
            sqlParameters[5] = new SqlParameter { ParameterName = "@toDate", SqlDbType = SqlDbType.VarChar, Value = toDate };

            using (IDataReader reader = _dataContext.GetDataReader("USPGetResultsFromInstitutionForChart", sqlParameters))
            {
                while (reader.Read())
                {
                    result.Add(new ResultsFromTheProgramForChart
                    {
                        Total = (reader["Total"] as int?).GetValueOrDefault(0),
                        NCorrect = (reader["Correct_N"] as int?).GetValueOrDefault(0),
                        ItemText = Convert.ToString(reader["ItemText"]),
                        Norm = (decimal)(reader["Norm"] as Single?).GetValueOrDefault(),
                        Percentage = (reader["Percentage"] as decimal?).GetValueOrDefault(0),
                        InstitutionName = Convert.ToString(reader["InstitutionName"]),
                    });
                }
            }

            return result;
        }

        public ResultsFromTheProgram GetQuestionResultsForCohort(string cohortIds, string testTypes, string testIds, int chartType, string groupIds)
        {
            ResultsFromTheProgram resultsFromTheProgram = new ResultsFromTheProgram();
            var sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter { ParameterName = "@cohortIds", SqlDbType = SqlDbType.VarChar, Value = cohortIds };
            sqlParameters[1] = new SqlParameter { ParameterName = "@charttype", SqlDbType = SqlDbType.Int, Value = chartType };
            sqlParameters[2] = new SqlParameter { ParameterName = "@testTypes", SqlDbType = SqlDbType.VarChar, Value = testTypes };
            sqlParameters[3] = new SqlParameter { ParameterName = "@testIds", SqlDbType = SqlDbType.VarChar, Value = testIds };
            sqlParameters[4] = new SqlParameter { ParameterName = "@GroupIds", SqlDbType = SqlDbType.VarChar, Value = groupIds };

            using (IDataReader reader = _dataContext.GetDataReader("USPGetQuestionResultsForCohort", sqlParameters))
            {
                if (reader.Read())
                {
                    if (chartType == 1)
                    {
                        resultsFromTheProgram = new ResultsFromTheProgram
                        {
                            Total = Convert.ToDecimal(reader["Total"]),
                        };
                    }
                    else if (chartType == 2)
                    {
                        resultsFromTheProgram = new ResultsFromTheProgram
                        {
                            NCorrect = (reader["N_Correct"] as int?).GetValueOrDefault(0),
                            NInCorrect = (reader["N_InCorrect"] as int?).GetValueOrDefault(0),
                            NAnswered = (reader["N_NAnswered"] as int?).GetValueOrDefault(0),
                            CI = (reader["N_CI"] as int?).GetValueOrDefault(0),
                            II = (reader["N_II"] as int?).GetValueOrDefault(0),
                            IC = (reader["N_IC"] as int?).GetValueOrDefault(0)
                        };
                    }
                }
            }

            return resultsFromTheProgram;
        }

        public ResultsFromTheProgram GetQuestionResultsForInstitution(int institutionId, string testTypes, string testIds, int chartType)
        {
            ResultsFromTheProgram resultsFromTheProgram = new ResultsFromTheProgram();
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter { ParameterName = "@InstitutionId", SqlDbType = SqlDbType.Int, Value = institutionId };
            sqlParameters[1] = new SqlParameter { ParameterName = "@charttype", SqlDbType = SqlDbType.Int, Value = chartType };
            sqlParameters[2] = new SqlParameter { ParameterName = "@testTypes", SqlDbType = SqlDbType.VarChar, Value = testTypes };
            sqlParameters[3] = new SqlParameter { ParameterName = "@testIds", SqlDbType = SqlDbType.VarChar, Value = testIds };

            using (IDataReader reader = _dataContext.GetDataReader("USPGetQuestionResultsForInstitution", sqlParameters))
            {
                if (reader.Read())
                {
                    if (chartType == 1)
                    {
                        resultsFromTheProgram = new ResultsFromTheProgram
                        {
                            Total = Convert.ToDecimal(reader["Total"]),
                        };
                    }
                    else if (chartType == 2)
                    {
                        resultsFromTheProgram = new ResultsFromTheProgram
                        {
                            NCorrect = (reader["N_Correct"] as int?).GetValueOrDefault(0),
                            NInCorrect = (reader["N_InCorrect"] as int?).GetValueOrDefault(0),
                            NAnswered = (reader["N_NAnswered"] as int?).GetValueOrDefault(0),
                            CI = (reader["N_CI"] as int?).GetValueOrDefault(0),
                            II = (reader["N_II"] as int?).GetValueOrDefault(0),
                            IC = (reader["N_IC"] as int?).GetValueOrDefault(0)
                        };
                    }
                }
            }

            return resultsFromTheProgram;
        }

        public decimal GetNormForTest(int testId)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter { ParameterName = "@TestID", SqlDbType = SqlDbType.Int, Value = testId };

            return (decimal)(_dataContext.ExecuteScalar("USPGetNormForTest", sqlParameters) as Single?).GetValueOrDefault();
        }

        public decimal GetRankForTest(int testId)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter { ParameterName = "@testId", SqlDbType = SqlDbType.Int, Value = testId };

            return (decimal)(_dataContext.ExecuteScalar("USPGetTestRank", sqlParameters) as Double?).GetValueOrDefault();
        }

        public IEnumerable<ResultsFromTheProgram> GetResultsFromInstitutions(string institutionIds, int chartType, string testTypeIds, string testIds, string fromDate, string toDate)
        {
            var resultsFromTheProgram = new List<ResultsFromTheProgram>();
            var sqlParameters = new SqlParameter[6];
            sqlParameters[0] = new SqlParameter { ParameterName = "@InstitutionIDs", SqlDbType = SqlDbType.VarChar, Value = institutionIds };
            sqlParameters[1] = new SqlParameter { ParameterName = "@charttype", SqlDbType = SqlDbType.Int, Value = chartType };
            sqlParameters[2] = new SqlParameter { ParameterName = "@TestTypeIds", SqlDbType = SqlDbType.VarChar, Value = testTypeIds };
            sqlParameters[3] = new SqlParameter { ParameterName = "@TestIDs", SqlDbType = SqlDbType.VarChar, Value = testIds };
            sqlParameters[4] = new SqlParameter { ParameterName = "@FromDate", SqlDbType = SqlDbType.VarChar, Value = fromDate };
            sqlParameters[5] = new SqlParameter { ParameterName = "@ToDate", SqlDbType = SqlDbType.VarChar, Value = toDate };

            using (IDataReader reader = _dataContext.GetDataReader("USPGetResultsFromInstitutions", sqlParameters))
            {
                while (reader.Read())
                {
                    if (chartType == 1)
                    {
                        resultsFromTheProgram.Add(new ResultsFromTheProgram
                        {
                            Total = Convert.ToDecimal(reader["Total"]),
                            InstitutionId = Convert.ToInt32(reader["InsitutionID"])
                        });
                    }
                    else if (chartType == 2)
                    {
                        resultsFromTheProgram.Add(new ResultsFromTheProgram
                        {
                            NCorrect = (reader["N_Correct"] as int?).GetValueOrDefault(0),
                            NInCorrect = (reader["N_InCorrect"] as int?).GetValueOrDefault(0),
                            NAnswered = (reader["N_NAnswered"] as int?).GetValueOrDefault(0),
                            CI = (reader["N_CI"] as int?).GetValueOrDefault(0),
                            II = (reader["N_II"] as int?).GetValueOrDefault(0),
                            IC = (reader["N_IC"] as int?).GetValueOrDefault(0)
                        });
                    }
                }
            }

            return resultsFromTheProgram;
        }

        public IEnumerable<ResultsFromTheCohortForChart> GetResultsForCohortsBySubCategoryChart(string cohorts, int categoryId, int subCategoryId, string cases, string modules, int institutionId)
        {
            var result = new List<ResultsFromTheCohortForChart>();
            var sqlParameters = new SqlParameter[6];
            sqlParameters[0] = new SqlParameter { ParameterName = "@CohortList", SqlDbType = SqlDbType.VarChar, Value = cohorts };
            sqlParameters[1] = new SqlParameter { ParameterName = "@SubCategory", SqlDbType = SqlDbType.Int, Value = subCategoryId };
            sqlParameters[2] = new SqlParameter { ParameterName = "@CaseList", SqlDbType = SqlDbType.VarChar, Value = cases };
            sqlParameters[3] = new SqlParameter { ParameterName = "@ModuleList", SqlDbType = SqlDbType.VarChar, Value = modules };
            sqlParameters[4] = new SqlParameter { ParameterName = "@InstitutionID", SqlDbType = SqlDbType.Int, Value = institutionId };
            sqlParameters[5] = new SqlParameter { ParameterName = "@CategoryID", SqlDbType = SqlDbType.Int, Value = categoryId };

            using (IDataReader reader = _dataContext.GetDataReader("USPResultsFromTheCohortsBySubCategory", sqlParameters))
            {
                while (reader.Read())
                {
                    result.Add(new ResultsFromTheCohortForChart
                    {
                        CohortName = Convert.ToString(reader["CohortName"]),
                        Correct = (reader["N_Correct"] as decimal?) ?? 0,
                    });
                }
            }

            return result;
        }

        public DataTable GetResultsForStudentSummaryByAnswerChoice(string cohortIds, int productId, int testId)
        {
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter { ParameterName = "@ProductId", SqlDbType = SqlDbType.Int, Value = productId };
            sqlParameters[1] = new SqlParameter { ParameterName = "@CohortIds", SqlDbType = SqlDbType.VarChar, Value = cohortIds };
            sqlParameters[2] = new SqlParameter { ParameterName = "@TestId", SqlDbType = SqlDbType.Int, Value = testId };

            return _dataContext.GetDataTable("USPReturnStudentSummaryByAnswerChoice", sqlParameters);
        }

        public DataTable GetResultsByCohortQuestions(string cohortIds, int productId, int testId)
        {
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter { ParameterName = "@ProductId", SqlDbType = SqlDbType.Int, Value = productId };
            sqlParameters[1] = new SqlParameter { ParameterName = "@CohortIds", SqlDbType = SqlDbType.VarChar, Value = cohortIds };
            sqlParameters[2] = new SqlParameter { ParameterName = "@TestId", SqlDbType = SqlDbType.Int, Value = testId };

            return _dataContext.GetDataTable("USPGetResultsByCohortQuestions", sqlParameters);
        }

        public DataTable GetResultsByInstitutionQuestions(string cohortIds, int productId, int testId)
        {
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter { ParameterName = "@ProductId", SqlDbType = SqlDbType.Int, Value = productId };
            sqlParameters[1] = new SqlParameter { ParameterName = "@CohortIds", SqlDbType = SqlDbType.VarChar, Value = cohortIds };
            sqlParameters[2] = new SqlParameter { ParameterName = "@TestId", SqlDbType = SqlDbType.Int, Value = testId };

            return _dataContext.GetDataTable("uspGetResultsByInstitutionCohortQuestions", sqlParameters);
        }

        public int GetStudentNumberByCohortTest(int cohortId, int productId, int testId)
        {
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter { ParameterName = "@ProductId", SqlDbType = SqlDbType.Int, Value = productId };
            sqlParameters[1] = new SqlParameter { ParameterName = "@CohortId", SqlDbType = SqlDbType.Int, Value = cohortId };
            sqlParameters[2] = new SqlParameter { ParameterName = "@TestId", SqlDbType = SqlDbType.Int, Value = testId };

            return (_dataContext.ExecuteScalar("USPGetStudentNumberByCohortTest", sqlParameters) as int?).GetValueOrDefault(0);
        }

        public IEnumerable<Institution> GetInstitutionByStudentID(int studentId)
        {
            var institutes = new List<Institution>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@UserID", SqlDbType.Int, 4) { Value = studentId };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetInstitutionByStudentID", sqlParameters))
            {
                while (reader.Read())
                {
                    institutes.Add(new Institution
                    {
                        InstitutionName = (reader["InstitutionName"] as string) ?? string.Empty,
                        InstitutionId = (reader["InstitutionID"] as int?) ?? 0,
                        ProgramOfStudyId = (reader["ProgramOfStudyId"] as int?) ?? 0,
                        ProgramofStudyName = (reader["ProgramofStudyName"] as string) ?? string.Empty,
                    });
                }
            }

            return institutes;
        }

        public IEnumerable<ReportTestsScheduledbyDate> GetTestsScheduledByDate(string programOfStudyName, string institutionIds, string cohortIds, string groupIds, string productIds, DateTime? startDate, DateTime? endDate)
        {
            #region Sql Parameters
            var result = new List<ReportTestsScheduledbyDate>();
            var sqlParameters = new SqlParameter[6];
            sqlParameters[0] = new SqlParameter("@InstitutionIds", SqlDbType.VarChar) { Value = institutionIds };
            sqlParameters[1] = new SqlParameter("@CohortIds", SqlDbType.NVarChar) { Value = cohortIds };
            sqlParameters[2] = new SqlParameter("@GroupIds", SqlDbType.NVarChar) { Value = groupIds };
            sqlParameters[3] = new SqlParameter("@ProductIds", SqlDbType.NVarChar, 100) { Value = productIds };
            sqlParameters[4] = new SqlParameter("@StartDate", SqlDbType.DateTime, 0) { Value = startDate ?? Convert.DBNull };
            sqlParameters[5] = new SqlParameter("@EndDate", SqlDbType.DateTime, 0) { Value = endDate ?? Convert.DBNull };

            var userTests = new List<UserTest>();
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("uspGetTestScheduleByDate", sqlParameters))
            {
                while (reader.Read())
                {
                    result.Add(new ReportTestsScheduledbyDate
                    {
                        InstitutionName =  string.Format("{0} - {1}", (reader["InstitutionName"] as string) ?? string.Empty, programOfStudyName),
                        CohortName = (reader["CohortName"] as string) ?? string.Empty,
                        TestType = (reader["ProductName"] as string) ?? string.Empty,
                        TestName = (reader["TestName"] as string) ?? string.Empty,
                        StartDate = (reader["StartDate"] as DateTime?) ?? null,
                        NumberOfStudents = (reader["Students"] as int?) ?? 0,
                    });
                }
            }

            return result;
        }

        public IEnumerable<State> GetStates(int countryId, int stateId)
        {
            var states = new List<State>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@CountryId", SqlDbType.Int) { Value = countryId };
            sqlParameters[1] = new SqlParameter("@StateId", SqlDbType.Int) { Value = stateId };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetStates", sqlParameters))
            {
                while (reader.Read())
                {
                    states.Add(new State
                    {
                        StateId = (reader["StateID"] as int?) ?? 0,
                        CountryId = (reader["CountryID"] as int?) ?? 0,
                        StateName = (reader["StateName"] as string) ?? string.Empty
                    });
                }
            }

            return states.ToArray();
        }

        public IEnumerable<UserTest> GetTestsForStudentReportCard(string productIds, string studentIds)
        {
            var tests = new List<UserTest>();
            #region Sql Parameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@ProductIDs", SqlDbType.VarChar) { Value = productIds };
            sqlParameters[1] = new SqlParameter("@StudentIds", SqlDbType.VarChar) { Value = studentIds };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetTestsForStudentReportCard", sqlParameters))
            {
                while (reader.Read())
                {
                    var test = new UserTest
                    {
                        TestName = Convert.ToString(reader["TestName"]),
                        TestId = (reader["TestId"] as int?).GetValueOrDefault(0),
                    };

                    tests.Add(test);
                }
            }

            return tests;
        }

        public IEnumerable<UserTest> GetTestsForEnglishNursingTracking(string cohortIds, string studentIds)
        {
            var tests = new List<UserTest>();
            #region Sql Parameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@CohortIDs", SqlDbType.VarChar) { Value = cohortIds };
            sqlParameters[1] = new SqlParameter("@StudentIds", SqlDbType.VarChar) { Value = studentIds };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetTestsForEnglishNursingTracking", sqlParameters))
            {
                while (reader.Read())
                {
                    var test = new UserTest
                    {
                        TestName = Convert.ToString(reader["TestName"]),
                        TestId = (reader["TestId"] as int?).GetValueOrDefault(0),
                    };

                    tests.Add(test);
                }
            }

            return tests;
        }

        public IEnumerable<Question> GetQIDForEnglishNursingTracking(string cohortIds, string studentIds, string testIds)
        {
            var questions = new List<Question>();
            #region Sql Parameters
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@CohortIDs", SqlDbType.VarChar) { Value = cohortIds };
            sqlParameters[1] = new SqlParameter("@StudentIds", SqlDbType.VarChar) { Value = studentIds };
            sqlParameters[2] = new SqlParameter("@TestIds", SqlDbType.VarChar) { Value = testIds };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetQIDForEnglishNursingTracking", sqlParameters))
            {
                while (reader.Read())
                {
                    var question = new Question
                    {
                        QuestionId = (reader["QuestionID"] as string) ?? string.Empty,
                        Id = (reader["QID"] as int?) ?? 0,
                    };

                    questions.Add(question);
                }
            }

            return questions;
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
        public IEnumerable<EnglishForNursingTracking> GetEnglishNursingTrackingDetails(string institutionId, string cohortIds, string studentIds, string testIds, string qIds)
        {
            var EnglishForNursingTrackingDetails = new List<EnglishForNursingTracking>();
            #region Sql Parameters
            var sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@InstitutionID", SqlDbType.VarChar) { Value = institutionId };
            sqlParameters[1] = new SqlParameter("@CohortIds", SqlDbType.VarChar) { Value = cohortIds };
            sqlParameters[2] = new SqlParameter("@StudentIds", SqlDbType.VarChar) { Value = studentIds };
            sqlParameters[3] = new SqlParameter("@testIds", SqlDbType.VarChar) { Value = testIds };
            sqlParameters[4] = new SqlParameter("@qIds", SqlDbType.VarChar) { Value = qIds };

            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetDetailsForEnglishNursingTracking", sqlParameters))
            {
                while (reader.Read())
                {
                    var EnglishForNursingTrackingDetail = new EnglishForNursingTracking
                    {
                        InstitutionName = string.Format("{0} - {1}", (reader["InstitutionName"] as string) ?? string.Empty, (reader["ProgramofStudyName"] as string) ?? string.Empty),
                        CohortName = (reader["CohortName"] as string) ?? string.Empty,
                        Student = new StudentEntity()
                        {
                            FirstName = (reader["FirstName"] as string) ?? string.Empty,
                            LastName = (reader["LastName"] as string) ?? string.Empty,
                        },
                        Correct = (reader["Correct"] as string) ?? string.Empty,
                        TestName = Convert.ToString(reader["TestName"]),
                        QuestionId = (reader["QuestionId"] as string) ?? string.Empty,
                        AltTabClickedDate = (reader["AltTabClickedDate"] as DateTime?) ?? null,
                        UserAction = (reader["UserAction"] as string) ?? string.Empty,
                    };

                    EnglishForNursingTrackingDetails.Add(EnglishForNursingTrackingDetail);
                }
            }

            return EnglishForNursingTrackingDetails;

        #endregion
        }

        public IEnumerable<StudentEntity> GetStudents(string institutionIds, string cohortIds)
        {
            var students = new List<StudentEntity>();
            #region Sql Parameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@InstitutionIds", SqlDbType.VarChar) { Value = institutionIds };
            sqlParameters[1] = new SqlParameter("@CohortIds", SqlDbType.VarChar) { Value = cohortIds };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetStudentsInInstitutionByCohort", sqlParameters))
            {
                while (reader.Read())
                {
                    var student = new StudentEntity
                    {
                        StudentName = Convert.ToString(reader["Name"]),
                        StudentId = (reader["UserId"] as int?).GetValueOrDefault(0),
                    };

                    students.Add(student);
                }
            }

            return students;
        }

        public List<Institution> GetInstitutions(int userId, string institutionIds)
        {
            var institutions = new List<Institution>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@InstitutionId", SqlDbType.Int) { Value = 0 };
            sqlParameters[1] = new SqlParameter("@InstitutionIds", SqlDbType.VarChar) { Value = institutionIds };
            sqlParameters[2] = new SqlParameter("@UserId", SqlDbType.Int) { Value = userId };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("USPGetInstitutions", sqlParameters))
            {
                while (reader.Read())
                {
                    institutions.Add(new Institution
                    {
                        InstitutionId = (reader["InstitutionId"] as int?) ?? 0,
                        InstitutionName = (reader["InstitutionName"] as String) ?? string.Empty,
                        Description = (reader["Description"] as String) ?? string.Empty,
                        ContactName = (reader["ContactName"] as String) ?? string.Empty,
                        ContactPhone = (reader["ContactPhone"] as String) ?? string.Empty,
                        CenterId = (reader["CenterID"] as String) ?? string.Empty,
                        TimeZone = (reader["TimeZone"] as int?) ?? 0,
                        IP = (reader["IP"] as String) ?? string.Empty,
                        FacilityID = (reader["FacilityID"] as int?) ?? 0,
                        ProgramID = (reader["ProgramID"] as int?) ?? 0,
                        Active = (reader["Active"] as int?) ?? 0,
                        Status = (reader["Status"] as String) ?? string.Empty,
                        InstitutionAddress = new Address { AddressId = (reader["AddressID"] as int?) ?? 0, },
                        Annotation = (reader["Annotation"] as String) ?? string.Empty,
                        ContractualStartDate = (reader["ContractualStartDate"] as DateTime?).ToString() ?? string.Empty,
                        ContractualStartDateReport = reader["ContractualStartDate"] as DateTime?,
                        Email = (reader["Email"] as String) ?? string.Empty,
                        InstitutionNameWithProgOfStudy = string.Format("{0} - {1}", (reader["InstitutionName"] as string) ?? string.Empty, (reader["ProgramofStudyName"] as string) ?? string.Empty)
                    });
                }
            }

            return institutions;
        }

        public List<Institution> GetInstitutions(int userId, int programofStudyId, string institutionIds)
        {
            var institutions = new List<Institution>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@InstitutionId", SqlDbType.Int) { Value = 0 };
            sqlParameters[1] = new SqlParameter("@InstitutionIds", SqlDbType.VarChar) { Value = institutionIds };
            sqlParameters[2] = new SqlParameter("@UserId", SqlDbType.Int) { Value = userId };
            sqlParameters[3] = new SqlParameter("@ProgramofStudyId", SqlDbType.Int) { Value = programofStudyId };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("USPGetInstitutions", sqlParameters))
            {
                while (reader.Read())
                {
                    var institutionNameWithProgOfStudy = string.Format("{0} - {1}", (reader["InstitutionName"] as string) ?? string.Empty, (reader["ProgramofStudyName"] as string) ?? string.Empty);
                    institutions.Add(new Institution
                    {
                        ProgramofStudyName = (reader["ProgramofStudyName"] as String) ?? string.Empty,
                        InstitutionId = (reader["InstitutionId"] as int?) ?? 0,
                        InstitutionName = (reader["InstitutionName"] as String) ?? string.Empty,
                        InstitutionNameWithProgOfStudy = institutionNameWithProgOfStudy,
                        Description = (reader["Description"] as String) ?? string.Empty,
                        ContactName = (reader["ContactName"] as String) ?? string.Empty,
                        ContactPhone = (reader["ContactPhone"] as String) ?? string.Empty,
                        CenterId = (reader["CenterID"] as String) ?? string.Empty,
                        TimeZone = (reader["TimeZone"] as int?) ?? 0,
                        IP = (reader["IP"] as String) ?? string.Empty,
                        FacilityID = (reader["FacilityID"] as int?) ?? 0,
                        ProgramID = (reader["ProgramID"] as int?) ?? 0,
                        Active = (reader["Active"] as int?) ?? 0,
                        Status = (reader["Status"] as String) ?? string.Empty,
                        InstitutionAddress = new Address { AddressId = (reader["AddressID"] as int?) ?? 0, },
                        Annotation = (reader["Annotation"] as String) ?? string.Empty,
                        ContractualStartDate = (reader["ContractualStartDate"] as DateTime?).ToString() ?? string.Empty,
                        Email = (reader["Email"] as String) ?? string.Empty,
                        PayLinkEnabled = (bool)(reader["PayLinkEnabled"] is DBNull ? false : reader["PayLinkEnabled"])
                    });
                }
            }

            return institutions.ToList();
        }

        public IEnumerable<Cohort> GetCohorts(int cohortId, string institutionIds)
        {
            var cohorts = new List<Cohort>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@CohortId", SqlDbType.Int, 4) { Value = cohortId };
            sqlParameters[1] = new SqlParameter("@InstitutionIds", SqlDbType.VarChar) { Value = institutionIds };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetCohorts", sqlParameters))
            {
                while (reader.Read())
                {
                    cohorts.Add(new Cohort
                    {
                        CohortName = (reader["CohortName"] as string) ?? string.Empty,
                        CohortId = (reader["CohortID"] as int?) ?? 0,
                        InstitutionId = (reader["InstitutionID"] as int?) ?? 0,
                        CohortStartDate = (reader["CohortStartDate"] as DateTime?) ?? null,
                        CohortEndDate = (reader["CohortEndDate"] as DateTime?) ?? null,
                        CohortStatus = (reader["CohortStatus"] as int?) ?? 0,
                        CohortDescription = (reader["CohortDescription"] as string) ?? string.Empty,
                    });
                }
            }

            return cohorts.ToArray();
        }

        public IEnumerable<Product> GetProducts(int productId)
        {
            var products = new List<Product>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ProductId", SqlDbType.Int) { Value = productId };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("USPGetAllProducts", sqlParameters))
            {
                while (reader.Read())
                {
                    var content = new Product
                    {
                        ProductId = (reader["ProductID"] as int?).GetValueOrDefault(0),
                        ProductName = Convert.ToString(reader["ProductName"]),
                        ProductType = Convert.ToString(reader["ProductType"]),
                        Bundle = (reader["Bundle"] as int?) ?? 0,
                        MultiUseTest = (reader["MultiUseTest"] as int?) ?? 0,
                        Remediation = (reader["Remediation"] as int?) ?? 0,
                        Scramble = (reader["Scramble"] as int?) ?? 0,
                        TestType = (reader["TestType"] as string) ?? string.Empty
                    };
                    products.Add(content);
                }
            }

            return products;
        }

        public IEnumerable<Student> GetStudents(int studentId, string searchText)
        {
            var students = new List<Student>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@StudentId", SqlDbType.Int, 4) { Value = studentId };

            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("USPGetStudents", sqlParameters))
            {
                while (reader.Read())
                {
                    students.Add(new Student
                    {
                        UserId = (reader["UserID"] as int?) ?? 0,
                        UserName = reader["UserName"] as string ?? string.Empty,
                        FirstName = reader["FirstName"] as string ?? string.Empty,
                        LastName = reader["LastName"] as string ?? string.Empty,
                        KaplanUserId = reader["KaplanUserID"] as string ?? string.Empty,
                        EnrollmentId = reader["EnrollmentID"] as string ?? string.Empty,
                        Ada = (bool)(reader["Ada"] is DBNull ? false : reader["Ada"]),
                        UserPass = reader["UserPass"] as string ?? string.Empty,
                        Telephone = reader["Telephone"] as string ?? string.Empty,
                        Email = reader["Email"] as string ?? string.Empty,
                        ContactPerson = reader["ContactPerson"] as string ?? string.Empty,
                        EmergencyPhone = reader["EmergencyPhone"] as string ?? string.Empty,
                        UserType = reader["UserType"] as string ?? string.Empty,
                        UserCreateDate = Convert.ToDateTime(reader["UserCreateDate"]),
                        UserExpireDate = (DateTime?)(reader["UserExpireDate"] is DBNull ? null : reader["UserExpireDate"]),
                        UserStartDate = (DateTime?)(reader["UserStartDate"] is DBNull ? null : reader["UserStartDate"]),
                        UserUpdateDate = (DateTime?)(reader["UserUpdateDate"] is DBNull ? null : reader["UserUpdateDate"]),
                        Cohort = new Cohort() { CohortId = (reader["CohortId"] as int?) ?? 0 },
                        Institution = new Institution() { InstitutionId = (reader["InstitutionID"] as int?) ?? 0 },
                        Group = new Group() { GroupId = (reader["GroupId"] as int?) ?? 0 },
                        StudentAddress = new Address() { AddressId = (reader["AddressID"] as int?) ?? 0 },
                    });
                }
            }

            return students.ToArray();
        }

        public IEnumerable<Test> GetTests(int productId, int questionId, string institutionIds, bool forCMS, int programofStudy)
        {
            var tests = new List<Test>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@ProductID", SqlDbType.Int, 4) { Value = productId };
            sqlParameters[1] = new SqlParameter("@QuestionId", SqlDbType.Int, 4) { Value = questionId };
            sqlParameters[2] = new SqlParameter("@InstitutionIds", SqlDbType.VarChar, 1000) { Value = institutionIds };
            sqlParameters[3] = new SqlParameter("@ForCMS", SqlDbType.Bit, 1000) { Value = forCMS };
            sqlParameters[4] = new SqlParameter("@ProgramofStudy", SqlDbType.Int, 4) { Value = programofStudy };

            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetTests", sqlParameters))
            {
                while (reader.Read())
                {
                    tests.Add(new Test
                    {
                        TestName = (reader["Name"] as string) ?? string.Empty,
                        TestId = (reader["TestID"] as int?) ?? 0,
                        TestNumber = (reader["TestNumber"] as int?) ?? 0,
                        Product = new Product() { ProductName = (reader["ProductName"] as string) ?? string.Empty }
                    });
                }
            }

            return tests.ToArray();
        }

        public IEnumerable<Test> GetTestsByProgramOfStudy(int productId, int programOfStudy)
        {
            var tests = new List<Test>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@ProductID", SqlDbType.Int, 4) { Value = productId };
            sqlParameters[1] = new SqlParameter("@QuestionId", SqlDbType.Int, 4) { Value = 0 };
            sqlParameters[2] = new SqlParameter("@InstitutionIds", SqlDbType.VarChar, 1000) { Value = string.Empty };
            sqlParameters[3] = new SqlParameter("@ForCMS", SqlDbType.Bit, 1000) { Value = false };
            sqlParameters[4] = new SqlParameter("@ProgramOfStudy", SqlDbType.Int, 4) { Value = programOfStudy };

            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetTests", sqlParameters))
            {
                while (reader.Read())
                {
                    tests.Add(new Test
                    {
                        TestName = (reader["Name"] as string) ?? string.Empty,
                        TestId = (reader["TestID"] as int?) ?? 0,
                        TestNumber = (reader["TestNumber"] as int?) ?? 0,
                        Product = new Product() { ProductName = (reader["ProductName"] as string) ?? string.Empty }
                    });
                }
            }

            return tests.ToArray();
        }

        public IEnumerable<Category> GetCategories()
        {
            var categories = new List<Category>();
            using (IDataReader reader = _dataContext.GetDataReader("UspGetAllCategories"))
            {
                while (reader.Read())
                {
                    categories.Add(new Category
                    {
                        CategoryID = (reader["CategoryID"] as int?) ?? 0,
                        TableName = (reader["TableName"] as string) ?? string.Empty,
                        OrderNumber = (reader["OrderNumber"] as int?) ?? 0,
                        ProgramofStudyId = (reader["programofStudyId"] as int?) ?? 0,
                        ProgramofStudyName = (reader["ProgramofStudyName"] as string) ?? string.Empty
                    });
                }
            }

            return categories;
        }

        public IEnumerable<CategoryDetail> GetCategoryDetails(int categoryId, int programOfStudyIdForCategory)
        {
            var categoryDetails = new List<CategoryDetail>();

            string spName = string.Empty;
            bool hasParentId = false;

            // Has to live with this switch case. This cannot be avoided since data is stored in different tables.
            // Extracting to independent methods may help only from code refactoring point of view.
            switch (categoryId)
            {
                case 1:
                case 13:
                    {
                        spName = "UspGetClientNeedsCategory";
                        break;
                    }

                case 2:
                case 14:
                    {
                        spName = "UspGetNursingProcessCategory";
                        break;
                    }

                case 3:
                case 15:
                    {
                        spName = "UspGetCriticalThinkingCategory";
                        break;
                    }

                case 4:
                case 16:
                    {
                        spName = "UspGetClinicalConceptCategory";
                        break;
                    }

                case 5:
                case 17:
                    {
                        spName = "UspGetDemographicCategory";
                        break;
                    }

                case 6:
                case 18:
                    {
                        spName = "UspGetCognitiveLevelCategory";
                        break;
                    }

                case 7:
                case 19:
                    {
                        spName = "UspGetSpecialtyAreaCategory";
                        break;
                    }

                case 8:
                case 20:
                    {
                        spName = "UspGetSystemsCategory";
                        break;
                    }

                case 9:
                case 21:
                    {
                        spName = "UspGetLevelOfDifficultyCategory";
                        break;
                    }

                case 10:
                case 22:
                    {
                        spName = "UspGetClientNeedCategoryCategory";
                        hasParentId = true;
                        break;
                    }

                case 11:
                    {
                        spName = "UspGetAccreditationCategoriesCategory";
                        break;
                    }

                case 12:
                    {
                        spName = "UspGetQSENKSACompetenciesCategory";
                        break;
                    }

                case 23:
                case 24:
                    {
                        spName = "UspGetConceptsCategory";
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException(string.Format("Category ID - {0} is not within the accepted range. Please check the argument passed in.", categoryId));
                    }
            }

            using (IDataReader reader = _dataContext.GetDataReader(spName))
            {
                while (reader.Read())
                {
                    var programOfStudyId = (reader["ProgramofStudyId"] as int?) ?? 0;
                    if (programOfStudyId.Equals(programOfStudyIdForCategory))
                    {
                        categoryDetails.Add(new CategoryDetail
                        {
                            Id = (reader["Id"] as int?) ?? 0,
                            Description = (reader["Description"] as string) ?? string.Empty,
                            ParentId = hasParentId ? reader["ParentId"] as int? ?? 0 : 0,
                            ProgramofStudy = programOfStudyId
                        });
                    }
                }
            }

            return categoryDetails;
        }

        public IEnumerable<MultiCampusReportDetails> GetMultiCastReportCardDetails(string studentIds, string testIds, string institutionIds, string testTypeIds)
        {
            var multicastReportCardDetails = new List<MultiCampusReportDetails>();
            #region Sql Parameters
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@StudentIds", SqlDbType.VarChar) { Value = studentIds };
            sqlParameters[1] = new SqlParameter("@testIds", SqlDbType.VarChar) { Value = testIds };
            sqlParameters[2] = new SqlParameter("@InstitutionIds", SqlDbType.VarChar) { Value = institutionIds };
            sqlParameters[3] = new SqlParameter("@testTypeId", SqlDbType.VarChar) { Value = testTypeIds };

            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetMultiCampusReportCardDetails", sqlParameters))
            {
                while (reader.Read())
                {
                    var multicastReportCardDetail = new MultiCampusReportDetails
                    {
                        Student = new StudentEntity()
                        {
                            FirstName = Convert.ToString(reader["FirstName"]),
                            LastName = Convert.ToString(reader["LastName"]),
                            StudentId = (reader["UserId"] as int?).GetValueOrDefault(0),
                        },
                        Cohort = new Cohort
                        {
                            CohortId = (reader["CohortId"] as int?).GetValueOrDefault(0),
                            CohortName = Convert.ToString(reader["CohortName"]),
                        },
                        Product = new Product
                        {
                            ProductId = (reader["TestTypeId"] as int?).GetValueOrDefault(0),
                            ProductName = Convert.ToString(reader["TestType"]),
                        },
                        Group = new Group
                        {
                            GroupName = Convert.ToString(reader["GroupName"]),
                        },
                        TestId = (reader["TestId"] as int?).GetValueOrDefault(0),
                        TestName = Convert.ToString(reader["TestName"]),
                        TestTaken = Convert.ToDateTime(reader["TestTaken"]),
                        RemediationTime = Convert.ToString(reader["RemediationTime"]),
                        Correct = Convert.ToDecimal(reader["Correct"]),
                        UserTestId = (reader["UserTestId"] as int?).GetValueOrDefault(0),
                        Rank = Convert.ToString(reader["Rank"]),
                        TimeUsed = reader["TimeUsed"] as string ?? string.Empty,
                        QuestionCount = (reader["QuestionCount"] as int?) ?? 0,
                        Ranking = (Convert.ToString(reader["Rank"]) == "n/a") ? 0 : Convert.ToInt32(reader["Rank"].ToString() == string.Empty ? "0" : reader["Rank"].ToString()),
                        InstitutionName = string.Format("{0} - {1}", (reader["InstitutionName"] as string) ?? string.Empty, (reader["ProgramofStudyName"] as string) ?? string.Empty),
                        InstitutionId = (reader["InstitutionId"] as int?) ?? 0,
                        TestStyle = (reader["TestStyle"] as string ?? string.Empty),
                    };

                    multicastReportCardDetails.Add(multicastReportCardDetail);
                }
            }

            return multicastReportCardDetails;
        }

        public IEnumerable<UserTest> GetTestByProdCohortId(string productIds, string cohortIds)
        {
            var tests = new List<UserTest>();
            #region Sql Parameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@ProductIDs", SqlDbType.VarChar) { Value = productIds };
            sqlParameters[1] = new SqlParameter("@CohortIDs", SqlDbType.VarChar) { Value = cohortIds };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetTestsByProdCohortId", sqlParameters))
            {
                while (reader.Read())
                {
                    var test = new UserTest
                    {
                        TestName = Convert.ToString(reader["TestName"]),
                        TestId = (reader["TestId"] as int?).GetValueOrDefault(0),
                    };

                    tests.Add(test);
                }
            }

            return tests;
        }

        public bool IsMultipleProgramofStudyAssignedToAdmin(int userId)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@AdminId", SqlDbType.Int, 4) { Value = userId };
            sqlParameters[1] = new SqlParameter("@IsMultiplePSAssigned", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            #endregion

            _dataContext.ExecuteStoredProcedure("uspIsMultipleProgramofStudyAssignedToAdmin", sqlParameters);
            return (bool)sqlParameters[1].Value;
        }
    }
}
