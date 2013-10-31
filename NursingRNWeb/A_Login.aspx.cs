using System;
using System.Web.UI;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Common;

public partial class AdminLogin : PageBase<IAdminLogin, AdminLoginPresenter>, IAdminLogin
{
    public override void PreInitialize()
    {
        //// Nothing to do here.
    }

    public void ShowMessage(UserAuthentication result)
    {
        LblResult.Visible = true;

        if (result.Status == AuthenticationRequest.InValidUser)
        {
            LblResult.Text = "Sorry, your user name and password are incorrect. Please enter again.";
        }
        else if (result.Status == AuthenticationRequest.NoInstitution)
        {
            LblResult.Text = "Unable to login due to incomplete Account Information.";
        }
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
            }

            LoginContent loginContent = Presenter.GetLoginContent();
            divLoginContent.InnerHtml = loginContent.Content;
            lblApplicationRestartDate.Text = "Site status: Configuration last loaded at " + KTPApp.ApplicationRestartDate.ToString();
        }
    }

    protected void btnLogIn_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.AuthenticateUser(TxtUserName.Text.Trim(), TxtPassword.Text.Trim(), WebHelper.Environment);
    }
}
