using System.Web;

/// <summary>
/// Summary description for ReportHelper
/// </summary>
public class ReportHelper
{
    public static void ExportToExcel(string excelData, string filename)
    {
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + (string.IsNullOrEmpty(filename) ? string.Empty : filename));
        HttpContext.Current.Response.Charset = string.Empty;

        HttpContext.Current.Response.Write(excelData);

        HttpContext.Current.Response.End();
    }

    public static string ReturnName(string categoryName)
    {
        string fName = categoryName.Trim();
        switch (fName)
        {
            case "ClientNeeds":
                fName = "Client Needs";
                break;
            case "NursingProcess":
                fName = "Nursing Process";
                break;
            case "CriticalThinking":
                fName = "Critical Thinking";
                break;
            case "ClinicalConcept":
                fName = "Clinical Concept";
                break;
            case "CognitiveLevel":
                fName = "Bloom's Cognitive Level";
                break;
            case "SpecialtyArea":
                fName = "Specialty Area";
                break;
            case "LevelOfDifficulty":
                fName = "Level Of Difficulty";
                break;
            case "AccreditationCategories":
                fName = "Accreditation Categories";
                break;
            case "QSENKSACompetencies":
                fName = "QSEN KSA Competencies";
                break;
        }

        return fName;
    }
}