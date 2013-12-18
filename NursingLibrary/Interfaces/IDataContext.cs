using System;
using System.Data;
using System.Data.Common;

namespace NursingLibrary.Interfaces
{
    public interface IDataContext : IDisposable
    {
        IDbConnection Connection { get; }
        
        IDbTransaction DbTransaction { set; }
        
        int ExecuteNonQuery(string procName, params DbParameter[] parameters);
        
        int ExecuteStoredProcedure(string procName, params DbParameter[] parameters);
        
        object ExecuteScalar(string procName, params DbParameter[] parameters);
        
        IDataReader GetDataReader(string procName, params DbParameter[] parameters);
        
        DataTable GetDataTable(string procName, params DbParameter[] parameters);
    }
}
