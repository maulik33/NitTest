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

namespace STUDENT
{
    public partial class QBankRDetails : StudentBasePage<IStudentQBankRDetailsView, StudentQBankRDetailsPresenter>, IStudentQBankRDetailsView
    {
        #region Properties

        public IEnumerable<ProgramResults> ProgramResults
        {
            get;
            set;
        }

        public string strDataURL1 { get; set; }

        public int UserTestID
        {
            get { return Convert.ToInt32(Request.QueryString["id"]); }
        }

        #endregion Properties

        #region IStudentQBankRDetails Methods

        public void BindData(ProgramResults programResult)
        {
            strDataURL1 = Server.UrlEncode("Graph.aspx?AType=1");
            lblNumberCorrect.Text = programResult.Correct.ToString();
            lblNumberIncorrect.Text = programResult.Incorrect.ToString();
            lblNotAnswered.Text = programResult.UnAnswered.ToString();
            lblC_I.Text = programResult.CorrectToIncorrect.ToString();
            lblI_C.Text = programResult.IncorrectToCorrect.ToString();
            lblI_I.Text = programResult.IncorrectToIncorrect.ToString();
        }
        #endregion IStudentQBankRDetails Methods

        public void LoadTables_N(IEnumerable<Category> list_C)
        {
            int i = 0;

            foreach (Category item in list_C)
            {
                i++;
                var C1_Title = new HtmlGenericControl
                                   {
                                       InnerHtml =
                                           "<div id='med_center_banner5' style='padding-left:15px;'>" +
                                           ReturnName(item.TableName.Trim()) + "</div>"
                                   };

                D_Graph.Controls.Add(C1_Title);
                Table T1 = Create_MainTable(item.TableName.Trim(), i);
                D_Graph.Controls.Add(T1);
                D_Graph.Controls.Add(new LiteralControl("</br>"));
            }
        }

        #region Event Handlers
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region Trace Information
                TraceHelper.WriteTraceEvent(TraceToken, "NCLEX-RN Prep > Previous Tests");
                #endregion
                Presenter.OnViewInitialized();
            }

