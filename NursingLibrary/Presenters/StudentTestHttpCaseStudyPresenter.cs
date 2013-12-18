using NursingLibrary.Interfaces;
using NursingLibrary.Presenters.Controller;

namespace NursingLibrary.Presenters
{
    public class StudentTestHttpCaseStudyPresenter : StudentPresenter<IStudentTestHttpCaseStudyView>
    {
        #region Constructors

        public StudentTestHttpCaseStudyPresenter(IStudentAppController appController)
            : base(appController)
        {
        }

        #endregion Constructors

        #region Methods
        
        public override void OnViewLoaded()
        {
            base.OnViewLoaded();
            View.TestHttpCaseStudyLoad();
        }

        #endregion Methods
    }
}