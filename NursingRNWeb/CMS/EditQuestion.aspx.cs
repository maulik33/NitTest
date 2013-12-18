using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;
using NursingRNWeb;

public partial class CMS_EditQuestion : PageBase<IQuestionView, QuestionPresenter>, IQuestionView
{
    private const string DEFAULT_DROPDOWN_VALUE = "0";

    private List<AnswerChoice> _answers;

    public string URLQuery { get; set; }

    public string TestId { get; set; }

    public string VType { get; set; }

    public int ProgramOfStudyId { get; set; }

    #region IQuestionView Methods

    public void PopulateTests(IEnumerable<UserTest> tests)
    {
    }

    public void DisplayUploadedQuestions(IEnumerable<UploadQuestionDetails> uploadedQuestions, int FileType, string UnZippedFolderPath)
    {
        throw new NotImplementedException();
    }

    public void ShowSearchQuestionResults(IEnumerable<QuestionResult> searchQuestionResults, SortInfo sortMetaData)
    {
        throw new NotImplementedException();
    }

    public void ShowSearchRemediationResults(IEnumerable<Remediation> searchRemediationResults)
    {
        throw new NotImplementedException();
    }

    public void PopulateTests(IEnumerable<Test> tests)
    {
        gvWhere.DataSource = tests;
        gvWhere.DataBind();
    }

    public void RefreshPage(Question question, UserAction action, Dictionary<string, string> fileType, Dictionary<string, string> questionType, string mode, string testId, bool hasDeletePermission, bool hasAddPermission)
    {
        hdnURL.Value = URLQuery;
        ddTypeOfFile.DataSource = fileType;
        ddTypeOfFile.DataTextField = "Value";
        ddTypeOfFile.DataValueField = "Key";
        ddTypeOfFile.DataBind();

        ddQuestionType.DataSource = questionType;
        ddQuestionType.DataTextField = "Value";
        ddQuestionType.DataValueField = "Key";
        ddQuestionType.DataBind();

        if (action == UserAction.Edit)
        {
            btnRemediation.Visible = true;
            lblWhere.Visible = true;
            btnAssign.Visible = true;
            btnView.Visible = true;
            btnDelete.Visible = true;
            btnDelete.Attributes.Add("onclick", " return confirm('Are you sure you want to make this question inactive?')");
            ucSubCategories.SetControlVisibility(ddProgramofStudy.SelectedValue);
        }
        else if (action == UserAction.Add || action == UserAction.Copy)
        {
            ////ddClientNeedsCategory.ClearData();
            btnDelete.Visible = false;
            btnView.Visible = false;
            btnAssign.Visible = false;
            lblWhere.Visible = false;
            btnRemediation.Visible = false;
            ucSubCategories.SetControlVisibility(ddProgramofStudy.SelectedValue);
        }
    }

    public void PopulateAnswers(IEnumerable<AnswerChoice> answers)
    {
        _answers = answers.ToList();
    }

    public void PopulateInitialQuestionParameters(IEnumerable<Topic> titles, IEnumerable<ProgramofStudy> programofStudy)
    {
        ControlHelper.PopulateProgramOfStudy(ddProgramofStudy, programofStudy);
        ControlHelper.PopulateTopicTitle(ddTopicTitle, titles);
    }

    public void PopulateSearchQuestionCriteria(IEnumerable<Product> products, IEnumerable<Topic> titles, IDictionary<CategoryName, Category> categoryData, int programofStudy)
    {
        var selectedvalue = ddTopicTitle.SelectedValue;
       
        ControlHelper.PopulateTopicTitle(ddTopicTitle, titles);
        ucSubCategories.PopulateSubCategories(categoryData, programofStudy);
        if (Presenter.ActionType == UserAction.Copy && selectedvalue.ToInt() > 0)
        {
            ddTopicTitle.SelectedValue = selectedvalue;
        }
    }

    public void PopulateClientNeedCategories(IDictionary<int, CategoryDetail> categories)
    {
        ucSubCategories.PopulateClientNeedCategories(categories);
    }

    public void PopulateAlternateTextDetails(Question question, UserAction actionType)
    {
        ucAltEditQuestion.PopulateAlternateTextDetails(question, actionType, _answers);
    }
    #endregion

