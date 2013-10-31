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

public partial class Admin_InstitutionList : PageBase<IInstitutionView, InstitutionPresenter>, IInstitutionView
{
    private CrystalReport.ReportDocument rpt = new CrystalReport.ReportDocument();

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

    public Institution Institution
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public Program Program
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public IEnumerable<TimeZones> TimeZones
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public int IID
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public int ActionType
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public UserType UserTypeValue
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    #region IInstitutionListView Members
    public void ShowInstitutionResults(IEnumerable<Institution> institutions, SortInfo sortMetaData)
    {
        if (institutions.Count() > 0)
        {
            lblM.Visible = false;
        }
        else
        {
            lblM.Visible = true;
        }

        gridInstitutions.DataSource = KTPSort.Sort<Institution>(institutions, sortMetaData);
        gridInstitutions.DataBind();
    }

    public void ExportInstitutions(IEnumerable<Institution> reportData, ReportAction printActions)
    {
        rpt.Load(Server.MapPath("~/Admin/Report/Institution.rpt"));
        rpt.SetDataSource(BuildDataSourceForInstitution(KTPSort.Sort<Institution>(reportData, SortHelper.Parse(hdnGridConfig.Value))));
        switch (printActions)
        {
            case ReportAction.ExportExcel:
                rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Page.Response, true, "Institutions");
                break;
            case ReportAction.ExportExcelDataOnly:
                rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                rpt.ReportDefinition.Sections["Section5"].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Page.Response, true, "Institutions");
                break;
            case ReportAction.PDFPrint:
                rpt.ReportDefinition.Sections[2].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Page.Response, true, "Institutions");
                break;
        }
    }
    #endregion

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.View);
    }

    #region UNImplemented Method/Properties

    public string ErrorMessage
    {
        get
        {
            throw new NotImplementedException();
        }
        set
        {
            throw new NotImplementedException();
        }
    }

    public string Name
    {
        get
        {
            throw new NotImplementedException();
        }
        set
        {
            throw new NotImplementedException();
        }
    }

    public string ProgramOfStudy
    {
        get
        {
            throw new NotImplementedException();
        }
        set
        {
            throw new NotImplementedException();
        }
    }

    public void BindData()
    {
        throw new NotImplementedException();
    }

    public void BindData(IEnumerable<TimeZones> timeZones, IEnumerable<Program> nurPrograms, IEnumerable<ProgramofStudy> programOfStudies)
    {
        throw new NotImplementedException();
    }

    public void PopulateCountry(IEnumerable<Country> country)
    {
        throw new NotImplementedException();
    }

    public void PopulateState(IEnumerable<State> state)
    {
        throw new NotImplementedException();
    }

    public void PopulateAddress(Address address)
    {
        throw new NotImplementedException();
    }

    public void PopulateInstitutionContacts(IEnumerable<InstitutionContact> institutionContacts)
    {
        throw new NotImplementedException();
    }

    public void PopulateProgram(IEnumerable<Program> programs)
    {
        throw new ArgumentException();
    }

    #region ICohortView Members

    public void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudy)
    {
        ControlHelper.PopulateProgramOfStudy(ddlProgramofStudy, programofStudy);

    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Presenter.GetInstitutionList(hdnGridConfig.Value);
            SearchInstitutions();
        }
    }

    protected void gridInstitutions_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!e.CommandName.Equals("Sort") && !e.CommandName.Equals("Page"))
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gridInstitutions.Rows[index];
            string id = Server.HtmlDecode(row.Cells[0].Text);
            switch (e.CommandName)
            {
                case "Select":
                    Presenter.NavigateToEditPage(id.ToInt());
                    break;

                case "Assets":
                    Presenter.NavigatetoAssetList(id.ToInt());
                    break;
            }
        }
    }

    protected void ddProgramOfStudy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProgramofStudy.SelectedValue.ToInt() > 0)
        {
            SearchInstitutions();
        }
    }

    protected void gridInstitutions_PageIndexChanged(Object sender, EventArgs e)
    {
        gridInstitutions.Visible = true;
    }

    protected void searchButton_Click(object sender, ImageClickEventArgs e)
    {
        SearchInstitutions();
    }

    protected void gridInstitutions_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);
        SearchInstitutions();
    }

    protected void gridInstitutions_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SearchInstitutions();
        gridInstitutions.PageIndex = e.NewPageIndex;
        gridInstitutions.DataBind();
    }

    protected void btnPrintExcel_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.ExportInstitutions(txtSearch.Text, ReportAction.ExportExcel, statusRadioButton.SelectedValue, ddlProgramofStudy.SelectedItem.Text);
    }

    protected void btnPrintPDF_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.ExportInstitutions(txtSearch.Text, ReportAction.PDFPrint, statusRadioButton.SelectedValue, ddlProgramofStudy.SelectedItem.Text);
    }

    protected void btnPrintExcelDataOnly_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.ExportInstitutions(txtSearch.Text, ReportAction.ExportExcelDataOnly, statusRadioButton.SelectedValue, ddlProgramofStudy.SelectedItem.Text);
    }

    protected void statusRadioButton_SelectedIndexChanged(object sender, EventArgs e)
    {
        SearchInstitutions();
    }

    protected override void OnUnload(EventArgs e)
    {
        base.OnUnload(e);
        rpt.Close();
        rpt.Dispose();
    }

    private void BindGridView(GridView gridInstitutions, object nurInstitutions)
    {
        gridInstitutions.DataSource = nurInstitutions;
        gridInstitutions.DataBind();
    }

    private void SearchInstitutions()
    {
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Institutions List Page")
            .Add("Search Keyword", txtSearch.Text)
            .Write();
        #endregion
        Presenter.SearchInstitutions(txtSearch.Text, hdnGridConfig.Value, statusRadioButton.SelectedValue, ddlProgramofStudy.SelectedItem.Text);
    }

    private DataSet BuildDataSourceForInstitution(IEnumerable<Institution> institutions)
    {
        InstitutionSearchInfo ds = new InstitutionSearchInfo();
        InstitutionSearchInfo.HeadRow rh = ds.Head.NewHeadRow();
        rh.SearchCriteria = txtSearch.Text;
        ds.Head.Rows.Add(rh);
        foreach (Institution institution in institutions)
        {
            InstitutionSearchInfo.InstitutionRow rd = ds.Institution.NewInstitutionRow();
            rd.InstitutionId = institution.InstitutionId;
            rd.Name = institution.InstitutionNameWithProgOfStudy;
            rd.Description = institution.Description;
            rd.Center = institution.CenterId;
            rd.PhoneNumber = institution.ContactPhone;
            rd.TimeZone = institution.TimeZones.Description;
            rd.HeadId = rh.HeadId;
            ds.Institution.Rows.Add(rd);
        }

        return ds;
    }
}
    #endregion