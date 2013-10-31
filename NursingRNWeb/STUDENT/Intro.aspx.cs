using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;
using WebControls;
using Action = NursingLibrary.Presenters.Action;

namespace STUDENT
{
    public partial class Intro : StudentBasePage<IStudentIntroView, StudentIntroPresenter>, IStudentIntroView
    {
        #region Properties

        public string BrowserType
        {
            get
            {
                return Request.Browser.Type;
            }
        }

        public int TabIndex { get; set; }

        public bool IsNextVisible
        {
            get
            {
                return btnNext.Visible == true;
            }
        }

        public string UserHostAddress
        {
            get { return Request.UserHostAddress; }
        }

        public bool Postback
        {
            get { return IsPostBack; }
        }

        public string HTTP_X_FORWARDED_FOR
        {
            get { return Request.ServerVariables["HTTP_X_FORWARDED_FOR"]; }
        }

        public int Timer
        {
            get
            {
                var timer = 0;
                var time = mytimer.Value.Trim().Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries).Select(t => Convert.ToInt32(t));
                switch (time.Count())
                {
                    case 3:
                        timer = (int)new TimeSpan(time.ElementAtOrDefault(0), time.ElementAtOrDefault(1),
                                                    time.ElementAtOrDefault(2)).TotalSeconds;
                        break;
                    case 2:
                        timer =
                            (int)new TimeSpan(0, time.ElementAtOrDefault(0), time.ElementAtOrDefault(1)).TotalSeconds;
                        break;
                    case 1:
                        timer = (int)new TimeSpan(0, 0, time.ElementAtOrDefault(0)).TotalSeconds;
                        break;
                }

                return timer;
            }
        }

        public string QuestionId
        {
            get { return txtQuestionID.Text; }
        }

        public string TimerCount
        {
            get { return timerCount.Value.Trim(); }
        }

        public bool Resume { get; set; }

        public string Timerup
        {
            get { return timer_up.Value.Trim(); }
        }

        public string AnswerTrack
        {
            get { return txtA.Value.Trim(); }
        }

        public string RequireResponse
        {
            get { return requireResponse.Value.Trim(); }
        }

        public string FileType
        {
            get { return txtFileType.Text.Trim(); }
        }

        public string QuestionNumber
        {
            get { return txtQuestionNumber.Text.Trim(); }
        }

        public string A1
        {
            get { return txtA1.Text.Trim(); }
        }

        public int CorrectQuestion
        {
            get;
            set;
        }

        public string TimedTestQB
        {
            get { return txtTimedTestQB.Value.Trim(); }
            set { txtTimedTestQB.Value = value; }
        }

        public string QuestionTypeText
        {
            get { return txtQuestionType.Text.Trim(); }
        }

        public string RemediationHtml { get; set; }

        public bool IsSkillModuleLinkClicked { get; set; }

        public UserTest UserTest
        {
            get
            {
                return new UserTest
                           {
                               UserTestId = Student.UserTestId,
                               SuspendQID = Convert.ToInt32(txtQuestionID.Text.Trim()),
                               SuspendQuestionNumber = Convert.ToInt32(txtQuestionNumber.Text.Trim()),
                               SuspendType = txtFileType.Text.Trim(),
                               TimeRemaining = ContinueTiming.Value == "1" ? remaining.Value : "0",
                           };
            }
        }

        public bool CheckIsQBankQuestion
        {
            get
            {
                bool result = false;
                if (!string.IsNullOrEmpty(Request.QueryString["CheckIsQBankQuestion"]))
                {
                    bool.TryParse(Request.QueryString["CheckIsQBankQuestion"], out result);
                }

                return result;
            }
        }

        public string Remaining
        {
            get;
            set;
        }

        public string Test
        {
            get { return txtTest.Text = Request.Form[txtTest.UniqueID] ?? txtTest.Text; }
            set { txtTest.Text = value; }
        }

        public string TestName
        {
            set { lblTestName.Text = value; }
        }

        public bool EnableLabel
        {
            set { Label1.Visible = value; }
        }

        public bool SetTabVisibility
        {
            set { TabQuestion.Visible = value; }
        }

        public bool SetTabAlternateVisibility
        {
            set { TabAlternate.Enabled = value; }
        }

        public bool CheckIsFocused
        {
            get { return false; }
            set { tabNotFocused.Visible = value; }
        }

        public bool NextInCorrectButton
        {
            set { btnNextIncorrect.Visible = value; }
        }

        public string TestType
        {
            set { txtTestType.Value = value; }
        }

        public string ADA
        {
            set { txtADA.Value = value; }
        }

        public string SecondPerQuestion
        {
            set { txtSecondPerQuestion.Value = value; }
        }

        public bool IsProctorTrackEnabled
        {
            get
            {
                return Student.ProductId == (int) ProductType.IntegratedTesting && PresentationHelper.IsProctorTrackEnabled(Student.IsProctorTrackEnabled);
            }
        }

        #endregion

        #region Public methods

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static void saveRemediationTime(string action, string timer, string qid, string userTestId)
        {
            switch (action)
            {
                case "Remediation":
                case "Review":
                    {
                        var presenter = Resolve<StudentIntroPresenter>() as StudentIntroPresenter;
                        if (presenter == null)
                        {
                            throw new InvalidOperationException("Presenter could not be resolved");
                        }

                        presenter.UpdateQuestionRemediation(timer, qid, userTestId, action);
                    }

                    break;
            }
        }

        public static string GenerateXMLForMatch(IEnumerable<UserAnswer> userAnswers)
        {
            var xmlstring = userAnswers.Aggregate(@"<orderingAnswerSet width=""700"" height=""250"" >", (current, answer) => string.Format("{0}<orderAnswer initialPos=\"{1}\">{2}</orderAnswer>", current, answer.initialPos.ToString(), answer.AText));
            xmlstring = string.Format("{0}</orderingAnswerSet>", xmlstring);
            return xmlstring;
        }

        public static string GenerateReviewXMLForMatch(Question userQuestion)
        {
            var xmlstring = "<orderMatch orderedIndexes=\" ";
            xmlstring = xmlstring + userQuestion.OrderedIndexes.Trim() + "\" correct=\"";
            if (userQuestion.Correct == 0 || userQuestion.Correct == 2)
            {
                xmlstring = xmlstring + "0\"/>";
            }
            else
            {
                xmlstring = xmlstring + "1\"/>";
            }

            return xmlstring;
        }

        public static string GenerateXMLForHotSpotNormalModeWithoutEnd(IEnumerable<UserAnswer> hotSpotAnswers)
        {
            return hotSpotAnswers.Aggregate("shapeString=", (current, hotSpotAnswer) => string.Format("{0}{1}&imagePath=flash/{2}&responseSeq=0&mode=sim", current, hotSpotAnswer.AText, hotSpotAnswer.Stimulus.Trim()));
        }

        public static string GenerateReviewXMLForHotSpot(Question userQuestion, IEnumerable<UserAnswer> hotSpotAnswers, string Mode)
        {
            string xmlstring = GenerateXMLForHotSpotNormalModeWithoutEnd(hotSpotAnswers);

            xmlstring = Mode == "Redo"
                            ? string.Format("{0}&mode=sim&redo={1}", xmlstring, userQuestion.OrderedIndexes.Trim())
                            : (userQuestion.Correct == 0 || userQuestion.Correct == 2
                                   ? string.Format("{0}&mode=review&review={1},0", xmlstring,
                                                   userQuestion.OrderedIndexes.Trim())
                                   : string.Format("{0}&mode=review&review={1},1", xmlstring,
                                                   userQuestion.OrderedIndexes.Trim()));
            xmlstring = string.Format("{0}\"/>", xmlstring);

            return xmlstring;
        }

