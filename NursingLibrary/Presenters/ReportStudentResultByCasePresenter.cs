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
    public class ReportStudentResultByCasePresenter : ReportPresenterBase<IReportStudentResultByCaseView>
    {
        #region Private Fields

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
        public ReportStudentResultByCasePresenter(IReportDataService service)
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
        }

        public void PreInitialize()
        {
            ReportParameter programOfStudyParam = new ReportParameter(ReportParamConstants.PARAM_PROGRAM_OF_Study, PopulateProgramOfStudies);
            ReportParameter institutionParam = new ReportParameter(ReportParamConstants.PARAM_INSTITUTION, PopulateInstitutions, View.PostBack ? ParamRefreshType.None : ParamRefreshType.RefreshData);
            ReportParameter cohortParam = new ReportParameter(ReportParamConstants.PARAM_COHORT, PopulateCohorts);
            ReportParameter caseParam = new ReportParameter(ReportParamConstants.PARAM_CASE, PopulateCases);

            AddParameter(programOfStudyParam);
            AddParameter(institutionParam, programOfStudyParam);
            AddParameter(cohortParam, institutionParam);
            AddParameter(caseParam);
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

        public void GenerateReport(string searchCriteria)
        {
            IEnumerable<StudentEntity> reportData = _reportDataService.GetListOfStudents(
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues.ToInt(),
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues.ToInt(),
                Parameters[ReportParamConstants.PARAM_CASE].SelectedValues.ToInt(),
                searchCriteria);

            View.RenderReport(reportData);
        }

        public bool IsMultipleProgramofStudyAssignedToAdmin(int userId)
        {
            return _reportDataService.IsMultipleProgramofStudyAssignedToAdmin(userId);
        }
    }
}
