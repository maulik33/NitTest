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
using NursingRNWeb;

public partial class CMS_SearchQuestion : PageBase<IQuestionView, QuestionPresenter>, IQuestionView
{
    private int NumberOfQuestions;
    private int _pageIndex = 1;

    public string URLQuery { get; set; }

    public string TestId { get; set; }

    public string VType { get; set; }

    public int ProgramOfStudyId { get; set; }

    
    // initialize search selections for URL query_Min
    public string urlQuery
    {
        get
        {
            string _urlQuery = string.Empty;
            if (ViewState["urlQuery"] == null && Request.QueryString["urlQuery"] == null)
            {
                _urlQuery = "0,0,0,0,0,0,0,0,0,0,0,0,0,,,0,0,0";
            }
            else
            {
                if (Convert.ToString(ViewState["urlQuery"]).Length > 0)
                {
                    _urlQuery = Convert.ToString(ViewState["urlQuery"]);
                }
                else
                {
                    _urlQuery = Request.QueryString["urlQuery"].ToString();
                }
            }

            return _urlQuery;
        }

        set
        {
            ViewState["urlQuery"] = value;
        }
    }

    public void ShowSearchRemediationResults(IEnumerable<Remediation> searchRemediationResults)
    {
        BindRemediationData(searchRemediationResults);
    }

    public void ShowSearchQuestionResults(IEnumerable<QuestionResult> searchQuestionResults, SortInfo sortMetaData)
    {
        BindData(searchQuestionResults, sortMetaData);
    }

