using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class CMS_EditR : PageBase<IRemediationView, RemediationPresenter>, IRemediationView
{
    public string DisplayMessage
    {
        set
        {
            lblM.Visible = true;
            lblM.Text = value;
        }
    }

    protected int navQid
    {
        get
        {
            return Convert.ToInt32(ViewState["navQid"]);
        }

        set
        {
            ViewState["navQid"] = value;
        }
    }

    #region IRemeditionView

    public void DisplayUploadedTopics(IEnumerable<Remediation> validRemediations, IEnumerable<Remediation> invalidRemediations, IEnumerable<Remediation> duplicateTopics, string filePath)
    {
        throw new NotImplementedException();
    }

    public void PopulateControls(Remediation remediation)
    {
        txtRID.Text = remediation.RemediationId.ToString();
        txtTitle.Text = remediation.TopicTitle;
        txtRem.Text = remediation.Explanation;
        Literal li = new Literal();
        li.Text = remediation.Explanation.Trim();
        p1.Controls.Add(li);
    }

    public void PopulateQuestions(IEnumerable<Question> questions)
    {
        string[] s = new string[1];
        s[0] = "Id";
        gvRemediation.DataKeyNames = s;
        gvRemediation.DataSource = questions;
        gvRemediation.DataBind();
    }
    #endregion

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.Edit);
    }

    public void PopulateLippincotts()
    {
        throw new NotImplementedException();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        btnDelete.Attributes.Add("onclick", " return confirm('Are you sure that you want to delete the question?')");
        Presenter.InitializeRemediationParameters();
        if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to Edit Remediation Page")
                                .Add("Remediation Id", Presenter.Id.ToString())
                                .Write();
            #endregion
            Presenter.DiplayRemeditionDetails();
        }

        Presenter.DisplayQuestions(txtRID.Text);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Presenter.DeleteRemediation();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Remediation remediation = new Remediation();
        remediation.RemediationId = 0;
        if (!string.IsNullOrEmpty(txtRID.Text))
        {
            remediation.RemediationId = txtRID.Text.ToInt();
        }

        remediation.TopicTitle = txtTitle.Text.Trim();
        remediation.Explanation = txtRem.Text.Trim();
        Presenter.SaveRemediation(remediation);
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Presenter.NavigateToSearchQuestionPage();
    }

    protected void gvRemediation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewRemediation")
        {
            Presenter.NavigateViewRemediationPage(gvRemediation.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString());
        }
    }
}
