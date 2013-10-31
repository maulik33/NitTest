using System;
using System.Collections.Generic;
using System.Web;
using NursingLibrary.Common;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters.Navigation;
using NursingLibrary.Utilities;

namespace NursingLibrary.Presenters.Controller
{
    public class StudentAppController : IStudentAppController
    {
        #region Fields

        private readonly ICacheManagement _cacheService;
        private readonly IPageNavigator _navigator;
        private readonly ISessionManagement _sessionMgr;
        private readonly IStudentService _studentService;

        #endregion Fields

        #region Constructors

        public StudentAppController(IStudentService service, ICacheManagement cacheService, ISessionManagement sessionMgr, IPageNavigator navigator, Student student, ReviewRemediation reviewRemediation)
        {
            _studentService = service;
            _sessionMgr = sessionMgr;
            _navigator = navigator;
            _cacheService = cacheService;
            Student = student;
            ReviewRemediation = reviewRemediation;
        }

        #endregion Constructors

        #region Properties

        public Student Student
        {
            get;
            private set;
        }

        public ReviewRemediation ReviewRemediation
        {
            get;
            private set;
        }
        #endregion Properties

        #region Methods

        public bool ChangePassword(string newPassword)
        {
            var request = new AuthenticateRequest { UserId = Student.UserId, Password = newPassword.Trim() };
            var response = _studentService.ChangePassword(request);
            return response.Exits;
        }

        public bool CheckExistCaseModuleStudent(int CID, int MID, string SID)
        {
            var response = _studentService.CheckExistCaseModuleStudent(CID, MID, SID);
            return response.Exits;
        }

        public bool CheckPercentileRankExists()
        {
            var request = new Request
             {
                 TestId = Student.TestId
             };

            var response = _studentService.CheckPercentileRankExists(request);
            return response.Exits;
        }

        public bool CheckProbabilityExists()
        {
            var request = new Request
             {
                 TestId = Student.TestId
             };

            var response = _studentService.CheckProbabilityExists(request);
            return response.Exits;
        }

        public bool ContinueTest()
        {
            var request = new Request { UserId = Student.UserId, TestId = Student.TestId };
            var response = _studentService.ContinueTest(request);
            return response.Exits;
        }

        public IEnumerable<UserTest> GetUserTests(int userId, int testId)
        {
            var request = new Request { UserId = userId, TestId = testId };
            return _studentService.GetUserTests(request).Result;
        }

        public bool DoesTestExists(int productId, int testSubgroup, int type)
        {
            var request = new Request { ProductId = productId, TestSubGroup = testSubgroup, Type = type, TimeOffSet = Student.TimeOffset };
            var response = _studentService.DoesTestExists(request);

            return response.Exits;
        }

        public Dictionary<CategoryName, Category> GetAllCategories()
        {
            return _cacheService.GetNotRemovableItem(Constants.CACHE_KEY_CATEGORY, () => _studentService.GetCategories().Result);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _cacheService.Get("Products", () => _studentService.GetAllProducts().Result, TimeSpan.FromHours(24));
        }

        public IEnumerable<ProductTest> GetAllProductTests()
        {
            // return _cacheService.Get("ProductTest", () => _studentService.GetAllProductTests().Result, TimeSpan.FromHours(24));
            return _studentService.GetAllProductTests().Result;
        }

        public IEnumerable<Test> GetAllTests(int bundleId)
        {
            var testRequest = new Request
            {
                UserId = Student.UserId,
                ProductId = Student.ProductId,
                TimeOffSet = Student.TimeOffset,
                TestSubGroup = Student.TestSubGroup,
                Type = bundleId
            };
            var response = _studentService.GetAllTests(testRequest);
            return response.Result;
        }

        public IEnumerable<UserAnswer> GetAnswersForQuestion()
        {
            var request = new Request { UserTestId = Student.UserTestId };
            var response = _studentService.GetAnswersForQuestion(request);
            return response.Result;
        }

        public IEnumerable<AvpContent> GetAvpContent(int productid, int testSubgroup)
        {
            var avpItemsRequest = new Request { UserId = Student.UserId, ProductId = productid, TestSubGroup = testSubgroup };
            var avpContentResponse = _studentService.GetAvpContents(avpItemsRequest);
            return avpContentResponse.Result;
        }

        public CaseModuleScore GetCaseModuleScore()
        {
            var response = _studentService.GetCaseModuleScore();
            return response.Result;
        }

