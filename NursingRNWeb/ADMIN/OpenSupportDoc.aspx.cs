using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Entity;
using NursingLibrary.Common;
using System.IO;

namespace NursingRNWeb.ADMIN
{
    public partial class OpenSupportDoc : PageBase<ISupportDocumentView, SupportDocumentPresenter>, ISupportDocumentView
    {
        private const string QUERY_PARAM_ID = "Id";
        public const string QUERY_PARAM_DOWNLOAD_ACTION_TYPE = "DownloadActionType";
        private const string PARAM_SEPERATOR = "\\";

        protected void Page_Load(object sender, EventArgs e)
        {
            OpenSupportDocuments();
        }

        public override void PreInitialize()
        {
            Presenter.PreInitialize(ViewMode.View);
        }

        public void DisplaySearchResult(IEnumerable<SupportDocument> docs, SortInfo sortInfo, bool canUploadFiles)
        { }

        public void DisplayDocumentData(SupportDocument supportDocument)
        { }

        public void OpenSupportDocuments()
        {
            SupportDocument supportDoc = new SupportDocument();
            byte[] buffer = new Byte[10000];
            string folderPath = KTPApp.SupportDocumentFolderPath;

            int Id = Convert.ToInt32(Presenter.GetParameterValue(QUERY_PARAM_ID));
            int actionType = Convert.ToInt32(Presenter.GetParameterValue(QUERY_PARAM_DOWNLOAD_ACTION_TYPE));
            supportDoc = Presenter.GetSupportDocById(Id);
            string fullPath = string.Concat(folderPath, PARAM_SEPERATOR, supportDoc.GUID, supportDoc.Type);
            FileInfo fileInfo = new FileInfo(supportDoc.FileName);
            if (File.Exists(fullPath))
            {
                Response.ContentType = ReturnExtension(fileInfo.Extension.ToLower());
                if (actionType == 1)
                {
                    Response.AddHeader("Content-Disposition", "attachment; filename=\"" + supportDoc.FileName + "\"");
                }
                Response.Clear();
                Response.TransmitFile(fullPath);
                Response.End();
            }
        }

        public void SaveDocument(string folderFullPath)
        { }

        public void DisplayErrorMessage(string fileName)
        { }

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