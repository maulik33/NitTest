using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class ReportTestResultByQuestionPresenter : ReportPresenterBase<IReportTestResultByQuestionView>
    {
        #region Fields
        public const string QUERY_PARAM_MODE = "Mode";
        public const string QUERY_PARAM_PROGRAMOFSTUDY = "ProgramofStudy";
        public const string QUERY_PARAM_INSTITUTIONID = "InstitutionId";
        public const string QUERY_PARAM_PRODUCTID = "ProductID";
        public const string QUERY_PARAM_TESTID = "TestID";
        public const string QUERY_PARAM_RTYPE = "RType";

        private readonly IReportDataService _reportDataService;
        #endregion

        #region Constructor
        public ReportTestResultByQuestionPresenter(IReportDataService service)
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

        public override void InitParamValues()
        {
            if (View.IsModeExistInQueryString)
            {
                View.Mode = GetParameterValue(QUERY_PARAM_MODE);
            }

            if (View.IsProgramofStudyExistInQueryString)
            {
                Parameters[ReportParamConstants.PARAM_PROGRAM_OF_Study].SelectedValues = GetParameterValue(QUERY_PARAM_PROGRAMOFSTUDY);
            }

            if (View.IsInstitutionIdExistInQueryString)
            {
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues = GetParameterValue(QUERY_PARAM_INSTITUTIONID);
            }

            if (View.IsCohortIdExistInQueryString)
            {
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues = GetParameterValue(QUERY_PARAM_ID);
            }

            if (View.IsTestTypeIdExistInQueryString)
            {
                Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues = GetParameterValue(QUERY_PARAM_PRODUCTID);
            }

            if (View.IsTestIdExistInQueryString)
            {
                Parameters[ReportParamConstants.PARAM_TEST].SelectedValues = GetParameterValue(QUERY_PARAM_TESTID);
            }

            if (View.IsRTypeExistInQueryString)
            {
                View.RType = GetParameterValue(QUERY_PARAM_RTYPE);
            }
            else
            {
                View.RType = string.Empty;
            }
        }

        public void PreInitialize()
        {
            ReportParameter programOfStudyParam = new ReportParameter(ReportParamConstants.PARAM_PROGRAM_OF_Study, PopulateProgramOfStudies);
            ReportParameter institutionParam = new ReportParameter(ReportParamConstants.PARAM_INSTITUTION, PopulateInstitutions, View.PostBack ? ParamRefreshType.None : ParamRefreshType.RefreshData);
            ReportParameter cohortParam = new ReportParameter(ReportParamConstants.PARAM_COHORT, PopulateCohorts);
            ReportParameter testTypeParam = new ReportParameter(ReportParamConstants.PARAM_TESTTYPE, PopulateProducts);
            ReportParameter testsParam = new ReportParameter(ReportParamConstants.PARAM_TEST, PopulateTests);

            AddParameter(programOfStudyParam);
            AddParameter(institutionParam, programOfStudyParam);
            AddParameter(cohortParam, institutionParam);
            AddParameter(testTypeParam, cohortParam, institutionParam);
            AddParameter(testsParam, testTypeParam);
        }

        public void PopulateProgramOfStudies()
        {
            IEnumerable<ProgramofStudy> programOfStudies = _reportDataService.GetProgramOfStudies();
            if (!View.PostBack && programOfStudies.HasElements())
            {
                Parameters[ReportParamConstants.PARAM_PROGRAM_OF_Study].SelectedValues = View.IsProgramofStudyExistInQueryString ? Parameters[ReportParamConstants.PARAM_PROGRAM_OF_Study].SelectedValues : programOfStudies.FirstOrDefault().ProgramofStudyId.ToString();
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
            View.HideButton();
        }

        public void PopulateProducts()
        {
            IEnumerable<Product> products = CacheManager.Get(
                Constants.CACHE_KEY_PRODUCTS, () => _reportDataService.GetProducts(), TimeSpan.FromHours(24));
            View.PopulateProducts(products);
            View.HideButton();
        }

        public void PopulateCohorts()
        {
            IEnumerable<Cohort> cohorts = _reportDataService.GetCohorts(
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues.ToInt());
            View.PopulateCohorts(cohorts);
            View.HideButton();
        }

        public void PopulateTests()
        {
            IEnumerable<UserTest> tests = _reportDataService.GetTestByProdCohortId(
                Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues,
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues);
            View.PopulateTests(tests);
            View.HideButton();
        }

        public void GenerateReport()
        {
            var reportData = _reportDataService.GetResultsByCohortQuestions(
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues,
                Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues.ToInt(),
                Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt());
            View.RenderReport(reportData);
        }

        public void GenerateReport(ReportAction printAction)
        {
            DataTable reportData = null;
            string[] cohortIds = null;

            if (View.IsSelectedCohorts)
            {
                cohortIds = Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues.Split('|');
                var cohortsForPage = string.Join("|", (from c in cohortIds
                                                       select c).Skip(View.CohortStartIndex).Take(View.CohortEndIndex).ToArray());

                reportData = _reportDataService.GetResultsByCohortQuestions(
                    cohortsForPage,
                    Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues.ToInt(),
                    Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt());
            }
            else
            {
                reportData = _reportDataService.GetResultsByCohortQuestions(
                    Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues,
                    Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues.ToInt(),
                    Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt());
            }

            View.ExportReport(reportData, printAction);
        }

        public bool IsMultipleProgramofStudyAssignedToAdmin(int userId)
        {
            return _reportDataService.IsMultipleProgramofStudyAssignedToAdmin(userId);
        }
    }
}
