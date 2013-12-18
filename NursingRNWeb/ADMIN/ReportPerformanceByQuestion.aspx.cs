using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class ADMIN_ReportPerformanceByQuestion : ReportPageBase<IReportPerformanceByQuestionView, ReportPerformanceByQuestionPresenter>, IReportPerformanceByQuestionView
{
    public override void PreInitialize()
    {
        Presenter.PreInitialize();
        MapControl(ddlProgramofStudy,lbxInstitution, lbxCohort, ddProducts, ddTests);
    }

    #region IReportPerformanceByQuestionView Methods

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
        set { trProgramofStudy.Visible = value;}
    }

    public void PopulateInstitutions(IEnumerable<Institution> institutions)
    {
        ControlHelper.PopulateInstitutions(lbxInstitution, institutions,true);
    }

    public void PopulateProducts(IEnumerable<Product> products)
    {
        ControlHelper.PopulateProducts(ddProducts, products);
    }

    public void PopulateCohorts(IEnumerable<Cohort> cohorts)
    {
        ControlHelper.PopulateCohorts(lbxCohort, cohorts);
    }

    public void PopulateTests(IEnumerable<UserTest> tests)
    {
        ControlHelper.PopulateTestsByTestId(ddTests, tests);
    }

    public void PopulateProgramOfStudies(IEnumerable<ProgramofStudy> programOfStudies)
    {
        ControlHelper.PopulateProgramofStudy(ddlProgramofStudy, programOfStudies);
        HideProgramofStudy();
    }

    public void GenerateReport()
    {
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Aggregate Reports > Student Performance by Question Report")
                    .Add("Institution Id", lbxInstitution.SelectedValue)
                    .Add("Cohort Id", lbxCohort.SelectedValue)
                    .Add("Product Id", ddProducts.SelectedValue)
                    .Add("Test Id", ddTests.SelectedValue)
                    .Write();
        #endregion
        Presenter.GenerateReport();
    }

    public void ExportReport(IEnumerable<SummaryPerformanceByQuestionResult> reportData)
    {
        StringBuilder excelData = new StringBuilder();

        string sep = string.Empty;
        excelData.Append(sep + " Summary Performance by Question Report\t\t\t\t\t\t(N= " + reportData.FirstOrDefault().StudentNumber + " students)\n");

        sep = "\t";
        excelData.Append("QuestionId" + sep + "Answer" + sep + "Total1" + sep + "Total2" + sep + "Total3" + sep + "Total4" + sep + "TotalN" + sep
            + "Total#Correct" + sep + "Total#Wrong" + sep + "CorrectPercent");
        excelData.Append("\n");

        foreach (SummaryPerformanceByQuestionResult record in reportData)
        {
            sep = string.Empty;
            excelData.Append(sep + record.QuestionId);
            sep = "\t";
            excelData.Append(sep + record.Answer);
            excelData.Append(sep + record.Total1);
            excelData.Append(sep + record.Total2);
            excelData.Append(sep + record.Total3);
            excelData.Append(sep + record.Total4);
            excelData.Append(sep + record.TotalN);
            excelData.Append(sep + record.TotalNumberCorrect);
            excelData.Append(sep + record.TotalNumberWrong);
            excelData.Append(sep + "=fixed(" + record.CorrectPercent + ",1)");
            excelData.Append("\n");
        }

        ReportHelper.ExportToExcel(excelData.ToString(), "ReportPerformanceByQuestion.xls");
    }

    public void RenderReport(IEnumerable<SummaryPerformanceByQuestionResult> reportData)
    {
       var studentNumber=0;
       if (reportData.Count() > 0)
       {
           studentNumber = (from record in reportData
                            select record.StudentNumber).Max();
       }
        lblN.Text = "(N= " + studentNumber.ToString() + " students)";
        gvResult.DataSource = KTPSort.Sort<SummaryPerformanceByQuestionResult>(reportData, SortHelper.Parse(hdnGridConfig.Value));
        gvResult.DataBind();
    }

   #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Aggregate Reports > Student Performance by Question Report");
            #endregion
        }
    }

    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        GenerateReport();
    }

    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.ExportReportToExcel();
    }

    protected void gvResult_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);

        GenerateReport();

        #region Change color of sorted column header

        switch (e.SortExpression)
        {
            case "QuestionId":
                gvResult.HeaderRow.Cells[0].BackColor = Color.Pink;
                break;
            case "Answer":
                gvResult.HeaderRow.Cells[1].BackColor = Color.Pink;
                break;
            case "Total1":
                gvResult.HeaderRow.Cells[2].BackColor = Color.Pink;
                break;
            case "Total2":
                gvResult.HeaderRow.Cells[3].BackColor = Color.Pink;
                break;
            case "Total3":
                gvResult.HeaderRow.Cells[4].BackColor = Color.Pink;
                break;
            case "Total4":
                gvResult.HeaderRow.Cells[5].BackColor = Color.Pink;
                break;
            case "TotalN":
                gvResult.HeaderRow.Cells[6].BackColor = Color.Pink;
                break;
            case "TotalNumberCorrect":
                gvResult.HeaderRow.Cells[7].BackColor = Color.Pink;
                break;
            case "TotalNumberWrong":
                gvResult.HeaderRow.Cells[8].BackColor = Color.Pink;
                break;
            case "CorrectPercent":
                gvResult.HeaderRow.Cells[9].BackColor = Color.Pink;
                break;
        }

        #endregion
    }

    private void HideProgramofStudy()
    {
        IsProgramofStudyVisible = Presenter.CurrentContext.UserType == UserType.SuperAdmin;
    }
}
