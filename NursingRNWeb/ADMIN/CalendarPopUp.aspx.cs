using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using NursingLibrary.Common;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

namespace NursingRNWeb.ADMIN
{
    public partial class CalendarPopUp : PageBase<IGroupView, GroupPresenter>, IGroupView
    {
        public string Name { get; set; }

        public int GroupId { get; set; }

        public int CohortId { get; set; }

        public string InstitutionId { get; set; }

        public int ProgramofStudyId
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string CohortIds
        {
            get { throw new NotImplementedException(); }
        }

        public void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudies)
        {
            throw new NotImplementedException(); 
        }

        public override void PreInitialize()
        {
            Presenter.PreInitialize(ViewMode.List);
        }

        public void ShowGroupResults(IEnumerable<Group> groups, SortInfo sortMetaData)
        {
            throw new NotImplementedException();
        }

        public void SaveGroup(int newGroupId)
        {
            throw new NotImplementedException();
        }

        public void DeleteGroup(int groupId)
        {
            throw new NotImplementedException();
        }

        public void PopulateInstitution(IEnumerable<Institution> institutions)
        {
            throw new NotImplementedException();
        }

        public void PopulateCohort(IEnumerable<Cohort> cohort)
        {
            throw new NotImplementedException();
        }

        public void RefreshPage(Group group, UserAction action, string title, string subTitle, bool hasDeletePermission, bool hasAddPermission)
        {
            throw new NotImplementedException();
        }

        public void ShowGroup(Group group)
        {
            throw new NotImplementedException();
        }

        public void PopulateGroupTest(GroupTestDates testDetail)
        {
            throw new NotImplementedException();
        }

        public void PopulateGroupTests(IEnumerable<GroupTestDates> testDetails)
        {
            throw new NotImplementedException();
        }

        public void PopulateProgramForTest(Program program)
        {
            throw new NotImplementedException();
        }

        public void ExportGroups(IEnumerable<Group> groups, ReportAction printActions)
        {
            throw new NotImplementedException();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region Trace Information
                TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Calendar Page.");
                #endregion
            }
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            StringBuilder sbScript = new StringBuilder();
            sbScript.Append("<script language='javascript'>");
            sbScript.Append(Environment.NewLine);
            sbScript.Append("window.opener.document.forms[0].elements['");
            sbScript.Append(Request.QueryString["src"]);
            sbScript.Append("'].value = '");
            sbScript.Append(Calendar1.SelectedDate.ToString("MM/dd/yyyy"));
            sbScript.Append("';");
            sbScript.Append(Environment.NewLine);
            sbScript.Append("window.close();");
            sbScript.Append(Environment.NewLine);
            sbScript.Append("</script>");
            ClientScript.RegisterStartupScript(Page.GetType(), "CloseWindow", sbScript.ToString());
        }
    }
}