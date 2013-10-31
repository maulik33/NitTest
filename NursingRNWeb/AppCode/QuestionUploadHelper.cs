using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using NursingLibrary.Entity;

namespace NursingRNWeb
{
    public enum QuestionToWordMapping
    {
        Feedback,
        Statisctics,
        QuestionId,
        Explanation,
        ClientNeedsId,
        ClientNeedsCategoryId,
        NursingProcessId,
        DemographicId,
        Stimulus,
        Stem,
        AlternateStem,
        TopicTitleId,
        ProductLineId,
        CreatorId,
        DateCreated,
        EditorId,
        DateEdited,
        Source_SBD,
        EditorId_2,
        DateEdited_2,
        CognitiveLevelId,
        TypeOfFileId,
        WhoOwns,
        WhereUsed,
        LevelOfDifficultyId,
        SystemId,
        SpecialtyAreaId,
        IntegratedConceptsId,
        ClinicalConceptsId,
        PointBiserialsId,
        CriticalThinkingId,
        AccreditationCategoriesId,
        QSENKSACompetenciesId,
        QuestionType,
        CorrectAnswer,
        Unit,
        ProgramofStudyName,
    }

    public enum AnswerToWordMapping
    {
        Atext,
        AlternateAText,
        Unit,
    }

    public class QuestionUploadHelper
    {
        public static Question GetQuestionValue(Question question, WordprocessingDocument wordProcDoc)
        {
            Array properties = Enum.GetValues(typeof(QuestionToWordMapping));
            foreach (QuestionToWordMapping val in properties)
            {
                string EntityPropName = "Id" + val.ToString();
                string Value = GetContentControlValue(wordProcDoc, EntityPropName);
                FillQuestionPropertyValue(val.ToString(), question, Value);
            }

            return question;
        }

        public static List<AnswerChoice> GetSingleSelectAnswerChoices(WordprocessingDocument wordProcDoc)
        {
            List<AnswerChoice> answerChoice = new List<AnswerChoice>();
            Array answerProperties = Enum.GetValues(typeof(AnswerToWordMapping));
            for (int i = 1; i <= 6; i++)
            {
                AnswerChoice answer = new AnswerChoice();
                foreach (AnswerToWordMapping val in answerProperties)
                {
                    string EntityPropName = "Id" + val.ToString() + i;

                    string Value = GetContentControlValue(wordProcDoc, EntityPropName);
                    if (Value != null)
                    {
                        FillAnswerPropertyValue(val.ToString(), answer, Value);
                    }
                }

                if (!string.IsNullOrEmpty(answer.Atext.Trim()) || !string.IsNullOrEmpty(answer.AlternateAText.Trim()) || !string.IsNullOrEmpty(answer.Unit.Trim()))
                {
                    string aindex = ReturnLetter(i);
                    if (string.IsNullOrWhiteSpace(answer.Unit.Trim()))
                    {
                        answer.Unit = string.Empty;
                    }

                    answer.ActionType = 1;
                    FillAnswerPropertyValue("Aindex", answer, aindex);
                    answerChoice.Add(answer);
                }
            }

            return answerChoice;
        }

