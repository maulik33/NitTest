using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;
using NursingRNWeb;

public partial class ADMIN_UserEdit : PageBase<IUserListView, UserListPresenter>, IUserListView
{
    private const string SELECT_DATE = "Please select date between ";
    private const string STRING_AND = " and ";
    private const string SELECT_COHORT = "Please select cohort ";
    private const string VALID_DATE_ERROR = "Please enter valid start and/or end date. ";
    private const string VALID_COHORT_DATE_ERROR = "Invalid Cohort start and/or end date. ";

    public int ProgramOfStudyId { get; set; }

    public int GroupId { get; set; }

    public int CohortId { get; set; }

    public string InstitutionId { get; set; }

    public int StudentId { get; set; }

    public string SearchString { get; set; }

    public bool IsUnAssigned { get; set; }

    public bool IfUserExists { get; set; }

    public string StudentStartDate { get; set; }

    public string StudentEndDate { get; set; }

    #region abstract members
    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.Edit);
    }
    #endregion

    #region IUserListView

    public void PopulateGroup(IEnumerable<Group> groups)
    {
        if (groups.Count() > 0)
        {
            ControlHelper.PopulateGroups(ddGroup, groups);
        }
        else
        {
            ddGroup.ClearData();
        }

        if (GroupId != 0)
        {
            ControlHelper.FindByValue(Convert.ToString(GroupId), ddGroup);
        }
    }

    public void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudy)
    {
        ControlHelper.PopulateProgramOfStudy(ddlProgramOfStudy, programofStudy);
    }

    public void PopulateInstitution(IEnumerable<Institution> institutions)
    {
        if (institutions.Count() > 0)
        {
            ControlHelper.PopulateInstitutions(ddInstitution, institutions, true);
        }
        else
        {
            ddInstitution.ClearData();
        }

        if (InstitutionId.ToInt() > 0)
        {
            ControlHelper.FindByValue(InstitutionId, ddInstitution);
        }
    }

    public void PopulateCohort(IEnumerable<Cohort> cohorts)
    {
        if (cohorts.Count() > 0)
        {
            ControlHelper.PopulateCohorts(ddCohort, cohorts);
        }
        else
        {
            ddCohort.ClearData();
        }

        if (CohortId != 0)
        {
            ControlHelper.FindByValue(CohortId.ToString(), ddCohort);
        }
    }

    public void ShowErrorMessage(string message)
    {
        lblMessage.Text = message;
    }

    public void PopulateStudent(IEnumerable<Student> students, SortInfo sortMetaData)
    {
    }

    public void PopulateGroupForTest(Group group)
    {
    }

    public void GetStudentDetails(Student student)
    {
        InstitutionId = student.Institution.InstitutionId.ToString();
        CohortId = student.Cohort.CohortId;
        GroupId = student.Group.GroupId;
        txtUserName.Text = student.UserName;
        txtIntegrated.Text = Convert.ToString(student.Integreted);
        txtPassword.Text = student.UserPass;
        txtEmail.Text = student.Email;
        txtFirstName.Text = student.FirstName;
        txtLastName.Text = student.LastName;
        txtUserStartDate.Text = student.UserStartDate.Format();
        txtUserExpireDate.Text = student.UserExpireDate.Format();
        txtKaplanID.Text = student.KaplanUserId;
        TextBox1.Text = student.EnrollmentId;
        rbtADA.SelectedValue = student.Ada == true ? "1" : "0";
        ddInstitution.SelectedValue = student.Institution.InstitutionId.ToString();
        ddInstitution.Enabled = true;
        TextBox1.Enabled = false;
        txtTelephone.Text = student.Telephone;
        txtEmergencyContactName.Text = student.ContactPerson;
        txtEmergencyPhone.Text = student.EmergencyPhone;

        ddCohort.SelectedValue = student.Cohort.CohortId.ToString();
        ddCohort.Enabled = true;

        if (GroupId == 0)
        {
            ddGroup.ShowNotAssigned = true;
        }
        else
        {
            ddGroup.SelectedValue = GroupId.ToString();
        }
        hdnExpiryDate.Value = student.RepeatExpiryDate;
        lblRepeatValue.Text = student.RepeatExpiryDate;
        hdnExpiryDateChange.Value = student.RepeatExpiryDate;
        SetRepeatControls(student.RepeatExpiryDate);
    }

    public void GetDatesByCohortId(StudentEntity student)
    {
        hdnStartDate.Value = student.StartDate;
        hdnEndDate.Value = student.EndDate;
    }

    public void DeleteUser(int groupId)
    {
    }

    public void SaveUser(int newGroupId)
    {
        throw new NotImplementedException();
    }

    public void RefreshPage(Student student, UserAction action, string title, string subTitle, bool hasDeletePermission, bool hasAddPermission, bool hasEditPermission)
    {
        lblTitle.Text = title;
        lblSubTitle.Text = subTitle;

        if (action == UserAction.Add)
        {
            btDelete.Visible = false;
            btDelete.Enabled = false;
            if (!hasAddPermission)
            {
                btSave.Visible = false;
                btSave.Enabled = false;
            }

            if (Presenter.CurrentContext.UserType == UserType.SuperAdmin)
            {
                lblProgOfStudyTxt.Visible = true;
                ddlProgramOfStudy.Visible = true;
            }

        }
        else if (action == UserAction.Edit)
        {
            btDelete.Visible = hasDeletePermission;
            btDelete.Enabled = hasDeletePermission;

            if (hasDeletePermission)
            {
                btDelete.Attributes.Add("onclick", " return confirm('Are you sure you want to delete student?')");
            }

            if (!hasEditPermission)
            {
                btSave.Visible = false;
                btSave.Enabled = false;
            }

            if (Presenter.CurrentContext.UserType != UserType.SuperAdmin)
            {
                ddInstitution.Enabled = false;
                ddCohort.Enabled = false;
                txtKaplanID.Enabled = false;
            }

            if (Presenter.CurrentContext.UserType == UserType.InstitutionalAdmin)
            {
                ControlCollection cc = Page.Master.FindControl("contentplaceholder1").Controls;
                ControlHelper.DisablePageControlCollectionControls(cc, false);
                ucAddress.DisableControls();
                btSave.Visible = false;
                btDelete.Visible = false;
            }

            if (Presenter.CurrentContext.UserType == UserType.LocalAdmin)
            {
                ControlCollection cc = Page.Master.FindControl("contentplaceholder1").Controls;
                ControlHelper.DisablePageControlCollectionControls(cc, false);
                ucAddress.DisableControls();
                DisableControlsForLocalAdmin();
            }

            if (Presenter.CurrentContext.UserType == UserType.SuperAdmin)
            {
                lblProgOfStudyTxt.Visible = true;
                lblProgOfStudy.Visible = true;
                lblProgOfStudy.Text = student.Institution.ProgramofStudyName;
            }
        }
        if (Presenter.CurrentContext.UserType == UserType.SuperAdmin)
        {
            if (student != null)
            {
                audit_trail.Visible = true;
                studentIdField.Value = student.UserId.ToString();
            }
        }
    }

    public void PopulateStudentTest(StudentTestDates testDates)
    {
        throw new NotImplementedException();
    }

    public void PopulateStudentTests(IEnumerable<StudentTestDates> testDates)
    {
        throw new NotImplementedException();
    }

    public void PopulateProgramForTest(Program program)
    {
        throw new NotImplementedException();
    }

    public void PopulateCountry(IEnumerable<Country> country)
    {
        if (Presenter.ActionType == UserAction.Add)
        {
            ucAddress.PopulateCountry(country, true);
        }
        else
        {
            ucAddress.PopulateCountry(country, false);
        }
    }

    public void PopulateState(IEnumerable<State> state)
    {
        ucAddress.PopulateState(state);
    }

    public void PopulateAddress(Address address)
    {
        ucAddress.SetAddressInformation(address, false);
    }

    public void PopulateStudentForTest(IEnumerable<Student> students)
    {
    }

    public void PopulateTests(IEnumerable<Test> tests)
    {
    }

    public void PopulateAdhocGroupForTest(IEnumerable<AdhocGroupTestDetails> adhocGrouptestdetails)
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ucAddress.OnCountrySelectionChange += new EventHandler<ItemSelectedEventArgs>(ucAddress_OnCountrySelectionChange);
        if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to User Edit Page")
                                .Add("User(Student) Id", Presenter.Id.ToString())
                                .Write();
            #endregion
            Presenter.GetStudentDetail();
            if ((Presenter.CurrentContext.UserType == UserType.SuperAdmin) && (Presenter.ActionType == UserAction.Edit))
            {
                ddCohort.Attributes.Add("onChange", "openStudentRepeat()");
                hdnCohort.Value = CohortId.ToString();
                hdnInstitution.Value = InstitutionId.ToString();
            }
        }

    }

    protected void btDelete_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.DeleteUser();
    }

    protected void btSave_Click(object sender, ImageClickEventArgs e)
    {

        if (Page.IsValid)
        {
            ////Verify student start date and end date in chort start date and end date --Min
            if (ddCohort.SelectedValue != string.Empty && ddCohort.SelectedValue.ToInt() != 0 && txtUserStartDate.Text != string.Empty && txtUserExpireDate.Text != string.Empty)
            {
                CohortId = ddCohort.SelectedValue.ToInt();
                InstitutionId = ddInstitution.SelectedValue;
                Presenter.GetDatesByCohortId();
                Label1.Visible = false;
                Label2.Visible = false;
                var result = IsValidDate(txtUserStartDate.Text);
                if (result)
                {
                    result = IsValidDate(txtUserExpireDate.Text);
                }

                if (result)
                {
                    if (!String.IsNullOrEmpty(hdnStartDate.Value) && !String.IsNullOrEmpty(hdnEndDate.Value))
                    {
                        if (DateTime.Parse(txtUserStartDate.Text) < DateTime.Parse(hdnStartDate.Value) || DateTime.Parse(txtUserStartDate.Text) > DateTime.Parse(hdnEndDate.Value))
                        {
                            Label2.Text = SELECT_DATE + hdnStartDate.Value + STRING_AND + hdnEndDate.Value;
                            Label2.Visible = true;
                            EnableRepeatControls();
                            OnLoad(e);
                        }
                        else if (DateTime.Parse(txtUserExpireDate.Text) < DateTime.Parse(hdnStartDate.Value) || DateTime.Parse(txtUserExpireDate.Text) > DateTime.Parse(hdnEndDate.Value))
                        {
                            Label1.Text = SELECT_DATE + hdnStartDate.Value + STRING_AND + hdnEndDate.Value;
                            Label1.Visible = true;
                            EnableRepeatControls();
                            OnLoad(e);
                        }
                        else
                        {
                            addStudent();
                        }
                    }
                    else
                    {
                        addStudent();
                    }
                }
                else
                {
                    Label1.Text = VALID_DATE_ERROR;
                    Label1.Visible = true;
                }
            }
            else
            {
                addStudent();
            }
        }
    }

    protected void ddCohort_SelectedIndexChanged(object sender, EventArgs e)
    {
        CohortId = ddCohort.SelectedValue.ToInt();
        InstitutionId = ddInstitution.SelectedValue;
        Presenter.GetGroupListForInstitute();
        if (CohortId > 0)
        {
            if (Label1.Text == SELECT_COHORT)
            {
                Label1.Visible = false;
                Label1.Text = string.Empty;
            }

            if (Label2.Text == SELECT_COHORT)
            {
                Label2.Visible = false;
                Label2.Text = string.Empty;
            }
        }

        if (Presenter.ActionType == UserAction.Edit)
        {
            if (txtUserStartDate.Text == string.Empty)
            {
                txtUserStartDate.Text = hdnStartDate.Value;
                txtUserExpireDate.Text = hdnEndDate.Value;
            }
            if ((Presenter.CurrentContext.UserType == UserType.SuperAdmin) && (hdnInstitution.Value.ToInt().ToInt() == ddInstitution.SelectedValue.ToInt()))
            {
                ddCohort.Attributes.Add("onChange", "openStudentRepeat()");
                EnableRepeatControls();

            }
        }
    }

    protected void ddlProgramOfStudy_SelectedIndexChanged(object sender, EventArgs e)
    {
        Presenter.GetInstitutionByProgramofStudy(ddlProgramOfStudy.SelectedValue.ToInt());
    }

    protected void ddInstitution_SelectedIndexChanged(object sender, EventArgs e)
    {
        InstitutionId = ddInstitution.SelectedValue;
        SearchString = string.Empty;
        lblRepeatValue.Text = string.Empty;
        hdnExpiryDateChange.Value = string.Empty;
        SetRepeatControls(hdnExpiryDateChange.Value);
        Presenter.GetCohortListForInstitute();
        CohortId = Convert.ToInt32(ddCohort.SelectedValue);
        Presenter.GetGroupListForInstitute();
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (txtUserStartDate.Text != string.Empty)
        {
            startDateCalendar.VisibleDate = Convert.ToDateTime(txtUserStartDate.Text);
        }

        startDateCalendar.Visible = true;
    }

    protected void startDateCalendar_SelectionChanged(object sender, EventArgs e)
    {
        if (ddCohort.SelectedValue.ToInt() > 0)
        {
            SearchString = string.Empty;
            CohortId = ddCohort.SelectedValue.ToInt();
            InstitutionId = ddInstitution.SelectedValue;
            GetCohortDates();
            if (String.IsNullOrEmpty(hdnStartDate.Value) && String.IsNullOrEmpty(hdnEndDate.Value))
            {
                Label2.Visible = false;
                txtUserStartDate.Text = startDateCalendar.SelectedDate.ToString();
                startDateCalendar.Visible = false;
            }
            else
            {
                var result = IsValidDate(hdnStartDate.Value);
                if (result)
                {
                    result = IsValidDate(hdnEndDate.Value);
                }

                if (result)
                {
                    if (startDateCalendar.SelectedDate >= DateTime.Parse(hdnStartDate.Value) && startDateCalendar.SelectedDate <= DateTime.Parse(hdnEndDate.Value))
                    {
                        Label2.Visible = false;
                        txtUserStartDate.Text = startDateCalendar.SelectedDate.ToString();
                        startDateCalendar.Visible = false;
                    }
                    else
                    {
                        Label2.Text = SELECT_DATE + hdnStartDate.Value + STRING_AND + hdnEndDate.Value;
                        Label2.Visible = true;
                    }
                }
                else
                {
                    Label1.Text = VALID_COHORT_DATE_ERROR;
                    Label1.Visible = true;
                    startDateCalendar.Visible = false;
                }
            }
        }
        else
        {
            Label2.Text = SELECT_COHORT;
            Label2.Visible = true;
            startDateCalendar.Visible = false;
        }
    }

    protected void btnEndDateCalendar_Click(object sender, ImageClickEventArgs e)
    {
        if (txtUserExpireDate.Text != string.Empty)
        {
            EndDateCalendar.VisibleDate = Convert.ToDateTime(txtUserExpireDate.Text);
        }

        EndDateCalendar.Visible = true;
    }

    protected void EndDateCalendar_SelectionChanged(object sender, EventArgs e)
    {
        if (ddCohort.SelectedValue.ToInt() > 0)
        {
            SearchString = string.Empty;
            CohortId = ddCohort.SelectedValue.ToInt();
            InstitutionId = ddInstitution.SelectedValue;
            GetCohortDates();
            if (String.IsNullOrEmpty(hdnStartDate.Value) && String.IsNullOrEmpty(hdnEndDate.Value))
            {
                Label1.Visible = false;
                txtUserExpireDate.Text = EndDateCalendar.SelectedDate.ToString();
                EndDateCalendar.Visible = false;
            }
            else
            {
                var result = IsValidDate(hdnStartDate.Value);
                if (result)
                {
                    result = IsValidDate(hdnEndDate.Value);
                }

                if (result)
                {
                    if (DateTime.Parse(hdnStartDate.Value) <= EndDateCalendar.SelectedDate && EndDateCalendar.SelectedDate <= DateTime.Parse(hdnEndDate.Value))
                    {
                        Label1.Visible = false;
                        txtUserExpireDate.Text = EndDateCalendar.SelectedDate.ToString();
                        EndDateCalendar.Visible = false;
                    }
                    else
                    {
                        Label1.Text = SELECT_DATE + hdnStartDate.Value + STRING_AND + hdnEndDate.Value;
                        Label1.Visible = true;
                    }
                }
                else
                {
                    Label1.Text = VALID_COHORT_DATE_ERROR;
                    Label1.Visible = true;
                    EndDateCalendar.Visible = false;
                }
            }
        }
        else
        {
            Label1.Text = SELECT_COHORT;
            Label1.Visible = true;
            EndDateCalendar.Visible = false;
        }
    }

    private bool IsValidDate(string dt)
    {
        DateTime _datestart;
        return DateTime.TryParse(dt, out _datestart);
    }

    private void addStudent()
    {
        var student = new Student();
        lblM.Visible = false;
        student.UserName = txtUserName.Text.Trim();
        student.UserType = "S";

        student.Email = txtEmail.Text.Trim();
        if (txtIntegrated.Text.Trim().Length != 0)
        {
            student.Integreted = Convert.ToInt32(txtIntegrated.Text.Trim());
        }
        else
        {
            student.Integreted = 0;
        }

        student.UserPass = txtPassword.Text.Trim();
        student.Group = new Group() { GroupId = Convert.ToInt32(ddGroup.SelectedValue) == -2 ? 0 : ddGroup.SelectedValue.ToInt() };
        student.Cohort = new Cohort() { CohortId = Convert.ToInt32(ddCohort.SelectedValue) };
        student.Institution = new Institution() { InstitutionId = Convert.ToInt32(ddInstitution.SelectedValue) };
        student.FirstName = txtFirstName.Text.Trim();
        student.LastName = txtLastName.Text.Trim();
        student.ExpireDate = txtUserExpireDate.Text.Trim();
        student.StartDate = txtUserStartDate.Text.Trim();
        student.KaplanUserId = txtKaplanID.Text.Trim();
        student.FirstName = txtFirstName.Text.Trim();
        student.LastName = txtLastName.Text.Trim();
        student.EmergencyPhone = txtEmergencyPhone.Text.Trim();
        student.ContactPerson = txtEmergencyContactName.Text.Trim();
        student.Telephone = txtTelephone.Text.Trim();
        if (TextBox1.Text.Trim().Length == 0)
        {
            student.EnrollmentId = string.Empty;
        }
        else
        {
            student.EnrollmentId = TextBox1.Text.Trim();
        }

        student.Ada = rbtADA.SelectedValue == "1" ? true : false;
        if (Presenter.ActionType == UserAction.Add)
        {
            student.UserId = 0;
        }
        else if (Presenter.ActionType == UserAction.Edit)
        {
            student.UserId = Presenter.Id;
            student.RepeatExpiryDate = hdnExpiryDateChange.Value;
        }
        var _addressInfo = ucAddress.GetAddressInformation();
        student.StudentAddress = _addressInfo;
        if (IsValidStudent(student))
        {
            var result = Presenter.SaveUser(student);
            lblM.Visible = result == -1 ? true : false;
        }
    }

    private bool IsValidStudent(Student student)
    {
        bool isValidstudent = true;

        if (Presenter.IsDuplicateUsername(student.UserName, student.UserId))
        {
            isValidstudent = false;
            ShowErrorMessage("User name already exists.");
        }

        return isValidstudent;
    }

    private void GetCohortDates()
    {
        if (String.IsNullOrEmpty(hdnStartDate.Value) || String.IsNullOrEmpty(hdnEndDate.Value))
        {
            Presenter.GetDatesByCohortId();
            Presenter.GetCohortListForInstitute();
        }
    }

    private void ucAddress_OnCountrySelectionChange(object sender, ItemSelectedEventArgs e)
    {
        var _countriesWithState = KTPApp.CountriesWithStates;
        var _countryId = e.SelectedValue.ToInt();

        if (_countriesWithState.Contains(_countryId.ToString()))
        {
            ucAddress.ShowStateAsTextBox(false);
            Presenter.PopulateStates(_countryId, 0);
        }
        else
        {
            ucAddress.ShowStateAsTextBox(true);
        }
    }

    private void DisableControlsForLocalAdmin()
    {
        ddCohort.Enabled = true;
        ddGroup.Enabled = true;
        btSave.Enabled = true;
        btDelete.Visible = false;
        txtUserStartDate.Enabled = true;
        ImageButton1.Enabled = true;
        startDateCalendar.Enabled = true;
        txtUserExpireDate.Enabled = true;
        btnEndDateCalendar.Enabled = true;
        EndDateCalendar.Enabled = true;
    }

    private void EnableRepeatControls()
    {
        if (hdnExpiryDateChange.Value == "undefined") hdnExpiryDateChange.Value = string.Empty;
        if (hdnExpiryDateChange.Value != string.Empty)
        {
            lblRepeatValue.Text = hdnExpiryDateChange.Value;
            lblRepeat.Style.Add("display", "inline");
            lblRepeatValue.Style.Add("display", "inline");
        }
        else
        {
            lblRepeatValue.Text = string.Empty;
            hdnExpiryDateChange.Value = string.Empty;
            lblRepeat.Style.Add("display", "none");
            lblRepeatValue.Style.Add("display", "none");
        }
    }

    private void SetRepeatControls(string repeatDate)
    {
        if ((Presenter.CurrentContext.UserType == UserType.SuperAdmin) && (!string.IsNullOrEmpty(repeatDate)))
        {
            lblRepeat.Style.Add("display", "inline");
            lblRepeatValue.Style.Add("display", "inline");

        }
        else
        {
            lblRepeat.Style.Add("display", "none");
            lblRepeatValue.Style.Add("display", "none");
        }
    }

    #endregion
}
