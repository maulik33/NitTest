using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;


public partial class CMS_ReleaseReview : PageBase<IReleaseView, ReleasePresenter>, IReleaseView
{
    public string ShowContent { get; set; }

    public string showLippincot { get; set; }

    public string showTests { get; set; }

    public string QuestionsSort
    {
        get
        {
            return questionsSortConfig.Value;
        }

        set
        {
            questionsSortConfig.Value = value;
        }
    }

    public string RemediationsSort
    {
        get
        {
            return remediationsSortConfig.Value;
        }

        set
        {
            remediationsSortConfig.Value = value;
        }
    }

    public string LippincottsSort
    {
        get
        {
            return lippincottsSortConfig.Value;
        }

        set
        {
            lippincottsSortConfig.Value = value;
        }
    }

    public string TestsSort
    {
        get
        {
            return testsSortConfig.Value;
        }

        set
        {
            testsSortConfig.Value = value;
        }
    }

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.Edit);
    }

    #region IReleaseQuestionView

    public void RenderReviewDetails(IEnumerable<Question> questions, List<Remediation> remediations, IEnumerable<Lippincott> lippincotts, List<Test> tests)
    {        
        if (ShowContent.ToLower() == "y")
        {
            pnlQuestions.Visible = true;
            pnlRemediations.Visible = true;
            //to deal with a data issue, in this case only I need to trim spaces from the systemid.  See jira ticket nursing-3113 for more info
            foreach (Question q in questions)
            {
                if (q != null) q.SystemId = q.SystemId.Trim();
            }
            gridQuestions.DataSource = KTPSort.Sort(questions, SortHelper.Parse(QuestionsSort));
            gridQuestions.DataBind();
            gridRemediations.DataSource = KTPSort.Sort(remediations, SortHelper.Parse(RemediationsSort));
            gridRemediations.DataBind();
        }

        if (showLippincot.ToLower() == "y")
        {
            pnlLippincott.Visible = true;
            gridLippincott.DataSource = KTPSort.Sort(lippincotts, SortHelper.Parse(LippincottsSort));
            gridLippincott.DataBind();
        }

        if (showTests.ToLower() == "y")
        {
            pnlTests.Visible = true;
            gridTests.DataSource = KTPSort.Sort(tests, SortHelper.Parse(TestsSort));
            gridTests.DataBind();
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        Presenter.ValidateAccess(Global.IsProductionApp);
        Presenter.InitializeReviewPorperties();
        if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Release Content Management Page");
            #endregion
            DisplayDetailView();
        }
    }

    protected void btnRelease_Click(object sender, EventArgs e)
    {
        saveAllSelections();
        Presenter.NavigateToBackupDataPage();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveAllSelections();
    }

    protected void saveAllSelections()
    {
        StringBuilder approvedQuestions = new StringBuilder();
        StringBuilder disApprovedQuestions = new StringBuilder();
        StringBuilder approvedRemediations = new StringBuilder();
        StringBuilder disApprovedRemediations = new StringBuilder();
        StringBuilder approvedLippincots = new StringBuilder();
        StringBuilder disApprovedLippincots = new StringBuilder();
        StringBuilder approvedTests = new StringBuilder();
        StringBuilder disApprovedTests = new StringBuilder();
        if (ShowContent.ToLower() == "y")
        {
            UpdateIdsToRelease(approvedQuestions, disApprovedQuestions, gridQuestions, "Id");
            UpdateIdsToRelease(approvedRemediations, disApprovedRemediations, gridRemediations, "RemediationID");
            Presenter.UpdateReleaseStatus(approvedQuestions.ToString(), "A", "Questions");
            Presenter.UpdateReleaseStatus(disApprovedQuestions.ToString(), "E", "Questions");
            Presenter.UpdateReleaseStatus(approvedRemediations.ToString(), "A", "Remediation");
            Presenter.UpdateReleaseStatus(disApprovedRemediations.ToString(), "E", "Remediation");
        }

        if (showLippincot.ToLower() == "y")
        {
            UpdateIdsToRelease(approvedLippincots, disApprovedLippincots, gridLippincott, "LippincottID");
            Presenter.UpdateReleaseStatus(approvedLippincots.ToString(), "A", "Lippincot");
            Presenter.UpdateReleaseStatus(disApprovedLippincots.ToString(), "E", "Lippincot");
        }

        if (showTests.ToLower() == "y")
        {
            UpdateIdsToRelease(approvedTests, disApprovedTests, gridTests, "TestID");
            Presenter.UpdateReleaseStatus(approvedTests.ToString(), "A", "Tests");
            Presenter.UpdateReleaseStatus(disApprovedTests.ToString(), "E", "Tests");
        }
    }

    protected void gridQuestions_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gv = (GridView)sender;
            int rowIndex = Convert.ToInt32(e.Row.RowIndex);
            string status = string.Empty;

            if (!gv.DataKeys[rowIndex].Values["ReleaseStatus"].ToString().Equals(string.Empty))
            {
                status = gv.DataKeys[rowIndex].Values["ReleaseStatus"].ToString();
            }

            CheckBox ch = (CheckBox)e.Row.FindControl("chkSelect");
            if (ch != null)
            {
                if (status.Equals("A"))
                {
                    ch.Checked = true;
                }
                else
                {
                    ch.Checked = false;
                }
            }
        }
    }

    protected void gridTests_Sorting(object sender, GridViewSortEventArgs e)
    {
        TestsSort = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), TestsSort);
        DisplayDetailView();
    }

    protected void gridLippincotts_Sorting(object sender, GridViewSortEventArgs e)
    {
        LippincottsSort = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), LippincottsSort);
        DisplayDetailView();
    }

    protected void gridQuestions_Sorting(object sender, GridViewSortEventArgs e)
    {
        QuestionsSort = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), QuestionsSort);
        DisplayDetailView();
    }

    protected void gridRemediations_Sorting(object sender, GridViewSortEventArgs e)
    {
        RemediationsSort = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), RemediationsSort);
        DisplayDetailView();
    }

    private void UpdateIdsToRelease(StringBuilder approvedIds, StringBuilder disApprovedIds, GridView gridView, string idColumn)
    {
        int count = gridView.Rows.Count;
        for (int i = 0; i < count; i++)
        {
            GridViewRow row = gridView.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("chkSelect")).Checked;
            string QID = gridView.DataKeys[i].Values[idColumn].ToString();
            if (isChecked)
            {
                approvedIds.Append(QID + "|");
            }
            else
            {
                disApprovedIds.Append(QID + "|");
            }
        }
    }

    private void DisplayDetailView()
    {
        Presenter.DisplayReviewDetails();
    }
}
