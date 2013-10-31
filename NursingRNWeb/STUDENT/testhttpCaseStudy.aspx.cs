using System;
using System.IO;
using System.Net;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

namespace STUDENT
{
    public partial class TesthttpCaseStudy : StudentBasePage<IStudentTestHttpCaseStudyView, StudentTestHttpCaseStudyPresenter>, IStudentTestHttpCaseStudyView
    {
        public void TestHttpCaseStudyLoad()
        {
            const string fileName = "C:\\test.xml";

            const string uri = "http://204.244.141.95/scripts/nursingrnstaging/SERVICE/CaseStudyResult.aspx";
            ////string uri = "http://localhost:4021/nursingrnweb/student/CaseStudyResult.aspx";

            var req = WebRequest.Create(uri);
            ////req.Proxy = WebProxy.GetDefaultProxy(); // Enable if using proxy
            req.Method = "POST";        //// Post method
            req.ContentType = "text/xml";     //// content type
            //// Wrap the request stream with a text-based writer
            using (var writer = new StreamWriter(req.GetRequestStream()))
            {
                writer.WriteLine(GetTextFromXMLFile(fileName));
            }
            //// Write the XML text into the stream
            //// Send the data to the webserver
            var rsp = req.GetResponse();
            using (var sr = new StreamReader(rsp.GetResponseStream()))
            {
                string strResult = sr.ReadToEnd();
                sr.Close();
                Label1.Text = strResult;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region Trace Information
                TraceHelper.WriteTraceEvent(TraceToken, "Navigated to Test http Case Study Page.");
                #endregion
                Presenter.OnViewInitialized();
            }

            Presenter.OnViewLoaded();
        }

        /// Function to read xml data from local system
        /// <summary>
        /// Read XML data from file
        /// </summary>
        /// <param name="file"></param>
        /// <returns>returns file content in XML string format</returns>
        private static string GetTextFromXMLFile(string file)
        {
            var reader = new StreamReader(file);
            string ret = reader.ReadToEnd();
            reader.Close();
            return ret;
        }
    }
}
