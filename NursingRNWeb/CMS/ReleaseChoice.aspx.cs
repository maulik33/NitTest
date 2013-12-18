using System;
using System.Collections.Generic;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class CMS_ReleaseChoice : PageBase<IReleaseView, ReleasePresenter>, IReleaseView
{
    public string ShowContent
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public string showLippincot
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public string showTests
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public override void PreInitialize()
    {
        // throw new NotImplementedException();
    }

    public void RenderReviewDetails(IEnumerable<Question> questions, List<Remediation> remediations, IEnumerable<Lippincott> lippincotts, List<Test> tests)
    {
        throw new NotImplementedException();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Presenter.ValidateAccess(Global.IsProductionApp);
        if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Release Choice Page");
            #endregion
        }
    }

    protected void btnContinue_Click(object sender, EventArgs e)
    {
        string showContent = "N";
        string showLippincot = "N";
        string showTests = "N";

        if (chkContent.Checked || chkLippincot.Checked || chkTests.Checked)
        {
            if (chkContent.Checked)
            {
                showContent = "Y";
            }

            if (chkLippincot.Checked)
            {
                showLippincot = "Y";
            }

            if (chkTests.Checked)
            {
                showTests = "Y";
            }

            Presenter.NavigateToReviewPage(showContent, showLippincot, showTests);
        }
    }
}
