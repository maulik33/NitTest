using NursingLibrary.Interfaces;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class ReportLaunchDxrPresenter : PresenterBase<IReportLaunchDxrView>
    {
        #region Fields
        public const string QUERY_PARAM_EID = "eid";
        public const string QUERY_PARAM_CID = "cid";
        public const string QUERY_PARAM_IID = "iid";
        public const string QUERY_PARAM_FIRSTNAME = "firstname";
        public const string QUERY_PARAM_LASTNAME = "lastname";

        private readonly IReportDataService _reportDataService;
        #endregion

        #region Constructor
        public ReportLaunchDxrPresenter(IReportDataService service)
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
        }

        public void GetCaseDetails()
        {
            View.EnrolmentId = GetParameterValue(QUERY_PARAM_EID);
            View.GetCaseDetails(GetParameterValue(QUERY_PARAM_CID), GetParameterValue(QUERY_PARAM_FIRSTNAME),
                GetParameterValue(QUERY_PARAM_LASTNAME));
        }

        public override void Authorize()
        {
        }
    }
}
