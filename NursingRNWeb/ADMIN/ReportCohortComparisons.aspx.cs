using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;
using NursingRNWeb.AppCode.Report_Ds;

public partial class ADMIN_ReportCohortComparisons : ReportPageBase<IReportCohortComparisonView, ReportCohortComparisonPresenter>, IReportCohortComparisonView
{
    private CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    public int Act { get; set; }

    public override void PreInitialize()
    {
        Presenter.PreInitialize();
        MapControl(ddlProgramofStudy, ddInstitution, lbCohort, ddlProgramOfStudyForTestsAndCategories, lbProduct, lbTest, lbCategory, ListBox1);
    }

    #region IReportCohortComparison Methods

    public bool PostBack
    {
        get
        {
            return IsPostBack;
        }
    }

    public void PopulateInstitutions(IEnumerable<Institution> institutions)
    {
        ControlHelper.PopulateInstitutions(ddInstitution, institutions, true);
    }

    public void PopulateProducts(IEnumerable<Product> products)
    {
        ControlHelper.PopulateProducts(lbProduct, products);
    }

    public void PopulateCohorts(IEnumerable<Cohort> cohorts)
    {
        ControlHelper.PopulateCohorts(lbCohort, cohorts);
    }

    public void PopulateTests(IEnumerable<UserTest> tests)
    {
        ControlHelper.PopulateTestsByTestId(lbTest, tests);
    }

    public void PopulateCategories(IEnumerable<Category> categories)
    {
        ControlHelper.PopulateCategories(lbCategory, categories);
    }

    public void PopulateSubCategories(IEnumerable<CategoryDetail> categoryDetails)
    {
        ControlHelper.PopulateSubCategories(ListBox1, categoryDetails);
    }

    public void RenderReport()
    {
        if (Presenter.IsPrintInterface == true)
        {
            ShowSelection();
            printBtnPanel.Visible = true;
        }
        else
        {
            Image111.Attributes.Add("onclick", "window.open('ReportCohortComparisons.aspx?act=" + (int)ReportAction.PrintInterface + "&ProgramofStudy=" + ddlProgramofStudy.SelectedValue + "&Institution=" + ddInstitution.SelectedValue + "&Cohorts=" + lbCohort.SelectedValuesText + "&ProgramOfStudyForTestsAndCategories=" + ddlProgramOfStudyForTestsAndCategories.SelectedValue + "&TestTypes=" + lbProduct.SelectedValuesText + "&Tests=" + lbTest.SelectedValuesText + "&Categories=" + lbCategory.SelectedValuesText + "&SubCategories=" + ListBox1.SelectedValuesText + "')");
            Image111.Style.Add("cursor", "pointer");
        }

        string ProductList = string.Empty;
        string TestList = string.Empty;
        string CategoryList = string.Empty;

        CategoryList = lbCohort.SelectedValuesText;
        ProductList = lbProduct.SelectedValuesText;
        TestList = lbTest.SelectedValuesText;
        int InstitutionID = Convert.ToInt32(ddInstitution.SelectedValue);
        if (InstitutionID == 0)
        {
            CategoryList = string.Empty;
        }

        for (int i = 0; i < lbCategory.Items.Count; i++)
        {
            if (lbCategory.Items[i].Selected)
            {
                HtmlGenericControl C1_Title = new HtmlGenericControl();
                C1_Title.InnerHtml = "<div id='med_center_banner_re' style='padding-left:15px;'>" + lbCategory.Items[i].Text + "</div>";
                D_Graph.Controls.Add(C1_Title);
                D_Graph.Controls.Add(new LiteralControl("</br>"));

                Table T1 = CreateMainTable(CategoryList, lbCategory.Items[i].Text, Convert.ToInt32(lbCategory.Items[i].Value), ProductList, TestList, InstitutionID);
                D_Graph.Controls.Add(T1);
                D_Graph.Controls.Add(new LiteralControl("</br>"));
            }
        }
    }

