using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;

namespace STUDENT
{
    public partial class FRRemediation : StudentBasePage<IStudentFRRemediationView, StudentFRRemediationPresenter>, IStudentFRRemediationView
    {
        #region Properties

        public string SystemId { get; set; }

        public string TopicId { get; set; }

        public string TestName { get; set; }

        public string RemediationNumber { get; set; }

        public int NumberOfRemediations { get; set; }

        public string TimeRemaining { get; set; }

        public string ReviewRemName { get; set; }

        public int RemTime { get; set; }

        public int Timer { get; set; }

        public string SystemName { get; set; }

        public string CategoryIds { get; set; }

        #endregion

        #region Public Methods

        public void HideShowPreviousIncorrectButton(bool p)
        {
        }

        public void PopulateEndForAllPages()
        {
        }

        public void PopulateRemediation(ReviewRemediation rem)
        {
        }

        public void PopulateEnd(ReviewRemediation remediation, bool lastRemediation)
        {
        }

        public void ShowLippincott(IEnumerable<Lippincott> lippinCotts, string explaination)
        {
        }

        public void SetRemediationCtrl(int remID, string action)
        {
        }

        public void PopulateRemediations(IEnumerable<ReviewRemediation> getTestsForTheUser)
        {
        }

        public void BindRemediationList(IEnumerable<ReviewRemediation> getTestsForTheUser, SortInfo sortMetaData)
        {
        }

        public void PopulateFields(ReviewRemediation ReviewRemediation)
        {
        }

        public void PopulateSystems(IEnumerable<Systems> systems)
        {
            FRBank.populateSystems(systems);
        }

        public void PopulateTopics(IEnumerable<Topic> topics)
        {
            FRBank.PopulateTopics(topics);
        }

        public void SetModeDetails()
        {
            Student.ProductId = (int)ProductType.FocusedReviewTests;
        }

        #endregion

        #region Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Presenter.OnViewInitialized();
            CategoryIds = Student.ProgramofStudyId == (int)ProgramofStudyType.RN ? "1|2|3" : "4|5|6";
            Presenter.ShowSystems();
            FRBank.SetHeader();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            FRBank.OnCategoriesSelectionChange += new EventHandler<FRBankEventArgs>(FRBank_OnCategoriesSelectionChange);
            FRBank.OnCreateFRBankClick += new EventHandler<FRBankEventArgs>(FRBank_OnCreateFRBankClick);
            FRBank.OnTopicSelectionChange += new EventHandler<FRBankEventArgs>(FRBank_OnTopicSelectionChange);
            FRBank.IsCFRTest = false;
            Presenter.OnViewLoaded();
            SetModeDetails();
        }

        protected void FRBank_OnTopicSelectionChange(object sender, FRBankEventArgs e)
        {
            SystemId = e.Systems;
            SetTopic(e);
            FRBank.DisplayAvailableCount(Presenter.GetAvailableRemediations(SystemId, TopicId));
            FRBank.SetTopicValues();
        }

        protected void FRBank_OnCreateFRBankClick(object sender, FRBankEventArgs e)
        {
            SystemId = e.Systems;
            SetTopic(e);
            SystemName = e.SystemName;
            NumberOfRemediations = e.QuestionCount.ToInt();
            Presenter.OnBtnCreateClick(e.QuestionCount);
        }

        protected void FRBank_OnCategoriesSelectionChange(object sender, FRBankEventArgs e)
        {
            SystemId = e.Systems;
            SetTopic(e);
            Presenter.PopulateTopics(SystemId);
            FRBank.DisplayAvailableCount(Presenter.GetAvailableRemediations(SystemId, TopicId));
        }

        protected void lb_Create_Click(object sender, EventArgs e)
        {
            Presenter.OnlbCreateClick();
        }

        protected void lb_ListReview_Click(object sender, EventArgs e)
        {
            Presenter.OnlbListReviewClick();
        }

        private void SetTopic(FRBankEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Topics))
            {
                IEnumerable<Topic> topics = Presenter.GetTopics(e.Systems);
                TopicId = string.Join("|", topics.Select(t => t.RemediationId));
            }
            else
            {
                TopicId = e.Topics;
            }
        }
        #endregion
    }
}