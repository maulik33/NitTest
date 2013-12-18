using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;
using NursingRNWeb.AppCode.Report_Ds;

public partial class ADMIN_ReportStudentQuestions : ReportPageBase<IReportStudentQuestionsView, ReportStudentQuestionsPresenter>, IReportStudentQuestionsView
{
    private const string CONST_SYSTEMS = "Systems";
    private const string CONST_SYSTEM = "System";
    private int TotalN;
    private int TotalR;
    private ReportDocument rpt = new ReportDocument();

    private int DY
    {
        get
        {
            object o = ViewState["DY"];
            if (o == null)
            {
                return 0;
            }
            else
            {
                return System.Convert.ToInt32(o);
            }
        }

        set
        {
            ViewState["DY"] = value;
        }
    }

    public override void PreInitialize()
    {
        Presenter.PreInitialize();
        MapControl(ddProducts, ddTests);
    }

    #region IReportStudentQuestionsView Members

    public bool PostBack
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public void PopulateInstitutions(IEnumerable<Institution> institutions)
    {
    }

    public void PopulateProducts(IEnumerable<Product> products)
    {
        ControlHelper.PopulateProducts(ddProducts, products);
    }

    public void PopulateCohorts(IEnumerable<Cohort> cohorts)
    {
    }

    public void PopulateTests(IEnumerable<UserTest> tests)
    {
        ControlHelper.PopulateTest(ddTests, tests);
    }

    public void GenerateReport()
    {
        Presenter.GenerateReport();
    }

    public void RenderReportForIntegratedTest(IEnumerable<TestCategory> testAssignment, IEnumerable<TestRemediationTimeDetails> reportData)
    {
        gvIntegrated.Visible = true;
        gvFocus.Visible = false;
        gvNCLEX.Visible = false;
        DY = 0;
        gvIntegrated.Columns.Clear();
        BoundField c1 = new BoundField();
        c1.HeaderText = "Q.ID";
        c1.DataField = "QuestionID";
        c1.Visible = false;
        gvIntegrated.Columns.Add(c1);

        c1 = new BoundField();
        c1.HeaderText = "Test Name";
        c1.DataField = "TestName";
        c1.Visible = false;
        gvIntegrated.Columns.Add(c1);

        c1 = new BoundField();
        c1.HeaderText = "Correct";
        c1.DataField = "Correct";
        c1.SortExpression = "Correct";
        c1.FooterText = "0";
        gvIntegrated.Columns.Add(c1);

        foreach (TestCategory testCategory in testAssignment)
        {
            if (testCategory.IsAdmin)
            {
                DY++;
                string Title = ReturnName(testCategory.TableName.Trim()).Trim();
                string DBBind = testCategory.TableName.Trim();
                if (DBBind == CONST_SYSTEMS)
                {
                    DBBind = CONST_SYSTEM;
                }

                BoundField c = new BoundField();
                c.DataField = DBBind;
                c.HeaderText = Title;
                c.SortExpression = DBBind;
                c.FooterText = DY.ToString();
                this.gvIntegrated.Columns.Add(c);
            }
        }

        int index = DY;
        c1 = new BoundField();
        c1.HeaderText = "Remediation Topic";
        c1.DataField = "TopicTitle";
        c1.SortExpression = "TopicTitle";
        index++;
        c1.FooterText = index.ToString();
        gvIntegrated.Columns.Add(c1);

        c1 = new BoundField();
        c1.HeaderText = "Time Remediated";
        c1.DataField = "TimeSpendForRemedation";
        c1.SortExpression = "TimeSpendForRemedation";
        index++;
        c1.FooterText = index.ToString();
        gvIntegrated.Columns.Add(c1);

        gvIntegrated.DataSource = KTPSort.Sort<TestRemediationTimeDetails>(reportData, SortHelper.Parse(hdnGridConfig.Value));
        gvIntegrated.DataBind();

        ShowTotalTime();
    }

    public void RenderReportForNCLX(IEnumerable<TestRemediationTimeDetails> reportData)
    {
        gvFocus.Visible = false;
        gvNCLEX.Visible = true;
        gvIntegrated.Visible = false;
        gvNCLEX.DataSource = KTPSort.Sort<TestRemediationTimeDetails>(reportData, SortHelper.Parse(hdnGridConfig.Value));
        gvNCLEX.DataBind();

        ShowTotalTime();
    }

    public void RenderReportForFocusedReview(IEnumerable<TestRemediationTimeDetails> reportData)
    {
        gvNCLEX.Visible = false;
        gvFocus.Visible = true;
        gvIntegrated.Visible = false;
        gvFocus.DataSource = KTPSort.Sort<TestRemediationTimeDetails>(reportData, SortHelper.Parse(hdnGridConfig.Value));
        gvFocus.DataBind();

        ShowTotalTime();
    }

