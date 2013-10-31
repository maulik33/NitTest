using System.Collections.Generic;
using System.Linq;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters.Controller;

namespace NursingLibrary.Presenters
{
    public class StudentQbankRPresenter : StudentPresenter<IStudentQBankRView>
    {
        #region Constructors

        public StudentQbankRPresenter(IStudentAppController appController)
            : base(appController)
        {
        }

        #endregion Constructors

        #region Methods

        public virtual IEnumerable<FinishedTest> GetTestsNCLEXInfoForTheUser(int testSubgroup)
        {
            return AppController.GetTestsNCLEXInfoForTheUser().Where(tst => tst.TestSubGroup == testSubgroup);
        }

        public virtual void OnAnalysis()
        {
            AppController.ShowPage(PageDirectory.QbankP, null, null);
        }

        public virtual void OnCreate()
        {
            AppController.ShowPage(PageDirectory.Qbank, null, null);
        }

        public virtual void OnGridListSampleRowCommand(string commandName, int userTestId, int testId, TestType testType)
        {
            Student.UserTestId = userTestId;
            Student.TestId = testId;
            Student.QuizOrQBank = testType;
            Student.TestType = GetTestType(Student.ProductId);
            Student.NumberOfQuestions = 0;
            if (commandName == "GoToReview")
            {
                AppController.ShowPage(PageDirectory.Review, null, null);
            }

            if (commandName == "GoToAnalysis")
            {
                AppController.ShowPage(PageDirectory.Analysis, null, null);
            }
            
            if (commandName == "Resume")
            {
                Student.Action = Action.Resume;
                AppController.ShowPage(PageDirectory.Resume, null, null);
            }
        }

        public virtual void OnListReview()
        {
            AppController.ShowPage(PageDirectory.QbankR, null, null);
        }

        public virtual void OnSampleClick(TestType testType, int testSubGroup, int productId)
        {
            Student.QuizOrQBank = testType;
            Student.TestSubGroup = testSubGroup;
            Student.ProductId = productId;
            AppController.ShowPage(PageDirectory.TestReview, null, null);
        }

        public override void OnViewLoaded()
        {
            base.OnViewLoaded();
            LoadView();
        }

        /// <summary>
        /// This method is used to populate view details on Load.
        /// </summary>
        private void LoadView()
        {
            if (View.EndQuery == "1")
            {
                AppController.UpdateTestStatus();
            }

            var tests = AppController.GetTestsNCLEXInfoForTheUser();
            View.BindViewSample(tests.Where(tst => tst.TestSubGroup == 2));
            View.BindViewList(tests.Where(tst => tst.TestSubGroup == 3));
        }

        #endregion Methods
    }
}