        public IEnumerable<CaseStudy> GetCaseStudies()
        {
            var response = _studentService.GetCaseStudies();
            return response.Result;
        }

        public CaseSubCategory GetCaseSubCategory()
        {
            var response = _studentService.GetCaseSubCategory();
            return response.Result;
        }

        public string GetCategoryDescription(CategoryName category, int categoryDetailId)
        {
            return GetAllCategories()[category].Details[categoryDetailId].Description;
        }

        public IEnumerable<FinishedTest> GetFinishedTests()
        {
            var finishedTestRequest = new Request
                                          {
                                              UserId = Student.UserId,
                                              ProductId = Student.ProductId,
                                              TimeOffSet = Student.TimeOffset
                                          };

            var response = _studentService.GetFinishedTests(finishedTestRequest);
            return response.Result;
        }

         public IEnumerable<UserAnswer> GetHotSpotAnswerByID()
        {
            var request = new Request { QuestionId = Student.QuestionId };
            var response = _studentService.GetHotSpotAnswerByID(request);
            return response.Result;
        }

        public IEnumerable<Lippincott> GetLippincottAssignedInQuestion()
        {
            var request = new Request { QuestionId = Student.QuestionId };
            var response = _studentService.GetLippincottAssignedInQuestion(request);
            return response.Result;
        }

        public IEnumerable<ClientNeeds> GetListOfAllClientNeeds(int programofStudyId)
        {
            var response = _studentService.GetListOfAllClientNeeds(programofStudyId);
            return response.Result;
        }

        public IEnumerable<ClientNeedsCategory> GetListOfAllClientNeedsCategoryInfo(int UserID)
        {
            var request = new Request { UserId = Student.UserId , QbankProgramofStudyId = Student.QBankProgramofStudyId};
            var response = _studentService.GetListOfAllClientNeedsCategoryInfo(request);
            return response.Result;
        }

        public int GetNumberOfCategory()
        {
            var response = _studentService.GetNumberOfCategory();
            return response.Result;
        }

        public int GetPercentileRank(int correctAnswers)
        {
            var request = new Request
             {
                 TestId = Student.TestId,
                 CorrectAnswers = correctAnswers
             };

            var response = _studentService.GetPercentileRank(request);
            return response.Result;
        }

        public IEnumerable<Question> GetPrevNextQuestionInTest(int questionNumber, QuestionFileType ftype)
        {
            var request = new Request { TestId = Student.TestId, QuestionId = questionNumber, TypeOfFileId = EnumHelper.GetQuestionFileTypeValue(ftype) };
            var response = _studentService.GetPrevNextQuestionInTest(request);
            return response.Result;
        }

        public IEnumerable<Question> GetPrevNextQuestions(int questionNumber, QuestionFileType ftype, bool inCorrectOnly)
        {
            var request = new Request
            {
                UserTestId = Student.UserTestId,
                TypeOfFileId = EnumHelper.GetQuestionFileTypeValue(ftype),
                InCorrectOnly = inCorrectOnly,
                QuestionId = questionNumber
            };
            var response = _studentService.GetPrevNextQuestions(request);
            return response.Result;
        }

        public int GetProbability(int correctAnswers)
        {
            var request = new Request
            {
                TestId = Student.TestId,
                CorrectAnswers = correctAnswers
            };
            var response = _studentService.GetProbability(request);
            return response.Result;
        }

        public ProgramResults GetProgramResults(int chartType)
        {
            var request = new Request
            {
                UserTestId = Student.UserTestId,
                ChartType = chartType
            };
            var response = _studentService.GetProgramResults(request);
            return response.Result;
        }

        public IEnumerable<ProgramResults> GetProgramResultsByNorm()
        {
            var request = new Request
             {
                 TestId = Student.TestId,
                 UserTestId = Student.UserTestId
             };

            // return _cacheService.Get(Constants.CACHE_KEY_PROGRAMRESULTSBYNORM, () => _studentService.GetProgramResultsByNorm(request).Result, TimeSpan.FromHours(24));
            return _studentService.GetProgramResultsByNorm(request).Result;
        }

        public string GetQBankGraphData(int aType)
        {
            var request = new Request
            {
                UserId = Student.UserId,
                ProductId = Student.ProductId,
                TestSubGroup = 3,
                TimeOffSet = Student.TimeOffset,
                Type = aType
            };
            var response = _studentService.GetQBankGraphXML(request);
            return response.Result;
        }

