using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingRNWeb;

public partial class CMS_Controls_SearchQuestionCriteria : UserControl
{
    private string[] itemSelected;
    private string urlQueryUc = "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,,,0,0,0,0,0";

    public event EventHandler onSearchQuestionClick;

    public event EventHandler onSearchRemediationClick;

    public event EventHandler onAddEditQuestionClick;

    public event EventHandler<ItemSelectedEventArgs> OnProductSelectionChange;

    public event EventHandler<EventArgs> OnbtnAddRClick;

    public event EventHandler<EventArgs> OnbtnCategoryClick;

    public event EventHandler<EventArgs> OnbtnLippincotClick;

    public event EventHandler<ItemSelectedEventArgs> OnPopulationTypeSelectedIndexChange;

    public SubCategories SubCategoryControl
    {
        get
        {
            return ucSubCategories;
        }
    }

    public int ProgramofStudyId
    {
        get
        {
            return ddProgramofStudy.SelectedValue.ToInt();
        }
    }

    public void PopulateSearchCriteria(IEnumerable<Product> products, IEnumerable<Topic> titles, IDictionary<CategoryName, Category> categoryData)
    {
        ControlHelper.PopulateProducts(ddTestCategory, products);
        ControlHelper.PopulateTopicTitle(ddTopicTitle, titles);
        ddTest.ClearData();
        ucSubCategories.PopulateSubCategories(categoryData, ddProgramofStudy.SelectedValue.ToInt());
    }

    public void PopulateClientNeedsCategory(IDictionary<int, CategoryDetail> clientNeedsCategories)
    {
        ucSubCategories.PopulateClientNeedCategories(clientNeedsCategories);
    }

    public void PopulateTests(IEnumerable<Test> tests)
    {
        ControlHelper.PopulateTests(ddTest, tests);
    }

    public QuestionCriteria GetSelectedValues()
    {
        QuestionCriteria obj = new QuestionCriteria();
        obj.ProgramOfStudy = ControlHelper.GetSelectedValue(ddProgramofStudy, "0").ToInt();
        obj.Product = ControlHelper.GetSelectedValue(ddTestCategory, "0").ToInt();
        obj.Test = ControlHelper.GetSelectedValue(ddTest, "0").ToInt();
        obj.ClientNeed = GetSelectedValue(ucSubCategories.ClientNeedsValue, "0").ToInt();
        obj.ClientNeedsCategory = GetSelectedValue(ucSubCategories.ClientNeedsCategoryValue, "0").ToInt();
        obj.ClinicalConcept = GetSelectedValue(ucSubCategories.ClinicalConceptsValue, "0").ToInt();
        obj.CognitiveLevel = GetSelectedValue(ucSubCategories.CognitiveLevel, "0").ToInt();
        obj.CriticalThinking = GetSelectedValue(ucSubCategories.CriticalThinkingValue, "0").ToInt();
        obj.Demographic = GetSelectedValue(ucSubCategories.DemographyValue, "0").ToInt();
        obj.LevelOfDifficulty = GetSelectedValue(ucSubCategories.LevelOfDifficultyValue, "0").ToInt();
        obj.NursingProcess = GetSelectedValue(ucSubCategories.NursingProcessValue, "0").ToInt();
        obj.Remediation = ControlHelper.GetSelectedValue(ddTopicTitle, "0").ToInt();
        obj.SpecialtyArea = GetSelectedValue(ucSubCategories.SpecialityAreaValue, "0").ToInt();
        obj.System = GetSelectedValue(ucSubCategories.SystemValue, "0").ToInt();
        obj.AccreditationCategories = GetSelectedValue(ucSubCategories.AccreditationCategoriesValue, "0").ToInt();
        obj.QSENKSACompetencies = GetSelectedValue(ucSubCategories.QSENKSACompetenciesValue, "0").ToInt();
        obj.QuestionID = txtQuestionID.Text;
        obj.Text = txtText.Text;
        obj.ItemType = ddTypeOfFile.SelectedValue;
        obj.Qtype = ddQuestionType.SelectedValue;
        obj.Active = ControlHelper.GetSelectedValue(ddActive, "0").ToInt();
        obj.Concepts = GetSelectedValue(ucSubCategories.ConceptsValue, "0").ToInt();
        return obj;
    }

