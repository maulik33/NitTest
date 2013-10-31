using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class CMS_NewLippincott : PageBase<ILippincottView, LippioncottPresenter>, ILippincottView
{
    public string AddMessage
    {
        set
        {
            errorMessage.Visible = true;
            errorMessage.Text = value;
        }
    }

    public string SerachTextBox
    {
        set { txtQuestion.Text = value; }
    }
 
    string ILippincottView.SearchCondition
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

    public string Mode
    {
        set { throw new NotImplementedException(); }
    }

    public int PageIndex
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

    public string Sort
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
        Presenter.PreInitialize(ViewMode.Edit);
    }

    #region ILippincottView Methods

    public void PopulateControls(IEnumerable<Remediation> remediations, Lippincott lippincott)
    {
        ddlRemediation.DataTextField = "TopicTitle";
        ddlRemediation.DataValueField = "RemediationID";
        ddlRemediation.DataSource = remediations;
        ddlRemediation.DataBind();
        ddlRemediation.Items.Insert(0, new ListItem("Not Selected", "-1"));

        if (lippincott != null && lippincott.LippincottID != 0)
        {
            txtLippincottTitle1.Text = lippincott.LippincottTitle;
            txtLippincottExplanation1.Text = lippincott.LippincottExplanation;
            txtLippincottTitle2.Text = lippincott.LippincottTitle2;
            txtLippincottExp2.Text = lippincott.LippincottExplanation2;
            ddlRemediation.SelectedValue = lippincott.RemediationId.ToString();
            lblHeader.Text = "Edit >= Lippincott";
        }
        else
        {
            lblHeader.Text = "Add > Lippincott";
        }

        Presenter.RefreshQuestionList();
    }

    public void PopulateQuestionList(IEnumerable<Lippincott> lippinCotts)
    {
        var questions = from q in lippinCotts
                        select q.Question;

        gvQuestions.DataSource = questions;
        gvQuestions.DataBind();
    }

    #endregion

    #region UnImplemented Methods/Prop

    public void OnViewInitialized()
    {
        throw new NotImplementedException();
    }

    public void SearchLippincott(IEnumerable<Lippincott> lippinCotts, SortInfo sortMetaData)
    {
        throw new NotImplementedException();
    }

    public void ShowMessage()
    {
        throw new NotImplementedException();
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            #region Trace Information
            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to Edit Lippincott Page")
                                .Add("Lippincott Id", Presenter.Id.ToString())
                                .Write();
            #endregion
            Presenter.PopulateLippinCottControls();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Add New Lippincott Button Clicked")
            .Add("New Question Text", txtQuestion.Text)
            .Write();
        #endregion
        if (!string.IsNullOrEmpty(txtQuestion.Text))
        {
            Presenter.SaveQuestions(txtQuestion.Text);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Confirm())
        {
            return;
        }

        Lippincott lippincott = new Lippincott();
        FillLippinCott(lippincott);
        Presenter.SaveLippincott(lippincott);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Presenter.NavigateToLippinCott();
    }

    protected void gvQuestions_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int qId = Convert.ToString(gvQuestions.DataKeys[e.RowIndex].Value).ToInt();
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Delete Lippincott Question Clicked")
            .Add("Delete Question Id", qId.ToString())
            .Write();
        #endregion
        Presenter.DeleteLippinCottQuestion(qId);
    }

    private void FillLippinCott(Lippincott lippincott)
    {
        lippincott.RemediationId = ddlRemediation.SelectedValue.ToInt();
        lippincott.LippincottTitle = txtLippincottTitle1.Text;
        lippincott.LippincottExplanation = txtLippincottExplanation1.Text;
        lippincott.LippincottTitle2 = txtLippincottTitle2.Text;
        lippincott.LippincottExplanation2 = txtLippincottExp2.Text;
    }

    private bool Confirm()
    {
        if (string.IsNullOrEmpty(this.txtLippincottTitle1.Text))
        {
            errorMessage.Text = "Lippincott Title is required.";
        }

        if (errorMessage.Text.Length > 0)
        {
            errorMessage.Visible = true;
            return false;
        }
        else
        {
            return true;
        }
    }
}