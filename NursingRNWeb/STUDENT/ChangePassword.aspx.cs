using System;
using System.Web.UI;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;

namespace STUDENT
{
    public partial class StudentChangePassword : StudentBasePage<IStudentChangePassword, StudentChangePasswordPresenter>, IStudentChangePassword
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Presenter.OnViewInitialized();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Presenter.OnViewLoaded();
        }

        protected void BtnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtOldPassword.Text))
            {
                LblMessage.Text = @"Old password is required.";
                return;
            }

            if (string.IsNullOrEmpty(TxtPassword.Text))
            {
                LblMessage.Text = @"New password is required.";
                return;
            }

            if (string.IsNullOrEmpty(TxtCpassword.Text))
            {
                LblMessage.Text = @"Confirmation of password is required.";
                return;
            }

            if (TxtPassword.Text != TxtCpassword.Text)
            {
                LblMessage.Text = @"New Passwords do not match";
                return;
            }

            if (!Presenter.ConfirmOldPassword(TxtOldPassword.Text.Trim()))
            {
                LblMessage.Text = @"Old Password do not match";
                return;
            }

           
            
            LblMessage.Text = Presenter.ChangePassword(TxtPassword.Text.Trim())
                                  ? "Password changed successfully!"
                                  : "Password change unsuccessful";
        }
    }
}
