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

public partial class admin_GroupList : PageBase<IGroupView, GroupPresenter>, IGroupView
{
    private int _groupId = 0;
    private CrystalReport.ReportDocument rpt = new CrystalReport.ReportDocument();

    public int GroupId
    {
        get
        {
            return _groupId;
        }

        set
        {
            _groupId = value;
        }
    }

    public string InstitutionId
    {
        get
        {
            return ddInstitution.SelectedValuesText;
        }

        set
        {
        }
    }

    public string CohortIds
    {
        get { return lbxCohort.SelectedValuesText; }
    }

    public bool reportFlag { get; set; }

    public string Name { get; set; }

    public int CohortId { get; set; }

    public int ProgramofStudyId
    {
        get
        {
            int programofStudyId = 0;
            if (ddlProgramofStudy.SelectedValue.ToInt() > 0)
            {
                programofStudyId = ddlProgramofStudy.SelectedValue.ToInt();
            }

            return programofStudyId;
        }
        set { throw new NotImplementedException(); }
    }

    #region IGroupView Members

    public void ShowGroupResults(IEnumerable<Group> groups, SortInfo sortMetaData)
    {
        gridGroups.DataSource = KTPSort.Sort<Group>(groups, sortMetaData);
        gridGroups.DataBind();
        lblM.Visible = groups.Count() == 0 ? true : false;
    }