        public void FillMultipleChoiceFields(IEnumerable<UserAnswer> answers, QuestionExhibit questionExhibit, IEnumerable<UserAnswer> userAnswers)
        {
            match.Visible = false;
            hotspot_D.Visible = false;
            hotspot_D.InnerHtml = string.Empty;
            match.InnerHtml = string.Empty;

            divmatch.Visible = false;
            divhotspot_D.Visible = false;
            divhotspot_D.InnerHtml = string.Empty;
            divmatch.InnerHtml = string.Empty;
            int tab = 0;

            if (!string.IsNullOrEmpty(questionExhibit.ExhibitTab1))
            {
                imgExhibit.Visible = true;
                ExhTitle1.Text = (questionExhibit.ExhibitTitle1 == string.Empty) ? "Tab 1" : questionExhibit.ExhibitTitle1;
                Exhibit1.InnerHtml = questionExhibit.ExhibitTab1;
                PanelExhibit.Visible = true;
                TabPanel1.Visible = true;
                ++tab;
            }
            else
            {
                imgExhibit.Visible = false;
                TabPanel1.Visible = false;
                PanelExhibit.Visible = false;
            }

            if (questionExhibit.ExhibitTab2 != string.Empty)
            {
                ExhTitle2.Text = (questionExhibit.ExhibitTitle2 == string.Empty) ? "Tab 2" : questionExhibit.ExhibitTitle2;
                Exhibit2.InnerHtml = questionExhibit.ExhibitTab2;
                TabPanel2.Visible = true;
                ++tab;
            }
            else
            {
                TabPanel2.Visible = false;
            }

            if (questionExhibit.ExhibitTab3 != string.Empty)
            {
                ExhTitle3.Text = (questionExhibit.ExhibitTitle3 == string.Empty) ? "Tab 3" : questionExhibit.ExhibitTitle3;
                Exhibit3.InnerHtml = questionExhibit.ExhibitTab3;
                TabPanel3.Visible = true;
                ++tab;
            }
            else
            {
                TabPanel3.Visible = false;
            }

            var text = (HtmlInputHidden)HiddenTexts.FindControl("tabNumber");
            text.Value = tab.ToString();

            var iChoice = 0;
            D_Answers.Controls.Clear();
            divD_Answers.Controls.Clear();
            var table = new HtmlTable { CellPadding = 8 };
            foreach (var answer in answers)
            {
                if (answer.AText.Trim() != string.Empty)
                {
                    iChoice++;
                    var radiobuttonI = new RadioButton
                                           {
                                               ID = "RB_" + ConvertToNumbers(answer.AIndex),
                                               Text =
                                                   string.Format(@"{0}.&nbsp;{1}", Convert.ToString(iChoice),
                                                                 ShowPicture(answer.AText)),
                                               GroupName = "RB"
                                           };
                    if (CheckIsFocused == false)
                    {
                        radiobuttonI.Attributes.Add("onclick", "EnableNextButton('" + ConvertToNumbers(answer.AIndex) + "', 'standard');");
                    }
                    else
                    {
                        radiobuttonI.Attributes.Add("onclick", "EnableNextButton('" + ConvertToNumbers(answer.AIndex) + "', '');");
                    }

                    var row = new HtmlTableRow();
                    table.Controls.Add(row);
                    var tableCell = new HtmlTableCell();
                    row.Controls.Add(tableCell);
                    tableCell.Controls.Add(radiobuttonI);

                    var textbox = new TextBox
                    {
                        ID = "TB_" + ConvertToNumbers(answer.AIndex),
                        Text = answer.AnswerID.ToString(),
                        Visible = false
                    };
                    tableCell = new HtmlTableCell();
                    row.Controls.Add(tableCell);
                    tableCell.Controls.Add(textbox);

                    var textboxC = new TextBox
                    {
                        ID = "TB_C_" + ConvertToNumbers(answer.AIndex),
                        Text = answer.Correct.ToString(),
                        Visible = false
                    };

                    tableCell = new HtmlTableCell();
                    row.Controls.Add(tableCell);
                    tableCell.Controls.Add(textboxC);

                    if (answer.Correct == 1)
                    {
                        txtA1.Text = answer.AIndex;
                    }

                    bool answersForQuestionExists = userAnswers.FirstOrDefault(ans => ans.QID == Student.QuestionId) != null;
                    if (Student.Action == Action.Review || answersForQuestionExists)
                    {
                        if (Student.Action == Action.Review && answer.Correct == 1)
                        {
                            radiobuttonI.BackColor = Color.FromArgb(228, 240, 216);
                        }

                        UserAnswer answer1 = answer;
                        var correctAnswer = userAnswers.FirstOrDefault(ans => ans.QID == Student.QuestionId && ans.AnswerID == answer1.AnswerID) != null;
                        if (correctAnswer)
                        {
                            radiobuttonI.Checked = true;
                        }
                    }

                    if (Student.Action == Action.Review)
                    {
                        radiobuttonI.Enabled = false;
                    }
                }
            }

            if (Student.ProductId == 3 && (Student.Action == Action.Remediation || Student.Action == Action.Review))
            {
                D_Answers.Controls.Add(table);
            }
            else
            {
                divD_Answers.Controls.Add(table);
            }
        }

        public void FillTheBlankFields(IEnumerable<UserAnswer> answers, IEnumerable<UserAnswer> userAnswers)
        {
            match.Visible = false;
            hotspot_D.Visible = false;
            hotspot_D.InnerHtml = string.Empty;
            match.InnerHtml = string.Empty;

            divmatch.Visible = false;
            divhotspot_D.Visible = false;
            divhotspot_D.InnerHtml = string.Empty;
            divmatch.InnerHtml = string.Empty;

            if (answers.Count() > 0)
            {
                foreach (var answer in answers)
                {
                    var textbox = new TextBox();
                    bool answersForQuestionExists = userAnswers.FirstOrDefault(ans => ans.QID == Student.QuestionId) != null;
                    if (answersForQuestionExists)
                    {
                        UserAnswer filteredAnswer = userAnswers.FirstOrDefault(ans => ans.QID == Student.QuestionId && ans.AnswerID == answer.AnswerID);
                        textbox.Text = (filteredAnswer != null) ? filteredAnswer.AText : string.Empty;
                    }
                    else
                    {
                        textbox.Text = string.Empty;
                    }

                    textbox.ID = "tx";
                    if (Student.ProductId == 3 && (Student.Action == Action.Remediation || Student.Action == Action.Review))
                    {
                        D_Answers.Controls.Clear();
                        D_Answers.Controls.Add(textbox);
                        textbox.Attributes.Add("onClick", "document.getElementById('" + textbox.ClientID + "').focus()");
                        textbox.Attributes.Add("onkeyup", "CheckTxt('standard');");
                    }
                    else
                    {
                        divD_Answers.Controls.Clear();
                        divD_Answers.Controls.Add(textbox);
                        textbox.Attributes.Add("onClick", "document.getElementById('" + textbox.ClientID + "').focus()");
                        textbox.Attributes.Add("onkeyup", "CheckTxt('');");
                    }

                    textbox.Enabled = true;
                    if (string.IsNullOrEmpty(textbox.Text) && Presenter.Student.Action != Action.Remediation && Presenter.Student.Action != Action.Review)
                    {
                        btnNext.Enabled = false;
                    }

                    var label = new Label { ID = "LB", Text = answer.Unit };
                    var textboxB = new TextBox { ID = "TB_1", Text = answer.AnswerID.ToString(), Visible = false };
                    var textboxC = new TextBox { ID = "TB_C_1", Text = answer.AText, Visible = false };

                    if (Student.ProductId == 3 && (Student.Action == Action.Remediation || Student.Action == Action.Review))
                    {
                        D_Answers.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
                        D_Answers.Controls.Add(label);
                        D_Answers.Controls.Add(textboxB);
                        D_Answers.Controls.Add(new LiteralControl("<br/>"));
                        D_Answers.Controls.Add(textboxC);
                        D_Answers.Controls.Add(new LiteralControl("<br/>"));
                    }
                    else
                    {
                        divD_Answers.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
                        divD_Answers.Controls.Add(label);
                        divD_Answers.Controls.Add(textboxB);
                        divD_Answers.Controls.Add(new LiteralControl("<br/>"));
                        divD_Answers.Controls.Add(textboxC);
                        divD_Answers.Controls.Add(new LiteralControl("<br/>"));
                    }

                    if (Student.Action == Action.Review)
                    {
                        textboxC.Enabled = false;
                    }
                }
            }
        }

