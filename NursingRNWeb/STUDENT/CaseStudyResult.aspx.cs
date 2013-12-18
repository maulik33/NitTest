using System;
using System.IO;
using System.Xml;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

namespace STUDENT
{
    public partial class CaseStudyResult : StudentBasePage<IStudentCaseStudyResultView, StudentCaseStudyResultPresenter>, IStudentCaseStudyResultView
    {
        #region Properties

        public CaseModuleScore CaseModuleScoreDetails { get; set; }

        #endregion Properties

        public void ReadXml()
        {
            Page.Response.ContentType = "text/xml";
            var reader = new StreamReader(Page.Request.InputStream);
            String xmlData = reader.ReadToEnd();
            string strResult = uploadscore(xmlData);
            string[] result = strResult.Split(',');

            var responseDoc = new XmlDocument();
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
                payloadElement.InnerText = "Record was exist";
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
                XmlElement errorDescriptionElement = responseDoc.CreateElement("desciption");
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

            Response.Write(responseDoc.OuterXml);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region Trace Information
                TraceHelper.WriteTraceEvent(TraceToken, "Navigated to Case Study Result Page.");
                #endregion
                Presenter.OnViewInitialized();
            }

            Presenter.OnViewLoaded();
        }

        private string uploadscore(string xmldata)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmldata);

            //// Collect target nodes from the XML document
            XmlElement root = xmlDoc.DocumentElement;
            XmlNodeList results = root.SelectNodes("/results");

            int lastModuleStudentId = CaseModuleScoreDetails != null ? CaseModuleScoreDetails.ModuleStudentId : 0;

            //// get studentid, moduleid, caseid correct and total number for module
            int cid;
            int mid;
            int correctModule;
            int totalModule;

            if (results[0].ChildNodes[0].Name != "student")
            {
                return "0,KP0006,No studentID information";
            }

            string sid = results[0].ChildNodes[0].Attributes["id"].Value;
            if (results[0].ChildNodes[1].Name != "case")
            {
                return "0,KP0007,No case ID information";
            }
            
            string caseid = results[0].ChildNodes[1].Attributes["id"].Value;
            if (!Int32.TryParse(caseid, out cid))
            {
                return "0,KP0002,Case ID is empty or not a number";
            }
            
            if (results[0].ChildNodes[1].ChildNodes[0].Name != "module")
            {
                return "0,KP0008,No module ID information ";
            }

            string moduleid = results[0].ChildNodes[1].ChildNodes[0].Attributes["id"].Value;
            if (Int32.TryParse(moduleid, out mid))
            {
                if (Presenter.CheckExistCaseModuleStudent(cid, mid, sid))
                {
                    return "2,KP0016,the student has take the case and module";
                }
            }
            else
            {
                return "0,KP0003,Module ID is empty or not a number";
            }

            if (results[0].ChildNodes[1].ChildNodes[0].ChildNodes[0].Name != "correct")
            {
                return "0,KP0009,No coreect number for module ";
            }

            string correct = results[0].ChildNodes[1].ChildNodes[0].ChildNodes[0].InnerText;
            if (!int.TryParse(correct, out correctModule))
            {
                return "0,KP0004,Correct number for the module is empty or not a number";
            }

            if (results[0].ChildNodes[1].ChildNodes[0].ChildNodes[1].Name != "total")
            {
                return "0,KP0010,No total number for module";
            }
            
            var total = results[0].ChildNodes[1].ChildNodes[0].ChildNodes[1].InnerText;
            if (!int.TryParse(total, out totalModule))
            {
                return "0,KP0005,Total number for the module ID is empty or not a number";
            }

            // Add new CaseModuleScore from XML
            var newCaseModuleScore = new CaseModuleScore
                                         {
                                             CaseId = cid,
                                             StudentId = sid,
                                             Correct = correctModule,
                                             Total = totalModule
                                         };

            if (mid % 5 == 0)
            {
                newCaseModuleScore.ModuleId = 5;
            }
            else
            {
                newCaseModuleScore.ModuleId = mid % 5;
            }

            Presenter.NewCaseModuleScore(newCaseModuleScore);

            // Get subcategory data
            CaseSubCategory caseSubCategoryDetails = Presenter.GetSubCategoryDetails();
            if (caseSubCategoryDetails != null)
            {
                foreach (XmlNode cNode in results[0].ChildNodes[1].ChildNodes[0])
                {
                    if (cNode.Name == "category")
                    {
                        foreach (XmlNode scNote in cNode)
                        {
                            var newCaseSubCategory = new CaseSubCategory { CategoryName = cNode.Attributes["name"].Value };

                            if (cNode.Attributes["name"].Value == "Critical Thinking")
                            {
                                newCaseSubCategory.CategoryId = 3;
                            }

                            if (cNode.Attributes["name"].Value == "Nursing Process")
                            {
                                newCaseSubCategory.CategoryId = 2;
                            }

                            newCaseSubCategory.ModuleStudentId = lastModuleStudentId;
                            string subCategoryID = scNote.Attributes["id"].Value;
                            int scid;
                            if (Int32.TryParse(subCategoryID, out scid))
                            {
                                newCaseSubCategory.SubCategoryId = scid;
                                if (scNote.ChildNodes[0].Name == "correct")
                                {
                                    string sCorrect = scNote.ChildNodes[0].InnerText;
                                    int note;
                                    if (Int32.TryParse(sCorrect, out note))
                                    {
                                        newCaseSubCategory.Correct = note;
                                    }
                                    else
                                    {
                                        return "0,KP0012,Subcategory correct number is empty or not a number";
                                    }
                                }
                                else
                                {
                                    return "0,KP0013,Subcategory correct number is empty";
                                }

                                if (scNote.ChildNodes[1].Name == "total")
                                {
                                    string sTotal = scNote.ChildNodes[1].InnerText;
                                    int note;
                                    if (Int32.TryParse(sTotal, out note))
                                    {
                                        newCaseSubCategory.Total = note;
                                    }
                                    else
                                    {
                                        return "0,KP0014,Subcategory total number is empty or not a number";
                                    }
                                }
                                else
                                {
                                    return "0,KP0015,Subcategory total number is empty";
                                }
                            }
                            else
                            {
                                return "0,KP0011,Subcategory ID is empty or not a number";
                            }

                            Presenter.NewCaseSubCategory(newCaseSubCategory);
                        }
                    }
                }
            }

            return "1,Success";
        }
    }
}
