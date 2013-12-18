using System;
using NursingLibrary.Utilities;

namespace NursingRNWeb.UTILITIES
{
    public partial class DbConnStr : System.Web.UI.Page
    {
       
        protected void btnSubmit_Click(Object sender, EventArgs e)
        {
            var isValidUser = Utilities.IsValidDomainUser(txtUserName.Text.Trim(), txtPassword.Text.Trim());
            if (isValidUser)
            {
                displayStringDiv.Visible = true;
                errMsgLbl.Visible = false;
                btnSubmit.Enabled = false;
            }
            else
                errMsgLbl.Visible = true;
        }

      
    }
}