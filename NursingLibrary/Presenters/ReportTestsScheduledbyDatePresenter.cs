using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class ReportTestsScheduledbyDatePresenter : ReportPresenterBase<IReportTestsScheduledbyDateView>
    {
        #region Fields
        private readonly IReportDataService _reportDataService;
        #endregion

        #region Constructor
        public ReportTestsScheduledbyDatePresenter(IReportDataService service)
            : base(Module.Reports)
        {
            _reportDataService = service;
        }

        #endregion

        public void PreInitialize()
        {
            ReportParameter programOfStudyParam = new ReportParameter(ReportParamConstants.PARAM_PROGRAM_OF_Study, PopulateProgramOfStudies);
            ReportParameter institutionParam = new ReportParameter(ReportParamConstants.PARAM_INSTITUTION, PopulateInstitutions, View.PostBack ? ParamRefreshType.None : ParamRefreshType.RefreshData);
            ReportParameter cohortParam = new ReportParameter(ReportParamConstants.PARAM_COHORT, PopulateCohorts);
            ReportParameter groupParam = new ReportParameter(ReportParamConstants.PARAM_GROUP, PopulateGroup);
            ReportParameter testTypeParam = new ReportParameter(ReportParamConstants.PARAM_TESTTYPE, PopulateProducts);

            AddParameter(programOfStudyParam);
            AddParameter(institutionParam, programOfStudyParam);
            AddParameter(cohortParam, institutionParam);
            AddParameter(groupParam, cohortParam, institutionParam);
            AddParameter(testTypeParam);
        }

        public void PopulateProgramOfStudies()
        {
            IEnumerable<ProgramofStudy> programOfStudies = _reportDataService.GetProgramOfStudies();
            var programofStudies = programOfStudies as IList<ProgramofStudy> ?? programOfStudies.ToList();
            if (!View.PostBack && programofStudies.HasElements())
            {
                var programOfStudy = programofStudies.FirstOrDefault();
                if (programOfStudy != null)
                    Parameters[ReportParamConstants.PARAM_PROGRAM_OF_Study].SelectedValues = programOfStudy.ProgramofStudyId.ToString(CultureInfo.InvariantCulture);
            }
            View.PopulateProgramOfStudies(programofStudies);
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

        public void PopulateCohorts()
        {
            IEnumerable<Cohort> cohorts = _reportDataService.GetCohorts(
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues).Where(r => r.CohortName != string.Empty);
            View.PopulateCohorts(cohorts);
        }

        public void PopulateGroup()
        {
            IEnumerable<Group> groups = _reportDataService.GetGroups(
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues,
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues);
            View.PopulateGroup(groups);
        }

        public void PopulateProducts()
        {
            IEnumerable<Product> products = CacheManager.Get(
                Constants.CACHE_KEY_PRODUCTS, () => _reportDataService.GetProducts(), TimeSpan.FromHours(24));
            View.PopulateProducts(products);
        }

        public void GenerateReport()
        {
            IEnumerable<ReportTestsScheduledbyDate> reportData = _reportDataService.GetTestsScheduledByDate(View.SelectedProgramOfStudyName,
                                                             Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues,
                                                             View.IsAllCohortsSelected ? string.Empty : Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues,
                                                             View.IsAllGroupSelected ? string.Empty : Parameters[ReportParamConstants.PARAM_GROUP].SelectedValues,
                                                             Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues, View.StartDate, View.EndDate);
            View.RenderReport(reportData);
        }

        public void GenerateReport(ReportAction printActions)
        {
            IEnumerable<ReportTestsScheduledbyDate> reportData = _reportDataService.GetTestsScheduledByDate(View.SelectedProgramOfStudyName, Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues,
                                                             View.IsAllCohortsSelected ? string.Empty : Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues,
                                                             View.IsAllGroupSelected ? string.Empty : Parameters[ReportParamConstants.PARAM_GROUP].SelectedValues,
                                                             Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues, View.StartDate, View.EndDate);
            View.GenerateReport(reportData, printActions);
        }

        public bool IsMultipleProgramofStudyAssignedToAdmin(int userId)
        {
            return _reportDataService.IsMultipleProgramofStudyAssignedToAdmin(userId);
        }
    }
}
