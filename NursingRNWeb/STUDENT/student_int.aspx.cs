using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;
using System.Text;

namespace STUDENT
{
    public partial class StudentInt : StudentBasePage<IStudentIntView, StudentIntPresenter>, IStudentIntView
    {
        public void LoadProducts(IEnumerable<Product> products)
        {
            ddProducts.DataSource = products;
            ddProducts.DataTextField = "ProductName";
            ddProducts.DataValueField = "ProductID";
            ddProducts.DataBind();
            ControlHelper.AssignSelectedValue(ddProducts, Student.ProductId.ToString());
        }

        public void LoadTests(IEnumerable<UserTest> tests)
        {
            ddTests.DataSource = tests;
            ddTests.DataTextField = "TestName";
            ddTests.DataValueField = "UserTestId";
            ddTests.DataBind();
            ControlHelper.AssignSelectedValue(ddTests, Student.UserTestId.ToString());
        }

        public void UpdateReviewResultsHeading()
        {
            lblName.Text = string.Format("{0}> Review Results", ddProducts.SelectedItem.Text);
        }

        public void ShowTestResults(IEnumerable<UserQuestion> testQuestions, IEnumerable<Category> testCharacteristics,
            TestType testType, bool isTestId74)
        {
            FillGrid(testQuestions, testCharacteristics, testType, isTestId74);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Presenter.InitializePresenter();
            }

            Presenter.OnViewLoaded();

            if (IsPostBack)
            {
                // Should not be called before OnViewLoaded()
                Presenter.SyncData(Request.Form["ddProducts"].ToInt(), Request.Form["ddTests"].ToInt());
            }

            #region Trace Information
            TraceHelper.Create(TraceToken, "Navigated To Student Int Page.")
                .Add("Product Id", Student.ProductId.ToString())
                .Add("User Test Id", Student.UserTestId.ToString())
                .Write();
            #endregion

            Presenter.RefreshData();
        }

        protected void gvIntegrated_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var gv = (GridView)sender;
                int rowIndex = Convert.ToInt32(e.Row.RowIndex);
                string TopicTitle = gv.DataKeys[rowIndex].Values["TopicTitle"].ToString();
                string correct = gv.DataKeys[rowIndex].Values["Correct"].ToString();

                var lbe = (LinkButton)e.Row.FindControl("lbE");
                ////lbe.Text = ddProducts.SelectedValue == "1" ? TopicTitle : "View Explanation";
                if (ddProducts.SelectedValue.ToInt() == (int)ProductType.IntegratedTesting)
                {
                    lbe.Text = TopicTitle;
                }
                else if (ddProducts.SelectedValue.ToInt() == (int)ProductType.SkillsModules)
                {
                    lbe.Text = "Review Question";
                }
                else
                {
                    lbe.Text = "View Explanation";
                }

                lbe.CommandName = "Explanation";
                lbe.CssClass = "s2";

                var im_correct = (Image)e.Row.FindControl("im_correct");
                if (im_correct != null)
                {
                    if (correct.Trim() == "1")
                    {
                        im_correct.ImageUrl = "../images/icon_corr.gif";
                    }

                    if (correct.Trim().Trim() == "0")
                    {
                        im_correct.ImageUrl = "../images/incorrect.gif";
                    }

                    if (correct.Trim().Trim() == "2")
                    {
                        im_correct.ImageUrl = "../images/skip.gif";
                    }
                }

                #region Build dynamic columns (using smart cache)
                Dictionary<string, int> dicCategoryNameId = new Dictionary<string, int>();
                foreach (string categoryNameId in hfcategories.Value.Split('|'))
                {
                    if (categoryNameId.Contains(":"))
                    {
                        dicCategoryNameId.Add(categoryNameId.Split(':')[0], categoryNameId.Split(':')[1].ToInt());
                    }
                }

