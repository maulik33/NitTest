using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NursingRNWeb.ADMIN
{
    public partial class StudentRepeat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                imgRepeatDate.Attributes.Add("onclick",
                                             string.Format(
                                                 "window.open('popupC.aspx?textbox={0}','cal','width=250,height=225,left=270,top=180')",
                                                 txtRepeatDate.ClientID));

                imgRepeatDate.Style.Add("cursor", "pointer");
            }
        }
    }
}