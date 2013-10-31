using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters.Controller;

namespace NursingLibrary.Presenters
{
    /// <summary>
    /// http://www.pnpguidance.net/Post/UnityIoCDependencyInjectionASPNETModelViewPresenter.aspx
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    public abstract class StudentPresenter<TView>
    {
        #region Constructors

        protected StudentPresenter(IStudentAppController appController)
        {
            AppController = appController;
        }

        #endregion Constructors

        #region Properties

        public Student Student
        {
            get { return AppController.Student; }
        }

        public ReviewRemediation ReviewRemediation
        {
            get { return AppController.ReviewRemediation; }
        }

        public TraceToken TraceToken
        {
            get { return AppController.GetTraceToken(); }
        }

        public TView View
        {
            get;
            set;
        }

        protected IStudentAppController AppController
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods

        public virtual IEnumerable<AvpContent> GetAvpContent(int productid, int testSubgroup)
        {
            return AppController.GetAvpContent(productid, testSubgroup);
        }

        public virtual TestType GetTestType(int productId)
        {
            TestType type;
            switch (productId)
            {
                case 1:
                    type = TestType.Integrated;
                    break;
                case 3:
                    type = TestType.FocusedReview;
                    break;
                case 4:
                    type = TestType.Nclex;
                    break;
                case 6:
                    type = TestType.SkillsModules;
                    break;
                default:
                    return TestType.Undefined;
            }

            return type;
        }

        // public virtual void LogInfo(string message)
        // {
        //    AppController.LogInfo(message);
        // }
        public virtual void OnViewError(Exception ex)
        {
            AppController.ShowPage(PageDirectory.Error, null, null);
        }

        public virtual void OnViewInitialized()
        {
            Initialize();
        }

        public virtual void OnViewLoaded()
        {
            Initialize();
        }

        public void OnViewUnload()
        {
            AppController.SaveSession();
        }

        public string PopupDxr(string number)
        {
            if (number == "100")
            {  
                string strJavascript = "<script type='text/javascript'>\n" +
              "<!--\n" +
               "window.open('http://kaptest.adobeconnect.com/p15678723/','Nursing','width=750,height=525,status=yes,fullscreen=no,toolbar=no,menubar=no,location=no');\n" +
                 "// -->\n" +
              "</script>\n";
                return strJavascript;
            }
            else
            {
                string winUrl = string.Format("../Service/Launchdxr.aspx?eid={0}&cid={1}&firstname={2}&lastname={3}",
               Student.EnrollmentId, number, HttpUtility.UrlEncode(Student.FirstName), HttpUtility.UrlEncode(Student.LastName));
                string strJavascript = "<script type='text/javascript'>\n" +
              "<!--\n" +
              "window.open('" + winUrl + "',\'Nursing\',\'status=1,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=1,height=600,width=974\');\n" +
              "// -->\n" +
              "</script>\n";
                return strJavascript;
            }
        }

        public virtual bool ValidateIpLock(string[] clientIps, string restrictedIps)
        {
            bool returnItem = false;
            try
            {
                if (string.IsNullOrEmpty(restrictedIps))
                {
                    returnItem = true;
                    return returnItem;
                }
                #region Parse Client Ips
                var clientIpAddresses = new List<IPAddress>();
                foreach (string t in clientIps)
                {
                    IPAddress clientIp;
                    if (IPAddress.TryParse(t, out clientIp))
                    {
                        clientIpAddresses.Add(clientIp);
                    }
                    else
                    {
                        returnItem = false;
                        return returnItem;
                    }
                }
                #endregion

                #region  Validate restrictedIP Check
                string[] restrictedIpList = restrictedIps.Split((Char)10);
                //// Outer loop - Looping through restricted Ips collection
                foreach (string restrictedIp in restrictedIpList)
                {
                    string restrictedIpReplace = restrictedIp.Replace("\r", string.Empty).Trim();
                    string[] restrictedIpSplitString = restrictedIpReplace.Split('.');
                    if (restrictedIpSplitString.Length == 4)
                    {
                        //// Start of the loop for checking client Ip 
                        foreach (var clientIp in clientIpAddresses)
                        {
                            string[] clientIpSplitString = Convert.ToString(clientIp).Split('.');
                            //// Section for identifying special character like * and - in restricted Ip
                            if (restrictedIpReplace.Contains("-") || restrictedIpReplace.Contains("*"))
                            {
                                if (restrictedIpSplitString[0].Trim() == clientIpSplitString[0].Trim() && restrictedIpSplitString[1].Trim() == clientIpSplitString[1].Trim() && restrictedIpSplitString[2].Trim() == clientIpSplitString[2].Trim())
                                {
                                    if (restrictedIpSplitString[3].Contains("*"))
                                    {
                                        returnItem = true;
                                    }
                                    //// Checking the range of the restrictedIp
                                    string[] rangeItem = restrictedIpSplitString[3].Split('-');
                                    if (rangeItem.Length == 2)
                                    {
                                        if ((Convert.ToInt32(clientIpSplitString[3].Trim()) >= Convert.ToInt32(rangeItem[0].Trim())) && (Convert.ToInt32(clientIpSplitString[3].Trim()) <= Convert.ToInt32(rangeItem[1].Trim())))
                                        {
                                            returnItem = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                returnItem = restrictedIpReplace == Convert.ToString(clientIp) ? true : returnItem;
                            }

                            if (returnItem)
                            {
                                break;
                            }
                        }
                    }
                }

                return returnItem;
                #endregion
            }
            catch
            {
                return returnItem;
            }
        }

        // protected void SetLogger()
        // {
        //    AppController.LoggerName  = Student != null ? string.Format("{0}-{1}", Student.UserName, GetType().Name) : GetType().Name;
        // }
        private void Initialize()
        {
            AppController.Intialize();

            // SetLogger();
        }

        #endregion Methods
    }
}