using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class CMS_ViewQuestion : PageBase<IQuestionView, QuestionPresenter>, IQuestionView
{
    private List<AnswerChoice> _answers;

    public string URLQuery { get; set; }

    public string TestId { get; set; }

    public string VType { get; set; }

    public int ProgramOfStudyId { get; set; }

    #region IQuestionView Methods

    #region dropdowns

    public void PopulateProducts(IEnumerable<Product> products)
    {
    }

    public void PopulateTopicTitle(IEnumerable<Topic> titles)
    {
        ControlHelper.PopulateTopicTitle(ddTopicTitle, titles);
    }

    public void PopulateClientNeedsCategory(IEnumerable<ClientNeedsCategory> clientNeedsCategory)
    {
        ControlHelper.PopulateClientNeedsCategory(ddClientNeedsCategory, clientNeedsCategory);
    }

    public void PopulateDropDowns(IEnumerable<Topic> titles, Dictionary<CategoryName, Category> categoryData)
    {
        ControlHelper.PopulateTopicTitle(ddTopicTitle, titles);
        PopulateCategory(ddClientNeeds, categoryData[CategoryName.ClientNeeds]);
        PopulateCategory(ddNursingProcess, categoryData[CategoryName.NursingProcess]);
        PopulateCategory(ddLevelOfDifficulty, categoryData[CategoryName.LevelOfDifficulty]);
        PopulateCategory(ddDemography, categoryData[CategoryName.Demographic]);
        PopulateCategory(ddBloom, categoryData[CategoryName.CognitiveLevel]);
        PopulateCategory(ddScpecialityArea, categoryData[CategoryName.SpecialtyArea]);
        PopulateCategory(ddSystem, categoryData[CategoryName.Systems]);
        PopulateCategory(ddCriticalThinking, categoryData[CategoryName.CriticalThinking]);
        PopulateCategory(ddClinicalConcepts, categoryData[CategoryName.ClinicalConcept]);
        PopulateCategory(ddAccreditationCategories, categoryData[CategoryName.AccreditationCategories]);
        PopulateCategory(ddQSENKSACompetencies, categoryData[CategoryName.QSENKSACompetencies]);
        PopulateCategory(ddConcepts, categoryData[CategoryName.Concepts]);
    }

    #endregion

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

    public void PopulateProgramOfStudy(IEnumerable<ProgramofStudy> programOfStudies)
    {
        throw new NotImplementedException();
    }

    public void ShowErrorMessage(string errorMsg)
    {
        throw new NotImplementedException();
    }

    public void PopulateInitialQuestionParameters(IEnumerable<Topic> titles, IEnumerable<ProgramofStudy> programofStudy)
    {
        throw new NotImplementedException();
    }

    public void PopulateQuestion(Question question, int mode)
    {
        var _notSelectedIndex = Constants.LIST_NOT_SELECTED_VALUE.ToInt();
        PopulateAlternateTextDetails(question, Presenter.ActionType);
        if (mode == 1)
        {
            if (question != null)
            {
                hdnQuestionId.Value = question.Id.ToString();
                hdnQuestionNumber.Value = question.QuestionNumber.ToString();
                var _title = question.Product.ProductName.ToString().Trim() + "/" + question.Test.TestName.Trim()
                             + "&nbsp;&nbsp;-&nbsp;&nbsp; <B> Question Number :  " + question.QuestionNumber + "</B>(" + question.Id.ToString() + ")<br /><br />";

                Presenter.GetAnswer();
                Presenter.InitializeSearchParameters(question.ProgramofStudyId);
                D_Title.InnerHtml = _title;
            }
            else
            {
                Presenter.NavigateToSearch(string.Empty);
            }
        }
        else
        {
            if (question != null)
            {
                if (question.QuestionType.Trim().ToInt() == (int)QuestionType.MultiChoiceSingleAnswer)
                {
                    FillMultipleChoiceFields();
                    ShowExhibit();
                    TBE_1.Text = question.ExhibitTab1.ToString();
                    TBE_2.Text = question.ExhibitTab2.ToString();
                    TBE_3.Text = question.ExhibitTab3.ToString();
                    TBT_1.Text = question.ExhibitTitle1.ToString();
                    TBT_2.Text = question.ExhibitTitle2.ToString();
                    TBT_3.Text = question.ExhibitTitle3.ToString();
                }
                else if (question.QuestionType.ToString().Trim().ToInt() == (int)QuestionType.Number)
                {
                    FillTheBlankFields();
                    HideExhibit();
                }
                else if (question.QuestionType.ToString().Trim().ToInt() == (int)QuestionType.Order)
                {
                    FillTheMatchFields();
                    HideExhibit();
                }
                else if (question.QuestionType.ToString().Trim().ToInt() == (int)QuestionType.MultiChoiceMultiAnswer)
                {
                    FillMultipleChoiceMultiSelectFields();
                    ShowExhibit();
                    TBE_1.Text = question.ExhibitTab1.ToString();
                    TBE_2.Text = question.ExhibitTab2.ToString();
                    TBE_3.Text = question.ExhibitTab3.ToString();
                    TBT_1.Text = question.ExhibitTitle1.ToString();
                    TBT_2.Text = question.ExhibitTitle2.ToString();
                    TBT_3.Text = question.ExhibitTitle3.ToString();
                }
                else if (question.QuestionType.ToString().Trim().ToInt() == (int)QuestionType.Hotspot)
                {
                    FillHotSpotFields();
                    HideExhibit();
                }

                D_Title.InnerHtml = "<B>" + " QID : (" + question.Id + ")</B><br /><br />";
                lQuestionID.Text = question.QuestionId.ToString().Trim();
                D_Stimulus.InnerHtml = question.Stimulus;
                D_Stem.InnerHtml = ShowPicture(question.Stem);
                txt_ListeningFileURL.Text = question.ListeningFileUrl;
                D_Explanation.InnerHtml = ShowPicture(question.Explanation);
                lProductLine.Text = question.ProductLineId;
                lPointb.Text = question.PointBiserialsId;
                lStatistics.Text = question.Statisctics;
                lCreator.Text = question.CreatorId;
                lDCreated.Text = question.DateCreated;
                lEditor.Text = question.EditorId;
                lDEdited.Text = question.DateEdited;
                l2Editor.Text = question.EditorId_2;
                l2DEdit.Text = question.DateEdited_2;
                lSBD.Text = question.Source_SBD;
                lFeedback.Text = question.Feedback;
                lWho.Text = question.WhoOwns;
                lProgramOfStudyName.Text = question.ProgramofStudyName;

                if (String.IsNullOrEmpty(question.Active.ToString()))
                {
                    rdoActive.SelectedValue = "1";
                }
                else
                {
                    rdoActive.SelectedValue = question.Active.ToString();
                }

                if (question.Q_Norming > 0)
                {
                    lNorming.Text = question.Q_Norming.ToString();
                }
                else
                {
                    lNorming.Text = string.Empty;
                }

                if (question.RemediationId > 0)
                {
                    ddTopicTitle.SelectedValue = question.RemediationId.ToString().Trim();
                }
                else
                {
                    ddTopicTitle.SelectedIndex = _notSelectedIndex;
                }

                if (question.ClinicalConceptsId.ToInt() > 0)
                {
                    ddClinicalConcepts.SelectedValue = question.ClinicalConceptsId;
                }
                else
                {
                    ddClinicalConcepts.SelectedIndex = _notSelectedIndex;
                }

                if (question.CriticalThinkingId.ToInt() > 0)
                {
                    // Hack: Added Trim() since ddCriticalThinking value might have spaces at end. 
                    ddCriticalThinking.SelectedValue = question.CriticalThinkingId.Trim();
                }
                else
                {
                    ddCriticalThinking.SelectedIndex = _notSelectedIndex;
                }

                if (question.SystemId.ToInt() > 0)
                {
                    ddSystem.SelectedValue = question.SystemId;
                }
                else
                {
                    ddSystem.SelectedIndex = _notSelectedIndex;
                }

                if (question.SpecialtyAreaId.ToInt() > 0)
                {
                    ddScpecialityArea.SelectedValue = question.SpecialtyAreaId;
                }
                else
                {
                    ddScpecialityArea.SelectedIndex = _notSelectedIndex;
                }

                if (question.CognitiveLevelId.ToInt() > 0)
                {
                    ddBloom.SelectedValue = question.CognitiveLevelId;
                }
                else
                {
                    ddBloom.SelectedIndex = _notSelectedIndex;
                }

                if (question.DemographicId.ToInt() > 0)
                {
                    ddDemography.SelectedValue = question.DemographicId;
                }
                else
                {
                    ddDemography.SelectedIndex = _notSelectedIndex;
                }

                if (question.LevelOfDifficultyId.ToInt() > 0)
                {
                    ddLevelOfDifficulty.SelectedValue = question.LevelOfDifficultyId;
                }
                else
                {
                    ddLevelOfDifficulty.SelectedIndex = _notSelectedIndex;
                }

                if (question.NursingProcessId.ToInt() > 0)
                {
                    ddNursingProcess.SelectedValue = question.NursingProcessId;
                }
                else
                {
                    ddNursingProcess.SelectedIndex = _notSelectedIndex;
                }

                if (question.ClientNeedsId.ToInt() > 0)
                {
                    ddClientNeeds.SelectedValue = question.ClientNeedsId;
                }
                else
                {
                    ddClientNeeds.SelectedIndex = _notSelectedIndex;
                }

                Presenter.GetClientNeedsCategory(ddClientNeeds.SelectedValue.ToInt(), question.ProgramofStudyId);

                if (question.ClientNeedsCategoryId.ToInt() > 0)
                {
                    ddClientNeedsCategory.SelectedValue = question.ClientNeedsCategoryId;
                }
                else
                {
                    ddClientNeedsCategory.SelectedIndex = _notSelectedIndex;
                }

                if (question.AccreditationCategoriesId.ToInt() > 0)
                {
                    ddAccreditationCategories.SelectedValue = question.AccreditationCategoriesId;
                }
                else
                {
                    ddAccreditationCategories.SelectedIndex = _notSelectedIndex;
                }

                if (question.QSENKSACompetenciesId.ToInt() > 0)
                {
                    ddQSENKSACompetencies.SelectedValue = question.QSENKSACompetenciesId;
                }
                else
                {
                    ddQSENKSACompetencies.SelectedIndex = _notSelectedIndex;
                }

                if (question.ConceptsId.ToInt() > 0)
                {
                    ddConcepts.SelectedValue = question.ConceptsId;
                }
                else
                {
                    ddConcepts.SelectedIndex = _notSelectedIndex;
                }

                D_Remediation.InnerHtml = question.Remediation;

                Presenter.GetTestsForQuestion();
            }
            else
            {
                Presenter.NavigateToEdit(Presenter.Id.ToString(), string.Empty, UserAction.Edit);
            }
        }
    }

    public void PopulateAnswers(IEnumerable<AnswerChoice> answers)
    {
        _answers = answers.ToList();
    }

    public void PopulateTests(IEnumerable<Test> tests)
    {
        gvWhere.DataSource = tests;
        gvWhere.DataBind();
    }

    public void PopulateAlternateTextDetails(Question question, UserAction action)
    {
        ucAltEditQuestion.PopulateAlternateTextDetails(question, action, _answers);
    }

    public void RefreshPage(Question question, UserAction action, Dictionary<string, string> fileType, Dictionary<string, string> questionType, string mode, string testId, bool hasDeletePermission, bool hasAddPermission)
    {
        hdnQuestionId.Value = Presenter.Id.ToString();
        Presenter.GetAnswer();
        Presenter.SetProgramOfStudyId();
        
        if (mode.Equals("1"))
        {
            hdnTestId.Value = testId;
            hdnVType.Value = "T";
            hdnQuestionId.Value = Presenter.Id.ToString();
            hdnURL.Value = URLQuery;

            btnPreviouse.Visible = true;
            btnNext.Visible = true;
            NextQuestion(0);
        }
        else
        {
            hdnTestId.Value = string.Empty;
            hdnVType.Value = "Q";
            hdnQuestionId.Value = Presenter.Id.ToString();
            hdnURL.Value = URLQuery;

            btnPreviouse.Visible = false;
            btnNext.Visible = false;
            Presenter.InitializeSearchParameters(ProgramOfStudyId);
            Presenter.ViewQuestion();
        }
    }

    public void PopulateSearchQuestionCriteria(IEnumerable<Product> products, IEnumerable<Topic> titles, IDictionary<CategoryName, Category> categoryData,int programofStudy)
    {
        ControlHelper.PopulateTopicTitle(ddTopicTitle, titles);

        int eProgramOfStudy = (int)ProgramofStudyType.RN;
        PopulateCategory(ddClientNeeds, eProgramOfStudy == programofStudy ? categoryData[CategoryName.ClientNeeds] : categoryData[CategoryName.PNClientNeeds]);
        PopulateCategory(ddNursingProcess, eProgramOfStudy == programofStudy ? categoryData[CategoryName.NursingProcess] : categoryData[CategoryName.PNNursingProcess]);
        PopulateCategory(ddLevelOfDifficulty, eProgramOfStudy == programofStudy ? categoryData[CategoryName.LevelOfDifficulty] : categoryData[CategoryName.PNLevelOfDifficulty]);
        PopulateCategory(ddDemography, eProgramOfStudy == programofStudy ? categoryData[CategoryName.Demographic] : categoryData[CategoryName.PNDemographic]);
        PopulateCategory(ddBloom, eProgramOfStudy == programofStudy ? categoryData[CategoryName.CognitiveLevel] : categoryData[CategoryName.PNCognitiveLevel]);
        PopulateCategory(ddScpecialityArea, eProgramOfStudy == programofStudy ? categoryData[CategoryName.SpecialtyArea] : categoryData[CategoryName.PNSpecialtyArea]);
        PopulateCategory(ddSystem, eProgramOfStudy == programofStudy ? categoryData[CategoryName.Systems] : categoryData[CategoryName.PNSystems]);
        PopulateCategory(ddCriticalThinking, eProgramOfStudy == programofStudy ? categoryData[CategoryName.CriticalThinking] : categoryData[CategoryName.PNCriticalThinking]);
        PopulateCategory(ddClinicalConcepts, eProgramOfStudy == programofStudy ? categoryData[CategoryName.ClinicalConcept] : categoryData[CategoryName.PNClinicalConcept]);
        PopulateCategory(ddConcepts, eProgramOfStudy == programofStudy ? categoryData[CategoryName.Concepts] : categoryData[CategoryName.PNConcepts]);
        if (programofStudy == eProgramOfStudy)
        {
            PopulateCategory(ddAccreditationCategories, categoryData[CategoryName.AccreditationCategories]);
            PopulateCategory(ddQSENKSACompetencies, categoryData[CategoryName.QSENKSACompetencies]);
        }
        else
        {
            NonPNCategories.Visible = false;
        }
    }

    public void PopulateClientNeedCategories(IDictionary<int, CategoryDetail> categories)
    {
        ddClientNeedsCategory.DataSource = categories.Values;
        ddClientNeedsCategory.DataTextField = "Description";
        ddClientNeedsCategory.DataValueField = "Id";
        ddClientNeedsCategory.DataBind();
    }

    #endregion

    #region Abstract Method

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.View);
    }

    #endregion

    public void ShowErrorMessage()
    {
        throw new NotImplementedException();
    }

    public string ShowPicture(string str)
    {
        string S_Stem = str.Trim();
        int intQ = S_Stem.Trim().IndexOf("<Picture=", 0);
        if (intQ > 0)
        {
            int intE = S_Stem.Trim().IndexOf("/>", intQ);
            string L_Part = StringFunctions.Left(S_Stem, intQ - 1);
            string s_file = StringFunctions.Mid(S_Stem, intQ + 9, intE - 9 - intQ);
            string R_Part = StringFunctions.Right(S_Stem, S_Stem.Length - intE - 2);
            S_Stem = L_Part + "<P>" + "<img src=\"..\\Content\\" + s_file + "\"/>" + "</P>" + R_Part;
        }

        return S_Stem;
    }

    public void FillTheBlankFields()
    {
        if (_answers.Count > 0)
        {
            foreach (var item in _answers)
            {
                var AText = item.Atext.ToString();
                AText = AText.Replace("<P>", string.Empty);
                AText = AText.Replace("</P>", string.Empty);

                TextBox tx = new TextBox();
                if (!AText.Trim().Equals(string.Empty))
                {
                    tx.Text = AText.Trim();
                }

                tx.ID = "tx";
                tx.Enabled = false;
                D_Answers.Controls.Clear();
                D_Answers.Controls.Add(tx);

                Label lb = new Label();
                lb.ID = "LB";
                lb.Text = item.Unit;
                D_Answers.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
                D_Answers.Controls.Add(lb);
            }
        }
    }

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region Trace Information
            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to View Question Page")
                                .Add("Question Id", Presenter.Id.ToString())
                                .Write();
            #endregion
        }

        Presenter.LoadQuestionDetails();
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(hdnURL.Value))
        {
            Presenter.NavigateToSearch(string.Empty);
        }
        else
        {
            Presenter.NavigateToSearch(hdnURL.Value);
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        Presenter.NavigateToEdit(hdnQuestionId.Value, hdnURL.Value, UserAction.Edit);
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        NextQuestion(1);
    }

    protected void btnPreviouse_Click(object sender, EventArgs e)
    {
        NextQuestion(2);
    }

    #endregion

    #region Private Methods

    private void HideExhibit()
    {
        lblExhibit.Visible = false;
        D_Exhibit.Visible = false;
    }

    private void ShowExhibit()
    {
        lblExhibit.Visible = true;
        D_Exhibit.Visible = true;
    }

    private void NextQuestion(int type)
    {
        if (type == 0)
        {
            Presenter.GetNextQuestion(hdnTestId.Value.ToInt(), 0, "Q");
        }
        else if (type == 1)
        {
            Presenter.GetNextQuestion(hdnTestId.Value.ToInt(), hdnQuestionNumber.Value.ToInt(), "Q");
        }
        else if (type == 2)
        {
            Presenter.GetPreviousQuestion(hdnTestId.Value.ToInt(), hdnQuestionNumber.Value.ToInt(), "Q");
        }
    }

    private void FillMultipleChoiceFields()
    {
        int i = 0;
        D_Answers.Controls.Clear();
        foreach (var item in _answers)
        {
            i++;
            RadioButton RB_i = new RadioButton();
            RB_i.ID = "RB_" + Convert.ToString(i);
            var AText = item.Atext.ToString();
            RB_i.Width = Unit.Pixel(400);
            RB_i.Text = i.ToString() + ".&nbsp;" + AText;
            RB_i.GroupName = "RB";

            D_Answers.Controls.Add(RB_i);
            D_Answers.Controls.Add(new LiteralControl("<br />"));

            if (item.Correct == 1)
            {
                RB_i.BackColor = Color.FromArgb(228, 240, 216);
                RB_i.Checked = true;
            }
        }
    }

    private void FillTheMatchFields()
    {
        int i = 0;
        D_Answers.Controls.Clear();
        foreach (var item in _answers)
        {
            i++;

            TextBox TB = new TextBox();
            TB.ID = "TB" + i.ToString();
            TB.Text = item.Atext.ToString();

            TextBox TB_P = new TextBox();
            TB_P.ID = "TB_P" + i.ToString();
            TB_P.Text = item.InitialPosition.ToString();

            D_Answers.Controls.Add(TB);
            D_Answers.Controls.Add(new LiteralControl("<br />"));

            D_Answers.Controls.Add(TB_P);
            D_Answers.Controls.Add(new LiteralControl("<br />"));
        }
    }

    private void FillMultipleChoiceMultiSelectFields()
    {
        int i = 0;
        D_Answers.Controls.Clear();
        foreach (var item in _answers)
        {
            i++;
            CheckBox CH_i = new CheckBox();
            CH_i.ID = "CH_" + Convert.ToString(i);

            var AText = item.Atext.ToString();
            CH_i.Text = i.ToString() + ".&nbsp;" + AText;
            CH_i.Width = Unit.Pixel(400);
            D_Answers.Controls.Add(CH_i);
            D_Answers.Controls.Add(new LiteralControl("<br />"));

            if (item.Correct == 1)
            {
                CH_i.BackColor = Color.FromArgb(228, 240, 216);
                CH_i.Checked = true;
            }
        }
    }

    private void FillHotSpotFields()
    {
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

    private void AssignValues(Question question)
    {
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

    private void PopulateCategory(DropDownList control, Category category)
    {
        control.DataSource = category.Details.Values;
        control.DataTextField = "Description";
        control.DataValueField = "Id";
        control.DataBind();
    }

    #endregion

}
