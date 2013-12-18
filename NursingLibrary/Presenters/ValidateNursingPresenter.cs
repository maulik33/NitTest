using System;
using System.Configuration;
using System.Net.Mail;
using NursingLibrary.Common;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class ValidateNursingPresenter : UnAuthenticatedPresenterBase<IValidateNursingView>
    {
        #region Fields
        private readonly IAdminService _adminService;
        private const string Commandregister = "register";
        private const string Commandstart = "start";
        private const string Emptyrstatusvalue = "400,600,0,0";
        private const string Codenursing = "NursingIntegrated";
        private const string Statusvalue = " 400,100,0,";
        private const string Nullrstatusvalue = "HTTP/1.1 204";
        private const string Rstatusdefaultvalue = "HTTP/1.1 206";
        #endregion

        #region Constructor
        public ValidateNursingPresenter(IAdminService adminService)
        {
            _adminService = adminService;
        }
        #endregion

        public void NavigateToInstanceErrorpage()
        {
            Navigator.NavigateTo(PageDirectory.KaplanInstanceError);
        }

        public void NavigateToUserErrorpage()
        {
            Navigator.NavigateTo(PageDirectory.KaplanUserError);
        }

        public void NavigateToHomePage()
        {
            Navigator.NavigateTo(PageDirectory.StudentHome);
        }

        public void ShowCommandValues()
        {
            string RStatus = string.Empty;
            ValidateNursingParams nursingParams = View.GetQueryParameters();
            string Command = nursingParams.Command;

            if (Command == Commandregister)
            {
                RStatus = GetCommandRegisterValues(nursingParams);
            }
            else if (Command == Commandstart)
            {
                GetCommandStartValues(nursingParams);
            }
            else
            {
                View.SendHttpresponse();
            }

            View.SendRStatus(RStatus);
        }

        public string GetCommandRegisterValues(ValidateNursingParams nursingParams)
        {
            int nUserId = 0;
            string productCode = string.Empty;
            bool createNew = false;
            string rStatus = string.Empty;
            string cpStatus = string.Empty;
            string enrollmentId = string.Empty;
            string classCode = string.Empty;
            if (nursingParams.UserId.Equals(string.Empty) || nursingParams.Products.Equals(string.Empty))
            {
                rStatus = Emptyrstatusvalue;
            }

            string[] listOfProducts = nursingParams.Products.Split(';');
            for (int i = 0; i < listOfProducts.Length; i++)
            {
                createNew = true;
                string[] ProductDetails = listOfProducts[i].Split(',');
                for (int k = 0; k < ProductDetails.Length; k++)
                {
                    if (k == 0)
                    {
                        productCode = ProductDetails[0];
                    }
                    else if (k == 1)
                    {
                        enrollmentId = ProductDetails[1];
                    }
                    else if (k == 3)
                    {
                        classCode = ProductDetails[3];
                    }
                    else if (k == 4)
                    {
                        nursingParams.FacilityId = ProductDetails[4];

                        if (nursingParams.FacilityId == string.Empty)
                        {
                            nursingParams.FacilityId = "0";
                        }
                    }
                }

                if (productCode != Codenursing)
                {
                    cpStatus = productCode + Statusvalue + nursingParams.CourseAccessId;
                }

                if (productCode == Codenursing)
                {
                    nUserId = Convert.ToInt32(GetUser(nursingParams.UserId));
                    if (nUserId > 0)
                    {
                        createNew = false;
                    }
                }

                if (cpStatus == string.Empty)
                {
                    Student studentInfo = new Student();
                    studentInfo.KaplanUserId = nursingParams.UserId;
                    studentInfo.EnrollmentId = enrollmentId;
                    studentInfo.UserType = "S";
                    studentInfo.FirstName = nursingParams.FirstName.Trim();
                    studentInfo.LastName = nursingParams.LastName.Trim();
                    studentInfo.Email = nursingParams.Email;
                    studentInfo.Integreted = 1;
                    studentInfo.ExpireDate = string.Empty;
                    studentInfo.StartDate = string.Empty;

                    if (nursingParams.FacilityId.Trim() == string.Empty)
                    {
                        studentInfo.Institution = new Institution() { InstitutionId = 0 };
                    }
                    else
                    {
                        studentInfo.Institution = new Institution() { InstitutionId = _adminService.GetInstitutionIDByFacilityID(Convert.ToInt32(nursingParams.FacilityId)) };
                    }

                    studentInfo.Group = new Group() { GroupId = 0 };
                    studentInfo.Cohort = new Cohort() { CohortId = 0 };
                    if (createNew == true)
                    {
                        string newUserName =  _adminService.GetUniqueUsername(nursingParams.FirstName.Trim(), nursingParams.LastName.Trim());

                        string Pass = _adminService.GetPassword();
                        studentInfo.UserName = newUserName;
                        studentInfo.UserPass = Pass;
                        studentInfo.UserId = 0;
                        studentInfo.CreateUser = 1;
                        _adminService.SaveUser(studentInfo,CurrentContext.User.UserId,CurrentContext.User.UserName);
                        if (!nursingParams.Email.Trim().Equals(string.Empty))
                        {
                            SendEmail(newUserName, Pass, 1, nursingParams.Email, nursingParams.FirstName, nursingParams.LastName);
                        }

                        nUserId = _adminService.GetUserID();
                        _adminService.GetUpdatedIntegratedUser(nUserId, classCode);
                    }
                }

                if (rStatus != string.Empty)
                {
                    rStatus = rStatus + ";" + cpStatus;
                }
                else
                {
                    rStatus = cpStatus;
                }

                if (rStatus == string.Empty)
                {
                    rStatus = Nullrstatusvalue;
                }
                else
                {
                    rStatus = Rstatusdefaultvalue + " " + rStatus;
                }
            }

            return rStatus;
        }

        public void GetCommandStartValues(ValidateNursingParams nursingParams)
        {
            if (nursingParams.ProductCode == Codenursing)
            {
                if (nursingParams.UserId == string.Empty)
                {
                    Navigator.NaviagteTo(PageDirectory.KaplanUserError, null, null);
                }

                int rezults = Convert.ToInt32(GetUser(nursingParams.UserId));
                if (rezults > 0)
                {
                    SessionManager.Set("UserID", rezults);
                    /*GetUserInfo does not work as ReturnUserInfoByUserID doesnot exists*/
                    //// IEnumerable<Student> userInfo = _adminService.GetUserInfo(Convert.ToInt32(nursingParams.UserId));
                    //// Student user = userInfo.FirstOrDefault();
                    Student user = new Student();
                    SessionManager.Set("CohortID", user.CohortId);
                    SessionManager.Set("InstitutionID", user.InstitutionId);
                    SessionManager.Set("ProgramID", user.ProgramId);
                    Navigator.NaviagteTo(PageDirectory.StudentHome, null, null);
                }
                else
                {
                    Navigator.NaviagteTo(PageDirectory.KaplanUserError, null, null);
                }
            }
            else
            {
                Navigator.NaviagteTo(PageDirectory.KaplanInstanceError, null, null);
            }
        }

        public override void RegisterQueryParameters()
        {
        }

        public string SendEmail(string UserName, string pass, int EditOrCreate, string Email, string FirstName, string LastName)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(new MailAddress(Email));
            mailMessage.From = new MailAddress("kaplan@testden.com");
            if (EditOrCreate == 1)
            {
                mailMessage.Subject = "Nursing Welcome Email";
            }
            else
            {
                mailMessage.Subject = "Nursing Change Confirmation";
            }

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.EnableSsl = true;
            object userState = mailMessage;
            ////The following method will fail as SMTP host was not specifed
            smtpClient.Host = ConfigurationManager.AppSettings["EmailServer"];
            smtpClient.Port = 25;
            Student obj = new Student();
            obj.Password = pass;
            obj.UserName = UserName.Trim();
            obj.UserType = "S";
            obj.FirstName = FirstName.Trim();
            obj.LastName = LastName.Trim();
            mailMessage.Body = GetEmailBody(pass, 1, obj);
            mailMessage.Priority = System.Net.Mail.MailPriority.High;
            ////SmtpMail.SmtpServer = ConfigurationSettings.AppSettings["EmailServer"];
            try
            {
                // SmtpMail.Send(mailMessage);
                smtpClient.Send(mailMessage);
                return "true";
            }
            catch (Exception exc)
            {
                return exc.ToString();
            }
        }

        public string GetEmailBody(string pass, int EditOrCreate, Student obj)
        {
            string Body = string.Empty;
            if (EditOrCreate == 1)
            {
                if (obj.UserType == "S")
                {
                    Body = "Dear " + obj.FirstName + " " + obj.LastName + ":" + "\r\n";
                    Body = Body + "Thank you for choosing FTC. To access your online course or course materials, you will need the following information: " + "\r\n" + "\r\n";
                    Body = Body + "URL: http://www.testden.com/scripts/ftc4success/FTC/student_login.aspx " + "\r\n";
                    Body = Body + "Username: " + obj.UserName + "\r\n" + "Password: " + obj.Password + "\r\n" + "\r\n";
                    Body = Body + "Make a note of the above information. " + "\r\n" + "\r\n";
                    Body = Body + "To log in, follow these steps: " + "\r\n" + "\r\n";
                    Body = Body + "     1. Type the URL, http://www.testden.com/scripts/ftc4success/FTC/student_login.aspx, into a browser. " + "\r\n";
                    Body = Body + "     2. Type in your username and password" + "\r\n";
                    Body = Body + "     3. Click the \"LogIn\" button." + "\r\n";
                    Body = Body + "     4. Click on the appropriate link for your needs" + "\r\n" + "\r\n";
                    Body = Body + "From your Welcome Page, you can access your online materials, review recently completed tests and see all your past test scores." + "\r\n" + "\r\n";
                    Body = Body + "You will have online access to these materials until your course expiration date. If you have questions about your FTC program, contact your local FTC centre. ";
                    Body = Body + "\r\n" + "\r\n" + "If you have any additional questions, or experience technical difficulties, please contact customer service at:" + "\r\n" + "\r\n";
                    Body = Body + "E-mail: etechnicalsupport@ftckaplan.com" + "\r\n";
                    Body = Body + "Tel: 0845 0707 582." + "\r\n" + "\r\n";
                    Body = Body + "IMPORTANT:" + "\r\n" + "***Please do not share your password with anyone else.*** " + "\r\n" + "If you are using a public computer (in a computer lab, library etc) please click logout when your session is finished so your online resources will not be available to the next person who uses the computer." + "\r\n" + "\r\n";
                    Body = Body + "Best of luck with your studies!" + "\r\n" + "\r\n";
                }

                if (obj.UserType == "A")
                {
                    Body = "Dear " + obj.FirstName + " " + obj.LastName + ":" + "\r\n";
                    Body = Body + "You have been given a local administrator account for the FTC E-Learning website. Please note the following username and password: " + "\r\n" + "\r\n";
                    Body = Body + "Username: " + obj.UserName + "\r\n" + "Password: " + obj.Password + "\r\n" + "\r\n";
                    Body = Body + "You should use this username and password to access the system at http://www.testden.com/scripts/ftc4success/FTC/student_login.aspx. " + "\r\n" + "Please refer to the Local Administrator Guide in PDF format once you log in for details on how to use the system. " + "\r\n" + "\r\n";
                    Body = Body + "If you have any additional questions, or experience technical difficulties, please contact customer service at:" + "\r\n" + "\r\n";
                    Body = Body + "E-mail: etechnicalsupport@ftckaplan.com" + "\r\n";
                    Body = Body + "Tel: 0845 0707 582." + "\r\n";
                }

                if (obj.UserType == "T")
                {
                    Body = "Dear " + obj.FirstName + " " + obj.LastName + ":" + "\r\n";
                    Body = Body + "Congratulations! You are now a registered teacher for the FTC E-Learning website. To log into the site you will need the following information: " + "\r\n" + "\r\n";
                    Body = Body + "URL: http://www.testden.com/scripts/ftc4success/FTC/student_login.aspx" + "\r\n";
                    Body = Body + "Username: " + obj.UserName + "\r\n" + "Password: " + obj.Password + "\r\n" + "\r\n";
                    Body = Body + "Please refer to the Tutor Guide in PDF format once you log in for details on how to use the system." + "\r\n" + "\r\n";
                    Body = Body + "As long as you are actively teaching a FTC course at your centre, you will have access to this site." + "\r\n" + "\r\n";
                    Body = Body + "If you have any questions about the site, speak with the local administrator at your centre." + "\r\n" + "If you have any additional questions, or experience technical difficulties, please contact customer service at:" + "\r\n" + "\r\n";
                    Body = Body + "E-mail: etechnicalsupport@ftckaplan.com" + "\r\n";
                    Body = Body + "Tel: 0845 0707 582." + "\r\n";
                    Body = Body + "IMPORTANT:" + "\r\n" + "***Please do not share your password with anyone else.*** " + "\r\n" + "If you are using a public computer (in a computer lab, library etc), logout when you have finished using the site." + "\r\n" + "\r\n";
                }
            }
            else
            {
                Body = "Dear " + obj.FirstName + " " + obj.LastName + ":" + "\r\n" + "\r\n";
                Body = Body + "A request has been made to resend you the user account for the FTC E-Learning website. Please note again your username and password: " + "\r\n" + "\r\n";
                Body = Body + "     Username: " + obj.UserName + "\r\n" + "     Password: " + obj.Password + "\r\n" + "\r\n";
                Body = Body + "Please use this login and password to access the FTC E-Learning site at http://www.testden.com/scripts/ftc4success/FTC/student_login.aspx." + "\r\n" + "\r\n";
                Body = Body + "If you did not request this email, or have additional questions, please contact customer service at:" + "\r\n" + "\r\n";
                Body = Body + "E-mail: etechnicalsupport@ftckaplan.com" + "\r\n";
                Body = Body + "Tel: 0845 0707 582." + "\r\n";
                Body = Body + "Sincerely," + "\r\n" + "The FTC Team " + "\r\n" + "\r\n" + "\r\n";
            }

            return Body;
        }

        public object GetUser(string userId)
        {
            return _adminService.GetUser(userId);
        }
    }
}
