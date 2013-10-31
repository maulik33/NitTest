using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;

namespace NursingLibrary.DAO
{
    public class QuestionRepository : IQuestionRepository
    {
        #region Fields

        private readonly IDataContext _dataContext;

        #endregion Fields

        #region Constructors

        public QuestionRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #endregion Constructors

        #region Methods

        public IEnumerable<UserAnswer> GetAnswersForQuestion(int userTestId)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[1];
            var parameterTestId = new SqlParameter("@UserTestID", SqlDbType.Int, 4) { Value = userTestId };
            sqlParameters[0] = parameterTestId;

            var userAnswers = new List<UserAnswer>();
            using (var reader = _dataContext.GetDataReader("uspGetUserAnswers", sqlParameters))
            {
                while (reader.Read())
                {
                    userAnswers.Add(new UserAnswer
                    {
                        ID = (reader["ID"] as int?) ?? 0,
                        UserTestID = (reader["UserTestID"] as int?) ?? 0,
                        QID = (reader["QID"] as int?) ?? 0,
                        AnswerID = (reader["AnswerID"] as int?) ?? 0,
                        AIndex = (reader["AIndex"] as string) ?? string.Empty,
                        AText = (reader["AText"] as string) ?? string.Empty,
                        Correct = (reader["Correct"] as int?) ?? 0,
                        AnswerConnectID = (reader["AnswerConnectID"] as int?) ?? 0,
                        AType = (reader["AType"] as int?) ?? 0,
                        initialPos = (reader["initialPos"] as int?) ?? 0
                    });
                }
            }

            return userAnswers.ToArray();

            #endregion
        }

        public IEnumerable<UserAnswer> GetHotSpotAnswerByID(int questionId)
        {
            var sqlParameters = new SqlParameter[1];
            var parameterTestId = new SqlParameter("@QuestionID", SqlDbType.Int, 4) { Value = questionId };
            sqlParameters[0] = parameterTestId;

            var userAnswers = new List<UserAnswer>();
            using (var reader = _dataContext.GetDataReader("uspGetHotSpotAnswerByID", sqlParameters))
            {
                while (reader.Read())
                {
                    userAnswers.Add(new UserAnswer
                    {
                        QID = (reader["QID"] as int?) ?? 0,
                        AnswerID = (reader["AnswerID"] as int?) ?? 0,
                        AIndex = (reader["AIndex"] as string) ?? string.Empty,
                        AText = (reader["AText"] as string) ?? string.Empty,
                        Correct = (reader["Correct"] as int?) ?? 0,
                        AnswerConnectID = (reader["AnswerConnectID"] as int?) ?? 0,
                        AType = (reader["AType"] as int?) ?? 0,
                        initialPos = (reader["initialPos"] as int?) ?? 0,
                        Stimulus = (reader["Stimulus"] as string) ?? string.Empty
                    });
                }
            }

            return userAnswers.ToArray();
        }

