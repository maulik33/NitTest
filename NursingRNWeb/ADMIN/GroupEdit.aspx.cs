using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class ADMIN_GroupEdit : PageBase<IGroupView, GroupPresenter>, IGroupView
{
    public int GroupId { get; set; }

    public int CohortId { get; set; }

    public string InstitutionId { get; set; }

    public string CohortIds { get; set; }

    public string Name
    {
        get
        {
            return txtGroupName.Text.Trim();
        }

        set
        {
            txtGroupName.Text = value;
        }
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

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.Edit);
    }

    #region IGroupView members
    public void DeleteGroup(int result)
    {
        Presenter.NavigateFromGroupList(AdminPageDirectory.GroupList, 0);
    }

    public void SaveGroup(int newGroupId)
    {
        Presenter.NavigateFromGroupList(AdminPageDirectory.GroupView, newGroupId);
    }

    public void PopulateInstitution(IEnumerable<Institution> institutes)
    {
        ControlHelper.PopulateInstitutions(ddInstitution, institutes, true);
        if (Presenter.ActionType == UserAction.Edit && InstitutionId.ToInt() > 0)
        {
            ControlHelper.FindByValue(InstitutionId, ddInstitution);
            lblProgramofStudyVal.Visible = true;
            lblProgramofStudyVal.Text = institutes.FirstOrDefault().ProgramofStudyName;
            ddlProgramofStudy.Visible = false;
        }
    }

    public void PopulateCohort(IEnumerable<Cohort> cohorts)
    {
        ControlHelper.PopulateCohorts(ddCohort, cohorts);
        if (CohortId != 0)
        {
            ControlHelper.FindByValue(CohortId.ToString(), ddCohort);
        }
    }

    public void ShowGroup(Group group)
    {
        CohortId = group.Cohort.CohortId;
        txtGroupName.Text = group.GroupName;
    }

    public void PopulateProgramForTest(Program program)
    {
        throw new NotImplementedException();
    }

    public void RefreshPage(Group group, UserAction action, string title, string subTitle,
        bool hasDeletePermission, bool hasAddPermission)
    {
        lblTitle.Text = title;
        lblSubTitle.Text = subTitle;
        if (action == UserAction.Edit)
        {
            var students = Presenter.GetStudentsForGroups(group.Cohort.CohortId, group.GroupId);
            var studentCount = students.Count(t => t.Group.GroupId == group.GroupId);
            btnDelete.Attributes.Add("onclick",
                                     studentCount > 0
                                         ? " alert('Please remove students from group before deleting the group');return false;"
                                         : " return confirm('Are you sure?')");
        }

        btnDelete.Visible = hasDeletePermission;
        btnSave.Visible = hasAddPermission;
    }

    #region notused

    public void ShowGroupResults(IEnumerable<Group> groups, SortInfo sortMetaData)
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

    public void ExportGroups(IEnumerable<Group> groups, ReportAction printActions)
    {
        throw new NotImplementedException();
    }
    #endregion

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
       if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to Group Edit Page.")
                                .Add("Group Id", Presenter.Id.ToString())
                                .Write();
            #endregion
            HideProgramofStudy();
            Presenter.ShowGroupDetails();
        }
    }

    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        CohortId = ddCohort.SelectedValue.ToInt();
        Presenter.SaveGroup();
    }

    protected void ddInstitution_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddInstitution.SelectedValue.Equals("-1"))
        {
            Presenter.GetCohortList(String.Empty);
        }
        else
        {
            Presenter.GetCohortList(ddInstitution.SelectedValue);    
        }
        
        InstitutionId = ddInstitution.SelectedValue;
    }

    protected  void ddProgramOfStudy_SelectedIndexChanged(object sender, EventArgs e)
    {
        Presenter.GetInstitutionByProgramofStudy(Convert.ToInt32(ddlProgramofStudy.SelectedValue));
        Presenter.GetCohortList(String.Empty);
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
      Presenter.DeleteGroup();
    }

    public void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudies)
    {
        ddlProgramofStudy.Visible = true;
        lblProgramofStudyVal.Visible = true;
        ddlProgramofStudy.DataSource = programofStudies;
        ddlProgramofStudy.DataTextField = "ProgramofStudyName";
        ddlProgramofStudy.DataValueField = "ProgramofStudyId";
        ddlProgramofStudy.DataBind();
        ddInstitution.Items.Insert(0, new ListItem("Not Selected", "0"));
    }
    
    private void HideProgramofStudy()
    {
        trProgramofStudy.Visible = Presenter.CurrentContext.UserType == UserType.SuperAdmin;
    }
}
