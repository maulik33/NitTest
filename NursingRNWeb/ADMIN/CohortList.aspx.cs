using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;
using NursingRNWeb.AppCode.Report_Ds;
using CrystalReport = CrystalDecisions.CrystalReports.Engine;
using RegularExpression = System.Text.RegularExpressions;

public partial class admin_CohortList : PageBase<ICohortView, CohortPresenter>, ICohortView
{
    private const string SELECT_INSTITUTION = "Please select Institution.";
    private const string DATE_FORMAT = "Date format error.";
    private const string LOGGEDIN_USER = "Super Admin";
    private bool _useSearchString = false;
    private CrystalReport.ReportDocument rpt = new CrystalReport.ReportDocument();

    public bool HasAddPermission { get; set; }

    public string SearchText
    {
        get
        {
            return txtSearch.Text.Trim();
        }

        set
        {
        }
    }

    public int GroupId { get; set; }

    public int CohortId { get; set; }

    public int ProductId
    {
        get
        {
            return ddProducts.SelectedValue.ToInt();
        }

        set
        {
        }
    }

    public string InstitutionId
    {
        get
        {
            return lbInstitution.SelectedValuesText;
        }

        set
        {
        }
    }

    public string Name { get; set; }

    public string StartDate
    {
        get
        {
            return txtDateFrom.Text;
        }

        set
        {
        }
    }

    public string EndDate
    {
        get
        {
            return txtDateTo.Text;
        }

        set
        {
        }
    }

    public string Description { get; set; }

    public int CohortStatus
    {
        get
        {
            return statusRadioButton.SelectedValue.ToInt();
        }

        set
        {
        }
    }

    public bool IsValidDate { get; set; }

    public int ProgramId { get; set; }

    public int TestId { get; set; }

    public string ErrorMessage { get; set; }

    public bool IsInValidCohort { get; set; }

