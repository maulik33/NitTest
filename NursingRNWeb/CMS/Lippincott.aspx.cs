using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.Common;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class CMS_Lippincott : PageBase<ILippincottView, LippioncottPresenter>, ILippincottView
{
    public string SearchCondition
    {
        get
        {
            return hdnSearch.Value;
        }

        set
        {
            hdnSearch.Value = value;
        }
    }

    public string Sort
    {
        get
        {
            return hdnGridConfig.Value;
        }

        set
        {
            hdnGridConfig.Value = value;
        }
    }

    public string AddMessage
    {
        set { throw new NotImplementedException(); }
    }

    public string SerachTextBox
    {
        set { throw new NotImplementedException(); }
    }

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.List);
    }

    #region ILippincottView Members

    public void SearchLippincott(IEnumerable<Lippincott> lippinCotts, SortInfo sortMetaData)
    {
        if (lippinCotts != null)
        {
            gvLippincott.DataSource = KTPSort.Sort<Lippincott>(lippinCotts, sortMetaData);
            gvLippincott.DataBind();
        }
    }
    #endregion

    #region UnImplementedMethods
    public void PopulateQuestionList(System.Collections.Generic.IEnumerable<Lippincott> lippinCotts)
    {
        throw new NotImplementedException();
    }

    public void PopulateControls(System.Collections.Generic.IEnumerable<Remediation> remediations, Lippincott lippincott)
    {
        throw new NotImplementedException();
    }

    public void ShowMessage()
    {
        throw new NotImplementedException();
    }

    public void OnViewInitialized()
    {
        // throw new NotImplementedException();
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            Presenter.InitializeSearchLippincott();
            SearchLippincott();
        }

        if (Global.IsProductionApp)
        {
            btnNewLippincott.Visible = false;
            btnReadLippincott.Visible = false;
        }
        else
        {
            btnNewLippincott.Visible = true;
            btnReadLippincott.Visible = true;
        }
    }

    protected void btnNewLippincott_Click(object sender, EventArgs e)
    {
        Presenter.NavigateToNewLippincott();
    }

    protected void btnReadLippincott_Click(object sender, EventArgs e)
    {
        Presenter.NavigateToLippincottTemplate();
    }

    protected void gvLippincott_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);
        SearchLippincott();
    }

    protected void gvLippincott_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLippincott.PageIndex = e.NewPageIndex;
        SearchLippincott();
    }

    protected void searchButton_Click(object sender, ImageClickEventArgs e)
    {
        hdnSearch.Value = txtSearch.Text.Trim();
        SearchLippincott();
    }

    protected void gvLippincott_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            WebControl deleteButton = (WebControl)e.Row.Cells[4].Controls[0];
            deleteButton.Attributes.Add("onclick", "return confirm('Are you sure that you want to delete the Test?')");
            HyperLink hlEdit = (HyperLink)e.Row.Cells[3].Controls[1];
            string url = hlEdit.NavigateUrl;
            url += "&SearchCondition=" + hdnSearch.Value + "&sort=" + hdnGridConfig.Value;
            hlEdit.NavigateUrl = url;
        }
    }

    protected void gvLippincott_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int lippincottId = Convert.ToInt32(gvLippincott.DataKeys[e.RowIndex].Values[0].ToString());
        Presenter.DeleteLippincott(lippincottId);
        SearchLippincott();
    }

    private void SearchLippincott()
    {
        if (hdnGridConfig.Value.Length == 0)
        {
            hdnGridConfig.Value = "LippincottID|DESC";
        }
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Lippincott List Page")
            .Add("Search Keyword", hdnSearch.Value)
            .Write();
        #endregion
        Presenter.SearchLippincott(hdnSearch.Value, hdnGridConfig.Value);
    }
}