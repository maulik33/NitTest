using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

namespace NursingRNWeb.ADMIN
{
    public partial class SearchHelpfulDocuments : PageBase<IHelpfulDocumentView, HelpfulDocumentPresenter>, IHelpfulDocumentView
    {
        private const string Message = "Could not find the file : ";

        public bool CanUploadFiles { get; set; }

        public bool IsLink { get; set; }

        public override void PreInitialize()
        {
            Presenter.PreInitialize(ViewMode.List);
        }

        #region IHelpfulDocumentView Methods

        public void SearchHelpfulDocs()
        {
            Presenter.SearchHelpfulDocs(txtKeyword.Text, hdnGridConfig.Value);
        }

        public void DisplaySearchResult(IEnumerable<HelpfulDocument> docs, SortInfo sortMetaData)
        {
            IEnumerable<HelpfulDocument> docList = KTPSort.Sort<HelpfulDocument>(docs, sortMetaData);
            gvHelpfulDocs.DataSource = docList;
            gvHelpfulDocs.DataBind();
        }

        #endregion

        public void DisplayDocumentData(HelpfulDocument HelpfulDocument)
        {
        }

        public void SaveDocument(string folderFullPath)
        {
        }

        public void DisplayErrorMessage(string errorMsg)
        {
            lblErrorMsg.Text = errorMsg;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Presenter.InitializeProps();
            SetControlTexts();
            if (!IsPostBack)
            {
                SearchHelpfulDocs();
            }

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
        }

        protected void gvHelpfulDocs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                WebControl objDelete = (WebControl)e.Row.Cells[9].Controls[0];

                if (IsLink)
                {
                    objDelete.Attributes.Add("onclick", "return confirm('Are you sure that you want to delete this Link?')");
                }
                else
                {
                    objDelete.Attributes.Add("onclick", "return confirm('Are you sure that you want to delete this Document?')");
                }
            }
        }

        protected void gvHelpfulDocs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvHelpfulDocs.PageIndex = e.NewPageIndex;
            SearchHelpfulDocs();
        }

        protected void gvHelpfulDocs_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Id = (int)gvHelpfulDocs.DataKeys[e.RowIndex].Value;
            Presenter.DeleteHelpfulDoc(Id);
            SearchHelpfulDocs();
        }

        protected void gvHelpfulDocs_Sorting(object sender, GridViewSortEventArgs e)
        {
            hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);
            SearchHelpfulDocs();
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            SearchHelpfulDocs();
        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            int Id = ((LinkButton)sender).CommandArgument.ToInt();
            int actionType = 1;
            Presenter.OpenDocument(Id, actionType);
        }

        protected void gvHelpfulDocs_DataBound(object sender, EventArgs e)
        {
            if (CanUploadFiles)
            {
                gvHelpfulDocs.Columns[8].Visible = true;
                gvHelpfulDocs.Columns[9].Visible = true;
            }
        }

        private void SetControlTexts()
        {
            if (IsLink)
            {
                lblBreadCrumb.Text = "View > View Links";
                lblSubTitle.Text = "Use this page to view or edit or delete a link";
                gvHelpfulDocs.Columns[3].Visible = false;
                gvHelpfulDocs.Columns[5].HeaderText = "Entered By";
                gvHelpfulDocs.Columns[6].HeaderText = "Enter Date";
                gvHelpfulDocs.Columns[10].Visible = false;

                if (!CanUploadFiles)
                {
                    lblSubTitle.Text = "Use this page to view the links";
                }

                hdnIsLink.Value = "1";
            }
            else
            {
                lblBreadCrumb.Text = "View > View Documents";
                lblSubTitle.Text = "Use this page to view or edit or delete a document";
                gvHelpfulDocs.Columns[4].Visible = false;
                gvHelpfulDocs.Columns[5].HeaderText = "Uploaded By";
                gvHelpfulDocs.Columns[6].HeaderText = "Uploaded Date";
                if (!CanUploadFiles)
                {
                    lblSubTitle.Text = "Use this page to view the documents";
                }

                hdnIsLink.Value = "0";
            }
        }
    }
}