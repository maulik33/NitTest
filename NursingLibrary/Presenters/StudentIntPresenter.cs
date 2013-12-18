using System;
using System.Collections.Generic;
using System.Linq;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters.Controller;

namespace NursingLibrary.Presenters
{
    public class StudentIntPresenter : StudentPresenter<IStudentIntView>
    {
        #region Constructors

        public StudentIntPresenter(IStudentAppController appController)
            : base(appController)
        {
        }

        #endregion Constructors

        #region Enumerations

        public enum RowCommand
        {
            Explanation,
            Explanation1
        }

        #endregion Enumerations

        #region Methods

        public virtual string GetCategoryDescription(int categoryId, int categoryDetailId)
        {
            return AppController.GetCategoryDescription((CategoryName)Enum.Parse(typeof(CategoryName), categoryId.ToString()), categoryDetailId);
        }
        
        // To be called only when Page is loaded first time
        public virtual void InitializePresenter()
        {
            OnViewInitialized();
            var productTest = AppController.GetAllProductTests().FirstOrDefault(tst => tst.TestId == Student.TestId);
            Student.ProductId = (productTest != null) ? productTest.ProductId : 0;
            Student.TestType = GetTestType(Student.ProductId);
        }

        public virtual void LoadProducts()
        {
            var products = new List<Product>(AppController.GetAllProducts());
            products.Insert(0, new Product { ProductId = 0, ProductName = "ALL" });
            View.LoadProducts(products);
        }

        public virtual void LoadTests()
        {
            var productTests = new List<UserTest>(AppController.GetTestByProductUser());
            productTests.Insert(0, new UserTest { UserTestId = 0, TestName = "Not Selected" });
            View.LoadTests(productTests);
        }

        public virtual void NavigateToAnalysis()
        {
            AppController.ShowPage(PageDirectory.Analysis, null, null);
        }

        public virtual void NavigateToIntro(RowCommand command, string qId, string item, string rId)
        {
            Action action = Action.Review;
            TestType? quizOrQBank = TestType.Quiz;
            string typeOfFileId = item;
            const string queryString = "";
            switch (command)
            {
                case RowCommand.Explanation:
                    {
                        if (Student.TestType == TestType.Integrated)
                        {
                            action = Action.Remediation;
                            typeOfFileId = null;
                        }
                        else if (Student.TestType == TestType.Nclex)
                        {
                            quizOrQBank = null;
                        }

                        break;
                    }
            }

            Student.Action = action;
            Student.QuestionId = Convert.ToInt32(qId);

            // Student.NumberOfQuestions = 0;
            if (quizOrQBank.HasValue)
            {
                Student.QuizOrQBank = quizOrQBank.Value;
            }

            if (typeOfFileId != null)
            {
                Student.SuspendType = typeOfFileId;
            }

            AppController.ShowPage(PageDirectory.Resume, null, queryString);
        }

        public virtual void RefreshData()
        {
            LoadProducts();
            LoadTests();
            UpdateReviewResultsHeading();
            UpdateResults();
        }

        public virtual void SyncData(int selectedProductId, int selectedUserTestId)
        {
            bool resetUserTestId = false;
            if (Student.ProductId.ToInt() != selectedProductId)
            {
                Student.ProductId = selectedProductId;
                Student.TestType = GetTestType(Student.ProductId);
                Student.TestId = 0;
                Student.UserTestId = 0;
                resetUserTestId = true;
            }

            if (false == resetUserTestId)
            {
                Student.UserTestId = selectedUserTestId;
            }
        }

        public virtual void UpdateResults()
        {
            if (Student.UserTestId == 0)
            {
                return;
            }

            Student.TestId = AppController.GetUserTestByID().FirstOrDefault().TestId;
            IEnumerable<UserQuestion> testQuestions = AppController.GetTestQuestionsForUser();
            IEnumerable<Category> testCharacteristics = AppController.GetStudentTestCharacteristics();

            UserQuestion lastQuestion = testQuestions.LastOrDefault();
            Student.NumberOfQuestions = (lastQuestion != null) ? lastQuestion.QuestionNumber : 0;

            View.ShowTestResults(testQuestions, testCharacteristics, Student.TestType, Student.TestId == 74);
        }

        public virtual void UpdateReviewResultsHeading()
        {
            View.UpdateReviewResultsHeading();
        }

        #endregion Methods
    }
}