        public void FillTheMatchFields(IEnumerable<UserAnswer> userAnswers, Question userQuestion)
        {
            match.Visible = true;
            hotspot_D.InnerHtml = string.Empty;
            hotspot_D.Visible = false;
            D_Answers.InnerHtml = string.Empty;
            D_Answers.Visible = false;

            divmatch.Visible = true;
            divhotspot_D.InnerHtml = string.Empty;
            divhotspot_D.Visible = false;
            divD_Answers.InnerHtml = string.Empty;
            divD_Answers.Visible = false;

            string browserType = Request.Browser.Type;
            string html;

            if (String.Compare(browserType, "IE7", StringComparison.OrdinalIgnoreCase) == 0 || String.Compare(browserType, "IE8", StringComparison.OrdinalIgnoreCase) == 0)
            {
                string flashVar = GenerateXMLForMatch(userAnswers);
                if (Student.Action == Action.Review)
                {
                    string interactionset = GenerateReviewXMLForMatch(userQuestion);
                    flashVar = "orderingAnswerSet=" + Server.UrlEncode(flashVar) + "&amp;mediaPrefix=/content/media/89/79389.24.&amp;mode=review&amp;interactionState=" + Server.UrlEncode(interactionset);
                }
                else
                {
                    flashVar = "orderingAnswerSet=" + Server.UrlEncode(flashVar) + "&amp;mediaPrefix=/content/media/89/79389.24.&amp;mode=sim&amp;interactionState=%3CorderMatch%20/%3E";
                }

                var swf = "<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" style=\"-khtml-user-drag:element;\"  codebase=\"http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#	version=7,0,0,0\" id=\"flashInteractionQuestions\" onclick=\"javascript:NoPrompt();\" onmousedown=\"javascript:NoPrompt();\" width=\"750\" height=\"250\">";
                swf = string.Format("{0} <param name=\"movie\" value=\"flash/orderingInteraction.swf\">", swf);
                swf = string.Format("{0} <param name=\"quality\" value=\"autohigh\">", swf);
                swf = string.Format("{0}  <param name=\"bgcolor\" value=\"#ffffff\">", swf);
                swf = string.Format("{0} <param name=\"scale\" value=\"noscale\">", swf);
                swf = string.Format("{0} <param name=\"menu\" value=\"false\">", swf);
                swf = string.Format("{0} <param name=\"wmode\" value=\"transparent\" />", swf);
                swf = string.Format("{0} <param name=\"allowscriptaccess\" value=\"always\" />", swf);
                swf = string.Format("{0} <param name=\"FlashVars\" value=\"{1}\">", swf, flashVar);
                swf = string.Format("{0} <embed scale=\"noscale\" menu=\"false\" quality=\"high\"", swf);
                swf = string.Format("{0} bgcolor=\"#ffffff\" name=\"flashInteractionQuestions\" allowScriptAccess=\"sameDomain\" type=\"application/x-shockwave-flash\" swLiveConnect=\"true\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" src=\"flash/orderingInteraction.swf\" FlashVars=\"{1}\" wmode=\"transparent\" allowscriptaccess=\"always\" width=\"750\" height=\"250\">", swf, flashVar);
                swf = string.Format("{0} </embed>", swf);
                swf = string.Format("{0} </object>", swf);
                divmatch.InnerHtml = swf;
                match.InnerHtml = swf;
            }
            else
            {
                var userAnswersList = userAnswers as IList<UserAnswer> ?? userAnswers.ToList();
                html = ControlHelper.GetDragDropFormatedHtml(userAnswersList, false);
                if (Student.Action == Action.Review)
                {
                    html = ControlHelper.GetDragDropFormatedHtmlForReview(userAnswersList, userQuestion, false);
                }

                divmatch.InnerHtml = html;
                match.InnerHtml = html;
            }
        }

        public void FillMultipleChoiceMultiSelectFields(IEnumerable<UserAnswer> answers, QuestionExhibit questionExhibit, IEnumerable<UserAnswer> userAnswers)
        {
            int tab = 0;
            match.Visible = false;
            hotspot_D.Visible = false;
            hotspot_D.InnerHtml = string.Empty;
            match.InnerHtml = string.Empty;
            divmatch.Visible = false;
            divhotspot_D.Visible = false;
            divhotspot_D.InnerHtml = string.Empty;
            divmatch.InnerHtml = string.Empty;
            if (!string.IsNullOrEmpty(questionExhibit.ExhibitTab1))
            {
                imgExhibit.Visible = true;
                Exhibit1.InnerHtml = questionExhibit.ExhibitTab1;
                ++tab;
                TabPanel1.Visible = true;
                PanelExhibit.Visible = true;
            }
            else
            {
                imgExhibit.Visible = false;
                TabPanel1.Visible = false;
            }

            if (!string.IsNullOrEmpty(questionExhibit.ExhibitTab2))
            {
                Exhibit2.InnerHtml = questionExhibit.ExhibitTab2;
                TabPanel2.Visible = true;
                ++tab;
            }
            else
            {
                TabPanel2.Visible = false;
            }

            if (!string.IsNullOrEmpty(questionExhibit.ExhibitTab3))
            {
                Exhibit3.InnerHtml = questionExhibit.ExhibitTab3;
                TabPanel3.Visible = true;
                ++tab;
            }
            else
            {
                TabPanel3.Visible = false;
            }

            var text = (HtmlInputHidden)HiddenTexts.FindControl("tabNumber");
            text.Value = tab.ToString();

            int iChoice = 0;
            D_Answers.Controls.Clear();
            divD_Answers.Controls.Clear();
            foreach (var answer in answers)
            {
                if (answer.AText.Trim() != string.Empty)
                {
                    iChoice++;
                    var chechboxI = new CheckBox
                    {
                        ID = "CH_" + ConvertToNumbers(answer.AIndex),
                        Text =
                            string.Format("{0}.&nbsp;{1}", Convert.ToString(iChoice),
                                          ShowPicture(answer.AText))
                    };

                    var textbox = new TextBox
                    {
                        ID = "TB_" + ConvertToNumbers(answer.AIndex),
                        Text = answer.AnswerID.ToString(),
                        Visible = false
                    };

                    if (Student.ProductId == 3 && (Student.Action == Action.Remediation || Student.Action == Action.Review))
                    {
                        D_Answers.Controls.Add(textbox);
                        D_Answers.Controls.Add(new LiteralControl("<br/>"));
                    }
                    else
                    {
                        divD_Answers.Controls.Add(textbox);
                        divD_Answers.Controls.Add(new LiteralControl("<br/>"));
                    }

                    var textboxC = new TextBox
                    {
                        ID = "TB_C_" + ConvertToNumbers(answer.AIndex),
                        Text = answer.Correct.ToString(),
                        Visible = false
                    };

                    if (Student.ProductId == 3 && (Student.Action == Action.Remediation || Student.Action == Action.Review))
                    {
                        D_Answers.Controls.Add(textboxC);
                        D_Answers.Controls.Add(new LiteralControl("<br/>"));
                        D_Answers.Controls.Add(chechboxI);
                        D_Answers.Controls.Add(new LiteralControl("<br/>"));
                    }
                    else
                    {
                        divD_Answers.Controls.Add(textboxC);
                        divD_Answers.Controls.Add(new LiteralControl("<br/>"));
                        divD_Answers.Controls.Add(chechboxI);
                        divD_Answers.Controls.Add(new LiteralControl("<br/>"));
                    }

                    bool answersForQuestionExists = userAnswers.FirstOrDefault(ans => ans.QID == Student.QuestionId) != null;
                    if (Student.Action == Action.Review || answersForQuestionExists)
                    {
                        if (Student.Action == Action.Review && answer.Correct == 1)
                        {
                            chechboxI.BackColor = Color.FromArgb(228, 240, 216);
                        }

                        // check the answer
                        UserAnswer answer1 = answer;
                        var correctAnswer = userAnswers.FirstOrDefault(ans => ans.QID == Student.QuestionId && ans.AnswerID == answer1.AnswerID) != null;
                        if (correctAnswer)
                        {
                            chechboxI.Checked = true;
                        }
                    }

                    chechboxI.Attributes.Add("onclick", "CheckForSelected('standard');");
                    if (Student.Action == Action.Review)
                    {
                        chechboxI.Enabled = false;
                    }
                }
            }
        }

        public void FillHotSpotFields(IEnumerable<UserAnswer> userAnswers, Question userQuestion, IEnumerable<UserAnswer> hotSpotAnswers)
        {
            match.Visible = false;
            hotspot_D.Visible = true;
            match.InnerHtml = string.Empty;
            D_Answers.InnerHtml = string.Empty;
            D_Answers.Visible = false;
            divmatch.Visible = false;
            divhotspot_D.Visible = true;
            divmatch.InnerHtml = string.Empty;
            divD_Answers.InnerHtml = string.Empty;
            divD_Answers.Visible = false;
            string shapeString;
            var answersForQuestionExists = userAnswers.FirstOrDefault(ans => ans.QID == Student.QuestionId) != null;

            if (Student.Action == Action.Review || answersForQuestionExists)
            {
                shapeString = GenerateReviewXMLForHotSpot(userQuestion, hotSpotAnswers, Resume && answersForQuestionExists ? "Redo" : "Review");
            }
            else
            {
                shapeString = GenerateXMLForHotSpotNormalModeWithoutEnd(hotSpotAnswers);
            }

            var swf = "<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" codebase=\"http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0\" onclick=\"javascript:NoPrompt();\" onmousedown=\"javascript:NoPrompt();\" width=\"549\" height=\"600\" id=\"hotSpotComp\" align=\"middle\">";
            swf = string.Format("{0} <param name=\"allowScriptAccess\" value=\"sameDomain\" />", swf);
            swf = string.Format("{0} <param name=\"movie\" value=\"flash/hotSpotComp.swf\" />", swf);
            swf = string.Format("{0} <param name=\"quality\" value=\"high\" />", swf);
            swf = string.Format("{0} <param name=\"scale\" value=\"noscale\" />", swf);
            swf = string.Format("{0} <param name=\"bgcolor\" value=\"#ffffff\" />", swf);
            swf = string.Format("{0} <param name=\"wmode\" value=\"transparent\" />", swf);
            swf = string.Format("{0} <param name=\"allowscriptaccess\" value=\"always\" />", swf);
            swf = string.Format("{0} <param name=\"FlashVars\" value=\"{1}\" />", swf, shapeString);
            swf = string.Format("{0} <embed src=\"flash/hotSpotComp.swf\" quality=\"high\" scale=\"noscale\" bgcolor=\"#ffffff\" wmode=\"transparent\" allowscriptaccess=\"always\"  width=\"540\" height=\"600\" name=\"hotSpotComp\" align=\"middle\" allowScriptAccess=\"sameDomain\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" FlashVars=\"{1}\" /></object>", swf, shapeString);
            divhotspot_D.InnerHtml = swf;
            hotspot_D.InnerHtml = swf;
        }

