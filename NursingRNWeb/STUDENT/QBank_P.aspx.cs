using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

// Yes, this has to be used!!
namespace STUDENT
{
    public partial class QBankP : StudentBasePage<IQBankPView, StudentQBankPPresenter>, IQBankPView
    {
        public string StrDataURL1 { get; set; }

        public string StrDataURL2 { get; set; }

        public string StrLength { get; set; }

        public string QBankProgramofStudyName
        {
            get { return Enum.GetName(typeof(ProgramofStudyType), Student.QBankProgramofStudyId); }
        }

        public void BindData(IEnumerable<ProgramResults> perfOverViewList, IEnumerable<ProgramResults> perfListWith4, IEnumerable<ProgramResults> perfListWith5)
        {
            StrDataURL1 = Server.UrlEncode("QBank_graph.aspx?AType=1");
            StrDataURL2 = Server.UrlEncode("QBank_graph.aspx?AType=2");

            if (perfOverViewList.Count() > 0)
            {
                lblNumberCorrect.Text = perfOverViewList.First().Correct.ToString();
                lblNumberIncorrect.Text = perfOverViewList.First().Incorrect.ToString();
                lblNotAnswered.Text = perfOverViewList.First().UnAnswered.ToString();
                lblC_I.Text = perfOverViewList.First().CorrectToIncorrect.ToString();
                lblI_C.Text = perfOverViewList.First().IncorrectToCorrect.ToString();
                lblI_I.Text = perfOverViewList.First().IncorrectToIncorrect.ToString();
            }

            C1.Visible = true;
            C2.Visible = true;
            C3.Visible = false;

            C1_Title.Visible = true;
            C2_Title.Visible = true;
            C3_Title.Visible = false;

            var T1 = CreateMainTable("N", 4, perfListWith4);
            C1.Controls.Add(T1);

            var T2 = CreateMainTable("N", 5, perfListWith5);
            C2.Controls.Add(T2);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Presenter.OnViewInitialized();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region Trace Information
                TraceHelper.WriteTraceEvent(TraceToken, "NCLEX-RN Prep > Cumulative performance");
                #endregion
                ////Presenter.OnViewInitialized();
            }

            Presenter.OnViewLoaded();
            StrLength = (Presenter.GetTestsNCLEXInfoForTheUser(3).Count() * 15).ToString();
        }

        protected void lb_Create_Click(object sender, EventArgs e)
        {
            Presenter.OnQBankCreateClick();
        }

        protected void lb_ListReview_Click(object sender, EventArgs e)
        {
            Presenter.OnListReviewClick();
        }

        protected void lb_Analysis_Click(object sender, EventArgs e)
        {
            Presenter.OnAnalysisClick();
        }

        protected void lb_Sample_Click(object sender, EventArgs e)
        {
            Presenter.OnSampleClick();
        }

        protected Table CreateMainTable(string PType, int AType, IEnumerable<ProgramResults> list)
        {
            C2.Visible = true;
            C3.Visible = true;
            int FirstColumnNumber;

            var NumberOfRecords = list.Count();

            if ((NumberOfRecords % 2) > 0)
            {
                FirstColumnNumber = (NumberOfRecords / 2) + 1;
            }
            else
            {
                FirstColumnNumber = NumberOfRecords / 2;
            }

            var T1 = new Table
                         {
                             Width = Unit.Percentage(100),
                             BorderWidth = 0,
                             CellPadding = 10,
                             CellSpacing = 0,
                             CssClass = "gdtable"
                         };

            // first table-first row
            var tRow = new TableRow();
            var tCell = new TableCell { Width = Unit.Percentage(50) };
            var InsideTable_1 = CreateInsideTable(PType, AType, 0, FirstColumnNumber - 1, list);
            tCell.Controls.Add(InsideTable_1);
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            tRow.Cells.Add(tCell);
            tCell.Width = Unit.Percentage(50);
            var InsideTable_2 = CreateInsideTable(PType, AType, FirstColumnNumber, NumberOfRecords - 1, list);
            tCell.Controls.Add(InsideTable_2);
            tRow.Cells.Add(tCell);

            T1.Rows.Add(tRow);

            // first table-second row
            tRow = new TableRow();
            tCell = new TableCell { ColumnSpan = 2 };

            var div = new HtmlGenericControl();

            if (PType == "N")
            {
                if (AType == 4)
                {
                    C1_Title.InnerHtml = "Client Needs";
                    div.InnerHtml = "<img src=\"../Temp/images/barv1.gif\" width=\"16\" height=\"18\"> %Correct &nbsp;&nbsp;&nbsp;<img src=\"../Temp/images/icon_corr.gif\" width=\"22\" height=\"16\"> Correct  &nbsp;&nbsp;&nbsp;&nbsp;<B>n=</B> Total";
                }

                if (AType == 5)
                {
                    C2_Title.InnerHtml = "Client Needs Category";
                    div.InnerHtml = "<img src=\"../Temp/images/barv2.gif\" width=\"16\" height=\"18\"> %Correct &nbsp;&nbsp;&nbsp;<img src=\"../Temp/images/icon_corr.gif\" width=\"22\" height=\"16\"> Correct  &nbsp;&nbsp;&nbsp;&nbsp;<B>n=</B> Total";
                }
            }

            tCell.Controls.Add(div);

            tRow.Cells.Add(tCell);
            T1.Rows.Add(tRow);
            return T1;
        }

