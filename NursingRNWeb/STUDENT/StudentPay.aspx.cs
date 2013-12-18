using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.Common;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

namespace NursingRNWeb.STUDENT
{
    public partial class StudentPay : StudentBasePage<IStudentPayView, StudentPayPresenter>, IStudentPayView
    {
        public void AddPostToKaptestScript(string userId, string courseAccessId, string encryptedToken)
        {
            string formId = "PostForm";

            ////Build the post form
            StringBuilder postForm = new StringBuilder();
            postForm.Append("<form id=\"" + formId + "\" name=\"" + formId + "\" action=\"" + KTPApp.AccountURL + "\" method=\"post\">");
            postForm.Append("<input type=\"hidden\" name=\"userId\" id=\"userId\" value=\"" + userId + "\">");
            postForm.Append("<input type=\"hidden\" name=\"courseAccessId\" id=\"courseAccessId\" value=\"" + courseAccessId + "\">");
            postForm.Append("<input type=\"hidden\" name=\"enToken\" id=\"enToken\" value=\"" + encryptedToken + "\">");
            postForm.Append("</form>");

            ////Build the javascript to post
            StringBuilder postScript = new StringBuilder();
            postScript.Append("<script language='javascript'>");
            postScript.Append("var sp" + formId + " = document." + formId + ";");
            postScript.Append("sp" + formId + ".submit();");
            postScript.Append("</script>");

            #region Trace Information
            TraceHelper.WriteTraceEvent(TraceToken, string.Format("Posting to {0} with params {1} - {2} - {3}.", KTPApp.AccountURL, userId, courseAccessId, encryptedToken));
            #endregion

            Page.Controls.Add(new LiteralControl(postForm.ToString() + postScript.ToString()));
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Presenter.OnViewInitialized();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region Trace Information
                TraceHelper.WriteTraceEvent(TraceToken, "Navigated to Student Pay Page.");
                #endregion

                Presenter.OnViewLoaded();
            }
        }
    }
}