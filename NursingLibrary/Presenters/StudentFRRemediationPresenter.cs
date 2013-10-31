using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Common;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters.Controller;
using NursingLibrary.Utilities;

namespace NursingLibrary.Presenters
{
    public class StudentFRRemediationPresenter : StudentPresenter<IStudentFRRemediationView>
    {
        #region Constructors

        public StudentFRRemediationPresenter(IStudentAppController appController)
            : base(appController)
        {
        }

        #endregion Constructors

        #region public Methods

        public void ShowSystems()
        {
            View.PopulateSystems(AppController.GetCategories(View.CategoryIds));
        }

        public void PopulateTopics(string systems)
        {
            View.PopulateTopics(AppController.GetTopics(systems, false));
        }

        public IEnumerable<Topic> GetTopics(string systems)
        {
            return AppController.GetTopics(systems, false);
        }

        public virtual void OnlbCreateClick()
        {
            AppController.ShowPage(PageDirectory.FRRemediation, null, null);
        }

        public virtual void OnlbListReviewClick()
        {
            AppController.ShowPage(PageDirectory.FRQBankR, null, null);
        }

        public virtual void OnBtnCreateClick(string qNumber)
        {
            if (qNumber != null && !qNumber.Trim().Equals(string.Empty))
            {
                View.SetModeDetails();
                Student.QuizOrQBank = TestType.FRRemediation;
                Student.NumberOfQuestions = View.NumberOfRemediations;
                Student.IsCustomizedFRRemediation = true;
                ReviewRemediation ReviewRem = new ReviewRemediation();
                ReviewRem.ReviewRemName = View.SystemName;
                ReviewRem.UserId = Student.UserId;
                ReviewRem.CreateDate = DateTime.Now;
                ReviewRem.RemediatedTime = View.RemTime;
                ReviewRem.NoOfRemediations = View.NumberOfRemediations;

                AppController.CreateFRQBankRemediation(ReviewRem, View.SystemId, View.TopicId);
                ReviewRem = AppController.GetRemediationExplainationByReviewID(ReviewRem.ReviewRemId);

                if (ReviewRem.ReviewRemId > 0)
                {
                    Student.Action = Action.Remediation;
                    Student.QuizOrQBank = TestType.FRRemediation;
                    Student.ProductId = (int)ProductType.FocusedReviewTests;
                    Student.TestType = TestType.FocusedReview;
                    Student.IsCustomizedFRRemediation = true;
                    ReviewRemediation.ReviewRemId = ReviewRem.ReviewRemId;
                    ReviewRemediation.RemReviewQuestionId = ReviewRem.RemReviewQuestionId;
                    ReviewRemediation.ReviewExplanation = ReviewRem.ReviewExplanation;
                    ReviewRemediation.RemediationNumber = ReviewRem.RemediationNumber;
                    ReviewRemediation.ReviewRemName = ReviewRem.ReviewRemName;
                    ReviewRemediation.TopicTitle = ReviewRem.TopicTitle;
                    Student.ReviewRemediation = ReviewRem;
                    AppController.ShowPage(PageDirectory.FRIntroRemediation, null, null);
                }
            }
        }

        public int GetAvailableRemediations(string SystemID, string TopicID)
        {
            return AppController.GetAvailableRemediations(SystemID, TopicID);
        }

        public void OnBackClick()
        {
            if (Student.Action == Action.Remediation || Student.Action == Action.Review)
            {
                AppController.UpdateReviewRemediation(Student.ReviewRemediation.RemReviewQuestionId, View.Timer);
            }

            GetNextPrevRemediation(Student.ReviewRemediation.ReviewRemId, Convert.ToInt32(View.RemediationNumber), "P");
        }

        public virtual void OnNextClick(bool visible)
        {
            if (Student.Action == Action.Remediation || Student.Action == Action.Review)
            {
                AppController.UpdateReviewRemediation(Student.ReviewRemediation.RemReviewQuestionId, View.Timer);

               GetNextPrevRemediation(Student.ReviewRemediation.ReviewRemId, Convert.ToInt32(View.RemediationNumber), "N");
            }
        }

        public void OnQuitClick()
        {
            AppController.UpdateTotalRemediatedTime(Student.ReviewRemediation.ReviewRemId);
            Student.Action = Action.Review;
            AppController.ShowPage(PageDirectory.FRQBankR, null, null);
        }

