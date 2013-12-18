using System;
using System.Collections.Generic;
using System.Linq;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class ReleasePresenter : AuthenticatedPresenterBase<IReleaseView>
    {
        public const string QUERY_PARAM_SHOW_QUESTIONS = "showContent";
        public const string QUERY_PARAM_SHOW_LIPPINCOTT = "showLippincott";
        public const string QUERY_PARAM_SHOW_TEST = "showTest";

        private readonly ICMSService _cmsService;
        private ViewMode _mode;

        public ReleasePresenter(ICMSService service)
            : base(Module.CMS)
        {
            _cmsService = service;
        }

        public void NavigateToReviewPage(string showContent, string showLippincott, string showTest)
        {
            Navigator.NavigateTo(AdminPageDirectory.ReleaseReview, string.Empty, string.Format("{0}={1}&{2}={3}&{4}={5}", QUERY_PARAM_SHOW_QUESTIONS, showContent, QUERY_PARAM_SHOW_LIPPINCOTT, showLippincott, QUERY_PARAM_SHOW_TEST, showTest));
        }

        public void NavigateToBackupDataPage()
        {
            Navigator.NavigateTo(AdminPageDirectory.BackupData, string.Empty, string.Format("{0}={1}&{2}={3}&{4}={5}", QUERY_PARAM_SHOW_QUESTIONS, GetParameterValue(QUERY_PARAM_SHOW_QUESTIONS), QUERY_PARAM_SHOW_LIPPINCOTT, GetParameterValue(QUERY_PARAM_SHOW_LIPPINCOTT), QUERY_PARAM_SHOW_TEST, GetParameterValue(QUERY_PARAM_SHOW_TEST)));
        }

        public override void RegisterAuthorizationRules()
        {
        }

        public override void RegisterQueryParameters()
        {
            if (_mode == ViewMode.Edit)
            {
                RegisterQueryParameter(QUERY_PARAM_SHOW_QUESTIONS);
                RegisterQueryParameter(QUERY_PARAM_SHOW_LIPPINCOTT);
                RegisterQueryParameter(QUERY_PARAM_SHOW_TEST);
            }
        }

        public void PreInitialize(ViewMode mode)
        {
            _mode = mode;
        }

        public void DisplayReviewDetails()
        {
            IEnumerable<Question> releaseQuestion = null;
            List<Remediation> remediations = null;
            List<Test> tests = null;
            IEnumerable<Lippincott> releaseLippinCotts = null;
            string showQuestion = GetParameterValue(QUERY_PARAM_SHOW_QUESTIONS).ToLower();
            string showLippinCott = GetParameterValue(QUERY_PARAM_SHOW_LIPPINCOTT).ToLower();
            string showTest = GetParameterValue(QUERY_PARAM_SHOW_TEST).ToLower();
            if (showQuestion == "y")
            {
                releaseQuestion = _cmsService.GetReleaseQuestions();
                remediations = _cmsService.GetRemediationByStatus("E").ToList();
                IEnumerable<Remediation> approvedRemediation = _cmsService.GetRemediationByStatus("A").ToList();
                if (approvedRemediation != null)
                {
                    remediations.AddRange(approvedRemediation.ToList());
                }
            }

            if (showLippinCott == "y")
            {
                releaseLippinCotts = _cmsService.GetReleaseLippinCots();
            }

            if (showTest == "y")
            {
                tests = _cmsService.GetReleaseTests("E").ToList();
                IEnumerable<Test> appoverdTests = _cmsService.GetReleaseTests("A");
                if (appoverdTests != null)
                {
                    tests.AddRange(appoverdTests.ToList());
                }
            }

            View.RenderReviewDetails(releaseQuestion, remediations, releaseLippinCotts, tests);
        }

        public void InitializeReviewPorperties()
        {
            View.ShowContent = GetParameterValue(QUERY_PARAM_SHOW_QUESTIONS).ToLower();
            View.showLippincot = GetParameterValue(QUERY_PARAM_SHOW_LIPPINCOTT).ToLower();
            View.showTests = GetParameterValue(QUERY_PARAM_SHOW_TEST).ToLower();
        }

        public void UpdateReleaseStatus(string releaseIds, string releaseStatus, string releaseChoice)
        {
            _cmsService.UpdateReleaseStatus(releaseIds, releaseStatus, releaseChoice);
        }

        public void ReleaseToProduction()
        {
            string showQuestion = GetParameterValue(QUERY_PARAM_SHOW_QUESTIONS).ToLower();
            string showLippinCott = GetParameterValue(QUERY_PARAM_SHOW_LIPPINCOTT).ToLower();
            string showTest = GetParameterValue(QUERY_PARAM_SHOW_TEST).ToLower();
            _cmsService.ReleaseToProduction(showQuestion, showLippinCott, showTest, CurrentContext.UserId);
        }

        public void ValidateAccess(bool IsProductionApplication)
        {
            if (IsProductionApplication)
            {
                throw new ApplicationException("Access denied to release to production.");
            }
        }
    }
}
