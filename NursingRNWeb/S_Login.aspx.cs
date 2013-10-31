using System;
using System.Web.UI;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Common;

public partial class Login : StudentBasePage<ILoginView, StudentLoginPresenter>, ILoginView
{
    public void FailedLogin(LoginFailure loginFailure)
    {
        // user has failed to login correctly
        LblResult.Visible = true;

        switch (loginFailure)
        {
            case LoginFailure.AuthenticationFailed:
            case LoginFailure.UserDetailsNotComplete:
                LblResult.Text =
                    "Student information has not been found. Please contact KAPLAN at 1 (800) 533 - 8850";
                break;
            case LoginFailure.InvalidCohortId:
                LblResult.Text =
                    "Student has not been assigned into any Cohort. Please contact KAPLAN at 1 (800) 533 - 8850";
                break;
            case LoginFailure.InvalidGroupId:
                LblResult.Text = @"You are not assigned into any Group. Please contact KAPLAN at 1 (800) 533 - 8850";
                break;
            default:
                LblResult.Text =
                    @"The username or password you entered is incorrect. For help, please contact your school or call 1(800) 533-8850.";
                break;
        }
    }

    public void DisplayMessage(string message, bool IsResetMailsent)
    {
        throw new NotImplementedException();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ispreview"] == "1")
            {
                TxtPassword.Enabled = false;
                TxtUserName.Enabled = false;
                BtnLogIn.Enabled = false;
                ForgotPassword.Enabled = false;
            }

            LoginContent loginContent = Presenter.GetLoginContent();
            divLoginContent.InnerHtml = loginContent.Content;
            lblApplicationRestartDate.Text = "Site status: Configuration last loaded at " + KTPApp.ApplicationRestartDate.ToString();
        }
    }

    protected void BtnLogIn_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.OnLoginButtonClick(TxtUserName.Text, TxtPassword.Text, Session.SessionID, WebHelper.Environment);
    }

    protected override void OnError(Exception exception)
    {
        if (exception is LoginException)
        {
            FailedLogin(LoginFailure.AuthenticationFailed);
        }
        else
        {
            FailedLogin(LoginFailure.SystemFailure);
        }
    }

    protected void ForgotPassword_Click(object sender, EventArgs e)
    {
        popupFrame.Attributes.Add("src", "PasswordConfirmation.aspx");
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), string.Empty, "openPopUp()", true);
    }
}
