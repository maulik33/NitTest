using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;

public partial class ADMIN_EmailReceiver : ReportPageBase<IEmailReceiverView, EmailReceiverPresenter>, IEmailReceiverView
{
    private bool _IsEmailIdExistInQueryString;

    public bool IsEmailEdit
    {
        get
        {
            return false;
        }
    }

    public bool IsEmailIdExistInQueryString
    {
        get
        {
            return _IsEmailIdExistInQueryString;
        }

        set
        {
            _IsEmailIdExistInQueryString = value;
        }
    }

    public string EmailId
    {
        get
        {
            object o = this.ViewState["EmailId"];
            int number = 0;
            if (o == null || !int.TryParse(o.ToString(), out number))
            {
                return "-1";
            }
            else
            {
                return o.ToString();
            }
        }

        set
        {
            this.ViewState["EmailId"] = value;
        }
    }

    public string EmailTo
    {
        get
        {
            // last two check boxes in this check box group deleted so cases deleted as well           
            if (cbxEmailTo.Items[0].Selected)
            {
                return "0";
            }
            else
            {
                return string.Empty;
            }
        }
    }

    public string EmailToStudentOrAdmin
    {
        get
        {
            if (CheckBoxList2.Items[0].Selected)
            {
                return "0";
            }
            else if (CheckBoxList2.Items[1].Selected)
            {
                return "1";
            }
            else
            {
                return string.Empty;
            }
        }
    }

    public string SelectedValue
    {
        get
        {
            return lbxSearchItems.SelectedValue.ToString();
        }
    }

