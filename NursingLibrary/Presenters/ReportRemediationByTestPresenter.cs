using System;
using System.Collections.Generic;
using System.Linq;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class ReportRemediationByTestPresenter : ReportPresenterBase<IReportRemediationByTestView>
    {
        #region Fields
        private readonly IReportDataService _reportDataService;

        #endregion

        #region Constructor
        public ReportRemediationByTestPresenter(IReportDataService service)
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
            ReportParameter groupParam = new ReportParameter(ReportParamConstants.PARAM_GROUP, PopulateGroup);
            ReportParameter studentParam = new ReportParameter(ReportParamConstants.PARAM_STUDENT, PopulateStudent);
            ReportParameter testTypeParam = new ReportParameter(ReportParamConstants.PARAM_TESTTYPE, PopulateProducts);

            AddParameter(programOfStudyParam);
            AddParameter(institutionParam, programOfStudyParam);
            AddParameter(cohortParam, institutionParam);
            AddParameter(groupParam, cohortParam);
            AddParameter(testTypeParam);
            AddParameter(studentParam, cohortParam, groupParam);
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

        public void PopulateGroup()
        {
            IEnumerable<Group> groups = _reportDataService.GetGroups(
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues.ToInt(),
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues);
            View.PopulateGroup(groups);
        }

        public void PopulateStudent()
        {
            IEnumerable<StudentEntity> students = _reportDataService.GetStudents(
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues,
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues,
                Parameters[ReportParamConstants.PARAM_GROUP].SelectedValues, CurrentContext.UserType);
            View.PopulateStudent(students);
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
            IEnumerable<Product> products = CacheManager.Get(
                Constants.CACHE_KEY_PRODUCTS, () => _reportDataService.GetProducts(), TimeSpan.FromHours(24));
            View.PopulateProducts(products);
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

        public void GenerateReport()
        {
            IEnumerable<TestRemediationExplainationDetails> reportData = GetReportResult();

            View.RenderReport(reportData);
        }

        public void GenerateReport(ReportAction printActions)
        {
            IEnumerable<TestRemediationExplainationDetails> reportData = GetReportResult();
            View.GenerateReport(reportData, printActions);
        }

        public bool IsMultipleProgramofStudyAssignedToAdmin(int userId)
        {
            return _reportDataService.IsMultipleProgramofStudyAssignedToAdmin(userId);
        }

        private IEnumerable<TestRemediationExplainationDetails> GetReportResult()
        {
            IEnumerable<TestRemediationExplainationDetails> reportData = new List<TestRemediationExplainationDetails>();
            if (Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues.ToInt() != -1)
            {
                reportData = _reportDataService.GetTestRemediation(Parameters[ReportParamConstants.PARAM_STUDENT].SelectedValues.ToInt(),
                Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues.ToInt(),
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues.ToInt(),
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues.ToString());
            }

            return reportData;
        }
    }
}
