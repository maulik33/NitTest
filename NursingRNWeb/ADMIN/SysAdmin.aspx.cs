using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

namespace NursingRNWeb.ADMIN
{
    public partial class SysAdmin : PageBase<ISysAdminView, SysAdminPresenter>, ISysAdminView
    {
        public override void PreInitialize()
        {
        }

        public void DisplayCheckSystemResults(IDictionary<int, string> results)
        {
            CheckSystemResultsGridView.DataSource = results;
            CheckSystemResultsGridView.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region Trace Information
                TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Sys Admin Page");
                #endregion

                Presenter.CheckSystem();
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Submitted Admin Request");
                AuthenticateRequest();
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private void AuthenticateRequest()
        {
            if (PassCodeTextBox.Text != "Kaplan@123")
            {
                throw new ApplicationException("Invalid Passcode. The Request cannot be processed.");
            }
        }

        private void ShowError(string message)
        {
            MessageLabel.Text = message;
        }
    }
}