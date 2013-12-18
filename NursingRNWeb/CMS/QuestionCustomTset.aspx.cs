using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

namespace NursingRNWeb.CMS
{
    public partial class QuestionCustomTset : PageBase<IQuestionCustomTestView, QuestionCustomTestPresenter>, IQuestionCustomTestView
    {
        public int TestId
        {
            get
            {
                if (ViewState["TestId"] == null)
                {
                    return -1;
                }
                else
                {
                    return ViewState["TestId"].ToInt();
                }
            }

            set
            {
                ViewState["TestId"] = value;
            }
        }



        public int ProgramofStudyId
        {
            get
            {
                return hfProgramofStudyId.Value.ToInt();
            }
            set
            {
                hfProgramofStudyId.Value = value.ToString();
            }
        }

        public int ProductId { get; set; }

        public override void PreInitialize()
        {
            // throw new NotImplementedException();
        }

        #region IQuestionCustomTestView members

        public void RenderQuestionCustomTest(Test test, IEnumerable<QuestionResult> questionResult)
        {
            if (test != null)
            {
                txtTestName.Text = test.TestName;
                hdnSecondsPerquestion.Value = Convert.ToString(test.SecondPerQuestion);
                hdnDefaultGroup.Value = test.DefaultGroup;
            }

            lbQuestions.Items.Clear();
            foreach (QuestionResult qr in questionResult)
            {
                HiddenField H = new HiddenField();
                H.Value = Server.HtmlEncode(qr.Stem);
                Ph1.Controls.Add(H);

                ListItem it = new ListItem(qr.QuestionID, qr.QID + "|" + H.ClientID);
                lbQuestions.Items.Add(it);
            }

            lblQuestionsTobeIncluded.Text = "Number of questions to be included: 0";
            lblQuestionIncluded.Text = "Number of questions included: " + lbQuestions.Items.Count;
            btnAddAllQuestions.Attributes.Add("onclick", "return confirm('Are you sure that you want to add all listed question into this test?')");
            btnRemoveAllQuestions.Attributes.Add("onclick", "return confirm('Are you sure that you want to remove all question from this test?')");
        }

        public void PopulateSearchCriteria(IEnumerable<Product> products, IEnumerable<Topic> titles, IDictionary<CategoryName, Category> categoryData)
        {
            ControlHelper.PopulateProducts(ddTestType, products);
            ControlHelper.PopulateTopicTitle(ddTopicTitle, titles);
            ddTest.ClearData();
            ucSubCategories.PopulateSubCategories(categoryData, hfProgramofStudyId.Value.ToInt());
            if (ProductId != 0 && ddTestCategory.Items.Count > 0)
            {
                ddTestCategory.SelectedValue = ProductId.ToString();
            }
        }

        public void PopulateClientNeedCategories(IDictionary<int, CategoryDetail> categories)
        {
            ucSubCategories.PopulateClientNeedCategories(categories);
        }

        public void PopulateTests(IEnumerable<Test> tests)
        {
            ControlHelper.PopulateTests(ddTest, tests);
        }

        #endregion

        public void PopulateProgramofStudyParameters(IEnumerable<Product> products, IEnumerable<Topic> topics)
        {
            if (hfProgramofStudyId.Value.ToInt() == (int)ProgramofStudyType.RN)
            {
                lblProgramofStudyVal.Text = ProgramofStudyType.RN.ToString();
            }
            else
            {
                lblProgramofStudyVal.Text = ProgramofStudyType.PN.ToString();
            }
            ControlHelper.PopulateTopicTitle(ddTopicTitle, topics);
            ControlHelper.PopulateProducts(ddTestType, products);
            ControlHelper.PopulateProducts(ddTestCategory, products);
            Presenter.ShowCustomTestDetails(hfProgramofStudyId.Value.ToInt());
            ucSubCategories.SetControlVisibility(hfProgramofStudyId.Value);
        }

        public void PopulateClientNeedsCategory(IDictionary<int, CategoryDetail> clientNeedsCategories)
        {
            ucSubCategories.PopulateClientNeedCategories(clientNeedsCategories);
        }

