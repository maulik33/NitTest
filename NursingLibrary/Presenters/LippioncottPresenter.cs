using System.Collections.Generic;
using System.IO;
using System.Linq;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class LippioncottPresenter : AuthenticatedPresenterBase<ILippincottView>
    {
        private const string QUERY_PARAM_LIPINCOTT_ID = "IID";
        private const string QUERY_PARAM_MODE = "Mode";
        private const string QUERY_PARAM_SEARCH_CONDITION = "SearchCondition";
        private const string QUERY_PARAM_SORT = "sort";
        private readonly ICMSService _cmsService;
        private ViewMode _mode;

        public LippioncottPresenter(ICMSService service)
            : base(Module.CMS)
        {
            _cmsService = service;
        }

        public override void RegisterAuthorizationRules()
        {
        }

        public override void RegisterQueryParameters()
        {
            if (_mode == ViewMode.Edit)
            {
                RegisterQueryParameter(QUERY_PARAM_LIPINCOTT_ID);
                RegisterQueryParameter(QUERY_PARAM_MODE);
                RegisterQueryParameter(QUERY_PARAM_SEARCH_CONDITION, QUERY_PARAM_MODE, "4");
                RegisterQueryParameter(QUERY_PARAM_SORT, QUERY_PARAM_MODE, "4");
            }

            if (_mode == ViewMode.List)
            {
                RegisterQueryParameter(QUERY_PARAM_MODE);
                RegisterQueryParameter(QUERY_PARAM_SEARCH_CONDITION, QUERY_PARAM_MODE, "4");
                RegisterQueryParameter(QUERY_PARAM_SORT, QUERY_PARAM_MODE, "4");
            }
        }

        public void PreInitialize(ViewMode mode)
        {
            _mode = mode;
        }

        public void InitializeSearchLippincott()
        {
            if (GetParameterValue(QUERY_PARAM_MODE) == "4")
            {
                View.SearchCondition = GetParameterValue(QUERY_PARAM_SEARCH_CONDITION);
                View.Sort = GetParameterValue(QUERY_PARAM_SORT);
            }
        }

        public void SearchLippincott(string searchCondition, string sortMetaData)
        {
            IEnumerable<Lippincott> lippincotts = _cmsService.SearchLippincotts(searchCondition);
            View.SearchLippincott(lippincotts,
                SortHelper.Parse(sortMetaData));
        }

        public void DeleteLippincott(int lippinCottId)
        {
            _cmsService.DeleteLippinCott(lippinCottId);
        }

        public void PopulateLippinCottControls()
        {
            int lippinCottId = GetParameterValue(QUERY_PARAM_LIPINCOTT_ID).ToInt();
            Lippincott lippinCott = null;
            IEnumerable<Remediation> remediations = _cmsService.GetRemediations();
            if (lippinCottId > 0)
            {
                lippinCott = _cmsService.GetLippincottRemediationByID(lippinCottId);
            }

            View.PopulateControls(remediations, lippinCott);
        }

        public void RefreshQuestionList()
        {
            int lippinCottId = GetParameterValue(QUERY_PARAM_LIPINCOTT_ID).ToInt();
            IEnumerable<Lippincott> lippincotts = _cmsService.GetQuestionLippincotts(lippinCottId);
            View.PopulateQuestionList(lippincotts);
        }

        public void SaveQuestions(string questionIds)
        {
            int lippinCottId = GetParameterValue(QUERY_PARAM_LIPINCOTT_ID).ToInt();
            string st = string.Empty;
            string erroMsg = string.Empty;
            string[] s = questionIds.Split(',');
            int qId = 0;
            foreach (string questionId in s)
            {
                if (!string.IsNullOrEmpty(questionId))
                {
                    if (IsValidQuestionId(questionId, out qId))
                    {
                        _cmsService.InsertQuestionLippinCott(qId, lippinCottId);
                    }
                    else
                    {
                        erroMsg = "QuestionID: '" + questionId + "' is not existing!";
                        st = st + questionId + ",";
                    }
                }
            }

            RefreshQuestionList();
            if (erroMsg.Length > 0)
            {
                View.AddMessage = erroMsg;
                st = st.Remove(st.Length - 1, 1);
                View.SerachTextBox = st;
            }
            else
            {
                View.SerachTextBox = string.Empty;
            }
        }

        public void SaveLippincott(Lippincott lippinCott)
        {
            int lippinCottId = GetParameterValue(QUERY_PARAM_LIPINCOTT_ID).ToInt();
            if (lippinCottId != -1)
            {
                lippinCott.LippincottID = lippinCottId;
            }

            _cmsService.SaveLippinCott(lippinCott);
            NavigateToLippinCott(lippinCott.LippincottID);
        }

        public void DeleteLippinCottQuestion(int questionId)
        {
            int lippinCottId = GetParameterValue(QUERY_PARAM_LIPINCOTT_ID).ToInt();
            _cmsService.DeleteLippinCottQuestion(lippinCottId, questionId);
            RefreshQuestionList();
        }

        public void ReadAndSaveLippincott(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            string Content = sr.ReadToEnd();

            // Remediation
            string RemediationExp = string.Empty;
            string Remediation = null;
            int p1 = Content.IndexOf("<topictitle>");
            int p2 = Content.IndexOf("</topictitle>");
            Remediation = Content.Substring(p1, p2 - p1);
            Remediation = Remediation.Replace("<topictitle>", string.Empty);
            Remediation = Remediation.Replace("<p>", string.Empty);
            Remediation = Remediation.Replace("</p>", string.Empty);

            int RemediationID = 0;
            List<int> ArQID = new List<int>();

            // add this Remediation TopicTitle into database
            Remediation remediation = GetRemeditionById(Remediation);
            remediation.TopicTitle = Remediation;
            remediation.Explanation = string.Empty;
            remediation.ReleaseStatus = string.Empty;
            _cmsService.SaveRemediation(remediation);
            RemediationID = remediation.RemediationId;
            RemediationExp = remediation.Explanation;
            string QIDs = null;
            p1 = Content.IndexOf("<relatedqid>");
            p2 = Content.IndexOf("</relatedqid>");
            QIDs = Content.Substring(p1, p2 - p1);
            QIDs = QIDs.Replace("<relatedqid>", string.Empty);
            QIDs = QIDs.Replace("<p>", string.Empty);
            QIDs = QIDs.Replace("</p>", string.Empty);
            QIDs = QIDs.Replace(" ", string.Empty);
            string[] QID = QIDs.Split(',');
            foreach (string id in QID)
            {
                Question question = _cmsService.GetQuestionByQuestionId(id);
                if (question == null)
                {
                    View.AddMessage = "QuestionID: '" + id + "' is not existing!";
                    continue;
                }

                question.RemediationId = RemediationID;
                question.TopicTitleId = Remediation;
                question.QuestionId = id;
                _cmsService.InsertUpdateQuestion(question);
                ArQID.Add(question.Id);
            }

            // LippincottTitle 1
            string LippincottTitle = null;
            p1 = Content.IndexOf("<title1>");
            p2 = Content.IndexOf("</title1>");
            LippincottTitle = Content.Substring(p1, p2 - p1);
            LippincottTitle = LippincottTitle.Replace("<title1>", string.Empty);
            string LippincottExplanation = null;
            p1 = Content.IndexOf("<explanation1>");
            p2 = Content.IndexOf("</explanation1>");
            LippincottExplanation = Content.Substring(p1, p2 - p1);
            LippincottExplanation = LippincottExplanation.Replace("<explanation1>", string.Empty);
            string LippincottTitle2 = null;
            p1 = Content.IndexOf("<title2>");
            p2 = Content.IndexOf("</title2>");
            LippincottTitle2 = Content.Substring(p1, p2 - p1);
            LippincottTitle2 = LippincottTitle2.Replace("<title2>", string.Empty);
            string LippincottExplanation2 = null;
            p1 = Content.IndexOf("<explanation2>");
            p2 = Content.IndexOf("</explanation2>");
            LippincottExplanation2 = Content.Substring(p1, p2 - p1);
            LippincottExplanation2 = LippincottExplanation2.Replace("<explanation2>", string.Empty);
            Lippincott lippinCott = _cmsService.GetLippincottByRemediationID(RemediationID);
            lippinCott.LippincottExplanation = LippincottExplanation;
            lippinCott.LippincottExplanation2 = LippincottExplanation2;
            lippinCott.LippincottTitle = LippincottTitle;
            lippinCott.LippincottTitle2 = LippincottTitle2;
            _cmsService.SaveLippinCott(lippinCott);
            foreach (int id in ArQID)
            {
                _cmsService.InsertQuestionLippinCott(id, lippinCott.LippincottID);
            }

            sr.Close();
            View.AddMessage = "All selected files have been transfered into the database.";
            View.ShowMessage();
        }

        public void NavigateToLippinCott()
        {
            string search = string.Empty;
            string sort = string.Empty;
            if (GetParameterValue(QUERY_PARAM_MODE) == "4")
            {
                search = GetParameterValue(QUERY_PARAM_SEARCH_CONDITION);
                sort = GetParameterValue(QUERY_PARAM_SORT);
            }

            Navigator.NavigateTo(AdminPageDirectory.Lippincott, string.Empty, string.Format("{0}={1}&{2}={3}&{4}={5}&CMS=1",
                QUERY_PARAM_MODE, 4, QUERY_PARAM_SEARCH_CONDITION, search, QUERY_PARAM_SORT, sort));
        }

        public void NavigateToLippinCott(int lippinCottId)
        {
            string search = string.Empty;
            string sort = string.Empty;
            if (GetParameterValue(QUERY_PARAM_MODE) == "4")
            {
                search = GetParameterValue(QUERY_PARAM_SEARCH_CONDITION);
                sort = GetParameterValue(QUERY_PARAM_SORT);
            }

            Navigator.NavigateTo(AdminPageDirectory.Lippincott, string.Empty, string.Format("{0}={1}&{2}={3}&{4}={5}&{6}={7}&CMS=1",
             QUERY_PARAM_MODE, 4, QUERY_PARAM_LIPINCOTT_ID, lippinCottId, QUERY_PARAM_SEARCH_CONDITION, search, QUERY_PARAM_SORT, sort));
        }

        public void NavigateToLippincottTemplate()
        {
            string index = string.Empty;
            Navigator.NavigateTo(AdminPageDirectory.ReadLippincottTemplate, string.Empty, string.Format("CMS=1"));
        }

        public void NavigateToNewLippincott()
        {
            string Id = "-1";
            Navigator.NavigateTo(AdminPageDirectory.NewLippincott, string.Empty, string.Format("{0}={1}&{2}={3}&{4}={5}&{6}={7}&CMS=1",
              QUERY_PARAM_LIPINCOTT_ID, Id, QUERY_PARAM_SEARCH_CONDITION, View.SearchCondition, QUERY_PARAM_SORT, View.Sort, QUERY_PARAM_MODE, "4"));
        }

        private bool IsValidQuestionId(string questionId, out int qId)
        {
            bool isValid = false;
            qId = 0;
            Question question = _cmsService.GetQuestionByQuestionId(questionId);
            if (question != null && question.Id != 0)
            {
                qId = question.Id;
                isValid = true;
            }

            return isValid;
        }

        private Remediation GetRemeditionById(string remediationId)
        {
            Remediation remediation = new Remediation();
            IEnumerable<Remediation> remediations = _cmsService.GetRemediations();
            if (remediations != null)
            {
                remediation = (Remediation)(from rem in remediations
                                            where rem.TopicTitle == remediationId
                                            select rem).FirstOrDefault();
            }

            if (remediation == null)
            {
                remediation = new Remediation();
            }

            return remediation;
        }
    }
}
