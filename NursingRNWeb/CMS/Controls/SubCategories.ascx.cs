using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingRNWeb;

public partial class SubCategories : UserControl
{
    private const string DEFAULT_DROPDOWN_VALUE = "0";

    public event EventHandler<ItemSelectedEventArgs> OnClientNeedsChange;

    #region SelectedValue Properties

    public int ProgramofStudyId { get; set; }

    public string ClientNeedsValue
    {
        get { return ddClientNeeds.SelectedValue; }
        set { ddClientNeeds.SelectedValue = value; }
    }

    public string ClientNeedsCategoryValue
    {
        get { return ddClientNeedsCategory.SelectedValue; }
        set { ddClientNeedsCategory.SelectedValue = value; }
    }

    public string NursingProcessValue
    {
        get { return ddNursingProcess.SelectedValue; }
        set { ddNursingProcess.SelectedValue = value; }
    }

    public string LevelOfDifficultyValue
    {
        get { return ddLevelOfDifficulty.SelectedValue; }
        set { ddLevelOfDifficulty.SelectedValue = value; }
    }

    public string DemographyValue
    {
        get { return ddDemography.SelectedValue; }
        set { ddDemography.SelectedValue = value; }
    }

    public string CognitiveLevel
    {
        get { return ddCognitiveLevel.SelectedValue; }
        set { ddCognitiveLevel.SelectedValue = value; }
    }

    public string SpecialityAreaValue
    {
        get { return ddSpecialityArea.SelectedValue; }
        set { ddSpecialityArea.SelectedValue = value; }
    }

    public string SystemValue
    {
        get { return ddSystem.SelectedValue; }
        set { ddSystem.SelectedValue = value; }
    }

    public string CriticalThinkingValue
    {
        get { return ddCriticalThinking.SelectedValue; }
        set { ddCriticalThinking.SelectedValue = value; }
    }

    public string ClinicalConceptsValue
    {
        get { return ddClinicalConcepts.SelectedValue; }
        set { ddClinicalConcepts.SelectedValue = value; }
    }

    public string AccreditationCategoriesValue
    {
        get { return ddAccreditationCategories.SelectedValue; }
        set { ddAccreditationCategories.SelectedValue = value; }
    }

    public string QSENKSACompetenciesValue
    {
        get { return ddQSENKSACompetencies.SelectedValue; }
        set { ddQSENKSACompetencies.SelectedValue = value; }
    }

    public string ConceptsValue
    {
        get { return ddConcepts.SelectedValue; }
        set { ddConcepts.SelectedValue = value; }
    }
    #endregion

    #region SelectedIndex Properties

    public int ClientNeedsSelectedIndex
    {
        get { return ddClientNeeds.SelectedIndex; }
        set { ddClientNeeds.SelectedIndex = value; }
    }

    public int ClientNeedsCategorySelectedIndex
    {
        get { return ddClientNeedsCategory.SelectedIndex; }
        set { ddClientNeedsCategory.SelectedIndex = value; }
    }

    public int NursingProcessSelectedIndex
    {
        get { return ddNursingProcess.SelectedIndex; }
        set { ddNursingProcess.SelectedIndex = value; }
    }

    public int LevelOfDifficultySelectedIndex
    {
        get { return ddLevelOfDifficulty.SelectedIndex; }
        set { ddLevelOfDifficulty.SelectedIndex = value; }
    }

    public int DemographySelectedIndex
    {
        get { return ddDemography.SelectedIndex; }
        set { ddDemography.SelectedIndex = value; }
    }

    public int CognitiveLevelSelectedIndex
    {
        get { return ddCognitiveLevel.SelectedIndex; }
        set { ddCognitiveLevel.SelectedIndex = value; }
    }

    public int SpecialityAreaSelectedIndex
    {
        get { return ddSpecialityArea.SelectedIndex; }
        set { ddSpecialityArea.SelectedIndex = value; }
    }

    public int SystemSelectedIndex
    {
        get { return ddSystem.SelectedIndex; }
        set { ddSystem.SelectedIndex = value; }
    }

    public int CriticalThinkingSelectedIndex
    {
        get { return ddCriticalThinking.SelectedIndex; }
        set { ddCriticalThinking.SelectedIndex = value; }
    }

    public int ClinicalConceptsSelectedIndex
    {
        get { return ddClinicalConcepts.SelectedIndex; }
        set { ddClinicalConcepts.SelectedIndex = value; }
    }

