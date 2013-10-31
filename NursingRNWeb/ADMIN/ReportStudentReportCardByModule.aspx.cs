using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;
using NursingRNWeb.AppCode.Report_Ds;

public partial class ADMIN_ReportStudentReportCardByModule : ReportPageBase<IReportStudentReportCardByModuleView, ReportStudentReportCardByModulePresenter>, IReportStudentReportCardByModuleView
{
    private ReportDocument rpt = new ReportDocument();

    public string CaseName { get; set; }

    public override void PreInitialize()
    {
        Presenter.PreInitialize();
        MapControl(ddlProgramofStudy,lbInstitution, ddCohorts, lbxStudent, ddCase, lbxModule);
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
        ControlHelper.PopulateCohorts(ddCohorts, cohorts);
    }

    public void PopulateTests(IEnumerable<UserTest> tests)
    {
    }

    public void PopulateStudent(IEnumerable<StudentEntity> students)
    {
        ControlHelper.PopulateStudents(lbxStudent, students);
    }

    public void PopulateCases(IEnumerable<CaseStudy> cases)
    {
        ControlHelper.PopulateCase(ddCase, cases);
    }

    public void PopulateModule(IEnumerable<Modules> module)
    {
        ControlHelper.PopulateModule(lbxModule, module);
    }

    public void GenerateReport()
    {
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Aggregate Reports > Student Report Card")
                            .Add("Institution Id", lbInstitution.SelectedValue)
                            .Add("Cohort Id", ddCohorts.SelectedValue)
                            .Add("Case Id", ddCase.SelectedValue)
                            .Add("Module Id", lbxModule.SelectedValue)
                            .Add("Student Id", lbxStudent.SelectedValue)
                            .Write();
        #endregion
        Presenter.GenerateReport();
    }

    public void GenerateReport(IEnumerable<ResultsForStudentReportCardByModule> reportData, ReportAction printActions)
    {
        IEnumerable<ResultsForStudentReportCardByModule> sortedData = KTPSort.Sort<ResultsForStudentReportCardByModule>(reportData, SortHelper.Parse(hdnGridConfig.Value));

        rpt.Load(Server.MapPath("~/Admin/Report/StudentReportCardByModule.rpt"));
        rpt.SetDataSource(BuildDataSourceForReport(sortedData));

        switch (printActions)
        {
            case ReportAction.ExportExcel:
                rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                rpt.ReportDefinition.Sections[6].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Page.Response, true, "StudentReportCardByModule");
                break;
            case ReportAction.PDFPrint:
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Page.Response, true, "StudentReportCardByModule");
                break;
            case ReportAction.ExportExcelDataOnly:
                rpt.ReportDefinition.Sections[6].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Page.Response, true, "StudentReportCardByModule");
                break;
        }
    }

    public void RenderReport(IEnumerable<ResultsForStudentReportCardByModule> reportData)
    {
        IEnumerable<ResultsForStudentReportCardByModule> sortedData = KTPSort.Sort<ResultsForStudentReportCardByModule>(reportData, SortHelper.Parse(hdnGridConfig.Value));

        grvResult.DataSource = sortedData;
        grvResult.DataBind();
        grvResult.Visible = true;
        if (grvResult.Rows.Count == 0)
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
        if (ddCase.SelectedItem != null)
        {
            CaseName = ddCase.SelectedItem.Text;
        }

        if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Aggregate Reports > Student Report Card By Module");
            #endregion
        }
    }

    protected override void OnUnload(EventArgs e)
    {
        base.OnUnload(e);
        rpt.Close();
        rpt.Dispose();
    }

    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        GenerateReport();
    }

    protected void grvResult_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);

        GenerateReport();

        #region Change color of sorted column header
        switch (e.SortExpression)
        {
            case "Student.FullName":
                grvResult.HeaderRow.Cells[0].BackColor = Color.Pink;
                break;
            case "Student.FirstName":
                grvResult.HeaderRow.Cells[1].BackColor = Color.Pink;
                break;
            case "CaseStudy.CaseName":
                grvResult.HeaderRow.Cells[2].BackColor = Color.Pink;
                break;
            case "Module.ModuleName":
                grvResult.HeaderRow.Cells[3].BackColor = Color.Pink;
                break;
            case "Correct":
                grvResult.HeaderRow.Cells[4].BackColor = Color.Pink;
                break;
        }
        #endregion
    }

    protected void btnPrintPDF_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.GenerateReport(ReportAction.PDFPrint);
    }

    protected void btnPrintExcel_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.GenerateReport(ReportAction.ExportExcel);
    }

    protected void btnPrintExcelDataOnly_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.GenerateReport(ReportAction.ExportExcelDataOnly);
    }

    private DataSet BuildDataSourceForReport(IEnumerable<ResultsForStudentReportCardByModule> reportData)
    {
        StudentReportCardByModule studentReportCardByModule = new StudentReportCardByModule();
        StudentReportCardByModule.HeadRow rh = studentReportCardByModule.Head.NewHeadRow();
        rh.ReportName = "Student Report Card";
        rh.InstitutionName = lbInstitution.SelectedItemsText;
        rh.CohortName = ddCohorts.SelectedItem.Text;

        studentReportCardByModule.Head.Rows.Add(rh);

        foreach (ResultsForStudentReportCardByModule resultsForStudentReportCardByModule in reportData)
        {
            StudentReportCardByModule.DetailRow rd = studentReportCardByModule.Detail.NewDetailRow();
            rd.FirstName = resultsForStudentReportCardByModule.Student.FirstName;
            rd.LastName = resultsForStudentReportCardByModule.Student.LastName;
            rd.CaseName = resultsForStudentReportCardByModule.CaseStudy.CaseName;
            rd.ModuleName = resultsForStudentReportCardByModule.Module.ModuleName;
            rd.Correct = resultsForStudentReportCardByModule.Correct;

            rd.HeadID = rh.HeadID;
            studentReportCardByModule.Detail.Rows.Add(rd);
        }

        return studentReportCardByModule;
    }

    private void HideProgramofStudy()
    {
        IsProgramofStudyVisible = Presenter.CurrentContext.UserType == UserType.SuperAdmin || Presenter.IsMultipleProgramofStudyAssignedToAdmin(Presenter.CurrentContext.UserId);
    }
}
