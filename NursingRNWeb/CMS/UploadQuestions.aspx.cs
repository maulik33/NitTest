using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Packaging;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;
using NursingRNWeb.AppCode;

namespace NursingRNWeb.CMS
{
    public partial class UploadQuestions : PageBase<IQuestionView, QuestionPresenter>, IQuestionView
    {
        private string[] fileExtensions = { ".DOCX", ".ZIP", ".DOCM" };

        #region UnImplemented props

        public string URLQuery
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string TestId
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string VType
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        public int ProgramOfStudyId { get; set; }

        #region Abstract Method
        public override void PreInitialize()
        {
        }
        #endregion

        #region UnImplemented Methods
        public void ShowErrorMessage(string errorMsg)
        {
        }

        public void PopulateInitialQuestionParameters(IEnumerable<Topic> titles, IEnumerable<ProgramofStudy> programofStudy)
        {
            throw new NotImplementedException();
        }

        public void PopulateSearchQuestionCriteria(IEnumerable<Product> products, IEnumerable<Topic> titles, IDictionary<CategoryName, Category> categories)
        {
            throw new NotImplementedException();
        }

        public void PopulateSearchQuestionCriteria(IEnumerable<Product> products, IEnumerable<Topic> titles, IDictionary<CategoryName, Category> categories, int programofStudy)
        {
            throw new NotImplementedException();
        }

        public void ShowSearchQuestionResults(IEnumerable<QuestionResult> searchQuestionResults, SortInfo sortMetaData)
        {
            throw new NotImplementedException();
        }

        public void ShowSearchRemediationResults(IEnumerable<Remediation> searchRemediationResults)
        {
            throw new NotImplementedException();
        }

        public void PopulateQuestion(Question question, int mode)
        {
            throw new NotImplementedException();
        }

        public void PopulateClientNeedCategories(IDictionary<int, CategoryDetail> categories)
        {
            throw new NotImplementedException();
        }

        public void PopulateAnswers(IEnumerable<AnswerChoice> answers)
        {
            throw new NotImplementedException();
        }

        public void PopulateTests(IEnumerable<Test> tests)
        {
            throw new NotImplementedException();
        }

        public void RefreshPage(Question question, UserAction action, Dictionary<string, string> fileType, Dictionary<string, string> questionType, string mode, string testId, bool hasDeletePermission, bool hasAddPermission)
        {
            throw new NotImplementedException();
        }

        public void ShowErrorMessage()
        {
            throw new NotImplementedException();
        }

        public void PopulateAlternateTextDetails(Question question, UserAction actionType)
        {
            throw new NotImplementedException();
        }

        public void PopulateProgramOfStudy(IEnumerable<ProgramofStudy> programOfStudies)
        {
            throw new NotImplementedException();
        }
        #endregion

