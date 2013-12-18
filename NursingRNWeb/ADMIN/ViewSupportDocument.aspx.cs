using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Entity;

namespace NursingRNWeb.ADMIN
{
    public partial class ViewSupportDocument : PageBase<ISupportDocumentView, SupportDocumentPresenter>, ISupportDocumentView
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Presenter.ShowDocumentDetail();
            }
        }

        public override void PreInitialize()
        {
            Presenter.PreInitialize(ViewMode.View);
        }

        public void DisplayDocumentData(SupportDocument supportDocument)
        {
            if (supportDocument != null)
            {
                lblDescription.Text = supportDocument.Description;
                lblTitle.Text = supportDocument.Title;
                lbtnOpenFile.Text = supportDocument.FileName;
                lblUploadedOn.Text = supportDocument.CreatedDateTime.ToString();
                lblUploadedBy.Text = string.Concat(supportDocument.Admin.LastName, ", ", supportDocument.Admin.FirstName);
            }
        }

        protected void lbtnOpenFile_Click(object sender, EventArgs e)
        {
            int actionType = 1;
            Presenter.OpenDocument(actionType);
        }

        public void DisplaySearchResult(IEnumerable<SupportDocument> docs, NursingLibrary.Common.SortInfo sortInfo, bool canUploadFiles)
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

    }
}