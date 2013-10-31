using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;

namespace NursingLibrary.DAO
{
    public class CMSRepository : ICMSRepository
    {
        #region Fields

        private readonly IDataContext _dataContext;
        private readonly IDataContext _liveAppDataContext;

        #endregion Fields

        #region Constructors

        public CMSRepository(IDataContext dataContext, IDataContext liveAppDataContext)
        {
            _dataContext = dataContext;
            _liveAppDataContext = liveAppDataContext;
        }
        #endregion

        public IEnumerable<QuestionResult> SearchQuestions(QuestionCriteria searchCriteria)
        {
            var questions = new List<QuestionResult>();
            #region SqlParameters
            SqlParameter[] arParams = new SqlParameter[23];
            arParams[0] = new SqlParameter("@ProductID", searchCriteria.Product);
            arParams[1] = new SqlParameter("@TestID", searchCriteria.Test);
            arParams[2] = new SqlParameter("@ClientNeedID", searchCriteria.ClientNeed);
            arParams[3] = new SqlParameter("@ClientNeedsCategoryID", searchCriteria.ClientNeedsCategory);
            arParams[4] = new SqlParameter("@ClinicalConceptID", searchCriteria.ClinicalConcept);
            arParams[5] = new SqlParameter("@CognitiveLevelID", searchCriteria.CognitiveLevel);
            arParams[6] = new SqlParameter("@CriticalThinkingID", searchCriteria.CriticalThinking);
            arParams[7] = new SqlParameter("@DemographicID", searchCriteria.Demographic);
            arParams[8] = new SqlParameter("@LevelOfDifficultyID", searchCriteria.LevelOfDifficulty);
            arParams[9] = new SqlParameter("@NursingProcessID", searchCriteria.NursingProcess);
            arParams[10] = new SqlParameter("@RemediationID", searchCriteria.Remediation);
            arParams[11] = new SqlParameter("@SpecialtyAreaID", searchCriteria.SpecialtyArea);
            arParams[12] = new SqlParameter("@SystemID", searchCriteria.System);
            arParams[13] = new SqlParameter("@QuestionID", searchCriteria.QuestionID);
            arParams[14] = new SqlParameter("@TypeOfFileID", searchCriteria.ItemType);
            arParams[15] = new SqlParameter("@QuestionType", searchCriteria.Qtype);
            arParams[16] = new SqlParameter("@Text", searchCriteria.Text);
            arParams[17] = new SqlParameter("@ReleaseStatus", searchCriteria.ReleaseStatus);
            arParams[18] = new SqlParameter("@Active", searchCriteria.Active);
            arParams[19] = new SqlParameter("@AccreditationCategoriesID", searchCriteria.AccreditationCategories);
            arParams[20] = new SqlParameter("@QSENKSACompetenciesID", searchCriteria.QSENKSACompetencies);
            arParams[21] = new SqlParameter("@ProgramOfStudy", searchCriteria.ProgramOfStudy);
            arParams[22] = new SqlParameter("@ConceptsID",searchCriteria.Concepts);
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetListOfQuestionsShowUnique", arParams))
            {
                while (reader.Read())
                {
                    questions.Add(new QuestionResult()
                    {
                        QID = (reader["QID"] as int?) ?? 0,
                        QN = (reader["QN"] as int?) ?? 0,
                        QuestionID = (reader["QuestionID"] as string) ?? string.Empty,
                        TopicTitle = (reader["TopicTitle"] as string) ?? string.Empty,
                        LevelofDifficulty = (reader["LevelOfDifficulty"] as string) ?? string.Empty,
                        NursingProcess = (reader["NursingProcess"] as string) ?? string.Empty,
                        Demographic = (reader["Demographic"] as string) ?? string.Empty,
                        ClinicalConcept = (reader["ClinicalConcept"] as string) ?? string.Empty,
                        ReleaseStatus = (reader["ReleaseStatus"] as string) ?? string.Empty,
                        Stem = (reader["Stem"] as string) ?? string.Empty
                    });
                }
            }

            return questions.ToList();
        }

        public IEnumerable<Remediation> SearchRemediation(QuestionCriteria searchCriteria)
        {
            var remediations = new List<Remediation>();
            #region SqlParameters
            SqlParameter[] arParams = new SqlParameter[2];
            arParams[0] = new SqlParameter("@RemediationID", searchCriteria.Remediation);
            arParams[1] = new SqlParameter("@Text", searchCriteria.Text);
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetListOfRem", arParams))
            {
                while (reader.Read())
                {
                    remediations.Add(new Remediation()
                    {
                        RemediationId = (reader["RemediationId"] as int?) ?? 0,
                        Explanation = (reader["Explanation"] as string) ?? string.Empty,
                        TopicTitle = (reader["TopicTitle"] as string) ?? string.Empty,
                        ReleaseStatus = (reader["ReleaseStatus"] as string) ?? string.Empty
                    });
                }
            }

            return remediations.ToList();
        }

        public List<Test> GetAVPItems(int testId, int productId, int testSubGroup)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[3];
            List<Test> tests = new List<Test>();

            var parameterProductId = new SqlParameter("@ProductId", SqlDbType.Int, 6) { Value = productId };
            sqlParameters[0] = parameterProductId;

            var parameterTestSubGroup = new SqlParameter("@TestSubGroup", SqlDbType.Int, 6) { Value = testSubGroup };
            sqlParameters[1] = parameterTestSubGroup;

            var parameterTestId = new SqlParameter("@TestID", SqlDbType.Int, 6) { Value = testId };
            sqlParameters[2] = parameterTestId;

            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPActiveTests", sqlParameters))
            {
                while (reader.Read())
                {
                    tests.Add(new Test
                    {
                        TestId = (reader["TestID"] as int?) ?? 0,
                        TestName = (reader["TestName"] as string) ?? string.Empty,
                        URL = (reader["URL"] as string) ?? string.Empty,
                        PopupHeight = (reader["PopHeight"] as int?) ?? 0,
                        PopupWidth = (reader["PopWidth"] as int?) ?? 0,
                        ProgramofStudyId = (reader["ProgramofStudyId"] as int?) ?? 0
                    });
                }
            }

            return tests;
        }

        public IEnumerable<Test> SearchAVPItems(string testName, int programOfStudyId)
        {
            List<Test> tests = new List<Test>();
            #region Sql Parameters
            var sqlParameters = new SqlParameter[2];
            var parameterTestName = new SqlParameter("@TestName", SqlDbType.VarChar, 50) { Value = testName };
            sqlParameters[0] = parameterTestName;
            var posId = new SqlParameter("@ProgramOfStudyId", SqlDbType.Int) { Value = programOfStudyId };
            sqlParameters[1] = posId;

            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("USPSearchAVPItems", sqlParameters))
            {
                while (reader.Read())
                {
                    tests.Add(new Test
                    {
                        TestId = (reader["TestID"] as int?) ?? 0,
                        TestName = (reader["TestName"] as string) ?? string.Empty,
                        URL = (reader["URL"] as string) ?? string.Empty,
                        PopupHeight = (reader["PopHeight"] as int?) ?? 0,
                        PopupWidth = (reader["PopWidth"] as int?) ?? 0,
                    });
                }
            }

