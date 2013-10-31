using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
using NursingRNWeb.AppCode.Report_Ds;

public partial class ADMIN_ReportCaseComparisonsByModule : ReportPageBase<IReportCaseComparisonsView, ReportCaseComparisonsPresenter>, IReportCaseComparisonsView
{
    private CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    public int Act { get; set; }

    public override void PreInitialize()
    {
        Presenter.PreInitialize();
        MapControl(ddlProgramofStudy,ddInstitution, lbCohort, lbCase, lbModule, lbCategory, lbSubCategory);
    }

    #region IReportCaseComparisonsView Methods

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

    public void PopulateCohorts(IEnumerable<Cohort> cohorts)
    {
        ControlHelper.PopulateCohorts(lbCohort, cohorts);
    }

    public void PopulateCase(IEnumerable<CaseStudy> caseStudies)
    {
        ControlHelper.PopulateCase(lbCase, caseStudies);
    }

    public void PopulateModule(IEnumerable<Modules> modules)
    {
        ControlHelper.PopulateModule(lbModule, modules);
    }

    public void PopulateCategories(IEnumerable<Category> categories)
    {
        ControlHelper.PopulateCategories(lbCategory, categories);
    }

    public void PopulateSubCategories(IEnumerable<KeyValuePair<string, string>> categoryDetails)
    {
        lbSubCategory.DataSource = categoryDetails;
        lbSubCategory.DataTextField = "value";
        lbSubCategory.DataValueField = "Key";
        lbSubCategory.DataBind();
    }

    public void PopulateSubCategories(IDictionary<string, string> categoryDetails)
    {
        lbSubCategory.DataSource = categoryDetails;
        lbSubCategory.DataTextField = "value";
        lbSubCategory.DataValueField = "Key";
        lbSubCategory.DataBind();
    }

    public void PopulateProducts(IEnumerable<Product> products)
    {
        throw new NotImplementedException();
    }

    public void PopulateTests(IEnumerable<UserTest> tests)
    {
        throw new NotImplementedException();
    }

    public void RenderReport()
    {
        if (Presenter.IsPrintInterface == true)
        {
            ShowSelection();
            Panel1.Visible = false;
            Panel3.Visible = true;
        }
        else
        {
            Panel3.Visible = false;
            int categoryId = 0;
            string[] categoryList = lbCategory.SelectedValuesText.Split('|');
            if (categoryList.Count() > 0)
            {
                categoryId = categoryList[0].ToInt();
            }

            Image111.Attributes.Add("onclick", "window.open('ReportCaseComparisonByModule.aspx?act=" + (int)ReportAction.PrintInterface + "&Institution=" + ddInstitution.SelectedValue + "&Cohorts=" + lbCohort.SelectedValuesText + "&CaseIDs=" + lbCase.SelectedValuesText + "&ModuleIDs=" + lbModule.SelectedValuesText + "&Categories=" + lbCategory.SelectedValuesText + "&SubCategories=" + lbSubCategory.SelectedValuesText + "')");
            Image111.Style.Add("cursor", "pointer");
        }

        string CohortList = string.Empty;
        CohortList = lbCohort.SelectedValuesText;
        if (ddInstitution.SelectedValue == Constants.LIST_SELECT_ALL_VALUE)
        {
            CohortList = string.Empty;
        }

        for (int i = 0; i < lbCategory.Items.Count; i++)
        {
            if (lbCategory.Items[i].Selected)
            {
                HtmlGenericControl C1_Title = new HtmlGenericControl();
                C1_Title.InnerHtml = "<div id='med_center_banner_re' style='padding-left:15px;'>" + lbCategory.Items[i].Text + "</div>";
                D_Graph.Controls.Add(C1_Title);
                D_Graph.Controls.Add(new LiteralControl("</br>"));
                Table T1 = CreateMainTable(CohortList, lbCategory.Items[i].Text, Convert.ToInt32(lbCategory.Items[i].Value), lbCase.SelectedValuesText, lbModule.SelectedValuesText, ddInstitution.SelectedValue.ToInt());
                D_Graph.Controls.Add(T1);
                D_Graph.Controls.Add(new LiteralControl("</br>"));
            }
        }
    }

