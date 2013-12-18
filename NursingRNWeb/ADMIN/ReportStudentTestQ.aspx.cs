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
using System.Drawing;
using NursingLibrary;

public partial class ADMIN_ReportStudentTestQ : System.Web.UI.Page
{
    public string strDataURL1;

    public ArrayList list;
    public int SID;
    public int offset;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null || Session["UserID"].ToString().Trim().Equals(""))
            Response.Redirect("../A_login.aspx");

        if (Session["InstitutionID"] != null)
        {
            if (Session["InstitutionID"].ToString().Trim().Equals(""))
            {
                Response.Redirect("../A_login.aspx");
            }
        }
        else
        {
            Response.Redirect("../A_login.aspx");
        }

        if (!IsPostBack)
        {
            SID = Convert.ToInt32(Request.QueryString["id"]);
            ViewState["SID"] = SID;

            lblName.Text = new cStudent().GetUserName(SID);

            string ProductID = "";
            string TestID = "";
            ProductID = Request.QueryString["ProductID"];
            TestID = Request.QueryString["TestID"];

            new cProduct().PopulateProducts(ddProducts);
            ddProducts.Items.Insert(0, new ListItem("All", "0"));
            if (ProductID.Trim() != "")
            {
                ddProducts.SelectedValue = ProductID.ToString();
            }

            new cTest().PopulateTests(ddTests, Convert.ToInt32(ddProducts.SelectedValue));
            //new cTest().PopulateTestsByUser(ddTests, Convert.ToInt32(ddProducts.SelectedValue), Convert.ToInt32(Session["UserID"].ToString()));
            ddTests.Items.Insert(0, new ListItem("Not Selected", "0"));
            if (TestID.Trim() != "")
            {
                ddTests.SelectedValue = TestID.ToString();
            }
            BindDate();

        }
    }
    protected void ddProducts_SelectedIndexChanged(object sender, EventArgs e)
    {
        offset = new cStudent().ReturnTimeZoneOffSet(Convert.ToInt32(ViewState["SID"].ToString()));
        new cTest().PopulateTestsByUser(ddTests, Convert.ToInt32(ddProducts.SelectedValue), Convert.ToInt32(ViewState["SID"].ToString()), offset);
        ddTests.Items.Insert(0, new ListItem("Not Selected", "0"));
        BindDate();
    }
    protected void ddTests_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindDate();

    }
    protected void BindDate()
    {

        if (ddTests.SelectedValue != "0")
        {
            Session["TestID"] = new cTest().GetTestIDByUserTestID(Convert.ToInt32(ddTests.SelectedValue));

            ArrayList list = new ArrayList();
            list = new cTest().GetTestCaracteristics(Convert.ToInt32(Session["TestID"].ToString()), "S");

            if (ddProducts.SelectedValue == "1")
            {
                FillGrid_I(list, "I");
            }
            if (ddProducts.SelectedValue == "3")
            {
                FillGrid_I(list, "F");
            }
            if (ddProducts.SelectedValue == "4")
            {
                FillGrid_I(list, "N");
            }
        }
        else
        {
            gvIntegrated.DataSource = null;
            gvIntegrated.DataBind();

        }






    }
    protected void FillGrid_I(ArrayList list, string p_type)
    {
        DataTable dt = new cTest().GetListOfItemsForTestForTheUser_I_New(Convert.ToInt32(ddTests.SelectedValue), Convert.ToInt32(Session["TestID"].ToString()), "03", list);

        gvIntegrated.RowDataBound += new GridViewRowEventHandler(gvIntegrated_RowDataBound);
        gvIntegrated.RowCommand += new GridViewCommandEventHandler(gvIntegrated_RowCommand);




        gvIntegrated.AutoGenerateColumns = false;
        gvIntegrated.Columns.Clear();


        BoundField QuestionIDConceptBoundField = new BoundField();
        QuestionIDConceptBoundField.DataField = "QuestionID";
        QuestionIDConceptBoundField.HeaderText = "Q.ID";
        gvIntegrated.Columns.Add(QuestionIDConceptBoundField);

        //QuestionIDConceptBoundField.Visible = false;


        BoundField tc1 = new BoundField();
        tc1.DataField = "Correct";
        tc1.HeaderText = "Correct";
        gvIntegrated.Columns.Add(tc1);





        foreach (string item in list)
        {
            if (item.Trim() == "ClientNeeds")
            {
                BoundField ClientNeedsBoundField = new BoundField();
                ClientNeedsBoundField.DataField = "ClientNeeds";
                ClientNeedsBoundField.HeaderText = "ClientNeeds";
                gvIntegrated.Columns.Add(ClientNeedsBoundField);
            }
            if (item.Trim() == "ClientNeedCategory")
            {

                BoundField ClientNeedCategoryBoundField = new BoundField();
                ClientNeedCategoryBoundField.DataField = "ClientNeedCategory";
                ClientNeedCategoryBoundField.HeaderText = "ClientNeedCategory";
                gvIntegrated.Columns.Add(ClientNeedCategoryBoundField);
            }
            if (item.Trim() == "Demographic")
            {
                BoundField DemographicBoundField = new BoundField();
                DemographicBoundField.DataField = "Demographic";
                DemographicBoundField.HeaderText = "Demographic";
                gvIntegrated.Columns.Add(DemographicBoundField);
            }
            if (item.Trim() == "ClinicalConcept")
            {
                BoundField ClinicalConceptBoundField = new BoundField();
                ClinicalConceptBoundField.DataField = "ClinicalConcept";
                ClinicalConceptBoundField.HeaderText = "Clinical Concept";
                gvIntegrated.Columns.Add(ClinicalConceptBoundField);
            }
            if (item.Trim() == "NursingProcess")
            {
                BoundField NursingProcessBoundField = new BoundField();
                NursingProcessBoundField.DataField = "NursingProcess";
                NursingProcessBoundField.HeaderText = "Nursing Process";
                gvIntegrated.Columns.Add(NursingProcessBoundField);

            }
            if (item.Trim() == "LevelOfDifficulty")
            {
                BoundField LevelOfDifficultyBoundField = new BoundField();
                LevelOfDifficultyBoundField.DataField = "LevelOfDifficulty";
                LevelOfDifficultyBoundField.HeaderText = "Level Of Difficulty";
                gvIntegrated.Columns.Add(LevelOfDifficultyBoundField);

            }
            if (item.Trim() == "CriticalThinking")
            {
                BoundField CriticalThinkingBoundField = new BoundField();
                CriticalThinkingBoundField.DataField = "CriticalThinking";
                CriticalThinkingBoundField.HeaderText = "Critical Thinking";
                gvIntegrated.Columns.Add(CriticalThinkingBoundField);

            }
            if (item.Trim() == "CognitiveLevel")
            {
                BoundField CognitiveLevelBoundField = new BoundField();
                CognitiveLevelBoundField.DataField = "CognitiveLevel";
                CognitiveLevelBoundField.HeaderText = "Cognitive Level";
                gvIntegrated.Columns.Add(CognitiveLevelBoundField);

            }
            if (item.Trim() == "SpecialtyArea")
            {
                BoundField SpecialtyAreaBoundField = new BoundField();
                SpecialtyAreaBoundField.DataField = "SpecialtyArea";
                SpecialtyAreaBoundField.HeaderText = "Specialty Area";
                gvIntegrated.Columns.Add(SpecialtyAreaBoundField);

            }
            if (item.Trim() == "Systems")
            {
                BoundField SystemsBoundField = new BoundField();
                SystemsBoundField.DataField = "System";
                SystemsBoundField.HeaderText = "System";
                gvIntegrated.Columns.Add(SystemsBoundField);

            }

        }


        BoundField tc2 = new BoundField();
        if (p_type == "I")
        {
            tc2.DataField = "TopicTitle";
            tc2.HeaderText = "Remediation";
            gvIntegrated.Columns.Add(tc2);

        }
        else
        {
            tc2.DataField = "TopicTitle";
            tc2.HeaderText = "Remediation";
            gvIntegrated.Columns.Add(tc2);

        }

        if (p_type == "I")
        {

            BoundField SecondsUsedBoundField = new BoundField();
            SecondsUsedBoundField.DataField = "TimeSpendForQuestion";
            SecondsUsedBoundField.HeaderText = "Time Remediated";
            gvIntegrated.Columns.Add(SecondsUsedBoundField);

        }

        else
        {
            BoundField SecondsUsedBoundField = new BoundField();
            SecondsUsedBoundField.DataField = "TimeSpendForRemedation";
            SecondsUsedBoundField.HeaderText = "Seconds Used";
            gvIntegrated.Columns.Add(SecondsUsedBoundField);
        }


        gvIntegrated.DataSource = dt;
        gvIntegrated.DataBind();


    }
    protected void gvIntegrated_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gv = (GridView)sender;
            int rowIndex = Convert.ToInt32(e.Row.RowIndex);
            string QID = gv.DataKeys[rowIndex].Values["QID"].ToString();
            string TopicTitle = gv.DataKeys[rowIndex].Values["TopicTitle"].ToString();
            string correct = gv.DataKeys[rowIndex].Values["Correct"].ToString();

            if (correct.Trim() == "1")
            {
                e.Row.Cells[1].Text = "y";
            }
            else
            {
                e.Row.Cells[1].Text = "n";
            }
        }
    }
    protected void gvIntegrated_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void ddCohort_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindDate();

    }
    protected void btn_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReportStudentQuestion.aspx?id=" + ViewState["SID"].ToString() + "&TestID=" + ddTests.SelectedValue + "&ProductID=" + ddProducts.SelectedValue);
    }
    protected string ReturnDiv(int i)
    {
        string result = "";
        if (i % 3 == 1)
        {
            result = "<img src=\"../Temp/images/barv1.gif\" width=\"16\" height=\"18\"> %Correct &nbsp;&nbsp;&nbsp;<img src=\"../Temp/images/icon_corr.gif\" width=\"22\" height=\"16\"> Correct  &nbsp;&nbsp;&nbsp;&nbsp;<B>n=</B> Total";
        }
        if (i % 3 == 2)
        {
            result = "<img src=\"../Temp/images/barv2.gif\" width=\"16\" height=\"18\"> %Correct &nbsp;&nbsp;&nbsp;<img src=\"../Temp/images/icon_corr.gif\" width=\"22\" height=\"16\"> Correct  &nbsp;&nbsp;&nbsp;&nbsp;<B>n=</B> Total";

        }
        if (i % 3 == 0)
        {
            result = "<img src=\"../Temp/images/barv3.gif\" width=\"16\" height=\"18\"> %Correct &nbsp;&nbsp;&nbsp;<img src=\"../Temp/images/icon_corr.gif\" width=\"22\" height=\"16\"> Correct  &nbsp;&nbsp;&nbsp;&nbsp;<B>n=</B> Total";
        }
        return result;
    }
    protected string ReturnColor(int i)
    {
        string result = "";
        if (i % 3 == 1)
        {
            result = "#CC99FF";
        }
        if (i % 3 == 2)
        {
            result = "#99CCFF";

        }
        if (i % 3 == 0)
        {
            result = "#F7DBC0";
        }
        return result;
    }
    protected string ReturnSeconDiv(int percentage, int i)
    {
        string result = "";
        if (i % 3 == 1)
        {
            result = "<img src=\"../Temp/images/barv1.gif\" width=\"" + percentage + "\" height=\"16\">";
        }
        if (i % 3 == 2)
        {
            result = "<img src=\"../Temp/images/barv2.gif\" width=\"" + percentage + "\" height=\"16\">";

        }
        if (i % 3 == 0)
        {
            result = "<img src=\"../Temp/images/barv3.gif\" width=\"" + percentage + "\" height=\"16\">";
        }
        return result;
    }
}
