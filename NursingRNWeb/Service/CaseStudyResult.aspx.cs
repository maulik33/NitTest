using System;
using System.IO;
using System.Web.UI;
using System.Xml;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;

public partial class STUDENT_CaseStudyResult : StudentBasePage<ICaseStudyResultView, CaseStudyResultPresenter>, ICaseStudyResultView
{
    public void WriteResponse(XmlDocument responseDoc)
    {
        Response.Write(responseDoc.OuterXml);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Response.ContentType = "text/xml";

        // Read XML posted via HTTP
        StreamReader reader = new StreamReader(Page.Request.InputStream);
        String xmlData = reader.ReadToEnd();
        Presenter.ShowCaseStudyResult(xmlData);
    }
}
