using NursingLibrary.Interfaces;
using NursingLibrary.Presenters.Controller;

namespace NursingLibrary.Presenters
{
    public class StudentGraphPresenter : StudentPresenter<IStudentGraphView>
    {
        #region Constructors

        public StudentGraphPresenter(IStudentAppController appController)
            : base(appController)
        {
        }

        #endregion Constructors

        #region Methods

        public override void OnViewLoaded()
        {
            base.OnViewLoaded();
            View.ResultsFromTheProgram = AppController.GetProgramResults(1);
            View.GenerateGraph();
        }

        #endregion Methods
    }
}