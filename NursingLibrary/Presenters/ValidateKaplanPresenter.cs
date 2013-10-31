using System;
using System.Collections.Generic;
using NursingLibrary.Common;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Presenters
{
    public class ValidateKaplanPresenter : AuthenticatedPresenterBase<IValidateKaplanView>
    {
        private const string QUERY_PARAM_USERID = "UserID";
        private const string QUERY_PARAM_FName = "FirstName";
        private const string QUERY_PARAM_LName = "LastName";
        private const string QUERY_PARAM_PRODUCTS = "Products";
        private const string QUERY_PARAM_EMAIL = "Email";
        private const string QUERY_PARAM_USERNAME = "UserName";
        private const string QUERY_PARAM_CENTERID = "CenterID";
        private const string QUERY_PARAM_EDATE = "ExpirationDate";
        private const string QUERY_PARAM_SDATE = "StartDate";
        private const string QUERY_PARAM_COUNTRY = "Country";
        private const string QUERY_PARAM_COMMAND = "Command";

        #region Fields
        private readonly IAdminService _adminService;
        #endregion

        #region Constructor
        public ValidateKaplanPresenter(IAdminService adminService)
            : base(Module.UserManagement)
        {
            _adminService = adminService;
        }
        #endregion

        public override void RegisterQueryParameters()
        {
            RegisterQueryParameter(QUERY_PARAM_COMMAND);
            RegisterQueryParameter(QUERY_PARAM_USERID);
            RegisterQueryParameter(QUERY_PARAM_FName);
            RegisterQueryParameter(QUERY_PARAM_LName);
            RegisterQueryParameter(QUERY_PARAM_PRODUCTS);
            RegisterQueryParameter(QUERY_PARAM_EMAIL);
            RegisterQueryParameter(QUERY_PARAM_USERNAME);
            RegisterQueryParameter(QUERY_PARAM_CENTERID);
            RegisterQueryParameter(QUERY_PARAM_EDATE);
            RegisterQueryParameter(QUERY_PARAM_SDATE);
            RegisterQueryParameter(QUERY_PARAM_COUNTRY);
        }

        public void GetCommandvalues()
        {
            string Command = GetParameterValue(QUERY_PARAM_COMMAND);

            if (Command == "register")
            {
                GetCommandRegisterValues();
            }

            if (Command == "start")
            {
                int result = GetCommandStartValues();
                ////After looking at the code, we assume these lines of code will never get called or when called would’ve resulted in an error. So we are purposefully making sure the condition fails when called.”
                if (result > 0 && false)
                {
                    ////Session["UserID"] = result;

                    var Info = GetUserInformation(result);

                    ////Session["CohortID"] = Info.CohortID;
                    ////Session["InstitutionID"] = Info.InstitutionID;
                    ////Session["ProgramID"] = Info.ProgramID;

                    NavigateToHomePage();
                }
                else
                {
                    NavigateToUserErrorpage();
                }
            }
        }

        public void NavigateToInstanceErrorpage()
        {
            Navigator.NaviagteTo(PageDirectory.KaplanInstanceError, null, null);
        }

        public void NavigateToUserErrorpage()
        {
            Navigator.NaviagteTo(PageDirectory.KaplanUserError, null, null);
        }

        public void NavigateToHomePage()
        {
            Navigator.NaviagteTo(PageDirectory.StudentHome, null, null);
        }

        public int GetLoggedInUser(string UserId, string Password)
        {
            return _adminService.UserLogIn(UserId, Password);
        }

        public int GetInstitutionID(int FacilityId)
        {
            return _adminService.GetInstitutionIDByFacilityID(FacilityId);
        }

        public bool GetUserIfExists(string UserName)
        {
            ////return _adminService.IFUserWithThatUserNameExists(UserName);
            return false;
        }

        public IEnumerable<Student> GetUserInformation(int UserID)
        {
            return _adminService.GetUserInfo(UserID);
        }

        public string GetCPStatus(string Products, string ProductCode, bool CreateNew, string pColor, string ExpireDate, string CancelDate, string Statuscode,
                                                string CenterID, string CountryCode, string StartDate, string CPStatus, string InstanceID)
        {
            string[] ListOfProducts = Products.Split(';');
            if (ListOfProducts.Length == 1)
            {
                ListOfProducts[0] = Products;
            }

            string[][] prodlist = new string[10][];

            string one_char = string.Empty;
            int wcount = 0;
            int pcount = 0;

            if (Products.Length > 0)
            {
                for (int i = 0; i < Products.Length - 1; i++)
                {
                    one_char = StringFunctions.Mid(Products, i, 1);
                    if (one_char != "," && one_char != ";")
                    {
                        prodlist[wcount][pcount] = prodlist[wcount][pcount] + one_char;
                    }
                    else
                    {
                        if (one_char.Equals(","))
                        {
                            pcount = pcount + 1;
                            prodlist[wcount][pcount] = string.Empty;
                        }

                        if (one_char.Equals(";"))
                        {
                            wcount = wcount + 1;
                            pcount = 1;
                            prodlist[wcount][pcount] = string.Empty;
                        }
                    }
                }
            }

            for (int i = 0; i <= wcount; i++)
            {
                ProductCode = prodlist[i][1];  // Product Code (KAPTEST1)
                CreateNew = true;

                if (ProductCode == "NURSING")
                {
                    pColor = "1";
                    if (prodlist[i].Length > 1)
                    {
                        ExpireDate = prodlist[i][2];
                    }

                    CancelDate = prodlist[i][3];  // Cancellation Date (not currently used)
                    Statuscode = prodlist[i][4];  // ??? (not currently used)
                    CenterID = prodlist[i][5]; // Center ID (maps to KAPN_BRANCH.Center_ID)
                    CountryCode = prodlist[i][6]; // Country Code  (for KAPN_USER_INFO table)
                    StartDate = prodlist[i][7];  // StartDate  (for KAPN_USER_INFO.User_Start_date table)
                    CPStatus = string.Empty; // Current product no problems yet
                    InstanceID = "1";   // Default
                }
                else
                {
                    CPStatus = ProductCode + " 400,100,0," + InstanceID;
                }

                if (InstanceID == string.Empty)
                {
                    // Invalid InstanceID
                    CPStatus = ProductCode + " 400,200,0," + InstanceID;
                }
            }

            return CPStatus;
        }

        public int GetUserId()
        {
            return _adminService.GetUserID();
        }

        public void SaveValidatedInfo(Student obj)
        {
            _adminService.SaveUser(obj, CurrentContext.User.UserId, CurrentContext.User.UserName);
        }

        public void GetCommandRegisterValues()
        {
            string UserID = GetParameterValue(QUERY_PARAM_USERID);
            string FirstName = GetParameterValue(QUERY_PARAM_FName);
            string LastName = GetParameterValue(QUERY_PARAM_LName);
            string Products = GetParameterValue(QUERY_PARAM_PRODUCTS);
            string Email = GetParameterValue(QUERY_PARAM_EMAIL);
            string UserName = GetParameterValue(QUERY_PARAM_USERNAME);
            string ProductCode = string.Empty;
            bool CreateNew = false;
            string pColor = string.Empty;
            string ExpireDate = string.Empty;
            string CancelDate = string.Empty;
            string Statuscode = string.Empty;
            string CenterID = string.Empty;
            string CountryCode = string.Empty;
            string StartDate = string.Empty;
            string CPStatus = string.Empty;
            string InstanceID = string.Empty;
            int N_UserID = 0;

            if (UserID.Equals(string.Empty) || Products.Equals(string.Empty))
            {
                NavigateToInstanceErrorpage();
            }

            CPStatus = GetCPStatus(Products, ProductCode, CreateNew, pColor, ExpireDate, CancelDate, Statuscode, CenterID, CountryCode, StartDate, CPStatus, InstanceID);

            if (ProductCode == "NURSING")
            {
                pColor = "1";
                N_UserID = Convert.ToInt32(GetUserIfExists(UserID));
                if (N_UserID > 0)
                {
                    CreateNew = false;
                }
            }

            if (CPStatus == string.Empty)
            {
                Student ValObj = new Student();

                ValObj.UserName = UserName.Trim();
                ValObj.UserType = "S";
                ValObj.Email = Email.Trim();
                ValObj.Integreted = 1;
                ValObj.Password = "NURSING";
                ValObj.ExpireDate = ExpireDate;
                ValObj.StartDate = StartDate;
                ValObj.InstitutionId = GetInstitutionID(Convert.ToInt32(CenterID));
                ValObj.CohortId = 0;
                ValObj.GroupId = 0;
                ////After looking at the code, we assume these lines of code will never get called or when called would’ve resulted in an error. So we are purposefully making sure the condition fails when called.”
                if (CreateNew == true && false)
                {
                    ValObj.UserId = 0;
                    ValObj.CreateUser = 1;
                    SaveValidatedInfo(ValObj);
                    N_UserID = GetUserId();
                }
                else
                {
                    ValObj.UpdateUser = 1;
                    ValObj.UserId = N_UserID;
                    SaveValidatedInfo(ValObj);
                }
            }
        }

        public int GetCommandStartValues()
        {
            string UserID = GetParameterValue(QUERY_PARAM_USERID);

            string Products = GetParameterValue(QUERY_PARAM_PRODUCTS);
            string Email = GetParameterValue(QUERY_PARAM_EMAIL);
            string UserName = GetParameterValue(QUERY_PARAM_USERNAME);
            string CenterID = GetParameterValue(QUERY_PARAM_CENTERID);
            string ExpireDate = GetParameterValue(QUERY_PARAM_EDATE);
            string StartDate = GetParameterValue(QUERY_PARAM_SDATE);
            string CountryCode = GetParameterValue(QUERY_PARAM_COUNTRY);
            string FirstName = GetParameterValue(QUERY_PARAM_FName);
            string LastName = GetParameterValue(QUERY_PARAM_LName);

            string InstanceID = string.Empty;

            if (InstanceID == string.Empty)
            {
                NavigateToInstanceErrorpage();
            }

            if (UserID == string.Empty)
            {
                NavigateToUserErrorpage();
            }

            int result = GetLoggedInUser(UserID, "NURSING");

            return result;
        }
    }
}