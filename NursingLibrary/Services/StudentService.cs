using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Services
{
    public class StudentService : IStudentService
    {
        #region Fields

        private readonly IQuestionRepository _questRep;
        private readonly IStudentRepository _studentRep;
        private readonly ITestRepository _testRep;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAdminService _adminService;
        private readonly ICMSService _cmsService;
        #endregion Fields

        #region Constructors

        public StudentService(IStudentRepository studentRep, ITestRepository testRep, IQuestionRepository questRep,
            IAdminService adminService, ICMSService cmsService, IUnitOfWork unitOfWork)
        {
            _studentRep = studentRep;
            _testRep = testRep;
            _questRep = questRep;
            _adminService = adminService;
            _cmsService = cmsService;
            _unitOfWork = unitOfWork;
        }
        #endregion Constructors

        #region Methods

        public Response<Student> AuthenticateStudent(AuthenticateRequest request)
        {
            var response = new Response<Student>();
            Student student = null;
            if (!string.IsNullOrEmpty(request.UserName) && !string.IsNullOrEmpty(request.Password))
            {
                student = _studentRep.GetStudent(request.UserName, request.Password);
            }
            else if (request.UserId > 0)
            {
                student = _studentRep.GetStudent(request.UserId);
            }

            if (student.UserId > 0)
            {
                // save session id for student (to block multiple logins)
                SetIsSkillsModule(student);
                SetQBankProgramofStudy(student);
                _studentRep.SaveUserSession(student.UserId, request.SessionId);
            }

            response.Result = student;
            return response;
        }

        public Response<String> GetSessionId(Request request)
        {
            var response = new Response<String>
            {
                Result = _studentRep.GetSession(request.UserId),
            };
            return response;
        }

        public BoolResponse ChangePassword(AuthenticateRequest request)
        {
            var response = new BoolResponse
                               {
                                   Exits = _studentRep.ChangePassword(request.UserId, request.Password),
                               };
            return response;
        }

        public BoolResponse CheckExistCaseModuleStudent(int CID, int MID, string SID)
        {
            var response = new BoolResponse
                               {
                                   Exits = _studentRep.CheckExistCaseModuleStudent(CID, MID, SID),
                               };
            return response;
        }

        public BoolResponse CheckPercentileRankExists(Request request)
        {
            var response = new BoolResponse
                               {
                                   Exits = _testRep.CheckPercentileRankExists(request.TestId)
                               };
            return response;
        }

        public BoolResponse CheckProbabilityExists(Request request)
        {
            var response = new BoolResponse
                               {
                                   Exits = _testRep.CheckProbabilityExists(request.TestId)
                               };
            return response;
        }

        public BoolResponse ContinueTest(Request request)
        {
            var response = new BoolResponse
                               {
                                   Exits = _testRep.ContinueTest(request.UserId, request.TestId)
                               };
            return response;
        }

        public BoolResponse DoesTestExists(Request request)
        {
            var response = new BoolResponse
                               {
                                   Exits =
                                       _testRep.TestExists(request.UserId, request.ProductId, request.TestSubGroup,
                                                           request.Type, request.TimeOffSet)
                               };
            return response;
        }

        public CollectionResponse<Product> GetAllProducts()
        {
            var response = new CollectionResponse<Product> { Result = _adminService.GetProducts() };
            return response;
        }

        public CollectionResponse<ProductTest> GetAllProductTests()
        {
            var response = new CollectionResponse<ProductTest> { Result = _testRep.LoadProductTest() };
            return response;
        }

        public CollectionResponse<Test> GetAllTests(Request request)
        {
            var response = new CollectionResponse<Test>
                               {
                                   Result =
                                       _testRep.GetAllTests(request.UserId, request.ProductId, request.Type,
                                                            request.TestSubGroup, request.TimeOffSet)
                               };
            return response;
        }

        public CollectionResponse<UserAnswer> GetAnswersForQuestion(Request request)
        {
            var response = new CollectionResponse<UserAnswer>
                               {
                                   Result = _questRep.GetAnswersForQuestion(request.UserTestId)
                               };
            return response;
        }

        public CollectionResponse<AvpContent> GetAvpContents(Request request)
        {
            var response = new CollectionResponse<AvpContent>
                               {
                                   Result =
                                       _studentRep.GetAvpContent(request.UserId, request.ProductId, request.TestSubGroup)
                               };
            return response;
        }

        public Response<CaseModuleScore> GetCaseModuleScore()
        {
            var response = new Response<CaseModuleScore> { Result = _studentRep.LoadCaseModuleScore() };
            return response;
        }

        public CollectionResponse<CaseStudy> GetCaseStudies()
        {
            var response = new CollectionResponse<CaseStudy> { Result = _studentRep.GetCaseStudies() };
            return response;
        }

        public Response<CaseSubCategory> GetCaseSubCategory()
        {
            var response = new Response<CaseSubCategory> { Result = _studentRep.LoadCaseSubCategory() };
            return response;
        }

        public Response<Dictionary<CategoryName, Category>> GetCategories()
        {
            var response = new Response<Dictionary<CategoryName, Category>> { Result = _adminService.GetCategories().ToDictionary(k => k.Key, v => v.Value) };
            return response;
        }

        public CollectionResponse<FinishedTest> GetFinishedTests(Request request)
        {
            var response = new CollectionResponse<FinishedTest>
                               {
                                   Result =
                                       _testRep.GetFinishedTests(request.UserId, request.ProductId, request.TimeOffSet)
                               };
            return response;
        }

        public CollectionResponse<UserAnswer> GetHotSpotAnswerByID(Request request)
        {
            var response = new CollectionResponse<UserAnswer>
                               {
                                   Result = _questRep.GetHotSpotAnswerByID(request.QuestionId)
                               };
            return response;
        }

        public CollectionResponse<Lippincott> GetLippincottAssignedInQuestion(Request request)
        {
            var response = new CollectionResponse<Lippincott>
                               {
                                   Result = _questRep.GetLippincottAssignedInQuestion(request.QuestionId)
                               };
            return response;
        }

        public CollectionResponse<ClientNeeds> GetListOfAllClientNeeds(int programofStudyId)
        {
            var response = new CollectionResponse<ClientNeeds> { Result = _studentRep.LoadClientNeeds(programofStudyId) };
            return response;
        }

        public CollectionResponse<ClientNeedsCategory> GetListOfAllClientNeedsCategoryInfo(Request request)
        {
            var response = new CollectionResponse<ClientNeedsCategory>
                               {
                                   Result = _studentRep.LoadClientNeedsCategoryInfo(request.UserId, request.QbankProgramofStudyId)
                               };
            return response;
        }

        public Response<int> GetNumberOfCategory()
        {
            var response = new Response<int> { Result = _studentRep.GetNumberOfCategory() };
            return response;
        }

        public Response<int> GetPercentileRank(Request request)
        {
            var response = new Response<int>
                               {
                                   Result = Convert.ToInt32(_testRep.GetPercentileRank(request.TestId, request.CorrectAnswers))
                               };
            return response;
        }

        public CollectionResponse<Question> GetPrevNextQuestionInTest(Request request)
        {
            var response = new CollectionResponse<Question>
                               {
                                   Result = _questRep.GetPrevNextQuestionInTest(request.TestId, request.QuestionId, request.TypeOfFileId)
                               };
            return response;
        }

        public CollectionResponse<Question> GetPrevNextQuestions(Request request)
        {
            var response = new CollectionResponse<Question>
                {
                    Result = _questRep.GetPrevNextQuestions(request.UserTestId, request.QuestionId, request.TypeOfFileId, request.InCorrectOnly)
                };
            return response;
        }

        public Response<int> GetProbability(Request request)
        {
            var response = new Response<int>
                               {
                                   Result = Convert.ToInt32(_testRep.GetProbability(request.TestId, request.CorrectAnswers))
                               };
            return response;
        }

        public Response<ProgramResults> GetProgramResults(Request request)
        {
            var response = new Response<ProgramResults>
                               {
                                   Result = _studentRep.GetProgramResults(request.UserTestId, request.ChartType)
                               };

            return response;
        }

        public CollectionResponse<ProgramResults> GetProgramResultsByNorm(Request request)
        {
            var response = new CollectionResponse<ProgramResults>
                               {
                                   Result = _studentRep.GetProgramResultsByNorm(request.UserTestId, request.TestId)
                               };
            return response;
        }

        public Response<string> GetQBankGraphXML(Request request)
        {
            var response = new Response<string>();
            var graphData = new QBankGraph();
            if (request.Type == 1)
            {
                IEnumerable<ProgramResults> performance = _testRep.GetQBankTestPerformancyByChartType(request.UserId,
                                                                                                      request.ProductId,
                                                                                                      1, 1);
                graphData.GenerateGraphData(performance);
            }
            else
            {
                IEnumerable<UserTest> userTests = _testRep.GetTestsByUserProductSubGroup(request.UserId,
                                                                                         request.ProductId,
                                                                                         request.TestSubGroup,
                                                                                         request.TimeOffSet);

                IEnumerable<ProgramResults> performance = _testRep.GetQBankTestPerformancyByChartType(request.UserId,
                                                                                                      request.ProductId,
                                                                                                      3, 1);

                graphData.GenerateGraphData(userTests, performance);
            }

            using (var stream = new MemoryStream())
            {
                var serializer = new XmlSerializer(graphData.GetType());
                serializer.Serialize(stream, graphData);
                using (var reader = new StreamReader(stream))
                {
                    stream.Position = 0;
                    response.Result = reader.ReadToEnd();
                    reader.Close();
                }
            }

            return response;
        }

        public CollectionResponse<ProgramResults> GetQBankTestPerformanceByProductIDChartType(Request request)
        {
            var response = new CollectionResponse<ProgramResults>
                               {
                                   Result =
                                       _testRep.GetQBankTestPerformancyByChartType(request.UserId, request.ProductId,
                                                                                   request.ChartType,
                                                                                   request.OverViewOrDetails)
                               };
            return response;
        }

        public CollectionResponse<Question> GetQuestionByTest(Request request)
        {
            var response = new CollectionResponse<Question>
                               {
                                   Result = _questRep.GetQuestionByTest(request.TestId)
                               };
            return response;
        }

        public Response<QuestionExhibit> GetQuestionExhibitByID(Request request)
        {
            var response = new Response<QuestionExhibit>
                               {
                                   Result = _questRep.GetQuestionExhibitByID(request.QuestionId)
                               };
            return response;
        }

        public CollectionResponse<Question> GetQuestionsByUserTest(Request request)
        {
            var response = new CollectionResponse<Question>
                               {
                                   Result = _questRep.GetQuestionsByUserTest(request.UserTestId)
                               };
            return response;
        }

        public Response<int> GetTestQuestionsCount(Request request)
        {
            var response = new Response<int>
            {
                Result = _testRep.GetTestQuestionsCount(request.TestId, request.TypeOfFileId)
            };
            return response;
        }

        public CollectionResponse<Category> GetStudentTestCharacteristics(Request request)
        {
            var response = new CollectionResponse<Category>
                               {
                                   Result = _testRep.GetStudentTestCharacteristics(request.TestId)
                               };
            return response;
        }

        public CollectionResponse<UserTest> GetSuspendedTests(Request request)
        {
            var response = new CollectionResponse<UserTest>
                               {
                                   Result =
                                       _testRep.GetSuspendedTests(request.UserId, request.ProductId,
                                                                  request.TestSubGroup, request.TimeOffSet)
                               };
            return response;
        }

        public CollectionResponse<UserTest> GetTakenTests(Request request)
        {
            var response = new CollectionResponse<UserTest>
                               {
                                   Result =
                                       _testRep.GetTakenTests(request.UserId, request.ProductId, request.TestSubGroup,
                                                              request.TimeOffSet)
                               };
            return response;
        }

        public CollectionResponse<UserTest> GetTestByProductUser(Request request)
        {
            var response = new CollectionResponse<UserTest>
                               {
                                   Result =
                                       _testRep.GetTestByProductUser(request.UserId, request.ProductId,
                                                                     request.TimeOffSet)
                               };
            return response;
        }

        public Response<string> GetTestName(Request request)
        {
            var response = new Response<string> { Result = _testRep.GetTestName(request.TestId) };
            return response;
        }

        public CollectionResponse<UserQuestion> GetTestQuestionsForUserId(Request request)
        {
            var response = new CollectionResponse<UserQuestion>
                               {
                                   Result = _testRep.GetTestQuestionsForUserId(request.UserTestId, request.TypeOfFileId)
                               };
            return response;
        }

        /// <summary>
        /// This method is used to get list of tests for Qbank_R
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CollectionResponse<FinishedTest> GetTestsNCLEXInfoForTheUser(Request request)
        {
            var response = new CollectionResponse<FinishedTest>
                               {
                                   Result =
                                       _testRep.GetTestsNCLEXInfoForTheUser(request.UserId, request.ProductId,
                                                                            request.TimeOffSet)
                               };
            return response;
        }

        public Response<TestType> GetTestType(Request request)
        {
            var response = new Response<TestType> { Result = _testRep.GetTestType(request.TestId) };
            return response;
        }

        public CollectionResponse<Test> GetUntakenTests(Request request)
        {
            var response = new CollectionResponse<Test>
                               {
                                   Result =
                                       _testRep.GetUntakenTests(request.UserId, request.ProductId, request.TestSubGroup,
                                                                request.TimeOffSet)
                               };
            return response;
        }

        public CollectionResponse<UserAnswer> GetUserAnswerByID(Request request)
        {
            var response = new CollectionResponse<UserAnswer>
                               {
                                   Result = _questRep.GetUserAnswerByID(request.QuestionId)
                               };
            return response;
        }

        public CollectionResponse<UserTest> GetUserTestByID(Request request)
        {
            var response = new CollectionResponse<UserTest>
                               {
                                   Result = _testRep.GetUserTestByID(request.UserTestId)
                               };
            return response;
        }

        public Response<UserTest> CreateTest(Request request)
        {
            var response = new Response<UserTest>
            {
                Result =
                    _questRep.CreateTest(request.UserId, request.ProductId, request.ProgramId,
                                               request.TimedTest, request.TestId)
            };
            return response;
        }

        public Response<UserTest> CreateQBankTest(Request request)
        {
            switch (request.QbankProgramofStudyId)
            {
                case (int)ProgramofStudyType.RN:
                    if (request.NumberOfQuestions > 75)
                    {
                        throw new InvalidOperationException(string.Format("Cannot create QBank Test with {0} Questions.", request.NumberOfQuestions));
                    }
                    break;
                default:
                    if (request.NumberOfQuestions > 85)
                    {
                        throw new InvalidOperationException(string.Format("Cannot create QBank Test with {0} Questions.", request.NumberOfQuestions));
                    }
                    break;
            }

            var response = new Response<UserTest>
            {
                Result =
                    _questRep.CreateQBankTest(request.UserId, request.ProductId, request.ProgramId,
                                               request.TimedTest, request.TestId, request.TutorMode, request.ReUseMode,
                                               request.NumberOfQuestions, request.CorrectAnswers, request.Options)
            };
            return response;
        }

        public BoolResponse InsertModuleScore(CaseModuleScore moduleScore)
        {
            var response = new BoolResponse { Exits = _studentRep.InsertModuleScore(moduleScore) };
            return response;
        }

        public BoolResponse InsertSubCategory(CaseSubCategory subCategory)
        {
            var response = new BoolResponse { Exits = _studentRep.InsertSubCategory(subCategory) };
            return response;
        }

        public BoolResponse IsTest74Question(Request request)
        {
            var response = new BoolResponse
                               {
                                   Exits =
                                       _testRep.TestExists(request.UserId, request.ProductId, request.TestSubGroup,
                                                           request.Type, request.TimeOffSet)
                               };
            return response;
        }

        public BoolResponse SaveQuestionInTheUserTest(SaveQuestionRequest request)
        {
            var response = new BoolResponse();
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                _questRep.UpdateUserQuestions(request.Question);
                _questRep.CreateUserAnswers(request.Question, request.UserAnswers);
                _questRep.UpdateUserTest(request.Question, request.UserTest);
                transaction.Commit();
                response.Exits = true;
            }

            return response;
        }

        public void UpdateEndTest(UserTest request)
        {
            _testRep.UpdateEndTest(request);
        }

        public void UpdateQuestionExplanation(Request request)
        {
            _questRep.UpdateQuestionExplanation(request.QuestionId, request.UserTestId, request.TimerValue);
        }

        public void UpdateQuestionRemediation(Request request)
        {
            _questRep.UpdateQuestionRemediation(request.QuestionId, request.UserTestId, request.TimerValue);
        }

        public void UpdateTestStatus(Request request)
        {
            _testRep.UpdateTestStatus(request.UserTestId, 1);
        }

        public void UpdateTestStatusOnExpiry(Request request)
        {
            _testRep.UpdateTestStatusOnExpiry(request.UserTestId, 1, 0, request.QuestionId);
        }

        public Response<string> GetRemediationExplainationByID(int remediationId)
        {
            var response = new Response<string>();

            response.Result = _studentRep.GetRemediationExplainationByID(remediationId);

            return response;
        }

        public CollectionResponse<UserTest> GetUserTests(Request request)
        {
            var response = new CollectionResponse<UserTest>
            {
                Result = _testRep.GetUserTests(request.UserId, request.TestId)
            };
            return response;
        }

        public IEnumerable<Systems> GetCategories(string typeIds)
        {
            return _studentRep.GetCategories(typeIds).OrderBy(key => key.System);
        }

        public IEnumerable<Topic> GetTopics(string categoryIds, bool isTest,int programofStudyId)
        {
            return _studentRep.GetTopics(categoryIds, isTest, programofStudyId).OrderBy(key => key.TopicTitle);
        }

        public void CreateFRQBankRemediation(ReviewRemediation ReviewRem, string systems, string topics,int programofstudyId)
        {
            _studentRep.CreateFRQBankRemediation(ReviewRem, systems, topics,programofstudyId);
        }

        public int CreateSkillsModulesDetails(int TestId, int UserId)
        {
            return _studentRep.CreateSkillsModulesDetails(TestId, UserId);
        }

        public IEnumerable<ReviewRemediation> GetRemediationsForTheUser(int studentId)
        {
            return _studentRep.GetRemediationsForTheUser(studentId);
        }

        public int GetAvailableRemediations(string systems, string topics,int programofstudyId)
        {
            return _studentRep.GetAvailableRemediations(systems, topics, programofstudyId);
        }

        public ReviewRemediation GetRemediationExplainationByReviewID(int revRemID)
        {
            return _studentRep.GetRemediationExplainationByReviewID(revRemID).OrderBy(key => key.RemediationNumber).FirstOrDefault();
        }

        public IEnumerable<Lippincott> GetLippincottForReviewRemediation(int revRemQID)
        {
            return _studentRep.GetLippincottForReviewRemediation(revRemQID);
        }

        public void UpdateReviewRemediation(int reviewId, int time)
        {
            _studentRep.UpdateReviewRemediation(reviewId, time);
        }

        public void DeleteRemediations(int remediationReviewId)
        {
            _studentRep.DeleteRemediations(remediationReviewId);
        }

        public IEnumerable<ReviewRemediation> GetNextPrevRemediation(int Id, int RemNumber, string action)
        {
            return _studentRep.GetNextPrevRemediation(Id, RemNumber, action);
        }

        public void UpdateTotalRemediatedTime(int remediationReviewId)
        {
            _studentRep.UpdateTotalRemediatedTime(remediationReviewId);
        }

        public Response<UserTest> CreateFRQBankTest(Request request)
        {
            if (request.NumberOfQuestions > 50)
            {
                throw new InvalidOperationException(string.Format("Cannot create QBank Test with {0} Questions.", request.NumberOfQuestions));
            }

            var response = new Response<UserTest>
            {
                Result =
                    _questRep.CreateFRQBankTest(request.UserId, request.ProductId, request.ProgramId,
                                               request.TimedTest, request.TestId, request.TutorMode, request.ReUseMode,
                                               request.NumberOfQuestions, request.CorrectAnswers, request.SystemIds, request.TopicIds, request.Name,request.ProgramofStudyId)
            };
            return response;
        }

        public int GetCFRAvailableQuestions(int userId, string categoryIds, string topicIds, bool isTest, int reUseMode, int programofstudyid)
        {
            return _questRep.GetCFRAvailableQuestions(userId, categoryIds, topicIds, isTest, reUseMode, programofstudyid);
        }

        public IEnumerable<UserTest> GetCustomizedFRTests(int userId, int timeOffSet)
        {
            return _testRep.GetCustomizedFRTests(userId, timeOffSet);
        }

        public IEnumerable<UserTest> GetSkillsModulesAvailableQuizzes(int userId, int timeOffSet, int productId)
        {
            return _testRep.GetSkillsModulesAvailableQuizzes(userId, timeOffSet, productId);
        }

        public UserTest CreateFRQBankRepeatTest(int userTestId)
        {
            return _studentRep.CreateFRQBankRepeatTest(userTestId);
        }

        public int GetCustomFRTestQuestionCount(int userTestId)
        {
            return _testRep.GetCustomFRTestQuestionCount(userTestId);
        }

        public int GetQuestionCount(int userTestId)
        {
            return _testRep.GetQuestionCount(userTestId);
        }

        public void UpdateAltTabClick(int userTestId, int QId, bool IsAltTabClicked)
        {
            _studentRep.UpdateAltTabClick(userTestId, QId, IsAltTabClicked);
        }

        public IEnumerable<SMUserVideoTransaction> GetSkillsModuleVideos(int skillsModuleUserId)
        {
            return _studentRep.GetSkillsModuleVideos(skillsModuleUserId);
        }

        public Response<UserTest> CreateSkillsModuleTest(Request request)
        {
            var response = new Response<UserTest>
            {
                Result = _questRep.CreateSkillsModuleTest(request.UserId, request.ProductId, request.ProductId, request.TimedTest, request.TutorMode, request.ReUseMode, request.TestId)
            };
            return response;
        }

        public void UpdateSkillModuleStatus(int SMUserVideoId)
        {
            _studentRep.UpdateSkillModuleStatus(SMUserVideoId);
        }

        public void UpdateSkillModuleVideoStatus(int SMUserVideoId)
        {
            _studentRep.UpdateSkillModuleVideoStatus(SMUserVideoId);
        }

        public IEnumerable<SMTest> GetSkillsModuleTests(int SMUserId, int TestId)
        {
            return _studentRep.GetSkillsModuleTests(SMUserId, TestId);
        }

        public void ResetSkillModuleStatus(int SMUserId)
        {
            _studentRep.ResetSkillModuleStatus(SMUserId);
        }

        public Student GetStudentInfoByUserNameEmail(string userName, string email)
        {
            return _studentRep.GetStudentInfoByUserNameEmail(userName, email);
        }

        public int GetSkillModuleUserId(int testId, int userId)
        {
            return _studentRep.GetSkillModuleUserId(testId, userId);
        }

        public int GetOriginalSMTestId(int newtestId, int userId)
        {
            return _studentRep.GetOriginalSMTestId(newtestId, userId);
        }

        public IEnumerable<SMTest> GetSkillsModuleTestsByUserId(int SMUserId)
        {
            return _studentRep.GetSkillsModuleTestsByUserId(SMUserId);
        }

        public LoginContent GetLoginContent(int contentId)
        {
            return _adminService.GetLoginContent(contentId);
        }

        public void UpdateScrambledAnswerChoice(string scrambledanswerchoice, int usertestId, int questionId)
        {
            _studentRep.UpdateScrambledAnswerChoice(scrambledanswerchoice, usertestId, questionId);
        }

        public string GetScrambledAnswerChoice(int usertestId, int questionId)
        {
            return _studentRep.GetScrambledAnswerChoice(usertestId, questionId);
        }

        public IEnumerable<AnswerChoice> GetAnswers(int questionId, int actionType)
        {
            return _cmsService.GetAnswers(questionId, actionType);
        }

        public IEnumerable<AssetDetail> GetDashBoardLinks(int programId)
        {
            return _studentRep.GetDashBoardLinks(programId);
        }

        public IEnumerable<AssetGroup> GetAssetGroupByProgramId(int programId)
        {
            return _studentRep.GetAssetGroupByProgramId(programId);
        }

        public IEnumerable<Test> GetQBankTest(int userId, int productId, int testSubGroupId, int timeOffSet)
        {
            return _studentRep.GetQBankTest(userId, productId, testSubGroupId, timeOffSet);
        }

        public string GetUserDetailsById(int userId)
        {
            return _studentRep.GetUserDetailsById(userId);
        }

        public LtiProvider GetLtiTestSecurityProviderByName(string ltiProviderName)
        {
            return _adminService.GetLtiTestSecurityProviderByName(ltiProviderName);
        }

        private void SetIsSkillsModule(Student student)
        {
            if (student.IsSkillsModuleTest)
            {
                //// If none of the skill module tests assigned to the student than disable skill module tests.
                IEnumerable<Test> tests = _testRep.GetUntakenTests(student.UserId, 6, 1, student.TimeOffset);
                if (tests.Count() == 0)
                {
                    student.IsSkillsModuleTest = false;
                }
            }
        }

        private void SetQBankProgramofStudy(Student student)
        {
            IEnumerable<Test> qBankTests = _studentRep.GetQBankTest(student.UserId, (int)ProductType.NCLEXRNPrep, 3, student.TimeOffset);
            qBankTests = qBankTests.OrderBy(q => q.ProgramofStudyId);
            if (qBankTests.Count() > 0)
            {
                student.QBankProgramofStudyId = qBankTests.First().ProgramofStudyId;
            }
            else if (qBankTests.Count() == 0)
            {
                student.QBankProgramofStudyId = student.ProgramofStudyId;
            }
        }
        #endregion Methods
    }
}