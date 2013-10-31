using System.Web.UI;
using NursingRNWeb;

public partial class ADMIN_header : UserControlBase
{
    protected void excelbtn_Click(object sender, ImageClickEventArgs e)
    {
        this.Page.Response.ContentType = "application/vnd.ms-excel";
    }

    protected void HomePageLogoButton_Click(object sender, ImageClickEventArgs e)
    {
        Navigator.NavigateTo(NursingLibrary.Presenters.AdminPageDirectory.AdminHome);
    }
}
