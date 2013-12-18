using NursingLibrary.Interfaces;
using NursingLibrary.Presenters.Controller;
using NursingLibrary.Utilities;

namespace NursingLibrary.Presenters
{
    public class StudentChangePasswordPresenter : StudentPresenter<IStudentChangePassword>
    {
        #region Constructors

        public StudentChangePasswordPresenter(IStudentAppController appController)
            : base(appController)
        {
        }

        #endregion Constructors

        #region Methods

        public virtual bool ChangePassword(string text)
        {
            #region Trace Information
            TraceHelper.Create(TraceToken, "Navigated To Change Password Page.")
                .Add("Password ", text)
                .Write();
            #endregion
            return AppController.ChangePassword(text);
        }

        public bool ConfirmOldPassword(string oldPassword)
        {
            var password = AppController.GetUserDetailsById(Student.UserId);
            return password == oldPassword;
        }

        #endregion Methods
    }
}