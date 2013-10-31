using System;
using System.Collections.Generic;
using System.Web.UI;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class LoginContentRelease : PageBase<ILoginContentReleaseView, LoginContentReleasePresenter>, ILoginContentReleaseView
{
    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.Edit);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoginContent loginContent = Presenter.GetLoginContent(ddlLoginContentType.SelectedValue.ToInt());
            txtContent.Text = loginContent.Content;
            SetEnableStatus(loginContent);

            if (Presenter.CurrentContext.UserType != UserType.SuperAdmin || Global.IsProductionApp == true)
            {
                btnpreview.Enabled = false;
                btnRelease.Enabled = false;
                txtContent.Enabled = false;
                btnRevert.Enabled = false;
            }
        }
    }

    protected void btnRelease_Click(object sender, EventArgs e)
    {
        LoginContent loginContent = new LoginContent();
        loginContent.Content = txtContent.Text;
        loginContent.Id = ddlLoginContentType.SelectedValue.ToInt();
        Presenter.ReleaseLoginContent(loginContent);
        SetEnableStatus(loginContent);
    }

    protected void btnpreview_Click(object sender, EventArgs e)
    {
        string _url = String.Empty;
        LoginContent loginContent = new LoginContent();
        loginContent.Content = txtContent.Text;
        loginContent.Id = ddlLoginContentType.SelectedValue.ToInt();
        Presenter.SaveLoginContents(loginContent);
        if (ddlLoginContentType.SelectedValue.ToInt() == (int)LoginContents.Admin)
        {
            _url = "../A_Login.aspx?ispreview=1";
        }
        else
        {
            _url = "../S_Login.aspx?ispreview=1";
        }

        this.Page.ClientScript.RegisterClientScriptBlock(
        this.GetType(),
        "openNewWindow", "window.open(\"" + _url + "\", '', 'width=960,height=660,resizable=yes,scrollbars=yes,toolbar=no,location=no,directories=no,status=no,menubar=no,copyhistory=no');",
        true);
        SetEnableStatus(loginContent);
    }

    protected void ddlLoginContentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoginContent loginContent = Presenter.GetLoginContent(ddlLoginContentType.SelectedValue.ToInt());
        txtContent.Text = loginContent.Content;
        if (loginContent.ReleaseStatus == "R")
        {
            btnRevert.Enabled = false;
            btnRelease.Enabled = false;
        }
        else
        {
            btnRevert.Enabled = true;
            btnRelease.Enabled = true;
        }
    }

    protected void btnRevert_Click(object sender, EventArgs e)
    {
        LoginContent loginContent = new LoginContent();
        loginContent.Id = ddlLoginContentType.SelectedValue.ToInt();
        Presenter.RevertLoginContent(loginContent);
        txtContent.Text = loginContent.Content;
        SetEnableStatus(loginContent);
    }

    private void SetEnableStatus(LoginContent loginContent)
    {
        if (loginContent.ReleaseStatus == "R")
        {
            btnRevert.Enabled = false;
            btnRelease.Enabled = false;
        }
        else
        {
            btnRevert.Enabled = true;
            btnRelease.Enabled = true;
        }
    }
}
