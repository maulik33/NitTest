using System.Collections.Generic;
using System.Linq;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters.Controller;

namespace NursingLibrary.Presenters
{
    public class StudentHomePresenter : StudentPresenter<IStudentHomeView>
    {
        #region Constructors

        public StudentHomePresenter(IStudentAppController appController)
            : base(appController)
        {
        }

        #endregion Constructors

        #region Methods

        public virtual void lbl_R_F_Click()
        {
            Student.ProductId = 1;
            Student.QuizOrQBank = TestType.Quiz;
            AppController.ShowPage(PageDirectory.ListReview, null, null);
        }

        public virtual void OnlbFocusedReview_Click()
        {
            Student.ProductId = (int)ProductType.FocusedReviewTests;
            Student.TestSubGroup = 1;
            AppController.ShowPage(PageDirectory.ListReview, null, null);
        }

        public virtual void OnlbFRTest_Click()
        {
            Student.ProductId = (int)ProductType.FocusedReviewTests;
            Student.TestSubGroup = 1;
            Student.QuizOrQBank = TestType.Quiz;
            AppController.ShowPage(PageDirectory.TestReview, null, null);
        }

        public virtual void OnlbIntegratedTests_Click()
        {
            Student.ProductId = (int)ProductType.IntegratedTesting;
            Student.TestSubGroup = 1;
            AppController.ShowPage(PageDirectory.ListReview, null, null);
        }

        public virtual void OnlbSMTest_Click()
        {
            Student.ProductId = (int)ProductType.SkillsModules;
            Student.TestSubGroup = 1;
            ////AppController.ShowPage(PageDirectory.TestReview, null, null);
        }

        public virtual void Onlbl_Q6_Click()
        {
            AppController.ShowPage(PageDirectory.KaplanReceiveCustomer, null, "productCode=NCLEXAV02");
        }

        public virtual void Onlbl_Q7_Click()
        {
            AppController.ShowPage(PageDirectory.KaplanReceiveCustomer, null, "productCode=NCLEXAV03");
        }

        public virtual void Onlbl_Q8_Click()
        {
            AppController.ShowPage(PageDirectory.KaplanReceiveCustomer, null, "productCode=NCLEXAV05");
        }

        public virtual void Onlbl_QBank_Click()
        {
            if (Student.IsQbankTest)
            {
                Student.ProductId = (int)ProductType.NCLEXRNPrep;
                Student.TestSubGroup = 3;
                Student.QuizOrQBank = TestType.Qbank;
                AppController.ShowPage(PageDirectory.Qbank, null, null);
            }
            else
            {
                Student.ProductId = (int)ProductType.NCLEXRNPrep;
                Student.TestSubGroup = 2;
                Student.QuizOrQBank = TestType.Qbank;
                AppController.ShowPage(PageDirectory.Nclex, null, null);
            }
        }

        public virtual void Onlbl_QT_Click()
        {
            Student.ProductId = (int)ProductType.NCLEXRNPrep;
            Student.TestSubGroup = 1;
            Student.QuizOrQBank = TestType.Quiz;
            AppController.ShowPage(PageDirectory.TestReview, null, null);
        }

        public virtual void Onlbl_R_F_Click()
        {
            Student.ProductId = (int)ProductType.FocusedReviewTests;
            Student.QuizOrQBank = TestType.Quiz;
            AppController.ShowPage(PageDirectory.ListReview, null, null);
        }

        public virtual void OnlbNCLEXPrepRezults_Click()
        {
            Student.ProductId = (int)ProductType.NCLEXRNPrep;
            AppController.ShowPage(PageDirectory.ListReview, null, null);
        }

        public virtual void Onlb_IT_Click()
        {
            Student.ProductId = (int)ProductType.IntegratedTesting;
            Student.TestSubGroup = 1;
            Student.QuizOrQBank = TestType.Quiz;
            AppController.ShowPage(PageDirectory.TestReview, null, null);
        }

        public virtual void OnLinkButton2_Click()
        {
            AppController.ShowPage(PageDirectory.Nclex, null, null);
        }

        public override void OnViewLoaded()
        {
            base.OnViewLoaded();
            View.EnableIntegratedTestCtrls();
            View.EnableFocusedReviewTestCtrld();
            var avpContents = AppController.GetAvpContent((int)ProductType.NCLEXRNPrep, 10);
            var enableNclexAvp = avpContents.Select(item => item.ProductId).ToArray().Length != 0;
            View.EnableNclexTestCtrls(enableNclexAvp);
            var caseStudies = AppController.GetCaseStudies();
            ////View.SetCaseStudiesControls(caseStudies.OrderBy(p => p.CaseOrder));
            View.SetheaderCss();
            var skillsModules = AppController.GetUnTakenTestsforSkillsModules();
            ////View.SetSkillsModulesControls(skillsModules.OrderBy(p => p.TestName));            
            View.PopulateSkills(skillsModules.OrderBy(p => p.TestName));
            View.PopulateCase(caseStudies.OrderBy(p => p.CaseOrder));
            IEnumerable<AssetGroup> assetGroups = AppController.GetAssetGroupByProgramId(Student.ProgramId);
            View.SetControlVisibility(assetGroups);
            View.PopulateDashBoardLinks(AppController.GetDashBoardLinks(Student.ProgramId));
        }

        public void CreateSkillsModulesDetails(int testId)
        {
            Student.SMUserId = AppController.CreateSkillsModulesDetails(testId, Student.UserId);
        }

        #endregion Methods

        public void OnlbFRSearchRemediation_Click()
        {
        }

        public void OnFRCreateOwnTest_Click()
        {
            Student.ProductId = (int)ProductType.FocusedReviewTests;
            Student.TestSubGroup = 1;
            Student.QuizOrQBank = TestType.Qbank;
            AppController.ShowPage(PageDirectory.FRRemediation, null, null);
        }

        public void OnCreateFRQBank()
        {
            Student.TestSubGroup = 1;
            Student.ProductId = (int)ProductType.FocusedReviewTests;
            Student.TestType = TestType.FocusedReview;
            AppController.ShowPage(PageDirectory.FRQBank, null, null);
        }

        public virtual void Onlb_SM_Click()
        {
            Student.ProductId = (int)ProductType.SkillsModules;
            Student.TestSubGroup = 1;
            AppController.ShowPage(PageDirectory.SkillsModules, null, null);
        }
    }
}