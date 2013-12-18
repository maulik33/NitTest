using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

namespace STUDENT
{
    public partial class QBank_R : StudentBasePage<IStudentQBankRView, StudentQbankRPresenter>, IStudentQBankRView
    {
        #region Properties

        public string EndQuery
        {
            get { return Request.QueryString["E"]; }
        }

        public string QBankProgramofStudyName
        {
            get { return Enum.GetName(typeof(ProgramofStudyType), Student.QBankProgramofStudyId); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// This method is used to bind Finished Tests for Qbank_R
        /// </summary>
        /// <param name="finishedTests"></param>
        public void BindViewSample(IEnumerable<FinishedTest> finishedTests)
        {
            gv_Sample.DataSource = finishedTests.OrderByDescending(key => key.TestStarted);
            gv_Sample.DataBind();
        }

        /// <summary>
        /// This methos is used to bind Finished tests for Qbank_R List
        /// </summary>
        /// <param name="finishedTests"></param>
        public void BindViewList(IEnumerable<FinishedTest> finishedTests)
        {
            gvList.DataSource = finishedTests.OrderByDescending(key => key.TestStarted);
            gvList.DataBind();
        }

        #endregion

        #region PageLoad

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region Trace Information
                TraceHelper.WriteTraceEvent(TraceToken, "Navigated to NCLEX-RN Prep > Qbank > Review Results");
                #endregion
            }

            Presenter.OnViewInitialized();
            Presenter.OnViewLoaded();
        }

        #endregion

