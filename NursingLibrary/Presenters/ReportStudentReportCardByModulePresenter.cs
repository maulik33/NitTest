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
    public class ReportStudentReportCardByModulePresenter : ReportPresenterBase<IReportStudentReportCardByModuleView>
    {
        #region Fields
        private readonly IReportDataService _reportDataService;

        #endregion

        #region Constructor
        public ReportStudentReportCardByModulePresenter(IReportDataService service)
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
            ReportParameter cohortParam = new ReportParameter(ReportParamConstants.PARAM_COHORT, PopulateCohorts);
            ReportParameter studentParam = new ReportParameter(ReportParamConstants.PARAM_STUDENT, PopulateStudent);
            ReportParameter caseParam = new ReportParameter(ReportParamConstants.PARAM_CASE, PopulateCases);
            ReportParameter moduleParam = new ReportParameter(ReportParamConstants.PARAM_MODULE, PopulateModule);

            AddParameter(programOfStudyParam);
            AddParameter(institutionParam, programOfStudyParam);
            AddParameter(cohortParam, institutionParam);
            AddParameter(studentParam, cohortParam);
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
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues);
            View.PopulateCohorts(cohorts);
        }

        public void PopulateTests()
        {
        }

        public void PopulateStudent()
        {
            IEnumerable<StudentEntity> students = _reportDataService.GetStudents(
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues,
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues,
                string.Empty, CurrentContext.UserType);
            View.PopulateStudent(students);
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
            IEnumerable<ResultsForStudentReportCardByModule> reportData = _reportDataService.GetResultsForStudentReportCardByModule(
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues,
                Parameters[ReportParamConstants.PARAM_CASE].SelectedValues.ToInt(),
                View.CaseName,
                Parameters[ReportParamConstants.PARAM_MODULE].SelectedValues,
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues.ToInt(),
                Parameters[ReportParamConstants.PARAM_STUDENT].SelectedValues);
            View.RenderReport(reportData);
        }

        public void GenerateReport(ReportAction printActions)
        {
            IEnumerable<ResultsForStudentReportCardByModule> reportData = _reportDataService.GetResultsForStudentReportCardByModule(
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues,
                Parameters[ReportParamConstants.PARAM_CASE].SelectedValues.ToInt(),
                View.CaseName,
                Parameters[ReportParamConstants.PARAM_MODULE].SelectedValues,
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues.ToInt(),
                Parameters[ReportParamConstants.PARAM_STUDENT].SelectedValues);
            View.GenerateReport(reportData, printActions);
        }

        public bool IsMultipleProgramofStudyAssignedToAdmin(int userId)
        {
            return _reportDataService.IsMultipleProgramofStudyAssignedToAdmin(userId);
        }
    }
}
