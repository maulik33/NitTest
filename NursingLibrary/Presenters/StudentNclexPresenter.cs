using NursingLibrary.Interfaces;
using NursingLibrary.Presenters.Controller;

namespace NursingLibrary.Presenters
{
    public class StudentNclexPresenter : StudentPresenter<IStudentNclexView>
    {
        #region Constructors

        public StudentNclexPresenter(IStudentAppController appController)
            : base(appController)
        {
        }

        #endregion Constructors

        #region Methods

        public virtual void OnGo_1_1()
        {
            Student.QuizOrQBank = TestType.Qbank;
            Student.TestSubGroup = 3;
            Student.ProductId = 4;
            AppController.ShowPage(PageDirectory.Qbank, null, null);
        }

        public virtual void OnGo_1_2()
        {
            Student.QuizOrQBank = TestType.Quiz;
            Student.TestSubGroup = 2;
            Student.ProductId = 4;
            AppController.ShowPage(PageDirectory.TestReview, null, null);
        }

        public virtual void OnGo_2_1()
        {
            Student.QuizOrQBank = TestType.Quiz;
            Student.TestSubGroup = 1;
            Student.ProductId = 4;
            AppController.ShowPage(PageDirectory.TestReview, null, null);
        }

        public virtual void OnGo_4_1()
        {
            Student.QuizOrQBank = TestType.Quiz;
            Student.TestSubGroup = 4;
            Student.ProductId = 4;
            AppController.ShowPage(PageDirectory.TestReview, null, null);
        }

        public virtual void OnGo_4_2()
        {
            Student.QuizOrQBank = TestType.Quiz;
            Student.TestSubGroup = 5;
            Student.ProductId = 4;
            AppController.ShowPage(PageDirectory.TestReview, null, null);
        }

        public override void OnViewInitialized()
        {
            base.OnViewInitialized();
            if (!Student.IsNclexTest)
            {
                AppController.ShowPage(PageDirectory.StudentHome, null, null);
            }

            Student.ProductId = 4;
            View.EnableNClexLinks();
            View.CreateAvpContentLink();
            if (AppController.DoesTestExists(4, 8, 0))
            {
                View.OnTestAssign(4, 8);
            }

            if (AppController.DoesTestExists(4, 9, 0))
            {
                View.OnTestAssign(4, 9);
            }
        }

        #endregion Methods
    }
}