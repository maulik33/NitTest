using System;
using System.Collections.Generic;
using System.Linq;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters.Controller;

namespace NursingLibrary.Presenters
{
    public class StudentAnalysisPresenter : StudentPresenter<IStudentAnalysisView>
    {
        #region Constructors

        public StudentAnalysisPresenter(IStudentAppController appController)
            : base(appController)
        {
        }

        #endregion Constructors

        #region Methods

        public virtual void OnReviewBtnClick()
        {
            AppController.ShowPage(PageDirectory.Review, null, null);
        }

        public override void OnViewInitialized()
        {
            base.OnViewInitialized();
            var productTest = AppController.GetAllProductTests().FirstOrDefault(tst => tst.TestId == Student.TestId);
            Student.ProductId = (productTest != null) ? productTest.ProductId : 0;
        }

        public virtual void RefreshData()
        {
            LoadProducts();
            PopulateTestsByUser();
            BindData();
        }

        public virtual void LoadProducts()
        {
            var products = new List<Product>(AppController.GetAllProducts());
            products.Insert(0, new Product { ProductId = 0, ProductName = "ALL" });
            View.PopulateProducts(products.ToArray());
        }

        public virtual void SyncData(int selectedProductId, int selectedUserTestId)
        {
            bool resetUserTestId = false;
            if (Student.ProductId.ToInt() != selectedProductId)
            {
                Student.ProductId = selectedProductId;
                Student.UserTestId = 0;
                Student.TestId = 0;
                resetUserTestId = true;
            }

            if (false == resetUserTestId)
            {
                Student.UserTestId = selectedUserTestId;
            }
        }

        private void BindData()
        {
            Student.TestType = GetTestType(Student.ProductId);
            UserTest userTest = AppController.GetUserTestByID().FirstOrDefault();
            if (userTest != null)
            {
                Student.TestId = userTest.TestId;
                var categories = AppController.GetStudentTestCharacteristics();
                View.BindData(AppController.GetProgramResults(2));
                View.ProgramResults = AppController.GetProgramResultsByNorm();
                View.LoadTables_I(categories, AppController.GetProbability(Convert.ToInt32(View.NumberCorrect)),
                    AppController.GetPercentileRank(Convert.ToInt32(View.NumberCorrect)), AppController.CheckProbabilityExists(), AppController.CheckPercentileRankExists());
            }
        }

        private void PopulateTestsByUser()
        {
            var productTests = new List<UserTest>(AppController.GetTestByProductUser());
            productTests.Insert(0, new UserTest { UserTestId = 0, TestName = "Not Selected" });
            View.PopulateTestsByUser(productTests);
        }
        #endregion Methods
    }
}