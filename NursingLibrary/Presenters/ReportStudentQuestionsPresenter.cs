using System;
using System.Collections.Generic;
using System.Linq;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class ReportStudentQuestionsPresenter : ReportPresenterBase<IReportStudentQuestionsView>
    {
        #region Fields
        public const string QUERY_PARAM_PRODUCTID = "ProductID";
        public const string QUERY_PARAM_USERTESTID = "UserTestID";
        public const string QUERY_PARAM_MODE = "Mode";
        private readonly IReportDataService _reportDataService;
        #endregion

        #region Constructor
        public ReportStudentQuestionsPresenter(IReportDataService service)
            : base(Module.Reports)
        {
            _reportDataService = service;
        }
        #endregion

        public override void RegisterAuthorizationRules()
        {
        }

        public override void RegisterQueryParameters()
        {
            RegisterQueryParameter(QUERY_PARAM_ID);
            RegisterQueryParameter(QUERY_PARAM_PRODUCTID);
            RegisterQueryParameter(QUERY_PARAM_USERTESTID);
            RegisterQueryParameter(QUERY_PARAM_MODE);
        }

        public override void InitParamValues()
        {
            Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues = GetParameterValue(QUERY_PARAM_PRODUCTID);
            Parameters[ReportParamConstants.PARAM_TEST].SelectedValues = GetParameterValue(QUERY_PARAM_USERTESTID);
        }

        public void PreInitialize()
        {
            ReportParameter testTypeParam = new ReportParameter(ReportParamConstants.PARAM_TESTTYPE, PopulateProducts);
            ReportParameter testsParam = new ReportParameter(ReportParamConstants.PARAM_TEST, PopulateTests);

            AddParameter(testTypeParam);
            AddParameter(testsParam, testTypeParam);
        }

        public void PopulateInstitutions()
        {
        }

        public void PopulateProducts()
        {
            IEnumerable<Product> products = CacheManager.Get(
                Constants.CACHE_KEY_PRODUCTS, () => _reportDataService.GetProducts(), TimeSpan.FromHours(24));
            View.PopulateProducts(products);
        }

        public void PopulateTests()
        {
            IEnumerable<UserTest> tests = _reportDataService.GetTests(Id,
                Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues.ToInt());
            View.PopulateTests(tests);
        }

        public void SetControlValues()
        {
            View.SetControlValues();
        }

        public void GenerateReport()
        {
            int productId = Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues.ToInt();

            if (productId == (int)ProductType.IntegratedTesting)
            {                      
                var reportData = _reportDataService.GetRemediationTimeForTest(Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt(), "03");
                var testAssignment = _reportDataService.GetTestAssignment(Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt());

                View.RenderReportForIntegratedTest(testAssignment, reportData);
            }
            else if (productId == (int)ProductType.NCLEXRNPrep)
            {
                var reportData = _reportDataService.GetRemediationTimeForNCLXTest(Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt(), "03");
                View.RenderReportForNCLX(reportData);
            }
            else if (productId == (int)ProductType.FocusedReviewTests)
            {
                var reportData = _reportDataService.GetRemediationTimeForTest(Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt(), "03");
                View.RenderReportForFocusedReview(reportData);
            }
        }

        public void ExportReport(ReportAction printActions)
        {
            int productId = Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues.ToInt();
            IEnumerable<TestRemediationTimeDetails> reportData = new List<TestRemediationTimeDetails>();

            if (productId == (int)ProductType.IntegratedTesting)
            {
                reportData = _reportDataService.GetRemediationTimeForTest(Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt(), "03");
            }
            else if (productId == (int)ProductType.NCLEXRNPrep)
            {
                reportData = _reportDataService.GetRemediationTimeForNCLXTest(Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt(), "03");
            }
            else if (productId == (int)ProductType.FocusedReviewTests)
            {
                reportData = _reportDataService.GetRemediationTimeForTest(Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt(), "03");
            }

            var testAssignment = _reportDataService.GetTestAssignment(Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt());

            View.ExportReport(GetInstitutionNames(), GetCohortNames(), testAssignment, reportData, printActions);
        }

        public string GetStudentName()
        {
            var result = _reportDataService.GetStudentDetails(Id);
            if (result != null)
            {
                return result.LastName + "," + result.FirstName;
            }
            else
            {
                return string.Empty;
            }
        }

        public string GetInstitutionNames()
        {
            string result = string.Empty;

            // var institutes = _reportDataService.GetInstitutions(Id);
            var institutes = _reportDataService.GetInstitutionByStudentID(Id);
            if (institutes != null && institutes.ToList().Count > 0)
            {
                result = (from institute in institutes
                          select institute.InstitutionName+ " - " + institute.ProgramofStudyName).Aggregate((workingSentence, next) =>
                                                     next + ", " + workingSentence);
            }

            return result;
        }

        public string GetCohortNames()
        {
            string result = string.Empty;
            var cohorts = _reportDataService.GetCohortsForStudent(Id);
            if (cohorts != null && cohorts.ToList().Count > 0)
            {
                result = (from cohort in cohorts
                          select cohort.CohortName).Aggregate((workingSentence, next) =>
                                                      next + ", " + workingSentence);
            }

            return result;
        }

        public void NavigateToReportTestStudent(int testId, int productId)
        {
            Navigator.NavigateTo(AdminPageDirectory.ReportTestStudent, string.Empty, string.Format("{0}={1}&{2}={3}&{4}={5}&{6}={7}",
                    QUERY_PARAM_ID, Id.ToString(), QUERY_PARAM_USERTESTID, testId, QUERY_PARAM_PRODUCTID, productId, QUERY_PARAM_MODE, GetParameterValue(QUERY_PARAM_MODE)));
        }
    }
}
