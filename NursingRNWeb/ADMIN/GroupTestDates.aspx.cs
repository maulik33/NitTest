using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NursingLibrary;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class ADMIN_GroupTestDates : PageBase<IGroupView, GroupPresenter>, IGroupView
{
    private bool _canEditTestDates;

    public string Name { get; set; }

    public int GroupId { get; set; }

    public int CohortId { get; set; }

    public string InstitutionId { get; set; }

    public string CohortIds
    {
        get { throw new NotImplementedException(); }
    }

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.List);
    }

    public void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudies)
    {
        throw new ArgumentException();
    }
    #region IGroupView Members

    public void ShowGroup(Group group)
    {
    }

    #region Not used

    public int ProgramofStudyId
    {
        get { throw new NotImplementedException(); }
        set { throw new NotImplementedException(); }
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

    public void PopulateInstitution(IEnumerable<Institution> groups)
    {
        throw new NotImplementedException();
    }

    public void PopulateCohort(IEnumerable<Cohort> Cohort)
    {
        throw new NotImplementedException();
    }

    public void RefreshPage(Group group, UserAction action, string title, string subTitle,
        bool hasDeletePermission, bool hasAddPermission)
    {
        _canEditTestDates = hasAddPermission;
        if (!_canEditTestDates)
        {
            btnAssign.Visible = false;
            btnAssign.Enabled = false;
        }
    }

    public void PopulateProgramForTest(Program program)
    {
        if (program != null)
        {
            HiddenProgramId.Value = program.ProgramId.ToString();
            lblPN.Text = program.ProgramName;
            lblDescription.Text = program.Description;
        }
    }

    public void PopulateGroupTest(GroupTestDates testDetails)
    {
        HiddenCohortId.Value = testDetails.Cohort.CohortId.ToString();
        HiddenGroupId.Value = testDetails.Group.GroupId.ToString();
        lblIN.Text = testDetails.Institution.InstitutionNameWithProgOfStudy;
        lblCName.Text = testDetails.Cohort.CohortName;
        lblGroup.Text = testDetails.Group.GroupName;
        lblCED.Text = testDetails.Cohort.CohortEndDate.Format();
        lblCSD.Text = testDetails.Cohort.CohortStartDate.Format();
    }

    public void PopulateGroupTests(IEnumerable<GroupTestDates> testDetails)
    {
        gvTests.DataSource = testDetails;
        gvTests.DataBind();
    }

    public void ExportGroups(IEnumerable<Group> groups, ReportAction printActions)
    {
        throw new NotImplementedException();
    }
    #endregion

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        lblTestAssignMsg.Text = string.Empty;
        lblM.Visible = false;
        if (!IsPostBack)
        {
            Presenter.ShowTestDatesForGroups();
        }
    }

    protected void gvTests_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int id = Convert.ToInt32(gvTests.DataKeys[e.Row.RowIndex].Values["TestId"].ToString());
            string Type = gvTests.DataKeys[e.Row.RowIndex].Values["Type"].ToString();

            DropDownList ddTime_S = (DropDownList)e.Row.FindControl("ddTime_S");
            DropDownList ddMin_S = (DropDownList)e.Row.FindControl("ddMin_S");
            DropDownList ddAMPM_S = (DropDownList)e.Row.FindControl("ddAMPM_S");

            System.Web.UI.HtmlControls.HtmlAnchor LnkCalendar = (HtmlAnchor)e.Row.FindControl("LnkCalendar");
            System.Web.UI.HtmlControls.HtmlInputText tbSD = (HtmlInputText)e.Row.FindControl("tbSD");
            LnkCalendar.Attributes.Add("href", "Javascript:pickDate('" + tbSD.Name + "')");

            DropDownList ddTime_E = (DropDownList)e.Row.FindControl("ddTime_E");
            DropDownList ddMin_E = (DropDownList)e.Row.FindControl("ddMin_E");
            DropDownList ddAMPM_E = (DropDownList)e.Row.FindControl("ddAMPM_E");

            System.Web.UI.HtmlControls.HtmlAnchor LnkCalendar2 = (HtmlAnchor)e.Row.FindControl("LnkCalendar2");
            System.Web.UI.HtmlControls.HtmlInputText tbED = (HtmlInputText)e.Row.FindControl("tbED");
            LnkCalendar2.Attributes.Add("href", "Javascript:pickDate('" + tbED.Name + "')");

            if (!_canEditTestDates)
            {
                LnkCalendar.Visible = false;
                LnkCalendar2.Visible = false;
                e.Row.Cells[3].Visible = true;
                e.Row.Cells[6].Visible = false;
            }

            if (tbSD != null && tbED != null)
            {
                var _startDate = ((HiddenField)e.Row.FindControl("TestStartDate")).Value;
                var _endDate = ((HiddenField)e.Row.FindControl("TestEndDate")).Value;
                if (_startDate.Length >= 10)
                {
                    tbSD.Value = ControlHelper.FormatDate(_startDate);
                }
                else
                {
                    tbSD.Value = string.Empty;
                }

                if (_endDate.Length >= 10)
                {
                    tbED.Value = ControlHelper.FormatDate(_endDate);
                }
                else
                {
                    tbED.Value = string.Empty;
                }

                DateTime _date;
                string _dayOrEvening;
                if (_startDate.Trim() != string.Empty)
                {
                    _date = Convert.ToDateTime(_startDate);
                    ddTime_S.SelectedValue = ControlHelper.TranslateHours(Convert.ToString(_date.Hour));
                    ddMin_S.SelectedValue = _date.Minute.ToString("00");
                    _dayOrEvening = StringFunctions.Right(_startDate.Trim(), 2);
                    if (_dayOrEvening == "AM" || _dayOrEvening == "PM")
                    {
                        ddAMPM_S.SelectedValue = _dayOrEvening;
                    }
                }

                if (_endDate.Trim() != string.Empty)
                {
                    _date = Convert.ToDateTime(_endDate);
                    ddTime_E.SelectedValue = ControlHelper.TranslateHours(Convert.ToString(_date.Hour));
                    ddMin_E.SelectedValue = _date.Minute.ToString("00");
                    _dayOrEvening = StringFunctions.Right(_endDate.Trim(), 2);
                    if (_dayOrEvening == "AM" || _dayOrEvening == "PM")
                    {
                        ddAMPM_E.SelectedValue = _dayOrEvening;
                    }
                }
            }
        }
    }

    protected void gvTests_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = gvTests.Rows[index];
        int id;

        if (e.CommandName == "SetAll")
        {
            System.Web.UI.HtmlControls.HtmlInputText tbSD = (HtmlInputText)row.FindControl("tbSD");
            System.Web.UI.HtmlControls.HtmlInputText tbED = (HtmlInputText)row.FindControl("tbED");

            DropDownList ddTime_E = (DropDownList)row.FindControl("ddTime_E");
            DropDownList ddMin_E = (DropDownList)row.FindControl("ddMin_E");
            DropDownList ddAMPM_E = (DropDownList)row.FindControl("ddAMPM_E");

            DropDownList ddTime_S = (DropDownList)row.FindControl("ddTime_S");
            DropDownList ddMin_S = (DropDownList)row.FindControl("ddMin_S");
            DropDownList ddAMPM_S = (DropDownList)row.FindControl("ddAMPM_S");

            foreach (GridViewRow r in gvTests.Rows)
            {
                CheckBox cb = (CheckBox)r.Cells[4].FindControl("CheckBox1");
                if (cb.Checked)
                {
                    ((HtmlInputText)r.FindControl("tbSD")).Value = tbSD.Value;
                    ((HtmlInputText)r.FindControl("tbED")).Value = tbED.Value;
                    ((DropDownList)row.FindControl("ddTime_E")).SelectedIndex = ddTime_E.SelectedIndex;
                    ((DropDownList)row.FindControl("ddMin_E")).SelectedIndex = ddMin_E.SelectedIndex;
                    ((DropDownList)row.FindControl("ddAMPM_E")).SelectedIndex = ddAMPM_E.SelectedIndex;
                    ((DropDownList)row.FindControl("ddTime_S")).SelectedIndex = ddTime_S.SelectedIndex;
                    ((DropDownList)row.FindControl("ddMin_S")).SelectedIndex = ddMin_S.SelectedIndex;
                    ((DropDownList)row.FindControl("ddAMPM_S")).SelectedIndex = ddAMPM_S.SelectedIndex;
                }
            }
        }
        else if (e.CommandName == "Save")
        {
            var studentMessage = string.Empty;
            List<GroupTestDates> lstGroupTestDate = new List<GroupTestDates>();
            id = Convert.ToInt32(gvTests.DataKeys[row.RowIndex].Values["TestId"].ToString());
            int type = Convert.ToInt32(gvTests.DataKeys[row.RowIndex].Values["Type"].ToString());

            System.Web.UI.HtmlControls.HtmlInputText tbSD = (HtmlInputText)row.FindControl("tbSD");
            System.Web.UI.HtmlControls.HtmlInputText tbED = (HtmlInputText)row.FindControl("tbED");

            DropDownList ddTime_E = (DropDownList)row.FindControl("ddTime_E");
            DropDownList ddMin_E = (DropDownList)row.FindControl("ddMin_E");
            DropDownList ddAMPM_E = (DropDownList)row.FindControl("ddAMPM_E");

            DropDownList ddTime_S = (DropDownList)row.FindControl("ddTime_S");
            DropDownList ddMin_S = (DropDownList)row.FindControl("ddMin_S");
            DropDownList ddAMPM_S = (DropDownList)row.FindControl("ddAMPM_S"); 
            var productType = row.Cells[1].Text;

            var objDate = new GroupTestDates();
            objDate.TestStartDate = tbSD.Value.Trim();
            objDate.TestEndDate = tbED.Value.Trim();
            objDate.TestStartHour = ddTime_S.SelectedValue.ToInt();
            objDate.TestEndHour = ddTime_E.SelectedValue.ToInt();
            objDate.TestStartTime = ddAMPM_S.SelectedValue;
            objDate.TestEndTime = ddAMPM_E.SelectedValue;
            objDate.TestEndMin = ddMin_E.SelectedValue.ToInt();
            objDate.TestStartMin = ddMin_S.SelectedValue.ToInt();
            objDate.Type = type;
            objDate.Product = new Product() { ProductId = id, ProductType = productType };
            objDate.Cohort = new Cohort() { CohortId = HiddenCohortId.Value.ToInt() };
            objDate.Group = new Group() { GroupId = HiddenGroupId.Value.ToInt() };
            objDate.TestName = gvTests.Rows[row.RowIndex].Cells[0].Text.Trim();
            lstGroupTestDate.Add(objDate);
            lblM.Visible = true;
            #region Trace Information
            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Assign Group Test Dates.")
                .Add("Group Id", HiddenGroupId.Value)
                .Add("Cohort Id", HiddenCohortId.Value)
                .Add("Test Id", id.ToString())
                .Add("Program Name", lblPN.Text)
                .Add("Institution Name", lblIN.Text)
                .Write();
            #endregion
            lblM.Text = Presenter.AssignTestDateToGroup(lstGroupTestDate, ref studentMessage);
            if (Presenter.CurrentContext.UserType.Equals(UserType.SuperAdmin))
            {
                if (studentMessage.Length > 0)
                {
                    lblTestAssignMsg.Text =
                        "The following student have either taken the exam or have tests scheduled in the future. Please consult faculty for next steps.<br/><br/> ";
                    lblTestAssignMsg.Text += studentMessage;
                }
            }
        }
    }

    protected void btnAssign_Click(object sender, ImageClickEventArgs e)
    {
        List<GroupTestDates> lstGroupTestDate = new List<GroupTestDates>();
        var objDate = new GroupTestDates();
        var studentMessage = string.Empty;
        foreach (GridViewRow row in gvTests.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                int id = Convert.ToInt32(gvTests.DataKeys[row.RowIndex].Values["TestId"].ToString());
                int type = Convert.ToInt32(gvTests.DataKeys[row.RowIndex].Values["Type"].ToString());

                System.Web.UI.HtmlControls.HtmlInputText tbSD = (HtmlInputText)row.FindControl("tbSD");
                System.Web.UI.HtmlControls.HtmlInputText tbED = (HtmlInputText)row.FindControl("tbED");

                DropDownList ddTime_E = (DropDownList)row.FindControl("ddTime_E");
                DropDownList ddMin_E = (DropDownList)row.FindControl("ddMin_E");
                DropDownList ddAMPM_E = (DropDownList)row.FindControl("ddAMPM_E");

                DropDownList ddTime_S = (DropDownList)row.FindControl("ddTime_S");
                DropDownList ddMin_S = (DropDownList)row.FindControl("ddMin_S");
                DropDownList ddAMPM_S = (DropDownList)row.FindControl("ddAMPM_S");
                var productType = row.Cells[1].Text;
                objDate = new GroupTestDates();
                objDate.TestStartDate = tbSD.Value.Trim();
                objDate.TestEndDate = tbED.Value.Trim();
                objDate.TestStartHour = ddTime_S.SelectedValue.ToInt();
                objDate.TestEndHour = ddTime_E.SelectedValue.ToInt();
                objDate.TestStartTime = ddAMPM_S.SelectedValue;
                objDate.TestEndTime = ddAMPM_E.SelectedValue;
                objDate.TestStartMin = ddMin_S.SelectedValue.ToInt();
                objDate.TestEndMin = ddMin_E.SelectedValue.ToInt();
                objDate.Type = type;
                objDate.Product = new Product() { ProductId = id, ProductType = productType };
                objDate.Cohort = new Cohort() { CohortId = HiddenCohortId.Value.ToInt() };
                objDate.Group = new Group() { GroupId = HiddenGroupId.Value.ToInt() };
                objDate.Test = new Test() { TestId = id };
                objDate.TestName = gvTests.Rows[row.RowIndex].Cells[0].Text.Trim();
                lstGroupTestDate.Add(objDate);
            }
        }
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Assign Group Test Dates.")
            .Add("Group Id ", HiddenGroupId.Value)
            .Add("Cohort Id ", HiddenCohortId.Value)
            .Add("Program Name ", lblPN.Text)
            .Add("Institution Name ", lblIN.Text)
            .Write();
        #endregion
        lblM.Visible = true;
        lblM.Text = Presenter.AssignTestDateToGroup(lstGroupTestDate, ref studentMessage);
        if (Presenter.CurrentContext.UserType.Equals(UserType.SuperAdmin))
        {
            if (studentMessage.Length > 0)
            {
                lblTestAssignMsg.Text =
                    "The following student have either taken the exam or have tests scheduled in the future. Please consult faculty for next steps.<br/><br/> ";
                lblTestAssignMsg.Text += studentMessage;
            }
        }
    }

    protected void seabtn_Click(object sender, ImageClickEventArgs e)
    {
        lblM.Text = string.Empty;
        lblM.Visible = false;
        CohortId = HiddenCohortId.Value.ToInt();
        GroupId = HiddenGroupId.Value.ToInt();
        Presenter.ShowTests(HiddenProgramId.Value.ToInt(), TextBox1.Text.Trim());
    }
}
