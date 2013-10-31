using NursingLibrary.Interfaces;

namespace NursingLibrary.Presenters
{
    public class ReportCohortTestByQuestionPresenter : AuthenticatedPresenterBase<IReportCohortTestByQuestionView>
    {
        private readonly IReportDataService _reportDataService;

        public ReportCohortTestByQuestionPresenter(IReportDataService service)
            : base(Module.Reports)
        {
            _reportDataService = service;
        }

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
    }
}
