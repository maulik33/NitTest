using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace NursingLibrary.Entity
{
    [XmlRoot(ElementName = "graph")]
    public class QBankGraph : IXmlSerializable
    {
        private readonly List<GraphData> _categories;
        private readonly List<Set> _dataSet;
        private int _aType;

        public QBankGraph()
        {
            _categories = new List<GraphData>();
            _dataSet = new List<Set>();
        }

        public string CanvasBgColor { get; set; }

        public string CanvasBaseColor { get; set; }
        
        public string XAxisName { get; set; }
        
        public string YAxisName { get; set; }
        
        public string HoverCapbg { get; set; }
        
        public string HoverCapBorder { get; set; }
        
        public string RotateNames { get; set; }
        
        public string Animation { get; set; }
        
        public string YAxisMaxValue { get; set; }
        
        public string NumDivLines { get; set; }
        
        public string DivLineColor { get; set; }
        
        public string DivLineAlpha { get; set; }
        
        public string DecimalPrecision { get; set; }
        
        public string ShowAlternateVGridColor { get; set; }
        
        public string AlternateVGridAlpha { get; set; }
        
        public string AlternateVGridColor { get; set; }
        
        public string Caption { get; set; }
        
        public string CanvasBorderThickness { get; set; }
        
        public string CanvasBorderColor { get; set; }

        public string BaseFont { get; set; }
        
        public string BaseFontSize { get; set; }
        
        public string ShowLegend { get; set; }
        
        public string ShowHoverCap { get; set; }
        
        public string NumberSuffix { get; set; }

        public string DataSetColor { get; set; }

        public string DataSetShowValues { get; set; }
        
        public string DataSetSeriesName { get; set; }

        public void GenerateGraphData(IEnumerable<ProgramResults> performance)
        {
            _aType = 1;
            DefaultValues(string.Empty, "0");
            Caption = string.Empty;
            ShowLegend = "0";
            ShowHoverCap = "0";
            NumberSuffix = "%25";
            DataSetSeriesName = null;

            AddCategory(string.Empty, null);

            foreach (ProgramResults performanceItem in performance)
            {
                AddSet(string.Empty, null, string.Empty, (performanceItem.DisplayTotal == 0) ? "0.000001" : performanceItem.Total.ToString());
            }
        }

        public void GenerateGraphData(IEnumerable<UserTest> userTests, IEnumerable<ProgramResults> performance)
        {
            _aType = 2;
            DefaultValues("Percentages", "1");
            DataSetSeriesName = string.Empty;

            var data = from t in userTests
                       join p in performance
                       on t.UserTestId equals p.UserTestID into t2
                       from p in t2.DefaultIfEmpty()
                       select new
                       {
                           UserTestId = t.UserTestId,
                           TestStarted = t.TestStarted,
                           Total = p == null ? 0 : p.Total
                       };

            foreach (var item in data)
            {
                string hoverText = item.TestStarted.ToShortDateString() + " " + item.TestStarted.ToLongTimeString();
                string name = item.TestStarted.ToShortDateString();
                AddCategory(name, hoverText);

                string link = string.Format("QBank_R_Details.aspx%3Fid={0}", item.UserTestId);
                AddSet(string.Format("{0} ...", item.UserTestId),
                    link, item.UserTestId.ToString(),
                    (item.Total == 0) ? "0.000001" : item.Total.ToString());
            }
        }

        #region IXmlSerializable Members

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            throw new NotImplementedException(); // Not a requirement now.
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteAttributeString("canvasBgColor", CanvasBgColor);
            writer.WriteAttributeString("canvasBaseColor", CanvasBaseColor);
            writer.WriteAttributeString("xaxisname", XAxisName);
            writer.WriteAttributeString("yaxisname", YAxisName);
            writer.WriteAttributeString("hovercapbg", HoverCapbg);
            writer.WriteAttributeString("hovercapborder", HoverCapBorder);
            writer.WriteAttributeString("rotateNames", RotateNames);
            writer.WriteAttributeString("animation", Animation);
            writer.WriteAttributeString("yAxisMaxValue", YAxisMaxValue);
            writer.WriteAttributeString("numdivlines", NumDivLines);
            writer.WriteAttributeString("divLineColor", DivLineColor);
            writer.WriteAttributeString("divLineAlpha", DivLineAlpha);
            writer.WriteAttributeString("decimalPrecision", DecimalPrecision);
            writer.WriteAttributeString("showAlternateVGridColor", ShowAlternateVGridColor);
            writer.WriteAttributeString("AlternateVGridAlpha", AlternateVGridAlpha);
            writer.WriteAttributeString("AlternateVGridColor", AlternateVGridColor);
            writer.WriteAttributeString("canvasBorderThickness", CanvasBorderThickness);
            writer.WriteAttributeString("canvasBorderColor", CanvasBorderColor);
            writer.WriteAttributeString("baseFont", BaseFont);
            writer.WriteAttributeString("baseFontSize", BaseFontSize);
            if (_aType == 1)
            {
                writer.WriteAttributeString("caption", Caption);
                writer.WriteAttributeString("ShowLegend", ShowLegend);
                writer.WriteAttributeString("showhovercap", ShowHoverCap);
                writer.WriteAttributeString("numberSuffix", NumberSuffix);
            }

            // Write Categories
            writer.WriteStartElement("categories");
            foreach (GraphData item in _categories)
            {
                writer.WriteStartElement("category");
                item.WriteXml(writer);
                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            // Write DataSet
            writer.WriteStartElement("dataset");
            if (_aType == 2)
            {
                writer.WriteAttributeString("seriesname", DataSetSeriesName);
            }

            writer.WriteAttributeString("color", DataSetColor);
            writer.WriteAttributeString("showValues", DataSetShowValues);
            foreach (GraphData item in _dataSet)
            {
                writer.WriteStartElement("set");
                item.WriteXml(writer);
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        #endregion

        private void AddSet(string name, string link, string hoverText, string value)
        {
            _dataSet.Add(new Set()
            {
                Name = name,
                Link = link,
                HoverText = hoverText,
                Value = value
            });
        }

        private void AddCategory(string name, string hoverText)
        {
            _categories.Add(new GraphData()
            {
                Name = name,
                HoverText = hoverText
            });
        }

        private void DefaultValues(string yAxisName, string rotationNames)
        {
            CanvasBgColor = "E2EBF6";
            CanvasBaseColor = "ADC4E4";
            XAxisName = string.Empty;
            YAxisName = yAxisName;
            HoverCapbg = "DEDEBE";
            HoverCapBorder = "889E6D";
            RotateNames = rotationNames;
            Animation = "1";
            YAxisMaxValue = "100";
            NumDivLines = "9";
            DivLineColor = "CCCCCC";
            DivLineAlpha = "80";
            DecimalPrecision = "1";
            ShowAlternateVGridColor = "1";
            AlternateVGridAlpha = "30";
            AlternateVGridColor = "CCCCCC";
            CanvasBorderThickness = "1";
            CanvasBorderColor = "000066";
            BaseFont = "Verdana";
            BaseFontSize = "11";

            DataSetColor = "E97595";
            DataSetShowValues = "1";
        }

        private class Set : GraphData, IXmlSerializable
        {
            public string Value { get; set; }

            public string Link { get; set; }

            public override void WriteXml(System.Xml.XmlWriter writer)
            {
                base.WriteXml(writer);
                if (Link != null)
                {
                    writer.WriteAttributeString("link", Link);
                }

                writer.WriteAttributeString("value", Value);
            }
        }

        private class GraphData : IXmlSerializable
        {
            public string Name { get; set; }

            public string HoverText { get; set; }

            #region IXmlSerializable Members

            public System.Xml.Schema.XmlSchema GetSchema()
            {
                return null;
            }

            public void ReadXml(System.Xml.XmlReader reader)
            {
                throw new NotImplementedException();
            }

            public virtual void WriteXml(System.Xml.XmlWriter writer)
            {
                writer.WriteAttributeString("name", Name);
                if (HoverText != null)
                {
                    writer.WriteAttributeString("hoverText", HoverText);
                }
            }

            #endregion
        }
    }
}
