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

public partial class ADMIN_Graph1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null || Session["UserID"].ToString().Trim().Equals(""))
            Response.Redirect("../A_login.aspx");

        Response.Expires = 0;
        Response.ContentType = "text/xml";

        int CohortID;
        int ProductID;
        int TestID;
        int AType;

        if (Request.QueryString["AType"].ToString().Equals(""))
        {
            AType = 1;
        }
        else
        {
            AType = Convert.ToInt32(Request.QueryString["AType"].ToString());
        }

        if (Request.QueryString["CohortID"].ToString().Equals(""))
        {
            CohortID = 0;
        }
        else
        {
            CohortID = Convert.ToInt32(Request.QueryString["CohortID"].ToString());
        }
        if (Request.QueryString["ProductID"].ToString().Equals(""))
        {
            ProductID = 0;
        }
        else
        {
            ProductID = Convert.ToInt32(Request.QueryString["ProductID"].ToString());
        }
        if (Request.QueryString["TestID"].ToString().Equals(""))
        {
            TestID = 0;
        }
        else
        {
            TestID = Convert.ToInt32(Request.QueryString["TestID"].ToString());
        }

        Response.Write(new cXML().GetStudentTestXML(CohortID, ProductID, TestID, AType));

    }


    
}
