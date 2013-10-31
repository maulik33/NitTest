using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;
using NursingRNWeb.AppCode.Report_Ds;
using Entities = NursingLibrary.Entity;

public partial class ADMIN_ReportCohortByTest : ReportPageBase<IReportCohortByTestView, CohortByTestPresenter>, IReportCohortByTestView
{
    private ReportDocument rpt = new ReportDocument();

    public string Mode
    {
        get
        {
            return hdnMode.Value;
        }
    }

    /// <summary>
    /// Preinitialize the control.
    /// </summary>
    public override void PreInitialize()
    {
        Presenter.PreInitialize();
        MapControl(ddlProgramofStudy, ddInstitution, lbxCohort, lbxGroup, lbxProducts, lbxTests);
    }

    #region IReportCohortByTestView Methods

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

    public void PopulateInstitutions(IEnumerable<Entities.Institution> institutions)
    {
        ControlHelper.PopulateInstitutions(ddInstitution, institutions,true);
    }

    public void PopulateProducts(IEnumerable<Entities.Product> products)
    {
        ControlHelper.PopulateProducts(lbxProducts, products);
    }

    public void PopulateCohorts(IEnumerable<Entities.Cohort> cohorts)
    {
        ControlHelper.PopulateCohorts(lbxCohort, cohorts);
    }

    public void PopulateGroup(IEnumerable<Entities.Group> groups)
    {
        ControlHelper.PopulateGroups(lbxGroup, groups);
    }

    public void PopulateTests(IEnumerable<Entities.UserTest> tests)
    {
        ControlHelper.PopulateTestsByTestId(lbxTests, tests);
    }
    
    public void GenerateReport()
    {
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Aggregate Reports > Student Report Card")
                            .Add("Institution Id", ddInstitution.SelectedValue)
                            .Add("Cohort Id", lbxCohort.SelectedValue)
                            .Add("Group Id", lbxGroup.SelectedValue)
                            .Add("Product Id", lbxProducts.SelectedValue)
                            .Add("Test Id", lbxTests.SelectedValue)
                            .Write();
        #endregion
        Presenter.GenerateReport();
    }

