using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;
using NursingRNWeb.AppCode.Report_Ds;
using NursingLibrary.DTC;

public partial class ADMIN_ReportInstitutionTestByQuestion : ReportPageBase<IReportInstitutionTestByQuestionView, ReportInstitutionTestByQuestionPresenter>, IReportInstitutionTestByQuestionView
{
    private ReportDocument _reportDocument = new ReportDocument();

    private bool _isExportToExcel = false;

    private bool _isPrintToPDF = false;

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

    public override void PreInitialize()
    {
        Presenter.PreInitialize();
        MapControl(ddlProgramofStudy, lbInstitution, lbCohort, ddProducts, ddTests);
    }


    #region IReportInstitutionTestByQuestionView Methods

    public void PopulateProgramOfStudies(IEnumerable<ProgramofStudy> programOfStudies)
    {
        ControlHelper.PopulateProgramofStudy(ddlProgramofStudy, programOfStudies);
        HideProgramofStudy();
    }

    public void PopulateInstitutions(IEnumerable<Institution> institutions)
    {
        ControlHelper.PopulateInstitutions(lbInstitution, institutions, true);
    }

    public void PopulateProducts(IEnumerable<Product> products)
    {
        ControlHelper.PopulateProducts(ddProducts, products);
    }

    public void PopulateCohorts(IEnumerable<Cohort> cohorts)
    {
        ControlHelper.PopulateCohorts(lbCohort, cohorts);
    }

    public void PopulateTests(IEnumerable<UserTest> tests)
    {
        ControlHelper.PopulateTestsByTestId(ddTests, tests);
    }

    public void GenerateReport()
    {
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Aggregate Reports > Institutional Test Result by Question")
                           .Add("Institution Id", lbInstitution.SelectedValue)
                           .Add("Cohort Id", lbCohort.SelectedValue)
                           .Add("Product Id", ddProducts.SelectedValue)
                           .Add("Test Name", ddTests.SelectedValue)
                           .Write();
        #endregion
        Presenter.GenerateReport();
    }

    public void ExportReport(DataTable reportData, ReportAction printActions)
    {
        if (_isPrintToPDF)
        {
            PrintToPDF(reportData);
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
            ExportToExcelDataOnly(reportData);
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
                    String nStudents = string.Empty;
                    var reportCol = reportData.Columns[i].ColumnName.LastIndexOf('/');
                    string[] names = reportData.Columns[i].ColumnName.Split('/');
                    int cohortId = names[names.Length - 1].ToInt();
                    nStudents = "(N=" + Presenter.GetStudentNumberByCohortTest(cohortId) + ")";
                    c1.HeaderText = reportData.Columns[i].ColumnName.Substring(0, reportCol) + nStudents;
                    c1.DataField = reportData.Columns[i].ColumnName.Substring(0, reportCol);
                    c1.SortExpression = reportData.Columns[i].ColumnName.Substring(0, reportCol);
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
        Button4.Visible = false;
        Button5.Visible = false;
        Button6.Visible = false;
        Button7.Visible = false;
        Button8.Visible = false;
        Button9.Visible = false;
        Button10.Visible = false;
        Button11.Visible = false;
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
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Aggregate Reports > Test Result by Question  ");
            #endregion
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

        Presenter.GenerateReport(ReportAction.ExportExcelDataOnly);
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
        _isPrintToPDF = true;
        Presenter.GenerateReport(ReportAction.PDFPrint);
    }

    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        this.sort = null;
        GenerateReport();
    }

    protected void PrintCohort_Click(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        SetPageSettings(Convert.ToInt32(btn.CommandArgument));
        Presenter.GenerateReport(ReportAction.PDFPrint);
    }

    #region Private Methods
    private void HideProgramofStudy()
    {
        IsProgramofStudyVisible = Presenter.CurrentContext.UserType == UserType.SuperAdmin || Presenter.IsMultipleProgramofStudyAssignedToAdmin(Presenter.CurrentContext.UserId);
    }

    private void ResetButtonFlags()
    {
        _isExportToExcel = false;
        _isPrintToPDF = false;
        IsSelectedCohorts = false;
    }

