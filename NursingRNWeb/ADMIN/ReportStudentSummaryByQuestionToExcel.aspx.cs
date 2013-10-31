using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using NursingLibrary;

public partial class ReportStudentSummaryByQuestionToExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string cohortID = Request.QueryString["cohortID"];
        int productID = Convert.ToInt32(Request.QueryString["productID"]);
        int testID = Convert.ToInt32(Request.QueryString["testID"]);

        cReport report = new cReport();
        DataTable dt = report.TransferTable(report.getStudentSummaryByQuestion(cohortID, productID, testID), report.getStudentSummaryHeader(cohortID, productID, testID));

        Response.Clear();
        Response.ContentType = "application/vnd.ms-excel";

        string sep = "";
        Response.Write(sep + " Student Summary By Question report\t\t\t\t\t\t (N= " + dt.Rows.Count + " students)\n");
        foreach (DataColumn dc in dt.Columns)
        {
            string aa = sep + dc.ColumnName;
            Response.Write(sep + dc.ColumnName);
            sep = "\t";
        }
        Response.Write("\n");

        int i;
        foreach (DataRow dr in dt.Rows)
        {
            sep = "";
            for (i = 0; i < dt.Columns.Count; i++)
            {
                string bb = sep + dr[i].ToString();
                Response.Write(sep + dr[i].ToString());
                sep = "\t";
            }
            Response.Write("\n");
        }

    }
    
}
