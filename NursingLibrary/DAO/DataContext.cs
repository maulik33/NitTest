using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using NursingLibrary.Interfaces;

namespace NursingLibrary.DAO
{
    public class DataContext : IDataContext
    {
        #region Fields

        private readonly SqlConnection dbConnection;
        private bool _disposed;

        #endregion Fields

        #region Constructors

        public DataContext(string connectionString)
        {
            dbConnection = new SqlConnection(connectionString);
        }

        #endregion Constructors

        #region Properties

        public bool IsInTransaction
        {
            get { return DbTransaction != null; }
        }

        public IDbConnection Connection
        {
            get
            {
                if (dbConnection.State != ConnectionState.Open)
                {
                    dbConnection.Open();
                }

                return dbConnection;
            }
        }

        public IDbTransaction DbTransaction { private get; set; }

        #endregion Properties

        #region Methods

        public void Dispose()
        {
            if (!_disposed)
            {
                Dispose(true);
                GC.SuppressFinalize(this);
                _disposed = true;
            }
        }

        public int ExecuteNonQuery(string procName, params DbParameter[] parameters)
        {
            using (var cmd = new SqlCommand(procName, Connection as SqlConnection))
            {
                if (IsInTransaction)
                {
                    cmd.Transaction = DbTransaction as SqlTransaction;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter param in parameters)
                {
                    cmd.Parameters.Add(param);
                }
                
                return cmd.ExecuteNonQuery();
            }
        }

        public object ExecuteScalar(string procName, params DbParameter[] parameters)
        {
            using (var cmd = new SqlCommand(procName, Connection as SqlConnection))
            {
                if (IsInTransaction)
                {
                    cmd.Transaction = DbTransaction as SqlTransaction;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter param in parameters)
                {
                    cmd.Parameters.Add(param);
                }
                
                return cmd.ExecuteScalar();
            }
        }

        public int ExecuteStoredProcedure(string procName, params DbParameter[] parameters)
        {
            using (var cmd = new SqlCommand(procName, Connection as SqlConnection))
            {
                if (IsInTransaction)
                {
                    cmd.Transaction = DbTransaction as SqlTransaction;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter param in parameters)
                {
                    cmd.Parameters.Add(param);
                }

                return cmd.ExecuteNonQuery();
            }
        }

        public IDataReader GetDataReader(string procName, params DbParameter[] parameters)
        {
            using (var cmd = new SqlCommand(procName, Connection as SqlConnection))
            {
                if (IsInTransaction)
                {
                    cmd.Transaction = DbTransaction as SqlTransaction;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter param in parameters)
                {
                    cmd.Parameters.Add(param);
                }
                
                return cmd.ExecuteReader();
            }
        }

        public DataTable GetDataTable(string procName, params DbParameter[] parameters)
        {
            using (var cmd = new SqlCommand(procName, Connection as SqlConnection))
            {
                if (IsInTransaction)
                {
                    cmd.Transaction = DbTransaction as SqlTransaction;
                }

                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    foreach (SqlParameter param in parameters)
                    {
                        cmd.Parameters.Add(param);
                    }
                }
                
                using (var dataAdapter = new SqlDataAdapter(cmd))
                {
                    var dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    return dataSet.Tables.Count > 0 ? dataSet.Tables[0] : null;
                }
            }
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dbConnection != null)
                {
                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }
        }

        #endregion Methods
    }
}