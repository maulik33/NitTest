using System;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class ADMIN_Graph_C : ReportPageBase<IReportGraphCView, ReportGraphCPresenter>, IReportGraphCView
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
        if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Graph_C Page");
            #endregion
        }

        Presenter.PopulateCohortGraph();
    }
}
