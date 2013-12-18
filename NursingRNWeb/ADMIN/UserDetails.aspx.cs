using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;
using NursingRNWeb.AppCode.Report_Ds;

namespace NursingRNWeb.ADMIN
{
    public partial class UserDetails : PageBase<IuserDetailsView, UserDetailsPresenter>,IuserDetailsView
    {
        public string SearchText { get; set; }

        public int ProgramofStudyId { get; set; }

        private const string Redirect = "5;URL=AdminHome.aspx"; 

         public void ShowUserResults(IEnumerable<NursingLibrary.Entity.UserDetails> userDetails, SortInfo sortMetaData)
        {
            if (userDetails.Count() > 0)
            {
                lblM.Visible = false;
            }
            else
            {
                lblM.Visible = true;
            }

            gridUser.DataSource = KTPSort.Sort<NursingLibrary.Entity.UserDetails>(userDetails, sortMetaData);
            gridUser.DataBind();
        }

        public override void PreInitialize()
        {
            Presenter.PreInitialize(ViewMode.View);           
        }

        public void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudies)
        {
            ddlProgramOfStudy.NotSelectedText = "Selection Required";
            ddlProgramOfStudy.DataSource = programofStudies;
            ddlProgramOfStudy.DataTextField = "ProgramofStudyName";
            ddlProgramOfStudy.DataValueField = "ProgramofStudyId";
            ddlProgramOfStudy.DataBind();
            ddlProgramOfStudy.Items.Insert(3, new ListItem("None", "3"));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)
            {
                if (Presenter.CurrentContext.UserType == UserType.SuperAdmin)
                {
                    trdata.Visible = true;
                    Presenter.ShowProgramofStudyDetails();
                }
                else
                {
                    trunautor.Visible = true;
                    Response.AppendHeader("REFRESH", Redirect);
                   
                }
            }
        }

        protected void searchButton_Click(object sender, ImageClickEventArgs e)
        {
            SearchUser();
        }

        protected void gridUserDetails_Sorting(object sender, GridViewSortEventArgs e)
        {
            hdnGridConfig.Value = SortHelper.Compare(e.SortExpression, e.SortDirection.ToString(), hdnGridConfig.Value);
            SearchUser();
        }

        protected void gridInstitutions_PageIndexChanged(Object sender, EventArgs e)
        {
           gridUser.Visible = true;
        }

        protected void gridUserDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SearchUser();
            gridUser.PageIndex = e.NewPageIndex;
            gridUser.DataBind();
        }

        private void SearchUser()
        {
            SearchText = txtSearch.Text.Trim();
            ProgramofStudyId = ddlProgramOfStudy.SelectedIndex;
            #region Trace Information
            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Userdetails Page")
                .Add("Search Keyword", txtSearch.Text)
                .Write();
            #endregion
            if (rfvProgramOfStudy.IsValid)
            {
                Presenter.SearchUserDetails(hdnGridConfig.Value, statusRadioButton.SelectedValue);
            }         
        }     
    }
}