        public IList<UserAnswer> PopulateMultipleChoice()
        {
            IList<UserAnswer> alist = new List<UserAnswer>();
            TextBox textbox;
            RadioButton radioButton;
            TextBox textboxC;
            var textboxA = new TextBox();
            var radioButtonA = new RadioButton();
            var textboxCA = new TextBox();

            for (int iChoice = 1; iChoice < 7; iChoice++)
            {
                if (Student.ProductId == 3 && (Student.Action == Action.Remediation || Student.Action == Action.Review))
                {
                    textbox = (TextBox)FindControl("TabQuestion").FindControl("TabStandard").FindControl("TB_" + Convert.ToString(iChoice));
                    radioButton = (RadioButton)FindControl("TabQuestion").FindControl("TabStandard").FindControl("RB_" + Convert.ToString(iChoice));
                    textboxC = (TextBox)FindControl("TabQuestion").FindControl("TabStandard").FindControl("TB_C_" + Convert.ToString(iChoice));

                    textboxA = (TextBox)FindControl("TabQuestion").FindControl("TabAlternate").FindControl("ucAlternateIntro").FindControl("ATB_" + Convert.ToString(iChoice));
                    radioButtonA = (RadioButton)FindControl("TabQuestion").FindControl("TabAlternate").FindControl("ucAlternateIntro").FindControl("ARB_" + Convert.ToString(iChoice));
                    textboxCA = (TextBox)FindControl("TabQuestion").FindControl("TabAlternate").FindControl("ucAlternateIntro").FindControl("ATB_C_" + Convert.ToString(iChoice));
                }
                else
                {
                    textbox = (TextBox)FindControl("TB_" + Convert.ToString(iChoice));
                    radioButton = (RadioButton)FindControl("RB_" + Convert.ToString(iChoice));
                    textboxC = (TextBox)FindControl("TB_C_" + Convert.ToString(iChoice));
                }

                if (radioButton != null)
                {
                    if (radioButton.Checked)
                    {
                        int correctAnswer = textboxC.Text.Trim() == "1" ? 1 : 0;
                        CorrectQuestion = correctAnswer;
                        var obj = new UserAnswer
                        {
                            AText = string.Empty,
                            AnswerConnectID = 0,
                            AnswerID = Convert.ToInt32(textbox.Text.Trim()),
                            AType = 1,
                            AIndex = ConvertToLetter(radioButton.ID.Trim().Substring(3, 1)),
                            Correct = correctAnswer,
                            UserTestID = Student.UserTestId,
                            QID = Convert.ToInt32(txtQuestionID.Text.Trim())
                        };
                        alist.Add(obj);
                    }
                    else if (radioButtonA != null)
                    {
                        if (radioButtonA.Checked)
                        {
                            int correctAltAnswer = textboxCA.Text.Trim() == "1" ? 1 : 0;
                            CorrectQuestion = correctAltAnswer;
                            var objAlt = new UserAnswer
                            {
                                AText = string.Empty,
                                AnswerConnectID = 0,
                                AnswerID = Convert.ToInt32(textboxA.Text.Trim()),
                                AType = 1,
                                AIndex = ConvertToLetter(radioButtonA.Text.Trim().Substring(0, 1)),
                                Correct = correctAltAnswer,
                                UserTestID = Student.UserTestId,
                                QID = Convert.ToInt32(txtQuestionID.Text.Trim())
                            };
                            alist.Add(objAlt);
                        }
                    }
                }
            }

            return alist;
        }

        public IList<UserAnswer> PopulateMultipleChoiceMultiSelect()
        {
            bool bCorrectQuestion = false;
            bool firstime = true;
            IList<UserAnswer> alist = new List<UserAnswer>();
            CorrectQuestion = 0;
            TextBox textbox;
            CheckBox checkbox;
            TextBox textboxC;
            TextBox textboxA;
            CheckBox checkboxA;
            TextBox textboxCA;

            for (int iChoice = 1; iChoice < 7; iChoice++)
            {
                if (Student.ProductId == 3 && (Student.Action == Action.Remediation || Student.Action == Action.Review))
                {
                    textbox = (TextBox)FindControl("TabQuestion").FindControl("TabStandard").FindControl("TB_" + Convert.ToString(iChoice));
                    checkbox = (CheckBox)FindControl("TabQuestion").FindControl("TabStandard").FindControl("CH_" + Convert.ToString(iChoice));
                    textboxC = (TextBox)FindControl("TabQuestion").FindControl("TabStandard").FindControl("TB_C_" + Convert.ToString(iChoice));

                    textboxA = (TextBox)FindControl("TabQuestion").FindControl("TabAlternate").FindControl("ucAlternateIntro").FindControl("ATB_" + Convert.ToString(iChoice));
                    checkboxA = (CheckBox)FindControl("TabQuestion").FindControl("TabAlternate").FindControl("ucAlternateIntro").FindControl("ACH_" + Convert.ToString(iChoice));
                    textboxCA = (TextBox)FindControl("TabQuestion").FindControl("TabAlternate").FindControl("ucAlternateIntro").FindControl("ATB_C_" + Convert.ToString(iChoice));
                }
                else
                {
                    textbox = (TextBox)FindControl("TB_" + Convert.ToString(iChoice));
                    checkbox = (CheckBox)FindControl("CH_" + Convert.ToString(iChoice));
                    textboxC = (TextBox)FindControl("TB_C_" + Convert.ToString(iChoice));
                    textboxA = null;
                    checkboxA = null;
                    textboxCA = null;
                }

                if (checkbox != null || checkboxA != null)
                {
                    if (firstime)
                    {
                        firstime = false;
                        bCorrectQuestion = true;
                    }

                    var obj = new UserAnswer();

                    switch (iChoice)
                    {
                        case 1: obj.AIndex = "A";
                            break;
                        case 2: obj.AIndex = "B";
                            break;
                        case 3: obj.AIndex = "C";
                            break;
                        case 4: obj.AIndex = "D";
                            break;
                        case 5: obj.AIndex = "E";
                            break;
                        case 6: obj.AIndex = "F";
                            break;
                    }

                    int correctAnswer = 0;

                    if (checkbox.Checked)
                    {
                        if (textboxC.Text.Trim() == "1")
                        {
                            correctAnswer = 1;
                            bCorrectQuestion = bCorrectQuestion && true;
                        }
                        else
                        {
                            correctAnswer = 0;
                            bCorrectQuestion = false;
                        }
                    }
                    else if (checkboxA != null && checkboxA.Checked)
                    {
                        if (textboxCA.Text.Trim() == "1")
                        {
                            correctAnswer = 1;
                            bCorrectQuestion = bCorrectQuestion && true;
                        }
                        else
                        {
                            correctAnswer = 0;
                            bCorrectQuestion = false;
                        }
                    }
                    else
                    {
                        if (textboxC.Text.Trim() == "1")
                        {
                            correctAnswer = 0;
                            bCorrectQuestion = false;
                        }
                    }

                    if (checkbox.Checked)
                    {
                        obj.AnswerConnectID = 0;
                        obj.AnswerID = Convert.ToInt32(textbox.Text.Trim());
                        obj.AType = 1;
                        obj.UserTestID = Student.UserTestId;
                        obj.QID = Convert.ToInt32(txtQuestionID.Text.Trim());
                        obj.AText = textbox.Text;
                        obj.Correct = correctAnswer;
                        alist.Add(obj);
                    }
                    else if (checkboxA != null && checkboxA.Checked)
                    {
                        obj.AnswerConnectID = 0;
                        obj.AnswerID = Convert.ToInt32(textboxA.Text.Trim());
                        obj.AType = 1;
                        obj.UserTestID = Student.UserTestId;
                        obj.QID = Convert.ToInt32(txtQuestionID.Text.Trim());
                        obj.AText = textboxA.Text;
                        obj.Correct = correctAnswer;
                        alist.Add(obj);
                    }
                }
            }

            CorrectQuestion = bCorrectQuestion ? 1 : 0;
            return alist;
        }

