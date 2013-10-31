using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class CMS_CustomTest : PageBase<ICustomTestView, CustomTestPresenter>, ICustomTestView
{
    public int PageIndex
    {
        get
        {
            return ViewState["PageIndex"].ToInt();
        }

        set
        {
            this.ViewState["PageIndex"] = value;
        }
    }

    public string SearchCondition
    {
        get
        {
            return Convert.ToString(ViewState["SearchCondition"]);
        }

        set
        {
            this.ViewState["SearchCondition"] = value;
        }
    }

    public string NewValue { get; set; }

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

    public void RenderCustomTest(IEnumerable<ProgramofStudy> ProgramofStudies, IEnumerable<Product> products, Test test)
    {
        ddlProgramOfStudy.DataSource = ProgramofStudies;
        ddlProgramOfStudy.DataTextField = "ProgramofStudyName";
        ddlProgramOfStudy.DataValueField = "ProgramofStudyId";
        ddlProgramOfStudy.DataBind();
    }

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.List);
    }
    #region ICustomTestView Methods

    public void SearchCustomTest()
    {
        if (rfvProgramOfStudy.IsValid)
        {
            #region Trace Information
            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Custom test search")
                .Add("SearchCondition ", SearchCondition)
                .Write();
            #endregion
            int programOfStudyId = ddlProgramOfStudy.SelectedValue.ToInt();
            Presenter.SearchCustomTests(programOfStudyId, SearchCondition, hdnGridConfig.Value);
        }
        else
        {
            gvCustomTest.DataSource = null;
            gvCustomTest.DataBind();
            gvCustomTest.Visible = false;
        }
    }

    public void DisplaySearchResult(IEnumerable<Test> tests, SortInfo sortMetaData)
    {
        IEnumerable<Test> testList = KTPSort.Sort<Test>(tests, sortMetaData);
        gvCustomTest.DataSource = testList;
        gvCustomTest.DataBind();
        gvCustomTest.Visible = true;
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Global.IsProductionApp)
        {
            btnNewTest.Visible = false;
        }

        if (!this.Page.IsPostBack)
        {
            #region Trace Information
            TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Custom Test List Page");
            #endregion

            Presenter.InitializeCustomTestParams();
        }
    }

    protected void btnNewTest_Click(object sender, EventArgs e)
    {
        Presenter.NavigateToNewCustomTest();
    }

    protected void btnTestCategories_Click(object sender, EventArgs e)
    {
        Presenter.NavigateToTestCategory();
    }

    protected void gvCustomTest_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            System.Web.UI.WebControls.WebControl l = (WebControl)e.Row.Cells[8].Controls[0];
            l.Attributes.Add("onclick", Global.IsProductionApp ? "return false;" : "return confirm('Are you sure that you want to delete this Test? Click OK to remove the test from all programs that contain it.')");

            if (e.Row.Cells[3].Text == "1" || e.Row.Cells[3].Text == "2")
            {
                e.Row.Cells[3].Text = "Y";
            }

            if (Global.IsProductionApp)
            {
                e.Row.Cells[6].Enabled = false;
                e.Row.Cells[7].Enabled = false;
                e.Row.Cells[8].Enabled = false;
            }
        }
    }

    protected void gvCustomTest_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCustomTest.PageIndex = e.NewPageIndex;
        SearchCustomTest();
    }

    protected void gvCustomTest_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int testId = (int)gvCustomTest.DataKeys[e.RowIndex].Value;
        Presenter.DeleteCustomeTest(testId);
        SearchCustomTest();
    }

    protected void gvCustomTest_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);
        SearchCustomTest();
    }

    protected void ibtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchCondition = TextBox1.Text;
        SearchCustomTest();
    }

    protected void ddlProgramOfStudy_SelectedIndexChanged(object sender, EventArgs e)
    {
        rfvProgramOfStudy.Validate();
        ibtnSearch_Click(null, null);
    }
}