        public IEnumerable<Lippincott> GetLippincottAssignedInQuestion(int questionId)
        {
            var sqlParameters = new SqlParameter[1];
            var parameterquestionId = new SqlParameter("@QID", SqlDbType.Int, 4) { Value = questionId };
            sqlParameters[0] = parameterquestionId;

            var lippincott = new List<Lippincott>();
            using (var reader = _dataContext.GetDataReader("uspGetLippincottAssignedInQuestion", sqlParameters))
            {
                while (reader.Read())
                {
                    lippincott.Add(new Lippincott
                    {
                        QId = (reader["QID"] as int?) ?? 0,
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

        public IEnumerable<Question> GetPrevNextQuestionInTest(int testId, int questionId, string typeOfFileId)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[3];
            var parameterTestId = new SqlParameter("@TestID", SqlDbType.Int, 4) { Value = testId };
            sqlParameters[0] = parameterTestId;
            var parameterquestionId = new SqlParameter("@questionId", SqlDbType.Int, 4) { Value = questionId };
            sqlParameters[1] = parameterquestionId;
            sqlParameters[2] = new SqlParameter("@typeOfFileId", SqlDbType.VarChar, 500) { Value = typeOfFileId };
            var questions = new List<Question>();
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetPrevNextItemInTest", sqlParameters))
            {
                while (reader.Read())
                {
                    questions.Add(new Question
                    {
                        Stem = (reader["Stem"] as string) ?? string.Empty,
                        Url = (reader["ListeningFileUrl"] as string) ?? string.Empty,
                        Explanation = (reader["Explanation"] as string) ?? string.Empty,
                        RemediationId = (reader["RemediationId"] as int?) ?? 0,
                        TopicTitleId = (reader["TopicTitleId"] as string) ?? string.Empty,
                        ItemTitle = (reader["ItemTitle"] as string) ?? string.Empty,
                        Id = (reader["QID"] as int?) ?? 0,
                        Type = GetQuestionType((reader["QuestionType"] as int?) ?? 0),
                        FileType = GetQuestionFileType((reader["TypeOfFileID"] as int?) ?? 0),
                        Pointer = GetQuestionPointer((reader["PointerType"] as int?) ?? 0),
                        QuestionNumber = (reader["QuestionNumber"] as int?) ?? 0,
                        Active = (reader["Active"] as int?) ?? 1
                    });
                }
            }

            return questions.ToArray();
        }

        public IEnumerable<Question> GetPrevNextQuestions(int userTestId, int questionId, string typeOfFileId, bool inCorrectOnly)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[4];
            var parameterTestId = new SqlParameter("@userTestId", SqlDbType.Int, 4) { Value = userTestId };
            sqlParameters[0] = parameterTestId;
            var parameterquestionId = new SqlParameter("@questionId", SqlDbType.Int, 4) { Value = questionId };
            sqlParameters[1] = parameterquestionId;
            sqlParameters[2] = new SqlParameter("@typeOfFileId", SqlDbType.VarChar, 500) { Value = typeOfFileId };
            sqlParameters[3] = new SqlParameter("@inCorrectOnly", SqlDbType.Bit, 1) { Value = inCorrectOnly };
            #endregion
            var questions = new List<Question>();
            using (IDataReader reader = _dataContext.GetDataReader("uspGetTestQuestions", sqlParameters))
            {
                while (reader.Read())
                {
                    questions.Add(new Question
                    {
                        RemediationTime = (reader["TimeSpendForRemedation"] as int?) ?? 0,
                        ExplanationTime = (reader["TimeSpendForExplanation"] as int?) ?? 0,
                        Stem = (reader["Stem"] as string) ?? string.Empty,
                        AlternateStem = (reader["AlternateStem"] as string) ?? string.Empty,
                        Url = (reader["ListeningFileUrl"] as string) ?? string.Empty,
                        Explanation = (reader["Explanation"] as string) ?? string.Empty,
                        RemediationId = (reader["RemediationId"] as int?) ?? 0,
                        TopicTitleId = (reader["TopicTitleId"] as string) ?? string.Empty,
                        ItemTitle = (reader["ItemTitle"] as string) ?? string.Empty,
                        Id = (reader["QID"] as int?) ?? 0,
                        UserTestId = (reader["UserTestID"] as int?) ?? 0,
                        Type = GetQuestionType((reader["QuestionType"] as int?) ?? 0),
                        FileType = GetQuestionFileType((reader["TypeOfFileID"] as int?) ?? 0),
                        Pointer = GetQuestionPointer((reader["PointerType"] as int?) ?? 0),
                        QuestionNumber = (reader["QuestionNumber"] as int?) ?? 0,
                        Active = (reader["Active"] as int?) ?? 1
                    });
                }
            }

            return questions.ToArray();
        }

        public IEnumerable<Question> GetQuestionByTest(int testId)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[1];
            var parameterTestId = new SqlParameter("@testId", SqlDbType.Int, 4) { Value = testId };
            sqlParameters[0] = parameterTestId;
            #endregion
            var questions = new List<Question>();
            using (IDataReader reader = _dataContext.GetDataReader("uspGetQuestionByTest", sqlParameters))
            {
                while (reader.Read())
                {
                    questions.Add(new Question
                    {
                        Stem = (reader["Stem"] as string) ?? string.Empty,
                        Url = (reader["ListeningFileUrl"] as string) ?? string.Empty,
                        Explanation = (reader["Explanation"] as string) ?? string.Empty,
                        RemediationId = (reader["RemediationId"] as int?) ?? 0,
                        TopicTitleId = (reader["TopicTitleId"] as string) ?? string.Empty,
                        ItemTitle = (reader["ItemTitle"] as string) ?? string.Empty,
                        Id = (reader["QID"] as int?) ?? 0,
                        Type = GetQuestionType((reader["QuestionType"] as int?) ?? 0),
                        FileType = GetQuestionFileType((reader["TypeOfFileID"] as int?) ?? 0),
                        Pointer = GetQuestionPointer((reader["PointerType"] as int?) ?? 0),
                        QuestionNumber = (reader["QuestionNumber"] as int?) ?? 0,
                        Active = (reader["Active"] as int?) ?? 1
                    });
                }
            }

            return questions.ToArray();
        }

        public QuestionExhibit GetQuestionExhibitByID(int questionId)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[1];
            var parameterQuestionId = new SqlParameter("@QuestionID", SqlDbType.Int, 4) { Value = questionId };
            sqlParameters[0] = parameterQuestionId;

            var questionExhibit = new QuestionExhibit();
            using (var reader = _dataContext.GetDataReader("uspGetExhibitByID", sqlParameters))
            {
                if (reader.Read())
                {
                    questionExhibit = new QuestionExhibit
                    {
                        QId = (reader["QID"] as int?) ?? 0,
                        ExhibitTab1 = (reader["ExhibitTab1"] as string) ?? string.Empty,
                        ExhibitTab2 = (reader["ExhibitTab2"] as string) ?? string.Empty,
                        ExhibitTab3 = (reader["ExhibitTab3"] as string) ?? string.Empty,
                        ExhibitTitle1 = (reader["ExhibitTitle1"] as string) ?? string.Empty,
                        ExhibitTitle2 = (reader["ExhibitTitle2"] as string) ?? string.Empty,
                        ExhibitTitle3 = (reader["ExhibitTitle3"] as string) ?? string.Empty
                    };
                }
            }

            return questionExhibit;

            #endregion
        }

        public IEnumerable<Question> GetQuestionsByUserTest(int userTestId)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[1];
            var parameterTestId = new SqlParameter("@UserTestID", SqlDbType.Int, 4) { Value = userTestId };
            sqlParameters[0] = parameterTestId;
            #endregion
            var questions = new List<Question>();
            using (var reader = _dataContext.GetDataReader("uspGetQuestionsByUserTest", sqlParameters))
            {
                while (reader.Read())
                {
                    questions.Add(new Question
                    {
                        Id = (reader["QID"] as int?) ?? 0,
                        QuestionNumber = (reader["QuestionNumber"] as int?) ?? 0,
                        RemediationTime = (reader["TimeSpendForRemedation"] as int?) ?? 0,
                        ExplanationTime = (reader["TimeSpendForExplanation"] as int?) ?? 0,
                        UserTestId = (reader["UserTestID"] as int?) ?? 0,
                        AnswserTrack = (reader["AnswerTrack"] as string) ?? string.Empty,
                        TimeSpendForQuestion = (reader["TimeSpendForQuestion"] as int?) ?? 0,
                        OrderedIndexes = (reader["OrderedIndexes"] as string) ?? string.Empty,
                        Correct = (reader["Correct"] as int?) ?? 0,
                    });
                }
            }

            return questions.ToArray();
        }

