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
    public class ReportGraph2Presenter : ReportPresenterBase<IReportGraph2View>
    {
        public const string QUERY_PARAM_CATERGORYNAME = "CategoryName";
        public const string QUERY_PARAM_PERCENTAGE = "Percentage";
        public const string QUERY_PARAM_INSTITUTIONID = "IID";
        public const string QUERY_PARAM_PROGRAMOFSTUDYNAME = "ProgramofStudyName";

        private readonly IReportDataService _reportService;

        #region Constructor
        public ReportGraph2Presenter(IReportDataService service)
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
            RegisterQueryParameter(QUERY_PARAM_CATERGORYNAME);
            RegisterQueryParameter(QUERY_PARAM_PERCENTAGE);
            RegisterQueryParameter(QUERY_PARAM_INSTITUTIONID);
            RegisterQueryParameter(QUERY_PARAM_PROGRAMOFSTUDYNAME);
        }

        public void GetGraph2Data()
        {
            //var graphResult = Presenter.GetResultsForChart(chartType, txtDateFrom.Text, txtDateTo.Text);
            string categoryName = View.CategoryName;
            decimal[] result = GetParameterValue(QUERY_PARAM_PERCENTAGE).Split(',').Select(x => decimal.Parse(x)).ToArray();
            string institutionIds = GetParameterValue(QUERY_PARAM_INSTITUTIONID).Replace('|', ',');
            string programofStudyName = GetParameterValue(QUERY_PARAM_PROGRAMOFSTUDYNAME);
            List<Institution> institutions = _reportService.GetInstitutionDetails(institutionIds).ToList();
            string strXML = string.Empty;
            string strXMLC = string.Empty;
            string strXMLS = string.Empty;

            string[] institutionnames = institutions.Select(s => (s.InstitutionName + " - " + programofStudyName)).ToArray();
            Array.Resize(ref institutionnames, institutionnames.Length + 1);
            institutionnames[institutionnames.Length - 1] = "Norming";
            //decimal[] result = new decimal[institutionnames.Length];

            strXML = "<graph xaxisname=\"\" yaxisname=\"\" hovercapbg=\"DEDEBE\" hovercapborder=\"889E6D\" rotateNames=\"0\" animation=\"0\" yAxisMaxValue=\"100\" numdivlines=\"9\" divLineColor=\"CCCCCC\" divLineAlpha=\"80\" decimalPrecision=\"1\" yAxisValueDecimals=\"0\" showAlternateVGridColor=\"1\" AlternateVGridAlpha=\"30\"chartRightMargin=\"30\" AlternateVGridColor=\"CCCCCC\" caption=\"" + categoryName + " \" subcaption=\"\">";
            strXMLC += "<categories font=\"Arial\" fontSize=\"11\" fontColor=\"000000\">";

            for (int j = 0; j < institutionnames.Length - 1; ++j)
            {
                if (institutionnames[j].Length > 15)
                {
                    strXMLC += "<category name=\"" + institutionnames[j].Substring(0, 15) + "\" /> ";
                }
                else
                {
                    strXMLC += "<category name=\"" + institutionnames[j] + "\" /> ";
                }
            }

            if (result[result.Length - 1] != 0)
            {
                strXMLC += "<category name=\"" + institutionnames[institutionnames.Length - 1] + "\" /> ";
            }

            strXMLC += "</categories>";
            strXMLS += "<dataset color=\"FDC12E\" alpha=\"70\">";
            for (int j = 0; j < result.Length; ++j)
            {
                if (j == result.Length - 1)
                {
                    if (result[j] != 0)
                    {
                        strXMLS += "<set value=\" " + result[j] + "\" color=\"#C0C0C0\"/> ";
                    }
                }
                else
                {
                    strXMLS += "<set value=\" " + result[j] + "\" color=\" " + ReturnColor(j) + "\"/> ";
                }
            }

            strXMLS = strXMLS + "</dataset>";
            strXML = strXML + strXMLC + strXMLS + "</graph>";
            View.ReturnGraphData(strXML);

        }

        private string ReturnColor(int index)
        {
            string result = string.Empty;
            if (index % 3 == 1)
            {
                result = "#CC99FF";
            }

            if (index % 3 == 2)
            {
                result = "#99CCFF";
            }

            if (index % 3 == 0)
            {
                result = "#FDC12E";
            }

            return result;
        }
    }
}
