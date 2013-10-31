using System.Linq;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters.Controller;

namespace NursingLibrary.Presenters
{
    public class StudentCaseStudiesPresenter : StudentPresenter<IStudentCaseStudiesView>
    {
        #region Constructors

        public StudentCaseStudiesPresenter(IStudentAppController appController)
            : base(appController)
        {
        }

        #endregion Constructors

        #region Methods

        public override void OnViewLoaded()
        {
            base.OnViewLoaded();
            View.AddCaseTable(AppController.GetCaseStudies().OrderBy(p => p.CaseOrder));
        }

        #endregion Methods
    }
}