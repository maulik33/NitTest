using System;
using NursingLibrary.DTC;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

namespace STUDENT
{
    public partial class QBankGraph : StudentBasePage<IStudentQBankGraphView, StudentQBankGraphPresenter>, IStudentQBankGraphView
    {
        public int AType
        {
            get { return Request.QueryString["AType"].ToInt(); }
        }

        #region IQBank_GraphView Members

        public void RefreshGraph(string xmlData)
        {
            Response.Expires = 0;
            Response.ContentType = "text/xml";
            Response.Write(xmlData);
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region Trace Information
                TraceHelper.Create(TraceToken, "Navigated Question Bank Graph Page")
                    .Add("AType ", AType.ToString())
                    .Write();
                #endregion
                Presenter.OnViewInitialized();
            }

            Presenter.OnViewLoaded();
        }
    }
}
