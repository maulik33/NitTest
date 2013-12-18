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
    /// <summary>
    /// Represents Report CohortByTest Presenter
    /// </summary>
    [Presenter]
    public class CohortByTestPresenter : ReportPresenterBase<IReportCohortByTestView>
    {
        #region Private Fields

        /// <summary>
        /// Field to store report service instance
        /// </summary>
        private readonly IReportDataService _reportDataService;

        private const string QUERY_PARAM_PROGRAMOFSTUDY = "ProgramofStudy";
        private const string QUERY_PARAM_INSTITUTION_ID = "InstitutionId";
        private const string QUERY_PARAM_PRODUCT_ID = "ProductID";
        private const string QUERY_PARAM_TEST_ID = "TestID";
        private const string QUERY_PARAM_RTYPE = "RType";
        private const string QUERY_PARAM_MODE = "Mode";
        private const string INSTITUTION = "Institution";
        private const string COHORT = "Cohort";
        private const string QUERY_PARAM_TEST_TYPE = "TestType";
        private const string SELECTED_TEST_TYPES = "SelectedTestTypes";
        private const string TEST_NAME = "TestName";
        private const string SORT = "Sort";
        private const string ACTION = "act";

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportCohortByTestPresenter"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public CohortByTestPresenter(IReportDataService service)
            : base(Module.Reports)
        {
            _reportDataService = service;
        }

        #endregion

        /// <summary>
        /// Registers the authorization rules.
        /// </summary>
        public override void RegisterAuthorizationRules()
        {
        }

        /// <summary>
        /// Registers the query parameters.
        /// </summary>
        public override void RegisterQueryParameters()
        {
        }

        /// <summary>
        /// Navigates to cohort test by question.
        /// </summary>
        /// <param name="cohortId">The cohort id.</param>
        /// <param name="institutionId">The institution id.</param>
        /// <param name="productId">The product id.</param>
        /// <param name="testId">The test id.</param>
        /// <param name="rType">Type of the r.</param>
        public void NavigateToCohortTestByQuestion(string cohortId, string institutionId, int productId, int testId, int rType, int programofStudy)
        {
            Navigator.NavigateTo(AdminPageDirectory.ReportCohortTestByQuestion, string.Empty, string.Format("{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}&{10}={11}&{12}={13}",
                    QUERY_PARAM_ID,
                    cohortId,
                    QUERY_PARAM_INSTITUTION_ID,
                    Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues.ToInt(),
                    QUERY_PARAM_PRODUCT_ID,
                    productId,
                    QUERY_PARAM_TEST_ID,
                    testId,
                    QUERY_PARAM_RTYPE,
                    rType,
                    QUERY_PARAM_MODE,
                    View.Mode,
                    QUERY_PARAM_PROGRAMOFSTUDY,
                     Parameters[ReportParamConstants.PARAM_PROGRAM_OF_Study].SelectedValues));
        }

        public void PreInitialize()
        {
            ReportParameter programOfStudyParam = new ReportParameter(ReportParamConstants.PARAM_PROGRAM_OF_Study, PopulateProgramOfStudies);
            ReportParameter institutionParam = new ReportParameter(ReportParamConstants.PARAM_INSTITUTION, PopulateInstitutions, View.PostBack ? ParamRefreshType.None : ParamRefreshType.RefreshData);
            ReportParameter cohortParam = new ReportParameter(ReportParamConstants.PARAM_COHORT, PopulateCohorts);
            ReportParameter groupParam = new ReportParameter(ReportParamConstants.PARAM_GROUP, PopulateGroup);
            ReportParameter testTypeParam = new ReportParameter(ReportParamConstants.PARAM_TESTTYPE, PopulateProducts);
            ReportParameter testsParam = new ReportParameter(ReportParamConstants.PARAM_TEST, PopulateTests);

            AddParameter(programOfStudyParam);
            AddParameter(institutionParam, programOfStudyParam);
            AddParameter(cohortParam, institutionParam);
            AddParameter(groupParam, cohortParam, institutionParam);
            AddParameter(testTypeParam);
            AddParameter(testsParam, testTypeParam, groupParam, cohortParam, institutionParam);
        }

        public void PopulateProgramOfStudies()
        {
            IEnumerable<ProgramofStudy> programOfStudies = _reportDataService.GetProgramOfStudies();
            if (!View.PostBack && programOfStudies.HasElements())
            {
                Parameters[ReportParamConstants.PARAM_PROGRAM_OF_Study].SelectedValues = programOfStudies.FirstOrDefault().ProgramofStudyId.ToString();
            }
            View.PopulateProgramOfStudies(programOfStudies);
        }

        public void PopulateGroup()
        {
            IEnumerable<Group> groups = _reportDataService.GetGroups(
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues.ToInt(),
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues);
            View.PopulateGroup(groups);
        }

        public void PopulateInstitutions()
        {
            int programofStudyId = 0;
            if (View.IsProgramofStudyVisible)
            {
                programofStudyId = Parameters[ReportParamConstants.PARAM_PROGRAM_OF_Study].SelectedValues.ToInt();
            }
            IEnumerable<Institution> institutions = _reportDataService.GetInstitutions(CurrentContext.UserId, programofStudyId, string.Empty);
            View.PopulateInstitutions(institutions);
        }

        public void PopulateProducts()
        {
            IEnumerable<Product> products = CacheManager.Get(
                Constants.CACHE_KEY_PRODUCTS, () => _reportDataService.GetProducts(), TimeSpan.FromHours(24));
            View.PopulateProducts(products);
        }

        public void PopulateCohorts()
        {
            IEnumerable<Cohort> cohorts = _reportDataService.GetCohorts(
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues.ToInt());
            View.PopulateCohorts(cohorts);
        }

        public void PopulateTests()
        {
            IEnumerable<UserTest> tests = _reportDataService.GetTests(
                Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues,
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues,
                "0");
            View.PopulateTests(tests);
        }

        public void GenerateReport()
        {
            IEnumerable<CohortByTest> reportData = _reportDataService.GetCohortByTestDetails(Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues.ToInt(),
                Parameters[ReportParamConstants.PARAM_TEST].SelectedValues,
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues,
                Parameters[ReportParamConstants.PARAM_GROUP].SelectedValues,
                Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues);

            View.RenderReport(reportData);
        }

        public void GenerateReport(ReportAction printActions)
        {
            IEnumerable<CohortByTest> reportData = _reportDataService.GetCohortByTestDetails(Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues.ToInt(),
                Parameters[ReportParamConstants.PARAM_TEST].SelectedValues,
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues,
                Parameters[ReportParamConstants.PARAM_GROUP].SelectedValues,
                Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues);

            View.GenerateReport(reportData, printActions);
        }

        public bool IsMultipleProgramofStudyAssignedToAdmin(int userId)
        {
            return _reportDataService.IsMultipleProgramofStudyAssignedToAdmin(userId);
        }
    }
}
