using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Collections;
using System.Data.SqlTypes;

/// <summary>
/// Summary description for TimeZone
/// </summary>
namespace NursingLibrary
{
    public class TimeZone
    {
        public TimeZone()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable GetTimeZones()
        {
            DataSet ds = new DataSet();
            string strSql;

            strSql = " SELECT * FROM TimeZones ORDER BY OrderNumber ";

            SqlConnection dbCon = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            try
            {
                ds = SqlHelper.ExecuteDataset(dbCon, CommandType.Text, strSql);
                return ds.Tables[0];

            }//end try
            catch (Exception ex)
            {
                throw (new Exception("Error in GetTimeZones_cAdmin", ex));
            }//end catch
            finally
            {
                if (dbCon != null) dbCon.Close();//close connection
                if (ds != null) ds.Dispose();//close dataset
            }//end finally

        }//end GetList
        public void PopulateTimeZones(System.Web.UI.WebControls.DropDownList dg)
        {
            dg.DataSource = GetTimeZones();
            dg.DataTextField = "Description";
            dg.DataValueField = "TimeZoneID";
            dg.DataBind();
        }
    }
}