        public IEnumerable<ProgramResults> GetQBankTestPerformanceByProductIDChartType(int chartType, int overViewOrDetails)
        {
            var request = new Request { UserId = Student.UserId, ProductId = Student.ProductId, ChartType = chartType, OverViewOrDetails = overViewOrDetails };
            var response = _studentService.GetQBankTestPerformanceByProductIDChartType(request);
            return response.Result;
        }

        public IEnumerable<Question> GetQuestionByTest()
        {
            var request = new Request { TestId = Student.TestId };
            var response = _studentService.GetQuestionByTest(request);
            return response.Result;
        }

        public IEnumerable<Question> GetQuestionByUserTest()
        {
            var request = new Request { UserTestId = Student.UserTestId };
            var response = _studentService.GetQuestionsByUserTest(request);
            return response.Result;
        }

        public QuestionExhibit GetQuestionExhibitByID()
        {
            var request = new Request { UserTestId = Student.QuestionId };
            var response = _studentService.GetQuestionExhibitByID(request);
            return response.Result;
        }

        public IEnumerable<Category> GetStudentTestCharacteristics()
        {
            var request = new Request
                              {
                                  TestId = Student.TestId
                              };

            var response = _studentService.GetStudentTestCharacteristics(request);
            return response.Result;
        }

        public IEnumerable<UserTest> GetSuspendedTests()
        {
            var request = new Request
            {
                UserId = Student.UserId,
                ProductId = Student.ProductId,
                TimeOffSet = Student.TimeOffset,
                TestSubGroup = Student.TestSubGroup
            };

            var response = _studentService.GetSuspendedTests(request);
            return response.Result;
        }

        public IEnumerable<UserTest> GetTakenTests()
        {
            var request = new Request
            {
                UserId = Student.UserId,
                ProductId = Student.ProductId,
                TimeOffSet = Student.TimeOffset,
                TestSubGroup = Student.TestSubGroup
            };
            var response = _studentService.GetTakenTests(request);
            return response.Result;
        }

        public IEnumerable<UserTest> GetTestByProductUser()
        {
            var request = new Request
             {
                 UserId = Student.UserId,
                 ProductId = Student.ProductId,
                 TimeOffSet = Student.TimeOffset,
             };

            var response = _studentService.GetTestByProductUser(request);
            return response.Result;
        }

        public string GetTestName()
        {
            var request = new Request { TestId = Student.TestId };
            var response = _studentService.GetTestName(request);
            return response.Result;
        }

        public IEnumerable<UserQuestion> GetTestQuestionsForUser()
        {
            // Find Test Questions
            var request = new Request { UserTestId = Student.UserTestId, TypeOfFileId = "03" };
            var response = _studentService.GetTestQuestionsForUserId(request);
            return response.Result;
        }

        public int GetTestQuestionsCount()
        {
            var request = new Request { TestId = Student.TestId, TypeOfFileId = EnumHelper.GetQuestionFileTypeValue(QuestionFileType.Question) };
            var response = _studentService.GetTestQuestionsCount(request);
            return response.Result;
        }

        /// <summary>
        /// This method is used to get list of tests for Qbank_R.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FinishedTest> GetTestsNCLEXInfoForTheUser()
        {
            var qbankRItemsRequest = new Request
             {
                 UserId = Student.UserId,
                 ProductId = Student.ProductId,
                 TimeOffSet = Student.TimeOffset,
             };

            // return _cacheService.Get(Constants.CACHE_KEY_TESTSNCLEXINFOFORTHEUSER, () => _studentService.GetTestsNCLEXInfoForTheUser(qbankRItemsRequest).Result, TimeSpan.FromHours(24));
            return _studentService.GetTestsNCLEXInfoForTheUser(qbankRItemsRequest).Result;
        }

        public TestType GetTestType()
        {
            var request = new Request { TestId = Student.TestId };
            var response = _studentService.GetTestType(request);
            return response.Result;
        }

        public IEnumerable<Test> GetUnTakenTests()
        {
            var testRequest = new Request
            {
                UserId = Student.UserId,
                ProductId = Student.ProductId,
                TimeOffSet = Student.TimeOffset,
                TestSubGroup = Student.TestSubGroup
            };

            var response = _studentService.GetUntakenTests(testRequest);
            return response.Result;
        }

        public IEnumerable<Test> GetUnTakenTestsforSkillsModules()
        {
            var testRequest = new Request
            {
                UserId = Student.UserId,
                ProductId = 6,
                TimeOffSet = Student.TimeOffset,
                TestSubGroup = 1
            };

            var response = _studentService.GetUntakenTests(testRequest);
            return response.Result;
        }

