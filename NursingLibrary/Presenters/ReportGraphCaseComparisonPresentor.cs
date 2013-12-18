using System.Collections.Generic;
using System.Linq;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class ReportGraphCaseComparisonPresentor : ReportPresenterBase<IReportGraphCaseComparisonView>
    {
        #region Fields
        public const string QUERY_PARAM_CATEGORY = "CategoryID";
        public const string QUERY_PARAM_SUBCATEGORY = "SubCategory";
        public const string QUERY_PARAM_COHORT_LIST = "list";
        public const string QUERY_PARAM_NAME = "Name";
        public const string QUERY_PARAM_CASE_LIST = "CaseList";
        public const string QUERY_PARAM_MODULE_LIST = "ModuleList";
        public const string QUERY_PARAM_INSTITUTIONID = "InstitutionID";
        public const string QUERY_PARAM_CATEGORY_NAME = "CategoryName";

        private readonly IReportDataService _reportService;
        #endregion

        public ReportGraphCaseComparisonPresentor(IReportDataService service)
            : base(Module.Reports)
        {
            _reportService = service;
        }

        public override void RegisterAuthorizationRules()
        {
            // throw new NotImplementedException();
        }

        public override void RegisterQueryParameters()
        {
            RegisterQueryParameter(QUERY_PARAM_COHORT_LIST);
            RegisterQueryParameter(QUERY_PARAM_CATEGORY);
            RegisterQueryParameter(QUERY_PARAM_SUBCATEGORY);
            RegisterQueryParameter(QUERY_PARAM_CASE_LIST);
            RegisterQueryParameter(QUERY_PARAM_MODULE_LIST);
            RegisterQueryParameter(QUERY_PARAM_INSTITUTIONID);
            RegisterQueryParameter(QUERY_PARAM_NAME);
        }

        public void PopulateCohortGraph()
        {
            string strXml = string.Empty;
            string cohorts = GetParameterValue(QUERY_PARAM_COHORT_LIST);
            int categoryId = GetParameterValue(QUERY_PARAM_CATEGORY).ToInt();
            int SubCategoryId = GetParameterValue(QUERY_PARAM_SUBCATEGORY).ToInt();
            string cases = GetParameterValue(QUERY_PARAM_CASE_LIST);
            string modules = GetParameterValue(QUERY_PARAM_MODULE_LIST);
            int institutionId = GetParameterValue(QUERY_PARAM_INSTITUTIONID).ToInt();
            string subCategoryName = GetParameterValue(QUERY_PARAM_NAME);
            IEnumerable<ResultsFromTheCohortForChart> results = _reportService.GetResultsForCohortsBySubCategoryChart(cohorts, categoryId, SubCategoryId, cases, modules, institutionId);

            if (results.Count() > 0)
            {
                strXml = "<graph xaxisname=\"\" yaxisname=\"\" hovercapbg=\"DEDEBE\" hovercapborder=\"889E6D\" rotateNames=\"0\" animation=\"1\" yAxisMaxValue=\"100\" numdivlines=\"9\" divLineColor=\"CCCCCC\" divLineAlpha=\"80\" decimalPrecision=\"0\" yAxisValueDecimals=\"0\" showAlternateVGridColor=\"1\" AlternateVGridAlpha=\"30\" AlternateVGridColor=\"CCCCCC\" caption=\"" + subCategoryName + " \" subcaption=\"\">";
                int NumberOfCohorts = results.Count();
                string[] cohort = new string[NumberOfCohorts];
                string[] per = new string[NumberOfCohorts];
                int i = 0;
                foreach (ResultsFromTheCohortForChart r in results)
                {
                    cohort[i] = r.CohortName;
                    per[i] = r.Correct.ToString();
                    i++;
                }

                string cat = "<categories font=\"Arial\" fontSize=\"11\" fontColor=\"000000\">";
                for (int j = 0; j < NumberOfCohorts; j++)
                {
                    cat = cat + "<category name=\"" + cohort[j] + "\" /> ";
                }

                cat = cat + "</categories>";
                string strd = "<dataset color=\"FDC12E\" alpha=\"70\">";
                for (int j = 0; j < NumberOfCohorts; j++)
                {
                    strd = strd + "<set value=\" " + per[j] + "\" color=' " + ReturnColor(j) + "'/> ";
                }

                strd = strd + "</dataset>";
                strXml = strXml + cat + strd + "</graph>";
            }

            View.WriteGraphData(strXml);
        }

        protected string ReturnColor(int i)
        {
            string result = string.Empty;
            if (i % 3 == 1)
            {
                result = "#CC99FF";
            }

            if (i % 3 == 2)
            {
                result = "#99CCFF";
            }

            if (i % 3 == 0)
            {
                result = "#FDC12E";
            }
            
            return result;
        }
    }
}
