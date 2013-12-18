namespace STUDENT
{
    public partial class STUDENT_AccessDenied : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            string Msg = Page.Request.QueryString["ErrMsg"];
            Messenger1.ShowMessage(!string.IsNullOrEmpty(Msg)
                                            ? Msg
                                            : "Your IP address is not allowed to access this page.");
        }
    }
}
