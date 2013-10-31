using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters.Controller;

namespace NursingLibrary.Presenters
{
    public class SkillModulePopUpPresenter : StudentPresenter<ISkillModulePopUpView>
    {
        private const string Suspended = "01";

        #region Constructors

        public SkillModulePopUpPresenter(IStudentAppController appController)
            : base(appController)
        {
        }

        #endregion Constructors

        public override void OnViewInitialized()
        {
            base.OnViewInitialized();
        }

        public void OnNextButtonClick()
        {
            DisplayPreviousNextVideos(ViewDirection.Next);
        }

        public void OnBackButtonClick()
        {
            DisplayPreviousNextVideos(ViewDirection.Back);
        }

        public void UpdateSkillModuleStatus(int SMUserVideoId)
        {
            AppController.UpdateSkillModuleStatus(SMUserVideoId);
        }

        public void UpdateSkillModuleVideoStatus(int SMUserVideoId)
        {
            AppController.UpdateSkillModuleVideoStatus(SMUserVideoId);
        }

        public void ViewCurrentVideo(int SMUserId)
        {
            if (Student.SMUserId > 0)
            {
                DisplayPreviousNextVideos(ViewDirection.Current);
            }
        }

        private void DisplayPreviousNextVideos(ViewDirection viewDirection)
        {
            int videoOrderNoToDisplay = 0;
            bool navigateToTest = false, navigateToLandingPage = false;
            SMUserVideoTransaction VideoTransDetails;
            IEnumerable<SMUserVideoTransaction> SMUserVideoTransactions = AppController.GetSkillsModuleVideos(Student.SMUserId).OrderBy(r => r.SMOrder);
            IEnumerable<SMTest> sMTests;
            sMTests = AppController.GetSkillsModuleTests(Student.UserId, View.TestId);
            if (SMUserVideoTransactions.Count() > 0)
            {
                if (viewDirection == ViewDirection.Next)
                {
                    sMTests = AppController.GetSkillsModuleTests(Student.UserId, View.TestId);
                    SMUserVideoTransaction videoViewed = SMUserVideoTransactions.Where(r => r.SMOrder == View.OrderNumber).SingleOrDefault();
                    var videoId = SMUserVideoTransactions.Where(r => r.SMOrder == View.OrderNumber).Select(p => p.SMUserVideoId).FirstOrDefault();
                    UpdateSkillModuleStatus(videoId);
                    ////if (sMTests.Count() > 0)
                    ////{
                    ////    UpdateSkillModuleStatus(videoId);
                    ////}
                    ////else
                    ////{
                    ////    if (videoViewed.SkillsModuleVideo.Type == (int)SMType.Text)
                    ////        {
                    ////            UpdateSkillModuleStatus(videoId);
                    ////        }
                    ////}
                }

                SetNavigationProps(SMUserVideoTransactions, viewDirection, sMTests.Count(), ref navigateToTest, ref navigateToLandingPage);
                if (navigateToTest && View.IsProductionApplication)
                {
                    // User can take quiz only in the production environment.
                    TakeSMQuiz(Student.UserId, View.TestId);
                }
                else if (navigateToTest && !View.IsProductionApplication)
                {
                    View.ShowSMPage(0);
                }

                if (navigateToLandingPage && View.FromIntroReview == false)
                {
                    View.ShowSMPage(0);
                }
                else if (navigateToLandingPage && View.FromIntroReview == true)
                {
                    View.ShowSMPage(2);
                }

                videoOrderNoToDisplay = GetVideoOrderNumber(viewDirection, sMTests.Count(), SMUserVideoTransactions);
                VideoTransDetails = SMUserVideoTransactions.Where(r => r.SMOrder == videoOrderNoToDisplay).SingleOrDefault();
                if (VideoTransDetails != null)
                {
                    View.DisplayVideo(VideoTransDetails);
                    if (VideoTransDetails.SMOrder == 1)
                    {
                        View.EnableBackButton = false;
                    }
                    else
                    {
                        View.EnableBackButton = true;
                    }

                    if (VideoTransDetails.IsVideoFullyViewed == true || VideoTransDetails.SkillsModuleVideo.Type == (int)SMType.Text || VideoTransDetails.SMCount > 0)
                    {
                        View.EnableNextButton = true;
                    }
                    else
                    {
                        View.EnableNextButton = false;
                    }
                }
            }
        }

