using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class ADMIN_UserListXML : PageBase<IUserListView, UserListPresenter>, IUserListView
{
    public bool IsUnAssigned { get; set; }

    public bool IfUserExists { get; set; }

    public int ProgramOfStudyId { get; set; }

    public int GroupId { get; set; }

    public int CohortId { get; set; }

    public int StudentId { get; set; }

    public string InstitutionId { get; set; }

    public string SearchString { get; set; }

    public string StudentStartDate { get; set; }

    public string StudentEndDate { get; set; }

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.List);
    }

    #region IUserList Members

    public void PopulateGroupForTest(Group group)
    {
    }

    public void PopulateStudentForTest(IEnumerable<Student> students)
    {      
    }

    public void PopulateProgramForTest(Program program)
    {
        throw new NotImplementedException();
    }

    public void SaveUser(int newGroupId)
    {
        throw new NotImplementedException();
    }

    public void DeleteUser(int groupId)
    {
        throw new NotImplementedException();
    }

    public void PopulateTests(IEnumerable<Test> tests)
    {
    }

    public void ShowErrorMessage(string message)
    {
        throw new NotImplementedException();
    }

    public void PopulateGroup(IEnumerable<Group> groups)
    {
        ddGroup.ClearData();
        if (groups.Count() > 0)
        {
            ControlHelper.PopulateGroups(ddGroup, groups);
        }

        ddGroup.Items.Insert(0, new ListItem("Select Group", "0"));
        ddGroup.SelectedIndex = 0;
    }

    public void PopulateInstitution(IEnumerable<Institution> Institutions)
    {
            ddInstitution.ClearData();
            if (Institutions.Count() > 0)
            {
                ControlHelper.PopulateInstitutions(ddInstitution, Institutions, true);
            }

            ddInstitution.Items.Insert(0, new ListItem("Default Institution", "0"));
            ddInstitution.SelectedIndex = 0;
            if (InstitutionId != string.Empty)
            {
                ddInstitution.SelectedValue = InstitutionId;
            }
    }

    public void PopulateCohort(IEnumerable<Cohort> cohorts)
    {
        ddCohort.ClearData();
        if (cohorts.Count() > 0)
        {
            ControlHelper.PopulateCohorts(ddCohort, cohorts);
        }

        ddCohort.Items.Insert(0, new ListItem("Default Cohort", "0"));
    }

    public void PopulateStudent(IEnumerable<Student> students, SortInfo sortMetaData)
    {
        gvStudents.DataSource = KTPSort.Sort<Student>(students, sortMetaData);
        gvStudents.DataBind();
    }

    public void RefreshPage(Student student, UserAction action, string title, string subTitle, bool hasDeletePermission, bool hasAddPermission, bool hasChangePermission)
    {
    }

    public void GetStudentDetails(Student student)
    {
        throw new NotImplementedException();
    }

    public void GetDatesByCohortId(StudentEntity student)
    {
        throw new NotImplementedException();
    }

    public void PopulateStudentTest(StudentTestDates testDates)
    {
        throw new NotImplementedException();
    }

    public void PopulateStudentTests(IEnumerable<StudentTestDates> testDates)
    {
        throw new NotImplementedException();
    }

    public void PopulateCountry(IEnumerable<Country> country)
    {
    }

    public void PopulateState(IEnumerable<State> state)
    {
    }

    public void PopulateAddress(Address address)
    {
    }

    public void PopulateAdhocGroupForTest(IEnumerable<AdhocGroupTestDetails> adhocGrouptestdetails)
    {
    }

    public void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudies)
    {
        ddlProgramOfStudy.DataSource = programofStudies;
        ddlProgramOfStudy.DataTextField = "ProgramOfStudyName";
        ddlProgramOfStudy.DataValueField = "ProgramOfStudyId";
        ddlProgramOfStudy.DataBind();       
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region Trace Information
            TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Assign User(Student) Page.");
            #endregion
            Presenter.ShowProgramDetails();
            SearchStudents();
            ddInstitution.Items.Insert(0, new ListItem("Default Institution", "0"));
            ddInstitution.SelectedIndex = 0;
        }
    }

    protected void gvStudents_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);
        SearchStudents();
    }

    protected void gvStudents_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        InstitutionId = ddInstitution.SelectedValue;
        CohortId = ddCohort.SelectedValue.ToInt();
        SearchStudents();
        gvStudents.PageIndex = e.NewPageIndex;
        gvStudents.DataBind();
    }

    protected void searchButton_Click(object sender, ImageClickEventArgs e)
    {
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Search Student Clicked.")
                            .Add("Institution Id ", InstitutionId)
                            .Add("Cohort Id ", CohortId.ToString())
                            .Add("Search Text ", txtSearch.Text)
                            .Write();
        #endregion
        lblM.Visible = false;
        lblM.Text = string.Empty;
        InstitutionId = ddInstitution.SelectedValue;
        CohortId = ddCohort.SelectedValue.ToInt();
        SearchStudents();
    }

    protected void ddInstitution_SelectedIndexChanged(object sender, EventArgs e)
    {
        InstitutionId = ddInstitution.SelectedValue;
        GroupId = 0;
        Presenter.GetCohortListForInstitute();
        if (!String.IsNullOrEmpty(ddCohort.SelectedValue))
        {
            CohortId = ddCohort.SelectedValue.ToInt();
        }

        SearchString = txtSearch.Text.Trim();
        IsUnAssigned = true;
        if (CohortId > 0)
        {
            Presenter.GetGroupListForInstitute();
        }
        else
        {
            ddGroup.ClearData();
            ddGroup.Items.Insert(0, new ListItem("Select Group", "0"));
        }
    }

    protected void ddCohort_SelectedIndexChanged(object sender, EventArgs e)
    {
        InstitutionId = ddInstitution.SelectedValue;
        CohortId = ddCohort.SelectedValue.ToInt();
        SearchString = txtSearch.Text.Trim();
        IsUnAssigned = true;
        if (CohortId > 0)
        {
            Presenter.GetGroupListForInstitute();
        }
    }

    protected void ddProgramofStudy_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddInstitution.ClearData();
        ddInstitution.Items.Insert(0, new ListItem("Default Institution", "0"));
        ddCohort.ClearData();
        ddCohort.Items.Insert(0, new ListItem("Default Cohort", "0"));
        ddGroup.ClearData();
        ddGroup.Items.Insert(0, new ListItem("Select Group", "0"));
        if (ddlProgramOfStudy.SelectedIndex > 0)
        {
            Presenter.GetInstitutionByProgramofStudy(ddlProgramOfStudy.SelectedValue.ToInt());
        }
      
    }

    protected void btnAssign_Click(object sender, ImageClickEventArgs e)
    {
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Assigne Student Clicked")
                            .Add("Institution Id ", InstitutionId)
                            .Add("Cohort Id ", CohortId.ToString())
                            .Add("Search Text ", txtSearch.Text)
                            .Write();
        #endregion
        if (ddInstitution.SelectedValue != "0" && ddCohort.SelectedValue != "0")
        {
            StringBuilder _assignedUser = new StringBuilder();

            foreach (GridViewRow row in gvStudents.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    int id = Convert.ToInt32(gvStudents.DataKeys[row.RowIndex].Values["UserID"].ToString());
                    CheckBox ch = (CheckBox)row.FindControl("chkSelect");
                    if (ch != null)
                    {
                        if (ch.Checked)
                        {
                            _assignedUser.Append(id.ToString() + "|");
                        }
                    }
                }
            }

            if (_assignedUser.Length == 0)
            {
                lblM.Visible = true;
                lblM.Text = "Select Students to assign";
            }
            else
            {
                string assignedUser = string.Empty;
                if (_assignedUser.ToString().EndsWith("|"))
                {
                    assignedUser = _assignedUser.ToString().Remove(_assignedUser.Length - 1, 1).ToString();
                }

                Presenter.AssignStudents(_assignedUser.ToString(), ddInstitution.SelectedValue.ToInt(), ddCohort.SelectedValue.ToInt(), ddGroup.SelectedValue.ToInt());
                SearchStudents();
                lblM.Text = "Selected Students have been assigned";
                lblM.Visible = true;
            }
        }
        else
        {
            lblM.Visible = true;
            lblM.Text = "Select Institution and Cohort";
        }
    }

    private void SearchStudents()
    {
        SearchString = txtSearch.Text.Trim();
        Presenter.GetUnAssignedStudentList(hdnGridConfig.Value);
    }
}
