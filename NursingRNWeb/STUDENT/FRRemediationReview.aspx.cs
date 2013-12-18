using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using NursingLibrary.Common;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

namespace STUDENT
{
    public partial class FRRemediationReview : StudentBasePage<IStudentFRRemediationView, StudentFRRemediationPresenter>, IStudentFRRemediationView
    {
        #region Unused Methods

        public string SystemId { get; set; }

        public string TopicId { get; set; }

        public string TestName { get; set; }

        public string CategoryIds { get; set; }

        public int NumberOfRemediations { get; set; }

        public int ReuseMode { get; set; }

        public string ReviewRemName { get; set; }

        public int RemTime { get; set; }

        public int Timer { get; set; }

        public string SystemName { get; set; }

        public string RemediationNumber { get; set; }

        public void PopulateRemediation(ReviewRemediation rem)
        {
        }

        public void PopulateFields(ReviewRemediation ReviewRemediation)
        {
        }

        public void PopulateEnd(ReviewRemediation remediation, bool lastRemediation)
        {
        }

        public void PopulateSystems(IEnumerable<Systems> systems)
        {
        }

        public void PopulateTopics(IEnumerable<Topic> topics)
        {
        }

        public void SetModeDetails()
        {
        }

        public void SetRemediationCtrl(int remID, string action)
        {
        }

        public void HideShowPreviousIncorrectButton(bool p)
        {
        }

        public void PopulateEndForAllPages()
        {
        }

        public void PopulateRemediation(ReviewRemediation rem, bool remNumber)
        {
        }

        public void ShowLippincott(IEnumerable<Lippincott> lippinCotts, string explaination)
        {
        }

        #endregion

        #region Public Methods
        
        public void BindRemediationList(IEnumerable<ReviewRemediation> remediations, SortInfo sortMetaData)
        {
            gvList.DataSource = KTPSort.Sort<ReviewRemediation>(remediations, sortMetaData);
            gvList.DataBind();
        }

        #endregion

        #region Protected Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            Presenter.OnViewInitialized();
            PopulateRemediations();
        }

       protected void gvList_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].ColumnSpan = 2;
                e.Row.Cells.RemoveAt(3);
            }
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbtnDelete = (LinkButton)e.Row.FindControl("lbDelete");
                lbtnDelete.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to delete this Remediation? " + "')");
            }
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Review")
            {
                var reviewParameters = e.CommandArgument.ToString().Split(',');
                Presenter.CreateRemediationReview(Convert.ToInt32(reviewParameters[0].ToString()), Convert.ToInt32(reviewParameters[1].ToString()));
            }
        }

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int remediationReviewId = Convert.ToInt32(gvList.DataKeys[e.RowIndex].Values[0].ToString());
            Presenter.DeleteRemediations(remediationReviewId);
            PopulateRemediations();
        }

        protected void lb_Create_Click(object sender, EventArgs e)
        {
            Presenter.OnlbCreateClick();
        }

        protected void lb_ListReview_Click(object sender, EventArgs e)
        {
            Presenter.OnlbListReviewClick();
        }

        protected void gvList_Sorting(object sender, GridViewSortEventArgs e)
        {
            hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);
            PopulateRemediations();
        }

        #endregion

        #region Private Methods

        private void PopulateRemediations()
        {
            Presenter.ShowRemediations(Student.UserId, hdnGridConfig.Value);
        }

        #endregion
    }
}