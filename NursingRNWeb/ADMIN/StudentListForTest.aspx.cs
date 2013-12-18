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

namespace NursingRNWeb.ADMIN
{
    public partial class StudentListForTest : PageBase<IUserListView, UserListPresenter>, IUserListView
    {
        #region variables
        private bool _showNotSelected = false;
        #endregion

        #region Properties

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
        #endregion

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

            ControlHelper.PopulateInstitutions(ddInstitution, institutes);
            ddInstitution.SelectedIndex = 0;
            InstitutionId = ddInstitution.SelectedValue;
            Presenter.GetCohortListForInstitute();

            if (!String.IsNullOrEmpty(ddCohort.SelectedValue))
            {
                CohortId = ddCohort.SelectedValue.ToInt();
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

        public void PopulateStudentForTest(IEnumerable<Student> students)
        {
            int _studentCount = students.Count();
            lblNumber.Text = "N=" + _studentCount;
            gridStudents.DataSource = students.ToArray();
            gridStudents.DataBind();

            lblM.Visible = _studentCount == 0 ? true : false;
        }

        public void RefreshPage(Student student, UserAction action, string title, string subTitle, bool canShowNotSelected, bool hasAddPermission, bool hasChangePermission)
        {
            _showNotSelected = canShowNotSelected;
        }

        #region notused

        public void ShowErrorMessage(string message)
        {
            throw new NotImplementedException();
        }

        public void PopulateGroupForTest(Group group)
        {
        }

        public void PopulateStudent(IEnumerable<Student> students, SortInfo sortMetaData)
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

        #endregion

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                gridStudents.Visible = true;
                CohortId = 0;
                GroupId = 0;
                SearchString = txtSearch.Text.Trim();
                Presenter.ShowStudentList();
            }

            lblError.Visible = false;
        }

        protected void searchButton_Click(object sender, ImageClickEventArgs e)
        {
            InstitutionId = ddInstitution.SelectedValue.ToInt().ToString();
            CohortId = ddCohort.SelectedValue.ToInt();
            GroupId = ddGroup.SelectedValue.ToInt();
            SearchStudents();
            lblError.Visible = false;
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

        protected void btnAssignTest_Click(object sender, EventArgs e)
        {
            var adhocGroup = new AdhocGroup();
            CohortId = ddCohort.SelectedValue.ToInt();
            List<int> checkedStudentIDs = new List<int>();
            RetrieveSelectedStudents(checkedStudentIDs);
            if (ddCohort.SelectedValue.ToInt() != -1 && ddInstitution.SelectedValue.ToInt() != -1)
            {
                Presenter.AssignStudentsToAdhocGroup(checkedStudentIDs, adhocGroup, CohortId);
            }
            else
            {
                lblError.Visible = true;
            }
        }

        protected void btnSaveADA_Click(object sender, EventArgs e)
        {
            List<int> checkedStudentIds = new List<int>();
            RetrieveSelectedStudents(checkedStudentIds);
            if (checkedStudentIds.Count > 0)
            {
                AdhocGroup adaAdhocGroup = new AdhocGroup();
                adaAdhocGroup.CreatedTime = DateTime.Now;
                adaAdhocGroup.IsAdaGroup = true;
                adaAdhocGroup.ADA = rbtADA.SelectedValue == "1" ? true : false;
                Presenter.SaveAdaAdhocGroup(checkedStudentIds, adaAdhocGroup);
                ClearControlValues();
            }
        }

        private void ClearControlValues()
        {
            foreach (GridViewRow row in gridStudents.Rows)
            {
                var chk = row.FindControl("chkSelected") as CheckBox;
                if (chk != null)
                {
                    chk.Checked = false;
                }

                rbtADA.SelectedValue = "0";
            }
        }

        private void RetrieveSelectedStudents(List<int> checkedStudentIds)
        {
            foreach (GridViewRow row in gridStudents.Rows)
            {
                var chk = row.FindControl("chkSelected") as CheckBox;
                if (chk.Checked)
                {
                    checkedStudentIds.Add(int.Parse(gridStudents.DataKeys[row.RowIndex].Value.ToString()));
                }
            }
        }

        private void SearchStudents()
        {
            SearchString = txtSearch.Text.Trim();
            InstitutionId = ddInstitution.SelectedValue;
            CohortId = ddCohort.SelectedValue.ToInt();
            GroupId = ddGroup.SelectedValue.ToInt();
            #region Trace Information
            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Student List Page")
                .Add("Institution Id", InstitutionId)
                .Add("Cohort Id", CohortId.ToString())
                .Add("Group Id", GroupId.ToString())
                .Add("Search String ", SearchString)
                .Write();
            #endregion
            if (InstitutionId.ToInt() > 0 || SearchString.Length > 0)
            {
                Presenter.GetStudentList();
            }
        }
    }
}