        private void SetNavigationProps(IEnumerable<SMUserVideoTransaction> SMUserVideoTransactions, ViewDirection viewDirection, int smTestCount, ref bool navigateToTest, ref bool navigateToLandingPage)
        {
            if (viewDirection == ViewDirection.Next && View.OrderNumber == SMUserVideoTransactions.Count())
            {
                // User is at last test video on click on next navigate to test page if user has not taken test atleast once otherwise navigate to landing page.
                if (smTestCount == 0)
                {
                    navigateToTest = true;
                }
                else
                {
                    navigateToLandingPage = true;
                }
            }
            else if (viewDirection == ViewDirection.Current)
            {
                // If user has watched all the videos and has not taken test atleast once than navigate him to test.
                if (smTestCount == 0 && SMUserVideoTransactions.Where(r => r.IsPageFullyViewed).Count() == SMUserVideoTransactions.Count())
                {
                    navigateToTest = true;
                }
            }
        }

        private void TakeSMQuiz(int userId, int skillmoduleId)
        {
            Student.QuizOrQBank = TestType.Qbank;
            Student.ProductId = (int)ProductType.SkillsModules;
            Student.SuspendType = Suspended;

            var newTest = AppController.CreateSkillsModuleTest(View.TestId);

            if (newTest.UserTestId > 0)
            {
                Student.TestId = newTest.TestId;
                Student.Action = Action.QBankCreate;
                Student.TestType = TestType.SkillsModules;
                Student.TestType = TestType.SkillsModules;
                Student.UserTestId = newTest.UserTestId;
                View.ShowSMPage(1);
                //// AppController.ShowPage(PageDirectory.Resume, null, null);
            }
        }

        private int GetVideoOrderNumber(ViewDirection viewDirection, int smTestCount, IEnumerable<SMUserVideoTransaction> SMUserVideoTransactions)
        {
            int videoOrderNoToDisplay = 0;
            if (viewDirection == ViewDirection.Next)
            {
                videoOrderNoToDisplay = View.OrderNumber + 1;
            }
            else if (viewDirection == ViewDirection.Back)
            {
                videoOrderNoToDisplay = View.OrderNumber - 1;
            }
            else if (viewDirection == ViewDirection.Current)
            {
                SMUserVideoTransaction VideoToDisplay;
                if (SMUserVideoTransactions.Where(r => r.IsPageFullyViewed == false).Count() > 0)
                {
                    if (smTestCount == 0)
                    {
                        // When unviewed video present than show unviewd video
                        IEnumerable<SMUserVideoTransaction> tempList = (IEnumerable<SMUserVideoTransaction>)SMUserVideoTransactions.Where(r => r.IsPageFullyViewed == false).OrderBy(q => q.SMOrder);
                        VideoToDisplay = tempList.ToList().FirstOrDefault();
                    }
                    else
                    {
                        VideoToDisplay = SMUserVideoTransactions.FirstOrDefault();
                    }
                }
                else
                {
                    // If all the videos are seen and the test is also taken the set all the videos back to Not viewed
                    AppController.ResetSkillModuleStatus(Student.SMUserId);
                    VideoToDisplay = SMUserVideoTransactions.FirstOrDefault();
                }

                if (VideoToDisplay != null)
                {
                    videoOrderNoToDisplay = VideoToDisplay.SMOrder;
                }
            }

            return videoOrderNoToDisplay;
        }
    }
}