        public IList<UserAnswer> PopulateFillIn()
        {
            IList<UserAnswer> alist = new List<UserAnswer>();
            var obj = new UserAnswer { AIndex = "A" };
            TextBox radioButton;
            var aradiobutton = new TextBox();
            TextBox textbox;
            TextBox textboxC;
            var textboxA = new TextBox();
            var textboxCA = new TextBox();

            if (Student.ProductId == 3 && (Student.Action == Action.Remediation || Student.Action == Action.Review))
            {
                radioButton = (TextBox)FindControl("TabQuestion").FindControl("TabStandard").FindControl("tx");
                aradiobutton = (TextBox)FindControl("TabQuestion").FindControl("TabAlternate").FindControl("ucAlternateIntro").FindControl("Atx");
                textbox = (TextBox)FindControl("TabQuestion").FindControl("TabStandard").FindControl("TB_1");
                textboxC = (TextBox)FindControl("TabQuestion").FindControl("TabStandard").FindControl("TB_C_1");
                textboxA = (TextBox)FindControl("TabQuestion").FindControl("TabAlternate").FindControl("ucAlternateIntro").FindControl("ATB_1");
                textboxCA = (TextBox)FindControl("TabQuestion").FindControl("TabAlternate").FindControl("ucAlternateIntro").FindControl("ATB_C_1");
            }
            else
            {
                radioButton = (TextBox)FindControl("tx");
                textbox = (TextBox)FindControl("TB_1");
                textboxC = (TextBox)FindControl("TB_C_1");
            }

            if (radioButton != null && radioButton.Text != string.Empty)
            {
                obj.AText = radioButton.Text;
            }
            else if (aradiobutton != null && aradiobutton.Text != string.Empty)
            {
                obj.AText = aradiobutton.Text;
            }
            else
            {
                obj.AText = string.Empty;
            }

            if (textboxC.Text.Trim() != string.Empty)
            {
                double correctValue = Convert.ToDouble(textboxC.Text.Trim());
                double givenValue = 0;
                if (isNumeric(radioButton.Text.Trim(), NumberStyles.Number))
                {
                    givenValue = Convert.ToDouble(radioButton.Text.Trim());
                }

                if (givenValue != 0)
                {
                    if (correctValue == givenValue)
                    {
                        obj.Correct = 1;
                        CorrectQuestion = 1;
                    }
                    else
                    {
                        obj.Correct = 0;
                        CorrectQuestion = 0;
                    }
                }
                else
                {
                    if (textboxC.Text.Trim() == radioButton.Text.Trim())
                    {
                        obj.Correct = 1;
                        CorrectQuestion = 1;
                    }
                    else
                    {
                        obj.Correct = 0;
                        CorrectQuestion = 0;
                    }
                }
            }

            if (textboxCA.Text.Trim() != string.Empty)
            {
                if (textboxCA.Text.Trim() == aradiobutton.Text.Trim())
                {
                    obj.Correct = 1;
                    CorrectQuestion = 1;
                }
                else
                {
                    obj.Correct = 0;
                    CorrectQuestion = 0;
                }
            }

            obj.AnswerConnectID = 0;
            obj.AType = 1;
            if (textbox.Text.Trim() != string.Empty)
            {
                obj.AnswerID = Convert.ToInt32(textbox.Text.Trim());
            }
            else if (textboxA.Text.Trim() != string.Empty)
            {
                obj.AnswerID = Convert.ToInt32(textboxA.Text.Trim());
            }

            obj.UserTestID = Student.UserTestId;
            obj.QID = Convert.ToInt32(txtQuestionID.Text.Trim());
            obj.AIndex = "A";
            alist.Add(obj);
            return alist;
        }

        public bool isNumeric(string val, NumberStyles NumberStyle)
        {
            Double result;
            return Double.TryParse(val, NumberStyle,
                CultureInfo.CurrentCulture, out result);
        }

        public void SetQBankCreateCtrl(UserTest suspendedTest)
        {
            btnQuit.Attributes.Add("onclick", "return quitAlert();");
            Remaining = suspendedTest.TimeRemaining;

            if (suspendedTest.TimedTest == 0)
            {
                lbltimer.Text = string.Empty;
                ContinueTiming.Value = "3";
            }
        }

        public void SetRejoinResumeCtrl(UserTest suspendedTest)
        {
            btnQuit.Attributes.Add("onclick", "return quitAlert();");
            if (suspendedTest.TutorMode == 1)
            {
                ContinueTiming.Value = "3";
            }

            Remaining = suspendedTest.TimeRemaining;
        }

        public void SetControls()
        {
            imgExhibit.Visible = false;
            PanelExhibit.Visible = false;
            divExhibit.Style.Add("height", "0px");
            question_main.Style.Add("height", "400px");
            ibSuspend.Attributes.Add("onclick", " return confirm('Are you sure that you want to suspend the test?')");

            if ((Student.Action == Action.NewTest) || (Student.Action == Action.QBankCreate) || (Student.Action == Action.Resume))
            {
                btnQuit.Attributes.Add("onclick", "return quitAlert();");
                if (Student.QuizOrQBank == NursingLibrary.Interfaces.TestType.Qbank)
                {
                    if (TimedTestQB == "0")
                    {
                        lbltimer.Text = string.Empty;
                        MessageRetire.Visible = false;
                        ibSkip.Visible = false;
                        intro_main.Visible = false;
                        Explanation_Div.Visible = true;
                        remediation.Visible = false;
                        remediation.InnerHtml = string.Empty;
                        Explanation.InnerHtml = hdnExplanation.Value;
                        divStem.Visible = true;
                        divStem.InnerHtml = hdnDivStem.Value;
                    }

                    if (TimedTestQB == "0" && Student.Action == Action.QBankCreate && ((Student.ProductId == (int)ProductType.FocusedReviewTests) || (Student.ProductId == (int)ProductType.SkillsModules)))
                    {
                        Explanation_Div.Visible = false;
                    }
                }
            }
        }

        public void PopulateFields(Question question)
        {
            txtA.Value = string.Empty;
            txtA1.Text = string.Empty;
            txtQuestionID.Text = question.Id.ToString();
            txtQID.Value = question.Id.ToString();
            txtQuestionNumber.Text = question.QuestionNumber.ToString();
            txtQuestionType.Text = question.Type.ToString();
            txtFileType.Text = question.FileType.ToString();
            PopulateAlternateTextDetails(question);
            hdProductId.Value = Student.ProductId.ToString();
            hdAction.Value = Student.Action.ToString();
        }

        public void PopulateEnd(Question question, bool IsLastQuestion)
        {
            lblQNumber.Visible = false;
            match.Visible = false;
            hotspot_D.Visible = false;

            hotspot_D.InnerHtml = string.Empty;
            match.InnerHtml = string.Empty;

            divmatch.Visible = false;
            divhotspot_D.Visible = false;

            divhotspot_D.InnerHtml = string.Empty;
            divmatch.InnerHtml = string.Empty;

            btnBack.Visible = false;
            btnBackIncorrect.Visible = false;
            btnNext.Visible = false;
            btnNextIncorrect.Visible = false;
            btnQuit.Visible = false;
            ibSkip.Visible = false;
            ibSuspend.Visible = false;

            intro_main.Visible = true;
            tutorial_main.Visible = false;
            question_main.Visible = false;
            intro_title.InnerHtml = "<h3>" + question.ItemTitle + "</h3>";
            intro_text.Visible = true;
            intro_text.InnerHtml = "<div style='text-align:center;'>Congratulations, your test has been submitted. Click End Test to continue.</div>";
            if (IsLastQuestion)
            {
                ibIntro_S.Visible = true;
                ibIntro_S.ImageUrl = "../images/btn_endtest.gif";
                ibIntro_S.AlternateText = @"End test";
                ibIntro_D.Visible = false;
                ibIntro_D.Visible = false;
                ibCalc.Visible = false;
                CalBar.Visible = false;
            }
            else
            {
                ibIntro_S.Visible = false;
                ibIntro_D.Visible = true;
            }
        }

        public void SetRemediationCtrl(int userTestId, string action)
        {
            body.Attributes.Add("onbeforeunload", "saveRemediationTime();");
            txtAction.Value = action;
            txtUserTestID.Value = userTestId.ToString();
            btnNextIncorrect.Visible = true;
        }

