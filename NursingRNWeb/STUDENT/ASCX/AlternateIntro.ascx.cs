using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;
using Action = NursingLibrary.Presenters.Action;

namespace STUDENT.ASCX
{
    public partial class AlternateIntro : UserControl
    {
        public bool Resume { get; set; }

        public string AlternateStem { get; set; }

        public void FillMultipleChoiceFields(IEnumerable<UserAnswer> answers, IEnumerable<UserAnswer> userAnswers, Student student)
        {
            altMatch.Visible = false;
            althotspot_D.Visible = false;
            althotspot_D.InnerHtml = string.Empty;
            altMatch.InnerHtml = string.Empty;

            Intro introPage = new Intro();

            var iChoice = 0;
            D_AltAnswers.Controls.Clear();
            var table = new HtmlTable { CellPadding = 8 };
            foreach (var altAnswer in answers)
            {
                if (altAnswer.AlternateAText.Trim() != string.Empty)
                {
                    iChoice++;
                    var radoibuttonI = new RadioButton
                    {
                        ID = "ARB_" + Convert.ToString(iChoice),
                        Text =
                            string.Format(@"{0}.&nbsp;{1}", introPage.ConvertToNumbers(altAnswer.AIndex),
                                          introPage.ShowPicture(altAnswer.AlternateAText)),
                        GroupName = "ARB"
                    };

                    radoibuttonI.Attributes.Add("onclick", "EnableNextButton('" + iChoice + "', 'alternate');");

                    var row = new HtmlTableRow();
                    table.Controls.Add(row);
                    var cell = new HtmlTableCell();
                    row.Controls.Add(cell);
                    cell.Controls.Add(radoibuttonI);

                    var textbox = new TextBox
                    {
                        ID = "ATB_" + Convert.ToString(iChoice),
                        Text = altAnswer.AnswerID.ToString(),
                        Visible = false
                    };
                    cell = new HtmlTableCell();
                    row.Controls.Add(cell);
                    cell.Controls.Add(textbox);

                    var textboxC = new TextBox
                    {
                        ID = "ATB_C_" + Convert.ToString(iChoice),
                        Text = altAnswer.Correct.ToString(),
                        Visible = false
                    };

                    cell = new HtmlTableCell();
                    row.Controls.Add(cell);
                    cell.Controls.Add(textboxC);

                    if (altAnswer.Correct == 1)
                    {
                        ////txtA1.Text = altAnswer.AIndex;
                    }

                    bool answersForQuestionExists = userAnswers.FirstOrDefault(ans => ans.QID == student.QuestionId) != null;
                    if (student.Action == Action.Review || answersForQuestionExists)
                    {
                        if (student.Action == Action.Review && altAnswer.Correct == 1)
                        {
                            radoibuttonI.BackColor = Color.FromArgb(228, 240, 216);
                        }

                        UserAnswer answer1 = altAnswer;
                        var correctAnswer = userAnswers.FirstOrDefault(ans => ans.QID == student.QuestionId && ans.AnswerID == answer1.AnswerID) != null;
                        if (correctAnswer)
                        {
                            radoibuttonI.Checked = true;
                        }
                    }

                    if (student.Action == Action.Review)
                    {
                        radoibuttonI.Enabled = false;
                    }
                }
            }

            D_AltAnswers.Controls.Add(table);
        }

