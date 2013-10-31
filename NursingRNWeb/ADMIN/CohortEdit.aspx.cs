using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class ADMIN_CohortEdit : PageBase<ICohortView, CohortPresenter>, ICohortView
{
    private const string WORNG_FORMAT = "Wrong date format";

    public string ErrorMessage { get; set; }

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
        Presenter.PreInitialize(ViewMode.Edit);
    }

    #region ICohortView Members

    public void ShowCohortResults(IEnumerable<Cohort> groups)
    {
        throw new NotImplementedException();
    }

    public void SaveCohort(int newCohortId)
    {
        throw new NotImplementedException();
    }

    public void PopulatePrograms(IEnumerable<Program> programs)
    {
        throw new NotImplementedException();
    }

    public void DeleteCohort()
    {
        throw new NotImplementedException();
    }

    public void PopulateInstitutions(IEnumerable<Institution> institutions)
    {
        ControlHelper.PopulateInstitutions(ddInstitution, institutions, true);
    }

    public void PopulateCohorts(IEnumerable<Cohort> Cohort, SortInfo sortMetaData)
    {
        throw new NotImplementedException();
    }

    public void PopulateStudents(IEnumerable<Student> students, SortInfo sortMetaData)
    {
        throw new NotImplementedException();
    }

    public void PopulateGroups(IEnumerable<Group> groups)
    {
        throw new NotImplementedException();
    }

    public void RefreshPage(Cohort cohort, UserAction action, string title, string subTitle, bool hasDeletePermission, bool hasAddPermission, bool hasAccessDatesEdit, bool hasEditPremission)
    {
        lblTitle.Text = title;
        lblSubTitle.Text = subTitle;
        if (action == UserAction.Edit)
        {
            btnDelete.Attributes.Add("onclick", " return confirm('Are you sure that you want to delete the Cohort?')");
        }

        if (!hasDeletePermission)
        {
            btnDelete.Visible = false;
            btnDelete.Enabled = false;
        }

        if (action == UserAction.Add)
        {
            ddInstitution.ShowNotAssigned = true;
            if (!hasAddPermission)
            {
                btnSave.Visible = false;
                btnSave.Enabled = false;
            }
        }
        else if (action == UserAction.Edit)
        {
            if (!hasEditPremission)
            {
                btnSave.Visible = false;
                btnSave.Enabled = false;
            }

            if (!hasAccessDatesEdit)
            {
                txtSD.Enabled = false;
                Image1.Visible = false;
                txtED.Enabled = false;
                Image2.Visible = false;
            }
        }

        if (Presenter.CurrentContext.UserType == UserType.InstitutionalAdmin || Presenter.CurrentContext.UserType == UserType.LocalAdmin)
        {
            ControlCollection cc = Page.Master.FindControl("contentplaceholder1").Controls;
            ControlHelper.DisablePageControlCollectionControls(cc, false);
            btnSave.Visible = false;
            btnDelete.Visible = false;
            Image1.Visible = false;
            Image2.Visible = false;
        }

        if (Presenter.CurrentContext.UserType != UserType.SuperAdmin)
        {
            trProgramodStudy.Visible = false;
        }
    }

    public void ShowCohort(Cohort cohort)
    {
        txtCohortName.Text = cohort.CohortName;
        txtDescription.Text = cohort.CohortDescription;
        RadioButtonList1.SelectedValue = cohort.CohortStatus.ToString();

        if (cohort.CohortEndDate != null)
        {
            txtED.Text = cohort.CohortEndDate.Value.ToShortDateString();
        }

        if (cohort.CohortStartDate != null)
        {
            txtSD.Text = cohort.CohortStartDate.Value.ToShortDateString();
        }

        ddInstitution.SelectedValue = cohort.InstitutionId.ToString();
        if (cohort.InstitutionId.ToString() == "0")
        {
            ddInstitution.Enabled = true;
        }
        else
        {
            ddInstitution.Enabled = false;
        }

        if (Presenter.ActionType == UserAction.Edit)
        {
            ddProgramofStudy.Visible = false;
            if (cohort.ProgramofStudyId == (int)ProgramofStudyType.PN)
            {
                lblProgramofStudyVal.Text = ProgramofStudyType.PN.ToString();
            }
            else if (cohort.ProgramofStudyId == (int)ProgramofStudyType.RN)
            {
                lblProgramofStudyVal.Text = ProgramofStudyType.RN.ToString();
            }
        }

        ddInstitution.ShowNotAssigned = true;
        ddProgramofStudy.SelectedValue = cohort.ProgramofStudyId.ToString();
    }

    public void PopulateProducts(IEnumerable<Product> products)
    {
        throw new NotImplementedException();
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
        ddInstitution.Items.Insert(0, new ListItem("Not Selected", "-1"));
    }
    #endregion

    protected void PopulateAnnotation_Click(object sender, EventArgs e)
    {
        mdlPopup.Show();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to Cohort Edit Page")
                                .Add("Cohort Id", Presenter.Id.ToString())
                                .Write();
            #endregion
            Presenter.ShowCohortDetails();
            if (Presenter.CurrentContext.UserType == UserType.SuperAdmin)
            {
                if (ddInstitution.SelectedValuesText != string.Empty)
                {
                    lblAnnotation.Text = Presenter.GetAnnotation(ddInstitution.SelectedValue.ToInt());
                    if (!string.IsNullOrEmpty(lblAnnotation.Text))
                    {
                        PopulateAnnotation.Visible = true;
                    }
                    else
                    {
                        PopulateAnnotation.Visible = false;
                    }
                }
            }
        }

        Image1.Attributes.Add("onclick", "window.open('popupC.aspx?textbox=" + txtSD.ClientID + "','cal','width=250,height=225,left=270,top=180')");
        Image2.Attributes.Add("onclick", "window.open('popupC.aspx?textbox=" + txtED.ClientID + "','cal','width=250,height=225,left=270,top=180')");
        Image1.Style.Add("cursor", "pointer");
        Image2.Style.Add("cursor", "pointer");
    }

    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        if (RequiredFieldValidator1.IsValid)
        {
            StartDate = txtSD.Text;
            EndDate = txtED.Text;
            InstitutionId = ddInstitution.SelectedValue;
            Name = txtCohortName.Text.Trim();
            Description = txtDescription.Text.Trim();
            Presenter.ValidateCohort();
            if (IsValidDate)
            {
                CohortStatus = RadioButtonList1.SelectedValue.ToInt();
                if (Presenter.ActionType == UserAction.Edit)
                {
                    CohortId = Presenter.Id;
                }
                else
                {
                    CohortId = 0;
                }

                if (!IsInValidCohort)
                {
                    Presenter.SaveCohort();
                }
            }
            else
            {
                lblM.Visible = true;
                lblM.Text = WORNG_FORMAT;
            }
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.DeleteCohort();
    }

    protected void ddInstitution_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateAnnotation.Visible = false;
        if (ddInstitution.SelectedValuesText != string.Empty)
        {
            lblAnnotation.Text = Presenter.GetAnnotation(ddInstitution.SelectedValue.ToInt());
            if (!string.IsNullOrEmpty(lblAnnotation.Text))
            {
                if (Presenter.CurrentContext.UserType == UserType.SuperAdmin)
                {
                    PopulateAnnotation.Visible = true;
                }
            }
        }
    }

    protected void ddProgramofStudy_SelectedIndexChanged(object sender, EventArgs e)
    {
        Presenter.PopulateInstitution(ddProgramofStudy.SelectedValue.ToInt());
    }


    public void ExportStudents(IEnumerable<Student> reportData, ReportAction printActions)
    {
        throw new NotImplementedException();
    }
}
