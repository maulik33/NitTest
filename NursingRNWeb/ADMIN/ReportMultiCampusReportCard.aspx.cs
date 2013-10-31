using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;
using NursingRNWeb.AppCode.Report_Ds;

namespace NursingRNWeb.ADMIN
{
    public partial class ReportMultiCampusReportCard : ReportPageBase<IReportMultiCampusReportCardView, ReportMultiCampusReportCardPresenter>, IReportMultiCampusReportCardView
    {
        private CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

        public override void PreInitialize()
        {
            Presenter.PreInitialize();
            MapControl(ddlProgramofStudy, lbxInstitution, lbxCohort, lbxGroup, lbxStudent, lbxProducts, lbxTests);
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
            ControlHelper.PopulateProducts(lbxProducts, products);
        }

        public void PopulateCohorts(IEnumerable<Cohort> cohorts)
        {
            ControlHelper.PopulateCohorts(lbxCohort, cohorts);
        }

        public void PopulateTests(IEnumerable<UserTest> tests)
        {
            ControlHelper.PopulateTestsByTestId(lbxTests, tests);
        }

        public void PopulateGroup(IEnumerable<Group> groups)
        {
            ControlHelper.PopulateGroups(lbxGroup, groups);
        }

        public void PopulateStudent(IEnumerable<StudentEntity> students)
        {
            ControlHelper.PopulateStudents(lbxStudent, students);
        }

        public void PopulateProgramOfStudies(IEnumerable<ProgramofStudy> programOfStudies)
        {
            ControlHelper.PopulateProgramofStudy(ddlProgramofStudy, programOfStudies);
            HideProgramofStudy();
        }

        public void GenerateReport()
        {
            #region Trace Information
            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Aggregate Reports > Multi campus report card")
                                .Add("Institution Id", lbxInstitution.SelectedValue)
                                .Add("Cohort Id", lbxCohort.SelectedValue)
                                .Add("Group Id", lbxGroup.SelectedValue)
                                .Add("Product Id", lbxProducts.SelectedValue)
                                .Add("Test Id", lbxTests.SelectedValue)
                                .Add("Student Id", lbxStudent.SelectedValue)
                                .Write();
            #endregion
            Presenter.GenerateReport();
        }

        public void GenerateReport(IEnumerable<MultiCampusReportDetails> reportData, ReportAction printActions)
        {
            string rptPath = string.Empty;
            IEnumerable<MultiCampusReportDetails> sortedData;
            if (printActions == ReportAction.PDFPrint)
            {
                rptPath = "~/Admin/Report/MultiCampusReportCardPdf.rpt";
                sortedData = hdnGridConfig.Value == "TestName|DESC" ? KTPSort.Sort<MultiCampusReportDetails>(reportData, SortHelper.Parse(hdnGridConfig.Value)).OrderByDescending(srt => srt.TestName) : KTPSort.Sort<MultiCampusReportDetails>(reportData, SortHelper.Parse(hdnGridConfig.Value)).OrderBy(srt => srt.TestName);
            }
            else
            {
                rptPath = "~/Admin/Report/MultiCampusReportCard.rpt";
                sortedData = KTPSort.Sort<MultiCampusReportDetails>(reportData, SortHelper.Parse(hdnGridConfig.Value));
            }

            rpt.Load(Server.MapPath(rptPath));
            rpt.SetDataSource(BuildDataSourceForReport(sortedData));

            switch (printActions)
            {
                case ReportAction.ExportExcelDataOnly:
                    rpt.ReportDefinition.Sections[6].SectionFormat.EnableSuppress = true;
                    rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Page.Response, true, "MultiCampusReportCard");
                    break;

                case ReportAction.ExportExcel:
                    rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                    rpt.ReportDefinition.Sections[6].SectionFormat.EnableSuppress = true;
                    rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Page.Response, true, "MultiCampusReportCard");
                    break;