        protected Table CreateInsideTable(string PType, int AType, int start, int end, IEnumerable<ProgramResults> list)
        {
            #region Trace Information
            TraceHelper.Create(TraceToken, "NCLEX-RN Prep > Cumulative performance : CreateInsideTable")
                .Add("AType ", AType.ToString())
                .Add("start ", start.ToString())
                .Add("end ", end.ToString())
                .Add("PType ", PType)
                .Write();
            #endregion

            var TB = new Table { Width = Unit.Percentage(100), BorderWidth = 0, CellPadding = 1, CellSpacing = 0 };
            TableRow tRow;
            TableCell tCell;
            HtmlGenericControl div;
            for (int i = start; i <= end; i++)
            {
                // first row with name od the category
                tRow = new TableRow();

                tCell = new TableCell { Text = list.ElementAt(i).ItemText, Width = Unit.Percentage(75) };

                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                tCell.Controls.Add(new LiteralControl("&nbsp;"));
                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                tCell.Controls.Add(new LiteralControl("&nbsp;"));
                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                div = new HtmlGenericControl
                          {
                              InnerHtml = "<img src=\"../Temp/images/icon_corr.gif\" width=\"22\" height=\"16\">"
                          };
                tCell.CssClass = "status4";
                tCell.Controls.Add(div);
                tRow.Cells.Add(tCell);

                tCell = new TableCell { Text = @"n=", CssClass = "status2" };
                tRow.Cells.Add(tCell);
                TB.Rows.Add(tRow);

                // second row with the rezults
                tRow = new TableRow();
                tCell = new TableCell { Text = list.ElementAt(i).ItemText, Width = Unit.Percentage(75) };
                div = new HtmlGenericControl();
                int percentage = list.ElementAt(i).Correct * 100 / list.ElementAt(i).Total;

                if (PType == "I")
                {
                    if (AType == 1)
                    {
                        tCell.BackColor = Color.FromName("#CC99FF");
                        div.InnerHtml = "<img src=\"../Temp/images/barv1.gif\" width=\"" + percentage + "%\" height=\"16\">";
                    }

                    if (AType == 2)
                    {
                        tCell.BackColor = Color.FromName("#99CCFF");
                        div.InnerHtml = "<img src=\"../Temp/images/barv2.gif\" width=\"" + percentage + "%\" height=\"16\">";
                    }

                    if (AType == 3)
                    {
                        tCell.BackColor = Color.FromName("#F7DBC0");
                        div.InnerHtml = "<img src=\"../Temp/images/barv3.gif\" width=\"" + percentage + "%\" height=\"16\">";
                    }
                }

                if (PType == "N")
                {
                    if (AType == 4)
                    {
                        tCell.BackColor = Color.FromName("#CC99FF");
                        div.InnerHtml = "<img src=\"../Temp/images/barv1.gif\" width=\"" + percentage + "%\" height=\"16\">";
                    }

                    if (AType == 5)
                    {
                        tCell.BackColor = Color.FromName("#99CCFF");
                        div.InnerHtml = "<img src=\"../Temp/images/barv2.gif\" width=\"" + percentage + "%\" height=\"16\">";
                    }

                    if (AType == 2)
                    {
                        tCell.BackColor = Color.FromName("#F7DBC0");
                        div.InnerHtml = "<img src=\"../Temp/images/barv3.gif\" width=\"" + percentage + "%\" height=\"16\">";
                    }
                }

                tCell.Controls.Add(div);
                tRow.Cells.Add(tCell);

                ////tCell = new TableCell
                ////            {
                ////                Text = string.Format(@"{0}%", percentage),
                ////                HorizontalAlign = HorizontalAlign.Center
                ////            };
                ////tRow.Cells.Add(tCell);

                tCell = new TableCell();
                double percentagef = list.ElementAt(i).Correct * 100.0 / list.ElementAt(i).Total;
                tCell.Text = percentagef.ToString("f1") + "%";
                tCell.HorizontalAlign = HorizontalAlign.Center;
                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                tCell.Controls.Add(new LiteralControl("&nbsp;"));
                tCell.HorizontalAlign = HorizontalAlign.Center;
                tRow.Cells.Add(tCell);

                tCell = new TableCell { Text = list.ElementAt(i).Correct.ToString(), CssClass = "status3" };
                tRow.Cells.Add(tCell);

                tCell = new TableCell { Text = list.ElementAt(i).Total.ToString(), CssClass = "status1" };
                tRow.Cells.Add(tCell);

                TB.Rows.Add(tRow);
            }

            return TB;
        }
    }
}