    public void ExportReport(string institutionNames, string cohortNames, IEnumerable<TestCategory> testAssignment, IEnumerable<TestRemediationTimeDetails> reportData, ReportAction act)
    {
        if (ddTests.SelectedValue == Constants.LIST_NOT_SELECTED_VALUE)
        {
            return;
        }

        StudentQuestion ds = new StudentQuestion();
        StudentQuestion.HeadRow rh = (StudentQuestion.HeadRow)ds.Head.NewRow();
        rh.StudentName = Presenter.GetStudentName();
        rh.ReportName = "Remediation Time by Question Report";
        rh.TestType = this.ddProducts.SelectedItem.Text;
        rh.TestName = this.ddTests.SelectedItem.Text;

        rh.InstitutionName = institutionNames;

        rh.CohortName = cohortNames;

        ds.Head.Rows.Add(rh);

        IEnumerable<TestRemediationTimeDetails> sortedData = null;
        sortedData = KTPSort.Sort<TestRemediationTimeDetails>(reportData, SortHelper.Parse(hdnGridConfig.Value));

        if (ddProducts.SelectedValue == TestType.Integrated.GetHashCode().ToString()
            || ddProducts.SelectedValue == "3")
        {
            foreach (TestRemediationTimeDetails r in sortedData)
            {
                StudentQuestion.DetailRow rd = (StudentQuestion.DetailRow)ds.Detail.NewRow();
                rd.HeadID = rh.HeadID;
                rd.QID = r.QId;
                rd.QuestionID = r.QuestionId;
                rd.ClientNeedCategory = r.ClientNeedCategory;
                rd.ClientNeeds = r.ClientNeeds;
                rd.ClinicalConcept = r.ClinicalConcept;
                rd.CognitiveLevel = r.CognitiveLevel;
                rd.Correct = r.Correct == 1 ? "Correct" : "Incorrect";
                rd.CriticalThinking = r.CriticalThinking;
                rd.Demographic = r.Demographic;
                rd.LevelOfDifficulty = r.LevelOfDifficulty;
                rd.NursingProcess = r.NursingProcess;
                rd.SpecialtyArea = r.SpecialtyArea;
                rd.AccreditationCategories = r.AccreditationCategories;
                rd.QSENKSACompetencies = r.QSENKSACompetencies;
                rd.Concepts = r.Concepts;
                rd.TimeSpendForRemedation = Convert.ToString(r.TimeSpendForRemedation);
                rd.TimeSpendForExplanation = r.TimeSpendForExplanation;
                rd.TopicTitle = r.TopicTitle;
                ds.Detail.Rows.Add(rd);
            }
        }
        else if (ddProducts.SelectedValue == TestType.Nclex.GetHashCode().ToString())
        {
            foreach (TestRemediationTimeDetails r in sortedData)
            {
                StudentQuestion.DetailRow rd = (StudentQuestion.DetailRow)ds.Detail.NewRow();
                rd.HeadID = rh.HeadID;
                rd.QID = r.QId;
                rd.QuestionID = r.QuestionId;
                rd.ClientNeedCategory = r.ClientNeedCategory;
                rd.ClientNeeds = r.ClientNeeds;
                rd.Correct = r.Correct == 1 ? "Correct" : "Incorrect";
                rd.Demographic = r.Demographic;
                rd.NursingProcess = r.NursingProcess;
                rd.TimeSpendForRemedation = Convert.ToString(r.TimeSpendForRemedation);
                rd.TimeSpendForExplanation = r.TimeSpendForExplanation;
                rd.TopicTitle = r.TopicTitle;
                ds.Detail.Rows.Add(rd);
            }
        }

        BuildReportColumes(testAssignment, ds, act);
    }

    public void SetControlValues()
    {
        lblName.Text = Presenter.GetStudentName();
    }

