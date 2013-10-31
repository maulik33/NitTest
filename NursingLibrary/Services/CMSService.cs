using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.DomainServices;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Services
{
    public class CMSService : ICMSService
    {
        #region Fields

        private readonly ICMSRepository _cmsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitOfWork _liveProdUnitOfWork;
        private readonly IAdminService _adminService;
        private readonly IReportDataService _reportDataservice;

        #endregion Fields

        #region Constructors
        public CMSService(ICMSRepository cmsRepository, IUnitOfWork unitOfWork, IAdminService adminService,
            IReportDataService reportService, IUnitOfWork liveProdUnitOfWork)
        {
            _cmsRepository = cmsRepository;
            _unitOfWork = unitOfWork;
            _adminService = adminService;
            _reportDataservice = reportService;
            _liveProdUnitOfWork = liveProdUnitOfWork;
        }

        #endregion Constructors

        #region Properties

        public ICacheManagement CacheManager { get; set; }

        #endregion Properties

        public IEnumerable<QuestionResult> SearchQuestions(QuestionCriteria searchCriteria)
        {
            return _cmsRepository.SearchQuestions(searchCriteria);
        }

        public IEnumerable<Remediation> SearchRemediaton(QuestionCriteria searchCriteria)
        {
            return _cmsRepository.SearchRemediation(searchCriteria);
        }

        public Test GetAVPItemByTestId(int testId)
        {
            return _cmsRepository.GetAVPItems(testId, 4, 10).SingleOrDefault();
        }

        public IEnumerable<Test> SearchAVPItems(string testName, int programOfStudyId)
        {
            return _cmsRepository.SearchAVPItems(testName, programOfStudyId);
        }

        public void DeleteTestById(int id)
        {
            _cmsRepository.DeleteTestById(id);
        }

        public void SaveAVPItems(Test test)
        {
            _cmsRepository.SaveAVPItems(test);
        }

        public void UpdateTestsReleaseStatusById(int testId, string releaseStatus)
        {
            _cmsRepository.UpdateTestsReleaseStatusById(testId, releaseStatus);
        }

        public IEnumerable<Norming> GetNormings(int testId)
        {
            return _cmsRepository.GetNormings(testId, string.Empty);
        }

        public void DeleteNormingById(int id)
        {
            _cmsRepository.DeleteNormingById(id);
        }

        public void SaveNorming(Norming norming)
        {
            _cmsRepository.SaveNorming(norming);
        }

        public IEnumerable<Product> GetListOfAllProducts()
        {
            return _adminService.GetProducts();
        }

        public IEnumerable<Test> GetTests(int productId)
        {
            return _adminService.GetTests(productId, string.Empty);
        }

        public IEnumerable<Test> GetTests(int productId, int programofStudyId)
        {
            return _adminService.GetTests(productId, programofStudyId);
        }

        public IEnumerable<Test> GetTests(int productId, int questionId, bool forCMS, int programofStudy)
        {
            return _adminService.GetTests(productId, questionId, string.Empty, forCMS, programofStudy);
        }

        public IEnumerable<ClientNeedsCategory> GetClientNeedCategory()
        {
            return _cmsRepository.GetClientNeedCategory(0);
        }

        public IEnumerable<ClientNeedsCategory> GetClientNeedCategory(int clinetNeedId)
        {
            return _cmsRepository.GetClientNeedCategory(clinetNeedId);
        }

        public IEnumerable<Norm> GetNorms(int testId, string chartType)
        {
            return _cmsRepository.GetNorms(testId, chartType, string.Empty);
        }

        public void SaveNorm(Norm norm)
        {
            _cmsRepository.SaveNorm(norm);
        }

        public Lippincott GetLippincottRemediationByID(int lippinCottId)
        {
            return _cmsRepository.GetLippincottRemediationByID(lippinCottId);
        }

        public IEnumerable<Lippincott> SearchLippincotts(string lippinCottTitle)
        {
            return _cmsRepository.SearchLippincotts(lippinCottTitle);
        }

        public void SaveLippinCott(Lippincott lippinCott)
        {
            _cmsRepository.SaveLippinCott(lippinCott);
        }

        public IEnumerable<QuestionLippincott> GetQuestionLippincottByIds(int QID, int lippinCottId)
        {
            return _cmsRepository.GetQuestionLippincottByIds(QID, lippinCottId, string.Empty);
        }

        public IEnumerable<Lippincott> GetLippincottById(int lippinCottId)
        {
            return _cmsRepository.GetLippincotts(lippinCottId, string.Empty);
        }

        public void InsertQuestionLippinCott(int QId, int lippinCottId)
        {
            _cmsRepository.InsertQuestionLippinCott(QId, lippinCottId);
        }

        public IEnumerable<Lippincott> GetQuestionLippincotts(int lippinCottId)
        {
            return _cmsRepository.GetQuestionLippincotts(lippinCottId);
        }

        public void DeleteLippinCott(int lippinCottId)
        {
            _cmsRepository.DeleteLippinCott(lippinCottId);
        }

        public IEnumerable<Remediation> GetRemediations()
        {
            return _cmsRepository.GetRemediations(0, string.Empty);
        }

        public IEnumerable<Remediation> GetRemediationByStatus(string status)
        {
            return _cmsRepository.GetRemediations(0, status);
        }

        public void DeleteLippinCottQuestion(int lippinCottId, int questionId)
        {
            _cmsRepository.DeleteLippinCottQuestion(lippinCottId, questionId);
        }

        public Question GetQuestion(int questionId, bool forEdit)
        {
            return _cmsRepository.GetQuestions(string.Empty, questionId, string.Empty, true, string.Empty).FirstOrDefault();
        }

        public Question GetQuestionByQuestionId(string questionId)
        {
            IEnumerable<Question> question = _cmsRepository.GetQuestions(questionId, 0, string.Empty, false, string.Empty);
            return question.FirstOrDefault();
        }

        public Question GetQuestionByQId(int qId)
        {
            IEnumerable<Question> question = _cmsRepository.GetQuestions(string.Empty, qId, string.Empty, false, string.Empty);
            return question.FirstOrDefault();
        }

        public IEnumerable<Question> GetQuestionByRemediationId(string remediationId)
        {
            return _cmsRepository.GetQuestions(string.Empty, 0, remediationId, false, string.Empty);
        }

        public Question InsertUpdateQuestion(Question question)
        {
            return _cmsRepository.InsertUpdateQuestion(question);
        }

        public Lippincott GetLippincottByRemediationID(int remediationId)
        {
            return _cmsRepository.GetLippincottByRemediationID(remediationId);
        }

        public Remediation GetRemediationById(int remediationId)
        {
            return _cmsRepository.GetRemediations(remediationId, string.Empty).FirstOrDefault();
        }

        public void SaveRemediation(Remediation remediation)
        {
            remediation.ReleaseStatus = "E";
            _cmsRepository.SaveRemediation(remediation);
        }

        public void DeleteRemediation(int remediationId)
        {
            _cmsRepository.DeleteRemediation(remediationId);
        }

        public List<Lippincott> GetLippincotts(int qId)
        {
            return _cmsRepository.GetLippincotts(qId);
        }

        public IEnumerable<Lippincott> GetLippincotts(string releaseStatus)
        {
            return _cmsRepository.GetLippincotts(0, releaseStatus);
        }

        public Test GetTestById(int testId)
        {
            return _cmsRepository.GetCustomTests(testId, 0, string.Empty).SingleOrDefault();
        }

        public void SaveCustomTest(Test test)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    _cmsRepository.SaveCustomTest(test);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }

        public IEnumerable<Test> SearchCustomTests(int programOfStudyId, string testName)
        {
            return _cmsRepository.SearchCustomTests(programOfStudyId, testName);
        }

        public IEnumerable<Test> GetCustomTests(int productId, string testName)
        {
            return _cmsRepository.GetCustomTests(0, productId, testName);
        }

        public void CopyCustomTest(int originalTestId, int newTestId)
        {
            _cmsRepository.CopyCustomTest(originalTestId, newTestId);
        }

        public void DeleteCustomTest(int testId)
        {
            _cmsRepository.DeleteCustomTest(testId);
        }

        public IEnumerable<CategoryDetail> GetTestcategoriesForTestQuestions(int testId, int testType)
        {
            return _cmsRepository.GetTestcategoriesForTestQuestions(testId, testType);
        }

        public void SaveTestCategory(int testId, int categoryId, int student, int admin)
        {
            _cmsRepository.SaveTestCategory(testId, categoryId, student, admin);
        }

        public IEnumerable<TestCategory> GetTestCategories(int testId)
        {
            return _cmsRepository.GetTestCategories(testId, string.Empty);
        }

        public IEnumerable<AnswerChoice> GetAnswers(int questionId, int actionType)
        {
            return _cmsRepository.GetAnswers(questionId, actionType, string.Empty);
        }

        public int SaveQuestion(Question question, List<AnswerChoice> lstAnswers, int adminId, List<AnswerChoice> dbAnswerChoices)
        {
            var _id = 0;
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    _id = _cmsRepository.SaveQuestion(question, adminId);
                    if (question.Id != 0)
                    {
                        //Answer choice editing
                        if (lstAnswers.Any())
                        {
                            //Update db answer table id to newly created list. 
                            SetAnswerIds(lstAnswers, dbAnswerChoices);
                        }

                    }
                    foreach (var answer in lstAnswers)
                    {
                        answer.Question = new Question() { Id = _id };
                        SaveAnswer(answer);
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }

            return _id;
        }

        private void SetAnswerIds(List<AnswerChoice> lstAnswers, List<AnswerChoice> dbAswerChoices)
        {
            lstAnswers = lstAnswers.OrderBy(o => o.Aindex).ToList();
            dbAswerChoices = dbAswerChoices.OrderBy(o => o.Aindex).ToList();
            var answerIdstoDelete = new StringBuilder();
            for (int i = 0; i < dbAswerChoices.Count; i++)
            {
                if (lstAnswers.Count() > i)
                {
                    lstAnswers[i].AnswerId = dbAswerChoices[i].AnswerId;
                }
                else
                {
                    answerIdstoDelete.Append(dbAswerChoices[i].AnswerId + "|");
                }
            }

            _cmsRepository.DeleteAnswerChoices(answerIdstoDelete.ToString().TrimEnd('|'));
        }

        public void SaveUploadedQuestions(List<UploadQuestionDetails> uploadQuestions, int adminId)
        {
            var _id = 0;

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    foreach (UploadQuestionDetails uq in uploadQuestions)
                    {
                        _id = _cmsRepository.SaveQuestion(uq.Question, adminId);
                        foreach (var answer in uq.Answers)
                        {
                            answer.Question = new Question() { Id = _id };
                            SaveAnswer(answer);
                        }
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }

        public void SaveAnswer(AnswerChoice answer)
        {
            _cmsRepository.SaveAnswer(answer);
        }

        public void DeleteQuestion(int questionId)
        {
            _cmsRepository.DeleteQuestion(questionId);
        }

        public void AssignQuestion(List<Test> lstTest)
        {
            foreach (var test in lstTest)
            {
                _cmsRepository.AssignQuestion(test);
            }
        }

        public IEnumerable<Test> GetTestsForQuestion(int questionId)
        {
            return _cmsRepository.GetTestsForQuestion(questionId);
        }

        public IEnumerable<Question> GetNextQuestion(int userTestId, int questionNumber, string typeOfFileId)
        {
            return _cmsRepository.GetNextQuestion(userTestId, questionNumber, typeOfFileId);
        }

        public IEnumerable<Question> GetPreviousQuestion(int userTestId, int questionNumber, string typeOfFileId)
        {
            return _cmsRepository.GetPreviousQuestion(userTestId, questionNumber, typeOfFileId);
        }

        public IEnumerable<Topic> GetTitles()
        {
            return _cmsRepository.GetTitles()
                .OrderBy(r => r.TopicTitle);
        }

        public IEnumerable<Question> GetReleaseQuestions()
        {
            return _cmsRepository.GetReleaseQuestions();
        }

        public IEnumerable<Lippincott> GetReleaseLippinCots()
        {
            return _cmsRepository.GetReleaseLippinCots();
        }

        public IEnumerable<Test> GetReleaseTests(string status)
        {
            return _cmsRepository.GetReleaseTests(status);
        }

        public void UpdateReleaseStatus(string ids, string releaseStatus, string releaseChoice)
        {
            _cmsRepository.UpdateReleaseStatus(ids, releaseStatus, releaseChoice);
        }

        public IEnumerable<QuestionLippincott> GetQuestionLippincottList(int lippincottId)
        {
            return _cmsRepository.GetQuestionLippincottByIds(0, lippincottId, string.Empty);
        }

        public IEnumerable<Question> GetQuestions(string releaseStatus)
        {
            return _cmsRepository.GetQuestions(string.Empty, 0, string.Empty, false, releaseStatus);
        }

        public void ReleaseToProduction(string showQuestion, string showLippinCott, string showTest, int userId)
        {
            IEnumerable<Question> questions = null;
            IEnumerable<Remediation> remediations = null;
            IEnumerable<Lippincott> lippinCotts = null;
            IEnumerable<Test> tests = null;
            bool releasedSuccessfully = false;
            StringBuilder qids = new StringBuilder();
            StringBuilder remediationIds = new StringBuilder();
            StringBuilder lippincottIds = new StringBuilder();
            StringBuilder testIds = new StringBuilder();
            LookupService lookupService = new LookupService(_adminService, _cmsRepository);

            using (var transaction = _liveProdUnitOfWork.BeginTransaction())
            {
                if (showQuestion == "y")
                {
                    questions = GetQuestions("A");
                    remediations = GetRemediationByStatus("A");
                    if (questions != null)
                    {
                        string questionIds = string.Join("|", questions.Select(q => q.Id));
                        foreach (Question q in questions)
                        {
                            q.ReleasedBy = userId;
                            q.ReleaseDate = DateTime.Now;
                            _cmsRepository.ReleaseQuestions(q, userId);
                            _cmsRepository.UpdateQuestionLog(q);
                            _cmsRepository.DeleteQuestionMappingByQId(q.Id);
                            lookupService.SyncQuestionLookupData(q);
                            qids.Append(q.Id + "|");
                        }

                        if (qids.Length > 1)
                        {
                            IEnumerable<AnswerChoice> answerChoices = _cmsRepository.GetAnswers(0, 0, qids.ToString());

                            foreach (AnswerChoice ac in answerChoices)
                            {
                                _cmsRepository.ReleaseAnswerChoice(ac);
                            }
                        }
                    }

                    if (remediations != null)
                    {
                        foreach (Remediation r in remediations)
                        {
                            _cmsRepository.ReleaseRemediation(r);
                            remediationIds.Append(r.RemediationId + "|");
                        }
                    }
                }

                if (showLippinCott == "y")
                {
                    lippinCotts = GetLippincotts("A");

                    foreach (Lippincott lc in lippinCotts)
                    {
                        _cmsRepository.ReleaseLippincott(lc);
                        lippincottIds.Append(lc.LippincottID + "|");
                    }

                    if (lippincottIds.Length > 1)
                    {
                        IEnumerable<QuestionLippincott> questionLippincots = _cmsRepository.GetQuestionLippincottByIds(0, 0, lippincottIds.ToString());
                        _cmsRepository.DeleteReleaseQuestionLippinCott(lippincottIds.ToString());
                        foreach (QuestionLippincott ql in questionLippincots)
                        {
                            _cmsRepository.ReleaseQuestionLippincott(ql);
                        }
                    }
                }

                if (showTest == "y")
                {
                    tests = GetReleaseTests("A");
                    if (tests != null)
                    {
                        foreach (Test t in tests)
                        {
                            _cmsRepository.ReleaseTests(t);
                            testIds.Append(t.TestId + "|");
                        }

                        if (testIds.Length > 1)
                        {
                            IEnumerable<TestQuestion> currentTestQuestions = _cmsRepository.GetProdTestQuestions(0, testIds.ToString());
                            IEnumerable<TestCategory> testCategories = _cmsRepository.GetTestCategories(0, testIds.ToString());
                            IEnumerable<TestQuestion> testQuestions = _cmsRepository.GetTestQuestions(0, testIds.ToString());
                            IEnumerable<Norming> normings = _cmsRepository.GetNormings(0, testIds.ToString());
                            IEnumerable<Norm> norms = _cmsRepository.GetNorms(0, string.Empty, testIds.ToString());
                            _cmsRepository.DeleteTestDependentRows(testIds.ToString());
                            foreach (TestCategory tc in testCategories)
                            {
                                _cmsRepository.ReleaseTestCategory(tc);
                            }

                            foreach (TestQuestion tq in testQuestions)
                            {
                                _cmsRepository.ReleaseTestQuestion(tq);
                            }

                            foreach (Norming norming in normings)
                            {
                                _cmsRepository.ReleaseNorming(norming);
                            }

                            foreach (Norm norm in norms)
                            {
                                _cmsRepository.ReleaseNorm(norm);
                            }

                           lookupService.SyncQuestionTestMappings(currentTestQuestions, testQuestions);
                        }
                    }
                }

                transaction.Commit();
                releasedSuccessfully = true;
            }

            if (releasedSuccessfully)
            {
                if (showQuestion == "y")
                {
                    _cmsRepository.UpdateReleaseStatus(qids.ToString(), "R", "Questions");
                    _cmsRepository.UpdateReleaseStatus(remediationIds.ToString(), "R", "Remediation");
                }

                if (showLippinCott == "y")
                {
                    _cmsRepository.UpdateReleaseStatus(lippincottIds.ToString(), "R", "Lippincot");
                }

                if (showTest == "y")
                {
                    _cmsRepository.UpdateReleaseStatus(testIds.ToString(), "R", "Tests");
                }
            }
        }

        public void SaveTestQuestion(TestQuestion testQuestion)
        {
            _cmsRepository.SaveTestQuestion(testQuestion);
        }

        public void DeleteTestQuestions(int testId)
        {
            _cmsRepository.DeleteTestQuestions(testId);
        }

        public IEnumerable<QuestionResult> GetQuestionListInTest(int testId)
        {
            return _cmsRepository.GetQuestionListInTest(testId);
        }

        public IList<LookupMapping> GetLookupMappings(int id, LookupType type, bool isReverseMapping)
        {
            return _cmsRepository.GetLookupMappings(id.ToString(), type, isReverseMapping);
        }

        public Lookup GetLookup(string matchText, LookupType type)
        {
            return _cmsRepository.GetLookup(matchText, type);
        }

        public bool IsQuestionIdExist(string questionId)
        {
            return _cmsRepository.IsQuestionIdExist(questionId);
        }

        public void SaveUploadedQuestionDetails(string GUID, string fileName, int userId)
        {
            _cmsRepository.SaveUploadedQuestionDetails(GUID, fileName, userId);
        }

        public void SaveUploadedRemediations(List<Remediation> remediations)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    foreach (Remediation remediation in remediations)
                    {
                        _cmsRepository.SaveUploadedRemediation(remediation);
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }

        public void SaveLoginContents(LoginContent loginContent)
        {
            _cmsRepository.SaveLoginContents(loginContent);
        }

        public void ReleaseLoginContent(LoginContent loginContent)
        {
            _cmsRepository.SaveLoginContents(loginContent);
            _cmsRepository.ReleaseLoginContent(loginContent);
        }

        public void RevertLoginContent(LoginContent loginContent)
        {
            _cmsRepository.RevertLoginContent(loginContent);
        }

        public LoginContent GetLoginContent(int contentId)
        {
            return _adminService.GetLoginContent(contentId);
        }

        public IEnumerable<ProgramofStudy> GetProgramofStudies()
        {
            return _adminService.GetProgramofStudies();
        }

        public IDictionary<CategoryName, Category> GetCategories(int programofstudyId)
        {
            return _adminService.GetCategories(programofstudyId);
        }
    }
}
