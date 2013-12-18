using System;
using System.Collections.Generic;
using System.Linq;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class ReportStudentPerformancePresenter : ReportPresenterBase<IReportStudentPerformanceView>
    {
        #region Fields
        public const string QUERY_PARAM_PRODUCTID = "ProductID";
        public const string QUERY_PARAM_USERTESTID = "UserTestID";
        public const string QUERY_PARAM_TESTID = "TestID";
        public const string QUERY_PARAM_TESTNAME = "TestName";
        public const string QUERY_PARAM_ACT = "act";
        public const string QUERY_PARAM_MODE = "Mode";
        private readonly IReportDataService _reportDataService;

        private static int _selectedTest;

        #endregion

        #region Constructor
        public ReportStudentPerformancePresenter(IReportDataService service)
            : base(Module.Reports)
        {
            _reportDataService = service;
        }
        #endregion

        public bool IsPrintInterface { get; set; }

        public void SetControlValues()
        {
            View.ProductId = GetParameterValue(QUERY_PARAM_PRODUCTID).ToInt();
            View.UserTestId = GetParameterValue(QUERY_PARAM_USERTESTID).ToInt();

            View.SetControlValues();
        }

        public IEnumerable<ResultsFromTheProgramForChart> GetResultsOfChart(int userTestId, string testCharacteristic)
        {
            return _reportDataService.GetResultsFromTheProgramForChart(userTestId, testCharacteristic);
        }

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
            if (View.IsProductTypeExistInQueryString)
            {
                Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues = GetParameterValue(QUERY_PARAM_PRODUCTID);
            }

            if (View.IsTestIdExistInQueryString)
            {
                Parameters[ReportParamConstants.PARAM_TEST].SelectedValues = GetParameterValue(QUERY_PARAM_USERTESTID);
                _selectedTest = Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt();
            }
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

        public void GenerateReport()
        {
            if (IsPrintInterface)
            {
                View.Action = ReportAction.PrintInterface;
            }
            else
            {
                View.Action = ReportAction.ShowPreview;
            }

            int userTestId = Parameters[ReportParamConstants.PARAM_TEST].SelectedValues != string.Empty ? Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt() : _selectedTest;
            #region Trace Information
            TraceHelper.Create(CurrentContext.TraceToken, "Report Student Performance")
                .Add("userTestId", userTestId.ToString())
                .Add("Test", Parameters[ReportParamConstants.PARAM_TEST].SelectedValues)
                .Write();
            #endregion
            ResultsFromTheProgram resultsFromTheProgram = _reportDataService.GetResultsFromTheProgram(userTestId, 2);
            IEnumerable<string> testCharacteristics = _reportDataService.GetTestCharacteristics(userTestId, "A");

            int testId = _reportDataService.GetUserTestByID(userTestId).FirstOrDefault().TestId;
            View.UserTestId = userTestId;
            View.RenderReport(testId, resultsFromTheProgram, testCharacteristics);
        }

        public void ExportReport(ReportAction printActions)
        {
            ResultsFromTheProgram resultsFromTheProgram1 = _reportDataService.GetResultsFromTheProgram(Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt(), 1);
            ResultsFromTheProgram resultsFromTheProgram2 = _reportDataService.GetResultsFromTheProgram(Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt(), 2);
            IEnumerable<string> testCharacteristics = _reportDataService.GetTestCharacteristics(Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt(), "A");

            View.ExportReport(GetInstitutionNames(), GetCohortNames(), resultsFromTheProgram1, resultsFromTheProgram2, testCharacteristics, printActions);
        }

        public int GetProbability(int userTestId, int NumberCorrect)
        {
            return _reportDataService.GetProbability(userTestId, NumberCorrect);
        }

        public int GetPercentileRank(int userTestId, int NumberCorrect)
        {
            return _reportDataService.GetPercentileRank(userTestId, NumberCorrect);
        }

        public int CheckProbabilityExist(int userTestId)
        {
            return _reportDataService.CheckProbabilityExist(userTestId);
        }

        public int CheckPercentileRankExist(int userTestId)
        {
            return _reportDataService.CheckPercentileRankExist(userTestId);
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

        /// <summary>
        /// Return comma seperated list of institute name
        /// </summary>
        /// <returns></returns>
        public string GetInstitutionNames()
        {
            string result = string.Empty;
            var institutes = _reportDataService.GetInstitutionByStudentID(Id);
            if (institutes != null && institutes.ToList().Count > 0)
            {
                result = (from institute in institutes
                          select institute.InstitutionName+" - "+institute.ProgramofStudyName).Aggregate((workingSentence, next) =>
                                                     next + ", " + workingSentence);
            }

            return result;
        }

        /// <summary>
        /// Return comma seperated list of cohort name
        /// </summary>
        /// <returns></returns>
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

        public void NavigateToStudentQuestionReport(int testId, int productId)
        {
            Navigator.NavigateTo(AdminPageDirectory.ReportStudentQuestion, string.Empty, string.Format("{0}={1}&{2}={3}&{4}={5}&{6}={7}",
                    QUERY_PARAM_ID, Id.ToString(), QUERY_PARAM_USERTESTID, testId, QUERY_PARAM_PRODUCTID, productId, QUERY_PARAM_MODE, GetParameterValue(QUERY_PARAM_MODE)));
        }
    }
}