    public void GenerateReport(ReportAction printActions)
    {
        CohortComparisons ds = new CohortComparisons();
        SetCohortComparison(ds);
        string FieldString = string.Empty;
        string[] FieldArray = null;
        int i = 0;
        Int16 count = 0;
        foreach (DataColumn c in ds.Detail.Columns)
        {
            count += 1;
            if (count > 3)
            {
                string Title = c.Caption;
                string DBBind = c.Caption;
                FieldString += DBBind + "|";
            }
        }

        FieldString = FieldString.TrimEnd('|');
        FieldArray = FieldString.Split('|');
        rpt.Load(this.Page.Server.MapPath("Report/CohortComparisons.rpt"));
        rpt.SetDataSource(ds);
        string index = string.Empty;
        for (i = 0; i < FieldArray.Length; i++)
        {
            index = "F" + (i + 1);
            rpt.DataDefinition.FormulaFields[index].Text = string.Format("{0:#.#}", "{Detail." + FieldArray[i] + "}".ToString());
            index = "P" + (i + 1);
            rpt.ParameterFields[index].CurrentValues.AddValue(FieldArray[i]);
            index = "P" + (i + 1) + "1";
            rpt.ParameterFields[index].CurrentValues.AddValue("% Correct");
        }

        for (int j = i + 1; j <= 6; j++)
        {
            index = "P" + j;
            rpt.ParameterFields[index].CurrentValues.AddValue(string.Empty);
            index = "P" + j + "1";
            rpt.ParameterFields[index].CurrentValues.AddValue(string.Empty);
        }

        switch (printActions)
        {
            case ReportAction.ExportExcel:
                rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Page.Response, true, "CohortComparisons");
                break;
            case ReportAction.ExportExcelDataOnly:
                rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                rpt.ReportDefinition.Sections["Section5"].SectionFormat.EnableSuppress = true;
                rpt.ReportDefinition.Sections["PageHeaderSection4"].SectionFormat.EnableSuppress = false;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Page.Response, true, "CohortComparisons");
                break;
            case ReportAction.PDFPrint:
                rpt.ReportDefinition.Sections[2].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Page.Response, true, "CohortComparisons");
                break;
            case ReportAction.ShowPreview:
                CrystalReportViewer1.ReportSource = rpt;
                this.CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.ActiveX;
                break;
            case ReportAction.DirectPrint:
                rpt.PrintToPrinter(1, false, 0, 0);
                break;
        }
    }

    public void GenerateReport()
    {
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Aggregate Reports > Category Comparisons")
                            .Add("Institution Program Of Study Id", ddlProgramofStudy.SelectedValue)
                            .Add("Institution Id", ddInstitution.SelectedValue)
                            .Add("Cohort Id", lbCohort.SelectedValue)
                            .Add("Category Program Of Study Id", ddlProgramOfStudyForTestsAndCategories.SelectedValue)
                            .Add("Product Id", lbProduct.SelectedValue)
                            .Add("Test Id", lbTest.SelectedValue)
                            .Add("Category", lbCategory.SelectedValue)
                            .Add("Sub Category", ListBox1.SelectedValue)
                            .Write();
        #endregion
        Presenter.GenerateReport();
    }

    public void PopulateProgramOfStudies(IEnumerable<ProgramofStudy> programOfStudies)
    {
        ControlHelper.PopulateProgramofStudy(ddlProgramofStudy, programOfStudies);
    }

    public void PopulateProgramOfStudiesForTestsAndCategories(IEnumerable<ProgramofStudy> programOfStudies, int? selectedProgramOfStudy)
    {
        ControlHelper.PopulateProgramofStudy(ddlProgramOfStudyForTestsAndCategories, programOfStudies);
        if (selectedProgramOfStudy.HasValue)
        {
            ddlProgramOfStudyForTestsAndCategories.SelectedValue = selectedProgramOfStudy.Value.ToString();
        }
    }

    #endregion

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        ReportAction act = (ReportAction)System.Convert.ToInt32(Page.Request.QueryString["act"]);
        Act = (int)act;

        if (act == ReportAction.PrintInterface)
        {
            Page.MasterPageFile = "EmptyMaster.master";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HideProgramofStudy();
        if (Act == (int)ReportAction.PrintInterface)
        {
            Presenter.IsPrintInterface = true;
            GenerateReport();
        }

        if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Aggregate Reports > Category Comparisons ");
            #endregion
        }
    }

    protected override void OnUnload(EventArgs e)
    {
        base.OnUnload(e);
        rpt.Close();
        rpt.Dispose();
    }

    private void HideProgramofStudy()
    {
        lblProgramOfStudy.Visible = ddlProgramofStudy.Visible = Presenter.IsProgramOfStudyInstitutionFilterVisible();
    }

    protected void btGo_Click(object sender, ImageClickEventArgs e)
    {
        GenerateReport();
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.GenerateReport(ReportAction.ExportExcelDataOnly);
    }

    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.GenerateReport(ReportAction.ExportExcelDataOnly);
    }

    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.GenerateReport(ReportAction.ExportExcel);
    }

    #region Private Methods

    private void ShowSelection()
    {
        Panel1.Visible = false;
        string CohortList = lbCohort.SelectedItemsText;
        string CategoryList = lbCategory.SelectedItemsText;
        string ProductList = lbProduct.SelectedItemsText;
        string TestList = lbTest.SelectedItemsText;
        string SubCategoryList = ListBox1.SelectedItemsText;
        string CohortListName = CohortList.Replace('|', ',');
        string CategoryListName = CategoryList.Replace('|', ',');
        string ProductListName = ProductList.Replace('|', ',');
        string TestListName = TestList.Replace('|', ',');
        string SubCategoryListName = SubCategoryList.Replace('|', ',');

        Label1.Text += "Institution: " + ddInstitution.SelectedItem.Text;
        Label1.Text += "<br/>";
        Label1.Text += "Cohorts: : " + CohortListName;
        Label1.Text += "<br/>";
        Label1.Text += "Test Type/Test Program of Study: " + ddlProgramOfStudyForTestsAndCategories.SelectedItem.Text;
        Label1.Text += "<br/>";
        Label1.Text += "Test Types: : " + ProductListName;
        Label1.Text += "<br/>";
        Label1.Text += "Tests: : " + TestListName;
        Label1.Text += "<br/>";
        Label1.Text += "Categories: : " + CategoryListName;
        Label1.Text += "<br/>";
        Label1.Text += "SubCategories: : " + SubCategoryListName;
        Label1.Text += "<br/>";
    }

    private void SetCohortComparison(CohortComparisons cohortComparisons)
    {
        CohortComparisons.HeadRow rh = cohortComparisons.Head.NewHeadRow();
        rh.CohortNames = lbCohort.SelectedItemsText;
        rh.InstitutionName = ddInstitution.SelectedItemsText;
        rh.ReportName = "Category Comparisons";
        rh.TestNames = lbTest.SelectedItemsText;
        rh.TestTypes = lbProduct.SelectedItemsText;
        cohortComparisons.Head.Rows.Add(rh);
        string cohortName = lbCohort.SelectedItemsText;
        string[] cohortnames = cohortName.Split(',');
        foreach (string s in cohortnames)
        {
            cohortComparisons.Detail.Columns.Add(s);
        }

        foreach (ListItem It in this.lbCategory.Items)
        {
            List<CategoryDetail> categoryDetails = new List<CategoryDetail>();
            if (It.Selected)
            {
                CohortComparisons.GroupRow rg = cohortComparisons.Group.NewGroupRow();
                rg.HeadID = rh.HeadID;
                rg.CategoryName = It.Text;
                cohortComparisons.Group.Rows.Add(rg);
                ArrayList SubCategories = default(ArrayList);
                if (this.ListBox1.SelectedItem == null)
                {
                    categoryDetails = Presenter.GetSubCategories(It.Value.ToInt());
                }
                else
                {
                    SubCategories = new ArrayList();
                    foreach (ListItem it1 in ListBox1.Items)
                    {
                        if (it1.Selected)
                        {
                            CategoryDetail categoryDetail = new CategoryDetail();
                            categoryDetail.Id = it1.Value.ToInt();
                            categoryDetail.Description = it1.Text;
                            categoryDetails.Add(categoryDetail);
                        }
                    }
                }

                foreach (CategoryDetail SubCategory in categoryDetails)
                {
                    IEnumerable<ResultsFromTheCohortForChart> results = Presenter.GetResultsFromTheCohotForChart(ddInstitution.SelectedValue.ToInt(), SubCategory.Id, It.Value.ToInt(), lbProduct.SelectedValuesText, lbTest.SelectedValuesText, lbCohort.SelectedValuesText);
                    CohortComparisons.DetailRow rd = cohortComparisons.Detail.NewDetailRow();
                    rd.GroupID = rg.GroupID;
                    rd.SubCategoryName = SubCategory.Description;

                    foreach (ResultsFromTheCohortForChart r in results)
                    {
                        rd[r.CohortName] = r.Correct;
                    }

                    cohortComparisons.Detail.Rows.Add(rd);
                }
            }
        }
    }

    private string GetBackName(string categoryName)
    {
        string fName = categoryName.Trim();
        if (fName == "Client Needs")
        {
            fName = "ClientNeeds";
        }
        else if (fName == "Nursing Process")
        {
            fName = "NursingProcess";
        }
        else if (fName == "Critical Thinking")
        {
            fName = "CriticalThinking";
        }
        else if (fName == "Clinical Concept")
        {
            fName = "ClinicalConcept";
        }
        else if (fName == "Bloom's Cognitive Level")
        {
            fName = "CognitiveLevel";
        }
        else if (fName == "Specialty Area")
        {
            fName = "SpecialtyArea";
        }
        else if (fName == "Level Of Difficulty")
        {
            fName = "LevelOfDifficulty";
        }
        else if (fName == "Client Need Category")
        {
            fName = "ClientNeedCategory";
        }
        else if (fName == "Accreditation Categories")
        {
            fName = "AccreditationCategories";
        }
        else if (fName == "QSEN KSA Competencies")
        {
            fName = "QSENKSACompetencies";
        }

        return fName;
    }

    private string CreateFlash(string list, int categoryId, int subCategory, string name, string productList, string testList, int institutionId, string categoryName)
    {
        string str = string.Empty;
        name = name.Replace(":", " ");
        string strDataURL1 = Server.UrlEncode("Graph_C.aspx?list=" + list + "&CategoryID=" + categoryId + "&SubCategory=" + subCategory + "&ProductList=" + productList + "&TestList=" + testList + "&InstitutionID=" + institutionId);
        string[] cohorts = list.Split('|');
        int NumberOfCohorts = cohorts.Count();
        str = "<script type=\"text/javascript\">";
        str = str + "var fo = new FlashObject(\"Charts/FC_2_3_MSBar2D.swf\", \"FC2Column\", \"350\",\"" + ((NumberOfCohorts * 30) + 100) + "\" ,\"7\", \"white\", true);";
        str = str + "fo.addParam(\"allowScriptAccess\", \"always\");";
        str = str + "fo.addParam(\"scale\", \"noScale\");";
        str = str + "fo.addParam(\"menu\", \"false\");";
        str = str + "fo.addVariable(\"dataURL\", \"" + strDataURL1 + "\");";
        str = str + "fo.addVariable(\"chartWidth\",\"300\");";
        str = str + "fo.addVariable(\"ChartHeight\",\"" + ((NumberOfCohorts * 30) + 100) + "\");";
        str = str + "fo.write(\"divchart\");";
        str = str + " </script>";
        return str;
    }

    private Table CreateMainTable(string cohortName, string categoryName, int categoryValue, string productList, string testList, int institutionId)
    {
        int NumberOfRows;
        int NumberOfRecords;
        categoryName = GetBackName(categoryName);

        List<CategoryDetail> subCategories = null;
        if (ListBox1.SelectedItem == null)
        {
            subCategories = Presenter.GetSubCategories(categoryValue).ToList();
        }
        else
        {
            subCategories = new List<CategoryDetail>(); ////ArrayList();
            foreach (ListItem it in ListBox1.Items)
            {
                if (it.Selected)
                {
                    CategoryDetail categoryDetail = new CategoryDetail();
                    categoryDetail.Id = it.Value.ToInt();
                    categoryDetail.Description = it.Text;
                    subCategories.Add(categoryDetail);
                }
            }
        }

        NumberOfRecords = subCategories.Count();
        if ((NumberOfRecords % 2) > 0)
        {
            NumberOfRows = (NumberOfRecords / 2) + 1;
        }
        else
        {
            NumberOfRows = NumberOfRecords / 2;
        }

        Table T1 = new Table();
        T1.Width = Unit.Percentage(100);
        T1.BorderWidth = 1;
        T1.CellPadding = 10;
        T1.CellSpacing = 0;
        T1.CssClass = "gdtable";
        TableRow tRow;
        TableCell tCell;

        int CurentSubCategory = 0;
        string SubCategoryName_1 = string.Empty;
        string SubCategoryName_2 = string.Empty;
        int SubCategoryID_1 = 0;
        int SubCategoryID_2 = 0;

        for (int j = 0; j < NumberOfRows; j++)
        {
            tRow = new TableRow();
            if (CurentSubCategory < NumberOfRecords)
            {
                tCell = new TableCell();
                tCell.Width = Unit.Percentage(50);
                tCell.HorizontalAlign = HorizontalAlign.Left;
                HtmlGenericControl Flsh1 = new HtmlGenericControl();

                CategoryDetail obj = (CategoryDetail)subCategories[CurentSubCategory];
                SubCategoryName_1 = obj.Description;
                SubCategoryID_1 = obj.Id;
                Flsh1.InnerHtml = CreateFlash(cohortName, categoryValue, SubCategoryID_1, SubCategoryName_1, productList, testList, institutionId, categoryName);
                tCell.Controls.Add(Flsh1);
                tRow.Cells.Add(tCell);
                CurentSubCategory++;
            }

            if (CurentSubCategory < NumberOfRecords)
            {
                tCell = new TableCell();
                tCell.Width = Unit.Percentage(50);
                tCell.HorizontalAlign = HorizontalAlign.Left;
                HtmlGenericControl Flsh2 = new HtmlGenericControl();
                CategoryDetail obj = (CategoryDetail)subCategories[CurentSubCategory];
                SubCategoryName_2 = obj.Description;
                SubCategoryID_2 = obj.Id;
                Flsh2.InnerHtml = CreateFlash(cohortName, categoryValue, SubCategoryID_2, SubCategoryName_2, productList, testList, institutionId, categoryName);
                tCell.Controls.Add(Flsh2);
                tRow.Cells.Add(tCell);
                CurentSubCategory++;
            }

            T1.Rows.Add(tRow);
        }

        return T1;
    }

    #endregion
}
