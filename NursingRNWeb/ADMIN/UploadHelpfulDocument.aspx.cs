using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using NursingLibrary.Common;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

namespace NursingRNWeb.ADMIN
{
    public partial class UploadHelpfulDocument : PageBase<IHelpfulDocumentView, HelpfulDocumentPresenter>, IHelpfulDocumentView
    {
        private const string Title_Manadatory = "Please enter document title.";
        private const string Exceeds_MaxUpload_Limit = "File size exceeds maximum upload limit of {0} MB.";
        private const string Upload_ErrorMessage = "You have chosen to upload a file and a link.Please choose one or the other.";
        private const string Upload_Mandatory = "Please choose a file or link to Upload.";
        private const string Link_Mandatory = "Please choose a link to save";
        private const string Web_Link = "http://";
        
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

        #region IHelpfulDocument Methods

        public void DisplayDocumentData(HelpfulDocument helpfulDocument)
        {
            if (helpfulDocument != null)
            {
                txtDescription.Text = helpfulDocument.Description;
                txtTitle.Text = helpfulDocument.Title;

                if (helpfulDocument.IsLink)
                {
                    linkUploadControlRow.Visible = true;
                    txtLink.Text = helpfulDocument.FileName;
                }
                else
                {
                    uploadedFile.Visible = true;
                    lbtnOpenFile.Text = helpfulDocument.FileName;
                }
            }
        }

        public void SaveDocument(string folderFullPath)
        {
            fuHelpfulDocuments.SaveAs(folderFullPath);
        }

        public override void PreInitialize()
        {
            Presenter.PreInitialize(ViewMode.Edit);
        }

        public void DisplaySearchResult(IEnumerable<HelpfulDocument> docs, NursingLibrary.Common.SortInfo sortInfo)
        {
        }

        public void DisplayErrorMessage(string errorMessage)
        {
            lblMessage.Text = errorMessage;
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Presenter.CurrentContext.UserType == UserType.LocalAdmin || Presenter.CurrentContext.UserType == UserType.TechAdmin || Presenter.CurrentContext.UserType == UserType.InstitutionalAdmin)
                {
                    List<Institution> _institutionIds = Presenter.GetAssignedInstitutions();
                    if (_institutionIds.Count() == 0)
                    {
                        Control menutab = this.Master.FindControl("menuDiv");
                        if (menutab != null)
                        {
                            menutab.Visible = false;
                        }
                    }
                }

                if (Presenter.Id > 0)
                {
                    fileUploadControlRow.Visible = false;
                    linkUploadControlRow.Visible = false;
                    btnUpload.Text = "Save";
                }

                Presenter.ShowDocumentDetail();
            }
        }

        protected void lbtnOpenFile_Click(object sender, EventArgs e)
        {
            int actionType = 1;
            Presenter.OpenDocument(actionType);
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fuHelpfulDocuments.HasFile || fileUploadControlRow.Visible == false || (!string.IsNullOrEmpty(txtLink.Text)))
            {
                if (ValidateDocumentUpload())
                {
                    HelpfulDocument helpfulDoc = new HelpfulDocument();
                    helpfulDoc.Title = txtTitle.Text;
                    helpfulDoc.Description = txtDescription.Text;
                    if (fuHelpfulDocuments.HasFile)
                    {
                        helpfulDoc.FileName = fuHelpfulDocuments.FileName;
                        helpfulDoc.Size = FileHelper.GetFileSize(fuHelpfulDocuments.PostedFile.ContentLength);
                        helpfulDoc.Type = FileHelper.GetFileExtension(fuHelpfulDocuments.FileName);
                        helpfulDoc.IsLink = false;
                    }
                    else
                    {
                        if (Presenter.Id > 0 && linkUploadControlRow.Visible)
                        {
                            if (string.IsNullOrEmpty(txtLink.Text))
                            {
                                lblMessage.Text = Link_Mandatory;
                                return;
                            }
                        }

                        if (!string.IsNullOrEmpty(txtLink.Text))
                        {
                            string link = txtLink.Text.ToLower();
                            helpfulDoc.FileName = link.StartsWith("http://") || link.StartsWith("https://")
                                                      ? txtLink.Text
                                                      : Web_Link + txtLink.Text;

                            helpfulDoc.Type = string.Empty;
                            helpfulDoc.GUID = string.Empty;
                            helpfulDoc.IsLink = true;
                        }
                    }

                    Presenter.UploadHelpfulDocument(helpfulDoc);
                }
            }
            else
            {
                lblMessage.Text = Upload_Mandatory;
            }
        }

        private bool ValidateDocumentUpload()
        {
            StringBuilder errorMessage = new StringBuilder();
            int maxUploadSize = KTPApp.HelpfulDocumentUploadLimit;

            if (fuHelpfulDocuments.HasFile && (!string.IsNullOrEmpty(txtLink.Text)))
            {
                errorMessage.Append(Upload_ErrorMessage);
            }
            else
            {
                if (string.IsNullOrEmpty(txtTitle.Text))
                {
                    errorMessage.Append(Title_Manadatory);
                }

                if (fuHelpfulDocuments.HasFile && FileHelper.GetFileSize(fuHelpfulDocuments.PostedFile.ContentLength) > maxUploadSize)
                {
                    errorMessage.AppendLine(string.Format(Exceeds_MaxUpload_Limit, maxUploadSize));
                }
            }

            if (errorMessage.Length > 0)
            {
                lblMessage.Text = errorMessage.ToString();
            }

            return errorMessage.Length == 0;
        }
    }
}