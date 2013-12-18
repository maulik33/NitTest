using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;
using NursingRNWeb.AppCode.Report_Ds;

public partial class ADMIN_ReportCohortTestByQuestion : ReportPageBase<IReportTestResultByQuestionView, ReportTestResultByQuestionPresenter>, IReportTestResultByQuestionView
{
    private ReportDocument _reportDocument = new ReportDocument();
    private bool _isExportToExcel = false;
    private bool _isExportToExcelDataOnly = false;

    public bool IsInstitutionIdExistInQueryString { get; set; }

    public bool IsTestTypeIdExistInQueryString { get; set; }

    public bool IsTestIdExistInQueryString { get; set; }

    public bool IsCohortIdExistInQueryString { get; set; }

    public bool IsRTypeExistInQueryString { get; set; }

    public bool IsModeExistInQueryString { get; set; }

    public bool IsProgramofStudyExistInQueryString { get; set; }

    public bool IsSelectedCohorts { get; set; }

    public int CohortNumberOfAPage
    {
        get
        {
            return 7;
        }
    }

    public int CohortStartIndex { get; set; }

    public int CohortEndIndex { get; set; }

    public string Mode
    {
        get
        {
            return hdnMode.Value;
        }

        set
        {
            hdnMode.Value = value;
        }
    }

    public string RType { get; set; }

    private string sort
    {
        get
        {
            object o = this.ViewState["sort"];
            if (o == null)
            {
                return "TopicTitle";
            }
            else
            {
                return o.ToString();
            }
        }

        set
        {
            this.ViewState["sort"] = value;
        }
    }

    public override void PreInitialize()
    {
        Presenter.PreInitialize();
        MapControl(ddlProgramofStudy,ddInstitution, ddCohorts, ddProducts, ddTests);
    }

    #region IReportTestResultByQuestionView Methods

    public bool PostBack
    {
        get
        {
            return IsPostBack;
        }
    }

    public void PopulateInstitutions(IEnumerable<Institution> institutions)
    {
        ControlHelper.PopulateInstitutions(ddInstitution, institutions,true);
    }

    public void PopulateProducts(IEnumerable<Product> products)
    {
        ControlHelper.PopulateProducts(ddProducts, products);
    }

    public void PopulateCohorts(IEnumerable<Cohort> cohorts)
    {
        ControlHelper.PopulateCohorts(ddCohorts, cohorts);
    }

    public void PopulateTests(IEnumerable<UserTest> tests)
    {
        ControlHelper.PopulateTestsByTestId(ddTests, tests);
    }

    public bool IsProgramofStudyVisible
    {
        get { return trProgramofStudy.Visible; }
        set { trProgramofStudy.Visible = value; }
    }

    public void GenerateReport()
    {
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Aggregate Reports > Test Result by Question")
                            .Add("Institution Id", ddInstitution.SelectedValue)
                            .Add("Cohort Id", ddCohorts.SelectedValue)
                            .Add("Product Id", ddProducts.SelectedValue)
                            .Add("Test Id", ddTests.SelectedValue)
                            .Write();
        #endregion
        Presenter.GenerateReport();
    }

    public void ExportReport(DataTable reportData, ReportAction printActions)
    {
        if (ddCohorts.SelectedItems.Count() > 1)
        {
            if (_isExportToExcelDataOnly)
            {
                ExportToExcelDataOnly(reportData);
            }
            else if (_isExportToExcel)
            {
                ExportToExcelForAllCohorts(reportData);
            }
            else if (IsSelectedCohorts)
            {
                ShowReportForSelectedCohorts(reportData);
            }
            else
            {
                ExportToExcel(reportData);
            }
        }
        else
        {
            _reportDocument.Load(Server.MapPath("~/Admin/Report/TestResultsbyQuestion.rpt"));
            _reportDocument.SetDataSource(BuildDataSourceForReport(reportData));

            switch (printActions)
            {
                case ReportAction.ExportExcelDataOnly:
                    _reportDocument.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                    _reportDocument.ReportDefinition.Sections["Section5"].SectionFormat.EnableSuppress = true;
                    _reportDocument.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Page.Response, true, "TestResultsbyQuestion");
                    break;

                case ReportAction.ExportExcel:
                    _reportDocument.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                    _reportDocument.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Page.Response, true, "TestResultsbyQuestion");
                    break;

                case ReportAction.PDFPrint:
                    _reportDocument.ReportDefinition.Sections[2].SectionFormat.EnableSuppress = true;
                    _reportDocument.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Page.Response, true, "TestResultsbyQuestion");
                    break;

