using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using NursingLibrary.Common;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using Action = NursingLibrary.Presenters.Action;

namespace NursingLibrary.DAO
{
    public class StudentRepository : IStudentRepository
    {
        #region Fields

        private readonly IDataContext _dataContext;

        #endregion Fields

        #region Constructors

        public StudentRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #endregion Constructors

        #region Methods

        public bool ChangePassword(int userId, string newPassword)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[2];
            var parameterUserId = new SqlParameter("@userId", SqlDbType.Int, 4) { Value = userId };
            sqlParameters[0] = parameterUserId;

            var parameterPassword = new SqlParameter("@password", SqlDbType.VarChar, 255) { Value = newPassword };
            sqlParameters[1] = parameterPassword;
            #endregion
            return _dataContext.ExecuteNonQuery("uspChangePassword", sqlParameters) > 0;
        }

        public bool CheckExistCaseModuleStudent(int CID, int MID, string SID)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[3];

            var parameterCID = new SqlParameter("@CID", SqlDbType.Int, 4) { Value = CID };
            sqlParameters[0] = parameterCID;

            var parameterMID = new SqlParameter("@MID", SqlDbType.Int, 4) { Value = MID };
            sqlParameters[1] = parameterMID;

            var parameterSID = new SqlParameter("@SID", SqlDbType.VarChar, 255) { Value = SID };
            sqlParameters[2] = parameterSID;