        public void FillTheBlankFields(IEnumerable<UserAnswer> answers, IEnumerable<UserAnswer> userAnswers, Student student)
        {
            altMatch.Visible = false;
            althotspot_D.Visible = false;
            althotspot_D.InnerHtml = string.Empty;
            altMatch.InnerHtml = string.Empty;

            if (answers.Count() > 0)
            {
                foreach (var answer in answers)
                {
                    var textbox = new TextBox();
                    bool answersForQuestionExists = userAnswers.FirstOrDefault(ans => ans.QID == student.QuestionId) != null;
                    if (answersForQuestionExists)
                    {
                        UserAnswer filteredAnswer = userAnswers.FirstOrDefault(ans => ans.QID == student.QuestionId && ans.AnswerID == answer.AnswerID);
                        textbox.Text = (filteredAnswer != null) ? filteredAnswer.AText : string.Empty;
                    }
                    else
                    {
                        textbox.Text = string.Empty;
                    }

                    textbox.ID = "Atx";
                    D_AltAnswers.Controls.Clear();
                    D_AltAnswers.Controls.Add(textbox);

                    textbox.Attributes.Add("onClick", "document.getElementById('" + textbox.ClientID + "').focus()");
                    textbox.Attributes.Add("onkeyup", "CheckTxt('alternate');");
                    textbox.Enabled = true;
                    if (string.IsNullOrEmpty(textbox.Text) && student.Action != Action.Remediation && student.Action != Action.Review)
                    {
                        ////btnNext.Enabled = false;
                    }

                    var label = new Label { ID = "ALB", Text = answer.Unit };
                    D_AltAnswers.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
                    D_AltAnswers.Controls.Add(label);

                    var textboxB = new TextBox { ID = "ATB_1", Text = answer.AnswerID.ToString(), Visible = false };
                    D_AltAnswers.Controls.Add(textboxB);
                    D_AltAnswers.Controls.Add(new LiteralControl("<br/>"));

                    var textboxC = new TextBox { ID = "ATB_C_1", Text = answer.AlternateAText, Visible = false };
                    D_AltAnswers.Controls.Add(textboxC);
                    D_AltAnswers.Controls.Add(new LiteralControl("<br/>"));

                    if (student.Action == Action.Review)
                    {
                        textboxC.Enabled = false;
                    }
                }
            }
        }

        public void FillMultipleChoiceMultiSelectFields(IEnumerable<UserAnswer> answers, IEnumerable<UserAnswer> userAnswers, Student student)
        {
            altMatch.Visible = false;
            althotspot_D.Visible = false;
            althotspot_D.InnerHtml = string.Empty;
            altMatch.InnerHtml = string.Empty;

            Intro alternateIntro = new Intro();
            int iChoice = 0;
            D_AltAnswers.Controls.Clear();
            foreach (var answer in answers)
            {
                if (answer.AlternateAText.Trim() != string.Empty)
                {
                    iChoice++;
                    var checkboxI = new CheckBox
                    {
                        ID = "ACH_" + Convert.ToString(iChoice),
                        Text =
                            string.Format("{0}.&nbsp;{1}", alternateIntro.ConvertToNumbers(answer.AIndex),
                                          alternateIntro.ShowPicture(answer.AlternateAText))
                    };

                    var textbox = new TextBox
                    {
                        ID = "ATB_" + Convert.ToString(iChoice),
                        Text = answer.AnswerID.ToString(),
                        Visible = false
                    };
                    D_AltAnswers.Controls.Add(textbox);
                    D_AltAnswers.Controls.Add(new LiteralControl("<br/>"));

                    var textboxC = new TextBox
                    {
                        ID = "ATB_C_" + Convert.ToString(iChoice),
                        Text = answer.Correct.ToString(),
                        Visible = false
                    };
                    D_AltAnswers.Controls.Add(textboxC);
                    D_AltAnswers.Controls.Add(new LiteralControl("<br/>"));
                    D_AltAnswers.Controls.Add(checkboxI);
                    D_AltAnswers.Controls.Add(new LiteralControl("<br/>"));

                    bool answersForQuestionExists = userAnswers.FirstOrDefault(ans => ans.QID == student.QuestionId) != null;
                    if (student.Action == Action.Review || answersForQuestionExists)
                    {
                        if (student.Action == Action.Review && answer.Correct == 1)
                        {
                            checkboxI.BackColor = Color.FromArgb(228, 240, 216);
                        }

                        // check the answer
                        UserAnswer answer1 = answer;
                        var correctAnswer = userAnswers.FirstOrDefault(ans => ans.QID == student.QuestionId && ans.AnswerID == answer1.AnswerID) != null;
                        if (correctAnswer)
                        {
                            checkboxI.Checked = true;
                        }
                    }

                    checkboxI.Attributes.Add("onclick", "CheckForSelected('alternate');");
                    if (student.Action == Action.Review)
                    {
                        checkboxI.Enabled = false;
                    }
                }
            }
        }

