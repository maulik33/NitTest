using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IReportInstitutionPerformanceView : IReportView
    {
        bool IsProgramofStudyVisible { get; set; }

        bool IsIdExistInQueryString { get; set; }

        bool IsTestIdExistInQueryString { get; set; }
        
        bool IsProductIdExistInQueryString { get; set; }

        bool IsProgramOfStudyIdExistInQueryString { get; set; } 

        void SetControlsIfMultipleTests(bool IsMultipleTests);

        void RenderReport(IEnumerable<ResultsFromTheProgram> resultsFromInstitution, decimal norm, IEnumerable<string> testCharacteristics);
        
        void ExportReport();
    }
}
