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
    public class ReportInstitutionTestByQuestionPresenter : ReportPresenterBase<IReportInstitutionTestByQuestionView>
    {
        #region Fields
        public const string QUERY_PARAM_INSTITUTIONID = "InstitutionId";
        public const string QUERY_PARAM_PRODUCTID = "ProductID";
        public const string QUERY_PARAM_TESTID = "TestID";

        private readonly IReportDataService _reportDataService;
        #endregion

         #region Constructor
        public ReportInstitutionTestByQuestionPresenter(IReportDataService service)
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
            AddParameter(testsParam, testTypeParam, cohortParam, institutionParam);
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
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues);
            View.PopulateCohorts(cohorts);
            View.HideButton();
        }

        public void PopulateTests()
        {
            IEnumerable<UserTest> tests = _reportDataService.GetTests(
                Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues,
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues,
                "0");
            View.PopulateTests(tests);
            View.HideButton();
        }

        public void GenerateReport()
        {
            var reportData = _reportDataService.GetResultsByInstitutionQuestions(
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

                reportData = _reportDataService.GetResultsByInstitutionQuestions(
                    cohortsForPage,
                    Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues.ToInt(),
                    Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt());
            }
            else
            {
                reportData = _reportDataService.GetResultsByInstitutionQuestions(
                    Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues,
                    Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues.ToInt(),
                    Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt());
            }

            View.ExportReport(reportData, printAction);
        }

        public int GetStudentNumberByCohortTest(int cohortId)
        {
            return _reportDataService.GetStudentNumberByCohortTest(
                cohortId,
                Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues.ToInt(),
                Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt());
        }

        public bool IsMultipleProgramofStudyAssignedToAdmin(int userId)
        {
            return _reportDataService.IsMultipleProgramofStudyAssignedToAdmin(userId);
        }
    }
}
