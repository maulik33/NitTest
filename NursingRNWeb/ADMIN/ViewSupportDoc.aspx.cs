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

namespace NursingRNWeb.ADMIN
{
    public partial class ViewSupportDoc : PageBase<ISupportDocumentView, SupportDocumentPresenter>, ISupportDocumentView
    {
        public int Id { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Presenter.ViewSupportDocument();
        }
        public override void PreInitialize()
        {
            Presenter.PreInitialize(ViewMode.View);
        }
        public void DisplaySearchResult(IEnumerable<SupportDocument> docs, SortInfo sortInfo)
        { }
        public void RenderSupportDocuments(SupportDocument supportDocument)
        { }
    }
}