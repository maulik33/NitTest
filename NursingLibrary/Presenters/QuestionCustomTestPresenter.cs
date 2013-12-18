using System.Collections.Generic;
using System.Linq;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class QuestionCustomTestPresenter : AuthenticatedPresenterBase<IQuestionCustomTestView>
    {
        private const string QUERY_PARAM_TESTID = "TestID";
        private const string QUERY_PARAM_TESTTYPE = "TestType";
        private const string QUERY_PARAM_SEARCH_CONDITION = "SearchCondition";
        private const string QUERY_PARAM_SORT = "Sort";
        private const string Query_PARAM_PRODUCT_ID = "ProductId";
        private const string Query_PARAM_PRODUCT_NewValue = "NewValue";
        private const string QUERY_PARAM_MODE = "mode";
        private const string QUERY_PARAM_PROGRAMOFSTUDYID = "ProgramOfStudyId";
        private readonly ICMSService _cmsService;
        private readonly IReportDataService _reportDataService;
        private readonly IAdminService _adminService;
        private ViewMode _mode;

        public QuestionCustomTestPresenter(ICMSService service, IAdminService adminService, IReportDataService reportService)
            : base(Module.CMS)
        {
            _cmsService = service;
            _adminService = adminService;
            _reportDataService = reportService;
        }

        public override void RegisterQueryParameters()
        {
            if (_mode != ViewMode.Edit)
            {
                return;
            }

            RegisterQueryParameter(QUERY_PARAM_TESTID);
            RegisterQueryParameter(QUERY_PARAM_SEARCH_CONDITION);
            RegisterQueryParameter(QUERY_PARAM_SORT);
            RegisterQueryParameter(Query_PARAM_PRODUCT_ID);
            RegisterQueryParameter(QUERY_PARAM_PROGRAMOFSTUDYID);
        }

        public void PreInitialize(ViewMode mode)
        {
            _mode = mode;
        }

        public IEnumerable<QuestionResult> SerachQuestion(QuestionCriteria searchCriteria)
        {
            return _cmsService.SearchQuestions(searchCriteria);
        }

        public bool IsCustomTestExist(int testId, int productId, string testName)
        {
            bool IsTestExist = false;
            IEnumerable<Test> tests = _cmsService.GetCustomTests(productId, testName);
            if (tests.Count() > 0)
            {
                foreach (Test test in tests)
                {
                    if (test.TestId != testId)
                    {
                        IsTestExist = true;
                    }
                }
            }

            return IsTestExist;
        }

        public Test GetTestById(int testId)
        {
            return _cmsService.GetTestById(testId);
        }

        public void SaveTest(Test test)
        {
            _cmsService.SaveCustomTest(test);
        }

        public void NavigateToTestCategory(int testId, int ProductId, int programOfStudyId)
        {
            Navigator.NavigateTo(AdminPageDirectory.TestCategories, string.Empty, string.Format("{0}={1}&{2}={3}&{4}={5}&{6}={7}&CMS=1",
                    QUERY_PARAM_TESTID, testId, QUERY_PARAM_TESTTYPE, ProductId, QUERY_PARAM_PROGRAMOFSTUDYID, programOfStudyId, QUERY_PARAM_MODE, "4"));

        }

        public void NavigateToCustomTest(int testId)
        {
            Navigator.NavigateTo(AdminPageDirectory.CustomTest, string.Empty, string.Format("{0}={1}&{2}={3}&{4}={5}&{6}={7}&CMS=1",
                    QUERY_PARAM_SEARCH_CONDITION, GetParameterValue(QUERY_PARAM_SEARCH_CONDITION), QUERY_PARAM_SORT, GetParameterValue(QUERY_PARAM_SORT), Query_PARAM_PRODUCT_NewValue, testId, QUERY_PARAM_MODE, "1"));
        }

        public Question GetQuestionByQid(int qId)
        {
            return _cmsService.GetQuestionByQId(qId);
        }

        public void DeleteTestQuestions(int testId)
        {
            _cmsService.DeleteTestQuestions(testId);
        }

        public void SaveTestQuestion(IEnumerable<TestQuestion> testQuestions)
        {
            foreach (TestQuestion tq in testQuestions)
            {
                _cmsService.SaveTestQuestion(tq);
            }
        }

        public IEnumerable<QuestionResult> GetQuestionListInTest(int testId)
        {
            return _cmsService.GetQuestionListInTest(testId);
        }

        public void ShowCustomTestDetails(int programofStudyId)
        {
            View.ProductId = GetParameterValue(Query_PARAM_PRODUCT_ID).ToInt();
            int testId = GetParameterValue(QUERY_PARAM_TESTID).ToInt();
            View.TestId = testId;
            View.PopulateSearchCriteria(_adminService.GetProducts(), _cmsService.GetTitles(), _adminService.GetCategories(programofStudyId));

            if (testId != 0)
            {
                Test test = _cmsService.GetTestById(testId);
                IEnumerable<QuestionResult> questionResult = _cmsService.GetQuestionListInTest(testId);
                View.RenderQuestionCustomTest(test, questionResult);
            }
        }

        public void GetTests(int productId, int programOfStudy)
        {
            IEnumerable<Test> tests = _reportDataService.GetTestsForProgramOfStudy(productId, programOfStudy);
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

        public void InitializeProgramOfStudyParameter()
        {
            int testId = GetParameterValue(QUERY_PARAM_TESTID).ToInt();
            View.ProgramofStudyId = _cmsService.GetTestById(testId).ProgramofStudyId;
            View.PopulateProgramofStudyParameters(_adminService.GetProducts(), _cmsService.GetTitles());
        }
    }
}
