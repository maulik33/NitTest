using System;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class admin_BackupData : PageBase<IReleaseView, ReleasePresenter>, IReleaseView
{
    #region  IReleaseView members

    public string ShowContent { get; set; }

    public string showLippincot { get; set; }

    public string showTests { get; set; }

    #endregion

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.Edit);
    }

    public void RenderReviewDetails(System.Collections.Generic.IEnumerable<NursingLibrary.Entity.Question> questions, System.Collections.Generic.List<NursingLibrary.Entity.Remediation> remediations, System.Collections.Generic.IEnumerable<NursingLibrary.Entity.Lippincott> lippincotts, System.Collections.Generic.List<NursingLibrary.Entity.Test> tests)
    {
        throw new NotImplementedException();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Presenter.ValidateAccess(Global.IsProductionApp);
        Presenter.InitializeReviewPorperties();
        Label2.Text = "You are about to release the approved items from the following areas:";

        if (ShowContent.ToLower() == "y")
        {
            Label2.Text += "<br><b>Content Questions and Remediations</b>";
        }

        if (showLippincot.ToLower() == "y")
        {
            Label2.Text += "<br><b>Lippincot</b>";
        }

        if (showTests.ToLower() == "y")
        {
            Label2.Text += "<br><b>Custom Tests</b>";
        }

        if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to Release the Approved items Page")
                                .Add("Content Questions and Remediations", ShowContent)
                                .Add("Lippincot", showLippincot)
                                .Add("Custom Tests", showTests)
                                .Write();
            #endregion
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Label4.Text = "Release to Production Started at: " + System.DateTime.Now;
        Presenter.ReleaseToProduction();
        Label4.Text += "<br>Release to Production  Finished at: " + System.DateTime.Now;
        Label1.Text = "<b>Successfully released.</b>.";
        Label2.Text = string.Empty;
        Button1.Visible = false;
        Button2.Visible = false;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Presenter.NavigateToReviewPage(ShowContent, showLippincot, showTests);
    }
}
