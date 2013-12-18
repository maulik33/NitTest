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

public partial class StudentSummaryReportByQuestion : ReportPageBase<IStudentSummaryReportByQuestionView, StudentSummaryReportByQuestionPresenter>, IStudentSummaryReportByQuestionView
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

    #region IReportStudentSummaryByQuestionView

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
        ControlHelper.PopulateTests(ddTest, tests, "TestID");
    }

    public void PopulateProgramOfStudies(IEnumerable<ProgramofStudy> programOfStudies)
    {
        ControlHelper.PopulateProgramofStudy(ddlProgramofStudy, programOfStudies);
        HideProgramofStudy();
    }

    public void GenerateReport()
    {
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Aggregate Reports > Student Summary By Question")
                            .Add("Institution Id", lbxInstitution.SelectedValue)
                            .Add("Cohort Id", lbxCohort.SelectedValue)
                            .Add("Product Id", ddProducts.SelectedValue)
                            .Add("Test Id", ddTest.SelectedValue)
                            .Write();
        #endregion
        Presenter.GenerateReport();
    }

    public void RenderReport(DataTable reportData)
    {
        if (reportData != null)
        {
            int count = reportData.Rows.Count;
            lblN.Text = "N=" + count.ToString() + " students";
            if (count == 0)
            {
                lblM.Visible = true;
                Panel1.Visible = false;
            }
            else
            {
                grvResult.DataSource = reportData;
                DataView dv = new DataView(reportData);
                this.Sort = this.Sort == string.Empty ? "Student Name" : this.Sort;

                reportData.DefaultView.Sort = this.Sort;
                grvResult.DataBind();
                Panel1.Visible = true;
                lblM.Visible = false;
            }
        }
        else
        {
            lblM.Visible = true;
            Panel1.Visible = false;
        }
    }

    public void ExportToExcel(DataTable reportData)
    {
        if (reportData != null)
        {
            StringBuilder excelData = new StringBuilder();
            StringBuilder sep = new StringBuilder();
            sep.Append(string.Empty);
            excelData.Append(sep.ToString() + " Student Summary By Question report\t\t\t\t\t\t (N= " + reportData.Rows.Count + " students)\n");
            foreach (DataColumn dc in reportData.Columns)
            {
                excelData.Append(sep.ToString() + dc.ColumnName);
                sep.Clear();
                sep.Append("\t");
            }

            excelData.Append("\n");

            int i;
            foreach (DataRow dr in reportData.Rows)
            {
                sep.Clear();
                sep.Append(string.Empty);
                for (i = 0; i < reportData.Columns.Count; i++)
                {
                    excelData.Append(sep.ToString() + dr[i].ToString());
                    sep.Clear();
                    sep.Append("\t");
                }

                excelData.Append("\n");
            }

            ReportHelper.ExportToExcel(excelData.ToString(), "StudentSummaryReportByQuestion.xls");
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Aggregate Reports > Student Summary By Question");
            #endregion
        }
    }

    protected void btnPrintExcel_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.ExportToExcel();
    }

    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        this.Sort =null;
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
       // this.Sort = string.Empty;
    }

    private void HideProgramofStudy()
    {
        IsProgramofStudyVisible = Presenter.CurrentContext.UserType == UserType.SuperAdmin || Presenter.IsMultipleProgramofStudyAssignedToAdmin(Presenter.CurrentContext.UserId); 
    }
}
