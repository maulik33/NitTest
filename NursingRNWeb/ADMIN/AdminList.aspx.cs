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

public partial class admin_AdminList : PageBase<IAdminView, AdminPresenter>, IAdminView
{
    private int _pageIndex = 0;
    private CrystalReport.ReportDocument rpt = new CrystalReport.ReportDocument();

    public int UserId { get; set; }

    public string Instituions { get; set; }

    public string SearchText { get; set; }

    public int AdminId { get; set; }

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.List);
    }

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

    #region IAdminView members
    public void PopulateAdmin(Admin admin)
    {
        throw new NotImplementedException();
    }

    public void RefreshPage(Admin admin, UserAction action, Dictionary<int, string> securityLevel, string title, string subTitle, bool hasDeletePermission, bool hasAddPermission)
    {
        throw new NotImplementedException();
    }

    public void PopulateInstitutions(IEnumerable<Institution> institutions)
    {
        throw new NotImplementedException();
    }

    public void ShowAdminList(IEnumerable<Admin> admins, SortInfo sortMetaData)
    {
        gridAdmins.DataSource = KTPSort.Sort<Admin>(admins, sortMetaData);
        gridAdmins.PageIndex = _pageIndex;
        gridAdmins.DataBind();
        lblM.Visible = admins.Count() == 0 ? true : false;
    }

    public void ExportAdminUserList(IEnumerable<Admin> admins, ReportAction printActions)
    {
        rpt.Load(Server.MapPath("~/Admin/Report/Administrator.rpt"));
        rpt.SetDataSource(BuildDataSourceForAdmin(KTPSort.Sort<Admin>(admins, SortHelper.Parse(hdnGridConfig.Value))));
        switch (printActions)
        {
            case ReportAction.ExportExcel:
                rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Page.Response, true, "Administrators");
                break;
            case ReportAction.ExportExcelDataOnly:
                rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                rpt.ReportDefinition.Sections["Section5"].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Page.Response, true, "Administrators");
                break;
            case ReportAction.PDFPrint:
                rpt.ReportDefinition.Sections[2].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Page.Response, true, "Administrators");
                break;
        }
    }

    public void ShowErrorMessage(string message)
    {
        throw new NotImplementedException();
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        HideProgramofStudy();
        if (!Page.IsPostBack)
        {
            if (Presenter.CurrentContext.UserType == UserType.SuperAdmin)
            {
                Presenter.ShowProgramofStudyDetails();
            }
            else
            {
                SearchAdmins();
            }
           
        }
        
    }

    protected void gridAdmins_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!e.CommandName.Equals("Sort") && !e.CommandName.Equals("Page"))
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gridAdmins.Rows[index];
            string _userId = Server.HtmlDecode(row.Cells[0].Text);
            Presenter.NavigateToEdit(_userId, UserAction.Edit);
        }
    }

    protected void gridAdmins_PageIndexChanged(Object sender, EventArgs e)
    {
        gridAdmins.Visible = true;
    }

    protected void gridAdmins_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);
        SearchText = txtSearch.Text.Trim();
        SearchAdmins();
    }

    protected void gridAdmins_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SearchText = txtSearch.Text.Trim();
        _pageIndex = e.NewPageIndex;
        SearchAdmins();
    }

    protected void searchButton_Click(object sender, ImageClickEventArgs e)
    {
        _pageIndex = 0;
        SearchText = txtSearch.Text.Trim();
        SearchAdmins();
    }

    protected void btnPrintExcel_Click(object sender, ImageClickEventArgs e)
    {
        ExportAdminUsers(ReportAction.ExportExcel);
    }

    protected void btnPrintPDF_Click(object sender, ImageClickEventArgs e)
    {
        ExportAdminUsers(ReportAction.PDFPrint);
    }

    protected void btnPrintExcelDataOnly_Click(object sender, ImageClickEventArgs e)
    {
        ExportAdminUsers(ReportAction.ExportExcelDataOnly);
    }

    protected override void OnUnload(EventArgs e)
    {
        base.OnUnload(e);
        rpt.Close();
        rpt.Dispose();
    }

    protected void ddProgramOfStudy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProgramofStudy.SelectedValue.ToInt() > 0)
        {
            SearchAdmins();
            gridAdmins.Visible = true;
        }
        else
        {
            gridAdmins.Visible = false;
        }
    }

    public void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudies)
    {
        ddlProgramofStudy.NotSelectedText = "Selection Required";
        ddlProgramofStudy.DataSource = programofStudies;
        ddlProgramofStudy.DataTextField = "ProgramofStudyName";
        ddlProgramofStudy.DataValueField = "ProgramofStudyId";
        ddlProgramofStudy.DataBind();
        ddlProgramofStudy.Items.Insert(3,new ListItem("None","3"));
    }

    private void SearchAdmins()
    {
        ////TODO: Gokul - Check why NurSecurityAdmin should be joined in Search Query
        SearchText = txtSearch.Text.Trim();
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Admin List Page")
            .Add("Search Keyword", txtSearch.Text)
            .Write();
        #endregion
        if (rfvProgramOfStudy.IsValid)
        {
            Presenter.ShowAdminUserList(hdnGridConfig.Value);
        }
        else
        {
            gridAdmins.DataSource = null;
            gridAdmins.DataBind();
            gridAdmins.Visible = false;
        }
    }

    private DataSet BuildDataSourceForAdmin(IEnumerable<Admin> admins)
    {
        AdministratorSearchInfo ds = new AdministratorSearchInfo();
        AdministratorSearchInfo.HeadRow rh = ds.Head.NewHeadRow();
        rh.SearchCriteria = txtSearch.Text;
        ds.Head.Rows.Add(rh);
        foreach (Admin admin in admins)
        {
            AdministratorSearchInfo.AdministratorSearchInfoRow rd = ds._AdministratorSearchInfo.NewAdministratorSearchInfoRow();
            rd.InstitutionName = admin.Institution.InstitutionNameWithProgOfStudy;
            rd.UserId = Convert.ToString(admin.UserId);
            rd.FirstName = admin.FirstName;
            rd.LastName = admin.LastName;
            rd.UserName = admin.UserName;
            rd.HeadId = rh.HeadId;
            ds._AdministratorSearchInfo.Rows.Add(rd);
        }

        return ds;
    }

    private void ExportAdminUsers(ReportAction printActions)
    {
        SearchText = txtSearch.Text.Trim();
        if (rfvProgramOfStudy.IsValid)
        {
            Presenter.ExportAdminUsers(printActions);
        }
        else
        {
            gridAdmins.DataSource = null;
            gridAdmins.DataBind();
            gridAdmins.Visible = false;
        }
       
    }

    private void HideProgramofStudy()
    {
        trProgramofStudy.Visible = Presenter.CurrentContext.UserType == UserType.SuperAdmin;
    }
}