        public IEnumerable<UserAnswer> GetUserAnswerByID()
        {
            var request = new Request { QuestionId = Student.QuestionId };
            var response = _studentService.GetUserAnswerByID(request);
            return response.Result;
        }

        public IEnumerable<UserTest> GetUserTestByID()
        {
            var request = new Request
            {
                UserTestId = Student.UserTestId,
            };

            var response = _studentService.GetUserTestByID(request);
            return response.Result;
        }

        public UserTest CreateTest()
        {
            TestTiming timedTest = TestTiming.Untimed;
            switch (Student.TestType)
            {
                case TestType.Integrated:
                    timedTest = TestTiming.Timed;
                    break;
                case TestType.Nclex:
                    timedTest = TestTiming.Untimed;
                    break;
                case TestType.FocusedReview:
                    timedTest = TestTiming.Untimed;
                    break;
            }

            var request = new Request
            {
                UserId = Student.UserId,
                ProductId = Student.ProductId,
                ProgramId = Student.ProgramId,
                TimedTest = Convert.ToInt32(timedTest),
                TestId = Student.TestId
            };
            var response = _studentService.CreateTest(request);
            return response.Result;
        }

        public UserTest CreateQBankTest(int tutorMode, int reUserMode, int correct, string options)
        {
            var request = new Request
            {
                UserId = Student.UserId,
                ProductId = Student.ProductId,
                ProgramId = Student.ProgramId,
                TimedTest = (tutorMode == 1) ? 0 : 1,
                TestId = Student.TestId,
                TutorMode = tutorMode,
                ReUseMode = reUserMode,
                NumberOfQuestions = Student.NumberOfQuestions,
                CorrectAnswers = correct,
                Options = options,
                QbankProgramofStudyId = Student.QBankProgramofStudyId
            };
            var response = _studentService.CreateQBankTest(request);
            return response.Result;
        }

        public bool InsertModuleScore(CaseModuleScore moduleScore)
        {
            var response = _studentService.InsertModuleScore(moduleScore);
            return response.Exits;
        }

        public bool InsertSubCategory(CaseSubCategory subCategory)
        {
            var response = _studentService.InsertSubCategory(subCategory);
            return response.Exits;
        }

        public void Intialize()
        {
            Student = _sessionMgr.Get<Student>(SessionKeys.LoggedInStudent);
            if (Student == null || Student.UserId <= 0)
            {
                throw new ApplicationException("NursingBasePage Session Validation Error: UserId is invalid.");
            }

            var request = new Request
            {
                UserId = Student.UserId
            };
            if (HttpContext.Current.Session.SessionID != _studentService.GetSessionId(request).Result)
            {
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Session.Abandon();
                TraceHelper.WriteTraceEnd(GetTraceToken());
                ShowPage(PageDirectory.StudentLogin, null, null);
            }
        }

        public bool IsTest74Question()
        {
            var request = new Request { QuestionId = Student.QuestionId };
            var response = _studentService.IsTest74Question(request);
            return response.Exits;
        }

        public void LogIn(string userName, string password, string sessionId)
        {
            var request = new AuthenticateRequest { UserName = userName.Trim(), Password = password.Trim(), SessionId = sessionId };
            var response = _studentService.AuthenticateStudent(request);
            Student = response.Result;
        }

        public bool SaveQuestionInTheUserTest(Question question, IList<UserAnswer> userAnswers, UserTest userTest)
        {
            var request = new SaveQuestionRequest { Question = question, UserAnswers = userAnswers, UserTest = userTest };
            var response = _studentService.SaveQuestionInTheUserTest(request);
            return response.Exits;
        }

        public void SaveSession()
        {
            if (Student != null && Student.UserId > 0)
            {
                _sessionMgr.Set(SessionKeys.LoggedInStudent, Student);
                _sessionMgr.Set("UserName", Student.UserName);
            }
        }

        public void StartTrace(int userId, string userName, string environment)
        {
            TraceToken token = TraceHelper.BeginTrace(userId, userName, environment);
            _sessionMgr.Set(SessionKeys.TRACE_TOKEN, token);
        }

        public TraceToken GetTraceToken()
        {
            return _sessionMgr.Get<TraceToken>(SessionKeys.TRACE_TOKEN);
        }

        public void ShowPage(PageDirectory page, string fragment, string query)
        {
            _navigator.NaviagteTo(page, fragment, query);
        }

        public bool UpdateEndTest(UserTest test)
        {
            _studentService.UpdateEndTest(test);
            return true;
        }

