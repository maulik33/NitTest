using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;
using NursingRNWeb.AppCode.Report_Ds;

namespace NursingRNWeb.ADMIN
{
    public partial class ReportEnglishForNursingTracking : ReportPageBase<IReportEnglishForNursingTrackingView, ReportEnglishForNursingTrackingPresenter>, IReportEnglishForNursingTrackingView
    {
        private CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();       

        public override void PreInitialize()
        {
            Presenter.PreInitialize();
            MapControl(ddlProgramofStudy, lbxInstitution, lbxCohort, lbxStudent, lbxTests, lbxQid);
        }

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

        public void PopulateCohorts(IEnumerable<Cohort> cohorts)
        {
            ControlHelper.PopulateCohorts(lbxCohort, cohorts);
        }

        public void PopulateTests(IEnumerable<UserTest> tests)
        {
            ControlHelper.PopulateTestsByTestId(lbxTests, tests);
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

        public void PopulateProducts(IEnumerable<Product> products)
        {
            throw new NotImplementedException();
        }

        public void PopulateQid(IEnumerable<Question> qid)
        {
            ControlHelper.PopulateQid(lbxQid, qid);
        }

        public void GenerateReport()
        {
            #region Trace Information
            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Aggregate Reports > English For Nursing Tracking")
                                .Add("Institution Id", lbxInstitution.SelectedValue)
                                .Add("Cohort Id", lbxCohort.SelectedValue)
                                .Add("Test Id", lbxTests.SelectedValue)
                                .Add("Student Id", lbxStudent.SelectedValue)
                                .Add("Group Id", lbxQid.SelectedValue)
                                .Write();
            #endregion
            Presenter.GenerateReport();
        }

        public void RenderReport(IEnumerable<EnglishForNursingTracking> reportData)
        {
            grvResult.DataSource = KTPSort.Sort<EnglishForNursingTracking>(reportData, SortHelper.Parse(hdnGridConfig.Value));
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

        public void GenerateReport(IEnumerable<EnglishForNursingTracking> reportData, ReportAction printActions)
        {
            rpt.Load(Server.MapPath("~/Admin/Report/ReportEnglishForNursingTracking.rpt"));
            rpt.SetDataSource(BuildDataSourceForReport(KTPSort.Sort<EnglishForNursingTracking>(reportData, SortHelper.Parse(hdnGridConfig.Value))));

            switch (printActions)
            {
                case ReportAction.ExportExcelDataOnly:
                    rpt.ReportDefinition.Sections[6].SectionFormat.EnableSuppress = true;
                    rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Page.Response, true, "ReportEnglishForNursingTracking");
                    break;

                case ReportAction.ExportExcel:
                    rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                    rpt.ReportDefinition.Sections[6].SectionFormat.EnableSuppress = true;
                    rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Page.Response, true, "ReportEnglishForNursingTracking");
                    break;

                case ReportAction.PDFPrint:
                    rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Page.Response, true, "ReportEnglishForNursingTracking");
                    break;
            }
        }

        public void DisableAccess()
        {
            lblMessage.Text = "Only super admin can access this page.";
            btnPrintExcel.Enabled = false;
            btnSubmit.Enabled = false;
            btnPrintPDF.Enabled = false;
            btnPrintExcelDataOnly.Enabled = false;
            lbxInstitution.Enabled = false;
            lbxCohort.Enabled = false;
            lbxStudent.Enabled = false;
            lbxTests.Enabled = false;
            lbxQid.Enabled = false;
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
                case "CohortName":
                    grvResult.HeaderRow.Cells[1].BackColor = Color.Pink;
                    break;
                case "Student.LastName":
                    grvResult.HeaderRow.Cells[2].BackColor = Color.Pink;
                    break;
                case "Student.FirstName":
                    grvResult.HeaderRow.Cells[3].BackColor = Color.Pink;
                    break;
                case "UserAction":
                    grvResult.HeaderRow.Cells[4].BackColor = Color.Pink;
                    break;
                case "TestName":
                    grvResult.HeaderRow.Cells[5].BackColor = Color.Pink;
                    break;
                case "QuestionId":
                    grvResult.HeaderRow.Cells[6].BackColor = Color.Pink;
                    break;
                case "AltTabClickedDate":
                    grvResult.HeaderRow.Cells[7].BackColor = Color.Pink;
                    break;
                case "Correct":
                    grvResult.HeaderRow.Cells[8].BackColor = Color.Pink;
                    break;
            }
            #endregion
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Presenter.ValidateAccess();
            if (!IsPostBack)
            {
                #region Trace Information
                TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Aggregate Reports > English For Nursing Tracking ");
                #endregion
            }
        }

        protected void btnPrintPDF_Click(object sender, ImageClickEventArgs e)
        {
            Presenter.GenerateReport(ReportAction.PDFPrint);
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            rpt.Close();
            rpt.Dispose();
        }

        protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
            GenerateReport();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Message.Contains("Timeout"))
                {                    
                ////    lblMessage.Text = "";
                }
            }      
        }

        protected void btnPrintExcel_Click(object sender, ImageClickEventArgs e)
        {
            Presenter.GenerateReport(ReportAction.ExportExcel);
        }

        protected void btnPrintExcelDataOnly_Click(object sender, ImageClickEventArgs e)
        {
            Presenter.GenerateReport(ReportAction.ExportExcelDataOnly);
        }

        private EnglishNursingTracking BuildDataSourceForReport(IEnumerable<EnglishForNursingTracking> reportData)
        {
            EnglishNursingTracking ds = new EnglishNursingTracking();
            EnglishNursingTracking.HeadRow rh = ds.Head.NewHeadRow();
            ////rh.ReportName = "English NursingTracking";
            rh.InstitutionName = lbxInstitution.SelectedItemsText;
            rh.CohortName = lbxCohort.SelectedItemsText;
            rh.Students = string.Join(";", lbxStudent.SelectedItems.Select(i => i.Text).ToArray());
            rh.Tests = lbxTests.SelectedItemsText;
            rh.QId = lbxQid.SelectedItemsText;

            ds.Head.Rows.Add(rh);
            reportData.ToList().ForEach(record =>
            {
                EnglishNursingTracking.DetailRow rd = ds.Detail.NewDetailRow();
                rd.Institution = record.InstitutionName;
                rd.CohortName = record.CohortName;
                rd.FirstName = record.Student.FirstName;
                rd.LastName = record.Student.LastName;
                rd.Test = record.TestName;
                rd.QId = record.QuestionId;
                rd.AltTextAccess = record.AltTabClickedDate.ToString();
                rd.UserAction = record.UserAction;
                ////rd.Correct = String.Format("{0:0.00}", record.Correct);
                rd.Correct = record.Correct;
                rd.HeadId = rh.HeadId;
                ds.Detail.Rows.Add(rd);
            });

            return ds;
        }

        private void HideProgramofStudy()
        {
            IsProgramofStudyVisible = Presenter.CurrentContext.UserType == UserType.SuperAdmin || Presenter.IsMultipleProgramofStudyAssignedToAdmin(Presenter.CurrentContext.UserId);
        }
    }
}