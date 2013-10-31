using System;
using System.Collections.Generic;
using System.Linq;
using NursingLibrary.DTC;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;

namespace NursingLibrary.Presenters
{
    [Presenter]
    public class ReportGraphCPresenter : ReportPresenterBase<IReportGraphCView>
    {
        #region Fields
        public const string QUERY_PARAM_CATEGORY = "CategoryID";
        public const string QUERY_PARAM_SUBCATEGORY = "SubCategory";
        public const string QUERY_PARAM_COHORT_LIST = "list";
        public const string QUERY_PARAM_PRODUCT_LIST = "ProductList";
        public const string QUERY_PARAM_TEST_LIST = "TestList";
        public const string QUERY_PARAM_INSTITUTIONID = "InstitutionID";

        private readonly IReportDataService _reportService;
        #endregion

        #region Constructor
        public ReportGraphCPresenter(IReportDataService service)
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
            RegisterQueryParameter(QUERY_PARAM_CATEGORY);
            RegisterQueryParameter(QUERY_PARAM_SUBCATEGORY);
            RegisterQueryParameter(QUERY_PARAM_COHORT_LIST);
            RegisterQueryParameter(QUERY_PARAM_PRODUCT_LIST);
            RegisterQueryParameter(QUERY_PARAM_TEST_LIST);
            RegisterQueryParameter(QUERY_PARAM_INSTITUTIONID);
        }

        public void PopulateCohortGraph()
        {
            int CategoryID = GetParameterValue(QUERY_PARAM_CATEGORY).ToInt();
            int SubCategory = GetParameterValue(QUERY_PARAM_SUBCATEGORY).ToInt();
            string CohortList = GetParameterValue(QUERY_PARAM_COHORT_LIST);
            string ProductList = GetParameterValue(QUERY_PARAM_PRODUCT_LIST);
            string TestList = GetParameterValue(QUERY_PARAM_TEST_LIST);
            int InstitutionID = GetParameterValue(QUERY_PARAM_INSTITUTIONID).ToInt();
            string strXml = string.Empty;
            IDictionary<CategoryName, Category> categories = _reportService.GetCategories();
            IEnumerable<CategoryDetail> categorydetails = categories[(CategoryName)Enum.Parse(typeof(CategoryName), CategoryID.ToString())].Details.Values;
            string name = categorydetails.Where(d => d.Id == SubCategory).Select(r => r.Description).SingleOrDefault();
            strXml = "<graph xaxisname=\"\" yaxisname=\"\" hovercapbg=\"DEDEBE\" hovercapborder=\"889E6D\" rotateNames=\"0\" animation=\"1\" yAxisMaxValue=\"100\" numdivlines=\"9\" divLineColor=\"CCCCCC\" divLineAlpha=\"80\" decimalPrecision=\"1\" yAxisValueDecimals=\"0\" showAlternateVGridColor=\"1\" AlternateVGridAlpha=\"30\" AlternateVGridColor=\"CCCCCC\" caption=\"" + name + " \" subcaption=\"\">";

            IEnumerable<ResultsFromTheCohortForChart> result = _reportService.GetResultsFromTheCohotForChart(InstitutionID, SubCategory, CategoryID, ProductList, TestList, CohortList);

            if (result != null)
            {
                int NumberOfCohorts = result.Count();
                string[] cohort = new string[NumberOfCohorts];
                string[] per = new string[NumberOfCohorts];
                int i = 0;
                foreach (ResultsFromTheCohortForChart r in result)
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
                    strd = strd + "<set value=\"" + per[j] + "\" color='" + ReturnColor(j) + "'/> ";
                }

                strd = strd + "</dataset>";
                strXml = strXml + cat + strd + "</graph>";
                View.WriteGraphData(strXml);
            }
        }

        // return strXml;
        // Response.Write(new cXML().GetXMLForComparation(CohortList, CategoryID, SubCategory, Name, ProductList, TestList, InstitutionID, CategoryName));
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