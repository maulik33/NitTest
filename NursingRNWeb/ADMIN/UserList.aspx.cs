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
using NursingRNWeb.AppCode.Report_Ds;

public partial class admin_UserList : PageBase<IUserListView, UserListPresenter>, IUserListView
{
    private bool _showNotSelected = false;
    private StudentInfo _studentInfo;
    private bool _forReport = false;
    private CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    public int GroupId { get; set; }

    public int CohortId { get; set; }

    public string InstitutionId { get; set; }

    public string SearchString { get; set; }

    public string StudentStartDate { get; set; }

    public string StudentEndDate { get; set; }

    public bool IsUnAssigned { get; set; }

    public int StudentId { get; set; }

    public bool IfUserExists { get; set; }

    public int ProgramOfStudyId { get; set; }

    #region IUserListView Members

    public void PopulateStudentTest(StudentTestDates testDates)
    {
        throw new NotImplementedException();
    }

    public void PopulateGroup(IEnumerable<Group> groups)
    {
        ControlHelper.PopulateGroups(ddGroup, groups);
        if (GroupId != 0)
        {
            ControlHelper.FindByValue(GroupId.ToString(), ddGroup);
        }
    }

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.List);
    }

    public void PopulateInstitution(IEnumerable<Institution> institutes)
    {
        if (_showNotSelected)
        {
            ddInstitution.ShowNotSelected = true;
        }
        else
        {
            ddInstitution.ShowNotSelected = false;
        }

        ControlHelper.PopulateInstitutions(ddInstitution, institutes, true);
        if (Presenter.ActionType == UserAction.Edit && InstitutionId.ToInt() > 0)
        {
            ControlHelper.FindByValue(InstitutionId, ddInstitution);
        }

        ddInstitution.SelectedIndex = 0;
        InstitutionId = ddInstitution.SelectedValue;
        Presenter.GetCohortListForInstitute();

        if (!String.IsNullOrEmpty(ddCohort.SelectedValue))
        {
            CohortId = ddCohort.SelectedValue.ToInt();
            Presenter.GetGroupListForInstitute();
        }

        SearchStudents();
    }

    public void PopulateCohort(IEnumerable<Cohort> cohorts)
    {
        ControlHelper.PopulateCohorts(ddCohort, cohorts);
        if (CohortId != 0)
        {
            ControlHelper.FindByValue(CohortId.ToString(), ddCohort);
        }
    }

    public void PopulateStudent(IEnumerable<Student> students, SortInfo sortMetaData)
    {
        int _studentCount = students.Count();
        lblNumber.Text = "N=" + _studentCount;
        gridUsers.DataSource = KTPSort.Sort<Student>(students, sortMetaData);
        gridUsers.DataBind();
        if (_forReport)
        {
            _studentInfo = new StudentInfo();
            foreach (var item in students)
            {
                StudentInfo.StudentsRow newRow = _studentInfo.Students.NewStudentsRow();
                newRow["StudentID"] = item.UserId;
                newRow["FirstName"] = item.FirstName;
                newRow["LastName"] = item.LastName;
                newRow["UserName"] = item.UserName;
                newRow["Email"] = item.Email;
                _studentInfo.Students.Rows.Add(newRow);
            }
        }
        lblM.Visible = _studentCount == 0;

        lbViewSecuritySummaryReport.Visible = false;
        gridUsers.Columns[9].Visible = false;
        if (students.Any() && students.FirstOrDefault().IsProctorTrackEnabled == 1)
        {
            gridUsers.Columns[9].Visible = true;
            lbViewSecuritySummaryReport.Visible = true;
        }
    }

    public void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudy)
    {
        ControlHelper.PopulateProgramOfStudy(ddlProgramOfStudy, programofStudy);
    }

    #region notused

    public void ShowErrorMessage(string message)
    {
        throw new NotImplementedException();
    }

    public void PopulateStudentForTest(IEnumerable<Student> students)
    {
    }

    public void PopulateGroupForTest(Group group)
    {
    }

    public void PopulateProgramForTest(Program program)
    {
        throw new NotImplementedException();
    }

    public void PopulateStudentTests(IEnumerable<StudentTestDates> testDates)
    {
        throw new NotImplementedException();
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

    public void RefreshPage(Student student, UserAction action, string title, string subTitle, bool canShowNotSelected, bool hasAddPermission, bool hasChangePermission)
    {
        _showNotSelected = canShowNotSelected;
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

    public void PopulateTests(IEnumerable<Test> tests)
    {
    }

    public void PopulateAdhocGroupForTest(IEnumerable<AdhocGroupTestDetails> adhocGrouptestdetails)
    {
    }

    #endregion

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            gridUsers.Visible = true;
            CohortId = 0;
            GroupId = 0;
            SearchString = txtSearch.Text.Trim();
            if (Presenter.CurrentContext.UserType == UserType.SuperAdmin)
            {
                lblProgOfStudyTxt.Visible = true;
                ddlProgramOfStudy.Visible = true;
            }
            Presenter.ShowStudentList();
        }
    }

    protected void gridUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!e.CommandName.Equals("Sort") && !e.CommandName.Equals("Page"))
        {
            int index = e.CommandArgument.ToInt();
            GridViewRow row = gridUsers.Rows[index];
            string id = Server.HtmlDecode(row.Cells[0].Text);
            string institutionId = ddInstitution.SelectedValue;

            switch (e.CommandName)
            {
                case "Select":
                    Presenter.NavigateToEdit(id, UserAction.Edit);
                    break;
                case "Tests":
                    Presenter.NavigateToAssignTests(id);
                    break;
                case "ViewSecurityReport":
                    var ltiRequstForm = Presenter.GetLtiRequestFormForTestSecurityProvider(id,institutionId);
                    Page.Controls.Add(new LiteralControl(ltiRequstForm));
                    break;
            }
        }
    }

    protected void gridUsers_PageIndexChanged(Object sender, EventArgs e)
    {
        gridUsers.Visible = true;
    }

    protected void gridUsers_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);
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

        Presenter.GetGroupListForInstitute();
        SearchStudents();
    }

    protected void ddCohort_SelectedIndexChanged(object sender, EventArgs e)
    {
        InstitutionId = ddInstitution.SelectedValue;
        CohortId = ddCohort.SelectedValue.ToInt();
        Presenter.GetGroupListForInstitute();
        SearchStudents();
    }

    protected void ddGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        InstitutionId = ddInstitution.SelectedValue;
        CohortId = ddCohort.SelectedValue.ToInt();
        GroupId = ddGroup.SelectedValue.ToInt();
        Presenter.GetGroupListForInstitute();
        SearchStudents();
    }

    protected void searchButton_Click(object sender, ImageClickEventArgs e)
    {
        InstitutionId = ddInstitution.SelectedValue.ToInt().ToString();
        CohortId = ddCohort.SelectedValue.ToInt();
        GroupId = ddGroup.SelectedValue.ToInt();
        SearchStudents();
    }

    protected void gridUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridUsers.PageIndex = e.NewPageIndex;
        SearchStudents();
    }

    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        _forReport = true;
        InstitutionId = ddInstitution.SelectedValue.ToInt().ToString();
        CohortId = ddCohort.SelectedValue.ToInt();
        GroupId = ddGroup.SelectedValue.ToInt();
        SearchStudents();
        rpt.Load(Server.MapPath("Report/StudentInfo.rpt"));
        rpt.SetDataSource(_studentInfo);
        rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Page.Response, true, "StudentInformation");
        _forReport = false;
    }

    protected override void OnUnload(EventArgs e)
    {
        base.OnUnload(e);
        rpt.Close();
        rpt.Dispose();
    }

    protected void btnCreateAdhocGroup_Click(object sender, EventArgs e)
    {
        Presenter.NavigateStudentToAdhocGroup();
    }  

    protected void ddlProgramOfStudy_SelectedIndexChanged(object sender, EventArgs e)
    {
        _showNotSelected = true;
        Presenter.GetInstitutionByProgramofStudy(ddlProgramOfStudy.SelectedValue.ToInt());
    }

    protected void lbViewSecuritySummaryReport_Click(object sender, EventArgs e)
    {
        string institutionId = ddInstitution.SelectedValue;
        var ltiRequstForm = Presenter.GetLtiRequestFormForTestSecurityProvider("0", institutionId);
        Page.Controls.Add(new LiteralControl(ltiRequstForm));
    }
    private void SearchStudents()
    {
        SearchString = txtSearch.Text.Trim();
        ProgramOfStudyId = ddlProgramOfStudy.SelectedValue.ToInt();
        InstitutionId = ddInstitution.SelectedValue;
        CohortId = ddCohort.SelectedValue.ToInt();
        GroupId = ddGroup.SelectedValue.ToInt();
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Group List Page")
            .Add("Institution Id", InstitutionId)
            .Add("Cohort Id", CohortId.ToString())
            .Add("Group Id", GroupId.ToString())
            .Add("Search String ", SearchString)
            .Write();
        #endregion
        if (InstitutionId.ToInt() > 0 || SearchString.Length > 0)
        {
            Presenter.GetStudentList(hdnGridConfig.Value);
        }
        else
        {
            gridUsers.DataSource = null;
            gridUsers.DataBind();
            lblNumber.Text = "N=" + 0;
        }
    }
}
