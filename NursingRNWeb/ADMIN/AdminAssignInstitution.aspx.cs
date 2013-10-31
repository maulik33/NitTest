using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class ADMIN_AdminAssignInstitution : PageBase<IAdminView, AdminPresenter>, IAdminView
{
    public int UserId { get; set; }

    public string Instituions { get; set; }

    public string SearchText { get; set; }

    public int AdminId { get; set; }

    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.List);
    }

    public int ProgramofStudyId
    {
        get
        {
            int programofStudyId = 0;
            if (ddlProgramofStudy.SelectedValue.ToInt() > 0)
            {
                programofStudyId = ddlProgramofStudy.SelectedValue.ToInt();
            }

            return programofStudyId;
        }
        set { throw new NotImplementedException(); }
    }

    #region IAdminView members

    public void PopulateAdmin(Admin admin)
    {
        txtUserName.Text = admin.UserName;
        txtFirstName.Text = admin.FirstName;
        txtLastName.Text = admin.LastName;
        ddLevel.SelectedValue = admin.SecurityLevel.ToString();
        hiddenDDLevel.Value = admin.SecurityLevel.ToString();
    }

    public void RefreshPage(Admin admin, UserAction action, Dictionary<int, string> securityLevel, string title, string subTitle, bool hasDeletePermission, bool hasAddPermission)
    {
        ddLevel.DataSource = securityLevel;
        ddLevel.DataTextField = "Value";
        ddLevel.DataValueField = "Key";
        ddLevel.DataBind();
    }

    public void ShowAdminList(IEnumerable<Admin> admins, SortInfo sortMetaData)
    {
        throw new NotImplementedException();
    }

    public void PopulateInstitutions(IEnumerable<Institution> institutions)
    {
        gvInst.DataSource = institutions;
        gvInst.DataBind();
    }

    public void PopulateProgramofStudy(IEnumerable<ProgramofStudy> programofStudies)
    {
        ddlProgramofStudy.NotSelectedText = "Selection Required";
        ddlProgramofStudy.DataSource = programofStudies;
        ddlProgramofStudy.DataTextField = "ProgramofStudyName";
        ddlProgramofStudy.DataValueField = "ProgramofStudyId";
        ddlProgramofStudy.DataBind();
    }

    public void ExportAdminUserList(IEnumerable<Admin> reportData, ReportAction printActions)
    {
        throw new NotImplementedException();
    }

    public void ShowErrorMessage(string message)
    {
        throw new NotImplementedException();
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        HideProgramofStudy();
        if (!Page.IsPostBack)
        {
            Presenter.ShowAdminDetails();
            if (Presenter.CurrentContext.UserType == UserType.SuperAdmin)
            {
                Presenter.ShowProgramofStudyDetails();
            }
            else
            {
                Presenter.ShowAssignInstitutions();
            }
           
        }
    }

    protected void btSave_Click(object sender, ImageClickEventArgs e)
    {
        #region Trace Information
        TraceHelper.Create(Presenter.CurrentContext.TraceToken, "Assign Institution to Admin Page")
            .Add("Admin Id ", Presenter.Id.ToString())
            .Write();
        #endregion
        int _isActive;
        int count = 0;
        int _userId = Presenter.Id;
        bool _isLocalOrTech = false;
        _isLocalOrTech = hiddenDDLevel.Value == "2" || hiddenDDLevel.Value == "3" ? true : false;

        var checkedIDs = from GridViewRow msgRow in gvInst.Rows
                         where ((CheckBox)msgRow.FindControl("ch")).Checked
                         select msgRow;

        List<Admin> lstAdmin = new List<Admin>();
        StringBuilder _sbInstitutionList = new StringBuilder();
        foreach (var item in checkedIDs)
        {
            var objAdmin = new Admin();
            var institutionId = item.Cells[1].Text;
            _isActive = 1;
            if (_isLocalOrTech)
            {
                if (count > 0)
                {
                    _isActive = 0;
                }
            }
            ++count;
            _sbInstitutionList.Append(institutionId + "|");
            objAdmin.UserId = _userId;
            objAdmin.Institution = new Institution() { InstitutionId = institutionId.ToInt(), Active = _isActive };
            lstAdmin.Add(objAdmin);
        }

        Presenter.AssignInstitutions(lstAdmin, _sbInstitutionList.ToString());
    }

    protected void gvInst_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gv = (GridView)sender;
            CheckBox ch = (CheckBox)e.Row.FindControl("ch");
            var _isActive = ((HiddenField)e.Row.FindControl("Active")).Value.ToInt();
            if (ch != null)
            {
                if (_isActive == 1)
                {
                    ch.Checked = true;
                }
                else
                {
                    ch.Checked = false;
                }
            }
        }
    }

    protected void ddProgramOfStudy_SelectedIndexChanged(object sender, EventArgs e)
   {
        if (ddlProgramofStudy.SelectedValue.ToInt() > 0)
        {
            Presenter.ShowInstitutions();
            gvInst.Visible = true;
        }
        else
        {
            gvInst.Visible = false;
        }
    }

    private void HideProgramofStudy()
    {
        trProgramofStudy.Visible = Presenter.CurrentContext.UserType == UserType.SuperAdmin;
    }

}