        public override void OnViewInitialized()
        {
            base.OnViewInitialized();
            if ((Student.Action == Action.Remediation || Student.Action == Action.Review) && (Student.IsCustomizedFRRemediation == true))
            {
                View.TestName = Student.ReviewRemediation.ReviewRemName;
                View.SetRemediationCtrl(Student.ReviewRemediation.ReviewRemId, Action.Remediation.ToString());
                View.PopulateFields(Student.ReviewRemediation);
                View.PopulateRemediation(Student.ReviewRemediation);
                Student.IsCustomizedFRRemediation = false;
            }
        }

        public IEnumerable<Lippincott> GetLippinCott()
        {
            var lippinCotts = AppController.GetLippincottForReviewRemediation(Student.ReviewRemediation.RemReviewQuestionId);
            return lippinCotts;
        }

        public override void OnViewLoaded()
        {
            base.OnViewLoaded();
            if ((Student.Action == Action.Remediation || Student.Action == Action.Review) && (Student.IsCustomizedFRRemediation == true))
            {
                AppController.UpdateReviewRemediation(Student.ReviewRemediation.RemReviewQuestionId, View.Timer);
            }
        }

        public void ShowRemediations(int studentId, string sortMetaData)
        {
            var remediations = AppController.GetRemediationsForTheUser(studentId);
            View.BindRemediationList(remediations, SortHelper.Parse(sortMetaData));
        }

        public void DeleteRemediations(int remediationReviewId)
        {
            AppController.DeleteRemediations(remediationReviewId);
        }

        public void CreateRemediationReview(int reviewRemediationId, int noOfRemediations)
        {
            var reviewRemediation = AppController.GetRemediationExplainationByReviewID(reviewRemediationId);
            if (reviewRemediation.ReviewRemId > 0)
            {
                Student.Action = Action.Review;
                Student.QuizOrQBank = TestType.FRRemediation;
                Student.ProductId = (int)ProductType.FocusedReviewTests;
                Student.TestType = TestType.FocusedReview;
                Student.NumberOfQuestions = noOfRemediations;
                Student.IsCustomizedFRRemediation = true;
                ReviewRemediation.ReviewRemId = reviewRemediation.ReviewRemId;
                ReviewRemediation.ReviewRemName = reviewRemediation.ReviewRemName;
                ReviewRemediation.RemReviewQuestionId = reviewRemediation.RemReviewQuestionId;
                ReviewRemediation.ReviewExplanation = reviewRemediation.ReviewExplanation;
                ReviewRemediation.RemediatedTime = reviewRemediation.RemediatedTime;
                Student.ReviewRemediation = reviewRemediation;
                AppController.ShowPage(PageDirectory.FRIntroRemediation, null, null);
            }
        }

        public void OnIbIntroSClick()
        {
            AppController.UpdateTotalRemediatedTime(Student.ReviewRemediation.ReviewRemId);
            AppController.ShowPage(PageDirectory.FRQBankR, null, null);
        }

        private void GetNextPrevRemediation(int Id, int RemNumber, string action)
        {
            IEnumerable<ReviewRemediation> remediations = new List<ReviewRemediation>().ToArray();

            switch (action)
            {
                case "N":
                    remediations = AppController.GetNextPrevRemediation(Id, RemNumber, "N");
                    break;
                case "P":
                    remediations = AppController.GetNextPrevRemediation(Id, RemNumber, "P");
                    break;
            }

            if (remediations.Count() != 0)
            {
                var remediation = remediations.FirstOrDefault();
                Student.ReviewRemediation.RemReviewQuestionId = remediation.RemReviewQuestionId;
                if (remediation.RemediationNumber != Student.ReviewRemediation.NoOfRemediations)
                {
                    View.PopulateFields(remediation);
                    View.PopulateRemediation(remediation);
                }
                else
                {
                    View.PopulateEnd(remediation, true);
                    View.PopulateEndForAllPages();
                }
            }
            else
            {
                if (Student.Action == Action.Remediation)
                {
                    View.PopulateEndForAllPages();
                }
                else if (Student.Action == Action.Review)
                {
                    AppController.ShowPage(PageDirectory.FRQBankR, null, null);
                }
            }
        }

        #endregion
    }
}