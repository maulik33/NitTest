using System;
using System.Web;

namespace NursingLibrary.Presenters.Navigation
{
    public class PageNavigator : IPageNavigator
    {
        public void NavigateTo(PageDirectory page, string frgament, string query)
        {
            bool invalidateParts = false;
            string redirectUrl = string.Empty;

            switch (page)
            {
                case PageDirectory.Error:
                    redirectUrl = "~/Error.aspx";
                    break;
                case PageDirectory.StudentLogin:
                    redirectUrl = "~/S_Login.aspx";
                    break;
                case PageDirectory.TestReview:
                    redirectUrl = "~/student/TestReview.aspx";
                    break;
                case PageDirectory.ListReview:
                    redirectUrl = "~/student/ListOfReviews.aspx";
                    break;
                case PageDirectory.StudentHome:
                    redirectUrl = "~/student/User_Home.aspx";
                    break;
                case PageDirectory.Qbank:
                    redirectUrl = "~/student/Qbank.aspx";
                    break;
                case PageDirectory.QbankR:
                    redirectUrl = "~/student/QBank_R.aspx";
                    break;
                case PageDirectory.QbankP:
                    redirectUrl = "~/student/Qbank_P.aspx";
                    break;
                case PageDirectory.Nclex:
                    redirectUrl = "~/student/Nclex.aspx";
                    break;
                case PageDirectory.CaseStudies:
                    redirectUrl = "~/student/CaseStudies.aspx";
                    break;
                case PageDirectory.KaplanReceiveCustomer:
                    redirectUrl = "http://www.kaplanlogin.com/dl/ReceiveCustomer.asp";
                    break;
                case PageDirectory.Review:
                    redirectUrl = "~/student/student_int.aspx";
                    break;
                case PageDirectory.Analysis:
                    redirectUrl = "~/student/Analysis.aspx";
                    break;
                case PageDirectory.Resume:
                    redirectUrl = "~/student/intro.aspx";
                    break;
                case PageDirectory.AccessDenied:
                    redirectUrl = "~/student/AccessDenied.aspx";
                    break;
                case PageDirectory.IntroError:
                    redirectUrl = "~/student/IntroError.aspx";
                    break;
                case PageDirectory.KaplanInstanceError:
                    {
                        invalidateParts = true;
                        redirectUrl = "http://www.kaptest.com/customer-service/integration/CAccessError.jhtml?ERRORCODE=400,200";
                    }

                    break;
                case PageDirectory.KaplanUserError:
                    {
                        invalidateParts = true;
                        redirectUrl = "http://www.kaptest.com/customer-service/integration/CAccessError.jhtml?ERRORCODE=400,500";
                    }

                    break;
                case PageDirectory.FRRemediation:
                    redirectUrl = "~/student/FRRemediation.aspx";
                    break;
                case PageDirectory.FRQBankR:
                    redirectUrl = "~/student/FRRemediationReview.aspx";
                    break;
                case PageDirectory.FRIntroRemediation:
                    redirectUrl = "~/student/RemediationIntro.aspx";
                    break;
                case PageDirectory.FRQBank:
                    redirectUrl = "~/student/FRQBank.aspx";
                    break;
                case PageDirectory.SkillsModules:
                    redirectUrl = "~/student/SkillsModule.aspx";
                    break;
                default:
                    throw new ApplicationException("Cannot navigate to " + page);
            }

            if (false == invalidateParts)
            {
                if (!string.IsNullOrEmpty(query))
                {
                    redirectUrl = string.Format("{0}?{1}", redirectUrl, query);
                }

                if (!string.IsNullOrEmpty(frgament))
                {
                    redirectUrl = string.Format("{0}#{1}", redirectUrl, frgament);
                }
            }

            HttpContext.Current.Response.Redirect(redirectUrl);
        }

        public void NavigateTo(PageDirectory page)
        {
            NavigateTo(page, string.Empty, string.Empty);
        }

        public void NaviagteTo(PageDirectory page, string fragment, string query)
        {
            NavigateTo(page, fragment, query);
        }

