using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class ADMIN_ReportStudentResultByCase : ReportPageBase<IReportStudentResultByCaseView, ReportStudentResultByCasePresenter>, IReportStudentResultByCaseView
{
    public override void PreInitialize()
    {
        Presenter.PreInitialize();
        MapControl(ddlProgramofStudy,ddInstitution, ddCohort, ddCase);
    }

    #region IReportStudentResultByCaseView Methods

    public bool PostBack
    {
        get
        {
            return IsPostBack;
        }
    }

    public bool IsProgramofStudyVisible
    {
        get { return trProgramofStudy.Visible; }
        set { trProgramofStudy.Visible = value; }
    }

    public void PopulateInstitutions(IEnumerable<Institution> institutions)
    {
        ControlHelper.PopulateInstitutions(ddInstitution, institutions, true);
    }

    public void PopulateProducts(IEnumerable<Product> products)
    {
    }

    public void PopulateCohorts(IEnumerable<Cohort> cohorts)
    {
        ControlHelper.PopulateCohorts(ddCohort, cohorts);
    }

    public void PopulateTests(IEnumerable<UserTest> tests)
    {
    }

    public void PopulateCases(IEnumerable<CaseStudy> cases)
    {
        ControlHelper.PopulateCase(ddCase, cases);
    }

    public void GenerateReport()
    {
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Student Reports >  Student Results by Case")
                            .Add("Institution Id", ddInstitution.SelectedValue)
                            .Add("Cohort Id", ddCohort.SelectedValue)
                            .Add("Case Id", ddCase.SelectedValue)
                            .Write();
        #endregion
        Presenter.GenerateReport(txtSearch.Text);
    }

    public void RenderReport(IEnumerable<StudentEntity> reportData)
    {
        gridUsers.DataSource = KTPSort.Sort<StudentEntity>(reportData, SortHelper.Parse(hdnGridConfig.Value));
        gridUsers.DataBind();

        if (gridUsers.Rows.Count == 0)
        {
            lblM.Visible = true;
        }
        else
        {
            lblM.Visible = false;
        }
    }

   public void PopulateProgramOfStudies(IEnumerable<ProgramofStudy> programOfStudies)
    {
        ControlHelper.PopulateProgramofStudy(ddlProgramofStudy, programOfStudies);
        HideProgramofStudy();
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Student Reports >  Student Results by Case");
            #endregion
        }
    }

    protected void gridUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!e.CommandName.Equals("Sort") && !e.CommandName.Equals("Page"))
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gridUsers.Rows[index];

            string id = gridUsers.DataKeys[index].Value.ToString();

            switch (e.CommandName)
            {
                case "View":
                    string newWindowUrl = "../Service/Launchdxr.aspx?eid=" + id + "&cid=" + ddCase.SelectedValue
                        + "&iid=" + Presenter.Id.ToString() + "&firstname=" + row.Cells[2].Text + "&lastname=" + row.Cells[1].Text;
                    string javaScript = PopupDXR(newWindowUrl);
                    ClientScript.RegisterStartupScript(typeof(Page), string.Empty, javaScript);
                    break;
            }
        }
    }

    protected void gridUsers_PageIndexChanged(Object sender, EventArgs e)
    {
        gridUsers.Visible = true;
    }

    protected void gridUsers_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);

        GenerateReport();

        #region Change color of sorted column header
        switch (e.SortExpression)
        {
            case "StudentId":
                gridUsers.HeaderRow.Cells[0].BackColor = Color.Pink;
                break;
            case "FullName":
                gridUsers.HeaderRow.Cells[1].BackColor = Color.Pink;
                break;
            case "FirstName":
                gridUsers.HeaderRow.Cells[2].BackColor = Color.Pink;
                break;
            case "UserName":
                gridUsers.HeaderRow.Cells[3].BackColor = Color.Pink;
                break;
        }
        #endregion
    }

    protected void Searchbtn_Click(object sender, ImageClickEventArgs e)
    {
        GenerateReport();
    }

    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        GenerateReport();
    }

    protected void gridUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GenerateReport();
        gridUsers.PageIndex = e.NewPageIndex;
        gridUsers.DataBind();
    }

    private string PopupDXR(string winUrl)
    {
        string strJavascript = "<script type='text/javascript'>\n" +
        "<!--\n" +
        "window.open('" + winUrl + "',\'Nursing\',\'status=1,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=1,height=600,width=974\');\n" +
        "// -->\n" +
        "</script>\n";
        return strJavascript;
    }

    private void HideProgramofStudy()
    {
        IsProgramofStudyVisible = Presenter.CurrentContext.UserType == UserType.SuperAdmin || Presenter.IsMultipleProgramofStudyAssignedToAdmin(Presenter.CurrentContext.UserId);
    }

}
