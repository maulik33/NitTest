using System;
using System.Collections.Generic;
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

public partial class ADMIN_CohortTestDates : PageBase<ICohortView, CohortPresenter>, ICohortView
{
    private bool _canEditTestDates = false;

    public bool HasAddPermission { get; set; }

    public string SearchText { get; set; }

    public int GroupId { get; set; }

    public int CohortId { get; set; }

    public int ProductId { get; set; }

    public string InstitutionId { get; set; }

    public string Name { get; set; }

    public string StartDate { get; set; }

    public string EndDate { get; set; }

    public string Description { get; set; }

    public int CohortStatus { get; set; }

    public bool IsValidDate { get; set; }

    public int ProgramId { get; set; }

    public int TestId { get; set; }

    public string State { get; set; }

    public string Type { get; set; }

    public string ErrorMessage { get; set; }

    public bool IsInValidCohort { get; set; }

    public int ProgramofStudyId
    {
        get { throw new NotImplementedException(); }
        set { throw new NotImplementedException(); }
    }

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.View);
    }

    #region ICohortView Members

    public void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudy)
    {
        throw new NotImplementedException();
    }

    public void ShowCohortResults(IEnumerable<Cohort> groups)
    {
        throw new NotImplementedException();
    }

    public void SaveCohort(int newGroupId)
    {
        throw new NotImplementedException();
    }

    public void DeleteCohort()
    {
        throw new NotImplementedException();
    }

    public void PopulatePrograms(IEnumerable<Program> programs)
    {
    }

    public void PopulateInstitutions(IEnumerable<Institution> institutions)
    {
    }

    public void PopulateCohorts(IEnumerable<Cohort> Cohort, SortInfo sortMetaData)
    {
        throw new NotImplementedException();
    }

    public void PopulateGroups(IEnumerable<Group> groups)
    {
        throw new NotImplementedException();
    }

    public void RefreshPage(Cohort group, UserAction action, string title, string subTitle, bool hasDeletePermission, bool hasAddPermission, bool hasAccessDatesEdit, bool hasEditPremission)
    {
        if (hasAccessDatesEdit)
        {
            _canEditTestDates = true;
            btnAssign.Visible = true;
            btnAssign.Enabled = true;
        }
        else
        {
            btnAssign.Visible = false;
            btnAssign.Enabled = false;
        }
    }

    public void PopulateProducts(IEnumerable<Product> products)
    {
        throw new NotImplementedException();
    }

    public void PopulateStudents(IEnumerable<Student> students, SortInfo sortMetaData)
    {
        throw new NotImplementedException();
    }

    public void ShowCohort(Cohort cohort)
    {
    }

    public void PopulateCohortTest(CohortTestDates testDetails)
    {
        CohortId = testDetails.Cohort.CohortId;
        HiddenCohortId.Value = testDetails.Cohort.CohortId.ToString();
        lblCName.Text = testDetails.Cohort.CohortName;
        lblIN.Text = testDetails.Institution.InstitutionNameWithProgOfStudy;
        if (!String.IsNullOrEmpty(testDetails.Cohort.CohortStartDate.ToString()))
        {
            lblCSD.Text = testDetails.Cohort.CohortStartDate.Format();
        }

        if (!String.IsNullOrEmpty(testDetails.Cohort.CohortEndDate.ToString()))
        {
            lblCED.Text = testDetails.Cohort.CohortEndDate.Format();
        }
    }

    public void PopulateTests(IEnumerable<Test> tests)
    {
        throw new NotImplementedException();
    }

    public void PopulateCohortTests(IEnumerable<CohortTestDates> testDetails)
    {
        gvTests.DataSource = testDetails;
        gvTests.DataBind();
    }

    public void PopulateProgramForTest(Program program)
    {
        if (program != null)
        {
            lblPN.Text = program.ProgramName;
            lblD.Text = program.Description;
        }
    }

    public void ShowProgramResults(IEnumerable<Program> programs, SortInfo sortMetaData)
    {
    }

    public void ExportCohortList(IEnumerable<Cohort> reportData, ReportAction printActions)
    {
        throw new NotImplementedException();
    }

    public void PopulateStates(IEnumerable<State> states)
    {
        throw new NotImplementedException();
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        lblTestAssignMsg.Text = string.Empty;
        lblM.Visible = false;
        if (!IsPostBack)
        {
            SearchText = string.Empty;
            Presenter.ShowTestForCohort();
            HiddenProgramId.Value = ProgramId.ToString();
        }
    }

    protected void gvTests_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int id = Convert.ToInt32(gvTests.DataKeys[e.Row.RowIndex].Values["TestId"].ToString());
            string Type = gvTests.DataKeys[e.Row.RowIndex].Values["Type"].ToString();

            System.Web.UI.HtmlControls.HtmlAnchor LnkCalendar = (HtmlAnchor)e.Row.FindControl("LnkCalendar");
            System.Web.UI.HtmlControls.HtmlInputText tbSD = (HtmlInputText)e.Row.FindControl("tbSD");
            LnkCalendar.Attributes.Add("href", "Javascript:pickDate('" + tbSD.Name + "')");

            DropDownList ddTime_S = (DropDownList)e.Row.FindControl("ddTime_S");
            DropDownList ddMin_S = (DropDownList)e.Row.FindControl("ddMin_S");
            DropDownList ddAMPM_S = (DropDownList)e.Row.FindControl("ddAMPM_S");

            System.Web.UI.HtmlControls.HtmlAnchor LnkCalendar2 = (HtmlAnchor)e.Row.FindControl("LnkCalendar2");
            System.Web.UI.HtmlControls.HtmlInputText tbED = (HtmlInputText)e.Row.FindControl("tbED");
            LnkCalendar2.Attributes.Add("href", "Javascript:pickDate('" + tbED.Name + "')");

            DropDownList ddTime_E = (DropDownList)e.Row.FindControl("ddTime_E");
            DropDownList ddMin_E = (DropDownList)e.Row.FindControl("ddMin_E");
            DropDownList ddAMPM_E = (DropDownList)e.Row.FindControl("ddAMPM_E");

            if (_canEditTestDates)
            {
                LnkCalendar.Visible = true;
                LnkCalendar2.Visible = true;
                e.Row.Cells[3].Visible = true;
            }
            else
            {
                LnkCalendar.Visible = false;
                LnkCalendar2.Visible = false;
                e.Row.Cells[3].Visible = true;
                e.Row.Cells[6].Visible = false;
            }

            var startDate = ((HiddenField)e.Row.FindControl("TestStartDate")).Value;
            var endDate = ((HiddenField)e.Row.FindControl("TestEndDate")).Value;

            if (tbSD != null && tbED != null)
            {
                DateTime _date;
                string _dayOrEvening;
                if (startDate.Length >= 10)
                {
                    tbSD.Value = ControlHelper.FormatDate(startDate);
                }
                else
                {
                    tbSD.Value = string.Empty;
                }

                if (endDate.Length >= 10)
                {
                    tbED.Value = ControlHelper.FormatDate(endDate);
                }
                else
                {
                    tbED.Value = string.Empty;
                }

                if (startDate.Trim() != string.Empty && !String.Equals(startDate, "&nbsp;"))
                {
                    _date = Convert.ToDateTime(startDate);
                    ddTime_S.SelectedValue = ControlHelper.TranslateHours(Convert.ToString(_date.Hour));
                    ddMin_S.SelectedValue = _date.Minute.ToString("00");
                    _dayOrEvening = StringFunctions.Right(startDate, 2);
                    if (_dayOrEvening == "AM" || _dayOrEvening == "PM")
                    {
                        ddAMPM_S.SelectedValue = _dayOrEvening;
                    }
                }

                if (endDate.Trim() != string.Empty)
                {
                    _date = Convert.ToDateTime(endDate);
                    ddTime_E.SelectedValue = ControlHelper.TranslateHours(Convert.ToString(_date.Hour));
                    ddMin_E.SelectedValue = _date.Minute.ToString("00");
                    _dayOrEvening = StringFunctions.Right(endDate.Trim(), 2);
                    if (_dayOrEvening == "AM" || _dayOrEvening == "PM")
                    {
                        ddAMPM_E.SelectedValue = _dayOrEvening;
                    }

                    tbED.Visible = true;
                    ddAMPM_E.Visible = true;
                }
            }
        }
    }

    protected void gvTests_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = gvTests.Rows[index];
        int id;
        var studentMessage = string.Empty;

        if (e.CommandName == "SetAll")
        {
            System.Web.UI.HtmlControls.HtmlInputText tbSD = (HtmlInputText)row.FindControl("tbSD");
            System.Web.UI.HtmlControls.HtmlInputText tbED = (HtmlInputText)row.FindControl("tbED");

            DropDownList ddTime_E = (DropDownList)row.FindControl("ddTime_E");
            DropDownList ddAMPM_E = (DropDownList)row.FindControl("ddAMPM_E");
            DropDownList ddMin_E = (DropDownList)row.FindControl("ddMin_E");

            DropDownList ddTime_S = (DropDownList)row.FindControl("ddTime_S");
            DropDownList ddAMPM_S = (DropDownList)row.FindControl("ddAMPM_S");
            DropDownList ddMin_S = (DropDownList)row.FindControl("ddMin_S");

            foreach (GridViewRow r in gvTests.Rows)
            {
                CheckBox cb = (CheckBox)r.Cells[4].FindControl("CheckBox1");
                if (cb.Checked)
                {
                    ((HtmlInputText)r.FindControl("tbSD")).Value = tbSD.Value;
                    ((HtmlInputText)r.FindControl("tbED")).Value = tbED.Value;
                    ((DropDownList)row.FindControl("ddTime_E")).SelectedIndex = ddTime_E.SelectedIndex;
                    ((DropDownList)row.FindControl("ddAMPM_E")).SelectedIndex = ddAMPM_E.SelectedIndex;
                    ((DropDownList)row.FindControl("ddMin_E")).SelectedIndex = ddMin_E.SelectedIndex;

                    ((DropDownList)row.FindControl("ddTime_S")).SelectedIndex = ddTime_S.SelectedIndex;
                    ((DropDownList)row.FindControl("ddAMPM_S")).SelectedIndex = ddAMPM_S.SelectedIndex;
                    ((DropDownList)row.FindControl("ddMin_S")).SelectedIndex = ddMin_S.SelectedIndex;
                }
            }
        }
        else if (e.CommandName == "Save")
        {
            List<CohortTestDates> lstCohortTestDate = new List<CohortTestDates>();
            id = Convert.ToInt32(gvTests.DataKeys[row.RowIndex].Values["TestId"].ToString());
            int type = Convert.ToInt32(gvTests.DataKeys[row.RowIndex].Values["Type"].ToString());
            System.Web.UI.HtmlControls.HtmlInputText tbSD = (HtmlInputText)row.FindControl("tbSD");
            System.Web.UI.HtmlControls.HtmlInputText tbED = (HtmlInputText)row.FindControl("tbED");

            DropDownList ddTime_E = (DropDownList)row.FindControl("ddTime_E");
            DropDownList ddAMPM_E = (DropDownList)row.FindControl("ddAMPM_E");
            DropDownList ddMin_E = (DropDownList)row.FindControl("ddMin_E");

            DropDownList ddTime_S = (DropDownList)row.FindControl("ddTime_S");
            DropDownList ddAMPM_S = (DropDownList)row.FindControl("ddAMPM_S");
            DropDownList ddMin_S = (DropDownList)row.FindControl("ddMin_S");

            Label lblProductType = (Label) row.FindControl("TestType");
            var productType = lblProductType.Text.Trim();
            var objDate = new CohortTestDates();
            objDate.TestStartDate = tbSD.Value.Trim();
            objDate.TestEndDate = tbED.Value.Trim();
            objDate.TestStartHour = ddTime_S.SelectedValue.ToInt();
            objDate.TestEndHour = ddTime_E.SelectedValue.ToInt();
            objDate.TestStartTime = ddAMPM_S.SelectedValue;
            objDate.TestEndTime = ddAMPM_E.SelectedValue;
            objDate.TestEndMin = ddMin_E.SelectedValue.ToInt();
            objDate.TestStartMin = ddMin_S.SelectedValue.ToInt();
            objDate.Type = type;
            objDate.Product = new Product() {ProductId = id, ProductType = productType};
            objDate.Cohort = new Cohort() { CohortId = HiddenCohortId.Value.ToInt() };
            objDate.TestName =  gvTests.Rows[row.RowIndex].Cells[0].Text.Trim();
            lstCohortTestDate.Add(objDate);
            lblM.Visible = true;
            #region Trace Information
            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Assign Cohort Test Dates.")
                .Add("Cohort Id ", HiddenCohortId.Value)
                .Add("Test Id ", id.ToString())
                .Add("Program Name ", lblPN.Text)
                .Add("Institution Name ", lblIN.Text)
                .Write();
            #endregion
            lblM.Text = Presenter.AssignTestDateToCohort(lstCohortTestDate, ref studentMessage);
            if (Presenter.CurrentContext.UserType == UserType.SuperAdmin)
            {
                if (studentMessage.Length > 0)
                {
                    lblTestAssignMsg.Text =
                        "The following group/student have either taken the exam or have tests scheduled in the future. Please consult faculty for next steps.<br/><br/>";
                    lblTestAssignMsg.Text += studentMessage;
                }
            }
        }
    }

    protected void btnAssign_Click(object sender, ImageClickEventArgs e)
    {
        List<CohortTestDates> lstCohortTestDate = new List<CohortTestDates>();
        var objDate = new CohortTestDates();
        var studentMessage = string.Empty;
        foreach (GridViewRow row in gvTests.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                int id = gvTests.DataKeys[row.RowIndex].Values["TestId"].ToInt();
                int type = gvTests.DataKeys[row.RowIndex].Values["Type"].ToInt();

                System.Web.UI.HtmlControls.HtmlInputText tbSD = (HtmlInputText)row.FindControl("tbSD");
                System.Web.UI.HtmlControls.HtmlInputText tbED = (HtmlInputText)row.FindControl("tbED");

                DropDownList ddTime_E = (DropDownList)row.FindControl("ddTime_E");
                DropDownList ddMin_E = (DropDownList)row.FindControl("ddMin_E");
                DropDownList ddAMPM_E = (DropDownList)row.FindControl("ddAMPM_E");

                DropDownList ddTime_S = (DropDownList)row.FindControl("ddTime_S");
                DropDownList ddMin_S = (DropDownList)row.FindControl("ddMin_S");
                DropDownList ddAMPM_S = (DropDownList)row.FindControl("ddAMPM_S");
                Label lblProductType = (Label)row.FindControl("TestType");
                var productType = lblProductType.Text.Trim();
                objDate = new CohortTestDates();
                objDate.TestStartDate = tbSD.Value.Trim();
                objDate.TestEndDate = tbED.Value.Trim();
                objDate.TestStartHour = ddTime_S.SelectedValue.ToInt();
                objDate.TestEndHour = ddTime_E.SelectedValue.ToInt();
                objDate.TestEndMin = ddMin_E.SelectedValue.ToInt();
                objDate.TestStartMin = ddMin_S.SelectedValue.ToInt();
                objDate.TestStartTime = ddAMPM_S.SelectedValue;
                objDate.TestEndTime = ddAMPM_E.SelectedValue;
                objDate.Type = type;
                objDate.Product = new Product() { ProductId = id,ProductType =  productType};
                objDate.Cohort = new Cohort() { CohortId = HiddenCohortId.Value.ToInt() };
                objDate.TestName = gvTests.Rows[row.RowIndex].Cells[0].Text.Trim();

                lstCohortTestDate.Add(objDate);
            }
        }
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Assign Cohort Test Dates.")
            .Add("Cohort Id ", HiddenCohortId.Value)
            .Add("Program Name ", lblPN.Text)
            .Add("Institution Name ", lblIN.Text)
            .Write();
        #endregion
        lblM.Visible = true;
        lblM.Text = Presenter.AssignTestDateToCohort(lstCohortTestDate, ref studentMessage);
        if (Presenter.CurrentContext.UserType == UserType.SuperAdmin)
        {
            if (studentMessage.Length > 0)
            {
                lblTestAssignMsg.Text =
                    "The following group/student have either taken the exam or have tests scheduled in the future. Please consult faculty for next steps.<br/><br/>";
                lblTestAssignMsg.Text += studentMessage;
            }
        }
    }

    protected void seabtn_Click(object sender, ImageClickEventArgs e)
    {
        ProgramId = HiddenProgramId.Value.ToInt();
        CohortId = HiddenCohortId.Value.ToInt();
        SearchText = TextBox1.Text.Trim();
        Presenter.ShowTests();
    }


    public void ExportStudents(IEnumerable<Student> reportData, ReportAction printActions)
    {
        throw new NotImplementedException();
    }
}
