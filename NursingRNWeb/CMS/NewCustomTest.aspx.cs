using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class CMS_NewCustomTest : PageBase<ICustomTestView, CustomTestPresenter>, ICustomTestView
{
    public int TestId
    {
        get
        {
            if (ViewState["TestId"].ToInt() == 0)
            {
                ViewState["TestId"] = -1;
            }

            return ViewState["TestId"].ToInt();
        }

        set
        {
            ViewState["TestId"] = value;
        }
    }

    public int PageIndex
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

    public string SearchCondition
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

    public string Sort
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

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.Edit);
    }

    #region ICustomTestView Methods

    public void RenderCustomTest(IEnumerable<ProgramofStudy> ProgramofStudies, IEnumerable<Product> products, Test test)
    {
        ddProducts.DataSource = products;
        ddProducts.DataTextField = "ProductName";
        ddProducts.DataValueField = "ProductId";
        ddProducts.DataBind();

        // ddProducts.Items.Insert(0, new ListItem("Not Selected", "0"));
        if (TestId != -1 && test != null)
        {
            TextBox1.Text = test.TestName;
            ddlProgramofStudy.Visible = false;
            lblProgramofStudyVal.Visible = true;
            lblProgramofStudyVal.Text = test.ProgramofStudyName;
            ddProducts.SelectedValue = Convert.ToString(test.ProductId);
            ddSecondsPerQuestion.SelectedValue = Convert.ToString(test.SecondPerQuestion);
            Label2.Text = "CMS > Rename Custom Test";

            // Focused Review
            if (ddProducts.SelectedValue == "3")
            {
                chbFRDefault.Enabled = true;
                if (test.DefaultGroup == "1" || test.DefaultGroup == "2")
                {
                    chbFRDefault.Checked = true;
                }
            }
        }
        else
        {
            Label2.Text = "CMS > New Custom Test";

            ddlProgramofStudy.DataSource = ProgramofStudies;
            ddlProgramofStudy.DataTextField = "ProgramofStudyName";
            ddlProgramofStudy.DataValueField = "ProgramofStudyId";
            ddlProgramofStudy.DataBind();

            ddlProgramofStudy.Items.Insert(0, new ListItem("Selection Required", "0"));
            ddlProgramofStudy.SelectedIndex = 0;
        }
    }

    #endregion

    #region UnImplementedMethods

    public void DisplaySearchResult(IEnumerable<Test> tests)
    {
        throw new NotImplementedException();
    }

    public void DisplaySearchResult(IEnumerable<Test> tests, NursingLibrary.Common.SortInfo sortMetaData)
    {
        throw new NotImplementedException();
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Global.IsProductionApp)
        {
            btnSave.Visible = false;
        }

        if (!Page.IsPostBack)
        {
            Presenter.PopulateCustomTestDetails();
            #region Trace Information
            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to Edit Custom Test Page")
                                .Add("Custom Test Id", Presenter.Id.ToString())
                                .Write();
            #endregion
        }
        else
        {
            ActivateControls();
        }
    }

    protected void ddProducts_SelectedIndexChanged(object sender, EventArgs e)
    {
        StringBuilder message = new StringBuilder();
        if (string.IsNullOrEmpty(TextBox1.Text))
        {
            message.Append("<li>Test Name is required.</li>");
        }

        if (ddProducts.SelectedIndex <= 0)
        {
            message.Append("<li>Please select product type.</li>");
        }

        if (message.Length > 0)
        {
            errorMessage.Text = message.ToString();
            errorMessage.Visible = true;
        }
        else
        {
            errorMessage.Text = string.Empty;
            errorMessage.Visible = false;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Confirm())
        {
            Test test = new Test();
            if (TestId != -1)
            {
              test =  Presenter.GetTestById(TestId);
            }

            if (ddlProgramofStudy.Visible == true)
            {
                test.ProgramofStudyId = ddlProgramofStudy.SelectedValue.ToInt();
            }

            if (chbFRDefault.Checked)
            {
                if (test.ProgramofStudyId == 1)
                {
                    test.GroupId = 1;
                }
                else if (test.ProgramofStudyId == 2)
                {
                    test.GroupId = 2;
                }
            }
            test.TestName = TextBox1.Text;
            test.ProductId = ddProducts.SelectedValue.ToInt();
            test.SecondPerQuestion = ddSecondsPerQuestion.SelectedValue.ToInt();
            Presenter.SaveCustomTest(test);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Presenter.NavigateToSearchCustomTest(TestId);
    }

    private void ActivateControls()
    {
        // Focused Review
        if (ddProducts.SelectedValue == "3")
        {
            chbFRDefault.Enabled = true;
        }
        else
        {
            chbFRDefault.Checked = false;
            chbFRDefault.Enabled = false;
        }
    }

    private bool Confirm()
    {
        int selectedProduct = -1;
        StringBuilder message = new StringBuilder();

        if (ddlProgramofStudy.SelectedIndex == 0)
        {
            message.Append("<li>Please select Program of Study.</li>");
        }

        if (string.IsNullOrEmpty(TextBox1.Text))
        {
            message.Append("<li>Test Name is required.</li>");
        }

        if (ddProducts.SelectedIndex == 0)
        {
            message.Append("<li>Please select product type.</li>");
        }

        if (ddProducts.SelectedValue.ToInt() != 0)
        {
            selectedProduct = ddProducts.SelectedValue.ToInt();
        }

        if (message.Length > 0)
        {
            errorMessage.Text = message.ToString();
            errorMessage.Visible = true;
            return false;
        }
        else
        {
            if (Presenter.IsCustomTestExist(TestId, selectedProduct, TextBox1.Text))
            {
                message.Append("<li>Test Name already exists -- please use another name.</li>");
                errorMessage.Text = message.ToString();
                errorMessage.Visible = true;
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}