    public int AccreditationCategoriesSelectedIndex
    {
        get { return ddAccreditationCategories.SelectedIndex; }
        set { ddAccreditationCategories.SelectedIndex = value; }
    }

    public int QSENKSACompetenciesSelectedIndex
    {
        get { return ddQSENKSACompetencies.SelectedIndex; }
        set { ddQSENKSACompetencies.SelectedIndex = value; }
    }
    public int ConceptsSelectedIndex
    {
        get { return ddConcepts.SelectedIndex; }
        set { ddConcepts.SelectedIndex = value; }
    }

    #endregion

    public void PopulateSubCategories(IDictionary<CategoryName, Category> categoryData, int programofStudy)
    {
        if (categoryData.Count > 0)
        {
            int RNProgramOfStudyId = (int)ProgramofStudyType.RN;
            PopulateCategory(ddClientNeeds, RNProgramOfStudyId == programofStudy ? categoryData[CategoryName.ClientNeeds] : categoryData[CategoryName.PNClientNeeds]);
            PopulateCategory(ddNursingProcess, RNProgramOfStudyId == programofStudy ? categoryData[CategoryName.NursingProcess] : categoryData[CategoryName.PNNursingProcess]);
            PopulateCategory(ddLevelOfDifficulty, RNProgramOfStudyId == programofStudy ? categoryData[CategoryName.LevelOfDifficulty] : categoryData[CategoryName.PNLevelOfDifficulty]);
            PopulateCategory(ddDemography, RNProgramOfStudyId == programofStudy ? categoryData[CategoryName.Demographic] : categoryData[CategoryName.PNDemographic]);
            PopulateCategory(ddCognitiveLevel, RNProgramOfStudyId == programofStudy ? categoryData[CategoryName.CognitiveLevel] : categoryData[CategoryName.PNCognitiveLevel]);
            PopulateCategory(ddSpecialityArea, RNProgramOfStudyId == programofStudy ? categoryData[CategoryName.SpecialtyArea] : categoryData[CategoryName.PNSpecialtyArea]);
            PopulateCategory(ddSystem, RNProgramOfStudyId == programofStudy ? categoryData[CategoryName.Systems] : categoryData[CategoryName.PNSystems]);
            PopulateCategory(ddCriticalThinking, RNProgramOfStudyId == programofStudy ? categoryData[CategoryName.CriticalThinking] : categoryData[CategoryName.PNCriticalThinking]);
            PopulateCategory(ddClinicalConcepts, RNProgramOfStudyId == programofStudy ? categoryData[CategoryName.ClinicalConcept] : categoryData[CategoryName.PNClinicalConcept]);
            if (programofStudy == RNProgramOfStudyId)
            {
                PopulateCategory(ddAccreditationCategories, categoryData[CategoryName.AccreditationCategories]);
                PopulateCategory(ddQSENKSACompetencies, categoryData[CategoryName.QSENKSACompetencies]);
            }
            PopulateCategory(ddConcepts, RNProgramOfStudyId == programofStudy ? categoryData[CategoryName.Concepts] : categoryData[CategoryName.PNConcepts]);
        }
        else
        {
            ddClientNeeds.ClearData();
            ddNursingProcess.ClearData();
            ddLevelOfDifficulty.ClearData();
            ddDemography.ClearData();
            ddCognitiveLevel.ClearData();
            ddSpecialityArea.ClearData();
            ddSystem.ClearData();
            ddCriticalThinking.ClearData();
            ddClinicalConcepts.ClearData();
            ddAccreditationCategories.ClearData();
            ddQSENKSACompetencies.ClearData();
            ddConcepts.ClearData();
        }
        ddClientNeedsCategory.ClearData();
    }

    public void PopulateSubategoryDefaultValue()
    {
        ddClientNeeds.Items.Insert(0, new ListItem("Not Selected", "0"));
        ddClientNeedsCategory.Items.Insert(0, new ListItem("Not Selected", "0"));
        ddNursingProcess.Items.Insert(0, new ListItem("Not Selected", "0"));
        ddLevelOfDifficulty.Items.Insert(0, new ListItem("Not Selected", "0"));

        ddDemography.Items.Insert(0, new ListItem("Not Selected", "0"));
        ddCognitiveLevel.Items.Insert(0, new ListItem("Not Selected", "0"));
        ddSpecialityArea.Items.Insert(0, new ListItem("Not Selected", "0"));

        ddSystem.Items.Insert(0, new ListItem("Not Selected", "0"));
        ddCriticalThinking.Items.Insert(0, new ListItem("Not Selected", "0"));
        ddClinicalConcepts.Items.Insert(0, new ListItem("Not Selected", "0"));

        ddAccreditationCategories.Items.Insert(0, new ListItem("Not Selected", "0"));
        ddQSENKSACompetencies.Items.Insert(0, new ListItem("Not Selected", "0"));

        ddConcepts.Items.Insert(0, new ListItem("Not Selected", "0"));
    }

