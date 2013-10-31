using System;
using System.Text;
using NursingLibrary.Common;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters.Controller;
using NursingLibrary.Utilities;

namespace NursingLibrary.Presenters
{
    public class StudentLoginPresenter : StudentPresenter<ILoginView>
    {
        #region Constructors

        public StudentLoginPresenter(IStudentAppController appController)
            : base(appController)
        {
        }

        #endregion Constructors

        #region Methods
        public ISessionManagement SessionManager { get; set; }

        public virtual void OnLoginButtonClick(string userName, string password,
            string sessionId, string environment)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                View.FailedLogin(LoginFailure.AuthenticationFailed);
            }

            AppController.LogIn(userName, password, sessionId);
            if (Student != null && Student.UserId > 0)
            {
                if (Student.CohortId == -1)
                {
                    View.FailedLogin(LoginFailure.InvalidCohortId);
                }

                if (Student.GroupId == -1)
                {
                    View.FailedLogin(LoginFailure.InvalidGroupId);
                }
                else
                {
                    ExecutionContext context = new ExecutionContext()
                    {
                        IsAdminLogin = false,
                        UserId = Student.UserId,
                        UserType = UserType.Student,
                        User = null,
                    };
                    Student.IsDiagnosticAndReadinessTest = Student.IsDignosticTest || Student.IsReadinessTest || Student.IsDignosticResultTest || Student.IsReadinessResultTest;
                    Student.ProductId = -1;
                    Student.TestType = TestType.Undefined;
                    AppController.StartTrace(Student.UserId, userName, environment);
                    AppController.SaveSession();
                    AppController.SaveSessionInfo(context);
                    LogStudentInfoOnLogin(Student);
                    AppController.ShowPage(PageDirectory.StudentHome, null, null);
                }
            }
            else
            {
                View.FailedLogin(Student != null ? LoginFailure.AuthenticationFailed : LoginFailure.SystemFailure);
            }
        }

        public void SendPasswordResetMail(string userName, string userEmailId, string environment)
        {
            Student student = AppController.GetStudentInfoByUserNameEmail(userName, userEmailId);
            bool IsResetMailsent = false;
            string message = string.Empty;
            if (student.UserId != 0)
            {
                string emailBody = GetResetPasswordMailContent(student.FirstName, student.UserName, student.Password);
                try
                {
                    EmailHelper.Send(userEmailId, "Kaplan Customer Care<Customer_Care@kaplan.com>", "Kaplan Password Reset", emailBody);
                    message = "Password Reset email was sent to the email address on file.";
                    IsResetMailsent = true;
                }
                catch (Exception ex)
                {
                    IsResetMailsent = false;
                    TraceToken traceToken = TraceHelper.BeginTrace(student.UserId, userName, environment);
                    TraceHelper.Create(traceToken, "Exception Occured while sending mail. Exception Message : " + ex.Message + "Inner Exception : " + ex.InnerException)
                        .Add("User Name", userName)
                        .Add("Email Id", userEmailId)
                        .Write();
                    message = ex.Message;
                    message = "The email could not be sent at this time. Please try again in a few minutes.";
                }
            }
            else
            {
                message = "The username and/or email address provided does not exist or is invalid. Please correct the information provided and resubmit or contact your administrator.";
            }

            View.DisplayMessage(message, IsResetMailsent);
        }

        public LoginContent GetLoginContent()
        {
            return AppController.GetLoginContent((int)LoginContents.Student);
        }

        private string GetResetPasswordMailContent(string firstName, string username, string password)
        {
            StringBuilder mailContent = new StringBuilder();
            firstName = UppercaseFirst(firstName);
            mailContent.Append("Dear " + firstName + "," + "<br /><br />");
            mailContent.AppendLine("We received a request for the password associated with this e-mail address.<br /><br />");
            mailContent.AppendLine("If you made this request, please follow the instructions below. If you did not request a password reset, you can safely ignore this email. Rest assured your account is safe.<br /><br />");
            mailContent.AppendLine("Your user name is: " + username + "<br /><br />");
            mailContent.AppendLine("Your password is: " + password + "<br /><br />");
            mailContent.AppendLine("Please click <a href=\"https://kaplanlwwtesting.kaplan.com/s_login.aspx\">here</a> and enter your user name and password.<br /><br />");
            mailContent.AppendLine("Sincerely, <br /><br />");
            mailContent.AppendLine("The Team at Kaplan");
            return mailContent.ToString();
        }

        private string UppercaseFirst(string s)
        {
            string name = string.Empty;
            if (!string.IsNullOrEmpty(s))
            {
                char[] a = s.ToCharArray();
                a[0] = char.ToUpper(a[0]);
                name = new string(a);
            }

            return name;
        }

        private void LogStudentInfoOnLogin(Student studentInfo)
        {
            if (studentInfo != null)
            {
                TraceHelper.Create(new TraceToken(), "On Successful Student Login")
                    .Add("User Id", studentInfo.UserId.ToString())
                    .Add("User Name", studentInfo.UserName)
                    .Add("Email Address", studentInfo.Email)
                    .Add("First Name", studentInfo.FirstName)
                    .Add("Last Name", studentInfo.LastName)
                    .Add("Enrollment Id", studentInfo.EnrollmentId)
                    .Add("Program Of Study Id", studentInfo.ProgramofStudyId.ToString())
                    .Add("Institution Id", studentInfo.InstitutionId.ToString())
                    .Add("Cohort Id", studentInfo.CohortId.ToString())
                    .Add("Program Id", studentInfo.ProgramId.ToString())
                    .Write();
            }
        }
        #endregion Methods
    }
}