    public void PopulateSearchQuestionCriteria(IEnumerable<Product> products, IEnumerable<Topic> titles, IDictionary<CategoryName, Category> categories, int programofStudy)
    {
        ucSearchQuestionCriteria.PopulateSearchCriteria(products, titles, categories);
    }

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.List);
    }

    #region IQuestionView Methods

    public void PopulateTests(IEnumerable<Test> tests)
    {
        ucSearchQuestionCriteria.PopulateTests(tests);
    }

    public void PopulateClientNeedsCategory(IDictionary<int, CategoryDetail> clientNeedsCategories)
    {
        ucSearchQuestionCriteria.PopulateClientNeedsCategory(clientNeedsCategories);
    }

    public void PopulateAlternateTextDetails(Question question, UserAction actionType)
    {
    }

    public void ShowErrorMessage(string errorMsg)
    {
    }
    #endregion

    public void PopulateInitialQuestionParameters(IEnumerable<Topic> titles, IEnumerable<ProgramofStudy> programofStudy)
    {
        ucSearchQuestionCriteria.PopulateInitialQuestionParameters(titles, programofStudy);
    }

    public void DisplayUploadedQuestions(IEnumerable<UploadQuestionDetails> uploadedQuestions, int FileType, string UnZippedFolderPath)
    {
        throw new NotImplementedException();
    }

    public void PopulateQuestion(Question question, int mode)
    {
        throw new NotImplementedException();
    }

    public void PopulateClientNeedCategories(IDictionary<int, CategoryDetail> categories)
    {
        ucSearchQuestionCriteria.PopulateClientNeedsCategory(categories);
    }

    public void RefreshPage(Question question, UserAction action, Dictionary<string, string> fileType,
        Dictionary<string, string> questionType, string title, string subTitle, bool hasDeletePermission, bool hasAddPermission)
    {
        throw new NotImplementedException();
    }

    public void PopulateQuestionType(Dictionary<string, string> questionType)
    {
        throw new NotImplementedException();
    }

    public void PopulateFileType(Dictionary<string, string> fileType)
    {
        throw new NotImplementedException();
    }

    public void PopulateAnswers(IEnumerable<AnswerChoice> answers)
    {
        throw new NotImplementedException();
    }

    public void PopulateTestQuestion(Question question)
    {
        throw new NotImplementedException();
    }

    public void ShowErrorMessage()
    {
        throw new NotImplementedException();
    }

    public void PopulateProgramOfStudy(IEnumerable<ProgramofStudy> programOfStudies)
    {
        throw new NotImplementedException();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        gvRem.Visible = false;
        gvQuestions.Visible = false;

        ucSearchQuestionCriteria.onSearchQuestionClick += new EventHandler(uc_SearchQuestionClick);
        ucSearchQuestionCriteria.onSearchRemediationClick += new EventHandler(uc_SearchRemediationClick);
        ucSearchQuestionCriteria.onAddEditQuestionClick += new EventHandler(uc_onAddEditQuestionClick);
        ucSearchQuestionCriteria.SubCategoryControl.OnClientNeedsChange += new EventHandler<ItemSelectedEventArgs>(ucSearchQuestionCriteria_OnClientNeedsChange);
        ucSearchQuestionCriteria.OnProductSelectionChange += new EventHandler<ItemSelectedEventArgs>(ucSearchQuestionCriteria_OnProductSelectionChange);
        ucSearchQuestionCriteria.OnbtnAddRClick += new EventHandler<EventArgs>(ucSearchQuestionCriteria_OnbtnAddRClick);
        ucSearchQuestionCriteria.OnbtnCategoryClick += new EventHandler<EventArgs>(ucSearchQuestionCriteria_OnbtnCategoryClick);
        ucSearchQuestionCriteria.OnbtnLippincotClick += new EventHandler<EventArgs>(ucSearchQuestionCriteria_OnbtnLippincotClick);
        ucSearchQuestionCriteria.OnPopulationTypeSelectedIndexChange += new EventHandler<ItemSelectedEventArgs>(ucSearchQuestionCriteria_OnPopulationTypeSelectedIndexChange);
        if (!IsPostBack)
        {
            Presenter.InitializeQuestionParameter();
        }
        else
        {
            lblNumberQ.Text = string.Empty;
        }
    }

    protected void BindData(IEnumerable<QuestionResult> searchQuestionResults, SortInfo sortMetaData)
    {
        lblNumberQ.Text = searchQuestionResults.Count().ToString();
        gvRem.Visible = false;
        gvQuestions.Visible = true;
        gvQuestions.DataSource = null;
        gvQuestions.DataSource = KTPSort.Sort<QuestionResult>(searchQuestionResults, sortMetaData);
        gvQuestions.DataBind();
    }

    protected void BindRemediationData(IEnumerable<Remediation> searchRemediationResults)
    {
        gvRem.Visible = true;
        gvQuestions.Visible = false;
        gvRem.DataSource = null;
        gvRem.DataSource = searchRemediationResults;
        gvRem.DataBind();
    }

    protected void gvrem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            NumberOfQuestions = NumberOfQuestions + 1;
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            lblNumberQ.Text = NumberOfQuestions.ToString();
        }
    }

    protected void gvQuestions_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!e.CommandName.Equals("Sort") && !e.CommandName.Equals("Page"))
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvQuestions.Rows[index];

            var _questionId = gvQuestions.DataKeys[row.RowIndex].Values["QID"].ToString();

            if (e.CommandName == "ViewQuestion")
            {
                Presenter.NavigateToViewQuestion(urlQuery, _questionId);
            }
            else if (e.CommandName == "EditQuestion")
            {
                Presenter.NavigateToEdit(_questionId, urlQuery, UserAction.Edit);
            }
            else if (e.CommandName == "CopyQuestion")
            {
                Presenter.NavigateToEdit(_questionId, urlQuery, UserAction.Copy);
            }
        }
    }

    protected void gvQuestions_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && Global.IsProductionApp)
        {
            e.Row.Cells[9].Enabled = false;
        }
    }

    protected void gvRem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = gvRem.Rows[index];
        int id;
        if (e.CommandName == "EditR")
        {
            id = Convert.ToInt32(gvRem.DataKeys[row.RowIndex].Values["RemediationID"].ToString());
            Presenter.NavigateToEditR(id, UserAction.Edit);
        }
    }

    protected void gvQuestions_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);
        QuestionCriteria searchCriteria1 = ucSearchQuestionCriteria.GetSelectedValues();
        this.urlQuery = ucSearchQuestionCriteria.GetUrlQuery();
        Presenter.SearchQuestions(searchCriteria1, hdnGridConfig.Value);
    }

    #region paging
    protected void gvQuestions_PageIndexChanged(Object sender, EventArgs e)
    {
        QuestionCriteria searchCriteria1 = ucSearchQuestionCriteria.GetSelectedValues();
        this.urlQuery = ucSearchQuestionCriteria.GetUrlQuery();
        Presenter.SearchQuestions(searchCriteria1, hdnGridConfig.Value);
        gvQuestions.PageIndex = _pageIndex;
        gvQuestions.Visible = true;
    }

    protected void gvQuestions_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        _pageIndex = e.NewPageIndex;
        gvQuestions.PageIndex = e.NewPageIndex;
    }
    #endregion

    private void ucSearchQuestionCriteria_OnbtnLippincotClick(object sender, EventArgs e)
    {
        Presenter.NavigateToLippincott();
    }

    private void ucSearchQuestionCriteria_OnbtnCategoryClick(object sender, EventArgs e)
    {
        Presenter.NavigateToCategory();
    }

    private void ucSearchQuestionCriteria_OnbtnAddRClick(object sender, EventArgs e)
    {
        Presenter.NavigateToEditR(0, UserAction.Add);
    }

    private void ucSearchQuestionCriteria_OnProductSelectionChange(object sender, ItemSelectedEventArgs e)
    {
        ProgramOfStudyId = ucSearchQuestionCriteria.ProgramofStudyId;
        Presenter.GetTests(e.SelectedValue.ToInt());
    }

    private void ucSearchQuestionCriteria_OnClientNeedsChange(object sender, ItemSelectedEventArgs e)
    {
        Presenter.GetClientNeedsCategory(e.SelectedValue.ToInt(), e.ProgramofStudyId);
    }

    private void uc_onAddEditQuestionClick(object sender, EventArgs e)
    {
        Presenter.NavigateToEdit("0", urlQuery, UserAction.Add);
    }

    private void uc_SearchQuestionClick(object sender, EventArgs e)
    {
        QuestionCriteria searchCriteria1 = ((SearchQuestionEventArgs)e).SearchCriteria;
        this.urlQuery = ((SearchQuestionEventArgs)e).UrlQuery;
        Presenter.SearchQuestions(searchCriteria1, hdnGridConfig.Value);
    }

    private void uc_SearchRemediationClick(object sender, EventArgs e)
    {
        QuestionCriteria searchCriteria1 = ((SearchQuestionEventArgs)e).SearchCriteria;
        this.urlQuery = ((SearchQuestionEventArgs)e).UrlQuery;
        Presenter.SearchRemediations(searchCriteria1);
    }

    private void ucSearchQuestionCriteria_OnPopulationTypeSelectedIndexChange(object sender, ItemSelectedEventArgs e)
    {
        Presenter.GetCategories(e.SelectedValue.ToInt());
    }
}