        public void NavigateTo(AdminPageDirectory page, string frgament, string query)
        {
            string redirectUrl = string.Empty;

            switch (page)
            {
                case AdminPageDirectory.Error:
                    redirectUrl = "~/Error.aspx";
                    break;
                case AdminPageDirectory.AdminLogin:
                    redirectUrl = "~/A_Login.aspx";
                    break;
                case AdminPageDirectory.ProgramView:
                    redirectUrl = "~/Admin/ProgramView.aspx";
                    break;
                case AdminPageDirectory.ProgramEdit:
                    redirectUrl = "~/Admin/ProgramEdit.aspx";
                    break;
                case AdminPageDirectory.ProgramList:
                    redirectUrl = "~/Admin/ProgramList.aspx";
                    break;
                case AdminPageDirectory.ProgramAddAssign:
                    redirectUrl = "~/Admin/ProgramAddAssign.aspx";
                    break;
                case AdminPageDirectory.GroupView:
                    redirectUrl = "~/Admin/GroupView.aspx";
                    break;
                case AdminPageDirectory.GroupEdit:
                    redirectUrl = "~/Admin/GroupEdit.aspx";
                    break;
                case AdminPageDirectory.StudentListForGroup:
                    redirectUrl = "~/Admin/StudentListForGroup.aspx";
                    break;
                case AdminPageDirectory.GroupTestDates:
                    redirectUrl = "~/Admin/GroupTestDates.aspx";
                    break;
                case AdminPageDirectory.GroupList:
                    redirectUrl = "~/Admin/GroupList.aspx";
                    break;
                case AdminPageDirectory.UserList:
                    redirectUrl = "~/Admin/UserList.aspx";
                    break;
                case AdminPageDirectory.UserEdit:
                    redirectUrl = "~/Admin/UserEdit.aspx";
                    break;
                case AdminPageDirectory.UserView:
                    redirectUrl = "~/Admin/UserView.aspx";
                    break;
                case AdminPageDirectory.UserTestDates:
                    redirectUrl = "~/Admin/StudentTestDates.aspx";
                    break;
                case AdminPageDirectory.InstitutionEdit:
                    redirectUrl = "~/Admin/InstitutionEdit.aspx";
                    break;
                case AdminPageDirectory.InstitutionList:
                    redirectUrl = "~/Admin/InstitutionList.aspx";
                    break;
                case AdminPageDirectory.InstitutionView:
                    redirectUrl = "~/Admin/InstitutionView.aspx";
                    break;
                case AdminPageDirectory.Lippincott:
                    redirectUrl = "~/CMS/Lippincott.aspx";
                    break;
                case AdminPageDirectory.ReadLippincottTemplate:
                    redirectUrl = "~/CMS/ReadLippincottTemplate.aspx";
                    break;
                case AdminPageDirectory.NewLippincott:
                    redirectUrl = "~/CMS/NewLippincott.aspx";
                    break;
                case AdminPageDirectory.ReportCohortTestByQuestion:
                    redirectUrl = "~/Admin/ReportCohortTestByQuestion.aspx";
                    break;
                case AdminPageDirectory.ReportInstitutionPerformance:
                    redirectUrl = "~/Admin/ReportInstitutionPerformance.aspx";
                    break;
                case AdminPageDirectory.AVPItems:
                    redirectUrl = "~/Admin/AVPItems.aspx";
                    break;
                case AdminPageDirectory.NewAVPItems:
                    redirectUrl = "~/Admin/NewAVPItems.aspx";
                    break;
                case AdminPageDirectory.CohortList:
                    redirectUrl = "~/Admin/CohortList.aspx";
                    break;
                case AdminPageDirectory.CohortEdit:
                    redirectUrl = "~/Admin/CohortEdit.aspx";
                    break;
                case AdminPageDirectory.CohortView:
                    redirectUrl = "~/Admin/CohortView.aspx";
                    break;
                case AdminPageDirectory.CohortProgramAssign:
                    redirectUrl = "~/Admin/CohortProgramAssign.aspx";
                    break;
                case AdminPageDirectory.StudentsInCohort:
                    redirectUrl = "~/Admin/StudentsInCohort.aspx";
                    break;
                case AdminPageDirectory.CohortTestDates:
                    redirectUrl = "~/Admin/CohortTestDates.aspx";
                    break;
                case AdminPageDirectory.SearchQuestion:
                    redirectUrl = "~/CMS/SearchQuestion.aspx";
                    break;
                case AdminPageDirectory.ViewRemediation:
                    redirectUrl = "~/CMS/ViewRemediation.aspx";
                    break;
                case AdminPageDirectory.EditQuestion:
                    redirectUrl = "~/CMS/EditQuestion.aspx";
                    break;
                case AdminPageDirectory.ViewQuestion:
                    redirectUrl = "~/CMS/ViewQuestion.aspx";
                    break;
                case AdminPageDirectory.EditR:
                    redirectUrl = "~/CMS/EditR.aspx";
                    break;
                case AdminPageDirectory.ReportStudentQuestion:
                    redirectUrl = "~/Admin/ReportStudentQuestion.aspx";
                    break;
                case AdminPageDirectory.ReportTestStudent:
                    redirectUrl = "~/Admin/ReportTestStudent.aspx";
                    break;
                case AdminPageDirectory.ReportCohortPerformance:
                    redirectUrl = "~/Admin/ReportCohortPerformance.aspx";
                    break;
                case AdminPageDirectory.AdminHome:
                    redirectUrl = "~/Admin/AdminHome.aspx";
                    break;
                case AdminPageDirectory.AdminList:
                    redirectUrl = "~/Admin/AdminList.aspx";
                    break;
                case AdminPageDirectory.AdminEdit:
                    redirectUrl = "~/Admin/AdminEdit.aspx";
                    break;
                case AdminPageDirectory.AdminView:
                    redirectUrl = "~/Admin/AdminView.aspx";
                    break;
                case AdminPageDirectory.AssignInstitute:
                    redirectUrl = "~/Admin/AdminAssignInstitution.aspx";
                    break;
                case AdminPageDirectory.ComponentAsset:
                    redirectUrl = "~/Admin/ComponentAssetList.aspx";
                    break;
                case AdminPageDirectory.CustomTest:
                    redirectUrl = "~/CMS/CustomTest.aspx";
                    break;
                case AdminPageDirectory.NewCustomTest:
                    redirectUrl = "~/CMS/NewCustomTest.aspx";
                    break;
                case AdminPageDirectory.CopyCustomTest:
                    redirectUrl = "~/CMS/CopyCustomTest.aspx";
                    break;
                case AdminPageDirectory.TestCategories:
                    redirectUrl = "~/CMS/TestCategories.aspx";
                    break;
                case AdminPageDirectory.ReleaseReview:
                    redirectUrl = "~/CMS/ReleaseReview.aspx";
                    break;
                case AdminPageDirectory.BackupData:
                    redirectUrl = "~/CMS/BackupData.aspx";
                    break;
                case AdminPageDirectory.EmailEdit:
                    redirectUrl = "~/Admin/EmailEdit.aspx";
                    break;
                case AdminPageDirectory.EmailReceiver:
                    redirectUrl = "~/Admin/EmailReceiver1.aspx";
                    break;
                case AdminPageDirectory.ReportCohortResultByModule:
                    redirectUrl = "~/Admin/ReportCohortResultByModule.aspx";
                    break;
                case AdminPageDirectory.SearchHelpfulDocuments:
                    redirectUrl = "~/Admin/SearchHelpfulDocuments.aspx";
                    break;
                case AdminPageDirectory.OpenHelpfulDocuments:
                    redirectUrl = "~/Admin/OpenHelpfulDoc.aspx";
                    break;
                case AdminPageDirectory.UploadHelpfulDocument:
                    redirectUrl = "~/Admin/UploadHelpfulDocument.aspx";
                    break;
                case AdminPageDirectory.ViewHelpfulDocument:
                    redirectUrl = "~/Admin/ViewHelpfulDocument.aspx";
                    break;
                case AdminPageDirectory.AssignStudentTest:
                    redirectUrl = "~/Admin/AssignStudentTest.aspx";
                    break;
                case AdminPageDirectory.AdhocGroup:
                    redirectUrl = "~/Admin/StudentListForTest.aspx";
                    break;
                case AdminPageDirectory.UploadQuestions:
                    redirectUrl = "~/CMS/UploadQuestions.aspx";
                    break;
                case AdminPageDirectory.LtiProviders:
                    redirectUrl = "~/Admin/LTIProviders.aspx";
                    break;
                default:
                    throw new ApplicationException("Cannot navigate to " + page);
            }

            if (!string.IsNullOrEmpty(query))
            {
                redirectUrl = string.Format("{0}?{1}", redirectUrl, query);
            }

            if (!string.IsNullOrEmpty(frgament))
            {
                redirectUrl = string.Format("{0}#{1}", redirectUrl, frgament);
            }

            HttpContext.Current.Response.Redirect(redirectUrl);
        }

        public void NavigateTo(AdminPageDirectory page)
        {
            NavigateTo(page, string.Empty, string.Empty);
        }
    }
}
