using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;

public partial class ADMIN_ReportPerformanceByQuestionToExcel : ReportPageBase<IReportPerformanceByQuestionToExcelView, ReportPerformanceByQuestionToExcelPresenter>, IReportPerformanceByQuestionToExcelView
{
    public override void PreInitialize()
    {
    }

    #region IReportStudentReportCardView Methods

    public bool PostBack
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public void PopulateInstitutions(IEnumerable<Institution> institutions)
    {
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

    public void GenerateReport()
    {
    }

    public void PopulateProgramOfStudies(IEnumerable<ProgramofStudy> programOfStudies)
    {
        throw new NotImplementedException();
    }

    public void RenderReport(IEnumerable<SummaryPerformanceByQuestionResult> reportData)
    {
        Response.Clear();
        Response.ContentType = "application/vnd.ms-excel";
        string sep = string.Empty;
        Response.Write(sep + " Summary Performance by Question Report\t\t\t\t\t\t(N= " + reportData.FirstOrDefault().StudentNumber + " students)\n");

        sep = "\t";
        Response.Write("QuestionId" + sep + "Answer" + sep + "Total1" + sep + "Total2" + sep + "Total3" + sep + "Total4" + sep + "TotalN" + sep
            + "Total#Correct" + sep + "Total#Wrong" + sep + "CorrectPercent");
        Response.Write("\n");

        foreach (SummaryPerformanceByQuestionResult record in reportData)
        {
            sep = string.Empty;
            Response.Write(sep + record.QuestionId);
            sep = "\t";
            Response.Write(sep + record.Answer);
            Response.Write(sep + record.Total1);
            Response.Write(sep + record.Total2);
            Response.Write(sep + record.Total3);
            Response.Write(sep + record.Total4);
            Response.Write(sep + record.TotalN);
            Response.Write(sep + record.TotalNumberCorrect);
            Response.Write(sep + record.TotalNumberWrong);
            Response.Write(sep + "=fixed(" + record.CorrectPercent + ",1)");
            
            Response.Write("\n");
        }

        Response.End();
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Presenter.ExportToExcel();
        }
    }
}
