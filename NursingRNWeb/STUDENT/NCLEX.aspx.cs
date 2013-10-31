using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

namespace STUDENT
{
    public partial class Nclex : StudentBasePage<IStudentNclexView, StudentNclexPresenter>, IStudentNclexView
    {
        public string QBankProgramofStudyName
        {
            get { return Enum.GetName(typeof (ProgramofStudyType), Student.QBankProgramofStudyId); }
        }

        public void EnableNClexLinks()
        {
            Label1.Text = Presenter.Student.IsNclexTest ? "1" : "0";

            // NCLEX Qbank links
            if (Student.IsQbankTest)
            {
                med_center_banner2_l.Visible = true;
                Table1.Visible = true;
            }
            else if (Student.IsQbankSampleTest)
            {
                med_center_banner2_l.Visible = true;
                Table1.Visible = true;
                Table1.Rows[0].Visible = false;
            }
            else
            {
                med_center_banner2_l.Visible = false;
                Table1.Visible = false;
            }

            // NCLEX Timed Qbank links
            if (Student.IsTimedQbankTest)
            {
                med_center_banner2_l_1.Visible = true;
                Table2.Visible = true;
            }
            else
            {
                med_center_banner2_l_1.Visible = false;
                Table2.Visible = false;
            }

            // NCLEX Diagnostic links
            TableRow6.Visible = Student.IsDignosticTest;

            // NCLEX Readiness links
            TableRow7.Visible = Student.IsReadinessTest;

            if (Student.IsDignosticResultTest)
            {
                TableRow r = CreateSvLinks(4, 6, "Go");
                r.BackColor = System.Drawing.Color.White;
                //Table5.Rows.Add(r);
            }

            if (Student.IsReadinessResultTest)
            {
                TableRow r = CreateSvLinks(4, 7, "Go");
                r.BackColor = System.Drawing.ColorTranslator.FromHtml("#EEEEEE");
                // Table5.Rows.Add(r);
            }
        }

        public void OnTestAssign(int productId, int testSubgroup)
        {
            #region Trace Information
            TraceHelper.Create(TraceToken, "NCLEX-RN Page : On Test Assign")
                .Add("product Id ", productId.ToString())
                .Add("Test Sub Group ", testSubgroup.ToString())
                .Write();
            #endregion
            TableRow r = CreateSvLinks(productId, testSubgroup, "Go");
            r.BackColor = (testSubgroup == 9) ? System.Drawing.ColorTranslator.FromHtml("#EEEEEE") : System.Drawing.Color.White;
            // Table5.Rows.Add(r);
        }

        public void CreateAvpContentLink()
        {
            IEnumerable<AvpContent> avpContents = Presenter.GetAvpContent(4, 10);
            var table = new HtmlTable();
            table.Attributes.Add("class", "nctable");
            table.Width = "100%";
            table.BgColor = "#ECE9D8";
            table.Style.Add("clear", "both");

            // temp counter
            int rowCount = 0;
            foreach (AvpContent content in avpContents)
            {
                rowCount++;
                var tableRow = new HtmlTableRow();
                var tableCell1 = new HtmlTableCell { BgColor = rowCount % 2 == 1 ? "#FFFFFF" : "#EEEEEE" };
                var testName = new Label { Text = content.TestName ?? string.Empty };
                var tableCell2 = new HtmlTableCell
                                               {
                                                   BgColor = rowCount % 2 == 1 ? "#FFFFFF" : "#EEEEEE",
                                                   Align = "right",
                                                   Width = "50%"
                                               };
                var contentUrl = new HyperLink { CssClass = "s2", Text = @"Go", NavigateUrl = "#" };

                contentUrl.Attributes.Add("onclick", string.Format(
                    "window.open('{0}','Nursing','width={1},height={2},status=yes,fullscreen=no,toolbar=no,menubar=no,location=no,resizable=yes')",
                    content.Url, content.PopWidth, content.PopHeight));

                table.Rows.Add(tableRow);
                tableRow.Cells.Add(tableCell1);
                tableRow.Cells.Add(tableCell2);
                tableCell1.Controls.Add(testName);
                tableCell2.Controls.Add(contentUrl);
            }

            AvpItems.Controls.Add(table);
        }

        // ReSharper disable InconsistentNaming
        protected void Page_Load(object sender, EventArgs e)
        // ReSharper restore InconsistentNaming
        {
            if (!IsPostBack)
            {
                #region Trace Information
                TraceHelper.WriteTraceEvent(TraceToken, "Navigated to NCLEX-RN Page.");
                #endregion
                Presenter.OnViewInitialized();
            }

            Presenter.OnViewLoaded();
        }

        protected void LbNclexClick(object sender, EventArgs e)
        {
            var lk = (LinkButton)sender;

            if (lk.CommandName == "Go_1_1")
            {
                Presenter.OnGo_1_1();
            }

            if (lk.CommandName == "Go_1_2")
            {
                Presenter.OnGo_1_2();
            }

            if (lk.CommandName == "Go_2_1")
            {
                Presenter.OnGo_2_1();
            }

            if (lk.CommandName == "Go_4_1")
            {
                Presenter.OnGo_4_1();
            }

            if (lk.CommandName == "Go_4_2")
            {
                Presenter.OnGo_4_2();
            }
        }
    }
}