            Presenter.OnViewLoaded();
        }

        protected void lb_Create_Click(object sender, EventArgs e)
        {
            Presenter.OnlbCreateClick();
        }

        protected void lb_ListReview_Click(object sender, EventArgs e)
        {
            Presenter.OnlbListReviewClick();
        }

        protected void lb_Analysis_Click(object sender, EventArgs e)
        {
            Presenter.OnlbAnalysisClick();
        }

        protected void lb_Sample_Click(object sender, EventArgs e)
        {
            Presenter.OnlbSampleClick();
        }

        protected void btnReview_Click(object sender, ImageClickEventArgs e)
        {
            Presenter.OnbtnReviewClicks();
        }

        #endregion Event Handlers

        private static string ReturnDiv(int i)
        {
            string result = string.Empty;
            if (i % 3 == 1)
            {
                result = "<img src=\"../Temp/images/barv1.gif\" width=\"16\" height=\"18\"> %Correct &nbsp;&nbsp;&nbsp;<img src=\"../Temp/images/icon_corr.gif\" width=\"22\" height=\"16\"> Correct  &nbsp;&nbsp;&nbsp;&nbsp;<B>n=</B> Total";
            }

            if (i % 3 == 2)
            {
                result = "<img src=\"../Temp/images/barv2.gif\" width=\"16\" height=\"18\"> %Correct &nbsp;&nbsp;&nbsp;<img src=\"../Temp/images/icon_corr.gif\" width=\"22\" height=\"16\"> Correct  &nbsp;&nbsp;&nbsp;&nbsp;<B>n=</B> Total";
            }

            if (i % 3 == 0)
            {
                result = "<img src=\"../Temp/images/barv3.gif\" width=\"16\" height=\"18\"> %Correct &nbsp;&nbsp;&nbsp;<img src=\"../Temp/images/icon_corr.gif\" width=\"22\" height=\"16\"> Correct  &nbsp;&nbsp;&nbsp;&nbsp;<B>n=</B> Total";
            }

            return result;
        }

        private static string ReturnColor(int i)
        {
            string result = string.Empty;
            if (i % 3 == 1)
            {
                result = "#CC99FF";
            }

            if (i % 3 == 2)
            {
                result = "#99CCFF";
            }

            if (i % 3 == 0)
            {
                result = "#F7DBC0";
            }

            return result;
        }

        private static string ReturnSeconDiv(int percentage, int i)
        {
            string result = string.Empty;
            if (i % 3 == 1)
            {
                result = "<img src=\"../Temp/images/barv1.gif\" width=\"" + percentage + "%\" height=\"16\">";
            }

            if (i % 3 == 2)
            {
                result = "<img src=\"../Temp/images/barv2.gif\" width=\"" + percentage + "%\" height=\"16\">";
            }

            if (i % 3 == 0)
            {
                result = "<img src=\"../Temp/images/barv3.gif\" width=\"" + percentage + "%\" height=\"16\">";
            }

            return result;
        }

        private static string ReturnName(string str)
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

            return f_name;
        }

        private Table Create_MainTable(string aType, int i)
        {
            var T1 = new Table();

            int NumberOfRecords = ProgramResults.Where(pr => pr.ChartType == aType).Count();

            int FirstColumnNumber;
            if ((NumberOfRecords % 2) > 0)
            {
                FirstColumnNumber = (NumberOfRecords / 2) + 1;
            }
            else
            {
                FirstColumnNumber = NumberOfRecords / 2;
            }

            T1.Width = Unit.Percentage(100);
            T1.BorderWidth = 0;
            T1.CellPadding = 10;
            T1.CellSpacing = 0;

            // first table-first row
            var tRow = new TableRow();
            var tCell = new TableCell { Width = Unit.Percentage(50) };
            Table InsideTable_1 = Create_InsideTable(aType, 0, FirstColumnNumber - 1, i);
            tCell.Controls.Add(InsideTable_1);
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            tRow.Cells.Add(tCell);
            tCell.Width = Unit.Percentage(50);
            Table InsideTable_2 = Create_InsideTable(aType, FirstColumnNumber, NumberOfRecords - 1, i);
            tCell.Controls.Add(InsideTable_2);
            tRow.Cells.Add(tCell);

            T1.Rows.Add(tRow);

            // first table-second row
            tRow = new TableRow();
            tCell = new TableCell { ColumnSpan = 2 };

            var div = new HtmlGenericControl { InnerHtml = ReturnDiv(i) };
            tCell.Controls.Add(div);
            tRow.Cells.Add(tCell);
            T1.Rows.Add(tRow);
            return T1;
        }

        private Table Create_InsideTable(string aType, int start, int end, int j)
        {
            #region Trace Information
            TraceHelper.Create(TraceToken, "NCLEX-RN Prep > Previous Tests : Create_InsideTable")
                .Add("aType ", aType)
                .Add("start ", start.ToString())
                .Add("end ", end.ToString())
                .Add("j ", j.ToString())
                .Write();
            #endregion
            var TB = new Table { Width = Unit.Percentage(100), BorderWidth = 0, CellPadding = 1, CellSpacing = 0 };

            TableRow tRow;
            TableCell tCell;
            HtmlGenericControl div;
            var programResults = ProgramResults.Where(pr => pr.ChartType == aType);
            for (var i = start; i <= end; i++)
            {
                // first row with name od the category
                tRow = new TableRow();

                tCell = new TableCell();
                var obj = programResults.ElementAtOrDefault(i);
                tCell.Text = obj.ItemText.Trim();
                tCell.Width = Unit.Percentage(75);
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

                tCell = new TableCell { Text = obj.ItemText, Width = Unit.Percentage(75) };

                div = new HtmlGenericControl();
                int percentage = obj.Correct * 100 / obj.Total;

                string Cell_color = ReturnColor(j);
                tCell.BackColor = Color.FromName(Cell_color);
                div.InnerHtml = ReturnSeconDiv(percentage, j);

                tCell.Controls.Add(div);
                tRow.Cells.Add(tCell);

                tCell = new TableCell { Text = string.Format("{0}%", percentage), HorizontalAlign = HorizontalAlign.Center };
                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                tCell.Controls.Add(new LiteralControl("&nbsp;"));
                tCell.HorizontalAlign = HorizontalAlign.Center;
                tRow.Cells.Add(tCell);

                tCell = new TableCell { Text = obj.Correct.ToString(), CssClass = "status3" };
                tRow.Cells.Add(tCell);

                tCell = new TableCell { Text = obj.Total.ToString(), CssClass = "status1" };
                tRow.Cells.Add(tCell);

                TB.Rows.Add(tRow);
            }

            return TB;
        }
    }
}