        public void UpdateQuestionListInTest(int TestId)
        {
            List<ListItem>[] A = new List<ListItem>[6];
            A[0] = new List<ListItem>();
            A[1] = new List<ListItem>();
            A[2] = new List<ListItem>();
            A[3] = new List<ListItem>();
            A[4] = new List<ListItem>();
            int[] I = { 0, 0, 0, 0, 0 };

            for (int j = 0; j <= lbQuestions.Items.Count - 1; j++)
            {
                int QID = Convert.ToInt32(lbQuestions.Items[j].Value.Split('|')[0]);
                Question question = Presenter.GetQuestionByQid(QID);
                string QuestionType = string.Empty;

                if (question != null)
                {
                    QuestionType = question.TypeOfFileId;
                }

                switch (QuestionType)
                {
                    case "00":
                        // Disclamer
                        A[0].Add(lbQuestions.Items[j]);
                        break;
                    case "01":
                        // Intro
                        A[1].Add(lbQuestions.Items[j]);
                        break;
                    case "02":
                        // Tutorial Item
                        A[2].Add(lbQuestions.Items[j]);
                        break;
                    case "03":
                        // Question
                        A[3].Add(lbQuestions.Items[j]);
                        break;
                    case "04":
                        // End Item
                        A[4].Add(lbQuestions.Items[j]);
                        break;
                }
            }

            List<TestQuestion> testQuestions = new List<TestQuestion>();
            for (int k = 0; k <= 4; k++)
            {
                foreach (ListItem it in A[k])
                {
                    TestQuestion testQuestion = new TestQuestion();
                    I[k] += 1;
                    testQuestion.TestId = TestId;
                    testQuestion.QId = Convert.ToInt32(it.Value.Split('|')[0]);
                    testQuestion.QuestionId = it.Text;
                    testQuestion.QuestionNumber = I[k];
                    testQuestions.Add(testQuestion);
                }
            }

            Presenter.DeleteTestQuestions(TestId);
            Presenter.SaveTestQuestion(testQuestions);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ucSubCategories.OnClientNeedsChange += new EventHandler<ItemSelectedEventArgs>(ucSubCategories_OnClientNeedsChange);

            if (!IsPostBack)
            {
                Presenter.InitializeProgramOfStudyParameter();
                #region Trace Information
                TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to Edit Custom Test Page")
                                    .Add("Test Id", TestId.ToString())
                                    .Write();
                #endregion
            }
            else
            {
                RefreshListBox();
            }

            lbSelectedQuestions.Attributes.Add("onchange", "ShowStem(this);");
            lbQuestions.Attributes.Add("onchange", "ShowStem(this);");
        }