                for (int i = 0; i < gv.Columns.Count; i++)
                {
                    if (gv.Columns[i] is BoundField)
                    {
                        int index;
                        var bf = (BoundField)gv.Columns[i];
                        if (bf.DataField != "QuestionNumber"
                            && bf.DataField != "TimeSpendForQuestion"
                            && bf.DataField != "QuestionID")
                        {
                            string categoryName = string.Empty;
                            if (Int32.TryParse(e.Row.Cells[i].Text, out index)
                                && index > 0)
                            {
                                int categoryId = dicCategoryNameId[bf.DataField];
                                categoryName = Presenter.GetCategoryDescription(categoryId, index);
                            }

                            e.Row.Cells[i].Text = categoryName;
                        }
                    }
                }
                #endregion
            }
        }

        protected void gvIntegrated_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var gv = (GridView)sender;
            var row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;

            if (gv != null)
            {
                string QID = gv.DataKeys[row.RowIndex].Values["QID"].ToString();
                string item = gv.DataKeys[row.RowIndex].Values["TypeOfFileID"].ToString();
                string RID = gv.DataKeys[row.RowIndex].Values["RemediationID"].ToString();

                switch (e.CommandName)
                {
                    case "Explanation":
                        Presenter.NavigateToIntro(StudentIntPresenter.RowCommand.Explanation, QID, item, RID);
                        break;
                    case "Explanation1":
                        Presenter.NavigateToIntro(StudentIntPresenter.RowCommand.Explanation1, QID, item, RID);
                        break;
                }
            }
        }

        protected void btnAnalyze_Click(object sender, ImageClickEventArgs e)
        {
            Presenter.NavigateToAnalysis();
        }

        protected void FillGrid(IEnumerable<UserQuestion> testQuestions, IEnumerable<Category> testCharacteristics,
                                TestType testType, bool isTestId74)
        {
            gvIntegrated.AutoGenerateColumns = false;
            gvIntegrated.Columns.Clear();

            var QNBoundField = new BoundField { DataField = "QuestionNumber", HeaderText = @"Q#" };
            gvIntegrated.Columns.Add(QNBoundField);

            var tc1 = new TemplateField
                          {
                              HeaderTemplate = new
                                  GridViewTemplate(ListItemType.Header, "Correct?", 0),
                              ItemTemplate = new
                                  GridViewTemplate(ListItemType.Item, string.Empty, 1)
                          };
            gvIntegrated.Columns.Add(tc1);

            var SecondsUsedBoundField = new BoundField
                                            {
                                                DataField = "TimeSpendForQuestion",
                                                HeaderText = @"Seconds Used"
                                            };
            gvIntegrated.Columns.Add(SecondsUsedBoundField);
            StringBuilder categoryIds = new StringBuilder();
            foreach (Category category in testCharacteristics)
            {
                string item = category.TableName.Trim();
                categoryIds.Append(item + ":" + category.CategoryID + "|");
                if (item == "ClientNeeds")
                {
                    var ClientNeedsBoundField = new BoundField { DataField = "ClientNeeds", HeaderText = @"Client Needs" };
                    gvIntegrated.Columns.Add(ClientNeedsBoundField);
                }

                if (item == "ClientNeedCategory")
                {
                    var ClientNeedCategoryBoundField = new BoundField
                                                           {
                                                               DataField = "ClientNeedCategory",
                                                               HeaderText = @"Client Needs Category",
                                                           };
                    gvIntegrated.Columns.Add(ClientNeedCategoryBoundField);
                }

                if (item == "Demographic")
                {
                    var DemographicBoundField = new BoundField
                                                    {
                                                        DataField = "Demographic",
                                                        HeaderText = @"Demographics"
                                                    };
                    gvIntegrated.Columns.Add(DemographicBoundField);
                }

                if (item == "ClinicalConcept")
                {
                    var ClinicalConceptBoundField = new BoundField
                                                        {
                                                            DataField = "ClinicalConcept",
                                                            HeaderText = @"Clinical Concepts"
                                                        };
                    gvIntegrated.Columns.Add(ClinicalConceptBoundField);
                }

                if (item == "NursingProcess")
                {
                    var NursingProcessBoundField = new BoundField
                                                       {
                                                           DataField = "NursingProcess",
                                                           HeaderText = @"Nursing Process"
                                                       };
                    gvIntegrated.Columns.Add(NursingProcessBoundField);
                }

                if (item == "LevelOfDifficulty")
                {
                    var AutoGenLevelOfDifficultyBoundField = new BoundField
                                                                 {
                                                                     DataField = "LevelOfDifficulty",
                                                                     HeaderText = @"Level of Difficulty"
                                                                 };
                    gvIntegrated.Columns.Add(AutoGenLevelOfDifficultyBoundField);
                }

                if (item == "CriticalThinking")
                {
                    var CriticalThinkingBoundField = new BoundField
                                                         {
                                                             DataField = "CriticalThinking",
                                                             HeaderText = @"Critical Thinking"
                                                         };
                    gvIntegrated.Columns.Add(CriticalThinkingBoundField);
                }

                if (item == "CognitiveLevel")
                {
                    var CognitiveLevelBoundField = new BoundField
                                                       {
                                                           DataField = "CognitiveLevel",
                                                           HeaderText = @"Cognitive Level"
                                                       };
                    gvIntegrated.Columns.Add(CognitiveLevelBoundField);
                }

                if (item == "SpecialtyArea")
                {
                    var SpecialtyAreaBoundField = new BoundField
                                                      {
                                                          DataField = "SpecialtyArea",
                                                          HeaderText = @"Specialty Area"
                                                      };
                    gvIntegrated.Columns.Add(SpecialtyAreaBoundField);
                }

                if (item == "Systems")
                {
                    var SystemsBoundField = new BoundField { DataField = "Systems", HeaderText = @"Systems" };
                    gvIntegrated.Columns.Add(SystemsBoundField);
                }

                if (item == "AccreditationCategories")
                {
                    var AccreditationCategoriesBoundField = new BoundField { DataField = "AccreditationCategories", HeaderText = @"Accreditation Categories" };
                    gvIntegrated.Columns.Add(AccreditationCategoriesBoundField);
                }

                if (item == "QSENKSACompetencies")
                {
                    var QSENKSACompetenciesBoundField = new BoundField { DataField = "QSENKSACompetencies", HeaderText = @"QSEN KSA Competencies" };
                    gvIntegrated.Columns.Add(QSENKSACompetenciesBoundField);
                }

                if (item == "Concepts")
                {
                    var NursingProcessBoundField = new BoundField
                    {
                        DataField = "Concepts",
                        HeaderText = @"Concepts"
                    };
                    gvIntegrated.Columns.Add(NursingProcessBoundField);
                }
            }
            hfcategories.Value = categoryIds.ToString().TrimEnd('|');
            ////if (Session["TestID"].ToString() == "74")
            if (isTestId74)
            {   ////change later not to be hardcoded !!!!!
                ////only for QBank
                var QuestionIDConceptBoundField = new BoundField { DataField = "QuestionID", HeaderText = @"Q.ID" };
                gvIntegrated.Columns.Add(QuestionIDConceptBoundField);
            }

            var tc2 = new TemplateField();
            string title = string.Empty;
            if (testType == TestType.Integrated)
            {
                title = "Remediation";
            }
            else if (testType == TestType.SkillsModules)
            {
                title = string.Empty;
            }
            else
            {
                title = "Explanation";
            }

            tc2.HeaderTemplate = new
                GridViewTemplate(ListItemType.Header, title, 0);
            tc2.ItemTemplate = new
                GridViewTemplate(ListItemType.Item, string.Empty, 2);

            gvIntegrated.Columns.Add(tc2);
            gvIntegrated.DataSource = testQuestions;
            gvIntegrated.DataBind();
        }
    }
}
