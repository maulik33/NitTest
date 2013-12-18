using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

namespace NursingLibrary.Common
{
    public class KTPApp
    {
        private static readonly object _lockObject = new object();
        private static KTPApp _instance;
        private bool _isInDebugMode;
        private string _logsFolder;
        private bool _isProductionApp;
        private bool _traceEnabled;
        private TraceToken _traceToken;
        private string _impersonateUserName;
        private string _impersonateUserPassword;
        private string _impersonateUserDomain;
        private string _helpfulDocFolderPath;
        private bool _helpfulDocImpersonationRequired;
        private int _helpfulDocumentUploadLimit;
        private string _defaultAddressCountry;
        private string _countriesWithStates;
        private int _CFRTestIdOffset;
        private int _psychiatricCategoryId;
        private int _managementOfCareCategoryId;
        private bool _impersonationRequired;
        private string _uploadQuestionZippedDocFolderPath;
        private string _uploadQuestionSavedDocFolderPath;
        private int _uploadQuestionDocUploadLimit;
        private string _uploadQuestionTemplatePath;
        private int _maxQuestionTemplateUploadLimit;
        private string _uploadTopicFolderPath;
        private string _TopicTemplatePath;
        private int _topicFileUploadLimit;
        private string _SMVideoBasePath;
        private string _uploadMultiSelectQuestionTemplatePath;
        private string _instructionsDocumentForUploadingQuestions;
        private string _accountURL;
        private string _accountParameter;
        private string _uploadNumericalFillInQuestionTemplatePath;
        private string _akamaiTokenServiceUrl;
        private string _ldapAuthPath;
        private string _proctorTrackTestStartUrl;
        private string _proctorTrackTestSubmitUrl;
        private string _MultipleChoiceMultiSelectQuestionTemplate_PN;
        private string _MultipleChoiceSingleBestAnswerQuestionTemplate_PN;
        private string _NumericalFillInQuestionTemplate_PN;
        private int _pnpsychiatricCategoryId;
        private int _pnmanagementOfCareCategoryId;

        public KTPApp()
        {
            Boolean.TryParse(ConfigurationManager.AppSettings["RunInDebugMode"], out _isInDebugMode);
            _logsFolder = ConfigurationManager.AppSettings["LogsFolder"];
            _isProductionApp = ConfigurationManager.AppSettings["AppRole"] == "1";
            _traceToken = TraceHelper.BeginTrace(0, "System", "Unknown");
            _impersonateUserName = ConfigurationManager.AppSettings["ImpersonateUserName"];
            _impersonateUserPassword = ConfigurationManager.AppSettings["ImpersonateUserPassword"];
            _impersonateUserDomain = ConfigurationManager.AppSettings["ImpersonateUserDomain"];
            _helpfulDocFolderPath = ConfigurationManager.AppSettings["HelpfulDocumentsFolder"];
            _helpfulDocImpersonationRequired = ConfigurationManager.AppSettings["ImpersonationRequired"].ToBool();
            _helpfulDocumentUploadLimit = ConfigurationManager.AppSettings["HelpfulDocUploadLimit"].ToInt();
            _impersonationRequired = ConfigurationManager.AppSettings["ImpersonationRequired"].ToBool();
            _uploadQuestionZippedDocFolderPath = ConfigurationManager.AppSettings["ZipFilePath"];
            _uploadQuestionSavedDocFolderPath = ConfigurationManager.AppSettings["UploadedQuestionSavePath"];
            _uploadQuestionDocUploadLimit = ConfigurationManager.AppSettings["QuestionUploadLimit"].ToInt();
            _uploadQuestionTemplatePath = ConfigurationManager.AppSettings["QuestionUploadTemplatePath"];
            _maxQuestionTemplateUploadLimit = ConfigurationManager.AppSettings["MaxQuestionTemplateUploadLimit"].ToInt();
            _uploadTopicFolderPath = ConfigurationManager.AppSettings["UploadedTopicSavePath"];
            _TopicTemplatePath = ConfigurationManager.AppSettings["TopicTemplatePath"];
            _topicFileUploadLimit = ConfigurationManager.AppSettings["TopicFileUploadLimit"].ToInt();
            _SMVideoBasePath = ConfigurationManager.AppSettings["SMVideoBasePath"];
            _uploadMultiSelectQuestionTemplatePath = ConfigurationManager.AppSettings["QuestionUploadMultiSelectTemplatePath"];
            _instructionsDocumentForUploadingQuestions = ConfigurationManager.AppSettings["InstructionsDocumentForUploadingQuestions"];
            _accountURL = ConfigurationManager.AppSettings["AccountURL"];
            _accountParameter = ConfigurationManager.AppSettings["AccountParameter"];
            _uploadNumericalFillInQuestionTemplatePath = ConfigurationManager.AppSettings["QuestionUploadNumericalFillInTemplatePath"];
            _akamaiTokenServiceUrl = ConfigurationManager.AppSettings["AkamaiTokenServiceURL"];
            _ldapAuthPath = ConfigurationManager.AppSettings["ldapAuthPath"];
            _proctorTrackTestStartUrl = ConfigurationManager.AppSettings["ProctorTrackTestStartUrl"];
            _proctorTrackTestSubmitUrl = ConfigurationManager.AppSettings["ProctorTrackTestSubmitUrl"];
            _MultipleChoiceMultiSelectQuestionTemplate_PN = ConfigurationManager.AppSettings["MultipleChoiceMultiSelectQuestionTemplate_PN"];
            _MultipleChoiceSingleBestAnswerQuestionTemplate_PN = ConfigurationManager.AppSettings["MultipleChoiceSingleBestAnswerQuestionTemplate_PN"];
            _NumericalFillInQuestionTemplate_PN = ConfigurationManager.AppSettings["NumericalFillInQuestionTemplate_PN"];
           
        }