        public void PopulateAlternateTextDetails(IEnumerable<UserAnswer> answers, IEnumerable<UserAnswer> userAnswers, Student student)
        {
            switch (student.QuestionType)
            {
                case QuestionType.MultiChoiceSingleAnswer:
                    {
                        FillMultipleChoiceFields(answers, userAnswers, student);
                    }

                    break;
                case QuestionType.MultiChoiceMultiAnswer:
                    {
                        FillMultipleChoiceMultiSelectFields(answers, userAnswers, student);
                    }

                    break;
                case QuestionType.Number:
                    {
                        FillTheBlankFields(answers, userAnswers, student);
                    }

                    break;
            }
        }

        public void PopulateAlternateTextDetails(Question question)
        {
            Intro altIntro = new Intro();
            if (!string.IsNullOrEmpty(question.AlternateStem.Trim()))
            {
                altStem.InnerHtml = altIntro.ShowPicture(question.AlternateStem);
            }
            else
            {
                altStem.InnerHtml = string.Empty;
            }
        }

        public string GenerateXMLForMatch(IEnumerable<UserAnswer> userAnswers)
        {
            var xmlstring = userAnswers.Aggregate(@"<orderingAnswerSet width=""700"" height=""250"" >", (current, answer) => string.Format("{0}<orderAnswer initialPos=\"{1}\">{2}</orderAnswer>", current, answer.initialPos.ToString(), answer.AlternateAText));
            xmlstring = string.Format("{0}</orderingAnswerSet>", xmlstring);
            return xmlstring;
        }

        public string GenerateXMLForHotSpotNormalModeWithoutEnd(IEnumerable<UserAnswer> hotSpotAnswers)
        {
            return hotSpotAnswers.Aggregate("shapeString=", (current, hotSpotAnswer) => string.Format("{0}{1}&imagePath=flash/{2}&responseSeq=0&mode=sim", current, hotSpotAnswer.AlternateAText, hotSpotAnswer.Stimulus.Trim()));
        }

        public string GenerateReviewXMLForMatch(Question userQuestion)
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

        public string GenerateReviewXMLForHotSpot(Question userQuestion, IEnumerable<UserAnswer> hotSpotAnswers, string Mode)
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

        public void PopulateAlternateTextDetails(IEnumerable<UserAnswer> userAnswers, Question userQuestion, IEnumerable<UserAnswer> hotSpotAnswers, Student student)
        {
            altMatch.Visible = false;
            althotspot_D.Visible = true;
            altMatch.InnerHtml = string.Empty;
            D_AltAnswers.InnerHtml = string.Empty;
            D_AltAnswers.Visible = false;
            string shapeString;
            var answersForQuestionExists = userAnswers.FirstOrDefault(ans => ans.QID == student.QuestionId) != null;
            if (student.Action == Action.Review || answersForQuestionExists)
            {
                shapeString = GenerateReviewXMLForHotSpot(userQuestion, hotSpotAnswers, Resume && answersForQuestionExists ? "Redo" : "Review");
            }
            else
            {
                shapeString = GenerateXMLForHotSpotNormalModeWithoutEnd(hotSpotAnswers);
            }

            var swf = "<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" codebase=\"http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0\" width=\"549\" height=\"600\" id=\"hotSpotComp\" align=\"middle\">";
            swf = string.Format("{0} <param name=\"allowScriptAccess\" value=\"sameDomain\" />", swf);
            swf = string.Format("{0} <param name=\"movie\" value=\"flash/hotSpotComp.swf\" />", swf);
            swf = string.Format("{0} <param name=\"quality\" value=\"high\" />", swf);
            swf = string.Format("{0} <param name=\"scale\" value=\"noscale\" />", swf);
            swf = string.Format("{0} <param name=\"bgcolor\" value=\"#ffffff\" />", swf);
            swf = string.Format("{0} <param name=\"FlashVars\" value=\"{1}\" />", swf, shapeString);
            swf = string.Format("{0} <embed src=\"flash/hotSpotComp.swf\" quality=\"high\" scale=\"noscale\" bgcolor=\"#ffffff\" width=\"540\" height=\"600\" name=\"hotSpotComp\" align=\"middle\" allowScriptAccess=\"sameDomain\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" FlashVars=\"{1}\" /></object>", swf, shapeString);
            althotspot_D.InnerHtml = swf;
        }