        protected void btnSearch_Click(object sender, System.EventArgs e)
        {
            #region Trace Information
            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Search Custom Test Page")
                                .Add("Test Id", TestId.ToString())
                                .Write();
            #endregion
            QuestionCriteria obj = new QuestionCriteria();
            obj.ProgramOfStudy = hfProgramofStudyId.Value.ToInt();
            obj.Product = ControlHelper.GetSelectedValue(ddTestType, "0").ToInt();
            obj.Test = ControlHelper.GetSelectedValue(ddTest, "0").ToInt();
            obj.ClientNeed = GetSelectedValue(ucSubCategories.ClientNeedsValue, "0").ToInt();
            obj.ClientNeedsCategory = GetSelectedValue(ucSubCategories.ClientNeedsCategoryValue, "0");
            obj.ClinicalConcept = GetSelectedValue(ucSubCategories.ClinicalConceptsValue, "0");
            obj.CognitiveLevel = GetSelectedValue(ucSubCategories.CognitiveLevel, "0");
            obj.CriticalThinking = GetSelectedValue(ucSubCategories.CriticalThinkingValue, "0");
            obj.Demographic = GetSelectedValue(ucSubCategories.DemographyValue, "0");
            obj.LevelOfDifficulty = GetSelectedValue(ucSubCategories.LevelOfDifficultyValue, "0");
            obj.NursingProcess = GetSelectedValue(ucSubCategories.NursingProcessValue, "0");
            obj.Remediation = ControlHelper.GetSelectedValue(ddTopicTitle, "0").ToInt();
            obj.SpecialtyArea = GetSelectedValue(ucSubCategories.SpecialityAreaValue, "0");
            obj.System = GetSelectedValue(ucSubCategories.SystemValue, "0");
            obj.AccreditationCategories = GetSelectedValue(ucSubCategories.AccreditationCategoriesValue, "0");
            obj.QSENKSACompetencies = GetSelectedValue(ucSubCategories.QSENKSACompetenciesValue, "0");
            obj.Concepts = GetSelectedValue(ucSubCategories.ConceptsValue, "0");
            obj.QuestionID = txtQuestionID.Text;
            obj.Text = txtText.Text;
            obj.ItemType = ddTypeOfFile.SelectedValue;
            obj.Qtype = ddQuestionType.SelectedValue;
            obj.Active = ControlHelper.GetSelectedValue(ddActive, "0").ToInt();

            IEnumerable<QuestionResult> searchResult = Presenter.SerachQuestion(obj);
            List<QuestionResult> questionResults = Scramble(searchResult, DropDownList1.SelectedValue.ToInt());

            lbSelectedQuestions.Items.Clear();
            foreach (QuestionResult r in questionResults)
            {
                HiddenField H = new HiddenField();
                H.Value = r.Stem;
                Ph1.Controls.Add(H);

                ListItem it = new ListItem(r.QuestionID, r.QID + "|" + H.ClientID);
                lbSelectedQuestions.Items.Add(it);
            }

            lblQuestionsTobeIncluded.Text = "Number of questions to be included: " + lbSelectedQuestions.Items.Count;
            lblQuestionIncluded.Text = "Number of questions included: " + lbQuestions.Items.Count;
        }

        protected void btnAddQuestions_Click(object sender, System.EventArgs e)
        {
            #region Trace Information
            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to Edit Custom Test Page : Add Question Clicked")
                                .Add("Question Ids", lbQuestions.SelectedValue)
                                .Write();
            #endregion
            ArrayList dAr = new ArrayList();
            bool ItemExist = false;
            foreach (ListItem L in lbSelectedQuestions.Items)
            {
                if (lbQuestions.Items.Count >= 265)
                {
                    Messenger1.Message.Add("This test has 265 questions already. Can not add more.");
                    break;
                }

                if (L.Selected)
                {
                    ItemExist = false;
                    foreach (ListItem T in lbQuestions.Items)
                    {
                        if (L.Value.Split('|')[0] == T.Value.Split('|')[0])
                        {
                            Messenger1.Message.Add("Question " + L.Text + " in this test already!");
                            ItemExist = true;
                            break;
                        }
                    }

                    if (!ItemExist)
                    {
                        lbQuestions.Items.Add(L);
                        L.Selected = false;
                        dAr.Add(L);
                    }
                }
            }

            foreach (ListItem i in dAr)
            {
                lbSelectedQuestions.Items.Remove(i);
            }

            lblQuestionsTobeIncluded.Text = "Number of questions to be included: " + lbSelectedQuestions.Items.Count;
            lblQuestionIncluded.Text = "Number of questions included: " + lbQuestions.Items.Count;
            if (Messenger1.Message.Count > 0)
            {
                Messenger1.ShowMessage();
            }
        }

