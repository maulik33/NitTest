using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class ADMIN_ReportCohortByStudent : System.Web.UI.Page
{
    public string sort
    {
        get
        {
            object o = this.ViewState["sort"];
            if (o == null) { return "LastName"; } else { return o.ToString(); };
        }
        set
        {
            this.ViewState["sort"] = value;
        }
    }

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
            if (Session["UserType"].ToString() == "2" || Session["UserType"].ToString() == "3")
            {
                int Inst = Convert.ToInt32(Session["InstitutionID"].ToString());
                new cStudent().PopulateInstitutionByID(ddInstitution, Inst);
            }
            else if (Session["UserType"].ToString() == "5")
            {
                string strInst = Session["InstitutionID"].ToString();
                new cStudent().PopulateInstitutionByID(ddInstitution, strInst);
            }
            else
            {
                new cStudent().PopulateInstitution(ddInstitution);
                ddInstitution.Items.Insert(0, new ListItem("Not Selected", "0"));
            }
            ViewState["InstitutionID"] = Convert.ToInt32(ddInstitution.SelectedValue);



            new cCohort().PopulateCohort(ddCohorts, Convert.ToInt32(ViewState["InstitutionID"].ToString()));
            ddCohorts.Items.Insert(0, new ListItem("Select All", "1"));
            ddCohorts.Items.Insert(0, new ListItem("Not Selected", "0"));

            new cProduct().PopulateProducts(ddProducts);
            ddProducts.Items.Insert(0, new ListItem("Not Selected", "0"));

            new cTest().PopulateTests(ddTests, Convert.ToInt32(ddProducts.SelectedValue));
            ddTests.Items.Insert(0, new ListItem("Not Selected", "0"));

            lblN.Visible = false;

        }

    }
    protected void BindData()
    {
        int empty;
        if (ddCohorts.SelectedValue == "0")
        {
            empty = 1;
        }
        else
        {
            empty = 0;
        }


        if (empty == 0)
        {
            DataTable dt = new cCohort().GetRezultsByCohortStudent(Convert.ToInt32(ViewState["InstitutionID"].ToString()), Convert.ToInt32(ddProducts.SelectedValue), Convert.ToInt32(ddTests.SelectedValue), Convert.ToInt32(ddCohorts.SelectedValue));
            DataView dv = new DataView(dt);


            dt.DefaultView.Sort = this.sort;
            gvCohorts.DataSource = dt.DefaultView;
            gvCohorts.DataBind();
            lblN.Visible = true;
            lblStudentNumber.Text = gvCohorts.Rows.Count.ToString();

            if (dt.Rows.Count == 0)
            {
                lblM.Visible = true;
            }
            else
            {
                lblM.Visible = false;
            }
        }
        else
        {
            gvCohorts.DataSource = null;
            gvCohorts.DataBind();
        }

    }
    protected void ddInstitution_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["InstitutionID"] = Convert.ToInt32(ddInstitution.SelectedValue);

        new cCohort().PopulateCohort(ddCohorts, Convert.ToInt32(ViewState["InstitutionID"].ToString()));
        ddCohorts.Items.Insert(0, new ListItem("Select All", "1"));
        ddCohorts.Items.Insert(0, new ListItem("Not Selected", "0"));
        ddProducts.SelectedIndex = 0;
        ddTests.Items.Clear();
        ddTests.Items.Insert(0, new ListItem("Not Selected", "0"));
    }

    protected void ddProducts_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddCohorts.SelectedValue == "1")
        {
            new cTest().PopulateTestsByProductAndInsitution(ddTests, Convert.ToInt32(ddInstitution.SelectedValue), Convert.ToInt32(ddProducts.SelectedValue));
        }
        else
        {
            new cTest().PopulateTestsByCohortIDAndProductID(ddTests, Convert.ToInt32(ddCohorts.SelectedValue), Convert.ToInt32(ddProducts.SelectedValue));
        }
        ddTests.Items.Insert(0, new ListItem("Not Selected", "0"));
        BindData();
    }
    protected void ddTests_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void gvCohorts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!e.CommandName.Equals("Sort") && !e.CommandName.Equals("Page"))
        {

            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvCohorts.Rows[index];

            int id = Convert.ToInt32(gvCohorts.DataKeys[row.RowIndex].Values["UserID"].ToString());//zyh

            int testID = Convert.ToInt32(ddTests.SelectedValue);

            string UserTestID = "";

            if (testID != 0)
            {
                //UserTestID = new cRN().getUserTestIDByTestID(id, testID).ToString();
            }

            string testName = string.Empty;
            if (ddTests.SelectedItem != null)
            {
                testName = ddTests.SelectedItem.Text;
            }

            switch (e.CommandName)
            {

                case "Performance":

                    Response.Redirect("ReportTestStudent.aspx?id=" + id + "&IID=" + ViewState["InstitutionID"].ToString() + "&ProductID=" + ddProducts.SelectedValue + "&UserTestID=" + UserTestID + "&TestName=" + testName);//zyh
                    break;

                case "Questions":

                    Response.Redirect("ReportStudentQuestion.aspx?id=" + id + "&IID=" + ViewState["InstitutionID"].ToString() + "&ProductID=" + ddProducts.SelectedValue + "&TestID=" + UserTestID);
                    break;


            }
        }
    }
    protected void ddCohorts_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddCohorts.SelectedValue == "1")
        {
            new cTest().PopulateTestsByProductAndInsitution(ddTests, Convert.ToInt32(ddInstitution.SelectedValue), Convert.ToInt32(ddProducts.SelectedValue));
        }
        else
        {
            new cTest().PopulateTestsByCohortIDAndProductID(ddTests, Convert.ToInt32(ddCohorts.SelectedValue), Convert.ToInt32(ddProducts.SelectedValue));
        }
        ddTests.Items.Insert(0, new ListItem("Not Selected", "0"));
        BindData();
    }
    protected void gvCohorts_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (this.sort == e.SortExpression)
        {
            this.sort += " DESC";
        }
        else
        {
            this.sort = e.SortExpression;
        }
        this.BindData();
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("P_CohortByStudent.aspx?Institution=" + this.ddInstitution.SelectedValue + "&Cohort=" + ddCohorts.SelectedValue + "&TestType=" + ddProducts.SelectedValue + "&TestName=" + ddTests.SelectedValue + "&Sort=" + this.sort);
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("P_CohortByStudent.aspx?Institution=" + this.ddInstitution.SelectedValue + "&Cohort=" + ddCohorts.SelectedValue + "&TestType=" + ddProducts.SelectedValue + "&TestName=" + ddTests.SelectedValue + "&Sort=" + this.sort + "&act=" + (int)SV.PrintActions.PDFPrint);

    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("P_CohortByStudent.aspx?Institution=" + this.ddInstitution.SelectedValue + "&Cohort=" + ddCohorts.SelectedValue + "&TestType=" + ddProducts.SelectedValue + "&TestName=" + ddTests.SelectedValue + "&Sort=" + this.sort + "&act=" + (int)SV.PrintActions.ExportExcel);
    }
    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("P_CohortByStudent.aspx?Institution=" + this.ddInstitution.SelectedValue + "&Cohort=" + ddCohorts.SelectedValue + "&TestType=" + ddProducts.SelectedValue + "&TestName=" + ddTests.SelectedValue + "&Sort=" + this.sort + "&act=" + (int)SV.PrintActions.ExportExcelDataOnly);
    }
}