    public void PopulateClientNeedCategories(IDictionary<int, CategoryDetail> categories)
    {
        ddClientNeedsCategory.Items.Clear();
        ddClientNeedsCategory.DataSource = categories.Values;
        ddClientNeedsCategory.DataTextField = "Description";
        ddClientNeedsCategory.DataValueField = "Id";
        ddClientNeedsCategory.DataBind();
    }

    public void PopulateSubCategoryValues(Question question)
    {
        ControlHelper.SetSelectedValue(ddClinicalConcepts, question.ClinicalConceptsId.ToInt().ToString(), DEFAULT_DROPDOWN_VALUE);
        ControlHelper.SetSelectedValue(ddCriticalThinking, question.CriticalThinkingId.ToInt().ToString(), DEFAULT_DROPDOWN_VALUE);
        ControlHelper.SetSelectedValue(ddSystem, question.SystemId.ToInt().ToString(), DEFAULT_DROPDOWN_VALUE);
        ControlHelper.SetSelectedValue(ddSpecialityArea, question.SpecialtyAreaId.ToInt().ToString(), DEFAULT_DROPDOWN_VALUE);
        ControlHelper.SetSelectedValue(ddCognitiveLevel, question.CognitiveLevelId.ToInt().ToString(), DEFAULT_DROPDOWN_VALUE);
        ControlHelper.SetSelectedValue(ddDemography, question.DemographicId.ToInt().ToString(), DEFAULT_DROPDOWN_VALUE);
        ControlHelper.SetSelectedValue(ddLevelOfDifficulty, question.LevelOfDifficultyId.ToInt().ToString(), DEFAULT_DROPDOWN_VALUE);
        ControlHelper.SetSelectedValue(ddNursingProcess, question.NursingProcessId.ToInt().ToString(), DEFAULT_DROPDOWN_VALUE);
        ControlHelper.SetSelectedValue(ddAccreditationCategories, question.AccreditationCategoriesId.ToInt().ToString(), DEFAULT_DROPDOWN_VALUE);
        ControlHelper.SetSelectedValue(ddQSENKSACompetencies, question.QSENKSACompetenciesId.ToInt().ToString(), DEFAULT_DROPDOWN_VALUE);
        ControlHelper.SetSelectedValue(ddClientNeeds, question.ClientNeedsId.ToInt().ToString(), DEFAULT_DROPDOWN_VALUE);
        ControlHelper.SetSelectedValue(ddConcepts,question.ConceptsId.ToInt().ToString(), DEFAULT_DROPDOWN_VALUE);
        if (question.ClientNeedsCategoryId.ToInt() > 0)
        {
            ControlHelper.SetSelectedValue(ddClientNeedsCategory, question.ClientNeedsCategoryId.ToInt().ToString(), DEFAULT_DROPDOWN_VALUE);
        }
    }

    public void SetControlVisibility(string programofStudy)
    {
        if (programofStudy == "2")
        {
            NonPNCategories.Visible = false;
        }
        else
        {
            NonPNCategories.Visible = true;
        }
    }

    public void PopulateClientNeedCategory(object sender)
    {
        if (OnClientNeedsChange != null)
        {
            ItemSelectedEventArgs args = new ItemSelectedEventArgs()
            {
                SelectedText = ddClientNeeds.SelectedItem.Text,
                SelectedValue = ddClientNeeds.SelectedItem.Value,
                ProgramofStudyId = ProgramofStudyId,
            };
            OnClientNeedsChange(sender, args);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ////
    }

    protected void ddClientNeeds_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateClientNeedCategory(sender);
    }

    private void PopulateCategory(DropDownList control, Category category)
    {
        control.DataSource = category.Details.Values;
        control.DataTextField = "Description";
        control.DataValueField = "Id";
        control.DataBind();
    }
}
