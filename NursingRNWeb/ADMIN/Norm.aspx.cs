using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class ADMIN_Norm : PageBase<INormView, NormPresenter>, INormView
{
    public int TestId
    {
        get
        {
            return ddTest.SelectedValue.ToInt();
        }
    }

    public string Category
    {
        get
        {
            return ddCategory.SelectedValue;
        }
    }

    public int ProgramodStudy
    {
        get
        {
            return ddProgramofStudy.SelectedValue.ToInt();
        }
    }

    protected int NumberOfItem
    {
        get
        {
            return ViewState["NumberOfItem"].ToInt();
        }

        set
        {
            ViewState["NumberOfItem"] = value;
        }
    }

    public override void PreInitialize()
    {
    }

    #region INormViewMembers

    public void PopulateNorm(IDictionary<int, CategoryDetail> listOfCategory, IEnumerable<Norm> overallNormValues, IEnumerable<Norm> specificNormValues)
    {
        if (Global.IsProductionApp)
        {
            btnSave.Enabled = false;
        }
        PlaceHolder1.Visible = true;
        PlaceHolder1.Controls.Clear();
        Table T = new Table();
        TableRow R = new TableRow();
        TableCell C = new TableCell();
        string overallNormValue = string.Empty;
        if (overallNormValues != null && overallNormValues.Count() > 0)
        {
            Norm norm = overallNormValues.ToList()[0];
            overallNormValue = Convert.ToString(norm.NormValue);
        }

        int i = 0;
        AddNewTextBox(ref T, i, overallNormValue, i.ToString(), "Overall");
        foreach (KeyValuePair<int, CategoryDetail> entry in listOfCategory)
        {
            CategoryDetail categoryDetail = entry.Value;
            i += 1;
            string normValue = GetNormValue(specificNormValues, categoryDetail.Id);
            AddNewTextBox(ref T, i, normValue, categoryDetail.Id.ToString(), categoryDetail.Description);
        }

        NumberOfItem = i;
        PlaceHolder1.Controls.Add(T);

    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            Presenter.ShowNormingDetails();
            lblMessage.Text = string.Empty;
        }
        else
        {
            Presenter.InitializeNormDetails();
        }
    }

    protected void ddCategory_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        Presenter.ShowNormingDetails();
        PlaceHolder1.Visible = true;
        btnSave.Enabled = true;
        if (ddCategory.SelectedIndex <= 0)
        {
            PlaceHolder1.Visible = false;
            btnSave.Enabled = false;
        }

        if (Global.IsProductionApp)
        {
            btnSave.Enabled = false;
        }
    }

    protected void ddTest_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        Presenter.ShowNormingDetails();
        ResetDropDown(ddCategory, ddTest.SelectedIndex != 0);
        PlaceHolder1.Visible = false;
        btnSave.Enabled = false;
    }

    protected void btnSave_Click(object sender, System.EventArgs e)
    {
        if (ddCategory.SelectedIndex == 0)
        {
            errorMessage.Text = "Please select a Category.";
            errorMessage.Visible = true;
            return;
        }

        List<Norm> norms = new List<Norm>();
        List<Norm> dbNorms = Presenter.GetNorms(ddTest.SelectedValue.ToInt(), ddCategory.SelectedValue);
        string Category = this.ddCategory.SelectedValue;
        string[] TestIDs = null;

        if (NumberOfItem > 0)
        {
            for (int i = 0; i <= NumberOfItem; i++)
            {
                Label L = (Label)PlaceHolder1.FindControl("L_" + i);
                HiddenField H = (HiddenField)PlaceHolder1.FindControl("H_" + i);
                TextBox B = (TextBox)PlaceHolder1.FindControl("B_" + i);
                if (!IsValidFloatValue(B.Text))
                {
                    errorMessage.Text = "Invalid data in field " + L.Text + ". ";
                    errorMessage.Visible = true;
                    return;
                }

                if (i == 0)
                {
                    Category = "OverAll";
                }
                else
                {
                    if (ddProgramofStudy.SelectedValue.ToInt() == (int)ProgramofStudyType.PN)
                    {
                        Category = ((CategoryName)ddCategory.SelectedValue.ToInt()).ToString();
                        Category = Category.Remove(0, 2);
                    }
                    else
                    {
                        Category = ((CategoryName)ddCategory.SelectedValue.ToInt()).ToString();
                    }
                }

                if (this.ddTest.SelectedValue.Split('|').Length == 1)
                {
                    FillNormValue(ddTest.SelectedValue.ToInt(), Category, H.Value.ToInt(), GetFloatValue(B.Text), norms, dbNorms);
                }
                else
                {
                    TestIDs = this.ddTest.SelectedValue.Split('|');
                    foreach (string tid in TestIDs)
                    {
                        FillNormValue(tid.ToInt(), Category, H.Value.ToInt(), GetFloatValue(B.Text), norms, dbNorms);
                    }
                }
            }
        }
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to Edit / Add Norm Page")
                            .Add("Norm Id", Presenter.Id.ToString())
                            .Add("Category", Category)
                            .Add("Test Type", ddTestType.SelectedValue)
                            .Write();
        #endregion
        Presenter.SaveNorms(norms);
        lblMessage.Text = "Data is saved.";
    }

    protected void ddTestType_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        Presenter.PopulateTest(ddTestType.SelectedValue.ToInt(), ddProgramofStudy.SelectedValue.ToInt());
        ResetDropDown(ddTest, ddTestType.SelectedIndex != 0);
        ResetDropDown(ddCategory, false);
        PlaceHolder1.Visible = false;
        btnSave.Enabled = false;
    }

    protected void ddProgramofStudy_SelectedIndexChanged(object sender, EventArgs e)
    {
        Presenter.GetCategories(ddProgramofStudy.SelectedValue.ToInt());
        ////Presenter.PopulateTest(ddTestType.SelectedValue.ToInt(), ddProgramofStudy.SelectedValue.ToInt());
        ResetDropDown(ddTestType, ddProgramofStudy.SelectedIndex != 0);
        ResetDropDown(ddTest, false);
        ResetDropDown(ddCategory, false);
        PlaceHolder1.Visible = false;
        btnSave.Enabled = false;
    }

    public void PopulateTest(IEnumerable<Test> tests)
    {
        ControlHelper.PopulateTests(ddTest, tests);
    }

    public void PopulateControls(IEnumerable<Product> products, IEnumerable<ProgramofStudy> programofStudy)
    {
        ControlHelper.PopulateProgramOfStudy(ddProgramofStudy, programofStudy);
        ControlHelper.PopulateProducts(ddTestType, products);
        ddTest.Items.Insert(0, new ListItem("Not Selected", "0"));
        ddCategory.Items.Insert(0, new ListItem("Not Selected", "-1"));
        ResetDropDown(ddTest, false);
        ResetDropDown(ddCategory, false);
        ResetDropDown(ddTestType, ddProgramofStudy.SelectedIndex != 0);
        PlaceHolder1.Visible = false;
        btnSave.Enabled = false;
        if (Global.IsProductionApp)
        {
            btnSave.Enabled = false;
        }
    }

    public void PopulateCategories(IDictionary<CategoryName, Category> categories)
    {
        ControlHelper.PopulateCategories(ddCategory, categories.Values);
    }

    private float? GetFloatValue(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            float convertedValue = 0;
            float.TryParse(value, out convertedValue);
            return convertedValue;
        }
        else
        {
            return null;
        }
    }

    private void ResetDropDown(DropDownList ddl, bool enabled)
    {
        ddl.SelectedIndex = 0;
        ddl.Enabled = enabled;
    }

    private bool IsValidFloatValue(string value)
    {
        float convertedValue = 0;
        bool isValid = true;
        if (!string.IsNullOrEmpty(value))
        {
            if (!float.TryParse(value, out convertedValue))
            {
                isValid = false;
            }
        }

        return isValid;
    }

    private void AddNewTextBox(ref Table T, int i, string normValue, string id, string text)
    {
        TableRow R = new TableRow();
        T.Controls.Add(R);
        TableCell C = new TableCell();
        R.Controls.Add(C);
        Label L = new Label();
        C.Controls.Add(L);
        L.Text = text;
        L.ID = "L_" + i;
        HiddenField H = new HiddenField();
        C.Controls.Add(H);
        H.Value = id;
        H.ID = "H_" + i;
        C = new TableCell();
        R.Controls.Add(C);
        TextBox B = new TextBox();
        C.Controls.Add(B);
        B.Text = normValue;
        B.ID = "B_" + i;
        if (Global.IsProductionApp)
        {
            B.Enabled = false;
        }
    }

    private string GetNormValue(IEnumerable<Norm> specificNorms, int id)
    {
        string normValue = string.Empty;
        if (specificNorms.Count() > 0)
        {
            var value = (from sn in specificNorms
                         where sn.ChartID == id
                         select sn.NormValue).Single();
            if (value != null)
            {
                normValue = value.ToString();
            }
        }

        return normValue;
    }

    private void FillNormValue(int testID, string catrgory, int chartID, float? value, List<Norm> norms, List<Norm> dbNorms)
    {
        Norm norm = new Norm();
        norm.Id = GetDbNormId(dbNorms, chartID);
        norm.NormValue = value;
        norm.TestId = testID;
        norm.ChartType = catrgory;
        norm.ChartID = chartID;
        norms.Add(norm);
    }

    private int GetDbNormId(List<Norm> dbNorms, int chartId)
    {
        int id = 0;
        if (dbNorms != null && dbNorms.Count > 0)
        {
            id = (from n in dbNorms
                  where n.ChartID == chartId
                  select n.Id).SingleOrDefault();
        }

        return id;
    }

}