    public void ExportReport(ReportAction printActions)
    {
        CohortComparisons ds = InitializeCohortComparison();
        string FieldString = string.Empty;
        string[] FieldArray = null;
        int i = 0;
        string fieldId = string.Empty;
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

        if (FieldString.Contains('|'))
        {
            FieldString = FieldString.TrimEnd('|');
        }

        FieldArray = FieldString.Split('|');
        rpt.Load(this.Page.Server.MapPath("Report/CaseCohortComparisons.rpt"));
        rpt.SetDataSource(ds);
        for (i = 0; i < FieldArray.Count(); i++)
        {
            fieldId = "F" + (i + 1);
            rpt.DataDefinition.FormulaFields[fieldId].Text = string.Format("{0:#.#}", "{Detail." + FieldArray[i] + "}".ToString());
            fieldId = "P" + (i + 1);
            rpt.ParameterFields[fieldId].CurrentValues.AddValue(FieldArray[i]);
            fieldId = "P" + (i + 1).ToString() + "1";
            rpt.ParameterFields[fieldId].CurrentValues.AddValue("% Correct");
        }

        for (int j = i + 1; j <= 6; j++)
        {
            fieldId = "P" + j;
            rpt.ParameterFields[fieldId].CurrentValues.AddValue(string.Empty);
            fieldId = "P" + j + "1";
            rpt.ParameterFields[fieldId].CurrentValues.AddValue(string.Empty);
        }

        switch (printActions)
        {
            case ReportAction.ExportExcel:
                rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Page.Response, true, "CaseCohortComparisons");
                break;
            case ReportAction.ExportExcelDataOnly:
                rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                rpt.ReportDefinition.Sections["Section5"].SectionFormat.EnableSuppress = true;
                rpt.ReportDefinition.Sections["PageHeaderSection4"].SectionFormat.EnableSuppress = false;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Page.Response, true, "CaseCohortComparisons");
                break;
        }
    }

    public void GenerateReport()
    {
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Aggregate Reports > Case Comparison By Module")
                            .Add("Institution Id ", ddInstitution.SelectedValue)
                            .Add("Cohort Id ", lbCohort.SelectedValue)
                            .Add("Case Id ", lbCase.SelectedValue)
                            .Add("Module Id ", lbModule.SelectedValue)
                            .Add("Category Id ", lbCategory.SelectedValue)
                            .Add("Sub Category Id ", lbSubCategory.SelectedValue)
                            .Write();
        #endregion
        Presenter.GenerateReport();
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
        ReportAction act = (ReportAction)Page.Request.QueryString["act"].ToInt();
        Act = (int)act;
        if (act == ReportAction.PrintInterface)
        {
            Page.MasterPageFile = "EmptyMaster.master";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Visible = false;
        lblMessage.Text = string.Empty;
        if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Aggregate Reports > Case Comparison By Module ");
            #endregion
        }

        if (Act == (int)ReportAction.PrintInterface)
        {
            Presenter.IsPrintInterface = true;
            GenerateReport();
        }
    }

    protected void Page_Unload(object sender, System.EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }

    protected void btGo_Click(object sender, ImageClickEventArgs e)
    {
        GenerateReport();
    }

    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        string CohortList = lbCohort.SelectedItemsText;
        string[] CohortListName = CohortList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        if (CohortListName.Length > 6)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "Maximum number of cohorts for outputting to excel (Data Only) is 6";
        }
        else
        {
            Presenter.GenerateReport(ReportAction.ExportExcelDataOnly);
        }
    }

    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        string CohortList = lbCohort.SelectedItemsText;
        string[] CohortListName = CohortList.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        if (CohortListName.Length > 6)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "Maximum number of cohorts for outputting to excel is 6";
        }
        else
        {
            Presenter.GenerateReport(ReportAction.ExportExcel);
        }
    }

    private CohortComparisons InitializeCohortComparison()
    {
        CohortComparisons cohortComparison = new CohortComparisons();
        string CohortListNames = string.Empty;
        string CategoryList = string.Empty;
        string[] ArrayQ = new string[lbCohort.Items.Count + 1];
        int categoryId = 0;
        CohortListNames = lbCohort.SelectedItemsText;
        CategoryList = lbCategory.SelectedValuesText;
        string[] cohortNames = CohortListNames.Split(',');
        foreach (string s in cohortNames)
        {
            cohortComparison.Detail.Columns.Add(s, typeof(decimal));
        }
         
        string[] categories = CategoryList.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
        if (categories.Count() > 0)
        {
            categoryId = categories[0].ToInt();
        }

        CohortComparisons.HeadRow rh = cohortComparison.Head.NewHeadRow();
        rh.CohortNames = CohortListNames;
        rh.InstitutionName = this.ddInstitution.SelectedItem.Text;
        rh.ReportName = "Category Comparisons";
        rh.TestNames = lbModule.SelectedItemsText;
        rh.TestTypes = lbCase.SelectedItemsText;
        cohortComparison.Head.Rows.Add(rh);
        foreach (ListItem It in this.lbCategory.Items)
        {
            if (!string.IsNullOrEmpty(It.Value))
            {
                categoryId = Convert.ToInt32(It.Value); 
            }
            
            if (It.Selected)
            {
                CohortComparisons.GroupRow rg = cohortComparison.Group.NewGroupRow();
                rg.HeadID = rh.HeadID;
                rg.CategoryName = It.Text;
                cohortComparison.Group.Rows.Add(rg);
                List<CategoryDetail> categoryDetails = new List<CategoryDetail>();
                if (lbSubCategory.SelectedItem == null)
                {
                    categoryDetails = Presenter.GetSubCategories(categoryId).ToList();
                }
                else
                {
                    for (int i = 0; i < lbSubCategory.Items.Count; i++)
                    {
                        if (lbSubCategory.Items[i].Selected)
                        {
                            int _id = lbSubCategory.Items[i].Value.ToInt();
                            if (lbSubCategory.Items[i].Value.IndexOf(".") != -1)
                            {
                                _id = lbSubCategory.Items[i].Value.Substring(lbSubCategory.Items[i].Value.IndexOf(".") + 1, lbSubCategory.Items[i].Value.IndexOf(".")).ToInt();
                            }

                            CategoryDetail item = new CategoryDetail();
                            item.Id = _id;
                            item.Description = lbSubCategory.Items[i].Text;
                            categoryDetails.Add(item);
                        }
                    }
                }

                foreach (CategoryDetail subCategory in categoryDetails)
                {
                    IEnumerable<ResultsFromTheCohortForChart> reults = Presenter.GetResultsForCohortsBySubCategoryChart(lbCohort.SelectedValuesText, categoryId, subCategory.Id, lbCase.SelectedValuesText, lbModule.SelectedValuesText, ddInstitution.SelectedValue.ToInt());
                    CohortComparisons.DetailRow rd = cohortComparison.Detail.NewDetailRow();
                    rd.GroupID = rg.GroupID;
                    rd.SubCategoryName = subCategory.Description;

                    if (!lbCohort.SelectedValuesText.Contains("|"))
                    {
                        if (reults.Count() > 0)
                        {
                            foreach (ResultsFromTheCohortForChart r in reults)
                            {
                                string rowName = r.CohortName;
                                rd[rowName] = r.Correct;
                            }
                        }
                        else
                        {
                           if (!string.IsNullOrEmpty(lbCohort.SelectedItemsText))
                           {
                               rd[lbCohort.SelectedItemsText] = "0.00";
                           }
                        }
                    }
                    else
                    {
                        string[] cohorts = CohortListNames.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string cohortName in cohorts)
                        {
                            rd[cohortName] = "0.00";
                        }

                        if (reults.Count() > 0)
                        {
                            foreach (ResultsFromTheCohortForChart r in reults)
                            {
                                string rowName = r.CohortName;
                                rd[rowName] = r.Correct;
                            }
                        }
                    }

                    cohortComparison.Detail.Rows.Add(rd);
                }
            }
        }

        return cohortComparison;
    }

    private void ShowSelection()
    {
        StringBuilder selectedValues = new StringBuilder();

        selectedValues.Append("Institution: " + ddInstitution.SelectedItemsText);
        selectedValues.Append("<br/>");
        selectedValues.Append("Cohorts: : " + lbCohort.SelectedItemsText);
        selectedValues.Append("<br/>");
        selectedValues.Append("Cases: : " + lbCase.SelectedItemsText);
        selectedValues.Append("<br/>");
        selectedValues.Append("Modules: : " + lbModule.SelectedItemsText);
        selectedValues.Append("<br/>");
        selectedValues.Append("Categories: : " + lbCategory.SelectedItemsText);
        selectedValues.Append("<br/>");
        selectedValues.Append("SubCategories: : " + lbSubCategory.SelectedItemsText);
        selectedValues.Append("<br/>");

        Label1.Text = selectedValues.ToString();
    }

    private string CreateFlash(string cohortList, int categoryId, int subCategory, string name, string caseList, string moduleList, int institutionId, string categoryName)
    {
        StringBuilder str = new StringBuilder();
        name = name.Replace(":", " ");
        string strDataURL1 = Server.UrlEncode("Graph_CaseComparation.aspx?list=" + cohortList + "&CategoryID=" + categoryId + "&SubCategory=" + subCategory + "&Name=" + name + "&CaseList=" + caseList + "&ModuleList=" + moduleList + "&InstitutionID=" + institutionId + "&CategoryName=" + categoryName);
        string[] cohorts = cohortList.Split('|');
        int NumberOfCohorts = cohorts.Count();
        str.Append("<script type=\"text/javascript\">");
        str.Append("var fo = new FlashObject(\"Charts/FC_2_3_MSBar2D.swf\", \"FC2Column\", \"350\",\"" + ((NumberOfCohorts * 30) + 100) + "\" ,\"7\", \"white\", true);");
        str.Append("fo.addParam(\"allowScriptAccess\", \"always\");");
        str.Append("fo.addParam(\"scale\", \"noScale\");");
        str.Append("fo.addParam(\"menu\", \"false\");");
        str.Append("fo.addVariable(\"dataURL\", \"" + strDataURL1 + "\");");
        str.Append("fo.addVariable(\"chartWidth\",\"300\");");
        str.Append("fo.addVariable(\"ChartHeight\",\"" + ((NumberOfCohorts * 30) + 100) + "\");");
        str.Append("fo.write(\"divchart\");");
        str.Append(" </script>");
        return str.ToString();
    }

    private Table CreateMainTable(string cohortList, string categoryName, int categoryId, string caseList, string moduleList, int institutionId)
    {
        int NumberOfRows;
        int NumberOfRecords = 0;
        List<CategoryDetail> categoryDetails = null;
        if (lbSubCategory.SelectedItem == null)
        {
            categoryDetails = Presenter.GetSubCategories(categoryId).ToList();
        }
        else
        {
            categoryDetails = new List<CategoryDetail>();
            foreach (ListItem it in lbSubCategory.Items)
            {
                if (it.Selected)
                {
                    CategoryDetail categoryDetail = new CategoryDetail();
                    if (it.Value.IndexOf(".") != -1)
                    {
                        categoryDetail.Id = it.Value.Substring(it.Value.IndexOf(".") + 1, it.Value.IndexOf(".")).ToInt();
                    }
                    else
                    {
                        categoryDetail.Id = it.Value.ToInt();
                    }

                    categoryDetail.Description = it.Text;
                    categoryDetails.Add(categoryDetail);
                }
            }
        }

        if (categoryDetails != null)
        {
            NumberOfRecords = categoryDetails.Count();
        }

        if ((NumberOfRecords % 2) > 0)
        {
            NumberOfRows = (NumberOfRecords / 2) + 1;
        }
        else
        {
            NumberOfRows = NumberOfRecords / 2;
        }

        Table table = new Table();
        table.Width = Unit.Percentage(100);
        table.BorderWidth = 1;
        table.CellPadding = 10;
        table.CellSpacing = 0;
        table.CssClass = "gdtable";
        TableRow tRow;
        TableCell tCell;

        int CurentSubCategory = 0;
        string SubCategoryName1 = string.Empty;
        string SubCategoryName2 = string.Empty;
        int SubCategoryId1 = 0;
        int SubCategoryId2 = 0;

        for (int j = 0; j < NumberOfRows; j++)
        {
            tRow = new TableRow();
            if (CurentSubCategory < NumberOfRecords)
            {
                tCell = new TableCell();
                tCell.Width = Unit.Percentage(50);
                tCell.HorizontalAlign = HorizontalAlign.Left;
                HtmlGenericControl Flsh1 = new HtmlGenericControl();
                CategoryDetail categoryDetail = categoryDetails[CurentSubCategory];
                SubCategoryName1 = categoryDetail.Description;
                SubCategoryId1 = categoryDetail.Id;
                Flsh1.InnerHtml = CreateFlash(cohortList, categoryId, SubCategoryId1, SubCategoryName1, caseList, moduleList, institutionId, categoryName);
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
                CategoryDetail categoryDetail = categoryDetails[CurentSubCategory];
                SubCategoryName2 = categoryDetail.Description;
                SubCategoryId2 = categoryDetail.Id;

                Flsh2.InnerHtml = CreateFlash(cohortList, categoryId, SubCategoryId2, SubCategoryName2, caseList, moduleList, institutionId, categoryName);
                tCell.Controls.Add(Flsh2);
                tRow.Cells.Add(tCell);
                CurentSubCategory++;
            }

            table.Rows.Add(tRow);
        }

        return table;
    }

    private void HideProgramofStudy()
    {
        IsProgramofStudyVisible = Presenter.CurrentContext.UserType == UserType.SuperAdmin || Presenter.IsMultipleProgramofStudyAssignedToAdmin(Presenter.CurrentContext.UserId);
    }

}
