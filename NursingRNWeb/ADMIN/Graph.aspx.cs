using System;
using System.Collections.Generic;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class STUDENT_Graph : ReportPageBase<IReportGraphView, ReportGraphPresenter>, IReportGraphView
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

    public void PopulateGroup(IEnumerable<Group> groups)
    {
    }

    public void PopulateStudent(IEnumerable<StudentEntity> students)
    {
    }

    public void GenerateReport()
    {
    }

    public void ReturnGraphData(string graphData)
    {
        Response.Expires = 0;
        Response.ContentType = "text/xml";

        Response.Write(graphData);
    }

    public void PopulateProgramOfStudies(IEnumerable<ProgramofStudy> programOfStudies)
    {
        throw new NotImplementedException();
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Graph Page");
            #endregion
        }

        Presenter.GetGraphData();
    }  
}