        public void UpdateQuestionExplanation(int timerValue)
        {
            var request = new Request { UserTestId = Student.UserTestId, QuestionId = Student.QuestionId, TimerValue = timerValue };
            _studentService.UpdateQuestionExplanation(request);
        }

        public void UpdateQuestionRemediation(int timerValue)
        {
            var request = new Request { UserTestId = Student.UserTestId, QuestionId = Student.QuestionId, TimerValue = timerValue };
            _studentService.UpdateQuestionRemediation(request);
        }

        public void UpdateTestStatus()
        {
            var request = new Request { UserTestId = Student.UserTestId };
            _studentService.UpdateTestStatus(request);
        }

        public void UpdateTestStatusOnExpiry()
        {
            var request = new Request { UserTestId = Student.UserTestId, QuestionId = Student.QuestionId };
            _studentService.UpdateTestStatusOnExpiry(request);
        }

        public string GetRemediationExplainationByID(int remediationId)
        {
            return _studentService.GetRemediationExplainationByID(remediationId).Result;
        }

        public int CreateSkillsModulesDetails(int TestId, int UserId)
        {
            return _studentService.CreateSkillsModulesDetails(TestId, UserId);
        }

        public void SaveSessionInfo(ExecutionContext context)
        {
            _sessionMgr.Set<ExecutionContext>(SessionKeys.EXECUTION_CONTEXT, context);
        }

        public IEnumerable<Systems> GetCategories(string typeIds)
        {
            return _studentService.GetCategories(typeIds);
        }

        public IEnumerable<Topic> GetTopics(string categoryIds, bool isTest)
        {
            return _studentService.GetTopics(categoryIds, isTest,Student.ProgramofStudyId);
        }

        public void CreateFRQBankRemediation(ReviewRemediation ReviewRem, string systems, string topics)
        {
            _studentService.CreateFRQBankRemediation(ReviewRem, systems, topics,Student.ProgramofStudyId);
        }

        public IEnumerable<ReviewRemediation> GetRemediationsForTheUser(int studentId)
        {
            return _studentService.GetRemediationsForTheUser(studentId);
        }

        public int GetAvailableRemediations(string systems, string topics)
        {
            return _studentService.GetAvailableRemediations(systems, topics,Student.ProgramofStudyId);
        }

        public ReviewRemediation GetRemediationExplainationByReviewID(int revRemID)
        {
            return _studentService.GetRemediationExplainationByReviewID(revRemID);
        }

        public IEnumerable<Lippincott> GetLippincottForReviewRemediation(int revRemQID)
        {
            return _studentService.GetLippincottForReviewRemediation(revRemQID);
        }

        public void UpdateReviewRemediation(int reviewId, int time)
        {
            _studentService.UpdateReviewRemediation(reviewId, time);
        }

        public void DeleteRemediations(int remediationsReviewId)
        {
            _studentService.DeleteRemediations(remediationsReviewId);
        }

        public IEnumerable<ReviewRemediation> GetNextPrevRemediation(int Id, int RemNumber, string action)
        {
            return _studentService.GetNextPrevRemediation(Id, RemNumber, action);
        }

        public void UpdateTotalRemediatedTime(int remediationReviewId)
        {
            _studentService.UpdateTotalRemediatedTime(remediationReviewId);
        }

        public UserTest CreateFRQBankTest(int tutorMode, int reUserMode, int correct, string systemIds, string topics, string name)
        {
            var request = new Request
            {
                UserId = Student.UserId,
                ProductId = Student.ProductId,
                ProgramId = Student.ProgramId,
                TimedTest = (tutorMode == 1) ? 0 : 1,
                TestId = Student.TestId,
                TutorMode = tutorMode,
                ReUseMode = reUserMode,
                NumberOfQuestions = Student.NumberOfQuestions,
                CorrectAnswers = correct,
                SystemIds = systemIds,
                TopicIds = topics,
                Name = name,
                ProgramofStudyId = Student.ProgramofStudyId,
            };

            var response = _studentService.CreateFRQBankTest(request);
            return response.Result;
        }

        public int GetCFRAvailableQuestions(int userId, string categoryIds, string topicIds, bool isTest, int reUseMode)
        {
            return _studentService.GetCFRAvailableQuestions(userId, categoryIds, topicIds, isTest, reUseMode,Student.ProgramofStudyId);
        }

        public UserTest CreateFRQBankRepeatTest(int userTestId)
        {
            return _studentService.CreateFRQBankRepeatTest(userTestId);
        }

