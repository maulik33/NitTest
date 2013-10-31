using System.Collections.Generic;
using System.Linq;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class RemediationPresenter : AuthenticatedPresenterBase<IRemediationView>
    {
        private const string MESSAGE_DELETE_BUSINESLOGIC = "This Remediation cannot be deleted because is connected with the question";
        private const string Query_PARAM_QID = "QID";
        private const string Query_PARAM_COME_FROM = "ComeFrom";
        private const string Query_PARAM_NAV_QID = "navQID";
        private const string Query_PARAM_NAV_ACTION = "navAction";
        private const string Query_PARAM_ACTION = "Action";
        private const string QUERY_PARAM_MODE = "mode";
        private const string QUERY_PARAM_SEARCHBACK = "searchback";

        private readonly ICMSService _cmsService;
        private ViewMode _mode;
        private int _remediationId = 0;
        private int _qId = 0;
        private int _navQId = 0;
        private int _navAction = 0;
        private string _comeFrom = string.Empty;

        public RemediationPresenter(ICMSService service)
            : base(Module.CMS)
        {
            _cmsService = service;
        }

        public int RemediationId
        {
            get
            {
                if (_remediationId == 0)
                {
                    _remediationId = -1;
                }

                return _remediationId;
            }

            set
            {
                _remediationId = value.ToInt();
            }
        }

        public int QId
        {
            get
            {
                if (_qId == 0)
                {
                    _qId = -1;
                }

                return _qId;
            }

            set
            {
                _qId = value.ToInt();
            }
        }

        public int NavQId
        {
            get
            {
                if (_navQId == 0)
                {
                    _navQId = -1;
                }

                return _navQId;
            }

            set
            {
                _navQId = value.ToInt();
            }
        }

        public int Action
        {
            get
            {
                if (_navAction == 0)
                {
                    _navAction = -1;
                }

                return _navAction;
            }

            set
            {
                _navAction = value.ToInt();
            }
        }

        public string ComeFrom
        {
            get
            {
                if (string.IsNullOrEmpty(_comeFrom))
                {
                    return "EditQuestion.aspx";
                }
                else
                {
                    return _comeFrom;
                }
            }

            set
            {
                _comeFrom = value;
            }
        }

        public override void RegisterAuthorizationRules()
        {
        }

        public override void RegisterQueryParameters()
        {
            if (_mode == ViewMode.View)
            {
                RegisterQueryParameter(Query_PARAM_QID);
                RegisterQueryParameter(QUERY_PARAM_MODE);
                RegisterQueryParameter(Query_PARAM_COME_FROM, QUERY_PARAM_MODE, "4");
                RegisterQueryParameter(Query_PARAM_NAV_ACTION, QUERY_PARAM_MODE, "4");
            }
            else if (_mode == ViewMode.Edit)
            {
                RegisterQueryParameter(Query_PARAM_ACTION);
                RegisterQueryParameter(Query_PARAM_QID, Query_PARAM_ACTION, "2");
            }
        }

        public void PreInitialize(ViewMode mode)
        {
            _mode = mode;
        }

        public void InitializeRemediationParameters()
        {
            if (_mode == ViewMode.View)
            {
                QId = GetParameterValue(Query_PARAM_QID).ToInt();
                if (GetParameterValue(QUERY_PARAM_MODE) == "4")
                {
                    ComeFrom = GetParameterValue(Query_PARAM_COME_FROM);
                    Action = GetParameterValue(Query_PARAM_NAV_ACTION).ToInt();
                    NavQId = GetParameterValue(Query_PARAM_NAV_QID).ToInt();
                }
            }
            else if (_mode == ViewMode.Edit)
            {
                Action = GetParameterValue(Query_PARAM_ACTION).ToInt();
                if (Action == 2)
                {
                    RemediationId = GetParameterValue(Query_PARAM_QID.ToLower()).ToInt();
                }
            }
        }

        #region View Remediation

        public void ShowRemediationDetails()
        {
            InitializeRemediationParameters();
            View.PopulateLippincotts();
        }

        public Question GetQuestionById(int QId)
        {
            return _cmsService.GetQuestionByQId(QId);
        }

        public Remediation GetRemediationById(int RemediationId)
        {
            return _cmsService.GetRemediationById(RemediationId);
        }

        public IEnumerable<Lippincott> GetLippincotts(int QId)
        {
            return _cmsService.GetLippincotts(QId);
        }

        #endregion

        public void DiplayRemeditionDetails()
        {
            if (Action == 2)
            {
                Remediation remediation = _cmsService.GetRemediationById(RemediationId);
                View.PopulateControls(remediation);
            }
        }

        public void DisplayQuestions(string remediation)
        {
            if (!string.IsNullOrEmpty(remediation))
            {
                IEnumerable<Question> questions = _cmsService.GetQuestionByRemediationId(remediation);
                View.PopulateQuestions(questions);
            }
        }

        public void SaveRemediation(Remediation remediation)
        {
            _cmsService.SaveRemediation(remediation);
            NavigateToSearchQuestionPage();
        }

        public void DeleteRemediation()
        {
            IEnumerable<Question> questions = _cmsService.GetQuestionByRemediationId(RemediationId.ToString());
            if (questions.Count() > 0)
            {
                View.DisplayMessage = MESSAGE_DELETE_BUSINESLOGIC;
            }
            else
            {
                _cmsService.DeleteRemediation(RemediationId);
                NavigateToSearchQuestionPage();
            }
        }

        public void NavigateToSearchQuestionPage()
        {
            Navigator.NavigateTo(AdminPageDirectory.SearchQuestion, string.Empty, string.Format("{0}={1}", "searchback", 0));
        }

        public void NavigateViewRemediationPage(string qId)
        {
            Navigator.NavigateTo(AdminPageDirectory.ViewRemediation, string.Empty, string.Format("{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}", Query_PARAM_COME_FROM, "EditR.aspx", Query_PARAM_QID, qId, Query_PARAM_NAV_ACTION, Action.ToString(), Query_PARAM_NAV_QID, RemediationId.ToString(), QUERY_PARAM_MODE, "4"));
        }

        public void NavigateToComeFromPage()
        {
            switch (ComeFrom)
            {
                case "EditQuestion.aspx":
                    Navigator.NavigateTo(AdminPageDirectory.EditQuestion, string.Empty, string.Format("{0}={1}&{2}={3}", QUERY_PARAM_ID, QId, QUERY_PARAM_ACTION_TYPE, 2));
                    break;
                case "EditR.aspx":
                    Navigator.NavigateTo(AdminPageDirectory.EditR, string.Empty, string.Format("{0}={1}&{2}={3}", Query_PARAM_QID.ToLower(), NavQId, Query_PARAM_ACTION.ToLower(), Action));
                    break;
            }
        }

        public void DisplayUploadedTopics(List<Remediation> remediations, string filePath)
        {
            List<Remediation> invalidRemediations = new List<Remediation>();
            List<Remediation> validRemediations = new List<Remediation>();
            List<Remediation> duplicateRemediations = new List<Remediation>();
            GetSegragatedTopics(remediations, invalidRemediations, validRemediations, duplicateRemediations);

            View.DisplayUploadedTopics(validRemediations, invalidRemediations, duplicateRemediations, filePath);
        }

        public void SaveUploadedTopics(List<Remediation> remediations)
        {
            List<Remediation> invalidRemediations = new List<Remediation>();
            List<Remediation> validRemediations = new List<Remediation>();
            List<Remediation> duplicateRemediations = new List<Remediation>();

            GetSegragatedTopics(remediations, invalidRemediations, validRemediations, duplicateRemediations);
            if (invalidRemediations.Count() == 0)
            {
                _cmsService.SaveUploadedRemediations(validRemediations);
            }
        }

        public void NavigateToSearch(string urlQuery)
        {
            Navigator.NavigateTo(AdminPageDirectory.SearchQuestion, string.Empty, string.Format("{0}={1}&CMS=1",
                        QUERY_PARAM_SEARCHBACK, 0));
        }

        private static void GetSegragatedTopics(List<Remediation> remediations, List<Remediation> invalidRemediations, List<Remediation> validRemediations, List<Remediation> duplicateTopics)
        {
            foreach (Remediation r in remediations)
            {
                if (string.IsNullOrEmpty(r.TopicTitle) || string.IsNullOrEmpty(r.Explanation))
                {
                    if (string.IsNullOrEmpty(r.TopicTitle))
                    {
                        r.ErrorMessage = "Topic Title is mandatory.";
                    }
                    else if (string.IsNullOrEmpty(r.Explanation))
                    {
                        r.ErrorMessage = "Description/Content is mandatory.";
                    }

                    invalidRemediations.Add(r);
                }
                else
                {
                    if (validRemediations.Exists(v => v.TopicTitle == r.TopicTitle))
                    {
                        Remediation rem = validRemediations.Where(v => v.TopicTitle == r.TopicTitle).SingleOrDefault();
                        if (rem != null)
                        {
                            duplicateTopics.Add(rem);
                        }

                        validRemediations.Remove(rem);
                        validRemediations.Add(r);
                    }
                    else
                    {
                        validRemediations.Add(r);
                    }
                }
            }
        }
    }
}