        public IEnumerable<UserAnswer> GetUserAnswerByID(int questionId)
        {
            var sqlParameters = new SqlParameter[1];
            var parameterTestId = new SqlParameter("@QuestionID", SqlDbType.Int, 4) { Value = questionId };
            sqlParameters[0] = parameterTestId;

            var userAnswers = new List<UserAnswer>();
            using (var reader = _dataContext.GetDataReader("uspGetUserAnswerByID", sqlParameters))
            {
                while (reader.Read())
                {
                    userAnswers.Add(new UserAnswer
                    {
                        QID = (reader["QID"] as int?) ?? 0,
                        AnswerID = (reader["AnswerID"] as int?) ?? 0,
                        AIndex = (reader["AIndex"] as string) ?? string.Empty,
                        AText = (reader["AText"] as string) ?? string.Empty,
                        Correct = (reader["Correct"] as int?) ?? 0,
                        AnswerConnectID = (reader["AnswerConnectID"] as int?) ?? 0,
                        AType = (reader["AType"] as int?) ?? 0,
                        initialPos = (reader["initialPos"] as int?) ?? 0,
                        Unit = (reader["Unit"] as string) ?? string.Empty,
                        AlternateAText = (reader["AlternateAText"] as string) ?? string.Empty,
                    });
                }
            }

            return userAnswers.ToArray();
        }