        public IEnumerable<UserTest> GetCustomizedFRTests(int userId, int timeOffSet)
        {
            return _studentService.GetCustomizedFRTests(userId, timeOffSet);
        }

        public IEnumerable<UserTest> GetSkillsModulesAvailableQuizzes(int userId, int timeOffSet, int productId)
        {
            return _studentService.GetSkillsModulesAvailableQuizzes(userId, timeOffSet, productId);
        }

        public int GetCustomFRTestQuestionCount(int userTestId)
        {
            return _studentService.GetCustomFRTestQuestionCount(userTestId);
        }

        public int GetQuestionCount(int userTestId)
        {
            return _studentService.GetQuestionCount(userTestId);
        }

        public void UpdateAltTabClick(int userTestId, int QId, bool IsAltTabClicked)
        {
            _studentService.UpdateAltTabClick(userTestId, QId, IsAltTabClicked);
        }

        public IEnumerable<SMUserVideoTransaction> GetSkillsModuleVideos(int skillsModuleUserId)
        {
            return _studentService.GetSkillsModuleVideos(skillsModuleUserId);
        }

        public UserTest CreateSkillsModuleTest(int testId)
        {
            var request = new Request
            {
                UserId = Student.UserId,
                ProductId = Student.ProductId,
                ProgramId = Student.ProgramId,
                TimedTest = 1,
                TestId = testId,
                TutorMode = 0,
                ReUseMode = 0,
                NumberOfQuestions = Student.NumberOfQuestions,
            };

            var response = _studentService.CreateSkillsModuleTest(request);
            return response.Result;
        }

        public void UpdateSkillModuleStatus(int SMUserVideoId)
        {
            _studentService.UpdateSkillModuleStatus(SMUserVideoId);
        }

        public void UpdateSkillModuleVideoStatus(int SMUserVideoId)
        {
            _studentService.UpdateSkillModuleVideoStatus(SMUserVideoId);
        }

        public IEnumerable<SMTest> GetSkillsModuleTests(int SMUserId, int TestId)
        {
            return _studentService.GetSkillsModuleTests(SMUserId, TestId);
        }

        public void ResetSkillModuleStatus(int SMUserId)
        {
            _studentService.ResetSkillModuleStatus(SMUserId);
        }

        public Student GetStudentInfoByUserNameEmail(string userName, string email)
        {
            return _studentService.GetStudentInfoByUserNameEmail(userName, email);
        }

        public int GetSkillModuleUserId(int testId, int userId)
        {
            return _studentService.GetSkillModuleUserId(testId, userId);
        }

        public int GetOriginalSMTestId(int newtestId, int userId)
        {
            return _studentService.GetOriginalSMTestId(newtestId, userId);
        }

        public IEnumerable<SMTest> GetSkillsModuleTestsByUserId(int SMUserId)
        {
            return _studentService.GetSkillsModuleTestsByUserId(SMUserId);
        }

        public LoginContent GetLoginContent(int contentId)
        {
            return _studentService.GetLoginContent(contentId);
        }

        public void UpdateScrambledAnswerChoice(string scrambledanswerchoice, int usertestId, int questionId)
        {
            _studentService.UpdateScrambledAnswerChoice(scrambledanswerchoice, usertestId, questionId);
        }

        public string GetScrambledAnswerChoice(int usertestId, int questionId)
        {
            return _studentService.GetScrambledAnswerChoice(usertestId, questionId);
        }

        public IEnumerable<AnswerChoice> GetAnswers(int questionId, int actionType)
        {
            return _studentService.GetAnswers(questionId, actionType);
        }

        public IEnumerable<AssetDetail> GetDashBoardLinks(int programId)
        {
            return _studentService.GetDashBoardLinks(programId);
        }

        public IEnumerable<AssetGroup> GetAssetGroupByProgramId(int programId)
        {
            return _studentService.GetAssetGroupByProgramId(programId);
        }

        public IEnumerable<Test> GetQBankTest(int userId,int productId, int testSubGroupId, int timeOffSet)
        {
            return _studentService.GetQBankTest(userId, productId, testSubGroupId,timeOffSet);
        }

        public string GetUserDetailsById(int userId)
        {
            return _studentService.GetUserDetailsById(userId);
        }

        public LtiProvider GetLtiTestSecurityProviderByName(string ltiProviderName)
        {
            return _studentService.GetLtiTestSecurityProviderByName(ltiProviderName);
        }

        #endregion Methods
    }
}