    public string GetUrlQuery()
    {
        string urlQuery = string.Empty;
        urlQuery = ddTestCategory.SelectedIndex.ToString() + ",";
        urlQuery += ddTest.SelectedIndex.ToString() + ",";
        urlQuery += ddTopicTitle.SelectedIndex.ToString() + ",";
        urlQuery += ucSubCategories.ClientNeedsSelectedIndex.ToString() + ",";
        urlQuery += ucSubCategories.ClientNeedsCategorySelectedIndex.ToString() + ",";
        urlQuery += ucSubCategories.NursingProcessSelectedIndex.ToString() + ",";
        urlQuery += ucSubCategories.LevelOfDifficultySelectedIndex.ToString() + ",";
        urlQuery += ucSubCategories.DemographySelectedIndex.ToString() + ",";
        urlQuery += ucSubCategories.CognitiveLevelSelectedIndex.ToString() + ",";
        urlQuery += ucSubCategories.SpecialityAreaSelectedIndex.ToString() + ",";
        urlQuery += ucSubCategories.SystemSelectedIndex.ToString() + ",";
        urlQuery += ucSubCategories.CriticalThinkingSelectedIndex.ToString() + ",";
        urlQuery += ucSubCategories.ClinicalConceptsSelectedIndex.ToString() + ",";
        urlQuery += ucSubCategories.AccreditationCategoriesSelectedIndex.ToString() + ",";
        urlQuery += ucSubCategories.QSENKSACompetenciesSelectedIndex.ToString() + ",";
        urlQuery += txtQuestionID.Text + ",";
        urlQuery += txtText.Text + ",";
        urlQuery += ddQuestionType.SelectedIndex.ToString() + ",";
        urlQuery += ddTypeOfFile.SelectedIndex.ToString() + ",";
        urlQuery += ddProgramofStudy.SelectedIndex.ToString() + ",";
        urlQuery += ucSubCategories.ConceptsSelectedIndex.ToString() ;
        return urlQuery;
    }

    public void PopulateInitialQuestionParameters(IEnumerable<Topic> titles, IEnumerable<ProgramofStudy> programofStudy)
    {
        ControlHelper.PopulateProgramOfStudy(ddProgramofStudy, programofStudy);
        ControlHelper.PopulateTopicTitle(ddTopicTitle, titles);
        ucSubCategories.PopulateSubategoryDefaultValue();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ucSubCategories.ProgramofStudyId = ddProgramofStudy.SelectedValue.ToInt();
        if (Global.IsProductionApp)
        {
            btnAdd.Visible = false;
            btnAddR.Visible = false;
        }

        if (!IsPostBack)
        {
            if (Request.QueryString["urlQuery"] != null && Request.QueryString["urlQuery"] != string.Empty)
            {
                itemSelected = Request.QueryString["urlQuery"].Split(',');
            }
            else
            {
                itemSelected = urlQueryUc.Split(',');
            }

            txtQuestionID.Text = itemSelected[15];
            txtText.Text = itemSelected[16];
            ddProgramofStudy.SelectedIndex = itemSelected[19].ToInt();
            PopulateOnProgramofStudyChange(itemSelected[19]);
            PopulateSelectedIndex(sender);
            if (Request.QueryString["searchback"].ToString() == "1")
            {
                QuestionCriteria searchCriteria = GetSelectedValues();
                SearchQuestionEventArgs eSearchCriteriaObj = new SearchQuestionEventArgs();
                eSearchCriteriaObj.SearchCriteria = searchCriteria;
                if (this.onSearchQuestionClick != null)
                {
                    onSearchQuestionClick(sender, eSearchCriteriaObj);
                }
            }
        }
    }