        public UserTest CreateTest(int userId, int productId, int programId, int timedTest, int testId)
        {
            var createdTest = new UserTest();
            #region SqlParameters

            var sqlParameters = new SqlParameter[5];
            var parameterUserId = new SqlParameter("@userId", SqlDbType.Int, 4) { Value = userId };
            sqlParameters[0] = parameterUserId;

            var parameterProductId = new SqlParameter("@productId", SqlDbType.Int, 4) { Value = productId };
            sqlParameters[1] = parameterProductId;

            var parameterProgramId = new SqlParameter("@programId", SqlDbType.Int, 4) { Value = programId };
            sqlParameters[2] = parameterProgramId;

            var parameterTimedTest = new SqlParameter("@timedTest", SqlDbType.Int, 4) { Value = timedTest };
            sqlParameters[3] = parameterTimedTest;

            var parameterTestId = new SqlParameter("@testId", SqlDbType.Int, 4) { Value = testId };
            sqlParameters[4] = parameterTestId;

            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspCreateTest", sqlParameters))
            {
                while (reader.Read())
                {
                    createdTest.UserTestId = reader["UserTestID"] as int? ?? 0;
                    createdTest.NumberOfQuestions = reader["NumberOfQuestions"] as int? ?? 0;
                    createdTest.TimeRemaining = reader["TimeRemaining"].ToString() ?? string.Empty;

                    QuestionFileType fileType = (QuestionFileType)Enum.Parse(typeof(QuestionFileType),
                        reader["TypeOfFileID"].ToString() ?? "0");

                    createdTest.Questions = new Dictionary<int, Question>()
                    { 
                        { 1, new Question() { FileType = fileType } }
                    };
                }
            }

            return createdTest;
        }