        public void DisplayUploadedQuestions(IEnumerable<UploadQuestionDetails> uploadedQuestions, int FileType, string UnZippedFolderPath)
        {
            ClearGridvalues();
            if (uploadedQuestions.Where(q => q.IsValid == false).Count() == 0)
            {
                btnSave.Visible = true;
                hfDocType.Value = FileType.ToString();
                hfUnZippedLocation.Value = UnZippedFolderPath;
                divError.Visible = false;
            }
            else if (uploadedQuestions.Where(q => q.IsValid == false).Count() > 0)
            {
                divError.Visible = true;
                gvInValidQuestions.DataSource = GetUploadErrors(uploadedQuestions);
                gvInValidQuestions.DataBind();
            }

            if (uploadedQuestions.Where(q => q.IsValid == true).Count() > 0)
            {
                divSuccess.Visible = true;
                gvValidQuestions.DataSource = uploadedQuestions.Where(q => q.IsValid == true);
                gvValidQuestions.DataBind();
            }
            else
            {
                divSuccess.Visible = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            btnSave.Visible = false;

            if (Global.IsProductionApp)
            {
                btnUpload.Visible = false;
                ddlTemplate.Visible = false;
                btnSave.Visible = false;
                lbInstructionDocument.Visible = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckFileExistence(hfUnZippedLocation.Value))
            {
                List<UploadQuestionDetails> uploadQuestions = new List<UploadQuestionDetails>();
                if (hfDocType.Value.ToInt() == 1)
                {
                    string[] files = GetFilesInFolder(hfUnZippedLocation.Value);
                    foreach (string file in files)
                    {
                        FillQuestionPropertyValues(uploadQuestions, file, 1);
                    }
                }
                else if (hfDocType.Value.ToInt() == 2)
                {
                    FillQuestionPropertyValues(uploadQuestions, hfUnZippedLocation.Value, 2);
                }

                bool uploadedSuccessfully = Presenter.SaveUploadedQuestions(uploadQuestions, hfDocType.Value.ToInt(), hfUnZippedLocation.Value);
                if (uploadedSuccessfully)
                {
                    hfDocType.Value = string.Empty;
                    hfUnZippedLocation.Value = string.Empty;
                    btnCMS.Visible = true;
                    lblSuccessMsg.Text = "Questions uploaded successfully";
                }
            }
        }

        /// <summary>
        /// Here we will upload the question template to server folder and will display save button if the uploaded template is valid. Will save uploded doc/folder path in hidden field.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            btnCMS.Visible = false;
            List<UploadQuestionDetails> uploadQuestions = new List<UploadQuestionDetails>();
            HttpFileCollection fileCollection = Request.Files;
            string uniquekey = string.Empty;
            string UnZippedFolderPath = string.Empty;
            string zipDocFilePath = string.Empty;
            string uploadedFileName = string.Empty;
            string fileType = string.Empty;
            bool IsValidUpload = false;
            int uploadedFileType = 2;
            ClearGridvalues();
            if (IsUploadedDocumentValid())
            {
                for (int i = 0; i < fileCollection.Count; i++)
                {
                    HttpPostedFile httpPostfile = fileCollection[i];
                    if (httpPostfile.ContentLength > 0)
                    {
                        uniquekey = GetUniqueKey();
                        fileType = FileHelper.GetFileExtension(httpPostfile.FileName);
                        zipDocFilePath = KTPApp.UploadQuestionZippedFilePath + uniquekey + fileType;

                        uploadedFileName = FileHelper.GetFileName(httpPostfile.FileName.ToString());
                        if (fileType.ToUpper() == ".ZIP")
                        {
                            SaveFile(true, httpPostfile, zipDocFilePath);
                            ////Check all the document format inside ziped folder.
                            if (ValidZipFolder(zipDocFilePath, uniquekey))
                            {
                                IsValidUpload = true;
                                uploadedFileType = 1;
                                ////Save extacted documents into specific folder.
                                ExtractZippedFiles(zipDocFilePath, uniquekey);
                                UnZippedFolderPath = KTPApp.UploadQuestionSavePath + uniquekey;
                                string[] files = GetFilesInFolder(UnZippedFolderPath);
                                foreach (string file in files)
                                {
                                    FillQuestionPropertyValues(uploadQuestions, file, uploadedFileType);
                                }
                            }
                        }
                        else
                        {
                            IsValidUpload = true;
                            UnZippedFolderPath = KTPApp.UploadQuestionSavePath + uniquekey + FileHelper.GetFileExtension(httpPostfile.FileName);
                            SaveFile(false, httpPostfile, UnZippedFolderPath);
                            FillQuestionPropertyValues(uploadQuestions, UnZippedFolderPath, uploadedFileType, uploadedFileName);
                        }

                        if (IsValidUpload)
                        {
                            Presenter.SaveUploadedQuestionDetails(uniquekey, uploadedFileName);
                            Presenter.DisplayUploadedQuestions(uploadQuestions, uploadedFileType, UnZippedFolderPath);
                        }
                    }
                }
            }
        }

