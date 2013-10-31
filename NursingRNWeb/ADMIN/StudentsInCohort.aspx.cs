using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;
using System.Web.UI;
using CrystalReport = CrystalDecisions.CrystalReports.Engine;
using System.Data;
using NursingRNWeb.AppCode.Report_Ds;

public partial class ADMIN_StudentsInCohort : PageBase<ICohortView, CohortPresenter>, ICohortView
{
    private bool _useSearchString = false;
    private CrystalReport.ReportDocument rpt = new CrystalReport.ReportDocument();

    public bool HasAddPermission { get; set; }

    public string SearchText { get; set; }

    public int GroupId { get; set; }

    public int CohortId { get; set; }

    public int ProductId { get; set; }

    public string InstitutionId { get; set; }

    public string Name { get; set; }

    public string StartDate { get; set; }

    public string EndDate { get; set; }

    public string Description { get; set; }

    public int CohortStatus { get; set; }

    public bool IsValidDate { get; set; }

    public int ProgramId { get; set; }

    public int TestId { get; set; }

    public string State { get; set; }

    public string Type { get; set; }

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
        set
        {
            if (value > 0)
            {
                ddProgramofStudy.SelectedValue = value.ToString();
            }
        }

    }

    #region Page base abstract member
    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.List);
    }
    #endregion

    #region ICohortView Members
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

    public void PopulateInstitutions(IEnumerable<Institution> institutions)
    {
        ControlHelper.PopulateInstitutions(ddInstitution, institutions, true);
    }

    public void PopulateCohorts(IEnumerable<Cohort> cohorts, SortInfo sortMetaData)
    {
        ControlHelper.PopulateCohorts(ddCohort, cohorts);
        if (CohortId != 0)
        {
            ControlHelper.FindByValue(CohortId.ToString(), ddCohort);
        }
    }

    public void PopulateGroups(IEnumerable<Group> groups)
    {
        if (groups.Count() > 0)
        {
            ControlHelper.PopulateGroups(ddGroup, groups);
        }
        else
        {
            ddGroup.ClearData();
        }
    }

    public void PopulateStudents(IEnumerable<Student> students, SortInfo sortMetaData)
    {
        gvStudents.DataSource = KTPSort.Sort<Student>(students, sortMetaData);
        gvStudents.DataBind();
        lblStudentNumber.Text = students.Count().ToString();
    }

    public void RefreshPage(Cohort group, UserAction action, string instiutionId, string subTitle, bool hasDeletePermission, bool hasAddPermission, bool hasAccessDatesEdit, bool hasEditPremission)
    {
        hdnInstitutionId.Value = instiutionId;
    }

    public void PopulateProducts(IEnumerable<Product> products)
    {
        throw new NotImplementedException();
    }

    public void ShowCohort(Cohort cohort)
    {
    }

    public void PopulateTests(IEnumerable<Test> tests)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    public void PopulateStates(IEnumerable<State> states)
    {
        throw new NotImplementedException();
    }

    public void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudy)
    {
        ControlHelper.PopulateProgramOfStudy(ddProgramofStudy, programofStudy);
        trProgramodStudy.Visible = true;
    }
    #endregion

    #region
    public void ExportStudents(IEnumerable<Student> reportData, ReportAction printActions)
    {
        rpt.Load(Server.MapPath("~/Admin/Report/StudentsinCohort.rpt"));

        rpt.SetDataSource(BuildDataSourceForCohortExport(KTPSort.Sort<Student>(reportData, SortHelper.Parse(hdnGridConfig.Value))));
        switch (printActions)
        {
            case ReportAction.ExportExcel:
                rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Page.Response, true, "Students");
                break;
            case ReportAction.ExportExcelDataOnly:
                rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                rpt.ReportDefinition.Sections["Section5"].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Page.Response, true, "Students");
                break;
            case ReportAction.PDFPrint:
                rpt.ReportDefinition.Sections[2].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Page.Response, true, "Students");
                break;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Presenter.ShowStudentsforCohort();
            ShowStudentList();

            if (InstitutionId != null)
            {
                ControlHelper.FindByValue(InstitutionId, ddInstitution);
                hdnInstitutionId.Value = InstitutionId;
            }
        }
    }

    protected void ddProgramofStudy_SelectedIndexChanged(object sender, EventArgs e)
    {
        Presenter.PopulateInstitution(ProgramofStudyId);
        InstitutionId = ddInstitution.SelectedValue;
        SearchText = string.Empty;
        hdnInstitutionId.Value = InstitutionId;
        Presenter.GetCohortList(InstitutionId);
        ClearGroup();
        CohortId = 0;
        GroupId = 0;
        ShowStudentList();
    }

    protected void ddInstitution_SelectedIndexChanged(object sender, EventArgs e)
    {
        InstitutionId = ddInstitution.SelectedValue;
        SearchText = string.Empty;
        hdnInstitutionId.Value = InstitutionId;
        Presenter.GetCohortList(InstitutionId);
        ClearGroup();
        CohortId = 0;
        GroupId = 0;
        ShowStudentList();
    }


    protected void ddGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        InstitutionId = ddInstitution.SelectedValue;
        CohortId = ddCohort.SelectedValue.ToInt();
        GroupId = ddGroup.SelectedValue.ToInt();
        ShowStudentList();
    }

    protected void gvStudents_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!e.CommandName.Equals("Sort") && !e.CommandName.Equals("Page"))
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvStudents.Rows[index];
            int id = Convert.ToInt32(gvStudents.DataKeys[row.RowIndex].Values["UserID"].ToString());

            if (e.CommandName == "Tests")
            {
                Presenter.NavigateFromStudentList(AdminPageDirectory.UserTestDates, id);
            }
            else if (e.CommandName == "Edit")
            {
                Presenter.NavigateFromStudentList(AdminPageDirectory.UserEdit, id);
            }
        }
    }

    protected void ddCohort_SelectedIndexChanged(object sender, EventArgs e)
    {
        InstitutionId = ddInstitution.SelectedValue;
        CohortId = ddCohort.SelectedValue.ToInt();
        GroupId = ddGroup.SelectedValue.ToInt();
        Presenter.ShowGroups();
        ShowStudentList();
    }

    protected void gvStudents_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);
        ShowStudentList();
    }

    protected void btnPrintExcel_Click(object sender, ImageClickEventArgs e)
    {
        ExportStudentsInCohorts(ReportAction.ExportExcel);
    }

    protected void btnPrintPDF_Click(object sender, ImageClickEventArgs e)
    {
        ExportStudentsInCohorts(ReportAction.PDFPrint);
    }

    protected void btnPrintExcelDataOnly_Click(object sender, ImageClickEventArgs e)
    {
        ExportStudentsInCohorts(ReportAction.ExportExcelDataOnly);
    }

    private void ExportStudentsInCohorts(ReportAction printAction)
    {
        InstitutionId = hdnInstitutionId.Value;
        CohortId = ddCohort.SelectedValue.ToInt();
        GroupId = ddGroup.SelectedValue.ToInt();
        Presenter.ExportStudentsinCohorts(printAction, _useSearchString);
    }

    private void ShowStudentList()
    {
        InstitutionId = hdnInstitutionId.Value;
        CohortId = ddCohort.SelectedValue.ToInt();
        GroupId = ddGroup.SelectedValue.ToInt();
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to Students In cohorts.")
                            .Add("InstitutionId ", InstitutionId)
                            .Add("Cohort Id ", ddCohort.SelectedValue)
                            .Add("Group Id", ddGroup.SelectedValue)
                            .Write();
        #endregion
        Presenter.GetStudentsForCohort(hdnGridConfig.Value);
    }


    private void ClearGroup()
    {
        ControlHelper.PopulateGroups(ddGroup, new List<Group>());
    }

    private DataSet BuildDataSourceForCohortExport(IEnumerable<Student> studentsInCohorts)
    {
        StudentsInCohort ds = new StudentsInCohort();
        StudentsInCohort.HeadRow rh = ds.Head.NewHeadRow();
        rh.InstitutionName = ddInstitution.SelectedItemsText;
        rh.CohortName = ddCohort.SelectedItemsText;
        rh.GroupName = ddGroup.SelectedItemsText;
        rh.ProgramofStudyName = ddProgramofStudy.SelectedItemsText;
        ds.Head.Rows.Add(rh);
        foreach (Student student in studentsInCohorts)
        {
            StudentsInCohort.DetailsRow rd = ds.Details.NewDetailsRow();
            rd.LastName = student.LastName;
            rd.FirstName = student.FirstName;
            rd.HeadID = rh.HeadID;
            ds.Details.Rows.Add(rd);
        }

        return ds;
    }
}
