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

    private CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

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
            DataTable dt;
            if (ddProducts.SelectedValue == "1")
            {
                dt = remediation();
                DataView dv = new DataView(dt);

                dt.DefaultView.Sort = this.sort;
                gvRemediated.DataSource = dt.DefaultView;
                gvRemediated.DataBind();
                lblStudentNumber.Visible = true;
                gvRemediated.Visible = true;
                gvExplanation.Visible = false;
            }
            else
            {
                dt = explaination();
                DataView dv = new DataView(dt);

                dt.DefaultView.Sort = this.sort;
                gvExplanation.DataSource = dt.DefaultView;
                gvExplanation.DataBind();
                gvRemediated.Visible = false;
                gvExplanation.Visible = true;
                lblStudentNumber.Visible = true;
            }
            lblStudentNumber.Text = "N=" + dt.Rows.Count.ToString();
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
            gvRemediated.DataSource = null;
            gvRemediated.DataBind();
            gvExplanation.DataSource = null;
            gvExplanation.DataBind();
        }

    }
    protected void ddInstitution_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["InstitutionID"] = Convert.ToInt32(ddInstitution.SelectedValue);

        new cCohort().PopulateCohort(ddCohorts, Convert.ToInt32(ViewState["InstitutionID"].ToString()));
        ddCohorts.Items.Insert(0, new ListItem("Select All", "1"));
        ddCohorts.Items.Insert(0, new ListItem("Not Selected", "0"));
        ddTests.Items.Clear();
        ddTests.Items.Insert(0, new ListItem("Not Selected", "0"));
        ddProducts.SelectedIndex = 0;
        gvRemediated.Visible = false;
        lblStudentNumber.Visible = false;
        gvExplanation.Visible = false;


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
        gvRemediated.Visible = false;
        gvExplanation.Visible = false;
        lblStudentNumber.Visible = false;
        //        BindData();
    }
    protected void ddTests_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void ddCohorts_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvRemediated.Visible = false;
        gvExplanation.Visible = false;
        lblStudentNumber.Visible = false;
        if (ddCohorts.SelectedValue == "1")
        {
            new cTest().PopulateTestsByProductAndInsitution(ddTests, Convert.ToInt32(ddInstitution.SelectedValue), Convert.ToInt32(ddProducts.SelectedValue));
        }
        else
        {
            new cTest().PopulateTestsByCohortIDAndProductID(ddTests, Convert.ToInt32(ddCohorts.SelectedValue), Convert.ToInt32(ddProducts.SelectedValue));
        }
        ddTests.Items.Insert(0, new ListItem("Not Selected", "0"));
        //      BindData();
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
        DataTable dt;
        if (ddProducts.SelectedValue == "1")
        {
            dt = remediation();
            rpt.Load(Server.MapPath("~/Admin/Report/RemediationByStudent.rpt"));
            rpt.SetDataSource(BuildDs(dt, true));
        }
        else
        {
            dt = explaination();
            //new cCohort().getExplanationTimeForStudent(Convert.ToInt32(ddCohorts.SelectedValue), Convert.ToInt32(ddTests.SelectedValue));
            rpt.Load(Server.MapPath("~/Admin/Report/ExplanationByStudent.rpt"));
            rpt.SetDataSource(BuildDs(dt, false));


        }

        rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Page.Response, true, "RemediationByCohort");
        /*
        Response.Redirect("P_RemediationByStudent.aspx?Institution=" + this.ddInstitution.SelectedValue + "&Cohort=" + ddCohorts.SelectedValue + "&TestType=" + ddProducts.SelectedValue + "&TestName=" + ddTests.SelectedValue + "&Sort=" + this.sort);
         **/
    }
    protected DataSet BuildDs(DataTable dt, bool remedTest)
    {
        RemediationByStudent ds = new RemediationByStudent();
        RemediationByStudent.HeadRow rh = ds.Head.NewHeadRow();
        rh.InstitutionName = ddInstitution.SelectedItem.Text;
        if (ddCohorts.SelectedItem.Text == "Select All")
        {
            rh.CohortName = "All Cohort";
        }
        else
        {
            rh.CohortName = ddCohorts.SelectedItem.Text;
        }
        rh.TestType = ddProducts.SelectedItem.Text;
        rh.TestName = ddTests.SelectedItem.Text;
        rh.ReportName = "Remediation by Cohort";
        ds.Head.Rows.Add(rh);

        foreach (DataRow r in dt.Rows)
        {
            RemediationByStudent.DetailRow rd = ds.Detail.NewDetailRow();
            rd.FirstName = r["FirstName"].ToString();
            rd.LastName = r["LastName"].ToString();
            if (remedTest)
            {
                rd.Remediated = r["Remediation"].ToString();
            }
            else
            {
                rd.Explanation = r["Explanation"].ToString();
            }
            rd.HeadID = rh.HeadID;
            ds.Detail.Rows.Add(rd);
        }
        return ds;
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt;
        if (ddProducts.SelectedValue == "1")
        {
            //           dt = new cCohort().getRemedationTimeForStudent(Convert.ToInt32(ddCohorts.SelectedValue), Convert.ToInt32(ddTests.SelectedValue));
            dt = remediation();
            rpt.Load(Server.MapPath("~/Admin/Report/RemediationByStudent.rpt"));
            rpt.SetDataSource(BuildDs(dt, true));
        }
        else
        {
            dt = explaination();
            //new cCohort().getExplanationTimeForStudent(Convert.ToInt32(ddCohorts.SelectedValue), Convert.ToInt32(ddTests.SelectedValue));
            rpt.Load(Server.MapPath("~/Admin/Report/ExplanationByStudent.rpt"));
            rpt.SetDataSource(BuildDs(dt, false));

        }
        rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Page.Response, true, "RemediationByCohort");
        /*Response.Redirect("P_RemediationByStudent.aspx?Institution=" + this.ddInstitution.SelectedValue + "&Cohort=" + ddCohorts.SelectedValue + "&TestType=" + ddProducts.SelectedValue + "&TestName=" + ddTests.SelectedValue + "&Sort=" + this.sort + "&act=" + (int)SV.PrintActions.PDFPrint);
         * */

    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt;
        if (ddProducts.SelectedValue == "1")
        {
            dt = remediation();
            //new cCohort().getRemedationTimeForStudent(Convert.ToInt32(ddCohorts.SelectedValue), Convert.ToInt32(ddTests.SelectedValue));
            rpt.Load(Server.MapPath("~/Admin/Report/RemediationByStudent.rpt"));
            rpt.SetDataSource(BuildDs(dt, true));


        }
        else
        {
            dt = explaination();
            //new cCohort().getExplanationTimeForStudent(Convert.ToInt32(ddCohorts.SelectedValue), Convert.ToInt32(ddTests.SelectedValue));
            rpt.Load(Server.MapPath("~/Admin/Report/ExplanationByStudent.rpt"));
            rpt.SetDataSource(BuildDs(dt, false));


        }

        rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Page.Response, true, "RemediationByCohort");
        /*
        Response.Redirect("P_RemediationByStudent.aspx?Institution=" + this.ddInstitution.SelectedValue + "&Cohort=" + ddCohorts.SelectedValue + "&TestType=" + ddProducts.SelectedValue + "&TestName=" + ddTests.SelectedValue + "&Sort=" + this.sort + "&act=" + (int)SV.PrintActions.ExportExcel);
        * */
    }

    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt;
        if (ddProducts.SelectedValue == "1")
        {
            dt = remediation();
            //new cCohort().getRemedationTimeForStudent(Convert.ToInt32(ddCohorts.SelectedValue), Convert.ToInt32(ddTests.SelectedValue));
            rpt.Load(Server.MapPath("~/Admin/Report/RemediationByStudent.rpt"));
            rpt.SetDataSource(BuildDs(dt, true));
        }
        else
        {
            dt = explaination();
            //new cCohort().getExplanationTimeForStudent(Convert.ToInt32(ddCohorts.SelectedValue), Convert.ToInt32(ddTests.SelectedValue));
            rpt.Load(Server.MapPath("~/Admin/Report/ExplanationByStudent.rpt"));
            rpt.SetDataSource(BuildDs(dt, false));

        }

        rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Page.Response, true, "RemediationByCohort");
        /*
        Response.Redirect("P_RemediationByStudent.aspx?Institution=" + this.ddInstitution.SelectedValue + "&Cohort=" + ddCohorts.SelectedValue + "&TestType=" + ddProducts.SelectedValue + "&TestName=" + ddTests.SelectedValue + "&Sort=" + this.sort + "&act=" + (int)SV.PrintActions.ExportExcelDataOnly);
        * */
    }

    protected override void OnUnload(EventArgs e)
    {
        base.OnUnload(e);
        rpt.Close(); rpt.Dispose();
    }


    protected DataTable remediation()
    {
        DataTable dt;
        if (ddCohorts.SelectedValue == "1")
        {
            dt = new cCohort().getRemedationTimeForStudentByInstitution(Convert.ToInt32(ddInstitution.SelectedValue), Convert.ToInt32(ddTests.SelectedValue));
        }
        else
        {
            dt = new cCohort().getRemedationTimeForStudent(Convert.ToInt32(ddCohorts.SelectedValue), Convert.ToInt32(ddTests.SelectedValue));
        }
        return dt;
    }

    protected DataTable explaination()
    {
        DataTable dt;
        if (ddCohorts.SelectedValue == "1")
        {
            dt = new cCohort().getExplanationTimeForStudentForInstitution(Convert.ToInt32(ddInstitution.SelectedValue), Convert.ToInt32(ddTests.SelectedValue));
        }
        else
        {
            dt = new cCohort().getExplanationTimeForStudent(Convert.ToInt32(ddCohorts.SelectedValue), Convert.ToInt32(ddTests.SelectedValue));
        }
        return dt;
    }
}
