using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class ADMIN_ReportInstitutionByStateAndType : ReportPageBase<IReportInstitutionByStateView, ReportInstitutionByStatePresenter>, IReportInstitutionByStateView
{
    private ReportDocument reportInstitution = new ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        #region Trace Information
        TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Reports for Administrators > Institutions by State ");
        #endregion
    }

    public override void PreInitialize()
    {
        Presenter.PreInitialize();
    }

    public void PopulateStates(IEnumerable<string> states)
    {
        ControlHelper.PopulateStates(ddState, states);
    }

    protected void gvInstitutions_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);
        if (ddType.SelectedValue != "Select All")
        {
            Presenter.GenerateReport(ddState.SelectedValue, ddType.SelectedValue);
        }
        else
        {
            Presenter.GenerateReport(ddState.SelectedValue, string.Empty);
        }
    }

    public void RenderReport(IEnumerable<InstitutionByState> reportData)
    {
        gvInstitutions.DataSource = KTPSort.Sort<InstitutionByState>(reportData, SortHelper.Parse(hdnGridConfig.Value));
        gvInstitutions.DataBind();

        if (reportData.Count() == 0)
        {
            lblMessage.Visible = true;
            gvInstitutions.Visible = false;
        }
        else
        {
            gvInstitutions.Visible = true;
        }
    }

    public void RenderReport(IEnumerable<InstitutionByState> reportData, ReportAction printActions)
    {
        var sortedData = KTPSort.Sort<InstitutionByState>(reportData, SortHelper.Parse(hdnGridConfig.Value));

        reportInstitution.Load(Server.MapPath("~/Admin/Report/InstitutionsByState.rpt"));
        reportInstitution.SetDataSource(reportData);

        switch (printActions)
        {
            case ReportAction.ExportExcelDataOnly:
                reportInstitution.ReportDefinition.Sections[5].SectionFormat.EnableSuppress = true;
                reportInstitution.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Page.Response, true, "InstitutionByState");
                break;

            case ReportAction.ExportExcel:
                reportInstitution.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                reportInstitution.ReportDefinition.Sections[5].SectionFormat.EnableSuppress = true;
                reportInstitution.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Page.Response, true, "InstitutionByState");
                break;

            case ReportAction.PDFPrint:
                reportInstitution.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Page.Response, true, "InstitutionByState");
                break;
        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (ddType.SelectedValue != "Select All")
        {
            Presenter.GenerateReport(ddState.SelectedValue, ddType.SelectedValue);
        }
        else
        {
            Presenter.GenerateReport(ddState.SelectedValue, string.Empty);
        }
    }

    protected void btnPrinterFriendly_Click(object sender, ImageClickEventArgs e)
    {
        if (ddType.SelectedValue != "Select All")
        {
            Presenter.GenerateReport(ReportAction.PDFPrint, ddState.SelectedValue, ddType.SelectedValue);
        }
        else
        {
            Presenter.GenerateReport(ReportAction.PDFPrint, ddState.SelectedValue, string.Empty);
        }
    }

    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        if (ddType.SelectedValue != "Select All")
        {
            Presenter.GenerateReport(ReportAction.ExportExcel, ddState.SelectedValue, ddType.SelectedValue);
        }
        else
        {
            Presenter.GenerateReport(ReportAction.ExportExcel, ddState.SelectedValue, string.Empty);
        }
    }

    protected void btnExcelData_Click(object sender, ImageClickEventArgs e)
    {
        if (ddType.SelectedValue != "Select All")
        {
            Presenter.GenerateReport(ReportAction.ExportExcelDataOnly, ddState.SelectedValue, ddType.SelectedValue);
        }
        else
        {
            Presenter.GenerateReport(ReportAction.ExportExcelDataOnly, ddState.SelectedValue, string.Empty);
        }
    }
}
