using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class CMS_Controls_AlternateEditQuestion : UserControl
{
    public string AlternateStem { get; set; }

    public void PopulateAlternateTextDetails(Question question, UserAction actionType, List<AnswerChoice> answers)
    {
        AlternateStem = txtAltStem.Text;
        CMS_ViewQuestion viewQuestion = new CMS_ViewQuestion();
        if (actionType == UserAction.View)
        {
            txtAltStem.Visible = false;
            D_AltStem.InnerHtml = viewQuestion.ShowPicture(question.AlternateStem);
        }
        else
        {
            D_AltStem.Visible = false;
        }

        if (question != null)
        {
            if (question.QuestionType.ToString().Trim().ToInt() == (int)QuestionType.MultiChoiceSingleAnswer)
            {
                FillMultipleChoiceFields(actionType, answers);
            }
            else if (question.QuestionType.ToString().Trim().ToInt() == (int)QuestionType.Number)
            {
                FillBlankFields(actionType, answers);
            }
            else if (question.QuestionType.ToString().Trim().ToInt() == (int)QuestionType.Order)
            {
                FillMatchFields(actionType, answers);
            }
            else if (question.QuestionType.ToString().Trim().ToInt() == (int)QuestionType.MultiChoiceMultiAnswer)
            {
                FillMultipleChoiceMultiSelectFields(actionType, answers);
            }
            else if (question.QuestionType.ToString().Trim().ToInt() == (int)QuestionType.Hotspot)
            {
                FillHotSpotFields(actionType, answers);
            }
            else if (question.QuestionType.ToString().Trim().ToInt() == (int)QuestionType.Item)
            {
                D_AltAnswers.Controls.Clear();
            }

            if (actionType == UserAction.Edit || actionType == UserAction.Copy)
            {
                txtAltStem.Text = question.AlternateStem.ToString().Trim();
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private void FillMultipleChoiceFields(UserAction actionType, List<AnswerChoice> answers)
    {
        if (actionType == UserAction.View)
        {
            int i = 0;
            D_AltAnswers.Controls.Clear();
            foreach (var item in answers)
            {
                i++;
                Label RB_i = new Label();
                RB_i.ID = "ARB_" + Convert.ToString(i);
                var AlternateAText = item.AlternateAText.ToString();
                RB_i.Width = Unit.Pixel(400);
                RB_i.Text = i.ToString() + ".&nbsp;" + AlternateAText;

                D_AltAnswers.Controls.Add(RB_i);
                D_AltAnswers.Controls.Add(new LiteralControl("<br />"));

                if (item.Correct == 1)
                {
                    RB_i.BackColor = Color.FromArgb(228, 240, 216);
                }
            }
        }
        else
        {
            D_AltAnswers.Controls.Clear();
            if (actionType == UserAction.Add)
            {
                for (int i = 1; i < 7; i++)
                {
                    TextBox TB_i = new TextBox();
                    TB_i.Width = Unit.Pixel(400);
                    TB_i.TextMode = TextBoxMode.MultiLine;
                    TB_i.Rows = 3;
                    TB_i.ID = "ATB_" + Convert.ToString(i);
                    TB_i.Text = string.Empty;

                    D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                    D_AltAnswers.Controls.Add(TB_i);
                    D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                    if (i > 1 && i < 4)
                    {
                        D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                    }
                }
            }
            else if (actionType == UserAction.Edit || actionType == UserAction.Copy)
            {
                var count = answers.Count;
                int i = 0;
                D_AltAnswers.Controls.Clear();

                foreach (var item in answers)
                {
                    i++;
                    TextBox TB_i = new TextBox();
                    TB_i.Width = Unit.Pixel(400);
                    TB_i.TextMode = TextBoxMode.MultiLine;
                    TB_i.Rows = 3;
                    TB_i.ID = "ATB_" + Convert.ToString(i);
                    var AlternateAText = item.AlternateAText.ToString();
                    TB_i.Text = AlternateAText;

                    D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                    D_AltAnswers.Controls.Add(TB_i);
                    D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                    if (i > 1 && i < 4)
                    {
                        D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                    }
                }

                if (i < 7)
                {
                    for (int j = i + 1; j < 7; j++)
                    {
                        TextBox TB_i = new TextBox();
                        TB_i.Width = Unit.Pixel(400);
                        TB_i.TextMode = TextBoxMode.MultiLine;
                        TB_i.Rows = 3;
                        TB_i.ID = "ATB_" + Convert.ToString(j);
                        TB_i.Text = string.Empty;

                        D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                        D_AltAnswers.Controls.Add(TB_i);
                        D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                        if (i > 1 && i < 4)
                        {
                            D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                        }
                    }
                }
            }
        }
    }

    private void FillBlankFields(UserAction actionType, List<AnswerChoice> answers)
    {
        if (actionType == UserAction.View)
        {
            if (answers.Count > 0)
            {
                foreach (var item in answers)
                {
                    var AlternateAText = item.AlternateAText.ToString();
                    AlternateAText = AlternateAText.Replace("<P>", string.Empty);
                    AlternateAText = AlternateAText.Replace("</P>", string.Empty);

                    TextBox tx = new TextBox();
                    if (!AlternateAText.Trim().Equals(string.Empty))
                    {
                        tx.Text = AlternateAText.Trim();
                    }

                    tx.ID = "Atx";
                    tx.Enabled = false;
                    D_AltAnswers.Controls.Clear();
                    D_AltAnswers.Controls.Add(tx);

                    Label lb = new Label();
                    lb.ID = "ALB";
                    lb.Text = item.Unit;
                    D_AltAnswers.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
                    D_AltAnswers.Controls.Add(lb);
                }
            }
        }
        else
        {
            D_AltAnswers.Controls.Clear();
            if (actionType == UserAction.Add)
            {
                TextBox tx = new TextBox();
                tx.ID = "Atx";
                tx.Text = string.Empty;
                D_AltAnswers.Controls.Add(tx);
                D_AltAnswers.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
            }
            else if (actionType == UserAction.Edit || actionType == UserAction.Copy)
            {
                if (answers.Count > 0)
                {
                    var item = answers.FirstOrDefault();
                    var AlternateAText = item.AlternateAText.ToString();
                    AlternateAText = AlternateAText.Replace("<P>", string.Empty);
                    AlternateAText = AlternateAText.Replace("</P>", string.Empty);

                    TextBox tx = new TextBox();
                    tx.Width = Unit.Pixel(400);
                    if (!AlternateAText.Trim().Equals(string.Empty))
                    {
                        tx.Text = AlternateAText.Trim();
                    }

                    tx.ID = "Atx";
                    D_AltAnswers.Controls.Add(tx);
                    D_AltAnswers.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
                }
                else
                {
                    TextBox tx = new TextBox();
                    tx.ID = "Atx";
                    tx.Text = string.Empty;
                    D_AltAnswers.Controls.Add(tx);
                    D_AltAnswers.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
                }
            }
        }
    }

    private void FillMatchFields(UserAction actionType, List<AnswerChoice> answers)
    {
        if (actionType == UserAction.View)
        {
            int i = 0;
            D_AltAnswers.Controls.Clear();
            foreach (var item in answers)
            {
                i++;

                TextBox TB = new TextBox();
                TB.ID = "ATB" + i.ToString();
                TB.Text = item.AlternateAText.ToString();

                TextBox TB_P = new TextBox();
                TB_P.ID = "ATB_P" + i.ToString();
                TB_P.Text = item.InitialPosition.ToString();

                D_AltAnswers.Controls.Add(TB);
                D_AltAnswers.Controls.Add(new LiteralControl("<br />"));

                D_AltAnswers.Controls.Add(TB_P);
                D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
            }
        }
        else
        {
            D_AltAnswers.Controls.Clear();
            if (actionType == UserAction.Add)
            {
                for (int i = 1; i < 7; i++)
                {
                    TextBox TB_O = new TextBox();
                    TB_O.Width = Unit.Pixel(100);
                    TB_O.Enabled = false;
                    TB_O.TextMode = TextBoxMode.SingleLine;
                    TB_O.Rows = 1;
                    TB_O.ID = "ATB_O" + Convert.ToString(i);
                    TB_O.Text = i.ToString();

                    TextBox TB_i = new TextBox();
                    TB_i.Width = Unit.Pixel(400);
                    TB_i.TextMode = TextBoxMode.MultiLine;
                    TB_i.Rows = 3;
                    TB_i.ID = "ATB_" + Convert.ToString(i);
                    TB_i.Text = string.Empty;

                    D_AltAnswers.Controls.Add(TB_O);
                    D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                    D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                    D_AltAnswers.Controls.Add(TB_i);
                    D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                    D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                    D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                    if (i > 1 && i < 4)
                    {
                        D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                    }
                }
            }
            else if (actionType == UserAction.Edit || actionType == UserAction.Copy)
            {
                int i = 0;
                D_AltAnswers.Controls.Clear();
                foreach (var item in answers)
                {
                    i++;
                    TextBox TB_O = new TextBox();
                    TB_O.Width = Unit.Pixel(100);
                    TB_O.Enabled = false;
                    TB_O.TextMode = TextBoxMode.SingleLine;
                    TB_O.Rows = 1;
                    TB_O.ID = "ATB_O" + Convert.ToString(i);
                    TB_O.Text = item.InitialPosition != 0 ? item.InitialPosition.ToString() : string.Empty;

                    TextBox TB_i = new TextBox();
                    TB_i.Width = Unit.Pixel(400);
                    TB_i.TextMode = TextBoxMode.MultiLine;
                    TB_i.Rows = 3;
                    TB_i.ID = "ATB_" + Convert.ToString(i);
                    var AlternateAText = item.AlternateAText.ToString();
                    TB_i.Text = AlternateAText;
                    D_AltAnswers.Controls.Add(TB_O);
                    D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                    D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                    D_AltAnswers.Controls.Add(TB_i);
                    D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                    D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                    D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                    if (i > 1 && i < 4)
                    {
                        D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                    }
                }

                if (i < 7)
                {
                    for (int j = i + 1; j < 7; j++)
                    {
                        TextBox TB_O = new TextBox();
                        TB_O.Width = Unit.Pixel(100);
                        TB_O.Enabled = false;
                        TB_O.TextMode = TextBoxMode.SingleLine;
                        TB_O.Rows = 1;
                        TB_O.ID = "ATB_O" + Convert.ToString(j);
                        TB_O.Text = j.ToString();

                        TextBox TB_i = new TextBox();
                        TB_i.Width = Unit.Pixel(400);
                        TB_i.TextMode = TextBoxMode.MultiLine;
                        TB_i.Rows = 3;
                        TB_i.ID = "ATB_" + Convert.ToString(j);
                        TB_i.Text = string.Empty;
                        D_AltAnswers.Controls.Add(TB_O);
                        D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                        D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                        D_AltAnswers.Controls.Add(TB_i);
                        D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                        D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                        D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                        if (i > 1 && i < 4)
                        {
                            D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                        }
                    }
                }
            }
        }
    }

    private void FillMultipleChoiceMultiSelectFields(UserAction actionType, List<AnswerChoice> answers)
    {
        if (actionType == UserAction.View)
        {
            int i = 0;
            D_AltAnswers.Controls.Clear();
            foreach (var item in answers)
            {
                i++;
                Label CH_i = new Label();
                CH_i.ID = "ACH_" + Convert.ToString(i);
                CH_i.Enabled = false;

                var altAText = item.AlternateAText.ToString();
                CH_i.Text = i.ToString() + ".&nbsp;" + altAText;

                D_AltAnswers.Controls.Add(CH_i);
                D_AltAnswers.Controls.Add(new LiteralControl("<br />"));

                if (item.Correct == 1)
                {
                    CH_i.BackColor = Color.FromArgb(228, 240, 216);
                }
            }
        }
        else
        {
            D_AltAnswers.Controls.Clear();
            if (actionType == UserAction.Add)
            {
                D_AltAnswers.Controls.Clear();
                for (int i = 1; i < 7; i++)
                {
                    TextBox TB_i = new TextBox();
                    TB_i.Width = Unit.Pixel(400);
                    TB_i.TextMode = TextBoxMode.MultiLine;
                    TB_i.Rows = 3;
                    TB_i.ID = "ATB_" + Convert.ToString(i);
                    TB_i.Text = string.Empty;

                    D_AltAnswers.Controls.Add(new LiteralControl("<br />"));

                    D_AltAnswers.Controls.Add(TB_i);
                    D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                    if (i > 1 && i < 4)
                    {
                        D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                    }
                }
            }
            else if (actionType == UserAction.Edit || actionType == UserAction.Copy)
            {
                var count = answers.Count;
                int i = 0;
                D_AltAnswers.Controls.Clear();
                foreach (var item in answers)
                {
                    i++;
                    TextBox TB_i = new TextBox();
                    TB_i.ID = "ATB_" + Convert.ToString(i);
                    TB_i.Width = Unit.Pixel(400);
                    TB_i.TextMode = TextBoxMode.MultiLine;
                    TB_i.Rows = 3;

                    var AlternateAText = item.AlternateAText.ToString();
                    TB_i.Text = AlternateAText;

                    D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                    D_AltAnswers.Controls.Add(TB_i);
                    D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                    if (i > 1 && i < 4)
                    {
                        D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                    }
                }

                if (i < 7)
                {
                    for (int j = i + 1; j < 7; j++)
                    {
                        TextBox TB_i = new TextBox();
                        TB_i.Width = Unit.Pixel(400);
                        TB_i.TextMode = TextBoxMode.MultiLine;
                        TB_i.Rows = 3;
                        TB_i.ID = "ATB_" + Convert.ToString(j);
                        TB_i.Text = string.Empty;
                        D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                        D_AltAnswers.Controls.Add(TB_i);
                        D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                        if (i > 1 && i < 4)
                        {
                            D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
                        }
                    }
                }
            }
        }
    }

    private void FillHotSpotFields(UserAction actionType, List<AnswerChoice> answers)
    {
        D_AltAnswers.Controls.Clear();
        if (actionType == UserAction.Add)
        {
            TextBox tx = new TextBox();
            tx.ID = "Atx";
            tx.Text = string.Empty;
            D_AltAnswers.Controls.Add(tx);
        }

        if (actionType == UserAction.Edit || actionType == UserAction.Copy)
        {
            var count = answers.Count;
            if (count > 0)
            {
                var item = answers.FirstOrDefault();
                var AlternateAText = item.AlternateAText.ToString();
                AlternateAText = AlternateAText.Replace("<P>", string.Empty);
                AlternateAText = AlternateAText.Replace("</P>", string.Empty);
                TextBox tx = new TextBox();
                tx.Width = Unit.Pixel(400);
                tx.TextMode = TextBoxMode.MultiLine;
                tx.Rows = 4;

                if (!AlternateAText.Trim().Equals(string.Empty))
                {
                    tx.Text = AlternateAText.Trim();
                }

                tx.ID = "Atx";
                D_AltAnswers.Controls.Add(tx);
                D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
            }
            else
            {
                TextBox tx = new TextBox();
                tx.ID = "Atx";
                tx.Width = Unit.Pixel(400);
                tx.Text = string.Empty;
                D_AltAnswers.Controls.Add(tx);
                D_AltAnswers.Controls.Add(new LiteralControl("<br />"));
            }
        }
    }

    private string ReturnLetter(int val)
    {
        var _val = string.Empty;
        switch (val)
        {
            case 1:
                _val = "A";
                break;
            case 2:
                _val = "B";
                break;
            case 3:
                _val = "C";
                break;
            case 4:
                _val = "D";
                break;
            case 5:
                _val = "E";
                break;
            case 6:
                _val = "F";
                break;
            default:
                _val = string.Empty;
                break;
        }

        return _val;
    }
}
