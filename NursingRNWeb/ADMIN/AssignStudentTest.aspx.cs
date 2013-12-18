using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

namespace NursingRNWeb.ADMIN
{
    public partial class AssignStudentTest : PageBase<IUserListView, UserListPresenter>, IUserListView
    {
        private const string MANDATORY_DATE_MESSAGE = "Start Dates and End Dates are Mandatory";
        private int cohortId = 0;

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

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        #endregion
        #region IUserListView Members

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

        public void PopulateStudentForTest(IEnumerable<Student> students)
        {
        }

        public void RefreshPage(Student student, UserAction action, string title, string subTitle, bool canShowNotSelected, bool hasAddPermission, bool hasChangePermission)
        {
        }

        #region notused
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

        public void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudy)
        {
            throw new NotImplementedException();
        }

        public void PopulateGroupForTest(Group group)
        {
            throw new NotImplementedException();
        }

        public void PopulateAdhocGroupForTest(IEnumerable<AdhocGroupTestDetails> adhocGrouptestdetails)
        {
            if (adhocGrouptestdetails.Count() > 0)
            {
                gvAdhocGroupTest.DataSource = adhocGrouptestdetails.ToArray();
                gvAdhocGroupTest.DataBind();
            }
            else
            {
                List<AdhocGroupTestDetails> objAdhocGroupTestDetails = new List<AdhocGroupTestDetails>();
                objAdhocGroupTestDetails.Add(new AdhocGroupTestDetails());
                gvAdhocGroupTest.DataSource = objAdhocGroupTestDetails;
                gvAdhocGroupTest.DataBind();

                int TotalColumns = gvAdhocGroupTest.Rows[0].Cells.Count;
                gvAdhocGroupTest.Rows[0].Cells.Clear();
                gvAdhocGroupTest.Rows[0].Cells.Add(new TableCell());
                gvAdhocGroupTest.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                gvAdhocGroupTest.Rows[0].Cells[0].Text = "No Record Found";
            }
        }

       
        #endregion

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            cohortId = Request.QueryString["cohortId"].ToInt();
            if (!Page.IsPostBack)
            {
                #region Trace Information
                TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to AssignStudentTest Page");
                #endregion
                Presenter.PopulateAdhocAssignTest();
            }

            lblError.Visible = false;
        }

        protected void gvAdhocGroupTest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                HtmlInputText txtStartDate = (HtmlInputText)e.Row.FindControl("tbSD");
                HtmlAnchor LnkCalendar = (HtmlAnchor)e.Row.FindControl("LnkNewCalendar");
                LnkCalendar.Attributes.Add("href", "Javascript:pickDate('" + txtStartDate.Name + "')");

                HtmlInputText txtEndDate = (HtmlInputText)e.Row.FindControl("tbED");
                HtmlAnchor LnkCalendar2 = (HtmlAnchor)e.Row.FindControl("LnkNewCalendar2");
                LnkCalendar2.Attributes.Add("href", "Javascript:pickDate('" + txtEndDate.Name + "')");

                DropDownList dropDownList = (DropDownList)e.Row.FindControl("ddlNewTest");                
                IEnumerable<Test> tests = Presenter.GetTests(cohortId);
                ////ControlHelper.PopulateTests(dropDownList, tests);

                var assignedTests = from t in tests
                                    select new KeyValuePair<string, string>(MakeIDsUnique(t.TestId, t.Type), t.TestName);
                dropDownList.DataSource = assignedTests;
                dropDownList.DataTextField = "Value";
                dropDownList.DataValueField = "Key";
                dropDownList.DataBind();
            }
        }

        protected void gvAdhocGroupTest_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("AddNew"))
            {
                lblM.Visible = false;
                var adhocGroupTestDetails = new AdhocGroupTestDetails();
                HtmlInputText tbSD = (HtmlInputText)gvAdhocGroupTest.FooterRow.FindControl("tbSD");
                HtmlInputText tbED = (HtmlInputText)gvAdhocGroupTest.FooterRow.FindControl("tbED");

                DropDownList ddTime_S = (DropDownList)gvAdhocGroupTest.FooterRow.FindControl("ddlNewTime_S");
                DropDownList ddAMPM_S = (DropDownList)gvAdhocGroupTest.FooterRow.FindControl("ddNewAMPM_S");

                DropDownList ddTime_E = (DropDownList)gvAdhocGroupTest.FooterRow.FindControl("ddlNewTime_E");
                DropDownList ddAMPM_E = (DropDownList)gvAdhocGroupTest.FooterRow.FindControl("ddNewAMPM_E");
                DropDownList ddlTest = (DropDownList)gvAdhocGroupTest.FooterRow.FindControl("ddlNewTest");

                if (ValidDate(tbSD.Value, tbED.Value))
                {
                    if (tbSD.Value.Trim().Length != 0)
                    {
                        adhocGroupTestDetails.StartDate = StartDate.ToString().ToDateTime().ToShortDateString() + " " + ddTime_S.SelectedValue.ToInt() + ":00:00" + ddAMPM_S.SelectedItem.Text;
                    }

                    if (tbED.Value.Trim().Length != 0)
                    {
                        adhocGroupTestDetails.EndDate = EndDate.ToString().ToDateTime().ToShortDateString() + " " + ddTime_E.SelectedValue.ToInt() + ":00:00" + ddAMPM_E.SelectedItem.Text;
                    }

                    adhocGroupTestDetails.CreatedDate = DateTime.Now;
                    if (ddlTest.SelectedIndex != 0)
                     {
                         var selectedTest = ddlTest.SelectedValue;
                         if (selectedTest.Contains("P."))
                         {
                             adhocGroupTestDetails.Type = 1;
                             adhocGroupTestDetails.TestId = selectedTest.Replace("P.", string.Empty).ToInt();
                         }
                         else
                         {
                             adhocGroupTestDetails.Type = 0;
                             adhocGroupTestDetails.TestId = selectedTest.Replace("T.", string.Empty).ToInt();
                         }

                        lblError.Text = Presenter.SaveAdhocGroupTest(adhocGroupTestDetails, cohortId);
                        lblError.Visible = true;
                    }
                    else
                    {
                        lblM.Visible = true;
                    }                   
                    
                    Presenter.PopulateAdhocAssignTest();
                }
                else
                {
                    lblError.Text = MANDATORY_DATE_MESSAGE;
                    lblError.Visible = true;
                }
            }
        }

        private bool ValidDate(string startDate, string endDate)
        {
            bool success = false;
            DateTime _tempStartDate;
            DateTime _tempEndDate;
            if (startDate.Trim().Length > 0)
            {
                success = DateTime.TryParse(startDate.Trim(), out _tempStartDate);
                if (success)
                {
                    StartDate = _tempStartDate;
                }
            }

            if (success)
            {
                if (endDate.Trim().Length > 0)
                {
                    success = DateTime.TryParse(endDate.Trim(), out _tempEndDate);
                    if (success)
                    {
                        EndDate = _tempEndDate;
                    }
                }
                else if (endDate.Trim().Length == 0)
                {
                    success = true;
                }
            }

            return success;
        }

        private string MakeIDsUnique(int test, string type)
        {
            if (type.Equals("0"))
            {
                return string.Format("T.{0}", test);
            }
            else
            {
                return string.Format("P.{0}", test);
            }
        }
    }
}