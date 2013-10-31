using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class QuestionPresenter : AuthenticatedPresenterBase<IQuestionView>
    {
        private const string QUERY_PARAM_URLQUERY = "urlQuery";
        private const string QUERY_PARAM_SEARCHBACK = "searchback";
        private const string Query_PARAM_NAV_ACTION = "navAction";
        private const string QUERY_PARAM_MODE = "mode";
        private const string QUERY_PARAM_TESTID = "TestID";
        private const string QUERT_PARAM_COMEFROM = "ComeFrom";
        private const string Query_PARAM_QID = "QID";
        private const string Query_PARAM_ACTION = "Action";
        private const string Query_PARAM_NAV_QID = "navQID";
        private const string notSelected = "not selected";
        private readonly ICMSService _cmsService;
        private readonly IAdminService _adminService;
        private readonly IReportDataService _reportDataService;
        private ViewMode _mode;
        private string _urlQuery = string.Empty;

        public QuestionPresenter(ICMSService service, IAdminService adminService, IReportDataService reportDataService)
            : base(Module.CMS)
        {
            _cmsService = service;
            _adminService = adminService;
            _reportDataService = reportDataService;
        }

        public void SearchQuestions(QuestionCriteria searchCriteria, string sortMetaData)
        {
            // _cmsService.CheckThis();
            View.ShowSearchQuestionResults(_cmsService.SearchQuestions(searchCriteria), SortHelper.Parse(sortMetaData));
        }

        public void SearchRemediations(QuestionCriteria searchCriteria)
        {
            View.ShowSearchRemediationResults(_cmsService.SearchRemediaton(searchCriteria));
        }

        public override void RegisterAuthorizationRules()
        {
            if (_mode != ViewMode.Edit)
            {
                return;
            }

            RegisterAuthorizationRule(UserAction.Add);
            RegisterAuthorizationRule(UserAction.Edit);
            RegisterAuthorizationRule(UserAction.Delete);
        }

        public override void RegisterQueryParameters()
        {
            if (_mode != ViewMode.Edit)
            {
                return;
            }

            RegisterQueryParameter(QUERY_PARAM_ACTION_TYPE);
            RegisterQueryParameter(QUERY_PARAM_ID, QUERY_PARAM_ACTION_TYPE, "2");
        }

        public void PreInitialize(ViewMode mode)
        {
            _mode = mode;
        }

        public void GetTests(int productId)
        {
            IEnumerable<Test> tests = _reportDataService.GetTestsByProgramofStudyType(productId, View.ProgramOfStudyId);
            View.PopulateTests(tests);
        }

        public void GetClientNeedsCategory(int clientNeedId, int programofStudyId)
        {
            CategoryName categoryName = CategoryName.ClientNeedCategory;
            if (programofStudyId == (int)ProgramofStudyType.PN)
            {
                categoryName = CategoryName.PNClientNeedCategory;
            }

            var filteredCategories = _adminService.GetCategories()[categoryName].Details
                .Where(c => c.Value.ParentId == clientNeedId)
                .ToDictionary(k => k.Key, v => v.Value);

            View.PopulateClientNeedCategories(filteredCategories);
        }

        public void NavigateToEdit(string questionId, string urlQuery, UserAction actionType)
        {
            if (actionType == UserAction.Add)
            {
                Navigator.NavigateTo(AdminPageDirectory.EditQuestion, string.Empty, string.Format("{0}={1}",
                    QUERY_PARAM_ACTION_TYPE, ((int)actionType).ToString()));
            }
            else if (actionType == UserAction.View)
            {
                Navigator.NavigateTo(AdminPageDirectory.ViewRemediation, string.Empty, string.Format("{0}={1}&{2}={3}",
                    QUERY_PARAM_ACTION_TYPE, ((int)actionType).ToString(), QUERY_PARAM_MODE, 2));
            }
            else
            {
                Navigator.NavigateTo(AdminPageDirectory.EditQuestion, string.Empty, string.Format("{0}={1}&{2}={3}&{4}={5}&CMS=1",
                    QUERY_PARAM_ACTION_TYPE, ((int)actionType).ToString(), QUERY_PARAM_ID, questionId, QUERY_PARAM_URLQUERY, urlQuery));
            }
        }

        public void NavigateToEditR(int qId, UserAction useraction)
        {
            if (qId > 0)
            {
                Navigator.NavigateTo(AdminPageDirectory.EditR, string.Empty, string.Format("{0}={1}&{2}={3}&CMS=1",
                    Query_PARAM_QID.ToLower(), qId, Query_PARAM_ACTION.ToLower(), ((int)useraction)));
            }
            else
            {
                Navigator.NavigateTo(AdminPageDirectory.EditR, string.Empty, string.Format("{0}={1}&CMS=1",
                    Query_PARAM_ACTION.ToLower(), ((int)useraction)));
            }
        }

        public void NavigateToLippincott()
        {
            Navigator.NavigateTo(AdminPageDirectory.Lippincott, string.Empty, string.Format("CMS=1&{0}={1}", QUERY_PARAM_MODE, "1"));
        }

        public void NavigateToCategory()
        {
            Navigator.NavigateTo(AdminPageDirectory.TestCategories, string.Empty, "CMS=1&mode=1");
        }

        public void NavigateToSearch(string urlQuery)
        {
            if (String.IsNullOrEmpty(urlQuery))
            {
                Navigator.NavigateTo(AdminPageDirectory.SearchQuestion, string.Empty, string.Format("{0}={1}&CMS=1",
                    QUERY_PARAM_SEARCHBACK, 0));
            }
            else
            {
                Navigator.NavigateTo(AdminPageDirectory.SearchQuestion, string.Empty, string.Format("{0}={1}&{2}={3}&{4}={5}&CMS=1",
                    QUERY_PARAM_ACTION_TYPE, 1, QUERY_PARAM_URLQUERY, urlQuery, QUERY_PARAM_SEARCHBACK, 1));
            }
        }

        public void NavigateToEdit(UserAction actionType)
        {
            NavigateToEdit(GetParameterValue(QUERY_PARAM_ID), string.Empty, actionType);
        }

        public void NavigateToUploadQuestions()
        {
            Navigator.NavigateTo(AdminPageDirectory.UploadQuestions, string.Empty, "CMS=1");
        }

        public void ShowQuestionDetails()
        {
            var _fileTypes = QuestionHelper.GetFileTypes();
            var _questionTypes = QuestionHelper.GetQuestionTypes();
            var _actionValue = GetParameterValue(QUERY_PARAM_ACTION_TYPE);

            if (IsQueryParameterExist(QUERY_PARAM_URLQUERY))
            {
                View.URLQuery = GetParameterValue(QUERY_PARAM_URLQUERY);
            }
            else
            {
                View.URLQuery = string.Empty;
            }

            if (_actionValue == "1")
            {
                ActionType = UserAction.Add;
            }
            else if (_actionValue == "2")
            {
                ActionType = UserAction.Edit;
                View.PopulateQuestion(_cmsService.GetQuestion(Id, true), 2);
            }
            else if (_actionValue.ToInt() == (int)UserAction.Copy)
            {
                ActionType = UserAction.Copy;
                View.PopulateQuestion(_cmsService.GetQuestion(Id, true), (int)UserAction.Copy);
            }

            View.RefreshPage(null, ActionType, _fileTypes, _questionTypes, string.Empty, string.Empty, false, false);
        }

        public void ViewQuestion()
        {
            View.PopulateQuestion(_cmsService.GetQuestion(Id, true), 2);
        }

        public void SetProgramOfStudyId()
        {
            View.ProgramOfStudyId = _cmsService.GetQuestion(Id, true).ProgramofStudyId;
        }

        public void LoadQuestionDetails()
        {
            string _mode = string.Empty;
            string _testId = string.Empty;

            if (IsQueryParameterExist(QUERY_PARAM_MODE))
            {
                _mode = GetParameterValue(QUERY_PARAM_MODE);
            }

            if (IsQueryParameterExist(QUERY_PARAM_TESTID))
            {
                _testId = GetParameterValue(QUERY_PARAM_TESTID);
            }

            if (IsQueryParameterExist(QUERY_PARAM_URLQUERY))
            {
                View.URLQuery = GetParameterValue(QUERY_PARAM_URLQUERY);
            }
            else
            {
                View.URLQuery = string.Empty;
            }

            View.RefreshPage(null, ActionType, null, null, _mode, _testId, false, false);
        }

        public void GetTestsForQuestion()
        {
            View.PopulateTests(_cmsService.GetTestsForQuestion(Id));
        }

        public void ShowTests(int programofStudyId)
        {
            View.PopulateTests(_cmsService.GetTests(0, Id, true, programofStudyId));
        }

        public void GetAnswer()
        {
            View.PopulateAnswers(_cmsService.GetAnswers(Id, 1));
        }

        public void SaveQuestion(Question question, List<AnswerChoice> lstAnswers)
        {
            List<AnswerChoice> dbAnswerChoice = new List<AnswerChoice>();
            bool IsvalidSave = true;
            string errorMsg = string.Empty;
            if (question.Id > 0)
            {
                dbAnswerChoice = (List<AnswerChoice>)_cmsService.GetAnswers(question.Id, 0);
                Question dbquestion = _cmsService.GetQuestionByQId(question.Id);
                IsvalidSave = ValidateQuestionEdit(dbquestion, dbAnswerChoice, lstAnswers);
                errorMsg = "Deleting/adding answer choices and changing correct answer options is not allowed for released questions.";
            }

            if (IsvalidSave)
            {
                var _qid = _cmsService.SaveQuestion(question, lstAnswers, CurrentContext.UserId, dbAnswerChoice);
                if (_qid > 0)
                {
                    Navigator.NavigateTo(AdminPageDirectory.ViewQuestion, string.Empty, string.Format("{0}={1}&{2}={3}&{4}={5}",
                         QUERY_PARAM_ID, _qid, QUERY_PARAM_URLQUERY, View.URLQuery, QUERY_PARAM_MODE, "2"));
                }
                else if (_qid == -1)
                {
                    errorMsg ="The entered QuestionID already exists please enter another.";
                    IsvalidSave = false;
                }
            }
            
            if(!IsvalidSave)
            {
                View.ShowErrorMessage(errorMsg);
            }
        }

        private bool ValidateQuestionEdit(Question dbquestion, List<AnswerChoice> dbAnswerChoice, List<AnswerChoice> lstAnswers)
        {
            bool IsValidEdit = true;
            if (dbquestion.ReleasedBy > 0)
            {
                if (dbAnswerChoice.Count != lstAnswers.Count)
                {
                    IsValidEdit = false;
                }
                else if (dbAnswerChoice.Where(y => lstAnswers.Any(z => (z.Correct != y.Correct && z.Aindex == y.Aindex))).Count() > 0 && (dbquestion.QuestionType.ToInt()  == (int)QuestionType.MultiChoiceSingleAnswer || dbquestion.QuestionType.ToInt()  == (int)QuestionType.MultiChoiceMultiAnswer))
                {
                    IsValidEdit = false;
                }
                else if (dbAnswerChoice.Where(y => lstAnswers.Any(z => (z.InitialPosition != y.InitialPosition && z.Aindex == y.Aindex))).Count() > 0 && dbquestion.QuestionType.ToInt() == (int)QuestionType.Order)
                {
                    IsValidEdit = false;
                }
                else if (dbquestion.QuestionType.ToInt() == (int) QuestionType.Number && (dbAnswerChoice[0].Atext != lstAnswers[0].Atext ||
                         dbAnswerChoice[0].AlternateAText != lstAnswers[0].AlternateAText))
                {
                    IsValidEdit = false;
                }
                else if (dbquestion.QuestionType.ToInt() == (int)QuestionType.Hotspot && dbAnswerChoice[0].Atext != lstAnswers[0].Atext)
                {
                    IsValidEdit = false;
                }
            }

            return IsValidEdit;
        }

        public void DeleteQuestion(int questionId, string urlQuery)
        {
            _cmsService.DeleteQuestion(questionId);
            if (String.IsNullOrEmpty(urlQuery))
            {
                Navigator.NavigateTo(AdminPageDirectory.SearchQuestion, string.Empty, string.Format("{0}={1}",
                    QUERY_PARAM_SEARCHBACK, "0"));
            }
            else
            {
                Navigator.NavigateTo(AdminPageDirectory.SearchQuestion, string.Empty, string.Format("{0}={1}&{2}={3}",
                        QUERY_PARAM_SEARCHBACK, "0", QUERY_PARAM_SEARCHBACK, urlQuery));
            }
        }

        public void NavigateToViewQuestion(string urlQuery, string questionId)
        {
            if (String.IsNullOrEmpty(urlQuery))
            {
                Navigator.NavigateTo(AdminPageDirectory.ViewQuestion, string.Empty, string.Format("{0}={1}",
                    QUERY_PARAM_ID, questionId));
            }
            else
            {
                Navigator.NavigateTo(AdminPageDirectory.ViewQuestion, string.Empty, string.Format("{0}={1}&{2}={3}",
                        QUERY_PARAM_ID, questionId, QUERY_PARAM_URLQUERY, urlQuery));
            }
        }

        public void NavigateToRemediation(string urlQuery, string questionId)
        {
            Navigator.NavigateTo(AdminPageDirectory.ViewRemediation, string.Empty,
                string.Format("{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}&{10}={11}&{12}={13}",
                QUERT_PARAM_COMEFROM, "EditQuestion.aspx", QUERY_PARAM_URLQUERY, urlQuery, QUERY_PARAM_ID, Id, Query_PARAM_NAV_ACTION, UserAction.View, Query_PARAM_QID, questionId, Query_PARAM_NAV_QID, "-1", QUERY_PARAM_MODE, "4"));
        }

        public void AssignQuestion(List<Test> lstTest)
        {
            _cmsService.AssignQuestion(lstTest);
        }

        public void GetNextQuestion(int userTestId, int questionNumber, string typeOfFileId)
        {
            View.PopulateQuestion(_cmsService.GetNextQuestion(userTestId, questionNumber, typeOfFileId).FirstOrDefault(), 1);
        }

        public void GetPreviousQuestion(int userTestId, int questionNumber, string typeOfFileId)
        {
            View.PopulateQuestion(_cmsService.GetPreviousQuestion(userTestId, questionNumber, typeOfFileId).FirstOrDefault(), 1);
        }

        public void InitializeSearchParameters(int programofStudy)
        {
            View.PopulateSearchQuestionCriteria(_adminService.GetProducts(), _cmsService.GetTitles(), _adminService.GetCategories(programofStudy), programofStudy);
        }

        public void InitializeQuestionParameter()
        {
            View.PopulateInitialQuestionParameters(_cmsService.GetTitles(), _cmsService.GetProgramofStudies());
        }

        public void PopulateAlternateTextDetails(UserAction actionType)
        {
            View.PopulateAlternateTextDetails(_cmsService.GetQuestion(Id, true), ActionType);
        }

        public void InitializeParametersQuestion()
        {
            View.PopulateSearchQuestionCriteria(_adminService.GetProducts(), _cmsService.GetTitles(), _adminService.GetCategories(), 0);
        }

        public bool IsQuestionIdExists(string questionId)
        {
            return _cmsService.IsQuestionIdExist(questionId);
        }

        public void DisplayUploadedQuestions(List<UploadQuestionDetails> uploadQuestions, int fileType, string filePath)
        {
            IDictionary<CategoryName, Category> categoryDatas = _adminService.GetCategories();
            IEnumerable<Topic> topicTitles = _cmsService.GetTitles();
            Dictionary<string, string> QuestionfileType = QuestionHelper.GetFileTypes();

            List<string> qids = new List<string>();
            bool isValid = true;
            foreach (UploadQuestionDetails uq in uploadQuestions)
            {
                SetQuestionType(uq.Question);
                isValid = ValidateQuestion(uq, categoryDatas, topicTitles, QuestionfileType, qids);
                if (isValid)
                {
                    qids.Add(uq.Question.QuestionId);
                }
            }

            View.DisplayUploadedQuestions(uploadQuestions, fileType, filePath);
        }

        public bool SaveUploadedQuestions(List<UploadQuestionDetails> uploadQuestions, int fileType, string filePath)
        {
            IDictionary<CategoryName, Category> categoryDatas = _adminService.GetCategories();
            IEnumerable<Topic> topicTitles = _cmsService.GetTitles();
            Dictionary<string, string> QuestionfileType = QuestionHelper.GetFileTypes();
            List<string> qids = new List<string>();
            bool isAllQuestionValid = true;
            foreach (UploadQuestionDetails uq in uploadQuestions)
            {
                SetQuestionType(uq.Question);
                if (!ValidateQuestion(uq, categoryDatas, topicTitles, QuestionfileType, qids))
                {
                    isAllQuestionValid = false;

                    break;
                }
                else
                {
                    qids.Add(uq.Question.QuestionId);
                }
            }

            if (isAllQuestionValid)
            {
                foreach (UploadQuestionDetails uq in uploadQuestions)
                {
                    ChangeDropdownValueTextToId(uq.Question, categoryDatas, topicTitles, QuestionfileType);
                    uq.Question.Active = 1;
                    uq.Question.ReadingCategoryId = "0";
                    uq.Question.ReadingId = "0";
                    uq.Question.WritingCategoryId = "0";
                    uq.Question.WritingId = "0";
                    uq.Question.MathCategoryId = "0";
                    uq.Question.MathId = "0";
                    uq.Question.ReleaseStatus = "E";
                    uq.Question.ItemTitle = string.Empty;
                    uq.Question.ExhibitTab1 = string.Empty;
                    uq.Question.ExhibitTab2 = string.Empty;
                    uq.Question.ExhibitTab3 = string.Empty;
                    uq.Question.ExhibitTitle1 = string.Empty;
                    uq.Question.ExhibitTitle2 = string.Empty;
                    uq.Question.ExhibitTitle3 = string.Empty;
                    uq.Question.ListeningFileUrl = string.Empty;
                    if (uq.Question.Type == QuestionType.MultiChoiceSingleAnswer)
                    {
                        uq.Question.QuestionType = "01";
                        uq.Answers[uq.Question.CorrectAnswer.ToInt() - 1].Correct = 1;
                    }
                    else if (uq.Question.Type == QuestionType.MultiChoiceMultiAnswer)
                    {
                        uq.Question.QuestionType = "02";
                        string[] correctAnswers = uq.Question.CorrectAnswer.Trim(',').Split(',');
                        foreach (string ca in correctAnswers)
                        {
                            uq.Answers[ca.ToInt() - 1].Correct = 1;
                        }
                    }
                    else if (uq.Question.Type == QuestionType.Number)
                    {
                        uq.Question.QuestionType = "04";
                    }

                    uq.Question.ProgramofStudyId = uq.Question.ProgramofStudyName.ToUpper() == ProgramofStudyType.RN.ToString()
                                           ? (int)ProgramofStudyType.RN
                                           : (int)ProgramofStudyType.PN;
                }

                _cmsService.SaveUploadedQuestions(uploadQuestions, CurrentContext.UserId);
            }
            else
            {
                View.DisplayUploadedQuestions(uploadQuestions, fileType, filePath);
            }

            return isAllQuestionValid;
        }

        public void SaveUploadedQuestionDetails(string GUID, string fileName)
        {
            _cmsService.SaveUploadedQuestionDetails(GUID, fileName, CurrentContext.UserId);
        }

        public void GetCategories(int programofstudyId)
        {
            View.PopulateSearchQuestionCriteria(_adminService.GetProducts(), _cmsService.GetTitles(), _adminService.GetCategories(programofstudyId), programofstudyId);
        }

        private void SetQuestionType(Question question)
        {
            Dictionary<string, string> hashedQuestionType = QuestionHelper.GetQuestionTypes();
            if (hashedQuestionType.ContainsValue(question.QuestionType))
            {
                if (hashedQuestionType["01"] == question.QuestionType)
                {
                    question.Type = QuestionType.MultiChoiceSingleAnswer;
                }
                else if (hashedQuestionType["02"] == question.QuestionType)
                {
                    question.Type = QuestionType.MultiChoiceMultiAnswer;
                }
                else if (hashedQuestionType["04"] == question.QuestionType)
                {
                    question.Type = QuestionType.Number;
                }
            }
        }

        private bool ValidateQuestion(UploadQuestionDetails uploadQuestion, IDictionary<CategoryName, Category> categoryDatas, IEnumerable<Topic> TopicTitles, Dictionary<string, string> fileType, List<string> uploadedQuestionIds)
        {
            ////List<string> errorMessage = new List<string>();
            Question question = uploadQuestion.Question;
            bool isValid = true;

            if (uploadQuestion.ErrorMessage == null)
            {
                uploadQuestion.ErrorMessage = new List<string>();
            }

            if (string.IsNullOrEmpty(question.QuestionType))
            {
                isValid = false;
                uploadQuestion.ErrorMessage.Add("Question type is Empty. Please use new question template.");
            }
            else if (question.Type != QuestionType.MultiChoiceMultiAnswer && question.Type != QuestionType.MultiChoiceSingleAnswer && question.Type != QuestionType.Number)
            {
                isValid = false;
                uploadQuestion.ErrorMessage.Add("Question type is invalid.");
            }
            else if (!(question.ProgramofStudyName.ToUpper() == ProgramofStudyType.RN.ToString() ||
                     question.ProgramofStudyName.ToUpper() == ProgramofStudyType.PN.ToString()))
            {
                isValid = false;
                uploadQuestion.ErrorMessage.Add("Program of study name is invalid.");
            }

            if (isValid)
            {
                if (string.IsNullOrEmpty(question.QuestionId))
                {
                    isValid = false;
                    uploadQuestion.ErrorMessage.Add("Question Id is mandatory.");
                }
                else if (IsQuestionIdExists(question.QuestionId))
                {
                    isValid = false;
                    uploadQuestion.ErrorMessage.Add("Question with same question id already exists.");
                }
                else if (uploadedQuestionIds.Contains(question.QuestionId))
                {
                    isValid = false;
                    uploadQuestion.ErrorMessage.Add("One of the uploaded template has same question id.");
                }

                if (string.IsNullOrEmpty(question.Stem))
                {
                    isValid = false;
                    uploadQuestion.ErrorMessage.Add("Stem is mandatory.");
                }

                if (uploadQuestion.Answers == null)
                {
                    isValid = false;
                    uploadQuestion.ErrorMessage.Add("Please enter answers.");
                }
                else if ((question.Type == QuestionType.MultiChoiceMultiAnswer || question.Type == QuestionType.MultiChoiceSingleAnswer) && !IsMinimumAnswersEntered(uploadQuestion.Answers))
                {
                    isValid = false;
                    uploadQuestion.ErrorMessage.Add("Please enter atleast 2 answer choices.");
                }
                else if (question.Type == QuestionType.Number && (string.IsNullOrEmpty(uploadQuestion.Answers.ToString()) && string.IsNullOrEmpty(question.Unit)))
                {
                    isValid = false;
                    uploadQuestion.ErrorMessage.Add("Please enter valid correct answer.");
                }
                else if (question.Type == QuestionType.MultiChoiceSingleAnswer && (question.CorrectAnswer.ToInt() <= 0 || question.CorrectAnswer.ToInt() > 6 || (uploadQuestion.Answers != null && question.CorrectAnswer.ToInt() > uploadQuestion.Answers.Count)))
                {
                    isValid = false;
                    uploadQuestion.ErrorMessage.Add("Please enter valid correct answer.");
                }
                else if (question.Type == QuestionType.MultiChoiceMultiAnswer && !ValidateCorrectAnswer(uploadQuestion))
                {
                    isValid = false;
                    uploadQuestion.ErrorMessage.Add("Please enter valid correct answer.");
                }
                else if (question.Type == QuestionType.Number && (uploadQuestion.Answers == null || uploadQuestion.Answers.Count() == 0 || String.IsNullOrEmpty(uploadQuestion.Answers[0].Atext)))
                {
                    isValid = false;
                    uploadQuestion.ErrorMessage.Add("Please enter answer choice.");
                }

                if (question.Type == QuestionType.Number && (uploadQuestion.Answers == null || uploadQuestion.Answers.Count() == 0 || String.IsNullOrEmpty(uploadQuestion.Answers[0].Unit)))
                {
                    isValid = false;
                    uploadQuestion.ErrorMessage.Add("Please enter unit.");
                }

                if ((categoryDatas[question.ProgramofStudyName.ToUpper() == ProgramofStudyType.RN.ToString() ? CategoryName.ClientNeeds : CategoryName.PNClientNeeds].Details.Values.Where(c => c.Description.ToLower().Trim() == question.ClientNeedsId.ToLower().Trim()).Count() == 0) && question.ClientNeedsId.ToLower().Trim() != notSelected)
                {
                    isValid = false;
                    uploadQuestion.ErrorMessage.Add("Client Needs is invalid.");
                }
                else if (!IsValidSubcategory(question, categoryDatas))
                {
                    isValid = false;
                    uploadQuestion.ErrorMessage.Add("Client Needs Category is invalid.");
                }

                if ((categoryDatas[question.ProgramofStudyName.ToUpper() == ProgramofStudyType.RN.ToString() ? CategoryName.NursingProcess : CategoryName.PNNursingProcess].Details.Values.Where(c => c.Description.ToLower().Trim() == question.NursingProcessId.ToLower().Trim()).Count() == 0) && question.NursingProcessId.ToLower().Trim() != notSelected)
                {
                    isValid = false;
                    uploadQuestion.ErrorMessage.Add("Nursingprocess is invalid");
                }

                if ((categoryDatas[question.ProgramofStudyName.ToUpper() == ProgramofStudyType.RN.ToString() ? CategoryName.Demographic : CategoryName.PNDemographic].Details.Values.Where(c => c.Description.ToLower().Trim() == question.DemographicId.ToLower().Trim()).Count() == 0) && question.DemographicId.ToLower().Trim() != notSelected)
                {
                    isValid = false;
                    uploadQuestion.ErrorMessage.Add("Demographic is invalid");
                }

                if ((categoryDatas[question.ProgramofStudyName.ToUpper() == ProgramofStudyType.RN.ToString() ? CategoryName.CognitiveLevel : CategoryName.PNCognitiveLevel].Details.Values.Where(c => c.Description.ToLower().Trim() == question.CognitiveLevelId.ToLower().Trim()).Count() == 0) && question.CognitiveLevelId.ToLower().Trim() != notSelected)
                {
                    isValid = false;
                    uploadQuestion.ErrorMessage.Add("CognitiveLevel is invalid");
                }

                if ((categoryDatas[question.ProgramofStudyName.ToUpper() == ProgramofStudyType.RN.ToString() ? CategoryName.LevelOfDifficulty : CategoryName.PNLevelOfDifficulty].Details.Values.Where(c => c.Description.ToLower().Trim() == question.LevelOfDifficultyId.ToLower().Trim()).Count() == 0) && question.LevelOfDifficultyId.ToLower().Trim() != notSelected)
                {
                    isValid = false;
                    uploadQuestion.ErrorMessage.Add("LevelOfDifficulty is invalid");
                }

                if ((categoryDatas[question.ProgramofStudyName.ToUpper() == ProgramofStudyType.RN.ToString() ? CategoryName.Systems : CategoryName.PNSystems].Details.Values.Where(c => c.Description.ToLower().Trim() == question.SystemId.ToLower().Trim()).Count() == 0) && question.SystemId.ToLower().Trim() != notSelected)
                {
                    isValid = false;
                    uploadQuestion.ErrorMessage.Add("System is invalid");
                }

                if ((categoryDatas[question.ProgramofStudyName.ToUpper() == ProgramofStudyType.RN.ToString() ? CategoryName.SpecialtyArea : CategoryName.PNSpecialtyArea].Details.Values.Where(c => c.Description.ToLower().Trim() == question.SpecialtyAreaId.ToLower().Trim()).Count() == 0) && question.SpecialtyAreaId.ToLower().Trim() != notSelected)
                {
                    isValid = false;
                    uploadQuestion.ErrorMessage.Add("SpecialtyArea is invalid");
                }

                if ((categoryDatas[question.ProgramofStudyName.ToUpper() == ProgramofStudyType.RN.ToString() ? CategoryName.ClinicalConcept : CategoryName.PNClinicalConcept].Details.Values.Where(c => c.Description.ToLower().Trim() == question.ClinicalConceptsId.ToLower().Trim()).Count() == 0) && question.ClinicalConceptsId.ToLower().Trim() != notSelected)
                {
                    isValid = false;
                    uploadQuestion.ErrorMessage.Add("ClinicalConcept is invalid");
                }

                if ((categoryDatas[question.ProgramofStudyName.ToUpper() == ProgramofStudyType.RN.ToString() ? CategoryName.CriticalThinking : CategoryName.PNCriticalThinking].Details.Values.Where(c => c.Description.ToLower().Trim() == question.CriticalThinkingId.ToLower().Trim()).Count() == 0) && question.ClinicalConceptsId.ToLower().Trim() != notSelected)
                {
                    isValid = false;
                    uploadQuestion.ErrorMessage.Add("CriticalThinking is invalid");
                }

                if (!string.IsNullOrEmpty(question.TopicTitleId.Trim()) && TopicTitles.Where(t => t.TopicTitle.ToLower().Trim() == question.TopicTitleId.ToLower().Trim()).Count() == 0)
                {
                    isValid = false;
                    uploadQuestion.ErrorMessage.Add("Please enter valid topic title.");
                }

                if (fileType.Where(r => r.Value == question.TypeOfFileId).Count() <= 0)
                {
                    isValid = false;
                    uploadQuestion.ErrorMessage.Add("Please enter valid file type.");
                }
            }

            uploadQuestion.IsValid = isValid;
            return isValid;
        }

        private bool ValidateCorrectAnswer(UploadQuestionDetails uploadQuestion)
        {
            bool isValid = true;
            Question question = uploadQuestion.Question;

            if (!string.IsNullOrEmpty(question.CorrectAnswer) && uploadQuestion.Answers != null)
            {
                string[] correctAnswers = question.CorrectAnswer.TrimEnd(',').Split(',');
                foreach (string answer in correctAnswers)
                {
                    if (answer.ToInt() <= 0 || answer.ToInt() > 6 || answer.ToInt() > uploadQuestion.Answers.Count)
                    {
                        isValid = false;
                        break;
                    }
                }
            }
            else
            {
                isValid = false;
            }

            return isValid;
        }

        private bool IsMinimumAnswersEntered(List<AnswerChoice> answerChoices)
        {
            int count = 0;
            foreach (AnswerChoice ac in answerChoices)
            {
                if (!string.IsNullOrEmpty(ac.Atext.Trim()))
                {
                    count++;
                }
            }

            return count >= 2;
        }

        private bool IsValidSubcategory(Question question, IDictionary<CategoryName, Category> categoryDatas)
        {
            bool isValid = true;
            CategoryDetail categoryDetail = categoryDatas[question.ProgramofStudyName.ToUpper() == ProgramofStudyType.RN.ToString() ? CategoryName.ClientNeeds : CategoryName.PNClientNeeds].Details.Values.Where(c => c.Description.ToLower().Trim() == question.ClientNeedsId.ToLower().Trim()).SingleOrDefault();
            if (question.ClientNeedsId.ToLower().Trim() == notSelected && question.ClientNeedsCategoryId.ToLower().Trim() != notSelected)
            {
                isValid = false;
            }
            else if (question.ClientNeedsCategoryId != notSelected && categoryDetail != null)
            {
                CategoryDetail subcategory = categoryDatas[question.ProgramofStudyName.ToUpper() == ProgramofStudyType.RN.ToString() ? CategoryName.ClientNeedCategory : CategoryName.PNClientNeedCategory].Details.Values.Where(c => c.Description.ToLower().Trim() == question.ClientNeedsCategoryId.ToLower().Trim() && c.ParentId == categoryDetail.Id).SingleOrDefault();
                if (subcategory == null)
                {
                    isValid = false;
                }
            }

            return isValid;
        }

        private bool IsQueryParameterExist(string key)
        {
            bool keyExist = false;
            string[] keys = HttpContext.Current.Request.QueryString.AllKeys;
            if (keys != null && keys.Count() > 0)
            {
                List<string> keylist = keys.ToList<string>();
                keyExist = keylist.Contains(key);
            }

            return keyExist;
        }

        private void ChangeDropdownValueTextToId(Question question, IDictionary<CategoryName, Category> categoryDatas, IEnumerable<Topic> TopicTitles, Dictionary<string, string> fileType)
        {
            question.ClientNeedsId = GetSelectedId(categoryDatas[question.ProgramofStudyName.ToUpper() == ProgramofStudyType.RN.ToString() ? CategoryName.ClientNeeds : CategoryName.PNClientNeeds].Details, question.ClientNeedsId);
            question.ClientNeedsCategoryId = GetSelectedId(categoryDatas[question.ProgramofStudyName.ToUpper() == ProgramofStudyType.RN.ToString() ? CategoryName.ClientNeedCategory : CategoryName.PNClientNeedCategory].Details, question.ClientNeedsCategoryId);
            question.NursingProcessId = GetSelectedId(categoryDatas[question.ProgramofStudyName.ToUpper() == ProgramofStudyType.RN.ToString() ? CategoryName.NursingProcess : CategoryName.PNNursingProcess].Details, question.NursingProcessId);
            question.DemographicId = GetSelectedId(categoryDatas[question.ProgramofStudyName.ToUpper() == ProgramofStudyType.RN.ToString() ? CategoryName.Demographic : CategoryName.PNDemographic].Details, question.DemographicId);
            question.CognitiveLevelId = GetSelectedId(categoryDatas[question.ProgramofStudyName.ToUpper() == ProgramofStudyType.RN.ToString() ? CategoryName.CognitiveLevel : CategoryName.PNCognitiveLevel].Details, question.CognitiveLevelId);
            question.LevelOfDifficultyId = GetSelectedId(categoryDatas[question.ProgramofStudyName.ToUpper() == ProgramofStudyType.RN.ToString() ? CategoryName.LevelOfDifficulty : CategoryName.PNLevelOfDifficulty].Details, question.LevelOfDifficultyId);
            question.SystemId = GetSelectedId(categoryDatas[question.ProgramofStudyName.ToUpper() == ProgramofStudyType.RN.ToString() ? CategoryName.Systems : CategoryName.PNSystems].Details, question.SystemId);
            question.SpecialtyAreaId = GetSelectedId(categoryDatas[question.ProgramofStudyName.ToUpper() == ProgramofStudyType.RN.ToString() ? CategoryName.SpecialtyArea : CategoryName.PNSpecialtyArea].Details, question.SpecialtyAreaId);
            question.ClinicalConceptsId = GetSelectedId(categoryDatas[question.ProgramofStudyName.ToUpper() == ProgramofStudyType.RN.ToString() ? CategoryName.ClinicalConcept : CategoryName.PNClinicalConcept].Details, question.ClinicalConceptsId);
            question.CriticalThinkingId = GetSelectedId(categoryDatas[question.ProgramofStudyName.ToUpper() == ProgramofStudyType.RN.ToString() ? CategoryName.CriticalThinking : CategoryName.PNCriticalThinking].Details, question.CriticalThinkingId);
            question.ConceptsId = GetSelectedId(categoryDatas[question.ProgramofStudyName.ToUpper() == ProgramofStudyType.RN.ToString() ? CategoryName.Concepts : CategoryName.PNConcepts].Details, question.ConceptsId);
            if (question.ProgramofStudyName.ToUpper() == ProgramofStudyType.RN.ToString())
            {
                question.AccreditationCategoriesId = GetSelectedId(categoryDatas[CategoryName.AccreditationCategories].Details, question.AccreditationCategoriesId);
                question.QSENKSACompetenciesId = GetSelectedId(categoryDatas[CategoryName.QSENKSACompetencies].Details, question.QSENKSACompetenciesId);
            }

            IEnumerable<Topic> topics = TopicTitles.Where(t => t.TopicTitle.ToLower().Trim() == question.TopicTitleId.ToLower().Trim());
            Topic topic = topics.FirstOrDefault();
            if (topic != null)
            {
                question.RemediationId = topic.RemediationId.ToInt();
            }

            KeyValuePair<string, string> file = fileType.Where(r => r.Value == question.TypeOfFileId).FirstOrDefault();
            question.TypeOfFileId = file.Key;
        }

        private string GetSelectedId(IDictionary<int, CategoryDetail> dicCategoryDetails, string questionEntityvalue)
        {
            if (questionEntityvalue.ToLower().Trim() == notSelected)
            {
                questionEntityvalue = "0";
            }
            else
            {
                questionEntityvalue = dicCategoryDetails.Values.Where(c => c.Description.ToLower().Trim() == questionEntityvalue.ToLower().Trim()).Single().Id.ToString();
            }

            return questionEntityvalue;
        }
    }
}
