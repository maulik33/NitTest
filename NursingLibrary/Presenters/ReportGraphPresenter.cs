using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class ReportGraphPresenter : ReportPresenterBase<IReportGraphView>
    {
        #region Fields
        public const string QUERY_PARAM_USERTESTID = "UserTestID";
        public const string QUERY_PARAM_ATYPE = "AType";
        public const string QUERY_PARAM_INSTITUTIONID = "IID";
        public const string QUERY_PARAM_PRODUCTID = "ProductID";
        public const string QUERY_PARAM_TESTID = "TestID";

        private readonly IReportDataService _reportService;
        #endregion

        #region Constructor
        public ReportGraphPresenter(IReportDataService service)
            : base(Module.Reports)
        {
            _reportService = service;
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
        }

        public void PopulateInstitutions()
        {
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

        public void GenerateReport()
        {
        }

        public void GetGraphData()
        {
            int userTestId = GetParameterValue(QUERY_PARAM_USERTESTID).ToInt();
            int charType = GetParameterValue(QUERY_PARAM_ATYPE).ToInt();

            ResultsFromTheProgram resultsFromTheProgram = _reportService.GetResultsFromTheProgram(userTestId, 1);

            string strCategoriesXML = string.Empty;
            string strXml = string.Empty;

            if (charType == 1)
            {
                strXml = "<graph canvasBgColor='E2EBF6' canvasBaseColor='ADC4E4'  xaxisname=\" \" yaxisname=\"\" hovercapbg=\"DEDEBE\" hovercapborder=\"889E6D\" rotateNames=\"0\" animation=\"1\" yAxisMaxValue=\"100\" numdivlines=\"9\" divLineColor=\"CCCCCC\" divLineAlpha=\"80\" decimalPrecision=\"1\"  showAlternateVGridColor=\"1\" AlternateVGridAlpha=\"30\" AlternateVGridColor=\"CCCCCC\" caption=\"\" canvasBorderThickness='1'   canvasBorderColor='000066' baseFont='Verdana' baseFontSize='11' ShowLegend='0'>";
                strCategoriesXML = GetXYAxis(userTestId, resultsFromTheProgram);
            }

            strXml = strXml + strCategoriesXML + "</graph>";

            View.ReturnGraphData(strXml);
        }

        public void GetGraph2Data()
        {
            int ProductID;
            int TestID;
            int AType;

            if (GetParameterValue(QUERY_PARAM_ATYPE).Equals(string.Empty))
            {
                AType = 1;
            }
            else
            {
                AType = Convert.ToInt32(GetParameterValue(QUERY_PARAM_ATYPE));
            }

            if (GetParameterValue(QUERY_PARAM_PRODUCTID).Equals(string.Empty))
            {
                ProductID = 0;
            }
            else
            {
                ProductID = Convert.ToInt32(GetParameterValue(QUERY_PARAM_PRODUCTID));
            }

            if (GetParameterValue(QUERY_PARAM_TESTID).Equals(string.Empty))
            {
                TestID = 0;
            }
            else
            {
                TestID = Convert.ToInt32(GetParameterValue(QUERY_PARAM_TESTID));
            }

            string StudentTestXML = GetStudentTestXML(ProductID, TestID, AType);
            View.ReturnGraphData(StudentTestXML);
        }

        public string GetStudentTestXML(int ProductID, int TestID, int AType)
        {
            string strCategoriesXML = string.Empty;
            string strXml = string.Empty;

            if (AType == 1)
            {
                strXml = "<graph canvasBgColor='E2EBF6' canvasBaseColor='ADC4E4'  xaxisname=\" \" yaxisname=\"\" hovercapbg=\"DEDEBE\" hovercapborder=\"889E6D\" rotateNames=\"0\" animation=\"1\" yAxisMaxValue=\"100\" numdivlines=\"9\" divLineColor=\"CCCCCC\" divLineAlpha=\"80\" decimalPrecision=\"0\"  showAlternateVGridColor=\"1\" AlternateVGridAlpha=\"30\" AlternateVGridColor=\"CCCCCC\" caption=\"\" canvasBorderThickness='1'   canvasBorderColor='000066' baseFont='Verdana' baseFontSize='11' ShowLegend='0'>";
                strCategoriesXML = GetStudentTestXYAxsis(ProductID, TestID, AType);
            }

            if (AType == 2)
            {
                strXml = "<graph canvasBgColor='E2EBF6' canvasBaseColor='ADC4E4'  xaxisname=\" \" yaxisname=\"\" hovercapbg=\"DEDEBE\" hovercapborder=\"889E6D\" rotateNames=\"0\" animation=\"1\" yAxisMaxValue=\"100\" numdivlines=\"9\" divLineColor=\"CCCCCC\" divLineAlpha=\"80\" decimalPrecision=\"0\"  showAlternateVGridColor=\"1\" AlternateVGridAlpha=\"30\" AlternateVGridColor=\"CCCCCC\" caption=\"\" canvasBorderThickness='1'   canvasBorderColor='000066' baseFont='Verdana' baseFontSize='11' ShowLegend='0'>";
                strCategoriesXML = GetTestRankXYAxsis();
            }

            strXml = strXml + strCategoriesXML + "</graph>";
            return strXml;
        }

        private string GetXYAxis(int userTestId, ResultsFromTheProgram resultsFromTheProgram)
        {
            string[] periods = new string[1];
            string[] student = new string[1];

            StringBuilder strSetXML = new StringBuilder();

            strSetXML.Append("<categories>");
            strSetXML.Append("<category name='' hoverText='Correct'/> ");
            strSetXML.Append("</categories>");
            strSetXML.Append("<dataset  color=\"E97595\" showValues=\"1\"> ");

            string svalue;

            if (resultsFromTheProgram != null)
            {
                string column = periods[0];
                column = string.Empty;

                if (resultsFromTheProgram.Total == 0)
                {
                    svalue = "0.000001";
                }
                else
                {
                    svalue = Convert.ToString(resultsFromTheProgram.Total);
                }

                strSetXML.Append("<set name='" + Convert.ToString(column) + "' hoverText='" + Convert.ToString(column) + "' value='" + svalue + "'/>");
            }

            strSetXML.Append("</dataset>");

            return strSetXML.ToString();
        }

        private string GetStudentTestXYAxsis(int ProductID, int TestID, int AType)
        {
            StringBuilder strSetXML = new StringBuilder();

            string[] student = new string[1];
            IEnumerable<ResultsFromTheProgram> tempResult = _reportService.GetResultsFromInstitutions(GetParameterValue(QUERY_PARAM_INSTITUTIONID),
                AType, Convert.ToString(ProductID), Convert.ToString(TestID), string.Empty, string.Empty);

            ResultsFromTheProgram result = new ResultsFromTheProgram();
            if (tempResult != null)
            {
                result = tempResult.First();
            }

            strSetXML.Append("<categories>");
            strSetXML.Append("<category name=\"\" /> ");
            strSetXML.Append("</categories>");
            strSetXML.Append("<dataset  color=\"E97595\" showValues=\"1\"> ");

            string svalue;
            if (result != null)
            {
                if (result.Total == 0)
                {
                    svalue = "0.000001";
                }
                else
                {
                    svalue = Convert.ToString(result.Total);
                }

                strSetXML.Append("<set name='" + string.Empty + "' hoverText='" + string.Empty + "' value='" + svalue + "'/>");
            }

            strSetXML.Append("</dataset>");

            return strSetXML.ToString();
        }

        private string GetTestRankXYAxsis()
        {
            StringBuilder strSetXML = new StringBuilder();
            string[] student = new string[1];
            decimal rank = _reportService.GetRankForTest(Convert.ToInt32(QUERY_PARAM_TESTID));

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
