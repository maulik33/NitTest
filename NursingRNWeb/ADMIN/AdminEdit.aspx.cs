using System;
using System.Collections.Generic;
using System.Web.UI;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class ADMIN_AdminEdit : PageBase<IAdminView, AdminPresenter>, IAdminView
{
    public int UserId { get; set; }

    public string Instituions { get; set; }

    public string SearchText { get; set; }

    public int AdminId { get; set; }

    public void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudies)
    {
        throw new ArgumentException();
    }

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.Edit);
    }

    #region IAdminView members

    public void PopulateAdmin(Admin admin)
    {
        txtUserName.Text = admin.UserName;
        txtPassword.Text = admin.UserPassword;
        txtEmail.Text = admin.Email;
        txtFirstName.Text = admin.FirstName;
        txtLastName.Text = admin.LastName;
        hiddenDDLevel.Value = admin.SecurityLevel.ToString();
        chkAbleToUpload.Checked = admin.UploadAccess;
    }

    public void RefreshPage(Admin admin, UserAction action, Dictionary<int, string> securityLevel, string title, string subTitle, bool hasDeletePermission, bool hasAddPermission)
    {
        ddLevel.DataSource = securityLevel;
        ddLevel.DataTextField = "Value";
        ddLevel.DataValueField = "Key";
        ddLevel.DataBind();
        lblTitle.Text = title;
        lblSubTitle.Text = subTitle;
        if (action == UserAction.Edit)
        {
            deleteButton.Attributes.Add("onclick", " return confirm('Are you sure that you want to delete the administrator?')");
        }

        deleteButton.Visible = hasDeletePermission;
        deleteButton.Visible = hasAddPermission;
        if (action == UserAction.Add)
        {
            deleteButton.Visible = false;
            hiddenUserId.Value = "0";
        }
        else if (action == UserAction.Edit)
        {
            deleteButton.Visible = true;
            ddLevel.Enabled = false;
            hiddenUserId.Value = Presenter.Id.ToString();
            ddLevel.SelectedValue = hiddenDDLevel.Value;
        }

        if (Presenter.CurrentContext.UserType != UserType.SuperAdmin)
        {
            chkAbleToUpload.Enabled = false;
            chkAbleToUpload.ToolTip = "No rights to perform this operation";
        }
    }

    public int ProgramofStudyId
    {
        get { throw new NotImplementedException(); }
        set { throw new NotImplementedException(); }
    }

    public void ShowAdminList(IEnumerable<Admin> admins, SortInfo sortMetaData)
    {
        throw new NotImplementedException();
    }

    public void PopulateInstitutions(IEnumerable<Institution> institutions)
    {
        throw new NotImplementedException();
    }

    public void ExportAdminUserList(IEnumerable<Admin> reportData, ReportAction printActions)
    {
        throw new NotImplementedException();
    }

    public void ShowErrorMessage(string message)
    {
        lblMessage.Text = message;
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region Trace Information
            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to Admin Edit Page")
                                .Add("Admin Id", Presenter.Id.ToString())
                                .Write();
            #endregion
            Presenter.GetAdminDetails();
        }
    }

    protected void deleteButton_Click(object sender, ImageClickEventArgs e)
    {
        Presenter.DeleteAdmin();
    }

    protected void saveButton_Click(object sender, ImageClickEventArgs e)
    {
        var _admin = new Admin();
        _admin.UserName = txtUserName.Text.Trim();
        _admin.Email = txtEmail.Text.Trim();
        _admin.UserPassword = txtPassword.Text.Trim();
        _admin.FirstName = txtFirstName.Text.Trim();
        _admin.LastName = txtLastName.Text.Trim();
        _admin.SecurityLevel = ddLevel.SelectedValue.ToInt();
        _admin.UserId = hiddenUserId.Value.ToInt();
        _admin.UploadAccess = chkAbleToUpload.Checked;
        _admin.AdminCreateUser = 1;
        Presenter.SaveAdmin(_admin);
        lblM.Visible = AdminId == -1 ? true : false;
    }  
}
