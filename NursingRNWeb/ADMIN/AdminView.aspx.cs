using System;
using System.Collections.Generic;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class ADMIN_AdminView : PageBase<IAdminView, AdminPresenter>, IAdminView
{
    public int UserId { get; set; }

    public string Instituions { get; set; }

    public string SearchText { get; set; }

    public int AdminId { get; set; }

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.List);
    }

    #region IAdminView members

    public void PopulateAdmin(Admin admin)
    {
        txtUserName.Text = admin.UserName;
        txtPassword.Text = admin.UserPassword;
        txtEmail.Text = admin.Email;
        txtFirstName.Text = admin.FirstName;
        txtLastName.Text = admin.LastName;
        ddLevel.SelectedValue = admin.SecurityLevel.ToString();
        hiddenDDLevel.Value = admin.SecurityLevel.ToString();
        hiddenUserId.Value = admin.UserId.ToString();
        chkAbleToUpload.Checked = admin.UploadAccess;
    }

    public void RefreshPage(Admin admin, UserAction action, Dictionary<int, string> securityLevel, string title, string subTitle, bool hasAssignPermission, bool hasAddPermission)
    {
        if (!hasAssignPermission)
        {
            lbAssign.Visible = false;
            lbAssign.Enabled = false;
        }

        ddLevel.DataSource = securityLevel;
        ddLevel.DataTextField = "Value";
        ddLevel.DataValueField = "Key";
        ddLevel.DataBind();
    }

    public void ShowAdminList(IEnumerable<Admin> admins, NursingLibrary.Common.SortInfo sortMetaData)
    {
        throw new NotImplementedException();
    }

    public void ShowErrorMessage(string message)
    {
        throw new NotImplementedException();
    }

    public void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudies)
    {
        throw new ArgumentException();
    }

    public int ProgramofStudyId
    {
        get { throw new NotImplementedException(); }
        set { throw new NotImplementedException(); }
    }

    public void PopulateInstitutions(IEnumerable<Institution> institutions)
    {
        gvInst.DataSource = institutions;
        gvInst.DataBind();
    }

    public void ExportAdminUserList(IEnumerable<Admin> reportData, ReportAction printActions)
    {
        throw new NotImplementedException();
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region Trace Information

            TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Navigated to Admin View Page")
                                .Add("Admin Id", Presenter.Id.ToString())
                                .Write();

            #endregion
        }

        Presenter.ShowAdminDetails();
        if (hiddenDDLevel.Value != "0")
        Presenter.ShowAdminInstitutions();
    }

    protected void lbEdit_Click(object sender, EventArgs e)
    {
        Presenter.NavigateToEdit(Presenter.Id.ToString(), UserAction.Edit);
    }

    protected void lbNew_Click(object sender, EventArgs e)
    {
        Presenter.NavigateToEdit(UserAction.Add);
    }

    protected void lbAssign_Click(object sender, EventArgs e)
    {
        Presenter.NavigateToAssignInstitutions(hiddenUserId.Value);
    }
}