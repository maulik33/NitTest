using NursingLibrary.Interfaces;
using NursingLibrary.Presenters.Controller;

namespace NursingLibrary.Presenters
{
    public class StudentQBankGraphPresenter : StudentPresenter<IStudentQBankGraphView>
    {
        #region Constructors

        public StudentQBankGraphPresenter(IStudentAppController appController)
            : base(appController)
        {
        }

        #endregion Constructors

        #region Methods

        public override void OnViewLoaded()
        {
            base.OnViewLoaded();
            View.RefreshGraph(AppController.GetQBankGraphData(View.AType));
        }

        #endregion Methods
    }
}