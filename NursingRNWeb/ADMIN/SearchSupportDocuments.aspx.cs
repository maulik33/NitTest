using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;
using System.Text;

namespace NursingRNWeb.ADMIN
{
    public partial class SearchSupportDocuments : PageBase<ISupportDocumentView, SupportDocumentPresenter>, ISupportDocumentView
    {
        private const string Message = "Could not find the file : ";
        private bool _canUploadFiles = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                SearchSupportDocs();
        }

        public override void PreInitialize()
        {
            Presenter.PreInitialize(ViewMode.List);
        }

        #region ISupportDocumentView Methods

        public void SearchSupportDocs()
        {
            Presenter.SearchSupportDocs(txtKeyword.Text, hdnGridConfig.Value);
        }

        public void DisplaySearchResult(IEnumerable<SupportDocument> docs, SortInfo sortMetaData, bool canUploadFiles)
        {
            _canUploadFiles = canUploadFiles;
            IEnumerable<SupportDocument> docList = KTPSort.Sort<SupportDocument>(docs, sortMetaData);
            gvSupportDocs.DataSource = docList;
            gvSupportDocs.DataBind();
        }

        #endregion

        protected void gvSupportDocs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                WebControl objDelete = ((WebControl)e.Row.Cells[8].Controls[0]);
                objDelete.Attributes.Add("onclick", "return confirm('Are you sure that you want to delete this Document?')");
            }
        }

        protected void gvSupportDocs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSupportDocs.PageIndex = e.NewPageIndex;
            SearchSupportDocs();
        }

        protected void gvSupportDocs_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Id = (int)gvSupportDocs.DataKeys[e.RowIndex].Value;
            Presenter.DeleteSupportDoc(Id);
            SearchSupportDocs();
        }

        protected void gvSupportDocs_Sorting(object sender, GridViewSortEventArgs e)
        {
            hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);
            SearchSupportDocs();
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            SearchSupportDocs();
        }

        public void DisplayDocumentData(SupportDocument supportDocument)
        { }

        public void SaveDocument(string folderFullPath)
        { }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            int Id = ((LinkButton)(sender)).CommandArgument.ToInt();
            int actionType = 1;
            Presenter.OpenDocument(Id, actionType);
        }

        public void DisplayErrorMessage(string errorMsg)
        {
            lblErrorMsg.Text = errorMsg;
        }

        protected void gvSupportDocs_DataBound(object sender, EventArgs e)
        {
            if (_canUploadFiles)
            {
                gvSupportDocs.Columns[7].Visible = true;
                gvSupportDocs.Columns[8].Visible = true;
            }
        }

    }
}