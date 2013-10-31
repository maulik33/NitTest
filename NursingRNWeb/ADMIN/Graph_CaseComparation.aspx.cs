using System;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;

public partial class Graph_CaseComparation : ReportPageBase<IReportGraphCaseComparisonView, ReportGraphCaseComparisonPresentor>, IReportGraphCaseComparisonView
{
    public void WriteGraphData(string graphData)
    {
        Response.Expires = 0;
        Response.ContentType = "text/xml";
        Response.Write(graphData);
    }

    public override void PreInitialize()
    {
        ////throw new NotImplementedException();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Presenter.PopulateCohortGraph();
    }
}
