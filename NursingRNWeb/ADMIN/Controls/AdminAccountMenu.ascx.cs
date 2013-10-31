using System;
using NursingLibrary.Interfaces;
using NursingRNWeb;

public partial class AdminAccountMenu : UserControlBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HideMenuItems();
        HideUploadLink();
    }

    private void HideUploadLink()
    {
        trHelpfulDocument.Visible = CurrentContext.User.UploadAccess;
    }

    private void HideMenuItems()
    {
        UserType adminType = CurrentContext.UserType;
        switch (adminType)
        {
            case UserType.SuperAdmin:
                if (CurrentContext.User.UploadAccess == false)
                {
                    trReportTestsScheduledbyDate.Attributes.Add("style", "background-color:#f4f4fe;");
                }

                trReportTestsScheduledbyDate.Visible = true;
                hyplnkBulkModify.Visible = true;
                break;
            case UserType.AcademicAdmin:
                trReportTestsScheduledbyDate.Visible = false;
                break;
            case UserType.InstitutionalAdmin:
                trInsitution.Visible = false;
                trProgam.Visible = false;
                trAssignStudents.Visible = false;
                hyplnkAddCohort.Visible = false;
                trReportTestsScheduledbyDate.Visible = false;
                trAdministrator.Attributes.Add("style", "background-color:#E1E2F7;");
                trCohort.Attributes.Add("style", "background-color:#f4f4fe;");
                trGroup.Attributes.Add("style", "background-color:#E1E2F7;");
                trStudent.Attributes.Add("style", "background-color:#f4f4fe;");
                trHelpfulDocument.Attributes.Add("style", "background-color:#E1E2F7;");
                break;
            case UserType.TechAdmin:
                trInsitution.Visible = false;
                trProgam.Visible = false;
                trAssignStudents.Visible = false;
                trAdministrator.Visible = false;
                hyplnkAddCohort.Visible = false;
                hyplnkAddGroup.Visible = false;
                trReportTestsScheduledbyDate.Visible = false;
                trCohort.Attributes.Add("style", "background-color:#E1E2F7;");
                imgCohort.Src = "../../Images/arrow_mouseout.bmp";
                trGroup.Attributes.Add("style", "background-color:#f4f4fe;");
                imgGroup.Src = "../../Images/arrow_mouseout_1.bmp";
                trStudent.Attributes.Add("style", "background-color:#E1E2F7;");
                imgStudent.Src = "../../Images/arrow_mouseout.bmp";
                break;
            case UserType.LocalAdmin:
                trInsitution.Visible = false;
                trProgam.Visible = false;
                trAssignStudents.Visible = false;
                hyplnkAddCohort.Visible = false;
                trReportTestsScheduledbyDate.Visible = false;
                trAdministrator.Attributes.Add("style", "background-color:#E1E2F7;");
                imgAdministrator.Src = "../../Images/arrow_mouseout.bmp";
                trHelpfulDocument.Attributes.Add("style", "background-color:#E1E2F7;");
                trCohort.Attributes.Add("style", "background-color:#f4f4fe;");
                trGroup.Attributes.Add("style", "background-color:#E1E2F7;");
                trStudent.Attributes.Add("style", "background-color:#f4f4fe;");
                break;
            default:
                Navigator.NavigateTo(NursingLibrary.Presenters.AdminPageDirectory.AdminLogin);
                break;
        }
    }
}
