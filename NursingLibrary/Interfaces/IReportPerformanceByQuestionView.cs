using System.Collections.Generic;
using NursingLibrary.Entity;

namespace NursingLibrary.Interfaces
{
    public interface IReportPerformanceByQuestionView : IReportView
    {
        bool IsProgramofStudyVisible { get; set; }

        void RenderReport(IEnumerable<SummaryPerformanceByQuestionResult> reportData);

        void ExportReport(IEnumerable<SummaryPerformanceByQuestionResult> reportData);
    }
}