    public void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudies)
    {
        ddlProgramofStudy.DataSource = programofStudies;
        ddlProgramofStudy.DataTextField = "ProgramofStudyName";
        ddlProgramofStudy.DataValueField = "ProgramofStudyId";
        ddlProgramofStudy.DataBind();
        Presenter.GetInstitutionList(hdnGridConfig.Value,txtSearch.Text);
    }

    public void PopulateInstitution(IEnumerable<Institution> institutions)
    {
        if (institutions.Count() > 0)
        {
            ControlHelper.PopulateInstitutions(ddInstitution, institutions, true);
            gridGroups.Visible = true;
        }
        else
        {
            ddInstitution.Items.Clear();
            ddInstitution.Items.Insert(0, new ListItem("Not Selected", "0"));
            gridGroups.Visible = false;
        }
    }

    public void PopulateCohort(IEnumerable<Cohort> cohorts)
    {
        if (cohorts.Count() > 0)
        {
            ControlHelper.PopulateCohorts(lbxCohort, cohorts);
            gridGroups.Visible = true;
        }
        else
        {
            lbxCohort.Items.Clear();
            lbxCohort.Items.Insert(0, new ListItem("Not Selected", "0"));
            gridGroups.Visible = false;
        }

    }

    public void ExportGroups(IEnumerable<Group> reportData, ReportAction printActions)
    {
        rpt.Load(Server.MapPath("~/Admin/Report/Group.rpt"));
        rpt.SetDataSource(BuildDataSourceForGroup(KTPSort.Sort<Group>(reportData, SortHelper.Parse(hdnGridConfig.Value))));
        switch (printActions)
        {
            case ReportAction.ExportExcel:
                rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Page.Response, true, "Groups");
                break;
            case ReportAction.ExportExcelDataOnly:
                rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                rpt.ReportDefinition.Sections["Section5"].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Page.Response, true, "Groups");
                break;
            case ReportAction.PDFPrint:
                rpt.ReportDefinition.Sections[2].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Page.Response, true, "Groups");
                break;
        }
    }

    #region not used

    public void SaveGroup(int newGroupId)
    {
        throw new NotImplementedException();
    }

    public void DeleteGroup(int groupId)
    {
        throw new NotImplementedException();
    }

    public void RefreshPage(Group group, UserAction action, string title, string subTitle,
        bool hasDeletePermission, bool hasAddPermission)
    {
        throw new NotImplementedException();
    }

    public void ShowGroup(Group group)
    {
        throw new NotImplementedException();
    }

    public void PopulateGroupTest(GroupTestDates testDetails)
    {
        throw new NotImplementedException();
    }

    public void PopulateGroupTests(IEnumerable<GroupTestDates> testDetails)
    {
        throw new NotImplementedException();
    }

    public void PopulateProgramForTest(Program program)
    {
        throw new NotImplementedException();
    }
    #endregion
    #endregion

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.List);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HideProgramofStudy();
            if (Presenter.CurrentContext.UserType == UserType.SuperAdmin)
            {
                Presenter.ShowProgramofStudyDetails();
            }

            Presenter.GetInstitutionList(hdnGridConfig.Value,txtSearch.Text);
        }
    }

    protected void gridGroups_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!e.CommandName.Equals("Sort") && !e.CommandName.Equals("Page"))
        {
            int index = e.CommandArgument.ToInt();
            GridViewRow row = gridGroups.Rows[index];
            _groupId = Server.HtmlDecode(row.Cells[0].Text).ToInt();

            switch (e.CommandName)
            {
                case "Select":
                    Presenter.NavigateToEdit(_groupId.ToString(), UserAction.Edit);
                    break;
                case "Students":
                    Presenter.NavigateFromGroupList(AdminPageDirectory.StudentListForGroup, _groupId);
                    break;
                case "Tests":
                    Presenter.NavigateFromGroupList(AdminPageDirectory.GroupTestDates, _groupId);
                    break;
            }
        }
    }

    protected void gridGroups_PageIndexChanged(Object sender, EventArgs e)
    {
        gridGroups.Visible = true;
    }

    protected void gridGroups_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);
        SearchGroups();
    }

    protected void gridGroups_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridGroups.PageIndex = e.NewPageIndex;
        SearchGroups();
    }

    protected void btnPrintExcel_Click(object sender, ImageClickEventArgs e)
    {
        reportFlag = true;
        Presenter.ExportGroups(txtSearch.Text, ReportAction.ExportExcel);
    }

    protected void btnPrintPDF_Click(object sender, ImageClickEventArgs e)
    {
        reportFlag = true;
        Presenter.ExportGroups(txtSearch.Text, ReportAction.PDFPrint);
    }

    protected void btnPrintExcelDataOnly_Click(object sender, ImageClickEventArgs e)
    {
        reportFlag = true;
        Presenter.ExportGroups(txtSearch.Text, ReportAction.ExportExcelDataOnly);
    }

    protected void searchButton_Click(object sender, ImageClickEventArgs e)
    {
        SearchGroups();
    }

    protected override void OnUnload(EventArgs e)
    {
        base.OnUnload(e);
        if (reportFlag)
        {
            rpt.Close();
            rpt.Dispose();
            reportFlag = false;
        }
    }

    protected void ddProgramOfStudy_SelectedIndexChanged(object sender, EventArgs e)
    {
        Presenter.GetInstitutionList(hdnGridConfig.Value,txtSearch.Text);
    }

    protected void ddInstitution_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddInstitution.SelectedValuesText != string.Empty)
        {
            Presenter.GetActiveCohortList(ddInstitution.SelectedValuesText);
            InstitutionId = ddInstitution.SelectedValue;
            Presenter.SearchGroups(txtSearch.Text, hdnGridConfig.Value, false);
        }
    }

    protected void lbxCohort_SelectedIndexChanged(object sender, EventArgs e)
    {
        SearchGroups();
    }

    private DataSet BuildDataSourceForGroup(IEnumerable<Group> groups)
    {
        GroupSearchInfo ds = new GroupSearchInfo();
        GroupSearchInfo.HeadRow rh = ds.Head.NewHeadRow();
        rh.SearchCriteria = txtSearch.Text;
        if (ddInstitution.SelectedItem != null)
        {
            rh.Institution = ddInstitution.SelectedItem.Text.Equals("Select All") ? "All Institutions" : HeaderText(ddInstitution.SelectedItemsText, true);
        }
        else
        {
            rh.Institution = string.Empty;
        }

        if (lbxCohort.SelectedItem != null)
        {
            rh.Cohorts = lbxCohort.SelectedItem.Text.Equals("Select All") ? "All Cohorts" : HeaderText(lbxCohort.SelectedItemsText, false);
        }
        else
        {
            rh.Cohorts = "All Cohorts";
        }

        ds.Head.Rows.Add(rh);
        foreach (Group group in groups)
        {
            GroupSearchInfo.GroupRow rd = ds.Group.NewGroupRow();
            rd.GroupId = Convert.ToString(group.GroupId);
            rd.Name = group.GroupName;
            rd.Cohort = group.Cohort.CohortName;
            rd.InstitutionName = group.Institution.InstitutionNameWithProgOfStudy;
            rd.HeadId = rh.HeadId;
            ds.Group.Rows.Add(rd);
        }

        return ds;
    }

    private void SearchGroups()
    {
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Group List Page")
            .Add("Search Keyword", txtSearch.Text)
            .Write();
        #endregion
        bool cohortSelectAllFlag = false;
        if (lbxCohort.SelectedValue == "0")
        {
            cohortSelectAllFlag = true;
        }

        Presenter.SearchGroups(txtSearch.Text, hdnGridConfig.Value, cohortSelectAllFlag);
    }

    private string HeaderText(string name, bool isIntitution)
    {
        if (name != null)
        {
            int count = RegularExpression.Regex.Matches(name, @"[,]+").Count;
            if (count > 1)
            {
                if (isIntitution)
                {
                    name = "Multiple Institutions";
                }
                else
                {
                    name = "Multiple Cohorts";
                }
            }
        }

        return name;
    }

    private void HideProgramofStudy()
    {
        trProgramofStudy.Visible = Presenter.CurrentContext.UserType == UserType.SuperAdmin;
    }
}
