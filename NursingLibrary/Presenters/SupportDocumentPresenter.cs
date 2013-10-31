using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;
using System.Text;
using System.Web.UI;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class SupportDocumentPresenter : AuthenticatedPresenterBase<ISupportDocumentView>
    {
        private readonly IAdminService _adminService;
        private ViewMode _mode;
        private const string FILENOTFOUND_MESSAGE = "Could not find the file : ";
        private const string QUERY_PARAM_DOWNLOAD_ACTION_TYPE = "DownloadActionType";
        public SupportDocumentPresenter(IAdminService service)
            : base(Module.UserManagement)
        {
            _adminService = service;
        }

        public void PreInitialize(ViewMode mode)
        {
            _mode = mode;
        }

        public override void RegisterQueryParameters()
        {
            if (_mode == ViewMode.Edit || _mode == ViewMode.View)
            {
                RegisterQueryParameter(QUERY_PARAM_ID);
            }
        }

        public void ShowDocumentDetail()
        {
            if (_mode == ViewMode.Edit && false == CurrentContext.User.UploadAccess)
            {
                throw new Exception(string.Format("User {0} dont have access to Upload files.", CurrentContext.User.UserName));
            }

            SupportDocument supportDocument = null;
            if (Id > 0)
            {
                supportDocument = _adminService.GetSupportDocument(Id);
            }
            View.DisplayDocumentData(supportDocument);
        }

        public void SearchSupportDocs(string searchKeyword, string sortMetaData)
        {
            IEnumerable<SupportDocument> docs = _adminService.SearchSupportDocs(searchKeyword);
            View.DisplaySearchResult(docs, SortHelper.Parse(sortMetaData), CurrentContext.User.UploadAccess);
        }

        public void DeleteSupportDoc(int docId)
        {
            _adminService.DeleteSupportDoc(CurrentContext.UserId, docId);
        }

        public void UploadSupportDocument(SupportDocument supportDocument)
        {
            supportDocument.Id = Id;
            if (Id == 0)
            {
                string uniqueGuid = GetUniqueGUID(supportDocument);
                string fileFullPath = supportDocument.GetFullFileName(uniqueGuid);
                using (ImpersonateUserBase adminUser = GetUserImpersonation())
                {
                    if (false == Directory.Exists(KTPApp.SupportDocumentFolderPath))
                    {
                        Directory.CreateDirectory(KTPApp.SupportDocumentFolderPath);
                    }
                    View.SaveDocument(fileFullPath);
                }
                supportDocument.GUID = uniqueGuid;
            }
            else
            {
                supportDocument.FileName = string.Empty;
                supportDocument.Type = string.Empty;
                supportDocument.GUID = string.Empty;
            }
            supportDocument.CreatedDateTime = DateTime.Now;
            supportDocument.Status = (int)Status.Active;
            supportDocument.CreatedBy = CurrentContext.UserId;
            _adminService.SaveSupportDocuments(supportDocument);
            NavigateToView(supportDocument.Id);
        }

        private string GetUniqueGUID(SupportDocument supportDocument)
        {
            string filePath = string.Empty;
            string guid = string.Empty;
            bool fileExist = false;
            do
            {
                fileExist = false;
                guid = Guid.NewGuid().ToString();
                filePath = supportDocument.GetFullFileName(guid);
                fileExist = CheckFileExistence(filePath);
            } while (fileExist);
            return guid;
        }

        private ImpersonateUserBase GetUserImpersonation()
        {
            if (KTPApp.SupportDocImpersontionRequired == true)
                return new ImpersonateUser(KTPApp.ImpersonateUserName, KTPApp.ImpersonateUserDomain, KTPApp.ImpersonateUserPassword);
            else
                return new ImpersonateUserBase();
        }

        public SupportDocument GetSupportDocById(int Id)
        {
            SupportDocument supportDoc = new SupportDocument();
            supportDoc = _adminService.GetSupportDocument(Id);
            return supportDoc;
        }

        public void NavigateToDownload(int Id,int actionType)
        {
           Navigator.NavigateTo(AdminPageDirectory.OpenSupportDocuments, "", string.Format("{0}={1}&{2}={3}"
                    , QUERY_PARAM_ID, Id, QUERY_PARAM_DOWNLOAD_ACTION_TYPE, ((int)actionType).ToString()));
        }

        public void NavigateToUpload(UserAction actionType)
        {
            if (actionType == UserAction.Add)
            {
                Navigator.NavigateTo(AdminPageDirectory.UploadSupportDocument, "", string.Format("{0}={1}"
                    , QUERY_PARAM_ID, 0));
            }
            else if (actionType == UserAction.Edit)
            {
                Navigator.NavigateTo(AdminPageDirectory.UploadSupportDocument, "", string.Format("{0}={1}"
                    , QUERY_PARAM_ID, GetParameterValue(QUERY_PARAM_ID)));
            }
        }

        public void NavigateToView(int id)
        {
            Navigator.NavigateTo(AdminPageDirectory.ViewSupportDocument, "", string.Format("{0}={1}"
                 , QUERY_PARAM_ID, id));
        }

        public void OpenDocument(int actionType)
        {
            OpenDocument(Id,actionType);
        }

        public void OpenDocument(int id,int actionType)
        {
            SupportDocument supportDocument = _adminService.GetSupportDocument(id);
            if (supportDocument != null)
            {
                if (CheckFileExistence(supportDocument.FullFileName))
                {
                    NavigateToDownload(id, actionType);
                }
                else
                {
                    View.DisplayErrorMessage(string.Concat(FILENOTFOUND_MESSAGE, supportDocument.FileName));
                }
            }
        }

        private bool CheckFileExistence(string filePath)
        {
            bool fileExists = false;
            using (ImpersonateUserBase adminUser = GetUserImpersonation())
            {
                fileExists = File.Exists(filePath);
            }
            return fileExists;
        }
    }
}