        #region Events

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[4].Text == "0")
                {
                    e.Row.Cells[4].Text = @"In Progress";

                    var lk = (LinkButton)e.Row.FindControl("lb1");
                    if (lk != null)
                    {
                        lk.CommandName = "Resume";
                        lk.Text = @"Resume";
                        lk.Visible = true;
                        lk.CssClass = "s2";
                    }

                    var lk3 = (LinkButton)e.Row.FindControl("lb2");
                    if (lk3 != null)
                    {
                        lk3.CommandName = string.Empty;
                        lk3.Visible = false;
                    }
                }
                else
                {
                    e.Row.Cells[4].Text = @"Completed";

                    var lk1 = (LinkButton)e.Row.FindControl("lb1");
                    if (lk1 != null)
                    {
                        lk1.Text = @"Review";
                        lk1.CommandName = "GoToReview";
                        lk1.Visible = true;
                        lk1.CssClass = "s2";
                    }

                    var lk2 = (LinkButton)e.Row.FindControl("lb2");
                    if (lk2 != null)
                    {
                        lk2.Visible = true;
                        lk2.Text = @"Analysis ";
                        lk2.CommandName = "GoToAnalysis";
                        lk2.CssClass = "s2";
                    }
                }
            }
        }

        protected void gv_Sample_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[4].Text == "0")
                {
                    e.Row.Cells[4].Text = @"In Progress";

                    var lk = (LinkButton)e.Row.FindControl("lb1");
                    if (lk != null)
                    {
                        lk.CommandName = "Resume";
                        lk.Text = @"Resume";
                        lk.Visible = true;
                        lk.CssClass = "s2";
                    }

                    var lk3 = (LinkButton)e.Row.FindControl("lb2");
                    if (lk3 != null)
                    {
                        lk3.CommandName = string.Empty;
                        lk3.Visible = false;
                    }
                }
                else
                {
                    e.Row.Cells[4].Text = @"Completed";

                    var lk1 = (LinkButton)e.Row.FindControl("lb1");
                    if (lk1 != null)
                    {
                        lk1.Text = @"Review";
                        lk1.CommandName = "GoToReview";
                        lk1.Visible = true;
                        lk1.CssClass = "s2";
                    }

                    var lk2 = (LinkButton)e.Row.FindControl("lb2");
                    if (lk2 != null)
                    {
                        lk2.Visible = true;
                        lk2.Text = @"Analysis ";
                        lk2.CommandName = "GoToAnalysis";
                        lk2.CssClass = "s2";
                    }
                }
            }
        }

        protected void gv_Sample_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!e.CommandName.Equals("Sort") && !e.CommandName.Equals("Page"))
            {
                var row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;

                var userTestId = (gv_Sample.DataKeys[row.RowIndex].Values["UserTestId"] as int?) ?? 0;
                var testId = (gv_Sample.DataKeys[row.RowIndex].Values["TestId"] as int?) ?? 0;
                var quizOrQBank = (gv_Sample.DataKeys[row.RowIndex].Values["QuizOrQBank"] as TestType?) ?? TestType.Quiz;
                Presenter.OnGridListSampleRowCommand(e.CommandName, userTestId, testId, quizOrQBank);
            }
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!e.CommandName.Equals("Sort") && !e.CommandName.Equals("Page"))
            {
                var row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                var userTestId = (gvList.DataKeys[row.RowIndex].Values["UserTestId"] as int?) ?? 0;
                var testId = (gvList.DataKeys[row.RowIndex].Values["TestId"] as int?) ?? 0;
                var quizOrQBank = (gvList.DataKeys[row.RowIndex].Values["QuizOrQBank"] as TestType?) ?? TestType.Quiz;
                Presenter.OnGridListSampleRowCommand(e.CommandName, userTestId, testId, quizOrQBank);
            }
        }

        protected void gv_Sample_Sorting(object sender, GridViewSortEventArgs e)
        {
            IEnumerable<FinishedTest> finishedTests = gv_Sample.DataSource as IEnumerable<FinishedTest> ?? Presenter.GetTestsNCLEXInfoForTheUser(2);

            switch (e.SortExpression)
            {
                case "TestName":
                    {
                        gv_Sample.DataSource = e.SortDirection == SortDirection.Ascending
                                                   ? finishedTests.OrderBy(key => key.TestName)
                                                   : finishedTests.OrderByDescending(key => key.TestName);
                    }

                    break;
                case "TestStarted":
                    {
                        gv_Sample.DataSource = e.SortDirection == SortDirection.Ascending
                                                   ? finishedTests.OrderBy(key => key.TestStarted)
                                                   : finishedTests.OrderByDescending(key => key.TestStarted);
                    }

                    break;
                case "TestStatus":
                    {
                        gv_Sample.DataSource = e.SortDirection == SortDirection.Ascending
                                                   ? finishedTests.OrderBy(key => key.TestStatus)
                                                   : finishedTests.OrderByDescending(key => key.TestStatus);
                    }

                    break;
            }

            gv_Sample.DataBind();
        }

        protected void gvList_Sorting(object sender, GridViewSortEventArgs e)
        {
            IEnumerable<FinishedTest> finishedTests = gvList.DataSource as IEnumerable<FinishedTest> ?? Presenter.GetTestsNCLEXInfoForTheUser(3);

            switch (e.SortExpression)
            {
                case "TestName":
                    {
                        gvList.DataSource = e.SortDirection == SortDirection.Ascending
                                                ? finishedTests.OrderBy(key => key.TestName)
                                                : finishedTests.OrderByDescending(key => key.TestName);
                    }

                    break;
                case "TestStarted":
                    {
                        gvList.DataSource = e.SortDirection == SortDirection.Ascending
                                                ? finishedTests.OrderBy(key => key.TestStarted)
                                                : finishedTests.OrderByDescending(key => key.TestStarted);
                    }

                    break;
                case "TestStatus":
                    {
                        gvList.DataSource = e.SortDirection == SortDirection.Ascending
                                                ? finishedTests.OrderBy(key => key.TestStatus)
                                                : finishedTests.OrderByDescending(key => key.TestStatus);
                    }

                    break;
            }

            gvList.DataBind();
        }

        protected void lb_ListReview_Click(object sender, EventArgs e)
        {
            Presenter.OnListReview();
        }

        protected void lb_Create_Click(object sender, EventArgs e)
        {
            Presenter.OnCreate();
        }

        protected void lb_Analysis_Click(object sender, EventArgs e)
        {
            Presenter.OnAnalysis();
        }

        protected void lb_Sample_Click(object sender, EventArgs e)
        {
            Presenter.OnSampleClick(TestType.Quiz, 2, 4);
        }

        #endregion
    }
}
