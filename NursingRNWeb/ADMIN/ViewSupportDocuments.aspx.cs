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

namespace NursingRNWeb.ADMIN
{
    public partial class ViewSupportDocuments : PageBase<ISupportDocumentView, SupportDocumentPresenter>, ISupportDocumentView
    {
        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public int PageIndex { get; set; }

        public int Id { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Image1.Attributes.Add("onclick", "window.open('popupC.aspx?textbox=" + txtSD.ClientID + "','cal','width=250,height=225,left=270,top=180')");
            Image2.Attributes.Add("onclick", "window.open('popupC.aspx?textbox=" + txtED.ClientID + "','cal','width=250,height=225,left=270,top=180')");
            Image1.Style.Add("cursor", "pointer");
            Image2.Style.Add("cursor", "pointer");
        }

        public override void PreInitialize()
        {
            Presenter.PreInitialize(ViewMode.List);
        }

        #region ISupportDocumentView Methods

        public void SearchSupportDocs()
        {
            string SearchKeyWord = TxtKeyword.Text;
            string SearchName = TxtUserName.Text;
            StartDate = txtSD.Text;
            EndDate = txtED.Text;

            Presenter.SearchSupportDocs(SearchKeyWord, SearchName, StartDate,EndDate, hdnGridConfig.Value);
        }

        public void DisplaySearchResult(IEnumerable<SupportDocument> docs, SortInfo sortMetaData)
        {
            IEnumerable<SupportDocument> docList = KTPSort.Sort<SupportDocument>(docs, sortMetaData);
            gvSupportDocs.DataSource = docList;
            gvSupportDocs.DataBind();
        }

        #endregion

        protected void gvSupportDocs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.WebControls.WebControl l = ((WebControl)e.Row.Cells[7].Controls[0]);
                l.Attributes.Add("onclick", "return confirm('Are you sure that you want to delete this Test? Click OK to remove the test from all programs that contain it.')");
            }
        }

        protected void gvSupportDocs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSupportDocs.PageIndex = e.NewPageIndex;
            SearchSupportDocs();
        }

        protected void gvSupportDocs_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Id = (int)gvSupportDocs.DataKeys[e.RowIndex].Value;
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
        public void RenderSupportDocuments(SupportDocument supportDocument)
        { }
       
    }
}