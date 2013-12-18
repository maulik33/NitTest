using System;
using System.Collections.Generic;
using System.Text;
using NursingLibrary.Common;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class Admin_PopUpC : PageBase<ICohortView, CohortPresenter>, ICohortView
{
    public string Name { get; set; }

    public string StartDate { get; set; }

    public string EndDate { get; set; }

    public string Description { get; set; }

    public string SearchText { get; set; }

    public string InstitutionId { get; set; }

    public int CohortStatus { get; set; }

    public int GroupId { get; set; }

    public int CohortId { get; set; }

    public int ProductId { get; set; }

    public int TestId { get; set; }

    public int ProgramId { get; set; }

    public bool IsValidDate { get; set; }

    public bool HasAddPermission { get; set; }

    public string State { get; set; }

    public string Type { get; set; }

    public string ErrorMessage { get; set; }

    public bool IsInValidCohort { get; set; }

    public int ProgramofStudyId
    {
        get { throw new NotImplementedException(); }
        set { throw new NotImplementedException(); }
    }

    #region Page base abstract member
    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.List);
    }
    #endregion

    public void SaveCohort(int newCohortId)
    {
        throw new NotImplementedException();
    }

    public void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudy)
    {
        throw new NotImplementedException();
    }

    public void DeleteCohort()
    {
        throw new NotImplementedException();
    }

    public void PopulateInstitutions(IEnumerable<Institution> institutions)
    {
        throw new NotImplementedException();
    }

    public void PopulateCohorts(IEnumerable<Cohort> cohort, SortInfo sortMetaData)
    {
        throw new NotImplementedException();
    }

    public void PopulateProducts(IEnumerable<Product> products)
    {
        throw new NotImplementedException();
    }

    public void PopulateTests(IEnumerable<Test> tests)
    {
        throw new NotImplementedException();
    }

    public void PopulateGroups(IEnumerable<Group> groups)
    {
        throw new NotImplementedException();
    }

    public void ShowCohortResults(IEnumerable<Cohort> cohort)
    {
        throw new NotImplementedException();
    }

    public void PopulateStudents(IEnumerable<Student> students, NursingLibrary.Common.SortInfo sortMetaData)
    {
        throw new NotImplementedException();
    }

    public void PopulatePrograms(IEnumerable<Program> programs)
    {
        throw new NotImplementedException();
    }

    public void RefreshPage(Cohort cohort, UserAction action, string title, string subTitle, bool hasDeletePermission, bool hasAddPermission, bool hasAccessDatesEdit, bool hasEditPremission)
    {
        throw new NotImplementedException();
    }

    public void ShowCohort(Cohort group)
    {
        throw new NotImplementedException();
    }

    public void PopulateCohortTest(CohortTestDates testDetail)
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

    protected void Page_Load(object sender, EventArgs e)
    {
        control.Value = Presenter.GetControlValue();

        if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Popup Calender Page");
            #endregion
        }
    }

    protected void Change_Date(object sender, EventArgs e)
    {
        StringBuilder strScriptEmpty = new StringBuilder();
        strScriptEmpty.Append("<script>window.opener.document.forms[0]." + control.Value + ".value = '");
        strScriptEmpty.Append(calDate.SelectedDate.ToString("MM/dd/yyyy"));
        strScriptEmpty.Append("';self.close()");
        strScriptEmpty.Append("</" + "script>");
        ClientScript.RegisterClientScriptBlock(typeof(Admin_PopUpC), "subscribescript", strScriptEmpty.ToString());
    }


    public void ExportStudents(IEnumerable<Student> reportData, ReportAction printActions)
    {
        throw new NotImplementedException();
    }
}
