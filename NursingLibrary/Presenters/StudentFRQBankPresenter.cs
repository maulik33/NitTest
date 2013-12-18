using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters.Controller;

namespace NursingLibrary.Presenters
{
    public class StudentFRQBankPresenter : StudentPresenter<IStudentFRQBankView>
    {
        private const string Suspended = "01";

        public StudentFRQBankPresenter(IStudentAppController appController)
            : base(appController)
        {
        }

        public void ShowSystems()
        {
            View.PopulateSystems(AppController.GetCategories(View.CategoryIds));
        }

        public void PopulateTopics(string categoryIds)
        {
            View.PopulateTopics(AppController.GetTopics(categoryIds, true));
            ShowAvailableQuestions(categoryIds, View.TopicIds, View.ReuseMode);
        }

        public void OnBtnCreateClick(string qNumber)
        {
            if (qNumber != null && !qNumber.Trim().Equals(string.Empty))
            {
                Student.QuizOrQBank = TestType.Qbank;
                Student.ProductId = (int)ProductType.FocusedReviewTests;
                Student.NumberOfQuestions = qNumber.ToInt();
                Student.SuspendType = Suspended;

                var newTest = AppController.CreateFRQBankTest(View.TutorMode, View.ReuseMode, View.Correct, View.SystemIds, View.TopicIds, View.SystemName);

                if (newTest.UserTestId > 0)
                {
                    Student.TestId = newTest.TestId;
                    Student.Action = Action.QBankCreate;
                    Student.ProductId = (int)ProductType.FocusedReviewTests;
                    Student.TestType = TestType.FocusedReview;
                    Student.UserTestId = newTest.UserTestId;
                    AppController.ShowPage(PageDirectory.Resume, null, null);
                }
            }
        }

        public void ShowAvailableQuestions(string categoryIds, string topicIds, int reuseMode)
        {
            View.DisplayAvailableQuestions(AppController.GetCFRAvailableQuestions(Student.UserId, categoryIds, topicIds, true, reuseMode));
        }

        public IEnumerable<Topic> GetTopics(string categoryIds)
        {
            return AppController.GetTopics(categoryIds, true);
        }

        public void NavigateSearchRemediation()
        {
            AppController.ShowPage(PageDirectory.FRRemediation, null, null);
        }

        public void NavigateToReviewCustomQBank()
        {
            AppController.ShowPage(PageDirectory.TestReview, null, null);
        }
    }
}