    private void SetPageSettings(int pageNumber)
    {
        ResetButtonFlags();
        IsSelectedCohorts = true;
        if (pageNumber == 1)
        {
            if (lbCohort.Items.Count - 1 >= CohortNumberOfAPage)
            {
                CohortStartIndex = 0;
                CohortEndIndex = CohortNumberOfAPage;
            }
            else
            {
                CohortStartIndex = 0;
                CohortEndIndex = lbCohort.SelectedValuesText.Split('|').Length;
            }
        }
        else
        {
            if (lbCohort.Items.Count - 1 >= CohortNumberOfAPage * pageNumber)
            {
                CohortStartIndex = (pageNumber - 1) * CohortNumberOfAPage;
                CohortEndIndex = CohortNumberOfAPage * pageNumber;
            }
            else
            {
                CohortStartIndex = (pageNumber - 1) * CohortNumberOfAPage;
                CohortEndIndex = lbCohort.SelectedValuesText.Split('|').Length;
            }
        }
    }

    private void ShowReportForSelectedCohorts(DataTable reportData)
    {
        CreateReport(reportData, CohortStartIndex, CohortEndIndex);
    }

    private DataSet BuildDataSourceForReport(DataTable reportData, int colums)
    {
        var ds = new ReportTestbyQuestionForAllCohort();
        var rh = ds.Head.NewHeadRow();
        rh.ReportName = "Institutional Test Result by Question";
        rh.InstitutionName = lbInstitution.SelectedItemsText;
        rh.TestType = ddProducts.SelectedItemsText;
        rh.TestName = ddTests.SelectedItemsText;
        rh.CohortName = lbCohort.SelectedItemsText;

        ds.Head.Rows.Add(rh);

        for (int i = 0; i < colums; ++i)
        {
            var columnName = reportData.Columns[i + 4].ColumnName.LastIndexOf('/');
            ds.Detail.Columns.Add(reportData.Columns[i + 4].ColumnName.Substring(0, columnName));
        }

        if (reportData.Columns.Contains(this.sort))
        {
            reportData.DefaultView.Sort = this.sort;
        }

        foreach (DataRow r in reportData.Rows)
        {
            var rd = ds.Detail.NewDetailRow();
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
                var colLength = reportData.Columns[i + 4].ColumnName.LastIndexOf('/');
                string cName = "{Detail." + reportData.Columns[i + 4].ColumnName.Substring(0, colLength) + "}";
                _reportDocument.DataDefinition.FormulaFields[fName].Text = cName;
                _reportDocument.ParameterFields["P" + (i + 1).ToString()].CurrentValues.AddValue(reportData.Columns[i + 4].ColumnName.Substring(0, colLength));
            }
            else
            {
                _reportDocument.ParameterFields["P" + (i + 1).ToString()].CurrentValues.AddValue(string.Empty);
            }
        }

