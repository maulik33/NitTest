using System;
using System.Collections.Generic;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class ReportPerformanceByQuestionToExcelPresenter : ReportPresenterBase<IReportPerformanceByQuestionToExcelView>
    {
        #region Private Fields
        private const string QUERY_PARAM_PRODUCT_ID = "ProductID";
        private const string QUERY_PARAM_TEST_ID = "TestID";
        private const string QUERY_PARAM_COHORT_ID = "CohortId";

        /// <summary>
        /// Field to store report service instance
        /// </summary>
        private readonly IReportDataService _reportService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportCohortByTestPresenter"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public ReportPerformanceByQuestionToExcelPresenter(IReportDataService service)
            : base(Module.Reports)
        {
            _reportService = service;
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

        public void PreInitialize()
        {  
        }

        public void PopulateInstitutions()
        {
        }

        public void PopulateProducts()
        {
        }

        public void PopulateCohorts()
        {
        }

        public void PopulateTests()
        {
        }

        public void GenerateReport()
        {
        }

        public void ExportToExcel()
        {
            IEnumerable<SummaryPerformanceByQuestionResult> reportData = _reportService.GetSummaryPerformanceByQuestionReportResult(
                GetParameterValue(QUERY_PARAM_COHORT_ID),
                GetParameterValue(QUERY_PARAM_PRODUCT_ID).ToInt(),
                GetParameterValue(QUERY_PARAM_TEST_ID).ToInt());

            View.RenderReport(reportData);
        }
    }
}
