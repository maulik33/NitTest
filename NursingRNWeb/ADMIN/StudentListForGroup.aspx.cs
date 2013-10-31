using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class ADMIN_StudentListForGroup : PageBase<IUserListView, UserListPresenter>, IUserListView
{
    private bool _canAssignStudents;

    public int ProgramOfStudyId { get; set; }

    public int GroupId { get; set; }

    public int CohortId { get; set; }

    public string InstitutionId { get; set; }

    public string SearchString { get; set; }

    public string StudentStartDate { get; set; }

    public string StudentEndDate { get; set; }

    public bool IsUnAssigned { get; set; }

    public int StudentId { get; set; }

    public bool IfUserExists { get; set; }

    #region IUserListView Members

    public void PopulateStudentForTest(IEnumerable<Student> students)
    {
    }
    
    public void PopulateStudentTest(StudentTestDates testDates)
    {
        throw new NotImplementedException();
    }

    public void ShowErrorMessage(string message)
    {
        throw new NotImplementedException();
    }

    public void PopulateGroup(IEnumerable<Group> groups)
    {
    }

    public void PopulateTests(IEnumerable<Test> tests)
    {
    }

    public void PopulateAdhocGroupForTest(IEnumerable<AdhocGroupTestDetails> adhocGrouptestdetails)
    {
    }

    public void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudy)
    {
        throw new NotImplementedException();
    }

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.List);
    }

    public void PopulateInstitution(IEnumerable<Institution> institutes)
    {
    }

    public void PopulateCohort(IEnumerable<Cohort> cohorts)
    {
    }

    public void PopulateStudent(IEnumerable<Student> students, SortInfo sortMetaData)
    {
        gvStudents.DataSource = KTPSort.Sort<Student>(students, sortMetaData);
        gvStudents.DataBind();
    }

    #region notused
    public void PopulateProgramForTest(Program program)
    {
        throw new NotImplementedException();
    }

    public void PopulateGroupForTest(Group group)
    {
        throw new NotImplementedException();
    }

    public void PopulateStudentTests(IEnumerable<StudentTestDates> testDates)
    {
        throw new NotImplementedException();
    }

    public void GetStudentDetails(Student student)
    {
        lblGroupName.Text = student.Group.GroupName;
        lblCohortName.Text = student.Cohort.CohortName;
        lblInstitutionName.Text = student.Institution.InstitutionNameWithProgOfStudy;
        lblProgram.Text = student.Program.ProgramName;
    }

    public void GetDatesByCohortId(StudentEntity student)
    {
        throw new NotImplementedException();
    }

    public int SaveUser(int newGroupId)
    {
        throw new NotImplementedException();
    }

    public void DeleteUser(int groupId)
    {
        throw new NotImplementedException();
    }

    void IUserListView.SaveUser(int newGroupId)
    {
        throw new NotImplementedException();
    }

    public void RefreshPage(Student student, UserAction action, string title, string subTitle, bool hasDeletePermission, bool hasAddPermission, bool hasAccessToAssign)
    {
        _canAssignStudents = hasAccessToAssign;

        if (!_canAssignStudents)
        {
            btAssign.Visible = false;
            btAssign.Enabled = false;
        }
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

    #endregion

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowStudetsList();
        }
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
                Presenter.NavigateToAssignTests(id.ToString());
            }
            else if (e.CommandName == "Edit")
            {
                Presenter.NavigateToEdit(id.ToString(), UserAction.Edit);
            }
        }
    }

    protected void gvStudents_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gv = (GridView)sender;
            int rowIndex = Convert.ToInt32(e.Row.RowIndex);
            var _isAsigned = ((HiddenField)e.Row.FindControl("IsAssigned")).Value.ToInt();

            CheckBox ch = (CheckBox)e.Row.FindControl("ch");
            if (ch != null)
            {
                if (_isAsigned == 1)
                {
                    ch.Checked = true;
                }
                else
                {
                    ch.Checked = false;
                }

                if (!_canAssignStudents)
                {
                    ch.Enabled = false;
                }
            }
        }
    }

    protected void btAssign_Click(object sender, ImageClickEventArgs e)
    {
        StringBuilder _assignedUser = new StringBuilder();
        StringBuilder _unAssignedUser = new StringBuilder();
        foreach (GridViewRow row in gvStudents.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                int _userId = Convert.ToInt32(gvStudents.DataKeys[row.RowIndex].Values["UserID"].ToString());
                CheckBox ch = (CheckBox)row.FindControl("ch");
                if (ch != null)
                {
                    if (ch.Checked)
                    {
                        _assignedUser.Append(_userId.ToString() + "|");
                    }
                    else
                    {
                        _unAssignedUser.Append(_userId.ToString() + "|");
                    }
                }
            }
        }

        Presenter.AssignStudentsToGroup(_assignedUser.ToString(), _unAssignedUser.ToString());
    }

    protected void gvStudents_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);
        ShowStudetsList();
    }

    private void ShowStudetsList()
    {
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to Students In Group.")
                            .Add("InstitutionId ", InstitutionId)
                            .Add("Group Id", Presenter.Id.ToString())
                            .Write();
        #endregion
        Presenter.ShowStduentsForGroup(hdnGridConfig.Value);
    }
}