        public static bool IsInDebugMode
        {
            get
            {
                return _instance._isInDebugMode;
            }
        }

        public static TraceToken SystemTraceToken
        {
            get
            {
                return _instance._traceToken;
            }
        }

        public static string LogsFolder
        {
            get
            {
                return _instance._logsFolder;
            }
        }

        public static bool TraceEnabled
        {
            get
            {
                return _instance._traceEnabled;
            }

            set
            {
                _instance._traceEnabled = value;
            }
        }

        public static string TraceFolder
        {
            get
            {
                return Path.Combine(_instance._logsFolder, "Trace");
            }
        }

        public static bool IsProductionApp
        {
            get
            {
                return _instance._isProductionApp;
            }
        }

        public static string ImpersonateUserName
        {
            get
            {
                return _instance._impersonateUserName;
            }
        }

        public static string ImpersonateUserPassword
        {
            get
            {
                return _instance._impersonateUserPassword;
            }
        }

        public static string ImpersonateUserDomain
        {
            get
            {
                return _instance._impersonateUserDomain;
            }
        }

        public static string HelpfulDocumentFolderPath
        {
            get
            {
                return _instance._helpfulDocFolderPath;
            }
        }

        public static bool HelpfulDocImpersonationRequired
        {
            get
            {
                return _instance._helpfulDocImpersonationRequired;
            }
        }

        public static int HelpfulDocumentUploadLimit
        {
            get
            {
                return _instance._helpfulDocumentUploadLimit;
            }
        }

        public static bool ImpersonationRequired
        {
            get
            {
                return _instance._impersonationRequired;
            }
        }

        public static string UploadQuestionZippedFilePath
        {
            get
            {
                return _instance._uploadQuestionZippedDocFolderPath;
            }
        }

        public static string UploadQuestionSavePath
        {
            get
            {
                return _instance._uploadQuestionSavedDocFolderPath;
            }
        }

        public static string UploadedTopicTemplateSavePath
        {
            get
            {
                return _instance._uploadTopicFolderPath;
            }
        }

        public static int TopicFileUploadLimit
        {
            get
            {
                return _instance._topicFileUploadLimit;
            }
        }

        public static string UploadTopicTemplatePath
        {
            get
            {
                return _instance._TopicTemplatePath;
            }
        }

        public static int QuestionFileUploadLimit
        {
            get
            {
                return _instance._uploadQuestionDocUploadLimit;
            }
        }

        public static string UploadQuestionTemplatePath
        {
            get
            {
                return _instance._uploadQuestionTemplatePath;
            }
        }

        public static string UploadMultiSelectQuestionTemplatePath
        {
            get
            {
                return _instance._uploadMultiSelectQuestionTemplatePath;
            }
        }

        public static string UploadNumericalFillInQuestionTemplatePath
        {
            get
            {
                return _instance._uploadNumericalFillInQuestionTemplatePath;
            }
        }

        public static int MaxQuestionTemplateUploadLimit
        {
            get
            {
                return _instance._maxQuestionTemplateUploadLimit;
            }
        }

        public static string SMVideoBasePath
        {
            get
            {
                return _instance._SMVideoBasePath;
            }
        }

