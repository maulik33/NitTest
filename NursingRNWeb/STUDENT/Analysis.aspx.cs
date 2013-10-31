using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

namespace STUDENT
{
    public partial class Analysis : StudentBasePage<IStudentAnalysisView, StudentAnalysisPresenter>, IStudentAnalysisView
    {
        private string strDataURL1;

        public string DataURL
        {
            get { return strDataURL1; }
        }

        public IEnumerable<ProgramResults> ProgramResults
        {
            get;
            set;
        }

        public string NumberCorrect
        {
            get
            {
                return lblNumberCorrect.Text.Trim();
            }
        }

        public void PopulateProducts(IEnumerable<Product> products)
        {
            ddProducts.DataSource = products;
            ddProducts.DataTextField = "ProductName";
            ddProducts.DataValueField = "ProductId";
            ddProducts.DataBind();
            lblName.Text = string.Format(@"{0}> Review Results", ddProducts.SelectedItem.Text);
            ControlHelper.AssignSelectedValue(ddProducts, Student.ProductId.ToString());
        }

        public void PopulateTestsByUser(IEnumerable<UserTest> userTests)
        {
            ddTests.DataSource = userTests;
            ddTests.DataTextField = "TestName";
            ddTests.DataValueField = "UserTestID";
            ddTests.DataBind();
            ControlHelper.AssignSelectedValue(ddTests, Student.UserTestId.ToString());
        }

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

        public void LoadTables_I(IEnumerable<Category> list_C, int probability, int percentileRank, bool probabilityExists, bool percentileRankExists)
        {
            int Percentile;

            int testID = Student.TestId;

            if (testID == 9 || testID == 82 || testID == 22 || testID == 83)
            {
                ////diagnoist and readness show probabilities.
                Percentile = probability;     
                lblRanking.Visible = false;            
                lblPR.Visible = false;
            }
            else
            {
                Percentile = percentileRank;
                lblRanking.Text = Percentile.ToString();
                lblRanking.Visible = true;
                lblPR.Visible = true;
            }

            if (Percentile > 0)
            {
                const int TW = 150;
                var T = new Table();
                var r = new TableRow();
                var c = new TableCell();
                var P = new Panel();
                PlaceHolder1.Controls.Add(T);
                T.Controls.Add(r);
                r.Controls.Add(c);
                c.Controls.Add(P);
                T.Width = new Unit(TW);
                T.Height = Unit.Pixel(20);
                T.Style.Add(HtmlTextWriterStyle.BorderStyle, "solid");
                T.Style.Add(HtmlTextWriterStyle.BorderColor, "#333366");
                T.Style.Add(HtmlTextWriterStyle.BorderWidth, "1");
                T.Style.Add("Margin-top", "4px");
                T.Style.Add("Margin-bootm", "8px");
                T.CellSpacing = 0;
                c.BackColor = ColorTranslator.FromHtml("#eef4fa");
                c.ToolTip = Percentile.ToString();
                c.Height = Unit.Pixel(18);
                P.BackImageUrl = "~/Temp/images/barv2.gif";
                P.Width = new Unit(TW * Percentile / 100);
                P.Height = Unit.Pixel(18);
            }
            else
            {
                if (testID == 9 || testID == 82 || testID == 22 || testID == 83)
                {
                    ////diagnoist and readness show probabilities.
                }
                else
                {
                    lblRanking.Text = percentileRankExists ? "0" : "N/A";
                }
            }

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

        public new void Init()
        {
            strDataURL1 = Server.UrlEncode("Graph.aspx?AType=1");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Init();
            if (!IsPostBack)
            {
                Presenter.OnViewInitialized();
            }

            Presenter.OnViewLoaded();

            if (IsPostBack)
            {
                //// Should not be called before OnViewLoaded()
                Presenter.SyncData(Request.Form["ddProducts"].ToInt(), Request.Form["ddTests"].ToInt());
            }
            #region Trace Information
            TraceHelper.Create(TraceToken, "Analysis Test")
                .Add("Product Id", Student.ProductId.ToString())
                .Add("User Test Id", Student.UserTestId.ToString())
                .Write();
            #endregion
            Presenter.RefreshData();
        }

        protected void ddProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////Presenter.OnDDProductsSelectedIndexChanged(ddProducts.SelectedValue);
        }

        protected void ddTests_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////Presenter.OnDDTestsSelectedIndexChanged(ddTests.SelectedValue);
        }

        protected Table Create_MainTable(string AType, int i)
        {
            int FirstColumnNumber;

            int NumberOfRecords = ProgramResults.Where(pg => pg.ChartType == AType).Count();

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

            ////first table-first row
            var tRow = new TableRow();
            var tCell = new TableCell { Width = Unit.Percentage(50) };
            Table InsideTable_1 = Create_InsideTable(AType, 0, FirstColumnNumber - 1, i);
            tCell.Controls.Add(InsideTable_1);
            tRow.Cells.Add(tCell);

            tCell = new TableCell();
            tRow.Cells.Add(tCell);
            tCell.Width = Unit.Percentage(50);
            Table InsideTable_2 = Create_InsideTable(AType, FirstColumnNumber, NumberOfRecords - 1, i);
            tCell.Controls.Add(InsideTable_2);
            tRow.Cells.Add(tCell);

            T1.Rows.Add(tRow);

            ////first table-second row
            tRow = new TableRow();
            tCell = new TableCell { ColumnSpan = 2 };

            var div = new HtmlGenericControl { InnerHtml = ReturnDiv(i) };

            tCell.Controls.Add(div);

            tRow.Cells.Add(tCell);
            T1.Rows.Add(tRow);
            return T1;
        }

