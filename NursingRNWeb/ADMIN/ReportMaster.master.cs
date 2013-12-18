using System;
using System.Web.Security;
using System.Web.UI;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingRNWeb;

public partial class ADMIN_ReportMaster : MasterPageBase
{
    protected void Page_Init(object sender, EventArgs e)
    {
        RegisterUserControls(ucAdminMainLeftMenu, ucAdminMainRightMenu, Head111);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(CurrentContext.UserId.ToString()))
        {
            Navigator.NavigateTo(AdminPageDirectory.AdminLogin);
        }

        if (CurrentContext.UserType == UserType.LocalAdmin)
        {
            tr3.Visible = false;
            tr31.Visible = false;
            tr32.Visible = false;

            tr5.Visible = false;
            tr51.Visible = false;
            tr52.Visible = false;
            tr53.Visible = false;
        }
        else if (CurrentContext.UserType == UserType.InstitutionalAdmin)
        {
            tr5.Visible = false;
            tr51.Visible = false;
            tr52.Visible = false;
            tr53.Visible = false;
        }
    }
    
    protected void btnLogout_Click(object sender, ImageClickEventArgs e)
    {
        Session.Abandon();
        FormsAuthentication.SignOut();
        Navigator.NavigateTo(AdminPageDirectory.AdminLogin);
    }
}