        protected void btnAddAllQuestions_Click(object sender, System.EventArgs e)
        {
            #region Trace Information
            TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Edit Custom Test Page : Add All Question Clicked.");
            #endregion
            ArrayList dAr = new ArrayList();
            bool ItemExist = false;
            foreach (ListItem L in lbSelectedQuestions.Items)
            {
                if (lbQuestions.Items.Count >= 265)
                {
                    Messenger1.Message.Add("This test has 265 questions already. Can not add more.");
                    break;
                }

                ItemExist = false;
                foreach (ListItem T in lbQuestions.Items)
                {
                    if (L.Value.Split('|')[0] == T.Value.Split('|')[0])
                    {
                        Messenger1.Message.Add("Question " + L.Text + " in this test already!");
                        ItemExist = true;
                    }
                }

                if (!ItemExist)
                {
                    lbQuestions.Items.Add(L);
                    L.Selected = false;
                    dAr.Add(L);
                }
            }

            foreach (ListItem i in dAr)
            {
                lbSelectedQuestions.Items.Remove(i);
            }

            lblQuestionsTobeIncluded.Text = "Number of questions to be included: " + lbSelectedQuestions.Items.Count;
            lblQuestionIncluded.Text = "Number of questions included: " + lbQuestions.Items.Count;
            if (Messenger1.Message.Count > 0)
            {
                Messenger1.ShowMessage();
            }
        }

        protected void btnRemoveQuestions_Click(object sender, System.EventArgs e)
        {
            #region Trace Information
            TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Edit Custom Test Page : Add All Question Clicked.");
            #endregion

            ArrayList dAr = new ArrayList();
            foreach (ListItem L in lbQuestions.Items)
            {
                if (L.Selected)
                {
                    lbSelectedQuestions.Items.Add(L);
                    dAr.Add(L);
                }
            }

            foreach (ListItem i in dAr)
            {
                lbQuestions.Items.Remove(i);
            }

            lblQuestionsTobeIncluded.Text = "Number of questions to be included: " + lbSelectedQuestions.Items.Count;
            lblQuestionIncluded.Text = "Number of questions included: " + lbQuestions.Items.Count;
            if (Messenger1.Message.Count > 0)
            {
                Messenger1.ShowMessage();
            }
        }

        protected void btnRemoveAllQuestions_Click(object sender, System.EventArgs e)
        {
            ArrayList dAr = new ArrayList();
            foreach (ListItem L in lbQuestions.Items)
            {
                lbSelectedQuestions.Items.Add(L);
                dAr.Add(L);
            }

            foreach (ListItem i in dAr)
            {
                lbQuestions.Items.Remove(i);
            }

            lblQuestionsTobeIncluded.Text = "Number of questions to be included: " + lbSelectedQuestions.Items.Count;
            lblQuestionIncluded.Text = "Number of questions included: " + lbQuestions.Items.Count;
            if (Messenger1.Message.Count > 0)
            {
                Messenger1.ShowMessage();
            }
        }

        protected void btnDone_Click(object sender, System.EventArgs e)
        {
            if (!Confirm())
            {
                return;
            }

            Test test = new Test();
            test.TestId = TestId;
            test.TestName = txtTestName.Text;
            test.ProductId = ddTestCategory.SelectedValue.ToInt();
            test.GroupId = test.ProductId == (int)ProductType.FocusedReviewTests ? hdnDefaultGroup.Value.ToInt() : 0;
            test.SecondPerQuestion = hdnSecondsPerquestion.Value.ToInt();
            Presenter.SaveTest(test);
            UpdateQuestionListInTest(TestId);
            Presenter.NavigateToCustomTest(TestId);
        }

        protected void btnCancel_Click(object sender, System.EventArgs e)
        {
            Presenter.NavigateToCustomTest(TestId);
        }

        protected void btnCategory_Click(object sender, System.EventArgs e)
        {
            Test test = Presenter.GetTestById(TestId);
            Presenter.NavigateToTestCategory(TestId, test.ProductId, test.ProgramofStudyId);
        }

        protected void btnMoveUp_Click(object sender, System.EventArgs e)
        {
            if (lbQuestions.SelectedItem == null)
            {
                Messenger1.ShowMessage("Please select questions in the test to move.");
                return;
            }

            if (lbQuestions.Items.IndexOf(lbQuestions.SelectedItem) == 0)
            {
                Messenger1.ShowMessage("Can not move.");
                return;
            }

            for (int i = 0; i <= lbQuestions.Items.Count - 1; i++)
            {
                if (lbQuestions.Items[i].Selected)
                {
                    ListItem it = lbQuestions.Items[i - 1];
                    lbQuestions.Items.RemoveAt(i - 1);
                    lbQuestions.Items.Insert(i, it);
                }
            }
        }