    public int ProgramofStudyId
    {
        get
        {
            int programofStudyId = 0;
            if (ddProgramofStudy.SelectedValue.ToInt() > 0)
            {
                programofStudyId = ddProgramofStudy.SelectedValue.ToInt();
            }

            return programofStudyId;
        }
        set { throw new NotImplementedException(); }
    }

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.List);
    }

    #region ICohortView Members

    public void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudy)
    {
        ControlHelper.PopulateProgramOfStudy(ddProgramofStudy, programofStudy);
        trProgramodStudy.Visible = true;
    }

    public void ShowCohortResults(IEnumerable<Cohort> groups)
    {
        throw new NotImplementedException();
    }

    public void SaveCohort(int newGroupId)
    {
        throw new NotImplementedException();
    }

    public void DeleteCohort()
    {
        throw new NotImplementedException();
    }

    public void PopulatePrograms(IEnumerable<Program> programs)
    {
        throw new NotImplementedException();
    }

    public void PopulateGroups(IEnumerable<Group> groups)
    {
        throw new NotImplementedException();
    }

    public void PopulateProducts(IEnumerable<Product> products)
    {
        ControlHelper.PopulateProducts(ddProducts, products);
        ResetDropDown(ddProducts, lbInstitution.SelectedIndex != -1);
    }

    public void PopulateInstitutions(IEnumerable<Institution> institutions)
    {
        if (hfAddPermission.Value.ToBool())
        {
            lbInstitution.ShowSelectAll = true;
        }
        else
        {
            lbInstitution.ShowSelectAll = false;
        }

        ControlHelper.PopulateInstitutions(lbInstitution, institutions, true);
    }

    public void PopulateStudents(IEnumerable<Student> students, SortInfo sortMetaData)
    {
        throw new NotImplementedException();
    }

    public void RefreshPage(Cohort cohort, UserAction action, string title, string subTitle, bool hasDeletePermission, bool hasAddPermission, bool hasAccessDatesEdit, bool hasEditPremission)
    {
        hfAddPermission.Value = hasAddPermission.ToString();
    }

    public void PopulateCohorts(IEnumerable<Cohort> cohorts, SortInfo sortMetaData)
    {
        if (cohorts.Count() > 0)
        {
            lblM.Visible = false;
        }
        else
        {
            lblM.Visible = true;
        }

        gridCohorts.DataSource = KTPSort.Sort<Cohort>(cohorts, sortMetaData);
        gridCohorts.DataBind();
    }

    public void ShowCohort(Cohort cohort)
    {
    }

    public void PopulateTests(IEnumerable<Test> tests)
    {
        ControlHelper.PopulateTests(ddTests, tests);
        ResetDropDown(ddTests, ddProducts.SelectedIndex != 0);
    }

    public void PopulateCohortTest(CohortTestDates TestDetails)
    {
        throw new NotImplementedException();
    }

    public void PopulateCohortTests(IEnumerable<CohortTestDates> testDetails)
    {
        throw new NotImplementedException();
    }

    public void PopulateProgramForTest(Program program)
    {
        throw new NotImplementedException();
    }

    public void ShowProgramResults(IEnumerable<Program> programs, SortInfo sortMetaData)
    {
    }

    public void ExportCohortList(IEnumerable<Cohort> reportData, ReportAction printActions)
    {
        rpt.Load(Server.MapPath("~/Admin/Report/Cohort.rpt"));
        rpt.SetDataSource(BuildDataSourceForCohortExport(KTPSort.Sort<Cohort>(reportData, SortHelper.Parse(hdnGridConfig.Value))));
        switch (printActions)
        {
            case ReportAction.ExportExcel:
                rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Page.Response, true, "Cohorts");
                break;
            case ReportAction.ExportExcelDataOnly:
                rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                rpt.ReportDefinition.Sections["Section5"].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Page.Response, true, "Cohorts");
                break;
            case ReportAction.PDFPrint:
                rpt.ReportDefinition.Sections[2].SectionFormat.EnableSuppress = true;
                //rpt.ReportDefinition.Sections["Section3"].SectionFormat.EnableSuppress = true;
                //rpt.ReportDefinition.Sections["PageHeaderSection2"].SectionFormat.EnableSuppress = true;      
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Page.Response, true, "Cohorts");
                break;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ProductId = 0;
            SearchText = string.Empty;
            TestId = 0;
            StartDate = string.Empty;
            EndDate = string.Empty;
            CohortStatus = 1;
            Presenter.GetInstitutionList(hdnGridConfig.Value);

            Image3.Attributes.Add("onclick", "window.open('popupC.aspx?textbox=" + txtDateFrom.ClientID + "','cal','width=250,height=225,left=270,top=180')");
            Image4.Attributes.Add("onclick", "window.open('popupC.aspx?textbox=" + txtDateTo.ClientID + "','cal','width=250,height=225,left=270,top=180')");
            Image3.Style.Add("cursor", "pointer");
            Image4.Style.Add("cursor", "pointer");
        }

        if (lbInstitution.Items.Count == 1)
        {
            lbInstitution.SelectedIndex = 0;
        }
    }

    protected void gridCohorts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!e.CommandName.Equals("Sort") && !e.CommandName.Equals("Page"))
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gridCohorts.Rows[index];
            CohortId = Server.HtmlDecode(row.Cells[0].Text).ToInt();

            switch (e.CommandName)
            {
                case "Select":
                    Presenter.NavigateToEdit(CohortId.ToString(), UserAction.Edit);
                    break;
                case "Students":
                    Presenter.NavigateFromCohortList(AdminPageDirectory.StudentsInCohort, CohortId);
                    break;
                case "Program":
                    Presenter.NavigateFromCohortList(AdminPageDirectory.CohortProgramAssign, CohortId);
                    break;
                case "Tests":
                    Presenter.NavigateFromCohortList(AdminPageDirectory.CohortTestDates, CohortId);
                    break;
            }
        }
    }

    protected void gridCohorts_PageIndexChanged(Object sender, EventArgs e)
    {
        gridCohorts.Visible = true;
    }

    protected void gridCohorts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            if (e.Row.Cells[3].Text.Trim() != string.Empty && e.Row.Cells[3].Text.Trim() != "&nbsp;")
            {
                e.Row.Cells[3].Text = ControlHelper.FormatDate(e.Row.Cells[3].Text);
            }

            if (e.Row.Cells[4].Text.Trim() != string.Empty && e.Row.Cells[4].Text.Trim() != "&nbsp;")
            {
                e.Row.Cells[4].Text = ControlHelper.FormatDate(e.Row.Cells[4].Text);
            }

            if (Presenter.CurrentContext.UserType == UserType.SuperAdmin)
            {
                if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Institution.Annotation")) != string.Empty)
                {
                    Label lblAnnotation = (Label)e.Row.FindControl("lblAnnotation");
                    lblAnnotation.Visible = true;
                }
            }
            else
            {
                e.Row.Cells[7].Visible=false;
                e.Row.Cells[11].Visible = false;
            }
        }
    }

    protected void gridCohorts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        _useSearchString = true;
        SearchCohorts();
        gridCohorts.PageIndex = e.NewPageIndex;
        gridCohorts.DataBind();
    }

    protected void gridCohorts_Sorting(object sender, GridViewSortEventArgs e)
    {
        _useSearchString = true;
        hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);
        SearchCohorts();
    }

    protected void searchButton_Click(object sender, ImageClickEventArgs e)
    {
        _useSearchString = true;
        lblM.Visible = false;
        SearchCohorts();
    }

    protected void ddProducts_SelectedIndexChanged(object sender, EventArgs e)
    {
        _useSearchString = true;
        Presenter.GetTests();
        ddTests.ShowNotSelected = true;
        ddTests.Visible = true;
        txtDateFrom.Text = string.Empty;
        txtDateTo.Text = string.Empty;
    }

    protected void ddTests_SelectedIndexChanged(object sender, EventArgs e)
    {
        _useSearchString = true;
        txtDateFrom.Text = string.Empty;
        txtDateTo.Text = string.Empty;
        SearchCohorts();
    }

    protected void searchByDatesButton_Click(object sender, ImageClickEventArgs e)
    {
        SearchCohorts();
    }

    protected void statusRadioButton_SelectedIndexChanged(object sender, EventArgs e)
    {
        ProductId = 0;
        TestId = 0;
        txtDateFrom.Text = string.Empty;
        txtDateFrom.Text = string.Empty;
        _useSearchString = true;
        SearchCohorts();
        txtSearch.Text = string.Empty;
    }

    protected void lbInstitution_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddProducts.SelectedIndex = 0;
        _useSearchString = false;
        txtDateFrom.Text = string.Empty;
        txtDateTo.Text = string.Empty;
        SearchCohorts();
        ddProducts.SelectedIndex = 0;
        ResetDropDown(ddProducts, lbInstitution.SelectedIndex != -1);
        ResetDropDown(ddTests, ddProducts.SelectedIndex != 0);
    }

    protected void btnPrintExcel_Click(object sender, ImageClickEventArgs e)
    {
        ExportCohorts(ReportAction.ExportExcel);
    }

    protected void btnPrintPDF_Click(object sender, ImageClickEventArgs e)
    {
        ExportCohorts(ReportAction.PDFPrint);
    }

    protected void btnPrintExcelDataOnly_Click(object sender, ImageClickEventArgs e)
    {
        ExportCohorts(ReportAction.ExportExcelDataOnly);
    }

    protected override void OnUnload(EventArgs e)
    {
        base.OnUnload(e);
        rpt.Close();
        rpt.Dispose();
    }


    protected void ddProgramofStudy_SelectedIndexChanged(object sender, EventArgs e)
    {
        Presenter.PopulateInstitution(ProgramofStudyId);
        SearchCohorts();
    }

    private void ExportCohorts(ReportAction printAction)
    {
        Presenter.ValidateDate();
        ValidateStartEndDate();
        if (IsValidDate)
        {
            _useSearchString = true;
            Presenter.ExportCohorts(printAction, _useSearchString);
            lblM.Visible = false;
        }
    }

    private void ValidateStartEndDate()
    {
        if (IsValidDate)
        {
            if (!string.IsNullOrEmpty(txtDateFrom.Text) && !string.IsNullOrEmpty(txtDateTo.Text) && txtDateFrom.Text.ToDateTime() > txtDateTo.Text.ToDateTime())
            {
                IsValidDate = false;
                lblDateError.Text = "To date should be greater than or equal to From date";
            }
        }
        else
        {
            lblM.Visible = true;
            lblM.Text = DATE_FORMAT;
        }
    }

    private void SearchCohorts()
    {
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Cohort List Page")
            .Add("Search Keyword", txtSearch.Text)
            .Add("InstitutionId", InstitutionId)
            .Add("StartDate", StartDate)
            .Add("EndDate", EndDate)
            .Add("TestId", TestId.ToString())
            .Write();
        #endregion
        Presenter.ValidateDate();
        ValidateStartEndDate();
        if (IsValidDate)
        {
            _useSearchString = true;
            lblM.Visible = false;
            Presenter.showCohorts(hdnGridConfig.Value, _useSearchString);
        }
    }

    private DataSet BuildDataSourceForCohortExport(IEnumerable<Cohort> cohorts)
    {
        CohortSearchInfo ds = new CohortSearchInfo();
        CohortSearchInfo.HeadRow rh = ds.Head.NewHeadRow();
        if (lbInstitution.SelectedItem == null)
        {
            rh.Institution = "All Institutions";
        }
        else
        {
            rh.Institution = lbInstitution.SelectedItem.Text.Equals("Select All") ? "All Institutions" : InstitutionName(lbInstitution.SelectedItemsText);
        }

        rh.TestType = ddProducts.SelectedItemsText;
        rh.TestName = ddTests.SelectedItemsText;
        rh.DateFrom = txtDateFrom.Text;
        rh.DateTo = txtDateTo.Text;
        rh.SearchCriteria = txtSearch.Text;
        rh.Active = statusRadioButton.SelectedItem.Text;
        ds.Head.Rows.Add(rh);
        foreach (Cohort cohort in cohorts)
        {
            CohortSearchInfo.CohortsRow rd = ds.Cohorts.NewCohortsRow();
            rd.InstitutionName = cohort.Institution.InstitutionNameWithProgOfStudy;
            rd.CohortId = Convert.ToString(cohort.CohortId);
            rd.CohortName = cohort.CohortName;
            rd.CohortCount = Convert.ToString(cohort.StudentCount);
            rd.RepeatExpiryDate = Convert.ToString(cohort.RepeatingStudentCount);
            rd.CohortCode = cohort.CohortDescription;
            if (Presenter.CurrentContext.UserType == UserType.SuperAdmin)
            {
                rd.checkadmin = LOGGEDIN_USER;
            }
            else
            {
                rd.checkadmin = null;
              
            }
            if (cohort.CohortStartDate != null)
            {
                rd.CohortStartDate = Convert.ToDateTime(cohort.CohortStartDate).ToShortDateString();
            }

            if (cohort.CohortEndDate != null)
            {
                rd.CohortEndDate = Convert.ToDateTime(cohort.CohortEndDate).ToShortDateString();
            }

            rd.HeadId = rh.HeadId;
            ds.Cohorts.Rows.Add(rd);
        }

        return ds;
    }

    private string InstitutionName(string name)
    {
        if (name != null)
        {
            int count = RegularExpression.Regex.Matches(name, @"[,]+").Count;
            if (count > 1)
            {
                name = "Multiple Institutions";
            }
        }

        return name;
    }

    private void ResetDropDown(DropDownList ddl, bool enabled)
    {
        ddl.SelectedIndex = 0;
        ddl.Enabled = enabled;
    }


    public void ExportStudents(IEnumerable<Student> reportData, ReportAction printActions)
    {
        throw new NotImplementedException();
    }
}
