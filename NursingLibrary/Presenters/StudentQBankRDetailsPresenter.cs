using System.Linq;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters.Controller;

namespace NursingLibrary.Presenters
{
    public class StudentQBankRDetailsPresenter : StudentPresenter<IStudentQBankRDetailsView>
    {
        #region Constructors

        public StudentQBankRDetailsPresenter(IStudentAppController appController)
            : base(appController)
        {
        }

        #endregion Constructors

        #region Methods

        public virtual void OnbtnReviewClicks()
        {
            Student.TestType = TestType.Nclex;
            Student.QuizOrQBank = TestType.Qbank;
            AppController.ShowPage(PageDirectory.Review, null, null);
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

        public override void OnViewInitialized()
        {
            base.OnViewInitialized();
            Student.UserTestId = View.UserTestID;
            Student.TestId = AppController.GetUserTestByID().FirstOrDefault().TestId;
            BindData();
        }

        private void BindData()
        {
            var categories = AppController.GetStudentTestCharacteristics();
            View.BindData(AppController.GetProgramResults(2));
            View.ProgramResults = AppController.GetProgramResultsByNorm();
            View.LoadTables_N(categories);
        }

        #endregion Methods
    }
}