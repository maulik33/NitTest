using System.Linq;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters.Controller;
using NursingLibrary.Utilities;

namespace NursingLibrary.Presenters
{
    public class StudentQBankPresenter : StudentPresenter<IStudentQBankView>
    {
        #region Constructors

        public StudentQBankPresenter(IStudentAppController appController)
            : base(appController)
        {
        }

        #endregion Constructors

        #region Methods

        public virtual void OnBtnCreateClick(string qNumber)
        {
            if (qNumber != null && !qNumber.Trim().Equals(string.Empty))
            {
                View.GetTestDetails();
                #region Trace Information
                TraceHelper.Create(TraceToken, "Create Test Clicked")
                    .Add("Test Id ", "74")
                    .Add("QuizOrQBank ", TestType.Qbank.ToString())
                    .Add("Number Of Questions ", TestType.Qbank.ToString())
                    .Add("Suspend Type ", "01")
                    .Add("Tutor Mode ", View.TutorMode.ToString())
                    .Add("Reuse Mode ", View.ReuseMode.ToString())
                    .Add("Correct ", View.Correct.ToString())
                    .Add("CategoryList ", View.CategoryList)
                    .Write();
                #endregion
                Student.QuizOrQBank = TestType.Qbank;
                Student.NumberOfQuestions = View.NumberOfQuestions;
                Student.SuspendType = "01";

                var newTest = AppController.CreateQBankTest(View.TutorMode, View.ReuseMode, View.Correct, View.CategoryList);
                if (newTest.UserTestId > 0)
                {
                    Student.Action = Action.QBankCreate;
                    Student.QuizOrQBank = TestType.Qbank;
                    Student.ProductId = 4;
                    Student.TestType = TestType.Nclex;
                    Student.UserTestId = newTest.UserTestId;
                    AppController.ShowPage(PageDirectory.Resume, null, null);
                }
            }
        }

        public virtual void OnlbAnalysisClick()
        {
            AppController.ShowPage(PageDirectory.QbankP, null, null);
        }

        public virtual void OnlbCreateClick()
        {
            AppController.ShowPage(PageDirectory.Qbank, null, null);
        }

        public virtual void OnlbListReviewClick()
        {
            AppController.ShowPage(PageDirectory.QbankR, null, null);
        }

        public virtual void OnlbSampleClick()
        {
            Student.QuizOrQBank = TestType.Quiz;
            Student.TestSubGroup = 2;
            Student.ProductId = 4;
            AppController.ShowPage(PageDirectory.TestReview, null, null);
        }

        public override void OnViewLoaded()
        {
            base.OnViewLoaded();
            
            var qbankTest = AppController.GetQBankTest(Student.UserId, (int)ProductType.NCLEXRNPrep, 3,Student.TimeOffset);
             if (qbankTest.ToList().Count() > 1)
             {
                 qbankTest = qbankTest.Where(rnqbankTest => rnqbankTest.ProgramofStudyId == (int) ProgramofStudyType.RN);
             }
            Student.TestId = qbankTest.FirstOrDefault().TestId;
            View.ClientNeeds = AppController.GetListOfAllClientNeeds(Student.QBankProgramofStudyId);
            var clientNeedsCategoryInfo = AppController.GetListOfAllClientNeedsCategoryInfo(Student.UserId);
            View.ClientNeedsCategory = clientNeedsCategoryInfo.ToArray();
            View.Create_MainTable();
            View.NumberOfCategory = AppController.GetNumberOfCategory();
            View.SetControls();
        }

        #endregion Methods
    }
}