        public static string GetContentControlValue(WordprocessingDocument theDoc, string tag)
        {
            StringBuilder sb = new StringBuilder();
            IEnumerable<OpenXmlElement> elements = theDoc.MainDocumentPart.Document.Elements().FirstOrDefault();
            foreach (OpenXmlElement oxe in elements)
            {
                if (oxe.LocalName == "tbl")
                {
                    foreach (DocumentFormat.OpenXml.Wordprocessing.TableRow oxerow in oxe.Descendants<DocumentFormat.OpenXml.Wordprocessing.TableRow>())
                    {
                        SdtCell sdtCell = oxerow.Descendants<SdtCell>().Where(r => r.SdtProperties.GetFirstChild<Tag>() == null ? false : r.SdtProperties.GetFirstChild<Tag>().Val == tag).FirstOrDefault<SdtCell>();
                        #region MyRegion

                        if (sdtCell != null)
                        {
                            SdtContentCell contCell = sdtCell.Descendants<SdtContentCell>().FirstOrDefault<SdtContentCell>();

                            ////SdtContentComboBox sdtComboBox = sdtCell.Descendants<SdtContentComboBox>().FirstOrDefault<SdtContentComboBox>();
                            ////List<DocumentFormat.OpenXml.Wordprocessing.ListItem> listItem;

                            if (contCell != null)
                            {
                                DocumentFormat.OpenXml.Wordprocessing.TableCell tableCell = contCell.Descendants<DocumentFormat.OpenXml.Wordprocessing.TableCell>().FirstOrDefault<DocumentFormat.OpenXml.Wordprocessing.TableCell>();
                                if (tableCell != null)
                                {
                                    IEnumerable<Paragraph> paragraphs = tableCell.Descendants<Paragraph>();
                                    foreach (Paragraph p in paragraphs)
                                    {
                                        if (!string.IsNullOrEmpty(p.InnerText))
                                        {
                                            ////if (sdtComboBox != null)
                                            ////{
                                            ////    listItem = sdtComboBox.Descendants<DocumentFormat.OpenXml.Wordprocessing.ListItem>().ToList();
                                            ////    if (listItem != null)
                                            ////    {
                                            ////        if (listItem.Where(c => c.DisplayText == p.InnerText.Trim()).Count() >= 1)
                                            ////        {
                                            ////            var cmbId = listItem.Where(c => c.DisplayText == p.InnerText.Trim()).FirstOrDefault().Value;
                                            ////            sb.Append(cmbId);
                                            ////            break;
                                            ////        }
                                            ////        else
                                            ////        {s
                                            ////            sb.Append("-1");
                                            ////            break;
                                            ////        }
                                            ////    }
                                            ////}

                                            if (paragraphs.Count() > 1)
                                            {
                                                sb.Append("<p>");
                                                sb.Append(GetFormatedText(p));
                                                sb.Append("</p>");
                                            }
                                            else
                                            {
                                                sb.Append(p.InnerText);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        #endregion
                    }
                }
            }

            return sb.ToString();
        }

        private static string GetFormatedText(Paragraph p)
        {
            StringBuilder value = new StringBuilder();
            string s = string.Empty;
            IEnumerable<Run> runs = p.Descendants<Run>();
            foreach (Run r in runs)
            {
                if (r.RunProperties != null)
                {
                    value.Append(AppendSpecialTags(r));
                }
                else
                {
                    value.Append(r.InnerText);
                }
            }

            return value.ToString();
        }

        private static void FillQuestionPropertyValue(string name, Question obj, string value)
        {
            Type type = obj.GetType();
            PropertyInfo pi = type.GetProperty(name);
            if (pi.PropertyType.Name == "String")
            {
                pi.SetValue(obj, value.Trim(), null);
            }
            else if (pi.PropertyType.Name == "DateTime")
            {
                pi.SetValue(obj, Convert.ToDateTime(value), null);
            }
            else if (pi.PropertyType.Name == "Boolean")
            {
                pi.SetValue(obj, Convert.ToBoolean(value), null);
            }
            else if (pi.PropertyType.Name == "Double")
            {
                pi.SetValue(obj, Convert.ToDouble(value), null);
            }
            else if (pi.PropertyType.Name == "Int32")
            {
                try
                {
                    pi.SetValue(obj, Convert.ToInt32(value), null);
                }
                catch
                {
                    if (value == null)
                    {
                        pi.SetValue(obj, Convert.ToInt32(0), null);
                    }
                    else
                    {
                        pi.SetValue(obj, Convert.ToInt32(-1), null);
                    }
                }
            }
        }

        private static void FillAnswerPropertyValue(string name, AnswerChoice obj, string value)
        {
            Type type = obj.GetType();
            PropertyInfo pi = type.GetProperty(name);
            if (pi.PropertyType.Name == "String")
            {
                pi.SetValue(obj, value, null);
            }
            else if (pi.PropertyType.Name == "DateTime")
            {
                pi.SetValue(obj, Convert.ToDateTime(value), null);
            }
            else if (pi.PropertyType.Name == "Boolean")
            {
                pi.SetValue(obj, Convert.ToBoolean(value), null);
            }
            else if (pi.PropertyType.Name == "Double")
            {
                pi.SetValue(obj, Convert.ToDouble(value), null);
            }
        }

        private static string AppendSpecialTags(Run r)
        {
            string text = r.InnerText;
            if (!string.IsNullOrEmpty(text.Trim()))
            {
                Bold bold = r.RunProperties.GetFirstChild<Bold>();
                Italic italic = r.RunProperties.GetFirstChild<Italic>();
                VerticalTextAlignment v = r.RunProperties.VerticalTextAlignment;

                if (bold != null)
                {
                    text = "<b>" + text + "</b>";
                }

                if (italic != null)
                {
                    text = "<i>" + text + "</i>";
                }

                if (r.RunProperties.VerticalTextAlignment != null)
                {
                    if (r.RunProperties.VerticalTextAlignment.Val == "superscript")
                    {
                        text = "<sup>" + text + "</sup>";
                    }
                    else if (r.RunProperties.VerticalTextAlignment.Val == "subscript")
                    {
                        text = "<sub>" + text + "</sub>";
                    }
                }
            }

            return text;
        }

        private static string ReturnLetter(int val)
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
}