using System;
using System.Collections.Generic;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters.Controller;
using NursingLibrary.Utilities;

namespace NursingLibrary.Presenters
{
    public class StudentListOfReviewsPresenter : StudentPresenter<IStudentListOfReviewsView>
    {
        #region Constructors

        public StudentListOfReviewsPresenter(IStudentAppController appController)
            : base(appController)
        {
        }

        #endregion Constructors

        #region Methods

        public virtual IEnumerable<FinishedTest> GetFinishedTests()
        {
            return AppController.GetFinishedTests();
        }

        public virtual void OnGvListRowCommand(string commandName, int userTestId, int testId, TestType testType)
        {
            Student.UserTestId = userTestId;
            Student.TestId = testId;
            Student.QuizOrQBank = testType;
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

        public virtual void OnProductsSelectionChanged(string selectedValue)
        {
            #region Trace Information
            TraceHelper.Create(TraceToken, "List of Reviews On Products Selection Changed")
                .Add("Product Id ", selectedValue)
                .Add("Test Type ", Student.ProductId.ToString())
                .Write();
            #endregion
            Student.ProductId = Convert.ToInt32(selectedValue);
            Student.TestType = GetTestType(Student.ProductId);
            View.BindFinishedTest(GetFinishedTests());
        }

        public override void OnViewInitialized()
        {
            base.OnViewInitialized();
            InitializeView();
        }

        public override void OnViewLoaded()
        {
            base.OnViewLoaded();
            View.SetTestResultinks();
        }

        private void InitializeView()
        {
            if (View.EndQuery == "1")
            {
                AppController.UpdateTestStatusOnExpiry();
            }

            var products = new List<Product>(AppController.GetAllProducts());
            products.Insert(0, new Product { ProductId = 0, ProductName = "ALL" });
            View.PopulateProducts(products.ToArray());
            View.BindFinishedTest(GetFinishedTests());
        }

        #endregion Methods
    }
}