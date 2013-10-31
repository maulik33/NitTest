using System;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;

public partial class Error : PageBase<IErrorView, ErrorPresenter>, IErrorView
{
    private const string ISADMINLOGIN = "IS_ADMIN_LOGIN";

    public override void PreInitialize()
    {
        // Nothing to do here.
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        bool isAdminLogin = IsAdminLogin();

        Session.Clear();

        Session[ISADMINLOGIN] = isAdminLogin;

        if (isAdminLogin)
        {
            login.HRef = "A_Login.aspx";
        }
        else
        {
            login.HRef = "S_Login.aspx";
        }
    }

    private bool IsAdminLogin()
    {
        bool isAdminLogin = false;
        do
        {
            if (Presenter.CurrentContext != null)
            {
                // When Session is not cleared yet (i.e User has not been logged out).
                if (false == Presenter.CurrentContext.IsAdminLogin)
                {
                    break;
                }
            }
            else
            {
                // When Session is cleared and user clicked back button or navigated thru some other means.
                if (Session[ISADMINLOGIN] == null
                    || string.Compare(Session[ISADMINLOGIN].ToString(), "True", true) != 0)
                {
                    break;
                }
            }

            isAdminLogin = true;
        }
        while (false);

        return isAdminLogin;
    }
}
