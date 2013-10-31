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

public partial class ADMIN_ReportTestStudent : ReportPageBase<IReportStudentPerformanceView, ReportStudentPerformancePresenter>, IReportStudentPerformanceView
{
    private CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    public ReportAction Action { get; set; }

    public int ProductId { get; set; }

    public int UserTestId { get; set; }

    public bool IsProductTypeExistInQueryString { get; set; }

    public bool IsTestIdExistInQueryString { get; set; }

    public string dataURL { get; set; }

    public override void PreInitialize()
    {
        Presenter.PreInitialize();
        MapControl(ddProducts, ddTests);
    }

    #region IReportStudentPerformanceView Members

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
        ControlHelper.PopulateProducts(ddProducts, products);
    }

    public void PopulateCohorts(IEnumerable<Cohort> cohorts)
    {
    }

    public void PopulateTests(IEnumerable<UserTest> tests)
    {
        ControlHelper.PopulateTest(ddTests, tests);
    }

    public void GenerateReport()
    {
        Presenter.GenerateReport();
    }

    public void RenderReport(int testId, ResultsFromTheProgram result, IEnumerable<string> testCharacteristics)
    {
        ImageButton4.Visible = true;
        Image111.Attributes.Add("onclick", "if(Validate()) window.open('ReportTestStudent.aspx?Mode=" + hdnMode.Value + "&act=" + (int)ReportAction.PrintInterface + "&TestType=" + ProductId
            + "&id=" + Convert.ToString(Request.QueryString["id"]) + "&UserTestID=" + UserTestId + "&ProductID=" + ProductId + "')");
        Image111.Style.Add("cursor", "pointer");

        dataURL = Server.UrlEncode("Graph.aspx?AType=1&UserID=" + Convert.ToString(Presenter.Id) + "&UserTestID=" + UserTestId);

        lblNumberCorrect.Text = result.NCorrect.ToString();
        lblNumberIncorrect.Text = result.NInCorrect.ToString();
        lblNotAnswered.Text = result.NAnswered.ToString();
        lblC_I.Text = result.CI.ToString();
        lblI_C.Text = result.IC.ToString();
        lblI_I.Text = result.II.ToString();

        LoadTables(testId, ddTests.SelectedValue, testCharacteristics);

        if (Action == ReportAction.PrintInterface)
        {
            Panel1.Visible = false;
            Image1.Visible = true;
            Panel3.Visible = true;

            Label2.Visible = true;
            Label2.Text = "Test Name: " + ddTests.SelectedItem.Text;
        }
        else
        {
            Panel3.Visible = false;
            Image1.Visible = false;
        }
    }

    public void SetControlValues()
    {
        lblName.Text = Presenter.GetStudentName();
        ImageButton4.Visible = true;
    }

    public void ExportReport(string institutionNames, string cohortNames, ResultsFromTheProgram result1, ResultsFromTheProgram result2,
       IEnumerable<string> testCharacteristics, ReportAction act)
    {
        TestStudent ds = new TestStudent();

        TestStudent.HeadRow rh = ds.Head.NewHeadRow();
        rh.ReportName = "Student Performance Report By Test";
        rh.TestType = ddProducts.SelectedItem.Text;
        rh.TestName = ddTests.SelectedItem.Text;
        rh.StudentName = lblName.Text;

        rh.InstitutionName = institutionNames;

        rh.CohortName = cohortNames;

        if (result1 != null)
        {
            rh.OverallPercentCorrect = result1.Total;
        }

        if (result2 != null)
        {
            rh.NumberCorrect = result2.NCorrect;
            rh.NumberIncorrect = result2.NInCorrect;
            rh.NumberNotReached = result2.NAnswered;
            rh.CorrectToIncorrect = result2.CI;
            rh.IncorrectToCorrect = result2.IC;
            rh.IncorrectToIncorrect = result2.II;
            rh.PercentileRanking = Presenter.GetPercentileRank(UserTestId, rh.NumberCorrect);
        }

        ds.Head.Rows.Add(rh);

        foreach (string Item in testCharacteristics)
        {
            TestStudent.CategoryRow rc = ds.Category.NewCategoryRow();
            rc.HeadID = rh.HeadID;
            rc.CategoryName = ReportHelper.ReturnName(Item);
            rc.CategoryName = "Category Name";
            ds.Category.Rows.Add(rc);

            IEnumerable<ResultsFromTheProgramForChart> listS = Presenter.GetResultsOfChart(Convert.ToInt32(ddTests.SelectedValue), Item);
            foreach (ResultsFromTheProgramForChart resultsFromTheProgramForChart in listS)
            {
                TestStudent.SubCategoryRow rs = ds.SubCategory.NewSubCategoryRow();
                rs.CategoryID = rc.CategoryID;
                rs.SubCategoryName = resultsFromTheProgramForChart.ItemText;
                rs.TotalItems = resultsFromTheProgramForChart.Total;
                rs.ItemsCorrect = resultsFromTheProgramForChart.NCorrect;
                rs.PercentageCorrect = Convert.ToDecimal(resultsFromTheProgramForChart.NCorrect * 100.0 / resultsFromTheProgramForChart.Total);
                rs.Norming = Convert.ToString(resultsFromTheProgramForChart.Norm);
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
        rpt.ParameterFields["P1"].CurrentValues.AddValue("Overall Percent Correct:");
        rpt.ParameterFields["P2"].CurrentValues.AddValue("Percentile Ranking:");

        switch (act)
        {
            case ReportAction.ExportExcel:
                rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                rpt.ReportDefinition.Sections["Section5"].SectionFormat.EnableSuppress = true;
                rpt.ReportDefinition.Sections["Section4"].SectionFormat.EnableSuppress = true;

                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Page.Response, true, "TestStudent");
                break;
            case ReportAction.ExportExcelDataOnly:
                rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                rpt.ReportDefinition.Sections["Section5"].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Page.Response, true, "TestStudent");
                break;
            case ReportAction.PDFPrint:
                rpt.ReportDefinition.Sections[2].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Page.Response, true, "TestStudent");
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

    public void PopulateProgramOfStudies(IEnumerable<ProgramofStudy> programOfStudies)
    {
        throw new NotImplementedException();
    }

    #endregion

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        if (Page.Request.QueryString["act"] != null && Page.Request.QueryString["act"] != string.Empty)
        {
            Page.MasterPageFile = "EmptyMaster.master";
        }

        if (Page.Request.QueryString["ProductID"] != null)
        {
            IsProductTypeExistInQueryString = true;
        }
        else
        {
            IsProductTypeExistInQueryString = false;
        }

        if (Page.Request.QueryString["UserTestID"] != null)
        {
            IsTestIdExistInQueryString = true;
        }
        else
        {
            IsTestIdExistInQueryString = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.Request.QueryString["act"] != null && Page.Request.QueryString["act"] != string.Empty)
        {
            Presenter.IsPrintInterface = true;
        }
        else
        {
            Presenter.IsPrintInterface = false;
        }

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
            Presenter.SetControlValues();
            Presenter.GenerateReport();
        }
    }

    protected void Page_Unload(object sender, System.EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        Presenter.NavigateToStudentQuestionReport(ddTests.SelectedValue.ToInt(), ddProducts.SelectedValue.ToInt());
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.ExportReport(ReportAction.PDFPrint);
    }

    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.ExportReport(ReportAction.ExportExcelDataOnly);
    }

    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.ExportReport(ReportAction.ExportExcel);
    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.ExportReport(ReportAction.PDFPrint);
    }

    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.GenerateReport();
    }

    private string ReturnDiv(int i)
    {
        string result = string.Empty;
        if (i % 3 == 1)
        {
            result = "<img src=\"../Temp/images/barv1.gif\" width=\"16\" height=\"18\">&nbsp;Student %Correct &nbsp;&nbsp;&nbsp;<img src=\"../Temp/images/barv4.gif\" width=\"16\" height=\"18\">&nbsp;Normed %Correct &nbsp;&nbsp;&nbsp;<img src=\"../Temp/images/icon_corr.gif\" width=\"22\" height=\"16\"> Correct  &nbsp;&nbsp;&nbsp;&nbsp;<B>n=</B> Total";
        }
        else if (i % 3 == 2)
        {
            result = "<img src=\"../Temp/images/barv2.gif\" width=\"16\" height=\"18\">&nbsp;Student %Correct &nbsp;&nbsp;&nbsp;<img src=\"../Temp/images/barv4.gif\" width=\"16\" height=\"18\">&nbsp;Normed %Correct &nbsp;&nbsp;&nbsp;<img src=\"../Temp/images/icon_corr.gif\" width=\"22\" height=\"16\"> Correct  &nbsp;&nbsp;&nbsp;&nbsp;<B>n=</B> Total";
        }
        else if (i % 3 == 0)
        {
            result = "<img src=\"../Temp/images/barv3.gif\" width=\"16\" height=\"18\">&nbsp;Student %Correct &nbsp;&nbsp;&nbsp;<img src=\"../Temp/images/barv4.gif\" width=\"16\" height=\"18\">&nbsp;Normed %Correct &nbsp;&nbsp;&nbsp;<img src=\"../Temp/images/icon_corr.gif\" width=\"22\" height=\"16\"> Correct  &nbsp;&nbsp;&nbsp;&nbsp;<B>n=</B> Total";
        }

        return result;
    }

    private string ReturnColor(int i)
    {
        string result = string.Empty;
        if (i % 3 == 1)
        {
            result = "#CC99FF";
        }
        else if (i % 3 == 2)
        {
            result = "#99CCFF";
        }
        else if (i % 3 == 0)
        {
            result = "#F7DBC0";
        }

        return result;
    }

    private string ReturnSecondDiv(int percentage, int i)
    {
        string result = string.Empty;
        if (i % 3 == 1)
        {
            result = "<img src=\"../Temp/images/barv1.gif\" width=\"" + percentage + "%\" height=\"16\">";
        }
        else if (i % 3 == 2)
        {
            result = "<img src=\"../Temp/images/barv2.gif\" width=\"" + percentage + "%\" height=\"16\">";
        }
        else if (i % 3 == 0)
        {
            result = "<img src=\"../Temp/images/barv3.gif\" width=\"" + percentage + "%\" height=\"16\">";
        }

        return result;
    }

    private string ReturnName(string str)
    {
        string f_name = str.Trim();
        if (f_name == "ClientNeeds")
        {
            f_name = "Client Needs";
        }
        else if (f_name == "NursingProcess")
        {
            f_name = "Nursing Process";
        }
        else if (f_name == "CriticalThinking")
        {
            f_name = "Critical Thinking";
        }
        else if (f_name == "ClinicalConcept")
        {
            f_name = "Clinical Concept";
        }
        else if (f_name == "CognitiveLevel")
        {
            f_name = "Bloom's Cognitive Level";
        }
        else if (f_name == "SpecialtyArea")
        {
            f_name = "Specialty Area";
        }
        else if (f_name == "LevelOfDifficulty")
        {
            f_name = "Level Of Difficulty";
        }
        else if (f_name == "ClientNeedCategory")
        {
            f_name = "Client Need Category ";
        }
        else if (f_name == "AccreditationCategories")
        {
            f_name = "Accreditation Categories";
        }
        else if (f_name == "QSENKSACompetencies")
        {
            f_name = "QSEN KSA Competencies";
        }

        return f_name;
    }

    private void LoadTables(int testId, string selectedTest, IEnumerable<string> testCharacteristics)
    {
        int NumberCorrect = 0;
        if (lblNumberCorrect.Text.Trim() != string.Empty)
        {
            NumberCorrect = Convert.ToInt32(lblNumberCorrect.Text.Trim());
        }

        int Percentile;
        if (testId == 9 || testId == 82 || testId == 22 || testId == 83)
        {
            //// Probability of passing NCLEX. Hardcoded for Diagnostic and Readiness tests in Integrated and NCLEX.
            Percentile = Presenter.GetProbability(ddTests.SelectedValue.ToInt(), NumberCorrect);
            lblProbability.Text = Percentile.ToString();
            lblProbability.Visible = true;
            lblRanking.Visible = false;
            lblPP.Visible = true;
            lblPR.Visible = false;
        }
        else
        {
            //// Load Percentile ranking 
            Percentile = Presenter.GetPercentileRank(ddTests.SelectedValue.ToInt(), NumberCorrect);
            lblRanking.Text = Percentile.ToString();
            lblRanking.Visible = true;
            lblProbability.Visible = false;
            lblPR.Visible = true;
            lblPP.Visible = false;
        }

        if (Percentile > 0)
        {
            //// Display Percentile or Probability of passing NCLEX
            const int TW = 150;
            Table T = new Table();
            TableRow r = new TableRow();
            TableCell c = new TableCell();
            Panel P = new Panel();
            this.PlaceHolder1.Controls.Add(T);
            T.Controls.Add(r);
            r.Controls.Add(c);
            c.Controls.Add(P);
            T.Width = new Unit(TW);
            T.Height = Unit.Pixel(20);
            T.Style.Add(HtmlTextWriterStyle.BorderStyle, "solid");
            T.Style.Add(HtmlTextWriterStyle.BorderColor, "#333366");
            T.Style.Add(HtmlTextWriterStyle.BorderWidth, "1");
            T.Style.Add("Margin-top", "4px");
            T.Style.Add("Margin-bootm", "8px");
            T.CellSpacing = 0;
            T.CellPadding = 0;
            T.BorderWidth = new Unit(1);
            c.BackColor = System.Drawing.ColorTranslator.FromHtml("#eef4fa");
            c.ToolTip = Percentile.ToString();
            c.Height = Unit.Pixel(18);
            P.BackImageUrl = "~/Temp/images/barv2.gif";
            P.Width = new Unit(TW * Percentile / 100);
            P.Height = Unit.Pixel(18);
        }
        else
        {
            if (testId == 9 || testId == 82 || testId == 22 || testId == 83)
            {
                if (Presenter.CheckProbabilityExist(ddTests.SelectedValue.ToInt()) == 0)
                {
                    lblProbability.Text = "N/A";
                }
                else
                {
                    lblProbability.Text = "0";
                }
            }
            else
            {
                if (Presenter.CheckPercentileRankExist(ddTests.SelectedValue.ToInt()) == 0)
                {
                    lblRanking.Text = "N/A";
                }
                else
                {
                    lblRanking.Text = "0";
                }
            }
        }

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
                C1_Title.InnerHtml = "<div id='med_center_banner5' style='padding-left:15px;'>" + ReturnName(item.Trim()) + "</div>";
                D_Graph.Controls.Add(C1_Title);
                Table T1 = CreateMainTable(item.Trim(), i, selectedTest);
                D_Graph.Controls.Add(T1);
                D_Graph.Controls.Add(new LiteralControl("</br>"));
            }
        }
    }

    private Table CreateMainTable(string aType, int i, string selectedTest)
    {
        int FirstColumnNumber;
        int SecondColumnNumber;
        int NumberOfRecords;
        IList<ResultsFromTheProgramForChart> list = Presenter.GetResultsOfChart(Convert.ToInt32(selectedTest), aType).ToList();

        NumberOfRecords = list.ToList().Count;

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
        TableRow tRow;
        TableCell tCell;

        ////first table-first row
        tRow = new TableRow();
        tCell = new TableCell();
        tCell.Width = Unit.Percentage(50);
        Table InsideTable_1 = CreateInsideTable(list, aType, 0, FirstColumnNumber - 1, i);
        tCell.Controls.Add(InsideTable_1);
        tRow.Cells.Add(tCell);

        tCell = new TableCell();
        tRow.Cells.Add(tCell);
        tCell.Width = Unit.Percentage(50);
        Table InsideTable_2 = CreateInsideTable(list, aType, FirstColumnNumber, NumberOfRecords - 1, i);
        tCell.Controls.Add(InsideTable_2);
        tRow.Cells.Add(tCell);

        T1.Rows.Add(tRow);

        ////first table-second row
        tRow = new TableRow();
        tCell = new TableCell();
        tCell.ColumnSpan = 2;

        HtmlGenericControl div = new HtmlGenericControl();
        div.InnerHtml = ReturnDiv(i);

        tCell.Controls.Add(div);

        tRow.Cells.Add(tCell);
        T1.Rows.Add(tRow);

        return T1;
    }

    private Table CreateInsideTable(IList<ResultsFromTheProgramForChart> list, string aType, int start, int end, int j)
    {
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
            ResultsFromTheProgramForChart resultsFromTheProgramForChart = list[i];
            tCell.Text = resultsFromTheProgramForChart.ItemText;
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
            tCell.Text = resultsFromTheProgramForChart.ItemText;
            tCell.Width = Unit.Percentage(75);

            div = new HtmlGenericControl();
            int percentage = resultsFromTheProgramForChart.NCorrect * 100 / resultsFromTheProgramForChart.Total;

            string Cell_color = ReturnColor(j);
            tCell.BackColor = Color.FromName(Cell_color);
            div.InnerHtml = ReturnSecondDiv(percentage, j);

            tCell.Controls.Add(div);
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            double percentagef = resultsFromTheProgramForChart.NCorrect * 100.0 / resultsFromTheProgramForChart.Total;
            tCell.Text = percentagef.ToString("f1") + "%";
            tCell.HorizontalAlign = HorizontalAlign.Center;
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            tCell.Controls.Add(new LiteralControl("&nbsp;"));
            tCell.HorizontalAlign = HorizontalAlign.Center;
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            tCell.Text = resultsFromTheProgramForChart.NCorrect.ToString();
            tCell.CssClass = "status3";
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            tCell.Text = resultsFromTheProgramForChart.Total.ToString();
            tCell.CssClass = "status1";
            tRow.Cells.Add(tCell);

            TB.Rows.Add(tRow);

            ////third row - NORMING
            string norm = "0";
            norm = Convert.ToString(resultsFromTheProgramForChart.Norm);
            if (!norm.Equals("0"))
            {
                tRow = new TableRow();

                tCell = new TableCell();
                ////tCell.Text = obj.Percentage.ToString();
                tCell.Text = norm;
                tCell.Width = Unit.Percentage(75);
                tCell.HorizontalAlign = HorizontalAlign.Left;

                div = new HtmlGenericControl();
                int percentage1 = resultsFromTheProgramForChart.NCorrect * 100 / resultsFromTheProgramForChart.Total;

                string Cell_color1 = ReturnColor(j);
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
}
