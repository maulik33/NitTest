using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class CMS_TestCategories : PageBase<ITestCategoryView, TestCategoryPresenter>, ITestCategoryView
{
    public int TestId { get; set; }

    public int TestType { get; set; }

    public int ProgramOfStudyId { get; set; }

    public List<TestCategory> TestCategories { get; set; }

    public override void PreInitialize()
    {
    }

    #region ITestCategoryView Methods
    public void RenderTestCategories(IEnumerable<ProgramofStudy> programofStudies, IEnumerable<Product> products, IEnumerable<Test> tests)
    {
        ddlProgramofStudy.Visible = true;
        lblProgramofStudyVal.Visible = true;
        ddlProgramofStudy.DataSource = programofStudies;
        ddlProgramofStudy.DataTextField = "ProgramofStudyName";
        ddlProgramofStudy.DataValueField = "ProgramofStudyId";
        ddlProgramofStudy.DataBind();
        
        ddTestCategory.DataSource = products;
        ddTestCategory.DataTextField = "ProductName";
        ddTestCategory.DataValueField = "ProductId";
        ddTestCategory.DataBind();
        ddTestCategory.Items.Insert(0, new ListItem("Not Selected", "0"));

        // tests may or may not be populated
        ddTest.DataSource = tests;
        ddTest.DataTextField = "TestName";
        ddTest.DataValueField = "TestId";
        ddTest.DataBind();
        ddTest.Items.Insert(0, new ListItem("Not Selected", "0"));
       
        if (TestId != 0 && TestType != 0 && ProgramOfStudyId != 0)
        {
            this.ddlProgramofStudy.SelectedValue = ProgramOfStudyId.ToString();
            this.ddTestCategory.SelectedValue = TestType.ToString();
            this.ddTest.SelectedValue = TestId.ToString();
            ddTest_SelectedIndexChanged(ddTest, new System.EventArgs()); // trigger category table build out
        }
        else
        {
            ddlProgramofStudy.SelectedIndex = 0;
            // as per Mike, disable test category drop down until user has selected a program of study
            ResetDropDown(ddTestCategory, false);
            ResetDropDown(ddTest, false);
            btnAssign.Enabled = false;
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Assign Test Category Page");
            #endregion
            Presenter.DisplayTestCategoryDetails();
        }
    }

    protected void ddProgramOfStudy_SelectedIndexChanged(object sender, EventArgs e)
    {
        //program of study has changed so change other stuff to not-selected/disabled
        ResetDropDown(ddTestCategory, ddlProgramofStudy.SelectedIndex != 0);
        ResetDropDown(ddTest, false); 
        gvCat.Visible = false;
        btnAssign.Enabled = false;
    }

    protected void ddTestCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvCat.Visible = false;
        btnAssign.Enabled = false;

        if (ddTestCategory.SelectedIndex != 0 && ddlProgramofStudy.SelectedIndex != 0) // the second operand should never be false if I am here
        {
            IEnumerable<Test> tests = Presenter.GetTests(ddTestCategory.SelectedValue.ToInt(), ddlProgramofStudy.SelectedValue.ToInt());
            ddTest.DataSource = tests;
            ddTest.DataTextField = "TestName";
            ddTest.DataValueField = "TestId";
            ddTest.DataBind();
            ddTest.Items.Insert(0, new ListItem("Not Selected", "0"));
            ResetDropDown(ddTest, true);
        }
        else
        {
            ResetDropDown(ddTest, false);
        }
    }

    protected void ddTest_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddTest.SelectedIndex != 0)
        {
            BindData();
            gvCat.Visible = true;
            btnAssign.Enabled = true;

        }
        else
        {
            gvCat.Visible = false;
            btnAssign.Enabled = false;

        }
    }
        
    private void ResetDropDown(DropDownList ddl, bool enabled)
    {
        ddl.SelectedIndex = 0;
        ddl.Enabled = enabled;
    }

    protected void BindData()
    {
        if (ddTest.SelectedValue != "0") //should always be the case
        {
            TestCategories = Presenter.GetTestCategories(ddTest.SelectedValue.ToInt()).ToList();
        }
        
        if (ddlProgramofStudy.SelectedValue != "0") //should always be the case
        {
            List<Category> categories = Presenter.GetCategories(ddlProgramofStudy.SelectedValue.ToInt()).ToList();
            gvCat.DataSource = categories;
            gvCat.DataBind();
        }
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        #region Trace Information

        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to Assign Test Category Page")
                            .Add("Test Category", ddTestCategory.SelectedValue)
                            .Add("Test Type", ddTest.SelectedValue)
                            .Write();

        #endregion
        if (ddTest.SelectedValue == "0")
        {
            this.Messenger1.ShowMessage("Please select a Test.");
            return;
        }

        foreach (GridViewRow row in gvCat.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                int CatID = Convert.ToInt32(gvCat.DataKeys[row.RowIndex].Values["CategoryID"].ToString());
                CheckBox chk_S = (CheckBox)row.FindControl("chk_S");
                CheckBox chk_A = (CheckBox)row.FindControl("chk_A");
                int Admin = 0;
                int Student = 0;
                if (chk_A.Checked)
                {
                    Admin = 1;
                }
                else
                {
                    Admin = 0;
                }

                if (chk_S.Checked)
                {
                    Student = 1;
                }
                else
                {
                    Student = 0;
                }

                Presenter.AssignTestCategory(ddTest.SelectedValue.ToInt(), CatID, Student, Admin);
            }
        }
    }


    protected void gvCat_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chk_S = (CheckBox)e.Row.FindControl("chk_S");
            CheckBox chk_A = (CheckBox)e.Row.FindControl("chk_A");
            if (chk_A != null && chk_S != null)
            {
                int catId = Convert.ToInt32(gvCat.DataKeys[e.Row.RowIndex].Values["CategoryID"].ToString());
                TestCategory testCategory = GetTestCategory(catId);
                if (testCategory != null)
                {
                    if (testCategory.Student == 0)
                    {
                        chk_S.Checked = false;
                    }
                    else
                    {
                        chk_S.Checked = true;
                    }

                    if (testCategory.Admin == 0)
                    {
                        chk_A.Checked = false;
                    }
                    else
                    {
                        chk_A.Checked = true;
                    }
                }
            }

            e.Row.Cells[0].Text = ReturnName(e.Row.Cells[0].Text);
        }
    }

    protected string ReturnName(string str)
    {
        string f_name = str.Trim();
        if (f_name == "ClientNeeds")
        {
            f_name = "Client Needs";
        }

        if (f_name == "NursingProcess")
        {
            f_name = "Nursing Process";
        }

        if (f_name == "CriticalThinking")
        {
            f_name = "Critical Thinking";
        }
        
        if (f_name == "ClinicalConcept")
        {
            f_name = "Clinical Concept";
        }

        if (f_name == "CognitiveLevel")
        {
            f_name = "Bloom's Cognitive Level";
        }

        if (f_name == "SpecialtyArea")
        {
            f_name = "Specialty Area";
        }
        
        if (f_name == "LevelOfDifficulty")
        {
            f_name = "Level Of Difficulty";
        }
        
        if (f_name == "ClientNeedCategory")
        {
            f_name = "Client Need Category ";
        }

        if (f_name == "AccreditationCategories")
        {
            f_name = "Accreditation Categories";
        }

        if (f_name == "QSENKSACompetencies")
        {
            f_name = "QSEN KSA Competencies";
        }
        
        return f_name;
    }

    private TestCategory GetTestCategory(int CatId)
    {
        TestCategory testcategory = null;
        if (TestCategories != null)
        {
            testcategory = (from tc in TestCategories
                            where tc.CategoryId == CatId
                            select tc).FirstOrDefault();
        }

        return testcategory;
    }
}
