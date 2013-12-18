using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class Admin_ProgramList : PageBase<IProgramView, ProgramPresenter>, IProgramView
{
    public string Name
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

    public string Description
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

    public IEnumerable<AssetGroup> AssetGroups
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

    public string SearchText { get; set; }

    public int ProgramId { get; set; }

    public int ProductId { get; set; }

    public int TestId { get; set; }

    public int Bundle { get; set; }

    public int Type { get; set; }

    public int ProgramOfStudyId
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public string ProgramOfStudyName
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

    public void PopulateAssetGroup(IEnumerable<AssetGroup> assetGroup)
    {
        throw new NotImplementedException();
    }

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.List);
    }

    #region IProgramView Members

    public void ShowProgramResults(IEnumerable<Program> programs, SortInfo sortMetaData)
    {
        gridPrograms.DataSource = KTPSort.Sort<Program>(programs, sortMetaData);
        gridPrograms.DataBind();

        if (programs.Count() == 0)
        {
            lblM.Visible = true;
        }
        else
        {
            lblM.Visible = false;
        }
    }

    public void PopulateProducts(IEnumerable<Product> products)
    {
        throw new NotImplementedException();
    }

    public void RefreshPage(Program program, UserAction action, string title, string subTitle,
        bool hasDeletePermission, bool hasAddPermission)
    {
        throw new NotImplementedException();
    }

    public void ShowBulkProgramResults(IEnumerable<Program> programs, string selectedProgramIds, SortInfo sortMetaData)
    {
        throw new NotImplementedException();
    }

    public void PopulateProgramOfStudies(IEnumerable<ProgramofStudy> programOfStudies)
    {
        ControlHelper.PopulateProgramOfStudy(ddlProgramOfStudy, programOfStudies);
    }
    #endregion

    #region Not Used

    public void PopulateTests(IEnumerable<Test> tests)
    {
        throw new NotImplementedException();
    }

    public void PopulateAssignedTest(IEnumerable<ProgramTestDates> tests)
    {
        throw new NotImplementedException();
    }

    public void PopulateAssets(IEnumerable<Asset> assets)
    {
        throw new NotImplementedException();
    }

    public void ShowMessage(string errorMsg)
    {
        throw new NotImplementedException();
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Presenter.PopulateProgramOfStudies();
            SearchPrograms();
        }
    }

    protected void gridPrograms_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!e.CommandName.Equals("Sort") && !e.CommandName.Equals("Page"))
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gridPrograms.Rows[index];
            string id = Server.HtmlDecode(row.Cells[0].Text);
            switch (e.CommandName)
            {
                case "Select":
                    Presenter.NavigateToEdit(id, UserAction.Edit);
                    break;
                case "Tests":
                    Presenter.NavigateToAssignTests(id);
                    break;
                case "Copy":
                    Presenter.NavigateToEdit(id, UserAction.Copy);
                    break;
            }
        }
    }

    protected void gridPrograms_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        /*
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gv = (GridView)sender;
            int rowIndex = Convert.ToInt32(e.Row.RowIndex);

            if (EditProgram == 0)
            {
                e.Row.Cells[3].Text = "View";
                e.Row.Cells[4].Text = "View Tests";
            }
        }
         */
    }

    protected void gridPrograms_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);
        SearchPrograms();
    }

    protected void gridPrograms_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridPrograms.PageIndex = e.NewPageIndex;
        SearchPrograms();
    }

    protected void seabtn_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.SearchPrograms(ddlProgramOfStudy.SelectedValue.ToInt(), txtsearch.Text, hdnGridConfig.Value);
    }

    protected void ddlProgramOfStudy_SelectedIndexChanged(object sender, EventArgs e)
    {
        Presenter.SearchPrograms(ddlProgramOfStudy.SelectedValue.ToInt(), txtsearch.Text, hdnGridConfig.Value);
    }

    private void SearchPrograms()
    {
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Program List Page")
            .Add("Search Keyword", txtsearch.Text)
            .Write();
        #endregion
        Presenter.SearchPrograms(ddlProgramOfStudy.SelectedValue.ToInt(), txtsearch.Text, hdnGridConfig.Value);
    }
}
