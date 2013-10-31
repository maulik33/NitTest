using System;

public partial class ADMIN_Calender : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LnkCalendar.Attributes.Add("href", "Javascript:pickDate('" + txtCalendar.Name + "')");
    }
}