    public void PopulateProgramOfStudies(IEnumerable<ProgramofStudy> programOfStudies)
    {
        throw new NotImplementedException();
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Mode"] == "1")
        {
            hdnMode.Value = "1";
        }
        else
        {
            hdnMode.Value = string.Empty;
        }

        if (!IsPostBack)
        {
            Presenter.SetControlValues();
            Presenter.GenerateReport();
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        Presenter.NavigateToReportTestStudent(ddTests.SelectedValue.ToInt(), ddProducts.SelectedValue.ToInt());
    }

    protected void gvIntegrated_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gv = (GridView)sender;

            if (e.Row.Cells[2].Text == "1")
            {
                e.Row.Cells[2].Text = "y";
            }
            else if (e.Row.Cells[2].Text == "0")
            {
                e.Row.Cells[2].Text = "n";
            }
            else
            {
                e.Row.Cells[2].Text = "-";
            }

            TotalN = TotalN + 1;
            TotalR = TotalR + Convert.ToInt32(e.Row.Cells[4 + DY].Text);
            Label1.Text = "Total Time Remediated:";
        }
    }

    protected void gvFocus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gv = (GridView)sender;

            if (e.Row.Cells[2].Text == "1")
            {
                e.Row.Cells[2].Text = "y";
            }
            else if (e.Row.Cells[2].Text == "0")
            {
                e.Row.Cells[2].Text = "n";
            }
            else
            {
                e.Row.Cells[2].Text = "-";
            }

            TotalN = TotalN + 1;
            TotalR = TotalR + Convert.ToInt32(e.Row.Cells[4].Text);
            Label1.Text = "Total Time Reviewed:";
        }
    }

    protected void gvNCLEX_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gv = (GridView)sender;

            if (e.Row.Cells[2].Text == "1")
            {
                e.Row.Cells[2].Text = "y";
            }
            else if (e.Row.Cells[2].Text == "0")
            {
                e.Row.Cells[2].Text = "n";
            }
            else
            {
                e.Row.Cells[2].Text = "-";
            }

            TotalN = TotalN + 1;
            TotalR = TotalR + Convert.ToInt32(e.Row.Cells[7].Text);
            Label1.Text = "Total Time Reviewed:";
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

    protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.ExportReport(ReportAction.ExportExcelDataOnly);
    }

    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.ExportReport(ReportAction.ExportExcel);
    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.ExportReport(ReportAction.PDFPrint);
    }

    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        GenerateReport();
    }

    protected void gvIntegrated_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);

        GenerateReport();

        #region Change color of sorted column header
        string sortExpression = e.SortExpression.Trim().ToUpper().Replace(" ", string.Empty);
        if (sortExpression.Equals("TOPICTITLE"))
        {
            sortExpression = "REMEDIATIONTOPIC";
        }
        else if (sortExpression.Equals("TIMESPENDFORREMEDATION"))
        {
            sortExpression = "TIMEREMEDIATED";
        }

        for (int count = 0; count < gvIntegrated.Columns.Count; count++)
        {
            if (gvIntegrated.Columns[count].HeaderText.Trim().ToUpper().Replace(" ", string.Empty).Equals(sortExpression))
            {
                int index = Convert.ToInt32(gvIntegrated.Columns[count].FooterText);
                gvIntegrated.HeaderRow.Cells[count].BackColor = Color.Pink;
                break;
            }
        }
        #endregion
    }

    protected void gvFocus_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);

        GenerateReport();

        #region Change color of sorted column header
        switch (e.SortExpression)
        {
            case "QuestionId":
                gvFocus.HeaderRow.Cells[0].BackColor = Color.Pink;
                break;
            case "TestName":
                gvFocus.HeaderRow.Cells[1].BackColor = Color.Pink;
                break;
            case "Correct":
                gvFocus.HeaderRow.Cells[2].BackColor = Color.Pink;
                break;
            case "TopicTitle":
                gvFocus.HeaderRow.Cells[3].BackColor = Color.Pink;
                break;
            case "TimeSpendForExplanation":
                gvFocus.HeaderRow.Cells[4].BackColor = Color.Pink;
                break;
        }
        #endregion
    }

    protected void gvNCLEX_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);

        GenerateReport();

        #region Change color of sorted column header
        switch (e.SortExpression)
        {
            case "QuestionId":
                gvNCLEX.HeaderRow.Cells[0].BackColor = Color.Pink;
                break;
            case "TestName":
                gvNCLEX.HeaderRow.Cells[1].BackColor = Color.Pink;
                break;
            case "Correct":
                gvNCLEX.HeaderRow.Cells[2].BackColor = Color.Pink;
                break;
            case "ClientNeeds":
                gvNCLEX.HeaderRow.Cells[3].BackColor = Color.Pink;
                break;
            case "ClientNeedCategory":
                gvNCLEX.HeaderRow.Cells[4].BackColor = Color.Pink;
                break;
            case "NursingProcess":
                gvNCLEX.HeaderRow.Cells[5].BackColor = Color.Pink;
                break;
            case "Demographic":
                gvNCLEX.HeaderRow.Cells[6].BackColor = Color.Pink;
                break;
            case "TimeSpendForExplanation":
                gvNCLEX.HeaderRow.Cells[7].BackColor = Color.Pink;
                break;
        }
        #endregion
    }

    private void ShowTotalTime()
    {
        lblTotalN.Text = TotalN.ToString();
        lblTotalR.Text = string.Format("{0:00}:{1:00}:{2:00}", TotalR / 3600, (TotalR / 60) % 60, TotalR % 60);
    }

    private void BuildReportColumes(IEnumerable<TestCategory> testAssignment, StudentQuestion ds, ReportAction act)
    {
        string FieldString = "Correct|";
        string[] FieldArray = null;
        int i = 0;

        ParameterFields ParamFields = new ParameterFields();
        ParameterField ParamField = default(ParameterField);
        ParameterDiscreteValue DiscreteVal = new ParameterDiscreteValue();

        int testTypeId = Convert.ToInt32(ddProducts.SelectedValue);

        if (testTypeId == 4)
        {
            FieldString = "QuestionID|Correct|";
        }

        if (testTypeId == 1 || testTypeId == 3)
        {
            Int16 count = 0;
            foreach (TestCategory r in testAssignment)
            {
                if (r.IsAdmin)
                {
                    count += 1;
                    if (count > 4)
                    {
                        break;
                    }

                    string Title = ReturnName(r.TableName.Trim()).Trim();
                    string DBBind = r.TableName.Trim();
                    if (DBBind == "Systems")
                    {
                        DBBind = "System";
                    }
                    
                    FieldString += DBBind + "|";
                }
            }
        }

        switch (testTypeId)
        {
            case 1:
                FieldString += "TopicTitle" + "|";
                FieldString += "TimeSpendForRemedation" + "|";
                break;
            case 3:
                FieldString += "TopicTitle" + "|";
                FieldString += "TimeSpendForExplanation" + "|";
                break;
            case 4:
                FieldString += "ClientNeeds" + "|" + "ClientNeedCategory" + "|" + "NursingProcess" + "|" + "Demographic" + "|" + "TimeSpendForExplanation" + "|";
                break;
        }

        if (FieldString.Contains("|"))
        {
            FieldString = FieldString.Remove(FieldString.Length - 1, 1);
        }

        FieldArray = FieldString.Split('|');

        rpt = new ReportDocument();
        rpt.Load(this.Page.Server.MapPath("Report/StudentQuestion.rpt"));
        rpt.SetDataSource(ds);

        string TestType = string.Empty;
        for (i = 0; i < FieldArray.Length; i++)
        {
            ParamField = new ParameterField();
            ParamField.ParameterFieldName = "P" + (i + 1).ToString();
            DiscreteVal = new ParameterDiscreteValue();
            string st = FieldArray[i];
            st = ReportHelper.ReturnName(st);
            if (st == "TopicTitle")
            {
                st = "Remediation Topic";
            }

            if (st == "TimeSpendForRemedation")
            {
                st = "Time Remediated";
                TestType = st;
            }

            if (st == "TimeSpendForExplanation")
            {
                st = "Time Reviewed";
                TestType = st;
            }
            
            DiscreteVal.Value = st;
            ParamField.CurrentValues.Add(DiscreteVal);
            ParamFields.Add(ParamField);
            rpt.DataDefinition.FormulaFields["F" + (i + 1).ToString()].Text = "{Detail." + FieldArray[i] + "}";
            rpt.ParameterFields["P" + (i + 1).ToString()].CurrentValues.AddValue(st);
        }

        int TolT = 0;
        if (TestType == "Time Remediated")
        {
            foreach (StudentQuestion.DetailRow r in ds.Detail.Rows)
            {
                TolT += Convert.ToInt32(r.TimeSpendForRemedation);
            }
        }

        if (TestType == "Time Reviewed")
        {
            foreach (StudentQuestion.DetailRow r in ds.Detail.Rows)
            {
                TolT += r.TimeSpendForExplanation;
            }
        }

        rpt.ParameterFields["T1"].CurrentValues.AddValue("Total " + TestType);
        rpt.ParameterFields["T2"].CurrentValues.AddValue(lblTotalR.Text);

        for (int j = i + 1; j < 9; j++)
        {
            ParamField = new ParameterField();
            ParamField.ParameterFieldName = "P" + j.ToString();
            ParamFields.Add(ParamField);
            DiscreteVal = new ParameterDiscreteValue();
            DiscreteVal.Value = string.Empty;
            ParamField.CurrentValues.Add(DiscreteVal);
            ParamFields.Add(ParamField);
            ParamField.AllowCustomValues = false;

            rpt.ParameterFields["P" + j.ToString()].CurrentValues.AddValue(string.Empty);
        }

        switch (act)
        {
            case ReportAction.ExportExcel:
                rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                rpt.ReportDefinition.Sections["Section5"].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Page.Response, true, "StudentQuestion");
                break;
            case ReportAction.ExportExcelDataOnly:
                rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                rpt.ReportDefinition.Sections["Section5"].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Page.Response, true, "StudentQuestion");
                break;
            case ReportAction.PDFPrint:
                rpt.ReportDefinition.Sections[2].SectionFormat.EnableSuppress = true;
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Page.Response, true, "StudentQuestion");
                break;
            case ReportAction.ShowPreview:
                this.CrystalReportViewer1.ReportSource = rpt;
                this.CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.ActiveX;
                break;
            case ReportAction.DirectPrint:
                rpt.PrintToPrinter(1, false, 0, 0);
                break;
        }
    }
}
