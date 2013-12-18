using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI.WebControls;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class ADMIN_AVPItems1 : PageBase<IAVPItemView, AVPItemPresenter>, IAVPItemView
{
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

    public string TestName
    {
        get
        {
            string testName = string.Empty;
            if (ViewState["TestName"] != null)
            {
                testName = Convert.ToString(ViewState["TestName"]);
            }

            return testName;
        }

        set
        {
            ViewState["TestName"] = value;
        }
    }

    public string URL
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

    public string PopWidth
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

    public string PopHeight
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

    public string HeaderLabelText
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

    public int TestId
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

    public bool Confirm()
    {
        throw new NotImplementedException();
    }

    public void RenderProgramOfStudyUI(IEnumerable<ProgramofStudy> programofStudies, int selectedProgramOfStudyId,
                                   bool programOfStudiesDropDownEnabled)
    {
        if (programOfStudiesDropDownEnabled)
        {
            ddlProgramOfStudy.ShowNotSelected = false;
            ddlProgramOfStudy.NotSelectedText = "Selection Required";

        }
        else
        {
            if (Enum.IsDefined(typeof(ProgramofStudyType), selectedProgramOfStudyId))
            {
                lblProgramofStudyVal.Text = ((ProgramofStudyType)selectedProgramOfStudyId).ToString();
            }
            else
            {
                throw new InvalidEnumArgumentException("Unexpected Program of Study Type " + selectedProgramOfStudyId.ToString());
            }
        }
        ddlProgramOfStudy.Visible = programOfStudiesDropDownEnabled;
        lblProgramofStudyVal.Visible = !programOfStudiesDropDownEnabled;

        ddlProgramOfStudy.DataSource = programofStudies;
        ddlProgramOfStudy.DataTextField = "ProgramofStudyName";
        ddlProgramOfStudy.DataValueField = "ProgramofStudyId";
        ddlProgramOfStudy.DataBind();
        ddlProgramOfStudy.SelectedValue = selectedProgramOfStudyId.ToString();

    }

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.List);
    }

    public void RefreshPage(IEnumerable<Test> tests, SortInfo sortMetaData)
    {
        gvAVPItems.DataSource = KTPSort.Sort<Test>(tests, sortMetaData);
        gvAVPItems.DataBind();
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            Presenter.InitializeAVPItemsItemProps();
            txtTestName.Text = TestName;
            SearchAVPItems(ddlProgramOfStudy.SelectedValue.ToInt());
            if (Global.IsProductionApp)
            {
                newAVPButton.Visible = false;
            }
        }
    }

    protected void newAVPButton_Click(object sender, System.EventArgs e)
    {
        int programOfStudyId = ddlProgramOfStudy.SelectedValue.ToInt();
        Presenter.NavigateToEdit(programOfStudyId);
    }

    protected void ddProgramOfStudy_SelectedIndexChanged(object sender, EventArgs e)
    {
        SearchAVPItems(ddlProgramOfStudy.SelectedValue.ToInt());
    }
    
    
    protected void gvAVPItems_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        gvAVPItems.PageIndex = e.NewPageIndex;
        SearchAVPItems(ddlProgramOfStudy.SelectedValue.ToInt());
    }

    protected void gvAVPItems_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Test test = (Test)e.Row.DataItem;
            System.Web.UI.WebControls.WebControl l = (WebControl)e.Row.Cells[4].Controls[0];
            l.Attributes.Add("onclick", "return confirm('Are you sure that you want to delete this AVP item?')");
            l = (WebControl)e.Row.Cells[2].Controls[1];
            l.Attributes.Add("onclick", "window.open('" + test.URL + "','Nursing','width=" + test.PopupWidth + ",height=" + test.PopupHeight + ",status=yes,fullscreen=no,toolbar=no,menubar=no,location=no,resizable=yes')");
        }
    }

    protected void gvAVPItems_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
    {
        hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);
        SearchAVPItems(ddlProgramOfStudy.SelectedValue.ToInt());
    }

    protected void searchButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        TestName = txtTestName.Text;
        SearchAVPItems(ddlProgramOfStudy.SelectedValue.ToInt());
    }

    protected void gvAVPItems_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
        int testId = (int)gvAVPItems.DataKeys[e.RowIndex].Value;
        Presenter.DeleteAVPItemById(testId);
        SearchAVPItems(ddlProgramOfStudy.SelectedValue.ToInt());
    }

    protected void gvAVPItems_RowEditing1(object sender, GridViewEditEventArgs e)
    {
        int testId = (int)gvAVPItems.DataKeys[e.NewEditIndex].Value;
        Presenter.NavigateToEdit(testId.ToString(), UserAction.Edit, ddlProgramOfStudy.SelectedValue.ToInt());
    }

    private void SearchAVPItems(int programOfStudyId)
    {
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "AVP Items List Page")
            .Add("Search Keyword", txtTestName.Text)
            .Write();
        #endregion
        Presenter.SearchAVPItems(hdnGridConfig.Value, programOfStudyId);
    }
}