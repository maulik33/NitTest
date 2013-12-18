using System;
using System.Collections.Generic;
using System.IO;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class HelpfulDocumentPresenter : AuthenticatedPresenterBase<IHelpfulDocumentView>
    {
        private const string FILENOTFOUND_MESSAGE = "Could not find the file : ";
        private const string QUERY_PARAM_DOWNLOAD_ACTION_TYPE = "DownloadActionType";
        private const string QUERY_PARAM_ISLINK = "IsLink";
        private readonly IAdminService _adminService;
        private ViewMode _mode;

        public HelpfulDocumentPresenter(IAdminService service)
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

            if (_mode == ViewMode.List)
            {
                RegisterQueryParameter(QUERY_PARAM_ISLINK);
            }
        }

        public void ShowDocumentDetail()
        {
            if (_mode == ViewMode.Edit && false == CurrentContext.User.UploadAccess)
            {
                throw new Exception(string.Format("User {0} dont have access to Upload files.", CurrentContext.User.UserName));
            }

            HelpfulDocument helpfulDocument = null;
            if (Id > 0)
            {
                helpfulDocument = _adminService.GetHelpfulDocument(Id);
            }

            View.DisplayDocumentData(helpfulDocument);
        }

        public void SearchHelpfulDocs(string searchKeyword, string sortMetaData)
        {
            IEnumerable<HelpfulDocument> docs = _adminService.SearchHelpfulDocs(searchKeyword, View.IsLink);
            View.DisplaySearchResult(docs, SortHelper.Parse(sortMetaData));
        }

        public void DeleteHelpfulDoc(int docId)
        {
            _adminService.DeleteHelpfulDoc(CurrentContext.UserId, docId);
        }

        public void UploadHelpfulDocument(HelpfulDocument helpfulDocument)
        {
            helpfulDocument.Id = Id;
            if (Id == 0)
            {
                if (helpfulDocument.IsLink == false)
                {
                    string uniqueGuid = GetUniqueGUID(helpfulDocument);
                    string fileFullPath = helpfulDocument.GetFullFileName(uniqueGuid);
                    using (ImpersonateUserBase adminUser = GetUserImpersonation())
                    {
                        if (false == Directory.Exists(KTPApp.HelpfulDocumentFolderPath))
                        {
                            Directory.CreateDirectory(KTPApp.HelpfulDocumentFolderPath);
                        }

                        View.SaveDocument(fileFullPath);
                    }

                    helpfulDocument.GUID = uniqueGuid;
                }
            }
            else
            {
                if (!helpfulDocument.IsLink)
                {
                    helpfulDocument.FileName = string.Empty;
                }

                helpfulDocument.Type = string.Empty;
                helpfulDocument.GUID = string.Empty;
            }

            helpfulDocument.CreatedDateTime = DateTime.Now;
            helpfulDocument.Status = (int)Status.Active;
            helpfulDocument.CreatedBy = CurrentContext.UserId;
            _adminService.SaveHelpfulDocuments(helpfulDocument);
            NavigateToView(helpfulDocument.Id);
        }

        public ImpersonateUserBase GetUserImpersonation()
        {
            if (KTPApp.HelpfulDocImpersonationRequired == true)
            {
                return new ImpersonateUser(KTPApp.ImpersonateUserName, KTPApp.ImpersonateUserDomain, KTPApp.ImpersonateUserPassword);
            }
            else
            {
                return new ImpersonateUserBase();
            }
        }

        public HelpfulDocument GetHelpfulDocById(int Id)
        {
            HelpfulDocument helpfulDoc = new HelpfulDocument();
            helpfulDoc = _adminService.GetHelpfulDocument(Id);
            return helpfulDoc;
        }

        public void NavigateToDownload(int Id, int actionType)
        {
            Navigator.NavigateTo(AdminPageDirectory.OpenHelpfulDocuments, string.Empty, string.Format("{0}={1}&{2}={3}",
                    QUERY_PARAM_ID, Id, QUERY_PARAM_DOWNLOAD_ACTION_TYPE, ((int)actionType).ToString()));
        }

        public void NavigateToUpload(UserAction actionType)
        {
            if (actionType == UserAction.Add)
            {
                Navigator.NavigateTo(AdminPageDirectory.UploadHelpfulDocument, string.Empty, string.Format("{0}={1}",
                    QUERY_PARAM_ID, 0));
            }
            else if (actionType == UserAction.Edit)
            {
                Navigator.NavigateTo(AdminPageDirectory.UploadHelpfulDocument, string.Empty, string.Format("{0}={1}",
                    QUERY_PARAM_ID, GetParameterValue(QUERY_PARAM_ID)));
            }
        }

        public void NavigateToView(int id)
        {
            Navigator.NavigateTo(AdminPageDirectory.ViewHelpfulDocument, string.Empty, string.Format("{0}={1}",
                 QUERY_PARAM_ID, id));
        }

        public void OpenDocument(int actionType)
        {
            OpenDocument(Id, actionType);
        }

        public void OpenDocument(int id, int actionType)
        {
            HelpfulDocument helpfulDocument = _adminService.GetHelpfulDocument(id);
            if (helpfulDocument != null)
            {
                if (helpfulDocument.IsLink == true)
                {
                    ////do something here
                }
                else
                {
                    if (CheckFileExistence(helpfulDocument.FullFileName))
                    {
                        NavigateToDownload(id, actionType);
                    }
                    else
                    {
                        View.DisplayErrorMessage(string.Concat(FILENOTFOUND_MESSAGE, helpfulDocument.FileName));
                    }
                }
            }
        }

        public bool CheckFileExistence(string filePath)
        {
            bool fileExists = false;
            using (ImpersonateUserBase adminUser = GetUserImpersonation())
            {
                fileExists = File.Exists(filePath);
            }

            return fileExists;
        }

        public void InitializeProps()
        {
            View.CanUploadFiles = CurrentContext.User.UploadAccess;
            View.IsLink = GetParameterValue(QUERY_PARAM_ISLINK).Trim() == "1" ? true : false;
        }

        public List<Institution> GetAssignedInstitutions()
        {
            return _adminService.GetInstitutions(CurrentContext.UserId, string.Empty);
        }

        private string GetUniqueGUID(HelpfulDocument helpfulDocument)
        {
            string filePath = string.Empty;
            string guid = string.Empty;
            bool fileExist = false;
            do
            {
                fileExist = false;
                guid = Guid.NewGuid().ToString();
                filePath = helpfulDocument.GetFullFileName(guid);
                fileExist = CheckFileExistence(filePath);
            }
            while (fileExist);
            return guid;
        }
    }
}
