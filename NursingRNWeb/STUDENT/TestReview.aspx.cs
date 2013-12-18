using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

namespace STUDENT
{
    public partial class TestReview : StudentBasePage<IStudentTestReviewView, StudentTestReviewPresenter>, IStudentTestReviewView
    {
        public String ProductName
        {
            set { lblName.Text = value; }
        }

        public bool EnableProctorTrack { get; set; }

        public string ProctorTrackStartUrl { get; set; }

        public void SetResponseProperties()
        {
            Response.Buffer = true;
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Page.Session.Timeout = 100;
        }

        public void BindAllTestsGrid(IEnumerable<Test> tests)
        {
            GridViewAllTest.DataSource = tests.OrderBy(t => t.TestName);
            GridViewAllTest.DataBind();
        }

        public void BindAllCustomizedTestsGrid(IEnumerable<UserTest> customTests)
        {
            GridViewAvailableCustomTest.DataSource = customTests;
            GridViewAvailableCustomTest.DataBind();
        }

        public void BindSuspendedTestsGrid(IEnumerable<UserTest> tests)
        {
            if (Student.ProductId == (int)ProductType.FocusedReviewTests)
            {
                GridViewSuspendedTests.DataSource = tests.Where(t => !t.IsCustomizedFRTest);
                GridViewSuspendedTests.DataBind();
                GridViewSuspendedCustomTests.DataSource = tests.Where(t => t.IsCustomizedFRTest).OrderByDescending(t => t.TestStarted);
                GridViewSuspendedCustomTests.DataBind();
            }
            else
            {
                GridViewSuspendedTests.Visible = true;
                GridViewSuspendedTests.DataSource = tests;
                GridViewSuspendedTests.DataBind();
                SuspendedTests.Visible = true;
            }
        }

        public void BindTakenTestsGrid(IEnumerable<UserTest> tests)
        {
            if (Student.ProductId == (int)ProductType.FocusedReviewTests)
            {
                GridViewTakenTests.DataSource = tests.Where(t => !t.IsCustomizedFRTest);
                GridViewTakenTests.DataBind();
                GridViewTakenCustomTests.DataSource = tests.Where(t => t.IsCustomizedFRTest).OrderByDescending(t => t.TestStarted);
                GridViewTakenCustomTests.DataBind();
            }
            else
            {
                GridViewTakenTests.DataSource = tests;
                GridViewTakenTests.DataBind();
            }            
        }

        public void LoadSecuredTest()
        {
            if (Session["selectedTestId"] == null) return;
            var userTestId = (int)Session["selectedTestId"];

            if ( userTestId > 0)
            {
                Presenter.OnGridViewAllTestRowCommand(userTestId);
            }
        }


        #region GridViewSuspendedTest Methods

        protected void GridViewSuspendedTestsRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Resume")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                var row = GridViewSuspendedTests.Rows[index];
                Presenter.OnGridViewSuspendedTestsRowCommand((GridViewSuspendedTests.DataKeys[row.RowIndex].Values["UserTestId"] as int?) ?? 0, (GridViewSuspendedTests.DataKeys[row.RowIndex].Values["TestId"] as int?) ?? 0, GridViewSuspendedTests.DataKeys[row.RowIndex].Values["SuspendType"] as string, false);
            }
        }

        protected void GridViewSuspendedCustomRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Resume")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                var row = GridViewSuspendedCustomTests.Rows[index];
                Presenter.OnGridViewSuspendedTestsRowCommand((GridViewSuspendedCustomTests.DataKeys[row.RowIndex].Values["UserTestId"] as int?) ?? 0, (GridViewSuspendedCustomTests.DataKeys[row.RowIndex].Values["TestId"] as int?) ?? 0, GridViewSuspendedCustomTests.DataKeys[row.RowIndex].Values["SuspendType"] as string, true);
            }
        }

        #endregion

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Presenter.OnViewInitialized();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            //Load Test using Verificient Security
            if (Request.Params.Get("__EVENTARGUMENT") != string.Empty && Request.Params.Get("__EVENTARGUMENT") != null)
            {
                string[] parameters = Request.Params.Get("__EVENTARGUMENT").Split(':');

                if (parameters.Any() && string.Compare(parameters[0].ToLower(),"launchsecuredtest") == 0)
                {
                    LaunchVerificient(Convert.ToInt32(parameters[1]));
                }
            }
        
           else
            {
                if (!IsPostBack)
                {
                    #region Trace Information

                    TraceHelper.Create(TraceToken, "Navigated to Test Review Page")
                               .Add("Product ID", Student.ProductId.ToString())
                               .Write();

                    #endregion

                    //as of 10/23 they are now going to use get
                    if (!string.IsNullOrEmpty(Request.Form["originator"]) && !string.IsNullOrEmpty(Request.Form["testsession_id"]))
                       //use for post if (!string.IsNullOrEmpty(Request.Form["originator"]) && !string.IsNullOrEmpty(Request.Form["testsession_id"]))
                    {
                        string formValue = Request.Form["originator"];
                        //use for post string formValue = Request.Form["originator"];

                        //we also need to grab testsession_id
                        string verificientTestSessionId = Request.Form["testsession_id"];
                        //use for post string verificientTestSessionId = Request.Form["testsession_id"];

                        if (formValue == "verificient")
                        {
                            Session["verificientTestSessionId"] = verificientTestSessionId;
                            LoadSecuredTest();
                        }

                    }
                }
                InitializeSetting();
                Presenter.OnViewLoaded();
            }

       }

        #region GridViewTakenTest Methods

        protected void GridViewTakenTestsRowDataBound(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    if (e.Row.Cells[2].Text == @"0")
                    {
                        var id = GridViewTakenTests.DataKeys[e.Row.RowIndex].Values["SuspendType"].ToString();

                        if (id == "01" || id == "02")
                        {
                            e.Row.Cells[2].Text = @"Rejoin";
                        }

                        if (id == "03")
                        {
                            e.Row.Cells[2].Text = @"Resume";
                        }
                    }

                    if (e.Row.Cells[2].Text == @"1")
                    {
                        e.Row.Cells[2].Text = @"Review";
                    }

                    break;
            }
        }

        protected void GridViewTakenTestsRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var gv = (GridView)e.CommandSource;
            var rowIndex = Convert.ToInt32(e.CommandArgument.ToString());
            Presenter.OnGridViewTakenTestsRowCommand((gv.DataKeys[rowIndex].Values["UserTestId"] as int?) ?? 0, (gv.DataKeys[rowIndex].Values["TestId"] as int?) ?? 0, e.CommandName == "Review");
        }

        protected void GridViewTakenCustomTestsRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var gv = (GridView)e.CommandSource;
            var rowIndex = Convert.ToInt32(e.CommandArgument.ToString());
            Presenter.OnGridViewTakenTestsRowCommand((gv.DataKeys[rowIndex].Values["UserTestId"] as int?) ?? 0, (gv.DataKeys[rowIndex].Values["TestId"] as int?) ?? 0, e.CommandName == "Review");
        }
        #endregion

        protected void GridViewAllTestRowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "TakeTest":
                    {
                        var index = Convert.ToInt32(e.CommandArgument);
                        var row = GridViewAllTest.Rows[index];
                        Presenter.OnGridViewAllTestRowCommand((GridViewAllTest.DataKeys[row.RowIndex].Values["TestId"] as int?) ?? 0);
                    }

                    break;
            }
        }

        protected void GridViewCustomTestRowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "TakeCustomTest":
                    {
                        var index = Convert.ToInt32(e.CommandArgument);
                        var row = GridViewAvailableCustomTest.Rows[index];
                        Presenter.OnGridViewCustomTestRowCommand((GridViewAvailableCustomTest.DataKeys[row.RowIndex].Values["UserTestId"] as int?) ?? 0);
                    }

                    break;
            }
        }

        protected void GridViewAllTestRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton button = (LinkButton)e.Row.FindControl("GridViewAllTestLinkButton");
                if (button != null)
                    button.OnClientClick = EnableProctorTrack ? "return GridViewAllTestDialogBox(this, true);" : "return GridViewAllTestDialogBox(this, false);";
            }
        }

        protected void GridViewSuspendedTestsRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton button = (LinkButton)e.Row.FindControl("GridViewSuspendedTestLinkButton");
                if (button !=null && EnableProctorTrack)
                {
                    button.OnClientClick = "return GridViewSuspendedTestDialogBox(this, '" + ProctorTrackStartUrl + "');";
                }
                }
        }

        protected void lbCreateTest_Click(object sender, EventArgs e)
        {
            Presenter.OnCreateTestClick();
        }

        protected void lbCustomTest_Click(object sender, EventArgs e)
        {
            Presenter.OnCreateCustomTestClick();
        }

        private void InitializeSetting()
        {
            if (Student.ProductId == (int)ProductType.FocusedReviewTests)
            {
                divFocus.Style.Add(HtmlTextWriterStyle.Position, "Relative");
                divFocus.Style.Add(HtmlTextWriterStyle.Visibility, "Visible");
                AvailableStandardTests.Style.Add(HtmlTextWriterStyle.Visibility, "Visible");
                AvailableStandardTests.Style.Add(HtmlTextWriterStyle.Position, "Relative");
                AvailableCustomTests.Style.Add(HtmlTextWriterStyle.Position, "Relative");
                AvailableCustomTests.Style.Add(HtmlTextWriterStyle.Visibility, "Visible");
                SuspendedStandardTests.Style.Add(HtmlTextWriterStyle.Visibility, "Visible");
                SuspendedStandardTests.Style.Add(HtmlTextWriterStyle.Position, "Relative");
                SuspendedCustomizedTests.Style.Add(HtmlTextWriterStyle.Visibility, "Visible");
                SuspendedCustomizedTests.Style.Add(HtmlTextWriterStyle.Position, "Relative");
                TakenStandardTest.Style.Add(HtmlTextWriterStyle.Visibility, "Visible");
                TakenStandardTest.Style.Add(HtmlTextWriterStyle.Position, "Relative");
                TakenCustomTest.Style.Add(HtmlTextWriterStyle.Visibility, "Visible");
                TakenCustomTest.Style.Add(HtmlTextWriterStyle.Position, "Relative");
                GridViewTakenCustomTests.Visible = true;
                GridViewSuspendedCustomTests.Visible = true;
                GridViewAvailableCustomTest.Visible = true;
            }
        }

        private void LaunchVerificient(int selectedTestId)
        {
            HttpContext.Current.Session["selectedTestId"] = selectedTestId;
            HttpContext.Current.Session["user_id"] = Student.UserId;

            var ltiRequstForm = Presenter.GetLtiRequestFormForTestSecurityProvider(Student.UserId.ToString(), Student.InstitutionId.ToString(), selectedTestId);
            Page.Controls.Add(new LiteralControl(ltiRequstForm));
        }

    }
}
