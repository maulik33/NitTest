using System;
using System.Collections.Generic;
using NursingLibrary.Common;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class ADMIN_CohortView : PageBase<ICohortView, CohortPresenter>, ICohortView
{
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

    public int ProgramofStudyId { get; set; }
   
    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.View);
    }

    #region ICohortView Members
    public void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudy)
    {
        throw new NotImplementedException();
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

    public void PopulateInstitutions(IEnumerable<Institution> institutions)
    {
    }

    public void PopulateCohorts(IEnumerable<Cohort> Cohort, SortInfo sortMetaData)
    {
        throw new NotImplementedException();
    }

    public void PopulateGroups(IEnumerable<Group> groups)
    {
        throw new NotImplementedException();
    }

    public void RefreshPage(Cohort group, UserAction action, string title, string subTitle, bool hasDeletePermission, bool hasAddPermission, bool hasAccessDatesEdit, bool hasEditPremission)
    {
        if (!hasAddPermission)
        {
            lbNew.Visible = false;
        }
    }

    public void PopulateProducts(IEnumerable<Product> products)
    {
        throw new NotImplementedException();
    }

    public void PopulateStudents(IEnumerable<Student> students, SortInfo sortMetaData)
    {
        throw new NotImplementedException();
    }

    public void ShowCohort(Cohort cohort)
    {
        if (Presenter.CurrentContext.UserType == UserType.SuperAdmin)
        {
            trProgramofStudy.Visible = true;
            if (cohort.ProgramofStudyId == (int)ProgramofStudyType.RN)
            {
                lblProgramofStudy.Text = ProgramofStudyType.RN.ToString();
            }
            else if (cohort.ProgramofStudyId == (int)ProgramofStudyType.PN)
            {
                lblProgramofStudy.Text = ProgramofStudyType.PN.ToString();
            }
        }

        CohortId = cohort.CohortId;
        lblCohortName.Text = cohort.CohortName;
        lblDescription.Text = cohort.CohortDescription;
        if (cohort.CohortEndDate != null)
        {
            lblED.Text = Convert.ToString(cohort.CohortEndDate.Value);
        }
        else
        {
            lblED.Text = string.Empty;
        }

        if (cohort.CohortStartDate != null)
        {
            lblSD.Text = Convert.ToString(cohort.CohortStartDate.Value);
        }
        else
        {
            lblSD.Text = string.Empty;
        }

        if (cohort.InstitutionId != 0)
        {
            lblLocation.Text = cohort.Institution.InstitutionNameWithProgOfStudy;
        }
        else
        {
            lblLocation.Text = string.Empty;
        }
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
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to Cohort View Page.")
                                .Add("Cohort Id", Presenter.Id.ToString())
                                .Write();
            #endregion
        }

        Presenter.ShowCohort();
    }

    protected void lbEdit_Click(object sender, EventArgs e)
    {
        Presenter.NavigateToEdit(UserAction.Edit);
    }

    protected void lbAssign_Click(object sender, EventArgs e)
    {
        Presenter.NavigateFromCohortList(AdminPageDirectory.CohortProgramAssign, CohortId);
    }

    protected void lbNew_Click(object sender, EventArgs e)
    {
        Presenter.NavigateToEdit(UserAction.Add);
    }


    public void ExportStudents(IEnumerable<Student> reportData, ReportAction printActions)
    {
        throw new NotImplementedException();
    }
}