        public void PopulateQuestions(Question question, IEnumerable<Lippincott> lippincotts, bool IsFirstUserQuestion)
        {
            lbltimer.Visible = true;
            imtimer.Visible = true;
            Explanation.Visible = true;
            if (Student.Action != Action.Review)
            {
                if (Student.QuizOrQBank == NursingLibrary.Interfaces.TestType.Quiz ||
                    Student.QuizOrQBank == NursingLibrary.Interfaces.TestType.Qbank)
                {
                    if (ContinueTiming.Value == "1")
                    {
                        remaining.Value = Remaining;
                    }

                    if (ContinueTiming.Value == "0")
                    {
                        lbltimer.Text = @"Time Remaining : expired";
                    }

                    if (ContinueTiming.Value == "3")
                    {
                        Remaining = "100";
                    }

                   // if (!IsProctorTrackEnabled || IsPostBack)  //to do not sure this logic is right, needs more testing
                   // {
                       body.Attributes.Add("onload", "javascript:startTimer(" + Remaining + ",false);");
                   // }

                }

                btnNext.Enabled = false;
                btnNextIncorrect.Visible = false;
                btnBack.Visible = false;
                btnBackIncorrect.Visible = false;
                btnBack.Enabled = false;
                btnQuit.Visible = true;
                btnQuit.ImageUrl = "../images/quit_new.gif";
                btnNext.Visible = true;
                ibSuspend.Visible = Student.TestType != NursingLibrary.Interfaces.TestType.Integrated;
                ibCalc.Visible = true;
                CalBar.Visible = true;
                Explanation.InnerHtml = string.Empty;
                remediation.InnerHtml = string.Empty;
                Explanation.Visible = false;
                remediation.Visible = false;
                Explanation_Div.Visible = false;
                if (Student.QuizOrQBank == NursingLibrary.Interfaces.TestType.Qbank)
                {
                    if (TimedTestQB == "0")
                    {
                        Explanation_Div.Visible = true;
                        hdnExplanationVisible.Value = "1";
                        Explanation.InnerHtml = string.Format("<P><B>Explanation</B><P><P>{0}<P>", ShowPicture(question.Explanation));
                        hdnExplanation.Value = Explanation.InnerHtml;
                        remediation.Visible = false;
                        remediation.InnerHtml = string.Empty;
                    }

                    if (TimedTestQB == "0" && Student.Action == Action.QBankCreate && Student.ProductId == (int)ProductType.FocusedReviewTests)
                    {
                        Explanation_Div.Visible = false;
                    }

                    if (Student.Action == Action.QBankCreate && Student.ProductId == (int)ProductType.SkillsModules)
                    {
                        Explanation_Div.Visible = false;
                    }
                }
            }
            else
            {
                int explanationTime;
                if (IsSkillModuleLinkClicked)
                {
                    explanationTime = Timer;
                }
                else
                {
                    explanationTime = question.ExplanationTime;
                }

                timer_up.Value = explanationTime.ToString();
                lbltimer.Visible = true;
                lbltimer.Text = @"Time Remaining:";
                lbltimer.Width = Unit.Pixel(50);
                imtimer.Visible = true;

                int minS = explanationTime / 60;
                int secS = explanationTime - (minS * 60);
                body.Attributes.Add("onload", "javascript:Up('" + minS + ":" + secS + "');");

                if (IsFirstUserQuestion)
                {
                    btnBack.Enabled = false;
                    btnBackIncorrect.Enabled = false;
                    btnBack.Visible = false;
                    btnBackIncorrect.Visible = false;
                }
                else
                {
                    btnBack.Enabled = true;
                    btnBack.Visible = true;
                }

                if (Student.ProductId == (int)ProductType.SkillsModules && Student.IsSkillsModuleTest)
                {
                    if (HasAccessToSMVideo())
                    {
                        SkillModuleLink_Div.Visible = true;
                        Explanation.Visible = true;
                    }
                    else
                    {
                        SkillModuleLink_Div.Visible = false;
                        Explanation.Visible = false;
                    }

                    string[] testNames = lblTestName.Text.Split('.');
                    if (testNames.Any())
                    {
                        lbtnSkilModule.Text = testNames[0];
                        Presenter.SetSkillsModuleId();
                    }
                }

                lbltimer.Visible = false;
                imtimer.Visible = false;

                btnQuit.Visible = true;
                btnQuit.ImageUrl = "../images/btn_btr.gif";

                ibSuspend.Visible = false;
                btnNext.Enabled = true;

                Explanation_Div.Visible = true;
                lnkExplanation.Visible = false;
                ibCalc.Visible = false;
                CalBar.Visible = false;

                if (Student.TestType == NursingLibrary.Interfaces.TestType.FocusedReview || Student.TestType == NursingLibrary.Interfaces.TestType.Nclex)
                {
                    Explanation.Visible = true;
                    Explanation.InnerHtml = string.Format("<P><B>Explanation</B><P><P>{0}<P>", ShowPicture(question.Explanation));
                    if (Student.TestType == NursingLibrary.Interfaces.TestType.Nclex)
                    {
                        remediation.Visible = false;
                        remediation.InnerHtml = string.Empty;
                    }
                }

                if (Student.TestType == NursingLibrary.Interfaces.TestType.SkillsModules && Student.IsSkillsModuleTest)
                {
                    if (!string.IsNullOrEmpty(question.Explanation))
                    {
                        Explanation.InnerHtml = string.Format("<P><B>Review the Skill</B><P><P>{0}<P>", ShowPicture(question.Explanation));
                    }
                    else if (IsFirstUserQuestion)
                    {
                        Explanation.InnerHtml = string.Format("<P><B>Review the Skill</B>");
                    }
                }

                if (Student.TestType == NursingLibrary.Interfaces.TestType.Integrated)
                {
                    Explanation.InnerHtml = string.Empty;
                    Explanation.Visible = false;
                    remediation.Visible = true;
                    stem.Visible = false;
                    stem.InnerHtml = string.Empty;
                    divStem.Visible = false;
                    divStem.InnerHtml = string.Empty;
                    D_Answers.Visible = false;
                    D_Answers.InnerHtml = string.Empty;
                    divD_Answers.Visible = false;
                    divD_Answers.InnerHtml = string.Empty;
                    D_QID.Visible = false;
                    D_QID.InnerHtml = string.Empty;

                    if (question.RemediationId > 0)
                    {
                        ShowLippincott(lippincotts, RemediationHtml);
                        if (RemediationHtml == string.Empty)
                        {
                            remediation.Visible = false;
                        }
                    }
                }

                if (Student.TestType == NursingLibrary.Interfaces.TestType.FocusedReview)
                {
                    remediation.Visible = true;
                    if (question.RemediationId > 0)
                    {
                        ShowLippincott(lippincotts, RemediationHtml);
                        if (RemediationHtml == string.Empty)
                        {
                            remediation.Visible = false;
                        }
                    }
                }
            }

            lblQNumber.Text = txtQuestionNumber.Text + @" of " + Student.NumberOfQuestions;
            lblQNumber.Visible = true;
            question_main.Visible = true;
            stem.Visible = true;
            D_Answers.Visible = true;
            divStem.Visible = true;
            divD_Answers.Visible = true;
            remediation.Visible = true;
            ibSkip.Visible = false;
            intro_main.Visible = false;
            intro_title.InnerHtml = string.Empty;
            intro_text.InnerHtml = string.Empty;
            intro_text.Visible = false;
            intro_title.Visible = false;

            tutorial_main.Visible = false;
            tutorial_main.InnerHtml = string.Empty;
            Tutorial_title.InnerHtml = string.Empty;
            Tutorial_title.Visible = false;
            Tutorial.InnerHtml = string.Empty;
            Tutorial.Visible = false;
            stem.InnerHtml = ShowPicture(question.Stem);
            divStem.InnerHtml = ShowPicture(question.Stem);
            hdnDivStem.Value = divStem.InnerHtml;
            Literal_Player.Text = string.Empty;
            txtA1.Text = string.Empty;
        }

        public void SetAnswerTrack(Question userQuestions, bool AnswersForQuestionExists)
        {
            txtA.Value = userQuestions.AnswserTrack;
            txtA1.Text = userQuestions.AnswserTrack;
            ViewState["PreviousTimeSpent"] = userQuestions.TimeSpendForQuestion;
            if (AnswersForQuestionExists)
            {
                btnNext.Enabled = true;
            }
        }

        public void HideShowPreviousIncorrectButton(bool show)
        {
            btnBackIncorrect.Visible = show;
        }