        public UserTest CreateQBankTest(int userId, int productId, int programId, int timedTest, int testId,
            int tutorMode, int reuseMode, int numberOfQuestions, int correct, string options)
        {
            var createdTest = new UserTest();
            #region SqlParameters

            var sqlParameters = new SqlParameter[10];

            sqlParameters[0] = new SqlParameter("@userId", SqlDbType.Int, 4) { Value = userId };

            sqlParameters[1] = new SqlParameter("@productId", SqlDbType.Int, 4) { Value = productId };

            sqlParameters[2] = new SqlParameter("@programId", SqlDbType.Int, 4) { Value = programId };

            sqlParameters[3] = new SqlParameter("@timedTest", SqlDbType.Int, 4) { Value = timedTest };

            sqlParameters[4] = new SqlParameter("@tutorMode", SqlDbType.Int, 4) { Value = tutorMode };

            sqlParameters[5] = new SqlParameter("@reuseMode", SqlDbType.Int, 4) { Value = reuseMode };

            sqlParameters[6] = new SqlParameter("@numberOfQuestions", SqlDbType.Int, 4) { Value = numberOfQuestions };

            sqlParameters[7] = new SqlParameter("@testId", SqlDbType.Int, 4) { Value = testId };

            sqlParameters[8] = new SqlParameter("@correct", SqlDbType.Int, 4) { Value = correct };

            sqlParameters[9] = new SqlParameter("@options", SqlDbType.VarChar, 500) { Value = options };

            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspCreateQBankTest", sqlParameters))
            {
                while (reader.Read())
                {
                    createdTest.UserTestId = reader["UserTestID"] as int? ?? 0;
                    createdTest.TimeRemaining = reader["TimeRemaining"].ToString() ?? string.Empty;
                }
            }

            return createdTest;
        }

        public bool IsTest74Question(int questionId)
        {
            var sqlParameters = new SqlParameter[1];
            var parameterTestId = new SqlParameter("@QID", SqlDbType.Int, 4) { Value = questionId };
            sqlParameters[0] = parameterTestId;
            bool ret = false;
            DataTable table = _dataContext.GetDataTable("uspGetHotSpotAnswerByID", sqlParameters);
            if (table != null)
            {
                ret = table.Rows.Count > 0;
            }

            return ret;
        }

        public int UpdateUserQuestions(Question obj)
        {
            var sqlUserQuestionsParameters = new SqlParameter[9];

            // build parameters
            var parameterQID = new SqlParameter("@QID", SqlDbType.Int, 4) { Value = obj.Id };
            sqlUserQuestionsParameters[0] = parameterQID;
            var parameterUserTestID = new SqlParameter("@UserTestID", SqlDbType.Int, 4) { Value = obj.UserTestId };
            sqlUserQuestionsParameters[1] = parameterUserTestID;

            var parameterCorrect = new SqlParameter("@Correct", SqlDbType.Int, 4) { Value = obj.Correct };
            sqlUserQuestionsParameters[2] = parameterCorrect;

            var parameterTimeSpendForQuestion = new SqlParameter("@TimeSpendForQuestion", SqlDbType.Int, 4) { Value = obj.TimeSpendForQuestion };
            sqlUserQuestionsParameters[3] = parameterTimeSpendForQuestion;

            var parameterTimeSpendForRemediation = new SqlParameter("@TimeSpendForRemediation", SqlDbType.Int, 4) { Value = obj.RemediationTime };
            sqlUserQuestionsParameters[4] = parameterTimeSpendForRemediation;

            var parameterTimeSpendForExplanation = new SqlParameter("@TimeSpendForExplanation", SqlDbType.Int, 4) { Value = obj.ExplanationTime };
            sqlUserQuestionsParameters[5] = parameterTimeSpendForExplanation;

            var parameterAnswerTrack = new SqlParameter("@AnswerTrack", SqlDbType.VarChar, 50) { Value = obj.AnswserTrack };
            sqlUserQuestionsParameters[6] = parameterAnswerTrack;

            var parameterAnswerChanges = new SqlParameter("@AnswerChanges", SqlDbType.Char, 2) { Value = obj.AnswerChanges };
            sqlUserQuestionsParameters[7] = parameterAnswerChanges;

            var parameterOrderedIndexes = new SqlParameter("@OrderedIndexes", SqlDbType.VarChar, 50) { Value = obj.OrderedIndexes };
            sqlUserQuestionsParameters[8] = parameterOrderedIndexes;

            return _dataContext.ExecuteNonQuery("uspUpdateUserQuestions", sqlUserQuestionsParameters);
        }

