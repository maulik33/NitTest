using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class ADMIN_ReportInstitutionPerformance : ReportPageBase<IReportInstitutionPerformanceView, ReportInstitutionPerformancePresenter>, IReportInstitutionPerformanceView
{
    private string _dataURL1;
    private string _dataURL2;
    private ReportAction _printAction = ReportAction.ShowPreview;
    private int _categoryNumber;
    private int _instituteNumber;

    public bool IsIdExistInQueryString { get; set; }

    public bool IsTestIdExistInQueryString { get; set; }

    public bool IsProductIdExistInQueryString { get; set; }

    public bool IsProgramOfStudyIdExistInQueryString { get; set; }

    public override void PreInitialize()
    {
        Presenter.PreInitialize();
        MapControl(ddlProgramofStudy, lbInstitution, ddProducts, lbTests);
    }

    #region IReportInstitutionPerformanceView Methods

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
        ControlHelper.PopulateProducts(ddProducts, products);
    }

    public void PopulateCohorts(IEnumerable<Cohort> cohorts)
    {
    }

    public void PopulateTests(IEnumerable<UserTest> tests)
    {
        ControlHelper.PopulateTestsByTestId(lbTests, tests);
    }

    public void PopulateProgramOfStudies(IEnumerable<ProgramofStudy> programOfStudies)
    {
        ControlHelper.PopulateProgramofStudy(ddlProgramofStudy, programOfStudies);
        HideProgramofStudy();
    }
    public void GenerateReport()
    {
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Aggregate Reports > Institution Performance Report")
                            .Add("Institution Id ", lbInstitution.SelectedValue)
                            .Add("Product Id ", ddProducts.SelectedValue)
                            .Add("Test Id ", lbTests.SelectedValue)
                            .Write();
        #endregion
        Presenter.GenerateReport(txtDateFrom.Text.Trim(), txtDateTo.Text.Trim());
    }

    public void RenderReport(IEnumerable<ResultsFromTheProgram> resultsFromInstitution, decimal norm, IEnumerable<string> testCharacteristics)
    {
        Image111.Attributes.Add("onclick", "window.open('ReportInstitutionPerformance.aspx?act=" + (int)ReportAction.PrintInterface + "&Id=" + lbInstitution.SelectedValuesText
            + "&ProductID=" + ddProducts.SelectedValuesText + "&TestID=" + lbTests.SelectedValuesText + "&ProgramOfStudy=" + ddlProgramofStudy.SelectedValuesText + "')");
        Image111.Style.Add("cursor", "pointer");

        if (resultsFromInstitution != null)
        {
            var result = resultsFromInstitution.FirstOrDefault();

            if (result != null)
            {
                _dataURL1 = Server.UrlEncode("Graph2.aspx?AType=1&IID=" + lbInstitution.SelectedValuesText + "&ProductID=" + ddProducts.SelectedValuesText + "&TestID=" + lbTests.SelectedValuesText);
                _dataURL2 = Server.UrlEncode("Graph2.aspx?AType=2&IID=" + lbInstitution.SelectedValuesText + "&ProductID=" + ddProducts.SelectedValuesText + "&TestID=" + lbTests.SelectedValuesText);

                lblNumberCorrect.Text = Convert.ToString(result.NCorrect);
                lblNumberIncorrect.Text = Convert.ToString(result.NInCorrect);
                lblNotAnswered.Text = Convert.ToString(result.NAnswered);
                lblC_I.Text = Convert.ToString(result.CI);
                lblI_C.Text = Convert.ToString(result.IC);
                lblI_I.Text = Convert.ToString(result.II);

                lblNumberCorrect.Visible = true;
                lblNumberIncorrect.Visible = true;
                lblNotAnswered.Visible = true;
                lblC_I.Visible = true;
                lblI_C.Visible = true;
                lblI_I.Visible = true;
                TableTitle.Visible = true;
                Panel2.Visible = true;

                StringBuilder st = new StringBuilder();
                StringBuilder strColum = new StringBuilder();
                int number = 0;
                string[] IID = lbInstitution.SelectedValuesText.Split('|');
                string[] IIName = lbInstitution.SelectedItemsText.Split(',');
                for (int i = 0; i < IID.Length; ++i)
                {
                    string name = string.Empty;
                    if (IIName[i].Length > 15)
                    {
                        name = IIName[i].Substring(0, 15);
                    }
                    else
                    {
                        name = IIName[i];
                    }

                    string testId = lbTests.SelectedValue.Split('|')[0];

                    var results = Presenter.GetResultsFromInstitution(IID[i], ddProducts.SelectedValue, testId, 1, txtDateFrom.Text, txtDateTo.Text);
                    if (results != null)
                    {
                        var firstResultsFromInstitution = results.FirstOrDefault();
                        if (firstResultsFromInstitution != null)
                        {
                            strColum.Append("<set name='" + name + "' value='" + firstResultsFromInstitution.Total + "' color='330099'/>");
                        }
                        else
                        {
                            strColum.Append("<set name='" + name + "' value='0' color='330099'/>");
                        }
                    }
                    else
                    {
                        strColum.Append("<set name='" + name + "' value='0' color='330099'/>");
                    }

                    ++number;
                }

                int intWidth = 100 + (30 * number);
                if (intWidth < 250)
                {
                    intWidth = 250;
                }

                st.Append("<script type=\"text/javascript\"> " +
                   "var fo = new FlashObject(\"Charts/FC_2_3_Column2D.swf\", \"FC2Column\", \"" + intWidth + "\", \"300\", \"7\", \"white\", true);" + "fo.addParam(\"allowScriptAccess\", \"always\");" +
                   "fo.addParam(\"scale\", \"noScale\");" +
                   "fo.addParam(\"menu\", \"false\");" +
                   "fo.addVariable(\"FlashVars\", \"&chartWidth=" + intWidth + "&chartHeight=300&dataXML=<graph zeroPlaneThickness='1' rotateNames='1' canvasBgColor='f0f0fe' canvasBaseColor='ADC4E4' xaxisname='' yaxisname='Correct %25' hovercapbg='DEDEBE' hovercapborder='889E6D' animation='0' yAxisMaxValue='100' numdivlines='9' divLineColor='CCCCCC' divLineAlpha='80' decimalPrecision='1' showAlternateVGridColor='1' AlternateVGridAlpha='30' AlternateVGridColor='CCCCCC' caption='' canvasBorderThickness='1' canvasBorderColor='000066' baseFont='Verdana' baseFontSize='11' ShowLegend='0'>");

                st = st.Append(strColum.ToString());
                if (norm != 0)
                {
                    st.Append("<set name='Normed' value='" + norm + "' color='99ccff'/>");
                }

                st.Append("</graph>\");" + "fo.write(\"divchart\");" + "</script>");

                Literal li = new Literal();
                li.Text = st.ToString();
                PlaceHolder1.Controls.Clear();
                PlaceHolder1.Controls.Add(li);

                LoadTables(testCharacteristics);

                lblM.Visible = false;
            }
            else
            {
                HideWhenNoData();
            }
        }
        else
        {
            HideWhenNoData();
        }

        lbltest.Text = lbTests.SelectedItemsText;

        if (_printAction == ReportAction.PrintInterface)
        {
            Panel1.Visible = false;
            Image1.Visible = true;
            Panel3.Visible = true;
            lbTests.Visible = true;
            Label5.Text = "Institutions: " + lbInstitution.SelectedItemsText;
        }
        else
        {
            Panel3.Visible = false;
            Image1.Visible = false;
        }
    }

    public void ExportReport()
    {
        System.IO.StringWriter myTextWriter = new System.IO.StringWriter();
        myTextWriter = ExportToExcelXML();

        ReportHelper.ExportToExcel(myTextWriter.ToString(), "InstitutionPerformanceReport.xls");
    }

    public void SetControlsIfMultipleTests(bool isMultipleTests)
    {
        pnlDateSearch.Visible = true;

        if (!isMultipleTests)
        {
            Image111.Visible = true;
            ImageButton1.Visible = true;
            lblM.Visible = false;
        }
        else
        {
            VisibleFalse();
            Image111.Visible = false;
            ImageButton1.Visible = false;
            lblM.Text = "When multiple tests are selected, only the Output to Excel option is available. Web and Printable view do not display.";
            lblM.Visible = true;
        }
    }


    #endregion

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        if (!string.IsNullOrEmpty(Page.Request.QueryString["act"]))
        {
            Page.MasterPageFile = "EmptyMaster.master";
            _printAction = (ReportAction)Convert.ToInt32(Page.Request.QueryString["act"]);
        }

        #region Check Querystring Parameters
        if (!string.IsNullOrEmpty(Page.Request.QueryString["Id"]))
        {
            IsIdExistInQueryString = true;
        }
        else
        {
            IsIdExistInQueryString = false;
        }

        if (!string.IsNullOrEmpty(Page.Request.QueryString["ProductID"]))
        {
            IsProductIdExistInQueryString = true;
        }
        else
        {
            IsProductIdExistInQueryString = false;
        }

        if (!string.IsNullOrEmpty(Page.Request.QueryString["TestID"]))
        {
            IsTestIdExistInQueryString = true;
        }
        else
        {
            IsTestIdExistInQueryString = false;
        }

        if (!string.IsNullOrEmpty(Page.Request.QueryString["ProgramofStudy"]))
        {
            IsProgramOfStudyIdExistInQueryString = true;
        }
        else
        {
            IsProgramOfStudyIdExistInQueryString = false;
        }

        #endregion
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Aggregate Reports > Institution Performance Report ");
            #endregion

            if (_printAction == ReportAction.PrintInterface || IsTestIdExistInQueryString)
            {
                GenerateReport();
            }
        }

        SetControls();
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.ShowPrinterFriendlyVersion();
    }

    protected void ImageButton1_Click1(object sender, ImageClickEventArgs e)
    {
        if (txtDateFrom.Text.ToNullableDateTime().HasValue && txtDateTo.Text.ToNullableDateTime().HasValue)
        {
            GenerateReport();
            lblMessage.Visible = false;
        }
        else
        {
            lblMessage.Visible = true;
            lblMessage.Text = "Date format error.";
            VisibleFalse();
        }
    }

    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.ExportReport();
    }

    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        GenerateReport();
    }

    private void HideWhenNoData()
    {
        lblM.Text = "There is not enough data to compile a student report.";
        lblM.Visible = true;
        lblNumberCorrect.Visible = false;
        lblNumberIncorrect.Visible = false;
        lblNotAnswered.Visible = false;
        lblC_I.Visible = false;
        lblI_C.Visible = false;
        lblI_I.Visible = false;
        TableTitle.Visible = false;
        Panel2.Visible = false;
        ImageButton4.Visible = false;
        ImageButton3.Visible = false;
    }

    #region Private Methods

    private void HideProgramofStudy()
    {
        IsProgramofStudyVisible = Presenter.CurrentContext.UserType == UserType.SuperAdmin || Presenter.IsMultipleProgramofStudyAssignedToAdmin(Presenter.CurrentContext.UserId);
    }

    private StringWriter ExportToExcelXML()
    {
        StringWriter excelDoc;
        excelDoc = new StringWriter();
        StringBuilder ExcelXML = new StringBuilder();
        ExcelXML.Append("<xml version>\r\n<Workbook ");
        ExcelXML.Append("xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\r\n");
        ExcelXML.Append(" xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n ");
        ExcelXML.Append("xmlns:x=\"urn:schemas- microsoft-com:office:");
        ExcelXML.Append("excel\"\r\n xmlns:ss=\"urn:schemas-microsoft-com:");
        ExcelXML.Append("office:spreadsheet\">\r\n <Styles>\r\n ");
        ExcelXML.Append("<Style ss:ID=\"Default\" ss:Name=\"Normal\">\r\n ");
        ExcelXML.Append("<Alignment ss:Vertical=\"Bottom\"/>\r\n <Borders/>");
        ExcelXML.Append("\r\n <Font/>\r\n <Interior/>\r\n <NumberFormat/>");
        ExcelXML.Append("\r\n <Protection/>\r\n </Style>\r\n ");
        ExcelXML.Append("<Style ss:ID=\"BoldColumn\">\r\n <Font ");
        ExcelXML.Append("x:Family=\"Swiss\" ss:Bold=\"1\"/>\r\n </Style>\r\n ");
        ExcelXML.Append("<Style ss:ID=\"StringLiteral\">\r\n <NumberFormat");
        ExcelXML.Append(" ss:Format=\"@\"/>\r\n </Style>\r\n <Style ");
        ExcelXML.Append("ss:ID=\"Decimal\">\r\n <NumberFormat ");
        ExcelXML.Append("ss:Format=\"0.0\"/>\r\n </Style>\r\n ");
        ExcelXML.Append("<Style ss:ID=\"Integer\">\r\n <NumberFormat ");
        ExcelXML.Append("ss:Format=\"0\"/>\r\n </Style>\r\n <Style ");
        ExcelXML.Append("ss:ID=\"DateLiteral\">\r\n <NumberFormat ");
        ExcelXML.Append("ss:Format=\"mm/dd/yyyy;@\"/>\r\n </Style>\r\n ");
        ExcelXML.Append("<Style ss:ID=\"s28\">\r\n");
        ExcelXML.Append("<Alignment ss:Horizontal=\"Left\" ss:Vertical=\"Top\" ss:ReadingOrder=\"LeftToRight\" ss:WrapText=\"1\"/>\r\n");
        ExcelXML.Append("<Font x:CharSet=\"1\" ss:Size=\"9\" ss:Color=\"#808080\" ss:Underline=\"Single\"/>\r\n");
        ExcelXML.Append("<Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>  </Style>\r\n");
        ExcelXML.Append("</Styles>\r\n ");
        string startExcelXML = ExcelXML.ToString();
        const string endExcelXML = "</Workbook>";

        int sheetCount = 1;

        excelDoc.Write(startExcelXML);

        string[] TID = lbTests.SelectedValuesText.Split('|');
        for (int k = 0; k < TID.Length; ++k)
        {
            string testName = lbTests.Items.FindByValue(TID[k]).Text;
            string testName1 = testName.Replace("/", "-");
            string[,] array = ExportToExcelXMLHelper(Convert.ToInt32(TID[k]));
            if (k != 0)
            {
                sheetCount++;
                excelDoc.Write("</Table>");
                excelDoc.Write(" </Worksheet>");
            }

            excelDoc.Write("<Worksheet ss:Name=\"" + testName1 + "\">");
            excelDoc.Write("<Table>");
            excelDoc.Write("<Row><Cell ss:MergeAcross=\"3\" ss:StyleID=\"BoldColumn\"> <Data ss:Type=\"String\">");
            excelDoc.Write(testName);
            excelDoc.Write("</Data></Cell>");
            excelDoc.Write("</Row>");

            for (int i = 0; i < _categoryNumber; ++i)
            {
                excelDoc.Write("<Row>");
                double total = 0;
                int count = 0;
                for (int j = 0; j < _instituteNumber + 4; ++j)
                {
                    string XMLstring = string.Empty;
                    if (i > 1)
                    {
                        if (j > 1 && j < _instituteNumber + 2)
                        {
                            if (array[i, j] == string.Empty)
                            {
                                array[i, j] = "0";
                            }
                            else if (array[i, j] != "0")
                            {
                                total += Convert.ToDouble(array[i, j]);
                                ++count;
                            }

                            excelDoc.Write("<Cell ss:StyleID=\"Decimal\"><Data ss:Type=\"Number\">");
                            XMLstring = array[i, j];
                            excelDoc.Write(XMLstring);
                            excelDoc.Write("</Data></Cell>");
                        }
                        else if (j == _instituteNumber + 2)
                        {
                            if (count != 0)
                            {
                                array[i, j] = Convert.ToString(total / count);
                            }

                            excelDoc.Write("<Cell ss:StyleID=\"Decimal\"><Data ss:Type=\"Number\">");
                            XMLstring = array[i, j].ToString();
                            excelDoc.Write(XMLstring);
                            excelDoc.Write("</Data></Cell>");
                        }
                        else if (j == _instituteNumber + 3)
                        {
                            excelDoc.Write("<Cell ss:StyleID=\"StringLiteral\"><Data ss:Type=\"String\">");
                            XMLstring = array[i, j].ToString();
                            excelDoc.Write(XMLstring);
                            excelDoc.Write("</Data></Cell>");
                        }
                        else
                        {
                            excelDoc.Write("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">");
                            XMLstring = array[i, j].ToString();
                            excelDoc.Write(XMLstring);
                            excelDoc.Write("</Data></Cell>");
                        }
                    }
                    else
                    {
                        excelDoc.Write("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">");
                        XMLstring = array[i, j].ToString();
                        excelDoc.Write(XMLstring);
                        excelDoc.Write("</Data></Cell>");
                    }
                }

                excelDoc.Write("</Row>");
            }
        }

        ////Ending Tag
        excelDoc.Write("</Table>");
        excelDoc.Write(" </Worksheet>");
        excelDoc.Write(endExcelXML);
        return excelDoc;
    }

    private string[,] ExportToExcelXMLHelper(int testid)
    {
        string[] IID = lbInstitution.SelectedValuesText.Split('|');
        string PID = ddProducts.SelectedValuesText;
        string dateFrom = txtDateFrom.Text.Trim();
        string dateTo = txtDateTo.Text.Trim();
        _instituteNumber = IID.Length;
        ////var testCharacteristics = Presenter.GetTestCharacteristics(testid);
        ////int arrayBoundary = FindArrayBoundary(testCharacteristics, IID, PID, Convert.ToString(testid), dateFrom, dateTo);
        var output = new string[100, _instituteNumber + 4];
        for (int n = 0; n < 100; ++n)
        {
            for (int m = 0; m < _instituteNumber + 4; ++m)
            {
                output[n, m] = string.Empty;
            }
        }

        output[2, 0] = "Overall";
        _categoryNumber = 0;
        for (int j = 0; j < IID.Length; ++j)
        {
            output[0, j + 2] = lbInstitution.Items.FindByValue(IID[j]).Text;
            var result = Presenter.GetResultsFromInstitution(IID[j], PID, Convert.ToString(testid), 1, string.Empty, string.Empty);
            output[1, j + 2] = "% Correct";
            if (result.ToList().Count == 0)
            {
                output[2, j + 2] = "0";
            }
            else
            {
                output[2, j + 2] = Convert.ToString(result.ToList()[0].Total);
                var testCharacteristics = Presenter.GetTestCharacteristics(testid);
                if (testCharacteristics != null)
                {
                    int k = 3;
                    foreach (string item in testCharacteristics)
                    {
                        output[k, 0] = item;
                        var chartResult = Presenter.GetResultsFromInstitutionForChart(IID[j], PID, testid.ToString(), item, dateFrom, dateTo);
                        foreach (ResultsFromTheProgramForChart I in chartResult)
                        {
                            output[k, 1] = I.ItemText;
                            output[k, j + 2] = (I.NCorrect * 100.0 / I.Total).ToString();

                            if (I.Norm.ToString() != string.Empty)
                            {
                                if (I.Norm.ToString().Contains("."))
                                {
                                    output[k, _instituteNumber + 3] = I.Norm.ToString();
                                }
                                else
                                {
                                    output[k, _instituteNumber + 3] = I.Norm + ".0";
                                }
                            }
                            ++k;
                        }
                    }

                    if (k > 3)
                    {
                        _categoryNumber = k;
                    }
                }
            }
        }

        var normId = Convert.ToString(Presenter.GetNormForTest(testid));
        if (normId.Contains("."))
        {
            output[2, _instituteNumber + 3] = normId;
        }
        else
        {
            output[2, _instituteNumber + 3] = normId + ".0";
        }

        output[0, _instituteNumber + 2] = "All Campus Total";
        output[1, _instituteNumber + 2] = "% Correct";
        output[0, _instituteNumber + 3] = "Norm Group";
        return output;
    }

    ////private int FindArrayBoundary(IEnumerable<string> testCategory, IEnumerable<string> institutionId, string productId, string testId, string fromDate, string toDate)
    ////{
    ////    int subCategoryLength = 0;
    ////    foreach (string t in institutionId)
    ////    {
    ////        int rowCount = 3;
    ////        foreach (string category in testCategory)
    ////        {
    ////            var chartResult = Presenter.GetResultsFromInstitutionForChart(t, productId, testId, category,
    ////                                                                          fromDate, toDate);
    ////            rowCount += chartResult.Count();
    ////        }

    ////        if (subCategoryLength < rowCount)
    ////        {
    ////            subCategoryLength = rowCount;
    ////        }
    ////    }

    ////    return subCategoryLength;
    ////}

    private void LoadTables(IEnumerable<string> testCharacteristics)
    {
        int i = 0;
        if (testCharacteristics == null)
        {
            ImageButton4.Visible = false;
            ImageButton3.Visible = false;
        }
        else
        {
            foreach (string item in testCharacteristics)
            {
                i++;
                HtmlGenericControl C1_Title = new HtmlGenericControl();
                C1_Title.InnerHtml = "<div id='med_center_banner5' style='padding-left:15px;'>" + ReturnCategoryName(item.Trim()) + "</div>";

                D_Graph.Controls.Add(C1_Title);
                Table T1 = null;
                T1 = CreateMainTable(item.Trim(), i, C1_Title);
                D_Graph.Controls.Add(T1);
                D_Graph.Controls.Add(new LiteralControl("<br/>"));
            }
        }
    }

    private Table CreateMainTable(string chartType, int index, HtmlGenericControl controlTitle)
    {
        var graphResult = Presenter.GetResultsForChart(chartType, txtDateFrom.Text, txtDateTo.Text);

        Table T1 = new Table();
        T1.Width = Unit.Percentage(100);
        T1.BorderWidth = 0;
        T1.CellPadding = 10;
        T1.CellSpacing = 0;
        T1.CssClass = "gdtable";

        string CategoryName = string.Empty;
        int instNumber = 0;
        int percentCount = 0;
        int charNumber = 2;
        if (graphResult != null && graphResult.ToList().Count > 0)
        {
            string[] IName = null;
            IName = (lbInstitution.SelectedItemsText + ",").Split(',');
            IName[IName.Length - 1] = "Norming";

            decimal[] percentage = new decimal[IName.Length];

            TableRow tRow = new TableRow();
            TableCell tCell;
            foreach (ResultsFromTheProgramForChart dr in graphResult)
            {
                if (CategoryName != dr.ItemText)
                {
                    if (instNumber != 0)
                    {
                        controlTitle.Visible = true;
                        if (charNumber % 2 == 0)
                        {
                            ////first table-first row
                            tRow = new TableRow();
                        }

                        tCell = new TableCell();
                        tCell.Width = Unit.Percentage(50);
                        tCell.HorizontalAlign = HorizontalAlign.Left;
                        HtmlGenericControl Flsh1 = new HtmlGenericControl();
                        Flsh1.InnerHtml = CreateFlash(charNumber, instNumber, CategoryName, percentage);
                        tCell.Controls.Add(Flsh1);

                        HtmlGenericControl div = new HtmlGenericControl("DIV");
                        div.Attributes.Add("ID", "divchart_" + charNumber);
                        tCell.Controls.Add(div);

                        tRow.Cells.Add(tCell);
                        if (charNumber % 2 != 0)
                        {
                            T1.Rows.Add(tRow);
                        }
                        ++charNumber;
                        instNumber = 0;
                        percentCount = 0;
                    }
                }

                CategoryName = dr.ItemText;
                if (ReturnPSTrimmedInstitutionName(IName[instNumber]) == dr.InstitutionName)
                {
                    percentage[percentCount] = Convert.ToDecimal(dr.NCorrect * 100.0 / dr.Total);
                    ++instNumber;
                    ++percentCount;
                    percentage[IName.Length - 1] = dr.Norm;
                }
                else
                {
                    while (ReturnPSTrimmedInstitutionName(IName[instNumber]) != dr.InstitutionName)
                    {
                        percentage[percentCount] = 0;
                        ++percentCount;
                        ++instNumber;
                    }

                    percentage[percentCount] = Convert.ToDecimal(dr.NCorrect * 100.0 / dr.Total);
                    ++instNumber;
                    ++percentCount;
                }
            }

            if (charNumber > 2)
            {
                controlTitle.Visible = true;
                if (charNumber % 2 == 0)
                {
                    ////first table-first row
                    tRow = new TableRow();
                }

                tCell = new TableCell();
                tCell.Width = Unit.Percentage(50);
                tCell.HorizontalAlign = HorizontalAlign.Left;
                HtmlGenericControl Flsh1 = new HtmlGenericControl();
                Flsh1.InnerHtml = CreateFlash(charNumber, instNumber, CategoryName, percentage);
                tCell.Controls.Add(Flsh1);

                HtmlGenericControl div = new HtmlGenericControl("DIV");
                div.Attributes.Add("ID", "divchart_" + charNumber);
                tCell.Controls.Add(div);

                tRow.Cells.Add(tCell);
                T1.Rows.Add(tRow);
                ++charNumber;
                instNumber = 0;
                percentCount = 0;
            }
        }
        else
        {
            controlTitle.Visible = false;
        }

        return T1;
    }

    private string CreateFlash(int chartNumber, int number, string categoryName, decimal[] percentages)
    {
        string encodecategory = ConvertStringToHex(categoryName);
        string percentage = string.Join(",", percentages);
        string strDataURL = Server.UrlEncode("Graph2.aspx?CategoryName=" + encodecategory + "&Percentage=" + percentage + "&IID=" + lbInstitution.SelectedValuesText + "&ProgramofStudyName=" + ddlProgramofStudy.SelectedItemsText);
        string str = string.Empty;
        str = "<script type=\"text/javascript\">";
        str = str + "var fo = new FlashObject(\"Charts/FC_2_3_MSBar2D.swf\", \"FC2Column\", \"300\",\"" + ((number * 20) + 150) + "\" ,\"7\", \"white\", true);";
        str = str + "fo.addParam(\"allowScriptAccess\", \"always\");";
        str = str + "fo.addParam(\"scale\", \"noScale\");";
        str = str + "fo.addParam(\"menu\", \"false\");";
        str = str + "fo.addVariable(\"dataURL\", \"" + strDataURL + "\");";
        str = str + "fo.addVariable(\"chartWidth\",\"300\");";
        str = str + "fo.addVariable(\"ChartHeight\",\"" + ((number * 20) + 150) + "\");";
        str = str + "fo.write(\"divchart_" + chartNumber + "\");";
        str = str + " </script>";

        return str;
    }

    private string ConvertStringToHex(String input)
    {
        Byte[] stringBytes = System.Text.Encoding.Unicode.GetBytes(input);
        StringBuilder sbBytes = new StringBuilder(stringBytes.Length * 2);
        foreach (byte b in stringBytes)
        {
            sbBytes.AppendFormat("{0:X2}", b);
        }
        return sbBytes.ToString();
    }

    private string ReturnPSTrimmedInstitutionName(string institutionName)
    {
        string[] remove = { "- RN", "- PN" };
        foreach (string item in remove)
        {
            if (institutionName.EndsWith(item))
            {
                institutionName = institutionName.Substring(0, institutionName.LastIndexOf(item)).TrimEnd();
            }
        }
        return institutionName;
    }

    private string ReturnColor(int index)
    {
        string result = string.Empty;
        if (index % 3 == 1)
        {
            result = "#CC99FF";
        }

        if (index % 3 == 2)
        {
            result = "#99CCFF";
        }

        if (index % 3 == 0)
        {
            result = "#FDC12E";
        }

        return result;
    }

    private string ReturnCategoryName(string categoryName)
    {
        string fName = categoryName.Trim();
        if (fName == "ClientNeeds")
        {
            fName = "Client Needs";
        }
        else if (fName == "NursingProcess")
        {
            fName = "Nursing Process";
        }
        else if (fName == "CriticalThinking")
        {
            fName = "Critical Thinking";
        }
        else if (fName == "ClinicalConcept")
        {
            fName = "Clinical Concept";
        }
        else if (fName == "CognitiveLevel")
        {
            fName = "Bloom's Cognitive Level";
        }
        else if (fName == "SpecialtyArea")
        {
            fName = "Specialty Area";
        }
        else if (fName == "LevelOfDifficulty")
        {
            fName = "Level Of Difficulty";
        }
        else if (fName == "ClientNeedCategory")
        {
            fName = "Client Need Category ";
        }
        else if (fName == "AccreditationCategories")
        {
            fName = "Accreditation Categories";
        }
        else if (fName == "QSENKSACompetencies")
        {
            fName = "QSEN KSA Competencies";
        }

        return fName;
    }

    private void VisibleFalse()
    {
        if (ddProducts.SelectedValue != Constants.LIST_NOT_SELECTED_VALUE &&
            lbInstitution.SelectedValue != Constants.LIST_NOT_SELECTED_VALUE &&
            lbTests.SelectedValue != Constants.LIST_NOT_SELECTED_VALUE)
        {
            lblM.Text = "There is not enough data to compile a student report. ";
            lblM.Visible = true;
        }

        lblNumberCorrect.Visible = false;
        lblNumberIncorrect.Visible = false;
        lblNotAnswered.Visible = false;
        lblC_I.Visible = false;
        lblI_C.Visible = false;
        lblI_I.Visible = false;
        TableTitle.Visible = false;
        Panel2.Visible = false;
    }

    private void SetControls()
    {
        if (ddProducts.SelectedValue == Constants.LIST_NOT_SELECTED_VALUE ||
            lbInstitution.SelectedValue == Constants.LIST_NOT_SELECTED_VALUE ||
            lbTests.SelectedValue == Constants.LIST_NOT_SELECTED_VALUE)
        {
            VisibleFalse();
        }

        Image3.Attributes.Add("onclick", "window.open('popupC.aspx?textbox=" + txtDateFrom.ClientID + "','cal','width=250,height=225,left=270,top=180')");
        Image4.Attributes.Add("onclick", "window.open('popupC.aspx?textbox=" + txtDateTo.ClientID + "','cal','width=250,height=225,left=270,top=180')");
        Image3.Style.Add("cursor", "pointer");
        Image4.Style.Add("cursor", "pointer");
        lblMessage.Visible = false;
        ImageButton3.Visible = true;

        if (_printAction != ReportAction.PrintInterface)
        {
            Panel3.Visible = false;
            Image1.Visible = false;
        }
    }

    #endregion
}
