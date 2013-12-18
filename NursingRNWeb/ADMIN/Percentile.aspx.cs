using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class ADMIN_Percentile : PageBase<IPercentileView, PercentilePresenter>, IPercentileView
{
    private List<Norming> _normingDetail;
    private int _normingId;
    private Norming _norming;

    public int TestID
    {
        get
        {
            if (ddTests.SelectedValue == "0")
            {
                return -1;
            }
            else
            {
                return Convert.ToInt32(ddTests.SelectedValue);
            }
        }

        set { }
    }
   
    public int NormingID
    {
        get
        {
            return _normingId;
        }

        set
        {
            _normingId = value;
        }
    }

    public int TestType { get; set; }

    public int ProgramOfStudyId { get; set; }

    public List<Norming> NormingDetails
    {
        get
        {
            return _normingDetail;
        }

        set
        {
            _normingDetail = value;
        }
    }

    public Norming NormingProp
    {
        get
        {
            return _norming;
        }

        set
        {
            _norming = value;
        }
    }

    public IEnumerable<Product> Institutions { get; set; }

    public IEnumerable<Test> Tests { get; set; }

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.Edit);
    }

    #region IPercentileView Members

    public void RefreshNormingDetails()
    {
        Presenter.InitializeNormingDetails();
        if (NormingDetails.Count > 0)
        {
            GridView1.DataSource = NormingDetails;
            GridView1.DataBind();
        }
        else
        {
            NormingDetails.Add(new Norming());
            GridView1.DataSource = NormingDetails;
            GridView1.DataBind();

            int TotalColumns = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = TotalColumns;
            GridView1.Rows[0].Cells[0].Text = "No Record Found";
        }

        if (Global.IsProductionApp)
        {
            GridView1.Enabled = false;
        }
    }

    public void PopulateProducts(IEnumerable<ProgramofStudy> programofStudies, IEnumerable<Product> products, IEnumerable<Test> tests)
    {
        ddlProgramofStudy.NotSelectedText = "Selection Required";
        ddlProgramofStudy.Visible = true;
        lblProgramofStudyVal.Visible = true;
        ddlProgramofStudy.DataSource = programofStudies;
        ddlProgramofStudy.DataTextField = "ProgramofStudyName";
        ddlProgramofStudy.DataValueField = "ProgramofStudyId";
        ddlProgramofStudy.DataBind();

        ddProducts.DataSource = products;
        ddProducts.DataTextField = "ProductName";
        ddProducts.DataValueField = "ProductId";
        ddProducts.DataBind();
        ddProducts.Items.Insert(0, new ListItem("Not Selected", "0"));

        ddTests.DataSource = tests;
        ddTests.DataTextField = "TestName";
        ddTests.DataValueField = "TestId";
        ddTests.DataBind();
        ddTests.Items.Insert(0, new ListItem("Not Selected", "0"));

        if (TestID > 0 && TestType != 0 && ProgramOfStudyId != 0)
        {
            this.ddlProgramofStudy.SelectedValue = ProgramOfStudyId.ToString();
            this.ddProducts.SelectedValue = TestType.ToString();
            this.ddTests.SelectedValue = TestID.ToString();
            ddTests_SelectedIndexChanged(ddTests, new System.EventArgs()); // trigger category table build out
        }
        else
        {
            ddlProgramofStudy.SelectedIndex = 0;
            ResetDropDown(ddProducts, false);
            ResetDropDown(ddTests, false);
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region Trace Information
            TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Percentile List Page");
            #endregion
            Presenter.InitializePercentile();
        }
    }

    protected void ddProgramOfStudy_SelectedIndexChanged(object sender, EventArgs e)
    {
        ResetDropDown(ddProducts, ddlProgramofStudy.SelectedIndex != 0);
        ResetDropDown(ddTests, false);
        GridView1.Visible = false;
    }

    protected void ddProducts_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddProducts.SelectedIndex != 0 && ddlProgramofStudy.SelectedIndex != 0) 
        {
            IEnumerable<Test> tests = Presenter.GetTests(ddProducts.SelectedValue.ToInt(),ddlProgramofStudy.SelectedValue.ToInt());
            ddTests.DataSource = tests;
            ddTests.DataTextField = "TestName";
            ddTests.DataValueField = "TestId";
            ddTests.DataBind();
            ddTests.Items.Insert(0, new ListItem("Not Selected", "0"));
            ResetDropDown(ddTests, true);
            RefreshNormingDetails();
            GridView1.Visible = true;
        }
        else
        {
            ResetDropDown(ddTests, false);
            GridView1.Visible = false;
        }
        
    }

    protected void ddTests_SelectedIndexChanged(object sender, EventArgs e)
    {
        RefreshNormingDetails();
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        RefreshNormingDetails();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            Norming norming = new Norming();
            TextBox txtNewNumber = (TextBox)GridView1.FooterRow.FindControl("txtNewNumberCorrect");
            TextBox txtNewCorrect = (TextBox)GridView1.FooterRow.FindControl("txtNewCorrect");
            TextBox txtnewPercentileRank = (TextBox)GridView1.FooterRow.FindControl("txtNewPercentileRank");
            TextBox txtNewProbability = (TextBox)GridView1.FooterRow.FindControl("txtNewProbability");
            norming.NumberCorrect = GetSingleValue(txtNewNumber);
            norming.Correct = GetSingleValue(txtNewCorrect);
            norming.PercentileRank = GetSingleValue(txtnewPercentileRank);
            norming.Probability = GetSingleValue(txtNewProbability);
            norming.TestId = Convert.ToInt32(ddTests.SelectedValue);
            NormingProp = norming;
            Presenter.SaveNorming();
            RefreshNormingDetails();
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        NormingID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0].ToString());
        Presenter.DeleteNorming();
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        RefreshNormingDetails();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Norming norming = new Norming();
        TextBox txtNewNumber = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtNumberCorrect");
        TextBox txtNewCorrect = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtCorrect");
        TextBox txtnewPercentileRank = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtPercentileRank");
        TextBox txtNewProbability = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtProbability");
        norming.NumberCorrect = GetSingleValue(txtNewNumber);
        norming.Correct = GetSingleValue(txtNewCorrect);
        norming.PercentileRank = GetSingleValue(txtnewPercentileRank);
        norming.Probability = GetSingleValue(txtNewProbability);
        norming.TestId = Convert.ToInt32(ddTests.SelectedValue);
        norming.Id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0].ToString());
        NormingProp = norming;
        Presenter.SaveNorming();
        GridView1.EditIndex = -1;
        RefreshNormingDetails();
    }

    private float GetSingleValue(TextBox textBox)
    {
        float resultValue = 0;
        if (textBox != null)
        {
            Single.TryParse(textBox.Text, out resultValue);
        }

        return resultValue;
    }

    private void ResetDropDown(DropDownList ddl, bool enabled)
    {
        ddl.SelectedIndex = 0;
        ddl.Enabled = enabled;
    }
}
