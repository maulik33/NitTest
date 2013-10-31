using System;
using System.Collections.Generic;
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
    public partial class QBank : StudentBasePage<IStudentQBankView, StudentQBankPresenter>, IStudentQBankView
    {
        public string UserHostAddress
        {
            get { return Request.UserHostAddress; }
        }

        public string HTTP_X_FORWARDED_FOR
        {
            get { return Request.ServerVariables["HTTP_X_FORWARDED_FOR"]; }
        }

        public string QBankProgramofStudyName
        {
            get { return Enum.GetName(typeof(ProgramofStudyType), Student.QBankProgramofStudyId); }
        }

        public int NumberOfCategory { get; set; }

        public int AllSubCategory { get; set; }

        public IEnumerable<ClientNeeds> ClientNeeds { get; set; }

        public IEnumerable<ClientNeedsCategory> ClientNeedsCategory { get; set; }

        public int TimedTest { get; set; }

        public int TutorMode { get; set; }

        public int ReuseMode { get; set; }

        public int NumberOfQuestions { get; set; }

        public string TimeRemaining { get; set; }

        public int SuspendQuestionNumber { get; set; }

        public int SuspendId { get; set; }

        public string TestName { get; set; }

        public int Correct { get; set; }

        public string Options { get; set; }

        public string CategoryList { get; set; }

        public void SetControls()
        {
            btnCreate.Attributes.Add("onClick", "return Validate('" + NumberOfCategory + "');");
            rblMode.Attributes.Add("onclick", "CalculateNum()");
            btnCreate.Enabled = true;
            if (Student.QBankProgramofStudyId == (int)ProgramofStudyType.RN)
            {
                lblMaxQuestion.Text = " (75 max):";
                RangeValidator1.MaximumValue = "75";
                RangeValidator1.ErrorMessage = "Number of questions should be between 1 and 75.";
            }
            else
            {
                lblMaxQuestion.Text = " (85 max):";
                RangeValidator1.MaximumValue = "85";
                RangeValidator1.ErrorMessage = "Number of questions should be between 1 and 85.";
            }
        }

        public void Create_MainTable()
        {
            AllSubCategory = 0;

            var T1 = new Table();
            Table T2;
            T1.Width = Unit.Percentage(100);
            T1.CellPadding = 10;
            T1.CellSpacing = 0;
            T1.BorderWidth = 0;

            TableRow tRow_2;
            TableCell tCell_2;

            var tRow = new TableRow();
            var tCell = new TableCell { Text = @"Main Category ", ColumnSpan = 2 };
            tRow.Cells.Add(tCell);
            tCell = new TableCell { Text = @"Sub Categories ", ColumnSpan = 2 };
            tRow.Cells.Add(tCell);

            T1.Rows.Add(tRow);

            int NumberOfSubCategories = 0;
            int NumberOfCategories = 0;
            foreach (var clientNeed in ClientNeeds)
            {
                NumberOfCategories = NumberOfCategories + 1;

                tRow = new TableRow();
                tCell = new TableCell { Width = Unit.Percentage(50), Text = clientNeed.Name };
                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                var ch = new CheckBox { ID = "ch_" + NumberOfCategories };
                ch.Attributes.Add("onclick", "EnableCategory(" + NumberOfCategories + ")");

                tCell.Controls.Add(ch);
                tRow.Cells.Add(tCell);

                tCell = new TableCell();
                var txs = new HtmlInputHidden
                              {
                                  ID = "txtCNumber_" + NumberOfCategories,
                                  Value = clientNeed.CategoryCount.ToString()
                              };

                tCell.Controls.Add(txs);
                tRow.Cells.Add(tCell);

                tCell = new TableCell { Width = Unit.Percentage(50) };

                if (ClientNeedsCategory != null)
                {
                    T2 = new Table { BorderWidth = 0, Width = Unit.Percentage(100) };
                    NumberOfSubCategories = 0;

                    ClientNeeds need = clientNeed;
                    IEnumerable<ClientNeedsCategory> ClientNeedsCategoryInfoForClinetNeedsCatID = from ClientNeedsCategory clientNeedsCategory in ClientNeedsCategory
                                                                                                  where clientNeedsCategory.Id == need.Id
                                                                                                  select clientNeedsCategory;

                    foreach (ClientNeedsCategory clientNeedsCategoryInfoForClinetNeedsCatID in ClientNeedsCategoryInfoForClinetNeedsCatID)
                    {
                        NumberOfSubCategories = NumberOfSubCategories + 1;
                        AllSubCategory = AllSubCategory + 1;
                        tRow_2 = new TableRow();
                        tCell_2 = new TableCell
                                      {
                                          Width = Unit.Percentage(80),
                                          Text = clientNeedsCategoryInfoForClinetNeedsCatID.CategoryName
                                      };

                        tRow_2.Cells.Add(tCell_2);
                        tCell_2 = new TableCell { Width = Unit.Percentage(20), HorizontalAlign = HorizontalAlign.Right };
                        var ch_sub = new CheckBox
                                         {
                                             ID = "ch_" + NumberOfCategories + "_" + NumberOfSubCategories
                                         };
                        ch_sub.Attributes.Add("onclick", "EnableSubCategory(" + NumberOfCategories + "," + NumberOfSubCategories + ")");

                        tCell_2.Controls.Add(ch_sub);
                        tRow_2.Cells.Add(tCell_2);

                        tCell_2 = new TableCell();
                        var tx = new HtmlInputHidden
                                     {
                                         ID = "txtAll" + NumberOfCategories + "_" + NumberOfSubCategories,
                                         Value = clientNeedsCategoryInfoForClinetNeedsCatID.TotQCount.ToString()
                                     };

                        tCell_2.Controls.Add(tx);
                        tRow_2.Cells.Add(tCell_2);

                        tCell_2 = new TableCell();
                        var tx1 = new HtmlInputHidden
                                      {
                                          ID = "txUnUsed" + NumberOfCategories + "_" + NumberOfSubCategories,
                                          Value = clientNeedsCategoryInfoForClinetNeedsCatID.UnUsedQCount.ToString()
                                      };

                        // tx1.Value = Convert.ToString(new cQuestions().ReturnNumberOfUnusedQuestionsInQBank(Convert.ToInt32(Session["UserID"].ToString()), Convert.ToInt32(dr2["ClientNeedCategoryID"].ToString())));
                        tCell_2.Controls.Add(tx1);
                        tRow_2.Cells.Add(tCell_2);

                        // Categories.Controls.Add(tx1); 
                        tCell_2 = new TableCell();
                        var tx2 = new HtmlInputHidden
                                      {
                                          ID = "txUnUsedIn" + NumberOfCategories + "_" + NumberOfSubCategories,
                                          Value =
                                              clientNeedsCategoryInfoForClinetNeedsCatID.UnUsedIncorrectQCount.ToString()
                                      };

                        tCell_2.Controls.Add(tx2);
                        tRow_2.Cells.Add(tCell_2);

                        tCell_2 = new TableCell();

                        var tx4 = new HtmlInputHidden
                                      {
                                          ID = "txtIn" + NumberOfCategories + "_" + NumberOfSubCategories,
                                          Value = clientNeedsCategoryInfoForClinetNeedsCatID.InCorrectQCount.ToString()
                                      };

                        tCell_2.Controls.Add(tx4);
                        tRow_2.Cells.Add(tCell_2);
                        tCell_2 = new TableCell();
                        var tx3 = new HtmlInputHidden
                                      {
                                          ID = "txtID" + NumberOfCategories + "_" + NumberOfSubCategories,
                                          Value = clientNeedsCategoryInfoForClinetNeedsCatID.CategoryId.ToString()
                                      };
                        tCell_2.Controls.Add(tx3);
                        tRow_2.Cells.Add(tCell_2);
                        T2.Rows.Add(tRow_2);
                    }

                    tCell.Controls.Add(T2);
                    tRow.Cells.Add(tCell);

                    T1.Rows.Add(tRow);
                }
            }

            Categories.Controls.Add(T1);
            txtNumberCat.Value = NumberOfCategories.ToString();
            txtNumberSubCat.Value = NumberOfSubCategories.ToString();

            chAll.Attributes.Add("onclick", "EnableAll(" + NumberOfCategories + ")");
        }

        public void GetTestDetails()
        {
            Student.ProductId = 4; // Hard Code since back button could override.
            if (txtQNumber.Text.Trim() != string.Empty)
            {
                NumberOfQuestions = Convert.ToInt32(txtQNumber.Text);
            }

            TimeRemaining = Convert.ToString(72 * NumberOfQuestions);
            if (rblStyle.Items[0].Selected)
            {
                TimedTest = 1;
                TutorMode = 0;
            }

            if (rblStyle.Items[1].Selected)
            {
                TimedTest = 0;
                TutorMode = 1;
            }

            if (rblMode.Items[0].Selected)
            {
                ReuseMode = 1;
            }

            if (rblMode.Items[1].Selected)
            {
                ReuseMode = 2;
            }

            if (rblMode.Items[2].Selected)
            {
                ReuseMode = 4;
            }

            if (rblMode.Items[3].Selected)
            {
                ReuseMode = 3;
            }

            CreateListOfCategories();
        }

        public string CreateListOfCategories()
        {
            var ArrayQ = new string[AllSubCategory];
            int loop = 0;
            int itest = 0;
            for (int i = 1; i <= NumberOfCategory; i++)
            {
                var tx = (HtmlInputHidden)FindControl("txtCNumber_" + i);
                if (tx != null)
                {
                    for (int j = 1; j <= Convert.ToInt32(tx.Value); j++)
                    {
                        var ch = (CheckBox)Categories.FindControl("ch_" + i + "_" + j);
                        if (ch != null)
                        {
                            if (ch.Checked)
                            {
                                var tx1 = (HtmlInputHidden)Categories.FindControl(string.Format("txtID{0}_{1}", i, j));
                                if (tx1 != null)
                                {
                                    ArrayQ[loop] = Convert.ToString(tx1.Value);
                                    loop = loop + 1;
                                    itest++;
                                }
                            }
                        }
                    }
                }
            }

            CategoryList = String.Join(",", ArrayQ, 0, itest);

            return CategoryList;
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
                TraceHelper.WriteTraceEvent(TraceToken, "Navigated to Question Bank Page.");
                #endregion
            }

            Presenter.OnViewLoaded();
        }

        protected void btnCreate_Click(object sender, ImageClickEventArgs e)
        {
            Presenter.OnBtnCreateClick(txtQNumber.Text);
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
    }
}