        public void PopulateRemediation(Question question, bool IsFirstUserQuestion)
        {
            timer_up.Value = question.RemediationTime.ToString();
            lbltimer.Visible = true;
            lbltimer.Text = @"Time:";
            lbltimer.Width = Unit.Pixel(30);
            imtimer.Visible = true;

            int minS = question.RemediationTime / 60;
            int secS = question.RemediationTime - (minS * 60);
            body.Attributes.Add("onload", "javascript:Up('" + minS + ":" + secS + "');");

            match.Visible = false;
            hotspot_D.Visible = false;

            hotspot_D.InnerHtml = string.Empty;
            match.InnerHtml = string.Empty;

            divmatch.Visible = false;
            divhotspot_D.Visible = false;

            divhotspot_D.InnerHtml = string.Empty;
            divmatch.InnerHtml = string.Empty;

            lblQNumber.Text = txtQuestionNumber.Text + @" of " + Student.NumberOfQuestions;
            lblQNumber.Visible = true;
            question_main.Visible = true;

            intro_main.Visible = false;
            intro_text.Visible = false;
            intro_title.Visible = false;
            intro_title.InnerHtml = string.Empty;
            intro_text.InnerHtml = string.Empty;
            intro_main.InnerHtml = string.Empty;

            tutorial_main.Visible = false;
            Tutorial_title.Visible = false;
            Tutorial.Visible = false;
            tutorial_main.InnerHtml = string.Empty;
            Tutorial_title.InnerHtml = string.Empty;
            Tutorial.InnerHtml = string.Empty;

            ibCalc.Visible = false;
            CalBar.Visible = false;
            btnQuit.Visible = true;
            btnQuit.ImageUrl = "../images/btn_btr.gif";

            btnBack.Visible = true;
            btnNext.Visible = true;
            btnNext.Enabled = true;
            btnNext.ToolTip = string.Empty;
            btnQuit.Visible = true;
            ibSuspend.Visible = false;
            ibSkip.Visible = false;

            if (IsFirstUserQuestion)
            {
                btnBack.Visible = false;
                btnBackIncorrect.Visible = false;
                btnBack.Enabled = false;
                btnNext.Enabled = true;
            }
            else
            {
                btnBack.Visible = true;
                btnBack.Enabled = true;
                btnNext.Enabled = true;
            }
        }

        public void ShowLippincott(IEnumerable<Lippincott> lippincotts, string remediationHtml)
        {
            var ca = new LippincottCard(lippincotts.Select(lp => lp.LippincottTitle).ToArray(), lippincotts.Select(lp => lp.LippincottExplanation).ToArray(), lippincotts.Select(lp => lp.LippincottTitle2).ToArray(), lippincotts.Select(lp => lp.LippincottExplanation2).ToArray(), remediationHtml)
            {
                ImgRemediationUrl = "~/Images/exp01.gif",
                ImgLippincottUrl = "~/Images/exp02.gif",
                ImgLippincott2Url = "~/Images/exp03.gif"
            };
            Lippincott.Controls.Add(ca);

            if (Student.TestType == NursingLibrary.Interfaces.TestType.Integrated)
            {
                Explanation.InnerHtml = string.Empty;
                Explanation.Visible = false;
                remediation.Visible = true;
            }

            EnableLabel = true;
        }

        public void ShowMessageCtrl(bool show)
        {
            MessageRetire.Visible = show;
            if (show)
            {
                question_main.Visible = false;
            }

            MessageRetireText.Text = show ? "In an effort to give you the best possible experience, this question has been retired. Please continue with your review." : string.Empty;
        }

        public void PopulateDisclamer(Question question)
        {
            lblQNumber.Visible = false;
            match.Visible = false;
            hotspot_D.Visible = false;
            hotspot_D.InnerHtml = string.Empty;
            match.InnerHtml = string.Empty;

            divmatch.Visible = false;
            divhotspot_D.Visible = false;
            divhotspot_D.InnerHtml = string.Empty;
            divmatch.InnerHtml = string.Empty;

            lbltimer.Visible = false;
            imtimer.Visible = false;

            btnBack.Visible = false;
            btnBackIncorrect.Visible = false;
            btnNext.Visible = false;
            btnNextIncorrect.Visible = false;
            btnQuit.Visible = false;
            ibSkip.Visible = false;
            ibSuspend.Visible = false;

            intro_main.Visible = true;
            tutorial_main.Visible = false;
            question_main.Visible = false;
            intro_title.InnerHtml = "<h3>" + question.ItemTitle + "</h3>";
            intro_text.InnerHtml = question.Stem;

            ibIntro_S.Visible = false;
            ibIntro_D.Visible = true;
            ibIntro_D.ImageUrl = "../images/btn_dis.gif";
        }

        public void PopulateEndForAllPages()
        {
            txtFileType.Text = @"04";

            lblQNumber.Visible = false;
            match.Visible = false;
            hotspot_D.Visible = false;
            hotspot_D.InnerHtml = string.Empty;
            match.InnerHtml = string.Empty;

            divmatch.Visible = false;
            divhotspot_D.Visible = false;
            divhotspot_D.InnerHtml = string.Empty;
            divmatch.InnerHtml = string.Empty;

            btnBack.Visible = false;
            btnBackIncorrect.Visible = false;
            btnNext.Visible = false;
            btnNextIncorrect.Visible = false;
            btnQuit.Visible = false;
            ibSkip.Visible = false;
            ibSuspend.Visible = false;

            intro_main.Visible = true;
            tutorial_main.Visible = false;
            question_main.Visible = false;
            intro_title.InnerHtml = "<h3>End of Test</h3>";
            intro_text.Visible = true;
            intro_text.InnerHtml = "<div style='text-align:center;'>Congratulations, your test has been submitted. Click End Test to continue.</div>";

            ibIntro_S.Visible = true;
            ibIntro_S.ImageUrl = "../images/btn_endtest.gif";
            ibIntro_S.AlternateText = @"End test";
            ibIntro_D.Visible = false;
            ibCalc.Visible = false;
            CalBar.Visible = false;
        }

        public void PopulateIntroduction(Question question, bool IsLastQuestion)
        {
            lblQNumber.Visible = false;
            match.Visible = false;
            hotspot_D.Visible = false;
            hotspot_D.InnerHtml = string.Empty;
            match.InnerHtml = string.Empty;

            divmatch.Visible = false;
            divhotspot_D.Visible = false;
            divhotspot_D.InnerHtml = string.Empty;
            divmatch.InnerHtml = string.Empty;

            lbltimer.Visible = false;
            imtimer.Visible = false;

            btnBack.Visible = false;
            btnBackIncorrect.Visible = false;
            btnNextIncorrect.Visible = false;
            btnNext.Visible = false;
            btnQuit.Visible = false;
            ibSkip.Visible = false;
            ibSuspend.Visible = false;

            intro_main.Visible = true;
            tutorial_main.Visible = false;
            question_main.Visible = false;
            intro_title.InnerHtml = "<h3>" + question.ItemTitle + "</h3>";
            intro_text.InnerHtml = question.Stem;

            if (IsLastQuestion)
            {
                ibIntro_S.Visible = true;
                ibIntro_S.ImageUrl = "../images/btn_start.gif";
                ibIntro_D.Visible = false;
            }
            else
            {
                ibIntro_S.Visible = false;
                ibIntro_D.Visible = true;
                ibIntro_D.ImageUrl = "../images/btn_dis.gif";
            }
        }

        public void PopulateTutorial(Question question, bool IsFirstQuestion)
        {
            btnNext.Visible = true;
            lblQNumber.Text = txtQuestionNumber.Text + @" of " + Student.NumberOfQuestions;

            lbltimer.Visible = false;
            imtimer.Visible = false;

            match.Visible = false;
            hotspot_D.Visible = false;

            hotspot_D.InnerHtml = string.Empty;
            match.InnerHtml = string.Empty;

            divmatch.Visible = false;
            divhotspot_D.Visible = false;

            divhotspot_D.InnerHtml = string.Empty;
            divmatch.InnerHtml = string.Empty;

            ibSkip.Visible = Student.Action != Action.Review;
            ibSuspend.Visible = false;
            btnQuit.Visible = true;

            question_main.Visible = false;

            intro_main.Visible = false;
            intro_title.InnerHtml = string.Empty;
            intro_text.InnerHtml = string.Empty;

            tutorial_main.Visible = true;
            Tutorial_title.InnerHtml = "<h3>" + question.ItemTitle + "</h3>";
            Tutorial.InnerHtml = question.Stem;
            Tutorial.Visible = true;
            if (IsFirstQuestion)
            {
                btnBack.Visible = false;
                btnBackIncorrect.Visible = false;
            }
            else
            {
                btnBack.Visible = true;
            }
        }

        public string ShowPicture(string str)
        {
            string sStem = str.Trim();
            int intQ = sStem.Trim().IndexOf("<Picture=", 0);
            if (intQ > 0)
            {
                int intE = sStem.Trim().IndexOf("/>", intQ);
                string leftPart = sStem.Substring(0, intQ);
                string pictureName = sStem.Substring(intQ + 9, intE - 9 - intQ);
                string rightPart = sStem.Substring(intE + 2);

                sStem = string.Format("{0}<P><img src=\"..\\Content\\{1}\"/></P>{2}", leftPart, pictureName, rightPart);
            }

            return sStem;
        }

        public string ConvertToNumbers(string letter)
        {
            string rezults = "0";
            switch (letter)
            {
                case "A":
                    rezults = "1";
                    break;
                case "B":
                    rezults = "2";
                    break;
                case "C":
                    rezults = "3";
                    break;
                case "D":
                    rezults = "4";
                    break;
                case "E":
                    rezults = "5";
                    break;
                case "F":
                    rezults = "6";
                    break;
                case "G":
                    rezults = "7";
                    break;
            }

            return rezults;
        }

