using System;
using System.Text;
using System.Web.Configuration;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

namespace Service
{
    public partial class LaunchDxr : PageBase<IReportLaunchDxrView, ReportLaunchDxrPresenter>, IReportLaunchDxrView
    {
        private readonly string _dxrKey = WebConfigurationManager.AppSettings["DxrKey"];
        private readonly string _dxrUrl = WebConfigurationManager.AppSettings["DxrUrl"];

        public string DxrKey
        {
            get { return _dxrKey; }
        }

        public string DxUrl
        {
            get { return _dxrUrl; }
        }

        public string DxrHash
        {
            get { return Utilities.CreateShaHash(DxrKey, TimeStamp, EnrolmentId); }
        }

        public string TimeStamp
        {
            get { return Utilities.GetIsoDate(DateTime.Now); }
        }

        public string EnrolmentId
        {
            get;
            set;
        }

        public override void PreInitialize()
        {
        }

        public void GetCaseDetails(string contentId, string firstName, string lastName)
        {
            #region Trace Information
            var traceToken = new TraceToken();
            TraceHelper.Create(traceToken, "Analysis Test")
                .Add("Enrolment Id ", EnrolmentId)
                .Add("Content Id ", contentId)
                .Add("TimeStamp ", TimeStamp)
                .Add("First Name ", firstName)
                .Add("Last Name ", lastName)
                .Write();
            #endregion
            string postData = string.Format("dxac=kplogin&eid={0}&cid={1}&ts={2}&st={3}&first_name={4}&last_name={5}",
                                                EnrolmentId, contentId, TimeStamp, DxrHash, firstName, lastName);
            byte[] postBuffer = Encoding.UTF8.GetBytes(postData);

            string response = Utilities.HttpPost(DxUrl, postBuffer);
            RegisterScript(response);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Presenter.GetCaseDetails();
        }

        private void RegisterScript(string response)
        {
            string injectionScript = "<script>";
            injectionScript += " var r = eval(" + "(" + response + ")" + ");";
            injectionScript +=
                @" if ( r.status == 1000 ) { document.getElementById('lblError').innerHTML = 'Loading........';self.location.replace(r.message);} else {document.getElementById('lblError').innerHTML = r.message;}</script>";
            ClientScript.RegisterStartupScript(GetType(), "kaplan", injectionScript);
        }
    }
}
