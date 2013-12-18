using System.Collections.Generic;
using System.Linq;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class CustomTestPresenter : AuthenticatedPresenterBase<ICustomTestView>
    {
        private const string QUERY_PARAM_TEST_ID = "TestID";
        private const string QUERY_PARAM_SEARCH_CONDITION = "SearchCondition";
        private const string QUERY_PARAM_SORT = "Sort";
        private const string QUERY_PARAM_NEWVALUE = "NewValue";
        private const string QUERY_PARAM_PAGE_MODE = "mode";
        private readonly ICMSService _cmsService;
        private ViewMode _mode;

        public CustomTestPresenter(ICMSService service)
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
                RegisterQueryParameter(QUERY_PARAM_TEST_ID);
                RegisterQueryParameter(QUERY_PARAM_SEARCH_CONDITION);
                RegisterQueryParameter(QUERY_PARAM_SORT);
            }

            if (_mode == ViewMode.List)
            {
                RegisterQueryParameter(QUERY_PARAM_PAGE_MODE);
                RegisterQueryParameter(QUERY_PARAM_SEARCH_CONDITION, QUERY_PARAM_PAGE_MODE, "1");
                RegisterQueryParameter(QUERY_PARAM_SORT, QUERY_PARAM_PAGE_MODE, "1");
            }
        }

        public void PreInitialize(ViewMode mode)
        {
            _mode = mode;
        }

        public void PopulateCustomTestDetails()
        {
            View.TestId = GetParameterValue(QUERY_PARAM_TEST_ID).ToInt();
            IEnumerable<ProgramofStudy> ProgramofStudies = _cmsService.GetProgramofStudies();
            IEnumerable<Product> product = _cmsService.GetListOfAllProducts();
            Test test = _cmsService.GetTestById(GetParameterValue(QUERY_PARAM_TEST_ID).ToInt());
            View.RenderCustomTest(ProgramofStudies, product, test);
        }

        public Test GetTestById(int testId)
        {
            return _cmsService.GetTestById(testId);
        }

        public void SaveCustomTest(Test test)
        {
            _cmsService.SaveCustomTest(test);
            NavigateToSearchCustomTest(test.TestId);
        }

        public void NavigateToSearchCustomTest(int testId)
        {
            Navigator.NavigateTo(AdminPageDirectory.CustomTest, string.Empty, string.Format("{0}={1}&CMS=1&{2}={3}&{4}={5}&{6}={7}",
              QUERY_PARAM_SEARCH_CONDITION, GetParameterValue(QUERY_PARAM_SEARCH_CONDITION), QUERY_PARAM_SORT, GetParameterValue(QUERY_PARAM_SORT), QUERY_PARAM_NEWVALUE, testId.ToString(), QUERY_PARAM_PAGE_MODE, "1"));
        }

        public void NavigateToNewCustomTest()
        {
            Navigator.NavigateTo(AdminPageDirectory.NewCustomTest, string.Empty, string.Format("{0}={1}&CMS=1&{2}={3}&{4}={5}",
              QUERY_PARAM_SEARCH_CONDITION, GetParameterValue(QUERY_PARAM_SEARCH_CONDITION), QUERY_PARAM_SORT, GetParameterValue(QUERY_PARAM_SORT), QUERY_PARAM_TEST_ID, "-1"));
        }

        public void NavigateToTestCategory()
        {
            Navigator.NavigateTo(AdminPageDirectory.TestCategories, string.Empty, string.Format("{0}={1}&CMS=1", QUERY_PARAM_PAGE_MODE, "1"));
        }

        public void SearchCustomTests(int programOfStudyId, string testName, string sortMetaData)
        {
            IEnumerable<Test> tests = _cmsService.SearchCustomTests(programOfStudyId, testName);
            View.DisplaySearchResult(tests, SortHelper.Parse(sortMetaData));
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

        public void CopyCustomTest(Test test)
        {
            _cmsService.SaveCustomTest(test);
            _cmsService.CopyCustomTest(View.TestId, test.TestId);
            NavigateToSearchCustomTest(test.TestId);
        }

        public void DeleteCustomeTest(int testId)
        {
            _cmsService.DeleteCustomTest(testId);
        }

        public void InitializeCustomTestParams()
        {
            int mode = GetParameterValue(QUERY_PARAM_PAGE_MODE).ToInt();
            if (mode == 1)
            {
                View.SearchCondition = GetParameterValue(QUERY_PARAM_SEARCH_CONDITION);
                if (!string.IsNullOrEmpty(GetParameterValue(QUERY_PARAM_SORT)))
                {
                    View.Sort = GetParameterValue(QUERY_PARAM_SORT);
                }
            }
            View.RenderCustomTest(_cmsService.GetProgramofStudies(), null, null);
        }
    }
}
