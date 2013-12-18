using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IReportCaseByCohortView : IReportView
    {
        bool IsProgramofStudyVisible { get; set; }

        void PopulateCases(IEnumerable<CaseStudy> cases);

        void PopulateModule(IEnumerable<Modules> module);

        void RenderReport(IEnumerable<CaseByCohortResult> reportData);
    
        void GenerateReport(IEnumerable<CaseByCohortResult> reportData, ReportAction printActions);
    }
}