    #region Abstract Method
    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.Edit);
    }
    #endregion

    public void ShowErrorMessage(string errorMsg)
    {
        lblError.Text = errorMsg;
    }

    public void PopulateQuestion(Question question, int mode)
    {
        if (question != null)
        {
            Presenter.GetCategories(question.ProgramofStudyId);
            if (question.QuestionType.Trim().ToInt() == (int)QuestionType.MultiChoiceSingleAnswer)
            {
                FillMultipleChoiceFields();
                AssignValues(question);
            }
            else if (question.QuestionType.ToString().Trim().ToInt() == (int)QuestionType.Number)
            {
                FillBlankFields();
            }
            else if (question.QuestionType.ToString().Trim().ToInt() == (int)QuestionType.Order)
            {
                FillMatchFields();
            }
            else if (question.QuestionType.ToString().Trim().ToInt() == (int)QuestionType.MultiChoiceMultiAnswer)
            {
                FillMultipleChoiceMultiSelectFields();

                AssignValues(question);
            }
            else if (question.QuestionType.ToString().Trim().ToInt() == (int)QuestionType.Hotspot)
            {
                FillHotSpotFields();
            }
            else if (question.QuestionType.ToString().Trim().ToInt() == (int)QuestionType.Item)
            {
                FillItem();
            }

            if (Presenter.ActionType == UserAction.Edit)
            {
                txtQID.Text = question.Id.ToString();
                txtQuestionID.Text = question.QuestionId.Trim();
                txtNorming.Text = question.Q_Norming > 0 ? question.Q_Norming.ToString() : string.Empty;
                ddProgramofStudy.Visible = false;
                lblProgramofStudyVal.Text = question.ProgramofStudyName;
                lblProgramofStudyVal.Visible = true;
                Presenter.ShowTests(question.ProgramofStudyId);
            }
            else if (Presenter.ActionType == UserAction.Copy)
            {
                ddProgramofStudy.Visible = true;
                lblProgramofStudyVal.Visible = false;
                lblCopyDetail.Text = "Copying Question '" + question.QuestionId + "' to a new question.";
                trCopy.Visible = true;
            }

            txtStimulus.Text = question.Stimulus.Trim();
            txtStem.Text = question.Stem.Trim();
            txtListeningFileURL.Text = question.ListeningFileUrl.Trim();
            txtExplanation.Text = question.Explanation.Trim();
            txtProductLine.Text = question.ProductLineId.Trim();
            txtPointB.Text = question.PointBiserialsId.Trim();
            txtStatistics.Text = question.Statisctics.Trim();
            txtCreator.Text = question.CreatorId.Trim();
            txtDCreated.Text = question.DateCreated.Trim();
            txtEditor.Text = question.EditorId.Trim();
            txtDEdited.Text = question.DateEdited.Trim();
            txt2Editor.Text = question.EditorId_2.Trim();
            txtD2Edit.Text = question.DateEdited_2.Trim();
            txtSBD.Text = question.Source_SBD.Trim();
            txtFeedback.Text = question.Feedback.Trim();
            txtWho.Text = question.WhoOwns.Trim();
            txtItemTitle.Text = question.ItemTitle.Trim();
            if (question.TypeOfFileId != "-1")
            {
                ddTypeOfFile.SelectedValue = question.TypeOfFileId.Trim();
            }

            ddQuestionType.SelectedValue = question.QuestionType.Trim();
            hdnQuestionType.Value = question.QuestionType.Trim();
            if (String.IsNullOrEmpty(question.Active.ToString()))
            {
                rdoActive.SelectedValue = "1";
            }
            else
            {
                rdoActive.SelectedValue = question.Active.ToString();
            }

            ControlHelper.SetSelectedValue(ddTopicTitle, question.RemediationId.ToString(), DEFAULT_DROPDOWN_VALUE);
            ucSubCategories.PopulateSubCategoryValues(question);
            ddProgramofStudy.SelectedValue = question.ProgramofStudyId.ToString();
            Presenter.GetClientNeedsCategory(ucSubCategories.ClientNeedsValue.ToInt(), question.ProgramofStudyId);
            D_Remediation.InnerHtml = question.Remediation.Trim();
        }
    }

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        ucSubCategories.OnClientNeedsChange += new EventHandler<ItemSelectedEventArgs>(ucSubCategories_OnClientNeedsChange);

        hdnQuestionId.Value = Presenter.Id.ToString();
        Presenter.GetAnswer();
        if (Presenter.ActionType == UserAction.Edit || Presenter.ActionType == UserAction.Copy)
        {
            Presenter.PopulateAlternateTextDetails(Presenter.ActionType);
        }

        if (Global.IsProductionApp)
        {
            btnUploadQues.Visible = false;
        }

        if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to Edit Question Page")
                                .Add("Question Id", Presenter.Id.ToString())
                                .Write();
            #endregion
            Presenter.InitializeQuestionParameter();
            ucSubCategories.PopulateSubategoryDefaultValue();
            Presenter.ShowQuestionDetails();
            hdnURL.Value = URLQuery;
        }
        else
        {
            var _questionType = hdnQuestionType.Value;

            if (_questionType != null || _questionType.Length != 0)
            {
                if (Presenter.ActionType == UserAction.Add)
                {
                    Question question = new Question();
                    question.QuestionType = _questionType;
                    PopulateAlternateTextDetails(question, Presenter.ActionType);
                }

                if (Enum.IsDefined(typeof(QuestionType), _questionType.ToInt()))
                {
                    var _qType = (QuestionType)_questionType.ToInt();
                    switch (_qType)
                    {
                        case QuestionType.MultiChoiceSingleAnswer:
                            FillMultipleChoiceFields();
                            FillExhibit();
                            break;
                        case QuestionType.MultiChoiceMultiAnswer:
                            FillMultipleChoiceMultiSelectFields();
                            FillExhibit();
                            break;
                        case QuestionType.Hotspot:
                            FillHotSpotFields();
                            break;
                        case QuestionType.Number:
                            FillBlankFields();
                            break;
                        case QuestionType.Order:
                            FillMatchFields();
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        URLQuery = hdnURL.Value;
        Presenter.NavigateToSearch(URLQuery);
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (ValidateProgramOfStudy())
        {
            if (ddQuestionType.SelectedValue == "-1")
            {
                lblError.Text = "Please select 'QuestionType'.";
                return;
            }

            URLQuery = hdnURL.Value;
            SaveQuestion();
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        URLQuery = hdnURL.Value;
        Presenter.NavigateToViewQuestion(URLQuery, hdnQuestionId.Value);
    }

    protected void ddQuestionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblExhibit.Visible = false;
        D_Exhibit.Controls.Clear();
        Question question = new Question();
        question.QuestionType = ddQuestionType.SelectedValue;
        if (Presenter.ActionType == UserAction.Add)
        {
            PopulateAlternateTextDetails(question, Presenter.ActionType);
        }

        if (ddQuestionType.SelectedValue.ToInt() == (int)QuestionType.Number)
        {
            hdnQuestionType.Value = "04";
            FillBlankFields();
        }
        else if (ddQuestionType.SelectedValue.ToInt() == (int)QuestionType.MultiChoiceSingleAnswer)
        {
            hdnQuestionType.Value = "01";
            FillMultipleChoiceFields();
            FillExhibit();
        }
        else if (ddQuestionType.SelectedValue.ToInt() == (int)QuestionType.MultiChoiceMultiAnswer)
        {
            hdnQuestionType.Value = "02";
            FillMultipleChoiceMultiSelectFields();
            FillExhibit();
        }
        else if (ddQuestionType.SelectedValue.ToInt() == (int)QuestionType.Hotspot)
        {
            hdnQuestionType.Value = "03";
            FillHotSpotFields();
        }
        else if (ddQuestionType.SelectedValue.ToInt() == (int)QuestionType.Order)
        {
            hdnQuestionType.Value = "05";
            FillMatchFields();
        }
        else if (ddQuestionType.SelectedValue == "00")
        {
            FillItem();
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Presenter.DeleteQuestion(hdnQuestionId.Value.ToInt(), hdnURL.Value.ToString());
    }

    protected void gvWhere_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox ch = (CheckBox)e.Row.FindControl("chkSelection");
            TextBox tb = (TextBox)e.Row.FindControl("txtOrder");
            var _questionNumber = ((HiddenField)e.Row.FindControl("TestNumber")).Value.ToInt();
            if (ch != null && tb != null)
            {
                int i = e.Row.DataItemIndex;
                if (_questionNumber > 0)
                {
                    ch.Checked = true;
                    tb.Text = _questionNumber.ToString();
                }
                else
                {
                    ch.Checked = false;
                    tb.Text = string.Empty;
                }
            }
        }
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        List<Test> lstTest = new List<Test>();
        foreach (GridViewRow row in gvWhere.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox ch = (CheckBox)row.FindControl("chkSelection");
                TextBox tb = (TextBox)row.FindControl("txtOrder");
                if (ch != null && tb != null)
                {
                    var objTest = new Test();
                    objTest.Question = new Question()
                    {
                        Id = hdnQuestionId.Value.ToInt(),
                        Active = ch.Checked ? 1 : 0,
                        QuestionNumber = tb.Text.ToInt()
                    };
                    objTest.TestId = gvWhere.DataKeys[row.DataItemIndex].Values["TestID"].ToString().ToInt();
                    lstTest.Add(objTest);
                }
            }
        }

        if (lstTest.Count > 0)
        {
            Presenter.AssignQuestion(lstTest);
        }
    }

    protected void btnRemediation_Click(object sender, EventArgs e)
    {
        URLQuery = String.IsNullOrEmpty(hdnURL.Value) ? string.Empty : hdnURL.Value;
        Presenter.NavigateToRemediation(URLQuery, hdnQuestionId.Value);
    }

    protected void btnUploadQues_Click(object sender, EventArgs e)
    {
        Presenter.NavigateToUploadQuestions();
    }

    protected void ddProgramofStudy_SelectedIndexChanged(object sender, EventArgs e)
    {
        Presenter.GetCategories(ddProgramofStudy.SelectedValue.ToInt());
        ucSubCategories.SetControlVisibility(ddProgramofStudy.SelectedValue);
    }
    #endregion

    #region Private Methods
    private string ReturnLetter(int val)
    {
        var _val = string.Empty;
        switch (val)
        {
            case 1:
                _val = "A";
                break;
            case 2:
                _val = "B";
                break;
            case 3:
                _val = "C";
                break;
            case 4:
                _val = "D";
                break;
            case 5:
                _val = "E";
                break;
            case 6:
                _val = "F";
                break;
            default:
                _val = string.Empty;
                break;
        }

        return _val;
    }

    private string ReturnValue(string val)
    {
        var _val = string.Empty;
        switch (val)
        {
            case "A":
                _val = "1";
                break;
            case "B":
                _val = "2";
                break;
            case "C":
                _val = "3";
                break;
            case "D":
                _val = "4";
                break;
            case "E":
                _val = "5";
                break;
            case "F":
                _val = "6";
                break;
            default:
                _val = string.Empty;
                break;
        }

        return _val;
    }

    private void AssignValues(Question question)
    {
        FillExhibit();
        for (int i = 1; i < 4; ++i)
        {
            TextBox tb = (TextBox)D_Exhibit.FindControl("TBE_" + i);
            string fieldName = "ExhibitTab" + i;
            tb.Text = GetValue(fieldName.ToString(), question);

            tb = (TextBox)D_Exhibit.FindControl("TBT_" + i);
            fieldName = "ExhibitTitle" + i;
            tb.Text = GetValue(fieldName.ToString(), question);
        }
    }

    private void FillItem()
    {
        hdnQuestionType.Value = "00";
        D_Answers.Controls.Clear();
    }

    private void FillExhibit()
    {
        lblExhibit.Visible = true;
        D_Exhibit.Controls.Clear();
        for (int i = 1; i < 4; ++i)
        {
            TextBox TB_E = new TextBox();
            TextBox TB_T = new TextBox();
            TB_E.Width = Unit.Pixel(400);
            TB_T.Width = Unit.Pixel(150);
            TB_E.TextMode = TextBoxMode.MultiLine;
            TB_E.Rows = 3;
            TB_E.ID = "TBE_" + Convert.ToString(i);
            TB_T.ID = "TBT_" + Convert.ToString(i);
            TB_E.Text = string.Empty;
            D_Exhibit.Controls.Add(new LiteralControl("Tab" + i + ": "));
            D_Exhibit.Controls.Add(TB_T);
            D_Exhibit.Controls.Add(new LiteralControl("<br/>"));
            D_Exhibit.Controls.Add(TB_E);
            D_Exhibit.Controls.Add(new LiteralControl("<br />"));
        }
    }

    private void FillMultipleChoiceFields()
    {
        hdnQuestionType.Value = "01";
        D_Answers.Controls.Clear();
        if (Presenter.ActionType == UserAction.Add)
        {
            for (int i = 1; i < 7; i++)
            {
                RadioButton RB_i = new RadioButton();
                RB_i.GroupName = "RB";
                RB_i.ID = "RB_" + Convert.ToString(i);
                RB_i.Text = i.ToString();
                TextBox TB_i = new TextBox();
                TB_i.Width = Unit.Pixel(400);
                TB_i.TextMode = TextBoxMode.MultiLine;
                TB_i.Rows = 3;
                TB_i.ID = "TB_" + Convert.ToString(i);
                TB_i.Text = string.Empty;

                D_Answers.Controls.Add(RB_i);
                D_Answers.Controls.Add(new LiteralControl("<br />"));
                D_Answers.Controls.Add(TB_i);
                D_Answers.Controls.Add(new LiteralControl("<br />"));
            }
        }
        else if (Presenter.ActionType == UserAction.Edit || Presenter.ActionType == UserAction.Copy)
        {
            var count = _answers.Count;
            int i = 0;
            D_Answers.Controls.Clear();

            foreach (var item in _answers)
            {
                i++;
                RadioButton RB_i = new RadioButton();
                RB_i.ID = "RB_" + Convert.ToString(i);
                TextBox TB_i = new TextBox();
                TB_i.Width = Unit.Pixel(400);
                TB_i.TextMode = TextBoxMode.MultiLine;
                TB_i.Rows = 3;
                TB_i.ID = "TB_" + Convert.ToString(i);
                var AText = item.Atext.ToString();

                RB_i.Text = i.ToString();
                RB_i.GroupName = "RB";
                TB_i.Text = AText;

                D_Answers.Controls.Add(RB_i);
                D_Answers.Controls.Add(new LiteralControl("<br />"));
                D_Answers.Controls.Add(TB_i);
                D_Answers.Controls.Add(new LiteralControl("<br />"));

                if (item.Correct.ToInt() == 1)
                {
                    RB_i.BackColor = Color.FromArgb(228, 240, 216);
                    RB_i.Checked = true;
                }
            }

            if (i < 7)
            {
                for (int j = i + 1; j < 7; j++)
                {
                    RadioButton RB_i = new RadioButton();
                    RB_i.ID = "RB_" + Convert.ToString(j);
                    RB_i.Text = j.ToString();
                    TextBox TB_i = new TextBox();
                    TB_i.Width = Unit.Pixel(400);
                    TB_i.TextMode = TextBoxMode.MultiLine;
                    TB_i.Rows = 3;
                    TB_i.ID = "TB_" + Convert.ToString(j);
                    TB_i.Text = string.Empty;

                    D_Answers.Controls.Add(RB_i);
                    D_Answers.Controls.Add(new LiteralControl("<br />"));
                    D_Answers.Controls.Add(TB_i);
                    D_Answers.Controls.Add(new LiteralControl("<br />"));
                }
            }
        }
    }

    private void FillBlankFields()
    {
        hdnQuestionType.Value = "04";
        D_Answers.Controls.Clear();
        if (Presenter.ActionType == UserAction.Add)
        {
            TextBox tx = new TextBox();
            tx.ID = "tx";
            tx.Text = string.Empty;
            D_Answers.Controls.Add(tx);
            D_Answers.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
            D_Answers.Controls.Add(new LiteralControl("Unit:"));
            D_Answers.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
            TextBox m_tx = new TextBox();
            m_tx.ID = "m_tx";
            m_tx.Text = string.Empty;
            D_Answers.Controls.Add(m_tx);
        }
        else if (Presenter.ActionType == UserAction.Edit || Presenter.ActionType == UserAction.Copy)
        {
            if (_answers.Count > 0)
            {
                var item = _answers.FirstOrDefault();
                var AText = item.Atext.ToString();
                AText = AText.Replace("<P>", string.Empty);
                AText = AText.Replace("</P>", string.Empty);

                TextBox tx = new TextBox();
                tx.Width = Unit.Pixel(400);
                if (!AText.Trim().Equals(string.Empty))
                {
                    tx.Text = AText.Trim();
                }

                tx.ID = "tx";
                D_Answers.Controls.Add(tx);
                D_Answers.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
                D_Answers.Controls.Add(new LiteralControl("Unit:"));
                D_Answers.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));

                TextBox m_tx = new TextBox();
                m_tx.ID = "m_tx";
                m_tx.Text = item.Unit.ToString();
                D_Answers.Controls.Add(m_tx);
            }
            else
            {
                TextBox tx = new TextBox();
                tx.ID = "tx";
                tx.Text = string.Empty;
                D_Answers.Controls.Add(tx);
                D_Answers.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
                D_Answers.Controls.Add(new LiteralControl("Unit:"));
                D_Answers.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
                TextBox m_tx = new TextBox();
                m_tx.ID = "m_tx";
                m_tx.Text = string.Empty;
                D_Answers.Controls.Add(m_tx);
            }
        }
    }

    private void FillMatchFields()
    {
        hdnQuestionType.Value = "05";
        D_Answers.Controls.Clear();
        if (Presenter.ActionType == UserAction.Add)
        {
            for (int i = 1; i < 7; i++)
            {
                TextBox TB_O = new TextBox();
                TB_O.Width = Unit.Pixel(100);
                TB_O.Enabled = false;
                TB_O.TextMode = TextBoxMode.SingleLine;
                TB_O.Rows = 1;
                TB_O.ID = "TB_O" + Convert.ToString(i);
                TB_O.Text = i.ToString();

                TextBox TB_i = new TextBox();
                TB_i.Width = Unit.Pixel(400);
                TB_i.TextMode = TextBoxMode.MultiLine;
                TB_i.Rows = 3;
                TB_i.ID = "TB_" + Convert.ToString(i);
                TB_i.Text = string.Empty;

                TextBox TB_P = new TextBox();
                TB_P.Width = Unit.Pixel(100);
                TB_P.TextMode = TextBoxMode.SingleLine;
                TB_P.Rows = 1;
                TB_P.ID = "TB_P" + Convert.ToString(i);
                TB_P.Text = string.Empty;

                D_Answers.Controls.Add(TB_O);
                D_Answers.Controls.Add(new LiteralControl("<br />"));
                D_Answers.Controls.Add(TB_i);
                D_Answers.Controls.Add(new LiteralControl("<br />"));
                D_Answers.Controls.Add(TB_P);
                D_Answers.Controls.Add(new LiteralControl("<br />"));
                D_Answers.Controls.Add(new LiteralControl("<br />"));
                D_Answers.Controls.Add(new LiteralControl("<br />"));
            }
        }
        else if (Presenter.ActionType == UserAction.Edit || Presenter.ActionType == UserAction.Copy)
        {
            int i = 0;
            D_Answers.Controls.Clear();
            foreach (var item in _answers)
            {
                i++;
                TextBox TB_O = new TextBox();
                TB_O.Width = Unit.Pixel(100);
                TB_O.Enabled = false;
                TB_O.TextMode = TextBoxMode.SingleLine;
                TB_O.Rows = 1;
                TB_O.ID = "TB_O" + Convert.ToString(i);
                //// TB_O.Text = i.ToString();

                TextBox TB_i = new TextBox();
                TB_i.Width = Unit.Pixel(400);
                TB_i.TextMode = TextBoxMode.MultiLine;
                TB_i.Rows = 3;
                TB_i.ID = "TB_" + Convert.ToString(i);
                var AText = item.Atext.ToString();
                TB_i.Text = AText;

                TextBox TB_P = new TextBox();
                TB_P.Width = Unit.Pixel(100);
                TB_P.TextMode = TextBoxMode.SingleLine;
                TB_P.Rows = 1;
                TB_P.ID = "TB_P" + Convert.ToString(i);
                var initialPosition = item.InitialPosition.ToString();

                TB_P.Text = ReturnValue(item.Aindex);
                TB_O.Text = initialPosition;
                //// i.ToString();

                D_Answers.Controls.Add(TB_O);
                D_Answers.Controls.Add(new LiteralControl("<br />"));
                D_Answers.Controls.Add(TB_i);
                D_Answers.Controls.Add(new LiteralControl("<br />"));
                D_Answers.Controls.Add(TB_P);
                D_Answers.Controls.Add(new LiteralControl("<br />"));
                D_Answers.Controls.Add(new LiteralControl("<br />"));
                D_Answers.Controls.Add(new LiteralControl("<br />"));
            }

            if (i < 7)
            {
                for (int j = i + 1; j < 7; j++)
                {
                    TextBox TB_O = new TextBox();
                    TB_O.Width = Unit.Pixel(100);
                    TB_O.Enabled = false;
                    TB_O.TextMode = TextBoxMode.SingleLine;
                    TB_O.Rows = 1;
                    TB_O.ID = "TB_O" + Convert.ToString(j);
                    TB_O.Text = j.ToString();

                    TextBox TB_i = new TextBox();
                    TB_i.Width = Unit.Pixel(400);
                    TB_i.TextMode = TextBoxMode.MultiLine;
                    TB_i.Rows = 3;
                    TB_i.ID = "TB_" + Convert.ToString(j);
                    TB_i.Text = string.Empty;

                    TextBox TB_P = new TextBox();
                    TB_P.Width = Unit.Pixel(100);
                    TB_P.TextMode = TextBoxMode.SingleLine;
                    TB_P.Rows = 1;
                    TB_P.ID = "TB_P" + Convert.ToString(j);
                    TB_P.Text = string.Empty;

                    D_Answers.Controls.Add(TB_O);
                    D_Answers.Controls.Add(new LiteralControl("<br />"));
                    D_Answers.Controls.Add(TB_i);
                    D_Answers.Controls.Add(new LiteralControl("<br />"));
                    D_Answers.Controls.Add(TB_P);
                    D_Answers.Controls.Add(new LiteralControl("<br />"));
                    D_Answers.Controls.Add(new LiteralControl("<br />"));
                    D_Answers.Controls.Add(new LiteralControl("<br />"));
                }
            }
        }
    }

    private void FillMultipleChoiceMultiSelectFields()
    {
        hdnQuestionType.Value = "02";
        D_Answers.Controls.Clear();
        if (Presenter.ActionType == UserAction.Add)
        {
            D_Answers.Controls.Clear();
            for (int i = 1; i < 7; i++)
            {
                CheckBox CH_i = new CheckBox();
                CH_i.ID = "CH_" + Convert.ToString(i);
                CH_i.Text = i.ToString();

                TextBox TB_i = new TextBox();
                TB_i.Width = Unit.Pixel(400);
                TB_i.TextMode = TextBoxMode.MultiLine;
                TB_i.Rows = 3;
                TB_i.ID = "TB_" + Convert.ToString(i);
                TB_i.Text = string.Empty;

                D_Answers.Controls.Add(CH_i);
                D_Answers.Controls.Add(new LiteralControl("<br />"));

                D_Answers.Controls.Add(TB_i);
                D_Answers.Controls.Add(new LiteralControl("<br />"));
            }
        }
        else if (Presenter.ActionType == UserAction.Edit || Presenter.ActionType == UserAction.Copy)
        {
            var count = _answers.Count;
            int i = 0;
            D_Answers.Controls.Clear();
            foreach (var item in _answers)
            {
                i++;
                CheckBox CH_i = new CheckBox();
                CH_i.ID = "CH_" + Convert.ToString(i);

                TextBox TB_i = new TextBox();
                TB_i.ID = "TB_" + Convert.ToString(i);
                TB_i.Width = Unit.Pixel(400);
                TB_i.TextMode = TextBoxMode.MultiLine;
                TB_i.Rows = 3;

                var AText = item.Atext.ToString();
                CH_i.Text = i.ToString() + ".&nbsp;" + AText;
                TB_i.Text = AText;

                D_Answers.Controls.Add(CH_i);
                D_Answers.Controls.Add(new LiteralControl("<br />"));

                D_Answers.Controls.Add(TB_i);
                D_Answers.Controls.Add(new LiteralControl("<br />"));

                if (item.Correct.ToInt() == 1)
                {
                    CH_i.BackColor = Color.FromArgb(228, 240, 216);
                    CH_i.Checked = true;
                }
            }

            if (i < 7)
            {
                for (int j = i + 1; j < 7; j++)
                {
                    CheckBox CH_i = new CheckBox();
                    CH_i.ID = "CH_" + Convert.ToString(j);
                    CH_i.Text = ReturnLetter(j);
                    TextBox TB_i = new TextBox();
                    TB_i.Width = Unit.Pixel(400);
                    TB_i.TextMode = TextBoxMode.MultiLine;
                    TB_i.Rows = 3;
                    TB_i.ID = "TB_" + Convert.ToString(j);
                    TB_i.Text = string.Empty;
                    D_Answers.Controls.Add(CH_i);
                    D_Answers.Controls.Add(new LiteralControl("<br />"));
                    D_Answers.Controls.Add(TB_i);
                    D_Answers.Controls.Add(new LiteralControl("<br />"));
                }
            }
        }
    }

    private void FillHotSpotFields()
    {
        hdnQuestionType.Value = "03";
        D_Answers.Controls.Clear();
        if (Presenter.ActionType == UserAction.Add)
        {
            TextBox tx = new TextBox();
            tx.ID = "tx";
            tx.Text = string.Empty;
            D_Answers.Controls.Add(tx);
        }

        if (Presenter.ActionType == UserAction.Edit || Presenter.ActionType == UserAction.Copy)
        {
            var count = _answers.Count;
            if (count > 0)
            {
                var item = _answers.FirstOrDefault();
                var AText = item.Atext.ToString();
                AText = AText.Replace("<P>", string.Empty);
                AText = AText.Replace("</P>", string.Empty);
                TextBox tx = new TextBox();
                tx.Width = Unit.Pixel(400);
                tx.TextMode = TextBoxMode.MultiLine;
                tx.Rows = 4;

                if (!AText.Trim().Equals(string.Empty))
                {
                    tx.Text = AText.Trim();
                }

                tx.ID = "tx";
                D_Answers.Controls.Add(tx);
                D_Answers.Controls.Add(new LiteralControl("<br />"));
            }
            else
            {
                TextBox tx = new TextBox();
                tx.ID = "tx";
                tx.Width = Unit.Pixel(400);
                tx.Text = string.Empty;
                D_Answers.Controls.Add(tx);
                D_Answers.Controls.Add(new LiteralControl("<br />"));
            }
        }
    }

    private List<AnswerChoice> PopulateMultipleChoice()
    {
        var Alist = new List<AnswerChoice>();

        for (int i = 1; i < 7; i++)
        {
            TextBox TB = (TextBox)this.Master.FindControl("ContentPlaceHolder1").FindControl("TB_" + Convert.ToString(i));
            RadioButton RB = (RadioButton)this.Master.FindControl("ContentPlaceHolder1").FindControl("RB_" + Convert.ToString(i));
            TextBox ATB = (TextBox)this.Master.FindControl("ContentPlaceHolder1").FindControl("ucAltEditQuestion").FindControl("ATB_" + Convert.ToString(i));
            RadioButton ARB = (RadioButton)this.Master.FindControl("ContentPlaceHolder1").FindControl("ucAltEditQuestion").FindControl("RB_" + Convert.ToString(i));

            if (RB != null)
            {
                if (TB.Text.Trim() != string.Empty)
                {
                    var obj = new AnswerChoice();
                    obj.Aindex = ReturnLetter(i);
                    obj.Atext = TB.Text;
                    obj.AnswerConnectId = 0;
                    obj.ActionType = 1;
                    obj.Unit = string.Empty;
                    obj.InitialPosition = 0;
                    if (RB.Checked == true)
                    {
                        obj.Correct = 1;
                    }
                    else
                    {
                        obj.Correct = 0;
                    }

                    if (ATB != null)
                    {
                        obj.AlternateAText = ATB.Text;
                    }
                    else
                    {
                        obj.AlternateAText = string.Empty;
                    }

                    Alist.Add(obj);
                }
            }
        }

        return Alist;
    }

    private List<AnswerChoice> PopulateMatchFields()
    {
        var Alist = new List<AnswerChoice>();
        for (int i = 1; i < 7; i++)
        {
            TextBox TB = (TextBox)this.Master.FindControl("ContentPlaceHolder1").FindControl("TB_" + Convert.ToString(i));
            TextBox TB_P = (TextBox)this.Master.FindControl("ContentPlaceHolder1").FindControl("TB_P" + Convert.ToString(i));
            TextBox TB_O = (TextBox)this.Master.FindControl("ContentPlaceHolder1").FindControl("TB_O" + Convert.ToString(i));
            TextBox ATB = (TextBox)this.Master.FindControl("ContentPlaceHolder1").FindControl("ucAltEditQuestion").FindControl("ATB_" + Convert.ToString(i));
            TextBox ATB_P = (TextBox)this.Master.FindControl("ContentPlaceHolder1").FindControl("ucAltEditQuestion").FindControl("ATB_P" + Convert.ToString(i));
            TextBox ATB_O = (TextBox)this.Master.FindControl("ContentPlaceHolder1").FindControl("ucAltEditQuestion").FindControl("ATB_O" + Convert.ToString(i));

            if (TB != null && !TB.Text.Equals(string.Empty))
            {
                var obj = new AnswerChoice();
                obj.InitialPosition = TB_O.Text.ToInt();
                ////obj.Aindex = ReturnLetter(i);
                obj.Atext = TB.Text;
                obj.AnswerConnectId = 0;
                obj.ActionType = 1;
                obj.Correct = 0;
                obj.Unit = string.Empty;
                if (TB_P.Text.Trim() != string.Empty)
                {
                    int value;
                    Int32.TryParse(TB_P.Text, out value);
                    obj.Aindex = ReturnLetter(value);
                    ////obj.InitialPosition = value;
                }
                else
                {
                    obj.Aindex = string.Empty;
                    //// obj.InitialPosition = 0;
                }

                if (ATB != null)
                {
                    obj.AlternateAText = ATB.Text;
                }
                else
                {
                    obj.AlternateAText = string.Empty;
                }

                Alist.Add(obj);
            }
        }

        return Alist;
    }

    private List<AnswerChoice> PopulateMultipleChoiceMultiSelect()
    {
        var Alist = new List<AnswerChoice>();
        for (int i = 1; i < 7; i++)
        {
            TextBox TB = (TextBox)this.Master.FindControl("ContentPlaceHolder1").FindControl("TB_" + Convert.ToString(i));
            CheckBox RB = (CheckBox)this.Master.FindControl("ContentPlaceHolder1").FindControl("CH_" + Convert.ToString(i));
            TextBox ATB = (TextBox)this.Master.FindControl("ContentPlaceHolder1").FindControl("ucAltEditQuestion").FindControl("ATB_" + Convert.ToString(i));
            CheckBox ARB = (CheckBox)this.Master.FindControl("ContentPlaceHolder1").FindControl("ucAltEditQuestion").FindControl("ACH_" + Convert.ToString(i));

            if (RB != null && !TB.Text.Equals(string.Empty))
            {
                var obj = new AnswerChoice();
                if (i == 1)
                {
                    obj.Aindex = "A";
                }
                else if (i == 2)
                {
                    obj.Aindex = "B";
                }
                else if (i == 3)
                {
                    obj.Aindex = "C";
                }
                else if (i == 4)
                {
                    obj.Aindex = "D";
                }
                else if (i == 5)
                {
                    obj.Aindex = "E";
                }
                else if (i == 6)
                {
                    obj.Aindex = "F";
                }

                obj.Atext = TB.Text;
                obj.AnswerConnectId = 0;
                obj.ActionType = 1;
                obj.Unit = string.Empty;
                obj.InitialPosition = 0;
                if (RB.Checked == true)
                {
                    obj.Correct = 1;
                }
                else
                {
                    obj.Correct = 0;
                }

                if (ATB != null)
                {
                    obj.AlternateAText = ATB.Text;
                }
                else
                {
                    obj.AlternateAText = string.Empty;
                }

                Alist.Add(obj);
            }
        }

        return Alist;
    }

    private List<AnswerChoice> PopulateFillIn()
    {
        var Alist = new List<AnswerChoice>();
        var obj = new AnswerChoice();
        obj.Aindex = "A";
        TextBox RB = (TextBox)this.Master.FindControl("ContentPlaceHolder1").FindControl("tx");
        TextBox MTB = (TextBox)this.Master.FindControl("ContentPlaceHolder1").FindControl("m_tx");
        TextBox ARB = (TextBox)this.Master.FindControl("ContentPlaceHolder1").FindControl("ucAltEditQuestion").FindControl("Atx");
        TextBox AMTB = (TextBox)this.Master.FindControl("ContentPlaceHolder1").FindControl("ucAltEditQuestion").FindControl("Am_tx");

        if (RB != null)
        {
            obj.Atext = RB.Text;
        }
        else
        {
            obj.Atext = string.Empty;
        }

        if (ARB != null)
        {
            obj.AlternateAText = ARB.Text;
        }
        else
        {
            obj.AlternateAText = string.Empty;
        }

        obj.AnswerConnectId = 0;
        obj.ActionType = 1;
        obj.Correct = 1;
        obj.Unit = MTB.Text.Trim();
        obj.InitialPosition = 0;
        Alist.Add(obj);
        return Alist;
    }

    private List<AnswerChoice> PopulateHotSpot()
    {
        var Alist = new List<AnswerChoice>();
        var obj = new AnswerChoice();
        obj.Aindex = "A";
        TextBox RB = (TextBox)this.Master.FindControl("ContentPlaceHolder1").FindControl("tx");
        TextBox ARB = (TextBox)this.Master.FindControl("ContentPlaceHolder1").FindControl("ucAltEditQuestion").FindControl("Atx");

        if (RB != null)
        {
            obj.Atext = RB.Text;
        }
        else
        {
            obj.Atext = string.Empty;
        }

        if (ARB != null)
        {
            obj.AlternateAText = ARB.Text;
        }
        else
        {
            obj.AlternateAText = string.Empty;
        }

        obj.AnswerConnectId = 0;
        obj.ActionType = 1;
        obj.Correct = 1;
        obj.InitialPosition = 0;
        obj.Unit = string.Empty;
        Alist.Add(obj);
        return Alist;
    }

    private List<AnswerChoice> PopulateItem()
    {
        var Alist = new List<AnswerChoice>();
        Alist = null;
        return Alist;
    }

    private List<AnswerChoice> PopulateAnswers()
    {
        Presenter.GetAnswer();
        var _answerlist = new List<AnswerChoice>();
        var _questionType = ddQuestionType.SelectedValue.ToInt();
        if (_questionType != 0)
        {
            if (Enum.IsDefined(typeof(QuestionType), _questionType.ToInt()))
            {
                var _qType = (QuestionType)_questionType.ToInt();
                switch (_qType)
                {
                    case QuestionType.MultiChoiceMultiAnswer:
                        _answerlist = PopulateMultipleChoiceMultiSelect();
                        break;
                    case QuestionType.MultiChoiceSingleAnswer:
                        _answerlist = PopulateMultipleChoice();
                        break;
                    case QuestionType.Hotspot:
                        _answerlist = PopulateHotSpot();
                        break;
                    case QuestionType.Number:
                        _answerlist = PopulateFillIn();
                        break;
                    case QuestionType.Order:
                        _answerlist = PopulateMatchFields();
                        break;
                    default:
                        break;
                }
            }
        }

        return _answerlist;
    }

    private void SaveQuestion()
    {
        var objQuestion = new Question();
        objQuestion.Id = txtQID.Text.ToInt();
        objQuestion.QuestionId = txtQuestionID.Text.Trim();
        objQuestion.QuestionType = ddQuestionType.SelectedValue;
        objQuestion.ClientNeedsId = GetSelectedValue(ucSubCategories.ClientNeedsValue, DEFAULT_DROPDOWN_VALUE);
        objQuestion.ClientNeedsCategoryId = GetSelectedValue(ucSubCategories.ClientNeedsCategoryValue, DEFAULT_DROPDOWN_VALUE);
        objQuestion.NursingProcessId = GetSelectedValue(ucSubCategories.NursingProcessValue, DEFAULT_DROPDOWN_VALUE);
        objQuestion.LevelOfDifficultyId = GetSelectedValue(ucSubCategories.LevelOfDifficultyValue, DEFAULT_DROPDOWN_VALUE);
        objQuestion.DemographicId = GetSelectedValue(ucSubCategories.DemographyValue, DEFAULT_DROPDOWN_VALUE);
        objQuestion.CognitiveLevelId = GetSelectedValue(ucSubCategories.CognitiveLevel, DEFAULT_DROPDOWN_VALUE);
        objQuestion.CriticalThinkingId = GetSelectedValue(ucSubCategories.CriticalThinkingValue, DEFAULT_DROPDOWN_VALUE);
        objQuestion.ClinicalConceptsId = GetSelectedValue(ucSubCategories.ClinicalConceptsValue, DEFAULT_DROPDOWN_VALUE);
        objQuestion.ConceptsId = GetSelectedValue(ucSubCategories.ConceptsValue, DEFAULT_DROPDOWN_VALUE);
        objQuestion.Stimulus = txtStimulus.Text.Trim();
        objQuestion.Stem = txtStem.Text.Trim();
        objQuestion.ListeningFileUrl = txtListeningFileURL.Text.Trim();
        objQuestion.Explanation = txtExplanation.Text.Trim();
        objQuestion.RemediationId = ddTopicTitle.SelectedValue.ToInt();
        objQuestion.SpecialtyAreaId = GetSelectedValue(ucSubCategories.SpecialityAreaValue, DEFAULT_DROPDOWN_VALUE);
        objQuestion.SystemId = GetSelectedValue(ucSubCategories.SystemValue, DEFAULT_DROPDOWN_VALUE);
        objQuestion.ProgramofStudyId = ddProgramofStudy.SelectedValue.ToInt();
        if (ddProgramofStudy.SelectedValue == "1")
        {
            objQuestion.AccreditationCategoriesId = GetSelectedValue(ucSubCategories.AccreditationCategoriesValue, DEFAULT_DROPDOWN_VALUE);
            objQuestion.QSENKSACompetenciesId = GetSelectedValue(ucSubCategories.QSENKSACompetenciesValue, DEFAULT_DROPDOWN_VALUE);
        }
        else
        {
            objQuestion.AccreditationCategoriesId = "0";
            objQuestion.QSENKSACompetenciesId = "0";
        }

        objQuestion.ReadingCategoryId = "0";
        objQuestion.ReadingId = "0";
        objQuestion.WritingCategoryId = "0";
        objQuestion.WritingId = "0";
        objQuestion.MathCategoryId = "0";
        objQuestion.MathId = "0";
        objQuestion.ItemTitle = txtItemTitle.Text.Trim();
        objQuestion.ProductLineId = txtProductLine.Text.Trim();
        if (ddTypeOfFile.SelectedValue == "-1")
        {
            objQuestion.TypeOfFileId = string.Empty;
        }
        else
        {
            objQuestion.TypeOfFileId = ddTypeOfFile.SelectedValue;
        }

        objQuestion.Statisctics = txtStatistics.Text.Trim();
        objQuestion.CreatorId = txtCreator.Text.Trim();
        objQuestion.DateCreated = txtDCreated.Text.Trim();
        objQuestion.EditorId = txtEditor.Text.Trim();
        objQuestion.DateEdited = txtDEdited.Text.Trim();
        objQuestion.EditorId_2 = txt2Editor.Text.Trim();
        objQuestion.DateEdited_2 = txtD2Edit.Text.Trim();
        objQuestion.Source_SBD = txtSBD.Text.Trim();
        objQuestion.WhoOwns = txtWho.Text.Trim();
        objQuestion.Active = rdoActive.SelectedValue.ToInt();
        objQuestion.PointBiserialsId = txtPointB.Text.Trim();
        objQuestion.Feedback = txtFeedback.Text.Trim();
        objQuestion.AlternateStem = ucAltEditQuestion.AlternateStem;
        if (txtNorming.Text != string.Empty)
        {
            float norming;
            objQuestion.Q_Norming = float.TryParse(txtNorming.Text, out norming) ? norming : -1;
        }
        else
        {
            objQuestion.Q_Norming = -1;
        }

        if (ddQuestionType.SelectedValue.ToInt() == (int)QuestionType.MultiChoiceSingleAnswer || ddQuestionType.SelectedValue.ToInt() == (int)QuestionType.MultiChoiceMultiAnswer)
        {
            TextBox td1 = (TextBox)D_Exhibit.FindControl("TBE_1");
            objQuestion.ExhibitTab1 = td1.Text.Trim();
            TextBox td2 = (TextBox)D_Exhibit.FindControl("TBE_2");
            objQuestion.ExhibitTab2 = td2.Text.Trim();
            TextBox td3 = (TextBox)D_Exhibit.FindControl("TBE_3");
            objQuestion.ExhibitTab3 = td3.Text.Trim();
            td1 = (TextBox)D_Exhibit.FindControl("TBT_1");
            objQuestion.ExhibitTitle1 = td1.Text.Trim();
            td2 = (TextBox)D_Exhibit.FindControl("TBT_2");
            objQuestion.ExhibitTitle2 = td2.Text.Trim();
            td3 = (TextBox)D_Exhibit.FindControl("TBT_3");
            objQuestion.ExhibitTitle3 = td3.Text.Trim();
        }
        else
        {
            objQuestion.ExhibitTab1 = string.Empty;
            objQuestion.ExhibitTab2 = string.Empty;
            objQuestion.ExhibitTab3 = string.Empty;
            objQuestion.ExhibitTitle1 = string.Empty;
            objQuestion.ExhibitTitle2 = string.Empty;
            objQuestion.ExhibitTitle3 = string.Empty;
        }

        var _answerlist = PopulateAnswers();
        Presenter.SaveQuestion(objQuestion, _answerlist);
    }

    private void PopulateCategory(DropDownList control, Category category)
    {
        control.DataSource = category.Details.Values;
        control.DataTextField = "Description";
        control.DataValueField = "Id";
        control.DataBind();
    }

    private string GetValue(string controlName, Question question)
    {
        var _val = string.Empty;
        switch (controlName)
        {
            case "ExhibitTab1":
                _val = question.ExhibitTab1.ToString();
                break;
            case "ExhibitTab2":
                _val = question.ExhibitTab2.ToString();
                break;
            case "ExhibitTab3":
                _val = question.ExhibitTab3.ToString();
                break;
            case "ExhibitTitle1":
                _val = question.ExhibitTitle1.ToString();
                break;
            case "ExhibitTitle2":
                _val = question.ExhibitTitle2.ToString();
                break;
            case "ExhibitTitle3":
                _val = question.ExhibitTitle3.ToString();
                break;
            default:
                _val = string.Empty;
                break;
        }

        return _val;
    }

    private void ucSubCategories_OnClientNeedsChange(object sender, ItemSelectedEventArgs e)
    {
        if (ValidateProgramOfStudy())
        {
            Presenter.GetClientNeedsCategory(e.SelectedValue.ToInt(), ddProgramofStudy.SelectedValue.ToInt());
        }
    }

    private string GetSelectedValue(string selectedValue, string defaultValue)
    {
        return (selectedValue == Constants.LIST_NOT_SELECTED_VALUE) ? defaultValue : selectedValue;
    }

    private bool ValidateProgramOfStudy()
    {
        if (ddProgramofStudy.Visible == true && ddProgramofStudy.SelectedIndex == 0)
        {
            ktpMessage.Message.Clear();
            ktpMessage.Message.Add("Please select Program of Study.");
            ktpMessage.ShowMessage();
            return false;
        }
        else
        {
            return true;
        }
    }
    #endregion
}
