using System;
using System.Web.UI;
using NursingLibrary.Presenters;
using NursingLibrary.Presenters.Navigation;

namespace STUDENT
{
    public partial class IntroError : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnQuit_Click(object sender, ImageClickEventArgs e)
        {
            new PageNavigator().NaviagteTo(PageDirectory.TestReview, null, null);
        }
    }
}