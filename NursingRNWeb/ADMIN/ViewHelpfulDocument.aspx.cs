using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;

namespace NursingRNWeb.ADMIN
{
    public partial class ViewHelpfulDocument : PageBase<IHelpfulDocumentView, HelpfulDocumentPresenter>, IHelpfulDocumentView
    {
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

        public void DisplayDocumentData(HelpfulDocument helpfulDocument)
        {
            if (helpfulDocument != null)
            {
                lblDescription.Text = helpfulDocument.Description;
                lblTitle.Text = helpfulDocument.Title;

                lblUploadedOn.Text = helpfulDocument.CreatedDateTime.ToString();
                lblUploadedBy.Text = string.Concat(helpfulDocument.Admin.LastName, ", ", helpfulDocument.Admin.FirstName);
                if (helpfulDocument.IsLink)
                {
                    lbtnOpenFile.Visible = false;
                    hlLink.Text = helpfulDocument.FileName;
                    hlLink.NavigateUrl = helpfulDocument.FileName;
                    lblUploadedDate.Text = "Enter Date";
                    lblUploadedUser.Text = "Entered By";
                    lblUploadedFile.Text = "Link";
                    lblHeader.Text = "View Link";
                }
                else
                {
                    lbtnOpenFile.Text = helpfulDocument.FileName;
                    hlLink.Visible = false;
                    lblUploadedDate.Text = "Uploaded On";
                    lblUploadedUser.Text = "Uploaded By";
                    lblUploadedFile.Text = "File";
                    lblHeader.Text = "View document";
                }
            }
        }

        public void DisplaySearchResult(IEnumerable<HelpfulDocument> docs, NursingLibrary.Common.SortInfo sortInfo)
        {
            throw new NotImplementedException();
        }

        public void SaveDocument(string folderFullPath)
        {
            throw new NotImplementedException();
        }

        public void ShowErrorMessage(string message)
        {
            throw new NotImplementedException();
        }

        public void DisplayErrorMessage(string errorMessage)
        {
            lblMessage.Text = errorMessage;
        }

        protected void Page_Load(object sender, EventArgs e)
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

            Presenter.ShowDocumentDetail();
        }

        protected void lbtnOpenFile_Click(object sender, EventArgs e)
        {
            int actionType = 1;
            Presenter.OpenDocument(actionType);
        }
    }
}