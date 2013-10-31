using System;
using System.Collections.Generic;
using System.Text;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class CMS_CopyCustomTest : PageBase<ICustomTestView, CustomTestPresenter>, ICustomTestView
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

    string ICustomTestView.SearchCondition
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

    string ICustomTestView.Sort
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

    protected string Sort
    {
        get
        {
            object o = this.ViewState["Sort"];
            if (o == null)
            {
                return "TestID";
            }
            else
            {
                return o.ToString();
            }
        }

        set
        {
            this.ViewState["Sort"] = value;
        }
    }

    private string SearchCondition
    {
        get
        {
            object o = this.ViewState["SearchCondition"];
            if (o == null)
            {
                return SearchCondition = string.Empty;
            }
            else
            {
                return SearchCondition = o.ToString();
            }
        }

        set
        {
            this.ViewState["SearchCondition"] = value;
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

        if (this.TestId != -1)
        {
           if (ddProducts.SelectedValue == "3")
            {
                chbFRDefault.Enabled = true;
            }

            Label3.Text = "Copying Test '" + test.TestName + "' to a new Custom Test.";
            lblProgramOfStudyName.Text = test.ProgramofStudyName;
            hdnProgramOfStudyId.Value = test.ProgramofStudyId.ToString();
        }
        else
        {
            Presenter.NavigateToSearchCustomTest(TestId);
        }
    }

    #endregion

    #region UnImplemented Methods

    public void DisplaySearchResult(IEnumerable<Test> tests, NursingLibrary.Common.SortInfo sortMetaData)
    {
        throw new NotImplementedException();
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            if (Global.IsProductionApp)
            {
                this.Button1.Visible = false;
            }

            Presenter.PopulateCustomTestDetails();
            #region Trace Information
            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to Custom Test Details Page")
                                .Add("Custom Test Id", TestId.ToString())
                                .Write();
            #endregion
        }
        else
        {
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
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Confirm())
        {
            Test test = new Test();
            test.GroupId = 0;
            if (this.chbFRDefault.Checked)
            {
                if (hdnProgramOfStudyId.Value.ToInt() == (int)BundleType.RN)
                {
                    test.GroupId = (int)BundleType.RN;
                }
                else if (hdnProgramOfStudyId.Value.ToInt() == (int)BundleType.PN)
                {
                    test.GroupId = (int)BundleType.PN;
                }
            }

            test.TestName = TextBox1.Text;
            test.ProductId = ddProducts.SelectedValue.ToInt();
            test.SecondPerQuestion = ddSecondsPerQuestion.SelectedValue.ToInt();
            test.ProgramofStudyId = hdnProgramOfStudyId.Value.ToInt();
            Presenter.CopyCustomTest(test);
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Presenter.NavigateToSearchCustomTest(TestId);
    }

    private bool Confirm()
    {
        int selectedProduct = -1;
        StringBuilder message = new StringBuilder();
        if (string.IsNullOrEmpty(this.TextBox1.Text))
        {
            message.Append("Test Name is required.<br />");
        }

        if (ddProducts.SelectedValue.ToInt() != 0)
        {
            selectedProduct = ddProducts.SelectedValue.ToInt();
        } ////Always we are creating new Custom test using copy custom test so testid is "-1"

        if (Presenter.IsCustomTestExist(-1, selectedProduct, TextBox1.Text))
        {
            message.Append("Test Name already exists -- please use another name.");
        }

        if (message.Length > 0)
        {
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