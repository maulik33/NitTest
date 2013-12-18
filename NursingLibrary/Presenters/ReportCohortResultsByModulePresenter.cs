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
    public class ReportCohortResultsByModulePresenter : ReportPresenterBase<IReportCohortResultsByModuleView>
    {
        #region Fields
        public const string QUERY_PARAM_INSTITUTIONID = "InstitutionId";
        public const string QUERY_PARAM_CASEID = "CaseId";
        public const string QUERY_PARAM_MODULEID = "ModuleId";
        public const string QUERY_PARAM_ACT = "act";

        private readonly IReportDataService _reportDataService;
        #endregion

        #region Constructor
        public ReportCohortResultsByModulePresenter(IReportDataService service)
            : base(Module.Reports)
        {
            _reportDataService = service;
        }
        #endregion

        public bool IsPrintInterface { get; set; }

        public override void RegisterAuthorizationRules()
        {
        }

        public override void RegisterQueryParameters()
        {
        }

        public override void InitParamValues()
        {
            if (View.IsInstitutionIdExistInQueryString)
            {
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues = GetParameterValue(QUERY_PARAM_INSTITUTIONID);
            }

            if (View.IsCohortIdExistInQueryString)
            {
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues = GetParameterValue(QUERY_PARAM_ID);
            }
            
            if (View.IsCaseIdExistInQueryString)
            {
                Parameters[ReportParamConstants.PARAM_CASE].SelectedValues = GetParameterValue(QUERY_PARAM_CASEID);
            }

            if (View.IsModuleIdExistInQueryString)
            {
                Parameters[ReportParamConstants.PARAM_MODULE].SelectedValues = GetParameterValue(QUERY_PARAM_MODULEID);
            }
        }

        public void PreInitialize()
        {
            ReportParameter programOfStudyParam = new ReportParameter(ReportParamConstants.PARAM_PROGRAM_OF_Study, PopulateProgramOfStudies);
            ReportParameter institutionParam = new ReportParameter(ReportParamConstants.PARAM_INSTITUTION, PopulateInstitutions, View.PostBack ? ParamRefreshType.None : ParamRefreshType.RefreshData);
            ReportParameter cohortParam = new ReportParameter(ReportParamConstants.PARAM_COHORT, PopulateCohorts);
            ReportParameter caseParam = new ReportParameter(ReportParamConstants.PARAM_CASE, PopulateCases);
            ReportParameter moduleParam = new ReportParameter(ReportParamConstants.PARAM_MODULE, PopulateModule);

            AddParameter(programOfStudyParam);
            AddParameter(institutionParam, programOfStudyParam);
            AddParameter(cohortParam, institutionParam);
            AddParameter(caseParam);
            AddParameter(moduleParam);
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
        }

        public void PopulateCohorts()
        {
            IEnumerable<Cohort> cohorts = _reportDataService.GetCohorts(
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues.ToInt());
            View.PopulateCohorts(cohorts);
        }

        public void PopulateTests()
        {
        }

        public void PopulateCases()
        {
            IEnumerable<CaseStudy> cases = _reportDataService.GetCaseStudies();
            View.PopulateCases(cases);
        }

        public void PopulateModule()
        {
            IEnumerable<Modules> modules = _reportDataService.GetModule();
            View.PopulateModule(modules);
        }

        public void GenerateReport()
        {
            #region Trace Information
            TraceHelper.Create(CurrentContext.TraceToken, "Cohort Results By Module Report")
                .Add("Case", Parameters[ReportParamConstants.PARAM_CASE].SelectedValues)
                .Add("Module", Parameters[ReportParamConstants.PARAM_MODULE].SelectedValues)
                .Add("Test", Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues)
                .Write();
            #endregion
            var cohortResultsbyModule = _reportDataService.GetCohortResultsbyModule(
                Parameters[ReportParamConstants.PARAM_CASE].SelectedValues.ToInt(),
                Parameters[ReportParamConstants.PARAM_MODULE].SelectedValues,
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues);

            var subCategories = _reportDataService.GetCaseSubCategories();

            View.RenderReport(cohortResultsbyModule, subCategories);
        }

        public void GenerateReport(ReportAction printActions)
        {
            var cohortResultsbyModule = _reportDataService.GetCohortResultsbyModule(
                Parameters[ReportParamConstants.PARAM_CASE].SelectedValues.ToInt(),
                Parameters[ReportParamConstants.PARAM_MODULE].SelectedValues,
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues);

            var subCategories = _reportDataService.GetCaseSubCategories();

            View.GenerateReport(cohortResultsbyModule, subCategories, printActions);
        }

        public IEnumerable<CohortResultsByModule> GetCaseSubCategoryResultbyCohortModule(string categoryName)
        {
            var caseSubCategoryResultbyCohortModule = _reportDataService.GetCaseSubCategoryResultbyCohortModule(
                Parameters[ReportParamConstants.PARAM_CASE].SelectedValues.ToInt(),
                Parameters[ReportParamConstants.PARAM_MODULE].SelectedValues,
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues, categoryName);

            return caseSubCategoryResultbyCohortModule;
        }

        public CategoryDetail GetCategoryDetails(CategoryName categoryName, int id)
        {
            return _reportDataService.GetCategories()[categoryName].Details[id];
        }

        public bool IsMultipleProgramofStudyAssignedToAdmin(int userId)
        {
            return _reportDataService.IsMultipleProgramofStudyAssignedToAdmin(userId);
        }
    }
}
