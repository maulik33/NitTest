using System;
using System.Web.UI;
using NursingLibrary.Interfaces;
using NursingRNWeb;

public partial class ADMIN_Controls_AdminMainRightMenu : UserControlBase
{
    public bool ShowMainMenuButton { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        HyperLink1.Visible = CurrentContext.UserType.Equals(UserType.SuperAdmin);

        if (ShowMainMenuButton == true)
        {
            btnMainMenu.Visible = true;
        }
    }

    protected void btnLogout_Click(object sender, ImageClickEventArgs e)
    {
        Session.Abandon();
        Navigator.NavigateTo(NursingLibrary.Presenters.AdminPageDirectory.AdminLogin);
    }

    protected void btnMainMenu_Click(object sender, ImageClickEventArgs e)
    {
        Navigator.NavigateTo(NursingLibrary.Presenters.AdminPageDirectory.AdminHome);
    }
}