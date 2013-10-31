using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using NursingLibrary.Common;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters.Controller;
using NursingLibrary.Utilities;

namespace NursingLibrary.Presenters
{
    public class StudentTestReviewPresenter : StudentPresenter<IStudentTestReviewView>
    {
        #region Constructors

        public StudentTestReviewPresenter(IStudentAppController appController)
            : base(appController)
        {
        }

        #endregion Constructors

        #region Methods

        public virtual void OnGridViewAllTestRowCommand(int testId)
        {
            Student.TestId = testId;
            Student.Action = AppController.ContinueTest() ? Action.NewTest : Action.NewTest;
            Student.NumberOfQuestions = 0;
            AppController.ShowPage(PageDirectory.Resume, null, null);
        }

        public virtual void OnGridViewCustomTestRowCommand(int userTestId)
        {
            var newTest = AppController.CreateFRQBankRepeatTest(userTestId);
            if (newTest.UserTestId > 0)
            {
                Student.QuizOrQBank = TestType.Qbank;
                Student.Action = Action.QBankCreate;
                Student.ProductId = (int)ProductType.FocusedReviewTests;
                Student.TestId = newTest.TestId;
                Student.TestType = TestType.FocusedReview;
                Student.UserTestId = newTest.UserTestId;
                AppController.ShowPage(PageDirectory.Resume, null, null);
            }
        }

        public virtual void OnGridViewSuspendedTestsRowCommand(int userTestId, int testId, string suspendType, bool customFRTest)
        {
            Student.Action = Action.Resume;
            Student.UserTestId = userTestId;
            Student.QuizOrQBank = TestType.Quiz;
            Student.TestId = testId;
            Student.SuspendType = suspendType;
            Student.NumberOfQuestions = customFRTest ? AppController.GetCustomFRTestQuestionCount(userTestId) : 0;
            AppController.ShowPage(PageDirectory.Resume, null, null);
        }

        public virtual void OnGridViewTakenTestsRowCommand(int userTestId, int testId, bool IsReviewCommand)
        {
            Student.Action = Action.Resume;
            Student.UserTestId = userTestId;
            Student.QuizOrQBank = TestType.Quiz;
            Student.TestId = testId;
            if (IsReviewCommand)
            {
                AppController.ShowPage(PageDirectory.Review, null, null);
            }
        }

        public override void OnViewLoaded()
        {
            base.OnViewLoaded();
            IntializeView();
        }

        public void OnCreateTestClick()
        {
            Student.QuizOrQBank = TestType.FocusedReview;
            Student.TestSubGroup = 1;
            Student.ProductId = (int)ProductType.FocusedReviewTests;
            AppController.ShowPage(PageDirectory.FRRemediation, null, null);
        }

        public void OnCreateCustomTestClick()
        {
            Student.TestSubGroup = 1;
            Student.ProductId = (int)ProductType.FocusedReviewTests;
            Student.TestType = TestType.FocusedReview;
            AppController.ShowPage(PageDirectory.FRQBank, null, null);
        }


        public string GetLtiRequestFormForTestSecurityProvider(string studentId, string institutionId,int testId = 0)
        {
            LtiProvider ltiProvider = GetLtiTestSecurityProvider();
            LtiResourceInfo ltiResourceInfo = GetLtiSecuredTestResourceInfo(studentId, institutionId,testId);

            var ltiRequst = LtiUtility.CreateLtiRequest(ltiProvider, ltiResourceInfo);
            HttpContext.Current.Session["consumer_key"] = ltiProvider.ConsumerKey;
            var ltiRequestForm = LtiRequestForm.BuildPostRequestForm(ltiRequst, "target = '_self'");

            return ltiRequestForm;
        }

        public LtiProvider GetLtiTestSecurityProvider()
        {
            return AppController.GetLtiTestSecurityProviderByName(Constants.LTI_VERIFICIENT_STARTTEST_SECURITY_PROVIDER);
        }

        public LtiResourceInfo GetLtiSecuredTestResourceInfo(string studentId, string institutionId, int testId)
        {
            return new LtiResourceInfo
            {
                UserId = studentId,
                FirstName =  Student.FirstName,
                LastName =  Student.LastName,
                UserType = UserType.Student.ToString(),
                StudentId = studentId,
                ProductId = 1,
                TestId = testId,
                TestName = "",
                InstitutionId = institutionId,
                LaunchPresentationReturnUrl = "student/TestReview.aspx",
                
            };
        }


        private void IntializeView()
        {
            // validation
            if (Student.UserId == -1 || Student.TestSubGroup == 0 || Student.ProductId == 0)
            {
                throw new InvalidOperationException(string.Format("Page Load Error, User:{0} TestSubGroup:{1} Product{2}", Student.UserId, Student.TestSubGroup, Student.ProductId));
            }

            var product = AppController.GetAllProducts().FirstOrDefault(prd => prd.ProductId == Student.ProductId);
            if (product == null)
            {
                throw new InvalidOperationException(string.Format("Page Load Error, Product doesnt exist for Product Id = {0}", Student.ProductId));
            }

            if (product.ProductId == (int) ProductType.IntegratedTesting)
            {
                View.EnableProctorTrack = PresentationHelper.IsProctorTrackEnabled(Student.IsProctorTrackEnabled);
                if (View.EnableProctorTrack)
                {
                    string proctorTrackStartUrl = KTPApp.ProctorTrackTestStartUrl();
                    if (proctorTrackStartUrl != null)
                    {
                        View.ProctorTrackStartUrl = proctorTrackStartUrl.Trim();
                    }
                    else
                    {
                        throw new Exception("ProctorTrack Url not present");
                    }

                }
            }
            View.SetResponseProperties();
            if (product.ProductName == "NCLEX" || product.ProductName == "NCLEX-RN Prep")
            {
                View.ProductName = "NCLEX-RN &reg; Prep";
            }
            else
            {
                View.ProductName = product.ProductName;
            }

            if (product.MultiUseTest == 0 || Student.TestSubGroup == 4 || Student.TestSubGroup == 5)
            {
                View.BindAllTestsGrid(AppController.GetUnTakenTests());
            }
            else
            {
                if (Student.ProductId == (int)ProductType.FocusedReviewTests)
                {
                    var allTests = new List<Test>();
                    allTests.AddRange(AppController.GetAllTests(2));
                    allTests.AddRange(AppController.GetAllTests(1));
                    allTests.AddRange(AppController.GetAllTests(0));
                    View.BindAllTestsGrid(allTests.ToArray());
                    View.BindAllCustomizedTestsGrid(AppController.GetCustomizedFRTests(Student.UserId, Student.TimeOffset));
                }
                else
                {
                    View.BindAllTestsGrid(AppController.GetAllTests(product.Bundle));
                }
            }

            Student.TestType = GetTestType(product.ProductId);
            if (product.TestType.Trim() == "F" || product.TestType.Trim() == "I" || product.TestType.Trim() == "N")
            {
                View.BindSuspendedTestsGrid(AppController.GetSuspendedTests());
            }

            View.BindTakenTestsGrid(AppController.GetTakenTests());

        }
        #endregion
    }
}