using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using NursingLibrary.Common;

namespace NursingRNWeb.STUDENT
{
    /// <summary>
    /// Summary description for SubmitToProctorTrackOnEndTest
    /// </summary>
    public class SubmitToProctorTrackOnEndTest : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string result = string.Empty;
            string proctorSubmitUrl = KTPApp.ProctorTrackTestSubmitUrl();
            string verificientTestSessionId = string.Empty;
            int testId = 0;
            int userId = 0;
            string consumerkey = string.Empty;

            if (!String.IsNullOrEmpty((string)HttpContext.Current.Session["verificientTestSessionId"]))
            {
                verificientTestSessionId = (string) HttpContext.Current.Session["verificientTestSessionId"];
            }
          
            if (HttpContext.Current.Session["selectedTestId"] != null)
            {
                testId = (int)HttpContext.Current.Session["selectedTestId"];
            }
            if (HttpContext.Current.Session["user_id"] != null)
            {
                userId = (int)HttpContext.Current.Session["user_id"];
            }
            if (!String.IsNullOrEmpty((string)HttpContext.Current.Session["consumer_key"]))
            {
                consumerkey = (string)HttpContext.Current.Session["consumer_key"];
            }

            if (proctorSubmitUrl != string.Empty)
            {
                proctorSubmitUrl = proctorSubmitUrl + "/" + verificientTestSessionId + "/end/";
                var postParameters = string.Format("user_id={0}&custom_test_id={1}&consumer_key={2}", userId, testId, consumerkey);
                try
                {
                    WebRequest verificientRequest = WebRequest.Create(proctorSubmitUrl);
                    verificientRequest.Method = "POST";

                    var byteParameters = Encoding.UTF8.GetBytes(postParameters);
                    verificientRequest.ContentLength = byteParameters.Length;
                    var requestStream = verificientRequest.GetRequestStream();
                    requestStream.Write(byteParameters, 0, byteParameters.Length);
                    requestStream.Close();

                    using (var response = (HttpWebResponse)verificientRequest.GetResponse())
                    {
                        result = response.StatusDescription;
                    }

                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                }
            }
            context.Response.Write(result);
            HttpContext.Current.Session.Remove("verificientTestSessionId");
            HttpContext.Current.Session.Remove("selectedTestId");
            HttpContext.Current.Session.Remove("user_id");
            HttpContext.Current.Session.Remove("consumer_key");
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}