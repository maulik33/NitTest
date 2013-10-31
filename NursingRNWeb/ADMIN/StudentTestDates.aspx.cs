using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NursingLibrary;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class ADMIN_StudentTestDates : PageBase<IUserListView, UserListPresenter>, IUserListView
{
    private bool _canEditDates;

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

    public void PopulateGroup(IEnumerable<Group> groups)
    {
    }

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.List);
    }

    public void RefreshPage(Student student, UserAction action, string title, string subTitle, bool hasDeletePermission, bool hasAddPermission, bool hasChangePermission)
    {
        _canEditDates = hasChangePermission;
        if (!_canEditDates)
        {
            btnAssign.Visible = false;
            btnAssign.Enabled = false;
        }
    }

    public void PopulateStudentTests(IEnumerable<StudentTestDates> testDates)
    {
        gvTests.DataSource = testDates;
        gvTests.DataBind();
    }

    public void PopulateProgramForTest(Program program)
    {
        if (program != null)
        {
            lblPN.Text = program.ProgramName;
            lblDescription.Text = program.Description;
            HiddenProgramId.Value = program.ProgramId.ToString();
        }
    }

    public void PopulateGroupForTest(Group group)
    {
        HiddenGroupId.Value = group.GroupId.ToString();
        lblGroup.Text = group.GroupName;
    }

    public void PopulateStudentTest(StudentTestDates testDate)
    {
        HiddenCohortId.Value = testDate.Cohort.CohortId.ToString();
        HiddenStudentId.Value = testDate.Student.UserId.ToString();
        lblCName.Text = testDate.Cohort.CohortName;
        lblIN.Text = testDate.Institution.InstitutionNameWithProgOfStudy;
        lblStudent.Text = testDate.Student.FirstName + " " + testDate.Student.LastName;
        lblCED.Text = testDate.Cohort.CohortEndDate.Format();
        lblCSD.Text = testDate.Cohort.CohortStartDate.Format();
    }

    public void PopulateTests(IEnumerable<Test> tests)
    {
    }

    #region notused

    public void ShowErrorMessage(string message)
    {
        throw new NotImplementedException();
    }

    public void PopulateInstitution(IEnumerable<Institution> institutes)
    {
    }

    public void PopulateCohort(IEnumerable<Cohort> cohorts)
    {
    }

    public void PopulateStudent(IEnumerable<Student> students, SortInfo sortMetaData)
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

    public void SaveUser(int newGroupId)
    {
        throw new NotImplementedException();
    }

    public void DeleteUser(int groupId)
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

    public void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudy)
    {
        throw new NotImplementedException();
    }

    public void PopulateStudentForTest(IEnumerable<Student> students)
    {
    }

    #endregion

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Presenter.ShowTestDatesForStudents();
        }
    }

    protected void gvTests_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int id = gvTests.DataKeys[e.Row.RowIndex].Values["TestId"].ToInt();
            string Type = gvTests.DataKeys[e.Row.RowIndex].Values["Type"].ToString();

            System.Web.UI.HtmlControls.HtmlAnchor LnkCalendar = (HtmlAnchor)e.Row.FindControl("LnkCalendar");
            System.Web.UI.HtmlControls.HtmlInputText tbSD = (HtmlInputText)e.Row.FindControl("tbSD");
            LnkCalendar.Attributes.Add("href", "Javascript:pickDate('" + tbSD.Name + "')");

            DropDownList ddTime_S = (DropDownList)e.Row.FindControl("ddTime_S");
            DropDownList ddMin_S = (DropDownList)e.Row.FindControl("ddMin_S");
            DropDownList ddAMPM_S = (DropDownList)e.Row.FindControl("ddAMPM_S");

            System.Web.UI.HtmlControls.HtmlAnchor LnkCalendar2 = (HtmlAnchor)e.Row.FindControl("LnkCalendar2");
            System.Web.UI.HtmlControls.HtmlInputText tbED = (HtmlInputText)e.Row.FindControl("tbED");
            LnkCalendar2.Attributes.Add("href", "Javascript:pickDate('" + tbED.Name + "')");

            DropDownList ddTime_E = (DropDownList)e.Row.FindControl("ddTime_E");
            DropDownList ddMin_E = (DropDownList)e.Row.FindControl("ddMin_E");
            DropDownList ddAMPM_E = (DropDownList)e.Row.FindControl("ddAMPM_E");

            if (!_canEditDates)
            {
                LnkCalendar.Visible = false;
                LnkCalendar2.Visible = false;
                e.Row.Cells[3].Visible = true;
                e.Row.Cells[6].Visible = false;
            }

            if (tbSD != null && tbED != null)
            {
                var _startDate = ((HiddenField)e.Row.FindControl("TestStartDate")).Value;
                var _endDate = ((HiddenField)e.Row.FindControl("TestEndDate")).Value;
                if (Convert.ToString(_startDate).Length >= 10)
                {
                    tbSD.Value = ControlHelper.FormatDate(Convert.ToString(_startDate));
                }
                else
                {
                    tbSD.Value = string.Empty;
                }

                if (Convert.ToString(_endDate).Length >= 10)
                {
                    tbED.Value = ControlHelper.FormatDate(Convert.ToString(_endDate));
                }
                else
                {
                    tbED.Value = string.Empty;
                }

                DateTime _date;
                string _dayOrEvening;
                if (_startDate.Trim() != string.Empty)
                {
                    _date = Convert.ToDateTime(_startDate);
                    ddTime_S.SelectedValue = ControlHelper.TranslateHours(Convert.ToString(_date.Hour));
                    ddMin_S.SelectedValue = _date.Minute.ToString("00");
                    _dayOrEvening = StringFunctions.Right(_startDate.Trim(), 2);
                    if (_dayOrEvening == "AM" || _dayOrEvening == "PM")
                    {
                        ddAMPM_S.SelectedValue = _dayOrEvening;
                    }
                }

                if (_endDate.Trim() != string.Empty)
                {
                    _date = Convert.ToDateTime(_endDate);
                    ddTime_E.SelectedValue = ControlHelper.TranslateHours(Convert.ToString(_date.Hour));
                    ddMin_E.SelectedValue = _date.Minute.ToString("00");
                    _dayOrEvening = StringFunctions.Right(_endDate.Trim(), 2);
                    if (_dayOrEvening == "AM" || _dayOrEvening == "PM")
                    {
                        ddAMPM_E.SelectedValue = _dayOrEvening;
                    }
                }
            }
        }
    }

    protected void gvTests_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = gvTests.Rows[index];
        int id;

        if (e.CommandName == "SetAll")
        {
            System.Web.UI.HtmlControls.HtmlInputText tbSD = (HtmlInputText)row.FindControl("tbSD");
            System.Web.UI.HtmlControls.HtmlInputText tbED = (HtmlInputText)row.FindControl("tbED");

            DropDownList ddTime_E = (DropDownList)row.FindControl("ddTime_E");
            DropDownList ddAMPM_E = (DropDownList)row.FindControl("ddAMPM_E");
            DropDownList ddMin_E = (DropDownList)row.FindControl("ddMin_E");

            DropDownList ddTime_S = (DropDownList)row.FindControl("ddTime_S");
            DropDownList ddAMPM_S = (DropDownList)row.FindControl("ddAMPM_S");
            DropDownList ddMin_S = (DropDownList)row.FindControl("ddMin_S");

            foreach (GridViewRow r in gvTests.Rows)
            {
                CheckBox cb = (CheckBox)r.Cells[4].FindControl("CheckBox1");
                if (cb.Checked)
                {
                    ((HtmlInputText)r.FindControl("tbSD")).Value = tbSD.Value;
                    ((HtmlInputText)r.FindControl("tbED")).Value = tbED.Value;
                    ((DropDownList)row.FindControl("ddTime_E")).SelectedIndex = ddTime_E.SelectedIndex;
                    ((DropDownList)row.FindControl("ddMin_E")).SelectedIndex = ddMin_E.SelectedIndex;
                    ((DropDownList)row.FindControl("ddAMPM_E")).SelectedIndex = ddAMPM_E.SelectedIndex;

                    ((DropDownList)row.FindControl("ddMin_S")).SelectedIndex = ddMin_S.SelectedIndex;
                    ((DropDownList)row.FindControl("ddTime_S")).SelectedIndex = ddTime_S.SelectedIndex;
                    ((DropDownList)row.FindControl("ddAMPM_S")).SelectedIndex = ddAMPM_S.SelectedIndex;
                }
            }
        }
        else if (e.CommandName == "Save")
        {
            List<StudentTestDates> lstStudentTestDate = new List<StudentTestDates>();
            id = gvTests.DataKeys[row.RowIndex].Values["TestId"].ToInt();
            int type = gvTests.DataKeys[row.RowIndex].Values["Type"].ToInt();

            HtmlInputText tbSD = (HtmlInputText)row.FindControl("tbSD");
            HtmlInputText tbED = (HtmlInputText)row.FindControl("tbED");

            DropDownList ddTime_E = (DropDownList)row.FindControl("ddTime_E");
            DropDownList ddMin_E = (DropDownList)row.FindControl("ddMin_E");
            DropDownList ddAMPM_E = (DropDownList)row.FindControl("ddAMPM_E");

            DropDownList ddTime_S = (DropDownList)row.FindControl("ddTime_S");
            DropDownList ddMin_S = (DropDownList)row.FindControl("ddMin_S");
            DropDownList ddAMPM_S = (DropDownList)row.FindControl("ddAMPM_S");

            var productType = row.Cells[1].Text;

            var objDate = new StudentTestDates();
            objDate.TestStartDate = tbSD.Value.Trim();
            objDate.TestEndDate = tbED.Value.Trim();
            objDate.TestStartHour = ddTime_S.SelectedValue.ToInt();
            objDate.TestEndHour = ddTime_E.SelectedValue.ToInt();
            objDate.TestStartTime = ddAMPM_S.SelectedValue;
            objDate.TestEndTime = ddAMPM_E.SelectedValue;
            objDate.TestEndMin = ddMin_E.SelectedValue.ToInt();
            objDate.TestStartMin = ddMin_S.SelectedValue.ToInt();
            objDate.Type = type;
            objDate.Product = new Product() { ProductId = id, ProductType = productType };
            objDate.Cohort = new Cohort() { CohortId = HiddenCohortId.Value.ToInt() };
            objDate.Group = new Group() { GroupId = HiddenGroupId.Value.ToInt() };
            objDate.TestName = gvTests.Rows[row.RowIndex].Cells[0].Text.Trim();
            lstStudentTestDate.Add(objDate);
            lblM.Visible = true;
            #region Trace Information
            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Assign Student Test Dates.")
                .Add("Cohort Id", HiddenCohortId.Value)
                .Add("Test Id", id.ToString())
                .Add("Program Name", lblPN.Text)
                .Add("Institution Name", lblIN.Text)
                .Write();
            #endregion
            lblM.Text = Presenter.AssignTestDateToStudent(lstStudentTestDate);
        }
    }

    protected void btnAssign_Click(object sender, ImageClickEventArgs e)
    {
        List<StudentTestDates> lstStudentTestDate = new List<StudentTestDates>();
        var objDate = new StudentTestDates();
        foreach (GridViewRow row in gvTests.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                int id = gvTests.DataKeys[row.RowIndex].Values["TestId"].ToInt();
                int type = gvTests.DataKeys[row.RowIndex].Values["Type"].ToInt();

                System.Web.UI.HtmlControls.HtmlInputText tbSD = (HtmlInputText)row.FindControl("tbSD");
                System.Web.UI.HtmlControls.HtmlInputText tbED = (HtmlInputText)row.FindControl("tbED");

                DropDownList ddTime_E = (DropDownList)row.FindControl("ddTime_E");
                DropDownList ddMin_E = (DropDownList)row.FindControl("ddMin_E");
                DropDownList ddAMPM_E = (DropDownList)row.FindControl("ddAMPM_E");

                DropDownList ddTime_S = (DropDownList)row.FindControl("ddTime_S");
                DropDownList ddMin_S = (DropDownList)row.FindControl("ddMin_S");
                DropDownList ddAMPM_S = (DropDownList)row.FindControl("ddAMPM_S");


                var productType = row.Cells[1].Text;

                objDate = new StudentTestDates();
                objDate.TestStartDate = tbSD.Value.Trim();
                objDate.TestEndDate = tbED.Value.Trim();
                objDate.TestStartHour = ddTime_S.SelectedValue.ToInt();
                objDate.TestEndHour = ddTime_E.SelectedValue.ToInt();
                objDate.TestStartTime = ddAMPM_S.SelectedValue;
                objDate.TestEndTime = ddAMPM_E.SelectedValue;
                objDate.TestEndMin = ddMin_E.SelectedValue.ToInt();
                objDate.TestStartMin = ddMin_S.SelectedValue.ToInt();
                objDate.Type = type;
                objDate.Product = new Product() { ProductId = id, ProductType = productType };
                objDate.Cohort = new Cohort() { CohortId = HiddenCohortId.Value.ToInt() };
                objDate.Group = new Group() { GroupId = HiddenGroupId.Value.ToInt() };
                objDate.Test = new Test() { TestId = id };
                objDate.Student = new Student() { UserId = HiddenStudentId.Value.ToInt() };
                objDate.TestName = gvTests.Rows[row.RowIndex].Cells[0].Text.Trim();
                lstStudentTestDate.Add(objDate);
            }
        }

        lblM.Visible = true;
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Assign Student Test Dates.")
            .Add("Cohort Id ", HiddenCohortId.Value)
            .Add("Program Name ", lblPN.Text)
            .Add("Institution Name ", lblIN.Text)
            .Write();
        #endregion
        lblM.Text = Presenter.AssignTestDateToStudent(lstStudentTestDate);
    }

    protected void seabtn_Click(object sender, ImageClickEventArgs e)
    {
        lblM.Text = string.Empty;
        lblM.Visible = false;
        CohortId = HiddenCohortId.Value.ToInt();
        GroupId = HiddenGroupId.Value.ToInt();
        StudentId = HiddenStudentId.Value.ToInt();
        SearchString = TextBox1.Text;
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Assign Student Test Dates Search Clicked.")
             .Add("Institution Name ", lblIN.Text)
             .Add("Cohort Id ", HiddenCohortId.Value)
             .Add("Group Id ", GroupId.ToString())
             .Add("Program Id ", HiddenProgramId.Value)
             .Write();
        #endregion
        Presenter.ShowTests(HiddenProgramId.Value.ToInt());
    }
}