        protected void btnDownloadTemplate_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            string filename = string.Empty;
            if (ddlProgramofStudy.SelectedValue != "-1")
            {
                if (ddlTemplate.SelectedValue != "-1")
                {
                    if (ddlTemplate.SelectedValue == "1")
                    {
                        filePath = ddlProgramofStudy.SelectedValue == "1" ? KTPApp.UploadQuestionTemplatePath : KTPApp.UploadMultipleChoiceSingleBestAnswerQuestionTemplate_PN;
                        filename = ddlProgramofStudy.SelectedValue == "1" ? "MultipleChoiceSingleBestAnswerQuestionTemplate.docm" : "MultipleChoiceSingleBestAnswerQuestionTemplate_PN.docm";
                    }
                    else if (ddlTemplate.SelectedValue == "2")
                    {
                        filePath = ddlProgramofStudy.SelectedValue == "1" ? KTPApp.UploadMultiSelectQuestionTemplatePath : KTPApp.UploadMultipleChoiceMultiSelectQuestionTemplate_PN;
                        filename = ddlProgramofStudy.SelectedValue == "1" ? "MultipleChoiceMultiSelectQuestionTemplate.docm" : "MultipleChoiceMultiSelectQuestionTemplate_PN.docm";
                    }
                    else if (ddlTemplate.SelectedValue == "3")
                    {
                        filePath = ddlProgramofStudy.SelectedValue == "1" ? KTPApp.UploadNumericalFillInQuestionTemplatePath : KTPApp.UploadNumericalFillInQuestionTemplate_PN;
                        filename = ddlProgramofStudy.SelectedValue == "1" ? "NumericalFillInTemplate.docm" : "NumericalFillInTemplate_PN.docm";
                    }

                    FileInfo fileInfo = new FileInfo(filePath);
                    if (CheckFileExistence(filePath))
                    {
                        using (ImpersonateUserBase adminUser = GetUserImpersonation())
                        {
                            Response.Clear();
                            Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
                            Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                            Response.ContentType = "application/octet-stream";
                            Response.WriteFile(fileInfo.FullName);
                            using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
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
            }
        }

        protected void btnCMS_Click(object sender, EventArgs e)
        {
            Presenter.NavigateToSearch(string.Empty);
        }

        protected void lbInstructionDocument_Click(object sender, EventArgs e)
        {
            string filePath = KTPApp.InstructionsDocumentForUploadingQuestions;
            string filename = "InstructionsDocumentForUploadingQuestions.pdf";
            FileInfo fileInfo = new FileInfo(filePath);

            if (CheckFileExistence(filePath))
            {
                using (ImpersonateUserBase adminUser = GetUserImpersonation())
                {
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
                    Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.WriteFile(fileInfo.FullName);
                    using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
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

        private List<UploadQuestionError> GetUploadErrors(IEnumerable<UploadQuestionDetails> uploadedQuestions)
        {
            List<UploadQuestionError> errors = new List<UploadQuestionError>();
            foreach (UploadQuestionDetails up in uploadedQuestions)
            {
                if (up.IsValid == false && up.ErrorMessage.Count() > 0)
                {
                    foreach (string errorMessage in up.ErrorMessage)
                    {
                        errors.Add(new UploadQuestionError { ErrorMessage = errorMessage, QuestionId = up.Question.QuestionId, FileName = up.FileName });
                    }
                }
            }

            return errors;
        }

        private void ClearGridvalues()
        {
            divError.Visible = false;
            divSuccess.Visible = false;
            gvInValidQuestions.DataSource = null;
            gvInValidQuestions.DataBind();
            gvValidQuestions.DataSource = null;
            gvValidQuestions.DataBind();
        }

        private string[] GetFilesInFolder(string folderPath)
        {
            using (ImpersonateUserBase adminUser = GetUserImpersonation())
            {
                return Directory.GetFiles(folderPath);
            }
        }

        private void SaveFile(bool isZippedFile, HttpPostedFile httpPostfile, string savePath)
        {
            string folderLocation = string.Empty;
            if (isZippedFile)
            {
                folderLocation = KTPApp.UploadQuestionZippedFilePath;
            }
            else
            {
                folderLocation = KTPApp.UploadQuestionSavePath;
            }

            using (ImpersonateUserBase adminUser = GetUserImpersonation())
            {
                if (false == Directory.Exists(folderLocation))
                {
                    Directory.CreateDirectory(folderLocation);
                }

                httpPostfile.SaveAs(savePath.Replace("/", "\\"));
            }
        }

        private void FillQuestionPropertyValues(List<UploadQuestionDetails> uploadQuestions, string filePath, int fileType, string uploadedFileName = "")
        {
            using (ImpersonateUserBase adminUser = GetUserImpersonation())
            {
                using (WordprocessingDocument wordProcDoc = WordprocessingDocument.Open(filePath, true))
                {
                    Question question = new Question();
                    UploadQuestionDetails uploadquestion = new UploadQuestionDetails();
                    question = QuestionUploadHelper.GetQuestionValue(question, wordProcDoc);
                    uploadquestion.Answers = QuestionUploadHelper.GetSingleSelectAnswerChoices(wordProcDoc);
                    uploadquestion.Question = question;
                    if (fileType == 1)
                    {
                        uploadquestion.FileName = GetDocumentName(filePath) + " ";
                    }
                    else
                    {
                        uploadquestion.FileName = uploadedFileName;
                    }

                    uploadquestion.IsValid = true;
                    uploadQuestions.Add(uploadquestion);
                }
            }
        }

        private bool IsUploadedDocumentValid()
        {
            string errorMsg = string.Empty;
            bool isValidUpload = true;
            HttpFileCollection hfc = Request.Files;
            if (!fuQuestions.HasFile)
            {
                errorMsg = "Please select a file to Upload.";
            }
            else
            {
                for (int i = 0; i < hfc.Count; i++)
                {
                    HttpPostedFile hpf = hfc[i];
                    if (FileHelper.GetFileSize(hpf.ContentLength) > KTPApp.QuestionFileUploadLimit)
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

        /// <summary>
        /// Check in the folder and create new unique key
        /// </summary>
        /// <returns></returns>
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

        private bool ValidZipFolder(string zipFilePath, string uniquekey)
        {
            bool isValidDocsZipped = true;
            int fileCount = 0;
            using (ImpersonateUserBase adminUser = GetUserImpersonation())
            {
                //// Open an existing zip file for reading
                ZipExtension zip = ZipExtension.Open(zipFilePath, FileAccess.Read);
                //// Read the central directory collection
                List<ZipExtension.ZipFileEntry> dir = zip.ReadCentralDir();
                //// Look for the desired file and check all related to .docx
                foreach (ZipExtension.ZipFileEntry entry in dir)
                {
                    fileCount++;
                    string fileExtension = FileHelper.GetFileExtension(entry.FilenameInZip).ToUpper();
                    if (fileExtension != ".DOCX" && fileExtension != ".DOCM")
                    {
                        isValidDocsZipped = false;
                        lblMsg.Text = "Zipped file has invalid documents.";
                        break;
                    }

                    if (fileCount > KTPApp.MaxQuestionTemplateUploadLimit)
                    {
                        break;
                    }
                }

                if (isValidDocsZipped == true && fileCount > KTPApp.MaxQuestionTemplateUploadLimit)
                {
                    isValidDocsZipped = false;
                    lblMsg.Text = "Number of files inside zipped folder exceeds max limit of " + KTPApp.MaxQuestionTemplateUploadLimit;
                }
                //// Zip folder contains only .docx we move to new folder
                zip.Close();
            }

            return isValidDocsZipped;
        }

        private void ExtractZippedFiles(string zipFilePath, string uniquekey)
        {
            int length = 0;
            string tempName = string.Empty;
            string name = string.Empty;
            using (ImpersonateUserBase adminUser = GetUserImpersonation())
            {
                if (false == Directory.Exists(KTPApp.UploadQuestionSavePath))
                {
                    Directory.CreateDirectory(KTPApp.UploadQuestionSavePath);
                }

                ZipExtension zip = ZipExtension.Open(zipFilePath, FileAccess.Read);
                List<ZipExtension.ZipFileEntry> dir = zip.ReadCentralDir();

                foreach (ZipExtension.ZipFileEntry entry in dir)
                {
                    tempName = entry.FilenameInZip.Replace("/", "\\");
                    name = GetDocumentName(tempName);
                    if (length >= 0)
                    {
                        zip.ExtractFile(entry, KTPApp.UploadQuestionSavePath + uniquekey + "\\" + name);
                    }
                }
            }
        }

        private string GetDocumentName(string tempName)
        {
            string[] name = tempName.Replace("/", "\\").Split('\\');
            int length = name.Count() - 1;
            return name[length];
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
    }
}