using System;
using System.Collections.Generic;
using System.IO;
using NursingLibrary.Common;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;

namespace NursingRNWeb.ADMIN
{
    public partial class OpenHelpfulDoc : PageBase<IHelpfulDocumentView, HelpfulDocumentPresenter>, IHelpfulDocumentView
    {
        public const string QUERY_PARAM_DOWNLOAD_ACTION_TYPE = "DownloadActionType";
        private const string QUERY_PARAM_ID = "Id";
        private const string PARAM_SEPERATOR = "\\";

        public bool IsLink
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

        public bool CanUploadFiles
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

        public override void PreInitialize()
        {
            Presenter.PreInitialize(ViewMode.View);
        }

        public void DisplaySearchResult(IEnumerable<HelpfulDocument> docs, SortInfo sortInfo)
        {
        }

        public void DisplayDocumentData(HelpfulDocument helpfulDocument)
        {
        }

        public void OpenHelpfulDocuments()
        {
            HelpfulDocument helpfulDoc = new HelpfulDocument();
            string folderPath = KTPApp.HelpfulDocumentFolderPath;

            int Id = Convert.ToInt32(Presenter.GetParameterValue(QUERY_PARAM_ID));
            int actionType = Convert.ToInt32(Presenter.GetParameterValue(QUERY_PARAM_DOWNLOAD_ACTION_TYPE));
            helpfulDoc = Presenter.GetHelpfulDocById(Id);
            string fullPath = string.Concat(folderPath, PARAM_SEPERATOR, helpfulDoc.GUID, helpfulDoc.Type);
            FileInfo fileInfo = new FileInfo(fullPath);
            if (Presenter.CheckFileExistence(fullPath))
            {
                using (ImpersonateUserBase adminUser = Presenter.GetUserImpersonation())
                {
                    // http://support.microsoft.com/kb/812406/EN-US/
                    Response.Clear();
                    Response.ContentType = ReturnExtension(fileInfo.Extension.ToLower());
                    if (actionType == 1)
                    {
                        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + helpfulDoc.FileName + "\"");
                    }

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

        public void SaveDocument(string folderFullPath)
        {
        }

        public void DisplayErrorMessage(string fileName)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            OpenHelpfulDocuments();
        }

        private string ReturnExtension(string fileExtension)
        {
            switch (fileExtension)
            {
                case ".htm":
                case ".html":
                case ".log":
                    return "text/HTML";
                case ".txt":
                    return "text/plain";
                case ".doc":
                    return "application/msword";
                case ".tiff":
                case ".tif":
                    return "image/tiff";
                case ".asf":
                    return "video/x-ms-asf";
                case ".avi":
                    return "video/avi";
                case ".zip":
                    return "application/zip";
                case ".rar":
                    return "application/x-rar-compressed";
                case ".xls":
                case ".csv":
                    return "application/vnd.ms-excel";
                case ".gif":
                    return "image/gif";
                case ".jpg":
                case "jpeg":
                    return "image/jpeg";
                case ".bmp":
                    return "image/bmp";
                case ".wav":
                    return "audio/wav";
                case ".mp3":
                    return "audio/mpeg3";
                case ".mpg":
                case "mpeg":
                    return "video/mpeg";
                case ".rtf":
                    return "application/rtf";
                case ".asp":
                    return "text/asp";
                case ".pdf":
                    return "application/pdf";
                case ".fdf":
                    return "application/vnd.fdf";
                case ".ppt":
                    return "application/mspowerpoint";
                case ".dwg":
                    return "image/vnd.dwg";
                case ".msg":
                    return "application/msoutlook";
                case ".xml":
                case ".sdxl":
                    return "application/xml";
                case ".xdp":
                    return "application/vnd.adobe.xdp+xml";
                case ".xlsx":
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case ".docx":
                    return "application/vnd.openxmlformats- officedocument.wordprocessingml.document";
                case ".pptx":
                    return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                default:
                    return "application/octet-stream";
            }
        }
    }
}