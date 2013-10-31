using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;
using NursingRNWeb.AppCode.Report_Ds;
using Entities = NursingLibrary.Entity;
using RegularExpression = System.Text.RegularExpressions;

namespace NursingRNWeb.ADMIN
{
    public partial class ReportTestsScheduledbyDate : ReportPageBase<IReportTestsScheduledbyDateView, ReportTestsScheduledbyDatePresenter>, IReportTestsScheduledbyDateView
    {
        private const string WORNG_DATE_FORMAT = "Wrong date format";
        private const string WORNG_TO_DATE = "Please enter Date To greater than/equal to today's date.";
        private const string WORNG_START_TO_DATE = "Date From should be less than/equal to Date To.";
        private ReportDocument rpt = new ReportDocument();

        public DateTime? StartDate
        {
            get
            {
                return txtDateFrom.Text.ToNullableDateTime();
            }
        }

        public DateTime? EndDate
        {
            get
            {
                return txtDateTo.Text.ToNullableDateTime();
            }
        }

        public string SelectedProgramOfStudyName
        {
            get { return ddlProgramofStudy.SelectedItemsText; }
        }

        public bool IsAllCohortsSelected { get; set; }

        public bool IsAllGroupSelected { get; set; }

        public override void PreInitialize()
        {
            Presenter.PreInitialize();
            MapControl(ddlProgramofStudy,lbxInstitution, lbxCohort, lbxGroup, lbxProducts);
        }

        #region IReportTestSchedulbyDateView members

        public bool PostBack
        {
            get{ return IsPostBack; }
        }

        public void PopulateInstitutions(IEnumerable<Entities.Institution> institutions)
        {
            ControlHelper.PopulateInstitutions(lbxInstitution, institutions,true);
        }

        public void PopulateGroup(IEnumerable<Entities.Group> groups)
        {
            ControlHelper.PopulateGroups(lbxGroup, groups);
        }

        public void PopulateCohorts(IEnumerable<Entities.Cohort> cohorts)
        {
            ControlHelper.PopulateCohorts(lbxCohort, cohorts);
        }

        public void PopulateProducts(IEnumerable<Entities.Product> products)
        {
            ControlHelper.PopulateProducts(lbxProducts, products);
        }

        public void PopulateTests(IEnumerable<Entities.UserTest> tests)
        {
            throw new NotImplementedException();
        }

        public void PopulateProgramOfStudies(IEnumerable<ProgramofStudy> programOfStudies)
        {
            ControlHelper.PopulateProgramofStudy(ddlProgramofStudy, programOfStudies);
            HideProgramofStudy();
        }

        public void GenerateReport()
        {
            Presenter.GenerateReport();
        }

        public void RenderReport(IEnumerable<Entities.ReportTestsScheduledbyDate> reportData)
        {
            gvTestSchedule.DataSource = KTPSort.Sort<Entities.ReportTestsScheduledbyDate>(reportData, SortHelper.Parse(hdnGridConfig.Value));
            gvTestSchedule.DataBind();

            if (gvTestSchedule.Rows.Count == 0)
            {
                lblM.Visible = true;
                gvTestSchedule.Visible = false;
            }
            else
            {
                gvTestSchedule.Visible = true;
                lblM.Visible = false;
            }
        }

