namespace NursingLibrary.DAO
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using NursingLibrary.Utilities;

    public static class NursingDao
    {
        #region Methods

        public static IDataReader GetCaseStudies()
        {
            try
            {
                return Core.GetDataReader("uspGetCaseStudies");
            }
            catch (SqlException sqlException)
            {
                Logger.LogError("GetCaseStudies Error", sqlException);
                Logger.LogDebug(sqlException.StackTrace);
            }
            return null;
        }

        public static string GetQuestionType(int testId)
        {
            #region Sql Parameters
            SqlParameter[] sqlParameters = new SqlParameter[1];
            SqlParameter parameterTestId = new SqlParameter("@testId", SqlDbType.Int, 4);
            parameterTestId.Value = testId;
            sqlParameters[0] = parameterTestId;
            #endregion

            object result = Core.ExecuteScalar("uspGetQuestionType", sqlParameters);
            return result == null ? null : result.ToString();
        }

        public static DataTable GetSuspendedQuestion(int userTestId)
        {
            #region Sql Parameters
            SqlParameter[] sqlParameters = new SqlParameter[1];
            SqlParameter parameterUserTestId = new SqlParameter("@userTestId", SqlDbType.Int, 4);
            parameterUserTestId.Value = userTestId;
            sqlParameters[0] = parameterUserTestId;
            #endregion

            try
            {
                return Core.GetDataTable("uspGetSuspendedQuestion", sqlParameters);
            }
            catch (SqlException sqlException)
            {
                Logger.LogError("GetSuspendedQuestion Error", sqlException);
            }
            return null;
        }

        public static string GetTestName(int testId)
        {
            #region Sql Parameters
            SqlParameter[] sqlParameters = new SqlParameter[1];
            SqlParameter parameterTestId = new SqlParameter("@testId", SqlDbType.Int, 4);
            parameterTestId.Value = testId;
            sqlParameters[0] = parameterTestId;
            #endregion

            object result = Core.ExecuteScalar("uspGetTestName", sqlParameters);
            return result == null ? null : result.ToString();
        }

        public static int GetTestQuestionCount(int testId, int type)
        {
            #region Sql Parameters
            SqlParameter[] sqlParameters = new SqlParameter[2];
            SqlParameter parameterTestId = new SqlParameter("@testId", SqlDbType.Int, 4);
            parameterTestId.Value = testId;
            sqlParameters[0] = parameterTestId;

            SqlParameter parameterType = new SqlParameter("@type", SqlDbType.Int, 4);
            parameterType.Value = type;
            sqlParameters[1] = parameterType;
            #endregion

            object result = Core.ExecuteScalar("uspGetTestQuestionCount", sqlParameters);

            int itemCount;
            return Int32.TryParse(result.ToString(), out itemCount) ? itemCount : 0;
        }

        public static void UpdateQuestionExplanation(int questionId, int userTestId, int timerValue)
        {
            #region Sql Parameters
            SqlParameter[] sqlParameters = new SqlParameter[3];
            SqlParameter parameterQuestionId = new SqlParameter("@questionId", SqlDbType.Int, 4);
            parameterQuestionId.Value = questionId;
            sqlParameters[0] = parameterQuestionId;

            SqlParameter parameterUserTestId = new SqlParameter("@userTestId", SqlDbType.Int, 4);
            parameterUserTestId.Value = userTestId;
            sqlParameters[1] = parameterUserTestId;

            SqlParameter parameterTimer = new SqlParameter("@timer", SqlDbType.Int, 4);
            parameterTimer.Value = timerValue;
            sqlParameters[2] = parameterTimer;
            #endregion

            Core.ExecuteNonQuery("uspUpdateQuestionExplanation", sqlParameters);
        }

        public static void UpdateQuestionRemediation(int questionId, int userTestId, int timerValue)
        {
            #region Sql Parameters
            SqlParameter[] sqlParameters = new SqlParameter[3];
            SqlParameter parameterQuestionId = new SqlParameter("@questionId", SqlDbType.Int, 4);
            parameterQuestionId.Value = questionId;
            sqlParameters[0] = parameterQuestionId;

            SqlParameter parameterUserTestId = new SqlParameter("@userTestId", SqlDbType.Int, 4);
            parameterUserTestId.Value = userTestId;
            sqlParameters[1] = parameterUserTestId;

            SqlParameter parameterTimer = new SqlParameter("@timer", SqlDbType.Int, 4);
            parameterTimer.Value = timerValue;
            sqlParameters[2] = parameterTimer;
            #endregion

            Core.ExecuteNonQuery("uspUpdateQuestionRemediation", sqlParameters);
        }

        public static void UpdateTestStatus(int userTestId, int testStatus)
        {
            #region Sql Parameters
            SqlParameter[] sqlParameters = new SqlParameter[2];
            SqlParameter parameterUserTestId = new SqlParameter("@userTestId", SqlDbType.Int, 4);
            parameterUserTestId.Value = userTestId;
            sqlParameters[0] = parameterUserTestId;

            SqlParameter parameterTestStatus = new SqlParameter("@testStatus", SqlDbType.Int, 4);
            parameterTestStatus.Value = testStatus;
            sqlParameters[1] = parameterTestStatus;
            #endregion

            Core.ExecuteNonQuery("usrUpdateTestStatus", sqlParameters);
        }

        #endregion Methods
    }
}