        public int CreateUserAnswers(Question obj, IList<UserAnswer> list)
        {
            if (list != null)
            {
                // create inserts based on object
                foreach (var a_obj in list)
                {
                    var sqlCreateUserAnswersParams = new List<SqlParameter>();

                    var parameterQID1 = new SqlParameter("@QID", SqlDbType.Int, 4) { Value = obj.Id };
                    parameterQID1.Value = a_obj.QID;
                    sqlCreateUserAnswersParams.Add(parameterQID1);

                    var parameterUserTestID2 = new SqlParameter("@UserTestID", SqlDbType.Int, 4) { Value = obj.UserTestId };
                    parameterUserTestID2.Value = a_obj.UserTestID;
                    sqlCreateUserAnswersParams.Add(parameterUserTestID2);

                    var parameterAnswerID = new SqlParameter("@AnswerID", SqlDbType.Int, 4) { Value = a_obj.AnswerID };
                    sqlCreateUserAnswersParams.Add(parameterAnswerID);

                    var parameterAText = new SqlParameter("@AText", SqlDbType.VarChar, 2000) { Value = a_obj.AText };
                    sqlCreateUserAnswersParams.Add(parameterAText);

                    var parameterCorrect2 = new SqlParameter("@Correct", SqlDbType.Int, 4) { Value = obj.Correct };
                    parameterCorrect2.Value = a_obj.Correct;
                    sqlCreateUserAnswersParams.Add(parameterCorrect2);

                    var parameterAnswerConnectID = new SqlParameter("@AnswerConnectID", SqlDbType.Int, 4) { Value = a_obj.AnswerConnectID };
                    sqlCreateUserAnswersParams.Add(parameterAnswerConnectID);

                    var parameterAType = new SqlParameter("@AType", SqlDbType.Int, 4) { Value = a_obj.AType };
                    sqlCreateUserAnswersParams.Add(parameterAType);

                    var parameterAIndex = new SqlParameter("@AIndex", SqlDbType.Char, 1) { Value = a_obj.AIndex };
                    sqlCreateUserAnswersParams.Add(parameterAIndex);

                    _dataContext.ExecuteNonQuery("uspCreateUserAnswers", sqlCreateUserAnswersParams.ToArray());
                }
            }

            return 0;
        }

        public int UpdateUserTest(Question obj, UserTest objTest)
        {
            var sqluspUpdateUserTestParams = new List<SqlParameter>();

            var parameterUserTestID3 = new SqlParameter("@UserTestID", SqlDbType.Int, 4) { Value = obj.UserTestId };
            parameterUserTestID3.Value = objTest.UserTestId;
            sqluspUpdateUserTestParams.Add(parameterUserTestID3);

            var parameterSuspendQuestionNumber = new SqlParameter("@SuspendQuestionNumber",
                                                                  SqlDbType.Int, 4) { Value = objTest.SuspendQuestionNumber };
            sqluspUpdateUserTestParams.Add(parameterSuspendQuestionNumber);

            var parameterSuspendQID = new SqlParameter("@SuspendQID", SqlDbType.Int, 4) { Value = objTest.SuspendQID };
            sqluspUpdateUserTestParams.Add(parameterSuspendQID);

            var parameterSuspendType = new SqlParameter("@SuspendType", SqlDbType.Char, 2) { Value = EnumHelper.GetQuestionFileTypeValue(objTest.SuspendType) };
            sqluspUpdateUserTestParams.Add(parameterSuspendType);

            var parameterTimeRemaining = new SqlParameter("@TimeRemaining", SqlDbType.VarChar, 50) { Value = objTest.TimeRemaining };
            sqluspUpdateUserTestParams.Add(parameterTimeRemaining);

            return _dataContext.ExecuteNonQuery("uspUpdateUserTest", sqluspUpdateUserTestParams.ToArray());
        }

        public void UpdateQuestionExplanation(int questionId, int userTestId, int timerValue)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[3];
            var parameterQuestionId = new SqlParameter("@questionId", SqlDbType.Int, 4) { Value = questionId };
            sqlParameters[0] = parameterQuestionId;