        public void PopulateAlternateTextDetails(Question question)
        {
            ucAlternateIntro.PopulateAlternateTextDetails(question);
            if (Student.ProductId == 3 && (Student.Action == Action.Remediation || Student.Action == Action.Review))
            {
                if (string.IsNullOrEmpty(question.AlternateStem.Trim()))
                {
                    SetTabAlternateVisibility = false;
                }
                else
                {
                    SetTabAlternateVisibility = true;
                }
            }
        }

        public void PopulateAlternateTextDetails(IEnumerable<UserAnswer> answers, IEnumerable<UserAnswer> userAnswers, Student Student)
        {
            ucAlternateIntro.PopulateAlternateTextDetails(answers, userAnswers, Student);
        }

        public void PopulateAlternateTextDetails(IEnumerable<UserAnswer> answers, Question question, IEnumerable<UserAnswer> hotSpotAnswers, Student student)
        {
            ucAlternateIntro.PopulateAlternateTextDetails(answers, question, hotSpotAnswers, student);
        }

        public void PopulateAlternateTextDetails(IEnumerable<UserAnswer> answers, Question question, Student student)
        {
            ucAlternateIntro.PopulateAlternateTextDetails(answers, question, student);
        }

        #endregion

        #region Protected methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Presenter.OnViewInitialized();
                Test = lblTestName.Text;
            }
            else
            {
                lblTestName.Text = Test;
            }

            if (Page.Request.Params.Get("__EVENTTARGET") != "lbtnSkilModule")
            {
                txtAction.Value = string.Empty;
                Remaining = remaining.Value;
                Presenter.OnViewLoaded();
                if (ContinueTiming.Value == "1")
                {
                    remaining.Value = Remaining;
                }
            }
            else
            {
                IsSkillModuleLinkClicked = true;
                Presenter.OnViewInitialized();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Student.Action == Action.Review)
            {
                btnNext.ToolTip = string.Empty;
            }
        }

        protected void IbIntroDClick(object sender, ImageClickEventArgs e)
        {
            TraceHelper.WriteTraceEvent(TraceToken, "Intro.IntroD Click Begin");
            Presenter.OnIbIntroDClick();
            TraceHelper.WriteTraceEvent(TraceToken, "Intro.IntroD Click End");
        }

        protected void IbIntroSClick(object sender, ImageClickEventArgs e)
        {
            TraceHelper.WriteTraceEvent(TraceToken, "Intro.IntroS Click Begin");
            Presenter.OnIbIntroSClick();
            TraceHelper.WriteTraceEvent(TraceToken, "Intro.IntroS Click End");
        }

        protected void LnkExplanationClick(object sender, EventArgs e)
        {
            if ((Student.Action == Action.QBankCreate) || (Student.Action == Action.Resume))
            {
                if (Student.QuizOrQBank == NursingLibrary.Interfaces.TestType.Qbank)
                {
                    if (TimedTestQB == "0")
                    {
                        lbltimer.Text = string.Empty;
                        ContinueTiming.Value = "3";
                        lblQNumber.Text = txtQuestionNumber.Text + @" of " + Student.NumberOfQuestions;
                        btnBack.Visible = false;
                        hdnExplanationVisible.Value = hdnExplanationVisible.Value == "0" ? "1" : "0";
                    }
                }
            }

            var literal = new Literal
            {
                Text = @"<div><script type=""text/javascript"">EnableNextButtonexp();</script></div>"
            };
            Page.Controls.Add(literal);
            Explanation.Visible = hdnExplanationVisible.Value != "1";
        }

        protected void BtnBackIncorrectClick(object sender, ImageClickEventArgs e)
        {
            TraceHelper.WriteTraceEvent(TraceToken, "Intro.Back InCorrect Click Begin");
            Presenter.OnBackIncorrectClick();
            Presenter.PopulateAlternateTextDetails(Student);
            TabQuestion.ActiveTabIndex = 0;
            TraceHelper.WriteTraceEvent(TraceToken, "Intro.Back InCorrect Click End");
        }

        protected void BtnNextIncorrectClick(object sender, ImageClickEventArgs e)
        {
            TraceHelper.WriteTraceEvent(TraceToken, "Intro.Next InCorrect Click Begin");
            body.Attributes.Clear();
            BlockCopyPaste();
            IntExhibit();
            Presenter.OnNextIncorrectClick(btnNext.Visible);
            Presenter.PopulateAlternateTextDetails(Student);
            TabQuestion.ActiveTabIndex = 0;
            TraceHelper.WriteTraceEvent(TraceToken, "Intro.Next InCorrect Click End");
        }

        protected void IbSkipClick(object sender, ImageClickEventArgs e)
        {
            Presenter.OnIbSkipClick();
        }

        protected void BtnQuitClick(object sender, ImageClickEventArgs e)
        {
            TraceHelper.Create(TraceToken, "Intro.Quit Click Begin")
                .Add("Question Id", Student.QuestionId.ToString())
                .Write();
            Presenter.OnQuitClick();
            TraceHelper.WriteTraceEvent(TraceToken, "Intro.Quit Click End");
        }

        protected void IbSuspendClick(object sender, ImageClickEventArgs e)
        {
            TraceHelper.WriteTraceEvent(TraceToken, "Intro.Suspend Click Begin");
            Presenter.GotoReviewPage();
        }

        protected void btnHiddenQuitClick(object sender, EventArgs e)
        {
            Presenter.OnQuitClick();
        }

        protected void BtnBackClick(object sender, ImageClickEventArgs e)
        {
            TraceHelper.WriteTraceEvent(TraceToken, "Intro.Back Click Begin");
            Presenter.OnBackClick();
            Presenter.PopulateAlternateTextDetails(Student);
            TabQuestion.ActiveTabIndex = 0;
            TraceHelper.WriteTraceEvent(TraceToken, "Intro.Back Click End");
        }

        protected void BtnNextClick(object sender, ImageClickEventArgs e)
        {
            TraceHelper.WriteTraceEvent(TraceToken, "Intro.Next Click Begin");
            body.Attributes.Clear();
            BlockCopyPaste();
            IntExhibit();
            Presenter.OnNextClick(btnNext.Visible);
            Presenter.PopulateAlternateTextDetails(Student);
            TabQuestion.ActiveTabIndex = 0;
            TraceHelper.WriteTraceEvent(TraceToken, "Intro.Next Click End");
        }

        protected void TabQuestion_ActiveTabChanged(object sender, EventArgs e)
        {
            TabContainer tabContainer = (TabContainer)sender;
            if (tabContainer != null)
            {
                TabQuestion.ActiveTabIndex = tabContainer.ActiveTabIndex;
                TabIndex = tabContainer.ActiveTabIndex;
            }

            Presenter.OnAltTabClick();
            Presenter.PopulateAlternateTextDetails(Student);
        }

        protected void lbtnSkilModule_Click(object sender, EventArgs e)
        {
            int testId = Presenter.GetOriginalSMTestId();
            popupFrame.Attributes.Add("src", "SkillModulePopUp.aspx?smId=" + testId + "&FromIntroReview=1");
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), string.Empty, "openPopUp()", true);
            Presenter.UpdateRemediation();
        }

        #endregion

        #region Private methods

        private bool HasAccessToSMVideo()
        {
            bool HasAccessToSM = false;
            if (hdnHasAccessToSM.Value == "1")
            {
                HasAccessToSM = true;
            }
            else if (hdnHasAccessToSM.Value != "0")
            {
                int testId = Presenter.GetOriginalSMTestId();
                IEnumerable<Test> untakenTests = Presenter.GetUnTakenTests();
                if (untakenTests.Where(r => r.TestId == testId).Count() > 0)
                {
                    hdnHasAccessToSM.Value = "1";
                    HasAccessToSM = true;
                }
                else
                {
                    hdnHasAccessToSM.Value = "0";
                    HasAccessToSM = false;
                }
            }

            return HasAccessToSM;
        }

        private void BlockCopyPaste()
        {
            body.Attributes.Add("onselectstart", "return   false");
            body.Attributes.Add("onpaste", "return   false");
            body.Attributes.Add("oncopy", "return   false");
            body.Attributes.Add("oncut", "return   false");
        }

        private void IntExhibit()
        {
            imgExhibit.Style.Add("visibility", "hidden");
            PanelExhibit.Visible = false;
            question_main.Style.Add("height", "400px");
            divExhibit.Style.Add("height", "0px");
        }

        private string ConvertToLetter(string number)
        {
            string rezults = "0";
            switch (number)
            {
                case "1":
                    rezults = "A";
                    break;
                case "2":
                    rezults = "B";
                    break;
                case "3":
                    rezults = "C";
                    break;
                case "4":
                    rezults = "D";
                    break;
                case "5":
                    rezults = "E";
                    break;
                case "6":
                    rezults = "F";
                    break;
                case "7":
                    rezults = "G";
                    break;
            }

            return rezults;
        }

        #endregion
    }
}