        public void PopulateAlternateTextDetails(IEnumerable<UserAnswer> userAnswers, Question userQuestion, Student student)
        {
            altMatch.Visible = true;
            althotspot_D.InnerHtml = string.Empty;
            althotspot_D.Visible = false;
            D_AltAnswers.InnerHtml = string.Empty;
            D_AltAnswers.Visible = false;
            string browserType = Request.Browser.Type;
            if (String.Compare(browserType, "IE7", StringComparison.OrdinalIgnoreCase) == 0 || String.Compare(browserType, "IE8", StringComparison.OrdinalIgnoreCase) == 0)
            {
                string flashVar = GenerateXMLForMatch(userAnswers);
                if (student.Action == Action.Review)
                {
                    string interactionset = GenerateReviewXMLForMatch(userQuestion);
                    flashVar = "orderingAnswerSet=" + Server.UrlEncode(flashVar) + "&amp;mediaPrefix=/content/media/89/79389.24.&amp;mode=review&amp;interactionState=" + Server.UrlEncode(interactionset);
                }
                else
                {
                    flashVar = "orderingAnswerSet=" + Server.UrlEncode(flashVar) + "&amp;mediaPrefix=/content/media/89/79389.24.&amp;mode=sim&amp;interactionState=%3CorderMatch%20/%3E";
                }

                var swf = "<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" style=\"-khtml-user-drag:element;\"  codebase=\"http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#	version=7,0,0,0\" id=\"flashInteractionQuestions\" width=\"750\" height=\"250\">";
                swf = string.Format("{0} <param name=\"movie\" value=\"flash/orderingInteraction.swf\">", swf);
                swf = string.Format("{0} <param name=\"quality\" value=\"autohigh\">", swf);
                swf = string.Format("{0}  <param name=\"bgcolor\" value=\"#ffffff\">", swf);
                swf = string.Format("{0} <param name=\"scale\" value=\"noscale\">", swf);
                swf = string.Format("{0} <param name=\"menu\" value=\"false\">", swf);
                swf = string.Format("{0} <param name=\"FlashVars\" value=\"{1}\">", swf, flashVar);
                swf = string.Format("{0} <embed scale=\"noscale\" menu=\"false\" quality=\"high\"", swf);
                swf = string.Format("{0} bgcolor=\"#ffffff\" name=\"flashInteractionQuestions\" allowScriptAccess=\"sameDomain\" type=\"application/x-shockwave-flash\" swLiveConnect=\"true\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" src=\"flash/orderingInteraction.swf\" FlashVars=\"{1}\"  width=\"750\" height=\"250\">", swf, flashVar);
                swf = string.Format("{0} </embed>", swf);
                swf = string.Format("{0} </object>", swf);
                altMatch.InnerHtml = swf;
            }
            else
            {
                string html = ControlHelper.GetDragDropFormatedHtml(userAnswers, true);
                html = ControlHelper.GetDragDropFormatedHtmlForReview(userAnswers, userQuestion, true);
                altMatch.InnerHtml = html;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AlternateStem = altStem.InnerHtml;
        }
    }
}