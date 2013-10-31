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
    public partial class StudentListOfReviews : StudentBasePage<IStudentListOfReviewsView, StudentListOfReviewsPresenter>, IStudentListOfReviewsView
    {
        public string EndQuery
        {
            get { return Request.QueryString["E"]; }
        }

        public void SetTestResultinks()
        {
            if (ddProducts.SelectedValue == "4" || ddProducts.SelectedValue == "0")
            {
                if (Student.IsDignosticResultTest)
                {
                    TableRow r = CreateSvLinks(4, 6, "Analysis");
                    r.BackColor = System.Drawing.Color.White;
                    Table5.Rows.Add(r);
                }

                if (Student.IsReadinessResultTest)
                {
                    TableRow r = CreateSvLinks(4, 7, "Analysis");
                    r.BackColor = System.Drawing.ColorTranslator.FromHtml("#EEEEEE");
                    Table5.Rows.Add(r);
                }
            }
        }

        public void BindFinishedTest(IEnumerable<FinishedTest> finishedTests)
        {
            gvList.DataSource = finishedTests.OrderByDescending(key => key.TestStarted);
            gvList.DataBind();
        }

        public void PopulateProducts(IEnumerable<Product> products)
        {
            ddProducts.DataSource = products;
            ddProducts.DataTextField = "ProductName";
            ddProducts.DataValueField = "ProductId";
            ddProducts.SelectedValue = Student.ProductId.ToString();
            ddProducts.DataBind();
        }

        protected void DdProductsSelectedIndexChanged(object sender, EventArgs e)
        {
            Presenter.OnProductsSelectionChanged(ddProducts.SelectedValue);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Presenter.OnViewInitialized();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region Trace Information
                TraceHelper.WriteTraceEvent(TraceToken, "Navigated to List of Review Page");
                #endregion
            }

            Presenter.OnViewLoaded();
        }

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
                        if (Student.TestType == TestType.Integrated)
                        {
                            lk.Text = @"Review";
                            lk.CommandName = "GoToReview";
                            lk.Visible = true;
                            lk.CssClass = "s2";
                        }
                        else
                        {
                            lk.CommandName = "Resume";
                            lk.Text = @"Resume";
                            lk.Visible = true;
                            lk.CssClass = "s2";
                        }
                    }

                    var lk3 = (LinkButton)e.Row.FindControl("lb2");
                    if (lk3 != null)
                    {
                        if (Student.TestType == TestType.Integrated)
                        {
                            lk3.Visible = true;
                            lk3.Text = @"Analysis ";
                            lk3.CommandName = "GoToAnalysis";
                            lk3.CssClass = "s2";
                        }
                        else
                        {
                            lk3.CommandName = string.Empty;
                            lk3.Visible = false;
                        }
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

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!e.CommandName.Equals("Sort") && !e.CommandName.Equals("Page"))
            {
                var row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;

                var userTestId = (gvList.DataKeys[row.RowIndex].Values["UserTestId"] as int?) ?? 0;
                var testId = (gvList.DataKeys[row.RowIndex].Values["TestId"] as int?) ?? 0;
                var quizOrQBank = (gvList.DataKeys[row.RowIndex].Values["QuizOrQBank"] as TestType?) ?? TestType.Quiz;
                Presenter.OnGvListRowCommand(e.CommandName, userTestId, testId, quizOrQBank);
            }
        }

        protected void gvList_Sorting(object sender, GridViewSortEventArgs e)
        {
            IEnumerable<FinishedTest> finishedTests = gvList.DataSource as IEnumerable<FinishedTest> ?? Presenter.GetFinishedTests();
            hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);
            gvList.DataSource = KTPSort.Sort<NursingLibrary.Entity.FinishedTest>(finishedTests, SortHelper.Parse(hdnGridConfig.Value));
            gvList.DataBind();
        }
    }
}