                case ReportAction.ShowPreview:
                    CrystalReportViewer1.ReportSource = _reportDocument;
                    CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.ActiveX;
                    break;
            }
        }
    }

    public void RenderReport(DataTable reportData)
    {
        if (reportData != null)
        {
            BoundField d1 = new BoundField();
            gvCohorts.Visible = true;
            gvCohorts.Columns.Clear();
            for (int i = 1; i < reportData.Columns.Count; ++i)
            {
                BoundField c1 = new BoundField();
                string headerName = reportData.Columns[i].ColumnName;
                if (headerName == "TopicTitle")
                {
                    c1.HeaderText = "Topic Title";
                }
                else if (headerName == "LevelOfDifficulty")
                {
                    c1.HeaderText = "Level of Difficulty";
                }
                else if (headerName == "Q_Norming")
                {
                    c1.HeaderText = "National Results";
                }
                else
                {
                    c1.HeaderText = reportData.Columns[i].ColumnName;
                }

                c1.DataField = reportData.Columns[i].ColumnName;
                c1.SortExpression = reportData.Columns[i].ColumnName;
                gvCohorts.Columns.Add(c1);
            }

            this.sort = this.sort == string.Empty ? "TopicTitle" : this.sort;
            reportData.DefaultView.Sort = this.sort;
            gvCohorts.DataSource = reportData.DefaultView;
            gvCohorts.DataBind();

            if (reportData.Rows.Count == 0)
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

    public void HideButton()
    {
        Button1.Visible = false;
        Button2.Visible = false;
        Button3.Visible = false;
    }

    public void PopulateProgramOfStudies(IEnumerable<ProgramofStudy> programOfStudies)
    {
        ControlHelper.PopulateProgramofStudy(ddlProgramofStudy, programOfStudies);
        HideProgramofStudy();
    }

   #endregion

    protected void gvCohorts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gv = (GridView)sender;
            int rowIndex = Convert.ToInt32(e.Row.RowIndex);
            var _cellCount = e.Row.Cells.Count;
            for (int i = 0; i < _cellCount; i++)
            {
                if (i > 1)
                {
                    e.Row.Cells[i].Text = e.Row.Cells[i].Text == "-100.0" ? "N/A" : e.Row.Cells[i].Text + "%";
                }
            }
        }
    }

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        #region Check Querystring Parameters
        if (!string.IsNullOrEmpty(Page.Request.QueryString["InstitutionId"]))
        {
            IsInstitutionIdExistInQueryString = true;
        }
        else
        {
            IsInstitutionIdExistInQueryString = false;
        }

        if (!string.IsNullOrEmpty(Page.Request.QueryString["Id"]))
        {
            IsCohortIdExistInQueryString = true;
        }
        else
        {
            IsCohortIdExistInQueryString = false;
        }

        if (!string.IsNullOrEmpty(Page.Request.QueryString["ProductID"]))
        {
            IsTestTypeIdExistInQueryString = true;
        }
        else
        {
            IsTestTypeIdExistInQueryString = false;
        }

        if (!string.IsNullOrEmpty(Page.Request.QueryString["TestID"]))
        {
            IsTestIdExistInQueryString = true;
        }
        else
        {
            IsTestIdExistInQueryString = false;
        }

        if (!string.IsNullOrEmpty(Page.Request.QueryString["RType"]))
        {
            IsRTypeExistInQueryString = true;
        }
        else
        {
            IsRTypeExistInQueryString = false;
        }

        if (!string.IsNullOrEmpty(Page.Request.QueryString["Mode"]))
        {
            IsModeExistInQueryString = true;
        }
        else
        {
            IsModeExistInQueryString = false;
        }

        if (!string.IsNullOrEmpty(Page.Request.QueryString["ProgramofStudy"]))
        {
            IsProgramofStudyExistInQueryString = true;
        }
        else
        {
            IsProgramofStudyExistInQueryString = false;
        }
        #endregion
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Aggregate Reports > Test Result by Question  ");
            #endregion
            if (RType.Trim().Equals("1"))
            {
                GenerateReport();
            }
        }
    }

    protected override void OnUnload(EventArgs e)
    {
        _reportDocument.Close();
        _reportDocument.Dispose();
    }

    protected void gvCohorts_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (this.sort == e.SortExpression)
        {
            this.sort += " DESC";
        }
        else
        {
            this.sort = e.SortExpression;
        }

        GenerateReport();

        #region Change color of sorted column header
        for (int i = 0; i < gvCohorts.Columns.Count; i++)
        {
            if (!string.IsNullOrEmpty(gvCohorts.Columns[i].SortExpression) && gvCohorts.Columns[i].SortExpression == e.SortExpression)
            {
                gvCohorts.HeaderRow.Cells[i].BackColor = Color.Pink;
                break;
            }
        }
        #endregion
    }

    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        ResetButtonFlags();

        if (ddCohorts.SelectedValue == Constants.LIST_SELECT_ALL_VALUE)
        {
            Presenter.GenerateReport(ReportAction.ExportExcel);
        }
        else
        {
            Presenter.GenerateReport(ReportAction.ExportExcelDataOnly);
        }
    }

    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        ResetButtonFlags();
        _isExportToExcel = true;
        Presenter.GenerateReport(ReportAction.ExportExcel);
    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        ResetButtonFlags();
        _isExportToExcelDataOnly = true;

        if (ddCohorts.SelectedValue == Constants.LIST_SELECT_ALL_VALUE)
        {
            Presenter.GenerateReport(ReportAction.ExportExcel);
        }
        else
        {
            Presenter.GenerateReport(ReportAction.PDFPrint);
        }
    }

    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        this.sort = null;
        GenerateReport();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        SetPageSettings(1);

        Presenter.GenerateReport(ReportAction.PDFPrint);
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        SetPageSettings(2);
        Presenter.GenerateReport(ReportAction.PDFPrint);
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        SetPageSettings(3);
        Presenter.GenerateReport(ReportAction.PDFPrint);
    }

    #region Private Methods

    private void ResetButtonFlags()
    {
        _isExportToExcel = false;
        _isExportToExcelDataOnly = false;
        IsSelectedCohorts = false;
    }

    private void SetPageSettings(int pageNumber)
    {
        ResetButtonFlags();
        IsSelectedCohorts = true;

        switch (pageNumber)
        {
            case 1:
                if (ddCohorts.Items.Count - 2 >= CohortNumberOfAPage)
                {
                    CohortStartIndex = 0;
                    CohortEndIndex = CohortNumberOfAPage;
                }
                else
                {
                    CohortStartIndex = 0;
                    CohortEndIndex = ddCohorts.SelectedValuesText.Split('|').Length;
                }

                break;
            case 2:
                if (ddCohorts.Items.Count - 2 >= CohortNumberOfAPage * 2)
                {
                    CohortStartIndex = CohortNumberOfAPage;
                    CohortEndIndex = CohortNumberOfAPage * 2;
                }
                else
                {
                    CohortStartIndex = CohortNumberOfAPage;
                    CohortEndIndex = ddCohorts.SelectedValuesText.Split('|').Length;
                }

                break;
            case 3:
                if (ddCohorts.Items.Count - 2 >= CohortNumberOfAPage * 3)
                {
                    CohortStartIndex = 2 * CohortNumberOfAPage;
                    CohortEndIndex = CohortNumberOfAPage * 3;
                }
                else
                {
                    CohortStartIndex = 2 * CohortNumberOfAPage;
                    CohortEndIndex = ddCohorts.SelectedValuesText.Split('|').Length;
                }

                break;
        }
    }

    private void ShowReportForSelectedCohorts(DataTable reportData)
    {
        CreateReport(reportData, CohortStartIndex, CohortEndIndex);
    }

    private DataSet BuildDataSourceForReport(DataTable reportData)
    {
        int selectedCohortColumn = (reportData != null) ? reportData.Columns.Count - 1 : 4;
        TestResultsbyQuestion ds = new TestResultsbyQuestion();
        TestResultsbyQuestion.HeadRow rh = (TestResultsbyQuestion.HeadRow)ds.Head.NewRow();
        rh.InstitutionName = ddInstitution.SelectedItemsText;
        rh.CohortName = ddCohorts.SelectedItemsText;
        rh.TestType = ddProducts.SelectedItemsText;
        rh.TestName = ddTests.SelectedItemsText;
        rh.ReportName = "Test Result by Question";
        ds.Head.Rows.Add(rh);

        if (reportData.Columns.Contains(this.sort))
        {
            reportData.DefaultView.Sort = this.sort;
        }
        
        foreach (System.Data.DataRowView r in reportData.DefaultView)
        {
            TestResultsbyQuestion.DetailRow rd = (TestResultsbyQuestion.DetailRow)ds.Detail.NewRow();
            rd.TopicTitle = r["TopicTitle"].ToString();
            rd.LevelOfDifficulty = r["LevelOfDifficulty"].ToString();
            rd.NationalResults = r["Q_Norming"].ToString() == "-100.0" ? "N/A" : r["Q_Norming"].ToString() + "%";
            rd.SchoolResults = r[selectedCohortColumn].ToString() == "-100.0" ? "0.0%" : r[selectedCohortColumn].ToString() + "%";
            rd.HeadID = rh.HeadID;
            ds.Detail.Rows.Add(rd);
        }

        return ds;
    }

    private DataSet BuildDataSourceForReport(DataTable reportData, int colums)
    {
        ReportTestbyQuestionForAllCohort ds = new ReportTestbyQuestionForAllCohort();
        ReportTestbyQuestionForAllCohort.HeadRow rh = ds.Head.NewHeadRow();
        rh.ReportName = "Test Results by Question";
        rh.InstitutionName = ddInstitution.SelectedItemsText;
        rh.TestType = ddProducts.SelectedItemsText;
        rh.TestName = ddTests.SelectedItemsText;
        rh.CohortName = ddCohorts.SelectedItemsText;
       // rh.ProgramofStudy = ddlProgramofStudy.SelectedItemsText;

        ds.Head.Rows.Add(rh);

        for (int i = 0; i < colums; ++i)
        {
            ds.Detail.Columns.Add(reportData.Columns[i + 4].ColumnName);
        }

        if (reportData.Columns.Contains(this.sort))
        {
            reportData.DefaultView.Sort = this.sort;
        }

        foreach (DataRow r in reportData.Rows)
        {
            ReportTestbyQuestionForAllCohort.DetailRow rd = ds.Detail.NewDetailRow();
            rd.TopicTitle = r["TopicTitle"].ToString();
            rd.LevelOfDifficulty = r["LevelOfDifficulty"].ToString();
            rd.NationalResults = r["Q_Norming"].ToString() == "-100.0" ? "N/A" : r["Q_Norming"].ToString() + "%";
            for (int i = 0; i < colums; ++i)
            {
                rd[i + 5] = r[4 + i].ToString() == "-100.0" ? "N/A" : r[4 + i].ToString() + "%";
            }

            rd.HeadID = rh.HeadID;
            ds.Detail.Rows.Add(rd);
        }

        return ds;
    }

    private void CreateReport(DataTable reportData, int startNumber, int endNumber)
    {
        _reportDocument.Load(Server.MapPath("~/Admin/Report/TestResultsbyQuestionForAllCohort.rpt"));
        _reportDocument.SetDataSource(BuildDataSourceForReport(reportData, endNumber - startNumber));
        for (int i = 0; i < CohortNumberOfAPage; ++i)
        {
            if ((i + startNumber) < endNumber)
            {
                string fName = "F" + (i + 1).ToString();
                string cName = "{Detail." + reportData.Columns[i + 4].ColumnName + "}";
                _reportDocument.DataDefinition.FormulaFields[fName].Text = cName;
                _reportDocument.ParameterFields["P" + (i + 1).ToString()].CurrentValues.AddValue(reportData.Columns[i + 4].ColumnName);
            }
            else
            {
                _reportDocument.ParameterFields["P" + (i + 1).ToString()].CurrentValues.AddValue(string.Empty);
            }
        }

        _reportDocument.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Page.Response, true, "TestResultsbyQuestionForAllCohort");

        Button1.Enabled = false;
    }

    private void ExportToExcel(DataTable reportData)
    {
        reportData.DefaultView.Sort = "TopicTitle";
        DataTable dtSort = reportData.DefaultView.ToTable();
        StringBuilder excelData = new StringBuilder();
        string sep = string.Empty;
        excelData.Append(sep + " Test Result By Question report\t\t\t\t\t\n");
        foreach (DataColumn dc in dtSort.Columns)
        {
            if (dc.ColumnName != "QuestionID")
            {
                excelData.Append(sep + dc.ColumnName);
                sep = "\t";
            }
        }

        excelData.Append("\n");

        foreach (DataRow dr in dtSort.Rows)
        {
            sep = string.Empty;
            for (var i = 1; i < dtSort.Columns.Count; i++)
            {
                if (i < 3)
                {
                    excelData.Append(sep + dr[i].ToString());
                }
                else
                {
                    var data = dr[i].ToString() == "-100.0" ? "N/A" : dr[i].ToString() + "%";
                    excelData.Append(sep + data);
                }

                sep = "\t";
            }

            excelData.Append("\n");
        }

        ReportHelper.ExportToExcel(excelData.ToString(), "TestResultByQuestion.xls");
    }

    private void ExportToExcelForAllCohorts(DataTable reportData)
    {
        int columCount = 4;

        int colums = ddCohorts.SelectedItems.Count();

        _reportDocument.Load(Server.MapPath("~/Admin/Report/TestResultsbyQuestionForAllCohortForExcel.rpt"));
        _reportDocument.SetDataSource(BuildDataSourceForReport(reportData, colums));
        for (int i = 0; i < 20; ++i)
        {
            //// 7 is TestResultsbyQuestionForAllCohortForExcel formular number
            if (i < colums)
            {
                string fName = "F" + (i + 1).ToString();
                string cName = "{Detail." + reportData.Columns[i + columCount].ColumnName + "}";
                _reportDocument.DataDefinition.FormulaFields[fName].Text = cName;
                _reportDocument.ParameterFields["P" + (i + 1).ToString()].CurrentValues.AddValue(reportData.Columns[i + columCount].ColumnName);
            }
            else
            {
                _reportDocument.ParameterFields["P" + (i + 1).ToString()].CurrentValues.AddValue(string.Empty);
            }
        }

        _reportDocument.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Page.Response, true, "TestResultsbyQuestionForAllCohortForExcel");
    }

    private void ExportToExcelDataOnly(DataTable reportData)
    {
        int columCount = 4;

        int colums = ddCohorts.SelectedItems.Count();

        if (colums <= CohortNumberOfAPage)
        {
            _reportDocument.Load(Server.MapPath("~/Admin/Report/TestResultsbyQuestionForAllCohort.rpt"));
            _reportDocument.SetDataSource(BuildDataSourceForReport(reportData, colums));
            for (int i = 0; i < CohortNumberOfAPage; ++i)
            {
                if (i < colums)
                {
                    string fName = "F" + (i + 1).ToString();
                    string cName = "{Detail." + reportData.Columns[i + columCount].ColumnName + "}";
                    _reportDocument.DataDefinition.FormulaFields[fName].Text = cName;
                    _reportDocument.ParameterFields["P" + (i + 1).ToString()].CurrentValues.AddValue(reportData.Columns[i + columCount].ColumnName);
                }
                else
                {
                    _reportDocument.ParameterFields["P" + (i + 1).ToString()].CurrentValues.AddValue(string.Empty);
                }
            }

            _reportDocument.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Page.Response, true, "TestResultsbyQuestionForAllCohort");
        }
        else
        {
            int nButton = 0;
            if ((colums % CohortNumberOfAPage) == 0)
            {
                nButton = colums / CohortNumberOfAPage;
            }
            else
            {
                nButton = (colums / CohortNumberOfAPage) + 1;
            }

            switch (nButton)
            {
                case 2:
                    Button1.Visible = true;
                    Button2.Visible = true;
                    break;
                case 3:
                    Button1.Visible = true;
                    Button2.Visible = true;
                    Button3.Visible = true;
                    break;
                default:
                    {
                        lblM.Text = "Can't print more than 75 cohorts";
                        lblM.Visible = true;
                        break;
                    }
            }
        }
    }

    private void HideProgramofStudy()
    {
        trProgramofStudy.Visible = IsProgramofStudyVisible = Presenter.CurrentContext.UserType == UserType.SuperAdmin || Presenter.IsMultipleProgramofStudyAssignedToAdmin(Presenter.CurrentContext.UserId);
    }

    #endregion
}
