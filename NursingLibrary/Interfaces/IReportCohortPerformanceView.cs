using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IReportCohortPerformanceView : IReportView
    {
        ReportAction Action { get; set; }

        void PopulateGroup(IEnumerable<Group> groups);

        void RenderReport(ResultsFromTheProgram resultsFromTheProgram, decimal norm, IEnumerable<string> testCharacteristics);
        
        void GenerateReport(ResultsFromTheProgram resultsFromTheProgram, decimal norm, IEnumerable<string> testCharacteristics, ReportAction printActions);

        bool IsProgramofStudyVisible { get; set; }
    }
}
