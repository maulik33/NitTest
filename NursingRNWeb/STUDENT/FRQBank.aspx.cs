using System;
using System.Collections.Generic;
using System.Linq;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;

namespace NursingRNWeb.STUDENT
{
    public partial class FRQBank : StudentBasePage<IStudentFRQBankView, StudentFRQBankPresenter>, IStudentFRQBankView
    {
        #region Properties

        public string SystemIds { get; set; }

        public string TopicIds { get; set; }

        public string CategoryIds { get; set; }

        public int TutorMode
        {
            get
            {
                return rblTutorMode.SelectedValue.ToInt();
            }
        }

        public int ReuseMode
        {
            get
            {
                return rblMode.SelectedValue.ToInt();
            }
        }

        public int Correct { get; set; }

        public string SystemName { get; set; }

        #endregion

        public void PopulateSystems(IEnumerable<Systems> systems)
        {
            FRBank.populateSystems(systems);
        }

        public void PopulateTopics(IEnumerable<Topic> topics)
        {
            FRBank.PopulateTopics(topics);
        }

        public void DisplayAvailableQuestions(int count)
        {
            FRBank.DisplayAvailableCount(count);
        }

        protected override void OnInit(EventArgs e)
        {
           base.OnInit(e);
            Presenter.OnViewInitialized();
            CategoryIds = Student.ProgramofStudyId == (int)ProgramofStudyType.RN ? "1|2|3" : "4|5|6";
            Presenter.ShowSystems();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            FRBank.IsCFRTest = true;
            FRBank.SetHeader();
            if (false == KTPApp.IsProductionApp)
            {
                StagingWarning.Visible = true;
                FRBank.DisableTestCreation();
            }
            else
            {
                FRBank.OnCategoriesSelectionChange += new EventHandler<FRBankEventArgs>(FRBank_OnCategoriesSelectionChange);
                FRBank.OnCreateFRBankClick += new EventHandler<FRBankEventArgs>(FRBank_OnCreateFRBankClick);
                FRBank.OnTopicSelectionChange += new EventHandler<FRBankEventArgs>(FRBank_OnTopicSelectionChange);
                Presenter.OnViewLoaded();
            }
        }

        protected void FRBank_OnTopicSelectionChange(object sender, FRBankEventArgs e)
        {
            SetTopic(e);
            Presenter.ShowAvailableQuestions(e.Systems, TopicIds, ReuseMode);
            FRBank.SetTopicValues();
        }

        protected void FRBank_OnCreateFRBankClick(object sender, FRBankEventArgs e)
        {
            SystemIds = e.Systems;
            SetTopic(e);
            SystemName = e.SystemName;
            Presenter.OnBtnCreateClick(e.QuestionCount);
        }

        protected void FRBank_OnCategoriesSelectionChange(object sender, FRBankEventArgs e)
        {
            SetTopic(e);
            Presenter.PopulateTopics(e.Systems);
        }

        protected void rblMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            FRBankEventArgs frBankEvent = new FRBankEventArgs();
            frBankEvent.Systems = FRBank.FRCategories;
            SetTopic(frBankEvent);
            Presenter.ShowAvailableQuestions(FRBank.FRCategories, TopicIds, ReuseMode);
        }

        protected void lbReview_Click(object sender, EventArgs e)
        {
            Presenter.NavigateToReviewCustomQBank();
        }

        protected void lbSearchRemediation_Click(object sender, EventArgs e)
        {
            Presenter.NavigateSearchRemediation();
        }

        private void SetTopic(FRBankEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Topics))
            {
                IEnumerable<Topic> topics = Presenter.GetTopics(e.Systems);
                TopicIds = string.Join("|", topics.Select(t => t.RemediationId));
            }
            else
            {
                TopicIds = e.Topics;
            }
        }
    }
}