using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IReportByCohortView : IReportView
    {
        bool IsProgramofStudyVisible { get; set; }

        void RenderReport(IEnumerable<TestByInstitutionResults> reportData);

        void GenerateReport(IEnumerable<TestByInstitutionResults> reportData, ReportAction printActions);
    }
}
