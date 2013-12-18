using System;
using System.Text;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class ADMIN_ReportBycohort : System.Web.UI.Page
{
    public string sort
    {
        get
        {
            object o = this.ViewState["sort"];
            if (o == null) { return "CohortName"; } else { return o.ToString(); };
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
            ViewState["TestID"] = "";

            new cProduct().PopulateProducts(ddProducts);
            ddProducts.Items.Insert(0, new ListItem("Not Selected", "0"));

            //new cTest().PopulateTests(ddTests, Convert.ToInt32(ddProducts.SelectedValue));
            if (Convert.ToInt32(ddInstitution.SelectedValue) != 0 && Convert.ToInt32(ddProducts.SelectedValue) != 0)
            {
                new cTest().PopulateTestsByProductAndInsitution(lbTests, Convert.ToInt32(ddInstitution.SelectedValue), Convert.ToInt32(ddProducts.SelectedValue));
            }
            //    ddTests.Items.Insert(0, new ListItem("Not Selected", "0"));



        }

    }
    protected void BindData()
    {
        int empty;
        if (ddProducts.SelectedValue == "0")
        {
            empty = 1;
        }
        else
        {
            empty = 0;
        }

        if (empty == 0)
        {
            DataTable dt = new cCohort().GetRezultsByCohort(Convert.ToInt32(ViewState["InstitutionID"].ToString()), Convert.ToInt32(ddProducts.SelectedValue), ViewState["TestID"].ToString());

            dt.DefaultView.Sort = this.sort;
            gvCohorts.DataSource = dt.DefaultView;
            gvCohorts.DataBind();

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
        if (Convert.ToInt32(ddInstitution.SelectedValue) != 0)
        {
            /*
            //PopulateProducts List By Institution ID__Min
            new cProduct().PopulateProductsByInstitutionID(ddProducts, ddInstitution.SelectedValue);
             * */
            if (Convert.ToInt32(ddProducts.SelectedValue) == 0)
            {
                // Populate Test By Institution _Min
                new cTest().PopulateTestsByInsitution(lbTests, Convert.ToInt32(ddInstitution.SelectedValue));
                //   ddTests.Items.Insert(0, new ListItem("Not Selected", "0"));
            }
            else
            {
                new cTest().PopulateTestsByProductAndInsitution(lbTests, Convert.ToInt32(ddInstitution.SelectedValue), Convert.ToInt32(ddProducts.SelectedValue));
                //   ddTests.Items.Insert(0, new ListItem("Not Selected", "0"));
            }

        }
        else
        {
            //PopulateAllProducts List __Min
            new cProduct().PopulateProducts(ddProducts);
            if (Convert.ToInt32(ddProducts.SelectedValue) != 0)
            {
                new cTest().PopulateTests(lbTests, Convert.ToInt32(ddProducts.SelectedValue));
                //   ddTests.Items.Insert(0, new ListItem("Not Selected", "0"));
            }
            else
            {
                lbTests.Items.Clear();
                //    ddTests.Items.Insert(0, new ListItem("Not Selected", "0"));
            }
        }
        ViewState["InstitutionID"] = Convert.ToInt32(ddInstitution.SelectedValue);
        BindData();

    }
    protected void ddProducts_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (Convert.ToInt32(ddProducts.SelectedValue) == 0)
        {
            if (Convert.ToInt32(ddInstitution.SelectedValue) != 0)
            {
                new cTest().PopulateTestsByInsitution(lbTests, Convert.ToInt32(ddInstitution.SelectedValue));
                //    ddTests.Items.Insert(0, new ListItem("Not Selected", "0"));
            }
            else
            {
                lbTests.Items.Clear();
                //ddTests.Items.Insert(0, new ListItem("Not Selected", "0"));
            }
        }
        else
        {
            if (Convert.ToInt32(ddInstitution.SelectedValue) == 0)
            {
                new cTest().PopulateTests(lbTests, Convert.ToInt32(ddProducts.SelectedValue));
            }
            else
            {
                new cTest().PopulateTestsByProductAndInsitution(lbTests, Convert.ToInt32(ddInstitution.SelectedValue), Convert.ToInt32(ddProducts.SelectedValue));
                //  ddTests.Items.Insert(0, new ListItem("Not Selected", "0"));
            }
        }
        BindData();
    }

    protected void ddTests_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void gvCohorts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!e.CommandName.Equals("Sort") && !e.CommandName.Equals("Page"))
        {

            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvCohorts.Rows[index];

            int id = Convert.ToInt32(gvCohorts.DataKeys[row.RowIndex].Values["CohortID"].ToString());



            switch (e.CommandName)
            {

                case "Performance":

                    Response.Redirect("ReportCohortPerformance.aspx?id=" + id + "&IID=" + ViewState["InstitutionID"].ToString() + "&ProductID=" + ddProducts.SelectedValue + "&TestID=0");
                    break;



            }
        }
    }
    protected void gvCohorts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Text = e.Row.Cells[2].Text + "%";
        }
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

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("P_ByCohot.aspx?Institution=" + this.ddInstitution.SelectedValue + "&TestType=" + ddProducts.SelectedValue + "&TestName=" + ViewState["TestID"].ToString() + "&Sort=" + sort + "&act=" + (int)SV.PrintActions.ShowPreview);
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("P_ByCohot.aspx?Institution=" + this.ddInstitution.SelectedValue + "&TestType=" + ddProducts.SelectedValue + "&TestName=" + ViewState["TestID"].ToString() + "&Sort=" + sort + "&act=" + (int)SV.PrintActions.DirectPrint);

    }
    protected void ImageButton1_Click1(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("P_ByCohot.aspx?Institution=" + this.ddInstitution.SelectedValue + "&TestType=" + ddProducts.SelectedValue + "&TestName=" + ViewState["TestID"].ToString()
           + "&SelectedTests=" + Convert.ToString(ViewState["selectedTestNames"]) + "&Sort=" + sort + "&act=" + (int)SV.PrintActions.PDFPrint);

    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("P_ByCohot.aspx?Institution=" + this.ddInstitution.SelectedValue + "&TestType=" + ddProducts.SelectedValue + "&TestName=" + ViewState["TestID"].ToString()
            + "&SelectedTests=" + Convert.ToString(ViewState["selectedTestNames"]) + "&Sort=" + sort + "&act=" + (int)SV.PrintActions.ExportExcelDataOnly);
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("P_ByCohot.aspx?Institution=" + this.ddInstitution.SelectedValue + "&TestType=" + ddProducts.SelectedValue + "&TestName=" + ViewState["TestID"].ToString()
            + "&SelectedTests=" + Convert.ToString(ViewState["selectedTestNames"]) + "&Sort=" + sort + "&act=" + (int)SV.PrintActions.ExportExcel);

    }
    protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string tid = "";
        StringBuilder testNames = new StringBuilder();

        foreach (ListItem item in lbTests.Items)
        {
            if (item.Selected)
            {
                tid += item.Value + ",";
                testNames.Append(item.Text + ",");
            }
        }
        ViewState["TestID"] = tid;
        string selectedTestNames = testNames.ToString();
        if (selectedTestNames.Length > 0)
        {
            selectedTestNames = selectedTestNames.Substring(0, selectedTestNames.LastIndexOf(","));
        }
        ViewState["selectedTestNames"] = selectedTestNames;

        BindData();
    }
}
