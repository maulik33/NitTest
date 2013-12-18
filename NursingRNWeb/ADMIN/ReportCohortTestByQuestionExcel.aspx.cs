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

public partial class ADMIN_ReportCohortTestByQuestionExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int InstitutionID = Convert.ToInt32(Request.QueryString["InstitutionID"]);
        int productID = Convert.ToInt32(Request.QueryString["productID"]);
        int testID = Convert.ToInt32(Request.QueryString["testID"]);
        DropDownList ddCohorts = new DropDownList();
        new cCohort().PopulateCohort(ddCohorts, InstitutionID);
        ddCohorts.Items.Insert(0, new ListItem("Select All", "1"));
        ddCohorts.Items.Insert(0, new ListItem("Not Selected", "0"));

        DataTable dt = new cCohort().GetRezultsByAllCohortQuestions(InstitutionID, productID, testID, ddCohorts);

        dt.DefaultView.Sort = "TopicTitle";
        DataTable dtSort = dt.DefaultView.ToTable();

        Response.Clear();
        Response.ContentType = "application/vnd.ms-excel";

        string sep = "";
        Response.Write(sep + " Test Rsult By Question report\t\t\t\t\t\n");
        foreach (DataColumn dc in dtSort.Columns)
        {
            if (dc.ColumnName != "QuestionID")
            {
                Response.Write(sep + dc.ColumnName);
                sep = "\t";
            }
        }
        Response.Write("\n");

        int i;
        foreach (DataRow dr in dtSort.Rows)
        {
            sep = "";
            for (i = 1; i < dtSort.Columns.Count; i++)
            {
                //     string bb = sep + dr[i].ToString();
                Response.Write(sep + dr[i].ToString());
                sep = "\t";
            }
            Response.Write("\n");
        }

    }
}
