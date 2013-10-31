using NursingLibrary.Interfaces;
using NursingLibrary.Presenters.Controller;

namespace NursingLibrary.Presenters
{
    public class StudentHeaderPresenter : StudentPresenter<IStudentHeaderView>
    {
        #region Constructors

        public StudentHeaderPresenter(IStudentAppController appController)
            : base(appController)
        {
        }

        #endregion Constructors

        #region Methods

        public virtual void OnIbCaseStudies_Click()
        {
            Student.ProductId = 5;
            AppController.ShowPage(PageDirectory.CaseStudies, null, null);
        }

        public virtual void OnIbFocusedReview_Click()
        {
            Student.TestSubGroup = 1;
            Student.ProductId = 3;
            Student.TestType = TestType.FocusedReview;
            AppController.ShowPage(PageDirectory.TestReview, null, null);
        }

        public virtual void OnIbHome_Click()
        {
            Student.ProductId = -1;
            AppController.ShowPage(PageDirectory.StudentHome, null, null);
        }

        public virtual void OnIbIntegratedTest_Click()
        {
            Student.TestSubGroup = 1;
               Student.ProductId = 1;
               Student.TestType = TestType.Integrated;
               AppController.ShowPage(PageDirectory.TestReview, null, null);
        }

        public virtual void OnIbSkillsModule_Click()
        {
            Student.TestSubGroup = 1;
            Student.ProductId = 6;
            Student.TestType = TestType.SkillsModules;
            AppController.ShowPage(PageDirectory.SkillsModules, null, null);
        }

        public virtual void OnIbNclex_Click()
        {
            Student.TestSubGroup = 1;
            Student.ProductId = 4;
            Student.TestType = TestType.Nclex;
            AppController.ShowPage(PageDirectory.Nclex, null, null);
        }

        public virtual void OnIbResults_Click()
        {
            Student.TestType = TestType.Undefined;
            Student.ProductId = 0;
            AppController.ShowPage(PageDirectory.ListReview, null, null);
        }

        public virtual void OnLbLogout_Click()
        {
            AppController.ShowPage(PageDirectory.StudentLogin, null, null);
        }

        public virtual void OnPreRender()
        {
            View.SetHeaderControls();
        }

        #endregion Methods
    }
}