        protected void btnMoveDown_Click(object sender, System.EventArgs e)
        {
            if (lbQuestions.SelectedItem == null)
            {
                Messenger1.ShowMessage("Please select questions in the test to move.");
                return;
            }

            if (lbQuestions.Items[lbQuestions.Items.Count - 1].Selected)
            {
                Messenger1.ShowMessage("Can not move.");
                return;
            }

            for (int i = lbQuestions.Items.Count - 1; i >= 0; i += -1)
            {
                if (lbQuestions.Items[i].Selected)
                {
                    ListItem it = lbQuestions.Items[i + 1];
                    lbQuestions.Items.RemoveAt(i + 1);
                    while (!((!lbQuestions.Items[i].Selected) | (i == 0)))
                    {
                        i -= 1;
                    }

                    i++;
                    lbQuestions.Items.Insert(i, it);
                }
            }
        }

        protected void ddTestType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Presenter.GetTests(ddTestType.SelectedValue.ToInt(), hfProgramofStudyId.Value.ToInt());
        }

        private void PopulateCategory(DropDownList control, Category category)
        {
            control.DataSource = category.Details.Values;
            control.DataTextField = "Description";
            control.DataValueField = "Id";
            control.DataBind();
        }

        private void ucSubCategories_OnClientNeedsChange(object sender, ItemSelectedEventArgs e)
        {
            Presenter.GetClientNeedsCategory(e.SelectedValue.ToInt(), hfProgramofStudyId.Value.ToInt());
        }

        private void RefreshListBox()
        {
            foreach (ListItem i in lbSelectedQuestions.Items)
            {
                int qid = i.Value.Split('|')[0].ToInt();

                Question question = Presenter.GetQuestionByQid(qid);
                HiddenField H = new HiddenField();
                H.Value = Server.HtmlEncode(question.Stem);
                Ph1.Controls.Add(H);
                i.Value = question.Id + "|" + H.ClientID;
            }

            foreach (ListItem i in lbQuestions.Items)
            {
                int qid = i.Value.Split('|')[0].ToInt();
                Question question = Presenter.GetQuestionByQid(qid);
                HiddenField H = new HiddenField();
                H.Value = Server.HtmlEncode(question.Stem);
                Ph1.Controls.Add(H);
                i.Value = question.Id + "|" + H.ClientID;
            }
        }

        private List<QuestionResult> Scramble(IEnumerable<QuestionResult> searchResult, int ReturnQuantity)
        {
            List<QuestionResult> questions = new List<QuestionResult>();
            if (searchResult != null)
            {
                questions = searchResult.ToList();
                Random random = new Random();

                for (int i = 0; i < questions.Count; i++)
                {
                    questions[i].Scramble = Convert.ToInt32(Convert.ToInt32((10000 * random.Next(2000)) + 1));
                }

                questions = (from q in questions
                             orderby q.Scramble
                             select q).ToList();

                if (questions.Count > ReturnQuantity)
                {
                    while (!(questions.Count == ReturnQuantity))
                    {
                        questions.RemoveAt(questions.Count - 1);
                    }
                }

                questions = (from q in questions
                             orderby q.QuestionID
                             select q).ToList();
            }

            return questions;
        }

        private bool Confirm()
        {
            if (string.IsNullOrEmpty(txtTestName.Text))
            {
                Messenger1.Message.Add("Test Name is required.");
            }

            if (Presenter.IsCustomTestExist(TestId, ddTestCategory.SelectedValue.ToInt(), txtTestName.Text))
            {
                Messenger1.Message.Add("Test Name existed; please use another.");
            }

            if (Messenger1.Message.Count > 0)
            {
                Messenger1.ShowMessage();
                return false;
            }
            else
            {
                return true;
            }
        }

        private int GetSelectedValue(string selectedValue, string defaultValue)
        {
            return (selectedValue == Constants.LIST_NOT_SELECTED_VALUE) ? defaultValue.ToInt() : selectedValue.ToInt();
        }
    }
}
