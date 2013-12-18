using System;
using System.Collections.Generic;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;
using System.Linq;


namespace NursingLibrary.Presenters
{
    [Presenter]
    public class ReportByCohortPresenter : ReportPresenterBase<IReportByCohortView>
    {
        #region Private Fields
        private const string QUERY_PARAM_PRODUCT_ID = "ProductID";
        private const string QUERY_PARAM_TEST_ID = "TestID";
        private const string QUERY_PARAM_PROGRAMOFSTUDY_ID = "ProgramOfStudy";
        /// <summary>
        /// Field to store report service instance
        /// </summary>
        private readonly IReportDataService _reportDataService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportCohortByTestPresenter"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public ReportByCohortPresenter(IReportDataService service)
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
            // RegisterQueryParameter(MODE);
        }

        public void PreInitialize()
        {
            ReportParameter programOfStudyParam = new ReportParameter(ReportParamConstants.PARAM_PROGRAM_OF_Study, PopulateProgramOfStudies);
            ReportParameter institutionParam = new ReportParameter(ReportParamConstants.PARAM_INSTITUTION, PopulateInstitutions,View.PostBack ? ParamRefreshType.None : ParamRefreshType.RefreshData);
            ReportParameter cohortParam = new ReportParameter(ReportParamConstants.PARAM_COHORT, PopulateCohorts);
            ReportParameter testTypeParam = new ReportParameter(ReportParamConstants.PARAM_TESTTYPE, PopulateProducts);
            ReportParameter testsParam = new ReportParameter(ReportParamConstants.PARAM_TEST, PopulateTests);

            AddParameter(programOfStudyParam);
            AddParameter(institutionParam, programOfStudyParam);
            AddParameter(cohortParam, institutionParam);
            AddParameter(testTypeParam);
            AddParameter(testsParam, testTypeParam, cohortParam);
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
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues);
            View.PopulateCohorts(cohorts);
        }

        public void PopulateTests()
        {
            IEnumerable<UserTest> tests = _reportDataService.GetTests(
                Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues,
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues);
            View.PopulateTests(tests);
        }

        public void GenerateReport()
        {
            IEnumerable<TestByInstitutionResults> reportData = _reportDataService.GetTestByInstitutionResults(
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues,
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues,
                Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues.ToInt(),
                Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt());

            View.RenderReport(reportData);
        }

        public void GenerateReport(ReportAction printActions)
        {
            IEnumerable<TestByInstitutionResults> reportData = _reportDataService.GetTestByInstitutionResults(
                 Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues,
                 Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues,
                 Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues.ToInt(),
                 Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt());

            View.GenerateReport(reportData, printActions);
        }

        public void NavigateToInstitutionPerfromanceReport(int institutionId)
        {
            Navigator.NavigateTo(AdminPageDirectory.ReportInstitutionPerformance, string.Empty, string.Format("{0}={1}&{2}={3}&{4}={5}&{6}={7}",
                    QUERY_PARAM_ID, institutionId,
                    QUERY_PARAM_PRODUCT_ID, Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues.ToInt(),
                    QUERY_PARAM_TEST_ID, Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt(),
                    QUERY_PARAM_PROGRAMOFSTUDY_ID, Parameters[ReportParamConstants.PARAM_PROGRAM_OF_Study].SelectedValues.ToInt()));
        }

        public bool IsMultipleProgramofStudyAssignedToAdmin(int userId)
        {
            return _reportDataService.IsMultipleProgramofStudyAssignedToAdmin(userId);
        }
    }
}
