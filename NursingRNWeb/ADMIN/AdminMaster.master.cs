using System;
using NursingLibrary.Interfaces;
using NursingRNWeb;

public partial class AdminMaster : MasterPageBase
{
    public void ShowMainMenuButton()
    {
        ucAdminMainRightMenu.ShowMainMenuButton = false;
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        RegisterUserControls(ucAdminMainLeftMenu, ucAdminMainRightMenu, Head111);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(CurrentContext.UserId.ToString()))
        {
            Navigator.NavigateTo(NursingLibrary.Presenters.AdminPageDirectory.AdminLogin);
        }

        if (Global.IsProductionApp)
        {
            DivRelease.Visible = false;
            DivUploadQues.Visible = false;
            DivUploadTopics.Visible = false;
        }
        else
        {
            DivRelease.Visible = true;
            DivUploadQues.Visible = true;
            DivUploadTopics.Visible = true;
        }

        if (CurrentContext.UserType.Equals(UserType.LocalAdmin))
        {
            ////for ADD
            Div2.Visible = false;
            Div4.Visible = false;
            Div5.Visible = false;

            ////For View/Edit
            Div9.Visible = false;
            Div11.Visible = false;
            Div15.Visible = false;
            Div21.Visible = false;
        }
        else if (CurrentContext.UserType.Equals(UserType.InstitutionalAdmin))
        {
            ////for ADD
            Div2.Visible = false;
            Div4.Visible = false;
            Div5.Visible = false;

            ////For View/Edit
            Div9.Visible = false;
            Div11.Visible = false;
            Div15.Visible = false;
            Div21.Visible = false;
        }
        else if (CurrentContext.UserType.Equals(UserType.TechAdmin))
        {
            ////for ADD
            Div2.Visible = false;
            Div4.Visible = false;
            Div5.Visible = false;
            Div3.Visible = false;
            Div5.Visible = false;
            Div6.Visible = false;
            Div101.Visible = false;
            HideUploadLink();

            ////For View/Edit
            Div9.Visible = false;
            Div11.Visible = false;
            Div15.Visible = false;
            Div10.Visible = false;
            Div21.Visible = false;
        }
        else if (CurrentContext.UserType.Equals(UserType.AcademicAdmin))
        {
            Div21.Visible = false;
        }

        if ((int)CurrentContext.UserType >= 0)
        {
            DivConMan.Visible = false;
            DivCstmTst.Visible = false;
            DivTstCat.Visible = false;
            DivHtmlLnk.Visible = false;
            DivLipp.Visible = false;
            DivNorm.Visible = false;
            DivProbab.Visible = false;
            DivRelease.Visible = false;
        }

        //// To hide Add, View & Edit ContextMenu When Super admin logins
        if (Request.QueryString.HasKeys()
            && (Request.QueryString["CMS"] != null)
            && CurrentContext.UserType.Equals(UserType.SuperAdmin))
        {
            Div101.Visible = false;
            Div2.Visible = false;
            Div3.Visible = false;
            Div4.Visible = false;
            Div5.Visible = false;
            Div6.Visible = false;
            Div20.Visible = false;

            Div102.Visible = false;
            Div9.Visible = false;
            Div10.Visible = false;
            Div11.Visible = false;
            Div12.Visible = false;
            Div13.Visible = false;
            Div14.Visible = false;
            Div15.Visible = false;
            Div19.Visible = false;
            Div22.Visible = false;

            DivConMan.Visible = true;
            DivCstmTst.Visible = true;
            DivTstCat.Visible = true;
            DivHtmlLnk.Visible = true;
            DivLipp.Visible = true;
            DivNorm.Visible = true;
            DivProbab.Visible = true;
            if (false == Global.IsProductionApp)
            {
                DivRelease.Visible = true;
            }
        }

        HideUploadLink();
    }

    private void HideUploadLink()
    {
        Div20.Visible = false;

        // Hack to avoid error. This is to be fixed in next release.
        try
        {
            Div20.Visible = CurrentContext.User.UploadAccess;
        }
        catch
        {
        }
    }
}