                case ReportAction.PDFPrint:
                    rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Page.Response, true, "MultiCampusReportCard");
                    break;
            }
        }

        public void RenderReport(IEnumerable<MultiCampusReportDetails> reportData)
        {
            grvResult.DataSource = KTPSort.Sort<MultiCampusReportDetails>(reportData, SortHelper.Parse(hdnGridConfig.Value));
            grvResult.DataBind();

            if (grvResult.Rows.Count > 0)
            {
                lblM.Visible = false;
            }
            else
            {
                lblM.Visible = true;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region Trace Information
                TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Aggregate Reports > Multi campus report card");
                #endregion
            }
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            rpt.Close();
            rpt.Dispose();
        }

        protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
        {
            GenerateReport();
        }

        protected void btnPrintPDF_Click(object sender, ImageClickEventArgs e)
        {
            Presenter.GenerateReport(ReportAction.PDFPrint);
        }

        protected void btnPrintExcel_Click(object sender, ImageClickEventArgs e)
        {
            Presenter.GenerateReport(ReportAction.ExportExcel);
        }

        protected void btnPrintExcelDataOnly_Click(object sender, ImageClickEventArgs e)
        {
            Presenter.GenerateReport(ReportAction.ExportExcelDataOnly);
        }

        protected void grvResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string id = ((HtmlInputHidden)e.Row.Cells[11].FindControl("hdnStudentId")).Value;
                LinkButton lnkbtn = new LinkButton();
                lnkbtn = (LinkButton)e.Row.Cells[5].FindControl("lnkbuttonPerformance");
                lnkbtn.PostBackUrl = "ReportTestStudent.aspx?Mode=1&id=" + id + "&IID=" + ((HtmlInputHidden)e.Row.Cells[11].FindControl("hdnInstitutionId")).Value + "&ProductID=" + ((HtmlInputHidden)e.Row.Cells[11].FindControl("hdnTestTypeId")).Value
                  + "&UserTestID=" + ((HtmlInputHidden)e.Row.Cells[11].FindControl("hdnTestId")).Value + "&TestName=" + ((HtmlInputHidden)e.Row.Cells[11].FindControl("hdnTestName")).Value;

                string testTaken = ((HtmlInputHidden)e.Row.Cells[11].FindControl("hdnTestTaken")).Value;

                Label lblTestTaken = new Label();
                lblTestTaken = (Label)e.Row.Cells[10].FindControl("lblTestTaken");
                lblTestTaken.Text = Convert.ToDateTime(testTaken, CultureInfo.CurrentCulture).ToString();

                LinkButton lnkbtnRemediationTime = new LinkButton();
                lnkbtnRemediationTime = (LinkButton)e.Row.Cells[10].FindControl("lnkRemediationTime");
                lnkbtnRemediationTime.PostBackUrl = "ReportStudentQuestion.aspx?Mode=1&id=" + id + "&IID=" + ((HtmlInputHidden)e.Row.Cells[11].FindControl("hdnInstitutionId")).Value + "&ProductID=" + ((HtmlInputHidden)e.Row.Cells[11].FindControl("hdnTestTypeId")).Value
                   + "&UserTestID=" + ((HtmlInputHidden)e.Row.Cells[11].FindControl("hdnTestId")).Value + "&TestName=" + ((HtmlInputHidden)e.Row.Cells[11].FindControl("hdnTestName")).Value;
                if (lnkbtnRemediationTime.Text.ToLower() == "n/a")
                {
                    Label lblNA = (Label)e.Row.Cells[10].FindControl("lblNA");
                    if (lblNA != null)
                    {
                        lblNA.Visible = true;
                        lnkbtnRemediationTime.Visible = false;
                    }
                }
            }
        }

        protected void grvResult_Sorting(object sender, GridViewSortEventArgs e)
        {
            hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);

            GenerateReport();

            #region Change color of sorted column header
            switch (e.SortExpression)
            {
                case "InstitutionName":
                    grvResult.HeaderRow.Cells[0].BackColor = Color.Pink;
                    break;
                case "Student.FullName":
                    grvResult.HeaderRow.Cells[1].BackColor = Color.Pink;
                    break;
                case "Student.FirstName":
                    grvResult.HeaderRow.Cells[2].BackColor = Color.Pink;
                    break;
                case "Cohort.CohortName":
                    grvResult.HeaderRow.Cells[3].BackColor = Color.Pink;
                    break;
                case "Group.GroupName":
                    grvResult.HeaderRow.Cells[4].BackColor = Color.Pink;
                    break;
                case "Product.ProductName":
                    grvResult.HeaderRow.Cells[5].BackColor = Color.Pink;
                    break;
                case "TestName":
                    grvResult.HeaderRow.Cells[6].BackColor = Color.Pink;
                    break;
                case "QuestionCount":
                    grvResult.HeaderRow.Cells[7].BackColor = Color.Pink;
                    break;
                case "Correct":
                    grvResult.HeaderRow.Cells[8].BackColor = Color.Pink;
                    break;
                case "Ranking":
                    grvResult.HeaderRow.Cells[9].BackColor = Color.Pink;
                    break;
                case "TestStyle":
                    grvResult.HeaderRow.Cells[10].BackColor = Color.Pink;
                    break;
                case "TestTaken":
                    grvResult.HeaderRow.Cells[11].BackColor = Color.Pink;
                    break;
                case "TimeUsed":
                    grvResult.HeaderRow.Cells[12].BackColor = Color.Pink;
                    break;
                case "RemediationTime":
                    grvResult.HeaderRow.Cells[13].BackColor = Color.Pink;
                    break;
            }
            #endregion
        }

        private void HideProgramofStudy()
        {
            IsProgramofStudyVisible = Presenter.CurrentContext.UserType == UserType.SuperAdmin || Presenter.IsMultipleProgramofStudyAssignedToAdmin(Presenter.CurrentContext.UserId);
        }

        private MultiCampusReportCard BuildDataSourceForReport(IEnumerable<MultiCampusReportDetails> reportData)
        {
            MultiCampusReportCard ds = new MultiCampusReportCard();
            MultiCampusReportCard.HeadRow rh = ds.Head.NewHeadRow();
            rh.ReportName = "Multi-Campus Report Card";
            rh.InstitutionName = lbxInstitution.SelectedItemsText;
            rh.CohortName = lbxCohort.SelectedItemsText;
            if (lbxProducts.SelectedValue == "0")
            {
                rh.TestType = "All Test Type";
            }
            else
            {
                rh.TestType = lbxProducts.SelectedItemsText;
            }

            ds.Head.Rows.Add(rh);
            reportData.ToList().ForEach(record =>
            {
                MultiCampusReportCard.DetailRow rd = ds.Detail.NewDetailRow();
                rd.InstitutionName = record.InstitutionName;
                rd.FirstName = record.Student.FirstName;
                rd.LastName = record.Student.LastName;
                rd.Test = record.TestName;
                rd.TestType = record.Product.ProductName;
                rd.TestTaken = record.TestTaken.ToString();
                rd.TimeUsed = record.TimeUsed.ToString();
                rd.RemediationTime = record.RemediationTime.ToString();
                rd.GroupName = record.Group.GroupName;
                rd.Correct = String.Format("{0:0.00}", record.Correct);
                rd.Rank = record.Rank;
                rd.QuestionCount = Convert.ToString(record.QuestionCount);
                rd.TestStyle = record.TestStyle.ToString();
                rd.CohortName = record.Cohort.CohortName;
                rd.HeadId = rh.HeadId;
                ds.Detail.Rows.Add(rd);
            });

            return ds;
        }
    }
}