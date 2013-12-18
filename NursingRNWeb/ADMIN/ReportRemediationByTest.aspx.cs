using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;
using NursingRNWeb.AppCode.Report_Ds;

public partial class ADMIN_ReportRemediationByTest : ReportPageBase<IReportRemediationByTestView, ReportRemediationByTestPresenter>, IReportRemediationByTestView
{
    private CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    public override void PreInitialize()
    {
        Presenter.PreInitialize();
        MapControl(ddlProgramofStudy, ddInstitution, lbCohorts, ddGroup, ddProducts, ddStudents);
    }

    #region IReportRemediationByTestView Methods

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
        ControlHelper.PopulateProducts(ddProducts, products);
    }

    public void PopulateCohorts(IEnumerable<Cohort> cohorts)
    {
        ControlHelper.PopulateCohorts(lbCohorts, cohorts);
    }

    public void PopulateGroup(IEnumerable<Group> groups)
    {
        ControlHelper.PopulateGroups(ddGroup, groups);
    }

    public void PopulateStudent(IEnumerable<StudentEntity> students)
    {
        ControlHelper.PopulateStudents(ddStudents, students);
    }

    public void PopulateProgramOfStudies(IEnumerable<ProgramofStudy> programOfStudies)
    {
        ControlHelper.PopulateProgramofStudy(ddlProgramofStudy, programOfStudies);
        HideProgramofStudy();
    }

    public void PopulateTests(IEnumerable<UserTest> tests)
    {
    }

    public void GenerateReport()
    {
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Aggregate Reports > Remediation Time")
                            .Add("Institution Id", ddInstitution.SelectedValue)
                            .Add("Cohort Id", lbCohorts.SelectedValue.ToString())
                            .Add("Group Id", ddGroup.SelectedValue)
                            .Add("Product Id", ddProducts.SelectedValue)
                            .Add("Student Id", ddStudents.SelectedValue)
                            .Write();
        #endregion
        Presenter.GenerateReport();
    }

    public void GenerateReport(IEnumerable<TestRemediationExplainationDetails> reportData, ReportAction printActions)
    {
        var sortedResult = KTPSort.Sort<TestRemediationExplainationDetails>(reportData, SortHelper.Parse(hdnGridConfig.Value));

        if (ddStudents.SelectedValue == Constants.LIST_SELECT_ALL_VALUE)
        {
            rpt.Load(Server.MapPath("~/Admin/Report/TestsRemediationByStudent.rpt"));
        }
        else
        {
            if (ddProducts.SelectedValue != "1")
            {
                rpt.Load(Server.MapPath("~/Admin/Report/TestsExplanationByStudent.rpt"));
            }
            else
            {
                rpt.Load(Server.MapPath("~/Admin/Report/TestsRemediationByStudent.rpt"));
            }
        }

        switch (printActions)
        {
            case ReportAction.ExportExcelDataOnly:
                rpt.SetDataSource(BuildDataSourceForReport(sortedResult));
                rpt.ReportDefinition.Sections[6].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Page.Response, true, "TestsRemediationByStudent");
                break;

            case ReportAction.ExportExcel:
                rpt.SetDataSource(BuildDataSourceForReport(sortedResult));
                rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                rpt.ReportDefinition.Sections[6].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Page.Response, true, "TestsRemediationByStudent");
                break;

            case ReportAction.PDFPrint:
                rpt.SetDataSource(BuildDataSourceForReport(sortedResult));
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Page.Response, true, "TestsRemediationByStudent");
                break;
        }
    }

    public void RenderReport(IEnumerable<TestRemediationExplainationDetails> reportData)
    {
        var sortedResult = KTPSort.Sort<TestRemediationExplainationDetails>(reportData, SortHelper.Parse(hdnGridConfig.Value));

        if (ddProducts.SelectedValue == ((int)ProductType.IntegratedTesting).ToString())
        {
            grvResult.DataSource = sortedResult;
            grvResult.DataBind();
            grvResult.Visible = true;
            grvExplanation.Visible = false;
        }
        else
        {
            grvExplanation.DataSource = sortedResult;
            grvExplanation.DataBind();
            grvExplanation.Visible = true;
            grvResult.Visible = false;
        }

        if (sortedResult.ToList().Count == 0)
        {
            lblM.Visible = true;
        }
        else
        {
            lblM.Visible = false;
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Aggregate Reports > Remediation Time ");
            #endregion
            Presenter.InitializeReport();
        }
    }

    protected override void OnUnload(EventArgs e)
    {
        base.OnUnload(e);
        rpt.Close();
        rpt.Dispose();
    }

    protected void btnPDF_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.GenerateReport(ReportAction.PDFPrint);
    }

    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.GenerateReport(ReportAction.ExportExcel);
    }

    protected void btnExcelOnlyData_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.GenerateReport(ReportAction.ExportExcelDataOnly);
    }

    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        hdnGridConfig.Value = "CohortName|ASC";
        GenerateReport();
    }

    protected void gvResult_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);

        GenerateReport();

        #region Change color of sorted column header
        switch (e.SortExpression)
        {
            case "CohortName":
                grvResult.HeaderRow.Cells[0].BackColor = Color.Pink;
                break;
            case "TestName":
                grvResult.HeaderRow.Cells[1].BackColor = Color.Pink;
                break;
            case "RemediationOrExplaination":
                grvResult.HeaderRow.Cells[2].BackColor = Color.Pink;
                break;
        }
        #endregion
    }

    protected void grvExplanation_sorting(object sender, GridViewSortEventArgs e)
    {
        hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);

        GenerateReport();

        #region Change color of sorted column header
        switch (e.SortExpression)
        {
            case "CohortName":
                grvExplanation.HeaderRow.Cells[0].BackColor = Color.Pink;
                break;
            case "TestName":
                grvExplanation.HeaderRow.Cells[1].BackColor = Color.Pink;
                break;
            case "RemediationOrExplaination":
                grvExplanation.HeaderRow.Cells[2].BackColor = Color.Pink;
                break;
        }
        #endregion
    }

    private DataSet BuildDataSourceForReport(IEnumerable<TestRemediationExplainationDetails> result)
    {
        TestRemediationByStudents ds = new TestRemediationByStudents();
        TestRemediationByStudents.HeadRow rh = ds.Head.NewHeadRow();
        rh.ReportName = "Remediation/Explanation Time By Student ";
        rh.InstitutionName = ddInstitution.SelectedItem.Text;
        rh.CohortName = lbCohorts.SelectedItemsText;
        rh.GroupName = ddGroup.SelectedItemsText;
        rh.StudentName = ddStudents.SelectedItemsText;
        rh.ProductName = ddProducts.SelectedItemsText;
        ds.Head.Rows.Add(rh);

        foreach (TestRemediationExplainationDetails testRemediationExplainationDetails in result)
        {
            TestRemediationByStudents.DetailRow rd = ds.Detail.NewDetailRow();
            rd.CohortName = testRemediationExplainationDetails.CohortName;
            rd.TestName = testRemediationExplainationDetails.TestName;
            rd.Remediation = testRemediationExplainationDetails.RemediationOrExplaination;
            rd.HeadID = rh.HeadID;
            ds.Detail.Rows.Add(rd);
        }

        return ds;
    }

    private void HideProgramofStudy()
    {
        IsProgramofStudyVisible = Presenter.CurrentContext.UserType == UserType.SuperAdmin || Presenter.IsMultipleProgramofStudyAssignedToAdmin(Presenter.CurrentContext.UserId);
    }
}
