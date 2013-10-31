using System;
using System.Collections.Generic;
using NursingLibrary.Entity;
using NursingLibrary.Interfaces;
using NursingLibrary.Presenters;

public partial class ADMIN_Graph2 : ReportPageBase<IReportGraph2View, ReportGraph2Presenter>, IReportGraph2View
{
    public override void PreInitialize()
    {
    }

    #region IReportGraphView Methods

    public bool PostBack
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public void PopulateInstitutions(IEnumerable<Institution> institutions)
    {
    }

    public void PopulateProducts(IEnumerable<Product> products)
    {
    }

    public void PopulateCohorts(IEnumerable<Cohort> cohorts)
    {
    }

    public void PopulateTests(IEnumerable<UserTest> tests)
    {
    }

    public void PopulateGroup(IEnumerable<Group> groups)
    {
    }

    public void PopulateStudent(IEnumerable<StudentEntity> students)
    {
    }

    public void GenerateReport()
    {
    }

    public void PopulateProgramOfStudies(IEnumerable<ProgramofStudy> programOfStudies)
    {
        throw new NotImplementedException();
    }

    public void ReturnGraphData(string graphData)
    {
        Response.Expires = 0;
        Response.ContentType = "text/xml";

        Response.Write(graphData);
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        Presenter.GetGraph2Data();
    }

    public string CategoryName
    {
        get { return ConvertHexToString(Presenter.GetParameterValue("CategoryName")); }
    }

    public static string ConvertHexToString(String hexInput)
    {
        int numberChars = hexInput.Length;
        byte[] bytes = new byte[numberChars / 2];
        for (int i = 0; i < numberChars; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(hexInput.Substring(i, 2), 16);
        }
        return  System.Text.Encoding.Unicode.GetString(bytes);
    }
}
