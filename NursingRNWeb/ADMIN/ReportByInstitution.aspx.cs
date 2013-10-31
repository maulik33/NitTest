using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;
using NursingRNWeb.AppCode.Report_Ds;

public partial class Admin_ReportByInstitution : ReportPageBase<IReportByCohortView, ReportByCohortPresenter>, IReportByCohortView
{
    private ReportDocument rpt = new ReportDocument();

    public override void PreInitialize()
    {
        Presenter.PreInitialize();
        MapControl(ddlProgramofStudy, lbInstitution, lbCohort, ddProducts, ddTests);
    }

    #region IReportByCohortView Methods

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
        ControlHelper.PopulateProducts(ddProducts, products);
    }

    public void PopulateCohorts(IEnumerable<Cohort> cohorts)
    {
        ControlHelper.PopulateCohorts(lbCohort, cohorts);
    }

    public void PopulateTests(IEnumerable<UserTest> tests)
    {
        ControlHelper.PopulateTestsByTestId(ddTests, tests);
    }

    public void GenerateReport()
    {
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Aggregate Reports > Tests by Institution")
                            .Add("Institution Id", lbInstitution.SelectedValue)
                            .Add("Cohort Id", lbCohort.SelectedValue)
                            .Add("Product Id", ddProducts.SelectedValue)
                            .Add("Test Name", ddTests.SelectedValue)
                            .Write();
        #endregion
        Presenter.GenerateReport();
    }

    public void PopulateProgramOfStudies(IEnumerable<ProgramofStudy> programOfStudies)
    {
        ControlHelper.PopulateProgramofStudy(ddlProgramofStudy, programOfStudies);
        HideProgramofStudy();
    }
    public void GenerateReport(IEnumerable<TestByInstitutionResults> reportData, ReportAction printActions)
    {
        rpt.Load(Server.MapPath("~/Admin/Report/ByInstitution.rpt"));
        rpt.SetDataSource(BuildDataSourceForReport(KTPSort.Sort<TestByInstitutionResults>(reportData, SortHelper.Parse(hdnGridConfig.Value))));

        switch (printActions)
        {
            case ReportAction.DirectPrint:
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Page.Response, true, "ByInstitution");
                break;

            case ReportAction.ExportExcel:
                rpt.ReportDefinition.Sections[2].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Page.Response, true, "ByInstitution");
                break;

            case ReportAction.ExportExcelDataOnly:
                rpt.ReportDefinition.Sections[2].SectionFormat.EnableSuppress = true;
                rpt.ReportDefinition.Sections[5].SectionFormat.EnableSuppress = true;
                rpt.ReportDefinition.Sections[6].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Page.Response, true, "ByInstitution");
                break;
        }

        rpt.Close();
        rpt.Dispose();
    }

    public void RenderReport(IEnumerable<TestByInstitutionResults> reportData)
    {
        gvCohorts.Visible = true;

        if (ddProducts.SelectedValue != Constants.LIST_NOT_SELECTED_VALUE)
        {
            gvCohorts.DataSource = KTPSort.Sort<TestByInstitutionResults>(reportData, SortHelper.Parse(hdnGridConfig.Value));
            gvCohorts.DataBind();

            if (gvCohorts.Rows.Count == 0)
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

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Aggregate Reports > Tests by Institution ");
            #endregion
        }
    }

    protected override void OnUnload(EventArgs e)
    {
        base.OnUnload(e);
        rpt.Close();
        rpt.Dispose();
    }

    protected void gvCohorts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!e.CommandName.Equals("Sort") && !e.CommandName.Equals("Page"))
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvCohorts.Rows[index];

            string institutionId = ((HtmlInputHidden)row.Cells[5].FindControl("hdnInstitutionId")).Value;
            switch (e.CommandName)
            {
                case "Performance":
                    Presenter.NavigateToInstitutionPerfromanceReport(Convert.ToInt32(institutionId));
                    break;
            }
        }
    }

    protected void gvCohorts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[3].Text = e.Row.Cells[3].Text + "%";
        }
    }

    protected void gvCohorts_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);

        GenerateReport();

        #region Change color of sorted column header
        switch (e.SortExpression)
        {
            case "Institution.InstitutionName":
                gvCohorts.HeaderRow.Cells[1].BackColor = Color.Pink;
                break;
            case "Cohort.CohortName":
                gvCohorts.HeaderRow.Cells[0].BackColor = Color.Pink;
                break;
            case "NStudents":
                gvCohorts.HeaderRow.Cells[2].BackColor = Color.Pink;
                break;
            case "Percantage":
                gvCohorts.HeaderRow.Cells[3].BackColor = Color.Pink;
                break;
            case "Normed":
                gvCohorts.HeaderRow.Cells[4].BackColor = Color.Pink;
                break;
        }
        #endregion
    }



    protected void ImageButton1_Click1(object sender, ImageClickEventArgs e)
    {
        Presenter.GenerateReport(ReportAction.DirectPrint);
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

    private void HideProgramofStudy()
    {
        IsProgramofStudyVisible = Presenter.CurrentContext.UserType == UserType.SuperAdmin || Presenter.IsMultipleProgramofStudyAssignedToAdmin(Presenter.CurrentContext.UserId);
    }

    private DataSet BuildDataSourceForReport(IEnumerable<TestByInstitutionResults> reportData)
    {
        ByInstitution ds = new ByInstitution();
        ByInstitution.HeadRow rh = ds.Head.NewHeadRow();
        rh.ReportName = "By Institution ";
        rh.TestType = ddProducts.SelectedItemsText;
        rh.TestName = ddTests.SelectedItem.Text;
        ds.Head.Rows.Add(rh);

        foreach (TestByInstitutionResults testByInstitutionResults in reportData)
        {
            ByInstitution.DetailRow rd = ds.Detail.NewDetailRow();
            rd.Institution = testByInstitutionResults.Institution.InstitutionName;
            rd.Cohort = testByInstitutionResults.Cohort.CohortName;
            rd.NStudents = testByInstitutionResults.NStudents;
            rd.PercentageCorrect = testByInstitutionResults.Percantage;
            rd.Normed = testByInstitutionResults.Normed;
            rd.HeadID = rh.HeadID;
            ds.Detail.Rows.Add(rd);
        }
        return ds;
    }
}
