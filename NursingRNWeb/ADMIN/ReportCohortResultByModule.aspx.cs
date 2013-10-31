using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;
using NursingRNWeb.AppCode.Report_Ds;

public partial class ADMIN_ReportCohortResultByModule : ReportPageBase<IReportCohortResultsByModuleView, ReportCohortResultsByModulePresenter>, IReportCohortResultsByModuleView
{
    private CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    public bool IsInstitutionIdExistInQueryString { get; set; }

    public bool IsCaseIdExistInQueryString { get; set; }

    public bool IsModuleIdExistInQueryString { get; set; }

    public bool IsCohortIdExistInQueryString { get; set; }

    public bool IsProgramOfStudyIdExistInQueryString { get; set; }

    public override void PreInitialize()
    {
        Presenter.PreInitialize();
        MapControl(ddlProgramofStudy,ddInstitution, lbxCohort, ddCase, ddModule);
    }

    #region IReportCohortResultsByModuleView Methods

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
        ControlHelper.PopulateInstitutions(ddInstitution, institutions, true);
    }

    public void PopulateProducts(IEnumerable<Product> products)
    {
    }

    public void PopulateCohorts(IEnumerable<Cohort> cohorts)
    {
        ControlHelper.PopulateCohorts(lbxCohort, cohorts);
    }

    public void PopulateTests(IEnumerable<UserTest> tests)
    {
    }

    public void PopulateCases(IEnumerable<CaseStudy> cases)
    {
        ControlHelper.PopulateCase(ddCase, cases);
    }

    public void PopulateModule(IEnumerable<Modules> module)
    {
        ControlHelper.PopulateModule(ddModule, module);
    }

    public void GenerateReport()
    {
        Presenter.GenerateReport();
    }

    public void GenerateReport(CohortResultsByModule cohortResultsByModule, IEnumerable<CategoryDetail> subCategories, ReportAction printActions)
    {
        if (cohortResultsByModule == null)
        {
            return;
        }

        CohortResultByModule ds = new CohortResultByModule();

        CohortResultByModule.HeadRow rh = (CohortResultByModule.HeadRow)ds.Head.NewRow();
        rh.ReportName = "Cohort Result By Module";
        rh.TestType = ddCase.SelectedItemsText;
        rh.TestName = ddModule.SelectedItemsText;
        rh.InstitutionName = ddInstitution.SelectedItemsText;
        rh.CohortName = lbxCohort.SelectedItemsText;
        rh.NumberCorrect = cohortResultsByModule.Correct;
        rh.Total = cohortResultsByModule.Total;

        int intdevi = cohortResultsByModule.Total;
        decimal Per = rh.NumberCorrect * 100 / intdevi;

        if (Per < 1)
        {
            Per = 0;
        }

        ds.Head.Rows.Add(rh);

        foreach (CategoryDetail Item in subCategories)
        {
            CohortResultByModule.CategoryRow rc = (CohortResultByModule.CategoryRow)ds.Category.NewRow();
            rc.HeadID = rh.HeadID;
            rc.CategoryName = ReturnCatagoryName(Item.Description.Trim());
            ds.Category.Rows.Add(rc);

            IEnumerable<CohortResultsByModule> result = Presenter.GetCaseSubCategoryResultbyCohortModule(Item.Description);

            foreach (CohortResultsByModule drResult in result)
            {
                CohortResultByModule.SubCategoryRow rs = (CohortResultByModule.SubCategoryRow)ds.SubCategory.NewRow();
                rs.CategoryID = rc.CategoryID;
                if (Item.Description.Trim() == "Critical Thinking")
                {
                    rs.SubCategoryName = Presenter.GetCategoryDetails(CategoryName.CriticalThinking, drResult.SubcategoryID).Description;
                }

                if (Item.Description.Trim() == "Nursing Process")
                {
                    rs.SubCategoryName = Presenter.GetCategoryDetails(CategoryName.NursingProcess, drResult.SubcategoryID).Description;
                }

                rs.TotalItems = drResult.Total;
                rs.ItemsCorrect = drResult.Correct;
                rs.PercentageCorrect = (decimal)(((decimal)((decimal)rs.ItemsCorrect / (decimal)rs.TotalItems)) * (decimal)100.0);
                ds.SubCategory.Rows.Add(rs);
            }
        }

        rpt.Load(Server.MapPath("~/Admin/Report/CohortResultByModule.rpt"));
        rpt.SetDataSource(ds);

        switch (printActions)
        {
            case ReportAction.ExportExcelDataOnly:
                rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                rpt.ReportDefinition.Sections["Section5"].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Page.Response, true, "CohortResultByModule");
                break;

            case ReportAction.ExportExcel:
                rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                rpt.ReportDefinition.Sections["Section5"].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Page.Response, true, "CohortResultByModule");
                break;

            case ReportAction.PDFPrint:
                rpt.ReportDefinition.Sections[2].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Page.Response, true, "CohortResultByModule");
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

    public void RenderReport(CohortResultsByModule cohortResultsByModule, IEnumerable<CategoryDetail> subCategories)
    {
        Image111.Attributes.Add("onclick", "window.open('ReportCohortResultByModule.aspx?act=" + (int)ReportAction.PrintInterface
            + "&InstitutionId=" + ddInstitution.SelectedValuesText + "&Id=" + lbxCohort.SelectedValuesText + "&CaseId=" + ddCase.SelectedValuesText
            + "&ModuleId=" + ddModule.SelectedValuesText + "&ProgramOfStudy=" + ddlProgramofStudy.SelectedValuesText + "')");
        Image111.Style.Add("cursor", "pointer");

        if (cohortResultsByModule != null && cohortResultsByModule.Total != 0)
        {
            int Per = cohortResultsByModule.Correct * 100 / cohortResultsByModule.Total;

            if (Per < 1)
            {
                Per = 0;
            }

            Literal li = new Literal();
            li.Text = GetChart(Per);
            PlaceHolder1.Controls.Add(li);

            LoadTables(subCategories);
            lblMessage.Visible = false;
        }
        else
        {
            lblMessage.Text = "Not enough data";
            lblMessage.Visible = true;
            ImageButton4.Visible = false;
            ImageButton3.Visible = false;
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

        if (!string.IsNullOrEmpty(Page.Request.QueryString["CaseId"]))
        {
            IsCaseIdExistInQueryString = true;
        }
        else
        {
            IsCaseIdExistInQueryString = false;
        }

        if (!string.IsNullOrEmpty(Page.Request.QueryString["ModuleId"]))
        {
            IsModuleIdExistInQueryString = true;
        }
        else
        {
            IsModuleIdExistInQueryString = false;
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
            TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Aggregate Reports > Cohort Results by Module");
            #endregion
            if (IsModuleIdExistInQueryString)
            {
                Presenter.GenerateReport();
            }
        }

        if (Page.Request.QueryString["act"] != null && Page.Request.QueryString["act"] != string.Empty)
        {
            Presenter.IsPrintInterface = true;
        }
        else
        {
            Presenter.IsPrintInterface = false;
        }

        SetControls();
    }

    protected void Page_Unload(object sender, System.EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }

    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Aggregate Reports > Cohort Results by Module")
                            .Add("Institution Id ", ddInstitution.SelectedValue)
                            .Add("Cohort Id ", lbxCohort.SelectedValue)
                            .Add("Module Id ", ddModule.SelectedValue)
                            .Add("Case Id ", ddCase.SelectedValue)
                            .Write();
        #endregion
        Presenter.GenerateReport(ReportAction.ExportExcelDataOnly);
    }

    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.GenerateReport(ReportAction.ExportExcel);
    }

    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        GenerateReport();
    }

    #region Private Methods
    private void SetControls()
    {
        Label6.Text = "Cohorts:" + lbxCohort.SelectedItemsText;

        if (Presenter.IsPrintInterface)
        {
            Panel1.Visible = false;
            Image1.Visible = true;

            Panel3.Visible = true;
            Label5.Text = "Institution: " + ddInstitution.SelectedItemsText;
        }
        else
        {
            Panel3.Visible = false;
            Image1.Visible = false;
        }

        lblMessage.Visible = false;
        ImageButton4.Visible = true;
        ImageButton3.Visible = true;
    }

    private string GetChart(int value)
    {
        string st;
        st = "<script type=\"text/javascript\"> " +
        "var fo = new FlashObject(\"Charts/FC_2_3_Column2D.swf\", \"FC2Column\", \"220\", \"250\", \"7\", \"white\", true);" +
        "fo.addParam(\"allowScriptAccess\", \"always\");" +
        "fo.addParam(\"scale\", \"noScale\");" +
        "fo.addParam(\"menu\", \"false\");" +
        "fo.addVariable(\"FlashVars\", \"&chartWidth=220&chartHeight=250&dataXML=<graph zeroPlaneThickness='1' canvasBgColor='f0f0fe' canvasBaseColor='ADC4E4' xaxisname='' yaxisname='Correct %25' hovercapbg='DEDEBE' hovercapborder='889E6D' rotateNames='0' animation='1' yAxisMaxValue='100' numdivlines='9' divLineColor='CCCCCC' divLineAlpha='80' decimalPrecision='2' showAlternateVGridColor='1' AlternateVGridAlpha='30' AlternateVGridColor='CCCCCC' caption='' canvasBorderThickness='1' canvasBorderColor='000066' baseFont='Verdana' baseFontSize='11' ShowLegend='0'><set name='Cohort' value='" + value + "' color='330099'/>";
        st += "</graph>\");" +
"fo.write(\"divchart\");" +
"</script>";
        return st;
    }

    private void LoadTables(IEnumerable<CategoryDetail> subCategories)
    {
        int i = 0;
        if (subCategories.ToList().Count == 0)
        {
            ImageButton4.Visible = false;
            ImageButton3.Visible = false;
        }
        else
        {
            foreach (CategoryDetail item in subCategories)
            {
                i++;
                HtmlGenericControl C1_Title = new HtmlGenericControl();
                C1_Title.InnerHtml = "<div id='med_center_banner5' style='padding-left:15px;'>" + ReturnCatagoryName(item.Description.Trim()) + "</div>";

                D_Graph.Controls.Add(C1_Title);
                System.Web.UI.WebControls.Table T1 = CreateMainTable(item.Description.Trim(), i, C1_Title);
                D_Graph.Controls.Add(T1);
                D_Graph.Controls.Add(new LiteralControl("<br/>"));
            }
        }
    }

    private Table CreateMainTable(string chartType, int index, HtmlGenericControl controlTitle)
    {
        int FirstColumnNumber;
        int SecondColumnNumber;
        int NumberOfRecords;

        IEnumerable<CohortResultsByModule> result = Presenter.GetCaseSubCategoryResultbyCohortModule(chartType);

        NumberOfRecords = result.ToList().Count;

        if ((NumberOfRecords % 2) > 0)
        {
            FirstColumnNumber = (NumberOfRecords / 2) + 1;
        }
        else
        {
            FirstColumnNumber = NumberOfRecords / 2;
        }

        SecondColumnNumber = NumberOfRecords - FirstColumnNumber;

        System.Web.UI.WebControls.Table T1 = new System.Web.UI.WebControls.Table();
        T1.Width = Unit.Percentage(100);
        T1.BorderWidth = 0;
        T1.CellPadding = 10;
        T1.CellSpacing = 0;
        T1.CssClass = "gdtable";

        if (NumberOfRecords > 0)
        {
            controlTitle.Visible = true;
            TableRow tRow;
            TableCell tCell;

            ////first table-first row
            tRow = new TableRow();
            tCell = new TableCell();
            tCell.Width = Unit.Percentage(50);
            System.Web.UI.WebControls.Table InsideTable_1 = CreateInsideTable(result, chartType, 0, FirstColumnNumber - 1, index);
            tCell.Controls.Add(InsideTable_1);
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            tRow.Cells.Add(tCell);
            tCell.Width = Unit.Percentage(50);
            System.Web.UI.WebControls.Table InsideTable_2 = CreateInsideTable(result, chartType, FirstColumnNumber, NumberOfRecords - 1, index);
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

    private Table CreateInsideTable(IEnumerable<CohortResultsByModule> result, string chartType, int start, int end, int index)
    {
        var resultList = result.ToList();

        System.Web.UI.WebControls.Table TB = new System.Web.UI.WebControls.Table();
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
            if (chartType == "Critical Thinking")
            {
                tCell.Text = Presenter.GetCategoryDetails(CategoryName.CriticalThinking, resultList[i].SubcategoryID).Description;
            }
            else if (chartType == "Nursing Process")
            {
                tCell.Text = Presenter.GetCategoryDetails(CategoryName.NursingProcess, resultList[i].SubcategoryID).Description;
            }

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
            tCell.Text = resultList[i].SubcategoryID.ToString();
            tCell.Width = Unit.Percentage(75);
            tCell.HorizontalAlign = HorizontalAlign.Left;

            div = new HtmlGenericControl();
            int percentage = resultList[i].Correct * 100 / resultList[i].Total;

            string Cell_color = ReturnColor(index);
            tCell.BackColor = Color.FromName(Cell_color);
            div.InnerHtml = ReturnSecondDiv(percentage, index);

            tCell.Controls.Add(div);
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            tCell.Text = ((int)resultList[i].Correct * 100.0 / (int)resultList[i].Total).ToString("F1") + "%";
            tCell.HorizontalAlign = HorizontalAlign.Center;
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            tCell.Controls.Add(new LiteralControl("&nbsp;"));
            tCell.HorizontalAlign = HorizontalAlign.Center;
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            tCell.Text = resultList[i].Correct.ToString();
            tCell.CssClass = "status3";
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            tCell.Text = resultList[i].Total.ToString();
            tCell.CssClass = "status1";
            tRow.Cells.Add(tCell);

            TB.Rows.Add(tRow);
        }

        return TB;
    }

    private string ReturnCatagoryName(string category)
    {
        string fName = category.Trim();
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

    private string ReturnDiv(int index)
    {
        string result = string.Empty;
        if (index % 3 == 1)
        {
            result = "<img src=\"../Temp/images/barv1.gif\" width=\"16\" height=\"18\" align=\"top\">&nbsp;Student  %Correct &nbsp;&nbsp;&nbsp;<img src=\"../Temp/images/icon_corr.gif\" width=\"22\" height=\"16\"> Correct  &nbsp;&nbsp;&nbsp;&nbsp;<B>n=</B> Total";
        }

        if (index % 3 == 2)
        {
            result = "<img src=\"../Temp/images/barv2.gif\" width=\"16\" height=\"18\" align=\"top\">&nbsp;Student %Correct &nbsp;&nbsp;&nbsp;<img src=\"../Temp/images/icon_corr.gif\" width=\"22\" height=\"16\"> Correct  &nbsp;&nbsp;&nbsp;&nbsp;<B>n=</B> Total";
        }

        if (index % 3 == 0)
        {
            result = "<img src=\"../Temp/images/barv3.gif\" width=\"16\" height=\"18\" align=\"top\">&nbsp;Student %Correct &nbsp;&nbsp;&nbsp;<img src=\"../Temp/images/icon_corr.gif\" width=\"22\" height=\"16\"> Correct  &nbsp;&nbsp;&nbsp;&nbsp;<B>n=</B> Total";
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

        if (index % 3 == 2)
        {
            result = "#99CCFF";
        }

        if (index % 3 == 0)
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

        if (index % 3 == 2)
        {
            result = "<img src=\"../Temp/images/barv2.gif\" width=\"" + percentage + "%\" height=\"16\" align=\"top\">";
        }

        if (index % 3 == 0)
        {
            result = "<img src=\"../Temp/images/barv3.gif\" width=\"" + percentage + "%\" height=\"16\" align=\"top\">";
        }

        return result;
    }

    private void HideProgramofStudy()
    {
        IsProgramofStudyVisible = Presenter.CurrentContext.UserType == UserType.SuperAdmin || Presenter.IsMultipleProgramofStudyAssignedToAdmin(Presenter.CurrentContext.UserId);
    }
    #endregion
}