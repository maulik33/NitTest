using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;

namespace NursingLibrary.DAO
{
    public class TestRepository : ITestRepository
    {
        #region Fields

        private readonly IDataContext _dataContext;

        #endregion Fields

        #region Constructors

        public TestRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #endregion Constructors

        #region Methods

        public bool CheckPercentileRankExists(int testId)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            var parameterTestId = new SqlParameter("@testId", SqlDbType.Int, 4) { Value = testId };
            sqlParameters[0] = parameterTestId;
            #endregion
            var result = _dataContext.ExecuteScalar("uspCheckPercentileRankExists", sqlParameters) as bool?;
            return result ?? false;
        }

        public bool CheckProbabilityExists(int testId)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            var parameterTestId = new SqlParameter("@testId", SqlDbType.Int, 4) { Value = testId };
            sqlParameters[0] = parameterTestId;
            #endregion

            var result = _dataContext.ExecuteScalar("uspCheckProbabilityExists", sqlParameters) as bool?;
            return result ?? false;
        }

        public bool ContinueTest(int userId, int testId)
        {
            var userTests = new List<UserTest>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            var parameterUserId = new SqlParameter("@userId", SqlDbType.Int, 4) { Value = userId };
            sqlParameters[0] = parameterUserId;

            var parameterProductId = new SqlParameter("@testId", SqlDbType.Int, 4) { Value = testId };
            sqlParameters[1] = parameterProductId;

            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetTestByUserAndTestID", sqlParameters))
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
                        Override = (reader["Override"] as bool?) ?? false
                    });
                }
            }

            return userTests.Count == 0 ? false : userTests.FirstOrDefault().Override;
        }

        public IList<UserTest> GetUserTests(int userId, int testId)
        {
            var userTests = new List<UserTest>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            var parameterUserId = new SqlParameter("@userId", SqlDbType.Int, 4) { Value = userId };
            sqlParameters[0] = parameterUserId;

            var parameterProductId = new SqlParameter("@testId", SqlDbType.Int, 4) { Value = testId };
            sqlParameters[1] = parameterProductId;

            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetTestByUserAndTestID", sqlParameters))
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
                        Override = (reader["Override"] as bool?) ?? false
                    });
                }
            }

            return userTests;
        }

        public IEnumerable<Test> GetAllTests(int userId, int productId, int bundleId, int testSubGroup, int timeOffset)
        {
            var tests = new List<Test>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[5];
            var parameterUserId = new SqlParameter("@userId", SqlDbType.Int, 4) { Value = userId };
            sqlParameters[0] = parameterUserId;

            var parameterProductId = new SqlParameter("@productId", SqlDbType.Int, 4) { Value = productId };
            sqlParameters[1] = parameterProductId;

            var parameterBundleId = new SqlParameter("@sType", SqlDbType.Int, 4) { Value = bundleId };
            sqlParameters[2] = parameterBundleId;

            var parameterTestSubGroup = new SqlParameter("@testSubGroup", SqlDbType.Int, 4) { Value = testSubGroup };
            sqlParameters[3] = parameterTestSubGroup;

            var parameterTimeOffset = new SqlParameter("@timeOffset", SqlDbType.Int, 4) { Value = timeOffset };
            sqlParameters[4] = parameterTimeOffset;
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetAllTests", sqlParameters))
            {
                while (reader.Read())
                {
                    tests.Add(new Test
                    {
                        Type = (reader["Type"] as String) ?? string.Empty,
                        TestName = (reader["TestName"] as String) ?? string.Empty,
                        StartDate = (reader["StartDate"] as DateTime?) ?? DateTime.MinValue,
                        EndDate = (reader["EndDate"] as DateTime?) ?? DateTime.MinValue,
                        TestId = (reader["TestID"] as int?) ?? 0,
                        TestNumber = (reader["TestNumber"] as int?) ?? 0,
                        TestSubGroup = (reader["TestSubGroup"] as int?) ?? 0,
                        ProgramId = (reader["ProgramID"] as int?) ?? 0,
                        UserId = (reader["UserID"] as int?) ?? 0,
                        StudentStartDate = (reader["Student_StartDate"] as DateTime?) ?? DateTime.MinValue,
                        StudentEndDate = (reader["Student_EndDate"] as DateTime?) ?? DateTime.MinValue,
                        CohortId = (reader["CohortID"] as int?) ?? 0,
                        GroupId = (reader["GroupID"] as int?) ?? 0,
                        GroupStartDate = (reader["Group_StartDate"] as DateTime?) ?? DateTime.MinValue,
                        GroupEndDate = (reader["Group_EndDate"] as DateTime?) ?? DateTime.MinValue,
                        ProductId = (reader["ProductID"] as int?) ?? 0,
                        StartDateAll = (reader["StartDate_All"] as DateTime?) ?? DateTime.MinValue,
                        EndDateAll = (reader["EndDate_All"] as DateTime?) ?? DateTime.MinValue
                    });
                }
            }

            return tests.ToArray();
        }

        public IEnumerable<FinishedTest> GetFinishedTests(int userId, int productId, int timeOffset)
        {
            var tests = new List<FinishedTest>();
            #region Sql Parameters
            var sqlParameters = new SqlParameter[3];
            var parameterUserId = new SqlParameter("@userId", SqlDbType.Int, 4) { Value = userId };
            sqlParameters[0] = parameterUserId;

            var parameterProductId = new SqlParameter("@productId", SqlDbType.Int, 4) { Value = productId };
            sqlParameters[1] = parameterProductId;

            var parameterTimeOffset = new SqlParameter("@timeOffset", SqlDbType.Int, 4) { Value = timeOffset };
            sqlParameters[2] = parameterTimeOffset;
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetFinishedTests", sqlParameters))
            {
                while (reader.Read())
                {
                    tests.Add(new FinishedTest
                    {
                        PercentCorrect = (reader["PercentCorrect"] as string) ?? string.Empty,
                        ProductName = (reader["ProductName"] as string) ?? string.Empty,
                        QuestionCount = (reader["QuestionCount"] as int?) ?? 0,
                        QuizOrQBank = ((reader["QuizOrQBank"] as string) ?? "Q") == "Q" ? TestType.Quiz : TestType.Qbank,
                        TestId = (reader["TestID"] as int?) ?? 0,
                        TestName = (reader["TestName"] as string) ?? string.Empty,
                        TestStarted = (reader["TestStarted"] as DateTime?) ?? DateTime.MinValue,
                        TestStatus = (reader["TestStatus"] as int?) ?? 0,
                        UserTestId = (reader["UserTestId"] as int?) ?? 0
                    });
                }
            }

            return tests.ToArray();
        }


        public IEnumerable<UserTest> GetCustomizedFRTests(int userId, int timeOffSet)
        {
            var tests = new List<UserTest>();
            #region SqlParameter
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@UserId", SqlDbType.Int, 4) { Value = userId };
            sqlParameters[1] = new SqlParameter("@TimeOffset", SqlDbType.Int, 4) { Value = timeOffSet };

            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetCustomizedFRTests", sqlParameters))
            {
                while (reader.Read())
                {
                    tests.Add(new UserTest
                    {
                        UserTestId = (reader["UserTestID"] as int?) ?? 0,
                        TestId = (reader["TestID"] as int?) ?? 0,
                        TestNumber = (reader["TestNumber"] as int?) ?? 0,
                        TestStarted = (reader["TestStarted"] as DateTime?) ?? DateTime.MinValue,
                        TestName = (reader["TestName"] as String) ?? string.Empty
                    });
                }
            }

            return tests.ToArray();
        }

        public IEnumerable<UserTest> GetSkillsModulesAvailableQuizzes(int userId, int timeOffSet, int productId)
        {
            var tests = new List<UserTest>();
            #region SqlParameter
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@UserId", SqlDbType.Int, 4) { Value = userId };
            sqlParameters[1] = new SqlParameter("@TimeOffset", SqlDbType.Int, 4) { Value = timeOffSet };
            sqlParameters[2] = new SqlParameter("@ProductId", SqlDbType.Int, 4) { Value = productId };

            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetSkillsModulesAvailableQuizzes", sqlParameters))
            {
                while (reader.Read())
                {
                    tests.Add(new UserTest
                    {
                        UserTestId = (reader["UserTestID"] as int?) ?? 0,
                        TestId = (reader["TestID"] as int?) ?? 0,
                        TestNumber = (reader["TestNumber"] as int?) ?? 0,
                        TestStarted = (reader["TestStarted"] as DateTime?) ?? DateTime.MinValue,
                        TestName = (reader["TestName"] as String) ?? string.Empty
                    });
                }
            }

            return tests.ToArray();
        }

        public double GetPercentileRank(int testId, int correctAnswers)
        {
            #region SqlParameter
            var sqlParameters = new SqlParameter[2];
            var parameterTestId = new SqlParameter("@testId", SqlDbType.Int, 4) { Value = testId };
            sqlParameters[0] = parameterTestId;

            var parameterCorrect = new SqlParameter("@correct", SqlDbType.Int, 4) { Value = correctAnswers };
            sqlParameters[1] = parameterCorrect;
            #endregion
            var result = _dataContext.ExecuteScalar("uspGetPercentileRank", sqlParameters) as double?;
            return result ?? -1;
        }

        public double GetProbability(int testId, int correctAnswers)
        {
            #region SqlParameter
            var sqlParameters = new SqlParameter[2];
            var parameterTestId = new SqlParameter("@testId", SqlDbType.Int, 4) { Value = testId };
            sqlParameters[0] = parameterTestId;

            var parameterCorrect = new SqlParameter("@correct", SqlDbType.Int, 4) { Value = correctAnswers };
            sqlParameters[1] = parameterCorrect;
            #endregion

            var result = (_dataContext.ExecuteScalar("uspGetProbability", sqlParameters) as double?) ?? 0;
            return result;
        }

        public IEnumerable<ProgramResults> GetQBankTestPerformancyByChartType(int userID, int productID, int chartType, int overViewOrDetails)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[3];
            var parameterUserId = new SqlParameter("@UserID", SqlDbType.Int, 4) { Value = userID };
            sqlParameters[0] = parameterUserId;

            var parameterProductId = new SqlParameter("@ProductID", SqlDbType.Int, 4) { Value = productID };
            sqlParameters[1] = parameterProductId;

            var parameterTimeOffset = new SqlParameter("@ChartType", SqlDbType.Int, 4) { Value = chartType };
            sqlParameters[2] = parameterTimeOffset;
            #endregion

            var qBankTestPerformanceList = new List<ProgramResults>();
            var sproc = overViewOrDetails == 0 ? "uspGetPerformanceDetailsByProductIDChartType" : "uspGetPerformanceOverviewByProductIDChartType";
            using (var reader = _dataContext.GetDataReader(sproc, sqlParameters))
            {
                while (reader.Read())
                {
                    if (sproc == "uspGetPerformanceDetailsByProductIDChartType")
                    {
                        qBankTestPerformanceList.Add(new ProgramResults
                        {
                            Total = (reader["Total"] as int?) ?? 0,
                            DisplayTotal = ((reader["Total"] as int?) ?? 0) == 0 ? 0 : Convert.ToDecimal(reader["Total"]),
                            ItemText = (reader["ItemText"] as string) ?? string.Empty,
                            Correct = (reader["N_Correct"] as int?) ?? 0
                        });
                    }
                    else
                    {
                        qBankTestPerformanceList.Add(new ProgramResults
                        {
                            Total = (reader["Total"] as int?) ?? 0,
                            DisplayTotal = ((reader["Total"] as int?) ?? 0) == 0 ? 0 : Convert.ToDecimal(reader["Total"]),
                            Correct = (reader["N_Correct"] as int?) ?? 0,
                            Incorrect = (reader["N_InCorrect"] as int?) ?? 0,
                            UnAnswered = (reader["N_NAnswered"] as int?) ?? 0,
                            CorrectToIncorrect = (reader["N_CI"] as int?) ?? 0,
                            IncorrectToCorrect = (reader["N_IC"] as int?) ?? 0,
                            IncorrectToIncorrect = (reader["N_II"] as int?) ?? 0,
                            UserTestID = (reader["UserTestID"] as int?) ?? 0
                        });
                    }
                }
            }

            return qBankTestPerformanceList.ToArray();
        }

        public IEnumerable<Category> GetStudentTestCharacteristics(int testId)
        {
            #region SqlParameters

            var categories = new List<Category>();
            var sqlParameters = new SqlParameter[1];
            var parameterTestId = new SqlParameter("@testId", SqlDbType.Int, 4) { Value = testId };
            sqlParameters[0] = parameterTestId;
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetStudentTestCharacteristics", sqlParameters))
            {
                while (reader.Read())
                {
                    categories.Add(new Category()
                       {
                           TableName = (reader["TableName"] as string) ?? string.Empty,
                           CategoryID = (reader["CategoryID"] as int?) ?? 0
                       });
                }
            }

            return categories.ToArray();
        }

        public IEnumerable<UserTest> GetSuspendedTests(int userId, int productId, int testSubGroup, int timeOffset)
        {
            return GetUserTests(userId, productId, testSubGroup, timeOffset, 0);
        }

        public IEnumerable<UserTest> GetTakenTests(int userId, int productId, int testSubGroup, int timeOffset)
        {
            return GetUserTests(userId, productId, testSubGroup, timeOffset, 1);
        }

        public IEnumerable<UserTest> GetTestByProductUser(int userId, int productId, int timeOffset)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[3];
            var parameterProductId = new SqlParameter("@productId", SqlDbType.Int, 4) { Value = productId };
            sqlParameters[0] = parameterProductId;

            var parameterUserId = new SqlParameter("@userId", SqlDbType.Int, 4) { Value = userId };
            sqlParameters[1] = parameterUserId;

            var parameterTimeOffset = new SqlParameter("@hour", SqlDbType.Int, 4) { Value = timeOffset };
            sqlParameters[2] = parameterTimeOffset;
            #endregion

            var userTests = new List<UserTest>();
            using (IDataReader reader = _dataContext.GetDataReader("uspGetTestsByProductUser", sqlParameters))
            {
                while (reader.Read())
                {
                    userTests.Add(new UserTest
                    {
                        UserTestId = (reader["UserTestID"] as int?) ?? 0,
                        TestName = (reader["TestName"] as string) ?? string.Empty,
                    });
                }
            }

            return userTests.ToArray();
        }

        public string GetTestName(int testId)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[1];
            var parameterTestId = new SqlParameter("@testId", SqlDbType.Int, 4) { Value = testId };
            sqlParameters[0] = parameterTestId;
            #endregion

            return (_dataContext.ExecuteScalar("uspGetTestName", sqlParameters) as string) ?? string.Empty;
        }

        public IEnumerable<UserQuestion> GetTestQuestionsForUserId(int userTestId, string typeOfFileId)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[2];
            var parameterUserTestId = new SqlParameter("@UserTestID", SqlDbType.Int, 4) { Value = userTestId };
            sqlParameters[0] = parameterUserTestId;

            var parameterTypeOfFileId = new SqlParameter("@TypeOfFileID", SqlDbType.VarChar, 500) { Value = typeOfFileId };
            sqlParameters[1] = parameterTypeOfFileId;
            #endregion

            var testQuestions = new List<UserQuestion>();
            using (IDataReader reader = _dataContext.GetDataReader("UspGetListOfItemsForTestForUserI", sqlParameters))
            {
                while (reader.Read())
                {
                    testQuestions.Add(new UserQuestion
                    {
                        QID = (reader["QID"] as int?) ?? 0,
                        QuestionID = (reader["QuestionID"] as string) ?? string.Empty,
                        QuestionType = (reader["QuestionType"] as string) ?? string.Empty,
                        RemediationID = (reader["RemediationID"] as string) ?? string.Empty,
                        TopicTitle = (reader["TopicTitle"] as string) ?? string.Empty,
                        QuestionNumber = (reader["QuestionNumber"] as int?) ?? 0,
                        TypeOfFileID = (reader["TypeOfFileID"] as string) ?? string.Empty,
                        TimeSpendForQuestion = (reader["TimeSpendForQuestion"] as int?) ?? 0,
                        TimeSpendForRemedation = (reader["TimeSpendForRemedation"] as int?) ?? 0,
                        Correct = (reader["Correct"] as int?) ?? 0,
                        LevelOfDifficulty = (reader["LevelOfDifficulty"] as string) ?? string.Empty,
                        NursingProcess = (reader["NursingProcess"] as string) ?? string.Empty,
                        ClinicalConcept = (reader["ClinicalConcept"] as string) ?? string.Empty,
                        Demographic = (reader["Demographic"] as string) ?? string.Empty,
                        CriticalThinking = (reader["CriticalThinking"] as string) ?? string.Empty,
                        SpecialtyArea = (reader["SpecialtyArea"] as string) ?? string.Empty,
                        Systems = (reader["Systems"] as string) ?? string.Empty,
                        CognitiveLevel = (reader["CognitiveLevel"] as string) ?? string.Empty,
                        ClientNeeds = (reader["ClientNeeds"] as string) ?? string.Empty,
                        AccreditationCategories = (reader["AccreditationCategories"] as string) ?? string.Empty,
                        QSENKSACompetencies = (reader["QSENKSACompetencies"] as string) ?? string.Empty,
                        ClientNeedCategory = (reader["ClientNeedCategory"] as string) ?? string.Empty,
                        Concepts = (reader["Concepts"] as string) ?? string.Empty
                    });
                }
            }

            return testQuestions;
        }

        public IEnumerable<UserTest> GetTestsByUserProductSubGroup(int userId, int productId, int testSubGroup, int timeOffset)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[4];
            var parameterUserId = new SqlParameter("@UserId", SqlDbType.Int, 4) { Value = userId };
            sqlParameters[0] = parameterUserId;

            var parameterProductId = new SqlParameter("@ProductId", SqlDbType.Int, 4) { Value = productId };
            sqlParameters[1] = parameterProductId;

            var parameterTestSubGroup = new SqlParameter("@TestSubGroup", SqlDbType.Int, 4) { Value = testSubGroup };
            sqlParameters[2] = parameterTestSubGroup;

            var parameterTimeOffset = new SqlParameter("@TimeOffset", SqlDbType.Int, 4) { Value = timeOffset };
            sqlParameters[3] = parameterTimeOffset;
            #endregion

            var userTests = new List<UserTest>();
            using (IDataReader reader = _dataContext.GetDataReader("UspGetTestsByUserProductSubGroup", sqlParameters))
            {
                while (reader.Read())
                {
                    userTests.Add(new UserTest
                    {
                        TestStarted = (reader["TestStarted"] as DateTime?) ?? DateTime.MinValue,
                        UserTestId = (reader["UserTestID"] as int?) ?? 0
                    });
                }
            }

            return userTests;
        }

        /// <summary>
        /// This Method is used to fecth tests NClex info for the User
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="ProductID"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public IEnumerable<FinishedTest> GetTestsNCLEXInfoForTheUser(int UserID, int ProductID, int offset)
        {
            var finishedTests = new List<FinishedTest>();

            #region SqlParameters

            var sqlParameters = new SqlParameter[3];
            var parameterProductId = new SqlParameter("@ProductID", SqlDbType.Int, 4) { Value = ProductID };
            sqlParameters[0] = parameterProductId;

            var parameterUserid = new SqlParameter("@UserID", SqlDbType.Int, 4) { Value = UserID };
            sqlParameters[1] = parameterUserid;

            var parameterHour = new SqlParameter("@Hour", SqlDbType.Int, 4) { Value = offset };
            sqlParameters[2] = parameterHour;

            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("uspGetTestsByUserIDProductID", sqlParameters))
            {
                while (reader.Read())
                {
                    finishedTests.Add(new FinishedTest
                    {
                        PercentCorrect = string.Format("{0}%", reader["PercentCorrect"].ToPercent()),
                        ProductName = (reader["ProductName"] as string) ?? string.Empty,
                        QuestionCount = (reader["QuestionCount"] as int?) ?? 0,
                        QuizOrQBank = ((reader["QuizOrQBank"] as string) ?? "Q") == "Q" ? TestType.Quiz : TestType.Qbank,
                        TestId = (reader["TestID"] as int?) ?? 0,
                        TestName = (reader["TestName"] as string) ?? string.Empty,
                        TestStarted = (reader["TestStarted"] as DateTime?) ?? DateTime.MinValue,
                        TestStatus = (reader["TestStatus"] as int?) ?? 0,
                        UserTestId = (reader["UserTestId"] as int?) ?? 0,
                        TestSubGroup = (reader["TestSubGroup"] as int?) ?? 0
                    });
                }
            }

            return finishedTests.ToArray();
        }

        public TestType GetTestType(int testId)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            var parameterTestId = new SqlParameter("@testId", SqlDbType.Int, 4) { Value = testId };
            sqlParameters[0] = parameterTestId;
            #endregion

            int testType = (_dataContext.ExecuteScalar("uspGetTestType", sqlParameters) as int?) ?? 0;
            TestType type;
            switch (testType)
            {
                case 1:
                    type = TestType.Integrated;
                    break;
                case 2:
                    type = TestType.FocusedReview;
                    break;
                case 4:
                    type = TestType.Nclex;
                    break;
                default:
                    return TestType.Undefined;
            }

            return type;
        }

        public IEnumerable<Test> GetUntakenTests(int userId, int productId, int testSubGroup, int timeOffset)
        {
            var tests = new List<Test>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[4];
            var parameterUserId = new SqlParameter("@userId", SqlDbType.Int, 4) { Value = userId };
            sqlParameters[0] = parameterUserId;

            var parameterProductId = new SqlParameter("@productId", SqlDbType.Int, 4) { Value = productId };
            sqlParameters[1] = parameterProductId;

            var parameterTestSubGroup = new SqlParameter("@testSubGroup", SqlDbType.Int, 4) { Value = testSubGroup };
            sqlParameters[2] = parameterTestSubGroup;

            var parameterTimeOffset = new SqlParameter("@timeOffset", SqlDbType.Int, 4) { Value = timeOffset };
            sqlParameters[3] = parameterTimeOffset;
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetUntakenTests", sqlParameters))
            {
                while (reader.Read())
                {
                    tests.Add(new Test
                    {
                        Type = (reader["Type"] as String) ?? string.Empty,
                        TestName = (reader["TestName"] as String) ?? string.Empty,
                        StartDate = (reader["StartDate"] as DateTime?) ?? DateTime.MinValue,
                        EndDate = (reader["EndDate"] as DateTime?) ?? DateTime.MinValue,
                        TestId = (reader["TestID"] as int?) ?? 0,
                        TestNumber = (reader["TestNumber"] as int?) ?? 0,
                        TestSubGroup = (reader["TestSubGroup"] as int?) ?? 0,
                        ProgramId = (reader["ProgramID"] as int?) ?? 0,
                        UserId = (reader["UserID"] as int?) ?? 0,
                        StudentStartDate = (reader["Student_StartDate"] as DateTime?) ?? DateTime.MinValue,
                        StudentEndDate = (reader["Student_EndDate"] as DateTime?) ?? DateTime.MinValue,
                        CohortId = (reader["CohortID"] as int?) ?? 0,
                        GroupId = (reader["GroupID"] as int?) ?? 0,
                        GroupStartDate = (reader["Group_StartDate"] as DateTime?) ?? DateTime.MinValue,
                        GroupEndDate = (reader["Group_EndDate"] as DateTime?) ?? DateTime.MinValue,
                        ProductId = (reader["ProductID"] as int?) ?? 0,
                        StartDateAll = (reader["StartDate_All"] as DateTime?) ?? DateTime.MinValue,
                        EndDateAll = (reader["EndDate_All"] as DateTime?) ?? DateTime.MinValue
                    });
                }
            }

            return tests.ToArray();
        }

        public IEnumerable<UserTest> GetUserTestByID(int userTestId)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[1];
            var parameterTestId = new SqlParameter("@UserTestID", SqlDbType.Int, 4) { Value = userTestId };
            sqlParameters[0] = parameterTestId;

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

            return userTests.ToArray();
        }

        public IEnumerable<UserTest> GetUserTests(int userId, int productId, int testSubGroup, int timeOffset, int testStatus)
        {
            var userTests = new List<UserTest>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[5];
            var parameterUserId = new SqlParameter("@userId", SqlDbType.Int, 4) { Value = userId };
            sqlParameters[0] = parameterUserId;

            var parameterProductId = new SqlParameter("@productId", SqlDbType.Int, 4) { Value = productId };
            sqlParameters[1] = parameterProductId;

            var parameterTestSubGroup = new SqlParameter("@testSubGroup", SqlDbType.Int, 4) { Value = testSubGroup };
            sqlParameters[2] = parameterTestSubGroup;

            var parameterTimeOffset = new SqlParameter("@timeOffset", SqlDbType.Int, 4) { Value = timeOffset };
            sqlParameters[3] = parameterTimeOffset;

            var parameterTestStatus = new SqlParameter("@testStatus", SqlDbType.Int, 4) { Value = testStatus };
            sqlParameters[4] = parameterTestStatus;
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetUserTests", sqlParameters))
            {
                while (reader.Read())
                {
                    userTests.Add(new UserTest
                    {
                        TestStarted = (reader["TestStarted"] as DateTime?) ?? DateTime.MinValue,
                        TestId = (reader["TestID"] as int?) ?? 0,
                        SuspendType = (reader["SuspendType"] as string) ?? string.Empty,
                        UserTestId = (reader["UserTestID"] as int?) ?? 0,
                        TestName = (reader["TN"] as string) ?? string.Empty,
                        TestNumber = (reader["TestNumber"] as int?) ?? 0,
                        IsCustomizedFRTest = (reader["IsCustomizedFRTest"] as bool?) ?? false
                    });
                }
            }

            return userTests.ToArray();
        }

        public IEnumerable<ProductTest> LoadProductTest()
        {
            var productTests = new List<ProductTest>();
            using (IDataReader reader = _dataContext.GetDataReader("uspGetProductByTest"))
            {
                while (reader.Read())
                {
                    productTests.Add(new ProductTest
                    {
                        ProductId = (reader["ProductID"] as int?) ?? 0,
                        TestId = (reader["TestID"] as int?) ?? 0,
                        TestName = (reader["TestName"] as string) ?? string.Empty,
                        TestNumber = (reader["TestNumber"] as int?) ?? 0,
                        SecondPerQuestion = (reader["SecondPerQuestion"] as int?) ?? 0,
                    });
                }
            }

            return productTests.ToArray();
        }

        public bool TestExists(int userId, int productId, int testSubGroup, int type, int timeOffset)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[6];
            var parameterUserId = new SqlParameter("@userId", SqlDbType.Int, 4) { Value = userId };
            sqlParameters[0] = parameterUserId;

            var parameterProductId = new SqlParameter("@productId", SqlDbType.Int, 4) { Value = productId };
            sqlParameters[1] = parameterProductId;

            var parameterTestSubGroup = new SqlParameter("@testSubGroup", SqlDbType.Int, 4) { Value = testSubGroup };
            sqlParameters[2] = parameterTestSubGroup;

            var parameterStype = new SqlParameter("@sType", SqlDbType.Int, 4) { Value = type };
            sqlParameters[3] = parameterStype;

            var parameterTimeOffset = new SqlParameter("@timeOffset", SqlDbType.Int, 4) { Value = timeOffset };
            sqlParameters[4] = parameterTimeOffset;

            var parameterResult = new SqlParameter("@result", SqlDbType.Int, 4) { Direction = ParameterDirection.Output };
            sqlParameters[5] = parameterResult;
            #endregion

            _dataContext.ExecuteScalar("uspCheckTestExists", sqlParameters);
            int result;
            return Int32.TryParse(parameterResult.Value.ToString(), out result) && result == 1;
        }

        public void UpdateEndTest(UserTest userTest)
        {
            #region Sql Parameters

            var sqlParameters = new SqlParameter[5];
            var parameterUserTestId = new SqlParameter("@UserTestID", SqlDbType.Int, 4) { Value = userTest.UserTestId };
            sqlParameters[0] = parameterUserTestId;

            var parameterQuestionNumber = new SqlParameter("@SuspendQuestionNumber", SqlDbType.Int, 4) { Value = userTest.SuspendQuestionNumber };
            sqlParameters[1] = parameterQuestionNumber;

            var parameterSuspendQID = new SqlParameter("@SuspendQID", SqlDbType.Int, 4) { Value = userTest.SuspendQID };
            sqlParameters[2] = parameterSuspendQID;

            var parameterSuspendType = new SqlParameter("@SuspendType", SqlDbType.Char) { Value = userTest.SuspendType };
            sqlParameters[3] = parameterSuspendType;

            var parameterTimeRemaining = new SqlParameter("@TimeRemaining", SqlDbType.Char) { Value = userTest.TimeRemaining };
            sqlParameters[4] = parameterTimeRemaining;

            #endregion
            _dataContext.ExecuteNonQuery("uspUpdateTestCompleted", sqlParameters);
        }

        public void UpdateTestStatus(int userTestId, int testStatus)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[2];
            var parameterUserTestId = new SqlParameter("@userTestId", SqlDbType.Int, 4) { Value = userTestId };
            sqlParameters[0] = parameterUserTestId;

            var parameterTestStatus = new SqlParameter("@testStatus", SqlDbType.Int, 4) { Value = testStatus };
            sqlParameters[1] = parameterTestStatus;
            #endregion
            _dataContext.ExecuteNonQuery("uspUpdateTestStatus", sqlParameters);
        }

        public void UpdateTestStatusOnExpiry(int userTestId, int testStatus, int timeRemaining, int questionId)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@userTestId", SqlDbType.Int, 4) { Value = userTestId };
            sqlParameters[1] = new SqlParameter("@testStatus", SqlDbType.Int, 4) { Value = testStatus };
            sqlParameters[2] = new SqlParameter("@timeRemaining", SqlDbType.Int, 4) { Value = timeRemaining };
            sqlParameters[3] = new SqlParameter("@QuestionId", SqlDbType.Int, 4) { Value = questionId };
            #endregion
            _dataContext.ExecuteNonQuery("uspUpdateTestStatusOnExpiry", sqlParameters);
        }

        public int GetTestQuestionsCount(int testId, string fileType)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[3];
            var parameterUserTestId = new SqlParameter("@TestId", SqlDbType.Int, 4) { Value = testId };
            sqlParameters[0] = parameterUserTestId;

            var parameterTestStatus = new SqlParameter("@TypeOfFileId", SqlDbType.VarChar, 500) { Value = fileType };
            sqlParameters[1] = parameterTestStatus;

            var parameterTotalCount = new SqlParameter("@TotalCount", SqlDbType.Int, 4);
            parameterTotalCount.Direction = ParameterDirection.Output;
            sqlParameters[2] = parameterTotalCount;
            #endregion

            _dataContext.ExecuteStoredProcedure("USPGetTestQuestionCountByFileTypeId", sqlParameters);

            return (parameterTotalCount.Value as int?) ?? 0;
        }

        public int GetCustomFRTestQuestionCount(int userTestId)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@UserTestId", SqlDbType.Int, 4) { Value = userTestId };
            var questionCount = new SqlParameter("@TotalCount", SqlDbType.Int, 4) { Direction = ParameterDirection.Output };
            sqlParameters[1] = questionCount;
            #endregion

            _dataContext.ExecuteStoredProcedure("uspGetCustomFRTestQuestionCount", sqlParameters);
            return (questionCount.Value as int?) ?? 0;
        }

        public int GetQuestionCount(int userTestId)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@UserTestId", SqlDbType.Int, 4) { Value = userTestId };
            var questionCount = new SqlParameter("@TotalCount", SqlDbType.Int, 4) { Direction = ParameterDirection.Output };
            sqlParameters[1] = questionCount;
            #endregion

            _dataContext.ExecuteStoredProcedure("uspGetQuestionCount", sqlParameters);
            return (questionCount.Value as int?) ?? 0;
        }
        #endregion Methods
    }
}