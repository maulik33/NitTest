using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

public partial class Admin_AdminHome : PageBase<IAdminHomeView, AdminHomePresenter>, IAdminHomeView
{
    public override void PreInitialize()
    {
        Presenter.PreInitialize(ViewMode.List);
        ((AdminMaster)this.Master).ShowMainMenuButton();
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        RegisterUserControls(ucAdminAccountMenu, ucAdminAccountMenu, ucAdminViewReportsMenu);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region Trace Information
            TraceHelper.WriteTraceEvent(Presenter.CurrentContext.TraceToken, "Navigated to Admin Home Page");
            #endregion
        }

        if (Presenter.CurrentContext.UserType == UserType.SuperAdmin)
        {
            divAdminContentManagementMenu.Visible = true;
        }

        if (Presenter.CurrentContext.UserType == UserType.LocalAdmin || Presenter.CurrentContext.UserType == UserType.TechAdmin || Presenter.CurrentContext.UserType == UserType.InstitutionalAdmin)
        {
             List<Institution> _institutionIds = Presenter.GetAssignedInstitutions();
             if (_institutionIds.Count() == 0)
            {
                ucAdminAccountMenu.Visible = false;
            }
        }

        HideBackButton();
    }

    protected override void OnUnload(EventArgs e)
    {
        base.OnUnload(e);
    }

    private void HideBackButton()
    {
        Control userControl = Master.FindControl("Head111");
        if (userControl != null)
        {
            Control button = userControl.FindControl("backbtn");
            if (button != null)
            {
                button.Visible = false;
            }
        }
    }
}
