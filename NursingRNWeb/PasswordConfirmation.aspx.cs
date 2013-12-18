using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;

public partial class PasswordConfirmation : StudentBasePage<ILoginView, StudentLoginPresenter>, ILoginView
{
    public void FailedLogin(LoginFailure loginFailure)
    {
    }

    public void DisplayMessage(string message, bool IsResetMailsent)
    {
        ////var strScript = "<script language=JavaScript>";
        ////strScript += "alert('" + message + "')";
        ////strScript += "</script>";
        ////ScriptManager.RegisterStartupScript(this, typeof(Page), "clientScript", strScript, false);
        hdnIsResetMailsent.Value = IsResetMailsent.ToString();
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), string.Empty, "openPopUp('" + message + "')", true);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        hdnIsResetMailsent.Value = "false";
    }

    protected void btnSendResetPasswordEmail_Click(object sender, EventArgs e)
    {
        string userName = txtUserName.Text.Trim();
        string userEmailId = txtEmail.Text.Trim();
        Presenter.SendPasswordResetMail(userName, userEmailId, WebHelper.Environment);
    }
}