    protected void ddProgramofStudy_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateOnProgramofStudyChange(sender);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ValidateProgramOfStudy())
        {
            SearchQuestionEventArgs eSearchCriteriaObj = new SearchQuestionEventArgs();
            eSearchCriteriaObj.UrlQuery = GetUrlQuery();
            eSearchCriteriaObj.SearchCriteria = GetSelectedValues();
            if (this.onSearchQuestionClick != null)
            {
                onSearchQuestionClick(sender, eSearchCriteriaObj);
            }
        }
    }

    protected void btnRem_Click(object sender, EventArgs e)
    {
        QuestionCriteria searchCriteria = GetSelectedValues();
        SearchQuestionEventArgs eSearchCriteriaObj = new SearchQuestionEventArgs();
        eSearchCriteriaObj.UrlQuery = GetUrlQuery();
        eSearchCriteriaObj.SearchCriteria = searchCriteria;
        if (this.onSearchRemediationClick != null)
        {
            onSearchRemediationClick(sender, eSearchCriteriaObj);
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (this.onAddEditQuestionClick != null)
        {
            onAddEditQuestionClick(sender, e);
        }
    }

    #region Comment
    /*
        protected void btnViewTestQ_Click(object sender, EventArgs e)
        {
            #region Old code to be removed
            NumberOfQuestions = 0;
            lblNumberQ.Text = "";
            obj.Product = Convert.ToInt32(ddTestCategory.SelectedValue);
            obj.Test = Convert.ToInt32(ddTest.SelectedValue);
            obj.ClientNeed = Convert.ToInt32(ddClientNeeds.SelectedValue);
            obj.ClientNeedsCategory = Convert.ToInt32(ddClientNeedsCategory.SelectedValue);
            obj.ClinicalConcept = Convert.ToInt32(ddClinicalConcepts.SelectedValue);
            obj.CognitiveLevel = Convert.ToInt32(ddBloom.SelectedValue);
            obj.CriticalThinking = Convert.ToInt32(ddCriticalThinking.SelectedValue);
            obj.Demographic = Convert.ToInt32(ddDemography.SelectedValue);
            obj.LevelOfDifficulty = Convert.ToInt32(ddLevelOfDifficulty.SelectedValue);
            obj.NursingProcess = Convert.ToInt32(ddNursingProcess.SelectedValue);
            obj.Remediation = Convert.ToInt32(ddTopicTitle.SelectedValue);
            obj.SpecialtyArea = Convert.ToInt32(ddScpecialitArea.SelectedValue);
            obj.System = Convert.ToInt32(ddSystem.SelectedValue);
            obj.QuestionID = txtQuestionID.Text;
            obj.Text = txtText.Text;
            obj.ItemType = ddTypeOfFile.SelectedValue;
            obj.Qtype = ddQuestionType.SelectedValue;
            obj.Active = Convert.ToInt32(ddActive.SelectedValue);
            gvRem.Visible = false;
            gvQuestions.Visible = true;
            gvQuestions.DataSource = null;
            gvQuestions.DataSource = obj.GetListOfQuestions(obj);
            gvQuestions.DataBind();
    #endregion

            QuestionCriteria searchCriteria = GetSelectedValues();
            SearchQuestionEventArgs eSearchCriteriaObj = new SearchQuestionEventArgs();
            eSearchCriteriaObj.SearchCriteria = searchCriteria;
            if (this.onSearchQuestionClick != null)
            {
                onSearchQuestionClick(sender, eSearchCriteriaObj);
            }
        }
     */
    #endregion

    protected void btnAddR_Click(object sender, EventArgs e)
    {
        //// Response.Redirect("EditR.aspx?Action=1&CMS=1");
        if (OnbtnAddRClick != null)
        {
            OnbtnAddRClick(sender, e);
        }
    }

    protected void btnCategory_Click(object sender, EventArgs e)
    {
        //// Response.Redirect("TestCategories.aspx?CMS=1&mode=1");
        if (OnbtnCategoryClick != null)
        {
            OnbtnCategoryClick(sender, e);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //// Response.Redirect("Lippincott.aspx?CMS=1");
        if (OnbtnLippincotClick != null)
        {
            OnbtnLippincotClick(sender, e);
        }
    }

    protected void ddTestCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ValidateProgramOfStudy())
        {
            if (OnProductSelectionChange != null)
            {
                ItemSelectedEventArgs args = new ItemSelectedEventArgs()
                {
                    SelectedText = ddTestCategory.SelectedItem.Text,
                    SelectedValue = ddTestCategory.SelectedItem.Value
                };
                OnProductSelectionChange(sender, args);
            }
        }
    }

    private void PopulateSelectedIndex(Object sender)
    {
        ddQuestionType.SelectedIndex = Convert.ToInt32(itemSelected[17]);
        ddTypeOfFile.SelectedIndex = Convert.ToInt32(itemSelected[18]);
        ddTestCategory.SelectedIndex = itemSelected[0].ToInt();
        if (ddTestCategory.SelectedIndex != 0)
        {
            if (this.OnProductSelectionChange != null)
            {
                ItemSelectedEventArgs args = new ItemSelectedEventArgs()
                {
                    SelectedText = ddTestCategory.SelectedItem.Text,
                    SelectedValue = ddTestCategory.SelectedItem.Value
                };
                OnProductSelectionChange(sender, args);
                ddTest.SelectedIndex = itemSelected[1].ToInt();
            }
        }

        ddTopicTitle.SelectedIndex = itemSelected[2].ToInt();
        ucSubCategories.ClientNeedsSelectedIndex = itemSelected[3].ToInt();
        ucSubCategories.PopulateClientNeedCategory(sender);
        ucSubCategories.ClientNeedsCategorySelectedIndex = itemSelected[4].ToInt();
        ucSubCategories.NursingProcessSelectedIndex = itemSelected[5].ToInt();
        ucSubCategories.LevelOfDifficultySelectedIndex = itemSelected[6].ToInt();
        ucSubCategories.DemographySelectedIndex = itemSelected[7].ToInt();
        ucSubCategories.CognitiveLevelSelectedIndex = itemSelected[8].ToInt();
        ucSubCategories.SpecialityAreaSelectedIndex = itemSelected[9].ToInt();
        ucSubCategories.SystemSelectedIndex = itemSelected[10].ToInt();
        ucSubCategories.CriticalThinkingSelectedIndex = itemSelected[11].ToInt();
        ucSubCategories.ClinicalConceptsSelectedIndex = itemSelected[12].ToInt();
        ucSubCategories.AccreditationCategoriesSelectedIndex = itemSelected[13].ToInt();
        ucSubCategories.QSENKSACompetenciesSelectedIndex = itemSelected[14].ToInt();
        ddQuestionType.SelectedIndex = itemSelected[17].ToInt();
        ddTypeOfFile.SelectedIndex = itemSelected[18].ToInt();
        ucSubCategories.ConceptsSelectedIndex = itemSelected[20].ToInt();
    }

    private int GetSelectedValue(string selectedValue, string defaultValue)
    {
        return (selectedValue == Constants.LIST_NOT_SELECTED_VALUE) ? defaultValue.ToInt() : selectedValue.ToInt();
    }

    private void PopulateOnProgramofStudyChange(object sender)
    {
        ucSubCategories.SetControlVisibility(ddProgramofStudy.SelectedValue);
        if (OnPopulationTypeSelectedIndexChange != null)
        {
            ItemSelectedEventArgs args = new ItemSelectedEventArgs()
            {
                SelectedText = ddProgramofStudy.SelectedItem.Text,
                SelectedValue = ddProgramofStudy.SelectedItem.Value
            };
            OnPopulationTypeSelectedIndexChange(sender, args);
        }
    }

    private bool ValidateProgramOfStudy()
    {
        if (ddProgramofStudy.SelectedIndex == 0)
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
}