using System;
using NursingLibrary;
using NursingLibrary.Utilities;
using NursingRNWeb;

public partial class ADMIN_Controls_AdminMainLeftMenu : UserControlBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblAdminName.Text = CurrentContext.User.FirstName; 
    }   
}