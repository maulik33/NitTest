using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NursingLibrary.Common;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Utilities;


namespace NursingLibrary.Presenters
{
    [Presenter]
    public class ReportInstitutionPerformancePresenter : ReportPresenterBase<IReportInstitutionPerformanceView>
    {
        #region Fields
        public const string QUERY_PARAM_PRODUCTID = "ProductID";
        public const string QUERY_PARAM_TESTID = "TestID";
        public const string QUERY_PARAM_ACT = "act";
        public const string QUERY_PARAM_PROGRAMOFSTUDYID = "ProgramOfStudy";

        private readonly IReportDataService _reportDataService;
        #endregion

        #region Constructor
        public ReportInstitutionPerformancePresenter(IReportDataService service)
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
            if (View.IsIdExistInQueryString)
            {
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues = GetParameterValue(QUERY_PARAM_ID);
            }

            if (View.IsProductIdExistInQueryString)
            {
                Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues = GetParameterValue(QUERY_PARAM_PRODUCTID);
            }

            if (View.IsTestIdExistInQueryString)
            {
                Parameters[ReportParamConstants.PARAM_TEST].SelectedValues = GetParameterValue(QUERY_PARAM_TESTID);
            }

            if (View.IsProgramOfStudyIdExistInQueryString)
            {
                Parameters[ReportParamConstants.PARAM_PROGRAM_OF_Study].SelectedValues = GetParameterValue(QUERY_PARAM_PROGRAMOFSTUDYID);
            }
        }

        public void PreInitialize()
        {
            ReportParameter programOfStudyParam = new ReportParameter(ReportParamConstants.PARAM_PROGRAM_OF_Study, PopulateProgramOfStudies);
            ReportParameter institutionParam = new ReportParameter(ReportParamConstants.PARAM_INSTITUTION, PopulateInstitutions, View.PostBack ? ParamRefreshType.None : ParamRefreshType.RefreshData);
            ReportParameter testTypeParam = new ReportParameter(ReportParamConstants.PARAM_TESTTYPE, PopulateProducts);
            ReportParameter testsParam = new ReportParameter(ReportParamConstants.PARAM_TEST, PopulateTests);

            AddParameter(programOfStudyParam);
            AddParameter(institutionParam, programOfStudyParam);
            AddParameter(testTypeParam);
            AddParameter(testsParam, testTypeParam, institutionParam);
        }

        public void PopulateProgramOfStudies()
        {
            IEnumerable<ProgramofStudy> programOfStudies = _reportDataService.GetProgramOfStudies();
            if (!View.PostBack && programOfStudies.HasElements())
            {
                Parameters[ReportParamConstants.PARAM_PROGRAM_OF_Study].SelectedValues = View.IsProgramOfStudyIdExistInQueryString == true ? Parameters[ReportParamConstants.PARAM_PROGRAM_OF_Study].SelectedValues : programOfStudies.FirstOrDefault().ProgramofStudyId.ToString();
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
        }

        public void PopulateTests()
        {
            IEnumerable<UserTest> tests = _reportDataService.GetTestsByInstitute(
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues,
                Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues);
            View.PopulateTests(tests);
        }

        public void GenerateReport(string fromDate, string toDate)
        {
            //// Check for Multiple Test Selected
            if (Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.Split('|').Length == 1)
            {
                View.SetControlsIfMultipleTests(false);
            }
            else
            {
                View.SetControlsIfMultipleTests(true);
                return;
            }

            var resultsFromInstitution = _reportDataService.GetResultsFromInstitutions(
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues,
                2,
                Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues,
                Parameters[ReportParamConstants.PARAM_TEST].SelectedValues,
                fromDate,
                toDate);

            int testId = Parameters[ReportParamConstants.PARAM_TEST].SelectedValues.ToInt();
            if (testId == 0)
            {
                testId = 1;
            }

            decimal norm = _reportDataService.GetNormForTest(testId);
            IEnumerable<string> testCharacteristics = _reportDataService.GetTestCharacteristicsByTestId(testId, "A");

            View.RenderReport(resultsFromInstitution, norm, testCharacteristics);
        }

        public void ExportReport()
        {
            View.ExportReport();
        }

        public IEnumerable<ResultsFromTheProgramForChart> GetResultsForChart(string chartType, string fromDate, string toDate)
        {
            return _reportDataService.GetResultsFromInstitutionForChart(
                Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues,
                Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues,
                Parameters[ReportParamConstants.PARAM_TEST].SelectedValues,
                chartType, fromDate, toDate);
        }

        public void ShowPrinterFriendlyVersion()
        {
            Navigator.NavigateTo(AdminPageDirectory.ReportInstitutionPerformance, string.Empty, string.Format("{0}={1}&{2}={3}&{4}={5}&{6}={7}",
                QUERY_PARAM_ACT, ReportAction.PrintInterface,
                QUERY_PARAM_ID, Parameters[ReportParamConstants.PARAM_INSTITUTION].SelectedValues.ToInt(),
                QUERY_PARAM_TESTID, Parameters[ReportParamConstants.PARAM_TEST].SelectedValues,
                QUERY_PARAM_PRODUCTID, Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues));
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

        public IEnumerable<ResultsFromTheProgram> GetResultsFromInstitution(string institutionId, string productId, string testId, int chartType, string fromDate, string toDate)
        {
            return _reportDataService.GetResultsFromInstitutions(
                 institutionId,
                 chartType,
                 Parameters[ReportParamConstants.PARAM_TESTTYPE].SelectedValues,
                 testId,
                 fromDate,
                 toDate);
        }

        public IEnumerable<string> GetTestCharacteristics(int testId)
        {
            return _reportDataService.GetTestCharacteristicsByTestId(testId, "A");
        }

        public IEnumerable<ResultsFromTheProgramForChart> GetResultsFromInstitutionForChart(string institutionId, string productId, string testId, string chartType, string fromDate, string toDate)
        {
            return _reportDataService.GetResultsFromInstitutionForChart(institutionId, productId, testId, chartType, fromDate, toDate);
        }

        public decimal GetNormForTest(int testId)
        {
            return _reportDataService.GetNormForTest(testId);
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
