using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IReportStudentPerformanceView : IReportView
    {
        ReportAction Action { get; set; }

        int ProductId { get; set; }

        int UserTestId { get; set; }
        
        bool IsProductTypeExistInQueryString { get; set; }
        
        bool IsTestIdExistInQueryString { get; set; }

        void SetControlValues();

        void RenderReport(int testId, ResultsFromTheProgram result, IEnumerable<string> testCharacteristics);

        void ExportReport(string institutionNames, string cohortNames, ResultsFromTheProgram result1, ResultsFromTheProgram result2,
                        IEnumerable<string> testCharacteristics, ReportAction act);
    }
}
