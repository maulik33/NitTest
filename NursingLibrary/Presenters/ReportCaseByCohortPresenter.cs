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
    public class ReportCaseByCohortPresenter : ReportPresenterBase<IReportCaseByCohortView>
    {
        #region Fields
        private const string QUERY_PARAM_INSTITUTION_ID = "InstitutionId";
        private const string QUERY_PARAM_CASE_ID = "CaseId";
        private const string QUERY_PARAM_MODULE_ID = "ModuleId";
        private const string QUERY_PARAM_PROGRAMOFSTUDY_ID = "ProgramOfStudy";

        private readonly IReportDataService _reportDataService;

        #endregion

        #region Constructor
        public ReportCaseByCohortPresenter(IReportDataService service)
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

        public void PreInitialize()
        {
            ReportParameter programOfStudyParam = new ReportParameter(ReportParamConstants.PARAM_PROGRAM_OF_Study, PopulateProgramOfStudies);
            ReportParameter institutionParam = new ReportParameter(ReportParamConstants.PARAM_INSTITUTION, PopulateInstitutions, View.PostBack ? ParamRefreshType.None : ParamRefreshType.RefreshData);
            ReportParameter caseParam = new ReportParameter(ReportParamConstants.PARAM_CASE, PopulateCases);
            ReportParameter moduleParam = new ReportParameter(ReportParamConstants.PARAM_MODULE, PopulateModule);

            AddParameter(programOfStudyParam);
            AddParameter(institutionParam, programOfStudyParam);
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
            IEnumerable<CaseByCohortResult> reportData = _reportDataService.GetCaseByCohort(
                 Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues,
                 Parameters[ReportParamConstants.PARAM_CASE].SelectedValues.ToInt(),
                 Parameters[ReportParamConstants.PARAM_MODULE].SelectedValues.ToInt());
            View.RenderReport(reportData);
        }

        public void GenerateReport(ReportAction printActions)
        {
            IEnumerable<CaseByCohortResult> reportData = _reportDataService.GetCaseByCohort(
                 Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues,
                 Parameters[ReportParamConstants.PARAM_CASE].SelectedValues.ToInt(),
                 Parameters[ReportParamConstants.PARAM_MODULE].SelectedValues.ToInt());
            View.GenerateReport(reportData, printActions);
        }

        public void NavigateToReportCohortResultByModule(int institutionId, int cohortId)
        {
            Navigator.NavigateTo(AdminPageDirectory.ReportCohortResultByModule, string.Empty, string.Format("{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}",
                    QUERY_PARAM_ID, cohortId,
                    QUERY_PARAM_INSTITUTION_ID, institutionId,
                    QUERY_PARAM_CASE_ID, Parameters[ReportParamConstants.PARAM_CASE].SelectedValues.ToInt(),
                    QUERY_PARAM_MODULE_ID, Parameters[ReportParamConstants.PARAM_MODULE].SelectedValues.ToInt(),
                    QUERY_PARAM_PROGRAMOFSTUDY_ID, Parameters[ReportParamConstants.PARAM_PROGRAM_OF_Study].SelectedValues.ToInt()));
        }

        public bool IsMultipleProgramofStudyAssignedToAdmin(int userId)
        {
            return _reportDataService.IsMultipleProgramofStudyAssignedToAdmin(userId);
        }

    }
}
