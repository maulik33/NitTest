using System;
using System.Collections.Generic;
using NursingLibrary.Common;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class ADMIN_GroupView : PageBase<IGroupView, GroupPresenter>, IGroupView
{
    public string Name { get; set; }

    public int GroupId { get; set; }

    public int CohortId { get; set; }

    public string InstitutionId { get; set; }

    public int ProgramofStudyId
    {
        get { throw new NotImplementedException(); }
        set { throw new NotImplementedException(); }
    }

    public string CohortIds
    {
        get { throw new NotImplementedException(); }
    }

    public void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudies)
    {
        throw new ArgumentException();
    }

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.View);
    }

    #region IGroupView Members

    public void ShowGroup(Group group)
    {
        if (Presenter.CurrentContext.UserType == UserType.SuperAdmin)
        {
            trProgramofStudy.Visible = true;
            if (group.Institution.ProgramOfStudyId == (int)ProgramofStudyType.RN)
            {
                lblProgramofStudy.Text = ProgramofStudyType.RN.ToString();
            }
            else if (group.Institution.ProgramOfStudyId == (int)ProgramofStudyType.PN)
            {
                lblProgramofStudy.Text = ProgramofStudyType.PN.ToString();
            }
        }
        lblCohortName.Text = group.GroupName;
        lblCohort.Text = group.Cohort.CohortName;
        lblLocation.Text = group.Institution.InstitutionNameWithProgOfStudy;
    }

    #region Not used
    public void PopulateProgramForTest(Program program)
    {
        throw new NotImplementedException();
    }

    public void ShowGroupResults(IEnumerable<Group> groups, SortInfo sortMetaData)
    {
        throw new NotImplementedException();
    }

    public void SaveGroup(int newGroupId)
    {
        throw new NotImplementedException();
    }

    public void DeleteGroup(int groupId)
    {
        throw new NotImplementedException();
    }

    public void PopulateInstitution(IEnumerable<Institution> groups)
    {
        throw new NotImplementedException();
    }

    public void PopulateCohort(IEnumerable<Cohort> cohort)
    {
        throw new NotImplementedException();
    }

    public void RefreshPage(Group group, UserAction action, string title, string subTitle,
        bool hasDeletePermission, bool hasAddPermission)
    {
        lbNew.Visible = hasAddPermission;
    }

    public void PopulateGroupTest(GroupTestDates testDetails)
    {
        throw new NotImplementedException();
    }

    public void PopulateGroupTests(IEnumerable<GroupTestDates> testDetails)
    {
        throw new NotImplementedException();
    }

    public void ExportGroups(IEnumerable<Group> groups, ReportAction printActions)
    {
        throw new NotImplementedException();
    }
    #endregion

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region Trace Information
            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to Group View Page")
                                .Add("Group Id", Presenter.Id.ToString())
                                .Write();
            #endregion
        }

        Presenter.ShowGroup();
    }

    protected void lbEdit_Click(object sender, EventArgs e)
    {
        Presenter.NavigateToEdit(UserAction.Edit);
    }

    protected void lbNew_Click(object sender, EventArgs e)
    {
        Presenter.NavigateToEdit(UserAction.Add);
    }
}
