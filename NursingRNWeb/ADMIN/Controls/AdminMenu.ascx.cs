using System;
using System.Web.UI;
using NursingLibrary.Interfaces;
using NursingRNWeb;

public partial class AdminMenu : UserControlBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HideMenuItems();
        if (Global.IsProductionApp)
        {
            //// cms menus
            Image20.Visible = false;
            HyperLink20.Visible = false;

            //// custom tests
            Image7.Visible = false;
            HyperLink7.Visible = false;

            //// avp 
            Image8.Visible = false;
            HyperLink8.Visible = false;
            Div8.CssClass = string.Empty;
        }
    }

    protected void BtnLogout_Click(object sender, ImageClickEventArgs e)
    {
        Session.Abandon();
        Navigator.NavigateTo(NursingLibrary.Presenters.AdminPageDirectory.AdminLogin);
    }

    private void HideMenuItems()
    {
        UserType adminType = CurrentContext.UserType;
        switch (adminType)
        {
            case UserType.SuperAdmin:
                break;
            case UserType.AcademicAdmin:
                break;
            case UserType.InstitutionalAdmin:
                HyperLink15.Visible = false;
                Div15.CssClass = string.Empty;
                HyperLink17.Visible = false;
                Div17.CssClass = string.Empty;
                HyperLink8.Visible = false;
                Div8.CssClass = string.Empty;
                HyperLink7.Visible = false;
                Div7.CssClass = string.Empty;
                HyperLink5.Visible = false;
                Div5.CssClass = string.Empty;
                HyperLink4.Visible = false;
                Div4.CssClass = string.Empty;
                HyperLink2.Visible = false;
                Div2.CssClass = string.Empty;
                HyperLink19.Visible = false;
                Div19.CssClass = string.Empty;
                HyperLink11.Visible = false;
                Div11.CssClass = string.Empty;
                HyperLink15.Visible = false;
                Div15.CssClass = string.Empty;
                HyperLink16.Visible = false;
                Div16.CssClass = string.Empty;
                HyperLink17.Visible = false;
                Div17.CssClass = string.Empty;
                HyperLink20.Visible = false;
                Div20.CssClass = string.Empty;
                HyperLink22.Visible = false;
                Div22.CssClass = string.Empty;
                HyperLink23.Visible = false;
                Div23.CssClass = string.Empty;
                Image15.Visible = false;
                Image17.Visible = false;
                Image8.Visible = false;
                Image7.Visible = false;
                Image5.Visible = false;
                Image4.Visible = false;
                Image2.Visible = false;
                Image19.Visible = false;
                Image11.Visible = false;
                Image15.Visible = false;
                Image16.Visible = false;
                Image17.Visible = false;
                Image20.Visible = false;
                Image22.Visible = false;
                break;
            case UserType.TechAdmin:
                HyperLink2.Visible = false;
                HyperLink3.Visible = false;
                HyperLink4.Visible = false;
                HyperLink5.Visible = false;
                HyperLink6.Visible = false;
                HyperLink7.Visible = false;
                HyperLink8.Visible = false;
                HyperLink9.Visible = false;
                HyperLink10.Visible = false;
                HyperLink11.Visible = false;
                HyperLink15.Visible = false;
                HyperLink16.Visible = false;
                HyperLink17.Visible = false;
                HyperLink18.Visible = false;
                HyperLink19.Visible = false;
                HyperLink20.Visible = false;
                HyperLink21.Visible = false;
                HyperLink22.Visible = false;
                HyperLink23.Visible = false;
                Image2.Visible = false;
                Image3.Visible = false;
                Image4.Visible = false;
                Image5.Visible = false;
                Image6.Visible = false;
                Image7.Visible = false;
                Image8.Visible = false;
                Image9.Visible = false;
                Image10.Visible = false;
                Image11.Visible = false;
                Image15.Visible = false;
                Image16.Visible = false;
                Image17.Visible = false;
                Image18.Visible = false;
                Image19.Visible = false;
                Image20.Visible = false;
                Image21.Visible = false;
                Image22.Visible = false;
                Addp.Visible = false;
                Rpt.Visible = false;
                break;
            default:
                Navigator.NavigateTo(NursingLibrary.Presenters.AdminPageDirectory.AdminLogin);
                break;
        }
    }
}
