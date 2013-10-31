using NursingLibrary.Interfaces;
using NursingLibrary.Presenters.Controller;

namespace NursingLibrary.Presenters
{
    public class StudentShowImagesPresenter : StudentPresenter<IStudentShowImagesView>
    {
        #region Constructors

        public StudentShowImagesPresenter(IStudentAppController appController)
            : base(appController)
        {
        }

        #endregion Constructors

        #region Methods

        public override void OnViewLoaded()
        {
            base.OnViewLoaded();
            View.ShowImage();
        }

        #endregion Methods
    }
}