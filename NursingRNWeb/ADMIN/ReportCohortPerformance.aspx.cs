using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingRNWeb.AppCode.Report_Ds;

public partial class ADMIN_ReportCohortPerformance : ReportPageBase<IReportCohortPerformanceView, ReportCohortPerformancePresenter>, IReportCohortPerformanceView
{
    private string strDataURL1;
    private string strDataURL2;
    private CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    public ReportAction Action { get; set; }

    public override void PreInitialize()
    {
        Presenter.PreInitialize();
        MapControl(ddlProgramofStudy,ddInstitution, lbxCohort, lbxGroup, ddProducts, ddTests);
    }

    #region IReportCohortPerformanceView Methods

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
        ControlHelper.PopulateCohorts(lbxCohort, cohorts);
    }

    public void PopulateTests(IEnumerable<UserTest> tests)
    {
        ControlHelper.PopulateTests(ddTests, tests, "TestId");
    }

    public void PopulateGroup(IEnumerable<Group> groups)
    {
        ControlHelper.PopulateGroups(lbxGroup, groups);
    }

    public bool IsProgramofStudyVisible
    {
        get { return trProgramofStudy.Visible; }
        set { trProgramofStudy.Visible = value; }
    }

    public void GenerateReport()
    {
        Presenter.GenerateReport();
    }

    public void GenerateReport(ResultsFromTheProgram resultsFromTheProgram, decimal norm, IEnumerable<string> testCharacteristics, ReportAction printActions)
    {
        if (resultsFromTheProgram == null)
        {
            return;
        }

        int TestID = ddTests.SelectedValue.ToInt();
        TestStudent ds = new TestStudent();

        TestStudent.HeadRow rh = (TestStudent.HeadRow)ds.Head.NewRow();
        rh.ReportName = "Cohort Performance Report";
        rh.TestType = ddProducts.SelectedItemsText;
        rh.TestName = ddTests.SelectedItemsText;
        rh.InstitutionName = ddInstitution.SelectedItemsText;
        rh.CohortName = lbxCohort.SelectedItemsText;
        rh.NumberCorrect = resultsFromTheProgram.NCorrect;
        rh.NumberIncorrect = resultsFromTheProgram.NInCorrect;
        rh.NumberNotReached = resultsFromTheProgram.NAnswered;
        rh.CorrectToIncorrect = resultsFromTheProgram.CI;
        rh.IncorrectToCorrect = resultsFromTheProgram.IC;
        rh.IncorrectToIncorrect = resultsFromTheProgram.II;

        int intdevi = 1;
        if (rh.NumberIncorrect + rh.NumberCorrect + rh.NumberNotReached != 0)
        {
            intdevi = rh.NumberIncorrect + rh.NumberCorrect + rh.NumberNotReached;
        }

        decimal Per = (decimal)(rh.NumberCorrect * 100.0 / intdevi);

        if (Per < 1)
        {
            Per = Convert.ToDecimal(0);
        }

        rh.OverallPercentCorrect = Per;
        rh.PercentileRanking = norm;

        ds.Head.Rows.Add(rh);

        foreach (string Item in testCharacteristics)
        {
            TestStudent.CategoryRow rc = (TestStudent.CategoryRow)ds.Category.NewRow();
            rc.HeadID = rh.HeadID;
            rc.CategoryName = ReportHelper.ReturnName(Item);
            rc.CategoryName = "Category Name";
            ds.Category.Rows.Add(rc);

            var List_S = Presenter.GetResultsForChart(Item, txtDateFrom.Text, txtDateTo.Text);

            foreach (ResultsFromTheProgramForChart I in List_S)
            {
                TestStudent.SubCategoryRow rs = (TestStudent.SubCategoryRow)ds.SubCategory.NewRow();
                rs.CategoryID = rc.CategoryID;
                rs.SubCategoryName = I.ItemText;
                rs.TotalItems = I.Total;
                rs.ItemsCorrect = I.NCorrect;
                rs.PercentageCorrect = (decimal)(I.NCorrect * 100.0 / I.Total);
                if (I.Norm.ToString().Contains(".") | I.Norm.ToString().Contains("/"))
                {
                    rs.Norming = Convert.ToString(I.Norm);
                }
                else
                {
                    rs.Norming = I.Norm + ".0";
                }

                ds.SubCategory.Rows.Add(rs);
            }
        }

        if (ds.SubCategory.Rows.Count == 0)
        {
            rpt.Load(Server.MapPath("~/Admin/Report/TestStudent_Focused.rpt"));
        }
        else
        {
            rpt.Load(Server.MapPath("~/Admin/Report/TestStudent.rpt"));
        }

        rpt.SetDataSource(ds);

        rpt.ReportDefinition.ReportObjects["Text22"].ObjectFormat.EnableSuppress = true;
        rpt.ParameterFields["P1"].CurrentValues.AddValue("Cohort % Correct:");
        rpt.ParameterFields["P2"].CurrentValues.AddValue("Normed % Correct:");

        switch (printActions)
        {
            case ReportAction.ExportExcel:
                rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                rpt.ReportDefinition.Sections["Section5"].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Page.Response, true, "CohortPerformance");
                break;
            case ReportAction.ExportExcelDataOnly:
                rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                rpt.ReportDefinition.Sections["Section5"].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Page.Response, true, "CohortPerformance");
                break;
            case ReportAction.PDFPrint:
                rpt.ReportDefinition.Sections[2].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Page.Response, true, "CohortPerformance");
                break;
            case ReportAction.ShowPreview:
                this.CrystalReportViewer1.ReportSource = rpt;
                this.CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.ActiveX;
                break;
            case ReportAction.DirectPrint:
                rpt.PrintToPrinter(1, false, 0, 0);
                break;
        }
    }

    public void RenderReport(ResultsFromTheProgram resultsFromTheProgram, decimal norm, IEnumerable<string> testCharacteristics)
    {
        Image111.Attributes.Add("onclick", "if(Validate()) window.open('ReportCohortPerformance.aspx?act=" + (int)ReportAction.PrintInterface
            + "&ProgramofStudyId=" + ddlProgramofStudy.SelectedValue + "&InstitutionId=" + ddInstitution.SelectedValue + "&Id=" + lbxCohort.SelectedValuesText + "&ProductId=" + ddProducts.SelectedValuesText
            + "&GroupId=" + lbxGroup.SelectedValuesText + "&TestId=" + ddTests.SelectedValuesText + "')");
        Image111.Style.Add("cursor", "pointer");

        if (resultsFromTheProgram != null)
        {
            strDataURL1 = Server.UrlEncode(Presenter.GetGraphData(1));
            strDataURL2 = Server.UrlEncode(Presenter.GetGraphData(2));

            lblNumberCorrect.Text = Convert.ToString(resultsFromTheProgram.NCorrect);
            lblNumberIncorrect.Text = Convert.ToString(resultsFromTheProgram.NInCorrect);
            lblNotAnswered.Text = Convert.ToString(resultsFromTheProgram.NAnswered);
            lblC_I.Text = Convert.ToString(resultsFromTheProgram.CI);
            lblI_C.Text = Convert.ToString(resultsFromTheProgram.IC);
            lblI_I.Text = Convert.ToString(resultsFromTheProgram.II);

            lblNumberCorrect.Visible = true;
            lblNumberIncorrect.Visible = true;
            lblNotAnswered.Visible = true;
            lblC_I.Visible = true;
            lblI_C.Visible = true;
            lblI_I.Visible = true;
            TableTitle.Visible = true;
            Panel2.Visible = true;

            int intdevi = 1;
            if ((Convert.ToInt32(lblNumberIncorrect.Text) + Convert.ToInt32(lblNumberCorrect.Text) + Convert.ToInt32(lblNotAnswered.Text)) != 0)
            {
                intdevi = Convert.ToInt32(lblNumberIncorrect.Text) + Convert.ToInt32(lblNumberCorrect.Text) + Convert.ToInt32(lblNotAnswered.Text);
            }

            decimal Per = Presenter.GetPercentage();

            if (Per < 1)
            {
                Per = 0;
            }

            Literal li = new Literal();
            li.Text = GetChart(Per, norm);
            PlaceHolder1.Controls.Add(li);

            LoadTables(testCharacteristics);

            lblM.Visible = false;
            ImageButton4.Visible = true;
        }
        else
        {
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
        }

        if (Action == ReportAction.PrintInterface)
        {
            Panel1.Visible = false;
            Image1.Visible = true;

            Panel3.Visible = true;
            lbltest.Text = ddTests.SelectedItem.Text;
            Label5.Text = "Institution: " + ddInstitution.SelectedItem.Text;

            Label6.Text = "Cohort: " + lbxCohort.SelectedItem.Text;
        }
        else
        {
            Panel3.Visible = false;
            Image1.Visible = false;
        }
    }

    public void PopulateProgramOfStudies(IEnumerable<ProgramofStudy> programOfStudies)
    {
        ControlHelper.PopulateProgramofStudy(ddlProgramofStudy, programOfStudies);
        HideProgramofStudy();
    }

    #endregion

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        if (Page.Request.QueryString["act"] != null && Page.Request.QueryString["act"] != string.Empty)
        {
            Page.MasterPageFile = "EmptyMaster.master";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Mode"] == "1")
        {
            hdnMode.Value = "1";
        }

        if (!IsPostBack)
        {
            if (Page.Request.QueryString["act"] != null && Page.Request.QueryString["act"] != string.Empty)
            {
                Presenter.IsPrintInterface = true;
            }
            else
            {
                Presenter.IsPrintInterface = false;
            }

            Presenter.GenerateReport();
            ImageButton4.Visible = true;
        }

        SetControls();
    }

    protected void OnUnload(object sender, EventArgs e)
    {
        base.OnUnload(e);
        rpt.Close();
        rpt.Dispose();
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.ShowPrinterFriendlyVersion();
    }

    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.GenerateReport(ReportAction.ExportExcelDataOnly);
    }

    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.GenerateReport(ReportAction.ExportExcel);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Presenter.GenerateReport(ReportAction.ShowPreview);
    }

    protected void ImageButton1_Click1(object sender, ImageClickEventArgs e)
    {
        GenerateReport();
    }

    #region Private Methods

    private void SetControls()
    {
        Image3.Attributes.Add("onclick", "window.open('popupC.aspx?textbox=" + txtDateFrom.ClientID + "','cal','width=250,height=225,left=270,top=180')");
        Image4.Attributes.Add("onclick", "window.open('popupC.aspx?textbox=" + txtDateTo.ClientID + "','cal','width=250,height=225,left=270,top=180')");
        Image3.Style.Add("cursor", "pointer");
        Image4.Style.Add("cursor", "pointer");
        lblMessage.Visible = false;
        lblM.Visible = false;
    }

    private string GetChart(decimal value1, decimal value2)
    {
        string st;
        st = "<script type=\"text/javascript\"> " +
        "var fo = new FlashObject(\"Charts/FC_2_3_Column2D.swf\", \"FC2Column\", \"220\", \"250\", \"7\", \"white\", true);" +
        "fo.addParam(\"allowScriptAccess\", \"always\");" +
        "fo.addParam(\"scale\", \"noScale\");" +
        "fo.addParam(\"menu\", \"false\");" +
        "fo.addVariable(\"FlashVars\", \"&chartWidth=220&chartHeight=250&dataXML=<graph zeroPlaneThickness='1' canvasBgColor='f0f0fe' canvasBaseColor='ADC4E4' xaxisname='' yaxisname='Correct %25' hovercapbg='DEDEBE' hovercapborder='889E6D' rotateNames='0' animation='1' yAxisMaxValue='100' numdivlines='9' divLineColor='CCCCCC' divLineAlpha='80' decimalPrecision='1' showAlternateVGridColor='1' AlternateVGridAlpha='30' AlternateVGridColor='CCCCCC' caption='' canvasBorderThickness='1' canvasBorderColor='000066' baseFont='Verdana' baseFontSize='11' ShowLegend='0'><set name='Cohort' value='" + value1 + "' color='330099'/>";
        if (value2 != 0)
        {
            st += "<set name='Normed' value='" + value2 + "' color='99ccff'/>";
        }

        st += "</graph>\");" +
        "fo.write(\"divchart\");" +
        "</script>";
        return st;
    }

    private void LoadTables(IEnumerable<string> testCharacteristics)
    {
        int i = 0;
        if (testCharacteristics.ToList().Count == 0)
        {
            ImageButton4.Visible = false;
        }
        else
        {
            foreach (string item in testCharacteristics)
            {
                i++;
                HtmlGenericControl C1_Title = new HtmlGenericControl();
                C1_Title.InnerHtml = "<div id='med_center_banner5' style='padding-left:15px;'>" + ReportHelper.ReturnName(item.Trim()) + "</div>";

                D_Graph.Controls.Add(C1_Title);
                Table T1 = CreateMainTable(item.Trim(), i, C1_Title);
                D_Graph.Controls.Add(T1);
                D_Graph.Controls.Add(new LiteralControl("<br/>"));
            }
        }
    }

    private Table CreateMainTable(string aType, int index, HtmlGenericControl controlTitle)
    {
        int FirstColumnNumber;
        int SecondColumnNumber;
        int NumberOfRecords;

        IEnumerable<ResultsFromTheProgramForChart> resultsForChart = Presenter.GetResultsForChart(aType, txtDateFrom.Text, txtDateTo.Text);
        NumberOfRecords = resultsForChart.ToList().Count;

        if ((NumberOfRecords % 2) > 0)
        {
            FirstColumnNumber = (NumberOfRecords / 2) + 1;
        }
        else
        {
            FirstColumnNumber = NumberOfRecords / 2;
        }

        SecondColumnNumber = NumberOfRecords - FirstColumnNumber;

        Table T1 = new Table();
        T1.Width = Unit.Percentage(100);
        T1.BorderWidth = 0;
        T1.CellPadding = 10;
        T1.CellSpacing = 0;
        T1.CssClass = "gdtable";

        if (resultsForChart.ToList().Count > 0)
        {
            controlTitle.Visible = true;
            TableRow tRow;
            TableCell tCell;

            ////first table-first row
            tRow = new TableRow();
            tCell = new TableCell();
            tCell.Width = Unit.Percentage(50);
            Table InsideTable_1 = CreateInsideTable(resultsForChart, aType, 0, FirstColumnNumber - 1, index);
            tCell.Controls.Add(InsideTable_1);
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            tRow.Cells.Add(tCell);
            tCell.Width = Unit.Percentage(50);
            Table InsideTable_2 = CreateInsideTable(resultsForChart, aType, FirstColumnNumber, NumberOfRecords - 1, index);
            tCell.Controls.Add(InsideTable_2);
            tRow.Cells.Add(tCell);

            T1.Rows.Add(tRow);

            ////first table-second row
            tRow = new TableRow();
            tCell = new TableCell();
            tCell.ColumnSpan = 2;
            tCell.Style.Add("text-align", "left");

            HtmlGenericControl div = new HtmlGenericControl();
            div.InnerHtml = ReturnDiv(index);
            tCell.Controls.Add(div);

            tRow.Cells.Add(tCell);
            T1.Rows.Add(tRow);
        }
        else
        {
            controlTitle.Visible = false;
        }

        return T1;
    }

    private Table CreateInsideTable(IEnumerable<ResultsFromTheProgramForChart> resultsForChart, string aType, int start, int end, int index)
    {
        var resultsForChartList = resultsForChart.ToList();
        Table TB = new Table();
        TB.Width = Unit.Percentage(100);
        TB.BorderWidth = 0;

        TB.CellPadding = 1;
        TB.CellSpacing = 0;

        TableRow tRow;
        TableCell tCell;
        HtmlGenericControl div;
        for (int i = start; i <= end; i++)
        {
            ////first row with name od the category
            tRow = new TableRow();

            tCell = new TableCell();
            ResultsFromTheProgramForChart obj = (ResultsFromTheProgramForChart)resultsForChartList[i];
            tCell.Text = obj.ItemText;
            tCell.HorizontalAlign = HorizontalAlign.Left;
            tCell.Width = Unit.Percentage(75);
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            tCell.Controls.Add(new LiteralControl("&nbsp;"));
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            tCell.Controls.Add(new LiteralControl("&nbsp;"));
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            div = new HtmlGenericControl();
            div.InnerHtml = "<img src=\"../Temp/images/icon_corr.gif\" width=\"22\" height=\"16\">";
            tCell.CssClass = "status4";
            tCell.Controls.Add(div);
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            tCell.Text = "n=";
            tCell.CssClass = "status2";
            tRow.Cells.Add(tCell);

            TB.Rows.Add(tRow);

            ////second row with the rezults
            tRow = new TableRow();

            tCell = new TableCell();
            tCell.Text = obj.ItemText;
            tCell.Width = Unit.Percentage(75);
            tCell.HorizontalAlign = HorizontalAlign.Left;

            div = new HtmlGenericControl();
            int percentage = obj.NCorrect * 100 / obj.Total;

            string Cell_color = ReturnColor(index);
            tCell.BackColor = Color.FromName(Cell_color);
            div.InnerHtml = ReturnSecondDiv(percentage, index);

            tCell.Controls.Add(div);
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            double percentagef = obj.NCorrect * 100.0 / obj.Total;
            tCell.Text = percentagef.ToString("f1") + "%";
            tCell.HorizontalAlign = HorizontalAlign.Center;
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            tCell.Controls.Add(new LiteralControl("&nbsp;"));
            tCell.HorizontalAlign = HorizontalAlign.Center;
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            tCell.Text = obj.NCorrect.ToString();
            tCell.CssClass = "status3";
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            tCell.Text = obj.Total.ToString();
            tCell.CssClass = "status1";
            tRow.Cells.Add(tCell);

            TB.Rows.Add(tRow);

            ////third row with the rezults
            string norm = "0";
            norm = Convert.ToString(obj.Norm);
            if (!norm.Equals("N/A"))
            {
                tRow = new TableRow();

                tCell = new TableCell();
                tCell.Text = norm;
                tCell.Width = Unit.Percentage(75);
                tCell.HorizontalAlign = HorizontalAlign.Left;

                div = new HtmlGenericControl();
                int percentage1 = obj.NCorrect * 100 / obj.Total;

                string Cell_color1 = ReturnColor(index);
                tCell.BackColor = Color.FromName("#eeeeee");
                div.InnerHtml = "<img src=\"../Temp/images/barv4.gif\" width=\"" + norm + "%\" height=\"16\">";

                tCell.Controls.Add(div);
                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                if (norm.Contains("."))
                {
                    tCell.Text = norm + "%";
                }
                else
                {
                    tCell.Text = norm + ".0%";
                }

                tCell.HorizontalAlign = HorizontalAlign.Center;
                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                tCell.Controls.Add(new LiteralControl("&nbsp;"));
                tCell.HorizontalAlign = HorizontalAlign.Center;
                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                tCell.Text = string.Empty;
                tCell.CssClass = "status3";
                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                tCell.Text = string.Empty;
                tCell.CssClass = "status1";
                tRow.Cells.Add(tCell);

                TB.Rows.Add(tRow);
            }
        }

        return TB;
    }

    private string ReturnDiv(int index)
    {
        string result = string.Empty;
        if (index % 3 == 1)
        {
            result = "<img src=\"../Temp/images/barv1.gif\" width=\"16\" height=\"18\" align=\"top\">&nbsp;Student  %Correct &nbsp;&nbsp;&nbsp;<img src=\"../Temp/images/barv4.gif\" width=\"16\" height=\"18\">&nbsp;Normed %Correct &nbsp;&nbsp;&nbsp;<img src=\"../Temp/images/icon_corr.gif\" width=\"22\" height=\"16\"> Correct  &nbsp;&nbsp;&nbsp;&nbsp;<B>n=</B> Total";
        }
        else if (index % 3 == 2)
        {
            result = "<img src=\"../Temp/images/barv2.gif\" width=\"16\" height=\"18\" align=\"top\">&nbsp;Student %Correct &nbsp;&nbsp;&nbsp;<img src=\"../Temp/images/barv4.gif\" width=\"16\" height=\"18\">&nbsp;Normed %Correct &nbsp;&nbsp;&nbsp;<img src=\"../Temp/images/icon_corr.gif\" width=\"22\" height=\"16\"> Correct  &nbsp;&nbsp;&nbsp;&nbsp;<B>n=</B> Total";
        }
        else if (index % 3 == 0)
        {
            result = "<img src=\"../Temp/images/barv3.gif\" width=\"16\" height=\"18\" align=\"top\">&nbsp;Student %Correct &nbsp;&nbsp;&nbsp;<img src=\"../Temp/images/barv4.gif\" width=\"16\" height=\"18\">&nbsp;Normed %Correct &nbsp;&nbsp;&nbsp;<img src=\"../Temp/images/icon_corr.gif\" width=\"22\" height=\"16\"> Correct  &nbsp;&nbsp;&nbsp;&nbsp;<B>n=</B> Total";
        }

        return result;
    }

    private string ReturnColor(int index)
    {
        string result = string.Empty;
        if (index % 3 == 1)
        {
            result = "#CC99FF";
        }
        else if (index % 3 == 2)
        {
            result = "#99CCFF";
        }
        else if (index % 3 == 0)
        {
            result = "#F7DBC0";
        }

        return result;
    }

    private string ReturnSecondDiv(int percentage, int index)
    {
        string result = string.Empty;
        if (index % 3 == 1)
        {
            result = "<img src=\"../Temp/images/barv1.gif\" width=\"" + percentage + "%\" height=\"16\" align=\"top\">";
        }
        else if (index % 3 == 2)
        {
            result = "<img src=\"../Temp/images/barv2.gif\" width=\"" + percentage + "%\" height=\"16\" align=\"top\">";
        }
        else if (index % 3 == 0)
        {
            result = "<img src=\"../Temp/images/barv3.gif\" width=\"" + percentage + "%\" height=\"16\" align=\"top\">";
        }

        return result;
    }

    private void HideProgramofStudy()
    {
        trProgramofStudy.Visible = Presenter.CurrentContext.UserType == UserType.SuperAdmin || Presenter.IsMultipleProgramofStudyAssignedToAdmin(Presenter.CurrentContext.UserId);
    }

    #endregion
}
