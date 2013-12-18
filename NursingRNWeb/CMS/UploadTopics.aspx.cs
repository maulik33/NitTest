using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

namespace NursingRNWeb.CMS
{
    public partial class UploadTopics : PageBase<IRemediationView, RemediationPresenter>, IRemediationView
    {
        private Dictionary<int, string> excelHeader = new Dictionary<int, string>() { { 1, "Title" }, { 2, "Description/Content" } };
        private Dictionary<string, int> mapHeadCellMap = new Dictionary<string, int> { { "A", 1 }, { "B", 2 } };
        private string workSheetName = "Topic Template";
        private string[] fileExtensions = { ".XLSX" };

        #region Interface Methods
        public string DisplayMessage
        {
            set { throw new NotImplementedException(); }
        }

        public void PopulateControls(Remediation remediation)
        {
            throw new NotImplementedException();
        }

        public void PopulateQuestions(IEnumerable<Question> questions)
        {
            throw new NotImplementedException();
        }

        public void PopulateLippincotts()
        {
            throw new NotImplementedException();
        }

        public void DisplayUploadedTopics(IEnumerable<Remediation> validRemediations, IEnumerable<Remediation> invalidRemediations, IEnumerable<Remediation> dupliacateTopics, string filePath)
        {
            gvInValidTopics.DataSource = invalidRemediations;
            gvValidTopicss.DataSource = validRemediations;
            gvInValidTopics.DataBind();
            gvValidTopicss.DataBind();
            gvDuplicateTopics.DataSource = dupliacateTopics;
            gvDuplicateTopics.DataBind();

            if (invalidRemediations.Count() > 0)
            {
                divError.Visible = true;
            }

            if (validRemediations.Count() > 0)
            {
                divSuccess.Visible = true;
                if (dupliacateTopics.Count() > 0 && invalidRemediations.Count() == 0)
                {
                    divDuplicateTopics.Visible = true;
                }
            }

            if (invalidRemediations.Count() == 0 && validRemediations.Count() > 0)
            {
                btnSave.Visible = true;
                hfTopic.Value = filePath;
            }

            if (invalidRemediations.Count() == 0 && validRemediations.Count() == 0)
            {
                lblMsg.Text = "Blank topic template uploaded.";
            }
        }

        #endregion

        public override void PreInitialize()
        {
            ////throw new NotImplementedException();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            if (Global.IsProductionApp)
            {
                btnUpload.Visible = false;
                lbDownloadTopicTemplate.Visible = false;
                btnSave.Visible = false;
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            ClearGridvalues();
            btnCMS.Visible = false;
            if (IsUploadedDocumentValid())
            {
                string uniquekey = string.Empty;
                string fileType = string.Empty;
                string topicTitleSavePath = string.Empty;
                HttpFileCollection fileCollection = Request.Files;
                HttpPostedFile httpPostfile = fileCollection[0];
                uniquekey = GetUniqueKey();
                fileType = FileHelper.GetFileExtension(httpPostfile.FileName);
                topicTitleSavePath = KTPApp.UploadedTopicTemplateSavePath + uniquekey + FileHelper.GetFileExtension(httpPostfile.FileName);
                SaveFile(false, httpPostfile, topicTitleSavePath);
                UploadSaveTopics(topicTitleSavePath, false);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hfTopic.Value))
            {
                if (UploadSaveTopics(hfTopic.Value, true))
                {
                    lblSuccessMsg.Text = "Topics uploaded successfully";
                    btnCMS.Visible = true;
                }
            }
        }

        protected void lbDownloadTopicTemplate_Click(object sender, EventArgs e)
        {
            string fullPath = KTPApp.UploadTopicTemplatePath;
            FileInfo fileInfo = new FileInfo(fullPath);
            if (CheckFileExistence(fullPath))
            {
                using (ImpersonateUserBase adminUser = GetUserImpersonation())
                {
                    Response.Clear();
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Disposition", "attachment; filename= TopicTemplate.xlsx");

                    using (Stream stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        long dataToRead = stream.Length;
                        byte[] buffer = new byte[1024];
                        int length;
                        while (dataToRead > 0)
                        {
                            if (Response.IsClientConnected)
                            {
                                length = stream.Read(buffer, 0, buffer.Length);
                                Response.OutputStream.Write(buffer, 0, length);
                                Response.Flush();
                                dataToRead -= length;
                                buffer = new byte[1024];
                            }
                            else
                            {
                                dataToRead = -1;
                            }
                        }
                    }

                    Response.End();
                }
            }
        }

        protected void btnCMS_Click(object sender, EventArgs e)
        {
            Presenter.NavigateToSearch(string.Empty);
        }