    public void GenerateReport(IEnumerable<NursingLibrary.Entity.CohortByTest> reportData, ReportAction printActions)
    {
        rpt.Load(Server.MapPath("~/Admin/Report/CohortByTest.rpt"));
        rpt.SetDataSource(BuildDataSourceForReport(KTPSort.Sort<NursingLibrary.Entity.CohortByTest>(reportData, SortHelper.Parse(hdnGridConfig.Value))));

        switch (printActions)
        {
            case ReportAction.ExportExcel:
                rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Page.Response, true, "CohortByTest");
                break;
            case ReportAction.ExportExcelDataOnly:
                rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                rpt.ReportDefinition.Sections["Section5"].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Page.Response, true, "CohortByTest");
                break;
            case ReportAction.PDFPrint:
                rpt.ReportDefinition.Sections[2].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Page.Response, true, "CohortByTest");
                break;
            case ReportAction.ShowPreview:
                CrystalReportViewer1.ReportSource = rpt;
                CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.ActiveX;
                break;
            case ReportAction.DirectPrint:
                rpt.PrintToPrinter(1, false, 0, 0);
                break;
        }
    }

    public void RenderReport(IEnumerable<NursingLibrary.Entity.CohortByTest> reportData)
    {
        gvCohorts.DataSource = KTPSort.Sort<NursingLibrary.Entity.CohortByTest>(reportData, SortHelper.Parse(hdnGridConfig.Value));
        gvCohorts.DataBind();

        if (gvCohorts.Rows.Count == 0)
        {
            lblM.Visible = true;
            gvCohorts.Visible = false;
        }
        else
        {
            gvCohorts.Visible = true;
            lblM.Visible = false;
        }
    }

    public void PopulateProgramOfStudies(IEnumerable<Entities.ProgramofStudy> programOfStudies)
    {
        ControlHelper.PopulateProgramofStudy(ddlProgramofStudy, programOfStudies);
        HideProgramofStudy();
    }

    #endregion

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Mode"] == "1")
        {
            hdnMode.Value = "1";
        }
        else
        {
            hdnMode.Value = string.Empty;
        }

        if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Aggregate Reports > Cohort by Test");
            #endregion
        }
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Unload"/> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains event data.</param>
    protected override void OnUnload(EventArgs e)
    {
        base.OnUnload(e);
        rpt.Close();
        rpt.Dispose();
    }
   
    /// <summary>
    /// Handles the RowCommand event of the gvCohorts control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCommandEventArgs"/> instance containing the event data.</param>
    protected void gvCohorts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!e.CommandName.Equals("Sort") && !e.CommandName.Equals("Page"))
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvCohorts.Rows[index];

            int id = Convert.ToInt32(gvCohorts.DataKeys[row.RowIndex].Values["TestID"].ToString());
            int pid = Convert.ToInt32(gvCohorts.DataKeys[row.RowIndex].Values["ProductID"].ToString());

            switch (e.CommandName)
            {
                case "Questions":
                    Presenter.NavigateToCohortTestByQuestion(Convert.ToString(gvCohorts.DataKeys[row.RowIndex].Values["CohortId"]), ddInstitution.SelectedValue, pid, id, 1,Convert.ToInt32(ddlProgramofStudy.SelectedValue));
                    break;
            }
        }
    }

    /// <summary>
    /// Handles the Sorting1 event of the gvCohorts control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewSortEventArgs"/> instance containing the event data.</param>
    protected void gvCohorts_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);

        GenerateReport();

        switch (e.SortExpression)
        {
            case "CohortName":
                gvCohorts.HeaderRow.Cells[0].BackColor = Color.Pink;
                break;
            case "TestName":
                gvCohorts.HeaderRow.Cells[1].BackColor = Color.Pink;
                break;
            case "NStudents":
                gvCohorts.HeaderRow.Cells[2].BackColor = Color.Pink;
                break;
            case "Percentage":
                gvCohorts.HeaderRow.Cells[3].BackColor = Color.Pink;
                break;
            case "NormedPercCorrect":
                gvCohorts.HeaderRow.Cells[4].BackColor = Color.Pink;
                break;
        }
    }

    /// <summary>
    /// Handles the RowDataBound event of the gvCohorts control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
    protected void gvCohorts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int id = Convert.ToInt32(gvCohorts.DataKeys[e.Row.RowIndex].Values["TestID"].ToString());
            int pid = Convert.ToInt32(gvCohorts.DataKeys[e.Row.RowIndex].Values["ProductID"].ToString());
            int cohortId = Convert.ToInt32(gvCohorts.DataKeys[e.Row.RowIndex].Values["CohortId"].ToString());
            var selectedGroups = lbxGroup.SelectedValuesText;
            selectedGroups = selectedGroups.Replace(",", "|");
            LinkButton lnkbtn = new LinkButton();
            lnkbtn = (LinkButton)e.Row.Cells[2].FindControl("lnkbuttonPerformance");
            lnkbtn.PostBackUrl = "ReportCohortPerformance.aspx?Id=" + cohortId
                +"&ProgramofStudyId="+ddlProgramofStudy.SelectedValue + "&InstitutionId=" + ddInstitution.SelectedValue + "&ProductId=" + pid + "&TestId=" + id + "&GroupId=" + selectedGroups + "&Mode=" + hdnMode.Value;
        }
    }

    /// <summary>
    /// Handles the Click event of the btnPrint control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
    protected void btnPrint_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.GenerateReport(ReportAction.DirectPrint);
    }

    /// <summary>
    /// Handles the Click event of the btnPrintExcelDataOnly control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
    protected void btnPrintExcelDataOnly_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.GenerateReport(ReportAction.ExportExcelDataOnly);
    }

    /// <summary>
    /// Handles the Click event of the btnPrintExcel control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
    protected void btnPrintExcel_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.GenerateReport(ReportAction.ExportExcel);
    }

    /// <summary>
    /// Handles the Click event of the btnPrintPDF control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
    protected void btnPrintPDF_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.GenerateReport(ReportAction.PDFPrint);
    }

    /// <summary>
    /// Handles the Click event of the btnSubmit control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        GenerateReport();
    }

    #region Private Methods

    private DataSet BuildDataSourceForReport(IEnumerable<NursingLibrary.Entity.CohortByTest> reportData)
    {
        CohortByTest ds = new CohortByTest();
        CohortByTest.HeadRow rh = ds.Head.NewHeadRow();
        rh.InstitutionName = ddInstitution.SelectedItemsText;
        rh.CohortName = lbxCohort.SelectedItemsText;
        rh.TestType = lbxProducts.SelectedItemsText;
        rh.ReportName = "Cohort by Test";
        ds.Head.Rows.Add(rh);

        foreach (NursingLibrary.Entity.CohortByTest r in reportData)
        {
            CohortByTest.DetailRow rd = ds.Detail.NewDetailRow();
            rd.TestName = r.TestName;
            rd.NStudents = r.NStudents;
            rd.PercentageCorrect = Convert.ToDecimal(r.Percentage);
            rd.CohortName = r.CohortName;
            rd.GroupName = r.GroupName;
            rd.NormedPercCorrect = Convert.ToString(r.NormedPercCorrect);
            if (rd.NormedPercCorrect.Contains("."))
            {
                rd.NormedPercCorrect = rd.NormedPercCorrect;
            }
            else
            {
                rd.NormedPercCorrect = rd.NormedPercCorrect + ".0";
            }

            rd.HeadID = rh.HeadID;
            ds.Detail.Rows.Add(rd);
        }

        return ds;
    }

    private void HideProgramofStudy()
    {
        trProgramofStudy.Visible = Presenter.CurrentContext.UserType == UserType.SuperAdmin || Presenter.IsMultipleProgramofStudyAssignedToAdmin(Presenter.CurrentContext.UserId); 
    }

    #endregion
}
