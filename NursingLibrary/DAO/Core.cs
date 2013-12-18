namespace NursingLibrary.DAO
{
    using System.Data;
    using System.Data.SqlClient;

    using NursingLibrary.Utilities;

    public static class Core
    {
        #region Fields

        public static string ConStr;

        #endregion Fields

        #region Constructors

        static Core()
        {
            ConStr = ConnectionString;
        }

        #endregion Constructors

        #region Properties

        public static string ConnectionString
        {
            get
            {
                if (!string.IsNullOrEmpty(ConStr))
                    return ConStr;

                ConStr = ConfigMgr.GetConnectionStringValue("Nursing");
                if (string.IsNullOrEmpty(ConStr))
                {
                    Logger.LogInfo("ConnectionString Property: Empty");
                    return null;
                }
                return ConStr;
            }
        }

        #endregion Properties

        #region Methods

        public static int ExecuteNonQuery(string procName, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(ConStr))
            {
                using (SqlCommand cmd = new SqlCommand(procName, conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (SqlParameter param in parameters)
                            cmd.Parameters.Add(param);

                        conn.Open();
                        return cmd.ExecuteNonQuery();
                    }
                    catch (SqlException sqlException)
                    {
                        Logger.LogError("ExecuteNonQuery Error", sqlException);
                        Logger.LogDebug(sqlException.StackTrace);
                        throw;
                    }
                }
            }
        }

        public static object ExecuteScalar(string procName, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(ConStr))
            {
                using (SqlCommand cmd = new SqlCommand(procName, conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (SqlParameter param in parameters)
                            cmd.Parameters.Add(param);
                        conn.Open();
                        return cmd.ExecuteScalar();
                    }
                    catch (SqlException sqlException)
                    {
                        Logger.LogError("ExecuteScalar Error", sqlException);
                        Logger.LogDebug(sqlException.StackTrace);
                        throw;
                    }
                }
            }
        }

        public static int ExecuteStoredProcedure(string procName, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(ConStr))
            {
                using (SqlCommand cmd = new SqlCommand(procName, conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (SqlParameter param in parameters)
                            cmd.Parameters.Add(param);

                        return cmd.ExecuteNonQuery();
                    }
                    catch (SqlException sqlException)
                    {
                        Logger.LogError("ExecuteStoredProcedure Error", sqlException);
                        Logger.LogDebug(sqlException.StackTrace);
                        throw;
                    }
                }
            }
        }

        public static SqlDataReader GetDataReader(string procName, params SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(ConStr);
            SqlCommand cmd = new SqlCommand(procName, conn);

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter param in parameters)
                    cmd.Parameters.Add(param);

                conn.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (SqlException sqlException)
            {
                conn.Close();
                Logger.LogError("GetDataReader Error", sqlException);
                Logger.LogDebug(sqlException.StackTrace);
                throw;
            }
        }

        public static DataTable GetDataTable(string procName, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(ConStr))
            {
                using (SqlCommand cmd = new SqlCommand(procName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if(parameters != null)
                        foreach (SqlParameter param in parameters)
                            cmd.Parameters.Add(param);

                    try
                    {
                        conn.Open();
                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                        {
                            DataSet dataSet = new DataSet();
                            dataAdapter.Fill(dataSet);
                            return dataSet.Tables.Count > 0 ? dataSet.Tables[0] : null;
                        }
                    }
                    catch (SqlException sqlException)
                    {
                        Logger.LogError("GetDataTable Error", sqlException);
                        Logger.LogDebug(sqlException.StackTrace);
                    }
                }
            }
            return null;
        }

        #endregion Methods
    }

    public static class SqlDataReaderExtensions
    {
        #region Methods

        //public static int GetOrdinalOrThrow(this IDataReader reader, string columnName)
        //{
        //    try
        //    {
        //        return reader.GetOrdinal(columnName);
        //    }
        //    catch (IndexOutOfRangeException)
        //    {
        //        throw new LoginException(Business.Core.LoginFailure.UserDetailsNotComplete,
        //                                 string.Format("Expected column '{0}' not found", columnName));
        //    }
        //}

        #endregion Methods
    }
}