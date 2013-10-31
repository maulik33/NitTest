using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class ReportEnglishForNursingTrackingPresenter : ReportPresenterBase<IReportEnglishForNursingTrackingView>
    {
        #region Fields
        private readonly IReportDataService _reportDataService;
        #endregion

        #region Constructor
        public ReportEnglishForNursingTrackingPresenter(IReportDataService service)
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
            ReportParameter studentParam = new ReportParameter(ReportParamConstants.PARAM_STUDENT, PopulateStudent);
            ReportParameter testsParam = new ReportParameter(ReportParamConstants.PARAM_TEST, PopulateTests);
            ReportParameter qidParam = new ReportParameter(ReportParamConstants.PARAM_QID, PopulateQid);

            AddParameter(programOfStudyParam);
            AddParameter(institutionParam, programOfStudyParam);
            AddParameter(cohortParam, institutionParam);
            AddParameter(studentParam, cohortParam);
            AddParameter(testsParam, cohortParam, studentParam);
            AddParameter(qidParam, cohortParam, studentParam, testsParam);
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

        public void PopulateStudent()
        {
            if (Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues != string.Empty)
            {
                IEnumerable<StudentEntity> students = _reportDataService.GetStudents(
                    Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues,
                    Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues);
                View.PopulateStudent(students);
            }
            else
            {
                View.PopulateStudent(null);
            }
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

        public void PopulateTests()
        {
            IEnumerable<UserTest> tests = _reportDataService.GetTestsForEnglishNursingTracking(
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues,
                Parameters[ReportParamConstants.PARAM_STUDENT].SelectedValues);
            View.PopulateTests(tests);
        }

        public void PopulateQid()
        {
            IEnumerable<Question> qid = _reportDataService.GetQIDForEnglishNursingTracking(
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues,
                Parameters[ReportParamConstants.PARAM_STUDENT].SelectedValues,
                Parameters[ReportParamConstants.PARAM_TEST].SelectedValues);
            View.PopulateQid(qid);
        }

        public void GenerateReport()
        {
            IEnumerable<EnglishForNursingTracking> reportData = _reportDataService.GetEnglishNursingTrackingDetails(
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues,
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues,
                Parameters[ReportParamConstants.PARAM_STUDENT].SelectedValues,
                Parameters[ReportParamConstants.PARAM_TEST].SelectedValues,
                Parameters[ReportParamConstants.PARAM_QID].SelectedValues);
            View.RenderReport(reportData);
        }

        public void GenerateReport(ReportAction printActions)
        {
            IEnumerable<EnglishForNursingTracking> reportData = _reportDataService.GetEnglishNursingTrackingDetails(
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues,
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues,
                Parameters[ReportParamConstants.PARAM_STUDENT].SelectedValues,
                Parameters[ReportParamConstants.PARAM_TEST].SelectedValues,
                Parameters[ReportParamConstants.PARAM_QID].SelectedValues);
            View.GenerateReport(reportData, printActions);
        }

        public void ValidateAccess()
        {
            if (CurrentContext.UserType != UserType.SuperAdmin)
            {
                View.DisableAccess();
            }
        }

        public bool IsMultipleProgramofStudyAssignedToAdmin(int userId)
        {
            return _reportDataService.IsMultipleProgramofStudyAssignedToAdmin(userId);
        }
    }
}
