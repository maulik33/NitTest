using System;
using System.Collections.Generic;
using System.Xml;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters.Controller;

namespace NursingLibrary.Presenters
{
    public class CaseStudyResultPresenter : StudentPresenter<ICaseStudyResultView>
    {
        public CaseStudyResultPresenter(IStudentAppController appController)
            : base(appController)
        {
        }

        public void ShowCaseStudyResult(string xmlData)
        {
            string strResult = UploadScore(xmlData);
            string[] result = strResult.Split(',');

            XmlDocument responseDoc = new XmlDocument();
            XmlElement responsesElem = responseDoc.CreateElement("response");
            responseDoc.AppendChild(responsesElem);
            XmlElement payloadElement = responseDoc.CreateElement("payload");
            if (result[0] == "1")
            {
                payloadElement.InnerText = "Success";
                responsesElem.AppendChild(payloadElement);
            }
            else if (result[0] == "2")
            {
                payloadElement.InnerText = "Posted";
                responsesElem.AppendChild(payloadElement);
            }
            else
            {
                payloadElement.InnerText = "Failed";
                responsesElem.AppendChild(payloadElement);
                XmlElement errorElement = responseDoc.CreateElement("error");
                XmlElement errorCodeElement = responseDoc.CreateElement("code");
                errorCodeElement.InnerText = result[1];
                errorElement.AppendChild(errorCodeElement);
                XmlElement errorDescriptionElement = responseDoc.CreateElement("description");
                errorDescriptionElement.InnerText = result[2];
                errorElement.AppendChild(errorDescriptionElement);
                if (result.Length >= 4)
                {
                    XmlElement errorStackTraceElement = responseDoc.CreateElement("stackTrace");
                    errorStackTraceElement.InnerText = result[3];
                    errorElement.AppendChild(errorStackTraceElement);
                }

                responsesElem.AppendChild(errorElement);
            }

            View.WriteResponse(responseDoc);
        }

        private string UploadScore(string xmlData)
        {
            CaseModuleScore caseModuleScore = new CaseModuleScore();
            List<CaseSubCategory> subCategories = new List<CaseSubCategory>();
            int lastModuleStudentId;
            string errorMessage = string.Empty;
            string subCategoryErrorMsg = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData);
            XmlNodeList results;
            XmlElement root = xmlDoc.DocumentElement;
            results = root.SelectNodes("/results");

            if (ValidateResultNode(results, ref errorMessage))
            {
                caseModuleScore.StudentId = results[0].ChildNodes[0].Attributes["id"].Value;
                caseModuleScore.CaseId = results[0].ChildNodes[1].Attributes["id"].Value.ToInt();
                caseModuleScore.ModuleId = results[0].ChildNodes[1].ChildNodes[0].Attributes["id"].Value.ToInt();
                caseModuleScore.Correct = results[0].ChildNodes[1].ChildNodes[0].ChildNodes[0].InnerText.ToInt();
                caseModuleScore.Total = results[0].ChildNodes[1].ChildNodes[0].ChildNodes[1].InnerText.ToInt();
                AppController.InsertModuleScore(caseModuleScore);
                lastModuleStudentId = caseModuleScore.ModuleStudentId;
            }
            else
            {
                return errorMessage;
            }

            foreach (XmlNode cNode in results[0].ChildNodes[1].ChildNodes[0])
            {
                if (cNode.Name == "category")
                {
                    foreach (XmlNode scNote in cNode)
                    {
                        if (ValidateSubCategory(scNote, ref subCategoryErrorMsg))
                        {
                            CaseSubCategory subCategory = new CaseSubCategory();
                            subCategory.CategoryName = cNode.Attributes["name"].Value.ToString();
                            if (cNode.Attributes["name"].Value.ToString() == "Critical Thinking")
                            {
                                subCategory.CategoryId = 3;
                            }

                            if (cNode.Attributes["name"].Value.ToString() == "Nursing Process")
                            {
                                subCategory.CategoryId = 2;
                            }

                            subCategory.ModuleStudentId = lastModuleStudentId;
                            subCategory.SubCategoryId = scNote.Attributes["id"].Value.ToInt();
                            subCategory.Correct = scNote.ChildNodes[0].InnerText.ToInt();
                            subCategory.Total = scNote.ChildNodes[1].InnerText.ToInt();
                        }
                        else
                        {
                            return subCategoryErrorMsg;
                        }
                    }
                }
            }

