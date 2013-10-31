using System;
using System.Collections.Generic;
using System.Data;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;
using System.Linq;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class StudentSummaryReportByQuestionPresenter : ReportPresenterBase<IStudentSummaryReportByQuestionView>
    {
        #region Fields

        private readonly IReportDataService _reportDataService;

        #endregion

        #region Constructor

        public StudentSummaryReportByQuestionPresenter(IReportDataService reportService)
            : base(Module.Reports)
        {
            _reportDataService = reportService;
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
            IEnumerable<UserTest> tests = _reportDataService.GetTests(
                Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues.ToInt(),
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues);
            View.PopulateTests(tests);
        }

        public void GenerateReport()
        {
            // var reportData = _reportDataService.GetStudentSummaryByQuestionDetails(
            //    Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues.ToInt()
            //    , Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues.ToInt()
            //    , Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues
            //    , Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt());
            var reportData = TransferTable(_reportDataService.GetStudentSummaryByQuestionDetails(
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues.ToInt(),
                Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues.ToInt(),
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues,
                Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt()),
                _reportDataService.GetStudentSummaryByQuestionHeader(
                Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues.ToInt(),
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues,
                Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt()));

            View.RenderReport(reportData);
        }

        public void ExportToExcel()
        {
            // var reportData = _reportDataService.GetStudentSummaryByQuestionDetails(
            //    Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues.ToInt()
            //    , Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues.ToInt()
            //    , Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues
            //    , Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt());
            var reportData = TransferTable(_reportDataService.GetStudentSummaryByQuestionDetails(
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues.ToInt(),
                Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues.ToInt(),
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues,
                Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt()),
                _reportDataService.GetStudentSummaryByQuestionHeader(
                Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues.ToInt(),
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues,
                Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt()));

            View.ExportToExcel(reportData);
        }

        public bool IsMultipleProgramofStudyAssignedToAdmin(int userId)
        {
            return _reportDataService.IsMultipleProgramofStudyAssignedToAdmin(userId);
        }

        #region Private Methods

        private DataTable TransferTable(DataTable StudentData, DataTable Header)
        {
            DataTable dtResult = new DataTable();
            DataColumn dl;
            dl = new DataColumn();
            dl.DataType = Type.GetType("System.String");
            dl.ColumnName = "Student Name";
            dtResult.Columns.Add(dl);

            dl = new DataColumn();
            dl.DataType = Type.GetType("System.Int32");
            dl.ColumnName = "StudentID";
            dtResult.Columns.Add(dl);

            foreach (DataRow headerR in Header.Rows)
            {
                if (headerR["QuestionID"].ToString() != string.Empty)
                {
                    dl = new DataColumn();
                    dl.DataType = Type.GetType("System.String");
                    dl.ColumnName = headerR["QuestionID"].ToString();
                    dtResult.Columns.Add(dl);
                }
            }

            dl = new DataColumn();
            dl.DataType = Type.GetType("System.Int32");
            dl.ColumnName = "Score";
            dtResult.Columns.Add(dl);

            string studentID = string.Empty;
            string questionID = string.Empty;
            DataRow newRow = dtResult.NewRow();
            int score = 0;
            int countStudent = 0;
            if (StudentData.Rows.Count != 0)
            {
                foreach (DataRow studentDataRow in StudentData.Rows)
                {
                    if (studentDataRow["UserID"].ToString() == studentID)
                    {
                        if (studentDataRow["QuestionID"].ToString() != questionID)
                        {
                            newRow[studentDataRow["QuestionID"].ToString()] = studentDataRow[2].ToString();
                            questionID = studentDataRow["QuestionID"].ToString();
                            if (!string.IsNullOrEmpty(studentDataRow["Correct"].ToString()))
                            {
                                score += Convert.ToInt32(studentDataRow["Correct"].ToString());
                            }
                        }
                    }
                    else
                    {
                        if (newRow == null || studentID == string.Empty)
                        {
                            ++countStudent;
                            newRow["Student Name"] = studentDataRow["StudentName"].ToString();
                            newRow["StudentID"] = studentDataRow["UserID"].ToString();
                        }
                        else
                        {
                            newRow["Score"] = score;
                            score = 0;
                            dtResult.Rows.Add(newRow);
                            newRow = dtResult.NewRow();
                            newRow["Student Name"] = studentDataRow["StudentName"].ToString();
                            newRow["StudentID"] = studentDataRow["UserID"].ToString();
                            ++countStudent;
                        }

                        newRow[studentDataRow["QuestionID"].ToString()] = studentDataRow[2].ToString();
                        questionID = studentDataRow["QuestionID"].ToString();

                        if (!string.IsNullOrEmpty(studentDataRow["Correct"].ToString()))
                        {
                            score += Convert.ToInt32(studentDataRow["Correct"].ToString());
                        }

                        studentID = studentDataRow["UserID"].ToString();
                    }
                }

                newRow["Score"] = score;
                dtResult.Rows.Add(newRow);
            }
            
            return dtResult;
        }

        #endregion
    }
}
