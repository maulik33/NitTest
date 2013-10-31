using System;
using System.Text;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;
using NursingLibrary.Utilities;

namespace STUDENT
{
    public partial class Graph : StudentBasePage<IStudentGraphView, StudentGraphPresenter>, IStudentGraphView
    {
        #region Properties

        public ProgramResults ResultsFromTheProgram { get; set; }

        public int AType
        {
            get
            {
                return Convert.ToInt32(Request.QueryString["AType"] ?? "1");
            }
        }

        #endregion

        #region IStudentGraphView Methods

        public void GenerateGraph()
        {
            Response.Expires = 0;
            Response.ContentType = "text/xml";

            Response.Write(GetXML());
        }

        public string GetXML()
        {
            string strCategoriesXML = string.Empty;
            string strXml = string.Empty;

            if (AType == 1)
            {
                strXml = "<graph canvasBgColor='E2EBF6' canvasBaseColor='ADC4E4'  xaxisname=\" \" yaxisname=\"\" hovercapbg=\"DEDEBE\" hovercapborder=\"889E6D\" rotateNames=\"0\" animation=\"1\" yAxisMaxValue=\"100\" numdivlines=\"9\" divLineColor=\"CCCCCC\" divLineAlpha=\"80\" decimalPrecision=\"1\"  showAlternateVGridColor=\"1\" AlternateVGridAlpha=\"30\" AlternateVGridColor=\"CCCCCC\" caption=\"\" canvasBorderThickness='1'   canvasBorderColor='000066' baseFont='Verdana' baseFontSize='11' ShowLegend='0'>";
                strCategoriesXML = GetXYAxsis();
            }

            strXml = strXml + strCategoriesXML + "</graph>";
            return strXml;
        }

        #endregion IStudentGraphView Methods

        public string GetXYAxsis()
        {
            var periods = new string[1];
            var strSetXML = new StringBuilder();

            strSetXML.Append("<categories>");
            strSetXML.Append("<category name='' hoverText='Correct'/> ");
            strSetXML.Append("</categories>");
            strSetXML.Append("<dataset  color=\"E97595\" showValues=\"1\"> ");

            string column = periods[0];
            column = string.Empty;
            string svalue = ResultsFromTheProgram.DisplayTotal == -1 ? "0.000001" : ResultsFromTheProgram.DisplayTotal.ToString();

            strSetXML.Append("<set name='" + Convert.ToString(column) + "' hoverText='" + Convert.ToString(column) + "' value='" + svalue + "'/>");

            strSetXML.Append("</dataset>");

            return strSetXML.ToString();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Presenter.OnViewInitialized();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region Trace Information
                TraceHelper.WriteTraceEvent(TraceToken, "Navigated to Graph Page.");
                #endregion
            }

            Presenter.OnViewLoaded();
        }
    }
}