using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;

namespace NursingRNWeb.ADMIN
{
    public partial class AuditTrailView : PageBase<IAuditTrailView,AuditTrailPresenter>,IAuditTrailView
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["USERID"] != null)
            {
                if (Request.QueryString["USERID"] != string.Empty)
                {
                    int userId = Convert.ToInt32(Request.QueryString["USERID"]);
                    Presenter.GetStudentAuditTrail(userId);
                }
            }
        }

        #region abstract members
        public override void PreInitialize()
        {
            Presenter.PreInitialize(ViewMode.View);
        }
        #endregion

        #region IAuditTrailView

        public void GetStudentAuditTrail(IEnumerable<AuditTrail> auditTrailData)
        {
            if (auditTrailData.Any())
            {
                lblStudentId.Text = auditTrailData.First().StudentId.ToString();
                gridAuditTrails.DataSource = auditTrailData;
                gridAuditTrails.DataBind();
            }
            else
            {
                lblNoDataAvailable.Visible = true;
                gridAuditTrails.Visible = false;
                lblStudentIDTxt.Visible = false;
            }
        }

        #endregion
    }
}