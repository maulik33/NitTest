﻿using System;
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
    public class ReportCohortPerformancePresenter : ReportPresenterBase<IReportCohortPerformanceView>
    {
        #region Fields
        public const string QUERY_PARAM_PROGRAMOFSTUDY = "ProgramofStudyId";
        public const string QUERY_PARAM_INSTITUTIONID = "InstitutionId";
        public const string QUERY_PARAM_PRODUCTID = "ProductId";
        public const string QUERY_PARAM_TESTID = "TestId";
        public const string QUERY_PARAM_GROUPID = "GroupId";
        public const string QUERY_PARAM_ACT = "act";

        private readonly IReportDataService _reportDataService;
        #endregion

        #region Constructor
        public ReportCohortPerformancePresenter(IReportDataService service)
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
            Parameters[ReportParamConstants.PARAM_PROGRAM_OF_Study].SelectedValues = GetParameterValue(QUERY_PARAM_PROGRAMOFSTUDY);
            Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues = GetParameterValue(QUERY_PARAM_INSTITUTIONID);
            Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues = GetParameterValue(QUERY_PARAM_ID);
            Parameters[ReportParamConstants.PARAM_GROUP].SelectedValues = GetParameterValue(QUERY_PARAM_GROUPID);
            Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues = GetParameterValue(QUERY_PARAM_PRODUCTID);
            Parameters[ReportParamConstants.PARAM_TEST].SelectedValues = GetParameterValue(QUERY_PARAM_TESTID);
        }

        public void PreInitialize()
        {
            ReportParameter programOfStudyParam = new ReportParameter(ReportParamConstants.PARAM_PROGRAM_OF_Study, PopulateProgramOfStudies);
            ReportParameter institutionParam = new ReportParameter(ReportParamConstants.PARAM_INSTITUTION, PopulateInstitutions, View.PostBack ? ParamRefreshType.None : ParamRefreshType.RefreshData);
            ReportParameter cohortParam = new ReportParameter(ReportParamConstants.PARAM_COHORT, PopulateCohorts);
            ReportParameter groupParam = new ReportParameter(ReportParamConstants.PARAM_GROUP, PopulateGroup);
            ReportParameter testTypeParam = new ReportParameter(ReportParamConstants.PARAM_TESTTYPE, PopulateProducts);
            ReportParameter testsParam = new ReportParameter(ReportParamConstants.PARAM_TEST, PopulateTests);

            AddParameter(programOfStudyParam);
            AddParameter(institutionParam, programOfStudyParam);
            AddParameter(cohortParam, institutionParam);
            AddParameter(groupParam, cohortParam, institutionParam);
            AddParameter(testTypeParam, groupParam, cohortParam, institutionParam);
            AddParameter(testsParam, testTypeParam, groupParam, cohortParam, institutionParam);
        }

        public void PopulateProgramOfStudies()
        {
            IEnumerable<ProgramofStudy> programOfStudies = _reportDataService.GetProgramOfStudies();
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

        public void PopulateGroup()
        {
            IEnumerable<Group> groups = _reportDataService.GetGroups(
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues.ToInt(),
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues);
            View.PopulateGroup(groups);
        }

        public void PopulateTests()
        {
            IEnumerable<UserTest> tests = _reportDataService.GetTests(
                Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues,
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues,
                "0");
            View.PopulateTests(tests);
        }

        public void GenerateReport()
        {
            if (IsPrintInterface)
            {
                View.Action = ReportAction.PrintInterface;
            }
            else
            {
                View.Action = ReportAction.ShowPreview;
            }

            ResultsFromTheProgram resultsFromTheProgram = new ResultsFromTheProgram();
            #region Trace Information
            TraceHelper.Create(CurrentContext.TraceToken, "Report Cohort Performance")
                .Add("Institutions", Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues)
                .Add("Test Type", Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues)
                .Add("Test", Parameters[ReportParamConstants.PARAM_TEST].SelectedValues)
                .Add("Cohort", Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues)
                .Write();
            #endregion
            if (Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues.ToInt() == 0)
            {
                resultsFromTheProgram = _reportDataService.GetQuestionResultsForInstitution(
                    Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues.ToInt(),
                    Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues,
                    Parameters[ReportParamConstants.PARAM_TEST].SelectedValues,
                    2);
            }
            else
            {
                resultsFromTheProgram = _reportDataService.GetQuestionResultsForCohort(
                    Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues,
                    Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues,
                    Parameters[ReportParamConstants.PARAM_TEST].SelectedValues,
                    2,
                    Parameters[ReportParamConstants.PARAM_GROUP].SelectedValues.Replace("-1", string.Empty));
            }

            decimal norm = _reportDataService.GetNormForTest(Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt());
            IEnumerable<string> testCharacteristics = _reportDataService.GetTestCharacteristicsByTestId(Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt(), "A");
            View.RenderReport(resultsFromTheProgram, norm, testCharacteristics);
        }

        public void GenerateReport(ReportAction printActions)
        {
            ResultsFromTheProgram resultsFromTheProgram = new ResultsFromTheProgram();
            if (Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues.ToInt() == 0)
            {
                resultsFromTheProgram = _reportDataService.GetQuestionResultsForInstitution(
                    Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues.ToInt(),
                    Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues,
                    Parameters[ReportParamConstants.PARAM_TEST].SelectedValues,
                    2);
            }
            else
            {
                resultsFromTheProgram = _reportDataService.GetQuestionResultsForCohort(
                    Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues,
                    Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues,
                    Parameters[ReportParamConstants.PARAM_TEST].SelectedValues,
                    2,
                    Parameters[ReportParamConstants.PARAM_GROUP].SelectedValues.Replace("-1", string.Empty));
            }

            decimal norm = _reportDataService.GetNormForTest(Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt());
            IEnumerable<string> testCharacteristics = _reportDataService.GetTestCharacteristicsByTestId(Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt(), "A");
            View.GenerateReport(resultsFromTheProgram, norm, testCharacteristics, printActions);
        }

        public IEnumerable<ResultsFromTheProgramForChart> GetResultsForChart(string chartType, string fromDate, string toDate)
        {
            if (Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues.ToInt() == 0)
            {
                return _reportDataService.GetResultsFromInstitutionForChart(
                    Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues,
                    Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues,
                    Parameters[ReportParamConstants.PARAM_TEST].SelectedValues,
                    chartType, fromDate, toDate);
            }
            else
            {
                return _reportDataService.GetResultsFromCohortForChart(
                    Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues,
                    Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues,
                    Parameters[ReportParamConstants.PARAM_TEST].SelectedValues,
                    chartType, fromDate, toDate);
            }
        }

        public void ShowPrinterFriendlyVersion()
        {
            Navigator.NavigateTo(AdminPageDirectory.ReportCohortPerformance, string.Empty, string.Format("{0}={1}&{2}={3}&{4}={5}&{6}={7}",
             QUERY_PARAM_ACT, ReportAction.PrintInterface,
             QUERY_PARAM_INSTITUTIONID, Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues.ToInt(),
             QUERY_PARAM_ID, Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues,
             QUERY_PARAM_PRODUCTID, Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues));
        }

        public decimal GetPercentage()
        {
            IEnumerable<CohortByTest> reportData = _reportDataService.GetCohortByTestDetails(Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues.ToInt(),
                Parameters[ReportParamConstants.PARAM_TEST].SelectedValues,
                Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues,
                Parameters[ReportParamConstants.PARAM_GROUP].SelectedValues.Replace("-1", string.Empty),
                Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues);

            CohortByTest result = null;
            decimal percentage = 0;

            if (reportData != null)
            {
                result = reportData.FirstOrDefault();
                if (result != null)
                {
                    percentage = result.Percentage;
                }
            }

            return percentage;
        }

        public string GetGraphData(int chartType)
        {
            string strCategoriesXML = string.Empty;
            string strXml = string.Empty;
            if (chartType == 1)
            {
                strXml = "<graph canvasBgColor='E2EBF6' canvasBaseColor='ADC4E4'  xaxisname=\" \" yaxisname=\"\" hovercapbg=\"DEDEBE\" hovercapborder=\"889E6D\" rotateNames=\"0\" animation=\"1\" yAxisMaxValue=\"100\" numdivlines=\"9\" divLineColor=\"CCCCCC\" divLineAlpha=\"80\" decimalPrecision=\"0\"  showAlternateVGridColor=\"1\" AlternateVGridAlpha=\"30\" AlternateVGridColor=\"CCCCCC\" caption=\"\" canvasBorderThickness='1'   canvasBorderColor='000066' baseFont='Verdana' baseFontSize='11' ShowLegend='0'>";
                strCategoriesXML = GetStudentTestXYAxsis();
            }
            else if (chartType == 2)
            {
                strXml = "<graph canvasBgColor='E2EBF6' canvasBaseColor='ADC4E4'  xaxisname=\" \" yaxisname=\"\" hovercapbg=\"DEDEBE\" hovercapborder=\"889E6D\" rotateNames=\"0\" animation=\"1\" yAxisMaxValue=\"100\" numdivlines=\"9\" divLineColor=\"CCCCCC\" divLineAlpha=\"80\" decimalPrecision=\"0\"  showAlternateVGridColor=\"1\" AlternateVGridAlpha=\"30\" AlternateVGridColor=\"CCCCCC\" caption=\"\" canvasBorderThickness='1'   canvasBorderColor='000066' baseFont='Verdana' baseFontSize='11' ShowLegend='0'>";
                strCategoriesXML = GetTestRankXYAxsis();
            }

            strXml = strXml + strCategoriesXML + "</graph>";
            return strXml;
        }

        public bool IsMultipleProgramofStudyAssignedToAdmin(int userId)
        {
            return _reportDataService.IsMultipleProgramofStudyAssignedToAdmin(userId);
        }

        private string GetStudentTestXYAxsis()
        {
            StringBuilder strSetXML = new StringBuilder();

            string[] periods = new string[1];

            var resultsFromTheProgram = _reportDataService.GetQuestionResultsForCohort(
                    Parameters[ReportParamConstants.PARAM_COHORT].SelectedValues,
                    Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues,
                    Parameters[ReportParamConstants.PARAM_TEST].SelectedValues,
                    1);

            strSetXML.Append("<categories>");
            strSetXML.Append("<category name=\"\" /> ");
            strSetXML.Append("</categories>");
            strSetXML.Append("<dataset  color=\"E97595\" showValues=\"1\"> ");

            string svalue;
            if (resultsFromTheProgram != null)
            {
                if (resultsFromTheProgram.Total == 0)
                {
                    svalue = "0.000001";
                }
                else
                {
                    svalue = Convert.ToString(resultsFromTheProgram.Total);
                }

                strSetXML.Append("<set name='" + string.Empty + "' hoverText='" + string.Empty + "' value='" + svalue + "'/>");
            }

            strSetXML.Append("</dataset>");

            return strSetXML.ToString();
        }

        private string GetTestRankXYAxsis()
        {
            StringBuilder strSetXML = new StringBuilder();
            string[] periods = new string[1];

            decimal rank = _reportDataService.GetRankForTest(Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt());

            strSetXML.Append("<categories>");
            strSetXML.Append("<category name=\"\" /> ");

            strSetXML.Append("</categories>");
            strSetXML.Append("<dataset  color=\"E97595\" showValues=\"1\"> ");

            string svalue;
            if (rank == 0)
            {
                svalue = "0.000001";
            }
            else
            {
                svalue = Convert.ToString(rank);
            }

            strSetXML.Append("<set name='" + string.Empty + "' hoverText='" + string.Empty + "' value='" + svalue + "'/>");
            strSetXML.Append("</dataset>");

            return strSetXML.ToString();
        }

    }
}