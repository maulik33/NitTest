using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;
using NursingRNWeb.AppCode.Report_Ds;

public partial class ADMIN_ReportCaseByCohort : ReportPageBase<IReportCaseByCohortView, ReportCaseByCohortPresenter>, IReportCaseByCohortView
{
    public string sort
    {
        get
        {
            object o = this.ViewState["sort"];
            if (o == null)
            {
                return "CohortName";
            }
            else
            {
                return o.ToString();
            }
        }

        set
        {
            this.ViewState["sort"] = value;
        }
    }

    public override void PreInitialize()
    {
        Presenter.PreInitialize();
        MapControl(ddlProgramofStudy,lbInstitution, ddCase, ddModule);
    }

    #region IReportStudentReportCardView Methods

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
        ControlHelper.PopulateInstitutions(lbInstitution, institutions, true);
    }

    public void PopulateProducts(IEnumerable<Product> products)
    {
    }

    public void PopulateCohorts(IEnumerable<Cohort> cohorts)
    {
    }

    public void PopulateTests(IEnumerable<UserTest> tests)
    {
    }

    public void PopulateCases(IEnumerable<CaseStudy> cases)
    {
        ControlHelper.PopulateCase(ddCase, cases);
    }

    public void PopulateModule(IEnumerable<Modules> module)
    {
        ControlHelper.PopulateModule(ddModule, module);
    }

    public void GenerateReport()
    {
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Aggregate Reports > Case Results by Cohort")
                            .Add("Institution Id", lbInstitution.SelectedValue)
                            .Add("Case Id", ddCase.SelectedValue)
                            .Add("Module Id", ddModule.SelectedValue)
                            .Write();
        #endregion
        Presenter.GenerateReport();
    }

    public void GenerateReport(IEnumerable<CaseByCohortResult> reportData, ReportAction printActions)
    {
        var sortedResult = GetSortedData(reportData);
        CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        rpt.Load(Server.MapPath("~/Admin/Report/CaseByCohort.rpt"));
        rpt.SetDataSource(BuildDs(reportData));

        switch (printActions)
        {
            case ReportAction.ExportExcelDataOnly:
                rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                rpt.ReportDefinition.Sections["Section5"].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Page.Response, true, "CaseByCohort");
                break;

            case ReportAction.ExportExcel:
                rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;

                rpt.ReportDefinition.Sections["Section5"].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Page.Response, true, "CaseByCohort");
                break;

            case ReportAction.PDFPrint:
                rpt.ReportDefinition.Sections[2].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Page.Response, true, "CaseByCohort");
                break;
        }
    }

    public void RenderReport(IEnumerable<CaseByCohortResult> reportData)
    {
        if (ddCase.SelectedValue != Constants.LIST_NOT_SELECTED_VALUE)
        {
            var sortedResult = GetSortedData(reportData);

            gvCohorts.DataSource = sortedResult;
            gvCohorts.DataBind();

            if (gvCohorts.Rows.Count > 0)
            {
                lblM.Visible = false;
            }
            else
            {
                lblM.Visible = true;
            }
        }
        else
        {
            gvCohorts.DataSource = null;
            gvCohorts.DataBind();
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
            TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Aggregate Reports > Case Results by Cohort");
            #endregion
        }
    }

    protected void gvCohorts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!e.CommandName.Equals("Sort") && !e.CommandName.Equals("Page"))
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvCohorts.Rows[index];

            int id = Convert.ToInt32(((HtmlInputHidden)row.Cells[4].FindControl("hdnCohortId")).Value);
            int institutionId = Convert.ToInt32(((HtmlInputHidden)row.Cells[4].FindControl("hdnInstitutionId")).Value);

            switch (e.CommandName)
            {
                case "Performance":
                    Presenter.NavigateToReportCohortResultByModule(institutionId, id);
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

        GenerateReport();

        #region Change color of sorted column header
        switch (e.SortExpression)
        {
            case "CohortName":
                gvCohorts.HeaderRow.Cells[0].BackColor = Color.Pink;
                break;
            case "NumberOfStudents":
                gvCohorts.HeaderRow.Cells[1].BackColor = Color.Pink;
                break;
            case "Percentage":
                gvCohorts.HeaderRow.Cells[2].BackColor = Color.Pink;
                break;
        }
        #endregion
    }

    protected void ImageButton1_Click1(object sender, ImageClickEventArgs e)
    {
        Presenter.GenerateReport(ReportAction.PDFPrint);
    }

    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.GenerateReport(ReportAction.ExportExcelDataOnly);
    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.GenerateReport(ReportAction.ExportExcel);
    }

    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        GenerateReport();
    }

    private DataSet BuildDs(IEnumerable<CaseByCohortResult> reportData)
    {
        CaseByCohort ds = new CaseByCohort();
        CaseByCohort.HeadRow rh = ds.Head.NewRow() as CaseByCohort.HeadRow;

        rh.InstitutionName = lbInstitution.SelectedItemsText.Replace("|", ",");

        rh.ModuleName = ddModule.SelectedItem.Text;
        rh.CaseName = ddCase.SelectedItem.Text;
        rh.ReportName = "Case By Cohort";
        ds.Head.Rows.Add(rh);

        var sortedResult = from record in reportData
                           orderby this.sort
                           select record;

        foreach (CaseByCohortResult r in sortedResult)
        {
            CaseByCohort.DetailRow rd = ds.Detail.NewRow() as CaseByCohort.DetailRow;
            rd.Cohort = r.Cohort.CohortName;
            rd.NStudents = r.NumberOfStudents;
            rd.PercentageCorrect = Convert.ToString(r.Percentage);
            rd.HeadID = rh.HeadID;
            ds.Detail.Rows.Add(rd);
        }

        return ds;
    }

    #region Sorting Methods

    private IEnumerable<CaseByCohortResult> GetSortedData(IEnumerable<CaseByCohortResult> reportData)
    {
        IEnumerable<CaseByCohortResult> sortedData = new List<CaseByCohortResult>();
        bool isDesc = this.sort.Contains(" DESC");

        switch (this.sort.Replace("DESC", string.Empty).Trim())
        {
            case "CohortName":
                sortedData = GetSortedDataHelper(reportData, rec => rec.Cohort.CohortName);
                break;
            case "NumberOfStudents":
                sortedData = GetSortedDataHelper(reportData, rec => rec.NumberOfStudents);
                break;
            case "Percentage":
                sortedData = GetSortedDataHelper(reportData, rec => rec.Percentage);
                break;
        }

        if (isDesc)
        {
            sortedData = sortedData.Reverse();
        }

        return sortedData;
    }

    private IEnumerable<CaseByCohortResult> GetSortedDataHelper<TKey>(IEnumerable<CaseByCohortResult> reportData,
                Func<CaseByCohortResult, TKey> Selector)
    {
        var query = from e in reportData
                    orderby Selector(e)
                    select e;
        return query;
    }

    private void HideProgramofStudy()
    {
        IsProgramofStudyVisible = Presenter.CurrentContext.UserType == UserType.SuperAdmin || Presenter.IsMultipleProgramofStudyAssignedToAdmin(Presenter.CurrentContext.UserId);
    }

    #endregion
}