            #endregion
            return _dataContext.ExecuteNonQuery("uspCheckExistCaseModuleStudent", sqlParameters) > 0;
        }

        public IEnumerable<AvpContent> GetAvpContent(int userId, int productId, int testSubGroup)
        {
            var avpContentList = new List<AvpContent>();
            #region Sql Parameters
            var sqlParameters = new SqlParameter[3];
            var parameterUserId = new SqlParameter("@userId", SqlDbType.Int, 4) { Value = userId };
            sqlParameters[0] = parameterUserId;

            var parameterProductId = new SqlParameter("@productId", SqlDbType.Int, 4) { Value = productId };
            sqlParameters[1] = parameterProductId;

            var parameterTestSubGroup = new SqlParameter("@testSubGroup", SqlDbType.Int, 4) { Value = testSubGroup };
            sqlParameters[2] = parameterTestSubGroup;
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetAvpItems", sqlParameters))
            {
                // pack data into the array
                while (reader.Read())
                {
                    var content = new AvpContent
                                      {
                                          TestName = (reader["TestName"] as string) ?? string.Empty,
                                          ProductId = (reader["ProductID"] as int?).GetValueOrDefault(0),
                                          PopWidth = (reader["PopWidth"] as int?).GetValueOrDefault(800),
                                          PopHeight = (reader["PopHeight"] as int?).GetValueOrDefault(800),
                                          Url = (reader["Url"] as string) ?? string.Empty
                                      };

                    avpContentList.Add(content);
                }
            }

            return avpContentList.ToArray();
        }

        public IEnumerable<CaseStudy> GetCaseStudies()
        {
            var caseStudies = new List<CaseStudy>();
            caseStudies.Add(new CaseStudy { CaseId = 100, CaseName = "Orientation Video", CaseOrder = -1 });
            using (IDataReader reader = _dataContext.GetDataReader("uspGetCaseStudies"))
            {
                while (reader.Read())
                {
                    caseStudies.Add(new CaseStudy { CaseId = (reader["CaseID"] as int?) ?? 0, CaseName = (reader["CaseName"] as string) ?? string.Empty, CaseOrder = (reader["CaseOrder"] as int?) ?? -1 });
                }
            }

            return caseStudies.ToArray();
        }

        public int GetNumberOfCategory()
        {
            return (_dataContext.ExecuteScalar("uspGetNumberOfCategory") as int?) ?? 0;
        }

        public ProgramResults GetProgramResults(int userTestId, int chartType)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            var parameterUserTestId = new SqlParameter("@userTestId", SqlDbType.Int, 4) { Value = userTestId };
            sqlParameters[0] = parameterUserTestId;

            var parameterChartType = new SqlParameter("@chartType", SqlDbType.Int, 4) { Value = chartType };
            sqlParameters[1] = parameterChartType;
            #endregion
            var programResults = new ProgramResults();
            using (IDataReader reader = _dataContext.GetDataReader("uspGetProgramResults", sqlParameters))
            {
                if (reader.Read())
                {
                    programResults = new ProgramResults
                    {
                        Correct = (reader["N_Correct"] as int?) ?? -1,
                        Incorrect = (reader["N_InCorrect"] as int?) ?? -1,
                        UnAnswered = (reader["N_NAnswered"] as int?) ?? -1,
                        CorrectToIncorrect = (reader["N_CI"] as int?) ?? -1,
                        IncorrectToCorrect = (reader["N_IC"] as int?) ?? -1,
                        IncorrectToIncorrect = (reader["N_II"] as int?) ?? -1,
                        Total = Convert.ToInt32(Math.Round(((reader["Total"] as decimal?) ?? -1))),
                        DisplayTotal = ((reader["Total"] as decimal?) ?? -1) == -1 ? -1 : Convert.ToDecimal(reader["Total"]),
                    };
                }
            }

            return programResults;
        }

        public IEnumerable<ProgramResults> GetProgramResultsByNorm(int userTestId, int testId)
        {
            #region SqlParameters

            var sqlParameters = new SqlParameter[2];
            var parameterUserTestId = new SqlParameter("@userTestId", SqlDbType.Int, 4) { Value = userTestId };
            sqlParameters[0] = parameterUserTestId;

            var parameterTestId = new SqlParameter("@testId", SqlDbType.Int, 4) { Value = testId };
            sqlParameters[1] = parameterTestId;

            #endregion

            var programResults = new List<ProgramResults>();
            using (IDataReader reader = _dataContext.GetDataReader("uspGetProgramResultsByNorm", sqlParameters))
            {
                while (reader.Read())
                {
                    programResults.Add(new ProgramResults
                    {
                        Correct = (reader["Correct_N"] as int?) ?? -1,
                        Total = (reader["Total"] as int?) ?? -1,
                        DisplayTotal = ((reader["Total"] as int?) ?? -1) == -1 ? -1 : Convert.ToDecimal(reader["Total"]),
                        ItemText = (reader["ItemText"] as string) ?? string.Empty,
                        Norm =
                            Convert.ToInt32(
                                Math.Round(((reader["Norm"] as Single?) ?? -1))),
                        ChartType = (reader["ChartType"] as string) ?? string.Empty,
                        DisplayNorm = (decimal)(reader["Norm"] as Single?).GetValueOrDefault(),
                    });
                }
            }

            return programResults.ToArray();
        }

        public Student GetStudent(string userName, string password)
        {
            Student student;
            using (var reader = LoginUser(userName, password))
            {
                if (reader.Read())
                {
                    student = LoadStudentInfo(reader);
                }
                else
                {
                    throw new LoginException(LoginFailure.AuthenticationFailed, null);
                }
            }

            return student;
        }

        public Student GetStudent(int userId)
        {
            Student student;
            using (var reader = GetStudentInfo(userId))
            {
                if (reader == null)
                {
                    throw new UserException(userId, "Student data could not be retrieved.");
                }

                student = LoadStudentInfo(reader);
            }

            return student;
        }

        public bool InsertModuleScore(CaseModuleScore moduleScore)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[6];

            var parameterCaseID = new SqlParameter("@CaseID", SqlDbType.Int, 4) { Value = moduleScore.CaseId };
            sqlParameters[0] = parameterCaseID;

            var parameterModuleID = new SqlParameter("@MID", SqlDbType.VarChar, 255) { Value = moduleScore.ModuleId };
            sqlParameters[1] = parameterModuleID;

            var parameterStudentID = new SqlParameter("@StudentID", SqlDbType.VarChar, 50) { Value = moduleScore.StudentId };
            sqlParameters[2] = parameterStudentID;

            var parameterCorrect = new SqlParameter("@Correct", SqlDbType.Int, 4) { Value = moduleScore.Correct };
            sqlParameters[3] = parameterCorrect;

            var parameterTotal = new SqlParameter("@Total", SqlDbType.Int, 4) { Value = moduleScore.Total };
            sqlParameters[4] = parameterTotal;

            var parameterId = new SqlParameter("@Id", SqlDbType.Int, 4) { Value = 0, Direction = ParameterDirection.InputOutput };
            sqlParameters[5] = parameterId;

            #endregion
            moduleScore.ModuleStudentId = (int)sqlParameters[5].Value;
            return _dataContext.ExecuteNonQuery("USPInsertCaseModuleScore", sqlParameters) > 0;
        }

        public bool InsertSubCategory(CaseSubCategory subCategory)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[6];
            var parameterModuleStudentID = new SqlParameter("@ModuleStudentID", SqlDbType.Int, 4) { Value = subCategory.ModuleStudentId };
            sqlParameters[0] = parameterModuleStudentID;

            var parameterSubcategoryID = new SqlParameter("@SubcategoryID", SqlDbType.Int, 4) { Value = subCategory.SubCategoryId };
            sqlParameters[1] = parameterSubcategoryID;

            var parameterCategoryName = new SqlParameter("@CategoryName", SqlDbType.VarChar, 50) { Value = subCategory.CategoryName };
            sqlParameters[2] = parameterCategoryName;

            var parameterCorrect = new SqlParameter("@Correct", SqlDbType.Int, 4) { Value = subCategory.Correct };
            sqlParameters[3] = parameterCorrect;

            var parameterTotal = new SqlParameter("@Total", SqlDbType.Int, 4) { Value = subCategory.Total };
            sqlParameters[4] = parameterTotal;

            var parameterCategoryID = new SqlParameter("@CategoryID", SqlDbType.Int, 4) { Value = subCategory.CategoryId };
            sqlParameters[5] = parameterCategoryID;
            #endregion
            return _dataContext.ExecuteNonQuery("uspInsertSubCategory", sqlParameters) > 0;
        }

        public CaseModuleScore LoadCaseModuleScore()
        {
            var clientNeeds = new CaseModuleScore();
            using (IDataReader reader = _dataContext.GetDataReader("uspGetCaseStudyResultModuleScore"))
            {
                while (reader.Read())
                {
                    clientNeeds.CaseId = (reader["CaseID"] as int?) ?? 0;
                    clientNeeds.Correct = (reader["Correct"] as int?) ?? 0;
                    clientNeeds.ModuleId = (reader["ModuleID"] as int?) ?? 0;
                    clientNeeds.ModuleStudentId = (reader["ModuleStudentID"] as int?) ?? 0;
                    clientNeeds.StudentId = reader["StudentID"].ToString();
                    clientNeeds.Total = (reader["Total"] as int?) ?? 0;
                }
            }

            return clientNeeds;
        }

        public CaseSubCategory LoadCaseSubCategory()
        {
            var clientNeeds = new CaseSubCategory();

            using (IDataReader reader = _dataContext.GetDataReader("uspGetCaseStudyResultSubCategoryScore"))
            {
                while (reader.Read())
                {
                    clientNeeds.CategoryId = (reader["CategoryID"] as int?) ?? 0;
                    clientNeeds.CategoryName = reader["CategoryName"].ToString();
                    clientNeeds.Correct = (reader["Correct"] as int?) ?? 0;
                    clientNeeds.Id = (reader["ID"] as int?) ?? 0;
                    clientNeeds.ModuleStudentId = (reader["ModuleStudentID"] as int?) ?? 0;
                    clientNeeds.SubCategoryId = (reader["SubCategoryID"] as int?) ?? 0;
                    clientNeeds.Total = (reader["Total"] as int?) ?? 0;
                }
            }

            return clientNeeds;
        }

        public IEnumerable<ClientNeeds> LoadClientNeeds(int programofStudyId)
        {
            var clientNeeds = new List<ClientNeeds>();
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ProgramofStudyId", SqlDbType.Int, 4) { Value = programofStudyId };
            using (IDataReader reader = _dataContext.GetDataReader("uspGetClientNeedsSummary",sqlParameters))
            {
                while (reader.Read())
                {
                    clientNeeds.Add(new ClientNeeds
                    {
                        Id = (reader["ClientNeedsID"] as int?) ?? 0,
                        Name = (reader["ClientNeeds"] as string) ?? string.Empty,
                        CategoryCount = (reader["ClientNeedCategoryCount"] as int?) ?? 0,
                    });
                }
            }

            return clientNeeds.ToArray();
        }

        public int CreateSkillsModulesDetails(int TestId, int UserId)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@SMUserId", SqlDbType.Int, 5) { Direction = ParameterDirection.Output };
            sqlParameters[1] = new SqlParameter("@TestId", SqlDbType.Int, 5) { Value = TestId };
            sqlParameters[2] = new SqlParameter("@UserId", SqlDbType.Int, 5) { Value = UserId };
            #endregion

            _dataContext.ExecuteStoredProcedure("uspCreateSkillsModulesDetails", sqlParameters);
            return (int)sqlParameters[0].Value;
        }

        public IEnumerable<ClientNeedsCategory> LoadClientNeedsCategoryInfo(int userID, int programofStudyId)
        {
            var clientNeedsCategoryInfo = new List<ClientNeedsCategory>();

            #region Sql Parameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@UserID", SqlDbType.Int, 4) { Value = userID };
             sqlParameters[1] = new SqlParameter("@ProgramofStudyId", SqlDbType.Int, 4) { Value = programofStudyId };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetClientNeedsCategoryInfo", sqlParameters))
            {
                while (reader.Read())
                {
                    clientNeedsCategoryInfo.Add(new ClientNeedsCategory
                    {
                        Id = (reader["ClientNeedsID"] as int?) ?? 0,
                        CategoryId = (reader["ClientNeedCategoryID"] as int?) ?? 0,
                        CategoryName = (reader["ClientNeedCategory"] as string) ?? string.Empty,
                        TotQCount = (reader["TotQCount"] as int?) ?? 0,
                        UnUsedIncorrectQCount = (reader["UnUsedIncorrectQCount"] as int?) ?? 0,
                        UnUsedQCount = (reader["UnUsedQCount"] as int?) ?? 0,
                        InCorrectQCount = (reader["InCorrectQCount"] as int?) ?? 0
                    });
                }
            }

            return clientNeedsCategoryInfo.ToArray();
        }

        public void SaveUserSession(int userId, string sessionId)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[2];
            var paramUserId = new SqlParameter("@userId", SqlDbType.Int, 4) { Value = userId };
            sqlParameters[0] = paramUserId;

            var paramSessionId = new SqlParameter("@sessionId", SqlDbType.VarChar, 50) { Value = sessionId };
            sqlParameters[1] = paramSessionId;
            #endregion

            _dataContext.ExecuteNonQuery("uspSaveSessionId", sqlParameters);
        }

        public string GetSession(int userId)
        {
            string sessionId = string.Empty;

            #region Sql Parameters
            var sqlParameters = new SqlParameter[1];
            var parameterUserId = new SqlParameter("@userId", SqlDbType.Int, 4) { Value = userId };
            sqlParameters[0] = parameterUserId;
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetSessionId", sqlParameters))
            {
                while (reader.Read())
                {
                    sessionId = (reader["SessionID"] as string) ?? string.Empty;
                }
            }

            return sessionId;
        }

        public string GetRemediationExplainationByID(int remediationId)
        {
            var sqlParameters = new SqlParameter[1];
            var parameterUserName = new SqlParameter("@RemediationId", SqlDbType.Int) { Value = remediationId };
            sqlParameters[0] = parameterUserName;

            return Convert.ToString(_dataContext.ExecuteScalar("USPGetRemediationExplainationByID", sqlParameters));
        }

        public IEnumerable<Systems> GetCategories(string typeIds)
        {
            var systems = new List<Systems>();
            #region Sql Parameters
            var sqlParameters = new SqlParameter[1];
            var parameterTypeId = new SqlParameter("@TypeIds", SqlDbType.VarChar, 200) { Value = typeIds };
            sqlParameters[0] = parameterTypeId;
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetLookupData", sqlParameters))
            {
                while (reader.Read())
                {
                    systems.Add(new Systems
                    {
                        SystemID = (reader["Id"] as int?) ?? 0,
                        System = (reader["DisplayText"] as string) ?? string.Empty,
                    });
                }
            }

            return systems.ToArray();
        }

        public IEnumerable<Topic> GetTopics(string categoryIds, bool isTest,int programofStudyId)
        {
            var topics = new List<Topic>();

            #region Sql Parameters
            var sqlParameters = new SqlParameter[3];
            var parameterSystemID = new SqlParameter("@CategoryIds", SqlDbType.VarChar, 2000) { Value = categoryIds };
            sqlParameters[0] = parameterSystemID;
            var parameterIsTest = new SqlParameter("@IsTest", SqlDbType.Bit) { Value = isTest };
            sqlParameters[1] = parameterIsTest;
            var parameterProgramofStudyId = new SqlParameter("@ProgramofStudyId", SqlDbType.Int) { Value = programofStudyId };
            sqlParameters[2] = parameterProgramofStudyId;
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetCFRTopics", sqlParameters))
            {
                while (reader.Read())
                {
                    topics.Add(new Topic
                    {
                        TopicTitle = (reader["DisplayText"] as string) ?? string.Empty,
                        RemediationId = (reader["Id"] as int?) ?? 0
                    });
                }
            }

            return topics.ToArray();
        }

        public void CreateFRQBankRemediation(ReviewRemediation ReviewRem, string systems, string topics, int programofstudyId)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[9];
            sqlParameters[0] = new SqlParameter("@RemReviewId", SqlDbType.Int) { Value = ReviewRem.ReviewRemId, Direction = ParameterDirection.InputOutput };
            sqlParameters[1] = new SqlParameter("@StudentId", SqlDbType.Int) { Value = ReviewRem.UserId };
            sqlParameters[2] = new SqlParameter("@Name", SqlDbType.VarChar) { Value = ReviewRem.ReviewRemName };
            sqlParameters[3] = new SqlParameter("@CreateDate", SqlDbType.DateTime) { Value = ReviewRem.CreateDate };
            sqlParameters[4] = new SqlParameter("@RemediatedTime", SqlDbType.Int) { Value = ReviewRem.TotalRemTime };
            sqlParameters[5] = new SqlParameter("@SystemIds", SqlDbType.VarChar) { Value = systems };
            sqlParameters[6] = new SqlParameter("@TopicIds", SqlDbType.VarChar) { Value = topics };
            sqlParameters[7] = new SqlParameter("@NoOfRemediations", SqlDbType.Int) { Value = ReviewRem.NoOfRemediations };
            sqlParameters[8] = new SqlParameter("@ProgramofStudyId", SqlDbType.Int) { Value = programofstudyId };

            #endregion

            _dataContext.ExecuteStoredProcedure("uspCreateFRRemediations", sqlParameters);
            ReviewRem.ReviewRemId = (int)sqlParameters[0].Value;
        }

        public int GetAvailableRemediations(string systems, string topics,int programofstudyId)
        {
            #region SqlParameters
            var remediations = new List<ReviewRemediation>();
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@CategoryIds", SqlDbType.VarChar) { Value = systems };
            sqlParameters[1] = new SqlParameter("@TopicIds", SqlDbType.VarChar) { Value = topics };
            sqlParameters[2] = new SqlParameter("@ProgramofStudyId", SqlDbType.Int) { Value = programofstudyId };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetFRRemediations", sqlParameters))
            {
                while (reader.Read())
                {
                    remediations.Add(new ReviewRemediation()
                    {
                        ReviewRemId = (reader["RID"] as int?) ?? 0,
                    });
                }
            }

            return remediations.Count();
        }

        public IEnumerable<ReviewRemediation> GetRemediationsForTheUser(int studentId)
        {
            var remediations = new List<ReviewRemediation>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@studentId", SqlDbType.Int) { Value = studentId };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetRemediationsForTheUser", sqlParameters))
            {
                while (reader.Read())
                {
                    remediations.Add(new ReviewRemediation()
                    {
                        ReviewRemId = (reader["RemReviewId"] as int?) ?? 0,
                        ReviewRemName = reader["Name"] as string ?? string.Empty,
                        UserId = (reader["StudentId"] as int?) ?? 0,
                        NoOfRemediations = (reader["NoOfRemediations"] as int?) ?? 0,
                        CreateDate = Convert.ToDateTime(reader["CreatedDate"].ToString())
                    });
                }
            }

            return remediations.ToArray();
        }

        public IEnumerable<ReviewRemediation> GetRemediationExplainationByReviewID(int revRemID)
        {
            var review = new List<ReviewRemediation>();
            #region SqlParameters

            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@RemReviewId", SqlDbType.Int) { Value = revRemID };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetRemediationExplanations", sqlParameters))
            {
                if (reader.Read())
                {
                    review.Add(new ReviewRemediation
                     {
                         ReviewRemId = (reader["RemReviewId"] as int?) ?? 0,
                         RemReviewQuestionId = (reader["RemReviewQuestionId"] as int?) ?? 0,
                         ReviewExplanation = (reader["Explanation"] as string) ?? string.Empty,
                         RemediationNumber = (reader["RemediationNumber"] as int?) ?? 0,
                         ReviewRemName = (reader["Name"] as string) ?? string.Empty,
                         RemediatedTime = (reader["RemediatedTime"] as int?) ?? 0,
                         TopicTitle = (reader["TopicTitle"] as string) ?? string.Empty
                     });
                }
            }

            return review.ToArray();
        }

        public IEnumerable<Lippincott> GetLippincottForReviewRemediation(int revRemQID)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@RemReviewQuestionId", SqlDbType.Int, 4) { Value = revRemQID };

            var lippincott = new List<Lippincott>();
            using (var reader = _dataContext.GetDataReader("uspGetLippincottForReviewRemediation", sqlParameters))
            {
                while (reader.Read())
                {
                    lippincott.Add(new Lippincott
                    {
                        LippincottTitle =
                            (reader["LippincottTitle"] as string) ?? string.Empty,
                        LippincottExplanation =
                            (reader["LippincottExplanation"] as string) ?? string.Empty,
                        LippincottTitle2 =
                            (reader["LippincottTitle2"] as string) ?? string.Empty,
                        LippincottExplanation2 =
                            (reader["LippincottExplanation2"] as string) ?? string.Empty,
                        LippincottID = (reader["LippincottID"] as int?) ?? 0
                    });
                }
            }

            return lippincott.Distinct(new LippincottComparer()).ToArray();
        }

        public void UpdateReviewRemediation(int reviewId, int time)
        {
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@RemReviewQuestionId", SqlDbType.Int, 4) { Value = reviewId };
            sqlParameters[1] = new SqlParameter("@RemTime", SqlDbType.Int, 4) { Value = time };
            _dataContext.ExecuteNonQuery("uspUpdateReviewRemediation", sqlParameters);
        }

        public void DeleteRemediations(int remediationReviewId)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@remediationReviewId", SqlDbType.Int) { Value = remediationReviewId };
            #endregion

            _dataContext.ExecuteNonQuery("uspDeleteRemediationReview", sqlParameters);
        }

        public IEnumerable<ReviewRemediation> GetNextPrevRemediation(int Id, int RemNumber, string action)
        {
            var review = new List<ReviewRemediation>();
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@RemReviewId", SqlDbType.Int, 4) { Value = Id };
            sqlParameters[1] = new SqlParameter("@RemediationNumber", SqlDbType.Int, 4) { Value = RemNumber };
            sqlParameters[2] = new SqlParameter("@Action", SqlDbType.Char, 4) { Value = action };

            using (IDataReader reader = _dataContext.GetDataReader("uspGetPrevNextRemediations", sqlParameters))
            {
                while (reader.Read())
                {
                    review.Add(new ReviewRemediation
                    {
                        RemReviewQuestionId = (reader["RemReviewQuestionId"] as int?) ?? 0,
                        RemediationNumber = (reader["RemediationNumber"] as int?) ?? 0,
                        ReviewExplanation = (reader["Explanation"] as string) ?? string.Empty,
                        RemediatedTime = (reader["RemediatedTime"] as int?) ?? 0,
                        ReviewRemName = (reader["Name"] as string) ?? string.Empty
                    });
                }
            }

            return review.ToArray();
        }

        public void UpdateTotalRemediatedTime(int remediationReviewId)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@RemReviewId", SqlDbType.Int, 4) { Value = remediationReviewId };
            _dataContext.ExecuteNonQuery("uspUpdateReviewRemediationTotalTime", sqlParameters);
        }

        public UserTest CreateFRQBankRepeatTest(int userTestId)
        {
            var newTest = new UserTest();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@OldUserTestId", SqlDbType.Int) { Value = userTestId };
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("uspCreateFRQBankTestRepeat", sqlParameters))
            {
                while (reader.Read())
                {
                    newTest.UserTestId = (reader["UserTestID"] as int?) ?? 0;
                    newTest.TestId = (reader["TestId"] as int?) ?? 0;
                }
            }

            return newTest;
        }

        public void UpdateAltTabClick(int userTestId, int QId, bool IsAltTabClicked)
        {
            var newTest = new UserTest();
            #region SqlParameters
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@userTestId", SqlDbType.Int) { Value = userTestId };
            sqlParameters[1] = new SqlParameter("@QId", SqlDbType.Int) { Value = QId };
            sqlParameters[2] = new SqlParameter("@AltTabClicked", SqlDbType.Bit) { Value = IsAltTabClicked };
            #endregion
            _dataContext.ExecuteNonQuery("uspUpdateAltTabClick", sqlParameters);
        }

        public IEnumerable<SMUserVideoTransaction> GetSkillsModuleVideos(int skillsModuleUserId)
        {
            var SMUserVideoTrans = new List<SMUserVideoTransaction>();
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@SMUserId", SqlDbType.Int, 6) { Value = skillsModuleUserId };

            using (IDataReader reader = _dataContext.GetDataReader("uspGetSkillModuleUserVideos", sqlParameters))
            {
                while (reader.Read())
                {
                    SMUserVideoTrans.Add(new SMUserVideoTransaction
                    {
                        SMUserVideoId = (reader["SMUserVideoId"] as int?) ?? 0,
                        SMUserId = (reader["SMUserId"] as int?) ?? 0,
                        IsPageFullyViewed = (reader["IsPageFullyViewed"] as bool?) ?? false,
                        SMOrder = (reader["SMOrder"] as int?) ?? 0,
                        SMCount = (reader["Count"] as int?) ?? 0,
                        IsVideoFullyViewed = (reader["IsVideoFullyViewed"] as bool?) ?? false,
                        SkillsModuleVideo = new SkillsModuleVideos()
                        {
                            MP4 = (reader["MP4"] as string) ?? string.Empty,
                            F4V = (reader["F4V"] as string) ?? string.Empty,
                            OGV = (reader["OGV"] as string) ?? string.Empty,
                            Type = (reader["Type"] as int?) ?? 0,
                            TextPosition = (reader["TextPosition"] as bool?) ?? false,
                            Title = (reader["Title"] as string) ?? string.Empty,
                            Text = (reader["Text"] as string) ?? string.Empty
                        }
                    });
                }
            }

            return SMUserVideoTrans.ToArray();
        }

        public IEnumerable<SMTest> GetSkillsModuleTests(int SMUserId, int TestId)
        {
            var SMTests = new List<SMTest>();
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@UserId", SqlDbType.Int, 6) { Value = SMUserId };
            sqlParameters[1] = new SqlParameter("@TestId", SqlDbType.Int, 6) { Value = TestId };

            using (IDataReader reader = _dataContext.GetDataReader("uspGetSMTests", sqlParameters))
            {
                while (reader.Read())
                {
                    SMTests.Add(new SMTest
                    {
                        SMTestId = (reader["SMTestId"] as int?) ?? 0,
                        UserId = (reader["UserId"] as int?) ?? 0,
                        SkillModuleId = (reader["TestId"] as int?) ?? 0,
                    });
                }
            }

            return SMTests.ToArray();
        }

        public void UpdateSkillModuleStatus(int SMUserVideoId)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@SMUserVideoId", SqlDbType.Int, 4) { Value = SMUserVideoId };
            _dataContext.ExecuteNonQuery("uspUpdateSkillsModuleStatus", sqlParameters);
        }

        public void UpdateSkillModuleVideoStatus(int SMUserVideoId)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@SMUserVideoId", SqlDbType.Int, 4) { Value = SMUserVideoId };
            _dataContext.ExecuteNonQuery("uspUpdateSkillsModuleVideoStatus", sqlParameters);
        }

        public void ResetSkillModuleStatus(int SMUserId)
        {
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@SMUserId", SqlDbType.Int, 4) { Value = SMUserId };
            _dataContext.ExecuteNonQuery("uspResetSkillsModuleStatus", sqlParameters);
        }

        public Student GetStudentInfoByUserNameEmail(string userName, string email)
        {
            Student student = new Student();
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@UserName", SqlDbType.NVarChar, 80) { Value = userName };
            sqlParameters[1] = new SqlParameter("@Email", SqlDbType.NVarChar, 80) { Value = email };

            using (IDataReader reader = _dataContext.GetDataReader("uspGetStudentByUserNameEmailId", sqlParameters))
            {
                while (reader.Read())
                {
                    student.UserId = (reader["UserID"] as int?) ?? 0;
                    student.UserName = reader["UserName"] as string ?? string.Empty;
                    student.FirstName = reader["FirstName"] as string ?? string.Empty;
                    student.LastName = reader["LastName"] as string ?? string.Empty;
                    student.Password = reader["UserPass"] as string ?? string.Empty;
                    student.Email = reader["Email"] as string ?? string.Empty;
                    student.InstitutionId = (reader["InstitutionID"] as int?) ?? 0;
                }
            }

            return student;
        }

        public int GetSkillModuleUserId(int testId, int userId)
        {
            int SMUserId = 0;
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@NewTestId", SqlDbType.Int) { Value = testId };
            sqlParameters[1] = new SqlParameter("@UserId", SqlDbType.Int) { Value = userId };

            using (IDataReader reader = _dataContext.GetDataReader("uspGetSMTest", sqlParameters))
            {
                while (reader.Read())
                {
                    SMUserId = (reader["SMUserId"] as int?) ?? 0;
                }
            }

            return SMUserId;
        }

        public int GetOriginalSMTestId(int newtestId, int userId)
        {
            int testId = 0;
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@NewTestId", SqlDbType.Int) { Value = newtestId };
            sqlParameters[1] = new SqlParameter("@UserId", SqlDbType.Int) { Value = userId };

            using (IDataReader reader = _dataContext.GetDataReader("uspGetOriginalSMTestId", sqlParameters))
            {
                while (reader.Read())
                {
                    testId = (reader["TestId"] as int?) ?? 0;
                }
            }

            return testId;
        }

        public IEnumerable<SMTest> GetSkillsModuleTestsByUserId(int SMUserId)
        {
            var SMTests = new List<SMTest>();
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@UserId", SqlDbType.Int, 6) { Value = SMUserId };

            using (IDataReader reader = _dataContext.GetDataReader("uspGetSMTestsByUserId", sqlParameters))
            {
                while (reader.Read())
                {
                    SMTests.Add(new SMTest
                    {
                        SMTestId = (reader["SMTestId"] as int?) ?? 0,
                        UserId = (reader["UserId"] as int?) ?? 0,
                        SkillModuleId = (reader["TestId"] as int?) ?? 0,
                        NewTestId = (reader["NewTestId"] as int?) ?? 0,
                    });
                }
            }

            return SMTests.ToArray();
        }

        public void UpdateScrambledAnswerChoice(string scrambledAnsOrder, int userTestId, int questionId)
        {
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@ScrambledAnswerChoice", SqlDbType.VarChar, 50) { Value = scrambledAnsOrder };
            sqlParameters[1] = new SqlParameter("@UserTestId", SqlDbType.Int, 4) { Value = userTestId };
            sqlParameters[2] = new SqlParameter("@QuestionId", SqlDbType.Int, 4) { Value = questionId };
            _dataContext.ExecuteNonQuery("uspUpdateScrambledAnswerChoice", sqlParameters);
        }

        public string GetScrambledAnswerChoice(int userTestId, int questionId)
        {
            var scrambledAnswerChoice = string.Empty;
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@UserTestId", SqlDbType.Int, 4) { Value = userTestId };
            sqlParameters[1] = new SqlParameter("@QuestionId", SqlDbType.Int, 4) { Value = questionId };
            using (IDataReader reader = _dataContext.GetDataReader("uspGetScrambledAnswerChoice", sqlParameters))
               {
                   while (reader.Read())
                   {
                       scrambledAnswerChoice = (reader["scrambledAnswerChoice"] as string) ?? string.Empty;
                   }
               }

            return scrambledAnswerChoice;
        }

        public IEnumerable<AssetDetail> GetDashBoardLinks(int programId)
        {
            var assetDetails = new List<AssetDetail>();
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ProgramId", SqlDbType.Int, 6) { Value = programId };

            using (IDataReader reader = _dataContext.GetDataReader("uspGetDashBoardLinks", sqlParameters))
            {
                while (reader.Read())
                {
                    assetDetails.Add(new AssetDetail
                    {
                        ProgramofStudyId = (reader["ProgramofStudyId"] as int?) ?? 0,
                        order = (reader["AssetLocationOrder"] as int?) ?? 0,
                        AssetName = (reader["AssetName"] as string ?? string.Empty),
                        AssetValue = (reader["AssetValue"] as string ?? string.Empty),
                        AssetLocationType = (reader["AssetLocationType"] as short?) ?? 0
                    });
                }
            }

            return assetDetails;
        }

        public IEnumerable<AssetGroup> GetAssetGroupByProgramId(int programId)
        {
            var assetGroup = new List<AssetGroup>();
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ProgramId", SqlDbType.Int, 6) { Value = programId };

            using (IDataReader reader = _dataContext.GetDataReader("uspGetAssetGroupByProgramId", sqlParameters))
            {
                while (reader.Read())
                {
                    assetGroup.Add(new AssetGroup
                    {
                        AssetGroupId = (reader["AssetGroupId"] as int?) ?? 0,
                        ProgramofStudyId = (reader["ProgramofStudyId"] as int?) ?? 0,
                        AssetGroupName = (reader["AssetGroupName"] as string ?? string.Empty),
                    });
                }
            }

            return assetGroup;
        }

        public IEnumerable<Test> GetQBankTest(int userId, int productId, int testSubGroupId, int timeOffSet)
        {
            var qbankTest = new List<Test>();
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@UserId", SqlDbType.Int, 6) { Value = userId };
            sqlParameters[1] = new SqlParameter("@ProductId", SqlDbType.Int, 6) { Value = productId };
            sqlParameters[2] = new SqlParameter("@TestSubGroupId", SqlDbType.Int, 6) { Value = testSubGroupId };
            sqlParameters[3] = new SqlParameter("@TimeOffSet", SqlDbType.Int, 6) { Value = timeOffSet };

            using (IDataReader reader = _dataContext.GetDataReader("uspGetQbankTest", sqlParameters))
            {
                while (reader.Read())
                {
                    qbankTest.Add(new Test
                                      {
                                          TestId = (reader["TestId"] as int?) ?? 0,
                                          ProgramofStudyId = (reader["ProgramofStudyId"] as int?) ?? 0,
                                      });
                }
            }

            return qbankTest;
        }

        public string GetUserDetailsById(int userId)
        {
            var userPass = string.Empty;
            var reader = GetStudentInfo(userId);
            while (reader.Read())
            {
             userPass = reader["UserPass"] as string ?? string.Empty;
            }
            return userPass;
        }

        private static Student LoadStudentInfo(IDataReader reader)
        {
            var student = new Student
                              {
                                  UserId = (reader["UserID"] as int?) ?? 0,
                                  UserName = reader["UserName"] as string ?? string.Empty,
                                  FirstName = reader["FirstName"] as string ?? string.Empty,
                                  LastName = reader["LastName"] as string ?? string.Empty,
                                  KaplanUserId = reader["KaplanUserID"] as string ?? string.Empty,
                                  CohortId = (reader["CohortId"] as int?) ?? -1,
                                  GroupId = (reader["GroupId"] as int?) ?? -1,
                                  InstitutionId = (reader["InstitutionId"] as int?) ?? 0,
                                  ProgramId = (reader["ProgramId"] as int?) ?? 0,
                                  TimeOffset = (reader["TimeOffset"] as int?) ?? 0,
                                  Ada = (reader["Ada"] as bool?) ?? false,
                                  EnrollmentId = reader["enrollmentId"] as string ?? string.Empty,
                                  InstitutionIpLock = reader["Ip"] as string ?? string.Empty,
                                  ManageAccount = (reader["PayLinkEnabled"] as bool?) ?? false,
                                  IsIntegratedTest = (reader["TestExistsIntegrated"] as bool?) ?? false,
                                  IsFocusedReviewTest = (reader["TestExistsFocussed"] as bool?) ?? false,
                                  IsNclexTest = (reader["TestExistsNclex"] as bool?) ?? false,
                                  IsQbankTest = (reader["TestExistsQbank"] as bool?) ?? false,
                                  IsQbankSampleTest = (reader["TestExistsQbankSample"] as bool?) ?? false,
                                  IsTimedQbankTest = (reader["TestExistsTimedQbank"] as bool?) ?? false,
                                  IsDignosticTest = (reader["TestExistsDiagnostic"] as bool?) ?? false,
                                  IsReadinessTest = (reader["TestExistsReadiness"] as bool?) ?? false,
                                  IsDignosticResultTest = (reader["TestExistsDiagnosticResult"] as bool?) ?? false,
                                  IsReadinessResultTest = (reader["TestExistsReadinessResult"] as bool?) ?? false,
                                  TestId = -1,
                                  ProductId = -1,
                                  UserTestId = -1,
                                  TestSubGroup = -1,
                                  TestType = TestType.Undefined,
                                  Action = Action.Undefined,
                                  IsSkillsModuleTest = (reader["TestExistsSkillsModule"] as bool?) ?? false,
                                  ProgramofStudyId = (reader["ProgramofStudyId"] as int?) ?? 0,
                                  IsProctorTrackEnabled = (reader["ProctorTrackEnabled"] as int?) ?? 0
                              };
            return student;
        }

        private IDataReader GetStudentInfo(int userId)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[1];
            var parameterUserId = new SqlParameter("@userId", SqlDbType.Int, 4) { Value = userId };
            sqlParameters[0] = parameterUserId;
            #endregion
            return _dataContext.GetDataReader("uspGetUserInfo", sqlParameters);
        }

        private IDataReader LoginUser(string username, string password)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[2];
            var parameterUserName = new SqlParameter("@username", SqlDbType.VarChar, 80) { Value = username };
            sqlParameters[0] = parameterUserName;

            var parameterPassword = new SqlParameter("@password", SqlDbType.VarChar, 50) { Value = password };
            sqlParameters[1] = parameterPassword;
            #endregion
            return _dataContext.GetDataReader("uspLoginUser", sqlParameters);
        }
        #endregion Methods
    }
}