        protected Table Create_InsideTable(string AType, int start, int end, int j)
        {
            var TB = new Table { Width = Unit.Percentage(100), BorderWidth = 0, CellPadding = 1, CellSpacing = 0 };

            TableRow tRow;
            TableCell tCell;
            HtmlGenericControl div;
            var programResults = ProgramResults.Where(pg => pg.ChartType == AType);
            for (int i = start; i <= end; i++)
            {
                ////first row with name od the category
                tRow = new TableRow();

                tCell = new TableCell();
                ProgramResults obj = programResults.ElementAtOrDefault(i);
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
                              InnerHtml = "<img src=\"../images/icon_corr.gif\" width=\"22\" height=\"16\">"
                          };
                tCell.CssClass = "status4";
                tCell.Controls.Add(div);
                tRow.Cells.Add(tCell);

                tCell = new TableCell { Text = @"n=", CssClass = "status2" };
                tRow.Cells.Add(tCell);

                TB.Rows.Add(tRow);

                ////second row with the rezults
                tRow = new TableRow();

                tCell = new TableCell { Text = obj.ItemText, Width = Unit.Percentage(75) };

                div = new HtmlGenericControl();
                int percentage = Convert.ToInt32(Math.Round(obj.Correct * 100.0 / obj.Total));

                string Cell_color = ReturnColor(j);
                tCell.BackColor = Color.FromName(Cell_color);
                div.InnerHtml = ReturnSeconDiv(percentage, j);

                tCell.Controls.Add(div);
                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                double percentagef = obj.Correct * 100.0 / obj.Total;
                tCell.Text = percentagef.ToString("f1") + "%";
                tCell.HorizontalAlign = HorizontalAlign.Center;
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

                ////third row - NORMING
                
                string norm = "0";
                norm = Convert.ToString(obj.DisplayNorm);
              
                if (!norm.Equals("0"))
                {
                    tRow = new TableRow();

                    tCell = new TableCell
                                {
                                    Text = norm.ToString(),
                                    Width = Unit.Percentage(75),
                                    HorizontalAlign = HorizontalAlign.Left
                                };
                    ////tCell.Text = obj.Percentage.ToString();

                    div = new HtmlGenericControl();

                    tCell.BackColor = Color.FromName("#eeeeee");
                    div.InnerHtml = "<img src=\"../Temp/images/barv4.gif\" width=\"" + norm + "%\" height=\"16\">";

                    tCell.Controls.Add(div);
                    tRow.Cells.Add(tCell);

                    tCell = new TableCell();
                    if (norm.Contains("."))
                    {
                        tCell.Text = norm + "%";
                    }
                    else
                    {
                        tCell.Text = norm + ".0%";
                    }

                    tCell.HorizontalAlign = HorizontalAlign.Center;
                    tRow.Cells.Add(tCell);

                    tCell = new TableCell();
                    tCell.Controls.Add(new LiteralControl("&nbsp;"));
                    tCell.HorizontalAlign = HorizontalAlign.Center;
                    tRow.Cells.Add(tCell);

                    tCell = new TableCell { Text = string.Empty, CssClass = "status3" };
                    tRow.Cells.Add(tCell);

                    tCell = new TableCell { Text = string.Empty, CssClass = "status1" };
                    tRow.Cells.Add(tCell);

                    TB.Rows.Add(tRow);
                }
            }

            return TB;
        }

        protected string ReturnDiv(int i)
        {
            string result = string.Empty;
            if (i % 3 == 1)
            {
                result = "<img src=\"../images/barv1.gif\" width=\"16\" height=\"18\"> &nbsp;Student %Correct &nbsp;&nbsp;&nbsp;<img src=\"../Temp/images/barv4.gif\" width=\"16\" height=\"18\">&nbsp;Normed %Correct &nbsp;&nbsp;&nbsp;<img src=\"../images/icon_corr.gif\" width=\"22\" height=\"16\"> Correct  &nbsp;&nbsp;&nbsp;&nbsp;<B>n=</B> Total";
            }

            if (i % 3 == 2)
            {
                result = "<img src=\"../images/barv2.gif\" width=\"16\" height=\"18\"> &nbsp;Student %Correct &nbsp;&nbsp;&nbsp;<img src=\"../Temp/images/barv4.gif\" width=\"16\" height=\"18\">&nbsp;Normed %Correct &nbsp;&nbsp;&nbsp;<img src=\"../images/icon_corr.gif\" width=\"22\" height=\"16\"> Correct  &nbsp;&nbsp;&nbsp;&nbsp;<B>n=</B> Total";
            }

            if (i % 3 == 0)
            {
                result = "<img src=\"../images/barv3.gif\" width=\"16\" height=\"18\">&nbsp;Student %Correct &nbsp;&nbsp;&nbsp;<img src=\"../Temp/images/barv4.gif\" width=\"16\" height=\"18\">&nbsp;Normed %Correct &nbsp;&nbsp;&nbsp;<img src=\"../images/icon_corr.gif\" width=\"22\" height=\"16\"> Correct  &nbsp;&nbsp;&nbsp;&nbsp;<B>n=</B> Total";
            }

            return result;
        }

        protected string ReturnColor(int i)
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

        protected string ReturnSeconDiv(int percentage, int i)
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

        protected void btnReview_Click(object sender, ImageClickEventArgs e)
        {
            Presenter.OnReviewBtnClick();
        }
    }
}
