using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters.Controller;
using NursingLibrary.Utilities;

namespace NursingLibrary.Presenters
{
    public class SkillModulesPresenter : StudentPresenter<ISkillModulesView>
    {
        private const string Suspended = "01";

        #region Constructors

        public SkillModulesPresenter(IStudentAppController appController)
            : base(appController)
        {
        }

        #endregion Constructors

        public override void OnViewInitialized()
        {
            base.OnViewInitialized();
            IEnumerable<SMTest> UserSMTest = AppController.GetSkillsModuleTestsByUserId(Student.UserId);
            IEnumerable<Test> UnTakenTests = AppController.GetUnTakenTests();
            View.BindSkillsModulesGrid(UnTakenTests);
            View.BindAvailableQuizzesGrid(GetActiveTest(AppController.GetSkillsModulesAvailableQuizzes(Student.UserId, Student.TimeOffset, Student.ProductId), UserSMTest, UnTakenTests));
            View.BindSuspendedQuizzesGrid(GetActiveTest(AppController.GetSuspendedTests(), UserSMTest, UnTakenTests));
            View.BindViewQuizResultsGrid(AppController.GetTakenTests());
        }

        public void CreateSkillsModulesDetails(int TestId)
        {
            Student.SMUserId = AppController.CreateSkillsModulesDetails(TestId, Student.UserId);
            if (Student.SMUserId > 0)
            {
                View.TestId = TestId;
            }
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

        public void RepeatQuizzes(int userId, int skillmoduleId)
        {
            TakeSMQuiz(userId, skillmoduleId);
        }

        public virtual void OnGridViewSuspendedQuizzesRowCommand(int userTestId, int testId, string suspendType, bool customFRTest)
        {
            Student.Action = Action.Resume;
            Student.UserTestId = userTestId;
            Student.QuizOrQBank = TestType.Qbank;
            Student.TestId = testId;
            Student.SuspendType = suspendType;
            Student.NumberOfQuestions = customFRTest ? AppController.GetQuestionCount(userTestId) : 0;
            AppController.ShowPage(PageDirectory.Resume, null, null);
        }

        public virtual void OnGridViewQuizResultsRowCommand(int userTestId, int testId, bool IsReviewCommand)
        {
            Student.Action = Action.Resume;
            Student.UserTestId = userTestId;
            Student.QuizOrQBank = TestType.Qbank;
            Student.TestId = testId;
            if (IsReviewCommand)
            {
                AppController.ShowPage(PageDirectory.Review, null, null);
            }
        }

        private void DisplayPreviousNextVideos(ViewDirection viewDirection)
        {
            int videoOrderNoToDisplay = 0;
            bool navigateToTest = false, navigateToLandingPage = false;
            SMUserVideoTransaction VideoTransDetails;
            IEnumerable<SMUserVideoTransaction> SMUserVideoTransactions = AppController.GetSkillsModuleVideos(Student.SMUserId).OrderBy(r => r.SMOrder);
            if (SMUserVideoTransactions.Count() > 0)
            {
                if (viewDirection == ViewDirection.Next)
                {
                    var videoId = SMUserVideoTransactions.Where(r => r.SMOrder == View.OrderNumber).Select(p => p.SMUserVideoId).FirstOrDefault();
                    UpdateSkillModuleStatus(videoId);
                }

                SetNavigationProps(SMUserVideoTransactions, viewDirection, ref navigateToTest, ref navigateToLandingPage);
                if (navigateToTest && View.IsProductionApplication)
                {
                    // User can take quiz only in the production environment.
                    TakeSMQuiz(Student.UserId, View.TestId);
                }
                else if (navigateToTest && !View.IsProductionApplication)
                {
                    View.ShowSMPage();
                }

                if (navigateToLandingPage)
                {
                    View.ShowSMPage();
                }

                videoOrderNoToDisplay = GetVideoOrderNumber(viewDirection, SMUserVideoTransactions);
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

        private void SetNavigationProps(IEnumerable<SMUserVideoTransaction> SMUserVideoTransactions, ViewDirection viewDirection, ref bool navigateToTest, ref bool navigateToLandingPage)
        {
            IEnumerable<SMTest> SMTests;
            if (viewDirection == ViewDirection.Next && View.OrderNumber == SMUserVideoTransactions.Count())
            {
                // User is at last test video on click on next navigate to test page if user has not taken test atleast once otherwise navigate to landing page.
                SMTests = AppController.GetSkillsModuleTests(Student.UserId, View.TestId);
                if (SMTests.Count() == 0)
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
                SMTests = AppController.GetSkillsModuleTests(Student.UserId, View.TestId);

                if (SMTests.Count() == 0 && SMUserVideoTransactions.Where(r => r.IsVideoFullyViewed).Count() == SMUserVideoTransactions.Count())
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

            var newTest = AppController.CreateSkillsModuleTest(skillmoduleId);

            if (newTest.UserTestId > 0)
            {
                Student.TestId = newTest.TestId;
                Student.Action = Action.QBankCreate;
                Student.TestType = TestType.SkillsModules;
                Student.TestType = TestType.SkillsModules;
                Student.UserTestId = newTest.UserTestId;
                AppController.ShowPage(PageDirectory.Resume, null, null);
            }
        }

        private int GetVideoOrderNumber(ViewDirection viewDirection, IEnumerable<SMUserVideoTransaction> SMUserVideoTransactions)
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
                if (SMUserVideoTransactions.Where(r => r.IsVideoFullyViewed == false).Count() > 0)
                {
                    // When unviewed video present than show unviewd video
                    IEnumerable<SMUserVideoTransaction> tempList = (IEnumerable<SMUserVideoTransaction>)SMUserVideoTransactions.Where(r => r.IsVideoFullyViewed == false).OrderBy(q => q.SMOrder);
                    VideoToDisplay = tempList.ToList().FirstOrDefault();
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

        private IEnumerable<UserTest> GetActiveTest(IEnumerable<UserTest> userTests, IEnumerable<SMTest> UserSmTests, IEnumerable<Test> unTakenTest)
        {
            var activeUserTests = from utt in unTakenTest
                                  join ust in UserSmTests on utt.TestId equals ust.SkillModuleId
                                  join ut in userTests on ust.NewTestId equals ut.TestId
                                  select ut;
            return activeUserTests;
        }
    }
}