        public void GenerateReport(IEnumerable<Entities.ReportTestsScheduledbyDate> reportData, ReportAction printActions)
        {
            rpt.Load(Server.MapPath("~/Admin/Report/TestScheduleByDate.rpt"));
            rpt.SetDataSource(BuildDataSourceForReport(KTPSort.Sort<Entities.ReportTestsScheduledbyDate>(reportData, SortHelper.Parse(hdnGridConfig.Value))));

            switch (printActions)
            {
                case ReportAction.ExportExcel:
                    rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                    rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Page.Response, true, "TestScheduleByDate");
                    break;
                case ReportAction.ExportExcelDataOnly:
                    rpt.ReportDefinition.Sections[1].SectionFormat.EnableSuppress = true;
                    rpt.ReportDefinition.Sections["Section5"].SectionFormat.EnableSuppress = true;
                    rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Page.Response, true, "TestScheduleByDate");
                    break;
                case ReportAction.PDFPrint:
                    rpt.ReportDefinition.Sections[2].SectionFormat.EnableSuppress = true;
                    rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Page.Response, true, "TestScheduleByDate");
                    break;
                case ReportAction.ShowPreview:
                    CrystalReportViewer1.ReportSource = rpt;
                    CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.ActiveX;
                    break;
                case ReportAction.DirectPrint:
                    rpt.PrintToPrinter(1, false, 0, 0);
                    break;
            }
        }

        public bool IsProgramofStudyVisible
        {
            get { return trProgramofStudy.Visible; }
            set { trProgramofStudy.Visible = value; }
        }

        #endregion

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                imgDateFrom.Attributes.Add("onclick", "window.open('popupC.aspx?textbox=" + txtDateFrom.ClientID + "','cal','width=250,height=225,left=270,top=180')");
                imgDateTo.Attributes.Add("onclick", "window.open('popupC.aspx?textbox=" + txtDateTo.ClientID + "','cal','width=250,height=225,left=270,top=180')");
                imgDateFrom.Style.Add("cursor", "pointer");
                imgDateTo.Style.Add("cursor", "pointer");
            }
        }

        protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
        {
            if (ValidateDates())
            {
                if (lbxCohort.SelectedValue == "0")
                {
                    IsAllCohortsSelected = true;
                }

                if (lbxGroup.SelectedValue == "0")
                {
                    IsAllGroupSelected = true;
                }

                GenerateReport();
            }
        }

        protected void gvTestSchedule_Sorting(object sender, GridViewSortEventArgs e)
        {
            hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);
            GenerateReport();
            #region Change color of sorted column header

            switch (e.SortExpression)
            {
                case "InstitutionName":
                    gvTestSchedule.HeaderRow.Cells[0].BackColor = Color.Pink;
                    break;
                case "CohortName":
                    gvTestSchedule.HeaderRow.Cells[1].BackColor = Color.Pink;
                    break;
                case "StartDate":
                    gvTestSchedule.HeaderRow.Cells[2].BackColor = Color.Pink;
                    break;
                case "TestType":
                    gvTestSchedule.HeaderRow.Cells[3].BackColor = Color.Pink;
                    break;
                case "TestName":
                    gvTestSchedule.HeaderRow.Cells[4].BackColor = Color.Pink;
                    break;
                case "NumberOfStudents":
                    gvTestSchedule.HeaderRow.Cells[5].BackColor = Color.Pink;
                    break;
            }

            #endregion
        }

        protected void btnPrintExcel_Click(object sender, ImageClickEventArgs e)
        {
            GenerateReport(ReportAction.ExportExcel);
        }

        protected void btnPrintPDF_Click(object sender, ImageClickEventArgs e)
        {
            GenerateReport(ReportAction.PDFPrint);
        }

        protected void btnPrintExcelDataOnly_Click(object sender, ImageClickEventArgs e)
        {
            GenerateReport(ReportAction.ExportExcelDataOnly);
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            rpt.Close();
            rpt.Dispose();
        }

        private DataSet BuildDataSourceForReport(IEnumerable<Entities.ReportTestsScheduledbyDate> reportData)
        {
            TestScheduleByDate ds = new TestScheduleByDate();
            TestScheduleByDate.HeadRow rh = ds.Head.NewHeadRow();
            rh.InstitutionName = lbxInstitution.SelectedItem.Text.Equals("Select All") ? "All Institutions" : HeaderText(lbxInstitution.SelectedItemsText, 1);
            rh.CohortName = lbxCohort.SelectedItem.Text.Equals("Select All") ? "All Cohorts" : HeaderText(lbxCohort.SelectedItemsText, 2);
            if (!string.IsNullOrEmpty(lbxGroup.SelectedItemsText))
            {
                rh.Group = lbxGroup.SelectedItem.Text.Equals("Select All") ? "All Groups" : HeaderText(lbxGroup.SelectedItemsText, 3);
            }

            rh.TestType = lbxProducts.SelectedItemsText;
            rh.DateFrom = txtDateFrom.Text;
            rh.DateTo = txtDateTo.Text;
            rh.ReportName = "Tests Scheduled By Date";
            ds.Head.Rows.Add(rh);
            foreach (Entities.ReportTestsScheduledbyDate r in reportData)
            {
                TestScheduleByDate.TestScheduleByDateRow rd = ds._TestScheduleByDate.NewTestScheduleByDateRow();
                rd.InstitutionName = r.InstitutionName;
                rd.CohortName = r.CohortName;
                rd.StartDate = Convert.ToString(r.StartDate);
                rd.TestName = r.TestName;
                rd.TestType = r.TestType;
                rd.TestName = r.TestName;
                rd.NumberOfStudents = r.NumberOfStudents;
                rd.HeadId = rh.HeadId;
                ds._TestScheduleByDate.Rows.Add(rd);
            }

            return ds;
        }

        private void GenerateReport(ReportAction printAction)
        {
            if (ValidateDates())
            {
                if (lbxCohort.SelectedValue == "0")
                {
                    IsAllCohortsSelected = true;
                }

                if (lbxGroup.SelectedValue == "0")
                {
                    IsAllGroupSelected = true;
                }

                Presenter.GenerateReport(printAction);
            }
        }

        private bool ValidateDates()
        {
            bool isvalid = true;
            DateTime startDate;
            DateTime endDate;
            if (!string.IsNullOrEmpty(txtDateFrom.Text) && !DateTime.TryParse(txtDateFrom.Text, out startDate))
            {
                isvalid = false;
                lblErrorMsg.Text = WORNG_DATE_FORMAT;
            }
            else if (!string.IsNullOrEmpty(txtDateTo.Text) && !DateTime.TryParse(txtDateTo.Text, out endDate))
            {
                isvalid = false;
                lblErrorMsg.Text = WORNG_DATE_FORMAT;
            }
            else if (!string.IsNullOrEmpty(txtDateFrom.Text) && !string.IsNullOrEmpty(txtDateTo.Text) && txtDateFrom.Text.ToDateTime().Date > txtDateTo.Text.ToDateTime().Date)
            {
                isvalid = false;
                lblErrorMsg.Text = WORNG_START_TO_DATE;
            }

            return isvalid;
        }

        private string HeaderText(string name, int type)
        {
            if (name != null)
            {
                int count = RegularExpression.Regex.Matches(name, @"[,]+").Count;
                if (count > 1)
                {
                    switch (type)
                    {
                        case 1:
                            name = "Multiple Institutions";
                            break;
                        case 2:
                            name = "Multiple Cohorts";
                            break;
                        case 3:
                            name = "Multiple Groups";
                            break;
                    }
                }
            }

            return name;
        }

        private void HideProgramofStudy()
        {
            IsProgramofStudyVisible = Presenter.CurrentContext.UserType == UserType.SuperAdmin || Presenter.IsMultipleProgramofStudyAssignedToAdmin(Presenter.CurrentContext.UserId);
        }
    }
}