        _reportDocument.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Page.Response, true, "InstitutionalTestResultsbyQuestionForAllCohort");

        Button1.Enabled = false;
    }

    private void ExportToExcelDataOnly(DataTable reportData)
    {
        reportData.DefaultView.Sort = "TopicTitle";
        DataTable dtSort = reportData.DefaultView.ToTable();
        StringBuilder excelData = new StringBuilder();
        string sep = string.Empty;
        excelData.Append(sep + " Institutional Test Result by Question Report\t\t\t\t\t\n");
        foreach (DataColumn dc in dtSort.Columns)
        {
            if (dc.ColumnName != "QuestionID")
            {
                var colLength = dc.ColumnName.LastIndexOf('/');
                if (colLength == -1)
                {
                    excelData.Append(sep + dc.ColumnName);
                }
                else
                {
                    excelData.Append(sep + dc.ColumnName.Substring(0, dc.ColumnName.LastIndexOf('/')));
                }

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

        ReportHelper.ExportToExcel(excelData.ToString(), "InstitutionalTestResultbyQuestion.xls");
    }

    private void ExportToExcelForAllCohorts(DataTable reportData)
    {
        int columCount = 4;
        var cohortColumns = lbCohort.SelectedValue == Constants.LIST_SELECT_ALL_VALUE
                     ? lbCohort.Items.Count - 1
                     : lbCohort.SelectedValuesText.Split('|').Length;

        _reportDocument.Load(Server.MapPath("~/Admin/Report/TestResultsbyQuestionForAllCohortForExcel.rpt"));
        _reportDocument.SetDataSource(BuildDataSourceForReport(reportData, cohortColumns));
        for (int i = 0; i < 20; ++i)
        {
            if (i < cohortColumns)
            {
                string fName = "F" + (i + 1).ToString();
                var colLength = reportData.Columns[i + 4].ColumnName.LastIndexOf('/');
                string cName = "{Detail." + reportData.Columns[i + columCount].ColumnName.Substring(0, colLength) + "}";
                _reportDocument.DataDefinition.FormulaFields[fName].Text = cName;
                _reportDocument.ParameterFields["P" + (i + 1).ToString()].CurrentValues.AddValue(reportData.Columns[i + columCount].ColumnName.Substring(0, colLength));
            }
            else
            {
                _reportDocument.ParameterFields["P" + (i + 1).ToString()].CurrentValues.AddValue(string.Empty);
            }
        }

        _reportDocument.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Page.Response, true, "InstitutionalTestResultbyQuestion");
    }

    private void PrintToPDF(DataTable reportData)
    {
        var columCount = 4;
        var cohortColumns = lbCohort.SelectedValue == Constants.LIST_SELECT_ALL_VALUE
                     ? lbCohort.Items.Count - 1
                     : lbCohort.SelectedValuesText.Split('|').Length;

        if (cohortColumns <= CohortNumberOfAPage)
        {
            _reportDocument.Load(Server.MapPath("~/Admin/Report/TestResultsbyQuestionForAllCohort.rpt"));
            _reportDocument.SetDataSource(BuildDataSourceForReport(reportData, cohortColumns));
            for (int i = 0; i < CohortNumberOfAPage; ++i)
            {
                if (i < cohortColumns)
                {
                    string fName = "F" + (i + 1).ToString();
                    var colLength = reportData.Columns[i + 4].ColumnName.LastIndexOf('/');
                    string cName = "{Detail." + reportData.Columns[i + columCount].ColumnName.Substring(0, colLength) + "}";
                    _reportDocument.DataDefinition.FormulaFields[fName].Text = cName;
                    _reportDocument.ParameterFields["P" + (i + 1).ToString()].CurrentValues.AddValue(reportData.Columns[i + columCount].ColumnName.Substring(0, colLength));
                }
                else
                {
                    _reportDocument.ParameterFields["P" + (i + 1).ToString()].CurrentValues.AddValue(string.Empty);
                }
            }

            _reportDocument.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Page.Response, true, "InstitutionalTestResultbyQuestion");
        }
        else
        {
            var nButton = 0;
            if ((cohortColumns % CohortNumberOfAPage) == 0)
            {
                nButton = cohortColumns / CohortNumberOfAPage;
            }
            else
            {
                nButton = (cohortColumns / CohortNumberOfAPage) + 1;
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
                case 4:
                    Button1.Visible = true;
                    Button2.Visible = true;
                    Button3.Visible = true;
                    Button4.Visible = true;
                    break;
                case 5:
                    Button1.Visible = true;
                    Button2.Visible = true;
                    Button3.Visible = true;
                    Button4.Visible = true;
                    Button5.Visible = true;
                    break;
                case 6:
                    Button1.Visible = true;
                    Button2.Visible = true;
                    Button3.Visible = true;
                    Button4.Visible = true;
                    Button5.Visible = true;
                    Button6.Visible = true;
                    break;
                case 7:
                    Button1.Visible = true;
                    Button2.Visible = true;
                    Button3.Visible = true;
                    Button4.Visible = true;
                    Button5.Visible = true;
                    Button6.Visible = true;
                    Button7.Visible = true;
                    break;
                case 8:
                    Button1.Visible = true;
                    Button2.Visible = true;
                    Button3.Visible = true;
                    Button4.Visible = true;
                    Button5.Visible = true;
                    Button6.Visible = true;
                    Button7.Visible = true;
                    Button8.Visible = true;
                    break;
                case 9:
                    Button1.Visible = true;
                    Button2.Visible = true;
                    Button3.Visible = true;
                    Button4.Visible = true;
                    Button5.Visible = true;
                    Button6.Visible = true;
                    Button7.Visible = true;
                    Button8.Visible = true;
                    Button9.Visible = true;
                    break;
                case 10:
                    Button1.Visible = true;
                    Button2.Visible = true;
                    Button3.Visible = true;
                    Button4.Visible = true;
                    Button5.Visible = true;
                    Button6.Visible = true;
                    Button7.Visible = true;
                    Button8.Visible = true;
                    Button9.Visible = true;
                    Button10.Visible = true;
                    break;
                case 11:
                    Button1.Visible = true;
                    Button2.Visible = true;
                    Button3.Visible = true;
                    Button4.Visible = true;
                    Button5.Visible = true;
                    Button6.Visible = true;
                    Button7.Visible = true;
                    Button8.Visible = true;
                    Button9.Visible = true;
                    Button10.Visible = true;
                    Button11.Visible = true;
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

    #endregion
}
