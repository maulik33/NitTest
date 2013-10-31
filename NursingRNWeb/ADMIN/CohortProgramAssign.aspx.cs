using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class ADMIN_CohortProgramAssign : PageBase<ICohortView, CohortPresenter>, ICohortView
{
    private bool _hasAddPermission;

    public bool HasAddPermission { get; set; }

    public string SearchText { get; set; }

    public int GroupId { get; set; }

    public int CohortId { get; set; }

    public int ProductId { get; set; }

    public string InstitutionId { get; set; }

    public string Name { get; set; }

    public string StartDate { get; set; }

    public string EndDate { get; set; }

    public string Description { get; set; }

    public int CohortStatus { get; set; }

    public bool IsValidDate { get; set; }

    public int ProgramId { get; set; }

    public int TestId { get; set; }

    public string State { get; set; }

    public string Type { get; set; }

    public bool IsInValidCohort { get; set; }

    public string ErrorMessage { get; set; }

    public int ProgramofStudyId
    {
        get { return (int) (ViewState["ProgramofStudyId"] ?? 0); }
        set { ViewState["ProgramofStudyId"] = value; }
    }

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.View);
    }

    #region ICohortView Members

    public void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudy)
    {
        throw new NotImplementedException();
    }

    public void ShowCohortResults(IEnumerable<Cohort> groups)
    {
        throw new NotImplementedException();
    }

    public void SaveCohort(int newGroupId)
    {
        throw new NotImplementedException();
    }

    public void DeleteCohort()
    {
        throw new NotImplementedException();
    }

    public void PopulatePrograms(IEnumerable<Program> programs)
    {
        gvPrograms.DataSource = programs;
        gvPrograms.DataBind();
    }

    public void ShowProgramResults(IEnumerable<Program> programs, SortInfo sortMetaData)
    {
        gvPrograms.DataSource = KTPSort.Sort<Program>(programs, sortMetaData);
        gvPrograms.DataBind();
    }

    public void PopulateInstitutions(IEnumerable<Institution> institutions)
    {
    }

    public void PopulateStudents(IEnumerable<Student> students, SortInfo sortMetaData)
    {
        throw new NotImplementedException();
    }

    public void PopulateCohorts(IEnumerable<Cohort> Cohort, SortInfo sortMetaData)
    {
        throw new NotImplementedException();
    }

    public void PopulateGroups(IEnumerable<Group> groups)
    {
        throw new NotImplementedException();
    }

    public void RefreshPage(Cohort group, UserAction action, string title, string subTitle, bool hasDeletePermission, bool hasAddPermission, bool hasAccessDatesEdit, bool hasEditPremission)
    {
        _hasAddPermission = hasAddPermission;
        if (!hasAddPermission)
        {
            addbtn.Visible = false;
            addbtn.Enabled = false;
        }
    }

    public void PopulateProducts(IEnumerable<Product> products)
    {
        throw new NotImplementedException();
    }

    public void ShowCohort(Cohort cohort)
    {
        CohortId = cohort.CohortId;
        lblCohortName.Text = cohort.CohortName;
        lblDescription.Text = cohort.CohortDescription;
        lblProgOfStudy.Text = cohort.Institution.ProgramofStudyName;
    }

    public void PopulateTests(IEnumerable<Test> tests)
    {
        throw new NotImplementedException();
    }

    public void PopulateCohortTest(CohortTestDates TestDetails)
    {
        throw new NotImplementedException();
    }

    public void PopulateCohortTests(IEnumerable<CohortTestDates> testDetails)
    {
        throw new NotImplementedException();
    }

    public void PopulateProgramForTest(Program program)
    {
        throw new NotImplementedException();
    }

    public void ExportCohortList(IEnumerable<Cohort> reportData, ReportAction printActions)
    {
        throw new NotImplementedException();
    }

    public void PopulateStates(IEnumerable<State> states)
    {
        throw new NotImplementedException();
    }
    #endregion

    public void rbsira_OnCheckedChanged(object sender, EventArgs e)
    {
        lblM.Visible = false;
        string sRbText = string.Empty;
        RadioButton rb = new RadioButton();
        rb = (RadioButton)sender;
        sRbText = rb.ClientID;

        foreach (GridViewRow row in gvPrograms.Rows)
        {
            rb = (RadioButton)row.FindControl("rbsira");
            rb.Checked = false;
            if (sRbText == rb.ClientID)
            {
                rb.Checked = true;
                txtSiraNo.Text = rb.Text.Trim();
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Presenter.ShowProgramList(hdnGridConfig.Value);
            lblProgram.Text = Name;
        }
    }

    protected void gvPrograms_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Presenter.HasAssignPermission();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int id = gvPrograms.DataKeys[e.Row.RowIndex].Values["ProgramId"].ToInt();
            RadioButton rb = (RadioButton)e.Row.FindControl("rbsira");
            if (rb != null)
            {
                if (ProgramId == id)
                {
                    rb.Checked = true;
                }
                else
                {
                    rb.Checked = false;
                }

                if (!HasAddPermission)
                {
                    e.Row.Cells[0].Visible = false;
                }
                else
                {
                    e.Row.Cells[0].Visible = true;
                }
            }
        }
    }

    protected void gvPrograms_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!e.CommandName.Equals("Sort") && !e.CommandName.Equals("Page"))
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvPrograms.Rows[index];
            int id = gvPrograms.DataKeys[row.RowIndex].Values["ProgramId"].ToInt();

            if (e.CommandName == "Tests")
            {
                Presenter.NavigateFromCohortList(AdminPageDirectory.ProgramAddAssign, id);
            }
        }
    }

    protected void addbtn_Click(object sender, ImageClickEventArgs e)
    {
        foreach (GridViewRow row in gvPrograms.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                RadioButton rb = (RadioButton)row.FindControl("rbsira");
                if (rb != null)
                {
                    if (rb.Checked)
                    { //// This will get called  only once 
                        int id = gvPrograms.DataKeys[row.RowIndex].Values["ProgramId"].ToInt();
                        Name = Server.HtmlDecode(row.Cells[2].Text);
                        ProgramId = id;
                        #region Trace Information
                        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to Cohort Program Assign Page")
                                        .Add("Program Id", ProgramId.ToString())
                                        .Add("Cohort Id", Presenter.Id.ToString())
                                        .Write();
                        #endregion
                        Presenter.AssignProgram(); //// added in loop as only one program can be assign to Cohort
                        lblM.Visible = true;
                        lblProgram.Text = Name;
                    }
                }
            }
        }
    }

    protected void gvPrograms_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);
        SearchPrograms();
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
       SearchPrograms();
    }

    private void SearchPrograms()
    {
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to Cohort Program Assign Page - Search Program List")
                            .Add("Search Text", txtSearch.Text)
                            .Add("Test Id", TestId.ToString())
                            .Add("Cohort Id", Presenter.Id.ToString())
                            .Write();
        #endregion
        SearchText = txtSearch.Text.Trim();
        Presenter.HasAssignPermission();
        if (!HasAddPermission)
        {
            Presenter.SearchProgramForCohorts(txtSearch.Text, hdnGridConfig.Value);
        }
        else
        {
            Presenter.SearchPrograms(txtSearch.Text, hdnGridConfig.Value);
            _hasAddPermission = true;
        }
    }


    public void ExportStudents(IEnumerable<Student> reportData, ReportAction printActions)
    {
        throw new NotImplementedException();
    }
}
