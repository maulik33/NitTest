using System;
using System.Collections.Generic;
using NursingLibrary.Common;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class ADMIN_UserView : PageBase<IUserListView, UserListPresenter>, IUserListView
{
    public bool IfUserExists { get; set; }

    public int ProgramOfStudyId { get; set; }

    public int GroupId { get; set; }

    public int CohortId { get; set; }

    public string InstitutionId { get; set; }

    public int StudentId { get; set; }

    public string SearchString { get; set; }

    public bool IsUnAssigned { get; set; }

    public string StudentStartDate { get; set; }

    public string StudentEndDate { get; set; }

    #region IUserListView members

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.View);
    }

    public void PopulateGroupForTest(Group group)
    {
    }

    public void ShowErrorMessage(string message)
    {
        throw new NotImplementedException();
    }

    public void PopulateProgramForTest(Program program)
    {
        throw new NotImplementedException();
    }

    public void PopulateGroup(IEnumerable<Group> groups)
    {
    }

    public void PopulateInstitution(IEnumerable<Institution> institutes)
    {
    }

    public void PopulateCohort(IEnumerable<Cohort> cohorts)
    {
    }

    public void PopulateCountry(IEnumerable<Country> country)
    {
        ucAddress.PopulateCountry(country, false);
    }

    public void PopulateState(IEnumerable<State> state)
    {
    }

    public void PopulateAddress(Address address)
    {
        ucAddress.SetAddressInformation(address, true);
    }

    public void GetStudentDetails(Student student)
    {
        InstitutionId = student.Institution.InstitutionId.ToString();
        CohortId = student.Cohort.CohortId;
        GroupId = student.Group.GroupId;
        lblUserName.Text = student.UserName;
        lblPassword.Text = Convert.ToString(student.UserPass);
        lblEmail.Text = Convert.ToString(student.Email);
        lblI.Text = student.Institution.InstitutionName;
        lblCohort.Text = student.Cohort.CohortName;
        lblGroup.Text = student.Group.GroupName;
        lblEmergencyContact.Text = student.ContactPerson;
        lblEmergencyPhone.Text = student.EmergencyPhone;
        lblPhone.Text = student.Telephone;
        lblProgOfStudy.Text = student.Institution.ProgramofStudyName;
    }

    public void GetDatesByCohortId(StudentEntity student)
    {
    }

    public void SaveUser(int newGroupId)
    {
        throw new NotImplementedException();
    }

    public void DeleteUser(int groupId)
    {
    }

    public void RefreshPage(Student student, UserAction action, string title, string subTitle, bool hasDeletePermission, bool hasAddPermission, bool hasChangePermission)
    {
    }

    public void PopulateStudent(IEnumerable<Student> students, SortInfo sortMetaData)
    {
    }

    public void PopulateStudentTest(StudentTestDates testDates)
    {
        throw new NotImplementedException();
    }

    public void PopulateStudentTests(IEnumerable<StudentTestDates> testDates)
    {
        throw new NotImplementedException();
    }

    public void PopulateStudentForTest(IEnumerable<Student> students)
    {
        throw new NotImplementedException();
    }

    public void PopulateAdhocGroupForTest(IEnumerable<AdhocGroupTestDetails> adhocGrouptestdetails)
    {
    }

    public void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudy)
    {
        throw new NotImplementedException();
    }

    public void PopulateTests(IEnumerable<Test> tests)
    {
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to User View Page")
                                .Add("User(Student) Id", Presenter.Id.ToString())
                                .Write();
            #endregion
            Presenter.ShowUser();
        }
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