            var parameterUserTestId = new SqlParameter("@userTestId", SqlDbType.Int, 4) { Value = userTestId };
            sqlParameters[1] = parameterUserTestId;

            var parameterTimer = new SqlParameter("@timer", SqlDbType.Int, 4) { Value = timerValue };
            sqlParameters[2] = parameterTimer;
            #endregion
            _dataContext.ExecuteNonQuery("uspUpdateQuestionExplanation", sqlParameters);
        }

        public void UpdateQuestionRemediation(int questionId, int userTestId, int timerValue)
        {
            #region Sql Parameters
            var sqlParameters = new SqlParameter[3];
            var parameterQuestionId = new SqlParameter("@questionId", SqlDbType.Int, 4) { Value = questionId };
            sqlParameters[0] = parameterQuestionId;

            var parameterUserTestId = new SqlParameter("@userTestId", SqlDbType.Int, 4) { Value = userTestId };
            sqlParameters[1] = parameterUserTestId;

            var parameterTimer = new SqlParameter("@timer", SqlDbType.Int, 4) { Value = timerValue };
            sqlParameters[2] = parameterTimer;
            #endregion
            _dataContext.ExecuteNonQuery("uspUpdateQuestionRemediation", sqlParameters);
        }

        public UserTest CreateFRQBankTest(int userId, int productId, int programId, int timedTest, int testId, int tutorMode, int reUseMode, int numberOfQuestions, int correctAnswers, string systemIds, string topicIds, string name, int programofstudyId)
        {
            var createdTest = new UserTest();
            #region SqlParameters

            var sqlParameters = new SqlParameter[12];

            sqlParameters[0] = new SqlParameter("@userId", SqlDbType.Int, 4) { Value = userId };

            sqlParameters[1] = new SqlParameter("@productId", SqlDbType.Int, 4) { Value = productId };

            sqlParameters[2] = new SqlParameter("@timedTest", SqlDbType.Int, 4) { Value = timedTest };

            sqlParameters[3] = new SqlParameter("@tutorMode", SqlDbType.Int, 4) { Value = tutorMode };

            sqlParameters[4] = new SqlParameter("@reuseMode", SqlDbType.Int, 4) { Value = reUseMode };

            sqlParameters[5] = new SqlParameter("@numberOfQuestions", SqlDbType.Int, 4) { Value = numberOfQuestions };

            sqlParameters[6] = new SqlParameter("@programId", SqlDbType.Int, 4) { Value = programId };

            sqlParameters[7] = new SqlParameter("@correct", SqlDbType.Int, 4) { Value = correctAnswers };

            sqlParameters[8] = new SqlParameter("@CategoryIds", SqlDbType.VarChar, 8000) { Value = systemIds };

            sqlParameters[9] = new SqlParameter("@TopicIds", SqlDbType.VarChar, 8000) { Value = topicIds };

            sqlParameters[10] = new SqlParameter("@Name", SqlDbType.VarChar, 200) { Value = name };

            sqlParameters[11] = new SqlParameter("@ProgramofStudyId", SqlDbType.Int, 4) { Value = programofstudyId };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspCreateFRQBankTest", sqlParameters))
            {
                while (reader.Read())
                {
                    createdTest.UserTestId = reader["UserTestID"] as int? ?? 0;
                    createdTest.TimeRemaining = reader["TimeRemaining"].ToString() ?? string.Empty;
                    createdTest.TestId = reader["TestId"] as int? ?? 0;
                }
            }

            return createdTest;
        }

        public int GetCFRAvailableQuestions(int userId, string categoryIds, string topicIds, bool isTest, int reUseMode,int programofstudyId)
        {
            var questions = new List<Question>();

            #region SqlParameters

            var sqlParameters = new SqlParameter[5];

            sqlParameters[0] = new SqlParameter("@UserId", SqlDbType.Int, 4) { Value = userId };

            sqlParameters[1] = new SqlParameter("@CategoryIds", SqlDbType.VarChar, 8000) { Value = categoryIds };

            sqlParameters[2] = new SqlParameter("@TopicIds", SqlDbType.VarChar, 8000) { Value = topicIds };

            sqlParameters[3] = new SqlParameter("@ReUseMode", SqlDbType.Int) { Value = reUseMode };

            sqlParameters[4] = new SqlParameter("@ProgramofStudyId", SqlDbType.Int) { Value = programofstudyId };
            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspGetAvailableCFRQuestions", sqlParameters))
            {
                while (reader.Read())
                {
                    questions.Add(new Question
                    {
                        Id = reader["QID"] as int? ?? 0,
                    });
                }
            }

            return questions.Count();
        }

        public UserTest CreateSkillsModuleTest(int userId, int productId, int programId, int timedTest, int tutorMode, int reUseMode, int TestId)
        {
            var createdTest = new UserTest();
            #region SqlParameters

            var sqlParameters = new SqlParameter[7];

            sqlParameters[0] = new SqlParameter("@userId", SqlDbType.Int, 4) { Value = userId };

            sqlParameters[1] = new SqlParameter("@productId", SqlDbType.Int, 4) { Value = productId };

            sqlParameters[2] = new SqlParameter("@timedTest", SqlDbType.Int, 4) { Value = timedTest };

            sqlParameters[3] = new SqlParameter("@tutorMode", SqlDbType.Int, 4) { Value = tutorMode };

            sqlParameters[4] = new SqlParameter("@reuseMode", SqlDbType.Int, 4) { Value = reUseMode };

            sqlParameters[5] = new SqlParameter("@programId", SqlDbType.Int, 4) { Value = programId };

            sqlParameters[6] = new SqlParameter("@SMTestId", SqlDbType.Int, 4) { Value = TestId };

            #endregion

            using (IDataReader reader = _dataContext.GetDataReader("uspCreateSMTest", sqlParameters))
            {
                while (reader.Read())
                {
                    createdTest.UserTestId = reader["UserTestID"] as int? ?? 0;
                    createdTest.TimeRemaining = reader["TimeRemaining"].ToString() ?? string.Empty;
                    createdTest.TestId = reader["TestId"] as int? ?? 0;
                }
            }

            return createdTest;
        }

        private static QuestionFileType GetQuestionFileType(int fileTypeId)
        {
            QuestionFileType fileType = QuestionFileType.Unknown;
            switch (fileTypeId)
            {
                case 0:
                    fileType = QuestionFileType.Unknown;
                    break;
                case 1:
                    fileType = QuestionFileType.Intro;
                    break;
                case 2:
                    fileType = QuestionFileType.TutorialItem;
                    break;
                case 3:
                    fileType = QuestionFileType.Question;
                    break;
                case 4:
                    fileType = QuestionFileType.EndItem;
                    break;
                case 5:
                    fileType = QuestionFileType.Disclaimer;
                    break;
            }

            return fileType;
        }

        private static QuestionPointer GetQuestionPointer(int pointerType)
        {
            switch (pointerType)
            {
                case 0:
                    return QuestionPointer.Current;
                case 1:
                    return QuestionPointer.Previous;
                case 2:
                    return QuestionPointer.Next;
                default:
                    return QuestionPointer.Unknown;
            }
        }

        private static QuestionType GetQuestionType(int questionId)
        {
            switch (questionId)
            {
                case 1:
                    return QuestionType.MultiChoiceSingleAnswer;
                case 2:
                    return QuestionType.MultiChoiceMultiAnswer;
                case 3:
                    return QuestionType.Hotspot;
                case 4:
                    return QuestionType.Number;
                case 5:
                    return QuestionType.Order;
                default:
                    return QuestionType.Unknown;
            }
        }

        #endregion Methods
    }
}