            return tests;
        }

        public void SaveAVPItems(Test test)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[6];
            var parameterTestId = new SqlParameter("@TestID", SqlDbType.Int, 6) { Value = test.TestId, Direction = ParameterDirection.InputOutput };
            sqlParameters[0] = parameterTestId;

            var parameterTestName = new SqlParameter("@TestName", SqlDbType.VarChar, 50) { Value = test.TestName };
            sqlParameters[1] = parameterTestName;

            var parameterURL = new SqlParameter("@Url", SqlDbType.NVarChar, 200) { Value = test.URL };
            sqlParameters[2] = parameterURL;

            var parameterPopHeight = new SqlParameter("@PopHeight", SqlDbType.Int, 6) { Value = test.PopupHeight };
            sqlParameters[3] = parameterPopHeight;

            var parameterPopWidth = new SqlParameter("@PopWidth", SqlDbType.Int, 6) { Value = test.PopupWidth };
            sqlParameters[4] = parameterPopWidth;

            var parameterPosId = new SqlParameter("@ProgramOfStudyId", SqlDbType.Int, 6) { Value = test.ProgramofStudyId };
            sqlParameters[5] = parameterPosId;

            #endregion

            _dataContext.ExecuteStoredProcedure("USPSaveAVPItems", sqlParameters);
            test.TestId = (int)sqlParameters[0].Value;
        }

        public void DeleteTestById(int id)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];

            var parameterID = new SqlParameter("@TestID", SqlDbType.Int, 6) { Value = id };
            sqlParameters[0] = parameterID;

            #endregion
            _dataContext.ExecuteNonQuery("USPDeleteTestById", sqlParameters);
        }

        public void SaveNorming(Norming norming)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[6];
            var parametertestId = new SqlParameter("@TestId", SqlDbType.Int, 6) { Value = norming.TestId };
            sqlParameters[0] = parametertestId;

            var parameterNumberOfCorrect = new SqlParameter("@NumberCorrect", SqlDbType.Float, 0) { Value = norming.NumberCorrect };
            sqlParameters[1] = parameterNumberOfCorrect;

            var parameterCorrect = new SqlParameter("@Correct", SqlDbType.Float, 0) { Value = norming.Correct };
            sqlParameters[2] = parameterCorrect;

            var parameterPercentileRank = new SqlParameter("@PercentileRank", SqlDbType.Float, 0) { Value = norming.PercentileRank };
            sqlParameters[3] = parameterPercentileRank;

            var parameterID = new SqlParameter("@Id", SqlDbType.Int, 0) { Value = norming.Id };
            sqlParameters[4] = parameterID;

            var parameterProbability = new SqlParameter("@Probability", SqlDbType.Int, 0) { Value = norming.Probability };
            sqlParameters[5] = parameterProbability;
            #endregion
            _dataContext.ExecuteNonQuery("USPSaveNorming", sqlParameters);
        }

        public void DeleteNormingById(int id)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];

            var parameterID = new SqlParameter("@Id", SqlDbType.Int, 0) { Value = id };
            sqlParameters[0] = parameterID;

            #endregion
            _dataContext.ExecuteNonQuery("USPDeleteNormingById", sqlParameters);
        }

        public IEnumerable<Norming> GetNormings(int testId, string testIds)
        {
            var normings = new List<Norming>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            var parameterTestId = new SqlParameter("@TestId", SqlDbType.Int, 6) { Value = testId };
            sqlParameters[0] = parameterTestId;
            var parameterTestIds = new SqlParameter("@TestIds", SqlDbType.VarChar, 4000) { Value = testIds };
            sqlParameters[1] = parameterTestIds;
            #endregion
            using (IDataReader reader = _dataContext.GetDataReader("USPGetNormings", sqlParameters))
            {
                while (reader.Read())
                {
                    normings.Add(new Norming
                    {
                        Id = (reader["id"] as int?) ?? 0,
                        NumberCorrect = (reader["NumberCorrect"] as double?) ?? 0,
                        Correct = (reader["Correct"] as double?) ?? 0,
                        PercentileRank = (reader["PercentileRank"] as double?) ?? 0,
                        Probability = (reader["Probability"] as double?) ?? 0,
                        TestId = (reader["TestID"] as int?) ?? 0
                    });
                }
            }

            return normings.ToArray();
        }

        public void UpdateTestsReleaseStatusById(int testId, string releaseStatus)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];

            var parameterID = new SqlParameter("@TestId", SqlDbType.Int, 6) { Value = testId };
            sqlParameters[0] = parameterID;

            var parameterStatus = new SqlParameter("@ReleaseStatus", SqlDbType.Char, 1) { Value = releaseStatus };
            sqlParameters[1] = parameterStatus;

            #endregion
            _dataContext.ExecuteNonQuery("USPUpdateTestsReleaseStatusById", sqlParameters);
        }

        public IEnumerable<ClientNeedsCategory> GetClientNeedCategory(int clientNeedId)
        {
            var ClientNeedCategories = new List<ClientNeedsCategory>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ClientNeedId", SqlDbType.Int, 4) { Value = clientNeedId };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetClientNeedsCategories", sqlParameters))
            {
                while (reader.Read())
                {
                    ClientNeedCategories.Add(new ClientNeedsCategory
                    {
                        Id = (reader["ClientNeedCategoryID"] as int?) ?? 0,
                        Name = (reader["ClientNeedCategory"] as String) ?? string.Empty,
                    });
                }
            }

            return ClientNeedCategories.ToArray();
        }

        public IEnumerable<Norm> GetNorms(int testId, string chartType, string testIds)
        {
            var lippinCotts = new List<Norm>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[3];
            var parametertestId = new SqlParameter("@TestId", SqlDbType.Int, 6) { Value = testId };
            sqlParameters[0] = parametertestId;
            var parameterChartType = new SqlParameter("@ChartType", SqlDbType.NVarChar, 50) { Value = chartType };
            sqlParameters[1] = parameterChartType;
            var parameterTestIds = new SqlParameter("@TestIds", SqlDbType.VarChar, 4000) { Value = testIds };
            sqlParameters[2] = parameterTestIds;
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetNorms", sqlParameters))
            {
                while (reader.Read())
                {
                    lippinCotts.Add(new Norm
                    {
                        Id = (reader["ID"] as int?) ?? 0,
                        ChartType = (reader["ChartType"] as String) ?? string.Empty,
                        ChartID = (reader["ChartID"] as int?) ?? 0,
                        NormValue = (reader["Norm"] as Single?) ?? null,
                        TestId = (reader["TestID"] as int?) ?? 0
                    });
                }
            }

            return lippinCotts.ToArray();
        }

        public void SaveNorm(Norm norm)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[5];
            var parameterNormId = new SqlParameter("@NormId", SqlDbType.Int, 6) { Value = norm.Id };
            sqlParameters[0] = parameterNormId;

            var parameterChartType = new SqlParameter("@ChartType", SqlDbType.NVarChar, 50) { Value = norm.ChartType };
            sqlParameters[1] = parameterChartType;

            var parameterChartId = new SqlParameter("@ChartId", SqlDbType.Int, 6) { Value = norm.ChartID };
            sqlParameters[2] = parameterChartId;

            var parameterNorm = new SqlParameter("@Norm", SqlDbType.Real, 0) { Value = norm.NormValue ?? Convert.DBNull };
            sqlParameters[3] = parameterNorm;

            var parameterTestId = new SqlParameter("@TestId", SqlDbType.Int, 6) { Value = norm.TestId };
            sqlParameters[4] = parameterTestId;
            #endregion
            _dataContext.ExecuteNonQuery("USPSaveNorm", sqlParameters);
        }

        public Lippincott GetLippincottRemediationByID(int lippinCottId)
        {
            Lippincott lippinCott = new Lippincott();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            var parameterLippinCottId = new SqlParameter("@LippinCottID", SqlDbType.Int, 6) { Value = lippinCottId };
            sqlParameters[0] = parameterLippinCottId;
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetLippincottRemediationByID", sqlParameters))
            {
                while (reader.Read())
                {
                    lippinCott.LippincottID = (reader["LippincottID"] as int?) ?? 0;
                    lippinCott.RemediationId = (reader["RemediationID"] as int?) ?? 0;
                    lippinCott.LippincottTitle = (reader["LippincottTitle"] as String) ?? string.Empty;
                    lippinCott.LippincottTitle2 = (reader["LippincottTitle2"] as String) ?? string.Empty;
                    lippinCott.LippincottExplanation = (reader["LippincottExplanation"] as String) ?? string.Empty;
                    lippinCott.LippincottExplanation2 = (reader["LippincottExplanation2"] as String) ?? string.Empty;
                    Remediation rem = new Remediation();
                    rem.TopicTitle = (reader["TopicTitle"] as String) ?? string.Empty;
                    lippinCott.Remediation = rem;
                }
            }

            return lippinCott;
        }

        public IEnumerable<Lippincott> SearchLippincotts(string lippinCottTitle)
        {
            var lippinCotts = new List<Lippincott>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            var parameterLippinCottTitle = new SqlParameter("@LippinCottTitle", SqlDbType.VarChar, 800) { Value = lippinCottTitle };
            sqlParameters[0] = parameterLippinCottTitle;
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPSearchLippincotts", sqlParameters))
            {
                while (reader.Read())
                {
                    BindLippinCottRemediationEntity(lippinCotts, reader);
                }
            }

            return lippinCotts.ToArray();
        }

        public void SaveLippinCott(Lippincott lippinCott)
        {
            Lippincott _lippinCott = new Lippincott();
            #region SqlParameters
            var sqlParameters = new SqlParameter[6];
            var parameterLippinCottId = new SqlParameter("@LippincottId", SqlDbType.Int, 6) { Value = lippinCott.LippincottID, Direction = ParameterDirection.InputOutput };
            sqlParameters[0] = parameterLippinCottId;

            var parameterRemediationId = new SqlParameter("@RemediationId", SqlDbType.Int, 6) { Value = lippinCott.RemediationId };
            sqlParameters[1] = parameterRemediationId;

            var parameterLippinCottTitle = new SqlParameter("@LippincottTitle", SqlDbType.VarChar, 800) { Value = lippinCott.LippincottTitle };
            sqlParameters[2] = parameterLippinCottTitle;

            var parameterLippinExp = new SqlParameter("@LippincottExplanation", SqlDbType.NText, 0) { Value = lippinCott.LippincottExplanation };
            sqlParameters[3] = parameterLippinExp;

            var parameterLippinCottTitle2 = new SqlParameter("@LippincottTitle2", SqlDbType.VarChar, 800) { Value = lippinCott.LippincottTitle2 };
            sqlParameters[4] = parameterLippinCottTitle2;

            var parameterLippinCottExp2 = new SqlParameter("@LippincottExplanation2", SqlDbType.NText, 0) { Value = lippinCott.LippincottExplanation2 };
            sqlParameters[5] = parameterLippinCottExp2;
            #endregion

            _dataContext.ExecuteStoredProcedure("USPSaveLippinCott", sqlParameters);
            _lippinCott.LippincottID = (int)sqlParameters[0].Value;
        }

        public IEnumerable<QuestionLippincott> GetQuestionLippincottByIds(int QID, int lippinCottId, string lippincottIds)
        {
            var questionLippinCotts = new List<QuestionLippincott>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[3];
            var parameterQId = new SqlParameter("@QID", SqlDbType.Int, 6) { Value = QID };
            sqlParameters[0] = parameterQId;
            var parameterLippinCottId = new SqlParameter("@LippincottId", SqlDbType.Int, 6) { Value = lippinCottId };
            sqlParameters[1] = parameterLippinCottId;
            var parameterLippinCottIds = new SqlParameter("@LippincottIds", SqlDbType.VarChar, 4000) { Value = lippincottIds };
            sqlParameters[2] = parameterLippinCottIds;
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetQuestionLippincottByIds", sqlParameters))
            {
                while (reader.Read())
                {
                    questionLippinCotts.Add(new QuestionLippincott
                    {
                        QID = (reader["QID"] as int?) ?? 0,
                        LippincottID = (reader["LippincottID"] as int?) ?? 0,
                    });
                }
            }

            return questionLippinCotts.ToArray();
        }

        public IEnumerable<Lippincott> GetLippincottById(int lippinCottId)
        {
            var lippinCotts = new List<Lippincott>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            var parameterLippinCottId = new SqlParameter("@LippincottId", SqlDbType.Int, 6) { Value = lippinCottId };
            sqlParameters[0] = parameterLippinCottId;
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetLippincottById", sqlParameters))
            {
                while (reader.Read())
                {
                    lippinCotts.Add(new Lippincott
                    {
                        LippincottID = (reader["LippincottID"] as int?) ?? 0,
                        RemediationId = (reader["RemediationID"] as int?) ?? 0,
                        LippincottTitle = (reader["LippincottTitle"] as String) ?? string.Empty,
                        LippincottTitle2 = (reader["LippincottTitle2"] as String) ?? string.Empty,
                        LippincottExplanation = (reader["LippincottExplanation"] as String) ?? string.Empty,
                        LippincottExplanation2 = (reader["LippincottExplanation2"] as String) ?? string.Empty,
                    });
                }
            }

            return lippinCotts.ToArray();
        }

        public void InsertQuestionLippinCott(int QId, int lippinCottId)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            var parameterQId = new SqlParameter("@QID", SqlDbType.Int, 6) { Value = QId };
            sqlParameters[0] = parameterQId;
            var parameterLippinCottId = new SqlParameter("@LippincottID", SqlDbType.Int, 6) { Value = lippinCottId };
            sqlParameters[1] = parameterLippinCottId;
            #endregion
            _dataContext.ExecuteNonQuery("USPInsertQuestionLippinCott", sqlParameters);
        }

        public IEnumerable<Lippincott> GetQuestionLippincotts(int lippinCottId)
        {
            var lippinCotts = new List<Lippincott>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            var parameterLippinCottId = new SqlParameter("@LippincottId", SqlDbType.Int, 6) { Value = lippinCottId };
            sqlParameters[0] = parameterLippinCottId;
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetQuestionsAndLippincotts", sqlParameters))
            {
                while (reader.Read())
                {
                    lippinCotts.Add(new Lippincott
                    {
                        LippincottID = (reader["LippincottID"] as int?) ?? 0,
                        Question = new Question
                        {
                            Id = (reader["QID"] as int?) ?? 0,
                            QuestionId = (reader["QuestionID"] as String) ?? string.Empty
                        }
                    });
                }
            }

            return lippinCotts.ToArray();
        }

        public void DeleteLippinCott(int lippinCottId)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            var parameterLippinCottId = new SqlParameter("@LippincottID", SqlDbType.Int, 6) { Value = lippinCottId };
            sqlParameters[0] = parameterLippinCottId;
            #endregion
            _dataContext.ExecuteNonQuery("USPDeleteLippincott", sqlParameters);
        }

        public void DeleteLippinCottQuestion(int lippinCottId, int questionId)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            var parameterLippinCottId = new SqlParameter("@LippincottID", SqlDbType.Int, 6) { Value = lippinCottId };
            sqlParameters[0] = parameterLippinCottId;
            var parameterQId = new SqlParameter("@QID", SqlDbType.Int, 6) { Value = questionId };
            sqlParameters[1] = parameterQId;
            #endregion
            _dataContext.ExecuteNonQuery("USPDeleteQuestionLippinCott", sqlParameters);
        }

        public IEnumerable<Question> GetQuestions(string questionId, int qId, string remediationId, bool forEdit, string releaseStatus)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[5];

            var parameterquestionId = new SqlParameter("@QuestionID", SqlDbType.VarChar, 50) { Value = questionId };
            sqlParameters[0] = parameterquestionId;

            var parameterqId = new SqlParameter("@QID", SqlDbType.Int, 6) { Value = qId };
            sqlParameters[1] = parameterqId;

            var parameterqRemediationId = new SqlParameter("@RemediationId", SqlDbType.VarChar, 5000) { Value = remediationId };
            sqlParameters[2] = parameterqRemediationId;

            var parameterForEdit = new SqlParameter("@ForEdit", SqlDbType.VarChar, 5000) { Value = forEdit };
            sqlParameters[3] = parameterForEdit;

            var parameterReleaseStatus = new SqlParameter("@ReleaseStatus", SqlDbType.Char, 1) { Value = releaseStatus };
            sqlParameters[4] = parameterReleaseStatus;
            #endregion
            var questions = new List<Question>();
            string tempQuestionId = string.Empty;
            using (IDataReader reader = _dataContext.GetDataReader("USPGetQuestions", sqlParameters))
            {
                while (reader.Read())
                {
                    Question question = new Question();
                    question.Id = (reader["QID"] as int?) ?? 0;
                    question.QuestionId = (reader["QuestionID"] as String) ?? string.Empty;
                    tempQuestionId = (reader["RemediationID"] as String) ?? string.Empty;
                    question.RemediationId = tempQuestionId.ToInt();
                    question.TopicTitleId = (reader["TopicTitleID"] as String) ?? string.Empty;
                    question.Explanation = (reader["Explanation"] as String) ?? string.Empty;
                    question.Remediation = (reader["RE"] as String) ?? string.Empty;
                    question.QuestionType = (reader["QuestionType"] as String) ?? string.Empty;
                    question.Stimulus = (reader["Stimulus"] as String) ?? string.Empty;
                    question.Stem = (reader["Stem"] as String) ?? string.Empty;
                    question.ListeningFileUrl = (reader["ListeningFileUrl"] as String) ?? string.Empty;
                    question.Explanation = (reader["Explanation"] as String) ?? string.Empty;
                    question.ProductLineId = (reader["ProductLineID"] as String) ?? string.Empty;
                    question.PointBiserialsId = (reader["PointBiserialsID"] as String) ?? string.Empty;
                    question.Statisctics = (reader["Statisctics"] as String) ?? string.Empty;
                    question.CreatorId = (reader["CreatorID"] as String) ?? string.Empty;
                    question.DateCreated = (reader["DateCreated"] as String) ?? string.Empty;
                    question.EditorId = (reader["EditorID"] as String) ?? string.Empty;
                    question.DateEdited = (reader["DateEdited"] as String) ?? string.Empty;
                    question.EditorId_2 = (reader["EditorID_2"] as String) ?? string.Empty;
                    question.DateEdited_2 = (reader["DateEdited_2"] as String) ?? string.Empty;
                    question.Source_SBD = (reader["Source_SBD"] as String) ?? string.Empty;
                    question.Feedback = (reader["Feedback"] as String) ?? string.Empty;
                    question.WhoOwns = (reader["WhoOwns"] as String) ?? string.Empty;
                    question.ItemTitle = (reader["ItemTitle"] as String) ?? string.Empty;
                    question.TypeOfFileId = (reader["TypeOfFileID"] as String) ?? string.Empty;
                    question.QuestionType = (reader["QuestionType"] as String) ?? string.Empty;
                    question.Active = (int)(reader["Active"] is DBNull ? 1 : reader["Active"]);
                    var _norming = reader["Q_Norming"].ToString();
                    float norming;
                    question.Q_Norming = float.TryParse(_norming, out norming) ? norming : -1;

                    question.ClinicalConceptsId = (reader["ClinicalConceptsID"] as String) ?? string.Empty;
                    question.CriticalThinkingId = (reader["CriticalThinkingID"] as String) ?? string.Empty;
                    question.SystemId = (reader["SystemID"] as String) ?? string.Empty;
                    question.SpecialtyAreaId = (reader["SpecialtyAreaID"] as String) ?? string.Empty;
                    question.CognitiveLevelId = (reader["CognitiveLevelID"] as String) ?? string.Empty;
                    question.DemographicId = (reader["DemographicID"] as String) ?? string.Empty;
                    question.LevelOfDifficultyId = (reader["LevelOfDifficultyID"] as String) ?? string.Empty;
                    question.NursingProcessId = (reader["NursingProcessID"] as String) ?? string.Empty;
                    question.ClientNeedsId = (reader["ClientNeedsID"] as String) ?? string.Empty;
                    question.ClientNeedsCategoryId = (reader["ClientNeedsCategoryID"] as String) ?? string.Empty;
                    question.ExhibitTab1 = (reader["ExhibitTab1"] as String) ?? string.Empty;
                    question.ExhibitTab2 = (reader["ExhibitTab2"] as String) ?? string.Empty;
                    question.ExhibitTab3 = (reader["ExhibitTab3"] as String) ?? string.Empty;
                    question.ListeningFileUrl = (reader["ListeningFileUrl"] as String) ?? string.Empty;
                    question.ExhibitTitle1 = (reader["ExhibitTitle1"] as String) ?? string.Empty;
                    question.ExhibitTitle2 = (reader["ExhibitTitle2"] as String) ?? string.Empty;
                    question.ExhibitTitle3 = (reader["ExhibitTitle3"] as String) ?? string.Empty;
                    question.XMLQID = (reader["XMLQID"] as String) ?? string.Empty;
                    question.IntegratedConceptsId = (reader["IntegratedConceptsID"] as String) ?? string.Empty;
                    question.ReadingCategoryId = (reader["ReadingCategoryID"] as String) ?? string.Empty;
                    question.ReadingId = (reader["ReadingID"] as String) ?? string.Empty;
                    question.WritingCategoryId = (reader["WritingCategoryID"] as String) ?? string.Empty;
                    question.WritingId = (reader["WritingID"] as String) ?? string.Empty;
                    question.MathCategoryId = (reader["MathCategoryID"] as String) ?? string.Empty;
                    question.MathId = (reader["MathID"] as String) ?? string.Empty;
                    question.WhereUsed = (reader["WhereUsed"] as String) ?? string.Empty;
                    question.Deleted = (reader["Deleted"] as int?) ?? 0;
                    question.QuestionNumberString = (reader["QuestionNumber"] as String) ?? string.Empty;
                    question.TestNumber = (reader["TestNumber"] as String) ?? string.Empty;
                    question.ReleaseStatus = (reader["ReleaseStatus"] as String) ?? string.Empty;
                    question.AlternateStem = (reader["AlternateStem"] as String) ?? string.Empty;
                    question.AccreditationCategoriesId = (reader["AccreditationCategoriesID"] as String) ?? string.Empty;
                    question.QSENKSACompetenciesId = (reader["QSENKSACompetenciesID"] as String) ?? string.Empty;
                    question.ConceptsId = (reader["ConceptsID"] as String) ?? string.Empty;
                    question.RemediationObj = new Remediation();
                    question.RemediationObj.TopicTitle = (reader["RemediationTopicTitle"] as String) ?? string.Empty;
                    question.ProgramofStudyId = (reader["ProgramofStudyId"] as int?) ?? 0;
                    question.ProgramofStudyName = (reader["ProgramofStudyName"] as String) ?? string.Empty;
                    question.CreatedOn = (reader["CreatedDate"] as DateTime?) ?? null;
                    question.CreatedBy = (reader["CreatedBy"] as int?) ?? 0;
                    question.UpdatedOn = (reader["UpdatedDate"] as DateTime?) ?? null;
                    question.UpdatedBy = (reader["UpdatedBy"] as int?) ?? 0;
                    question.ReleasedBy = (reader["ReleasedBy"] as int?) ?? 0;
                    questions.Add(question);
                }
            }

            return questions;
        }

        public Question InsertUpdateQuestion(Question question)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[4];

            var parameterID = new SqlParameter("@QId", SqlDbType.Int, 6) { Value = question.Id };
            sqlParameters[0] = parameterID;

            var parameterQuestionId = new SqlParameter("@QuestionID", SqlDbType.VarChar, 50) { Value = question.QuestionId };
            sqlParameters[1] = parameterQuestionId;

            var parameterRemediationId = new SqlParameter("@RemediationId", SqlDbType.Int, 6) { Value = question.RemediationId };
            sqlParameters[2] = parameterRemediationId;

            var parameterTopicTitleId = new SqlParameter("@TopicTitle", SqlDbType.VarChar, 500) { Value = question.TopicTitleId };
            sqlParameters[3] = parameterTopicTitleId;

            #endregion
            Question savedQuestion = new Question();
            using (IDataReader reader = _dataContext.GetDataReader("USPInsertUpdateQuestion", sqlParameters))
            {
                while (reader.Read())
                {
                    savedQuestion.Id = (reader["QID"] as int?) ?? 0;
                    savedQuestion.QuestionId = (reader["QuestionID"] as String) ?? string.Empty;
                    savedQuestion.RemediationId = (reader["RemediationID"] as int?) ?? 0;
                    savedQuestion.TopicTitleId = (reader["TopicTitleID"] as String) ?? string.Empty;
                }
            }

            return question;
        }

        public Lippincott GetLippincottByRemediationID(int remediationId)
        {
            Lippincott lippinCott = new Lippincott();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            var parameterRemId = new SqlParameter("@RemediationId", SqlDbType.Int, 6) { Value = remediationId };
            sqlParameters[0] = parameterRemId;
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetLippincottByRemediationId", sqlParameters))
            {
                while (reader.Read())
                {
                    lippinCott.LippincottID = (reader["LippincottID"] as int?) ?? 0;
                    lippinCott.RemediationId = (reader["RemediationID"] as int?) ?? 0;
                    lippinCott.LippincottTitle = (reader["LippincottTitle"] as String) ?? string.Empty;
                    lippinCott.LippincottTitle2 = (reader["LippincottTitle2"] as String) ?? string.Empty;
                    lippinCott.LippincottExplanation = (reader["LippincottExplanation"] as String) ?? string.Empty;
                    lippinCott.LippincottExplanation2 = (reader["LippincottExplanation2"] as String) ?? string.Empty;
                }
            }

            return lippinCott;
        }

        public IEnumerable<Remediation> GetRemediations(int remediationId, string releaseStatus)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];

            var parameterremediationId = new SqlParameter("@RemediationId", SqlDbType.VarChar, 50) { Value = remediationId };
            sqlParameters[0] = parameterremediationId;
            var parameterReleaseStatus = new SqlParameter("@ReleaseStatus", SqlDbType.Char, 1) { Value = releaseStatus };
            sqlParameters[1] = parameterReleaseStatus;
            #endregion
            var remediations = new List<Remediation>();
            using (IDataReader reader = _dataContext.GetDataReader("USPGetRemediations", sqlParameters))
            {
                while (reader.Read())
                {
                    remediations.Add(new Remediation
                    {
                        RemediationId = (reader["RemediationID"] as int?) ?? 0,
                        Explanation = (reader["Explanation"] as String) ?? string.Empty,
                        TopicTitle = (reader["TopicTitle"] as String) ?? string.Empty,
                        ReleaseStatus = (reader["ReleaseStatus"] as String) ?? string.Empty,
                    });
                }
            }

            return remediations;
        }

        public void SaveRemediation(Remediation remediation)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[4];

            var parameterRemediationId = new SqlParameter("@RemediationId", SqlDbType.Int, 6) { Value = remediation.RemediationId };
            sqlParameters[0] = parameterRemediationId;

            var parameterExplanation = new SqlParameter("@Explanation", SqlDbType.VarChar, 5000) { Value = remediation.Explanation };
            sqlParameters[1] = parameterExplanation;

            var parameterTopicTitle = new SqlParameter("@TopicTitle", SqlDbType.VarChar, 500) { Value = remediation.TopicTitle };
            sqlParameters[2] = parameterTopicTitle;

            var parameterReleaseStatus = new SqlParameter("@ReleaseStatus", SqlDbType.Char, 1) { Value = remediation.ReleaseStatus };
            sqlParameters[3] = parameterReleaseStatus;

            #endregion

            _dataContext.ExecuteStoredProcedure("USPSaveRemediation", sqlParameters);
            remediation.RemediationId = (int)sqlParameters[0].Value;
        }

        public void DeleteRemediation(int remediationId)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            var parameterLippinCottId = new SqlParameter("@RemediationId", SqlDbType.Int, 6) { Value = remediationId };
            sqlParameters[0] = parameterLippinCottId;
            #endregion
            _dataContext.ExecuteNonQuery("USPDeleteRemediation", sqlParameters);
        }

        public List<Lippincott> GetLippincotts(int qId)
        {
            List<Lippincott> lippinCotts = new List<Lippincott>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            var parameterRemId = new SqlParameter("@QId", SqlDbType.Int, 6) { Value = qId };
            sqlParameters[0] = parameterRemId;
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetLippincottAssignedInQuestion", sqlParameters))
            {
                while (reader.Read())
                {
                    lippinCotts.Add(new Lippincott
                    {
                        QId = (reader["QID"] as int?) ?? 0,
                        LippincottID = (reader["LippincottID"] as int?) ?? 0,
                        LippincottTitle = (reader["LippincottTitle"] as String) ?? string.Empty,
                        LippincottTitle2 = (reader["LippincottTitle2"] as String) ?? string.Empty,
                        LippincottExplanation = (reader["LippincottExplanation"] as String) ?? string.Empty,
                        LippincottExplanation2 = (reader["LippincottExplanation2"] as String) ?? string.Empty,
                    });
                }

                return lippinCotts;
            }
        }

        public IEnumerable<Lippincott> GetLippincotts(int lippinCottId, string releaseStatus)
        {
            var lippinCotts = new List<Lippincott>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            var parameterLippinCottId = new SqlParameter("@LippincottId", SqlDbType.Int, 6) { Value = lippinCottId };
            sqlParameters[0] = parameterLippinCottId;
            var parameterReleaseStatus = new SqlParameter("@ReleaseStatus", SqlDbType.Char, 1) { Value = releaseStatus };
            sqlParameters[1] = parameterReleaseStatus;
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPGetLippincotts", sqlParameters))
            {
                while (reader.Read())
                {
                    lippinCotts.Add(new Lippincott
                    {
                        LippincottID = (reader["LippincottID"] as int?) ?? 0,
                        RemediationId = (reader["RemediationID"] as int?) ?? 0,
                        LippincottTitle = (reader["LippincottTitle"] as String) ?? string.Empty,
                        LippincottTitle2 = (reader["LippincottTitle2"] as String) ?? string.Empty,
                        LippincottExplanation = (reader["LippincottExplanation"] as String) ?? string.Empty,
                        LippincottExplanation2 = (reader["LippincottExplanation2"] as String) ?? string.Empty,
                    });
                }
            }

            return lippinCotts.ToArray();
        }

        public IEnumerable<Test> GetCustomTests(int testId, int productId, string testName)
        {
            var tests = new List<Test>();
            var sqlParameters = new SqlParameter[3];
            var parameterTestId = new SqlParameter("@TestId", SqlDbType.Int, 6) { Value = testId };
            sqlParameters[0] = parameterTestId;
            var parameterProductId = new SqlParameter("@ProductId", SqlDbType.Int, 6) { Value = productId };
            sqlParameters[1] = parameterProductId;
            var parameterTestName = new SqlParameter("@TestName", SqlDbType.VarChar, 50) { Value = testName };
            sqlParameters[2] = parameterTestName;

            using (IDataReader reader = _dataContext.GetDataReader("USPGetCustomTests", sqlParameters))
            {
                while (reader.Read())
                {
                    tests.Add(new Test
                    {
                        TestId = (reader["TestID"] as int?) ?? 0,
                        TestName = (reader["Name"] as string) ?? string.Empty,
                        ProductId = (reader["ProductId"] as int?) ?? 0,
                        DefaultGroup = (reader["DefaultGroup"] as string) ?? string.Empty,
                        SecondPerQuestion = (reader["SecondPerQuestion"] as int?) ?? 0,
                        ProgramofStudyName = (reader["ProgramofStudyName"] as string) ?? string.Empty,
                        ProgramofStudyId = (reader["ProgramofStudyId"] as int?) ?? 0,
                    });
                }
            }

            return tests.ToArray();
        }

        public void SaveCustomTest(Test test)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[6];
            var parameterTestId = new SqlParameter("@TestId", SqlDbType.Int, 6) { Value = test.TestId, Direction = ParameterDirection.InputOutput };
            sqlParameters[0] = parameterTestId;

            var parameterTestName = new SqlParameter("@Name", SqlDbType.VarChar, 50) { Value = test.TestName };
            sqlParameters[1] = parameterTestName;

            var parameterProductId = new SqlParameter("@ProductId", SqlDbType.Int, 6) { Value = test.ProductId };
            sqlParameters[2] = parameterProductId;

            var parameterDefaultGroup = new SqlParameter("@DefaultGroup", SqlDbType.Int, 6) { Value = test.GroupId };
            sqlParameters[3] = parameterDefaultGroup;

            var parameterSecondPerQuestion = new SqlParameter("@SecondPerQuestion", SqlDbType.Int, 6) { Value = test.SecondPerQuestion };
            sqlParameters[4] = parameterSecondPerQuestion;

            var parameterProgramofStudy = new SqlParameter("@ProgramofStudyId", SqlDbType.Int, 6) { Value = test.ProgramofStudyId };
            sqlParameters[5] = parameterProgramofStudy;

            #endregion

            _dataContext.ExecuteStoredProcedure("USPSaveCustomTest", sqlParameters);
            test.TestId = (int)sqlParameters[0].Value;

            //// Hack to seperate Custom FR tests VS Tests created through CMS.
            //// Since Custom FR tests created in production environment is not available in Staging environment
            //// we will not be able to generate Test Id that is unique in both environments.
            //// The solution for now (hack) is to avoid creating Custom FR tests in Staging Environment and keep the Test Id less than KTPApp.CFRTestIdOffset value.
            //// In production environment we dont let user create CMS Custom tests at all so that all test Ids above KTPApp.CFRTestIdOffset will always be Custom FR test.
            if (KTPApp.IsProductionApp)
            {
                throw new ApplicationException("Custom Tests are not supposed to be created in this Environment. Please contact Technical Support.");
            }
            else if (test.TestId > KTPApp.CFRTestIdOffset)
            {
                throw new ApplicationException("Custom Test Id has exceeded the Threshold set in this environment. Please contact Technical Support.");
            }
        }

        public IEnumerable<Test> SearchCustomTests(int programOfStudyId, string testName)
        {
            var tests = new List<Test>();
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@ProgramOfStudyId", SqlDbType.Int, 6) { Value = programOfStudyId };
            sqlParameters[1] = new SqlParameter("@TestName", SqlDbType.VarChar, 50) { Value = testName };

            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("USPSearchCustomTests", sqlParameters))
            {
                while (reader.Read())
                {
                    tests.Add(new Test
                    {
                        TestName = (reader["TestName"] as string) ?? string.Empty,
                        TestId = (reader["TestID"] as int?) ?? 0,
                        ProductId = (reader["ProductID"] as int?) ?? 0,
                        GroupId = ((reader["DefaultGroup"] as string) ?? string.Empty).ToInt(),
                    });
                }
            }

            return tests.ToArray();
        }

        public void CopyCustomTest(int originalTestId, int newTestId)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[2];
            var parameterTestId = new SqlParameter("@OriginalTestID", SqlDbType.Int, 6) { Value = originalTestId };
            sqlParameters[0] = parameterTestId;

            var parameterNewTestId = new SqlParameter("@NewTestID", SqlDbType.Int, 6) { Value = newTestId };
            sqlParameters[1] = parameterNewTestId;

            #endregion

            _dataContext.ExecuteStoredProcedure("USPCopyCustomTest", sqlParameters);
        }

        public void DeleteCustomTest(int testId)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[1];
            var parameterTestId = new SqlParameter("@TestId", SqlDbType.Int, 6) { Value = testId };
            sqlParameters[0] = parameterTestId;
            #endregion
            _dataContext.ExecuteStoredProcedure("USPDeleteCustomTest", sqlParameters);
        }

        public IEnumerable<CategoryDetail> GetTestcategoriesForTestQuestions(int testId, int testType)
        {
            var CategoryDetails = new List<CategoryDetail>();
            var sqlParameters = new SqlParameter[2];
            var parameterTestId = new SqlParameter("@TestType", SqlDbType.Int, 6) { Value = testType };
            sqlParameters[0] = parameterTestId;
            var parameterProductId = new SqlParameter("@TestId", SqlDbType.Int, 6) { Value = testId };
            sqlParameters[1] = parameterProductId;

            using (IDataReader reader = _dataContext.GetDataReader("USPGetTestCategoriesForTestQuestion", sqlParameters))
            {
                while (reader.Read())
                {
                    CategoryDetails.Add(new CategoryDetail
                    {
                        Id = (reader["Id"] as int?) ?? 0,
                        Description = (reader["Description"] as string) ?? string.Empty,
                    });
                }
            }

            return CategoryDetails.ToArray();
        }

        public IEnumerable<TestCategory> GetTestCategories(int testId, string testIds)
        {
            var testCategories = new List<TestCategory>();
            var sqlParameters = new SqlParameter[2];
            var parameterTestId = new SqlParameter("@TestId", SqlDbType.Int, 6) { Value = testId };
            sqlParameters[0] = parameterTestId;
            var parameterTestIds = new SqlParameter("@TestIds", SqlDbType.VarChar, 4000) { Value = testIds };
            sqlParameters[1] = parameterTestIds;

            using (IDataReader reader = _dataContext.GetDataReader("USPGetTestCategories", sqlParameters))
            {
                while (reader.Read())
                {
                    testCategories.Add(new TestCategory
                    {
                        Id = (reader["Id"] as int?) ?? 0,
                        TestId = (reader["TestID"] as int?) ?? 0,
                        CategoryId = (reader["CategoryID"] as int?) ?? 0,
                        Student = (reader["Student"] as int?) ?? 0,
                        Admin = (reader["Admin"] as int?) ?? 0,
                    });
                }
            }

            return testCategories.ToArray();
        }

        public void SaveTestCategory(int testId, int categoryId, int student, int admin)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[4];
            var parameterTestId = new SqlParameter("@TestId", SqlDbType.Int, 6) { Value = testId };
            sqlParameters[0] = parameterTestId;

            var parameterCategoryId = new SqlParameter("@CategoryId", SqlDbType.Int, 50) { Value = categoryId };
            sqlParameters[1] = parameterCategoryId;

            var parameterStudent = new SqlParameter("@Student", SqlDbType.Int, 6) { Value = student };
            sqlParameters[2] = parameterStudent;

            var parameterAdmin = new SqlParameter("@Admin", SqlDbType.Int, 6) { Value = admin };
            sqlParameters[3] = parameterAdmin;

            #endregion

            _dataContext.ExecuteStoredProcedure("USPSaveTestCategory", sqlParameters);
        }

        public IEnumerable<TestCategory> GetTestCategories(int testId)
        {
            var testCategories = new List<TestCategory>();
            var sqlParameters = new SqlParameter[1];
            var parameterTestId = new SqlParameter("@TestId", SqlDbType.Int, 6) { Value = testId };
            sqlParameters[0] = parameterTestId;

            using (IDataReader reader = _dataContext.GetDataReader("USPGetTestCategories", sqlParameters))
            {
                while (reader.Read())
                {
                    testCategories.Add(new TestCategory
                    {
                        TestId = (reader["TestID"] as int?) ?? 0,
                        CategoryId = (reader["CategoryID"] as int?) ?? 0,
                        Student = (reader["Student"] as int?) ?? 0,
                        Admin = (reader["Admin"] as int?) ?? 0,
                    });
                }
            }

            return testCategories.ToArray();
        }

        public IEnumerable<AnswerChoice> GetAnswers(int qId, int actionType, string Qids)
        {
            var answerChoice = new List<AnswerChoice>();
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@QuestionId", SqlDbType.Int, 6) { Value = qId };
            sqlParameters[1] = new SqlParameter("@ActionType", SqlDbType.Int, 6) { Value = actionType };
            sqlParameters[2] = new SqlParameter("@QIds", SqlDbType.VarChar, 4000) { Value = Qids };

            using (IDataReader reader = _dataContext.GetDataReader("USPGetAnswerChoices", sqlParameters))
            {
                while (reader.Read())
                {
                    answerChoice.Add(new AnswerChoice
                    {
                        Id = (reader["QID"] as int?) ?? 0,
                        AnswerId = (reader["AnswerID"] as int?) ?? 0,
                        Aindex = (reader["AIndex"] as String) ?? string.Empty,
                        Atext = (reader["AText"] as String) ?? string.Empty,
                        Correct = (reader["Correct"] as int?) ?? 0,
                        AnswerConnectId = (reader["AnswerConnectID"] as int?) ?? 0,
                        ActionType = (reader["AType"] as int?) ?? 0,
                        InitialPosition = (reader["initialPos"] as int?) ?? 0,
                        Unit = (reader["Unit"] as String) ?? string.Empty,
                        AlternateAText = (reader["AlternateAText"] as String) ?? string.Empty,
                    });
                }

                return answerChoice;
            }
        }

        public void SaveAnswer(AnswerChoice answer)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[10];
            sqlParameters[0] = new SqlParameter("@QID", SqlDbType.Int, 4) { Value = answer.Question.Id };
            sqlParameters[1] = new SqlParameter("@AIndex", SqlDbType.Char, 1) { Value = answer.Aindex };
            sqlParameters[2] = new SqlParameter("@AText", SqlDbType.VarChar, 3000) { Value = answer.Atext };
            sqlParameters[3] = new SqlParameter("@Correct", SqlDbType.Int, 4) { Value = answer.Correct };
            sqlParameters[4] = new SqlParameter("@AnswerConnectId", SqlDbType.Int, 4) { Value = answer.AnswerConnectId };
            sqlParameters[5] = new SqlParameter("@ActionType", SqlDbType.Int, 4) { Value = answer.ActionType };
            sqlParameters[6] = new SqlParameter("@InitialPosition", SqlDbType.Int, 4) { Value = answer.InitialPosition };
            sqlParameters[7] = new SqlParameter("@Unit", SqlDbType.VarChar, 50) { Value = answer.Unit };
            sqlParameters[8] = new SqlParameter("@AlternateAText", SqlDbType.VarChar, 3000) { Value = answer.AlternateAText };
            sqlParameters[9] = new SqlParameter("@AnswerId", SqlDbType.Int, 4) { Value = answer.AnswerId };
            #endregion
            _dataContext.ExecuteStoredProcedure("USPSaveAnswer", sqlParameters);
        }

        public void DeleteQuestion(int questionId)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@QuestionId", SqlDbType.Int, 4) { Value = questionId };
            #endregion
            _dataContext.ExecuteStoredProcedure("USPDeleteQuestion", sqlParameters);
        }

        public int SaveQuestion(Question question, int adminId)
        {
            var sqlParameters = new SqlParameter[54];
            sqlParameters[0] = new SqlParameter("@QID", SqlDbType.Int, 4) { Value = question.Id };
            sqlParameters[1] = new SqlParameter("@QuestionId", SqlDbType.VarChar, 50) { Value = question.QuestionId };
            sqlParameters[2] = new SqlParameter("@QuestionType", SqlDbType.Char, 2) { Value = question.QuestionType };
            sqlParameters[3] = new SqlParameter("@ClientNeedsId", SqlDbType.VarChar, 500) { Value = question.ClientNeedsId };
            sqlParameters[4] = new SqlParameter("@ClientNeedsCategoryId", SqlDbType.VarChar, 500) { Value = question.ClientNeedsCategoryId };
            sqlParameters[5] = new SqlParameter("@NursingProcessId", SqlDbType.VarChar, 500) { Value = question.NursingProcessId };
            sqlParameters[6] = new SqlParameter("@LevelOfDifficultyId", SqlDbType.VarChar, 500) { Value = question.LevelOfDifficultyId };
            sqlParameters[7] = new SqlParameter("@DemographicId", SqlDbType.VarChar, 500) { Value = question.DemographicId };
            sqlParameters[8] = new SqlParameter("@CognitiveLevelId", SqlDbType.VarChar, 500) { Value = question.CognitiveLevelId };
            sqlParameters[9] = new SqlParameter("@CriticalThinkingId", SqlDbType.VarChar, 500) { Value = question.CriticalThinkingId };
            sqlParameters[10] = new SqlParameter("@IntegratedConceptsId", SqlDbType.VarChar, 500) { Value = String.IsNullOrEmpty(question.IntegratedConceptsId) ? string.Empty : question.IntegratedConceptsId };
            sqlParameters[11] = new SqlParameter("@ClinicalConceptsId", SqlDbType.VarChar, 500) { Value = String.IsNullOrEmpty(question.ClinicalConceptsId) ? string.Empty : question.ClinicalConceptsId };
            sqlParameters[12] = new SqlParameter("@Stimulus", SqlDbType.VarChar, 500) { Value = question.Stimulus };
            sqlParameters[13] = new SqlParameter("@Stem", SqlDbType.VarChar, 5000) { Value = question.Stem };
            sqlParameters[14] = new SqlParameter("@Explanation", SqlDbType.VarChar, 5000) { Value = question.Explanation };
            sqlParameters[15] = new SqlParameter("@RemediationId", SqlDbType.VarChar, 5000) { Value = question.RemediationId };
            sqlParameters[16] = new SqlParameter("@SpecialtyAreaId", SqlDbType.VarChar, 500) { Value = question.SpecialtyAreaId };
            sqlParameters[17] = new SqlParameter("@SystemId", SqlDbType.VarChar, 500) { Value = question.SystemId };
            sqlParameters[18] = new SqlParameter("@ReadingCategoryId", SqlDbType.VarChar, 500) { Value = question.ReadingCategoryId };
            sqlParameters[19] = new SqlParameter("@ReadingId", SqlDbType.VarChar, 500) { Value = question.ReadingId };
            sqlParameters[20] = new SqlParameter("@WritingCategoryId", SqlDbType.VarChar, 500) { Value = question.WritingCategoryId };
            sqlParameters[21] = new SqlParameter("@WritingId", SqlDbType.VarChar, 500) { Value = question.WritingId };
            sqlParameters[22] = new SqlParameter("@MathCategoryId", SqlDbType.VarChar, 500) { Value = question.MathCategoryId };
            sqlParameters[23] = new SqlParameter("@MathId", SqlDbType.VarChar, 500) { Value = question.MathId };
            sqlParameters[24] = new SqlParameter("@ProductLineId", SqlDbType.VarChar, 500) { Value = question.ProductLineId };
            sqlParameters[25] = new SqlParameter("@TypeOfFileId", SqlDbType.VarChar, 500) { Value = question.TypeOfFileId };
            sqlParameters[26] = new SqlParameter("@Statisctics", SqlDbType.VarChar, 500) { Value = question.Statisctics };
            sqlParameters[27] = new SqlParameter("@CreatorId", SqlDbType.VarChar, 500) { Value = question.CreatorId };
            sqlParameters[28] = new SqlParameter("@DateCreated", SqlDbType.VarChar, 50) { Value = question.DateCreated };
            sqlParameters[29] = new SqlParameter("@EditorId", SqlDbType.VarChar, 500) { Value = question.EditorId };
            sqlParameters[30] = new SqlParameter("@DateEdited", SqlDbType.VarChar, 500) { Value = question.DateEdited };
            sqlParameters[31] = new SqlParameter("@EditorId_2", SqlDbType.VarChar, 500) { Value = question.EditorId_2 };
            sqlParameters[32] = new SqlParameter("@DateEdited_2", SqlDbType.VarChar, 500) { Value = question.DateEdited_2 };
            sqlParameters[33] = new SqlParameter("@Source_SBD", SqlDbType.VarChar, 500) { Value = question.Source_SBD };
            sqlParameters[34] = new SqlParameter("@WhoOwns", SqlDbType.VarChar, 500) { Value = question.WhoOwns };
            sqlParameters[35] = new SqlParameter("@Feedback", SqlDbType.VarChar, 500) { Value = question.Feedback };
            sqlParameters[36] = new SqlParameter("@Active", SqlDbType.Int, 4) { Value = question.Active };
            sqlParameters[37] = new SqlParameter("@PointBiserialsId", SqlDbType.VarChar, 500) { Value = question.PointBiserialsId };
            sqlParameters[38] = new SqlParameter("@ItemTitle", SqlDbType.VarChar, 500) { Value = question.ItemTitle };
            sqlParameters[39] = new SqlParameter("@ExhibitTab1", SqlDbType.VarChar, 5000) { Value = question.ExhibitTab1 };
            sqlParameters[40] = new SqlParameter("@ExhibitTab2", SqlDbType.VarChar, 5000) { Value = question.ExhibitTab2 };
            sqlParameters[41] = new SqlParameter("@ExhibitTab3", SqlDbType.VarChar, 5000) { Value = question.ExhibitTab3 };
            sqlParameters[42] = new SqlParameter("@Norming", SqlDbType.Float, 53) { Value = question.Q_Norming };
            sqlParameters[43] = new SqlParameter("@ExhibitTitle1", SqlDbType.VarChar, 1000) { Value = question.ExhibitTitle1 };
            sqlParameters[44] = new SqlParameter("@ExhibitTitle2", SqlDbType.VarChar, 1000) { Value = question.ExhibitTitle2 };
            sqlParameters[45] = new SqlParameter("@ExhibitTitle3", SqlDbType.VarChar, 1000) { Value = question.ExhibitTitle3 };
            sqlParameters[46] = new SqlParameter("@ListeningFileUrl", SqlDbType.VarChar, 1000) { Value = question.ListeningFileUrl };
            sqlParameters[47] = new SqlParameter("@AlternateStem", SqlDbType.VarChar, 5000) { Value = question.AlternateStem };
            sqlParameters[48] = new SqlParameter("@NewQuestionId", SqlDbType.Int, 4);
            sqlParameters[48].Direction = ParameterDirection.Output;
            sqlParameters[49] = new SqlParameter("@AccreditationCategoriesId", SqlDbType.VarChar, 500) { Value = question.AccreditationCategoriesId };
            sqlParameters[50] = new SqlParameter("@QSENKSACompetenciesId", SqlDbType.VarChar, 500) { Value = question.QSENKSACompetenciesId };
            sqlParameters[51] = new SqlParameter("@ProgramofStudyId", SqlDbType.Int, 6) { Value = question.ProgramofStudyId };
            sqlParameters[52] = new SqlParameter("@AdminId", SqlDbType.Int, 4) { Value = adminId };
            sqlParameters[53] = new SqlParameter("@ConceptsId", SqlDbType.VarChar, 500) { Value = question.ConceptsId };
            _dataContext.ExecuteNonQuery("USPSaveQuestion", sqlParameters);
            var _newQuestionId = (int)sqlParameters[48].Value;
            return _newQuestionId;
        }

        public bool IsQuestionIdExist(string questionId)
        {
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@QuestionId", SqlDbType.VarChar, 50) { Value = questionId.Trim() };
            sqlParameters[1] = new SqlParameter("@IsExit", SqlDbType.Bit, 1);
            sqlParameters[1].Direction = ParameterDirection.Output;
            _dataContext.ExecuteNonQuery("uspIsQuestionidExist", sqlParameters);
            return (bool)sqlParameters[1].Value;
        }

        public void AssignQuestion(Test test)
        {
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@TestId", SqlDbType.Int, 4) { Value = test.TestId };
            sqlParameters[1] = new SqlParameter("@QuestionId", SqlDbType.Int, 4) { Value = test.Question.Id };
            sqlParameters[2] = new SqlParameter("@QuestionNumber", SqlDbType.Int, 4) { Value = test.Question.QuestionNumber };
            sqlParameters[3] = new SqlParameter("@Active", SqlDbType.Int, 4) { Value = test.Question.Active };
            _dataContext.ExecuteNonQuery("USPAssignQuestion", sqlParameters);
        }

        public IEnumerable<Test> GetTestsForQuestion(int questionId)
        {
            var tests = new List<Test>();
            var sqlParameters = new SqlParameter[1];
            var parameterTestId = new SqlParameter("@QuestionId", SqlDbType.Int) { Value = questionId };
            sqlParameters[0] = parameterTestId;

            using (IDataReader reader = _dataContext.GetDataReader("USPGetTestsForQuestion", sqlParameters))
            {
                while (reader.Read())
                {
                    tests.Add(new Test
                    {
                        TestName = (reader["TestName"] as string) ?? string.Empty,
                        Product = new Product() { ProductName = (reader["ProductName"] as string) ?? string.Empty },
                        Question = new Question() { QuestionId = (reader["QuestionID"] as string) ?? string.Empty, Id = (reader["QID"] as int?) ?? 0 }
                    });
                }
            }

            return tests.ToArray();
        }

        public IEnumerable<Question> GetNextQuestion(int userTestId, int questionNumber, string typeOfFileId)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[3];

            var parameterquestionId = new SqlParameter("@UserTestId", SqlDbType.Int, 6) { Value = userTestId };
            sqlParameters[0] = parameterquestionId;

            var parameterqId = new SqlParameter("@QuestionNumber", SqlDbType.Int, 6) { Value = questionNumber };
            sqlParameters[1] = parameterqId;

            var parameterqRemediationId = new SqlParameter("@TypeOfFileId", SqlDbType.VarChar, 500) { Value = typeOfFileId };
            sqlParameters[2] = parameterqRemediationId;

            #endregion
            var questions = new List<Question>();
            string tempQuestionId = string.Empty;
            using (IDataReader reader = _dataContext.GetDataReader("USPReturnNextQuestion", sqlParameters))
            {
                while (reader.Read())
                {
                    Question question = new Question();
                    question.Product = new Product() { ProductName = (reader["ProductName"] as String) ?? string.Empty };
                    question.Id = (reader["QID"] as int?) ?? 0;
                    question.QuestionNumber = (reader["QuestionNumber"] as int?) ?? 0;
                    question.Test = new Test()
                    {
                        TestName = (reader["TestName"] as String) ?? string.Empty,
                        TestId = (reader["TestID"] as int?) ?? 0
                    };

                    questions.Add(question);
                }
            }

            return questions;
        }

        public IEnumerable<Question> GetPreviousQuestion(int userTestId, int questionNumber, string typeOfFileId)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[3];

            var parameterquestionId = new SqlParameter("@UserTestId", SqlDbType.Int, 6) { Value = userTestId };
            sqlParameters[0] = parameterquestionId;

            var parameterqId = new SqlParameter("@QuestionNumber", SqlDbType.Int, 6) { Value = questionNumber };
            sqlParameters[1] = parameterqId;

            var parameterqRemediationId = new SqlParameter("@TypeOfFileId", SqlDbType.VarChar, 500) { Value = typeOfFileId };
            sqlParameters[2] = parameterqRemediationId;

            #endregion
            var questions = new List<Question>();
            string tempQuestionId = string.Empty;
            using (IDataReader reader = _dataContext.GetDataReader("USPReturnPreviousQuestion", sqlParameters))
            {
                while (reader.Read())
                {
                    Question question = new Question();
                    question.Product = new Product() { ProductName = (reader["ProductName"] as String) ?? string.Empty };
                    question.Id = (reader["QID"] as int?) ?? 0;
                    question.QuestionNumber = (reader["QuestionNumber"] as int?) ?? 0;
                    question.Test = new Test()
                    {
                        TestName = (reader["TestName"] as String) ?? string.Empty,
                        TestId = (reader["TestID"] as int?) ?? 0
                    };

                    questions.Add(question);
                }
            }

            return questions;
        }

        #region Search Questions Methods
        public IEnumerable<Topic> GetTitles()
        {
            var topics = new List<Topic>();
            using (IDataReader reader = _dataContext.GetDataReader("USPReturnTitles"))
            {
                while (reader.Read())
                {
                    topics.Add(new Topic()
                    {
                        RemediationId = (reader["RemediationId"] as int?) ?? 0,
                        TopicTitle = (reader["TopicTitle"] as string) ?? string.Empty,
                    });
                }
            }

            return topics;
        }

        public IEnumerable<Question> GetReleaseQuestions()
        {
            var questions = new List<Question>();
            var sqlParameters = new SqlParameter[0];

            using (IDataReader reader = _dataContext.GetDataReader("USPGetReleaseQuestions", sqlParameters))
            {
                while (reader.Read())
                {
                    questions.Add(new Question
                    {
                        Id = (reader["QID"] as int?) ?? 0,
                        QuestionId = (reader["QuestionID"] as string) ?? string.Empty,
                        ReleaseStatus = (reader["ReleaseStatus"] as string) ?? string.Empty,
                        TopicTitleId = (reader["TopicTitle"] as string) ?? string.Empty,
                        ClientNeedsId = (reader["ClientNeeds"] as string) ?? string.Empty,
                        ClientNeedsCategoryId = (reader["ClientNeedCategory"] as string) ?? string.Empty,
                        SystemId = (reader["System"] as string) ?? string.Empty,
                        NursingProcessId = (reader["NursingProcess"] as string) ?? string.Empty,
                        ProgramofStudyName = (reader["ProgramOfStudyName"] as string) ?? string.Empty
                    });
                }
            }

            return questions;
        }

        public IEnumerable<Lippincott> GetReleaseLippinCots()
        {
            var lippinCotts = new List<Lippincott>();
            var sqlParameters = new SqlParameter[0];

            using (IDataReader reader = _dataContext.GetDataReader("USPGetReleaseLippinCots", sqlParameters))
            {
                while (reader.Read())
                {
                    Lippincott lippincott = new Lippincott();
                    lippincott.LippincottID = (reader["LippincottID"] as int?) ?? 0;
                    lippincott.RemediationId = (reader["RemediationID"] as int?) ?? 0;
                    lippincott.LippincottTitle = (reader["LippincottTitle"] as String) ?? string.Empty;
                    lippincott.ReleaseStatus = (reader["ReleaseStatus"] as String) ?? string.Empty;
                    Remediation rem = new Remediation();
                    rem.TopicTitle = (reader["TopicTitle"] as String) ?? string.Empty;
                    lippincott.Remediation = rem;
                    lippinCotts.Add(lippincott);
                }
            }

            return lippinCotts;
        }

        public IEnumerable<Test> GetReleaseTests(string status)
        {
            var tests = new List<Test>();
            var sqlParameters = new SqlParameter[1];
            var parameterReleaseStatus = new SqlParameter("@ReleaseStatus", SqlDbType.Char, 1) { Value = status };
            sqlParameters[0] = parameterReleaseStatus;

            using (IDataReader reader = _dataContext.GetDataReader("USPGetReleaseTests", sqlParameters))
            {
                while (reader.Read())
                {
                    tests.Add(new Test
                    {
                        TestId = (reader["TestID"] as int?) ?? 0,
                        ProductId = (reader["ProductID"] as int?) ?? 0,
                        TestName = (reader["TestName"] as string) ?? string.Empty,
                        TestNumber = (reader["TestNumber"] as int?) ?? 0,
                        ActvationTime = (reader["ActivationTime"] as DateTime?) ?? DateTime.MinValue,
                        TimeActivated = (reader["TimeActivated"] as int?) ?? 0,
                        SecureTestS = (reader["SecureTest_S"] as int?) ?? 0,
                        SecureTestD = (reader["SecureTest_D"] as int?) ?? 0,
                        ScramblingS = (reader["Scrambling_S"] as int?) ?? 0,
                        ScramblingD = (reader["Scrambling_D"] as int?) ?? 0,
                        RemediationS = (reader["Remediation_S"] as int?) ?? 0,
                        RemediationD = (reader["Remediation_D"] as int?) ?? 0,
                        ExplanationD = (reader["Explanation_D"] as int?) ?? 0,
                        ExplanationS = (reader["Explanation_S"] as int?) ?? 0,
                        LevelOfDifficultyD = (reader["LevelOfDifficulty_D"] as int?) ?? 0,
                        LevelOfDifficultyS = (reader["LevelOfDifficulty_S"] as int?) ?? 0,
                        NursingProcessS = (reader["NursingProcess_S"] as int?) ?? 0,
                        NursingProcessD = (reader["NursingProcess_D"] as int?) ?? 0,
                        ClinicalConceptsS = (reader["ClinicalConcepts_S"] as int?) ?? 0,
                        ClinicalConceptsD = (reader["ClinicalConcepts_D"] as int?) ?? 0,
                        DemographicsS = (reader["Demographics_S"] as int?) ?? 0,
                        DemographicsD = (reader["Demographics_D"] as int?) ?? 0,
                        ClientNeedsS = (reader["ClientNeeds_S"] as int?) ?? 0,
                        ClientNeedsD = (reader["ClientNeeds_D"] as int?) ?? 0,
                        BloomsS = (reader["Blooms_S"] as int?) ?? 0,
                        BloomsD = (reader["Blooms_D"] as int?) ?? 0,
                        TopicS = (reader["Topic_S"] as int?) ?? 0,
                        TopicD = (reader["Topic_D"] as int?) ?? 0,
                        SpecialtyAreaS = (reader["SpecialtyArea_S"] as int?) ?? 0,
                        SpecialtyAreaD = (reader["SpecialtyArea_D"] as int?) ?? 0,
                        SystemS = (reader["System_S"] as int?) ?? 0,
                        SystemD = (reader["System_D"] as int?) ?? 0,
                        CriticalThinkingS = (reader["CriticalThinking_S"] as int?) ?? 0,
                        CriticalThinkingD = (reader["CriticalThinking_D"] as int?) ?? 0,
                        ReadingS = (reader["Reading_S"] as int?) ?? 0,
                        ReadingD = (reader["Reading_D"] as int?) ?? 0,
                        MathS = (reader["Math_S"] as int?) ?? 0,
                        MathD = (reader["Math_D"] as int?) ?? 0,
                        WritingS = (reader["Writing_S"] as int?) ?? 0,
                        WritingD = (reader["Writing_D"] as int?) ?? 0,
                        RemedationTimeS = (reader["RemedationTime_S"] as int?) ?? 0,
                        RemedationTimeD = (reader["RemedationTime_D"] as int?) ?? 0,
                        ExplanationTimeS = (reader["ExplanationTime_S"] as int?) ?? 0,
                        ExplanationTimeD = (reader["ExplanationTime_D"] as int?) ?? 0,
                        TimeStampS = (reader["TimeStamp_S"] as int?) ?? 0,
                        TimeStampD = (reader["TimeStamp_D"] as int?) ?? 0,
                        ActiveTest = (reader["ActiveTest"] as int?) ?? 0,
                        TestSubGroup = (reader["TestSubGroup"] as int?) ?? 0,
                        URL = (reader["Url"] as string) ?? string.Empty,
                        DefaultGroup = (reader["DefaultGroup"] as string) ?? string.Empty,
                        ReleaseStatus = (reader["ReleaseStatus"] as string) ?? string.Empty,
                        PopupHeight = (reader["PopHeight"] as int?) ?? 0,
                        PopupWidth = (reader["PopWidth"] as int?) ?? 0,
                        SecondPerQuestion = (reader["SecondPerQuestion"] as int?) ?? 0,
                        ProgramofStudyName = (reader["ProgramOfStudyName"] as String) ?? string.Empty,
                        ProgramofStudyId = (reader["ProgramofStudyId"] as int?) ?? 0
                    });
                }
            }

            return tests;
        }

        public void UpdateReleaseStatus(string ids, string releaseStatus, string releaseChoice)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[3];
            var parameterId = new SqlParameter("@Ids", SqlDbType.NVarChar, 2000) { Value = ids };
            sqlParameters[0] = parameterId;

            var parameterReleaseStatus = new SqlParameter("@ReleaseStatus", SqlDbType.Char, 1) { Value = releaseStatus ?? Convert.DBNull };
            sqlParameters[1] = parameterReleaseStatus;

            var parameterReleaseChoice = new SqlParameter("@ReleaseChoice", SqlDbType.NVarChar, 50) { Value = releaseChoice };
            sqlParameters[2] = parameterReleaseChoice;

            #endregion

            _dataContext.ExecuteStoredProcedure("USPUpdateReleaseStatus", sqlParameters);
        }

        public IEnumerable<TestQuestion> GetTestQuestions(int testId, string testIds)
        {
            return GetTestQuestions(_dataContext, testId, testIds);
        }

        public IEnumerable<TestQuestion> GetProdTestQuestions(int testId, string testIds)
        {
            return GetTestQuestions(_liveAppDataContext, testId, testIds);
        }

        public void ReleaseQuestions(Question questions, int userId)
        {
            var sqlParameters = new SqlParameter[63];
            var parameterSourceNumber = new SqlParameter("@SourceNumber", SqlDbType.Int, 6) { Value = questions.SourceNumber };
            sqlParameters[0] = parameterSourceNumber;
            var parameterQID = new SqlParameter("@QID", SqlDbType.Int, 6) { Value = questions.Id };
            sqlParameters[1] = parameterQID;
            var parameterXMLQID = new SqlParameter("@XMLQID", SqlDbType.VarChar, 50) { Value = questions.XMLQID };
            sqlParameters[2] = parameterXMLQID;
            var parameterQuestionId = new SqlParameter("@QuestionId", SqlDbType.VarChar, 50) { Value = questions.QuestionId };
            sqlParameters[3] = parameterQuestionId;
            var parameterQuestionType = new SqlParameter("@QuestionType", SqlDbType.Char, 2) { Value = questions.QuestionType };
            sqlParameters[4] = parameterQuestionType;
            var parameterClientNeeds = new SqlParameter("@ClientNeedsId", SqlDbType.VarChar, 500) { Value = questions.ClientNeedsId };
            sqlParameters[5] = parameterClientNeeds;
            var parameterClientNeedsCategory = new SqlParameter("@ClientNeedsCategoryId", SqlDbType.VarChar, 500) { Value = questions.ClientNeedsCategoryId };
            sqlParameters[6] = parameterClientNeedsCategory;
            var parameterNursingProcess = new SqlParameter("@NursingProcessId", SqlDbType.VarChar, 500) { Value = questions.NursingProcessId };
            sqlParameters[7] = parameterNursingProcess;
            var parameteLevelOfDifficulty = new SqlParameter("@LevelOfDifficultyId", SqlDbType.VarChar, 500) { Value = questions.LevelOfDifficultyId };
            sqlParameters[8] = parameteLevelOfDifficulty;
            var parameteDemographicId = new SqlParameter("@DemographicId", SqlDbType.VarChar, 500) { Value = questions.DemographicId };
            sqlParameters[9] = parameteDemographicId;
            var parameteCognitiveLevelId = new SqlParameter("@CognitiveLevelId", SqlDbType.VarChar, 500) { Value = questions.CognitiveLevelId };
            sqlParameters[10] = parameteCognitiveLevelId;
            var parameteCriticalThinkingId = new SqlParameter("@CriticalThinkingId", SqlDbType.VarChar, 500) { Value = questions.CriticalThinkingId };
            sqlParameters[11] = parameteCriticalThinkingId;
            var parameteIntegratedConceptsId = new SqlParameter("@IntegratedConceptsId", SqlDbType.VarChar, 500) { Value = questions.IntegratedConceptsId };
            sqlParameters[12] = parameteIntegratedConceptsId;
            var parameteClinicalConceptsId = new SqlParameter("@ClinicalConceptsId", SqlDbType.VarChar, 500) { Value = questions.ClinicalConceptsId };
            sqlParameters[13] = parameteClinicalConceptsId;
            var parameteStimulus = new SqlParameter("@Stimulus", SqlDbType.VarChar, 500) { Value = questions.Stimulus };
            sqlParameters[14] = parameteStimulus;
            var parameteStem = new SqlParameter("@Stem", SqlDbType.VarChar, 5000) { Value = questions.Stem };
            sqlParameters[15] = parameteStem;
            var parameteExplanation = new SqlParameter("@Explanation", SqlDbType.VarChar, 5000) { Value = questions.Explanation };
            sqlParameters[16] = parameteExplanation;
            var parameteRemediation = new SqlParameter("@Remediation", SqlDbType.VarChar, 5000) { Value = questions.Remediation };
            sqlParameters[17] = parameteRemediation;
            var parameteRemediationId = new SqlParameter("@RemediationId", SqlDbType.VarChar, 5000) { Value = questions.RemediationId };
            sqlParameters[18] = parameteRemediationId;
            var parameteTopicTitleId = new SqlParameter("@TopicTitleId", SqlDbType.VarChar, 500) { Value = questions.TopicTitleId };
            sqlParameters[19] = parameteTopicTitleId;
            var parameteSpecialtyAreaId = new SqlParameter("@SpecialtyAreaId", SqlDbType.VarChar, 500) { Value = questions.SpecialtyAreaId };
            sqlParameters[20] = parameteSpecialtyAreaId;
            var parameteSystemId = new SqlParameter("@SystemId", SqlDbType.VarChar, 500) { Value = questions.SystemId };
            sqlParameters[21] = parameteSystemId;
            var parameteReadingCategoryId = new SqlParameter("@ReadingCategoryId ", SqlDbType.VarChar, 500) { Value = questions.ReadingCategoryId };
            sqlParameters[22] = parameteReadingCategoryId;
            var parameteReadingId = new SqlParameter("@ReadingId", SqlDbType.VarChar, 500) { Value = questions.ReadingId };
            sqlParameters[23] = parameteReadingId;
            var parameteWritingCategoryId = new SqlParameter("@WritingCategoryId", SqlDbType.VarChar, 500) { Value = questions.WritingCategoryId };
            sqlParameters[24] = parameteWritingCategoryId;
            var parameteWritingId = new SqlParameter("@WritingId", SqlDbType.VarChar, 500) { Value = questions.WritingId };
            sqlParameters[25] = parameteWritingId;
            var parameteMathCategoryId = new SqlParameter("@MathCategoryId", SqlDbType.VarChar, 500) { Value = questions.MathCategoryId };
            sqlParameters[26] = parameteMathCategoryId;
            var parameteMathId = new SqlParameter("@MathId", SqlDbType.VarChar, 500) { Value = questions.MathId };
            sqlParameters[27] = parameteMathId;
            var parameteProductLineId = new SqlParameter("@ProductLineId", SqlDbType.VarChar, 500) { Value = questions.ProductLineId };
            sqlParameters[28] = parameteProductLineId;
            var parameteTypeOfFileId = new SqlParameter("@TypeOfFileId", SqlDbType.VarChar, 500) { Value = questions.TypeOfFileId };
            sqlParameters[29] = parameteTypeOfFileId;
            var parameteItemTitle = new SqlParameter("@ItemTitle", SqlDbType.VarChar, 500) { Value = questions.ItemTitle };
            sqlParameters[30] = parameteItemTitle;
            var parameteStatisctics = new SqlParameter("@Statisctics", SqlDbType.VarChar, 500) { Value = questions.Statisctics };
            sqlParameters[31] = parameteStatisctics;
            var parameteCreatorId = new SqlParameter("@CreatorId", SqlDbType.VarChar, 500) { Value = questions.CreatorId };
            sqlParameters[32] = parameteCreatorId;
            var parameteDateCreated = new SqlParameter("@DateCreated", SqlDbType.VarChar, 50) { Value = questions.DateCreated };
            sqlParameters[33] = parameteDateCreated;
            var parameteEditorId = new SqlParameter("@EditorId", SqlDbType.VarChar, 500) { Value = questions.EditorId };
            sqlParameters[34] = parameteEditorId;
            var parameteDateEdited = new SqlParameter("@DateEdited", SqlDbType.VarChar, 500) { Value = questions.DateEdited };
            sqlParameters[35] = parameteDateEdited;
            var parameteEditorId2 = new SqlParameter("@EditorId2", SqlDbType.VarChar, 500) { Value = questions.EditorId_2 };
            sqlParameters[36] = parameteEditorId2;
            var parameteDateEdited2 = new SqlParameter("@DateEdited2", SqlDbType.VarChar, 500) { Value = questions.DateEdited_2 };
            sqlParameters[37] = parameteDateEdited2;
            var parameteSourceSBD = new SqlParameter("@SourceSBD", SqlDbType.VarChar, 500) { Value = questions.Source_SBD };
            sqlParameters[38] = parameteSourceSBD;
            var parameteWhoOwns = new SqlParameter("@WhoOwns", SqlDbType.VarChar, 500) { Value = questions.WhoOwns };
            sqlParameters[39] = parameteWhoOwns;
            var parameteWhereUsed = new SqlParameter("@WhereUsed", SqlDbType.VarChar, 500) { Value = questions.WhereUsed };
            sqlParameters[40] = parameteWhereUsed;
            var parametePointBiserialsId = new SqlParameter("@PointBiserialsId", SqlDbType.VarChar, 500) { Value = questions.PointBiserialsId };
            sqlParameters[41] = parametePointBiserialsId;
            var parameteFeedback = new SqlParameter("@Feedback", SqlDbType.VarChar, 500) { Value = questions.Feedback };
            sqlParameters[42] = parameteFeedback;
            var parameteActive = new SqlParameter("@Active", SqlDbType.Int, 6) { Value = questions.Active };
            sqlParameters[43] = parameteActive;
            var parameteDeleted = new SqlParameter("@Deleted", SqlDbType.Int, 6) { Value = questions.Deleted };
            sqlParameters[44] = parameteDeleted;
            var parameteQuestionNumber = new SqlParameter("@QuestionNumber", SqlDbType.NChar, 10) { Value = questions.QuestionNumber };
            sqlParameters[45] = parameteQuestionNumber;
            var parameteTestNumber = new SqlParameter("@TestNumber", SqlDbType.NChar, 10) { Value = questions.TestNumber };
            sqlParameters[46] = parameteTestNumber;
            var parameteQNorming = new SqlParameter("@QNorming", SqlDbType.Float, 6) { Value = questions.Q_Norming };
            sqlParameters[47] = parameteQNorming;
            var parameteExhibitTab1 = new SqlParameter("@ExhibitTab1", SqlDbType.VarChar, 5000) { Value = questions.ExhibitTab1 };
            sqlParameters[48] = parameteExhibitTab1;
            var parameteExhibitTab2 = new SqlParameter("@ExhibitTab2", SqlDbType.VarChar, 5000) { Value = questions.ExhibitTab2 };
            sqlParameters[49] = parameteExhibitTab2;
            var parameteExhibitTab3 = new SqlParameter("@ExhibitTab3", SqlDbType.VarChar, 5000) { Value = questions.ExhibitTab3 };
            sqlParameters[50] = parameteExhibitTab3;
            var parameteListeningFileUrl = new SqlParameter("@ListeningFileUrl", SqlDbType.VarChar, 500) { Value = questions.ListeningFileUrl };
            sqlParameters[51] = parameteListeningFileUrl;
            sqlParameters[52] = new SqlParameter("@AlternateStem", SqlDbType.VarChar, 5000) { Value = questions.AlternateStem };
            var parameteAccreditationCategories = new SqlParameter("@AccreditationCategoriesId", SqlDbType.VarChar, 500) { Value = questions.AccreditationCategoriesId };
            sqlParameters[53] = parameteAccreditationCategories;
            var parameteQSENKSACompetencies = new SqlParameter("@QSENKSACompetenciesId", SqlDbType.VarChar, 500) { Value = questions.QSENKSACompetenciesId };
            sqlParameters[54] = parameteQSENKSACompetencies;
            var parameterProgramOfStudyId = new SqlParameter("@ProgramOfStudyId", SqlDbType.Int, 6) { Value = questions.ProgramofStudyId };
            sqlParameters[55] = parameterProgramOfStudyId;
            sqlParameters[56] = new SqlParameter("@CreatedBy", SqlDbType.Int, 6) { Value = questions.CreatedBy };
            sqlParameters[57] = new SqlParameter("@CreatedDate", SqlDbType.DateTime) { Value = questions.CreatedOn };
            sqlParameters[58] = new SqlParameter("@UpdatedBy", SqlDbType.Int, 6) { Value = (Object)questions.UpdatedBy ?? DBNull.Value };
            sqlParameters[59] = new SqlParameter("@UpdatedDate", SqlDbType.DateTime) { Value = (Object)questions.UpdatedOn ?? DBNull.Value };
            sqlParameters[60] = new SqlParameter("@ReleasedBy", SqlDbType.Int, 6) { Value = userId };
            sqlParameters[61] = new SqlParameter("@ReleasedDate", SqlDbType.DateTime) { Value = questions.ReleaseDate };
            sqlParameters[62] = new SqlParameter("@ConceptsId", SqlDbType.VarChar, 500) { Value = questions.ConceptsId };
            _liveAppDataContext.ExecuteStoredProcedure("USPReleaseQuestionsToProduction", sqlParameters);
        }

        public void ReleaseTests(Test test)
        {
            var sqlParameters = new SqlParameter[54];
            var parameterTestId = new SqlParameter("@TestId", SqlDbType.Int, 6) { Value = test.TestId };
            sqlParameters[0] = parameterTestId;
            var parameterProductId = new SqlParameter("@ProductId", SqlDbType.Int, 6) { Value = test.ProductId };
            sqlParameters[1] = parameterProductId;
            var parameterTestName = new SqlParameter("@TestName", SqlDbType.VarChar, 50) { Value = test.TestName };
            sqlParameters[2] = parameterTestName;
            var parameterTestNumber = new SqlParameter("@TestNumber", SqlDbType.Int, 6) { Value = test.TestNumber };
            sqlParameters[3] = parameterTestNumber;
            if (test.ActvationTime.Date != DateTime.MinValue)
            {
                var parameterActivationTime = new SqlParameter("@ActivationTime", SqlDbType.DateTime, 0) { Value = test.ActvationTime };
                sqlParameters[4] = parameterActivationTime;
            }
            else
            {
                var parameterActivationTime = new SqlParameter("@ActivationTime", SqlDbType.DateTime, 0) { Value = DBNull.Value };
                sqlParameters[4] = parameterActivationTime;
            }

            var parameterTimeActivated = new SqlParameter("@TimeActivated", SqlDbType.Int, 6) { Value = test.TimeActivated };
            sqlParameters[5] = parameterTimeActivated;
            var parameterSecureTestS = new SqlParameter("@SecureTestS", SqlDbType.Int, 6) { Value = test.SecureTestS };
            sqlParameters[6] = parameterSecureTestS;
            var parameterSecureTestD = new SqlParameter("@SecureTestD", SqlDbType.Int, 6) { Value = test.SecureTestD };
            sqlParameters[7] = parameterSecureTestD;
            var parameterScramblingS = new SqlParameter("@ScramblingS", SqlDbType.Int, 6) { Value = test.ScramblingS };
            sqlParameters[8] = parameterScramblingS;
            var parameterScramblingD = new SqlParameter("@ScramblingD", SqlDbType.Int, 6) { Value = test.ScramblingD };
            sqlParameters[9] = parameterScramblingD;
            var parameterRemediationS = new SqlParameter("@RemediationS", SqlDbType.Int, 6) { Value = test.RemediationS };
            sqlParameters[10] = parameterRemediationS;
            var parameterRemediationD = new SqlParameter("@RemediationD", SqlDbType.Int, 6) { Value = test.RemediationD };
            sqlParameters[11] = parameterRemediationD;
            var parameterExplanationD = new SqlParameter("@ExplanationD", SqlDbType.Int, 6) { Value = test.ExplanationD };
            sqlParameters[12] = parameterExplanationD;
            var parameterExplanationS = new SqlParameter("@ExplanationS", SqlDbType.Int, 6) { Value = test.ExplanationS };
            sqlParameters[13] = parameterExplanationS;
            var parameterLevelOfDifficultyS = new SqlParameter("@LevelOfDifficultyS", SqlDbType.Int, 6) { Value = test.LevelOfDifficultyS };
            sqlParameters[14] = parameterLevelOfDifficultyS;
            var parameterLevelOfDifficultyD = new SqlParameter("@LevelOfDifficultyD", SqlDbType.Int, 6) { Value = test.LevelOfDifficultyD };
            sqlParameters[15] = parameterLevelOfDifficultyD;
            var parameterNursingProcessS = new SqlParameter("@NursingProcessS", SqlDbType.Int, 6) { Value = test.NursingProcessS };
            sqlParameters[16] = parameterNursingProcessS;
            var parameterNursingProcessD = new SqlParameter("@NursingProcessD", SqlDbType.Int, 6) { Value = test.NursingProcessD };
            sqlParameters[17] = parameterNursingProcessD;
            var parameterClinicalConceptsS = new SqlParameter("@ClinicalConceptsS", SqlDbType.Int, 6) { Value = test.ClinicalConceptsS };
            sqlParameters[18] = parameterClinicalConceptsS;
            var parameterClinicalConceptsD = new SqlParameter("@ClinicalConceptsD", SqlDbType.Int, 6) { Value = test.ClinicalConceptsD };
            sqlParameters[19] = parameterClinicalConceptsD;
            var parameterDemographicsS = new SqlParameter("@DemographicsS", SqlDbType.Int, 6) { Value = test.DemographicsS };
            sqlParameters[20] = parameterDemographicsS;
            var parameterDemographicsD = new SqlParameter("@DemographicsD", SqlDbType.Int, 6) { Value = test.DemographicsD };
            sqlParameters[21] = parameterDemographicsD;
            var parameterClientNeedsS = new SqlParameter("@ClientNeedsS", SqlDbType.Int, 6) { Value = test.ClientNeedsS };
            sqlParameters[22] = parameterClientNeedsS;
            var parameterClientNeedsD = new SqlParameter("@ClientNeedsD", SqlDbType.Int, 6) { Value = test.ClientNeedsD };
            sqlParameters[23] = parameterClientNeedsD;
            var parameterBloomsS = new SqlParameter("@BloomsS", SqlDbType.Int, 6) { Value = test.BloomsS };
            sqlParameters[24] = parameterBloomsS;
            var parameterBloomsD = new SqlParameter("@BloomsD", SqlDbType.Int, 6) { Value = test.BloomsD };
            sqlParameters[25] = parameterBloomsD;
            var parameterTopicS = new SqlParameter("@TopicS", SqlDbType.Int, 6) { Value = test.TopicS };
            sqlParameters[26] = parameterTopicS;
            var parameterTopicD = new SqlParameter("@TopicD", SqlDbType.Int, 6) { Value = test.TopicD };
            sqlParameters[27] = parameterTopicD;
            var parameterSpecialtyAreaS = new SqlParameter("@SpecialtyAreaS", SqlDbType.Int, 6) { Value = test.SpecialtyAreaS };
            sqlParameters[28] = parameterSpecialtyAreaS;
            var parameterSpecialtyAreaD = new SqlParameter("@SpecialtyAreaD", SqlDbType.Int, 6) { Value = test.SpecialtyAreaD };
            sqlParameters[29] = parameterSpecialtyAreaD;
            var parameterSystemS = new SqlParameter("@SystemS", SqlDbType.Int, 6) { Value = test.SystemS };
            sqlParameters[30] = parameterSystemS;
            var parameterSystemD = new SqlParameter("@SystemD", SqlDbType.Int, 6) { Value = test.SystemD };
            sqlParameters[31] = parameterSystemD;
            var parameterCriticalThinkingS = new SqlParameter("@CriticalThinkingS", SqlDbType.Int, 6) { Value = test.CriticalThinkingS };
            sqlParameters[32] = parameterCriticalThinkingS;
            var parameterCriticalThinkingD = new SqlParameter("@CriticalThinkingD", SqlDbType.Int, 6) { Value = test.CriticalThinkingD };
            sqlParameters[33] = parameterCriticalThinkingD;
            var parameterReadingS = new SqlParameter("@ReadingS", SqlDbType.Int, 6) { Value = test.ReadingS };
            sqlParameters[34] = parameterReadingS;
            var parameterReadingD = new SqlParameter("@ReadingD", SqlDbType.Int, 6) { Value = test.ReadingD };
            sqlParameters[35] = parameterReadingD;
            var parameterMathS = new SqlParameter("@MathS", SqlDbType.Int, 6) { Value = test.MathS };
            sqlParameters[36] = parameterMathS;
            var parameterMathD = new SqlParameter("@MathD", SqlDbType.Int, 6) { Value = test.MathD };
            sqlParameters[37] = parameterMathD;
            var parameterWritingS = new SqlParameter("@WritingS", SqlDbType.Int, 6) { Value = test.WritingS };
            sqlParameters[38] = parameterWritingS;
            var parameterWritingD = new SqlParameter("@WritingD", SqlDbType.Int, 6) { Value = test.WritingD };
            sqlParameters[39] = parameterWritingD;
            var parameterRemedationTimeS = new SqlParameter("@RemedationTimeS", SqlDbType.Int, 6) { Value = test.RemedationTimeS };
            sqlParameters[40] = parameterRemedationTimeS;
            var parameterRemedationTimeD = new SqlParameter("@RemedationTimeD", SqlDbType.Int, 6) { Value = test.RemedationTimeD };
            sqlParameters[41] = parameterRemedationTimeD;
            var parameterExplanationTimeS = new SqlParameter("@ExplanationTimeS", SqlDbType.Int, 6) { Value = test.ExplanationTimeS };
            sqlParameters[42] = parameterExplanationTimeS;
            var parameterExplanationTimeD = new SqlParameter("@ExplanationTimeD", SqlDbType.Int, 6) { Value = test.ExplanationTimeD };
            sqlParameters[43] = parameterExplanationTimeD;
            var parameterTimeStampS = new SqlParameter("@TimeStampS", SqlDbType.Int, 6) { Value = test.TimeStampS };
            sqlParameters[44] = parameterTimeStampS;
            var parameterTimeStampD = new SqlParameter("@TimeStampD", SqlDbType.Int, 6) { Value = test.TimeStampD };
            sqlParameters[45] = parameterTimeStampD;
            var parameterActiveTest = new SqlParameter("@ActiveTest", SqlDbType.Int, 6) { Value = test.ActiveTest };
            sqlParameters[46] = parameterActiveTest;
            var parameterTestSubGroup = new SqlParameter("@TestSubGroup", SqlDbType.Int, 6) { Value = test.TestSubGroup };
            sqlParameters[47] = parameterTestSubGroup;
            var parameterUrl = new SqlParameter("@Url", SqlDbType.NVarChar, 200) { Value = test.URL };
            sqlParameters[48] = parameterUrl;
            var parameterPopHeight = new SqlParameter("@PopHeight", SqlDbType.Int, 6) { Value = test.PopupHeight };
            sqlParameters[49] = parameterPopHeight;
            var parameterPopWidth = new SqlParameter("@PopWidth", SqlDbType.Int, 6) { Value = test.PopupWidth };
            sqlParameters[50] = parameterPopWidth;
            var parameterDefaultGroup = new SqlParameter("@DefaultGroup", SqlDbType.Char, 1) { Value = test.DefaultGroup };
            sqlParameters[51] = parameterDefaultGroup;
            var parameterSecondPerQuestion = new SqlParameter("@SecondPerQuestion", SqlDbType.Int, 6) { Value = test.SecondPerQuestion };
            sqlParameters[52] = parameterSecondPerQuestion;
            sqlParameters[53] = new SqlParameter("@ProgramofStudyId", SqlDbType.Int, 6) { Value = test.ProgramofStudyId };

            _liveAppDataContext.ExecuteStoredProcedure("USPReleaseTestsToProduction", sqlParameters);
        }

        public void ReleaseTestQuestion(TestQuestion testQuestion)
        {
            var sqlParameters = new SqlParameter[6];
            var parameterId = new SqlParameter("@Id", SqlDbType.Int, 6) { Value = testQuestion.Id };
            sqlParameters[0] = parameterId;
            var parameterTestId = new SqlParameter("@TestId", SqlDbType.Int, 6) { Value = testQuestion.TestId };
            sqlParameters[1] = parameterTestId;
            var parameterQuestionId = new SqlParameter("@QuestionId", SqlDbType.VarChar, 50) { Value = testQuestion.QuestionId };
            sqlParameters[2] = parameterQuestionId;
            var parameterQID = new SqlParameter("@QID", SqlDbType.Int, 6) { Value = testQuestion.QId };
            sqlParameters[3] = parameterQID;
            var parameterQuestionNumber = new SqlParameter("@QuestionNumber", SqlDbType.Int, 6) { Value = testQuestion.QuestionNumber };
            sqlParameters[4] = parameterQuestionNumber;
            var parameterQNorming = new SqlParameter("@QNorming", SqlDbType.Float, 6) { Value = testQuestion.QNorming };
            sqlParameters[5] = parameterQNorming;

            _liveAppDataContext.ExecuteStoredProcedure("USPReleaseTestQuestionsToProduction", sqlParameters);
        }

        public void ReleaseTestCategory(TestCategory testCategory)
        {
            var sqlParameters = new SqlParameter[5];
            var parameterId = new SqlParameter("@Id", SqlDbType.Int, 6) { Value = testCategory.Id };
            sqlParameters[0] = parameterId;
            var parameterTestId = new SqlParameter("@TestId", SqlDbType.Int, 6) { Value = testCategory.TestId };
            sqlParameters[1] = parameterTestId;
            var parameterCategoryId = new SqlParameter("@CategoryId", SqlDbType.VarChar, 50) { Value = testCategory.CategoryId };
            sqlParameters[2] = parameterCategoryId;
            var parameterStudent = new SqlParameter("@Student", SqlDbType.Int, 6) { Value = testCategory.Student };
            sqlParameters[3] = parameterStudent;
            var parameterAdmin = new SqlParameter("@Admin", SqlDbType.Int, 6) { Value = testCategory.Admin };
            sqlParameters[4] = parameterAdmin;

            _liveAppDataContext.ExecuteStoredProcedure("USPReleaseTestCategoryToProduction", sqlParameters);
        }

        public void ReleaseNorming(Norming norming)
        {
            var sqlParameters = new SqlParameter[6];
            var parameterId = new SqlParameter("@Id", SqlDbType.Int, 6) { Value = norming.Id };
            sqlParameters[0] = parameterId;
            var parameterNumberCorrect = new SqlParameter("@NumberCorrect", SqlDbType.Float, 6) { Value = norming.NumberCorrect };
            sqlParameters[1] = parameterNumberCorrect;
            var parameterCorrect = new SqlParameter("@Correct", SqlDbType.Float, 6) { Value = norming.Correct };
            sqlParameters[2] = parameterCorrect;
            var parameterPercentileRank = new SqlParameter("@PercentileRank", SqlDbType.Float, 6) { Value = norming.PercentileRank };
            sqlParameters[3] = parameterPercentileRank;
            var parameterProbability = new SqlParameter("@Probability", SqlDbType.Int, 6) { Value = norming.Probability };
            sqlParameters[4] = parameterProbability;
            var parameterTestId = new SqlParameter("@TestId", SqlDbType.Int, 6) { Value = norming.TestId };
            sqlParameters[5] = parameterTestId;

            _liveAppDataContext.ExecuteStoredProcedure("USPReleaseNormingToProduction", sqlParameters);
        }

        public void ReleaseNorm(Norm norm)
        {
            var sqlParameters = new SqlParameter[5];
            var parameterId = new SqlParameter("@Id", SqlDbType.Int, 6) { Value = norm.Id };
            sqlParameters[0] = parameterId;
            var parameterChartType = new SqlParameter("@ChartType", SqlDbType.NVarChar, 50) { Value = norm.ChartType };
            sqlParameters[1] = parameterChartType;
            var parameterChartId = new SqlParameter("@ChartId", SqlDbType.Int, 6) { Value = norm.ChartID };
            sqlParameters[2] = parameterChartId;
            var parameterNorm = new SqlParameter("@Norm", SqlDbType.Real, 6) { Value = norm.NormValue ?? Convert.DBNull };
            sqlParameters[3] = parameterNorm;
            var parameterTestId = new SqlParameter("@TestId", SqlDbType.Int, 6) { Value = norm.TestId };
            sqlParameters[4] = parameterTestId;

            _liveAppDataContext.ExecuteStoredProcedure("USPReleaseNormToProduction", sqlParameters);
        }

        public void ReleaseQuestionLippincott(QuestionLippincott questionLippincot)
        {
            var sqlParameters = new SqlParameter[2];
            var parameterId = new SqlParameter("@LippincottId", SqlDbType.Int, 6) { Value = questionLippincot.LippincottID };
            sqlParameters[0] = parameterId;
            var parameterQID = new SqlParameter("@QID", SqlDbType.Int, 6) { Value = questionLippincot.QID };
            sqlParameters[1] = parameterQID;

            _liveAppDataContext.ExecuteStoredProcedure("USPReleaseQuestionLippincotToProduction", sqlParameters);
        }

        public void ReleaseLippincott(Lippincott lippincot)
        {
            var sqlParameters = new SqlParameter[6];
            var parameterId = new SqlParameter("@LippincottId", SqlDbType.Int, 6) { Value = lippincot.LippincottID };
            sqlParameters[0] = parameterId;
            var parameterRemediationId = new SqlParameter("@RemediationId", SqlDbType.Int, 6) { Value = lippincot.RemediationId };
            sqlParameters[1] = parameterRemediationId;
            var parameterLippincottTitle = new SqlParameter("@LippincottTitle", SqlDbType.VarChar, 800) { Value = lippincot.LippincottTitle };
            sqlParameters[2] = parameterLippincottTitle;
            var parameterLippincottExplanation = new SqlParameter("@LippincottExplanation", SqlDbType.NText, 0) { Value = lippincot.LippincottExplanation };
            sqlParameters[3] = parameterLippincottExplanation;
            var parameterLippincottTitle2 = new SqlParameter("@LippincottTitle2", SqlDbType.VarChar, 800) { Value = lippincot.LippincottTitle2 };
            sqlParameters[4] = parameterLippincottTitle2;
            var parameterLippincottExplanation2 = new SqlParameter("@LippincottExplanation2", SqlDbType.NText, 0) { Value = lippincot.LippincottExplanation2 };
            sqlParameters[5] = parameterLippincottExplanation2;

            _liveAppDataContext.ExecuteStoredProcedure("USPReleaseLippincotToProduction", sqlParameters);
        }

        public void ReleaseRemediation(Remediation remediation)
        {
            var sqlParameters = new SqlParameter[3];
            var parameterId = new SqlParameter("@RemediationId", SqlDbType.Int, 6) { Value = remediation.RemediationId };
            sqlParameters[0] = parameterId;
            var parameterExplanation = new SqlParameter("@Explanation", SqlDbType.VarChar, 5000) { Value = remediation.Explanation };
            sqlParameters[1] = parameterExplanation;
            var parameterTopicTitle = new SqlParameter("@TopicTitle", SqlDbType.VarChar, 500) { Value = remediation.TopicTitle };
            sqlParameters[2] = parameterTopicTitle;

            _liveAppDataContext.ExecuteStoredProcedure("USPReleaseRemediationToProduction", sqlParameters);
        }

        public void ReleaseAnswerChoice(AnswerChoice answerChoice)
        {
            var sqlParameters = new SqlParameter[10];
            sqlParameters[0] = new SqlParameter("@QId", SqlDbType.Int, 6) { Value = answerChoice.Id };
            sqlParameters[1] = new SqlParameter("@AIndex", SqlDbType.Char, 1) { Value = answerChoice.Aindex };
            sqlParameters[2] = new SqlParameter("@AText", SqlDbType.VarChar, 3000) { Value = answerChoice.Atext };
            sqlParameters[3] = new SqlParameter("@Correct", SqlDbType.Int, 6) { Value = answerChoice.Correct };
            sqlParameters[4] = new SqlParameter("@AnswerConnectId", SqlDbType.Int, 6) { Value = answerChoice.AnswerConnectId };
            sqlParameters[5] = new SqlParameter("@AType", SqlDbType.Int, 6) { Value = answerChoice.ActionType };
            sqlParameters[6] = new SqlParameter("@InitialPos", SqlDbType.Int, 6) { Value = answerChoice.InitialPosition };
            sqlParameters[7] = new SqlParameter("@Unit", SqlDbType.VarChar, 50) { Value = answerChoice.Unit };
            sqlParameters[8] = new SqlParameter("@AnswerId", SqlDbType.Int, 6) { Value = answerChoice.AnswerId };
            sqlParameters[9] = new SqlParameter("@AlternateAText", SqlDbType.VarChar, 3000) { Value = answerChoice.AlternateAText };
            _liveAppDataContext.ExecuteStoredProcedure("USPReleaseAnswerChoiceToProduction", sqlParameters);
        }

        public void DeleteTestDependentRows(string testIds)
        {
            var sqlParameters = new SqlParameter[1];
            var parameterIds = new SqlParameter("@TestIds", SqlDbType.VarChar, 4000) { Value = testIds };
            sqlParameters[0] = parameterIds;

            _liveAppDataContext.ExecuteStoredProcedure("USPDeleteTestChildTableRows", sqlParameters);
        }

        public void DeleteReleaseQuestionLippinCott(string lippincottIds)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@LippincottIds", SqlDbType.VarChar, 4000) { Value = lippincottIds };
            #endregion

            _liveAppDataContext.ExecuteNonQuery("USPDeleteReleaseQuestionLippinCott", sqlParameters);
        }

        public void DeleteTestQuestions(int testId)
        {
            var sqlParameters = new SqlParameter[1];
            var parameterIds = new SqlParameter("@TestId", SqlDbType.Int, 4) { Value = testId };
            sqlParameters[0] = parameterIds;

            _dataContext.ExecuteStoredProcedure("USPDeleteTestQuestion", sqlParameters);
        }

        public void SaveTestQuestion(TestQuestion testQuestion)
        {
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@TestId", SqlDbType.Int, 4) { Value = testQuestion.TestId };
            sqlParameters[1] = new SqlParameter("@QuestionId", SqlDbType.VarChar, 50) { Value = testQuestion.QuestionId };
            sqlParameters[2] = new SqlParameter("@Qid", SqlDbType.Int, 4) { Value = testQuestion.QId };
            sqlParameters[3] = new SqlParameter("@QuestionNumber", SqlDbType.Int, 4) { Value = testQuestion.QuestionNumber };

            _dataContext.ExecuteStoredProcedure("USPSaveTestQuestion", sqlParameters);
        }

        public IEnumerable<QuestionResult> GetQuestionListInTest(int testId)
        {
            var questionResult = new List<QuestionResult>();
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@TestId", SqlDbType.Int, 4) { Value = testId };

            using (IDataReader reader = _dataContext.GetDataReader("USPGetQuestionsInTest", sqlParameters))
            {
                while (reader.Read())
                {
                    questionResult.Add(new QuestionResult
                    {
                        Stem = (reader["Stem"] as string) ?? string.Empty,
                        QuestionID = (reader["QuestionID"] as string) ?? string.Empty,
                        QID = (reader["QID"] as int?) ?? 0,
                        QN = (reader["QuestionNumber"] as int?) ?? 0
                    });
                }
            }

            return questionResult.ToArray();
        }

        public void SyncQuestionLookup(Question question)
        {
            // Check tis
        }

        public IList<LookupMapping> GetLookupMappings(string ids, LookupType type, bool IsReverseMapping)
        {
            IList<LookupMapping> mappingData = new List<LookupMapping>();

            #region SqlParameters
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@Ids", SqlDbType.VarChar, 5000) { Value = ids };
            sqlParameters[1] = new SqlParameter("@TypeId", SqlDbType.TinyInt, 1) { Value = (byte)type };
            sqlParameters[2] = new SqlParameter("@IsReverseMapping", SqlDbType.Bit, 1) { Value = IsReverseMapping };
            #endregion

            using (IDataReader reader = _liveAppDataContext.GetDataReader("dbo.uspGetLookupMappings", sqlParameters))
            {
                while (reader.Read())
                {
                    mappingData.Add(new LookupMapping(
                        reader["MappingId"].ToInt(),
                        type,
                        IsReverseMapping ? reader["MappedId"].ToInt() : reader["Id"].ToInt(),
                        IsReverseMapping ? reader["Id"].ToInt() : reader["MappedId"].ToInt()));
                }
            }

            return mappingData;
        }

        public Lookup GetLookup(string matchText, LookupType type)
        {
            Lookup lookup = null;

            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@MatchText", SqlDbType.VarChar, 300) { Value = matchText };
            sqlParameters[1] = new SqlParameter("@TypeId", SqlDbType.TinyInt, 1) { Value = (int)type };
            #endregion

            using (IDataReader reader = _liveAppDataContext.GetDataReader("dbo.uspGetLookupByText", sqlParameters))
            {
                while (reader.Read())
                {
                    lookup = new Lookup(
                        reader["Id"].ToInt(),
                        type,
                        reader["OriginalId"].ToInt(),
                        matchText);
                }
            }

            return lookup;
        }

        public Lookup GetLookup(int id)
        {
            Lookup lookup = null;

            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id };
            #endregion

            using (IDataReader reader = _liveAppDataContext.GetDataReader("dbo.uspGetLookup", sqlParameters))
            {
                while (reader.Read())
                {
                    lookup = new Lookup(
                        id,
                        EnumHelper.GetLookupType(reader["TypeId"].ToInt()),
                        reader["OriginalId"].ToInt(),
                        reader["DisplayText"].ToString());
                }
            }

            return lookup;
        }

        public Lookup GetLookup(int originalId, LookupType type)
        {
            Lookup lookup = null;

            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@OriginalId", SqlDbType.Int, 4) { Value = originalId };
            sqlParameters[1] = new SqlParameter("@TypeId", SqlDbType.TinyInt, 1) { Value = (byte)type };
            #endregion

            using (IDataReader reader = _liveAppDataContext.GetDataReader("dbo.uspGetLookupByOriginalId", sqlParameters))
            {
                while (reader.Read())
                {
                    lookup = new Lookup(
                        reader["Id"].ToInt(),
                        type,
                        originalId,
                        reader["DisplayText"].ToString());
                }
            }

            return lookup;
        }

        public int InsertLookup(int originalId, LookupType type, string displayText, int sortOrder)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@OriginalId", SqlDbType.Int, 4) { Value = originalId };
            sqlParameters[1] = new SqlParameter("@TypeId", SqlDbType.TinyInt, 1) { Value = (byte)type };
            sqlParameters[2] = new SqlParameter("@DisplayText", SqlDbType.VarChar, 300) { Value = displayText };
            sqlParameters[3] = new SqlParameter("@SortOrder", SqlDbType.Int, 4) { Value = sortOrder };
            sqlParameters[4] = new SqlParameter("@Id", SqlDbType.Int, 4);
            sqlParameters[4].Direction = ParameterDirection.Output;
            #endregion

            _liveAppDataContext.ExecuteNonQuery("dbo.uspInsertLookup", sqlParameters);

            return sqlParameters[4].Value.ToInt();
        }

        public int InsertLookupMapping(LookupType type, int lookupId, int mappedTo)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@TypeId", SqlDbType.TinyInt, 1) { Value = (byte)type };
            sqlParameters[1] = new SqlParameter("@LookupId", SqlDbType.Int, 4) { Value = lookupId };
            sqlParameters[2] = new SqlParameter("@MappedTo", SqlDbType.Int, 4) { Value = mappedTo };
            sqlParameters[3] = new SqlParameter("@Id", SqlDbType.Int, 4);
            sqlParameters[3].Direction = ParameterDirection.Output;
            #endregion

            _liveAppDataContext.ExecuteNonQuery("dbo.uspInsertLookupMapping", sqlParameters);

            return sqlParameters[3].Value.ToInt();
        }

        public void DeleteLookup(int id)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id };
            #endregion

            _liveAppDataContext.ExecuteNonQuery("dbo.uspDeleteLookup", sqlParameters);
        }

        public void DeleteLookupMapping(int id)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", SqlDbType.Int, 4) { Value = id };
            #endregion

            _liveAppDataContext.ExecuteNonQuery("dbo.uspDeleteLookupMapping", sqlParameters);
        }

        public CustomFRLookupData GetCustomFRLookupMappings(int questionId,int programofstudyId)
        {
            CustomFRLookupData mappingData = new CustomFRLookupData();

            #region SqlParameters
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@QuestionId", SqlDbType.Int, 4) { Value = questionId };
            sqlParameters[1] = new SqlParameter("@ProgramofStudyId", SqlDbType.Int, 4) { Value = programofstudyId };
            #endregion

            List<LookupMapping> _mappings = new List<LookupMapping>();
            List<Lookup> _lookup = new List<Lookup>();

            using (IDataReader reader = _liveAppDataContext.GetDataReader("uspGetCustomFRLookupMappings", sqlParameters))
            {
                while (reader.Read())
                {
                    _mappings.Add(new LookupMapping(
                        reader["MappingId"].ToInt(),
                        EnumHelper.GetLookupType(reader["TypeId"].ToInt()),
                        reader["LookupId"].ToInt(),
                        reader["MappedTo"].ToInt()));
                }

                reader.NextResult();
                var topicMapping = programofstudyId == 1
                                       ? LookupType.Lookup123TopicMapping
                                       : LookupType.Lookup456TopicMapping;

                while (reader.Read())
                {
                    var categoryTopicMapping = new LookupMapping(
                        reader["MappingId"].ToInt(),
                        topicMapping,
                        reader["LookupId"].ToInt(),
                        reader["MappedTo"].ToInt());

                    var categoryLookup = new Lookup(
                        reader["LookupId"].ToInt(),
                        EnumHelper.GetLookupType(reader["CategoryTypeId"].ToInt()),
                        reader["CategoryOriginalId"].ToInt(),
                        string.Empty);
                    _lookup.Add(categoryLookup);

                    var topicLookup = _lookup.Where(p => p.Type == LookupType.CustomizedFRTopics
                        && p.Id == reader["MappedTo"].ToInt()).FirstOrDefault();

                    if (topicLookup == null)
                    {
                        topicLookup = new Lookup(
                            reader["MappedTo"].ToInt(),
                            LookupType.CustomizedFRTopics,
                            0,
                            reader["TopicText"].ToString());
                        _lookup.Add(topicLookup);
                    }

                    mappingData.CategoryTopicMappings.Add(categoryTopicMapping, new KeyValuePair<Lookup, Lookup>(categoryLookup, topicLookup));
                }

                reader.NextResult();

                while (reader.Read())
                {
                    mappingData.IsRemediationMapped = reader["IsRemediationMapped"].ToBool();
                    mappingData.IsTestMapped = reader["IsTestMapped"].ToBool();
                }

                var questionMapping = programofstudyId == 1
                                          ? LookupType.Type17QuestionMappingforTests
                                          : LookupType.Type20QuestionMappingforTests;
                var remediationMapping = programofstudyId == 1
                                          ? LookupType.Type17QuestionMappingforRemediation
                                          : LookupType.Type20QuestionMappingforRemediation;
                var systemCategory = programofstudyId == 1
                                          ? LookupType.CustomizedFRSystemCategory
                                          : LookupType.PNCustomizedFRSystemCategory;
                var psychiatricCategory = programofstudyId == 1
                                         ? LookupType.CustomizedFRPsychiatricCategory
                                         : LookupType.PNCustomizedFRPsychiatricCategory;
                var managementOfCareCategory = programofstudyId == 1
                                         ? LookupType.CustomizedFRManagementofCareCategory
                                         : LookupType.PNCustomizedFRManagementofCareCategory;

                mappingData.TestMappings = _mappings.Where(p => p.Type == questionMapping).ToDictionary(p => p.Id);

                mappingData.RemediationMappings = _mappings.Where(p => p.Type == remediationMapping).ToDictionary(p => p.Id);

                mappingData.SystemCategories = _lookup.Where(p => p.Type == systemCategory).ToDictionary(p => p.Id);

                mappingData.PsychiatricCategory = _lookup.Where(p => p.Type == psychiatricCategory).ToDictionary(p => p.Id);

                mappingData.ManagementOfCareCategory = _lookup.Where(p => p.Type == managementOfCareCategory).ToDictionary(p => p.Id);

                mappingData.Topics = _lookup.Where(p => p.Type == LookupType.CustomizedFRTopics).ToDictionary(p => p.Id);
            }

            return mappingData;
        }

        public void SaveUploadedQuestionDetails(string GUID, string fileName, int userId)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@FileName", SqlDbType.VarChar, 100) { Value = fileName };

            sqlParameters[1] = new SqlParameter("@GUID", SqlDbType.VarChar, 100) { Value = GUID };

            sqlParameters[2] = new SqlParameter("@CreatedBy", SqlDbType.Int, 6) { Value = userId };
            #endregion
            _dataContext.ExecuteNonQuery("uspSaveUploadedQuestionDetails", sqlParameters);
        }

        public void SaveUploadedRemediation(Remediation remediation)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[2];

            var parameterRemediationId = new SqlParameter("@Explanation", SqlDbType.VarChar, 5000) { Value = remediation.Explanation };
            sqlParameters[0] = parameterRemediationId;

            var parameterExplanation = new SqlParameter("@TopicTitle", SqlDbType.VarChar, 500) { Value = remediation.TopicTitle };
            sqlParameters[1] = parameterExplanation;

            #endregion

            _dataContext.ExecuteStoredProcedure("uspSaveUploadedRemediations", sqlParameters);
        }

        public void SaveLoginContents(LoginContent loginContent)
        {
            var sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@Id", SqlDbType.Int, 4) { Value = loginContent.Id };
            sqlParameters[1] = new SqlParameter("@Content", SqlDbType.NVarChar, 8000) { Value = loginContent.Content };
            sqlParameters[2] = new SqlParameter("@ReleaseStatus", SqlDbType.Char, 1) { Value = loginContent.ReleaseStatus };
            sqlParameters[3] = new SqlParameter("@UpdaetdBy", SqlDbType.Int, 4) { Value = loginContent.AdminUserId };
            _dataContext.ExecuteStoredProcedure("uspUpdateLoginContent", sqlParameters);
        }

        public void ReleaseLoginContent(LoginContent loginContent)
        {
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@Id", SqlDbType.Int, 4) { Value = loginContent.Id };
            sqlParameters[1] = new SqlParameter("@Content", SqlDbType.NVarChar, 8000) { Value = loginContent.Content };
            sqlParameters[2] = new SqlParameter("@ReleasedBy", SqlDbType.Int, 4) { Value = loginContent.AdminUserId };
            _liveAppDataContext.ExecuteStoredProcedure("uspReleaseLoginContent", sqlParameters);
        }

        public void RevertLoginContent(LoginContent loginContent)
        {
            var sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@Id", SqlDbType.Int, 4) { Value = loginContent.Id };
            sqlParameters[1] = new SqlParameter("@UpdatedBy", SqlDbType.Int, 4) { Value = loginContent.AdminUserId };
            using (IDataReader reader = _dataContext.GetDataReader("uspRevertLoginContent", sqlParameters))
            {
                while (reader.Read())
                {
                    loginContent.Id = (reader["Id"] as int?) ?? 0;
                    loginContent.Content = (reader["Content"] as string) ?? string.Empty;
                    loginContent.ReleaseStatus = (reader["ReleaseStatus"] as string) ?? string.Empty;
                    loginContent.ReleasedContent = (reader["ReleasedContent"] as string) ?? string.Empty;
                }
            }
        }

        public void UpdateQuestionLog(Question question)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[5];

            sqlParameters[0] = new SqlParameter("@QId", SqlDbType.Int, 4) { Value = question.Id };
            sqlParameters[1] = new SqlParameter("@UpdatedDate", SqlDbType.DateTime) { Value = (Object)question.UpdatedOn ?? DBNull.Value };
            sqlParameters[2] = new SqlParameter("@UpdatedBy", SqlDbType.Int, 4) { Value = question.UpdatedBy };
            sqlParameters[3] = new SqlParameter("@ReleasedDate", SqlDbType.DateTime) { Value = question.ReleaseDate };
            sqlParameters[4] = new SqlParameter("@ReleasedBy", SqlDbType.Int, 4) { Value = question.ReleasedBy };

            #endregion

            _dataContext.ExecuteStoredProcedure("uspUpdateQuestionLog", sqlParameters);
        }

        public void DeleteAnswerChoices(string AnswerIds)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@AnswerIds", SqlDbType.VarChar, 200) { Value = AnswerIds };
            #endregion
            _dataContext.ExecuteStoredProcedure("uspDeleteAnswerChoicesByIds", sqlParameters);
        }

        public void DeleteQuestionMappingByQId(int QId)
        {
            #region SqlParameters
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@QId", SqlDbType.Int) { Value = QId };
            #endregion
            _liveAppDataContext.ExecuteStoredProcedure("uspDeleteQuestionMappingByQId", sqlParameters);
        }
        #endregion


        private static void BindLippinCottRemediationEntity(List<Lippincott> lippinCotts, IDataReader reader)
        {
            lippinCotts.Add(new Lippincott
            {
                LippincottID = (reader["LippincottID"] as int?) ?? 0,
                RemediationId = (reader["RemediationID"] as int?) ?? 0,
                LippincottTitle = (reader["LippincottTitle"] as String) ?? string.Empty,
                Remediation = new Remediation
                {
                    TopicTitle = (reader["TopicTitle"] as String) ?? string.Empty
                }
            });
        }

        private IEnumerable<TestQuestion> GetTestQuestions(IDataContext dataContext, int testId, string testIds)
        {
            var testQuestion = new List<TestQuestion>();
            var sqlParameters = new SqlParameter[2];
            var parameterId = new SqlParameter("@TestId", SqlDbType.Int, 6) { Value = testId };
            sqlParameters[0] = parameterId;
            var parameterTestIds = new SqlParameter("@TestIds", SqlDbType.VarChar, 4000) { Value = testIds };
            sqlParameters[1] = parameterTestIds;

            using (IDataReader reader = dataContext.GetDataReader("USPGetListOfTestQuestions", sqlParameters))
            {
                while (reader.Read())
                {
                    testQuestion.Add(new TestQuestion
                    {
                        TestId = (reader["TestID"] as int?) ?? 0,
                        QuestionId = (reader["QuestionID"] as String) ?? string.Empty,
                        QId = (reader["QID"] as int?) ?? 0,
                        QuestionNumber = (reader["QuestionNumber"] as int?) ?? 0,
                        Id = (reader["id"] as int?) ?? 0,
                        QNorming = (reader["Q_Norming"] as double?) ?? 0,
                    });
                }

                return testQuestion;
            }
        }
    }
}