            foreach (CaseSubCategory csb in subCategories)
            {
                AppController.InsertSubCategory(csb);
            }

            return "1,Success";
        }

        private bool ValidateResultNode(XmlNodeList results, ref string errorMessage)
        {
            bool IsNodeValid = true;
            int cid, mid, correctModule, totalModule;
            string sid = results[0].ChildNodes[0].Attributes["id"].Value;
            if (results[0].ChildNodes[0].Name != "student")
            {
                errorMessage = "0,KP0006,No studentID information";
                IsNodeValid = false;
            }
            else if (results[0].ChildNodes[1].Name != "case")
            {
                errorMessage = "0,KP0007,No case ID information";
                IsNodeValid = false;
            }
            else if (!Int32.TryParse(results[0].ChildNodes[1].Attributes["id"].Value, out cid))
            {
                errorMessage = "0,KP0002,Case ID is empty or not a number";
                IsNodeValid = false;
            }
            else if (results[0].ChildNodes[1].ChildNodes[0].Name != "module")
            {
                errorMessage = "0,KP0008,No module ID information ";
                IsNodeValid = false;
            }
            else if (!Int32.TryParse(results[0].ChildNodes[1].ChildNodes[0].Attributes["id"].Value, out mid))
            {
                errorMessage = "0,KP0003,Module ID is empty or not a number";
                IsNodeValid = false;
            }
            else if (CheckExistCaseModuleStudent(cid, mid, sid))
            {
                errorMessage = "2,KP0016,the student has take the case and module";
                IsNodeValid = false;
            }
            else if (results[0].ChildNodes[1].ChildNodes[0].ChildNodes[0].Name != "correct")
            {
                errorMessage = "0,KP0009,No correct number for module ";
                IsNodeValid = false;
            }
            else if (!int.TryParse(results[0].ChildNodes[1].ChildNodes[0].ChildNodes[0].InnerText, out correctModule))
            {
                errorMessage = "0,KP0004,Correct number for the module is empty or not a number";
                IsNodeValid = false;
            }
            else if (results[0].ChildNodes[1].ChildNodes[0].ChildNodes[1].Name != "total")
            {
                errorMessage = "0,KP0010,No total number for module";
                IsNodeValid = false;
            }
            else if (!int.TryParse(results[0].ChildNodes[1].ChildNodes[0].ChildNodes[1].InnerText, out totalModule))
            {
                errorMessage = "0,KP0005,Total number for the module ID is empty or not a number";
                IsNodeValid = false;
            }

            return IsNodeValid;
        }

        private bool ValidateSubCategory(XmlNode scNote, ref string errorMessage)
        {
            bool IsValidSubCategory = true;
            int scid, correct, total;
            if (!Int32.TryParse(scNote.Attributes["id"].Value, out scid))
            {
                errorMessage = "0,KP0011,Subcategory ID is empty or not a number";
                IsValidSubCategory = false;
            }
            else if (scNote.ChildNodes[0].Name != "correct")
            {
                errorMessage = "0,KP0013,Subcategory correct number is empty";
                IsValidSubCategory = false;
            }
            else if (!Int32.TryParse(scNote.ChildNodes[0].InnerText, out correct))
            {
                errorMessage = "0,KP0012,Subcategory correct number is empty or not a number";
                IsValidSubCategory = false;
            }
            else if (scNote.ChildNodes[1].Name != "total")
            {
                errorMessage = "0,KP0015,Subcategory total number is empty";
                IsValidSubCategory = false;
            }
            else if (!Int32.TryParse(scNote.ChildNodes[1].InnerText, out total))
            {
                errorMessage = "0,KP0014,Subcategory total number is empty or not a number";
                IsValidSubCategory = false;
            }

            return IsValidSubCategory;
        }

        private bool CheckExistCaseModuleStudent(int cid, int mid, string sid)
        {
            return AppController.CheckExistCaseModuleStudent(cid, mid, sid);
        }
    }
}