        public static string DefaultAddressCountry
        {
            get
            {
                return _instance._defaultAddressCountry;
            }

            set
            {
                _instance._defaultAddressCountry = value;
            }
        }

        public static string CountriesWithStates
        {
            get
            {
                return _instance._countriesWithStates;
            }

            set
            {
                _instance._countriesWithStates = value;
            }
        }

        public static int CFRTestIdOffset
        {
            get
            {
                return _instance._CFRTestIdOffset;
            }

            set
            {
                _instance._CFRTestIdOffset = value;
            }
        }

        public static int PsychiatricCategoryId
        {
            get
            {
                return _instance._psychiatricCategoryId;
            }

            set
            {
                _instance._psychiatricCategoryId = value;
            }
        }

        public static int ManagementOfCareCategoryId
        {
            get
            {
                return _instance._managementOfCareCategoryId;
            }

            set
            {
                _instance._managementOfCareCategoryId = value;
            }
        }

        public static string InstructionsDocumentForUploadingQuestions
        {
            get
            {
                return _instance._instructionsDocumentForUploadingQuestions;
            }
        }

        public static string AccountURL
        {
            get
            {
                return _instance._accountURL;
            }
        }
        
        public static string AccountParameter
        {
            get { return _instance._accountParameter; }
        }

        public static string AkamaiTokenServiceUrl
        {
            get { return _instance._akamaiTokenServiceUrl;  }
        }


        public static string LdapAuthPath
        {
            get { return _instance._ldapAuthPath; }
        }

        public static int PNPsychiatricCategoryId
        {
            get
            {
                return _instance._pnpsychiatricCategoryId;
            }

            set
            {
                _instance._pnpsychiatricCategoryId = value;
            }
        }

        public static int PNManagementOfCareCategoryId
        {
            get
            {
                return _instance._pnmanagementOfCareCategoryId;
            }

            set
            {
                _instance._pnmanagementOfCareCategoryId = value;
            }
        }

        public static void Initialize()
        {
            lock (_lockObject)
            {
                _instance = new KTPApp();
            }
        }

        public static DateTime ApplicationRestartDate { get; set; }

        public static void RefreshAppSettings(ServiceHelper serviceHelper)
        {
            var appSettings = serviceHelper.GetAppSettings();
            TraceEnabled = appSettings[AppSettings.TraceEnabled].ToBool();
            DefaultAddressCountry = appSettings[AppSettings.DefaultAddressCountry].ToString();
            CountriesWithStates = appSettings[AppSettings.CountryWithStates].ToString();
            CFRTestIdOffset = appSettings[AppSettings.CFRTestIdOffset].ToInt();
            PsychiatricCategoryId = appSettings[AppSettings.PsychiatricCategoryId].ToInt();
            ManagementOfCareCategoryId = appSettings[AppSettings.ManagementOfCareCategoryId].ToInt();
            PNPsychiatricCategoryId = appSettings[AppSettings.PNPsychiatricCategoryId].ToInt();
            PNManagementOfCareCategoryId = appSettings[AppSettings.PNManagementOfCareCategoryId].ToInt();
        }

        public static void SaveAppSettings(ServiceHelper serviceHelper)
        {
            serviceHelper.SaveAppSetting(AppSettings.TraceEnabled, TraceEnabled.ToString());
            RefreshAppSettings(serviceHelper);
        }

        public static IEnumerable<Type> GetAdminPresenterTypes()
        {
            var presenters = from t in Assembly.GetExecutingAssembly().GetTypes()
                             where (t.GetCustomAttributes(typeof(PresenterAttribute), false).Length > 0)
                             select t;

            return presenters.ToList();
        }

        public static string ProctorTrackTestSubmitUrl()
        {
            return _instance._proctorTrackTestSubmitUrl;
        }

        public static string ProctorTrackTestStartUrl()
        {
            return _instance._proctorTrackTestStartUrl;
        }


        public static string UploadMultipleChoiceMultiSelectQuestionTemplate_PN
        {
            get
            {
                return _instance._MultipleChoiceMultiSelectQuestionTemplate_PN;
            }
        }

        public static string UploadMultipleChoiceSingleBestAnswerQuestionTemplate_PN
        {
            get
            {
                return _instance._MultipleChoiceSingleBestAnswerQuestionTemplate_PN;
            }
        }

        public static string UploadNumericalFillInQuestionTemplate_PN
        {
            get
            {
                return _instance._NumericalFillInQuestionTemplate_PN;
            }
        }
    }
}
