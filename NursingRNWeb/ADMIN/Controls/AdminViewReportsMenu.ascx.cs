using System;
using NursingLibrary.Interfaces;
using NursingRNWeb;

public partial class AdminViewReportsMenu : UserControlBase
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        HideMenuItems();
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
                liNormingData.Visible = false;                
                break;
            case UserType.TechAdmin:
                liNursingFaculty.Visible = false;
                liDeansDirectors.Visible = false;
                liInstitutionalAdmins.Visible = false;
                liNormingData.Visible = false;
                liCaseReports.Visible = false;
                break;
            case UserType.LocalAdmin:
                liNormingData.Visible = false;
                liInstitutionalAdmins.Visible = false;
                break;
            default:
                Navigator.NavigateTo(NursingLibrary.Presenters.AdminPageDirectory.AdminLogin);
                break;
        }
    }
}