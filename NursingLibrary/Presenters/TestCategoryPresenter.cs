using System.Collections.Generic;
using System.Linq;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class TestCategoryPresenter : AuthenticatedPresenterBase<ITestCategoryView>
    {
        private readonly ICMSService _cmsService;
        private readonly IAdminService _adminService;

        private const string QUERY_PARAM_MODE = "mode";
        private const string QUERY_PARAM_TESTTYPE = "TestType";
        private const string QUERY_PARAM_TESTID = "TestID";
        private const string QUERY_PARAM_PROGRAMOFSTUDYID = "ProgramOfStudyId";

        public TestCategoryPresenter(IAdminService adminService, ICMSService service)
            : base(Module.CMS)
        {
            _adminService = adminService;
            _cmsService = service;
        }

        public override void RegisterAuthorizationRules()
        {
        }

        public override void RegisterQueryParameters()
        {
            RegisterQueryParameter(QUERY_PARAM_MODE);
            RegisterQueryParameter(QUERY_PARAM_TESTTYPE, QUERY_PARAM_MODE, "4");
            RegisterQueryParameter(QUERY_PARAM_TESTID, QUERY_PARAM_MODE, "4");
            RegisterQueryParameter(QUERY_PARAM_PROGRAMOFSTUDYID, QUERY_PARAM_MODE, "4");
        }

        public void DisplayTestCategoryDetails()
        {
            View.TestId = GetParameterValue(QUERY_PARAM_TESTID).ToInt();
            View.TestType = GetParameterValue(QUERY_PARAM_TESTTYPE).ToInt();
            View.ProgramOfStudyId = GetParameterValue(QUERY_PARAM_PROGRAMOFSTUDYID).ToInt();

            IEnumerable<ProgramofStudy> programofStudies = _cmsService.GetProgramofStudies();
            IEnumerable<Product> products = _cmsService.GetListOfAllProducts();
            IEnumerable<Test> tests;
            if (View.TestId > 0 && View.TestType > 0 && View.ProgramOfStudyId > 0)
            {
                tests = _cmsService.GetTests(View.TestType, 0, false, View.ProgramOfStudyId);
            }
            else
            {
                tests = new List<Test>();
            }

            View.RenderTestCategories(programofStudies, products, tests);
        }

        public IEnumerable<Category> GetCategories(int programOfStudyId)
        {
            IDictionary<CategoryName, Category> categories = _adminService.GetCategories(programOfStudyId);
            return categories.Values.ToList();
        }

        public IEnumerable<CategoryDetail> GetTestcategoriesForTestQuestions(int testId, int testType)
        {
            return _cmsService.GetTestcategoriesForTestQuestions(testId, testType);
        }

        public void AssignTestCategory(int testId, int categoryId, int student, int admin)
        {
            _cmsService.SaveTestCategory(testId, categoryId, student, admin);
        }

        public IEnumerable<Test> GetTests(int productId, int programOfStudy)
        {
            return _cmsService.GetTests(productId, 0, false, programOfStudy);
        }

        public IEnumerable<Product> GetProducts() // product list does NOT depend on program of study so no need to change
        {
            return _cmsService.GetListOfAllProducts();
        }

        public IEnumerable<TestCategory> GetTestCategories(int testId)
        {
            return _cmsService.GetTestCategories(testId);
        }
    }
}