    public bool CustomEmailToAdmins
    {
        get
        {
            if (CheckBoxList2.Items[0].Selected)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool CustomEmailToStudents
    {
        get
        {
            if (CheckBoxList2.Items[1].Selected)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool UserEmailToStudents
    {
        get
        {
            if (cbxEmailTo.Items[0].Selected)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool UserEmailLocalAdmins
    {
        // this is no longer an option
        get { return false; }
    }

    public bool UserEmailTechAdmins
    {
        // this is no longer an option
        get { return false; }
    }

    public override void PreInitialize()
    {
        Presenter.PreInitialize();
        MapControl(ddCustomEmails, ddlProgramofStudy, lbxInstitution, lbxCohort, lbxGroup);
    }

    #region IEmailReceiverView Methods

    public void PopulateCustomEmails(IEnumerable<Email> emails)
    {
        ControlHelper.PopulateCustomEmails(ddCustomEmails, emails);
    }

    public void PopulateInstitutions(IEnumerable<Institution> institutions)
    {
        lbxGroup.ClearData();
        ControlHelper.PopulateInstitutions(lbxInstitution, institutions, true);
    }

    public void PopulateCohorts(IEnumerable<Cohort> cohorts)
    {
        ControlHelper.PopulateCohorts(lbxCohort, cohorts);
    }

    public void PopulateGroup(IEnumerable<Group> groups)
    {
        if (string.IsNullOrEmpty(lbxCohort.SelectedValue))
        {
            groups = new List<Group>();
        }
        ControlHelper.PopulateGroups(lbxGroup, groups);
    }

    public void PopulateStudent(IEnumerable<StudentEntity> students)
    {
        ControlHelper.PopulateStudents(lbxStudent, students);
    }

    public void PopulateAdmin(IEnumerable<Admin> admins)
    {
        ControlHelper.PopulateAdmin(lbxAdmin, admins);
    }

    public void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programOfStudies)
    {
        ControlHelper.PopulateProgramofStudy(ddlProgramofStudy, programOfStudies);
    }

    public void ShowSendEmailResult(string msg)
    {
        Messenger.Text = msg;
    }

    public void ShowEmailDetails(Email email)
    {
    }

    public void SetControlsSpecial()
    {
        foreach (ListItem i in this.cbxEmailTo.Items)
        {
            i.Enabled = false;
        }
    }

    #endregion

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        if (!string.IsNullOrEmpty(Page.Request.QueryString["EmailId"]))
        {
            IsEmailIdExistInQueryString = true;
        }
        else
        {
            IsEmailIdExistInQueryString = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckIsSuperAdmin();
        if (!string.IsNullOrEmpty(SelectedValue))
        {
            ddCustomEmails.SelectedValue = SelectedValue;
        }

        if (!this.Page.IsPostBack)
        {
            SetControls();
            Presenter.PopulateInstitutions();
        }
    }

    protected void btnNewEmail_Click(object sender, EventArgs e)
    {
        Presenter.NavigateToEmailEdit(Constants.LIST_NOT_SELECTED_VALUE.ToInt());
    }

    protected void btnEditEMail_Click(object sender, EventArgs e)
    {
        Presenter.NavigateToEmailEdit(ddCustomEmails.SelectedValue.ToInt());
    }

    protected void btnEmailSearch_Click(object sender, EventArgs e)
    {
        lbxSearchItems.ClearData();
        mdlPopup.Show();
    }

    protected void btnKeyword_Click(object sender, EventArgs e)
    {
        Thread.Sleep(4000);
        IEnumerable<Email> emailList = Presenter.SearchCustomEmails(txtEmailKeyword.Text);
        if (emailList != null && emailList.Count() != 0)
        {
            ControlHelper.PopulateEmails(lbxSearchItems, emailList);
            lbxSearchItems.Style.Add(HtmlTextWriterStyle.Cursor, "pointer");
            ClientScript.RegisterOnSubmitStatement(GetType(), string.Empty, "<script type=text/javascript>document.body.style.cursor = 'default';</script>");
        }
        else
        {
            lblMessage.Visible = true;
            lblMessage.Text = "No such custom emails found";
            lbxSearchItems.Style.Remove(HtmlTextWriterStyle.Cursor);
        }
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        txtEmailKeyword.Text = string.Empty;
        lbxSearchItems.ClearData();
    }

    protected void Reset_Click(object sender, EventArgs e)
    {
        ResetControls();
    }

    protected void btnPopulate_Click(object sender, EventArgs e)
    {
        Presenter.PoupulateStudentAdmin();
        Panel1.Visible = true;
    }

    protected void SearchStudent_Click(object sender, EventArgs e)
    {
        if (TextBox11.Text != string.Empty)
        {
            Presenter.SearchStudent(TextBox11.Text.Trim());
            Panel1.Visible = true;
        }
    }

    protected void SearchAdmin_Click(object sender, EventArgs e)
    {
        if (TextBox12.Text != string.Empty)
        {
            Presenter.SearchAdmin(TextBox12.Text.Trim());
            Panel1.Visible = true;
        }
    }

    protected void SendNow_Click(object sender, EventArgs e)
    {
        if (IsValidData())
        {
            Presenter.GenerateEmailMission(DateTime.Now, ddCustomEmails.SelectedValue.ToInt(), lbxAdmin.SelectedValuesText, lbxStudent.SelectedValuesText);
        }
    }

    protected void btnSendLater_Click(object sender, EventArgs e)
    {
        if (IsValidData())
        {
            DateTime validDate = DateTime.Now;

            if (!DateTime.TryParse(this.dtSendLater.Text, out validDate))
            {
                Messenger.Text = "Preset send Email time is invalid.";
                return;
            }
            else
            {
                Messenger.Text = string.Empty;
            }

            Presenter.GenerateEmailMission(validDate, ddCustomEmails.SelectedValue.ToInt(), lbxAdmin.SelectedValuesText, lbxStudent.SelectedValuesText);
        }
    }

    #region Private Methods

    private void SetControls()
    {
        foreach (ListItem i in this.cbxEmailTo.Items)
        {
            i.Selected = false;
        }

        Panel1.Visible = false;
    }

    private void ResetControls()
    {
        this.EmailId = Constants.LIST_NOT_SELECTED_VALUE;
        Panel1.Visible = false;
        ddCustomEmails.SelectedValue = Constants.LIST_NOT_SELECTED_VALUE;
        ddlProgramofStudy.SelectedValue = ((int)ProgramofStudyType.RN).ToString();
        lbxInstitution.ClearData();
        lbxCohort.ClearData();
        lbxGroup.ClearData();
        lbxAdmin.ClearData();
        lbxStudent.ClearData();
        cbxEmailTo.ClearSelection();
        CheckBoxList2.ClearSelection();
        dtSendLater.Text = string.Empty;
        TextBox11.Text = string.Empty;
        TextBox12.Text = string.Empty;
        Presenter.PopulateInstitutions(((int)ProgramofStudyType.RN));
    }

    private bool IsValidData()
    {
        if (ddCustomEmails.SelectedValue == Constants.LIST_NOT_SELECTED_VALUE && cbxEmailTo.SelectedItem == null)
        {
            Messenger.Text = "Please select Email to send.";
            return false;
        }
        else if (ddCustomEmails.SelectedValue != Constants.LIST_NOT_SELECTED_VALUE && CheckBoxList2.SelectedItem == null)
        {
            Messenger.Text = "Please select Email to Student or Admin.";
            return false;
        }

        Messenger.Text = string.Empty;
        return true;
    }

    private void CheckIsSuperAdmin()
    {
        if (Presenter.CurrentContext.UserType != UserType.SuperAdmin)
        {
            Session.Abandon();
            Presenter.NavigateToLogin();
        }
    }

    #endregion
}