        private bool UploadSaveTopics(string filePath, bool IsSave)
        {
            bool isValid = true;
            using (ImpersonateUserBase adminUser = GetUserImpersonation())
            {
                using (SpreadsheetDocument spreadsheetCocument = SpreadsheetDocument.Open(filePath, true))
                {
                    WorkbookPart wbPart = spreadsheetCocument.WorkbookPart;
                    Sheet sheetName = wbPart.Workbook.Descendants<Sheet>().Where(s => s.Name == workSheetName).FirstOrDefault();
                    if (sheetName != null)
                    {
                        WorksheetPart worksheetPart = (WorksheetPart)wbPart.GetPartById(sheetName.Id);

                        if (worksheetPart != null)
                        {
                            SharedStringTablePart stringTablePart = spreadsheetCocument.WorkbookPart.SharedStringTablePart;
                            Row lastRow = worksheetPart.Worksheet.Descendants<Row>().LastOrDefault();
                            Row firstRow = worksheetPart.Worksheet.Descendants<Row>().FirstOrDefault();
                            if (ValidateTopicTemplate(firstRow, stringTablePart))
                            {
                                MapHeaderColumnIndexToName(wbPart, worksheetPart);
                                List<Remediation> remediations = FillRemediations(lastRow, stringTablePart, worksheetPart);
                                if (!IsSave)
                                {
                                    Presenter.DisplayUploadedTopics(remediations, filePath);
                                }
                                else if (IsSave)
                                {
                                    Presenter.SaveUploadedTopics(remediations);
                                    hfTopic.Value = string.Empty;
                                }
                            }
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Please upload valid template.";
                        isValid = false;
                    }
                }
            }

            return isValid;
        }

        private void MapHeaderColumnIndexToName(WorkbookPart wbPart, WorksheetPart wsPart)
        {
            foreach (KeyValuePair<int, String> entry in excelHeader)
            {
                SharedStringItem sharedStringItem = wbPart.SharedStringTablePart.SharedStringTable.Descendants<SharedStringItem>().Where(
                   stringItem => stringItem.FirstChild.InnerText.Equals(entry.Value)).SingleOrDefault();
                if (sharedStringItem != null)
                {
                    var sharedStringItems = wbPart.SharedStringTablePart.SharedStringTable.Descendants<SharedStringItem>();
                    var sharedStringItemsList = sharedStringItems.ToList();
                    int sharedStringIndex = sharedStringItemsList.IndexOf(sharedStringItem);

                    Cell headerCell = wsPart.Worksheet.Descendants<Row>().First().Descendants<Cell>().Where(cell =>
                       cell.DataType == CellValues.SharedString &&
                       ((CellValue)cell.FirstChild).Text == sharedStringIndex.ToString()).SingleOrDefault();

                    if (headerCell != null)
                    {
                        string headerColumnLetter = Regex.Replace(headerCell.CellReference, @"\d", string.Empty);
                    }
                }
            }
        }

        private List<Remediation> FillRemediations(Row lastRow, SharedStringTablePart stringTablePart, WorksheetPart worksheetPart)
        {
            List<Remediation> remediations = new List<Remediation>();
            int colPosition = 0;
            for (int i = 2; i <= lastRow.RowIndex; i++)
            {
                Remediation remediation = new Remediation();
                remediation.TopicTitle = string.Empty;
                remediation.Explanation = string.Empty;
                Row row = worksheetPart.Worksheet.Descendants<Row>().Where(r => i == r.RowIndex).FirstOrDefault();
                if (row != null)
                {
                    foreach (Cell c in row.ChildElements)
                    {
                        colPosition = 0;
                        string value = GetValue(c, stringTablePart, ref colPosition);
                        if (colPosition == 1)
                        {
                            remediation.TopicTitle = value.Trim();
                        }
                        else if (colPosition == 2)
                        {
                            remediation.Explanation = value.Trim();
                        }
                    }

                    if (!string.IsNullOrEmpty(remediation.Explanation.Trim()) || !string.IsNullOrEmpty(remediation.TopicTitle.Trim()))
                    {
                        remediations.Add(remediation);
                    }
                }
            }

            return remediations;
        }

        private bool ValidateTopicTemplate(Row firstRow, SharedStringTablePart stringTablePart)
        {
            bool isValidHeaders = true;
            int i = 0;
            foreach (Cell c in firstRow.ChildElements)
            {
                i++;
                string value = GetHeaderText(c, stringTablePart, i);
                if (!excelHeader.ContainsKey(i) || string.IsNullOrEmpty(value) || value.ToLower().Trim() != excelHeader[i].ToLower().Trim())
                {
                    isValidHeaders = false;
                }
            }

            if (!isValidHeaders)
            {
                lblMsg.Text = "Please upload valid template.";
            }

            return isValidHeaders;
        }

        private string GetValue(Cell cell, SharedStringTablePart stringTablePart, ref int colPosition)
        {
            string value = string.Empty;
            string cellColumnLetter = Regex.Replace(cell.CellReference, @"\d", string.Empty);
            if (cell.ChildElements.Count == 0)
            {
                return string.Empty;
            }

            if (mapHeadCellMap.ContainsKey(cellColumnLetter))
            {
                colPosition = mapHeadCellMap[cellColumnLetter];
                value = cell.ElementAt(0).InnerText;
                if ((cell.DataType != null) && (cell.DataType == CellValues.SharedString))
                {
                    value = stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
                }

                if (value == null)
                {
                    value = string.Empty;
                }
            }

            return value;
        }

        private string GetHeaderText(Cell cell, SharedStringTablePart stringTablePart, int colPosition)
        {
            string value = string.Empty;
            string cellColumnLetter = Regex.Replace(cell.CellReference, @"\d", string.Empty);
            if (cell.ChildElements.Count == 0)
            {
                return string.Empty;
            }

            if (mapHeadCellMap.ContainsKey(cellColumnLetter) && mapHeadCellMap[cellColumnLetter] == colPosition)
            {
                value = cell.ElementAt(0).InnerText;
                if ((cell.DataType != null) && (cell.DataType == CellValues.SharedString))
                {
                    value = stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
                }

                if (value == null)
                {
                    value = string.Empty;
                }
            }

            return value;
        }

        private string GetUniqueKey()
        {
            string filePath = string.Empty;
            string guid = string.Empty;
            bool fileExist = false;
            do
            {
                fileExist = false;
                guid = Guid.NewGuid().ToString();
                fileExist = CheckFileExistence(filePath);
            }
            while (fileExist);
            return guid;
        }

        private bool CheckFileExistence(string filePath)
        {
            bool fileExists = false;
            using (ImpersonateUserBase adminUser = GetUserImpersonation())
            {
                fileExists = File.Exists(filePath);
                if (!fileExists)
                {
                    fileExists = Directory.Exists(filePath);
                }
            }

            return fileExists;
        }

        private ImpersonateUserBase GetUserImpersonation()
        {
            if (KTPApp.ImpersonationRequired == true)
            {
                return new ImpersonateUser(KTPApp.ImpersonateUserName, KTPApp.ImpersonateUserDomain, KTPApp.ImpersonateUserPassword);
            }
            else
            {
                return new ImpersonateUserBase();
            }
        }

        private void SaveFile(bool isZippedFile, HttpPostedFile httpPostfile, string savePath)
        {
            string folderLocation = KTPApp.UploadedTopicTemplateSavePath;
            using (ImpersonateUserBase adminUser = GetUserImpersonation())
            {
                if (false == Directory.Exists(folderLocation))
                {
                    Directory.CreateDirectory(folderLocation);
                }

                httpPostfile.SaveAs(savePath.Replace("/", "\\"));
            }
        }

        private bool IsUploadedDocumentValid()
        {
            string errorMsg = string.Empty;
            bool isValidUpload = true;
            HttpFileCollection hfc = Request.Files;
            if (!fuTopics.HasFile)
            {
                errorMsg = "Please select a file to Upload.";
            }
            else
            {
                for (int i = 0; i < hfc.Count; i++)
                {
                    HttpPostedFile hpf = hfc[i];
                    if (FileHelper.GetFileSize(hpf.ContentLength) > KTPApp.TopicFileUploadLimit)
                    {
                        errorMsg = errorMsg + "File Size is Too Large. ";
                    }
                    else if (!IsValidFile(hpf.FileName))
                    {
                        errorMsg = errorMsg + "Invalid File Type.";
                    }
                }
            }

            if (!string.IsNullOrEmpty(errorMsg))
            {
                lblMsg.Text = errorMsg;
                isValidUpload = false;
            }

            return isValidUpload;
        }

        private bool IsValidFile(string filePath)
        {
            bool isValid = false;

            for (int i = 0; i < fileExtensions.Length; i++)
            {
                if (filePath.ToUpper().Contains(fileExtensions[i]))
                {
                    isValid = true;
                    break;
                }
            }

            return isValid;
        }

        private void ClearGridvalues()
        {
            divError.Visible = false;
            divSuccess.Visible = false;
            divDuplicateTopics.Visible = false;
            gvInValidTopics.DataSource = null;
            gvInValidTopics.DataBind();
            gvValidTopicss.DataSource = null;
            gvValidTopicss.DataBind();
            gvDuplicateTopics.DataSource = null;
            gvDuplicateTopics.DataBind();
        }
    }
}