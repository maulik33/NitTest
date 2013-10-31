using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class ADMIN_ReportStudentSummaryByAnswerChoice : ReportPageBase<IReportStudentSummaryByAnswerChoiceView, ReportStudentSummaryByAnswerChoicePresenter>, IReportStudentSummaryByAnswerChoiceView
{
    public string Sort
    {
        get
        {
            object sortInfo = this.ViewState["sort"];
            if (sortInfo == null)
            {
                return string.Empty;
            }
            else
            {
                return sortInfo.ToString();
            }
        }

        set
        {
            this.ViewState["sort"] = value;
        }
    }

    public override void PreInitialize()
    {
        Presenter.PreInitialize();
        MapControl(ddlProgramofStudy,lbxInstitution, lbxCohort, ddProducts, ddTest);
    }

    #region IReportStudentReportCardView Methods

    public bool PostBack
    {
        get
        {
            return IsPostBack;
        }
    }

    public bool IsProgramofStudyVisible
    {
        get { return trProgramofStudy.Visible; }
        set { trProgramofStudy.Visible = value; }
    }

    public void PopulateInstitutions(IEnumerable<Institution> institutions)
    {
        ControlHelper.PopulateInstitutions(lbxInstitution, institutions, true);
    }

    public void PopulateProducts(IEnumerable<Product> products)
    {
        ControlHelper.PopulateProducts(ddProducts, products);
    }

    public void PopulateCohorts(IEnumerable<Cohort> cohorts)
    {
        ControlHelper.PopulateCohorts(lbxCohort, cohorts);
    }

    public void PopulateTests(IEnumerable<UserTest> tests)
    {
        ControlHelper.PopulateTests(ddTest, tests, "TestId");
    }

    public void PopulateProgramOfStudies(IEnumerable<ProgramofStudy> programOfStudies)
    {
        ControlHelper.PopulateProgramofStudy(ddlProgramofStudy, programOfStudies);
        HideProgramofStudy();
    }

    public void GenerateReport()
    {
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Statistical Information > Student Summary By Answer Choice")
                            .Add("Institution Id", lbxInstitution.SelectedValue)
                            .Add("Cohort Id", lbxCohort.SelectedValue)
                            .Add("Product Id", ddProducts.SelectedValue)
                            .Add("Test Id", ddTest.SelectedValue)
                            .Write();
        #endregion
        Presenter.GenerateReport();
    }

    public void ExportReport(DataTable reportData)
    {
        StringBuilder excelData = new StringBuilder();
        string sep = string.Empty;
        excelData.Append(sep + " Student Summary By Answer Choice report\t\t\t\t\t(N=" + reportData.Rows.Count + " students)\n");
        foreach (DataColumn dc in reportData.Columns)
        {
            string aa = sep + dc.ColumnName;
            excelData.Append(sep + dc.ColumnName);
            sep = "\t";
        }

        excelData.Append("\n");

        int i;
        foreach (DataRow dr in reportData.Rows)
        {
            sep = string.Empty;
            for (i = 0; i < reportData.Columns.Count; i++)
            {
                string bb = sep + dr[i].ToString();
                excelData.Append(sep + dr[i].ToString());
                sep = "\t";
            }

            excelData.Append("\n");
        }

        ReportHelper.ExportToExcel(excelData.ToString(), "StudentSummaryByAnswerChoiceReport.xls");
    }

    public void RenderReport(DataTable reportData)
    {
        if (reportData.Rows.Count == 0)
        {
            lblM.Visible = true;
            grvResult.Visible = false;
            Panel1.Visible = false;
            lblN.Text = string.Empty;
        }
        else
        {
            lblN.Text = "N=" + reportData.Rows.Count.ToString() + " students";
            grvResult.DataSource = reportData;
            DataView dv = new DataView(reportData);
            this.Sort = this.Sort == string.Empty ? "Student Name" : this.Sort;
            reportData.DefaultView.Sort = this.Sort;
            grvResult.DataBind();
            grvResult.Visible = true;
            Panel1.Visible = true;
            lblM.Visible = false;
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Statistical Information > Student Summary By Answer Choice");
            #endregion
        }
    }

    protected void btnPrintExcel_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.ExportToExcel();
    }

    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        this.Sort = null;
        GenerateReport();
    }

    protected void grvResult_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (this.Sort == e.SortExpression)
        {
            this.Sort += " DESC";
        }
        else
        {
            this.Sort = e.SortExpression;
        }

        GenerateReport();
        for (int count = 0; count < ((DataTable)grvResult.DataSource).DefaultView.Table.Columns.Count; count++)
        {
            if (((DataTable)grvResult.DataSource).DefaultView.Table.Columns[count].Caption.Equals(e.SortExpression))
            {
                grvResult.HeaderRow.Cells[count].BackColor = Color.Pink;
                break;
            }
        }
    }

    private void HideProgramofStudy()
    {
        IsProgramofStudyVisible = Presenter.CurrentContext.UserType == UserType.SuperAdmin || Presenter.IsMultipleProgramofStudyAssignedToAdmin(Presenter.CurrentContext.UserId); 
    }
}
